using System.Collections.Generic;
using QuestionSystem;

public class BiochemistryIntroductionQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    private List<Question> questions = new List<Question>
    {
        // Questão 01
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Um estudante afirma que todas as opções abaixo descrevem características de seres vivos. Identifique a afirmação INCORRETA",
            answers = new string[] {
                "Utilizam energia do ambiente para manter suas funções",
                "Possuem organização molecular e celular",
                "São capazes de se autorreplicar",
                "São indiferentes a mudanças no ambiente"
            },
            correctIndex = 3,
            questionNumber = 1,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_001",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Seres vivos possuem propriedades fundamentais que os distinguem da matéria inanimada. Entre essas propriedades estão: organização molecular e celular, metabolismo (uso de energia), reprodução e resposta a estímulos do ambiente. A capacidade de detectar e responder a mudanças no ambiente (irritabilidade) é essencial para a sobrevivência. Um ser vivo que não respondesse a nenhuma mudança ambiental não conseguiria se adaptar nem sobreviver.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 02
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual teoria defendia que os seres vivos possuíam uma propriedade exclusiva, ausente na matéria inanimada, que explicava seus processos vitais?",
            answers = new string[] {
                "Teoria da abiogênese",
                "Teoria da biogênese",
                "Vitalismo",
                "Teoria celular"
            },
            correctIndex = 2,
            questionNumber = 2,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_002",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O Vitalismo foi uma corrente filosófico-científica que propunha a existência de uma 'força vital' (vis vitalis) exclusiva dos seres vivos. Segundo os vitalistas, compostos orgânicos só podiam ser produzidos por organismos vivos, nunca em laboratório. Essa teoria foi abalada em 1828, quando Friedrich Wöhler sintetizou ureia (composto orgânico) a partir de cianato de amônio (inorgânico). Não confunda: abiogênese = vida surge da matéria inanimada; biogênese = vida vem de vida; vitalismo = vida tem força especial.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 03
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Os experimentos de Pasteur mostraram que, sem contato com o ar contaminado, caldos nutritivos não apodreciam. O que isso revelou sobre a origem dos seres vivos?",
            answers = new string[] {
                "Que seres vivos só surgem a partir de outros seres vivos preexistentes",
                "Que compostos orgânicos não podem ser sintetizados sem um organismo vivo",
                "Que a força vital presente no ar é responsável pela geração de vida",
                "Que microrganismos surgem espontaneamente em meios nutritivos aquecidos"
            },
            correctIndex = 0,
            questionNumber = 3,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_003",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Louis Pasteur usou frascos de pescoço de cisne para mostrar que caldos esterilizados não apodreciam quando impedidos de contato com ar contaminado. Quando o pescoço do frasco era quebrado, os microrganismos do ar entravam e o caldo apodreciia. Isso demonstrou que os microrganismos vinham do ar (de seres preexistentes), e não surgiam espontaneamente. Esse experimento confirmou a teoria da biogênese: omne vivum ex vivo (toda vida vem de vida).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 04
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Ao provar que a vida não surge espontaneamente da matéria inanimada, Pasteur enfraqueceu o vitalismo porque:",
            answers = new string[] {
                "demonstrou que compostos orgânicos podem ser sintetizados em laboratório",
                "eliminou a necessidade de uma força especial para explicar a origem da vida",
                "provou que seres vivos são formados apenas por átomos e moléculas comuns",
                "estabeleceu que toda célula origina-se de outra célula preexistente"
            },
            correctIndex = 1,
            questionNumber = 4,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_004",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O Vitalismo precisava da geração espontânea para explicar como a 'força vital' poderia surgir na matéria inanimada. Pasteur mostrou que microrganismos sempre vêm de outros microrganismos — não há geração espontânea. Com isso, a ideia de que seria necessária uma força vital especial para 'animar' a matéria perdeu sustentação. Atenção: a síntese de compostos orgânicos em laboratório (Wöhler, 1828) também derrubou o vitalismo, mas por outro argumento.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 05
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Pasteur demonstrou que a fermentação alcoólica só ocorre na presença de leveduras vivas. Qual foi a principal implicação bioquímica dessa descoberta?",
            answers = new string[] {
                "Que transformações químicas em alimentos são processos exclusivamente físicos",
                "Que microrganismos vivos são capazes de catalisar transformações químicas",
                "Que a fermentação e a respiração celular são processos idênticos",
                "Que leveduras produzem energia apenas na presença de oxigênio"
            },
            correctIndex = 1,
            questionNumber = 5,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_005",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Pasteur demonstrou que a fermentação alcoólica (glicose → etanol + CO₂) é um processo biológico, não puramente químico. Leveduras vivas realizam essa transformação usando enzimas — catalisadores biológicos. Essa descoberta foi o alicerce da bioquímica: organismos vivos catalisam reações químicas específicas. Lembre: fermentação é anaeróbia (sem O₂); respiração celular é aeróbia (com O₂) — são processos diferentes.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 06
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual das alternativas representa corretamente a diversidade de biomoléculas que compõem os seres vivos?",
            answers = new string[] {
                "Proteínas, lipídios, carboidratos e ácidos nucleicos",
                "Proteínas, lipídios, minerais e água",
                "Carboidratos, vitaminas, ácidos nucleicos e sais minerais",
                "Lipídios, proteínas, hormônios e enzimas"
            },
            correctIndex = 0,
            questionNumber = 6,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_006",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "As quatro grandes classes de biomoléculas orgânicas são: proteínas, lipídios, carboidratos e ácidos nucleicos. Proteínas: funções estruturais e enzimáticas. Lipídios: membranas e reserva energética. Carboidratos: energia imediata e estrutura. Ácidos nucleicos: informação genética (DNA e RNA). Hormônios e enzimas não são classes — são exemplos de proteínas. Vitaminas e minerais são micronutrientes, não biomoléculas orgânicas principais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 07
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Por que diferentes biomoléculas, como proteínas e lipídios, apresentam propriedades e funções tão distintas?",
            answers = new string[] {
                "Porque são formadas por átomos de elementos químicos diferentes",
                "Porque possuem grupos funcionais diferentes, que determinam sua identidade química",
                "Porque proteínas são moléculas orgânicas e lipídios são moléculas inorgânicas",
                "Porque o tamanho da molécula é o único fator que define suas propriedades"
            },
            correctIndex = 1,
            questionNumber = 7,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_007",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Grupos funcionais são conjuntos de átomos que conferem propriedades químicas específicas à molécula. Proteínas possuem ligações peptídicas (-CO-NH-) e grupos amina/carboxila. Lipídios possuem ésteres e longas cadeias apolares. Tanto proteínas quanto lipídios contêm C, H e O — não é diferença de elementos que define as propriedades. É a combinação e disposição dos grupos funcionais que determina reatividade, solubilidade e função biológica.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 08
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Identifique a estrutura que representa um hidrocarboneto ramificado",
            answers = new string[] {
                "AnswerImages/IntroductionDB/benzeno",
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/2-3-dimetil-pentano",
                "AnswerImages/IntroductionDB/propanamina"
            },
            correctIndex = 2,
            questionNumber = 8,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_008",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Hidrocarboneto: molécula formada apenas por carbono e hidrogênio (sem outros elementos como O, N ou S). Ramificado: a cadeia carbônica principal possui ramificações (carbonos que saem da cadeia principal). Elimine: benzeno (aromático, não ramificado), 2-butanol (tem -OH, não é hidrocarboneto), propanamina (tem N, não é hidrocarboneto). 2,3-dimetilpentano: é apenas C e H, com grupos metil saindo da cadeia principal — hidrocarboneto ramificado.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 09
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Identifique a estrutura molecular de um hidrocarboneto insaturado",
            answers = new string[] {
                "AnswerImages/IntroductionDB/ciclo-hexano",
                "AnswerImages/IntroductionDB/3-metil-hexano",
                "AnswerImages/IntroductionDB/2-metil-butanol",
                "AnswerImages/IntroductionDB/2-hexeno"
            },
            correctIndex = 3,
            questionNumber = 9,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_009",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Hidrocarboneto insaturado: possui pelo menos uma ligação dupla (C=C) ou tripla (C≡C) entre carbonos. Saturado: apenas ligações simples C-C (ex: alcanos como hexano, ciclo-hexano). Ciclo-hexano: saturado (só ligações simples, apenas em anel). 3-metil-hexano: saturado ramificado. 2-metil-butanol: tem -OH (não é hidrocarboneto). 2-hexeno: tem C=C na posição 2 — hidrocarboneto insaturado (alceno).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 10
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Identifique a estrutura molecular da hidrocarboneto alifático",
            answers = new string[] {
                "AnswerImages/IntroductionDB/ciclo-hexano",
                "AnswerImages/IntroductionDB/hexano",
                "AnswerImages/IntroductionDB/benzeno",
                "AnswerImages/IntroductionDB/2-hexeno"
            },
            correctIndex = 1,
            questionNumber = 10,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_010",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Hidrocarboneto alifático: cadeia aberta (não aromática), podendo ser saturado ou insaturado. Aromático: possui anel benzênico com elétrons deslocalizados (ex: benzeno). Cíclico: anel, mas sem aromaticidade (ex: ciclo-hexano). Benzeno → aromático (não alifático). Ciclo-hexano → cíclico, mas saturado e não aromático — é alifático cíclico. Hexano (cadeia aberta, saturada) e 2-hexeno (cadeia aberta, insaturada) são alifáticos. Hexano é o exemplo mais clássico de alifático simples.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 11
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/haxano",
                "AnswerImages/IntroductionDB/ciclo-hexano",
                "AnswerImages/IntroductionDB/benzeno",
                "AnswerImages/IntroductionDB/3-metil-haxano"
            },
            correctIndex = 1,
            questionNumber = 11,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer11",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_011",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão mostra o 2-hexeno (C₆H₁₂) e pede um isômero — mesma fórmula molecular (C₆H₁₂), estrutura diferente. Isômeros têm a mesma fórmula molecular mas arranjos estruturais distintos. Hexano: C₆H₁₄ (não é isômero). Benzeno: C₆H₆ (não é isômero). 3-metil-hexano: C₇H₁₆ (não é isômero). Ciclo-hexano: C₆H₁₂ — mesma fórmula que o 2-hexeno! É um isômero cíclico do alceno.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 12
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-metil-3-dimetilpentano",
                "AnswerImages/IntroductionDB/ciclo-hexano",
                "AnswerImages/IntroductionDB/benzeno",
                "AnswerImages/IntroductionDB/2-3-dimetil-pentano"
            },
            correctIndex = 3,
            questionNumber = 12,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer12",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_011",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão mostra o 3-metil-hexano (C₇H₁₆) e pede um isômero com a mesma fórmula molecular. Conte os carbonos: 3-metil-hexano tem 6 (cadeia) + 1 (metil) = 7 carbonos → C₇H₁₆. Ciclo-hexano: C₆H₁₂. Benzeno: C₆H₆. Esses não têm C₇H₁₆. 2,3-dimetilpentano: 5 (cadeia) + 1 + 1 (dois metilas) = 7 carbonos → C₇H₁₆. É o isômero correto!",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 13
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-metil-3-dimetilpentano",
                "AnswerImages/IntroductionDB/3-metil-pentano",
                "AnswerImages/IntroductionDB/2-metil-butanol",
                "AnswerImages/IntroductionDB/2-3-dimetil-pentano"
            },
            correctIndex = 3,
            questionNumber = 13,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer13",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_013",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Carbono quiral (assimétrico): carbono ligado a quatro grupos DIFERENTES simultaneamente. Para identificar: verifique cada carbono sp3 e veja se todos os quatro substituintes são distintos. 3-metil-pentano e 2-metil-butanol: verifique se algum carbono central tem 4 grupos diferentes. 2,3-dimetilpentano: o carbono C3 está ligado a CH₃, C₂H₅, C₂H₅(com metil) e H — quatro grupos distintos. É o composto com carbono quiral.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 14
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Compostos orgânicos insaturados podem ser classificados como isômeros cis e trans. Assinale a alternativa que representa um isômero cis.",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-4-hexadieno",
                "AnswerImages/IntroductionDB/2-hexeno",
                "AnswerImages/IntroductionDB/cis-3-hexeno",
                "AnswerImages/IntroductionDB/trans-3-hexeno"
            },
            correctIndex = 2,
            questionNumber = 14,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_014",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Isomeria cis/trans ocorre em alcenos com dupla ligação C=C (sem rotação livre). Cis: grupos iguais (ou de maior prioridade) no mesmo lado da dupla ligação. Trans: grupos iguais (ou de maior prioridade) em lados opostos da dupla ligação. cis-3-hexeno: os dois grupos etila ficam do mesmo lado da dupla C=C. Trans-3-hexeno: ficam em lados opostos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 15
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Alcoóis são compostos orgânicos onde o grupo -OH está ligado a um carbono com hibridação sp3 (carbono saturado com 4 ligações simples). Qual estrutura representa um álcool?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-2-pentenol",
                "AnswerImages/IntroductionDB/fenol",
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/metil-propil-eter"
            },
            correctIndex = 2,
            questionNumber = 15,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_015",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Álcool: grupo -OH ligado a carbono sp3 (saturado, 4 ligações simples). Fórmula geral: R-OH. Fenol: -OH ligado a carbono sp2 de anel aromático — não é álcool, é fenol (classe diferente). Éter: oxigênio ligado a dois carbonos (R-O-R'), sem -OH livre. 2-pentenol: tem -OH, mas ligado próximo a uma dupla ligação — verifique a hibridação do carbono que carrega o -OH. 2-butanol: -OH no carbono 2 (sp3, saturado) — álcool secundário clássico.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 16
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Álcoois são classificados pelo número de carbonos ligados ao carbono que carrega o -OH. Álcool primário: 1 carbono vizinho. Secundário: 2 carbonos vizinhos. Terciário: 3 carbonos vizinhos. Qual estrutura é um álcool secundário?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/fenol",
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/butanol",
                "AnswerImages/IntroductionDB/2-metil-2-butanol"
            },
            correctIndex = 1,
            questionNumber = 16,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_016",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Classificação dos álcoois pelo carbono que carrega o -OH: • Primário (1°): C-OH ligado a apenas 1 outro carbono (ex: butanol, etanol). • Secundário (2°): C-OH ligado a 2 outros carbonos (ex: 2-butanol). • Terciário (3°): C-OH ligado a 3 outros carbonos (ex: 2-metil-2-butanol). No 2-butanol: CH₃-CH(OH)-CH₂-CH₃ → o carbono com -OH está entre dois outros carbonos = secundário.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 17
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Assinale a estrutura que apresenta um álcool terciário.",
            answers = new string[] {
                "AnswerImages/IntroductionDB/fenol",
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/butanol",
                "AnswerImages/IntroductionDB/2-metil-2-butanol"
            },
            correctIndex = 3,
            questionNumber = 17,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_017",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Álcool terciário: o carbono que carrega o -OH está ligado a 3 outros carbonos. Butanol: -OH na extremidade → primário. 2-butanol: -OH no C2 com 2 vizinhos → secundário. 2-metil-2-butanol: CH₃-C(OH)(CH₃)-CH₂-CH₃ → C2 tem três carbonos vizinhos (dois CH₃ e um CH₂) = terciário. Fenol: -OH em anel aromático → não é álcool.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 18
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Cetonas são compostos orgânicos que contêm um grupo carbonila (C=O) ligado a dois carbonos diferentes, ou seja, a carbonila nunca está na extremidade da cadeia carbônica. Qual estrutura é uma cetona?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanal",
                "AnswerImages/IntroductionDB/2-butanona",
                "AnswerImages/IntroductionDB/propanoato-demetila",
                "AnswerImages/IntroductionDB/acido-propanoico"
            },
            correctIndex = 1,
            questionNumber = 18,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_018",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Cetona: grupo carbonila (C=O) no interior da cadeia, ligado a dois carbonos. Fórmula geral: R-CO-R'. Aldeído: C=O na extremidade da cadeia, ligado a H. Fórmula geral: R-CHO. Propanal: C=O na extremidade (aldeído). Ácido propanoico: tem -COOH (ácido carboxílico). 2-butanona (metil-etil-cetona): CH₃-CO-CH₂-CH₃ → C=O entre dois carbonos = cetona.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 19
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Alguns açúcares, chamados cetoses, possuem um grupo carbonila (C=O) de uma cetona em sua estrutura. Em cetoses a carbonila está ligada a dois carbonos diferentes, nunca na ponta. Qual estrutura é uma cetose?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanoato-demetila",
                "AnswerImages/IntroductionDB/propanal",
                "AnswerImages/IntroductionDB/aldose",
                "AnswerImages/IntroductionDB/cetose"
            },
            correctIndex = 3,
            questionNumber = 19,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_019",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Monossacarídeos são classificados pelo grupo carbonila: • Aldoses: têm grupo aldeído (C=O na extremidade) — ex: glicose, galactose. • Cetoses: têm grupo cetona (C=O no interior da cadeia) — ex: frutose. Identifique nas estruturas: na aldose, o C=O está na ponta com um H; na cetose, o C=O está no meio da cadeia. Propanoato de metila é um éster; propanal é aldeído simples (não açúcar).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 20
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Abaixo existem três compostos isômeros. Ou seja, mesma fórmula molécular e diferentes fórmulas estruturais. Assinale o composto que não é um isômero.",
            answers = new string[] {
                "AnswerImages/IntroductionDB/ciclo-hexanol",
                "AnswerImages/IntroductionDB/2-metil-2-butanol",
                "AnswerImages/IntroductionDB/2-2-hexenol",
                "AnswerImages/IntroductionDB/ciclo-heptano-eter"
            },
            correctIndex = 1,
            questionNumber = 20,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_020",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para identificar o não-isômero, conte os átomos de cada estrutura e compare as fórmulas moleculares. Ciclo-hexanol: C₆H₁₂O. 2-hexenol: C₆H₁₂O (6C, dupla ligação, -OH). Ciclo-heptano éter: verifique a fórmula. 2-metil-2-butanol: CH₃-C(OH)(CH₃)-CH₂-CH₃ → 5 carbonos = C₅H₁₂O. C₅H₁₂O ≠ C₆H₁₂O → 2-metil-2-butanol tem uma fórmula diferente: não é isômero dos demais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 21
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Aldeídos são compostos com grupo carbonila (C=O) ligado a um carbono e a um hidrogênio. A carbonila sempre fica na extremidade da cadeia carbônica. O carbono sp2 da carbonila tem apenas um vizinho carbônico. Qual estrutura é um aldeído?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanal",
                "AnswerImages/IntroductionDB/2-butanona",
                "AnswerImages/IntroductionDB/propanoato-de-metila",
                "AnswerImages/IntroductionDB/acido-propanoico"
            },
            correctIndex = 0,
            questionNumber = 21,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_021",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aldeído: grupo funcional -CHO (carbonila na extremidade ligada a H). Fórmula geral: R-CHO. Cetona: C=O no interior (R-CO-R'). Ácido carboxílico: -COOH (carbonila + hidroxila na extremidade). Éster: -COO- (carbonila + oxigênio ligado a outro carbono). Propanal: CH₃-CH₂-CHO → C=O na extremidade com H = aldeído de 3 carbonos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 22
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Alguns açúcares, chamados aldose, possuem um grupo carbonila (C=O) de um aldeído em sua estrutura. Em aldoses a carbonila está presente na extremidade da cadeia carbônica. Qual estrutura é uma aldose?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanoato-demetila",
                "AnswerImages/IntroductionDB/propanal",
                "AnswerImages/IntroductionDB/aldose",
                "AnswerImages/IntroductionDB/cetose"
            },
            correctIndex = 2,
            questionNumber = 22,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_022",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aldose = açúcar com grupo aldeído (-CHO) na extremidade da cadeia + múltiplos grupos -OH. Cetose = açúcar com grupo cetona (C=O no interior) + múltiplos grupos -OH. Propanal é um aldeído simples, mas não é um açúcar (não tem múltiplos -OH na cadeia). Identifique a aldose pela combinação: C=O na ponta + vários grupos -OH ao longo da cadeia carbônica.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 23
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Os ácidos carboxílicos apresentam o grupo carboxila (-COOH), que combina carbonila (C=O) ligada a uma hidroxila (-OH). A carboxila sempre fica na extremidade da cadeia. Qual é um ácido carboxílico?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanamida",
                "AnswerImages/IntroductionDB/n-metil-propamida",
                "AnswerImages/IntroductionDB/propanoato-demetila",
                "AnswerImages/IntroductionDB/acido-propanoico"
            },
            correctIndex = 3,
            questionNumber = 23,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_023",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Ácido carboxílico: grupo -COOH na extremidade (carbonila + hidroxila no mesmo carbono). Fórmula geral: R-COOH. Amida: -CO-NH- (carbonila ligada a nitrogênio). Éster: -COO- (carbonila ligada a oxigênio de outro carbono). Propanamida e N-metil-propanamida: têm N ligado à carbonila → amidas. Propanoato de metila: CH₃-CH₂-COO-CH₃ → éster (sem -OH livre). Ácido propanoico: CH₃-CH₂-COOH → ácido carboxílico.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 24
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Ácidos graxos são moléculas compostas por uma longa cadeia carbônica (geralmente 12-18 carbonos) com um grupo carboxila (-COOH) na extremidade. Quanto maior a cadeia, mais hidrofóbico é o ácido graxo. Assinale a estrutura do ácido graxo?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/acido-propanoico",
                "AnswerImages/IntroductionDB/acido-graxo",
                "AnswerImages/IntroductionDB/hidrocarbonetosaturado",
                "AnswerImages/IntroductionDB/hidrocarbonetoinsaturado"
            },
            correctIndex = 1,
            questionNumber = 24,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_024",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Ácido graxo = cadeia longa (12-18C ou mais) + grupo -COOH na extremidade. Ácido propanoico: só 3 carbonos — curto demais para ser ácido graxo. Hidrocarbonetos (saturado ou insaturado): não têm -COOH, apenas C e H. O ácido graxo combina a longa cauda hidrofóbica (apolar) com a cabeça hidrofílica (-COOH), sendo o bloco construtor dos lipídios.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 25
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Ésteres são compostos formados pela reação entre um ácido carboxílico e um álcool, com eliminação de água. Contêm o grupo funcional -COOR- (onde R é um hidrocarboneto). Qual estrutura é um éster?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/acido-propanoico",
                "AnswerImages/IntroductionDB/propanamida",
                "AnswerImages/IntroductionDB/propanoato-demetila",
                "AnswerImages/IntroductionDB/butanol"
            },
            correctIndex = 2,
            questionNumber = 25,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_025",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Éster: grupo funcional -COO- (carbonila + oxigênio éter). Formado por ácido + álcool → éster + água. Ácido propanoico: tem -COOH (não é éster, é o reagente ácido). Propanamida: tem -CO-NH₂ (amida). Butanol: álcool (R-OH). Não tem carbonila. Propanoato de metila: CH₃-CH₂-COO-CH₃ → grupo -COO- presente = éster. Formado de ácido propanoico + metanol.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 26
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/acido-propanoico",
                "AnswerImages/IntroductionDB/acetato-de-benzila",
                "AnswerImages/IntroductionDB/fenol",
                "AnswerImages/IntroductionDB/propanoato-de-metila"
            },
            correctIndex = 1,
            questionNumber = 26,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer26",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_026",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão mostra: ácido acético (CH₃COOH) + álcool benzílico (C₆H₅-CH₂-OH) → H₂O + produto ? Reação de esterificação: ácido carboxílico + álcool → éster + água. O produto tem o grupo -COO- do ácido acético ligado ao grupo benzílico do álcool. Acetato de benzila: CH₃-COO-CH₂-C₆H₅ → éster aromático com aroma floral. Propanoato de metila tem cadeia diferente.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 27
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/n-dimetil-etanamina",
                "AnswerImages/IntroductionDB/propanamina",
                "AnswerImages/IntroductionDB/n-metil-propanamina",
                "AnswerImages/IntroductionDB/anilina"
            },
            correctIndex = 1,
            questionNumber = 27,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer27",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_027",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão pede uma amina primária: N ligado a apenas 1 carbono e 2 hidrogênios (-NH₂). • Amina primária: R-NH₂ (1 carbono no N) • Amina secundária: R-NH-R' (2 carbonos no N) • Amina terciária: R-N(-R')(-R'') (3 carbonos no N) N,N-dimetil-etanamina: 3 carbonos no N = terciária. N-metil-propanamina: 2 carbonos = secundária. Propanamina: CH₃-CH₂-CH₂-NH₂ → 1 carbono no N = primária.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 28
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Assinale a amina secundária",
            answers = new string[] {
                "AnswerImages/IntroductionDB/n-dimetil-etanamina",
                "AnswerImages/IntroductionDB/propanamina",
                "AnswerImages/IntroductionDB/n-metil-propanamina",
                "AnswerImages/IntroductionDB/anilina"
            },
            correctIndex = 2,
            questionNumber = 28,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_028",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Conte o número de carbonos ligados diretamente ao átomo de nitrogênio: • 1 carbono no N → amina primária (ex: propanamina: N-H₂, 1 cadeia) • 2 carbonos no N → amina secundária (N-H, 2 cadeias) • 3 carbonos no N → amina terciária (sem H no N, 3 cadeias) N-metil-propanamina: CH₃-NH-CH₂-CH₂-CH₃ → N ligado a CH₃ e propila = 2 carbonos = secundária.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 29
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Assinale a amina terciária",
            answers = new string[] {
                "AnswerImages/IntroductionDB/n-dimetil-etanamina",
                "AnswerImages/IntroductionDB/propanamina",
                "AnswerImages/IntroductionDB/n-metil-propanamina",
                "AnswerImages/IntroductionDB/anilina"
            },
            correctIndex = 0,
            questionNumber = 29,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_029",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Amina terciária: nitrogênio ligado a 3 grupos carbônicos diferentes, sem H no nitrogênio. N,N-dimetil-etanamina: (CH₃)₂-N-CH₂-CH₃ → N ligado a dois grupos metila e um grupo etila = 3 carbonos = terciária. Anilina: N ligado a anel aromático + 2H → primária aromática (não confunda com terciária). Lembre: na terciária não há H no nitrogênio; na secundária há 1H; na primária há 2H.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 30
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/n-dimetil-etanamina",
                "AnswerImages/IntroductionDB/propanamina",
                "AnswerImages/IntroductionDB/n-metil-propanamina",
                "AnswerImages/IntroductionDB/n-trimetil-etanamina"
            },
            correctIndex = 3,
            questionNumber = 30,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer30",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_030",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Amina quaternária: N⁺ ligado a 4 grupos carbônicos, com carga positiva permanente — sempre iônica. Difere das terciárias pois o par de elétrons do N foi usado para ligar um 4° carbono, gerando carga positiva. N,N,N-trimetil-etanamina (cloreto de tetralquilamônio): (CH₃)₃-N⁺-CH₂-CH₃ → 4 carbonos no N⁺ = quaternária. N,N-dimetil-etanamina: 3 carbonos no N, sem carga = terciária (não quaternária).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 31
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Dentre as aminas listadas, assinale a amina que possui um carbono quiral em sua cadeia carbônica.",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-metil-2-aminobutano",
                "AnswerImages/IntroductionDB/2-amino-butano",
                "AnswerImages/IntroductionDB/n-metil-propanamina",
                "AnswerImages/IntroductionDB/n-trimetil-etanamina"
            },
            correctIndex = 1,
            questionNumber = 31,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_031",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Carbono quiral: ligado a 4 grupos DIFERENTES. Gera enantiômeros (imagens especulares não sobreponíveis). 2-metil-2-aminobutano: o C2 tem -NH₂, -CH₃, -CH₃ e -CH₂CH₃ → dois grupos CH₃ iguais = NÃO é quiral. 2-aminobutano: CH₃-CH(NH₂)-CH₂-CH₃ → C2 ligado a -NH₂, -H, -CH₃ e -CH₂CH₃ = 4 grupos diferentes = QUIRAL. N-metil-propanamina e N-trimetil-etanamina: o N é que varia, não há C quiral claro nas cadeias.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 32
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/aminoalcool",
                "AnswerImages/IntroductionDB/aminoacido",
                "AnswerImages/IntroductionDB/aminocetona",
                "AnswerImages/IntroductionDB/propanamida"
            },
            correctIndex = 1,
            questionNumber = 32,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer32",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_032",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aminoácido: molécula com grupo amina (-NH₂) E grupo carboxila (-COOH), ambos ligados ao mesmo carbono α (sp3). Aminoálcool: tem -NH₂ e -OH (sem -COOH). Aminocetona: tem -NH₂ e C=O cetônico (sem -COOH). Propanamida: tem -CO-NH₂ (amida) — o N está ligado à carbonila, não é amina livre. Identifique o aminoácido pela presença simultânea de -NH₂ e -COOH no mesmo carbono central.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 33
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-butanona",
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/metil-propil-eter",
                "AnswerImages/IntroductionDB/metil-propil-tioeter"
            },
            correctIndex = 2,
            questionNumber = 33,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer33",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_033",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Éter: grupo funcional R-O-R' (oxigênio simples entre dois carbonos sp3, sem C=O e sem -OH livre). 2-butanona: tem C=O cetônico (cetona). 2-butanol: tem -OH (álcool). Tioéter: R-S-R' (igual ao éter, mas com enxofre em vez de oxigênio). Metil-propil-éter: CH₃-O-CH₂-CH₂-CH₃ → oxigênio entre dois carbonos saturados = éter.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 34
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-butanona",
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/metil-propil-eter",
                "AnswerImages/IntroductionDB/metil-propil-tioeter"
            },
            correctIndex = 3,
            questionNumber = 34,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer34",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_034",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Tioéter: grupo funcional R-S-R' (enxofre simples entre dois carbonos). Análogo ao éter, mas com S no lugar do O. Diferença chave: éter usa O; tioéter usa S. Ambos têm dois carbonos sp3 vizinhos ao heteroátomo. Metil-propil-éter: C-O-C (éter com oxigênio). Metil-propil-tioéter: C-S-C (tioéter com enxofre). Tioéteres são ligeiramente mais reativos que éteres. A metionina (aminoácido) possui grupo tioéter.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 35
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanotiol",
                "AnswerImages/IntroductionDB/ciclo-hexatioeter",
                "AnswerImages/IntroductionDB/metil-propil-eter",
                "AnswerImages/IntroductionDB/metil-propil-tioeter"
            },
            correctIndex = 0,
            questionNumber = 35,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer35",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_035",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Tiol: grupo funcional -SH (enxofre + hidrogênio) ligado a carbono sp3. Análogo ao álcool, mas com S no lugar do O. Éter vs Tioéter: ambos têm o heteroátomo entre dois carbonos (sem H no S ou O). Tiol vs Tioéter: tiol tem -SH (1 carbono); tioéter tem -S- (2 carbonos). Propanotiol: CH₃-CH₂-CH₂-SH → grupo -SH na extremidade = tiol. Possui odor intenso característico (como gambá).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 36
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Amidas são compostos com grupo funcional -CO-N- (carbonila ligada a nitrogênio), e elas são a espinha dorsal para a formação de proteínas. Assinale a estrutura da amida?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/aminoacido",
                "AnswerImages/IntroductionDB/aminocetona",
                "AnswerImages/IntroductionDB/propanamida",
                "AnswerImages/IntroductionDB/anilina"
            },
            correctIndex = 2,
            questionNumber = 36,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_036",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Amida: grupo -CO-NH₂ ou -CO-NHR ou -CO-NR₂ (carbonila ligada diretamente ao nitrogênio). É o grupo funcional da ligação peptídica entre aminoácidos nas proteínas. Anilina: amina aromática (N-H₂ no anel, sem carbonila). Aminocetona: tem N e C=O cetônico (separados). Propanamida: CH₃-CH₂-CO-NH₂ → carbonila diretamente ligada ao N = amida primária.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 37
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Iminas são compostos com grupo funcional -C=N- (ligação dupla carbono-nitrogênio). Diferem de amidas e aminas, estão presentes em diversas moléculas biológicas. Assinale o composto que possui grupo funcional imina?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanamida",
                "AnswerImages/IntroductionDB/aminocetona",
                "AnswerImages/IntroductionDB/cetose",
                "AnswerImages/IntroductionDB/histidina"
            },
            correctIndex = 3,
            questionNumber = 37,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_037",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Imina: grupo C=N (dupla ligação carbono-nitrogênio). Difere da amina (C-N simples) e da amida (CO-N). Propanamida: C-CO-N (amida). Aminocetona: C=O cetônico + NH₂ separados. Cetose: apenas C=O e OH. Histidina: aminoácido com anel imidazol — contém C=N dentro do anel aromático nitrogenado = imina. Iminas são também chamadas de bases de Schiff quando formadas por aldeído + amina; estão em cofatores como piridoxal fosfato.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 38
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual das seguintes moléculas é uma amina terciária?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/n-dimetil-etanamina",
                "AnswerImages/IntroductionDB/2-metil-2-aminobutano",
                "AnswerImages/IntroductionDB/2-amino-butano",
                "AnswerImages/IntroductionDB/n-trimetil-etanamina"
            },
            correctIndex = 0,
            questionNumber = 38,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_038",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Amina terciária: N ligado a 3 grupos carbônicos, sem H no nitrogênio. 2-metil-2-aminobutano: N-H₂ ligado a carbono terciário da cadeia → amina primária (o 'terciário' refere-se ao carbono, não à amina!). 2-aminobutano: N-H₂ → amina primária. N-trimetil-etanamina: N ligado a 4 carbonos = quaternária (N⁺). N,N-dimetil-etanamina: (CH₃)₂-N-CH₂-CH₃ → 3 carbonos no N, sem H = amina terciária.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 39
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-metil-butanal",
                "AnswerImages/IntroductionDB/3-metil-2-butanona",
                "AnswerImages/IntroductionDB/2-butanona",
                "AnswerImages/IntroductionDB/2-butanol"
            },
            correctIndex = 1,
            questionNumber = 39,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer39",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_039",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão descreve C₅H₁₀O, com grupo cetona (C=O interno) e ramificação. C₅H₁₀O com cetona: 2-butanona tem C₄H₈O (4C apenas). 2-metil-butanal: aldeído (C=O na ponta), não cetona. 2-butanol: álcool (C₄H₁₀O, sem C=O). Não tem 5 carbonos. 3-metil-2-butanona: CH₃-CO-CH(CH₃)-CH₃ → C=O na posição 2 (interior) + metila na posição 3 = cetona ramificada com 5 carbonos = C₅H₁₀O.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 40
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual estrutura representa um álcool terciário?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/ciclo-exanol",
                "AnswerImages/IntroductionDB/2-metil-2-butanol",
                "AnswerImages/IntroductionDB/fenol"
            },
            correctIndex = 2,
            questionNumber = 40,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_040",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Álcool terciário: carbono que carrega -OH está ligado a 3 outros carbonos. 2-butanol: C-OH com 2 vizinhos → secundário. Fenol: -OH em sp2 aromático → não é álcool. Ciclo-hexanol: -OH em C do anel com 2 vizinhos → secundário cíclico. 2-metil-2-butanol: C2 tem -OH, -CH₃, -CH₃ e -CH₂CH₃ como vizinhos → 3 carbonos ao redor do C-OH = terciário.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 41
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual das opções abaixo é um éter?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/n-metil-propanamina",
                "AnswerImages/IntroductionDB/pirano",
                "AnswerImages/IntroductionDB/2-2-pentenol",
                "AnswerImages/IntroductionDB/piridina"
            },
            correctIndex = 1,
            questionNumber = 41,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_041",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Éter: oxigênio ligado a dois carbonos (R-O-R'), sem C=O e sem -OH livre. Pode ser cíclico. N-metil-propanamina: tem N, não O → amina. Piridina: anel aromático com N → não é éter. 2-pentenol: tem -OH (álcool/enol). Pirano: anel de 6 membros com 1 oxigênio dentro do anel (C-O-C cíclico) = éter cíclico. Pirano é o esqueleto de muitos açúcares em forma de anel (piranoses como a glicose).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 42
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual fórmula geral representa um álcool?",
            answers = new string[] {
                "R-CHO",
                "R-COOH",
                "R-OH",
                "R-O-R'"
            },
            correctIndex = 2,
            questionNumber = 42,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_042",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Fórmulas gerais dos principais grupos funcionais: • R-CHO → aldeído (carbonila na extremidade com H) • R-COOH → ácido carboxílico (carboxila na extremidade) • R-OH → álcool (hidroxila em carbono sp3) • R-O-R' → éter (oxigênio entre dois carbonos) R representa qualquer cadeia carbônica. O álcool é definido pelo -OH ligado a C sp3.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 43
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Porque têm tamanhos diferentes",
                "Porque têm grupos funcionais diferentes e interagem de forma diferente",
                "Porque são feitos de elementos diferentes",
                "Não há nenhuma diferença real entre eles"
            },
            correctIndex = 1,
            questionNumber = 43,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer43",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_043",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "Compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão compara álcool e éter com mesma fórmula molecular (isômeros funcionais). Álcool (-OH): faz ligações de hidrogênio (O-H...O) → maior ponto de ebulição, maior solubilidade em água. Éter (-O-): aceita ligação de H mas não doa → interações mais fracas, mais volátil. Mesmos elementos (C, H, O), mesmo tamanho molecular — a diferença é o grupo funcional e as interações que ele promove.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 44
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O aldeído tem a dupla C=O no final da cadeia; cetona no meio",
                "O aldeído tem mais átomos de carbono",
                "O aldeído tem a dupla C=O no meio da cadeia; cetona no final",
                "Não há diferença significativa"
            },
            correctIndex = 0,
            questionNumber = 44,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer44",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_044",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "Compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A imagem mostra dois compostos carbonílicos. A diferença estrutural fundamental: • Aldeído (R-CHO): C=O na EXTREMIDADE, ligado a H. O carbono carbonílico tem apenas 1 vizinho carbônico. • Cetona (R-CO-R'): C=O no INTERIOR, entre dois carbonos. O carbono carbonílico tem 2 vizinhos carbônicos. Na imagem: o composto com R e H no carbonílico = aldeído; o com R e R' = cetona. O número de carbonos não define isso.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 45
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "porque o álcool faz interações intermoleculares mais fracas",
                "Porque o grupo -OH forma ligações de hidrogênio intermoleculares",
                "Porque o álcool é mais denso",
                "Porque tem mais prótons no núcleo"
            },
            correctIndex = 1,
            questionNumber = 45,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer45",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_045",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "Compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão compara o ponto de ebulição de um álcool e um alcano de tamanho similar. Ligação de hidrogênio: ocorre quando H está ligado a átomo muito eletronegativo (O, N ou F). No álcool (-OH): H ligado a O → formam ligações de H intermoleculares fortes entre moléculas. No alcano: apenas ligações de van der Waals (fracas). Mais energia é necessária para separar as moléculas do álcool → maior ponto de ebulição.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 46
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual das seguintes afirmações é correta sobre isômeros?",
            answers = new string[] {
                "Isômeros têm a mesma fórmula molecular e sempre as mesmas propriedades",
                "Isômeros têm fórmulas moleculares diferentes",
                "Isômeros têm a mesma fórmula molecular mas estruturas diferentes",
                "Isômeros são tipos diferentes de moléculas não relacionadas"
            },
            correctIndex = 2,
            questionNumber = 46,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "biochem_046",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "Compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Isômeros: mesma fórmula molecular (mesmo número e tipo de átomos), mas estruturas diferentes. Por terem estruturas diferentes, isômeros possuem propriedades físicas e químicas distintas. Tipos principais: isômeria estrutural (posição, cadeia, função) e isômeria espacial (cis/trans, óptica). Exemplo: etanol (C₂H₆O, álcool) e éter dimetílico (C₂H₆O, éter) → mesma fórmula, propriedades muito diferentes.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 47
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "O que significa dizer que um carbono tem hibridação sp3?",
            answers = new string[] {
                "O carbono está ligado a apenas oxigênio",
                "O carbono faz uma dupla ligação",
                "O carbono faz 4 ligações simples em geometria tetraédrica",
                "O carbono faz 3 ligações duplas"
            },
            correctIndex = 2,
            questionNumber = 47,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_047",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "Compreender",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Hibridação do carbono determina sua geometria e tipo de ligações: • sp3: 4 ligações simples, geometria tetraédrica, ângulo ≈ 109,5° (alcanos, álcoois). • sp2: 1 dupla ligação + 2 simples, geometria planar trigonal, ângulo ≈ 120° (alcenos, carbonilas). • sp: 1 tripla ligação + 1 simples, geometria linear, ângulo 180° (alcinos). No contexto dos álcoois: a definição exige -OH em carbono sp3 (saturado) para ser álcool e não fenol.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 48
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/IntroductionDB/propanol",
                "AnswerImages/IntroductionDB/metil-etil-eter",
                "AnswerImages/IntroductionDB/propanal",
                "AnswerImages/IntroductionDB/2-propanol"
            },
            correctIndex = 2,
            questionNumber = 48,
            isImageAnswer = true,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer48",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_048",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A fórmula C₃H₈O permite diferentes estruturas. Determine quais são possíveis: • Propanol (C₃H₈O): álcool primário — POSSÍVEL. • 2-propanol (C₃H₈O): álcool secundário — POSSÍVEL. • Metil-etil-éter (C₃H₈O): R-O-R' — POSSÍVEL. • Propanal (C₃H₆O): aldeído — tem apenas 6H, não 8H! C₃H₆O ≠ C₃H₈O. NÃO é isômero possível.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 49
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual dos seguintes compostos não é um álcool?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-butanol",
                "AnswerImages/IntroductionDB/fenol",
                "AnswerImages/IntroductionDB/2-propanol",
                "AnswerImages/IntroductionDB/ciclo-hexanol"
            },
            correctIndex = 1,
            questionNumber = 49,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_049",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Álcool: -OH ligado a carbono sp3 (saturado, 4 ligações simples). 2-butanol: sp3 → álcool secundário. 2-propanol: sp3 → álcool secundário. Ciclo-hexanol: sp3 → álcool secundário cíclico. Fenol: -OH ligado ao carbono sp2 do anel benzênico. O carbono sp2 participa de ressonância com o anel. Fenol é uma classe funcional diferente — mais ácido que álcoois, com reatividade distinta.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 50
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual estrutura representa uma imina?",
            answers = new string[] {
                "AnswerImages/IntroductionDB/piridina",
                "AnswerImages/IntroductionDB/anilina",
                "AnswerImages/IntroductionDB/n-trimetiletanamina",
                "AnswerImages/IntroductionDB/aminocetona"
            },
            correctIndex = 0,
            questionNumber = 50,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_050",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Imina: ligação dupla C=N. O nitrogênio faz uma dupla ligação com carbono (sp2). Anilina: N-H₂ ligado ao anel — amina aromática (C-N simples). N-trimetiletanamina: amina terciária (C-N simples). Aminocetona: tem NH₂ e C=O separados — não é imina (o N não faz dupla ligação com o C carbonílico). Piridina: anel aromático com N=C dentro do anel (heteroaromático) — contém ligação C=N = imina cíclica aromática.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 51
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Assinale a estrutura de um aminoácido que possui o grupo funcional tiol",
            answers = new string[] {
                "AnswerImages/IntroductionDB/aminoacido",
                "AnswerImages/IntroductionDB/cisteina",
                "AnswerImages/IntroductionDB/metionina",
                "AnswerImages/IntroductionDB/histidina"
            },
            correctIndex = 1,
            questionNumber = 51,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_051",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Tiol: grupo -SH (enxofre + hidrogênio). Aminoácidos com enxofre: cisteína e metionina. Cisteína: tem -CH₂-SH na cadeia lateral → grupo TIOL (-SH livre). Metionina: tem -CH₂-CH₂-S-CH₃ → grupo TIOÉTER (S entre dois C, sem H no S). Histidina: tem anel imidazol (N, sem enxofre). O aminoácido genérico não tem S.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 52
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Assinale a estrutura do aminoácido que possui o grupo funcional tioéter",
            answers = new string[] {
                "AnswerImages/IntroductionDB/aminoacido",
                "AnswerImages/IntroductionDB/cisteina",
                "AnswerImages/IntroductionDB/metionina",
                "AnswerImages/IntroductionDB/histidina"
            },
            correctIndex = 2,
            questionNumber = 52,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_052",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "aplicar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Tioéter: grupo R-S-R' (enxofre entre dois carbonos, sem H no S). Análogo ao éter, mas com S. Cisteína: -CH₂-SH → tiol (tem H no S). Histidina: anel imidazol nitrogenado (sem S). Metionina: -CH₂-CH₂-S-CH₃ → S entre carbono da cadeia e grupo metila = TIOÉTER. Metionina é o aminoácido iniciador da síntese proteica (códon AUG) e possui tioéter em sua cadeia lateral.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 53
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "2,2-dimetil-propano, porque é o mais compacto",
                "Pentano linear, porque tem a maior área de superfície para interações",
                "2-metil-butano, porque está no meio",
                "Todos têm o mesmo ponto de ebulição"
            },
            correctIndex = 1,
            questionNumber = 53,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer53",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_053",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Isômeros do pentano (C₅H₁₂): pentano linear, 2-metilbutano e 2,2-dimetilpropano. Todos são apolares → as forças de atração são as de dispersão de London (van der Waals). Maior área de contato superficial → mais interações de dispersão → maior ponto de ebulição. Pentano linear: forma mais estendida, maior área de superfície. 2,2-dimetilpropano: mais esférico/compacto, menor superfície de contato = menor PE.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 54
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Ambos têm a mesma estrutura e propriedades",
                "Ambos têm a mesma fórmula molecular, mas posições diferentes do C=O",
                "O aldeído é sempre mais tóxico que a cetona",
                "A cetona tem uma dupla ligação, o aldeído não"
            },
            correctIndex = 1,
            questionNumber = 54,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer54",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_054",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Propanal (C₃H₆O) e propanona/acetona (C₃H₆O): mesma fórmula molecular → são isômeros funcionais! Ambos têm C=O (grupo carbonila) — a diferença é a POSIÇÃO do C=O. Propanal: C=O na extremidade (carbono 1, com H) → aldeído. Propanona: C=O no meio (carbono 2) → cetona. Ambos têm dupla ligação C=O. A toxicidade não é regra geral entre as classes.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 55
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Isomeria estrutural",
                "Isomeria espacial (geométrica)",
                "Isomeria óptica",
                "Isomeria funcional"
            },
            correctIndex = 1,
            questionNumber = 55,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer55",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_055",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Cis-2-buteno e trans-2-buteno: mesma fórmula molecular (C₄H₈), mesma cadeia, mesmo tipo de ligação. A única diferença é a disposição espacial dos grupos em torno da dupla C=C (que não permite rotação livre). Isomeria geométrica (espacial/cis-trans): mesma conectividade, diferente arranjo no espaço. Isomeria óptica: envolve carbono quiral (não dupla ligação). Isomeria estrutural: conectividade diferente.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 56
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Um aluno afirma: 'Todos os álcoois são mais solúveis em água que seus éteres correspondentes.' Avalie essa afirmação.",
            answers = new string[] {
                "Verdadeira, todos os álcoois formam pontes de hidrogênio com água",
                "Falsa, a solubilidade depende da cadeia carbônica, não do grupo funcional",
                "Verdadeira apenas para álcoois primários",
                "A afirmação está correta em geral, pois álcoois têm -OH"
            },
            correctIndex = 0,
            questionNumber = 56,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_056",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "avaliar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Álcoois: grupo -OH pode tanto DOAR quanto ACEITAR ligação de hidrogênio com a água. Éteres: o oxigênio pode apenas ACEITAR ligação de H (não tem H para doar) → interação mais fraca com água. Para comparações de tamanho equivalente (mesmo número de carbonos), álcoois sempre são mais solúveis. A afirmação é verdadeira para a comparação entre álcool e éter de tamanho correspondente — a cadeia afeta o quanto, mas o grupo funcional define o tipo de interação.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 57
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Qual critério é MAIS IMPORTANTE para classificar uma estrutura como 'álcool primário' vs 'álcool secundário'?",
            answers = new string[] {
                "O tamanho total da molécula",
                "O número de carbonos ligados ao carbono que carrega o -OH",
                "A cor da solução",
                "Se a molécula tem uma dupla ligação"
            },
            correctIndex = 1,
            questionNumber = 57,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_057",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "avaliar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A classificação de álcoois depende exclusivamente do carbono que suporta o grupo -OH: • Primário: C-OH ligado a 1 carbono (ou nenhum, como no metanol). • Secundário: C-OH ligado a 2 carbonos. • Terciário: C-OH ligado a 3 carbonos. Tamanho da molécula, cor e presença de dupla ligação C=C não determinam essa classificação — apenas a vizinhança do carbono carbinol.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 58
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Um composto possui em sua fórmula molecular apenas C, H e O. Ele não faz ligações de hidrogênio intermoleculares. Qual seria a conclusão mais apropriada sobre sua estrutura?",
            answers = new string[] {
                "Provavelmente ser um ácido carboxílico",
                "Deve ser um álcool",
                "Provavelmente é um éster ou éter, não um álcool",
                "Deve conter uma dupla ligação C=C"
            },
            correctIndex = 2,
            questionNumber = 58,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_058",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "avaliar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Ligação de hidrogênio intermolecular requer H ligado a átomo muito eletronegativo (O, N, F). Álcoois (-OH) e ácidos carboxílicos (-COOH): fazem ligações de H → descartados pela premissa. Éter (R-O-R'): O presente, mas sem H ligado ao O → não faz ligação de H como doador. Éster (-COO-): também sem H no O → não faz ligação de H intermolecular. Ambos (éter e éster) contêm C, H, O mas não formam pontes de H.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 59
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Assinale a alternativa que representa um isômero trans.",
            answers = new string[] {
                "AnswerImages/IntroductionDB/benzeno",
                "AnswerImages/IntroductionDB/trans-3-hexeno",
                "AnswerImages/IntroductionDB/cis-3-hexeno",
                "AnswerImages/IntroductionDB/anilina"
            },
            correctIndex = 1,
            questionNumber = 59,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_059",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "lembrar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Isomeria trans: grupos substituintes de maior prioridade (ou iguais) em lados OPOSTOS da dupla C=C. Isomeria cis: grupos iguais (ou de maior prioridade) no MESMO lado da dupla C=C. Benzeno: anel aromático, sem isomeria cis/trans. Anilina: amina aromática, sem C=C isomerizável. Trans-3-hexeno: os dois grupos etila ficam em lados opostos da dupla na posição 3 = configuração trans.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 60
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Os aminoácidos são os monômeros que formam todas as proteínas que conhecemos na natureza. Assinale a opção que apresenta um aminoácido que nao tem carbono quiral",
            answers = new string[] {
                "AnswerImages/IntroductionDB/histidina",
                "AnswerImages/IntroductionDB/cisteina",
                "AnswerImages/IntroductionDB/glicina",
                "AnswerImages/IntroductionDB/metionina"
            },
            correctIndex = 2,
            questionNumber = 60,
            isImageAnswer = true,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_060",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Carbono quiral: ligado a 4 grupos DIFERENTES. Na maioria dos aminoácidos, o carbono α (central) é quiral. Histidina, cisteína, metionina: carbono α ligado a -NH₂, -COOH, -H e cadeias laterais diferentes → quiral (L-aminoácido). Glicina: o único aminoácido sem carbono quiral. Seu carbono α está ligado a -NH₂, -COOH e DOIS -H. Com dois H iguais no mesmo carbono, ele tem dois substituintes idênticos → não é quiral → glicina não tem isômero L ou D.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 61
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                " 4",
                " 5",
                " 6",
                " 7"
            },
            correctIndex = 0,
            questionNumber = 61,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer61",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_061",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A glicose tem 6 carbonos. Analise cada carbono para identificar os quirais: • C1: aldeído (C=O com H) → sp2, não quiral. • C2, C3, C4, C5: cada um tem -OH, -H e duas cadeias diferentes → 4 grupos diferentes = QUIRAIS. • C6: -CH₂OH → ligado a 2H iguais = não quiral. Total: 4 carbonos quirais (C2, C3, C4, C5). Isso gera até 2⁴ = 16 estereoisômeros possíveis para a glicose.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 62
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "amida",
                "amina terciária",
                "imina",
                "anilina"
            },
            correctIndex = 2,
            questionNumber = 62,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer62",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_062",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A adenina é uma base nitrogenada com anel imidazol e anel pirimidínico fundidos (purina). Grupos visíveis na adenina: -NH₂ (amina primária aromática), N-H do anel (amina secundária aromática). O terceiro tipo de N na adenina: N=C dentro do anel aromático, com dupla ligação carbono-nitrogênio. Ligação C=N → grupo imina. Na adenina, os N do anel participam de ligações duplas C=N deslocalizadas = iminas aromáticas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 63
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Estruturas moleculares de álcoois nunca possuem anéis aromáticos",
                "Porque a hidroxila está fora do anel aromático",
                "Porque está faltando um grupo NH no anel aromático",
                "Porque a hidroxila não está ligada em um carbono com geometria sp3"
            },
            correctIndex = 3,
            questionNumber = 63,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer63",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_063",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analisar",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Definição de álcool: -OH ligado a carbono sp3 (4 ligações simples, geometria tetraédrica). No fenol: -OH está ligado diretamente ao carbono do anel benzênico, que é sp2 (participa da dupla ligação do anel). Carbono sp2 ≠ carbono sp3 → a condição de álcool não é satisfeita. Álcoois benzílicos (R = CH₂-C₆H₅, com -OH fora do anel em carbono sp3) SÃO álcoois. É a hibridação do carbono que importa.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 64
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Hidrocarbonetos saturados de cadeia longa tendem a ser sólidos à temperatura ambiente, enquanto hidrocarbonetos insaturados de cadeia semelhante são líquidos. O que melhor explica essa diferença?",
            answers = new string[] {
                "Hidrocarbonetos insaturados têm maior massa molecular",
                "As duplas ligações criam dobras na cadeia, impedindo o empacotamento ordenado entre as moléculas e enfraquecendo as forças de dispersão",
                "Hidrocarbonetos saturados são mais polares e formam ligações de hidrogênio",
                "Hidrocarbonetos insaturados possuem mais átomos de oxigênio em sua estrutura"
            },
            correctIndex = 1,
            questionNumber = 64,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_064",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Isso é fundamental para entender lipídios: gorduras saturadas vs insaturadas! Cadeia saturada (só C-C simples): linear e flexível → moléculas se empacotam próximas → fortes forças de dispersão → sólido. Cadeia insaturada (C=C): a dupla ligação cria uma DOBRA rígida (especialmente cis) → empacotamento impedido → forças mais fracas → líquido. Hidrocarbonetos: sem O, sem N — sem ligação de H. O que varia é apenas o empacotamento molecular.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 65
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O éter tem mais carbonos que o etanol",
                "O etanol é capaz de formar ligações de hidrogênio intermoleculares; o éter, não",
                "O éter é mais polar que o etanol",
                "O etanol possui ligação dupla C=C"
            },
            correctIndex = 1,
            questionNumber = 65,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_065",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Etanol (C₂H₅OH, PE=78°C) e éter dimetílico (CH₃-O-CH₃, PE=-24°C) têm a mesma fórmula C₂H₆O. Etanol: -OH com H disponível → doa E aceita ligação de hidrogênio com outras moléculas → forte coesão intermolecular. Éter: O aceita H, mas sem H ligado ao O → só aceita ligação de H → coesão muito mais fraca. Resultado: etanol precisa de muito mais energia para evaporar (PE muito maior) apesar de mesma massa molecular.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 66
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Dois compostos podem ter a mesma fórmula molecular e os mesmos grupos funcionais, mas serem moléculas completamente diferentes no espaço. Por que essa diferença espacial (estereoisomeria) é relevante em química orgânica?",
            answers = new string[] {
                "Porque estereoisômeros sempre têm pontos de fusão idênticos e são intercambiáveis",
                "Porque a disposição espacial dos grupos ao redor de um carbono quiral gera moléculas não sobreponíveis, com propriedades distintas em ambientes assimétricos",
                "Porque estereoisômeros diferem no número de átomos de carbono",
                "Porque a estereoisomeria só ocorre em compostos com dupla ligação C=C"
            },
            correctIndex = 1,
            questionNumber = 66,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_066",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Enantiômeros (estereoisômeros por carbono quiral): imagens especulares não sobreponíveis. Em meios simétricos (solventes): propriedades físicas idênticas (PE, densidade, solubilidade). Em ambientes assimétricos (enzimas, receptores biológicos): interagem de forma completamente diferente! Exemplo clássico: L-aminoácidos são usados pelas células; D-aminoácidos geralmente não. Um fármaco pode ser eficaz em uma configuração e tóxico na outra (ex: talidomida).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 67
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Porque ácidos carboxílicos possuem mais carbonos",
                "Porque ácidos carboxílicos formam dímeros estabilizados por duas ligações de hidrogênio simultâneas",
                "Porque o grupo carboxila é mais eletronegativo que a hidroxila",
                "Porque ácidos carboxílicos são compostos inorgânicos"
            },
            correctIndex = 1,
            questionNumber = 67,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_067",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Etanol (2C, PE=78°C) vs ácido acético (2C, PE=118°C): mesmo número de carbonos, PE muito diferente! Ácidos carboxílicos formam DÍMEROS: duas moléculas se unem por DUAS ligações de hidrogênio simultâneas (C=O...HO e HO...O=C). Isso efetivamente dobra o 'tamanho' das unidades que interagem → muito mais energia para separar. Álcoois também fazem ligação de H, mas apenas uma por par de moléculas — bem menos eficiente que o dímero do ácido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 68
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Porque o nitrogênio da amida tem geometria tetraédrica sp3",
                "Porque o par de elétrons do nitrogênio é parcialmente compartilhado com a carbonila, dando caráter de dupla ligação à ligação C–N e restringindo a rotação",
                "Porque a amida forma ligações de hidrogênio intramoleculares que fixam os átomos",
                "Porque o grupo carbonila atrai os átomos vizinhos por força eletrostática"
            },
            correctIndex = 1,
            questionNumber = 68,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_068",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A ligação amida é planar — isso é CRUCIAL para a estrutura de proteínas! Por que é planar? O par de elétrons do N se deslocaliza para o C=O (ressonância): C-N ↔ C=N⁻. Isso dá à ligação C-N caráter parcial de dupla ligação (≈40%) → rotação restringida → planaridade. Consequência biológica: toda a ligação peptídica é rígida e planar, definindo a conformação das proteínas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 69
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Forma-se um éter sulfurado; a reação é uma substituição nucleofílica",
                "Forma-se uma ligação dissulfeto (-S–S-), unindo dois tióis por oxidação e eliminando dois hidrogênios",
                "Forma-se um tioéter cíclico; a reação é uma condensação com perda de água",
                "Forma-se um ácido sulfônico; a reação é uma oxidação completa do enxofre"
            },
            correctIndex = 1,
            questionNumber = 69,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_069",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A reação mostrada: 2 R-SH + [O] → R-S-S-R + H₂O Dois tióis (-SH) são oxidados: cada S perde 1 H (oxidação), e os dois S se unem → ponte dissulfeto (-S-S-). Esta reação é FUNDAMENTAL em bioquímica: é o que estabiliza a estrutura 3D de proteínas (ex: insulina, anticorpos). A ponte dissulfeto entre cisteínas é reversível sob condições redutoras — importante para regulação de proteínas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 70
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Por que ésteres apresentam aroma característico, enquanto os ácidos carboxílicos correspondentes têm odor pungente e desagradável?",
            answers = new string[] {
                "Porque ésteres têm menor massa molecular",
                "Porque ésteres são apolares, mais voláteis e não formam ligações de hidrogênio fortes; os ácidos são polares e associam-se em dímeros",
                "Porque ácidos carboxílicos contêm enxofre em sua estrutura",
                "Porque ésteres possuem dupla ligação C=C"
            },
            correctIndex = 1,
            questionNumber = 70,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_070",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Ésteres: sem -OH livre → não formam ligações de H fortes entre si → mais voláteis → chegam facilmente ao nariz → aroma. Ácidos carboxílicos: -COOH forma dímeros por dupla ligação de H → menor volatilidade, mas quando voláteis interagem intensamente com receptores → odor forte. Exemplos de ésteres aromáticos: acetato de isoamila (banana), acetato de etila (frutas), acetato de benzila (jasmim). Sem enxofre em nenhum dos dois — o odor do ácido vem da interação polar intensa com a mucosa nasal.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 71
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Porque fenol não possui carbono em sua estrutura",
                "Porque no fenol o -OH está ligado diretamente a um carbono sp2 de anel aromático, alterando sua reatividade; no álcool, o -OH está em carbono sp3",
                "Porque fenol é um composto inorgânico",
                "Porque álcoois nunca possuem anel aromático"
            },
            correctIndex = 1,
            questionNumber = 71,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_071",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Ciclo-hexanol: -OH em carbono sp3 do anel saturado → álcool secundário cíclico. Fenol: -OH diretamente no carbono sp2 do anel benzênico → fenol (classe distinta). Por quê isso importa? O par de elétrons do O do fenol se deslocaliza para o anel → -OH mais ácido, reatividade diferente. Álcoois benzílicos (anel + CH₂-OH) SÃO álcoois — o -OH está no CH₂ (sp3), fora do anel. A posição do -OH é o que define a classe.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 72
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Uma molécula possui um carbono ligado a quatro grupos diferentes. O que essa característica implica sobre a molécula?",
            answers = new string[] {
                "A molécula necessariamente possui uma dupla ligação C=C",
                "Esse carbono é quiral, e a molécula existe como dois estereoisômeros não sobreponíveis (enantiômeros)",
                "A molécula não pode ser dissolvida em água",
                "Esse carbono tem hibridação sp2 e geometria plana"
            },
            correctIndex = 1,
            questionNumber = 72,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_072",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Carbono quiral (estereogênico): sp3 com 4 substituintes DIFERENTES → gera dois arranjos espaciais distintos. Esses dois arranjos são enantiômeros: imagens especulares não sobreponíveis (como mãos direita e esquerda). Carbono quiral tem hibridação sp3 (tetraédrico), não sp2. Não implica dupla ligação. A solubilidade em água depende dos grupos funcionais, não da quiralidade. Quiralidade é sobre disposição espacial.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 73
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Amidas possuem o grupo –CO–N–, enquanto iminas possuem o grupo –C=N–. Qual é a diferença estrutural fundamental entre esses dois grupos funcionais?",
            answers = new string[] {
                "Amidas contêm enxofre; iminas contêm oxigênio",
                "Na amida, o nitrogênio está ligado a uma carbonila por ligação simples; na imina, o nitrogênio faz uma ligação dupla diretamente com o carbono",
                "Iminas sempre possuem anel aromático; amidas nunca possuem",
                "Amidas são grupos funcionais de compostos inorgânicos; iminas são de compostos orgânicos"
            },
            correctIndex = 1,
            questionNumber = 73,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_073",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Amida: -CO-NH- → carbono sp2 faz dupla ligação com O (C=O) e ligação SIMPLES com N. Imina: -C=N- → carbono faz dupla ligação DIRETAMENTE com o nitrogênio (sem O intermediário). Ambas contêm N e C. Nenhuma contém S ou é inorgânica. Iminas podem ser cíclicas e aromáticas (piridina, adenina) ou abertas — não é uma regra. Amidas também podem ter anéis.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 74
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Carbonila (C=O) e amina (–NH₂); a combinação torna o composto básico",
                "Carbonila (C=O) e hidroxila (–OH); a combinação permite tanto doar próton quanto fazer ligações de hidrogênio, tornando o ácido mais polar que aldeídos e cetonas",
                "Carbonila (C=O) e tiol (–SH); a combinação torna o composto volátil",
                "Hidroxila (–OH) e amina (–NH₂); a combinação torna o composto anfótero"
            },
            correctIndex = 1,
            questionNumber = 74,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "biochem_074",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "understand",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O grupo carboxila (-COOH) é uma combinação única: carbonila (C=O) + hidroxila (-OH) no MESMO carbono. A presença do C=O polariza e enfraquece a ligação O-H → o H do -OH é facilmente doado (comportamento ácido). Isso distingue ácidos carboxílicos de aldeídos/cetonas (sem -OH) e de álcoois (sem C=O). O -COOH também faz fortes ligações de H (formando dímeros), elevando muito o ponto de ebulição.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 75
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O composto é uma cetona",
                "O composto é um ácido carboxílico",
                "O composto é um aldeído",
                "O composto é uma amina"
            },
            correctIndex = 1,
            questionNumber = 75,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_075",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "apply",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A imagem mostra: ? + R-OH → H₂O + produto com grupo -COO-. Raciocine: éster (-COO-) = ácido carboxílico + álcool → éster + água. Se o produto é éster (-COO-) e um dos reagentes é álcool (R-OH), o outro reagente é o ÁCIDO. Ácido carboxílico + álcool → éster + água (reação de esterificação de Fischer). O composto desconhecido é ácido carboxílico (-COOH).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 76
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Observe as quatro estruturas moleculares. Identifique qual delas possui simultaneamente um grupo carbonila e uma hidroxila ligados ao mesmo carbono",
            answers = new string[] {
                "AnswerImages/IntroductionDB/2-butanona",
                "AnswerImages/IntroductionDB/propanal",
                "AnswerImages/IntroductionDB/propanol",
                "AnswerImages/IntroductionDB/acido-propanoico"
            },
            correctIndex = 3,
            questionNumber = 76,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer76",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_076",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "apply",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A questão pede: C=O e -OH no MESMO carbono. 2-butanona: apenas C=O (cetona), sem -OH. Propanol: apenas -OH (álcool), sem C=O. Propanal: C=O com H na extremidade (aldeído) — o carbono tem C=O e H, não -OH. Ácido propanoico (-COOH): o carbono carboxílico tem C=O E -OH no mesmo carbono = grupo carboxila. Apenas nele as duas funções coexistem no mesmo carbono.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 77
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A e B, pois ambas possuem apenas carbonila",
                "A e B, pois têm a mesma fórmula molecular mas grupos funcionais diferentes (–COOH e –COO–)",
                "B e C, pois ambas possuem oxigênio",
                "A e C, pois ambas possuem grupo –OH"
            },
            correctIndex = 1,
            questionNumber = 77,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer77",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_077",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analyze",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "As três estruturas têm C₄H₈O₂. Analise os grupos funcionais: A: éster (-COO-) → sem -OH livre, sem -COOH. B: ácido carboxílico (-COOH) → tem C=O e -OH no mesmo C. C: outra combinação de C₄H₈O₂ (possivelmente outra posição de -COOH ou -COO-). Isômeros funcionais: mesma fórmula molecular, grupos funcionais DIFERENTES. A (éster) e B (ácido) têm C₄H₈O₂ mas funções distintas = isômeros funcionais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 78
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A e B são equivalentes porque ambos possuem nitrogênio ligado a hidrogênio",
                "A é uma amida; B é uma amina; C é uma imina; D é uma amina quaternária",
                "C e D são a mesma classe funcional porque ambos não possuem hidrogênio no nitrogênio",
                "B e C são isômeros funcionais porque possuem a mesma fórmula molecular"
            },
            correctIndex = 1,
            questionNumber = 78,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer78",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_078",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "analyze",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A imagem mostra 4 compostos nitrogenados (A=amida, B=amina, C=imina/anel, D=quaternária): A: carbonila + NH₂ → amida (-CO-NH₂). B: NH₂ em cadeia aberta → amina primária. C: anel com N=C → imina aromática (ex: piridina ou similar). D: N⁺ com 4 carbonos → amina quaternária. C e D NÃO são a mesma classe — imina (C=N) ≠ amina quaternária (N⁺). N sem H pode ser imina, terciária ou quaternária.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 79
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Um aluno afirma: 'Todo composto que contém oxigênio e carbono é necessariamente polar e solúvel em água.' Avalie essa afirmação.",
            answers = new string[] {
                "Verdadeira, pois todo composto com oxigênio forma ligações de hidrogênio com a água",
                "Falsa, pois compostos como ésteres e éteres de cadeia longa contêm oxigênio mas são predominantemente apolares e pouco solúveis em água",
                "Verdadeira apenas para compostos com grupo carbonila",
                "Falsa apenas para compostos com mais de 10 carbonos"
            },
            correctIndex = 1,
            questionNumber = 79,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_079",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "evaluate",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A solubilidade em água depende do BALANÇO entre a parte polar (grupo funcional) e a parte apolar (cadeia carbônica). Éster de cadeia longa (ex: triglicerídeo): tem -COO- (polar) mas a cadeia de 16-18C domina → apolar → insolúvel em água. Éter de cadeia longa: idem — O presente mas cadeia apolar domina. A regra geral: se a cadeia carbônica for grande o suficiente, ela supera o efeito do grupo polar. Presença de O não garante solubilidade.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 80
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O composto B, pois o tioéter é mais reativo e mais volátil que o tiol",
                "O composto A, pois o grupo –SH do tiol confere odor característico intenso e pode ser oxidado a dissulfeto",
                "Ambos têm odor e reatividade equivalentes por possuírem enxofre",
                "O composto B, pois o enxofre entre dois carbonos é mais eletronegativo"
            },
            correctIndex = 1,
            questionNumber = 80,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer80",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_080",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "evaluate",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Tiol (A, R-SH) vs tioéter (B, R-S-R'): ambos têm S, mas reatividades distintas. Tiol: o H do -SH é facilmente removido → pode ser oxidado (2 R-SH → R-S-S-R) e tem odor intensíssimo (gambá, gás natural). Tioéter: S protegido entre dois carbonos → menos reativo, odor menos intenso que tiol. A eletronegatividade do S não aumenta por estar entre dois carbonos. Tioéteres são mais estáveis e menos voláteis que tióis.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 81
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Avalie a seguinte afirmação: 'Isômeros cis e trans de um alceno possuem as mesmas propriedades físicas, pois têm a mesma fórmula molecular e o mesmo tipo de ligação.'",
            answers = new string[] {
                "Verdadeira, pois a fórmula molecular determina todas as propriedades",
                "Falsa, pois a disposição espacial dos grupos em torno da dupla ligação resulta em diferentes momentos de dipolo, pontos de ebulição e solubilidade",
                "Verdadeira apenas para alcenos com mais de 6 carbonos",
                "Falsa apenas quando os substituintes são grupos funcionais polares"
            },
            correctIndex = 1,
            questionNumber = 81,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_081",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "evaluate",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Isômeros cis/trans: mesma fórmula, mesma conectividade, mas diferente disposição espacial. Diferença de dipolo: cis-2-buteno tem dipolos que se somam → polar; trans tem dipolos que se cancelam → apolar. Resultado: pontos de ebulição diferentes, solubilidades diferentes, reatividades diferentes. Relevância biológica: ácidos graxos cis (membrana fluida) vs trans (mais rígidos, artificiais) têm efeitos fisiológicos opostos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 82
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O ácido tem maior ponto de ebulição porque tem maior massa molecular que os outros dois",
                "O ácido tem maior ponto de ebulição porque forma dímeros por ligação de hidrogênio; o éster tem o menor porque não forma ligações de hidrogênio;",
                "O Éster tem menor ponto de ebulição porque possui mais oxigênios",
                "O álcool tem ponto de ebulição maior que o éster porque o álcool é mais pesado"
            },
            correctIndex = 1,
            questionNumber = 82,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer82",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_082",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "evaluate",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A imagem mostra: etanol (78°C) < ácido acético (118°C) e éster com PE baixo (~77°C). Éster: sem -OH livre → não doa H para ligação de H → interações fracas → PE mais baixo. Álcool: -OH doa e aceita H → ligações de H intermoleculares → PE moderado. Ácido carboxílico: forma DÍMEROS com duas ligações de H simultâneas → precisa de muito mais energia → PE mais alto. Massa molecular não é o fator determinante aqui.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 83
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Um aluno afirma: 'A presença de um carbono quiral em uma molécula garante que ela será biologicamente ativa.' Avalie essa afirmação.",
            answers = new string[] {
                "Verdadeira, pois carbono quiral sempre gera atividade óptica em sistemas biológicos",
                "Parcialmente correta: carbono quiral gera enantiômeros com potencial de interação diferencial em ambientes assimétricos, mas a atividade biológica depende do contexto.",
                "Falsa, pois carbono quiral não tem relação com atividade biológica",
                "Verdadeira apenas para moléculas com mais de um carbono quiral"
            },
            correctIndex = 1,
            questionNumber = 83,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_083",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "evaluate",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Carbono quiral gera enantiômeros que podem interagir diferentemente com enzimas e receptores (assimétricos). Mas 'atividade biológica' não é garantida apenas pela quiralidade — depende da estrutura completa e do receptor. Exemplo: glicina (sem C quiral) é biologicamente ativa. Muitos compostos aquirais são fármacos eficazes. A afirmação é PARCIALMENTE correta: quiralidade é importante para a especificidade, mas não é condição suficiente para atividade biológica.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 84
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Um estudante afirma: 'Amida e amina são grupos funcionais equivalentes porque ambos possuem nitrogênio ligado a hidrogênio. Avalie essa afirmação e assinale a alternativa verdadeira.",
            answers = new string[] {
                "Verdadeira, pois todo nitrogênio com par livre é igualmente básico",
                "Falsa: embora ambos possuam par de elétrons no nitrogênio, na amida (A) esse par está deslocalizado com a carbonila, tornando-a muito menos básica que a amina (B)",
                "Verdadeira apenas para a amida, pois o oxigênio da carbonila aumenta a basicidade do nitrogênio",
                "Falsa: nenhum dos dois é básico, pois nitrogênio orgânico não doa elétrons"
            },
            correctIndex = 1,
            questionNumber = 84,
            isImageAnswer = false,
            isImageQuestion = true,
            questionImagePath = "QuestionImages/IntroductionDB/introductionDB_ImageQuestionContainer84",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_084",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "evaluate",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aminas: par de elétrons do N disponível → bom nucleófilo e base (pKa do ácido conjugado ~10). Amidas: par de elétrons do N deslocalizado para o C=O (ressonância) → N muito menos básico (pKa ~0). Por isso, proteínas (com ligações amida) não são básicas nos N das ligações peptídicas — apenas nas aminas terminais e cadeias laterais. Ambos têm N-H e par de elétrons, mas a ressonância da amida torna o par do N indisponível para protonação.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        // Questão 85
        new Question
        {
            questionDatabankName = "BiochemistryIntroductionQuestionDatabase",
            questionText = "Considere a seguinte afirmação: 'Para classificar um composto orgânico, basta identificar os átomos presentes em sua fórmula molecular.' Avalie se essa estratégia é suficiente.",
            answers = new string[] {
                "Sim, pois a fórmula molecular contém toda a informação necessária para classificar um composto",
                "Não, pois compostos com a mesma fórmula molecular podem ter grupos funcionais diferentes (isômeros funcionais) e, portanto, classes e propriedades distintas — é necessário analisar a estrutura",
                "Sim, mas apenas para compostos com menos de 4 carbonos",
                "Não, mas apenas quando o composto possui nitrogênio ou enxofre"
            },
            correctIndex = 1,
            questionNumber = 85,
            isImageAnswer = false,
            isImageQuestion = false,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "biochem_085",
            topic = "biochem",
            subtopic = null,
            displayName = "Introdução à Bioquímica",
            bloomLevel = "evaluate",
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A fórmula molecular é insuficiente para classificar — ela apenas informa quais e quantos átomos existem. Exemplo: C₂H₆O pode ser etanol (álcool) ou éter dimetílico (éter) — mesma fórmula, classes completamente diferentes. Para classificar: é obrigatório analisar a fórmula estrutural (como os átomos estão conectados) e identificar os grupos funcionais. Esta é a lição central da química orgânica: estrutura determina função — não apenas composição.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.biochem;
    public string GetDatabankName() => "BiochemistryIntroductionQuestionDatabase";
    public string GetDisplayName() => "Introdução à Bioquímica";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
