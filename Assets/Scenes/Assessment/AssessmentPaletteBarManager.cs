using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using QuestionSystem;
using Edutesc.BioBlocks.Core.Models;

public class AssessmentPaletteBarManager : MonoBehaviour
{
    [Header("Container & Prefab")]
    [SerializeField] private RectTransform container;
    [SerializeField] private GameObject cellPrefab; // Opcional: se nulo, cria dinamicamente

    [Header("Level Colors")]
    [SerializeField] private Color basicColor = new Color(0.18f, 0.8f, 0.44f, 1f);        // #2ECC71 (Verde)
    [SerializeField] private Color intermediateColor = new Color(0.95f, 0.61f, 0.07f, 1f); // #F39C12 (Laranja)
    [SerializeField] private Color hardColor = new Color(0.61f, 0.35f, 0.71f, 1f);        // #9B59B6 (Roxo)

    [Header("Cell Layout Settings")]
    [SerializeField] private Vector2 cellSize = new Vector2(40f, 40f);
    [SerializeField] private float cellSpacing = 8f;

    private readonly List<CellItem> _cells = new List<CellItem>();
    public event Action<int> OnQuestionSelected;

    public IReadOnlyList<CellItem> Cells => _cells;

    public class CellItem
    {
        public int Index;
        public int Level;
        public Button Button;
        public Image Background;
        public Outline Outline;
        public TextMeshProUGUI NumberText;
        public RectTransform RectTransform;
        public GameObject Root;
    }

    private void Awake()
    {
        if (container == null)
        {
            container = GetComponent<RectTransform>();
        }
    }

    public void Initialize(AssessmentSession session)
    {
        if (session == null || session.Questions == null || session.Questions.Count == 0)
        {
            return;
        }

        ClearCells();
        SetupContainerLayout();

        for (int i = 0; i < session.Questions.Count; i++)
        {
            var question = session.Questions[i];
            var cell = CreateCell(i, question);
            _cells.Add(cell);
        }

        RefreshPalette();
    }

    private void SetupContainerLayout()
    {
        if (container == null) return;

        var layout = container.GetComponent<HorizontalLayoutGroup>();
        if (layout == null)
        {
            layout = container.gameObject.AddComponent<HorizontalLayoutGroup>();
        }

        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.spacing = cellSpacing;
        layout.childControlWidth = false;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = false;
        layout.childForceExpandHeight = false;
    }

    private CellItem CreateCell(int index, Question question)
    {
        GameObject cellObj;

        if (cellPrefab != null)
        {
            cellObj = Instantiate(cellPrefab, container);
        }
        else
        {
            cellObj = CreateProceduralCellObject(index);
        }

        cellObj.name = $"QuestionCell_{index + 1}";

        var rect = cellObj.GetComponent<RectTransform>();
        rect.sizeDelta = cellSize;

        var img = cellObj.GetComponent<Image>();
        var outline = cellObj.GetComponent<Outline>();
        if (outline == null)
        {
            outline = cellObj.AddComponent<Outline>();
        }

        var btn = cellObj.GetComponent<Button>();
        var tmp = cellObj.GetComponentInChildren<TextMeshProUGUI>();

        if (tmp != null)
        {
            tmp.text = (index + 1).ToString();
        }

        int targetIndex = index;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => OnQuestionSelected?.Invoke(targetIndex));

        return new CellItem
        {
            Index = index,
            Level = question.questionLevel,
            Button = btn,
            Background = img,
            Outline = outline,
            NumberText = tmp,
            RectTransform = rect,
            Root = cellObj
        };
    }

    private GameObject CreateProceduralCellObject(int index)
    {
        var cellObj = new GameObject($"Cell_{index + 1}", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button), typeof(Outline));
        cellObj.transform.SetParent(container, false);

        var img = cellObj.GetComponent<Image>();
        img.type = Image.Type.Simple;

        var textObj = new GameObject("NumberText", typeof(RectTransform), typeof(TextMeshProUGUI));
        textObj.transform.SetParent(cellObj.transform, false);

        var textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
        textRect.anchoredPosition = Vector2.zero;

        var tmp = textObj.GetComponent<TextMeshProUGUI>();
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 20f;
        tmp.fontStyle = FontStyles.Bold;

        return cellObj;
    }

    public void RefreshPalette()
    {
        var session = AssessmentSession.Current;
        if (session == null || _cells.Count == 0) return;

        for (int i = 0; i < _cells.Count; i++)
        {
            var cell = _cells[i];
            bool isCurrent = (cell.Index == session.CurrentQuestionIndex);
            int selectedAns = session.GetSelectedAnswer(cell.Index);
            bool isAnswered = (selectedAns >= 0);

            Color levelColor = GetColorForLevel(cell.Level);

            if (isAnswered)
            {
                // Estado: Respondida (Fundo preenchido com a cor do nível)
                if (cell.Background != null) cell.Background.color = levelColor;
                if (cell.NumberText != null) cell.NumberText.color = Color.white;
                if (cell.Outline != null)
                {
                    cell.Outline.effectColor = isCurrent ? Color.white : new Color(levelColor.r * 0.7f, levelColor.g * 0.7f, levelColor.b * 0.7f, 1f);
                    cell.Outline.effectDistance = isCurrent ? new Vector2(3f, -3f) : new Vector2(1.5f, -1.5f);
                }
            }
            else
            {
                // Estado: Não Respondida (Fundo branco/transparente, borda com a cor do nível)
                if (cell.Background != null) cell.Background.color = new Color(1f, 1f, 1f, 0.9f);
                if (cell.NumberText != null) cell.NumberText.color = levelColor;
                if (cell.Outline != null)
                {
                    cell.Outline.effectColor = isCurrent ? Color.black : levelColor;
                    cell.Outline.effectDistance = isCurrent ? new Vector2(3f, -3f) : new Vector2(2f, -2f);
                }
            }

            // Estado de Foco (Questão Atual)
            if (cell.RectTransform != null)
            {
                cell.RectTransform.localScale = isCurrent ? new Vector3(1.15f, 1.15f, 1f) : Vector3.one;
            }
        }
    }

    public Color GetColorForLevel(int level)
    {
        return level switch
        {
            1 => basicColor,
            2 => intermediateColor,
            >= 3 => hardColor,
            _ => basicColor
        };
    }

    private void ClearCells()
    {
        foreach (var cell in _cells)
        {
            if (cell.Root != null)
            {
                Destroy(cell.Root);
            }
        }
        _cells.Clear();
    }
}
