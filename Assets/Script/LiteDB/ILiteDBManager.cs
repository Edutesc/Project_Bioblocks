using System;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;

public interface ILiteDBManager
{
    bool IsInitialized { get; }

    LiteDatabase Database { get; }

    ILiteCollection<UserDataDB>    Users        { get; }
    ILiteCollection<RankingDB>     Rankings     { get; }
    ILiteCollection<CachedImageDB> CachedImages { get; }
    ILiteCollection<QuestionDB>    Questions    { get; }

    void Initialize();

    Task ExecuteWriteAsync(Action<LiteDatabase> action, CancellationToken ct = default);
    Task<T> ExecuteReadAsync<T>(Func<LiteDatabase, T> action, CancellationToken ct = default);

    void ExecuteWrite(Action<LiteDatabase> action);
    T ExecuteRead<T>(Func<LiteDatabase, T> action);

    void Close();
}