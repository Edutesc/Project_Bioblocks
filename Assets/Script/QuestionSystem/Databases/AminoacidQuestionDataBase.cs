using System.Collections.Generic;
using QuestionSystem;

public class AminoacidQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que define um aminoácido?",
            answers = new string[] {
                "Uma molécula orgânica com um grupo amino e um grupo carboxila.",
                "Uma molécula inorgânica com um grupo amino e um grupo carboxila.",
                "Uma molécula orgânica com apenas um grupo amino.",
                "Uma molécula inorgânica com apenas um grupo carboxila."
            },
            correctIndex = 0,
            questionNumber = 1,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_001",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Um aminoácido pode ser definido como uma molécula orgânica que possui simultaneamente um grupo amino (-NH₂) e um grupo carboxila (-COOH) ligados ao mesmo carbono central (carbono alfa), além de um átomo de hidrogênio e uma cadeia lateral variável (radical R). Essas moléculas são consideradas orgânicas porque são formadas por átomos de carbono ligados covalentemente entre si ou a outros elementos como hidrogênio, oxigênio, nitrogênio, etc. A presença conjunta do grupo amino (de caráter básico) e do grupo carboxila (de caráter ácido) é a característica fundamental que define um aminoácido. Retirado de “Princípios de Bioquímica de Lehninger”, ed. 6, pg. 76",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual o papel principal dos aminoácidos?",
            answers = new string[] {
                "Formar carboidratos.",
                "Formar lipídios.",
                "Formar proteínas.",
                "Formar ácidos nucléicos."
            },
            correctIndex = 2,
            questionNumber = 2,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_002",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos são as unidades estruturais básicas das proteínas. Eles podem se ligar entre si por meio de ligações peptídicas e formar cadeias polipeptídicas que originam as proteínas. Os aminoácidos também podem cumprir outras funções nos sistemas biológicos, porém, seu papel principal é servir como “blocos de construção\" das proteínas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique o aminoácido cuja cadeia lateral apresenta característica polar não carregada.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/glicina",
                "AnswerImages/AminoacidsDB/aminoacid_images/histidina",
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina"
            },
            correctIndex = 0,
            questionNumber = 3,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_003",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A treonina é um aminoácido cuja cadeia lateral contém um grupo hidroxila (-OH), o que a torna polar. No entanto, essa cadeia lateral não apresenta carga elétrica em pH fisiológico, sendo classificada como polar não carregada. Por sua vez, a glicina e a alanina são aminoácidos classificados como apolar, o primeiro por possuir uma cadeia lateral formada apenas por um átomo hidrogênio e o segundo por ter uma cadeia lateral metil (-CH₃). Já a histidina apresenta um grupo imidazol que pode se protonar, sendo classificada como polar carregada.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que diferencia um aminoácido do outro?",
            answers = new string[] {
                "O grupo amino.",
                "O grupo carboxila.",
                "A sua cadeia lateral (R).",
                "O átomo de carbono alfa."
            },
            correctIndex = 2,
            questionNumber = 4,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_004",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Todos os aminoácidos possuem uma estrutura básica em comum: um carbono central (carbono alfa) ligado a um grupo amino (–NH₂), a um grupo carboxila (–COOH), a um átomo de hidrogênio e a uma cadeia lateral variável (radical R). Exceto o último, todos esses grupos são partes fixas da estrutura e estão presentes em todos os aminoácidos. Portanto, o que realmente diferencia um aminoácido do outro é a cadeia lateral. Essa cadeia pode variar em tamanho, forma, carga elétrica e polaridade, determinando características como: se o aminoácido é polar ou apolar; se possui carga positiva, negativa ou neutra; e se é hidrofílico ou hidrofóbico.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique o alfa-aminoácido abaixo",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina",
                "AnswerImages/AminoacidsDB/moleculas_organicas/3-amino-2-butanona",
                "AnswerImages/AminoacidsDB/moleculas_organicas/beta-alanina",
                "AnswerImages/AminoacidsDB/moleculas_organicas/2-amino-propanoato_de_metila"
            },
            correctIndex = 0,
            questionNumber = 5,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_005",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Um α-aminoácido é aquele em que o grupo amino (-NH₂) está ligado ao carbono alfa, ou seja, ao carbono imediatamente adjacente ao grupo carboxila (-COOH). A alanina apresenta exatamente essa configuração: o grupo amino e o grupo carboxila estão ligados ao mesmo carbono central (carbono α), caracterizando-a como um α-aminoácido típico. Quanto a 3-amino-2-butanona, não se trata de um aminoácido. Mesmo que sua estrutura apresenta algumas similaridades com a estrutura básica de um aminoácido (grupos amino e carbonila), como não possui o grupo carboxila característico, não é um aminoácido. A beta-alanina é um aminoácido, porém o grupo amino está ligado ao carbono beta, e não ao carbono alfa, portanto não é um α-aminoácido. Por fim, 2-amino-propanoato de metila é derivado de aminoácido. O grupo carboxila está modificado (o hidrogênio foi substituído por um grupo metila), formando um éster, portanto, não um aminoácido livre.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com cadeias laterais alifáticas são:",
            answers = new string[] {
                "Polares.",
                "Apolares.",
                "Carregados positivamente.",
                "Carregados negativamente."
            },
            correctIndex = 1,
            questionNumber = 6,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_006",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A classificação dos aminoácidos depende das características químicas de suas cadeias laterais (radicais R). Quando a cadeia lateral é alifática, ela é formada por cadeias de carbono e hidrogênio, sem grupos funcionais capazes de formar cargas elétricas ou interagir fortemente com a água. Como consequência, aminoácidos com cadeias laterais alifáticas são classificados como apolares.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique o aminoácido que absorve o comprimento de onda de 280 nm.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/cisteina",
                "AnswerImages/AminoacidsDB/aminoacid_images/fenilalanina"
            },
            correctIndex = 3,
            questionNumber = 7,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_007",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A absorção de luz no comprimento de onda de 280 nm está relacionada à presença de anéis aromáticos na estrutura do aminoácido. Dentre os 20 aminoácidos comuns, os únicos que apresentam grupos R aromáticos são fenilalanina, tirosina e triptofano.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/moleculas_organicas/d-alanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/cisteina",
                "AnswerImages/AminoacidsDB/moleculas_organicas/d-alanina"
            },
            correctIndex = 3,
            questionNumber = 8,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer8",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_008",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Duas moléculas são enantiômeros uma da outra quando possuem a mesma fórmula molecular e sequência de ligações, mas diferem na orientação espacial (como imagens no espelho que não podem ser sobrepostas).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "pH = 2,3",
                "pH = 6,0",
                "pH = 7,0",
                "pH = 9,7"
            },
            correctIndex = 1,
            questionNumber = 9,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer9",
            questionLevel = 2,
            questionInDevelopment = true,
            globalId = "aminoacids_009",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O aminoácido valina será considerado neutro quando sua carga líquida for zero. Nessa condição, a valina está na forma zwitteriônica, com o grupo amino protonado (-NH₃⁺) e o grupo carboxila desprotonado (-COO⁻), cujas cargas se anulam. Como a valina não possui cadeia lateral ionizável, o pH em que isso ocorre é calculado pela média dos pKas (constantes de dissociação) do grupo carboxila e do grupo amino. (pK1+pK2)/2 = (2,3+9,7)/2 = 12/2 = 6",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina_zw",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina_positiva",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina_negativa"
            },
            correctIndex = 2,
            questionNumber = 10,
            answerType = AnswerType.Image,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer10",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_010",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para entender o estado de protonação da isoleucina nessa situação, é importante lembrar que o pKa corresponde ao valor de pH no qual um grupo químico está 50% protonado e 50% desprotonado, servindo como referência para prever se um grupo tende a ganhar ou perder prótons. Para valores de pH menores do que o pKa, a forma protonada predomina, já para valores de pH maiores do que o pKa, a forma desprotonada predomina. Em pH = 1, o meio é fortemente ácido (com alta concentração de íons H⁺) e os grupos funcionais tendem a ser protonados. Como a isoleucina possui pKa = 2,3 para o grupo carboxila (–COOH/–COO⁻) e pKa = 9,8 para o grupo amino (–NH₃⁺/–NH₂), e pH = 1 é menor que ambos os valores de pKa, tanto o grupo carboxila quanto o grupo amino permanecem protonados (–COOH e –NH₃⁺, respectivamente), resultando em uma molécula com carga líquida positiva (+1), de modo que a forma predominante é a protonada positiva",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
         new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina_zw",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina_positiva",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina_negativa"
            },
            correctIndex = 1,
            questionNumber = 11,
            answerType = AnswerType.Image,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer11",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_011",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para entender o estado de protonação da prolina nessa situação, é importante lembrar que o pKa corresponde ao valor de pH no qual um grupo químico está 50% protonado e 50% desprotonado, servindo como referência para prever se um grupo tende a ganhar ou perder prótons. Para valores de pH menores do que o pKa, a forma protonada predomina, já para valores de pH maiores do que o pKa, a forma desprotonada predomina. A prolina possui pKa₁ = 2,0 para o grupo carboxila (–COOH/–COO⁻) e pKa₂ = 10,6 para o grupo amino cíclico (–NH₂⁺/–NH) e queremos saber seu estado de protonação em pH = 6,3 (meio próximo da neutralidade). Como pH > pKa1 e pH < pKa2, o grupo carboxila estará predominantemente desprotonado (–COO⁻), enquanto o grupo amino permanecerá protonado (–NH₂⁺), resultando em uma molécula com cargas opostas que se anulam, caracterizando a forma zwitteriônica (carga líquida zero).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "pH = 5,5",
                "pH = 9,0",
                "pH = 10,7",
                "pH = 12,5"
            },
            correctIndex = 2,
            questionNumber = 12,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer12",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_012",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para determinar o pH em que a arginina está totalmente neutra (ou seja, com carga líquida zero), é importante lembrar que a forma neutra de um aminoácido ocorre, em geral, entre os pKas que envolvem a perda sequencial de prótons. A arginina possui três grupos ionizáveis: pK₁ = 2 (grupo carboxila, –COOH/–COO⁻), pK₂ = 9 (grupo amino, –NH₃⁺/–NH₂) e pKR = 12,5 (grupo guanidínio da cadeia lateral, carregado positivamente quando protonado). Em pH baixo, a molécula é altamente protonada e positiva, e à medida que o pH aumenta, primeiro a carboxila perde próton (ficando –COO⁻), depois o grupo amino perde próton (–NH₂), e por último o grupo da cadeia lateral; a forma com carga líquida zero ocorre entre os pKa₂ e pKR, portanto o pH isoelétrico (pI) pode ser estimado pela média desses dois valores: (9 + 12,5)/2 = 10,75 ≈ 10,7, indicando que nesse pH as cargas positivas e negativas se equilibram",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "pH = 3,0",
                "pH = 5,5",
                "pH = 3,9",
                "pH = 9,8"
            },
            correctIndex = 0,
            questionNumber = 13,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer13",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_013",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O ponto isoelétrico (pI) é o pH no qual o aminoácido apresenta carga líquida zero, e para determiná-lo é necessário considerar os valores de pKa dos grupos ionizáveis. No caso do ácido aspártico, há três grupos: o grupo carboxila α (pK₁ = 2,1), o grupo carboxila da cadeia lateral (pKR = 3,9) e o grupo amino (pK₂ = 9,8); como se trata de um aminoácido ácido (com dois grupos carboxila), o pI é calculado pela média dos dois menores pKa, pois são eles que delimitam a faixa de pH em que a molécula passa pelo estado de carga zero; assim, pI = (2,1 + 3,9) / 2 = 3,0",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos aspártico e glutâmico possuem:",
            answers = new string[] {
                "Um grupo carboxila no radical R.",
                "Um grupo amino no radical R.",
                "Um grupo sulfidrila no radical R.",
                "Um anel aromático no radical R."
            },
            correctIndex = 0,
            questionNumber = 14,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_014",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos aspártico e glutâmico pertencem ao grupo dos aminoácidos ácidos. Isso significa que, além do grupo carboxila comum a todos os aminoácidos (ligado ao carbono alfa), eles apresentam um grupo carboxila adicional no radical R. Essa característica confere a ambos uma carga negativa em pH fisiológico, já que o grupo carboxila tende a perder prótons (H⁺), tornando-os importantes para processos de interação eletrostática em proteínas e para o equilíbrio ácido-base no organismo.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido cuja cadeia lateral é considerada básica",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina",
                "AnswerImages/AminoacidsDB/aminoacid_images/isoleucina",
                "AnswerImages/AminoacidsDB/aminoacid_images/acido_aspartico",
                "AnswerImages/AminoacidsDB/aminoacid_images/arginina"
            },
            correctIndex = 3,
            questionNumber = 15,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_015",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para identificar o aminoácido com cadeia lateral básica a partir das estruturas moleculares, analisa-se os grupos funcionais presentes nas cadeias laterais, buscando aqueles capazes de aceitar prótons (H⁺) e adquirir carga positiva. Cadeias laterais básicas apresentam átomos de nitrogênio adicionais com possibilidade de protonação. Entre as opções, a prolina e a isoleucina possuem cadeias laterais apolares e sem grupos ionizáveis, enquanto o ácido aspártico apresenta um grupo carboxila, característico de cadeia ácida (que perde H⁺ e fica negativamente carregada). Por último, a arginina possui uma cadeia lateral com grupo guanidínio, rico em nitrogênios e com alta capacidade de protonação, permanecendo positivamente carregado em pH fisiológico, sendo, portanto, um aminoácido com cadeia lateral básica.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido cuja cadeia lateral é considerada ácida",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/fenilalanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina",
                "AnswerImages/AminoacidsDB/aminoacid_images/acido_aspartico",
                "AnswerImages/AminoacidsDB/aminoacid_images/arginina"
            },
            correctIndex = 2,
            questionNumber = 16,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_016",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para identificar o aminoácido com cadeia lateral ácida a partir das estruturas moleculares, é necessário observar a presença de grupos funcionais capazes de doar prótons (H⁺). Entre as opções apresentadas, fenilalanina e alanina possuem cadeias laterais apolares e não ionizáveis, enquanto a arginina apresenta uma cadeia lateral básica, com grupo guanidínio que tende a se protonar e ficar positivamente carregado, já o ácido aspártico possui uma cadeia lateral com grupo carboxila adicional, característico de aminoácidos ácidos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido cuja cadeia lateral apresenta um grupo funcional álcool.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/tirosina",
                "AnswerImages/AminoacidsDB/aminoacid_images/prolina",
                "AnswerImages/AminoacidsDB/aminoacid_images/treonina",
                "AnswerImages/AminoacidsDB/aminoacid_images/leucina"
            },
            correctIndex = 2,
            questionNumber = 17,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_017",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para identificar o aminoácido cuja cadeia lateral apresenta um grupo funcional álcool, é necessário observar a presença de um grupo hidroxila (–OH) ligado a um carbono saturado na cadeia lateral. Entre as opções apresentadas, a tirosina possui uma cadeia aromática com grupo fenólico (–OH ligado ao anel aromático), que não é classificado como álcool alifático, enquanto a prolina e a leucina possuem cadeias laterais apolares sem grupos funcionais oxigenados. Por fim, a treonina apresenta em sua cadeia lateral um grupo hidroxila ligado a um carbono saturado, caracterizando um grupo funcional álcool.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em pH ácido, o estado de protonação da maioria dos aminoácidos presentes na solução terá carga líquida:",
            answers = new string[] {
                "Negativa",
                "Neutra",
                "Positiva",
                "Variável"
            },
            correctIndex = 2,
            questionNumber = 18,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_018",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Em meio ácido (pH baixo), há alta concentração de íons H⁺ na solução. Esse cenário favorece a protonação dos grupos ionizáveis dos aminoácidos, especialmente o grupo amino (–NH₂), que tende a se transformar em –NH₃⁺, enquanto os grupos carboxila (–COO⁻) são mais facilmente convertidos para a forma neutra –COOH. Como resultado, a maioria dos aminoácidos apresenta predominância de cargas positivas, pois há ganho de prótons sem compensação equivalente por desprotonação.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Identifique abaixo o aminoácido que absorve luz de comprimento de onda 280 nm.",
            answers = new string[] {
                "AnswerImages/AminoacidsDB/aminoacid_images/triptofano",
                "AnswerImages/AminoacidsDB/aminoacid_images/glutamina",
                "AnswerImages/AminoacidsDB/aminoacid_images/glicina",
                "AnswerImages/AminoacidsDB/aminoacid_images/alanina"
            },
            correctIndex = 0,
            questionNumber = 19,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_019",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A absorção de luz no comprimento de onda de 280 nm está relacionada à presença de anéis aromáticos na estrutura do aminoácido. Dentre os 20 aminoácidos comuns, os únicos que apresentam grupos R aromáticos são fenilalanina, tirosina e triptofano.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em pH básico, o estado de protonação da maioria dos aminoácidos presentes na solução terá carga líquida:",
            answers = new string[] {
                "Positiva",
                "Neutra",
                "Negativa",
                "Variável"
            },
            correctIndex = 2,
            questionNumber = 20,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_020",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Em meio básico (pH alto), há baixa concentração de íons H⁺ na solução, o que favorece a desprotonação dos grupos ionizáveis dos aminoácidos, especialmente o grupo carboxila (–COOH), que perde próton e passa para a forma carregada negativamente (–COO⁻). O grupo amino (–NH₃⁺), por sua vez, tende a perder próton e se converter em –NH₂, reduzindo a carga positiva. Como consequência, há predominância de espécies com carga negativa na maioria dos aminoácidos, pois os grupos carboxila desprotonados se tornam mais prevalentes do que os grupos amino protonados.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que é um carbono quiral?",
            answers = new string[] {
                "Um carbono ligado a quatro átomos diferentes.",
                "Um carbono ligado a dois átomos iguais.",
                "Um carbono com dupla ligação.",
                "Um carbono com tripla ligação."
            },
            correctIndex = 0,
            questionNumber = 21,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_021",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Um carbono quiral é definido como sendo um átomo de carbono que possui quatro ligantes diferentes. Essa característica faz com que ele seja um centro de assimetria na molécula, permitindo a existência de isômeros ópticos (enantiômeros), que são moléculas com a mesma fórmula molecular e sequência de ligações, mas que diferem na orientação espacial (como imagens no espelho que não podem ser sobrepostas).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Uma molécula com um carbono quiral é:",
            answers = new string[] {
                "Apolar",
                "Assimétrica",
                "Linear",
                "Simétrica"
            },
            correctIndex = 1,
            questionNumber = 22,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_022",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Um carbono quiral é aquele que está ligado a quatro substituintes diferentes. A presença desse tipo de carbono cria uma assimetria estrutural na molécula, pois não existe um plano interno de simetria que divide a molécula em duas partes idênticas. Essa assimetria impede que a molécula seja sobreponível à sua imagem no espelho, originando os chamados isômeros ópticos (enantiômeros).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Isômeros que são imagens especulares um do outro, e NÃO são sobreponíveis:",
            answers = new string[] {
                "Enantiômeros",
                "Diasteroisômeros",
                "Isômeros constitucionais",
                "Isômeros conformacionais"
            },
            correctIndex = 0,
            questionNumber = 23,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_023",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Dois compostos são isômeros se apresentarem a mesma fórmula molecular mas possuírem estruturas diferentes. Existem diferentes tipos de isomeria. Na estereoisomeria (isomeria espacial) os dois compostos são diferenciados somente pelo arranjo espacial de seus átomos. Enantiômeros e diasteroisômeros são dois tipos de estereoisômeros. Em enantiômeros os isômeros se relacionam como objeto e imagem no espelho, não sendo sobreponíveis, enquanto os diasteroisômeros (isomeria cis-trans) não apresentam essa relação. Já os isômeros constitucionais são aqueles que possuem a mesma fórmula molecular, mas diferem na organização dos átomos, alterando a estrutura da cadeia. Por fim, os isômeros conformacionais correspondem a diferentes arranjos atômicos da mesma molécula, obtidos pela rotação em torno de ligações simples, sem que haja quebra de ligações químicas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "A designação D e L para aminoácidos se refere a:",
            answers = new string[] {
                "Sua composição química.",
                "Sua estrutura tridimensional.",
                "Sua solubilidade em água.",
                "Seu ponto isoelétrico."
            },
            correctIndex = 1,
            questionNumber = 24,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_024",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A designação D e L para aminoácidos está relacionada à estrutura tridimensional dessas moléculas, especificamente à orientação espacial do grupo funcional em torno do carbono quiral. Essa classificação tem origem na comparação com a molécula de gliceraldeído, servindo como referência para determinar a orientação dos grupos funcionais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Quais aminoácidos são encontrados principalmente nas proteínas?",
            answers = new string[] {
                "D-aminoácidos",
                "L-aminoácidos",
                "Ambos em quantidades iguais",
                "Depende do organismo"
            },
            correctIndex = 1,
            questionNumber = 25,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_025",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Nos sistemas biológicos, especialmente nas proteínas, há uma predominância quase absoluta dos L-aminoácidos. No decorrer da evolução dos sistemas biológicos as enzimas responsáveis pela síntese de proteínas se especializaram para essa forma, reconhecendo e incorporando apenas aminoácidos da configuração L durante a tradução genética. Embora D-aminoácidos possam ser encontrados em alguns contextos específicos, como na parede celular de certas bactérias, eles não são os principais constituintes das proteínas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos essenciais são aqueles que:",
            answers = new string[] {
                "Nosso corpo produz.",
                "Devem ser obtidos pela dieta.",
                "São encontrados em plantas.",
                "São encontrados em animais."
            },
            correctIndex = 1,
            questionNumber = 26,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_026",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos essenciais são aqueles que o organismo humano não consegue sintetizar em quantidade suficiente (ou não consegue produzir de forma alguma). Por isso, é indispensável que eles sejam obtidos por meio da alimentação.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos não-essenciais são aqueles que:",
            answers = new string[] {
                "Devem ser obtidos pela dieta.",
                "Nosso corpo produz.",
                "São encontrados apenas em animais.",
                "São encontrados apenas em plantas."
            },
            correctIndex = 1,
            questionNumber = 27,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_027",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos não essenciais são aqueles que o organismo humano é capaz de sintetizar por conta própria, a partir de outras moléculas presentes no metabolismo. Isso significa que, mesmo que não sejam ingeridos diretamente pela alimentação, o corpo consegue produzi-los em quantidades adequadas para suprir suas necessidades. Eles também podem ser obtidos pela dieta, mas isso não é obrigatório, ao contrário dos aminoácidos essenciais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Uma amida",
                "H<sup><size=150%> +</size></sup>",
                "Água",
                "OH<sup><size=150%> -</size></sup>"
            },
            correctIndex = 2,
            questionNumber = 28,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer28",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_028",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Na formação de um peptídeo ocorrem reações de condensação (ou desidratação), na qual o grupo carboxila (–COOH) de um aminoácido reage com o grupo amino (–NH₂) de outro, formando uma ligação peptídica (amida) e liberando uma molécula de água como subproduto. Assim, além de representar os dois aminoácidos reagentes e o dipeptídeo formado, é fundamental representar também a água (H₂O), que é eliminada durante o processo de formação da ligação peptídica.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
       new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual é o nome da ligação que ocorre entre os aminoácidos para forma proteínas",
            answers = new string[] {
                "Ponte de hidrogênio",
                "Ligação proteica",
                "Ligação peptídica",
                "Ligação eletrostática"
            },
            correctIndex = 2,
            questionNumber = 29,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_029",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos se unem para formar proteínas por meio de uma ligação específica chamada ligação peptídica. Essa ligação ocorre entre o grupo carboxila (-COOH) de um aminoácido e o grupo amino (-NH₂) de outro, com a liberação de uma molécula de água (reação de condensação). Esse tipo de ligação é fundamental para a formação das cadeias polipeptídicas, que posteriormente se organizam em estruturas mais complexas, dando origem às proteínas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual o nome do grupo funcional é criado pela condensação de dois aminoácidos para formar um peptídeo?",
            answers = new string[] {
                "Grupo funcional álcool",
                "Grupo funcional amina",
                "Grupo funcional ácido carboxílico",
                "Grupo funcional amida"
            },
            correctIndex = 3,
            questionNumber = 30,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_030",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Quando dois aminoácidos se unem por meio de uma ligação peptídica, ocorre uma reação entre o grupo carboxila (-COOH) de um e o grupo amino (-NH₂) de outro, com liberação de água. Como resultado dessa ligação, forma-se o grupo funcional amida (caracterizado pela presença de um grupo carbonila, C=O, ligado a um átomo de nitrogênio, -CONH-) na molécula.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "2 aminoácidos",
                "3 aminoácidos",
                "4 aminoácidos",
                "5 aminoácidos"
            },
            correctIndex = 2,
            questionNumber = 31,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer31",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_031",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Aminoácido",
                "Dipeptídeo",
                "Tripeptídeo",
                "Tetrapeptídeo"
            },
            correctIndex = 3,
            questionNumber = 32,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer32",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_032",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com carga líquida positiva em pH 7 são:",
            answers = new string[] {
                "Ácidos",
                "Básicos",
                "Apolares",
                "Neutros"
            },
            correctIndex = 1,
            questionNumber = 33,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_033",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos podem ser classificados de acordo com a carga elétrica de suas cadeias laterais (radicais). Aqueles que apresentam carga líquida positiva geralmente possuem grupos que podem aceitar prótons (H⁺), característica típica de substâncias básicas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com carga líquida negativa em pH 7 são:",
            answers = new string[] {
                "Básicos",
                "Ácidos",
                "Apolares",
                "Neutros"
            },
            correctIndex = 1,
            questionNumber = 34,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_034",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos podem ser classificados de acordo com a carga elétrica de suas cadeias laterais (radicais). Os aminoácidos com carga líquida negativa possuem cadeias laterais que tendem a perder prótons (H⁺), característica típica de substâncias ácidas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O ponto isoelétrico (pI) de um aminoácido é:",
            answers = new string[] {
                "O pH em que ele é completamente protonado.",
                "O pH em que ele é completamente desprotonado.",
                "O pH em que sua carga líquida é zero.",
                "O pH em que sua solubilidade é máxima."
            },
            correctIndex = 2,
            questionNumber = 35,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_035",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O ponto isoelétrico (pI) de um aminoácido é o valor de pH no qual a molécula apresenta carga líquida igual a zero. Nesse ponto, as cargas positivas e negativas presentes no aminoácido se equilibram, resultando em uma forma eletricamente neutra (zwitteriônica). Isso não significa que o aminoácido esteja completamente protonado ou desprotonado, mas sim que há um balanço entre as cargas opostas. Além disso, no pI, a solubilidade do aminoácido geralmente é mínima.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Para um aminoácido com dois pKs, o pI é calculado como:",
            answers = new string[] {
                "A média dos dois pKs.",
                "A diferença entre os dois pKs.",
                "O maior dos dois pKs.",
                "O menor dos dois pKs."
            },
            correctIndex = 0,
            questionNumber = 36,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_036",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Para aminoácidos, em geral, o ponto isoelétrico corresponde a média dos valores de pKa dos grupos amina e carboxila, ou seja, para aminoácidos com dois pKas, pI = (pKa1 + pKa2) / 2. Isso ocorre porque o pI representa o pH no qual a molécula apresenta carga líquida zero, o que acontece entre as duas constantes de dissociação relevantes para o aminoácido (geralmente a do grupo carboxila e a do grupo amino).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em uma titulação de um aminoácido, os platôs na curva representam:",
            answers = new string[] {
                "Mudanças rápidas de pH.",
                "Mudanças lentas de pH.",
                "Dissociação de grupamentos ionizáveis.",
                "Adição de ácido ou base."
            },
            correctIndex = 2,
            questionNumber = 37,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_037",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Em uma curva de titulação de um aminoácido, os platôs correspondem às regiões de tamponamento (ou regiões tampão), nas quais ocorre a dissociação gradual dos grupamentos ionizáveis presentes na molécula, como o grupo carboxila (-COOH), o grupo amino (-NH3⁺) e, quando presente, grupos da cadeia lateral.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que represente o pK de um grupamento ionizável em um aminoácido?",
            answers = new string[] {
                "O pH em que o grupamento está completamente protonado.",
                "O pH em que o grupamento está completamente desprotonado.",
                "O pH em que metade do grupamento está protonado e metade desprotonado.",
                "O pH em que o aminoácido tem carga líquida zero."
            },
            correctIndex = 2,
            questionNumber = 38,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_038",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O valor de pK (ou pKa) de um grupamento ionizável representa o pH no qual há um equilíbrio entre as formas protonada e desprotonada de um determinado grupo químico. Em termos práticos, isso significa que, quando o pH da solução é igual ao pKa de um grupamento ionizável, 50% das moléculas desse grupo estão protonadas (com H⁺) e 50% estão desprotonadas (sem H⁺).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que representa o pI de um aminoácido?:",
            answers = new string[] {
                "O pH em que ocorre a primeira dissociação.",
                "O pH em que ocorre a última dissociação.",
                "O pH em que a carga líquida do aminoácido é zero.",
                "O pH em que a concentração de H<sup><size=150%> +</size></sup> é máxima."
            },
            correctIndex = 2,
            questionNumber = 39,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_039",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O ponto isoelétrico (pI) de um aminoácido corresponde ao valor de pH no qual a molécula apresenta carga elétrica líquida igual a zero. Isso ocorre porque, nesse pH, o aminoácido se encontra em sua forma zwitteriônica predominante, ou seja, possui simultaneamente cargas positivas e negativas que se equilibram.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual o nível estrutural de uma proteína que corresponde à sequência linear de aminoácidos?",
            answers = new string[] {
                "Estrutura secundária",
                "Estrutura terciária",
                "Estrutura quaternária",
                "Estrutura primária"
            },
            correctIndex = 3,
            questionNumber = 40,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_040",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A estrutura primária de uma proteína corresponde à sequência linear de aminoácidos unidos entre si por ligações peptídicas. Esse nível estrutural representa a ordem específica dos aminoácidos determinada pela informação genética do organismo.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "2 aminoácidos",
                "3 aminoácidos",
                "4 aminoácidos",
                "5 aminoácidos"
            },
            correctIndex = 3,
            questionNumber = 41,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer41",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_041",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "2 ligações peptídicas",
                "3 ligações peptídicas",
                "4 ligações peptídicas",
                "5 ligações peptídicas"
            },
            correctIndex = 1,
            questionNumber = 42,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer42",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_042",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Ponte de Hidrogênio",
                "Ponte Dissulfeto",
                "Interação hidrofóbica",
                "Interação eletrostática"
            },
            correctIndex = 1,
            questionNumber = 43,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer43",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_043",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A ponte dissulfeto é uma ligação química covalente que se forma entre dois átomos de enxofre presentes em resíduos do aminoácido cisteína, resultando na estrutura chamada cistina. Essa ligação ocorre quando dois grupos tiol (R-SH) sofrem uma reação de oxidação, unindo-se e liberando hidrogênio. As pontes dissulfeto desempenham um papel fundamental na estabilização da estrutura tridimensional das proteínas",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Ponte de Hidrogênio",
                "Ponte Dissulfeto",
                "Interação hidrofóbica",
                "Interação eletrostática"
            },
            correctIndex = 0,
            questionNumber = 44,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer44",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_044",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A ponte de hidrogênio é uma interação intermolecular fraca que ocorre quando um átomo de hidrogênio, já ligado covalentemente a um átomo eletronegativo como oxigênio ou nitrogênio, é atraído por outro átomo eletronegativo próximo. Nas proteínas, esse tipo de ligação costuma acontecer entre o hidrogênio do grupo amida (-NH) de um aminoácido e o oxigênio do grupo carbonila (C=O) de outro, contribuindo para a organização e estabilização das estruturas secundárias.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com a cadeia lateral R carregada negativamente são:",
            answers = new string[] {
                "Básicos",
                "Ácidos",
                "Neutros",
                "Apolares"
            },
            correctIndex = 1,
            questionNumber = 45,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_045",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aminoácidos com cadeia lateral R carregada negativamente são classificados como ácidos, pois possuem grupos funcionais que tendem a perder prótons (H⁺) em solução, resultando em carga negativa. Exemplos clássicos incluem o ácido aspártico e o ácido glutâmico.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Aminoácidos com a cadeia lateral R carregada positivamente são:",
            answers = new string[] {
                "Básicos",
                "Ácidos",
                "Neutros",
                "Apolares"
            },
            correctIndex = 0,
            questionNumber = 46,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "aminoacids_046",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aminoácidos com cadeia lateral R carregada positivamente são classificados como básicos, pois possuem grupos funcionais que tendem a aceitar prótons (H⁺), adquirindo carga positiva em pH fisiológico. Exemplos como lisina, arginina e histidina apresentam grupos amino em suas cadeias laterais que se protonam facilmente, o que lhes confere essa característica.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Uma Ponte Dissulfeto",
                "Duas Pontes Dissulfeto",
                "Três Pontes Dissulfeto",
                "Não há Pontes Dissulfeto"
            },
            correctIndex = 2,
            questionNumber = 47,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer47",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_047",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Esturuta Primária",
                "Esturuta Secundária",
                "Esturuta Terciária",
                "Esturuta Quaternária"
            },
            correctIndex = 3,
            questionNumber = 48,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/AminoacidsDB/aminoacidDB_ImageQuestionContainer48",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_048",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A insulina apresenta estrutura quaternária, uma vez que é formada por mais de uma cadeia polipeptídica (cadeias A e B).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que é a estrutura terciária de uma proteína?",
            answers = new string[] {
                "A sua sequência linear de aminoácidos",
                "É a estrutura tridimensional da proteína",
                "São pequenas diferentes estruturas conservadas que dão forma a proteína.",
                "É a estrutura de três proteínas contectadas"
            },
            correctIndex = 1,
            questionNumber = 49,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_049",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A estrutura terciária de uma proteína corresponde ao arranjo tridimensional completo que a cadeia polipeptídica assume no espaço. Essa conformação resulta do dobramento da estrutura secundária (como hélices α e folhas β) e é estabilizada por interações entre os grupos R dos aminoácidos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual a importância das interações hidrofóbicas em proteínas?",
            answers = new string[] {
                "Não há interações hidrofóbicas em proteínas",
                "Servem para estabilizar as ligações peptídicas",
                "Permitem que as proteínas interajam com outras moléculas hidrofóbicas",
                "Estabilizam moléculas de água no interior das proteínas"
            },
            correctIndex = 2,
            questionNumber = 50,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "aminoacids_050",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "As interações hidrofóbicas ocorrem entre regiões apolares (que “evitam” a água) das moléculas. Em proteínas, esses grupos hidrofóbicos tendem a se agrupar no interior da estrutura, afastando-se do meio aquoso. Esse processo é fundamental para o dobramento correto da proteína e para a formação de sua estrutura tridimensional. Além disso, essas interações criam superfícies específicas que permitem que a proteína reconheça e se ligue a outras moléculas, como substratos, ligantes ou outras proteínas. Isso é essencial para funções como catálise enzimática, sinalização celular e transporte.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos são considerados os blocos de construção de qual macromolécula?",
            answers = new string[] {
                "Proteínas",
                "Carboidratos",
                "Lipídios",
                "Ácidos nucleicos"
            },
            correctIndex = 0,
            questionNumber = 51,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_051",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos são conhecidos como os “blocos de construção” das proteínas, pois se unem por meio de ligações peptídicas para formar cadeias polipeptídicas. Essas cadeias, ao se dobrarem adequadamente, originam proteínas funcionais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos essenciais são aqueles que:",
            answers = new string[] {
                "Precisam ser obtidos pela dieta",
                "Não participam de síntese proteica",
                "São encontrados apenas em proteínas animais",
                "Podem ser sintetizados pelo corpo humano em qualquer condição"
            },
            correctIndex = 0,
            questionNumber = 52,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_052",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos essenciais são aqueles que o organismo humano não consegue sintetizar (ou não produz em quantidade suficiente) para atender às suas necessidades metabólicas. Por isso, eles devem ser obtidos por meio da alimentação.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido é conhecido por ser o mais simples, possuindo apenas um átomo de hidrogênio como cadeia lateral?",
            answers = new string[] {
                "Serina",
                "Alanina",
                "Prolina",
                "Glicina"
            },
            correctIndex = 3,
            questionNumber = 53,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_053",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual grupo funcional é característico de todos os aminoácidos?",
            answers = new string[] {
                "Amino e carboxila",
                "Aldeído e cetona",
                "Hidroxila e metila",
                "Fosfato e sulfato"
            },
            correctIndex = 0,
            questionNumber = 54,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_054",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Todos os aminoácidos possuem em comum dois grupos funcionais essenciais: o grupo amino (-NH₂) e o grupo carboxila (-COOH), ambos ligados ao carbono alfa central. Retirado de “Princípios de Bioquímica de Lehninger”, ed. 6, pg. 76",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual dos seguintes aminoácidos NÃO é essencial para o ser humano adulto?",
            answers = new string[] {
                "Valina",
                "Leucina",
                "Hidroxila e metila",
                "Glicina"
            },
            correctIndex = 3,
            questionNumber = 55,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_055",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aminoácidos essenciais são aqueles que precisam ser obtidos por meio da ingestão dos alimentos, por exemplo, valina e leucina. A glicina é classificada como aminoácido não essencial, pois pode ser produzida pelo organismo a partir de outros compostos metabólicos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Quantos aminoácidos proteicos canônicos existem no código genético padrão?",
            answers = new string[] {
                "30",
                "10",
                "64",
                "20"
            },
            correctIndex = 3,
            questionNumber = 56,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_056",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os 20 aminoácidos padrão que são codificados diretamente pelo código genético universal e incorporados nas proteínas durante a tradução do RNA mensageiro são conhecidos como “canônicos” porque podem ser encontrados constituindo as proteínas de diferentes organismos vivos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido possui uma cadeia lateral com enxofre, sendo importante para a formação de pontes dissulfeto?",
            answers = new string[] {
                "Treonina",
                "Cisteína",
                "Fenilalanina",
                "Valina"
            },
            correctIndex = 1,
            questionNumber = 57,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_057",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O grupo R de um aminoácido determina:",
            answers = new string[] {
                "O número de códons",
                "A ligação peptídica",
                "A quantidade de ATP produzido",
                "As propriedades químicas e estruturais do aminoácido"
            },
            correctIndex = 3,
            questionNumber = 58,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_058",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O grupo R (ou cadeia lateral) determina como o aminoácido interage quimicamente (polaridade, acidez ou basicidade) e sua capacidade de formar ligações específicas dentro da proteína. Essas características influenciam diretamente o dobramento da proteína, a formação de estruturas secundárias e terciárias, e as interações com outras moléculas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Os aminoácidos são considerados as unidades básicas de qual macromolécula?",
            answers = new string[] {
                "Lipídios",
                "Carboidratos",
                "Proteínas",
                "Ácidos nucleicos"
            },
            correctIndex = 2,
            questionNumber = 59,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_059",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos são considerados as unidades básicas/estruturais ou “blocos de construção” das proteínas, pois se unem por meio de ligações peptídicas formando cadeias polipeptídicas. Essas cadeias, ao se dobrar adequadamente, originam proteínas funcionais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual grupo funcional está presente em todos os aminoácidos?",
            answers = new string[] {
                "Hidroxila (OH) e fosfato (PO<sub><size=150%>4</size></sub><sup><size=150%>3-</size></sup>)",
                "Amino (NH<sub><size=150%>2</size></sub>) e carboxila (COOH)",
                "Sulfato (SO<sub><size=150%>4</size></sub><sup><size=150%>2-</size></sup>) e éster (COOR)",
                "Aldeído (CHO) e cetona (C=O)"
            },
            correctIndex = 1,
            questionNumber = 60,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_060",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Todos os aminoácidos apresentam uma estrutura básica comum formada por dois grupos funcionais ligados ao carbono alfa: o grupo amino (NH₂) e o grupo carboxila (COOH).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "O que diferencia os aminoácidos entre si?",
            answers = new string[] {
                "O número de carbonos do grupo carboxila",
                "O tipo de ligação peptídica formada",
                "A cadeia lateral (radical R)",
                "A presença de nitrogênio no grupo amino"
            },
            correctIndex = 2,
            questionNumber = 61,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_061",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Todos os aminoácidos compartilham a mesma estrutura básica: um grupo amino, um hidrogênio, um grupo carboxila e uma cadeia lateral (R), todos ligados ao carbono α. É justamente essa cadeia lateral que diferencia cada aminoácido, determinando suas propriedades químicas, como polaridade, acidez, basicidade, e suas interações em proteínas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Quantos aminoácidos são considerados essenciais para humanos adultos?",
            answers = new string[] {
                "3",
                "9",
                "12",
                "20"
            },
            correctIndex = 1,
            questionNumber = 63,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_063",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Os aminoácidos essenciais são aqueles que o corpo humano não consegue sintetizar ou produz em quantidade insuficiente, devendo ser obtidos obrigatoriamente pela dieta. Existem nove aminoácidos essenciais: histidina, isoleucina, leucina, lisina, metionina, fenilalanina, treonina, triptofano e valina.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido contém enxofre em sua estrutura?",
            answers = new string[] {
                "Glicina",
                "Alanina",
                "Cisteína",
                "Lisina"
            },
            correctIndex = 2,
            questionNumber = 64,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_064",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A cisteína é um aminoácido que contém enxofre em sua cadeia lateral, na forma de um grupo tiol (R–SH). Esse grupo é quimicamente reativo e desempenha um papel fundamental na formação de pontes de dissulfeto (–S–S–) entre diferentes partes da proteína ou entre cadeias polipeptídicas, contribuindo para a estabilidade da estrutura terciária e quaternária da proteína.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "A ligação entre dois aminoácidos é chamada de:",
            answers = new string[] {
                "Ligação glicosídica",
                "Ligação peptídica",
                "Ligação fosfodiéster",
                "Ligação de hidrogênio"
            },
            correctIndex = 1,
            questionNumber = 65,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_065",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A ligação entre dois aminoácidos é chamada de ligação peptídica, que é uma ligação covalente formada entre o grupo carboxila (–COOH) de um aminoácido e o grupo amino (–NH₂) de outro.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual aminoácido é o mais simples, com apenas um átomo de hidrogênio como cadeia lateral?",
            answers = new string[] {
                "Alanina",
                "Glicina",
                "Serina",
                "Prolina"
            },
            correctIndex = 1,
            questionNumber = 66,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_066",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A glicina é considerada o aminoácido mais simples porque é o único em que a cadeia lateral (R) é apenas um átomo de hidrogênio (–H).",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Qual destes aminoácidos é aromático?",
            answers = new string[] {
                "Valina",
                "Fenilalanina",
                "Lisina",
                "Treonina"
            },
            correctIndex = 1,
            questionNumber = 67,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_067",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Aminoácidos aromáticos são aqueles que possuem um anel benzênico em sua cadeia lateral (grupo R). Dentre os 20 aminoácidos comuns, somente fenilalanina, tirosina e triptofano são aminoácidos aromáticos.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "A prolina é considerada um aminoácido especial porque:",
            answers = new string[] {
                "Não participa de ligações peptídicas",
                "Possui cadeia lateral aromática",
                "Sua cadeia lateral se liga ao próprio nitrogênio do grupo amino",
                "Não contém grupo carboxila"
            },
            correctIndex = 2,
            questionNumber = 68,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_068",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },
        new Question
        {
            questionDatabankName = "AminoacidQuestionDatabase",
            questionText = "Em pH fisiológico (~7,4), um aminoácido geralmente está em qual forma?",
            answers = new string[] {
                "Totalmente protonado",
                "Totalmente desprotonado",
                "Zwitteriônica (com cargas positivas e negativas)",
                "Neutra, sem cargas"
            },
            correctIndex = 2,
            questionNumber = 69,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "aminoacids_069",
            topic = "aminoacids",
            subtopic = null,
            displayName = "Aminoácidos e peptídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Em pH fisiológico (~7,4), os aminoácidos geralmente se encontram na forma zwitteriônica, também chamada de íon dipolar. Nessa condição, o grupo carboxila (COOH) perde um próton e fica carregado negativamente (COO⁻), enquanto o grupo amino (NH₂) ganha um próton e fica carregado positivamente (NH₃⁺), resultando em uma molécula com cargas opostas simultâneas, mas com carga global neutra. Esse comportamento ocorre porque os aminoácidos são anfóteros e podem tanto doar quanto receber prótons dependendo do pH do meio.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        }
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.aminoacids;
    public string GetDatabankName()  => "AminoacidQuestionDatabase";
    public string GetDisplayName()   => "Aminoácidos e peptídeos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}