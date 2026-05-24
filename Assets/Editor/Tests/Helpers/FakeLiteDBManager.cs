using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;

/// <summary>
/// Fake do ILiteDBManager para testes unitários.
/// Opera inteiramente em memória — sem arquivo em disco.
/// Descartado automaticamente ao fim de cada teste via Close().
///
/// Como usar:
///   var db = new FakeLiteDBManager();
///   var repo = new QuestionLocalRepository();
///   repo.InjectDependencies(db);
/// </summary>
public class FakeLiteDBManager : ILiteDBManager
{
    private LiteDatabase _db;
    private MemoryStream _stream;

    private readonly BsonMapper _mapper;
    private readonly SemaphoreSlim _dbGate = new SemaphoreSlim(1, 1);

    public FakeLiteDBManager()
    {
        _mapper = CreateMapper();
        Initialize();
    }

    public bool IsInitialized { get; private set; }

    // ── Acesso direto ao banco ────────────────────────────────────────────────

    public LiteDatabase Database
    {
        get
        {
            if (_db == null)
                throw new Exception("[FakeLiteDBManager] Banco não inicializado.");

            return _db;
        }
    }

    // ── Collections existentes ────────────────────────────────────────────────

    public ILiteCollection<UserDataDB> Users
        => Database.GetCollection<UserDataDB>("users");

    public ILiteCollection<CachedImageDB> CachedImages
        => Database.GetCollection<CachedImageDB>("cached_images");

    public ILiteCollection<RankingDB> Rankings
        => Database.GetCollection<RankingDB>("rankings");

    public ILiteCollection<QuestionDB> Questions
        => Database.GetCollection<QuestionDB>("questions");

    // ── Inicialização ─────────────────────────────────────────────────────────

    public void Initialize()
    {
        if (IsInitialized && _db != null)
            return;

        _stream = new MemoryStream();
        _db = new LiteDatabase(_stream, _mapper);

        EnsureIndexes();

        IsInitialized = true;
    }

    // ── Execução protegida: async ─────────────────────────────────────────────

    public async Task ExecuteWriteAsync(
        Action<LiteDatabase> action,
        CancellationToken ct = default)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        await _dbGate.WaitAsync(ct);

        try
        {
            EnsureInitialized();
            action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    public async Task<T> ExecuteReadAsync<T>(
        Func<LiteDatabase, T> action,
        CancellationToken ct = default)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        await _dbGate.WaitAsync(ct);

        try
        {
            EnsureInitialized();
            return action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    // ── Execução protegida: síncrona ──────────────────────────────────────────

    public void ExecuteWrite(Action<LiteDatabase> action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        _dbGate.Wait();

        try
        {
            EnsureInitialized();
            action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    public T ExecuteRead<T>(Func<LiteDatabase, T> action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));

        _dbGate.Wait();

        try
        {
            EnsureInitialized();
            return action(Database);
        }
        finally
        {
            _dbGate.Release();
        }
    }

    // ── Fechamento ────────────────────────────────────────────────────────────

    public void Close()
    {
        _dbGate.Wait();

        try
        {
            _db?.Dispose();
            _db = null;

            _stream?.Dispose();
            _stream = null;

            IsInitialized = false;
        }
        finally
        {
            _dbGate.Release();
        }
    }

    // ── Utilitários ───────────────────────────────────────────────────────────

    private void EnsureInitialized()
    {
        if (!IsInitialized || _db == null)
            throw new InvalidOperationException("[FakeLiteDBManager] Banco não inicializado.");
    }

    private static BsonMapper CreateMapper()
    {
        var mapper = new BsonMapper();

        mapper.ResolveMember += (type, memberInfo, memberMapper) =>
        {
            if (memberMapper.DataType == typeof(DateTime))
            {
                memberMapper.Serialize = (obj, m) =>
                    new BsonValue(((DateTime)obj).ToUniversalTime());

                memberMapper.Deserialize = (val, m) =>
                    DateTime.SpecifyKind(val.AsDateTime, DateTimeKind.Utc);
            }
        };

        return mapper;
    }

    private void EnsureIndexes()
    {
        Users.EnsureIndex(x => x.UserId, unique: true);

        CachedImages.EnsureIndex(x => x.ImageUrl, unique: true);
        CachedImages.EnsureIndex(x => x.Topic);

        Rankings.EnsureIndex(x => x.Score);
        Rankings.EnsureIndex(x => x.WeekScore);

        Questions.EnsureIndex(x => x.QuestionDatabankName);
        Questions.EnsureIndex(x => x.Topic);
        Questions.EnsureIndex(x => x.CachedAt);
    }
}