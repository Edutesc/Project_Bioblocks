using UnityEngine;
using System.Collections.Generic;
using QuestionSystem;

public class AcidBaseBufferQuestionDatabase : MonoBehaviour, IQuestionDatabase
{

    [Header("Development Settings")]
    [SerializeField] private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Segundo Arrhenius, o que caracteriza um ácido?",
            answers = new string[] {
                "Libera íons H+ em solução aquosa.",
                "Recebe prótons (H+) em solução aquosa.",
                "Libera íons OH- em solução aquosa.",
                "Recebe íons OH- em solução aquosa."
            },
            correctIndex = 0,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 1,
                    text = "A teoria de Arrhenius define ácidos pelo que eles LIBERAM em água. " +
                           "Não pelo que recebem. Foque nos íons de hidrogênio (H⁺) — são eles " +
                           "que caracterizam um ácido de Arrhenius em solução aquosa."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Segundo Arrhenius, o que caracteriza uma base?",
            answers = new string[] {
                "Libera íons H+ em solução aquosa.",
                "Recebe prótons (H+) em solução aquosa.",
                "Libera íons OH- em solução aquosa.",
                "Recebe íons OH- em solução aquosa."
            },
            correctIndex = 2,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 2,
                    text = "Na teoria de Arrhenius, ácido LIBERA H⁺ e base LIBERA OH⁻. " +
                           "Ambos são definidos pelo que LIBERAM em água — o que os diferencia " +
                           "é qual íon cada um disponibiliza na solução aquosa."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "De acordo com Brønsted-Lowry, o que é um ácido?",
            answers = new string[] {
                "Doador de prótons (H+).",
                "Receptor de prótons (H+).",
                "Doador de íons OH-. ",
                "Receptor de íons OH-."
            },
            correctIndex = 0,
            questionNumber = 3,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 3,
                    text = "Brønsted-Lowry amplia Arrhenius: em vez de \"liberar H⁺\", " +
                           "o ácido é definido como aquele que DOTA (doa) prótons à outra espécie. " +
                           "Quem doa é o ácido; quem recebe é a base."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "De acordo com Brønsted-Lowry, o que é uma base?",
            answers = new string[] {
                "Doador de prótons (H+).",
                "Receptor de prótons (H+).",
                "Doador de íons OH-. ",
                "Receptor de íons OH-."
            },
            correctIndex = 1,
            questionNumber = 4,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 4,
                    text = "Em Brønsted-Lowry, a reação ácido-base é sempre uma transferência de próton. " +
                           "Se o ácido DOA o H⁺, a base necessariamente deve fazer o quê com esse próton " +
                           "— doá-lo também ou aceitá-lo?"
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A água pode atuar como:",
            answers = new string[] {
                "Apenas ácido.",
                "Apenas base.",
                "Tanto ácido quanto base.",
                "Nem ácido nem base."
            },
            correctIndex = 2,
            questionNumber = 5,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 5,
                    text = "A água é anfiprótica — pode DOAR um próton (agindo como ácido) " +
                           "ou RECEBER um próton (agindo como base), dependendo com quem reage. " +
                           "Essa característica é chamada de anfotérica, e é por isso que a água " +
                           "se autoioniza: H₂O + H₂O ⇌ H₃O⁺ + OH⁻."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O que é a base conjugada do HCl?",
            answers = new string[] {
                "H<sup><size=150%> +</size></sup>",
                "Cl<sup><size=150%> -</size></sup>",
                "H<sub><size=150%>2</size></sub> O",
                "OH<sup><size=150%> -</size></sup>"
            },
            correctIndex = 1,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 6,
                    text = "A base conjugada é o que SOBRA do ácido após ele DOAR um próton (H⁺). " +
                           "HCl doa H⁺ → o que resta do HCl depois dessa doação? " +
                           "Subtraia mentalmente um H⁺ da fórmula do HCl."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O que é o ácido conjugado da NH<sub><size=150%>3</size></sub>?",
            answers = new string[] {
                "H<sup><size=150%> +</size></sup>",
                "OH<sup><size=150%> -</size></sup>",
                "NH<sub><size=150%>4</size></sub><sup><size=150%> +</size></sup>",
                "NH<sub><size=150%>2</size></sub><sup><size=150%> -</size></sup>"
            },
            correctIndex = 2,
            questionNumber = 7,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 7,
                    text = "O ácido conjugado é formado quando a BASE RECEBE um próton (H⁺). " +
                           "NH₃ recebe H⁺ → some H⁺ à fórmula do NH₃. " +
                           "NH₃ + H⁺ = ? (lembre-se de ajustar a carga também)."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Um ácido forte em solução aquosa:",
            answers = new string[] {
                "Se dissocia parcialmente.",
                "Se dissocia completamente.",
                "Não se dissocia.",
                "Forma ligações de hidrogênio."
            },
            correctIndex = 1,
            questionNumber = 8,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 8,
                    text = "A \"força\" de um ácido reflete o quanto ele se ioniza em água. " +
                           "Um ácido FORTE tem Ka muito elevado, o que significa que o equilíbrio " +
                           "pende totalmente para os produtos — praticamente todas as moléculas cedem H⁺."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Um ácido fraco em solução aquosa:",
            answers = new string[] {
                "Se dissocia completamente.",
                "Se dissocia parcialmente.",
                "Não se dissocia.",
                "Forma ligações iônicas."
            },
            correctIndex = 1,
            questionNumber = 9,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 9,
                    text = "Um ácido fraco tem Ka pequeno: o equilíbrio favorece os REAGENTES, " +
                           "ou seja, a maioria das moléculas permanece intacta em solução. " +
                           "Apenas uma fração delas cede H⁺ — por isso a dissociação é parcial."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A constante de equilíbrio (Keq) de uma reação indica:",
            answers = new string[] {
                "A velocidade da reação.",
                "A proporção de reagentes e produtos no equilíbrio.",
                "A energia de ativação da reação.",
                "A concentração dos reagentes."
            },
            correctIndex = 1,
            questionNumber = 10,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 10,
                    text = "Keq NÃO informa velocidade nem energia de ativação — isso é domínio da cinética. " +
                           "Keq é uma razão calculada com as concentrações de produtos e reagentes " +
                           "quando a reação atinge o equilíbrio: Keq = [produtos] / [reagentes]."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Em uma reação em equilíbrio, se Keq > 1:",
            answers = new string[] {
                "Os reagentes são favorecidos.",
                "Os produtos são favorecidos.",
                "Os reagentes e produtos têm concentrações iguais.",
                "A reação é irreversível."
            },
            correctIndex = 1,
            questionNumber = 11,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 11,
                    text = "Keq = [produtos] / [reagentes]. Se Keq > 1, o numerador (produtos) é " +
                           "MAIOR que o denominador (reagentes) no equilíbrio. " +
                           "Logo, há mais produtos que reagentes — os produtos são favorecidos."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Em uma reação em equilíbrio, se Keq < 1:",
            answers = new string[] {
                "Os produtos são favorecidos.",
                "Os reagentes são favorecidos.",
                "Os reagentes e produtos têm concentrações iguais.",
                "A reação é irreversível."
            },
            correctIndex = 1,
            questionNumber = 12,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 12,
                    text = "Se Keq < 1, o numerador (produtos) é MENOR que o denominador (reagentes). " +
                           "Isso indica que no equilíbrio predominam os reagentes — " +
                           "a reação não avança muito em direção aos produtos."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A constante de dissociação ácida (Ka) mede:",
            answers = new string[] {
                "A força de uma base.",
                "A força de um ácido.",
                "A velocidade de uma reação.",
                "O equilíbrio de uma reação."
            },
            correctIndex = 1,
            questionNumber = 13,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 13,
                    text = "Ka é o Keq específico para a ionização de um ácido em água. " +
                           "O subscrito \"a\" vem de \"acid\" (ácido). " +
                           "Quanto maior o Ka, mais o ácido se ioniza — portanto, mais forte ele é."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Um ácido forte tem um valor de Ka:",
            answers = new string[] {
                "Baixo",
                "Alto",
                "Próximo a 1",
                "Próximo a 0"
            },
            correctIndex = 1,
            questionNumber = 14,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 14,
                    text = "Ka alto significa que o numerador ([H⁺][A⁻]) é muito maior que o denominador ([HA]) — " +
                           "ou seja, a ionização é extensa. Ácidos fortes como HCl têm Ka tão elevado " +
                           "que se considera a dissociação completa."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Um ácido fraco tem um valor de Ka:",
            answers = new string[] {
                "Alto",
                "Baixo",
                "Próximo a 1",
                "Próximo a 0"
            },
            correctIndex = 1,
            questionNumber = 15,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 15,
                    text = "Ka baixo indica que a maioria das moléculas do ácido NÃO se ioniza em água — " +
                           "o denominador ([HA]) permanece grande em relação ao numerador. " +
                           "O ácido acético (CH₃COOH), por exemplo, tem Ka ≈ 1,8 × 10⁻⁵."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O pKa de um ácido é definido como:",
            answers = new string[] {
                "log Ka",
                "-log Ka",
                "1/Ka",
                "10/Ka"
            },
            correctIndex = 1,
            questionNumber = 16,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 16,
                    text = "O prefixo \"p\" em química significa SEMPRE \"-log\" (logaritmo negativo na base 10). " +
                           "Assim como pH = -log[H⁺] e pOH = -log[OH⁻], " +
                           "pKa = -log(Ka). O sinal negativo inverte a escala: Ka alto → pKa baixo."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Um ácido com um pKa baixo é:",
            answers = new string[] {
                "Fraco",
                "Forte",
                "De força moderada",
                "Indeterminado"
            },
            correctIndex = 1,
            questionNumber = 17,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 17,
                    text = "pKa = -log(Ka). Se pKa é baixo, então Ka é ALTO (relação inversa pelo sinal negativo). " +
                           "Ka alto → maior ionização → ácido mais forte. " +
                           "Exemplo: HCl tem pKa ≈ -7 (muito ácido); ácido acético tem pKa ≈ 4,75 (ácido fraco)."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Um ácido com um pKa alto é:",
            answers = new string[] {
                "Forte",
                "Fraco",
                "De força moderada",
                "Indeterminado"
            },
            correctIndex = 1,
            questionNumber = 18,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 18,
                    text = "pKa alto → Ka baixo (lembre: pKa = -log Ka, relação inversa). " +
                           "Ka baixo significa pouca ionização em água → ácido fraco. " +
                           "Quanto maior o pKa, mais fraco é o ácido."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A equação de Henderson-Hasselbalch relaciona:",
            answers = new string[] {
                "pH, pKa e a razão entre base conjugada e ácido.",
                "pH, pKa e a concentração de íons H+",
                "pH, pOH e a concentração de íons OH-",
                "pKa, pKb e a concentração de íons H+"
            },
            correctIndex = 0,
            questionNumber = 19,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 19,
                    text = "A equação de Henderson-Hasselbalch é: pH = pKa + log([A⁻]/[HA]). " +
                           "Ela conecta três grandezas: o pH da solução, o pKa do ácido fraco, " +
                           "e a RAZÃO entre a concentração da base conjugada [A⁻] e do ácido [HA]."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Em uma solução-tampão, o pH permanece relativamente constante porque:",
            answers = new string[] {
                "O ácido se dissocia completamente.",
                "A base se dissocia completamente.",
                "Há um equilíbrio entre ácido e sua base conjugada.",
                "Não há interações entre o ácido e a base."
            },
            correctIndex = 2,
            questionNumber = 20,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 20,
                    text = "O tampão funciona como um \"sistema de defesa\" do pH: " +
                           "se H⁺ é adicionado, a base conjugada o absorve; " +
                           "se OH⁻ é adicionado, o ácido fraco o neutraliza. " +
                           "Isso só é possível graças ao equilíbrio dinâmico entre o ácido e sua base conjugada."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A faixa de tamponamento de uma solução-tampão é:",
            answers = new string[] {
                "Muito menor que o pKa.",
                "Igual ao pKa.",
                "Aproximadamente ± 1 unidade de pH em relação ao pKa.",
                "Muito maior que o pKa."
            },
            correctIndex = 2,
            questionNumber = 21,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 21,
                    text = "Pela equação de Henderson-Hasselbalch, o tampão é eficiente enquanto " +
                           "a razão [A⁻]/[HA] está entre 0,1 e 10 (ou seja, log entre -1 e +1). " +
                           "Isso define uma faixa de pH = pKa ± 1 unidade ao redor do pKa."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O pH do sangue é mantido constante principalmente pelo sistema tampão:",
            answers = new string[] {
                "Fosfato",
                "Acetato",
                "Bicarbonato",
                "Tris"
            },
            correctIndex = 2,
            questionNumber = 22,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 22,
                    text = "O sistema tampão do sangue usa o par H₂CO₃ / HCO₃⁻. " +
                           "O CO₂ produzido no metabolismo se dissolve em água formando H₂CO₃, " +
                           "que se converte em HCO₃⁻ (bicarbonato). Esse par mantém o pH sanguíneo em ~7,4."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O que acontece com o pH do sangue durante o exercício intenso?",
            answers = new string[] {
                "Aumenta.",
                "Diminui.",
                "Permanece constante.",
                "Varia de forma imprevisível."
            },
            correctIndex = 1,
            questionNumber = 23,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 23,
                    text = "Durante exercício intenso, os músculos produzem ácido lático (lactato + H⁺). " +
                           "Esse aumento de H⁺ no sangue significa maior acidez — " +
                           "e maior acidez corresponde a pH mais BAIXO na escala de pH."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Como o corpo responde à diminuição do pH sangüíneo durante o exercício?",
            answers = new string[] {
                "Diminui a taxa respiratória.",
                "Aumenta a taxa respiratória.",
                "Mantém a taxa respiratória constante.",
                "Para de respirar."
            },
            correctIndex = 1,
            questionNumber = 24,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 24,
                    text = "O excesso de H⁺ no sangue converte-se em H₂CO₃, que se decompõe em CO₂ + H₂O. " +
                           "Ao expirar mais CO₂, o corpo REMOVE ácido do sangue, elevando o pH de volta. " +
                           "Por isso a respiração ACELERA durante o exercício — é uma compensação respiratória."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O que é pH?",
            answers = new string[] {
                "Uma medida da concentração de OH-",
                "Uma medida da concentração de H+",
                "Uma medida da temperatura",
                "Uma medida da pressão"
            },
            correctIndex = 1,
            questionNumber = 25,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 25,
                    text = "\"pH\" vem do latim/alemão \"potentia Hydrogenii\" — potência do hidrogênio. " +
                           "É definido como pH = -log[H⁺]. Mede indiretamente a concentração de prótons " +
                           "(íons H⁺) em solução — não OH⁻, não temperatura, não pressão."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com pH 3 é:",
            answers = new string[] {
                "Neutra",
                "Básica",
                "Ácida",
                "Tampão"
            },
            correctIndex = 2,
            questionNumber = 26,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 26,
                    text = "Na escala de pH: < 7 = ácido, = 7 = neutro, > 7 = básico. " +
                           "pH 3 está bem abaixo de 7, próximo ao extremo ácido da escala. " +
                           "Para comparar: suco de limão tem pH ~2; café tem pH ~5."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com pH 11 é:",
            answers = new string[] {
                "Ácida",
                "Neutra",
                "Básica",
                "Tampão"
            },
            correctIndex = 2,
            questionNumber = 27,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 27,
                    text = "pH 11 está bem acima de 7 — próximo ao extremo básico da escala. " +
                           "Para comparar: leite de magnésia tem pH ~10; água sanitária tem pH ~12. " +
                           "Soluções com pH > 7 são básicas (alcalinas)."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com pH 7 é:",
            answers = new string[] {
                "Ácida",
                "Neutra",
                "Básica",
                "Tampão"
            },
            correctIndex = 1,
            questionNumber = 28,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 28,
                    text = "pH 7 é o ponto central da escala — é o pH onde [H⁺] = [OH⁻] = 10⁻⁷ M. " +
                           "Isso define a NEUTRALIDADE química. A água pura a 25°C possui pH exatamente 7."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O pOH de uma solução é uma medida de:",
            answers = new string[] {
                "Concentração de H+",
                "Concentração de OH-",
                "Acidez",
                "Basicidade"
            },
            correctIndex = 1,
            questionNumber = 29,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 29,
                    text = "Assim como pH mede H⁺ (íon hidrogênio), pOH mede OH⁻ (íon hidroxila). " +
                           "pOH = -log[OH⁻]. A letra \"O\" no pOH faz referência ao íon hidroxila (OH⁻), " +
                           "não ao íon H⁺."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A relação entre pH e pOH é:",
            answers = new string[] {
                "pH + pOH = 0",
                "pH + pOH = 7",
                "pH + pOH = 14",
                "pH + pOH = 21"
            },
            correctIndex = 2,
            questionNumber = 30,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 30,
                    text = "Essa relação deriva do produto iônico da água: Kw = [H⁺][OH⁻] = 10⁻¹⁴. " +
                           "Aplicando -log em ambos os lados: -log(Kw) = -log[H⁺] + (-log[OH⁻]) " +
                           "→ 14 = pH + pOH. Em qualquer solução aquosa a 25°C, pH + pOH = 14."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual o produto iônico da água (Kw) a 25<sup><size=100%>o</size></sup> C?",
            answers = new string[] {
                "10<sup><size=150%>-7</size></sup> ",
                "10<sup><size=150%>-14</size></sup> ",
                "10<sup><size=150%>0</size></sup> ",
                "10<sup><size=150%>14</size></sup> "
            },
            correctIndex = 1,
            questionNumber = 31,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 31,
                    text = "Kw = [H⁺] × [OH⁻]. Na água pura, [H⁺] = [OH⁻] = 10⁻⁷ M. " +
                           "Portanto Kw = 10⁻⁷ × 10⁻⁷ = 10⁻¹⁴. " +
                           "Cuidado: 10⁻⁷ é a concentração de CADA íon, não o produto deles."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Em água pura, a concentração de H<sup><size=150%>+</size></sup> é:",
            answers = new string[] {
                "10<sup><size=150%>-14</size></sup> M",
                "10<sup><size=150%>-7</size></sup> M",
                "10<sup><size=150%>0</size></sup> M",
                "10<sup><size=150%>7</size></sup> M"
            },
            correctIndex = 1,
            questionNumber = 32,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 32,
                    text = "A água pura tem pH = 7. Usando a definição: pH = -log[H⁺], " +
                           "podemos calcular [H⁺]: 7 = -log[H⁺] → [H⁺] = 10⁻⁷ M. " +
                           "O valor 10⁻¹⁴ é o Kw (produto de H⁺ × OH⁻), não a concentração de cada um."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Em água pura, a concentração de OH<sup><size=150%>-</size></sup>  é:",
            answers = new string[] {
                "10<sup><size=150%>-14</size></sup> M",
                "10<sup><size=150%>-7</size></sup> M",
                "10<sup><size=150%>0</size></sup> M",
                "10<sup><size=150%>7</size></sup> M"
            },
            correctIndex = 1,
            questionNumber = 33,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 33,
                    text = "Em água pura, [H⁺] = [OH⁻] por definição de solução neutra. " +
                           "Como [H⁺] = 10⁻⁷ M (derivado do pH = 7), então [OH⁻] também é 10⁻⁷ M. " +
                           "Confirmação: [H⁺] × [OH⁻] = 10⁻⁷ × 10⁻⁷ = 10⁻¹⁴ = Kw. ✓"
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual a fórmula para calcular o pH?",
            answers = new string[] {
                "pH = log[H<sup><size=150%>+</size></sup>]",
                "pH = -log[H<sup><size=150%>+</size></sup>]",
                "pH = log[OH<sup><size=150%>-</size></sup>]",
                "pH = -log[OH<sup><size=150%>-</size></sup>]"
            },
            correctIndex = 1,
            questionNumber = 34,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 34,
                    text = "O sinal negativo é essencial: como [H⁺] é um número muito pequeno (ex: 10⁻⁷), " +
                           "seu log seria negativo (-7). O sinal \"-\" transforma esse valor em positivo (+7), " +
                           "resultando na escala de pH familiar (0 a 14) com valores positivos."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual a fórmula para calcular o pOH?",
            answers = new string[] {
                "pOH = -log[OH<sup><size=150%>-</size></sup>]",
                "pOH = log[OH<sup><size=150%>-</size></sup>]",
                "pOH = -log[OH<sup><size=150%>+</size></sup>]",
                "pOH = log[OH<sup><size=150%>+</size></sup>]"
            },
            correctIndex = 0,
            questionNumber = 35,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 35,
                    text = "pOH segue a mesma lógica do pH: \"p\" sempre significa \"-log\". " +
                           "E pOH mede OH⁻ (não OH⁺, que não existe). " +
                           "Portanto: pOH = -log[OH⁻]. Atenção ao sinal e ao íon correto."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual o valor mínimo de pH possível?",
            answers = new string[] {
                "0",
                "7",
                "14",
                "-14"
            },
            correctIndex = 0,
            questionNumber = 36,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 36,
                    text = "A escala convencional de pH vai de 0 a 14 (embora valores negativos sejam " +
                           "possíveis em ácidos extremamente concentrados). No contexto padrão, " +
                           "o valor MÍNIMO é 0, que corresponde a [H⁺] = 1 mol/L — solução extremamente ácida."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual o valor máximo de pH possível?",
            answers = new string[] {
                "0",
                "7",
                "14",
                "-14"
            },
            correctIndex = 2,
            questionNumber = 37,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 37,
                    text = "A escala convencional de pH vai de 0 a 14. O valor MÁXIMO é 14, " +
                           "que corresponde a [H⁺] = 10⁻¹⁴ mol/L (ou [OH⁻] = 1 mol/L) — " +
                           "solução extremamente básica, como NaOH 1 mol/L."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual o pH de uma solução neutra?",
            answers = new string[] {
                "0",
                "7",
                "14",
                "Variavel"
            },
            correctIndex = 1,
            questionNumber = 38,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 38,
                    text = "Uma solução neutra é aquela em que [H⁺] = [OH⁻]. " +
                           "Como em água pura [H⁺] = [OH⁻] = 10⁻⁷ M, o pH = -log(10⁻⁷) = 7. " +
                           "O pH 7 é o ponto de equilíbrio exato entre acidez e basicidade a 25°C."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com pH abaixo de 7 é:",
            answers = new string[] {
                "Neutra",
                "Básica",
                "Ácida",
                "Tampão"
            },
            correctIndex = 2,
            questionNumber = 39,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 39,
                    text = "pH < 7 significa [H⁺] > 10⁻⁷ M — há mais prótons do que em água pura. " +
                           "Mais H⁺ = mais ácida. A regra é simples: pH menor que 7 → ácida; " +
                           "pH igual a 7 → neutra; pH maior que 7 → básica."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com pH acima de 7 é:",
            answers = new string[] {
                "Ácida",
                "Neutra",
                "Básica",
                "Tampão"
            },
            correctIndex = 2,
            questionNumber = 40,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 40,
                    text = "pH > 7 significa [H⁺] < 10⁻⁷ M — há menos prótons que na água pura, " +
                           "e consequentemente mais OH⁻. Mais OH⁻ = mais básica. " +
                           "Exemplos: água do mar tem pH ~8; bicarbonato tem pH ~8,3; NaOH tem pH ~14."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O processo de neutralização envolve:",
            answers = new string[] {
                "A adição de um ácido a uma base.",
                "A adição de uma base a um ácido.",
                "A reação entre um ácido e uma base, resultando em água e um sal.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 2,
            questionNumber = 41,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 41,
                    text = "Neutralização é uma reação específica, não apenas misturar ácido com base. " +
                           "Na reação, H⁺ do ácido + OH⁻ da base → H₂O, e os íons restantes formam um SAL. " +
                           "Exemplo: HCl + NaOH → NaCl (sal) + H₂O (água)."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Durante uma titulação, o ponto de equivalência é atingido quando:",
            answers = new string[] {
                "A concentração de H+ é igual à concentração de OH<sup><size=150%>-</size></sup>. ",
                "O pH é igual a 0.",
                "O pH é igual a 7.",
                "O pH é igual a 14."
            },
            correctIndex = 0,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 42,
                    text = "Atenção: o ponto de equivalência NÃO é necessariamente pH = 7! " +
                           "O pH = 7 só ocorre em titulações de ácido forte com base forte. " +
                           "O verdadeiro critério do ponto de equivalência é que mols de H⁺ = mols de OH⁻ " +
                           "adicionados — ou seja, [H⁺] = [OH⁻] naquele momento."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Um indicador de pH é uma substância que:",
            answers = new string[] {
                "Muda de cor em um determinado intervalo de pH.",
                "Muda de cor em qualquer pH.",
                "Mantém o pH constante.",
                "Neutraliza ácidos e bases."
            },
            correctIndex = 0,
            questionNumber = 43,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 43,
                    text = "Indicadores de pH são ácidos ou bases fracos cujas formas ácida e básica " +
                           "têm CORES diferentes. A mudança de cor ocorre em uma faixa específica de pH " +
                           "(geralmente ± 1 do pKa do indicador) — não em qualquer pH."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O que é uma solução-tampão?",
            answers = new string[] {
                "Uma solução que resiste a mudanças de temperatura.",
                "Uma solução que resiste a mudanças de pressão.",
                "Uma solução que resiste a mudanças de pH.",
                "Uma solução que resiste a mudanças de volume."
            },
            correctIndex = 2,
            questionNumber = 44,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 44,
                    text = "\"Tampão\" vem da ideia de amortecer/tamponar variações. " +
                           "Em química, o tampão (buffer) não interfere em temperatura, pressão ou volume — " +
                           "sua única função é minimizar variações de pH quando ácidos ou bases são adicionados."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução-tampão é tipicamente composta de:",
            answers = new string[] {
                "Um ácido forte e uma base forte.",
                "Um ácido fraco e sua base conjugada.",
                "Um ácido forte e sua base conjugada.",
                "Um ácido fraco e uma base forte."
            },
            correctIndex = 1,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 45,
                    text = "Um ácido FORTE se dissocia completamente — não sobra ácido intacto para reagir " +
                           "com bases adicionadas, então não tampona. O tampão precisa de um ácido FRACO " +
                           "(para neutralizar bases) E sua base conjugada (para neutralizar ácidos)."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A capacidade de tamponamento de uma solução-tampão é máxima em:",
            answers = new string[] {
                "pH = 0",
                "pH = 7",
                "pH = pKa",
                "pH = 14"
            },
            correctIndex = 2,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 46,
                    text = "Quando pH = pKa, pela equação de Henderson-Hasselbalch: " +
                           "pH = pKa + log([A⁻]/[HA]) → log([A⁻]/[HA]) = 0 → [A⁻] = [HA]. " +
                           "Com concentrações iguais de ácido e base conjugada, o tampão tem " +
                           "capacidade máxima de absorver tanto H⁺ quanto OH⁻."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A faixa de tamponamento de uma solução-tampão é aproximadamente:",
            answers = new string[] {
                "Igual ao pKa",
                "± 1 unidade de pH em relação ao pKa",
                "± 2 unidades de pH em relação ao pKa",
                "± 3 unidades de pH em relação ao pKa"
            },
            correctIndex = 1,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 47,
                    text = "O tampão é eficaz quando a razão [A⁻]/[HA] está entre 1:10 e 10:1. " +
                           "log(1/10) = -1 e log(10/1) = +1. Substituindo em Henderson-Hasselbalch: " +
                           "pH vai de pKa - 1 a pKa + 1 → faixa de ± 1 unidade ao redor do pKa."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual a principal função do sistema tampão do sangue?",
            answers = new string[] {
                "Regular a temperatura corporal",
                "Manter o pH do sangue constante",
                "Regular a pressão sanguínea",
                "Transportar oxigênio"
            },
            correctIndex = 1,
            questionNumber = 48,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 48,
                    text = "O pH do sangue deve ser mantido entre 7,35 e 7,45. Fora dessa faixa estreita, " +
                           "proteínas e enzimas perdem função — o que pode ser fatal. " +
                           "O sistema tampão não regula temperatura nem pressão — essas são funções de outros sistemas."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O principal sistema tampão do sangue é o sistema:",
            answers = new string[] {
                "Fosfato",
                "Acetato",
                "Bicarbonato",
                "Hemoglobina"
            },
            correctIndex = 2,
            questionNumber = 49,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 49,
                    text = "O fosfato tampona bem em células e urina, mas não é o principal no sangue. " +
                           "A hemoglobina contribui, mas não é o principal. " +
                           "O sistema dominante no plasma sanguíneo é o par H₂CO₃ / HCO₃⁻, " +
                           "pois é regulado simultaneamente pelos rins e pelos pulmões."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Durante o exercício intenso, o aumento da produção de ácido lático causa:",
            answers = new string[] {
                "Aumento do pH do sangue",
                "Diminuição do pH do sangue",
                "Aumento da taxa respiratória",
                "Diminuição da taxa respiratória"
            },
            correctIndex = 1,
            questionNumber = 50,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 50,
                    text = "O ácido lático é um ÁCIDO — libera H⁺ ao se dissociar em lactato + H⁺. " +
                           "Mais H⁺ no sangue = maior acidez = pH menor. " +
                           "A alternativa C (aumento respiratório) é a CONSEQUÊNCIA, não a causa direta."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Segundo Arrhenius, um ácido é toda substância que em solução aquosa libera:",
            answers = new string[] {
                "OH⁻",
                "H⁺ (prótons)",
                "Na⁺",
                "Cl⁻"
            },
            correctIndex = 1,
            questionNumber = 51,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 51,
                    text = "Arrhenius definiu ácidos por um único critério: o que eles LIBERAM em água. " +
                           "OH⁻ é o que as BASES liberam. Na⁺ e Cl⁻ são íons de sais. " +
                           "O ácido de Arrhenius libera especificamente prótons — íons H⁺."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Segundo Arrhenius, uma base é toda substância que em solução aquosa libera:",
            answers = new string[] {
                "H⁺",
                "OH⁻",
                "CO₂",
                "O₂"
            },
            correctIndex = 1,
            questionNumber = 52,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 52,
                    text = "Na teoria de Arrhenius, o oposto do ácido (libera H⁺) é a base (libera OH⁻). " +
                           "Exemplo clássico: NaOH → Na⁺ + OH⁻. CO₂ e O₂ não se enquadram — " +
                           "aliás, CO₂ em água forma H₂CO₃, que é ácido, não base."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "A escala de pH mede:",
            answers = new string[] {
                "A concentração de oxigênio em uma solução",
                "A concentração de prótons (H⁺) em uma solução",
                "A quantidade de sais dissolvidos",
                "A densidade da água"
            },
            correctIndex = 1,
            questionNumber = 53,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 53,
                    text = "pH = -log[H⁺]. A letra \"H\" em pH referencia o íon hidrogênio (H⁺). " +
                           "Oxigênio dissolvido, sais e densidade são medidos por outros instrumentos e escalas — " +
                           "o pH é específico para a concentração de prótons."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com pH menor que 7 é considerada:",
            answers = new string[] {
                "Neutra",
                "Ácida",
                "Básica",
                "Isotônica"
            },
            correctIndex = 1,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 54,
                    text = "Lembre a regra de três da escala de pH: " +
                           "pH < 7 = ácida | pH = 7 = neutra | pH > 7 = básica. " +
                           "\"Isotônica\" é um termo de osmolaridade, não de pH — cuidado com essa distração."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com pH maior que 7 é considerada:",
            answers = new string[] {
                "Ácida",
                "Neutra",
                "Básica",
                "Saturada"
            },
            correctIndex = 2,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 55,
                    text = "pH > 7 indica que [H⁺] < 10⁻⁷ M — há poucos prótons e, " +
                           "consequentemente, mais OH⁻ (lembrando que pH + pOH = 14). " +
                           "\"Saturada\" se refere à capacidade de dissolução de solutos — não tem relação com pH."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O pH de uma solução neutra (como água pura, a 25 °C) é:",
            answers = new string[] {
                "0",
                "7",
                "10",
                "14"
            },
            correctIndex = 1,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 56,
                    text = "Solução neutra = [H⁺] = [OH⁻]. Em água pura, ambas as concentrações " +
                           "são 10⁻⁷ mol/L a 25°C. Aplicando pH = -log(10⁻⁷) = 7. " +
                           "pH 0 e 14 são os extremos ácido e básico; pH 10 seria básico."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Os sistemas tampão (buffers) no organismo têm como principal função:",
            answers = new string[] {
                "Regular a temperatura corporal",
                "Transportar oxigênio",
                "Manter o pH estável",
                "Produzir energia imediata"
            },
            correctIndex = 2,
            questionNumber = 57,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 57,
                    text = "Temperatura é regulada por mecanismos como sudorese e vasodilatação. " +
                           "O oxigênio é transportado pela hemoglobina. ATP é a fonte de energia. " +
                           "Buffers têm uma função exclusiva: resistir a variações de pH, mantendo-o estável."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual par funciona como sistema tampão importante no sangue?",
            answers = new string[] {
                "Glicose/Insulina",
                "Hemoglobina/O₂",
                "H₂CO₃/HCO₃⁻ (ácido carbônico/bicarbonato)",
                "DNA/RNA"
            },
            correctIndex = 2,
            questionNumber = 58,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 58,
                    text = "Um sistema tampão precisa de um ácido fraco e sua base conjugada. " +
                           "H₂CO₃ é o ácido fraco e HCO₃⁻ (bicarbonato) é sua base conjugada. " +
                           "Glicose/Insulina é regulação glicêmica; Hemoglobina/O₂ é transporte de gases — " +
                           "nenhum desses é um par ácido-base conjugado."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Uma solução com alta concentração de íons OH⁻ é classificada como:",
            answers = new string[] {
                "Ácida",
                "Neutra",
                "Básica",
                "Isotônica"
            },
            correctIndex = 2,
            questionNumber = 59,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 59,
                    text = "Alta concentração de OH⁻ significa baixa concentração de H⁺ (pois [H⁺] × [OH⁻] = 10⁻¹⁴). " +
                           "Menos H⁺ → pH mais alto → solução básica (alcalina). " +
                           "Ácidas têm mais H⁺; neutras têm [H⁺] = [OH⁻]."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual indicador muda de cor para identificar se uma solução é ácida ou básica?",
            answers = new string[] {
                "Cloreto de sódio",
                "Fenolftaleína ou papel de tornassol",
                "Glicose",
                "Albumina"
            },
            correctIndex = 1,
            questionNumber = 60,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 60,
                    text = "NaCl (sal), glicose (açúcar) e albumina (proteína) não mudam de cor com pH. " +
                           "Os indicadores de pH são substâncias específicas cujas formas ácida e básica " +
                           "têm cores diferentes — como a fenolftaleína (incolor em ácido, rosa em base) " +
                           "e o papel de tornassol (vermelho em ácido, azul em base)."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual das substâncias abaixo é considerada uma base de Arrhenius?",
            answers = new string[] {
                "HCl",
                "NaOH",
                "CO₂",
                "H₂SO₄"
            },
            correctIndex = 1,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 61,
                    text = "HCl libera H⁺ → ácido. H₂SO₄ libera H⁺ → ácido. " +
                           "CO₂ em água forma H₂CO₃ → ácido. " +
                           "Apenas o NaOH libera OH⁻ (NaOH → Na⁺ + OH⁻) — portanto, é a base de Arrhenius."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O par ácido-base que difere por apenas um próton (H⁺) é chamado de:",
            answers = new string[] {
                "Par conjugado",
                "Par isotópico",
                "Par redox",
                "Par covalente"
            },
            correctIndex = 0,
            questionNumber = 62,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 62,
                    text = "\"Conjugado\" significa ligado, relacionado. Na teoria de Brønsted-Lowry, " +
                           "quando um ácido perde H⁺, forma sua base conjugada — elas diferem por apenas 1 próton. " +
                           "Par redox envolve transferência de elétrons; par isotópico envolve isótopos — nada a ver com ácido-base."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual o pH de uma solução neutra a 25 °C?",
            answers = new string[] {
                "0",
                "7",
                "14",
                "10"
            },
            correctIndex = 1,
            questionNumber = 63,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 63,
                    text = "Uma solução neutra tem [H⁺] = [OH⁻]. Em água pura a 25°C, " +
                           "essa concentração é exatamente 10⁻⁷ mol/L para ambos os íons. " +
                           "pH = -log(10⁻⁷) = 7. Os extremos 0 e 14 são ácido máximo e básico máximo."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Se uma solução tem [H⁺] = 1 × 10⁻⁹ mol/L, seu pH é:",
            answers = new string[] {
                "5",
                "7",
                "9",
                "11"
            },
            correctIndex = 2,
            questionNumber = 64,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 64,
                    text = "Aplique diretamente: pH = -log[H⁺] = -log(1 × 10⁻⁹). " +
                           "log(10⁻⁹) = -9, então pH = -(-9) = 9. " +
                           "Como pH 9 > 7, confirma-se que a solução é básica — coerente com [H⁺] baixo."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O ácido clorídrico (HCl) é classificado como:",
            answers = new string[] {
                "Ácido fraco",
                "Base fraca",
                "Ácido forte",
                "Base forte"
            },
            correctIndex = 2,
            questionNumber = 65,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 65,
                    text = "HCl libera H⁺ em solução → é um ácido (não uma base). " +
                           "Ele se dissocia quase completamente em água (Ka muito alto) → é um ácido FORTE. " +
                           "Os 6 ácidos fortes mais comuns: HCl, HBr, HI, HNO₃, H₂SO₄ e HClO₄."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "Qual destas soluções apresenta caráter básico?",
            answers = new string[] {
                "pH = 2",
                "pH = 6",
                "pH = 7",
                "pH = 12"
            },
            correctIndex = 3,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 66,
                    text = "Analise cada opção: pH 2 → ácida; pH 6 → ácida (abaixo de 7); " +
                           "pH 7 → neutra; pH 12 → básica (acima de 7). " +
                           "Somente o pH 12 está acima do ponto neutro — portanto, apenas ele tem caráter básico."
                }
            }
        },
        new Question
        {
            questionDatabankName = "AcidBaseBufferQuestionDatabase",
            questionText = "O produto iônico da água a 25 °C (Kw) é:",
            answers = new string[] {
                "1 × 10⁻¹⁴",
                "1 × 10⁻⁷",
                "1 × 10⁻¹",
                "1 × 10⁻¹⁰"
            },
            correctIndex = 0,
            questionNumber = 67,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,

            hint = new System.Collections.Generic.List<QuestionSystem.Hint>
            {
                new TextHint
                {
                    dataBankName   = "AcidBaseBufferQuestionDatabase",
                    questionNumber = 67,
                    text = "Kw = [H⁺] × [OH⁻]. Em água pura, ambas as concentrações são 10⁻⁷ M. " +
                           "Portanto Kw = 10⁻⁷ × 10⁻⁷ = 10⁻¹⁴. " +
                           "Cuidado: 10⁻⁷ é a concentração de CADA íon individualmente — não o produto."
                }
            }
        }
    };

    public List<Question> GetQuestions()
    {
        return questions;
    }

    public QuestionSet GetQuestionSetType()
    {
        return QuestionSet.acidsBase;
    }

    public string GetDatabankName()
    {
        return "AcidBaseBufferQuestionDatabase";
    }

    public bool IsDatabaseInDevelopment()
    {
        return databaseInDevelopment;
    }
}