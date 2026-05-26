using System.Collections.Generic;
using QuestionSystem;

public class EnzymeQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que são enzimas?",
            answers = new string[] {
                "Catalisadores químicos inorgânicos.",
                "Catalisadores biológicos, principalmente proteínas.",
                "Substratos que participam de reações químicas.",
                "Produtos de reações químicas."
            },
            correctIndex = 1,
            questionNumber = 1,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_001",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Unclassified,
            conceptTags = null,
            prerequisites = null,
            questionHint = null
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual a principal função de uma enzima?",
            answers = new string[] {
                "Sintetizar proteínas.",
                "Aumentar a velocidade de uma reação.",
                "Regular a temperatura corporal.",
                "Transportar oxigênio."
            },
            correctIndex = 1,
            questionNumber = 2,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_002",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Unclassified,
            conceptTags = null,
            prerequisites = null,
            questionHint = null
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Como as enzimas aumentam a velocidade das reações?",
            answers = new string[] {
                "Aumentando a energia de ativação.",
                "Diminuindo a energia de ativação.",
                "Alterando o equilíbrio da reação.",
                "Aumentando a concentração de substrato."
            },
            correctIndex = 1,
            questionNumber = 3,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_003",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "As enzimas aceleram reações químicas ao reduzir a energia de ativação, que é a quantidade mínima de energia necessária para que uma reação ocorra. Ao se ligar ao substrato no sítio ativo, a enzima estabiliza o estado de transição — a configuração molecular de maior energia durante a transformação — tornando esse patamar energético mais baixo e, consequentemente, permitindo que a reação aconteça com muito mais facilidade e rapidez do que ocorreria espontaneamente." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que é o estado de transição em uma reação?",
            answers = new string[] {
                "O estado inicial da reação.",
                "O estado final da reação.",
                "Um estado intermediário de alta energia.",
                "Um catalisador."
            },
            correctIndex = 2,
            questionNumber = 4,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_004",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O estado de transição é uma configuração molecular instável e de alta energia que ocorre durante uma reação química, situando-se entre os reagentes e os produtos. Nesse ponto, as ligações químicas antigas estão sendo rompidas e as novas ainda estão se formando, o que exige um pico de energia conhecido como energia de ativação. As enzimas atuam justamente estabilizando esse estado intermediário, reduzindo a energia necessária para atingi-lo e acelerando assim a reação." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que é energia de ativação?",
            answers = new string[] {
                "A energia necessária para iniciar uma reação.",
                "A energia liberada durante uma reação.",
                "A diferença de energia entre o substrato e o estado de transição.",
                "A energia do produto."
            },
            correctIndex = 2,
            questionNumber = 5,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_005",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A energia de ativação é a quantidade de energia que os reagentes precisam absorver para atingir o estado de transição e, a partir daí, se transformarem em produtos. Em termos práticos, ela representa a diferença de energia entre o estado inicial do substrato e o ponto de maior energia da reação — o estado de transição. Quanto maior essa barreira energética, mais difícil e lenta é a reação; as enzimas atuam reduzindo essa barreira, tornando as reações biologicamente viáveis." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas atuam em condições:",
            answers = new string[] {
                "Extremas de temperatura e pH.",
                "Compatíveis com a vida.",
                "Exclusivamente in vitro.",
                "Independentes do meio."
            },
            correctIndex = 1,
            questionNumber = 6,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_006",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Uma das grandes vantagens das enzimas é que elas realizam catálise em condições brandas, como temperaturas próximas à corporal, pH fisiológico e pressão atmosférica normal — condições plenamente compatíveis com a vida celular. Isso contrasta com a catálise química industrial, que frequentemente exige altas temperaturas, pressões extremas ou reagentes corrosivos. Essa característica torna as enzimas não apenas essenciais para os organismos vivos, mas também muito atrativas para aplicações biotecnológicas e industriais." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O sítio ativo de uma enzima é:",
            answers = new string[] {
                "A região onde a enzima se liga ao produto.",
                "A região onde a enzima se liga ao substrato.",
                "A região responsável pela regulação da enzima.",
                "A região onde a enzima se liga a cofatores."
            },
            correctIndex = 1,
            questionNumber = 7,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_007",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O sítio ativo é uma região tridimensional específica da enzima, formada por um conjunto de aminoácidos que se dobram de maneira a criar uma cavidade ou reentrância com forma e propriedades químicas complementares ao substrato. É nessa região que ocorre a ligação entre a enzima e seu substrato, formando o complexo enzima-substrato, e onde a reação química é catalisada. A alta especificidade das enzimas decorre justamente das características únicas do sítio ativo de cada uma delas." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O modelo chave-fechadura descreve a interação enzima-substrato como:",
            answers = new string[] {
                "Um ajuste induzido.",
                "Uma ligação covalente.",
                "Um encaixe complementar.",
                "Uma interação hidrofóbica."
            },
            correctIndex = 2,
            questionNumber = 8,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_008",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Proposto por Emil Fischer em 1894, o modelo chave-fechadura descreve a interação entre enzima e substrato como um encaixe rígido e complementar: assim como uma chave específica se encaixa perfeitamente em sua fechadura, o substrato se encaixa de forma precisa no sítio ativo da enzima, que já possui uma forma pré-determinada e complementar à do substrato. Esse modelo explica bem a especificidade enzimática, embora seja considerado simplificado, pois não contempla as mudanças conformacionais observadas em muitas enzimas — o que levou ao desenvolvimento do modelo do ajuste induzido." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual fator é essencial para que uma enzima exerça sua ativiade plenamente",
            answers = new string[] {
                "A sua estrutura primária",
                "A estabilidade de sua estrutura terciária",
                "A quantidade de alfa-hélices na estrutura da enzima",
                "A formação de estrutura quaternária"
            },
            correctIndex = 1,
            questionNumber = 9,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_009",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A atividade catalítica de uma enzima depende fundamentalmente da integridade de sua estrutura terciária, ou seja, do dobramento tridimensional preciso da cadeia polipeptídica. É esse dobramento que define a forma e as propriedades químicas do sítio ativo — responsável pelo reconhecimento e ligação ao substrato. Fatores como temperatura elevada, pH inadequado ou a presença de agentes desnaturantes podem desestabilizar a estrutura terciária, distorcendo o sítio ativo e levando à perda de função enzimática, fenômeno chamado de desnaturação." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Por que enzimas podem ser usadas na indústria",
            answers = new string[] {
                "Reação enzimática ocorre em temperaturas brandas.",
                "Enzimas são altamente específicas.",
                "Necessita-se de quantidades bem pequenas de enzimas, mesmo em escala industrial.",
                "Todas as alternativas são corretas."
            },
            correctIndex = 3,
            questionNumber = 10,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_010",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "As enzimas reúnem um conjunto de propriedades que as tornam extremamente vantajosas para uso industrial: atuam em condições brandas de temperatura e pH, reduzindo o consumo de energia nos processos; são altamente específicas, minimizando a formação de subprodutos indesejados; e são biologicamente eficientes, sendo necessárias em pequenas quantidades para catalisar grandes volumes de reação. Por isso, são amplamente utilizadas em indústrias alimentícias, farmacêuticas, de detergentes e de biocombustíveis, entre outras." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas podem ser agrupadas em seis grandes grupos, de acordo com o tipo de reação que ela catalisa. Abaixo temos alguns nome de grupos de enzimas, exceto: ",
            answers = new string[] {
                "Hidrolases",
                "Ribolase",
                "Oxidoredutases",
                "Liases"
            },
            correctIndex = 1,
            questionNumber = 11,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_011",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "De acordo com a nomenclatura estabelecida pela União Internacional de Bioquímica e Biologia Molecular (IUBMB), as enzimas são classificadas em seis grandes classes: oxidoredutases (catalisam reações de oxirredução), transferases (transferem grupos funcionais entre moléculas), hidrolases (catalisam hidrólise de ligações), liases (adicionam ou removem grupos sem hidrólise ou oxirredução), isomerases (interconvertem isômeros) e ligases (unem moléculas com consumo de ATP). O termo \\\"ribolase\\\" não corresponde a nenhuma dessas classes oficiais." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Região da enzima responsável por interagir com a água",
                "Região da enzima com grande afinidade por íons",
                "Região da enzima que participa diretamente da catálise",
                "Região da enzima altamente hidrofóbica"
            },
            correctIndex = 2,
            questionNumber = 12,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer12",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_012",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Unclassified,
            conceptTags = null,
            prerequisites = null,
            questionHint = null
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A atividade de uma enzima pode ser afetada por:",
            answers = new string[] {
                "Temperatura e pH.",
                "Concentração de substrato.",
                "Presença de inibidores.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 13,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_013",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A atividade enzimática é influenciada por múltiplos fatores simultaneamente. A temperatura e o pH afetam diretamente a estrutura tridimensional da enzima e as interações no sítio ativo; a concentração de substrato determina com que frequência o sítio ativo é ocupado; e inibidores são moléculas capazes de reduzir ou bloquear a atividade catalítica, seja ocupando o sítio ativo ou alterando a conformação da enzima. Compreender esses fatores é fundamental para entender como a célula regula sua própria bioquímica." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O pH ótimo de uma enzima é:",
            answers = new string[] {
                "O pH em que a enzima tem atividade máxima.",
                "O pH em que a enzima é inativada.",
                "O pH em que a enzima é desnaturada.",
                "O pH do meio celular."
            },
            correctIndex = 0,
            questionNumber = 14,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_014",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O pH ótimo é o valor de pH no qual uma enzima apresenta sua maior velocidade catalítica, pois nessa condição a ionização dos aminoácidos do sítio ativo está no estado mais favorável à ligação com o substrato e à catálise. Cada enzima possui seu próprio pH ótimo, que geralmente reflete o ambiente onde ela naturalmente atua: por exemplo, a pepsina gástrica tem pH ótimo em torno de 2, enquanto a maioria das enzimas intracelulares opera melhor próximo ao pH neutro (6,5 a 7,5)." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A temperatura ótima de uma enzima é:",
            answers = new string[] {
                "A temperatura em que a enzima é desnaturada.",
                "A temperatura em que a enzima tem atividade máxima.",
                "A temperatura ambiente.",
                "A temperatura do organismo."
            },
            correctIndex = 1,
            questionNumber = 15,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_015",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A temperatura ótima é aquela na qual a enzima exibe sua maior taxa de atividade catalítica. Abaixo desse ponto, o aumento de temperatura favorece a reação ao incrementar a energia cinética das moléculas; acima dele, porém, o calor excessivo começa a romper as interações não covalentes que mantêm a estrutura tridimensional da enzima, levando à desnaturação e à perda de função. Para a maioria das enzimas humanas, a temperatura ótima situa-se próxima aos 37 °C, compatível com a temperatura corporal." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que acontece com a atividade de uma enzima quando a temperatura aumenta muito além da sua temperatura ótima?",
            answers = new string[] {
                "Aumenta.",
                "Diminui.",
                "Permanece constante.",
                "Varia de forma imprevisível."
            },
            correctIndex = 1,
            questionNumber = 16,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_016",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Quando a temperatura supera o valor ótimo da enzima, o excesso de energia térmica desestabiliza as interações fracas — como pontes de hidrogênio e interações hidrofóbicas — que sustentam o dobramento tridimensional da proteína. Isso provoca a desnaturação da enzima, distorcendo o sítio ativo e impedindo a ligação adequada ao substrato. Como resultado, a atividade catalítica cai progressivamente e pode ser completamente perdida, tornando a enzima inativa." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Desnaturação de uma enzima significa:",
            answers = new string[] {
                "Ativação da enzima.",
                "Perda da atividade enzimática devido à alteração da sua estrutura.",
                "Aumento da velocidade da reação.",
                "Formação de um complexo enzima-substrato."
            },
            correctIndex = 1,
            questionNumber = 17,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_017",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A desnaturação é o processo pelo qual a estrutura tridimensional de uma proteína — e, no caso das enzimas, especialmente o sítio ativo — é desfeita por agentes físicos ou químicos, como calor extremo, variações bruscas de pH ou solventes orgânicos. Como a função enzimática depende diretamente de sua conformação espacial precisa, a perda dessa estrutura resulta em inativação. Em muitos casos, a desnaturação é irreversível: a proteína não consegue se redobrar corretamente, perdendo definitivamente sua capacidade catalítica." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Quais fatores podem causar a desnaturação de uma enzima?",
            answers = new string[] {
                "Altas temperaturas.",
                "Variações de pH.",
                "Solventes orgânicos.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 18,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_018",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Diversos agentes são capazes de desnaturar enzimas ao romper as interações que mantêm sua estrutura tridimensional. Altas temperaturas aumentam a agitação molecular a ponto de desfazer pontes de hidrogênio e interações hidrofóbicas; variações extremas de pH alteram o estado de ionização dos aminoácidos, perturbando a conformação da proteína; e solventes orgânicos competem pelas interações hidrofóbicas internas, desestabilizando o núcleo proteico. Em todos os casos, o resultado final é a perda da estrutura funcional da enzima." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Inibidores enzimáticos são moléculas que:",
            answers = new string[] {
                "Aumentam a atividade da enzima.",
                "Diminuem ou impedem a atividade da enzima.",
                "Alteram o equilíbrio da reação.",
                "São substratos da enzima."
            },
            correctIndex = 1,
            questionNumber = 19,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_019",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Inibidores enzimáticos são moléculas que reduzem ou bloqueiam a atividade catalítica de uma enzima, podendo atuar de diferentes formas: ocupando o sítio ativo e impedindo a ligação do substrato, ou ligando-se a outras regiões da enzima e alterando sua conformação. A inibição enzimática é um mecanismo biológico fundamental para a regulação do metabolismo celular e também é amplamente explorado na medicina — muitos fármacos, como antibióticos e antivirais, funcionam como inibidores de enzimas essenciais para patógenos." }
        },
         new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Inibidores irreversíveis se ligam à enzima:",
            answers = new string[] {
                "Reversivelmente.",
                "Irreversivelmente, modificando permanentemente sua estrutura.",
                "Em um sítio alostérico.",
                "Somente em pH ácido."
            },
            correctIndex = 1,
            questionNumber = 20,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_020",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Inibidores irreversíveis formam ligações covalentes fortes com a enzima — geralmente no sítio ativo ou em resíduos de aminoácidos essenciais — modificando permanentemente sua estrutura e inativando-a de forma definitiva. Como a enzima não pode ser regenerada, a célula precisa sintetizar novas moléculas para restaurar a atividade. Um exemplo clássico é o ácido acetilsalicílico (aspirina), que inativa irreversivelmente a enzima ciclo-oxigenase, responsável pela síntese de prostaglandinas envolvidas na inflamação e na dor." }
        },
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Inibição Irreversível",
                "Inibição Competitiva",
                "Inibição  Não-Competitiva",
                "Inibição A-Competitiva"
            },
            correctIndex = 2,
            questionNumber = 21,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer21",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_021",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Unclassified,
            conceptTags = null,
            prerequisites = null,
            questionHint = null
        },
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Inibição Irreversível",
                "Inibição Competitiva",
                "Inibição  Não-Competitiva",
                "Inibição A-Competitiva"
            },
            correctIndex = 1,
            questionNumber = 22,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer22",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_022",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Unclassified,
            conceptTags = null,
            prerequisites = null,
            questionHint = null
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A constante de Michaelis (Km) indica:",
            answers = new string[] {
                "A velocidade máxima da reação.",
                "A concentração de enzima.",
                "A concentração de substrato necessária para a enzima atingir metade da sua velocidade máxima.",
                "A energia de ativação."
            },
            correctIndex = 2,
            questionNumber = 23,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_023",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O Km, ou constante de Michaelis, é um parâmetro cinético que expressa a concentração de substrato necessária para que uma enzima opere a exatamente metade de sua velocidade máxima (Vmax/2). Na prática, o Km é uma medida indireta da afinidade da enzima pelo substrato: quanto menor o Km, menor a quantidade de substrato necessária para saturar a enzima, indicando alta afinidade; quanto maior o Km, maior a concentração de substrato necessária, indicando baixa afinidade." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um Km baixo indica:",
            answers = new string[] {
                "Baixa interação da enzima com substrato.",
                "Alta interação da enzima com substrato.",
                "Velocidade máxima de reação baixa.",
                "Velocidade máxima de reação alta."
            },
            correctIndex = 1,
            questionNumber = 24,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_024",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Um Km baixo significa que a enzima consegue atingir metade da sua velocidade máxima mesmo com concentrações muito pequenas de substrato, o que reflete uma alta afinidade entre a enzima e seu substrato. Em termos moleculares, isso indica que o complexo enzima-substrato é formado com facilidade e se dissocia lentamente, favorecendo a catálise. É importante notar que o Km diz respeito apenas à afinidade e não à velocidade máxima da reação, que é determinada por um parâmetro separado, o Vmax." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um Km alto indica:",
            answers = new string[] {
                "Baixa interação da enzima com substrato.",
                "Alta interação da enzima com substrato.",
                "Velocidade máxima de reação alta.",
                "Velocidade máxima de reação baixa."
            },
            correctIndex = 0,
            questionNumber = 25,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_025",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Um Km alto indica que a enzima necessita de concentrações elevadas de substrato para operar a metade de sua velocidade máxima, o que reflete baixa afinidade entre a enzima e o substrato. Nessa situação, o complexo enzima-substrato se forma com dificuldade ou se dissocia rapidamente antes que a catálise ocorra. Do ponto de vista fisiológico, uma enzima com Km alto será menos eficiente em ambientes onde o substrato está em baixa concentração, tornando-se mais relevante apenas quando o substrato está abundantemente disponível." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A equação de Michaelis-Menten relaciona:",
            answers = new string[] {
                "Km, Vmax e a concentração de substrato.",
                "KM, pH e temperatura.",
                "Vmax, temperatura e pH.",
                "KM, pKa e a concentração de substrato."
            },
            correctIndex = 0,
            questionNumber = 26,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_026",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A equação de Michaelis-Menten descreve matematicamente como a velocidade de uma reação enzimática (v) varia em função da concentração de substrato [S], utilizando dois parâmetros fundamentais: o Km (constante de Michaelis, que reflete a afinidade da enzima pelo substrato) e o Vmax (velocidade máxima, atingida quando todos os sítios ativos estão saturados). A equação é expressa como v = (Vmax × [S]) / (Km + [S]), e seu gráfico característico tem formato hiperbólico, sendo uma das ferramentas mais importantes da cinética enzimática." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Na equação de Michaelis-Menten, Vmax representa:",
            answers = new string[] {
                "A velocidade inicial da reação.",
                "A velocidade máxima da reação.",
                "A constante de Michaelis.",
                "A concentração de substrato."
            },
            correctIndex = 1,
            questionNumber = 27,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_027",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O Vmax, ou velocidade máxima, é a taxa de reação mais alta que uma enzima pode atingir quando todos os seus sítios ativos estão continuamente ocupados pelo substrato — ou seja, quando a enzima está completamente saturada. Nessa condição, adicionar mais substrato não aumenta a velocidade da reação, pois não há sítios ativos livres disponíveis. O Vmax depende diretamente da quantidade total de enzima presente e da eficiência catalítica de cada molécula, sendo um reflexo da capacidade máxima de processamento do sistema enzimático." }
        },
       new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Gráfico de Michaelis-Menten",
                "Gráfico Enzimático",
                "Gráfico de Lineweaver-Burk",
                "Gráfico Competitivo"
            },
            correctIndex = 2,
            questionNumber = 28,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer28",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_028",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Unclassified,
            conceptTags = null,
            prerequisites = null,
            questionHint = null
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa o RNA é:",
            answers = new string[] {
                "DNA polimerase",
                "RNA polimerase",
                "Ribonuclease",
                "Protease"
            },
            correctIndex = 2,
            questionNumber = 29,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_029",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A ribonuclease (RNase) é a enzima responsável pela hidrólise do RNA, catalisando a quebra das ligações fosfodiéster que unem os nucleotídeos da cadeia ribonucleica. Ela é essencial em processos como o processamento de RNA mensageiro, a degradação de moléculas de RNA após o uso e a defesa contra RNA viral. Existem diferentes tipos de ribonucleases, cada uma atuando em contextos celulares específicos, mas todas compartilham a função de clivar moléculas de RNA." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa proteínas é:",
            answers = new string[] {
                "Ribonuclease",
                "Protease",
                "Lipase",
                "Amílase"
            },
            correctIndex = 1,
            questionNumber = 30,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_030",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "As proteases, também chamadas de peptidases ou proteinases, são enzimas que catalisam a hidrólise das ligações peptídicas que unem os aminoácidos nas proteínas e peptídeos. Elas desempenham papéis fundamentais na digestão de proteínas alimentares, na regulação de processos celulares e na defesa imunológica. Exemplos bem conhecidos incluem a pepsina gástrica, a tripsina e a quimotripsina pancreáticas, cada uma com especificidades distintas quanto aos sítios de clivagem na cadeia proteica." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa lipídios é:",
            answers = new string[] {
                "Amílase",
                "Protease",
                "Lipase",
                "Ribonuclease"
            },
            correctIndex = 2,
            questionNumber = 31,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_031",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "As lipases são enzimas que catalisam a hidrólise de lipídios, especialmente triglicerídeos, quebrando as ligações éster entre o glicerol e os ácidos graxos. No sistema digestório humano, a lipase pancreática é a principal responsável pela digestão de gorduras no intestino delgado, liberando ácidos graxos e monoglicerídeos que serão absorvidos pelo organismo. As lipases também têm ampla aplicação industrial, sendo usadas na produção de biodiesel, alimentos e detergentes enzimáticos." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que hidrolisa amido é:",
            answers = new string[] {
                "Lipase",
                "Protease",
                "Amílase",
                "Ribonuclease"
            },
            correctIndex = 2,
            questionNumber = 32,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_032",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A amilase é a enzima responsável pela hidrólise do amido, um polissacarídeo de reserva vegetal composto por cadeias de glicose. Ela atua quebrando as ligações glicosídicas alfa-1,4 da amilose e da amilopectina, gerando moléculas menores como maltose e dextrinas, que posteriormente são convertidas em glicose para absorção. No ser humano, a amilase está presente tanto na saliva (amilase salivar, dando início à digestão de carboidratos na boca) quanto no suco pancreático (amilase pancreática)." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A pepsina é uma enzima:",
            answers = new string[] {
                "Que hidrolisa carboidratos.",
                "Que hidrolisa proteínas.",
                "Que hidrolisa lipídios.",
                "Que hidrolisa ácidos nucléicos."
            },
            correctIndex = 1,
            questionNumber = 33,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_033",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A pepsina é uma protease produzida no estômago a partir do zimogênio inativo pepsinogênio, que é ativado pelo ambiente altamente ácido do suco gástrico. Ela atua hidrolisando ligações peptídicas de proteínas da dieta, preferencialmente após aminoácidos aromáticos como fenilalanina, triptofano e tirosina, dando início à digestão proteica no estômago. Por ser uma enzima adaptada ao meio ácido, a pepsina possui pH ótimo de aproximadamente 2, tornando-se inativa em condições neutras ou alcalinas." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A pepsina atua melhor em qual pH?",
            answers = new string[] {
                "pH 7",
                "pH 10",
                "pH 2",
                "pH 14"
            },
            correctIndex = 2,
            questionNumber = 34,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_034",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A pepsina tem pH ótimo em torno de 2, compatível com o ambiente extremamente ácido do estômago, onde o ácido clorídrico (HCl) mantém o pH gástrico entre 1,5 e 3,5. Essa acidez é essencial não apenas para a atividade da pepsina, mas também para a ativação do pepsinogênio, seu precursor inativo. Em pH neutro ou alcalino — como no intestino delgado — a pepsina é rapidamente inativada, encerrando sua ação catalítica e evitando danos à mucosa intestinal." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A quimotripsina é uma enzima:",
            answers = new string[] {
                "Que hidrolisa carboidratos.",
                "Que hidrolisa proteínas.",
                "Que hidrolisa lipídios.",
                "Que hidrolisa ácidos nucléicos."
            },
            correctIndex = 1,
            questionNumber = 35,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_035",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A quimotripsina é uma protease serínica secretada pelo pâncreas como zimogênio inativo (quimotripsinogênio) e ativada no intestino delgado pela tripsina. Ela hidrolisa ligações peptídicas preferencialmente após aminoácidos com cadeias laterais grandes e hidrofóbicas ou aromáticas, como fenilalanina, triptofano, tirosina e leucina. Junto com a tripsina e a elastase, a quimotripsina compõe o conjunto de proteases pancreáticas responsáveis pela digestão de proteínas no duodeno." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A quimotripsina atua melhor em qual pH?",
            answers = new string[] {
                "pH 2",
                "pH 7",
                "pH 8",
                "pH 14"
            },
            correctIndex = 2,
            questionNumber = 36,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_036",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A quimotripsina tem pH ótimo em torno de 8, levemente alcalino, compatível com o ambiente do intestino delgado, onde o bicarbonato secretado pelo pâncreas neutraliza o quimo ácido proveniente do estômago. Esse contraste com a pepsina gástrica ilustra perfeitamente como cada enzima digestiva é adaptada ao compartimento onde atua: o estômago ácido favorece a pepsina, enquanto o duodeno levemente alcalino é o ambiente ideal para as proteases pancreáticas como a quimotripsina." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A amilase salivar é uma enzima:",
            answers = new string[] {
                "Que hidrolisa lipídios.",
                "Que hidrolisa proteínas.",
                "Que hidrolisa carboidratos.",
                "Que hidrolisa ácidos nucléicos."
            },
            correctIndex = 2,
            questionNumber = 37,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_037",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A amilase salivar, também chamada de ptialina, é produzida pelas glândulas salivares e dá início à digestão de carboidratos ainda na boca. Ela hidrolisa as ligações alfa-1,4-glicosídicas do amido, convertendo-o em fragmentos menores como maltose e dextrinas. Embora sua ação seja interrompida pela acidez estomacal, a amilase salivar já realiza uma digestão parcial significativa dos carboidratos durante a mastigação, sendo a primeira enzima digestiva a entrar em contato com o alimento." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A amilase salivar atua melhor em qual pH?",
            answers = new string[] {
                "pH 2",
                "pH 7",
                "pH 8",
                "pH 14"
            },
            correctIndex = 1,
            questionNumber = 38,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_038",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A amilase salivar tem pH ótimo próximo de 7, neutro, condizente com o ambiente bucal onde é secretada pelas glândulas salivares. Quando o alimento é deglutido e chega ao estômago, o pH cai drasticamente para valores em torno de 2, o que desnatura e inativa a amilase salivar, encerrando sua ação catalítica. Por isso, a digestão de carboidratos iniciada na boca é retomada apenas no intestino delgado, pela amilase pancreática, que atua em pH levemente alcalino." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A inibição enzimática irreversível causa:",
            answers = new string[] {
                "Uma diminuição temporária da atividade enzimática.",
                "Uma diminuição permanente da atividade enzimática.",
                "Um aumento da atividade enzimática.",
                "Nenhuma alteração na atividade enzimática."
            },
            correctIndex = 1,
            questionNumber = 39,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_039",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na inibição irreversível, o inibidor forma uma ligação covalente estável com a enzima — geralmente no sítio ativo ou em resíduos essenciais à catálise — modificando permanentemente sua estrutura e impossibilitando a recuperação da atividade catalítica. Como a enzima não pode ser reativada, a célula precisa sintetizar novas moléculas para repor a função perdida. Exemplos incluem os organofosforados (como pesticidas e agentes nervosos), que inibem irreversivelmente a acetilcolinesterase, e a aspirina, que inativa permanentemente a ciclo-oxigenase." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A inibição enzimática reversível competitiva pode ser superada por:",
            answers = new string[] {
                "Aumento da concentração do inibidor.",
                "Diminuição da concentração do inibidor.",
                "Aumento da concentração do substrato.",
                "Diminuição da concentração do substrato."
            },
            correctIndex = 2,
            questionNumber = 40,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_040",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na inibição competitiva, o inibidor possui estrutura semelhante ao substrato e compete com ele pela ocupação do sítio ativo da enzima. Como essa ligação é reversível, o substrato e o inibidor disputam o sítio ativo de forma dinâmica. Ao aumentar a concentração de substrato, a probabilidade de ele ocupar o sítio ativo aumenta, deslocando o inibidor e restaurando progressivamente a atividade enzimática. Por isso, a inibição competitiva pode ser superada pelo excesso de substrato, e o Vmax da enzima permanece inalterado, embora o Km aparente aumente." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A inibição enzimática reversível não-competitiva pode ser superada por:",
            answers = new string[] {
                "Aumento da concentração do substrato.",
                "Diminuição da concentração do substrato.",
                "Aumento da concentração do inibidor.",
                "Não pode ser superada."
            },
            correctIndex = 3,
            questionNumber = 41,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_041",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na inibição não competitiva, o inibidor se liga a um sítio diferente do sítio ativo — chamado sítio alostérico — e altera a conformação da enzima de forma que ela perde eficiência catalítica, mesmo quando o substrato está ligado. Como o inibidor não compete pela ocupação do sítio ativo, aumentar a concentração de substrato não reverte o efeito inibitório: o substrato continua se ligando normalmente, mas a enzima não catalisa com a mesma eficiência. O resultado é uma redução do Vmax sem alteração do Km." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O captopril e o enalapril inibem a enzima:",
            answers = new string[] {
                "Ciclooxigenase",
                "ECA (enzima conversora de angiotensina)",
                "Lipase",
                "Protease"
            },
            correctIndex = 1,
            questionNumber = 42,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_042",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O captopril e o enalapril são fármacos da classe dos inibidores da ECA (enzima conversora de angiotensina), amplamente utilizados no tratamento da hipertensão arterial e da insuficiência cardíaca. A ECA converte angiotensina I em angiotensina II, um potente vasoconstritor que também estimula a liberação de aldosterona, aumentando a pressão arterial. Ao inibir a ECA, esses medicamentos reduzem os níveis de angiotensina II, promovendo vasodilatação e diminuição da pressão arterial, sendo um exemplo clássico de aplicação terapêutica da inibição enzimática." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas são classificadas como:",
            answers = new string[] {
                "Proteínas",
                "Carboidratos",
                "Vitaminas",
                "Lipídios"
            },
            correctIndex = 0,
            questionNumber = 43,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_043",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A grande maioria das enzimas são proteínas, ou seja, são compostas por cadeias de aminoácidos dobradas em estruturas tridimensionais específicas. Essa natureza proteica é fundamental para sua função, pois é o arranjo espacial preciso dos aminoácidos que forma o sítio ativo e confere especificidade à catálise. Vale mencionar que existe uma exceção importante: os ribozimas, moléculas de RNA com atividade catalítica, demonstram que nem todo catalisador biológico é necessariamente uma proteína, embora esses sejam casos particulares e minoritários." }
        },    
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual é a principal função das enzimas no metabolismo celular?",
            answers = new string[] {
                "Transportar oxigênio",
                "Armazenar energia",
                "Produzir hormônios",
                "Acelerar reações químicas"
            },
            correctIndex = 3,
            questionNumber = 44,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_044",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "No metabolismo celular, as enzimas atuam como catalisadores biológicos, acelerando as inúmeras reações químicas necessárias para a manutenção da vida. Sem enzimas, a maioria dessas reações ocorreria em velocidades extremamente lentas — incompatíveis com as necessidades celulares — ou simplesmente não ocorreria nas condições brandas do ambiente intracelular. Cada via metabólica, seja a glicólise, o ciclo de Krebs ou a síntese proteica, depende de um conjunto específico de enzimas que orquestram as transformações moleculares com precisão e eficiência." }
        }, 
         new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O local da enzima onde o substrato se liga é chamado de:",
            answers = new string[] {
                "Sítio ativo",
                "Cofator",
                "Produto",
                "Cofator enzimático"
            },
            correctIndex = 0,
            questionNumber = 45,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_045",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O sítio ativo é a região tridimensional específica da enzima responsável pelo reconhecimento e ligação ao substrato, bem como pela catálise da reação. Ele é formado por um subconjunto de aminoácidos que, após o dobramento da proteína, ficam posicionados de forma a criar uma cavidade com formato e propriedades químicas complementares ao substrato. A especificidade enzimática — ou seja, a capacidade de cada enzima reconhecer apenas determinados substratos — é determinada pelas características únicas do sítio ativo de cada enzima." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual destes fatores pode alterar a atividade enzimática?",
            answers = new string[] {
                "Cor da enzima",
                "Pressão osmótica",
                "Temperatura e pH",
                "Massa molecular"
            },
            correctIndex = 2,
            questionNumber = 46,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_046",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A temperatura e o pH são os principais fatores ambientais que afetam a atividade enzimática, pois influenciam diretamente a integridade da estrutura tridimensional da enzima e as interações químicas no sítio ativo. Cada enzima possui valores ótimos de temperatura e pH nos quais sua atividade é máxima; desvios significativos desses valores podem reduzir a eficiência catalítica ou levar à desnaturação irreversível. Outros fatores relevantes incluem a concentração de substrato, a presença de inibidores ou ativadores, e a disponibilidade de cofatores." }
        }, 
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Quando uma enzima perde sua estrutura tridimensional devido a altas temperaturas, esse processo é chamado:",
            answers = new string[] {
                "Redução",
                "Fusão",
                "Oxidação",
                "Desnaturação"
            },
            correctIndex = 3,
            questionNumber = 47,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_047",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A desnaturação é o processo pelo qual uma proteína perde sua estrutura tridimensional funcional devido à ruptura das interações não covalentes — como pontes de hidrogênio, interações hidrofóbicas e pontes dissulfeto — que a mantêm dobrada. No caso das enzimas, a desnaturação desfaz o sítio ativo, inativando a molécula. O calor excessivo é um dos agentes desnaturantes mais comuns, mas variações extremas de pH, solventes orgânicos e detergentes também podem causar desnaturação. Em geral, esse processo é irreversível sob condições fisiológicas." }
        }, 
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A energia mínima necessária para iniciar uma reação química é chamada de:",
            answers = new string[] {
                "Energia solar",
                "nergia cinética",
                "Energia de ativação",
                "Energia potencial"
            },
            correctIndex = 2,
            questionNumber = 48,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_048",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A energia de ativação é a quantidade mínima de energia que os reagentes precisam possuir para que uma reação química ocorra, correspondendo à barreira energética que deve ser superada para que as moléculas atinjam o estado de transição e se transformem em produtos. Nas células, as enzimas são fundamentais justamente por reduzir essa barreira energética, tornando as reações viáveis nas condições brandas do ambiente celular, sem a necessidade de altas temperaturas ou outros estímulos extremos que seriam incompatíveis com a vida." }
        }, 
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Quando uma molécula semelhante ao substrato compete pelo sítio ativo da enzima, temos:",
            answers = new string[] {
                "Ativação enzimática",
                "Inibição não-competitiva",
                "Inibição competitiva",
                "Regulação alostérica"
            },
            correctIndex = 2,
            questionNumber = 49,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_049",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A inibição competitiva ocorre quando uma molécula com estrutura semelhante à do substrato ocupa o sítio ativo da enzima, impedindo temporariamente que o substrato verdadeiro se ligue e seja convertido em produto. Como o inibidor e o substrato disputam o mesmo sítio, o efeito inibitório pode ser revertido aumentando-se a concentração de substrato. Esse tipo de inibição reduz a eficiência aparente da enzima (aumenta o Km aparente), mas o Vmax permanece inalterado quando o substrato está em excesso suficiente." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas atuam de forma mais eficiente em:",
            answers = new string[] {
                "Temperatura e pH ótimos",
                "Qualquer temperatura ou pH",
                "Ambiente sem água",
                "Altas pressões"
            },
            correctIndex = 0,
            questionNumber = 50,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_050",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Cada enzima possui condições ideais de temperatura e pH — chamadas de temperatura ótima e pH ótimo — nas quais sua estrutura tridimensional está perfeitamente estável e o sítio ativo encontra-se na configuração mais favorável à catálise. Nessas condições, a enzima atinge sua velocidade de reação máxima. Afastar-se dessas condições ótimas, seja por aquecimento excessivo, resfriamento extremo ou variação de pH, compromete progressivamente a estrutura e a função enzimática, podendo levar à inativação completa." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A região da enzima onde o substrato se liga é chamada de:",
            answers = new string[] {
                "Cofator",
                "Sítio ativo",
                "Centro metabólico",
                "Núcleo catalítico"
            },
            correctIndex = 1,
            questionNumber = 51,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_051",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O sítio ativo é a região tridimensional da enzima onde ocorre a ligação específica ao substrato e onde a reação catalítica acontece. Ele é formado por aminoácidos que, após o dobramento da cadeia polipeptídica, ficam posicionados espacialmente de maneira a criar um ambiente com forma, polaridade e reatividade química complementares ao substrato. A especificidade de cada enzima pelo seu substrato é determinada pelas características únicas dessa região, tornando o sítio ativo o elemento central da função enzimática." }
        },    
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O modelo chave-fechadura é usado para explicar:",
            answers = new string[] {
                "A especificidade entre enzima e substrato",
                "O armazenamento de energia na célula",
                "A formação de polissacarídeos",
                "A síntese de proteínas"
            },
            correctIndex = 0,
            questionNumber = 52,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_052",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O modelo chave-fechadura, proposto por Emil Fischer em 1894, é uma analogia utilizada para explicar a especificidade da interação entre enzima e substrato. Assim como uma chave só se encaixa em sua fechadura correspondente, o substrato possui uma forma complementar e precisa ao sítio ativo da enzima, de modo que apenas moléculas com a geometria adequada conseguem se ligar e ser catalisadas. Esse modelo foi fundamental para compreender por que cada enzima reconhece apenas determinados substratos, embora modelos mais modernos, como o do ajuste induzido, tenham ampliado essa compreensão." }
        },  
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual desses fatores não influencia a atividade enzimática?",
            answers = new string[] {
                "Temperatura",
                "pH",
                "Concentração de substrato",
                "Cor da solução"
            },
            correctIndex = 3,
            questionNumber = 53,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_053",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A atividade enzimática é regulada por fatores que afetam diretamente a estrutura da enzima ou a dinâmica da reação, como temperatura, pH, concentração de substrato, presença de inibidores ou ativadores e disponibilidade de cofatores. A cor da solução, por sua vez, é uma propriedade óptica que não interfere nas interações moleculares entre a enzima e seu substrato, nem na integridade estrutural da proteína, não sendo portanto um fator relevante para a atividade catalítica." }
        },   
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Coenzimas são:",
            answers = new string[] {
                "Íons metálicos que ajudam as enzimas",
                "Moléculas orgânicas auxiliares, muitas vezes derivadas de vitaminas",
                "Aminoácidos que formam o sítio ativo",
                "Produtos da reação enzimática"
            },
            correctIndex = 1,
            questionNumber = 54,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_054",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Coenzimas são moléculas orgânicas de baixo peso molecular que se associam a certas enzimas e são indispensáveis para sua atividade catalítica, atuando como transportadoras de grupos químicos ou elétrons durante a reação. Muitas coenzimas são derivadas de vitaminas do complexo B: por exemplo, o NAD⁺ é derivado da niacina (vitamina B3) e o FAD, da riboflavina (vitamina B2). Essa relação explica por que deficiências vitamínicas podem comprometer diversas vias metabólicas — sem a vitamina, a coenzima não é produzida e a enzima perde sua função." }
        },    
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas aceleram as reações químicas porque:",
            answers = new string[] {
                "Aumentam a energia de ativação",
                "Diminuem a energia de ativação",
                "Fornecem calor à reação",
                "Transformam substratos em vitaminas"
            },
            correctIndex = 1,
            questionNumber = 55,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_055",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "As enzimas aceleram reações químicas ao reduzir a energia de ativação — a barreira energética que os reagentes precisam superar para se transformar em produtos. Elas fazem isso estabilizando o estado de transição da reação, ou seja, a configuração molecular de maior energia que ocorre entre reagentes e produtos. Com uma barreira mais baixa, muito mais moléculas de substrato possuem energia suficiente para reagir em um dado momento, aumentando enormemente a velocidade da reação sem alterar o equilíbrio termodinâmico ou consumir a enzima no processo." }
        },   
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que catalisa a quebra de amido em maltose é:",
            answers = new string[] {
                "Lactase",
                "Amilase",
                "Lipase",
                "Protease"
            },
            correctIndex = 1,
            questionNumber = 56,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_056",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A amilase é a enzima responsável por hidrolisar o amido — polissacarídeo de reserva dos vegetais composto por longas cadeias de glicose — em unidades menores, como a maltose (um dissacarídeo de duas glicoses). Ela cliva as ligações alfa-1,4-glicosídicas presentes na amilose e na amilopectina. No organismo humano, a digestão do amido começa na boca pela amilase salivar e continua no intestino delgado pela amilase pancreática, ilustrando como uma mesma classe de enzimas pode atuar em diferentes compartimentos digestivos." }
        },    
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A urease, enzima que degrada ureia, foi a primeira enzima cristalizada. Isso demonstrou que:",
            answers = new string[] {
                "Todas as enzimas são carboidratos",
                "Enzimas são proteínas",
                "Enzimas não podem ser isoladas",
                "Enzimas são apenas cofatores minerais"
            },
            correctIndex = 1,
            questionNumber = 57,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_057",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Em 1926, o bioquímico James Sumner conseguiu cristalizar a urease — enzima que catalisa a hidrólise da ureia em amônia e dióxido de carbono — e demonstrou que o cristal obtido era composto por proteína. Essa foi uma descoberta revolucionária, pois comprovou definitivamente que as enzimas são moléculas proteicas, encerrando décadas de debate sobre sua natureza química. O feito rendeu a Sumner o Prêmio Nobel de Química em 1946 e estabeleceu as bases para toda a enzimologia moderna." }
        }, 
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O inibidor competitivo atua:",
            answers = new string[] {
                "Ligando-se a um local diferente do sítio ativo",
                "Alterando irreversivelmente a enzima",
                "Compete com o substrato pelo sítio ativo",
                "Aumentando a afinidade da enzima pelo substrato"
            },
            correctIndex = 2,
            questionNumber = 58,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_058",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O inibidor competitivo possui estrutura molecular semelhante à do substrato, o que lhe permite se ligar ao sítio ativo da enzima e impedir temporariamente a ligação do substrato verdadeiro. Trata-se de uma competição direta e reversível: quanto mais inibidor houver em relação ao substrato, maior será o bloqueio; ao aumentar a concentração de substrato, ele pode deslocar o inibidor e restaurar a atividade enzimática. Esse tipo de inibição aumenta o Km aparente da enzima, mas não altera o Vmax quando o substrato está em excesso suficiente." }
        },   
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A função principal das enzimas é:",
            answers = new string[] {
                "Armazenar energia",
                "Acelerar reações químicas",
                "Servir como estrutura da célula",
                "Transportar oxigênio"
            },
            correctIndex = 1,
            questionNumber = 59,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_059",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A função principal das enzimas é atuar como catalisadores biológicos, acelerando as reações químicas do metabolismo celular ao reduzir a energia de ativação necessária para que ocorram. Sem enzimas, a maioria das reações bioquímicas seria tão lenta que seria incompatível com a manutenção da vida. Cada enzima é altamente específica, catalisando um tipo particular de reação e reconhecendo um substrato definido, o que permite à célula regular com precisão todas as suas vias metabólicas." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O local da enzima onde o substrato se liga é chamado de:",
            answers = new string[] {
                "Cofator",
                "Sítio ativo",
                "Grupo prostético",
                "Complexo enzimático"
            },
            correctIndex = 1,
            questionNumber = 60,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_060",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O sítio ativo é a região tridimensional da enzima onde o substrato se encaixa e onde ocorre a catálise. Ele é formado por um conjunto específico de aminoácidos que, após o dobramento da cadeia polipeptídica, cria uma cavidade com forma, polaridade e reatividade química complementares ao substrato. Essa complementaridade estrutural é o que confere às enzimas sua alta especificidade: apenas moléculas com a geometria e as propriedades químicas adequadas conseguem se ligar ao sítio ativo e ser catalisadas com eficiência." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O modelo que explica a interação enzima-substrato como chave-fechadura é conhecido como:",
            answers = new string[] {
                "Modelo do encaixe induzido",
                "Modelo da catálise covalente",
                "Modelo chave-fechadura",
                "Modelo do estado de transição"
            },
            correctIndex = 2,
            questionNumber = 61,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_061",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O modelo chave-fechadura, proposto por Emil Fischer em 1894, descreve a interação enzima-substrato como um encaixe rígido e preciso: o substrato (a chave) possui uma forma exatamente complementar ao sítio ativo da enzima (a fechadura). Esse modelo foi pioneiro ao explicar a especificidade enzimática, mas foi posteriormente complementado pelo modelo do ajuste induzido, proposto por Daniel Koshland em 1958, que reconhece que tanto a enzima quanto o substrato podem sofrer pequenas mudanças conformacionais ao se aproximarem, resultando em um encaixe mais dinâmico e realista." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Qual dos fatores abaixo não afeta a atividade enzimática?",
            answers = new string[] {
                "Temperatura",
                "pH",
                "Concentração de substrato",
                "Cor do substrato"
            },
            correctIndex = 3,
            questionNumber = 62,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_062",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A atividade enzimática é sensível a fatores que afetam as interações moleculares entre a enzima e o substrato ou a estabilidade da estrutura proteica, como temperatura, pH e concentração de substrato. A cor do substrato, no entanto, é uma propriedade relacionada à absorção de luz e não interfere em nenhuma dessas interações moleculares. Portanto, ela não tem nenhum efeito sobre a capacidade catalítica da enzima, sendo o único fator da lista que não influencia a atividade enzimática." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Moléculas não proteicas que auxiliam algumas enzimas em sua atividade são chamadas de:",
            answers = new string[] {
                "Cofatores",
                "Polissacarídeos",
                "Hormônios",
                "Nucleotídeos"
            },
            correctIndex = 0,
            questionNumber = 63,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_063",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "Cofatores são moléculas ou íons não proteicos que se associam a certas enzimas e são necessários para que elas exerçam plena atividade catalítica. Eles podem ser de natureza inorgânica — como íons metálicos de zinco, ferro, magnésio ou cobre — ou orgânica, caso em que recebem o nome específico de coenzimas. A enzima sem seu cofator é chamada de apoenzima e encontra-se inativa; a combinação entre a apoenzima e o cofator forma a holoenzima, que é a forma cataliticamente ativa." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A enzima que catalisa a quebra de moléculas pela adição de água é:",
            answers = new string[] {
                "Oxidorredutase",
                "Hidrolase",
                "Isomerase",
                "Ligase"
            },
            correctIndex = 1,
            questionNumber = 64,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_064",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "As hidrolases são enzimas que catalisam reações de hidrólise, ou seja, a quebra de ligações químicas pela adição de uma molécula de água. Elas constituem uma das seis grandes classes da classificação enzimática da IUBMB e são essenciais para a digestão de biomoléculas: lipases hidrolisam lipídios, proteases hidrolisam proteínas e amilases hidrolisam carboidratos, por exemplo. O próprio nome \\\"hidrolase\\\" já carrega a informação sobre seu mecanismo de ação, sendo uma nomenclatura sistemática baseada no tipo de reação catalisada." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A desnaturalização de uma enzima ocorre quando:",
            answers = new string[] {
                "A enzima é ativada por cofatores",
                "Há alteração em sua estrutura tridimensional",
                "O substrato se liga ao sítio ativo",
                "O pH se mantém constante"
            },
            correctIndex = 1,
            questionNumber = 65,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_065",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A desnaturação de uma enzima é o processo em que sua estrutura tridimensional é desfeita por agentes físicos ou químicos — como calor excessivo, variações extremas de pH ou solventes orgânicos — que rompem as interações não covalentes responsáveis pelo dobramento da proteína. Como a função catalítica depende diretamente da integridade do sítio ativo, que só existe graças ao dobramento correto da cadeia polipeptídica, a desnaturação leva à perda total ou parcial da atividade enzimática. Em geral, esse processo é irreversível nas condições fisiológicas." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas reduzem:",
            answers = new string[] {
                "A quantidade de solvente",
                "A energia de ativação da reação",
                "A quantidade de produtos formados",
                "A velocidade da reação"
            },
            correctIndex = 1,
            questionNumber = 66,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_066",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "O mecanismo central pelo qual as enzimas exercem sua função catalítica é a redução da energia de ativação da reação. Ao se ligar ao substrato e estabilizar o estado de transição, a enzima diminui a barreira energética que os reagentes precisam superar para se transformar em produtos, tornando a reação muito mais rápida. É importante destacar que as enzimas não alteram o equilíbrio termodinâmico da reação — ou seja, não modificam a quantidade final de produtos formados —, apenas aceleram a velocidade com que esse equilíbrio é atingido." }
        },
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "As enzimas apresentam elevada:",
            answers = new string[] {
                "Generalidade",
                "Inespecificidade",
                "Especificidade",
                "Toxicidade"
            },
            correctIndex = 2,
            questionNumber = 67,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_067",
            topic = "enzymes",
            subtopic = null,
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = null,
            prerequisites = null,
            questionHint = new QuestionHint { text = "A especificidade é uma das propriedades mais marcantes das enzimas: cada enzima reconhece e se liga a um substrato particular — ou a um grupo muito restrito de substratos — e catalisa apenas um tipo específico de reação. Essa seletividade é determinada pelo sítio ativo, cuja forma e propriedades químicas são complementares ao substrato de forma precisa. A alta especificidade enzimática é o que permite à célula coordenar centenas de vias metabólicas distintas simultaneamente, sem que as enzimas de uma via interfiram nas reações de outras." }
        }
    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.enzymes;
    public string GetDatabankName()  => "EnzymeQuestionDatabase";
    public string GetDisplayName()   => "Enzimas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
