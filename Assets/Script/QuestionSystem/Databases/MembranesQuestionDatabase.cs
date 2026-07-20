using System.Collections.Generic;
using QuestionSystem;

public class MembranesQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        // Question 001
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o principal componente de uma membrana biológica?",
            answers = new string[] {
                "Carboidratos", 
                "Lipídeos", 
                "Proteínas", 
                "Ácidos Nucleicos"},
            correctIndex = 1,
            questionNumber = 1,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_001",
            topic = "membranes",
            subtopic = "membrane_composition",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "lipids", "biological_membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe quais moléculas formam a base física da barreira celular. Pense no que permite criar uma região interna pouco favorável à água, enquanto as superfícies permanecem compatíveis com o meio aquoso dentro e fora da célula. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
       },

        // Question 002
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Quais os três principais tipos de lipídeos encontrados em membranas biológicas?",
            answers = new string[] {
                "Triacilgliceróis, fosfolipídeos, esfingolipídeos", 
                "Glicerofosfolipídeos, esfingolipídeos, esteroides", 
                "Ácidos graxos, colesterol, triglicerídeos", 
                "Ceras, esteroides, glicerofosfolipídeos"},
            correctIndex = 1,
            questionNumber = 2,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_002",
            topic = "membranes",
            subtopic = "membrane_lipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "glycerophospholipids", "sphingolipids", "sterols" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare moléculas estruturais de membrana com moléculas usadas principalmente como reserva energética. A resposta reúne classes que aparecem de modo recorrente em bicamadas, incluindo componentes com cabeças polares, esqueletos variados e compostos que ajustam propriedades físicas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
       },

        // Question 003
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que significa anfipático?",
            answers = new string[] { 
                "Apresentar regiões polares e apolares", 
                "Apenas polar", 
                "Apenas apolar", 
                "Solúvel apenas em água"},
            correctIndex = 0,
            questionNumber = 3,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_003",
            topic = "membranes",
            subtopic = "amphipathic_molecules",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "polar_region", "nonpolar_region", "phospholipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em uma molécula que precisa se organizar espontaneamente na água. Ela deve ter uma parte confortável em contato com o meio aquoso e outra que evita esse contato, favorecendo agregados como micelas ou bicamadas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
       },

        // Question 004
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a principal função da membrana plasmática?",
            answers = new string[] { 
                "Produção de energia", 
                "Síntese de proteínas", 
                "Manutenção do ambiente celular", 
                "Remoção de resíduos"},
            correctIndex = 2,
            questionNumber = 4,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_004",
            topic = "membranes",
            subtopic = "membrane_function",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "cellular_homeostasis", "selective_barrier" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Considere a membrana como uma fronteira dinâmica, não apenas uma parede. Ela separa meios, seleciona o que atravessa, permite comunicação e ajuda a manter condições internas adequadas para as reações celulares continuarem ocorrendo. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
       },

        // Question 005
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Que tipo de ligação une os ácidos graxos ao glicerol nos glicerofosfolipídeos?",
            answers = new string[] { 
                "Ligação peptídica", 
                "Ligação glicosídica", 
                "Ligação éster", 
                "Ligação fosfodiéster"},
            correctIndex = 2,
            questionNumber = 5,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_005",
            topic = "membranes",
            subtopic = "glycerophospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "ester_bond", "fatty_acids", "glycerol" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relembre como ácidos carboxílicos se unem a grupos hidroxila em moléculas orgânicas. Nos glicerofosfolipídeos, as cadeias derivadas de ácidos graxos ficam presas ao esqueleto de glicerol por esse tipo clássico de conexão. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
       },

        // Question 006
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual destes NÃO é um componente dos glicerofosfolipídeos?",
            answers = new string[] { 
                "Ácidos graxos", 
                "Glicerol", 
                "Esfingosina", 
                "Fosfato"},
            correctIndex = 2,
            questionNumber = 6,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_006",
            topic = "membranes",
            subtopic = "glycerophospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "phosphate_group", "fatty_acids", "glycerol" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Desmonte mentalmente um glicerofosfolipídeo: comece pelo esqueleto de três carbonos, adicione duas cadeias hidrofóbicas e depois um grupo carregado ligado ao terceiro carbono. A alternativa estranha pertence a outra família de moléculas de membrana. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
       },

        // Question 007
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função do grupo de cabeça polar em um fosfolipídeo?",
            answers = new string[] { 
                "Interage com a água", 
                "Forma a cauda hidrofóbica", 
                "Fornece rigidez estrutural", 
                "Armazena energia"},
            correctIndex = 0,
            questionNumber = 7,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_007",
            topic = "membranes",
            subtopic = "phospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "polar_head_group", "water_interaction", "amphipathic_molecules" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relacione a posição dessa parte da molécula com o ambiente ao redor da bicamada. A extremidade voltada para fora precisa lidar bem com o citosol ou o meio extracelular, enquanto as caudas ficam protegidas no interior. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
       },

        // Question 008
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Que tipo de ligação une o grupo fosfato ao glicerol em um glicerofosfolipídeo?",
            answers = new string[] { 
                "Ligação peptídica", 
                "Ligação glicosídica",
                 "Ligação éster", 
                 "Ligação fosfodiéster" },
            correctIndex = 3,
            questionNumber = 8,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_008",
            topic = "membranes",
            subtopic = "glycerophospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "phosphodiester_bond", "phosphate_group", "glycerol" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe que o grupo fosfato pode conectar mais de um álcool dentro da arquitetura do fosfolipídeo. Pense no nome usado quando um fosfato atua como ponte química entre duas regiões orgânicas contendo hidroxilas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 009
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o nome do glicerofosfolipídeo mais comum?",
            answers = new string[] { 
                "Esfingomielina", 
                "Fosfatidilcolina", 
                "Cardiolipina", 
                "Cerebrosídeo" },
            correctIndex = 1,
            questionNumber = 9,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_009",
            topic = "membranes",
            subtopic = "glycerophospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "phosphatidylcholine", "membrane_lipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Procure lembrar qual glicerofosfolipídeo aparece com grande frequência em membranas e contém um grupo de cabeça derivado de uma base nitrogenada comum. Ele é muito citado em exemplos de bicamadas e lipoproteínas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 010
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função da cardiolipina?",
            answers = new string[] { 
                "Isolamento", 
                "Armazenamento de energia", 
                "Encontrada principalmente em membranas mitocondriais", 
                "Sinalização celular" },
            correctIndex = 2,
            questionNumber = 10,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_010",
            topic = "membranes",
            subtopic = "specialized_membrane_lipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "cardiolipin", "mitochondrial_membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em uma membrana interna muito especializada, rica em processos de transferência de elétrons e produção de ATP. A molécula perguntada é especialmente associada à estabilidade e organização desse ambiente energético. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 011
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual destas substâncias não está presente em membranas celulares?",
            answers = new string[] {
                "AnswerImages/LipidDB/glicolipideo",
                "AnswerImages/LipidDB/porfirina",
                "AnswerImages/LipidDB/fosfatidilcolina",
                "AnswerImages/LipidDB/colesterol"
            },
            correctIndex = 1,
            questionNumber = 11,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_011",
            topic = "membranes",
            subtopic = "membrane_composition",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "membrane_lipids", "membrane_proteins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare as imagens como famílias químicas de biomoléculas. Procure estruturas típicas de membranas, como moléculas anfipáticas, esteróis ou lipídeos com açúcares. A opção fora do contexto tem outra função biológica principal e não compõe usualmente a bicamada. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 012
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a unidade estrutural básica dos esfingolipídeos?",
            answers = new string[] { 
                "Glicerol", 
                "Esfingosina", 
                "Ácido graxo", 
                "Fosfato" },
            correctIndex = 1,
            questionNumber = 12,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_012",
            topic = "membranes",
            subtopic = "sphingolipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "sphingosine", "membrane_lipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Para identificar essa família, não comece pelo glicerol. Pense em um esqueleto de longa cadeia contendo grupo amino e hidroxilas, capaz de receber ácido graxo e formar derivados importantes em membranas, especialmente no sistema nervoso. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 013
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Do que a ceramida é composta?",
            answers = new string[] { 
                "Esfingosina e um ácido graxo", 
                "Glicerol e ácidos graxos", 
                "Esfingosina e fosfato", 
                "Glicerol e esfingosina" },
            correctIndex = 0,
            questionNumber = 13,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_013",
            topic = "membranes",
            subtopic = "sphingolipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "ceramide", "sphingosine", "fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Imagine a estrutura mínima antes da adição de açúcares, fosfocolina ou outros grupos de cabeça. Ela surge quando o esqueleto característico dessa família recebe uma cadeia acila, formando o núcleo comum de moléculas mais complexas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 014
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que diz o modelo do mozaico fluido da formação de membranas celulares?",
            answers = new string[] {
                "Membranas apresentam-se como um grande mozaico de lipídeos.",
                "A estrutura de uma membrana celular não é estática, e os lipídeos podem movimentar-se através dela.",
                "O colesterol move-se livremente na membrana.",
                "Membranas são basicamente lipídeos, sem nenhum outro tipo de molécula em sua composição"
            },
            correctIndex = 1,
            questionNumber = 14,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_014",
            topic = "membranes",
            subtopic = "fluid_mosaic_model",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_fluidity", "lipid_movement" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Concentre-se na palavra fluido: o modelo não descreve uma parede rígida. Ele propõe uma organização dinâmica, com componentes distribuídos na bicamada e capazes de se deslocar lateralmente, mantendo a estrutura geral da membrana. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 015
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que são gangliosídeos?",
            answers = new string[] { 
                "Esfingolipídeos simples", 
                "Esfingolipídeos complexos com oligossacarídeos e ácido siálico", 
                "Glicerofosfolipídeos", 
                "Esteroides" },
            correctIndex = 1,
            questionNumber = 15,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_015",
            topic = "membranes",
            subtopic = "glycolipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "gangliosides", "oligosaccharides", "sialic_acid" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em derivados de uma família de lipídeos de membrana que carregam cadeias de açúcares na superfície celular. Esses grupos participam de reconhecimento e comunicação, e um açúcar ácido específico costuma aparecer em sua composição. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 016
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a estrutura básica dos esteroides?",
            answers = new string[] { 
                "Três anéis de seis carbonos e um anel de cinco carbonos", 
                "Uma longa cadeia de hidrocarbonetos", 
                "Uma estrutura de glicerol", 
                "Um grupo fosfato" },
            correctIndex = 0,
            questionNumber = 16,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_016",
            topic = "membranes",
            subtopic = "sterols",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "steroid_ring", "cholesterol" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Lembre que essa classe química tem um núcleo rígido, diferente de moléculas com longas caudas flexíveis. Procure a alternativa que descreve um conjunto de anéis fusionados, característico de hormônios esteroides e colesterol. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 017
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o esteroide mais abundante em células animais?",
            answers = new string[] { 
                "Estrogênio", 
                "Testosterona", 
                "Colesterol", 
                "Cortisol" },
            correctIndex = 2,
            questionNumber = 17,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_017",
            topic = "membranes",
            subtopic = "cholesterol",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "sterols", "animal_membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Entre os compostos listados, procure aquele que é componente estrutural frequente da membrana de células animais, não apenas um hormônio sinalizador. Ele aparece intercalado entre caudas de fosfolipídeos e influencia propriedades físicas da bicamada. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 018
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a principal função do colesterol nas membranas?",
            answers = new string[] { 
                "Armazenamento de energia", 
                "Transdução de sinal", 
                "Modulação da fluidez da membrana", 
                "Atividade enzimática" },
            correctIndex = 2,
            questionNumber = 18,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_018",
            topic = "membranes",
            subtopic = "cholesterol",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_fluidity", "animal_membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense no efeito de uma molécula rígida encaixada entre caudas hidrocarbonadas. Dependendo da temperatura, ela pode limitar movimentos excessivos ou impedir compactação demasiada, ajudando a ajustar uma propriedade física essencial da bicamada. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 019
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que é a monocamada de Langmuir?",
            answers = new string[] { 
                "Uma bicamada lipídica", 
                "Uma única camada de lipídeos na superfície da água", 
                "Uma micela lipídica", 
                "Um tipo de proteína" },
            correctIndex = 1,
            questionNumber = 19,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_019",
            topic = "membranes",
            subtopic = "membrane_models_history",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "langmuir_monolayer", "lipid_monolayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Imagine um experimento em que moléculas anfipáticas são espalhadas sobre a superfície da água. Em vez de formar uma membrana completa, elas ocupam apenas uma interface ar-água, permitindo medir área molecular e comportamento de compactação. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 020
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que Gorter e Grendel concluíram sobre o arranjo dos lipídeos na membrana celular?",
            answers = new string[] { 
                "Os lipídeos formam uma monocamada", 
                "Os lipídeos formam uma bicamada", 
                "Os lipídeos formam micelas", 
                "Os lipídeos estão distribuídos aleatoriamente" },
            correctIndex = 1,
            questionNumber = 20,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_020",
            topic = "membranes",
            subtopic = "membrane_models_history",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "gorter_grendel", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relembre que esses pesquisadores compararam a área ocupada por lipídeos extraídos de células com a área superficial das próprias células. O resultado sugeriu que havia material suficiente para duas faces organizadas, não apenas uma. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 021
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Que técnica Frye e Edidin usaram para estudar a fluidez da membrana?",
            answers = new string[] { 
                "Microscopia eletrônica", 
                "Difração de raios-X", 
                "Fusão celular com marcadores fluorescentes", 
                "Ressonância magnética nuclear" },
            correctIndex = 2,
            questionNumber = 21,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_021",
            topic = "membranes",
            subtopic = "membrane_models_history",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "frye_edidin_experiment", "fluorescent_labeling", "membrane_fluidity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em um experimento visual, feito com células de espécies diferentes, no qual proteínas marcadas por fluorescência foram acompanhadas após a união das membranas. A mistura dos marcadores revelou mobilidade lateral dos componentes. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 022
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que é o modelo do mosaico fluido?",
            answers = new string[] { 
                "Um modelo da estrutura da membrana celular", 
                "Um modelo de enovelamento de proteínas", 
                "Um modelo de replicação do DNA", 
                "Um modelo de metabolismo de carboidratos" },
            correctIndex = 0,
            questionNumber = 22,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_022",
            topic = "membranes",
            subtopic = "fluid_mosaic_model",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "membrane_structure", "membrane_proteins", "membrane_fluidity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Considere uma explicação geral para como lipídeos, proteínas e carboidratos se organizam na fronteira celular. O modelo descreve uma estrutura dinâmica, com componentes móveis, e serve como referência para entender permeabilidade, sinalização e transporte. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 023
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o principal tipo de movimento dos fosfolipídeos dentro de uma membrana?",
            answers = new string[] { 
                "Flip-flop", 
                "Difusão lateral", 
                "Rotação", 
                "Difusão transversal" },
            correctIndex = 1,
            questionNumber = 23,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_023",
            topic = "membranes",
            subtopic = "lipid_movement",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "lateral_diffusion", "membrane_fluidity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare movimentos frequentes e raros na bicamada. Trocar de uma face para outra exige superar uma barreira energética alta, enquanto deslocar-se no mesmo plano da membrana ocorre com muito mais facilidade e rapidez. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 024
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Utilizados na determinação do tipo sanguíneo",
                "Utilizados como biomedicamentos para várias doenças",
                "São marcadores tumorais",
                "Agem como hormônios no sistema nervoso central"
            },
            correctIndex = 0,
            questionNumber = 24,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/MembraneDB/membraneDB_ImageQuestionContainer24",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_024",
            topic = "membranes",
            subtopic = "membrane_carbohydrates",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "blood_type", "glycolipids", "cell_recognition" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe a imagem procurando cadeias lipídicas ligadas a uma região de carboidratos voltada para fora da membrana. Esse tipo de molécula costuma funcionar como identidade de superfície celular, permitindo interações específicas entre células e moléculas externas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 025
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Como os ácidos graxos saturados afetam a fluidez da membrana?",
            answers = new string[] { 
                "Aumentam a fluidez", 
                "Diminuem a fluidez", 
                "Não têm efeito", 
                "Aumentam a permeabilidade" },
            correctIndex = 1,
            questionNumber = 25,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_025",
            topic = "membranes",
            subtopic = "membrane_fluidity",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "saturated_fatty_acids", "lipid_packing" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense na geometria das caudas. Cadeias sem dobras se empacotam de maneira mais próxima, aumentando interações entre moléculas vizinhas. Esse arranjo reduz a liberdade de movimento dentro da bicamada em comparação com cadeias que apresentam curvaturas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 026
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Como os ácidos graxos insaturados afetam a fluidez da membrana?",
            answers = new string[] { 
                "Aumentam a fluidez", 
                "Diminuem a fluidez", 
                "Não têm efeito", 
                "Aumentam a permeabilidade" },
            correctIndex = 0,
            questionNumber = 26,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_026",
            topic = "membranes",
            subtopic = "membrane_fluidity",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "unsaturated_fatty_acids", "lipid_packing" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe o efeito de uma dobra em uma cauda hidrocarbonada. Quando as moléculas não conseguem se aproximar tanto, o empacotamento fica menos compacto, criando mais espaço para movimento lateral e flexibilidade na bicamada. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 027
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A bainha de mielina é composta principalmente por:",
            answers = new string[] {
                "Proteínas",
                "Glicerofosfolipídeos",
                "Esfingolipídeos",
                "Ácidos nucléicos"
            },
            correctIndex = 2,
            questionNumber = 27,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_027",
            topic = "membranes",
            subtopic = "sphingolipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "myelin_sheath", "nervous_system" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relacione a pergunta ao isolamento elétrico dos axônios. A bainha precisa de muitas moléculas de membrana especializadas, abundantes no tecido nervoso, que ajudam a formar camadas compactas ao redor das fibras. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 028
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o papel das proteínas na membrana celular?",
            answers = new string[] { 
                "Suporte estrutural", 
                "Transporte", 
                "Receptores", 
                "Todas as alternativas acima" },
            correctIndex = 3,
            questionNumber = 28,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_028",
            topic = "membranes",
            subtopic = "membrane_proteins",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "transport", "receptors", "enzymes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Proteínas de membrana não têm apenas uma tarefa. Algumas atravessam a bicamada, outras ficam associadas à superfície, e suas regiões expostas podem reconhecer sinais, catalisar reações, ancorar estruturas ou permitir passagem seletiva de substâncias. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 029
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O que é uma ligação fosfodiéster?",
            answers = new string[] { 
                "Uma ligação entre dois açúcares", 
                "Uma ligação entre um fosfato e dois álcoois", 
                "Uma ligação entre dois ácidos graxos", 
                "Uma ligação entre um fosfato e o glicerol" },
            correctIndex = 1,
            questionNumber = 29,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_029",
            topic = "membranes",
            subtopic = "glycerophospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "phosphodiester_bond", "phosphate_group" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em um grupo fosfato como uma ponte química. Ele pode ligar duas moléculas ou duas regiões orgânicas por meio de oxigênios, formando uma conexão importante tanto em fosfolipídeos quanto em ácidos nucleicos. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 030
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a diferença entre uma micela e uma bicamada?",
            answers = new string[] { 
                "As micelas são esféricas, as bicamadas são planares", 
                "As micelas são polares, as bicamadas são apolares", 
                "As micelas são encontradas no citoplasma, as bicamadas são encontradas na membrana", 
                "As micelas são pequenas, as bicamadas são grandes" },
            correctIndex = 0,
            questionNumber = 30,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_030",
            topic = "membranes",
            subtopic = "lipid_assemblies",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "micelles", "lipid_bilayer", "amphipathic_molecules" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare a forma resultante quando moléculas anfipáticas se agregam em água. Uma organização fecha as caudas no centro como uma esfera; a outra cria duas folhas opostas, adequadas para separar dois ambientes aquosos. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 031
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função da bicamada fosfolipídica?",
            answers = new string[] { 
                "Formar uma barreira seletivamente permeável", 
                "Fornecer energia para a célula", 
                "Armazenar informação genética", 
                "Sintetizar proteínas" },
            correctIndex = 0,
            questionNumber = 31,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_031",
            topic = "membranes",
            subtopic = "lipid_bilayer",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "selective_barrier", "phospholipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense no arranjo com cabeças voltadas para meios aquosos e caudas escondidas no interior. Essa estrutura cria uma fronteira que dificulta a passagem livre de muitas substâncias, mas ainda permite controle por canais e transportadores. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 032
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a importância da fluidez da membrana?",
            answers = new string[] { 
                "Permite o movimento e a função das proteínas da membrana", 
                "Permite a sinalização celular", 
                "Mantém a integridade da membrana", 
                "Todas as alternativas acima" },
            correctIndex = 3,
            questionNumber = 32,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_032",
            topic = "membranes",
            subtopic = "membrane_fluidity",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_function", "protein_movement", "lipid_movement" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A fluidez não é apenas uma característica estética da membrana. Ela influencia deslocamento de moléculas, funcionamento de proteínas, reparo da bicamada, comunicação celular e manutenção da barreira; por isso, mais de um aspecto pode ser relevante. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 033
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Quais fatores afetam a fluidez da membrana?",
            answers = new string[] { 
                "Temperatura, composição lipídica, teor de colesterol", 
                "pH, pressão, atividade enzimática", 
                "Intensidade de luz, concentração de oxigênio, salinidade", 
                "Todas as alternativas acima" },
            correctIndex = 0,
            questionNumber = 33,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_033",
            topic = "membranes",
            subtopic = "membrane_fluidity",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "temperature_effects", "lipid_composition", "cholesterol" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Ao avaliar fluidez, considere tanto condições externas quanto composição da bicamada. Temperatura altera movimento molecular, caudas lipídicas mudam empacotamento, e moléculas rígidas intercaladas podem amortecer variações físicas da membrana. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 034
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o papel dos glicolipídeos na membrana celular?",
            answers = new string[] { 
                "Reconhecimento celular", 
                "Armazenamento de energia", 
                "Suporte estrutural", 
                "Atividade enzimática" },
            correctIndex = 0,
            questionNumber = 34,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_034",
            topic = "membranes",
            subtopic = "glycolipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "cell_recognition", "membrane_carbohydrates" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense nos carboidratos expostos na face externa da célula. Eles funcionam como etiquetas moleculares, ajudando outras células, proteínas e componentes do sistema imune a distinguir tipos celulares e estados fisiológicos. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 035
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a função do movimento flip-flop dos fosfolipídeos?",
            answers = new string[] { 
                "Manter a assimetria da membrana", 
                "Facilitar o transporte de proteínas", 
                "Regular a fluidez da membrana", 
                "Armazenar energia" },
            correctIndex = 0,
            questionNumber = 35,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_035",
            topic = "membranes",
            subtopic = "lipid_movement",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "flip_flop", "membrane_asymmetry" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Esse movimento troca moléculas entre as duas faces da bicamada e é energeticamente desfavorável sem enzimas. Reflita por que a célula controlaria essa troca para manter diferenças entre a face interna e externa. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 036
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o significado do experimento conduzido por Frye e Edidin?",
            answers = new string[] { 
                "Demonstrou a fluidez da membrana", 
                "Confirmou a estrutura da bicamada lipídica", 
                "Identificou as proteínas da membrana", 
                "Mediu a espessura da membrana" },
            correctIndex = 0,
            questionNumber = 36,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_036",
            topic = "membranes",
            subtopic = "membrane_models_history",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "frye_edidin_experiment", "membrane_fluidity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relembre o desenho experimental: células com marcadores diferentes foram fundidas e, depois de algum tempo, os sinais se misturaram pela membrana. O ponto central era demonstrar mobilidade dos componentes, não medir espessura ou identificar todas as proteínas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 037
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Formam micelas perfeitas em solução aquosa.",
                "São importantes para a formação de membranas de camada simples",
                "Fazem parte da composição de muitas membranas biológicas",
                "Ao reagirem com bases formam ótimos biocombustíveis"
            },
            correctIndex = 2,
            questionNumber = 37,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/MembraneDB/membraneDB_ImageQuestionContainer37",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_037",
            topic = "membranes",
            subtopic = "glycerophospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "membrane_lipids", "biological_membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na imagem, repare no esqueleto de glicerol, no fosfato e nas duas caudas, uma delas com dobra. Esse formato anfipático favorece participação em estruturas de membrana, pois combina uma região compatível com água e outra hidrofóbica. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 038
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Glicerofosfoliplideos",
                "Esfingolipídeos",
                "Esteróis",
                "Ceramidas"
            },
            correctIndex = 0,
            questionNumber = 38,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/MembraneDB/membraneDB_ImageQuestionContainer38",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_038",
            topic = "membranes",
            subtopic = "glycerophospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "membrane_lipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe que a molécula mostrada tem glicerol, grupo fosfato, cabeça polar nitrogenada e duas cadeias de ácidos graxos. A pergunta pede reconhecer a família estrutural, não apenas identificar uma cadeia ou um grupo isolado. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 039
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Indique abaixo o lipídeo pertencente a família dos esfingolipídeos",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/glicolipideo",
                "AnswerImages/LipidDB/fosfatidilcolina",
                "AnswerImages/LipidDB/colesterol"
            },
            correctIndex = 1,
            questionNumber = 39,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_039",
            topic = "membranes",
            subtopic = "sphingolipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "glycolipids", "membrane_lipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare as estruturas desenhadas nas alternativas. Procure aquela que combina porção lipídica com carboidrato e pertence a uma família muito associada à superfície celular e ao reconhecimento, diferente de esteróis ou fosfolipídeos baseados em glicerol. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 040
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a importância da composição lipídica na manutenção da fluidez da membrana?",
            answers = new string[] { 
                "Ácidos graxos saturados diminuem a fluidez, ácidos graxos insaturados aumentam a fluidez", 
                "Ácidos graxos saturados aumentam a fluidez, ácidos graxos insaturados diminuem a fluidez", 
                "A composição lipídica não tem efeito na fluidez", 
                "O comprimento das caudas dos ácidos graxos determina a fluidez" },
            correctIndex = 0,
            questionNumber = 40,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_040",
            topic = "membranes",
            subtopic = "membrane_fluidity",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "saturated_fatty_acids", "unsaturated_fatty_acids", "lipid_composition" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Para responder, pense em como diferentes caudas ocupam espaço na bicamada. Cadeias retas tendem a se aproximar mais, enquanto cadeias com dobras reduzem empacotamento. A fluidez depende desse equilíbrio entre ordem e movimento. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 041
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o significado do modelo do mosaico fluido da membrana?",
            answers = new string[] { 
                "Explica a fluidez da membrana e a mobilidade das proteínas", 
                "Descreve a interação entre lipídeos e proteínas", 
                "Fornece uma estrutura para entender a função da membrana", 
                "Todas as alternativas acima" },
            correctIndex = 3,
            questionNumber = 41,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_041",
            topic = "membranes",
            subtopic = "fluid_mosaic_model",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_structure", "membrane_fluidity", "membrane_proteins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O modelo ajuda a juntar várias ideias: a membrana é uma bicamada dinâmica, contém proteínas distribuídas de modo irregular e permite mobilidade lateral. Se uma alternativa sintetiza esses aspectos complementares, ela merece atenção especial. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 042
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual o papel dos componentes de carboidratos na membrana celular?",
            answers = new string[] { 
                "Sinalização e reconhecimento celular", 
                "Suporte estrutural", 
                "Respostas imunológicas", 
                "Todas as alternativas acima" },
            correctIndex = 3,
            questionNumber = 42,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_042",
            topic = "membranes",
            subtopic = "membrane_carbohydrates",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "cell_recognition", "cell_communication", "glycoproteins", "glycolipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Carboidratos de membrana geralmente ficam expostos para o lado externo, ligados a lipídeos ou proteínas. Pense em como essas cadeias podem atuar como sinais de identidade, pontos de comunicação e elementos reconhecidos por outras células. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 043
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual a diferença entre proteínas integrais e periféricas da membrana?",
            answers = new string[] { 
                "As proteínas integrais atravessam a membrana, as proteínas periféricas estão frouxamente associadas", 
                "As proteínas integrais são hidrofílicas, as proteínas periféricas são hidrofóbicas", 
                "As proteínas integrais são encontradas no citoplasma, as proteínas periféricas são encontradas na superfície", 
                "As proteínas integrais são enzimas, as proteínas periféricas são estruturais" },
            correctIndex = 0,
            questionNumber = 43,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_043",
            topic = "membranes",
            subtopic = "membrane_proteins",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "integral_proteins", "peripheral_proteins", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare a profundidade da associação com a bicamada. Algumas proteínas têm regiões hidrofóbicas inseridas no interior lipídico, enquanto outras interagem mais superficialmente com cabeças polares, proteínas vizinhas ou o citoesqueleto. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 044
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A membrana plasmática é composta principalmente por:",
            answers = new string[] { 
                "Proteínas e ácidos nucleicos", 
                "Lipídios e proteínas", 
                "Carboidratos e aminoácidos", 
                "Água e sais minerais" },
            correctIndex = 1,
            questionNumber = 44,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_044",
            topic = "membranes",
            subtopic = "membrane_composition",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "lipids", "membrane_proteins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Lembre-se de que a membrana precisa tanto de uma barreira flexível quanto de componentes funcionais para transporte, sinalização e reconhecimento. As duas classes mais abundantes cumprem essas funções estruturais e operacionais em conjunto. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 045
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O modelo aceito para descrever a estrutura da membrana plasmática é chamado:",
            answers = new string[] { 
                "Modelo mosaico fluido", 
                "Modelo chave-fechadura", 
                "Modelo helicoidal", 
                "Modelo tripla hélice" },
            correctIndex = 0,
            questionNumber = 45,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_045",
            topic = "membranes",
            subtopic = "fluid_mosaic_model",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "membrane_structure" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Procure o nome do modelo que combina a ideia de componentes variados distribuídos como peças e, ao mesmo tempo, capazes de se mover lateralmente. Ele substituiu visões mais rígidas da membrana plasmática. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 046
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual é o lipídeo mais abundante nas membranas celulares?",
            answers = new string[] { 
                "Triglicerídeos", 
                "Fosfolipídios", 
                "Esteroides", 
                "Cerídeos" },
            correctIndex = 1,
            questionNumber = 46,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_046",
            topic = "membranes",
            subtopic = "phospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "membrane_lipids", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense no tipo de molécula que forma espontaneamente bicamadas em água por ter cabeça hidrofílica e caudas hidrofóbicas. Moléculas usadas para reserva energética ou proteção cerosa não são a base estrutural dominante das membranas celulares. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 047
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A bicamada lipídica é formada por fosfolipídios que apresentam regiões:",
            answers = new string[] { 
                "Totalmente polares", 
                "Totalmente apolares", 
                "Polares e apolares (anfipáticas)", 
                "Apenas hidrofílicas" },
            correctIndex = 2,
            questionNumber = 47,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_047",
            topic = "membranes",
            subtopic = "lipid_bilayer",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "phospholipids", "amphipathic_molecules" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Analise por que fosfolipídios formam bicamadas em meio aquoso. Uma parte precisa ficar em contato com água, enquanto outra se agrupa no interior para evitar esse contato. Essa dupla natureza explica a organização espontânea. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 048
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A bicamada lipídica é formada por fosfolipídios que apresentam regiões:",
            answers = new string[] { 
                "Totalmente polares", 
                "Totalmente apolares", 
                "Polares e apolares (anfipáticas)", 
                "Apenas hidrofílicas" },
            correctIndex = 2,
            questionNumber = 48,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_048",
            topic = "membranes",
            subtopic = "lipid_bilayer",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "phospholipids", "amphipathic_molecules" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A mesma lógica vale para qualquer bicamada fosfolipídica: observe a presença de uma região que interage bem com água e outra formada por cadeias hidrocarbonadas. Essa combinação orienta as moléculas em duas folhas opostas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 049
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O colesterol, presente nas membranas, tem como função principal:",
            answers = new string[] { 
                "Armazenar energia", 
                "Regular a fluidez da membrana", 
                "Transportar oxigênio", 
                "Produzir ATP" },
            correctIndex = 1,
            questionNumber = 49,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "membranes_049",
            topic = "membranes",
            subtopic = "cholesterol",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_fluidity", "animal_membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Imagine uma molécula rígida inserida entre caudas de fosfolipídios. Ela não serve como combustível imediato nem carrega oxigênio; sua presença altera o quanto as caudas se movem e se compactam na membrana. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 050
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As proteínas que atravessam toda a bicamada lipídica são chamadas de:",
            answers = new string[] { 
                "Proteínas periféricas", 
                "Proteínas integrais", 
                "Enzimas extracelulares", 
                "Proteínas nucleares" },
            correctIndex = 1,
            questionNumber = 50,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_050",
            topic = "membranes",
            subtopic = "membrane_proteins",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "integral_proteins", "transmembrane_proteins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Visualize a bicamada como uma faixa com interior hidrofóbico. Proteínas que passam de um lado ao outro precisam ter regiões compatíveis com esse interior e partes expostas aos meios aquosos, formando canais, receptores ou transportadores. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 051
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Os carboidratos presentes na membrana estão associados principalmente a:",
            answers = new string[] { 
                "Reconhecimento celular", 
                "Produção de energia imediata", 
                "Transporte ativo", 
                "Síntese proteica" },
            correctIndex = 0,
            questionNumber = 51,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_051",
            topic = "membranes",
            subtopic = "membrane_carbohydrates",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "cell_recognition", "glycoproteins", "glycolipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Carboidratos de membrana ficam geralmente voltados para o exterior, presos a proteínas ou lipídeos. Pense neles como marcas superficiais que ajudam células e moléculas a se identificarem durante interações, adesão e respostas imunes. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 052
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O transporte de moléculas contra o gradiente de concentração, com gasto de energia, é chamado:",
            answers = new string[] { 
                "Difusão simples", 
                "Difusão facilitada", 
                "Transporte ativo", 
                "Osmose" },
            correctIndex = 2,
            questionNumber = 52,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_052",
            topic = "membranes",
            subtopic = "membrane_transport",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "active_transport", "energy_use", "concentration_gradient" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare o sentido do movimento com a tendência natural de difusão. Se a molécula precisa ir para uma região onde já está mais concentrada, a célula deve investir energia e usar proteínas específicas. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 053
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A passagem de água pela membrana sem gasto de energia recebe o nome de:",
            answers = new string[] { 
                "Osmose", 
                "Transporte ativo", 
                "Endocitose", 
                "Exocitose" },
            correctIndex = 0,
            questionNumber = 53,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_053",
            topic = "membranes",
            subtopic = "osmosis",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "passive_transport", "water_transport" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense especificamente no movimento da água através de uma membrana sem consumo de ATP. A direção depende da diferença de concentração de solutos entre os lados, e canais especializados podem acelerar esse fluxo. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 054
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual dos processos abaixo envolve a entrada de partículas grandes ou fluidos pela membrana?",
            answers = new string[] { 
                "Osmose", 
                "Difusão simples", 
                "Endocitose", 
                "Transporte passivo" },
            correctIndex = 2,
            questionNumber = 54,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_054",
            topic = "membranes",
            subtopic = "vesicular_transport",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "endocytosis", "large_particles" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Quando a partícula é grande demais para atravessar por canais ou se dissolver na bicamada, a membrana pode se deformar e envolver o material. Esse processo usa vesículas e é diferente da simples passagem molecular. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 055
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A função principal das proteínas de membrana é:",
            answers = new string[] { 
                "Atuar como enzimas, transportadores ou receptores", 
                "Fornecer energia para a célula", 
                "Servir como reserva de aminoácidos", 
                "Produzir ATP" },
            correctIndex = 0,
            questionNumber = 55,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_055",
            topic = "membranes",
            subtopic = "membrane_proteins",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "transporters", "receptors", "enzymes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Para escolher, lembre que proteínas de membrana costumam ser peças funcionais versáteis. Elas podem reconhecer sinais, acelerar reações, formar passagens seletivas, ancorar estruturas e conectar a célula ao ambiente externo. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 056
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O colesterol na membrana plasmática atua principalmente:",
            answers = new string[] { 
                "Fornecendo energia à célula", 
                "Estabilizando a fluidez da membrana", 
                "Participando da respiração celular", 
                "Transportando oxigênio" },
            correctIndex = 1,
            questionNumber = 56,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_056",
            topic = "membranes",
            subtopic = "cholesterol",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_fluidity", "membrane_stability" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em como uma molécula rígida, intercalada entre fosfolipídios, pode impedir tanto excesso de movimento quanto compactação exagerada. O efeito principal é manter propriedades físicas adequadas da bicamada em diferentes condições. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 057
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O transporte passivo é caracterizado por:",
            answers = new string[] { 
                "Consumo de ATP", 
                "Movimento contra o gradiente de concentração", 
                "Movimento a favor do gradiente de concentração sem gasto de energia", 
                "Exclusivamente realizado por proteínas" },
            correctIndex = 2,
            questionNumber = 57,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_057",
            topic = "membranes",
            subtopic = "membrane_transport",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "passive_transport", "concentration_gradient" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe duas pistas: direção do gradiente e uso de energia. Nesse tipo de transporte, a molécula segue a tendência natural de concentração, portanto a célula não precisa gastar ATP diretamente para impulsionar o movimento. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 058
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual das opções abaixo é um exemplo de transporte ativo?",
            answers = new string[] { 
                "Difusão simples", 
                "Difusão facilitada", 
                "Osmose", 
                "Bomba de sódio e potássio" },
            correctIndex = 3,
            questionNumber = 58,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_058",
            topic = "membranes",
            subtopic = "membrane_transport",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "active_transport", "sodium_potassium_pump", "energy_use" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Procure o exemplo em que íons são movidos de modo controlado contra suas tendências de concentração. Esse processo exige energia química e uma proteína específica, diferente de difusão ou movimento espontâneo da água. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 059
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A entrada de água através da membrana por diferença de concentração é chamada de:",
            answers = new string[] { 
                "Pinocitose", 
                "Osmose", 
                "Difusão simples", 
                "Transporte ativo" },
            correctIndex = 1,
            questionNumber = 59,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "membranes_059",
            topic = "membranes",
            subtopic = "osmosis",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "water_transport", "concentration_gradient" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Quando a pergunta fala em água e diferença de concentração, pense no movimento do solvente através da membrana. Ele ocorre para equilibrar concentrações de solutos, sem ser englobamento vesicular nem transporte movido por ATP. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 060
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As glicoproteínas e glicolipídeos da membrana têm papel fundamental em:",
            answers = new string[] { 
                "Armazenar energia", 
                "Formação de ATP", 
                "Reconhecimento celular e comunicação", 
                "Produção de hormônios" },
            correctIndex = 2,
            questionNumber = 60,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_060",
            topic = "membranes",
            subtopic = "membrane_carbohydrates",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "glycoproteins", "glycolipids", "cell_recognition", "cell_communication" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Essas moléculas ficam na superfície celular com cadeias de carboidratos expostas. Elas funcionam como marcas de identidade e pontos de interação, permitindo que células reconheçam vizinhas, respondam a sinais e participem de comunicação tecidual. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 061
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O principal modelo que descreve a estrutura da membrana plasmática é chamado de:",
            answers = new string[] { 
                "Modelo do mosaico fluido", 
                "Modelo da dupla hélice", 
                "Modelo chave-fechadura", 
                "Modelo do tapete contínuo" },
            correctIndex = 0,
            questionNumber = 61,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_061",
            topic = "membranes",
            subtopic = "fluid_mosaic_model",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "membrane_structure" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Lembre o nome do modelo que descreve a membrana como uma bicamada dinâmica, com proteínas distribuídas e móveis. Ele não se refere ao DNA, a encaixe enzimático específico nem a uma superfície rígida contínua. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 062
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As membranas biológicas são constituídas principalmente por:",
            answers = new string[] { 
                "Proteínas e ácidos nucleicos", 
                "Lipídeos e carboidratos", 
                "Lipídeos e proteínas", 
                "Carboidratos e aminoácidos" },
            correctIndex = 2,
            questionNumber = 62,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_062",
            topic = "membranes",
            subtopic = "membrane_composition",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "lipids", "membrane_proteins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense nos dois grupos que, juntos, explicam estrutura e função da membrana: um forma a barreira anfipática básica e o outro executa tarefas como transporte, recepção de sinais, ancoragem e atividade enzimática. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 063
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Os lipídeos mais abundantes nas membranas celulares são:",
            answers = new string[] { 
                "Glicerídeos", 
                "Fosfolipídeos", 
                "Esteroides", 
                "Carotenoides" },
            correctIndex = 1,
            questionNumber = 63,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_063",
            topic = "membranes",
            subtopic = "phospholipids",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "membrane_lipids", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Entre as opções, procure moléculas anfipáticas capazes de montar bicamadas estáveis em água. Elas possuem cabeça polar e caudas hidrofóbicas, formando a matriz estrutural mais comum das membranas celulares. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 064
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Qual lipídeo ajuda a regular a fluidez da membrana plasmática em células animais?",
            answers = new string[] { 
                "Triglicerídeos", 
                "Colesterol", 
                "Carotenoides", 
                "Ácidos graxos livres" },
            correctIndex = 1,
            questionNumber = 64,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_064",
            topic = "membranes",
            subtopic = "cholesterol",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "membrane_fluidity", "animal_membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Em células animais, uma molécula rígida se encaixa entre fosfolipídios e ajuda a ajustar o empacotamento das caudas. Ela não é uma reserva energética principal nem pigmento; seu papel é modular propriedades físicas da membrana. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 065
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "As proteínas que atravessam completamente a bicamada lipídica são chamadas de:",
            answers = new string[] { 
                "Proteínas periféricas", 
                "Proteínas integrais de membrana", 
                "Enzimas citoplasmáticas", 
                "Proteínas ribossômicas" },
            correctIndex = 1,
            questionNumber = 65,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_065",
            topic = "membranes",
            subtopic = "membrane_proteins",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "integral_proteins", "transmembrane_proteins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Imagine uma proteína que possui partes expostas nos dois lados da membrana e regiões hidrofóbicas atravessando o interior da bicamada. Esse posicionamento permite formar canais, transportadores ou receptores que conectam ambientes diferentes. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 066
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Os carboidratos presentes na membrana estão ligados principalmente a:",
            answers = new string[] { 
                "Aminoácidos essenciais", 
                "DNA e RNA", 
                "Fosfolipídeos e proteínas", 
                "Colesterol e triglicerídeos" },
            correctIndex = 2,
            questionNumber = 66,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_066",
            topic = "membranes",
            subtopic = "membrane_carbohydrates",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "glycoproteins", "glycolipids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os carboidratos de membrana raramente ficam livres na bicamada. Eles aparecem como cadeias ligadas a moléculas maiores, formando glicoconjugados expostos na superfície celular e importantes para identificação, comunicação e interação entre células. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 067
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A função mais importante da membrana plasmática é:",
            answers = new string[] { 
                "Produzir energia", 
                "Estocar material genético", 
                "Fosfolipídeos e proteínas", 
                "Regular a entrada e saída de substâncias" },
            correctIndex = 3,
            questionNumber = 67,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_067",
            topic = "membranes",
            subtopic = "membrane_function",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "selective_barrier", "transport_regulation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense na membrana como uma fronteira seletiva. Mais do que produzir energia ou guardar informação genética, ela controla trocas com o ambiente, mantendo condições internas compatíveis com metabolismo, sinalização e sobrevivência celular. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 068
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "O transporte que ocorre a favor do gradiente de concentração, sem gasto de energia, é chamado de:",
            answers = new string[] { 
                "Transporte ativo", 
                "Osmose", 
                "Transporte passivo", 
                "Endocitose" },
            correctIndex = 2,
            questionNumber = 68,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_068",
            topic = "membranes",
            subtopic = "membrane_transport",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "passive_transport", "concentration_gradient" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare com transporte que usa ATP para vencer gradientes. Aqui, a substância segue a tendência natural, indo do lado mais concentrado para o menos concentrado, podendo ou não usar canais, mas sem gasto energético direto. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 069
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "A difusão facilitada se diferencia da difusão simples porque:",
            answers = new string[] { 
                "Precisa de energia (ATP)", 
                "Utiliza proteínas transportadoras ou canais", 
                "Só ocorre em soluções hipertônicas", 
                "É exclusiva de bactérias" },
            correctIndex = 1,
            questionNumber = 69,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_069",
            topic = "membranes",
            subtopic = "membrane_transport",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "facilitated_diffusion", "channel_proteins", "carrier_proteins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na difusão simples, moléculas pequenas e compatíveis atravessam diretamente a bicamada. Em outro caso, a passagem ainda segue o gradiente, mas depende de estruturas específicas que oferecem caminho seletivo para solutos polares ou carregados. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        },

        // Question 070
        new Question
        {
            questionDatabankName = "MembranesQuestionDatabase",
            questionText = "Quando a célula engloba partículas grandes por meio da membrana, esse processo é chamado de:",
            answers = new string[] { 
                "Exocitose", 
                "Pinocitose", 
                "Fagocitose", 
                "Difusão" },
            correctIndex = 2,
            questionNumber = 70,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "membranes_070",
            topic = "membranes",
            subtopic = "vesicular_transport",
            displayName = "Membranas Biológicas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "phagocytosis", "endocytosis", "large_particles" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Quando a célula precisa internalizar partículas grandes, a membrana se projeta ao redor do material e forma uma vesícula. Compare esse mecanismo com a entrada de líquidos, a saída de vesículas e a difusão molecular. Elimine opções que descrevem outro contexto e relacione a pista ao papel da membrana." }
        }
    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.membranes;
    public string GetDatabankName()  => "MembranesQuestionDatabase";
    public string GetDisplayName()   => "Membranas Biológicas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;

}