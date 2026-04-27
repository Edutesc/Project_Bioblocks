using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestionSystem;

// puxa informações através do QuestionManager para realizar funções relacionadas ao sistema de dicas
// verifica se a questão realmente faz parte do banco de dados, para não desbloquear dicas de questões erradas
// controle da UI
// ao errar uma questão, uma chave é criada referente á questão errada, essa chave valida a questão e verifica se a dica referente a questão 
// está disponível e salva no PlayerPrefs e no unlockedKeys
public class QuestionHintManager : MonoBehaviour
{
    // UI - botão que abre o painel de dica
    [Header("UI — Botão de dica")]
    [SerializeField] private Button openHintPanelButton;
    [SerializeField] private GameObject hintLockedIcon;
    [SerializeField] private GameObject hintUnlockedIcon;
    [SerializeField] private TextMeshProUGUI hintStatusText;

    // UI - painel de dicas
    [Header("Canal de dados")]
    [SerializeField] private HintChannelSO hintChannel;

    // Prefab - sistema modular que carrega diversos tipos de questões
    [Header("Prefabs de Dica")]
    [SerializeField] private GameObject textHintPrefab;
    [SerializeField] private GameObject imageHintPrefab;
    [SerializeField] private GameObject linkHintPrefab;

    private const string KeyPrefix = "HINTS_";
    private const string UserIdKey = "HINTS_ActiveUserId";
    private Question currentQuestion;
    private string activeDatabankName;
    private HashSet<string> unlockedKeys = new HashSet<string>();

    // faz verificações, atribui funções aos botões e inicia o painel fechado
    private void Awake()
    {
        ValidateComponents();
        if (openHintPanelButton != null)
        {
            openHintPanelButton.onClick.AddListener(OpenPanel);
        }
        // if (closeHintPanelButton != null)
        // {
        //     closeHintPanelButton.onClick.AddListener(ClosePanel);
        // }
        // ClosePanel();
    }

    // remove as funções dos botões
    private void OnDestroy()
    {
        if (openHintPanelButton != null)
        {
            openHintPanelButton.onClick.RemoveListener(OpenPanel);
        }
        // if (closeHintPanelButton != null)
        // {
        //     closeHintPanelButton.onClick.RemoveListener(ClosePanel);
        // }
    }

    // puxa o banco de dados ativo e a lista de questões para puxar as dicas já desbloqueadas
    public void Initialize(string databankName, List<Question> allQuestions, string userId)
    {
        activeDatabankName = databankName;
        // passa userId
        LoadCacheFromPlayerPrefs(allQuestions, userId);
        Debug.Log($"Hint Manager - Banco atual: {databankName} | {unlockedKeys.Count} dica(s) existentes.");
    }

    // o QuestionManager avisa sempre que há uma troca na questão que está sendo exibida
    public void OnQuestionChanged(Question question)
    {
        currentQuestion = question;
        // ClosePanel();
        RefreshButtonUI();
    }

    // desbloqueia as dicas de forma permanente, verifica o banco de dados para nao desbloquear dicas em outros bancos de dados
    public void OnAnswerWrong(Question question)
    {
        // garante que a questão errada pertence ao banco atualmente carregado
        if (question.questionDatabankName != activeDatabankName)
        {
            Debug.Log($"Hint Manager - Banco atual: {activeDatabankName}, " + $"banco da questão exibida: {question.questionDatabankName}.");
            return;
        }

        // quando erra a questão, ele cria um codigo referente a dica
        string key = BuildKey(question.questionDatabankName, question.questionNumber);

        // verifica se esse codigo existe, caso exista ele nao é salvo
        if (unlockedKeys.Contains(key))
        {
            return;
        }

        // popula a unlockedKeys variavel (uma lista) e salva no PlayerPrefs
        unlockedKeys.Add(key);
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();

        Debug.Log($"Hint Manager - Desbloqueado: {key}");

        // se a questão errada é a que está na tela, atualiza a UI imediatamente
        if (currentQuestion != null && BuildKey(currentQuestion.questionDatabankName, currentQuestion.questionNumber) == key)
        {
            RefreshButtonUI();
        }
    }

