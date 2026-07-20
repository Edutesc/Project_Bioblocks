using System.Collections.Generic;
using QuestionSystem;

public class EnzymeQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        // // Question 001
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "O que são enzimas?",
        //     answers = new string[] {
        //         "Catalisadores químicos inorgânicos.",
        //         "Catalisadores biológicos, principalmente proteínas.",
        //         "Substratos que participam de reações químicas.",
        //         "Produtos de reações químicas."
        //     },
        //     correctIndex = 1,
        //     questionNumber = 1,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_001",
        //     topic = "enzymes",
        //     subtopic = "enzyme_definition",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Remember,
        //     conceptTags = new List<string> { "biological_catalysts", "proteins" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Compare as alternativas pensando no papel de uma molécula dentro da reação. Uma enzima participa facilitando transformações químicas, mas não é o reagente que será convertido nem o produto final formado. Considere também de que tipo de molécula celular ela costuma ser feita." }
        // },

        // // Question 002
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Qual a principal função de uma enzima?",
        //     answers = new string[] {
        //         "Sintetizar proteínas.",
        //         "Aumentar a velocidade de uma reação.",
        //         "Regular a temperatura corporal.",
        //         "Transportar oxigênio."
        //     },
        //     correctIndex = 1,
        //     questionNumber = 2,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_002",
        //     topic = "enzymes",
        //     subtopic = "enzyme_catalysis",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Remember,
        //     conceptTags = new List<string> { "reaction_rate", "biological_catalysts" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Pense no que uma célula precisa para que reações ocorram em tempo compatível com a vida. A enzima não deve ser vista como fonte de energia, transportador ou produto, mas como um componente que altera a facilidade com que uma transformação acontece." }
        // },

        // // Question 003
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Como as enzimas aumentam a velocidade das reações?",
        //     answers = new string[] {
        //         "Aumentando a energia de ativação.",
        //         "Diminuindo a energia de ativação.",
        //         "Alterando o equilíbrio da reação.",
        //         "Aumentando a concentração de substrato."
        //     },
        //     correctIndex = 1,
        //     questionNumber = 3,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_003",
        //     topic = "enzymes",
        //     subtopic = "activation_energy",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Understand,
        //     conceptTags = new List<string> { "enzyme_catalysis", "reaction_rate" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Relacione velocidade de reação com a barreira que separa reagentes e produtos. A enzima oferece um caminho alternativo para a transformação química, tornando mais fácil atingir o estado intermediário necessário sem mudar o equilíbrio final da reação." }
        // },

        // // Question 004
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Por que o estado de transição é central para entender a catálise enzimática?",
        //     answers = new string[] {
        //         "Porque é o estado mais estável e final da reação.",
        //         "Porque é o ponto de maior energia que a enzima ajuda a estabilizar.",
        //         "Porque corresponde ao substrato antes de se ligar à enzima.",
        //         "Porque representa o produto já liberado pela enzima."
        //     },
        //     correctIndex = 1,
        //     questionNumber = 4,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_004",
        //     topic = "enzymes",
        //     subtopic = "transition_state",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Understand,
        //     conceptTags = new List<string> { "transition_state", "activation_energy", "enzyme_catalysis" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Imagine a reação como uma trilha com um ponto de maior dificuldade entre início e fim. A pergunta pede por que esse ponto é importante para entender a ação catalítica. Foque no que a enzima faz com esse momento instável da transformação." }
        // },

        // // Question 005
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Ao comparar duas reações, uma com energia de ativação alta e outra com energia de ativação baixa, qual tende a ocorrer mais rapidamente nas mesmas condições?",
        //     answers = new string[] {
        //         "A reação com energia de ativação mais baixa.",
        //         "A reação com energia de ativação mais alta.",
        //         "As duas terão sempre a mesma velocidade.",
        //         "A reação com menor quantidade de produto final."
        //     },
        //     correctIndex = 0,
        //     questionNumber = 5,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_005",
        //     topic = "enzymes",
        //     subtopic = "activation_energy",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Understand,
        //     conceptTags = new List<string> { "activation_energy", "reaction_rate", "enzyme_catalysis" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Compare as duas reações como obstáculos de alturas diferentes. Nas mesmas condições, moléculas atravessam com mais frequência o obstáculo que exige menor energia para iniciar a transformação. Use essa ideia para relacionar barreira energética e velocidade observada." }
        // },

        // // Question 006
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Uma empresa quer substituir um processo químico que exige alta temperatura por uma rota mais compatível com alimentos. Que propriedade das enzimas justifica essa escolha?",
        //     answers = new string[] {
        //         "Atuam em condições brandas compatíveis com sistemas biológicos.",
        //         "Funcionam apenas em solventes orgânicos concentrados.",
        //         "Aumentam a temperatura ótima de qualquer reação.",
        //         "Eliminam a necessidade de substrato na reação."
        //     },
        //     correctIndex = 0,
        //     questionNumber = 6,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_006",
        //     topic = "enzymes",
        //     subtopic = "enzyme_conditions",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Apply,
        //     conceptTags = new List<string> { "physiological_conditions", "industry", "enzyme_applications" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Pense nas limitações de processos industriais envolvendo alimentos: calor excessivo pode degradar compostos sensíveis e aumentar custos. A propriedade mais útil das enzimas nesse contexto envolve realizar transformações específicas sem exigir condições físicas extremas." }
        // },

        // // Question 007
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "O sítio ativo de uma enzima é:",
        //     answers = new string[] {
        //         "A região onde a enzima se liga ao produto.",
        //         "A região onde a enzima se liga ao substrato.",
        //         "A região responsável pela regulação da enzima.",
        //         "A região onde a enzima se liga a cofatores."
        //     },
        //     correctIndex = 1,
        //     questionNumber = 7,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_007",
        //     topic = "enzymes",
        //     subtopic = "active_site",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Remember,
        //     conceptTags = new List<string> { "enzyme_substrate_interaction", "substrate_binding" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Observe que a pergunta trata de uma região da própria enzima, não de uma molécula externa. Lembre que a função catalítica depende de uma parte com forma e propriedades químicas adequadas para reconhecer o reagente específico." }
        // },

        // // Question 008
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "O modelo chave-fechadura descreve a interação enzima-substrato como:",
        //     answers = new string[] {
        //         "Um ajuste induzido.",
        //         "Uma ligação covalente.",
        //         "Um encaixe complementar.",
        //         "Uma interação hidrofóbica."
        //     },
        //     correctIndex = 2,
        //     questionNumber = 8,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_008",
        //     topic = "enzymes",
        //     subtopic = "enzyme_substrate_models",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Understand,
        //     conceptTags = new List<string> { "lock_and_key_model", "specificity" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Use a analogia do próprio nome do modelo. Ele foi proposto para explicar especificidade por meio de formas que combinam entre si. Pense em uma interação mais rígida e pré-ajustada, diferente de modelos que enfatizam mudanças conformacionais posteriores." }
        // },

        // // Question 009
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Qual fator é essencial para que uma enzima exerça sua atividade plenamente?",
        //     answers = new string[] {
        //         "A sua estrutura primária",
        //         "A estabilidade de sua estrutura terciária",
        //         "A quantidade de alfa-hélices na estrutura da enzima",
        //         "A formação de estrutura quaternária"
        //     },
        //     correctIndex = 1,
        //     questionNumber = 9,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_009",
        //     topic = "enzymes",
        //     subtopic = "enzyme_structure_function",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Understand,
        //     conceptTags = new List<string> { "tertiary_structure", "protein_stability", "active_site" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "A atividade de uma enzima depende de mais do que sua sequência linear. Pense em como o dobramento da proteína organiza grupos químicos no espaço e cria uma região funcional capaz de reconhecer moléculas e catalisar reações." }
        // },

        // // Question 010
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Por que enzimas podem ser usadas na indústria",
        //     answers = new string[] {
        //         "Reação enzimática ocorre em temperaturas brandas.",
        //         "Enzimas são altamente específicas.",
        //         "Necessita-se de quantidades bem pequenas de enzimas, mesmo em escala industrial.",
        //         "Todas as alternativas são corretas."
        //     },
        //     correctIndex = 3,
        //     questionNumber = 10,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_010",
        //     topic = "enzymes",
        //     subtopic = "enzyme_applications",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Apply,
        //     conceptTags = new List<string> { "industry", "specificity", "mild_conditions" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Avalie as vantagens práticas de catalisadores biológicos em processos produtivos. Considere gasto energético, seletividade da reação e quantidade de catalisador necessária. A alternativa mais adequada deve integrar essas propriedades, não escolher apenas uma aplicação isolada." }
        // },

        // // Question 011
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "Uma enzima catalisa a transferência de um grupo fosfato de ATP para uma proteína. Em qual classe ela provavelmente se enquadra?",
        //     answers = new string[] {
        //         "Hidrolase",
        //         "Transferase",
        //         "Isomerase",
        //         "Liase"
        //     },
        //     correctIndex = 1,
        //     questionNumber = 11,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_011",
        //     topic = "enzymes",
        //     subtopic = "enzyme_classification",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Analyze,
        //     conceptTags = new List<string> { "enzyme_classes", "transferases", "reaction_types" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Para classificar uma enzima, olhe primeiro para o tipo de transformação química descrita. A pergunta envolve mover um grupo químico de uma molécula para outra. Associe esse padrão à classe enzimática definida pelo tipo de reação catalisada." }
        // },

        // // Question 012
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "",
        //     answers = new string[] {
        //         "Região da enzima responsável por interagir com a água",
        //         "Região da enzima com grande afinidade por íons",
        //         "Região da enzima que participa diretamente da catálise",
        //         "Região da enzima altamente hidrofóbica"
        //     },
        //     correctIndex = 2,
        //     questionNumber = 12,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Image,
        //     questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer12",
        //     questionLevel = 2,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_012",
        //     topic = "enzymes",
        //     subtopic = "active_site",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Apply,
        //     conceptTags = new List<string> { "enzyme_catalysis", "substrate_binding" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Na imagem, observe onde a pequena molécula aparece encaixada na superfície da proteína. A pergunta pede identificar o significado funcional dessa região. Procure a alternativa que descreve uma área de contato diretamente relacionada à ligação e à reação." }
        // },

        // // Question 013
        // new Question
        // {
        //     questionDatabankName = "EnzymeQuestionDatabase",
        //     questionText = "A atividade de uma enzima pode ser afetada por:",
        //     answers = new string[] {
        //         "Temperatura e pH.",
        //         "Concentração de substrato.",
        //         "Presença de inibidores.",
        //         "Todas as alternativas anteriores."
        //     },
        //     correctIndex = 3,
        //     questionNumber = 13,
        //     answerType = AnswerType.Text,
        //     questionType = QuestionType.Text,
        //     questionImagePath = "",
        //     questionLevel = 1,
        //     questionInDevelopment = false,
        //     globalId = "enzymes_013",
        //     topic = "enzymes",
        //     subtopic = "enzyme_activity_factors",
        //     displayName = "Enzimas",
        //     bloomLevel = BloomLevel.Remember,
        //     conceptTags = new List<string> { "ph_effects", "temperature_effects", "substrate_concentration" },
        //     prerequisites = null,
        //     questionHint = new QuestionHint { text = "Pense em fatores que podem modificar a forma da enzima, a frequência de encontro com o substrato ou a ocupação de regiões funcionais. A pergunta pede uma visão integrada da atividade enzimática, não apenas um único fator físico isolado." }
        // },

        // Question 014
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O pH ótimo dessa enzima está próximo de 7.",
                "A enzima foi desnaturada em pH 7.",
                "O pH não influencia a atividade enzimática.",
                "A enzima só funciona em pH extremamente ácido."
            },
            correctIndex = 0,
            questionNumber = 14,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_014",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_014",
            topic = "enzymes",
            subtopic = "ph_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "ph_effects", "optimal_conditions", "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Interprete o ensaio comparando as velocidades medidas em diferentes valores de pH. A condição com maior atividade indica onde grupos químicos da enzima e do substrato ficam mais adequados para interação e catálise naquele sistema experimental." }
        },

        // Question 015
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A 60 °C, a enzima provavelmente perdeu estrutura funcional por desnaturação.",
                "A enzima passou a produzir mais substrato em alta temperatura.",
                "A 60 °C, o sítio ativo se torna sempre mais específico.",
                "A temperatura não tem relação com a atividade observada."
            },
            correctIndex = 0,
            questionNumber = 15,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_015",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_015",
            topic = "enzymes",
            subtopic = "temperature_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "temperature_effects", "enzyme_denaturation", "optimal_conditions" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Separe dois efeitos da temperatura: aumentos moderados podem favorecer colisões moleculares, mas calor excessivo pode prejudicar a estrutura proteica. A queda abrupta em temperatura muito alta sugere analisar a estabilidade conformacional da enzima." }
        },

        // Question 016
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_016",
            topic = "enzymes",
            subtopic = "temperature_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_denaturation", "optimal_conditions" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense no que acontece quando o aquecimento ultrapassa o intervalo tolerado por uma proteína. A pergunta não trata de pequenas variações de temperatura, mas de uma condição capaz de comprometer a forma necessária para a função catalítica." }
        },

        // Question 017
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_017",
            topic = "enzymes",
            subtopic = "enzyme_denaturation",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "protein_structure", "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Associe função enzimática à organização tridimensional da proteína. Quando essa organização é perturbada, a região funcional pode deixar de reconhecer o substrato corretamente. A pergunta pede o significado geral desse processo, não um exemplo específico." }
        },

        // Question 018
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_018",
            topic = "enzymes",
            subtopic = "enzyme_denaturation",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "temperature_effects", "ph_effects", "protein_structure" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Considere agentes que desestabilizam interações responsáveis pelo dobramento proteico. Temperatura elevada, mudanças intensas de acidez e certos solventes podem afetar a conformação. Avalie se as opções representam mecanismos semelhantes de perda estrutural." }
        },

        // Question 019
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_019",
            topic = "enzymes",
            subtopic = "enzyme_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Analise o termo pelo efeito sobre a reação catalisada. Essas moléculas interferem na atividade da enzima por ocupação de regiões funcionais ou alteração de conformação. Pense no resultado esperado quando a enzima encontra esse tipo de regulador." }
        },

        // Question 020
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_020",
            topic = "enzymes",
            subtopic = "irreversible_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_inhibition", "covalent_modification" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare interações transitórias com modificações duradouras na enzima. A pergunta destaca um tipo de inibição em que a recuperação espontânea da atividade não é simples. Foque na estabilidade da ligação ou alteração produzida pelo inibidor." }
        },

        // Question 021
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Inibição Irreversível",
                "Inibição Competitiva",
                "Inibição Não-Competitiva",
                "Inibição A-Competitiva"
            },
            correctIndex = 2,
            questionNumber = 21,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer21",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_021",
            topic = "enzymes",
            subtopic = "noncompetitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "enzyme_inhibition", "kinetics" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na imagem, repare que o substrato ainda aparece no encaixe principal, enquanto outra molécula se liga em uma região separada. Use essa relação espacial para inferir o tipo de inibição, sem depender apenas do rótulo “inibidor”." }
        },

        // Question 022
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Inibição Irreversível",
                "Inibição Competitiva",
                "Inibição Não-Competitiva",
                "Inibição A-Competitiva"
            },
            correctIndex = 1,
            questionNumber = 22,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/enzymeDB_ImageQuestionContainer22",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_022",
            topic = "enzymes",
            subtopic = "competitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "enzyme_inhibition", "active_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na imagem, observe que a molécula indicada ocupa a cavidade onde outra molécula normalmente deveria se encaixar. A pista principal está na disputa pelo mesmo espaço físico da enzima, e não em uma alteração distante da estrutura." }
        },

        // Question 023
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = " O que a constante de Michaelis (Km) indica em um Gráfico de cinética enzimática?",
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_023",
            topic = "enzymes",
            subtopic = "michaelis_menten_kinetics",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "km", "vmax", "substrate_concentration" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Em gráficos de cinética, alguns parâmetros descrevem velocidade máxima e outros descrevem a concentração de substrato associada a pontos específicos da curva. Para interpretar Km, procure a ideia ligada à meia velocidade máxima, não ao produto final." }
        },

        // Question 024
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "O que representa um valor baixo de Km em um gráfico de cinética enzimática?",
            answers = new string[] {
                "Que a enzima atinge metade da velocidade máxima com baixa concentração de substrato.",
                "Que a velocidade máxima da reação é necessariamente baixa.",
                "Que a velocidade máxima da reação é necessariamente alta.",
                "Que a enzima aumenta a energia de ativação da reação em baixas concentrações de substrato."
            },
            correctIndex = 0,
            questionNumber = 24,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_024",
            topic = "enzymes",
            subtopic = "km",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_affinity", "michaelis_menten_kinetics" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relacione o valor de Km com a quantidade de substrato necessária para alcançar uma mesma fração da atividade máxima. Um valor menor indica que pouca quantidade de substrato já produz resposta considerável, mas não informa sozinho toda a eficiência da enzima." }
        },

        // Question 025
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_025",
            topic = "enzymes",
            subtopic = "km",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_affinity", "michaelis_menten_kinetics" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense no Km como uma medida operacional ligada à concentração de substrato. Se o valor é alto, o sistema precisa de mais substrato para atingir a mesma fração da velocidade máxima. Use essa relação para interpretar a interação aparente enzima-substrato." }
        },

        // Question 026
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Km, Vmax e concentração de substrato.",
                "pH, temperatura e massa da enzima.",
                "Energia livre, pKa e concentração de produto.",
                "Concentração de inibidor, cor do substrato e pressão."
            },
            correctIndex = 0,
            questionNumber = 26,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_026",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_026",
            topic = "enzymes",
            subtopic = "michaelis_menten_kinetics",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "michaelis_menten_kinetics", "km", "vmax" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A equação apresentada combina parâmetros cinéticos com a concentração disponível de substrato. Antes de calcular velocidade, identifique quais grandezas aparecem diretamente na fórmula. A resposta deve reunir os termos necessários, sem incluir variáveis externas ao modelo." }
        },

        // Question 027
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_027",
            topic = "enzymes",
            subtopic = "vmax",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "michaelis_menten_kinetics", "enzyme_saturation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na equação de Michaelis-Menten, diferencie o parâmetro associado ao limite superior da curva daquele ligado à concentração de substrato. Pense no que acontece quando praticamente todos os sítios catalíticos estão ocupados durante o ensaio." }
        },

        // Question 028
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_028",
            topic = "enzymes",
            subtopic = "enzyme_kinetics_plots",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "lineweaver_burk_plot", "michaelis_menten_kinetics" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe os eixos do gráfico: ambos usam grandezas invertidas, e a reta permite estimar parâmetros cinéticos pelos interceptos. A pista está no formato linear derivado da cinética de Michaelis-Menten, não em uma curva hiperbólica comum." }
        },

        // Question 029
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Em uma amostra, deseja-se degradar moléculas de RNA sem hidrolisar proteínas. Qual enzima seria mais adequada?",
            answers = new string[] {
                "Protease",
                "Lipase",
                "Ribonuclease",
                "Amilase"
            },
            correctIndex = 2,
            questionNumber = 29,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_029",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "ribonuclease", "rna_hydrolysis", "enzyme_specificity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Associe cada enzima ao tipo de biomolécula e ligação que ela reconhece. A amostra contém ácido nucleico específico, enquanto a restrição evita agir sobre proteínas. Procure a alternativa cujo campo de ação corresponde ao substrato descrito." }
        },

        // Question 030
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Para digerir uma proteína alimentar em peptídeos menores, qual tipo de enzima deve ser utilizado?",
            answers = new string[] {
                "Ribonuclease",
                "Protease",
                "Lipase",
                "Amilase"
            },
            correctIndex = 1,
            questionNumber = 30,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_030",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "protease", "protein_hydrolysis", "digestive_enzymes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense na composição das proteínas: aminoácidos unidos por ligações peptídicas. A pergunta pede o tipo de enzima capaz de quebrar essas ligações durante digestão ou processamento molecular, distinguindo esse substrato de lipídios, carboidratos e ácidos nucleicos." }
        },

        // Question 031
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma formulação de detergente precisa remover manchas ricas em gordura. Qual enzima seria mais indicada?",
            answers = new string[] {
                "Amilase",
                "Protease",
                "Lipase",
                "Ribonuclease"
            },
            correctIndex = 2,
            questionNumber = 31,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_031",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "lipase", "lipid_hydrolysis", "enzyme_applications" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Concentre-se no tipo de mancha descrita e no substrato químico predominante. Gorduras envolvem lipídios e ligações características de triglicerídeos. A enzima adequada deve atuar sobre esse grupo de moléculas, não sobre proteínas, amido ou RNA." }
        },

        // Question 032
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Durante a digestão de pão ou batata, qual enzima inicia a quebra do amido em moléculas menores?",
            answers = new string[] {
                "Lipase",
                "Protease",
                "Amilase",
                "Ribonuclease"
            },
            correctIndex = 2,
            questionNumber = 32,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_032",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "amylase", "starch_hydrolysis", "digestive_enzymes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pão e batata são ricos em polímeros de glicose. A pergunta pede a enzima que inicia a quebra desse carboidrato em fragmentos menores durante a digestão. Relacione o substrato alimentar ao tipo de ligação química presente no polímero." }
        },

        // Question 033
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um alimento proteico chega ao estômago, onde o pH é fortemente ácido. Qual enzima atua melhor nesse ambiente?",
            answers = new string[] {
                "Amilase salivar",
                "Pepsina",
                "Lipase pancreática",
                "Ribonuclease"
            },
            correctIndex = 1,
            questionNumber = 33,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_033",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "pepsin", "protein_hydrolysis", "ph_effects" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Use duas pistas juntas: o alimento contém proteínas e o ambiente descrito é fortemente ácido. Entre enzimas digestivas, algumas atuam melhor no estômago e outras em regiões mais neutras ou alcalinas. Relacione compartimento, pH e substrato." }
        },

        // Question 034
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Pepsina e quimotripsina são proteases, mas atuam em compartimentos com pH diferente. O que explica a pepsina funcionar melhor no estômago?",
            answers = new string[] {
                "Seu sítio ativo é estável e funcional em pH ácido.",
                "Ela possui Vmax sempre maior que qualquer enzima intestinal.",
                "Ela não depende de estrutura tridimensional.",
                "Ela hidrolisa carboidratos apenas em pH ácido."
            },
            correctIndex = 0,
            questionNumber = 34,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_034",
            topic = "enzymes",
            subtopic = "ph_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "pepsin", "ph_effects", "protein_structure" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare enzimas que atuam sobre substratos semelhantes, mas em ambientes diferentes. A pergunta destaca adaptação ao estômago, então avalie como pH ácido pode preservar ou prejudicar a conformação e os grupos químicos da região catalítica." }
        },

        // Question 035
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "No intestino delgado, uma protease pancreática precisa hidrolisar proteínas após aminoácidos aromáticos. Qual enzima corresponde melhor a essa descrição?",
            answers = new string[] {
                "Amilase",
                "Quimotripsina",
                "Lipase",
                "Ribonuclease"
            },
            correctIndex = 1,
            questionNumber = 35,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_035",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "chymotrypsin", "protein_hydrolysis", "digestive_enzymes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A descrição combina localização intestinal, origem pancreática e preferência por certos aminoácidos. Em vez de pensar apenas em “enzima digestiva”, relacione a especificidade da protease ao tipo de resíduo próximo da ligação peptídica a ser rompida." }
        },

        // Question 036
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A quimotripsina é adaptada ao ambiente alcalino do intestino delgado.",
                "A quimotripsina é uma enzima gástrica ativada por ácido forte.",
                "O pH não interfere na conformação do sítio ativo.",
                "Toda protease funciona melhor no mesmo pH."
            },
            correctIndex = 0,
            questionNumber = 36,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_036",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_036",
            topic = "enzymes",
            subtopic = "ph_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "chymotrypsin", "ph_effects", "optimal_conditions" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Interprete o gráfico como evidência experimental de condição ótima. Baixa atividade em meio muito ácido e alta atividade em pH levemente alcalino indicam adaptação ao compartimento onde a enzima normalmente funciona." }
        },

        // Question 037
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Durante a mastigação de um alimento rico em amido, qual enzima começa a agir ainda na boca?",
            answers = new string[] {
                "Lipase pancreática",
                "Pepsina",
                "Amilase salivar",
                "Quimotripsina"
            },
            correctIndex = 2,
            questionNumber = 37,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_037",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "salivary_amylase", "carbohydrate_hydrolysis", "digestive_enzymes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A pergunta situa o processo na boca e menciona alimento rico em amido. Pense em qual enzima está presente na saliva e inicia a digestão de carboidratos antes que o alimento chegue ao ambiente ácido do estômago." }
        },

        // Question 038
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A queda brusca de pH no estômago altera sua estrutura funcional.",
                "O estômago remove o substrato da amilase por completo.",
                "A enzima passa a hidrolisar proteínas em pH ácido.",
                "A concentração de água no estômago é sempre zero."
            },
            correctIndex = 0,
            questionNumber = 38,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_038",
            topic = "enzymes",
            subtopic = "ph_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "salivary_amylase", "ph_effects", "enzyme_denaturation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare o pH da boca com o do estômago. Uma enzima ativa em ambiente quase neutro pode perder desempenho quando grupos químicos mudam de ionização ou a estrutura se desestabiliza em acidez intensa." }
        },

        // Question 039
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_039",
            topic = "enzymes",
            subtopic = "irreversible_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_inhibition", "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A palavra-chave é irreversível. Pense em uma alteração que não desaparece facilmente quando o inibidor é removido. A consequência sobre a atividade deve ser avaliada pela dificuldade de recuperar a enzima funcional." }
        },

        // Question 040
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_040",
            topic = "enzymes",
            subtopic = "competitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_inhibition", "substrate_concentration", "active_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Nesse tipo de inibição, duas moléculas disputam uma mesma região funcional. Pergunte-se qual mudança experimental aumentaria a chance de o substrato ocupar esse espaço em vez do inibidor, sem modificar diretamente a enzima." }
        },

        // Question 041
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_041",
            topic = "enzymes",
            subtopic = "noncompetitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_inhibition", "allosteric_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Diferencie uma disputa pelo encaixe principal de uma ligação em outro local da enzima. Se o problema não está apenas na ocupação do sítio do substrato, aumentar substrato pode não restaurar plenamente a catálise." }
        },

        // Question 042
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
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_042",
            topic = "enzymes",
            subtopic = "enzyme_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "ace_inhibitors", "drug_action" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relacione os fármacos ao sistema fisiológico que regula pressão arterial. Eles interferem em uma etapa de conversão hormonal envolvendo angiotensina. A alternativa correta deve fazer sentido dentro desse mecanismo cardiovascular." }
        },

        // Question 043
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um estudante afirma: \"todas as enzimas são proteínas, sem exceção\". Como essa afirmação deve ser avaliada?",
            answers = new string[] {
                "Está totalmente correta, pois não existem catalisadores biológicos de RNA.",
                "Está incompleta, pois a maioria é proteica, mas existem ribozimas catalíticas.",
                "Está errada, pois enzimas são principalmente lipídios.",
                "Está correta apenas para enzimas que atuam no estômago."
            },
            correctIndex = 1,
            questionNumber = 43,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_043",
            topic = "enzymes",
            subtopic = "enzyme_definition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "proteins", "ribozymes", "enzyme_definition" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Avalie a frase procurando exceções importantes. Em bioquímica, muitas regras introdutórias são verdadeiras para a maioria dos casos, mas podem falhar quando moléculas de RNA com atividade catalítica entram na discussão." }
        },

        // Question 044
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Se uma via metabólica celular precisa ocorrer rapidamente em temperatura corporal, que papel as enzimas cumprem nessa situação?",
            answers = new string[] {
                "Aceleram as reações ao reduzir a energia de ativação.",
                "Transformam todas as reações em processos espontâneos sem substrato.",
                "Aumentam a temperatura interna da célula.",
                "Impedem a formação de produtos metabólicos."
            },
            correctIndex = 0,
            questionNumber = 44,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_044",
            topic = "enzymes",
            subtopic = "enzyme_catalysis",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "metabolism", "reaction_rate", "activation_energy" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense no desafio de manter vias metabólicas rápidas sem aquecer a célula ou alterar drasticamente o meio. A função da enzima deve ser relacionada ao caminho energético da reação e ao tempo necessário para atingir produtos." }
        },

        // Question 045
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma mutação altera aminoácidos que formam a cavidade de ligação ao substrato. Qual consequência é mais provável?",
            answers = new string[] {
                "Mudança na especificidade ou perda de atividade da enzima.",
                "Aumento obrigatório do número de produtos possíveis.",
                "Transformação da enzima em carboidrato.",
                "Independência total em relação ao pH."
            },
            correctIndex = 0,
            questionNumber = 45,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_045",
            topic = "enzymes",
            subtopic = "active_site",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "active_site", "substrate_binding", "specificity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A mutação está localizada justamente em aminoácidos que formam a cavidade funcional. Pergunte-se como mudanças de forma, carga ou polaridade nessa região afetariam reconhecimento molecular, ligação ao substrato e transformação química durante a catálise." }
        },

        // Question 046
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Em um experimento, a atividade de uma enzima cai quando o pH é alterado e também quando a temperatura aumenta demais. O que esses dois fatores têm em comum?",
            answers = new string[] {
                "Podem alterar a estrutura do sítio ativo e a ionização de grupos catalíticos.",
                "Aumentam sempre a concentração de substrato disponível.",
                "Transformam produtos em cofatores obrigatórios.",
                "Eliminam a necessidade de energia de ativação."
            },
            correctIndex = 0,
            questionNumber = 46,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_046",
            topic = "enzymes",
            subtopic = "enzyme_activity_factors",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "temperature_effects", "ph_effects", "active_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Procure o mecanismo comum por trás de pH e temperatura. Ambos podem alterar propriedades essenciais da proteína, seja mudando cargas de grupos químicos, seja desestabilizando interações que mantêm a forma funcional." }
        },

        // Question 047
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
            subtopic = "enzyme_denaturation",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "temperature_effects", "protein_structure" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A pergunta descreve perda de forma tridimensional após calor intenso. Relacione esse evento à estabilidade de proteínas e ao efeito sobre regiões funcionais. O termo procurado nomeia o processo estrutural, não uma reação metabólica." }
        },

        // Question 048
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A energia mínima necessária para iniciar uma reação química é chamada de:",
            answers = new string[] {
                "Energia solar",
                "Energia cinética",
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
            subtopic = "activation_energy",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "enzyme_catalysis" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Antes de uma reação ocorrer, os reagentes precisam alcançar uma condição energética favorável. A pergunta pede o nome dessa exigência inicial. Pense na barreira que uma enzima ajuda a contornar, não na energia liberada depois." }
        },

        // Question 049
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
            subtopic = "competitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_inhibition", "active_site", "substrate_analogs" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe a relação espacial: uma molécula parecida com o substrato ocupa a região onde o substrato deveria se ligar. Esse cenário envolve disputa direta por acesso à enzima, diferente de uma ligação regulatória distante." }
        },

        // Question 050
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
            subtopic = "enzyme_conditions",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "optimal_conditions", "temperature_effects", "ph_effects" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A pergunta pede condições de maior eficiência, não limites extremos. Pense em valores nos quais a estrutura da enzima permanece estável e os grupos químicos do sítio funcional estão adequados para a interação com o substrato." }
        },

        // Question 051
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
            subtopic = "active_site",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "substrate_binding" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relacione a região perguntada ao primeiro contato funcional entre enzima e substrato. Essa parte da proteína é organizada pelo dobramento tridimensional e reúne características de forma e química adequadas ao reconhecimento molecular." }
        },

        // Question 052
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
            subtopic = "enzyme_substrate_models",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "lock_and_key_model", "specificity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Use a analogia do modelo para pensar em especificidade. A pergunta não trata de armazenamento de energia nem síntese de outras biomoléculas, mas de como uma enzima distingue moléculas compatíveis com sua região funcional." }
        },

        // Question 053
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
            subtopic = "enzyme_activity_factors",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "temperature_effects", "ph_effects" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Separe fatores que afetam interações moleculares de características apenas visuais. Atividade enzimática depende de condições que mudam estrutura, carga, disponibilidade de substrato ou presença de reguladores, não de qualquer propriedade observável da solução." }
        },

        // Question 054
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
            subtopic = "coenzymes_cofactors",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "coenzymes", "vitamins" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em moléculas auxiliares que não são a cadeia proteica principal da enzima. Muitas participam carregando elétrons ou grupos químicos e podem derivar de vitaminas. Diferencie essa função de hormônios, polímeros ou substratos estruturais." }
        },

        // Question 055
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
            subtopic = "activation_energy",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_catalysis", "reaction_rate" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A pergunta busca o mecanismo geral da catálise. Compare uma reação sem enzima com uma reação catalisada e foque na dificuldade energética para atingir o estado intermediário, sem confundir velocidade com mudança no equilíbrio final." }
        },

        // Question 056
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma amostra contém amido e o objetivo é gerar maltose e dextrinas. Qual enzima deve ser escolhida?",
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
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_056",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "amylase", "starch_hydrolysis", "enzyme_specificity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Identifique primeiro o substrato e os produtos desejados. Amido é um carboidrato polimérico, e maltose e dextrinas surgem de sua quebra parcial. A enzima escolhida deve corresponder a esse tipo de ligação e biomolécula." }
        },

        // Question 057
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
            subtopic = "enzyme_definition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "proteins", "urease", "enzyme_history" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A descoberta histórica mencionada ajudou a resolver uma dúvida sobre a natureza química das enzimas. Pense no que a cristalização permitiu demonstrar sobre a composição molecular dessa enzima específica e sua relação com outras enzimas conhecidas." }
        },

        // Question 058
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
            subtopic = "competitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_inhibition", "active_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Analise onde o inibidor se liga e com quem ele compete. Se sua ação depende de semelhança com o substrato, o efeito principal ocorre na disputa pelo encaixe funcional, não por destruição permanente da enzima." }
        },

        // Question 059
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
            subtopic = "enzyme_catalysis",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "reaction_rate", "metabolism" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Considere a função das enzimas em vias metabólicas. Elas tornam transformações químicas viáveis em tempo biológico, sem serem consumidas como reagentes principais. A alternativa deve expressar esse papel geral, não uma função estrutural ou de transporte." }
        },

        // Question 060
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um relatório diz que uma enzima manteve o mesmo sítio ativo após desnaturação completa. Essa conclusão é aceitável?",
            answers = new string[] {
                "Sim, porque desnaturação altera apenas a sequência primária.",
                "Não, porque a desnaturação desfaz a conformação que organiza o sítio ativo.",
                "Sim, porque o sítio ativo não depende da estrutura tridimensional.",
                "Não, porque toda desnaturação aumenta a atividade enzimática."
            },
            correctIndex = 1,
            questionNumber = 60,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_060",
            topic = "enzymes",
            subtopic = "active_site",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "active_site", "enzyme_denaturation", "protein_structure" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Avalie a conclusão relacionando estrutura e função. Mesmo que a sequência da proteína permaneça, a organização tridimensional pode ser perdida. Pergunte-se se a região funcional continuaria corretamente formada após uma alteração estrutural completa." }
        },

        // Question 061
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_061",
            topic = "enzymes",
            subtopic = "enzyme_substrate_models",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "lock_and_key_model", "specificity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O enunciado já descreve uma analogia clássica. Para escolher o nome, lembre qual modelo compara a compatibilidade entre enzima e substrato a objetos com formas correspondentes, em contraste com explicações mais flexíveis." }
        },

        // Question 062
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_062",
            topic = "enzymes",
            subtopic = "enzyme_activity_factors",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "temperature_effects", "ph_effects" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Classifique as opções entre fatores químicos ou físicos que alteram a função e propriedades que apenas descrevem aparência. Uma enzima responde a condições que afetam estrutura, cargas ou concentração de substrato." }
        },

        // Question 063
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_063",
            topic = "enzymes",
            subtopic = "coenzymes_cofactors",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "cofactors", "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense em componentes auxiliares necessários para algumas enzimas funcionarem plenamente. Eles não fazem parte da cadeia proteica principal e podem ser íons metálicos ou moléculas orgânicas. A pergunta busca o nome geral desse grupo." }
        },

        // Question 064
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma enzima foi chamada de hidrolase porque quebrou uma ligação química usando água. Essa classificação é adequada?",
            answers = new string[] {
                "Sim, pois hidrolases catalisam reações de hidrólise.",
                "Não, pois hidrolases transferem elétrons entre moléculas.",
                "Não, pois hidrolases unem moléculas com ATP.",
                "Sim, mas apenas se a enzima atuar sobre DNA."
            },
            correctIndex = 0,
            questionNumber = 64,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_064",
            topic = "enzymes",
            subtopic = "enzyme_classification",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "hydrolases", "hydrolysis", "enzyme_classes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Avalie a classificação observando o tipo de reação, não o substrato específico. Quando uma ligação é quebrada com participação de água, a nomenclatura enzimática costuma agrupar esse mecanismo em uma classe própria." }
        },

        // Question 065
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "A desnaturação de uma enzima ocorre quando:",
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_065",
            topic = "enzymes",
            subtopic = "enzyme_denaturation",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "protein_structure", "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Relacione a atividade enzimática ao dobramento da proteína. A pergunta descreve uma mudança estrutural que prejudica o arranjo da região funcional. Pense no processo, não apenas em uma condição ambiental específica." }
        },

        // Question 066
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_066",
            topic = "enzymes",
            subtopic = "activation_energy",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "enzyme_catalysis" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Concentre-se no efeito da enzima sobre o caminho da reação. Ela não muda necessariamente a quantidade final de produto, nem elimina solvente ou substrato. A pista está na barreira que separa reagentes do estado intermediário." }
        },

        // Question 067
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
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "enzymes_067",
            topic = "enzymes",
            subtopic = "specificity",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "enzyme_substrate_interaction", "active_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense na capacidade de uma enzima reconhecer determinados substratos entre muitas moléculas celulares. Essa propriedade depende da forma e das características químicas da região funcional, permitindo seletividade nas vias metabólicas." }
        },

        // Question 068
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Dobrar o valor de Km da reação.",
                "Diminuir à metade o valor da velocidade máxima",
                "Dobrar a velocidade máxima observada.",
                "Diminuir o valor de Km à metade do valor inicial"
            },
            correctIndex = 2,
            questionNumber = 68,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_068",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_068",
            topic = "enzymes",
            subtopic = "vmax",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "vmax", "enzyme_concentration", "enzyme_kinetics" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare as duas curvas sem assumir que o Km mudou. A quantidade de enzima altera quantos sítios catalíticos estão disponíveis no ensaio. Observe principalmente o patamar que a velocidade pode alcançar quando o substrato deixa de ser o fator limitante." }
        },

        // Question 069
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um pesquisador quer comparar a afinidade de duas enzimas pelo mesmo substrato. Qual parâmetro deve observar primeiro?",
            answers = new string[] {
                "Temperatura ambiente",
                "Km",
                "Cor da solução",
                "Massa do tubo de ensaio"
            },
            correctIndex = 1,
            questionNumber = 69,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_069",
            topic = "enzymes",
            subtopic = "km",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "km", "enzyme_affinity", "michaelis_menten_kinetics" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Para comparar afinidade aparente, use um parâmetro extraído da curva de Michaelis-Menten. Ele relaciona concentração de substrato com uma fração da velocidade máxima. A comparação só faz sentido se as condições experimentais forem semelhantes." }
        },

        // Question 070
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A enzima B sempre catalisa mais rápido em qualquer concentração.",
                "As duas enzimas têm afinidades pelo substrato necessariamente idênticas.",
                "A enzima A foi desnaturada.",
                "A enzima A atinge metade do Vmax com menor concentração de substrato."
            },
            correctIndex = 3,
            questionNumber = 70,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_070",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_070",
            topic = "enzymes",
            subtopic = "km",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "km", "vmax", "enzyme_affinity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Como o valor máximo é igual, concentre-se na concentração de substrato necessária para atingir parte da atividade. Um parâmetro menor indica que a enzima responde melhor quando o substrato está menos disponível." }
        },

        // Question 071
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "A enzima deixa de existir após o primeiro produto formado.",
                "O sítio catalítico da enzima fica progressivamente saturado pelo aumento substrato.",
                "O substrato passa a diminuir a energia de ativação sozinho.",
                "O Km aumenta indefinidamente no platô."
            },
            correctIndex = 1,
            questionNumber = 71,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_071",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_071",
            topic = "enzymes",
            subtopic = "michaelis_menten_kinetics",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "enzyme_saturation", "vmax", "substrate_concentration" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O platô indica que aumentar o substrato deixa de produzir grande aumento de velocidade. Pense no número limitado de regiões catalíticas disponíveis e no que acontece quando elas passam a trabalhar próximas da capacidade máxima." }
        },

        // Question 072
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Não competitiva pura",
                "Irreversível completa",
                "Competitiva",
                "Desnaturação térmica"
            },
            correctIndex = 2,
            questionNumber = 72,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_072",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_072",
            topic = "enzymes",
            subtopic = "competitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "competitive_inhibition", "km", "vmax" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe se as duas curvas tendem ao mesmo platô de velocidade máxima. Depois compare a posição em que cada curva atinge Vmax/2, metade da velocidade máxima. Essa referência ajuda a identificar mudança no Km aparente sem confundir com alteração de Vmax." }
        },

        // Question 073
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um inibidor reduz o Vmax, mas o Km permanece praticamente igual. Qual interpretação é mais compatível?",
            answers = new string[] {
                "O inibidor compete com o substrato pelo sítio ativo.",
                "O substrato foi totalmente removido do meio.",
                "A enzima passou a ter maior número de sítios ativos.",
                "O inibidor reduz a eficiência catalítica sem impedir diretamente a ligação do substrato."
            },
            correctIndex = 3,
            questionNumber = 73,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_073",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_073",
            topic = "enzymes",
            subtopic = "noncompetitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "noncompetitive_inhibition", "vmax", "allosteric_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Quando a afinidade aparente parece semelhante, mas o limite máximo de velocidade cai, procure uma explicação que envolva menor eficiência catalítica. A ligação do substrato pode ocorrer, mas o processamento fica comprometido." }
        },

        // Question 074
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma enzima perde atividade após alteração de pH, mas recupera parte da função quando retorna ao pH ótimo. O que isso sugere?",
            answers = new string[] {
                "A sequência primária foi destruída de forma permanente.",
                "A mudança afetou estados de ionização sem desnaturação irreversível completa.",
                "O substrato foi convertido em enzima.",
                "A energia de ativação deixou de existir."
            },
            correctIndex = 1,
            questionNumber = 74,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_074",
            topic = "enzymes",
            subtopic = "ph_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "ph_effects", "enzyme_activity", "protein_structure" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A recuperação parcial após retorno ao pH adequado sugere que nem toda alteração foi permanente. Pense em mudanças reversíveis de carga e interação molecular, distinguindo-as de perda estrutural completa da proteína." }
        },

        // Question 075
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma mutação distante do sítio ativo altera a atividade de uma enzima regulada alostericamente. Como isso pode ocorrer?",
            answers = new string[] {
                "Apenas aminoácidos dentro do sítio ativo afetam enzimas.",
                "Toda mutação distante transforma a enzima em substrato.",
                "A região mutada pode modificar a conformação do sítio ativo à distância.",
                "Regulação alostérica não depende de conformação proteica."
            },
            correctIndex = 2,
            questionNumber = 75,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_075",
            topic = "enzymes",
            subtopic = "allosteric_regulation",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "allosteric_site", "protein_structure", "enzyme_regulation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Em proteínas, regiões distantes podem se comunicar por mudanças conformacionais. Uma alteração fora da cavidade principal ainda pode modificar a forma ou dinâmica da região funcional, especialmente em sistemas regulados alostericamente." }
        },

        // Question 076
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um zimogênio é secretado inativo e ativado apenas no local de ação. Qual vantagem biológica esse mecanismo oferece?",
            answers = new string[] {
                "Impede que a enzima tenha estrutura terciária.",
                "Garante que qualquer pH seja igualmente ótimo.",
                "Transforma toda enzima em cofator metálico.",
                "Evita que enzimas digestivas degradem tecidos antes de chegar ao compartimento correto."
            },
            correctIndex = 3,
            questionNumber = 76,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_076",
            topic = "enzymes",
            subtopic = "zymogens",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "zymogens", "digestive_enzymes", "enzyme_regulation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Pense na utilidade de produzir certas enzimas em forma inativa. Em processos digestivos, ativar a molécula apenas no compartimento correto pode proteger células produtoras e tecidos antes do momento adequado de ação." }
        },

        // Question 077
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma coenzima derivada de vitamina participa carregando elétrons entre reações. O que isso revela sobre sua função?",
            answers = new string[] {
                "Ela substitui permanentemente o sítio ativo da enzima.",
                "Ela auxilia a catálise ao transportar grupos químicos ou elétrons.",
                "Ela é sempre o produto final da reação.",
                "Ela impede a formação da holoenzima."
            },
            correctIndex = 1,
            questionNumber = 77,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_077",
            topic = "enzymes",
            subtopic = "coenzymes_cofactors",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "coenzymes", "cofactors", "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Considere que algumas moléculas auxiliares participam da reação carregando elétrons ou grupos químicos entre etapas. A pergunta pede interpretar essa função dentro da catálise, não confundir a molécula auxiliar com a proteína enzimática." }
        },

        // Question 078
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma enzima apresenta alta especificidade por um substrato, mas baixa atividade após aquecimento intenso. Como esses fatos se relacionam?",
            answers = new string[] {
                "A especificidade aumenta sempre que a enzima desnatura.",
                "O calor transforma especificidade em inibição competitiva.",
                "A especificidade depende da conformação do sítio ativo, que pode ser perdida com calor.",
                "A especificidade não depende da estrutura da enzima."
            },
            correctIndex = 2,
            questionNumber = 78,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "enzymes_078",
            topic = "enzymes",
            subtopic = "specificity",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "specificity", "active_site", "temperature_effects" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Una as duas observações: reconhecimento seletivo e perda de atividade após calor. Ambas dependem da manutenção da forma tridimensional. Se o aquecimento altera essa forma, a interação específica com o substrato também pode ser afetada." }
        },

        // Question 079
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Sim, porque Km está diretamente relacionado à velocidade de conversão de substrato em produto.",
                "Sim, porque Km baixo sempre significa maior produto final no equilíbrio.",
                "Não, porque Km não tem relação com substrato.",
                "Não, porque Km baixo indica afinidade aparente, mas o contexto e o Vmax também importam."
            },
            correctIndex = 3,
            questionNumber = 79,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_079",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_079",
            topic = "enzymes",
            subtopic = "km",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "km", "vmax", "enzyme_efficiency" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Avalie a frase como uma generalização. Um parâmetro pode sugerir afinidade aparente, mas desempenho enzimático também depende de velocidade máxima, concentração de substrato, regulação, localização da enzima e contexto celular ou experimental." }
        },

        // Question 080
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um anúncio diz que uma enzima \"aumenta a quantidade final de produto porque muda o equilíbrio químico\". Como avaliar essa afirmação?",
            answers = new string[] {
                "Está correta: toda enzima desloca o equilíbrio para produtos.",
                "Está incorreta: enzimas aceleram a chegada ao equilíbrio, mas não alteram o equilíbrio final.",
                "Está correta apenas para hidrolases.",
                "Está incorreta porque enzimas sempre diminuem a velocidade da reação."
            },
            correctIndex = 1,
            questionNumber = 80,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_080",
            topic = "enzymes",
            subtopic = "enzyme_catalysis",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "enzyme_catalysis", "chemical_equilibrium", "activation_energy" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Diferencie velocidade de reação e posição de equilíbrio. Catalisadores ajudam o sistema a chegar mais rápido ao estado final permitido pela termodinâmica, mas não devem ser interpretados como força que cria rendimento extra por si só." }
        },

        // Question 081
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um protocolo propõe ferver uma enzima proteica antes de usá-la em um ensaio de atividade. Esse procedimento faz sentido?",
            answers = new string[] {
                "Sim, porque fervura sempre aumenta a especificidade enzimática.",
                "Sim, porque enzimas proteicas funcionam melhor após perder a estrutura terciária.",
                "Geralmente não, pois fervura tende a desnaturar a proteína e destruir o sítio ativo.",
                "Não, porque fervura transforma enzimas em carboidratos."
            },
            correctIndex = 2,
            questionNumber = 81,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_081",
            topic = "enzymes",
            subtopic = "temperature_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "enzyme_denaturation", "temperature_effects", "active_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Antes de aceitar o protocolo, considere a estabilidade de proteínas sob aquecimento intenso. Se a função depende do dobramento correto, uma etapa de fervura precisa ser justificada por uma propriedade especial da enzima usada." }
        },

        // Question 082
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um estudante interpreta que, se aumentar o substrato recupera a atividade, então o inibidor provavelmente é não competitivo. Essa interpretação é correta?",
            answers = new string[] {
                "Sim, porque inibição não competitiva sempre é revertida por substrato.",
                "Sim, porque todo inibidor se liga ao sítio ativo.",
                "Não, porque substrato nunca afeta uma reação enzimática.",
                "Não, recuperação por excesso de substrato é mais compatível com inibição competitiva."
            },
            correctIndex = 3,
            questionNumber = 82,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_082",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_082",
            topic = "enzymes",
            subtopic = "competitive_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "competitive_inhibition", "noncompetitive_inhibition", "substrate_concentration" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Compare padrões de inibição observando o efeito de adicionar mais substrato. Quando essa adição recupera atividade, pense em disputa pelo mesmo local. Quando não recupera, o problema pode estar em outro ponto da enzima." }
        },

        // Question 083
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma questão pede para escolher uma enzima para degradar lipídios, mas marca protease como resposta correta. Como você avaliaria o gabarito?",
            answers = new string[] {
                "O gabarito está correto; proteases hidrolisam triglicerídeos.",
                "O gabarito está inadequado; a enzima esperada para lipídios é lipase.",
                "O gabarito está correto apenas em pH neutro.",
                "Não é possível avaliar porque enzimas não têm especificidade."
            },
            correctIndex = 1,
            questionNumber = 83,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_083",
            topic = "enzymes",
            subtopic = "digestive_enzymes",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "lipase", "protease", "enzyme_specificity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Avalie o gabarito usando especificidade enzimática. Primeiro identifique qual biomolécula deve ser degradada; depois associe esse substrato ao tipo de ligação química que a enzima reconhece. Um bom gabarito precisa respeitar essa relação." }
        },

        // Question 084
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um texto afirma que uma enzima desnaturada pode continuar catalisando normalmente se a sequência de aminoácidos não mudou. Essa justificativa é suficiente?",
            answers = new string[] {
                "Sim, porque apenas a sequência primária determina toda atividade em qualquer condição.",
                "Sim, porque desnaturação melhora o contato com o substrato.",
                "Não, porque a função depende também do dobramento tridimensional.",
                "Não, porque enzimas não possuem aminoácidos."
            },
            correctIndex = 2,
            questionNumber = 84,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_084",
            topic = "enzymes",
            subtopic = "enzyme_denaturation",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "protein_structure", "enzyme_denaturation", "active_site" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A justificativa menciona apenas sequência, mas função proteica também depende de dobramento. Pergunte-se se uma enzima pode manter sua região funcional organizada quando a conformação tridimensional foi perdida ou profundamente alterada." }
        },

        // Question 085
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Ao comparar pepsina e amilase salivar, alguém diz que ambas devem ter o mesmo pH ótimo porque são enzimas digestivas. Essa avaliação é correta?",
            answers = new string[] {
                "Sim, pois toda enzima digestiva atua melhor em pH 2.",
                "Sim, pois pH ótimo depende apenas do tipo de alimento.",
                "Não, porque amilase salivar não é enzima.",
                "Não, pois enzimas digestivas diferentes são adaptadas a compartimentos com pH distintos."
            },
            correctIndex = 3,
            questionNumber = 85,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_085",
            topic = "enzymes",
            subtopic = "ph_effects",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "pepsin", "salivary_amylase", "optimal_conditions" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Não assuma que todas as enzimas digestivas compartilham o mesmo ambiente. Compare boca, estômago e intestino, lembrando que cada compartimento tem pH próprio e seleciona enzimas adaptadas a suas condições." }
        },

        // Question 086
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um material didático usa \"chave-fechadura\" como única explicação para todas as interações enzima-substrato. Qual crítica é mais adequada?",
            answers = new string[] {
                "O modelo é inútil porque enzimas nunca reconhecem substratos específicos.",
                "O modelo ajuda a entender especificidade, mas é limitado por não representar a flexibilidade do ajuste induzido.",
                "O modelo prova que enzimas são rígidas em todas as etapas da catálise.",
                "O modelo substitui a necessidade de estudar sítio ativo."
            },
            correctIndex = 1,
            questionNumber = 86,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_086",
            topic = "enzymes",
            subtopic = "enzyme_substrate_models",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "lock_and_key_model", "induced_fit_model", "specificity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Avalie o modelo como ferramenta didática, não como descrição completa. A analogia rígida ajuda a introduzir especificidade, mas muitas interações reais envolvem mudanças conformacionais durante a aproximação entre enzima e substrato." }
        },

        // Question 087
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma indústria escolhe enzimas para reduzir temperatura, subprodutos e consumo energético de um processo. Essa decisão é bem fundamentada?",
            answers = new string[] {
                "Não, porque enzimas exigem sempre temperaturas extremas.",
                "Não, porque enzimas aumentam inevitavelmente subprodutos.",
                "Sim, porque enzimas costumam atuar em condições brandas e com alta especificidade.",
                "Sim, mas apenas porque enzimas não dependem de substratos."
            },
            correctIndex = 2,
            questionNumber = 87,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_087",
            topic = "enzymes",
            subtopic = "enzyme_applications",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "industry", "specificity", "mild_conditions" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Para julgar a decisão industrial, conecte propriedades de enzimas com objetivos do processo. Condições brandas, seletividade e menor formação de subprodutos podem ser vantagens, mas a escolha também depende de estabilidade e custo operacional." }
        },

        // Question 088
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Sim, porque enzimas com o mesmo Km sempre têm o mesmo Vmax.",
                "Sim, porque Km determina sozinho o platô da curva.",
                "Não, porque o gráfico mostra o mesmo Km, mas velocidades máximas diferentes.",
                "Não, porque curvas de Michaelis-Menten não permitem comparar Vmax."
            },
            correctIndex = 2,
            questionNumber = 88,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_088",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_088",
            topic = "enzymes",
            subtopic = "km",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "km", "vmax", "michaelis_menten_kinetics" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Observe separadamente a posição marcada para Km e o patamar alcançado por cada curva. Duas curvas podem compartilhar uma mesma referência de substrato sem atingir o mesmo limite de velocidade quando a concentração de substrato aumenta." }
        },

        // Question 089
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Um pesquisador conclui que um inibidor é irreversível apenas porque a atividade diminuiu. Essa conclusão é suficiente?",
            answers = new string[] {
                "Sim, qualquer queda de atividade prova ligação covalente permanente.",
                "Não, é preciso testar se a atividade retorna após remover ou diluir o inibidor.",
                "Sim, porque inibidores reversíveis não reduzem atividade.",
                "Não, porque inibidores nunca afetam enzimas."
            },
            correctIndex = 1,
            questionNumber = 89,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/EnzymeDB/imageQuestion_enzymes_089",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_089",
            topic = "enzymes",
            subtopic = "irreversible_inhibition",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "irreversible_inhibition", "reversible_inhibition", "enzyme_activity" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Uma queda de atividade não revela sozinha se o efeito é permanente. Pense em testes de reversibilidade, como remover, diluir ou competir com o inibidor, antes de concluir que houve modificação definitiva da enzima." }
        },

        // Question 090
        new Question
        {
            questionDatabankName = "EnzymeQuestionDatabase",
            questionText = "Uma explicação afirma que cofatores são sempre proteínas que substituem a enzima. Como avaliar essa explicação?",
            answers = new string[] {
                "Está correta: cofatores são enzimas completas independentes.",
                "Está correta apenas para amilase salivar.",
                "Está incorreta: cofatores são componentes não proteicos que auxiliam algumas enzimas.",
                "Está incorreta porque cofatores impedem a formação da holoenzima."
            },
            correctIndex = 2,
            questionNumber = 90,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "enzymes_090",
            topic = "enzymes",
            subtopic = "coenzymes_cofactors",
            displayName = "Enzimas",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string> { "cofactors", "coenzymes", "holoenzyme" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Revise a definição de componentes auxiliares da catálise. Eles não substituem a enzima como proteína completa; ajudam certas reações por meio de íons ou moléculas orgânicas associadas à estrutura enzimática." }
        }

    };
    
    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.enzymes;
    public string GetDatabankName()  => "EnzymeQuestionDatabase";
    public string GetDisplayName()   => "Enzimas";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