    // controle de UI
    private void RefreshButtonUI()
    {
        bool hasHints;
        bool canOpen;

        if (currentQuestion == null)
        {
            return;
        }

        // se a questao nao tiver dicas e o numero de dicas for maior que 0, mostra o botao
        if (currentQuestion.hint != null && currentQuestion.hint.Count > 0)
        {
            hasHints = true;
        }
        else
        {
            hasHints = false;
        }

        bool isUnlocked = IsCurrentUnlocked();

        // se a questao tem dica disponivel, e a dica já foi desbloeada, pode abrir o painel de dica
        if (hasHints && isUnlocked)
        {
            canOpen = true;
        }
        else
        {
            canOpen = false;
        }

        // mostra apenas se a questão tiver dicas cadastradas
        if (openHintPanelButton != null)
        {
            openHintPanelButton.gameObject.SetActive(hasHints);
            openHintPanelButton.interactable = canOpen;
        }

        // muda o icone do cadeado
        if (hintLockedIcon != null)
        {
            hintLockedIcon.SetActive(hasHints && !isUnlocked);
        }
        if (hintUnlockedIcon != null)
        {
            hintUnlockedIcon.SetActive(hasHints && isUnlocked);
        }

        // texto no botão sobre quantas dicas faltam
        if (hintStatusText != null)
        {
            if (!hasHints)
            {
                hintStatusText.text = string.Empty;
            }
            else if (!isUnlocked)
            {
                hintStatusText.text = "Erre para desbloquear";
            }
            else
            {
                hintStatusText.text = "Dica disponível";
            }
        }
    }

    // logica para abrir o painel de dica
    private void OpenPanel()
    {
        if (currentQuestion == null || !IsCurrentUnlocked()) return;
        if (currentQuestion.hint == null || currentQuestion.hint.Count == 0) return;

        // Publica os dados ANTES de abrir a scene
        hintChannel.Publish(
            $"Dicas — Questão {currentQuestion.questionNumber}",
            currentQuestion.hint
        );

        // Abre a modal scene por cima (Additive)
        UnityEngine.SceneManagement.SceneManager
            .LoadScene("QuestionModalScene",
                       UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    // fecha o painel e limpa o conteuda do painel (destoi os prefabs)
    // private void ClosePanel()
    // {
    //     if (hintPanel != null)
    //     {
    //         hintPanel.SetActive(false);
    //     }
    //     ClearPanelContent();
    // }

    // é passado uma lista com todas as hints referente ao banco de dado ativo, ele limpa o conteudo atual, e com um foreach
    // vai delegando cada caso (TextHint, ImageHint, LinkHint) para cada metodo com o objetivo de spawnar os prefabs
    // private void PopulatePanel(List<Hint> hints)
    // {
    //     ClearPanelContent();
    //     foreach (var hint in hints)
    //     {
    //         switch (hint)
    //         {
    //             case TextHint th: SpawnTextHint(th); break;
    //             case ImageHint ih: SpawnImageHint(ih); break;
    //             case LinkHint lh: SpawnLinkHint(lh); break;
    //         }
    //     }
    // }

    // se o painel nao exibir nada, nao faz nada, se tiver conteudo, passa com um foreach em cada elemento e destroi eles 
    // private void ClearPanelContent()
    // {
    //     if (hintContentParent == null)
    //     {
    //         return;
    //     }

    //     // passa por cada prefab dentro do hintContent (Scroll View) e destroi eles
    //     foreach (Transform child in hintContentParent)
    //     {
    //         Destroy(child.gameObject);
    //     }
    // }

    // // instanciar prefabs no hintContent (Scroll View)
    // // instancia o prefab, pega o textMesh e coloca o texto da dica nele
    // private void SpawnTextHint(TextHint hint)
    // {
    //     if (textHintPrefab == null)
    //     {
    //         return;
    //     }
    //     var gameObj = Instantiate(textHintPrefab, hintContentParent);
    //     var textMesh = gameObj.GetComponentInChildren<TextMeshProUGUI>();
    //     if (textMesh != null)
    //     {
    //         textMesh.text = hint.text;
    //     }
    // }

    // // instancia o prefab, pega o componente de imagem dele e coloca o path da imagem da dica nele
    // private void SpawnImageHint(ImageHint hint)
    // {
    //     if (imageHintPrefab == null)
    //     {
    //         return;
    //     }
    //     var gameObj = Instantiate(imageHintPrefab, hintContentParent);
    //     var img = gameObj.GetComponentInChildren<Image>();
    //     if (img != null)
    //     {
    //         img.sprite = Resources.Load<Sprite>(hint.imagePath);
    //     }
    // }

    // // instancia, pega o componente de texto e o botao, altera o texto e adiciona a função de OpenURL no botão, e puxa o link da dica
    // private void SpawnLinkHint(LinkHint hint)
    // {
    //     if (linkHintPrefab == null)
    //     {
    //         return;
    //     }
    //     var gameObj = Instantiate(linkHintPrefab, hintContentParent);
    //     var textMesh = gameObj.GetComponentInChildren<TextMeshProUGUI>();
    //     var btn = gameObj.GetComponentInChildren<Button>();
    //     if (textMesh != null)
    //     {
    //         textMesh.text = hint.link;
    //     }
    //     if (btn != null)
    //     {
    //         string link = hint.link;

    //         btn.onClick.AddListener(OpenLink);

    //         void OpenLink()
    //         {
    //             Application.OpenURL(link);
    //         }
    //     }
    // }

    // verifica se a questao é nula e verifica se a chave gerada existe no unlockedKeys
    private bool IsCurrentUnlocked()
    {
        if (currentQuestion == null)
        {
            return false;
        }
        return unlockedKeys.Contains(BuildKey(currentQuestion.questionDatabankName, currentQuestion.questionNumber));
    }

    // builda uma key usando o prefixo (variavel la no inicio), o nome do banco de dados e o numero da questao
    private static string BuildKey(string databankName, int questionNumber)
    {
        return $"{KeyPrefix}{databankName}_{questionNumber}";
    }

    // limpa as dicas registradas e verifica, por meio do playerPrefs, ao comparar com as questões, se ja tem dica registrada
    // sobre ela
    private void LoadCacheFromPlayerPrefs(List<Question> questions, string userId)
    {
        unlockedKeys.Clear();

        string savedUserId = PlayerPrefs.GetString(UserIdKey, string.Empty);

        // se for usuarios diferente, apaga as dicas e reseta
        if (savedUserId != userId)
        {
            Debug.Log($"Hint Manager - Usuário mudou ({savedUserId} → {userId}). Resetando dicas.");

            foreach (var q in questions)
            {
                string key = BuildKey(q.questionDatabankName, q.questionNumber);
                PlayerPrefs.DeleteKey(key);
            }

            PlayerPrefs.SetString(UserIdKey, userId);
            PlayerPrefs.Save();
            // unlockedKeys permanece vazio — nenhuma dica desbloqueada
            return;
        }

        // se for o mesmo usuario, so roda normalmente
        foreach (var q in questions)
        {
            string key = BuildKey(q.questionDatabankName, q.questionNumber);
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                unlockedKeys.Add(key);
            }
        }
    }

    // valida os componentes 
    private void ValidateComponents()
    {
        // if (hintPanel == null)
        //     Debug.LogError("Hint Manager - hintPanel não atribuído no Inspector.");
        // if (hintContentParent == null)
        //     Debug.LogError("Hint Manager - hintContentParent não atribuído no Inspector.");
        if (openHintPanelButton == null)
            Debug.LogWarning("Hint Manager - openHintPanelButton não atribuído.");
    }
    // default para carregar imagens (preciso puxar de algum banco de dados)
}
