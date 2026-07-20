using System.Collections.Generic;
using QuestionSystem;

public class LipidsQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        // QUESTION 001
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que são lipídios?",
            answers = new string[] {
                "Moléculas polares, que se associam através de interações eletrostáticas",
                "Moléculas apolares, que se associam através de interações hidrofóbicas",
                "Moléculas anfipáticas, que se associam através de interações hidrofóbicas",
                "Moléculas anfipáticas, que se associam através da pontes de hidrogênio"
            },
            correctIndex = 2,
            questionNumber = 1,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_001",
            topic = "lipids",
            subtopic = "lipid_properties",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "amphipathic_molecules", "hydrophobic_interactions" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Na bioquímica, lipídios são definidos como moléculas biologicamente relevantes que são insolúveis em água e solúveis em solventes orgânicos (éter, clorofórmio etc.). Grande parte dos lipídios de membrana (fosfolipídios, glicolipídios, esfingolipídios) é anfipática, ou seja, possui parte polar (hidrofílica) e parte apolar (hidrofóbica). Além disso, em meio aquoso, as caudas hidrofóbicas dos lipídios tendem a se afastar da água e se agrupar entre si. Esse agrupamento é chamado de efeito hidrofóbico, que leva à formação de: Micelas, bicamadas lipídicas, lipossomos. Ou seja, a associação entre lipídios ocorre principalmente por interações hidrofóbicas entre as partes apolares" }
        },

        // QUESTION 002
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "São totalmente apolares",
                "São totalmente polares",
                "São hidrofílicos",
                "São anfipáticos"
            },
            correctIndex = 3,
            questionNumber = 2,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/LipidDB/LipidsDB_ImageQuestionContainer2",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_002",
            topic = "lipids",
            subtopic = "amphipathic_molecules",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "polar_region", "nonpolar_region" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídios são moléculas orgânicas formadas principalmente por cadeias de hidrocarbonetos, podendo apresentar diferentes grupos funcionais em sua estrutura. A imagem ilustra moléculas anfipáticas, isto é, compostos que possuem uma região hidrofílica (“cabeça” polar) e uma região hidrofóbica (“cauda” apolar). Essa característica é fundamental para a formação de membranas biológicas e para a ação de substâncias como fosfolipídios e detergentes.As diferenças estruturais entre as moléculas apresentadas influenciam diretamente propriedades físicas e biológicas, como solubilidade, fluidez e interação com a água. Além disso, o tamanho da cadeia carbônica e o grau de insaturação das caudas lipídicas podem alterar características como ponto de fusão e organização das membranas celulares." }
        },

        // QUESTION 003
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Dentre as moléculas a seguir, selecione o lipídeo",
            answers = new string[] {
                "AnswerImages/LipidDB/colesterol",
                "AnswerImages/AminoacidsDB/glutamina",
                "AnswerImages/CarbohydrateDB/beta-galactopiranose",
                "AnswerImages/CarbohydrateDB/D-galactose"
            },
            correctIndex = 0,
            questionNumber = 3,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_003",
            topic = "lipids",
            subtopic = "lipid_classes",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "cholesterol", "sterols" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 004
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O termo hidrofóbico refere-se a:",
            answers = new string[] {
                "Repulsão por água",
                "Afinidade por água",
                "Afinidade por solventes aquosos",
                "Afinidade por altas temperaturas"
            },
            correctIndex = 0,
            questionNumber = 4,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_004",
            topic = "lipids",
            subtopic = "hydrophobicity",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "water_solubility" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O termo hidrofóbico é usado em bioquímica e química para descrever grupos ou moléculas que “não gostam” de água, isto é, têm baixa solubilidade em água e tendem a evitá-la. Por isso, na questão proposta, a alternativa correta é \"Repulsão por água\"." }
        },

        // QUESTION 005
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Moléculas com regiões polares e apolares são chamadas:",
            answers = new string[] {
                "Hidrofílicas",
                "Hidrofóbicas",
                "Anfipáticas",
                "Apolares"
            },
            correctIndex = 2,
            questionNumber = 5,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_005",
            topic = "lipids",
            subtopic = "amphipathic_molecules",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "polar_region", "nonpolar_region" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Moléculas que possuem ao mesmo tempo uma região que “gosta” de água (polar/hidrofílica) e uma região que “foge” de água (apolar/hidrofóbica) recebem o nome de anfipáticas. As outras alternativas não estão corretas, pois hidrofílicas indicariam apenas caráter polar, não a coexistência de parte apolar, já hidrofóbicas e apolares indicariam apenas caráter não polar." }
        },

        // QUESTION 006
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Triglicerídeos",
                "Fosfolipídios",
                "Ácidos graxos",
                "Esteróides"
            },
            correctIndex = 2,
            questionNumber = 6,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/LipidDB/LipidsDB_ImageQuestionContainer6",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_006",
            topic = "lipids",
            subtopic = "fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "lipid_classes" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 007
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que caracteriza um ácido graxo?",
            answers = new string[] {
                "Uma longa cadeia de hidrocarbonetos com um grupo carboxila.",
                "Um anel de hidrocarbonetos com um grupo amino.",
                "Uma cadeia curta de hidrocarbonetos com um grupo fosfato.",
                "Um açúcar com múltiplos grupos hidroxila."
            },
            correctIndex = 0,
            questionNumber = 7,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_007",
            topic = "lipids",
            subtopic = "fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "hydrocarbon_chain", "carboxyl_group" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Ácidos graxos, são caracterizados por terem uma estrutura com: Cadeia longa de hidrocarbonetos (R–) sendo uma sequência de carbonos e hidrogênios (–CH₂–CH₂–CH₂–…), apolar e hidrofóbica, além de um grupo carboxila (–COOH), que confere o caráter de “ácido” ao ácido graxo podendo doar H⁺. Portanto, a presença de uma longa cadeia de hidrocarbonetos ligada a um grupo carboxila é exatamente o que caracteriza um ácido graxo. As outras alternativas descrevem tipos de moléculas completamente diferentes (respectivamente: aminoácidos, fosfolipídios, carboidratos)" }
        },

        // QUESTION 008
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Indique abaixo o lipídeo mono-insaturado",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 1,
            questionNumber = 8,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_008",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "monounsaturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 009
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos poli-insaturados possuem:",
            answers = new string[] {
                "Apenas ligações simples carbono-carbono.",
                "mais de uma ligação dupla carbono-carbono.",
                "uma ligação dupla carbono-carbono",
                "não possuem insaturações"
            },
            correctIndex = 1,
            questionNumber = 9,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_009",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "polyunsaturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Ácidos graxos são cadeias longas de hidrocarbonetos com um grupo carboxila (–COOH). Quanto às ligações C–C, eles se classificam em: Saturados: só têm ligações simples C–C (sem insaturações). Mono-insaturados: possuem uma única ligação dupla C=C na cadeia. Poli-insaturados: possuem duas ou mais ligações duplas C=C na cadeia. Essas ligações duplas são chamadas de insaturações. Portanto: “Apenas ligações simples” descreve ácidos graxos saturados. “Uma ligação dupla” descreve ácidos graxos monoinsaturados. “Não possuem insaturações” também descreve saturados. Por isso, poli-insaturados necessariamente têm mais de uma ligação dupla C=C na cadeia." }
        },

        // QUESTION 010
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos se agrupam através de interações hidrofóbicas. Indique abaixo qual lipídeo possuirá interações mais fracas.",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 3,
            questionNumber = 10,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_010",
            topic = "lipids",
            subtopic = "hydrophobic_interactions",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "unsaturated_fatty_acids", "melting_point" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 011
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Quais os dois fatores que afetam diretamente o ponto de fusão de lipídeos?",
            answers = new string[] {
                "densidade /tensão superficial",
                "grau de instaturação / polaridade",
                "tamanho da cadeia carbônica / grau de insaturação",
                "viscosidade / tamanho da cadeia carbônica."
            },
            correctIndex = 2,
            questionNumber = 11,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_011",
            topic = "lipids",
            subtopic = "melting_point",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "chain_length", "degree_of_unsaturation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O ponto de fusão dos lipídeos depende principalmente do tamanho da cadeia carbônica e do grau de insaturação. Cadeias carbônicas mais longas apresentam maior interação entre as moléculas, exigindo mais energia para a fusão e, consequentemente, aumentando o ponto de fusão. Já a presença de insaturações (duplas ligações) dificulta o empacotamento das moléculas, reduzindo as interações intermoleculares e diminuindo o ponto de fusão." }
        },

        // QUESTION 012
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos saturados geralmente são:",
            answers = new string[] {
                "Líquidos à temperatura ambiente.",
                "Sólidos à temperatura ambiente.",
                "Gasosos à temperatura ambiente.",
                "Insolúveis em solventes orgânicos."
            },
            correctIndex = 1,
            questionNumber = 12,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_012",
            topic = "lipids",
            subtopic = "saturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "melting_point", "solid_fats" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Ácidos graxos saturados não possuem ligações duplas, o que permite que suas cadeias lineares se empacotem de forma densa e organizada. Esse empacotamento forte eleva o ponto de fusão, fazendo com que, em condições comuns, fiquem no estado sólido." }
        },

        // QUESTION 013
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos insaturados geralmente são:",
            answers = new string[] {
                "Líquidos à temperatura ambiente.",
                "Sólidos à temperatura ambiente.",
                "Gasosos à temperatura ambiente.",
                "Insolúveis em solventes orgânicos."
            },
            correctIndex = 0,
            questionNumber = 13,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_013",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "melting_point", "vegetable_oils" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Ácidos graxos insaturados possuem uma ou mais ligações duplas C=C na cadeia carbônica. Com menor empacotamento e interações mais fracas, é mais fácil separar as moléculas, o que reduz o ponto de fusão. Por isso, muitos ácidos graxos insaturados (como os presentes em óleos vegetais) são líquidos à temperatura ambiente, ao contrário dos saturados, que tendem a ser sólidos." }
        },

        // QUESTION 014
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Indique abaixo o lípideo com o MAIOR ponto de fusão",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 0,
            questionNumber = 14,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_014",
            topic = "lipids",
            subtopic = "melting_point",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "saturated_fatty_acids", "chain_packing" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 015
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Indique abaixo o lípideo com o MENOR ponto de fusão",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 3,
            questionNumber = 15,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_015",
            topic = "lipids",
            subtopic = "melting_point",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "unsaturated_fatty_acids", "degree_of_unsaturation" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 016
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos têm um sistema de nomenclatura e abreviações bem peculiar. Indique abaixo o lipídeo cuja abreviação é 18:2^{∆9, 12}",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_saturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 2,
            questionNumber = 16,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_016",
            topic = "lipids",
            subtopic = "fatty_acid_nomenclature",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "double_bonds", "linoleic_acid" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 017
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Há um sistema de classificação que identifica os lipídeos através de sua extremidade ômega. Sendo assim, indique abaixo o lípideo que pertence a família ômega-3",
            answers = new string[] {
                "AnswerImages/LipidDB/colesterol",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado"
            },
            correctIndex = 3,
            questionNumber = 17,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_017",
            topic = "lipids",
            subtopic = "omega_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "omega_3", "essential_fatty_acids" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 018
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triacilgliceróis são formados por:",
            answers = new string[] {
                "Três ácidos graxos e uma molécula de glicerol.",
                "Dois ácidos graxos e uma molécula de glicerol.",
                "Um ácido graxo e uma molécula de glicerol.",
                "Três ácidos graxos e duas moléculas de glicerol."
            },
            correctIndex = 0,
            questionNumber = 18,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_018",
            topic = "lipids",
            subtopic = "triacylglycerols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "glycerol", "fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os triacilgliceróis (ou triglicerídeos) são ésteres formados pela reação de um glicerol (um álcool com três grupos –OH, por isso “tri-alcool”) com três moléculas de ácidos graxos. Cada ácido graxo se liga a um dos grupos hidroxila do glicerol, formando três ligações éster – daí o prefixo “tri” em triacilglicerol." }
        },

        // QUESTION 019
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual a principal função dos triacilgliceróis no organismo?",
            answers = new string[] {
                "Formar membranas celulares.",
                "Armazenar energia.",
                "Sintetizar hormônios.",
                "Transportar oxigênio."
            },
            correctIndex = 1,
            questionNumber = 19,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_019",
            topic = "lipids",
            subtopic = "triacylglycerols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "energy_storage" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Triacilgliceróis (triglicerídeos) são lipídeos neutros cuja função central no organismo é armazenar energia de forma concentrada e eficiente." }
        },

        // QUESTION 020
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triacilgliceróis são armazenados principalmente em:",
            answers = new string[] {
                "Fígado",
                "Músculos",
                "Cérebro",
                "Células adiposas"
            },
            correctIndex = 3,
            questionNumber = 20,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_020",
            topic = "lipids",
            subtopic = "adipose_tissue",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "triacylglycerols", "energy_storage" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os triacilglicerois, sendo a principal forma de armazenamento de energia em animais, são estocados quase exclusivamente no tecido adiposo, dentro de células especializadas chamadas adipócitos (ou células adiposas)." }
        },

        // QUESTION 021
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O tecido adiposo tem como função:",
            answers = new string[] {
                "Armazenar gordura.",
                "Isolar o organismo.",
                "Proteger órgãos.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 21,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_021",
            topic = "lipids",
            subtopic = "adipose_tissue",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "energy_storage", "thermal_insulation", "organ_protection" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Todas as alternativas estão corretas, pois o tecido adiposo, armazena gordura como reservatório de energia do organismo, cria um isolamento térmico, especialmente em camadas subcutâneas e protege órgãos, pois o depósitos de gordura funcionam como amortecedores mecânicos, fornecendo sustentação." }
        },

        // QUESTION 022
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A hibernação é uma estratégia de sobrevivência que envolve:",
            answers = new string[] {
                "Aumento do consumo de oxigênio.",
                "Diminuição do consumo de oxigênio.",
                "Armazenamento de lipídeos.",
                "Aumento da atividade enzimática."
            },
            correctIndex = 2,
            questionNumber = 22,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_022",
            topic = "lipids",
            subtopic = "energy_storage",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "triacylglycerols", "hibernation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A hibernação é um estado especial em que o animal reduz drasticamente seu gasto de energia para sobreviver a frio intenso e falta de alimento, usando reservas internas por longos períodos. O foco central é economizar energia, o que envolve tanto mudanças no combustível usado (mais lipídeos) quanto forte depressão do metabolismo. Em esquilos, ursos, morcegos, répteis e anfíbios, há forte evidência de que reservas lipídicas são cruciais para a sobrevivência no período de jejum prolongado" }
        },

        // QUESTION 023
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Em comparação com carboidratos e proteínas, os triacilgliceróis armazenam:",
            answers = new string[] {
                "Menor quantidade de energia por grama.",
                "Maior quantidade de energia por grama.",
                "A mesma quantidade de energia por grama.",
                "Não armazenam energia."
            },
            correctIndex = 1,
            questionNumber = 23,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_023",
            topic = "lipids",
            subtopic = "energy_storage",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "triacylglycerols", "energy_density" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Triacilgliceróis (gorduras) são lipídios muito ricos em energia quando comparados a carboidratos e proteínas. A gordura é descrita como “energia densa”, porque 1 g de gordura fornece cerca de 9 kcal, enquanto 1 g de carboidrato ou proteína fornece cerca de 4 kcal 12. Por unidade de massa, a oxidação de ácidos graxos rende quantidade de ATP coerente com esse maior conteúdo energético em relação à glicose, confirmando a superioridade energética dos lipídios por grama" }
        },

        // QUESTION 024
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos essenciais são aqueles que:",
            answers = new string[] {
                "O organismo produz em grande quantidade.",
                "O organismo não consegue sintetizar.",
                "São encontrados apenas em animais.",
                "São encontrados apenas em plantas."
            },
            correctIndex = 1,
            questionNumber = 24,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_024",
            topic = "lipids",
            subtopic = "essential_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "dietary_fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os ácidos graxos essenciais são chamados assim porque o corpo humano precisa deles para funcionar adequadamente, mas não é capaz de produzi-los sozinho em quantidade suficiente. Por isso, eles devem ser obtidos por meio da alimentação." }
        },

        // QUESTION 025
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Exemplos de ácidos graxos essenciais são:",
            answers = new string[] {
                "Ácido esteárico e ácido palmítico.",
                "Ácido linoléico e ácido linolênico.",
                "Ácido oléico e ácido palmitoléico.",
                "Ácido araquidônico e ácido eicosapentaenóico."
            },
            correctIndex = 1,
            questionNumber = 25,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_025",
            topic = "lipids",
            subtopic = "essential_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "linoleic_acid", "linolenic_acid" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Tanto o Ácido linoleico (ômega-6), quanto o Ácido linolênico (ômega-3), são considerados os principais ácidos graxos essenciais, porque o organismo humano não consegue sintetizá-los e, portanto, precisam ser obtidos pela alimentação." }
        },

        // QUESTION 026
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O ácido linoléico (ômega-6) é precursor de:",
            answers = new string[] {
                "Prostaglandinas e tromboxanas.",
                "Vitamina D.",
                "Colesterol",
                "Glicogênio"
            },
            correctIndex = 0,
            questionNumber = 26,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_026",
            topic = "lipids",
            subtopic = "essential_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "omega_6", "eicosanoids", "linoleic_acid" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O ácido linoleico (ômega-6) é um ácido graxo essencial que participa da formação do ácido araquidônico, o qual serve de precursor para a produção de vários eicosanoides, incluindo: prostaglandinas, tromboxanas, leucotrienos. As outras alternativas estão incorretas porque: Vitamina D é produzida a partir do colesterol na pele sob ação da luz solar; colesterol não é derivado do ácido linoleico; glicogênio é uma forma de armazenamento de glicose, relacionado ao metabolismo de carboidratos, não de lipídios." }
        },

        // QUESTION 027
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "O desenvolvimento cerebral.",
                "A função imunológica.",
                "A saúde da retina.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 27,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/LipidDB/LipidsDB_ImageQuestionContainer27",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_027",
            topic = "lipids",
            subtopic = "essential_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "omega_fatty_acids", "dietary_fatty_acids" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 028
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A deficiência de ácidos graxos essenciais pode causar:",
            answers = new string[] {
                "Dermatite.",
                "Problemas neurológicos.",
                "Problemas no desenvolvimento de bebês.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 28,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_028",
            topic = "lipids",
            subtopic = "essential_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "deficiency", "skin_health", "growth" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A deficiência de ácidos graxos essenciais pode causar diversos problemas porque eles são fundamentais para o funcionamento normal do organismo.Entre as consequências estão: Dermatite, problemas neurológicos, problemas no desenvolvimento de bebês. Esses efeitos ocorrem porque os ácidos graxos essenciais ajudam na: formação das membranas celulares, produção de substâncias reguladoras, manutenção do cérebro e da pele, crescimento e desenvolvimento infantil." }
        },

        // QUESTION 029
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Identifique abaixo o ácido graxo na conformação trans.",
            answers = new string[] {
                "AnswerImages/LipidDB/colesterol",
                "AnswerImages/LipidDB/acido_graxo_di_insaturado",
                "AnswerImages/LipidDB/acido_graxo_mono_insaturado",
                "AnswerImages/LipidDB/acido_graxo_trans"
            },
            correctIndex = 3,
            questionNumber = 29,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_029",
            topic = "lipids",
            subtopic = "trans_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "cis_trans_isomerism", "unsaturated_fatty_acids" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 030
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O consumo de ácidos graxos trans está associado a:",
            answers = new string[] {
                "Diminuição do risco de doenças cardíacas.",
                "Aumento do risco de doenças cardíacas.",
                "Nenhuma alteração no risco de doenças cardíacas.",
                "Aumento da produção de HDL."
            },
            correctIndex = 1,
            questionNumber = 30,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_030",
            topic = "lipids",
            subtopic = "trans_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "cardiovascular_risk", "health_effects" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os ácidos graxos trans estão associados ao aumento do risco de doenças cardiovasculares porque provocam alterações prejudiciais no perfil lipídico, como: aumento do LDL (“colesterol ruim”), redução do HDL (“colesterol bom”), maior inflamação e formação de placas nas artérias. Esses efeitos favorecem problemas como: aterosclerose, infarto e acidente vascular cerebral (AVC)." }
        },

        // QUESTION 031
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A margarina é um composto:",
            answers = new string[] {
                "Natural, composto somente por ácidos graxos saturados.",
                "Artificial, composto somente por ácidos graxos insaturados.",
                "Artificial, composto por ácidos graxos saturados e insaturados.",
                "Natural, composto somente por ácidos graxos trans."
            },
            correctIndex = 2,
            questionNumber = 31,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_031",
            topic = "lipids",
            subtopic = "hydrogenation",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "margarine", "trans_fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A margarina é um alimento industrializado (artificial) produzido principalmente a partir de óleos vegetais líquidos. Para adquirir consistência semissólida semelhante à da manteiga, esses óleos passam por processos tecnológicos, como a hidrogenação parcial ou a interesterificação. Os óleos vegetais utilizados na fabricação da margarina são ricos em ácidos graxos insaturados, mas durante o processamento parte dessas moléculas pode ser transformada em ácidos graxos saturados ou ter sua estrutura modificada." }
        },

        // QUESTION 032
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A hidrogenação de óleos vegetais resulta em:",
            answers = new string[] {
                "Aumento do grau de insaturação.",
                "Diminuição cadeia carbônica.",
                "Aumento do ponto de fusão.",
                "Diminuição do ponto de fusão."
            },
            correctIndex = 2,
            questionNumber = 32,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_032",
            topic = "lipids",
            subtopic = "hydrogenation",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "melting_point", "vegetable_oils", "trans_fatty_acids" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 033
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Reação de neutralização",
                "Saponificação",
                "Acilação",
                "Esterificação"
            },
            correctIndex = 1,
            questionNumber = 33,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/LipidDB/LipidsDB_ImageQuestionContainer33",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_033",
            topic = "lipids",
            subtopic = "lipid_reactions",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "saponification", "fatty_acid_salts" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A saponificação é uma hidrólise alcalina de ésteres. Como os triglicerídeos são ésteres formados por glicerol e ácidos graxos, a ação do NaOH quebra as ligações éster, liberando o glicerol e formando os sais dos ácidos graxos." }
        },

        // QUESTION 034
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Transesterificação",
                "Saponificação",
                "Acilação",
                "Esterificação"
            },
            correctIndex = 0,
            questionNumber = 34,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/LipidDB/LipidsDB_ImageQuestionContainer34",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_034",
            topic = "lipids",
            subtopic = "lipid_reactions",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "transesterification", "biodiesel" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A reação é denominada transesterificação porque ocorre a troca do grupo alcoólico ligado ao éster. Os ácidos graxos originalmente esterificados ao glicerol passam a ficar ligados ao metanol ou etanol, formando novos ésteres. O glicerol é liberado como subproduto. Essa reação é amplamente utilizada na produção de biodiesel." }
        },

        // QUESTION 035
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Óleo de cozinha",
                "Lubrificante",
                "Biodiesel",
                "Detergente"
            },
            correctIndex = 3,
            questionNumber = 35,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/LipidDB/LipidsDB_ImageQuestionContainer35",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_035",
            topic = "lipids",
            subtopic = "amphipathic_molecules",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "detergents", "micelles" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 036
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "",
            answers = new string[] {
                "Óleo de cozinha",
                "Lubrificante",
                "Biodiesel",
                "Detergente"
            },
            correctIndex = 2,
            questionNumber = 36,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/LipidDB/LipidsDB_ImageQuestionContainer36",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_036",
            topic = "lipids",
            subtopic = "lipid_reactions",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "biodiesel", "transesterification" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 037
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os esteróis são a terceira maior classe de lipídeos encontrados em membranas celulares. O principal deles é o colesterol. Qual é a estrutura do colesterol?",
            answers = new string[] {
                "AnswerImages/LipidDB/acido_graxo_tri_insaturado",
                "AnswerImages/LipidDB/esterol",
                "AnswerImages/LipidDB/fosfatidilcolina",
                "AnswerImages/LipidDB/colesterol"
            },
            correctIndex = 3,
            questionNumber = 37,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_037",
            topic = "lipids",
            subtopic = "sterols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "cholesterol", "steroid_ring" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 038
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O colesterol tem como função:",
            answers = new string[] {
                "Formar membranas celulares.",
                "Ser precursor de hormônios.",
                "Ser precursor de sais biliares.",
                "Todas as alternativas anteriores."
            },
            correctIndex = 3,
            questionNumber = 38,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_038",
            topic = "lipids",
            subtopic = "cholesterol",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membranes", "steroid_hormones", "bile_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O colesterol é um componente estrutural das membranas plasmáticas das células animais. Ele se intercala entre os fosfolipídios da membrana, contribuindo para a sua estabilidade, fluidez e permeabilidade seletiva. Além de ser a molécula precursora dos hormônios esteroides, produzidos principalmente pelas glândulas suprarrenais e pelas gônadas. No fígado, o colesterol é convertido em ácidos e sais biliares, substâncias armazenadas na vesícula biliar e liberadas no intestino delgado durante a digestão. Os sais biliares atuam na emulsificação das gorduras, aumentando a eficiência da digestão e da absorção dos lipídios e das vitaminas lipossolúveis (A, D, E e K)." }
        },

        // QUESTION 039
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual a importância do colesterol para a membrana celular?",
            answers = new string[] {
                "Confere maior rigidez a membrana celular",
                "Forma sítios hidrofílicos no meio da membrana celular",
                "Atuam interagindo com a água",
                "Introduz insaturações do tipo trans na membrana celular."
            },
            correctIndex = 0,
            questionNumber = 39,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_039",
            topic = "lipids",
            subtopic = "cholesterol",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_fluidity", "membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Ele se intercala entre os fosfolipídios da membrana, contribuindo para a sua estabilidade, fluidez e permeabilidade seletiva. Dessa forma, ajuda a manter a integridade da célula e a regular a movimentação de substâncias através da membrana. Portanto, confere maior rigidez à membrana celular." }
        },

        // QUESTION 040
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Ácidos graxos insaturados são encontrados principalmente em:",
            answers = new string[] {
                "Gorduras animais.",
                "Óleos vegetais.",
                "Cereais.",
                "Leguminosas."
            },
            correctIndex = 1,
            questionNumber = 40,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_040",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "vegetable_oils" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os ácidos graxos insaturados possuem uma ou mais ligações duplas na cadeia carbônica, característica que lhes confere menor ponto de fusão e estado líquido em temperatura ambiente. Esses lipídios são encontrados principalmente nos óleos vegetais." }
        },

        // QUESTION 041
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que é um ácido graxo monoinsaturado?",
            answers = new string[] {
                "Um ácido graxo com uma dupla ligação.",
                "Um ácido graxo com múltiplas ligações duplas.",
                "Um ácido graxo saturado.",
                "Um ácido graxo com um grupo amino."
            },
            correctIndex = 0,
            questionNumber = 41,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_041",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "monounsaturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os ácidos graxos monoinsaturados possuem uma única ligação dupla em sua cadeia carbônica. Essa característica os diferencia dos ácidos graxos saturados (sem ligações duplas) e dos poli-insaturados (com duas ou mais ligações duplas)" }
        },

        // QUESTION 042
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que é um ácido graxo poliinsaturado?",
            answers = new string[] {
                "Um ácido graxo saturado.",
                "Um ácido graxo com uma dupla ligação.",
                "Um ácido graxo com múltiplas ligações duplas.",
                "Um ácido graxo com um grupo fosfato."
            },
            correctIndex = 2,
            questionNumber = 42,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_042",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "polyunsaturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os ácidos graxos poli-insaturados são lipídios que apresentam duas ou mais ligações duplas em sua cadeia carbônica, como os ômegas 3 e 6. Essas insaturações aumentam a fluidez das moléculas e são fundamentais para diversas funções biológicas." }
        },

        // QUESTION 043
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A configuração cis e trans em ácidos graxos se refere a:",
            answers = new string[] {
                "O comprimento da cadeia.",
                "O grau de saturação.",
                "A posição das duplas ligações.",
                "A orientação dos grupamentos ao redor de uma ligação dupla."
            },
            correctIndex = 3,
            questionNumber = 43,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_043",
            topic = "lipids",
            subtopic = "cis_trans_isomerism",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "unsaturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "As denominações cis e trans descrevem a configuração espacial dos átomos ou grupamentos ligados aos carbonos de uma ligação dupla (C=C) em um ácido graxo. Na forma cis, a cadeia apresenta uma dobra; na forma trans, a molécula é mais linear." }
        },

        // QUESTION 044
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são, em sua maioria, compostos por:",
            answers = new string[] {
                "Nitrogênio e fósforo",
                "Carbono e enxofre",
                "Carbono, hidrogênio e oxigênio",
                "Oxigênio e cloro"
            },
            correctIndex = 2,
            questionNumber = 44,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_044",
            topic = "lipids",
            subtopic = "lipid_properties",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "carbon", "hydrogen", "oxygen" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídeos constituem um grupo diversificado de biomoléculas orgânicas caracterizadas por serem insolúveis em água e solúveis em solventes orgânicos. Em sua composição química, predominam os elementos carbono (C), hidrogênio (H) e oxigênio (O)." }
        },

        // QUESTION 045
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual das funções abaixo é típica dos lipídeos?",
            answers = new string[] {
                "Transportar oxigênio",
                "Carregar informações genéticas",
                "Catalisar reações químicas",
                "Armazenar energia"
            },
            correctIndex = 3,
            questionNumber = 45,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_045",
            topic = "lipids",
            subtopic = "lipid_functions",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "energy_storage" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídeos desempenham diversas funções biológicas, mas uma de suas principais funções é o armazenamento de energia. Nos organismos animais, a energia é armazenada principalmente na forma de triacilgliceróis (triglicerídeos), acumulados no tecido adiposo." }
        },

        // QUESTION 046
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "As cadeias carbônicas dos ácidos graxos são caracterizadas como:",
            answers = new string[] {
                "Hidrofílicas",
                "Hidrofóbicos",
                "Anfipáticas",
                "Polares"
            },
            correctIndex = 1,
            questionNumber = 46,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_046",
            topic = "lipids",
            subtopic = "fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "hydrophobicity", "hydrocarbon_chain" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A cadeia carbônica é constituída por ligações apolares entre carbono e hidrogênio, não apresentando cargas elétricas capazes de interagir de forma significativa com as moléculas de água. Por esse motivo, essa região da molécula é considerada hidrofóbica (\"aversão à água\")." }
        },

        // QUESTION 047
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual lipídeo é o principal componente das membranas celulares?",
            answers = new string[] {
                "Fosfolipídios",
                "Triglicerídeos",
                "Cerídeos",
                "Esteroides"
            },
            correctIndex = 0,
            questionNumber = 47,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_047",
            topic = "lipids",
            subtopic = "phospholipids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "membranes", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os fosfolipídios são os principais constituintes das membranas celulares devido à sua natureza anfipática, que permite a formação espontânea da bicamada lipídica. Embora o colesterol e proteínas também façam parte da membrana, os fosfolipídios formam sua estrutura básica." }
        },

        // QUESTION 048
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triglicerídeos são formados por:",
            answers = new string[] {
                "Ácidos graxos e glicerol",
                "Triglicerídeos",
                "Nucleotídeos e açúcar",
                "Glicerol e bases nitrogenadas"
            },
            correctIndex = 0,
            questionNumber = 48,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_048",
            topic = "lipids",
            subtopic = "triacylglycerols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "glycerol", "fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os triglicerídeos são moléculas formadas pela ligação de três ácidos graxos a uma molécula de glicerol, constituindo a principal reserva energética dos organismos." }
        },

        // QUESTION 049
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os triglicerídeos são formados por:",
            answers = new string[] {
                "Ácidos graxos e glicerol",
                "Triglicerídeos",
                "Nucleotídeos e açúcar",
                "Glicerol e bases nitrogenadas"
            },
            correctIndex = 0,
            questionNumber = 49,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_049",
            topic = "lipids",
            subtopic = "triacylglycerols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "glycerol", "fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os triglicerídeos são moléculas formadas pela ligação de três ácidos graxos a uma molécula de glicerol, constituindo a principal reserva energética dos organismos." }
        },

        // QUESTION 050
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual lipídeo atua como precursor dos hormônios esteroides?",
            answers = new string[] {
                "Lecitina",
                "Colesterol",
                "Ácido oleico",
                "Cerídeos"
            },
            correctIndex = 1,
            questionNumber = 50,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_050",
            topic = "lipids",
            subtopic = "cholesterol",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "steroid_hormones", "sterols" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O colesterol é um lipídeo pertencente à classe dos esteróis e desempenha funções fundamentais no organismo animal. Uma de suas funções mais importantes é atuar como precursor dos hormônios esteroides, moléculas responsáveis pela regulação de diversos processos fisiológicos." }
        },

        // QUESTION 051
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos insaturados diferem dos saturados por:",
            answers = new string[] {
                "Possuírem ligações duplas na cadeia carbônica",
                "Possuírem apenas ligações simples",
                "Não apresentarem carbono",
                "Serem sempre sólidos à temperatura ambiente"
            },
            correctIndex = 0,
            questionNumber = 51,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_051",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "saturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A principal diferença entre ácidos graxos saturados e insaturados é a presença de ligações duplas na cadeia carbônica. Essas ligações modificam as propriedades físicas dos lipídios, tornando-os mais fluidos e geralmente líquidos à temperatura ambiente." }
        },

        // QUESTION 052
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são biomoléculas caracterizadas principalmente por:",
            answers = new string[] {
                "Alta solubilidade em água",
                "Baixa solubilidade em água",
                "Presença obrigatória de nitrogênio",
                "Função exclusivamente energética"
            },
            correctIndex = 1,
            questionNumber = 52,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_052",
            topic = "lipids",
            subtopic = "lipid_properties",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "water_solubility", "organic_solvents" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídeos são biomoléculas formadas predominantemente por longas cadeias de carbono e hidrogênio, o que lhes confere caráter apolar (hidrofóbico). Por isso, apresentam baixa solubilidade em água e alta solubilidade em solventes orgânicos, como éter e clorofórmio." }
        },

        // QUESTION 053
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Uma das principais funções dos lipídeos no organismo é:",
            answers = new string[] {
                "Armazenamento de energia",
                "Transmissão de impulsos nervosos exclusivamente",
                "Formação de proteínas",
                "Transporte de oxigênio no sangue"
            },
            correctIndex = 0,
            questionNumber = 53,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_053",
            topic = "lipids",
            subtopic = "lipid_functions",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "energy_storage" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídeos constituem a principal forma de reserva energética de longo prazo dos organismos, armazenando grande quantidade de energia em um volume relativamente pequeno." }
        },

        // QUESTION 054
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Um exemplo de lipídeo de reserva energética encontrado em animais é:",
            answers = new string[] {
                "Glicogênio",
                "Colesterol",
                "Triglicerídeo",
                "Fosfolipídio"
            },
            correctIndex = 2,
            questionNumber = 54,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_054",
            topic = "lipids",
            subtopic = "triacylglycerols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "energy_storage", "animal_fats" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os triglicerídeos (ou triacilgliceróis) são os lipídeos mais abundantes nos organismos vivos e representam a principal forma de armazenamento de energia em animais e plantas. Estes armazenam mais que o dobro da energia por grama quando comparados aos carboidratos." }
        },

        // QUESTION 055
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos que apresentam regiões polares e apolares são chamados de:",
            answers = new string[] {
                "Hidrofílicos",
                "Hidrofóbicos",
                "Aromáticos",
                "Anfipáticos"
            },
            correctIndex = 3,
            questionNumber = 55,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_055",
            topic = "lipids",
            subtopic = "amphipathic_molecules",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "polar_region", "nonpolar_region" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O caráter de possui uma região polar (hidrofílica), que interage com a água e uma região apolar (hidrofóbica), que evita contato com a água é chamado de anfipático. Um exemplo clássico de lipídios anfipáticos são os fosfolipídios, constituintes de membranas celulares." }
        },

        // QUESTION 056
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A função dos fosfolipídios na membrana celular é principalmente:",
            answers = new string[] {
                "Produzir energia imediata",
                "Armazenar glicose",
                "Formar a bicamada lipídica",
                "Catalisar reações químicas"
            },
            correctIndex = 2,
            questionNumber = 56,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_056",
            topic = "lipids",
            subtopic = "phospholipids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membranes", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Quando estão em meio aquoso, os fosfolipídios organizam-se espontaneamente em uma bicamada fosfolipídica, na qual as cabeças polares ficam voltadas para os meios intra e extracelular, enquanto as caudas apolares ficam voltadas umas para as outras. Essa bicamada constitui a base estrutural da membrana plasmática, conforme o modelo do mosaico fluido, proposto por Singer e Nicolson (1972)." }
        },

        // QUESTION 057
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídeos são principalmente compostos por:",
            answers = new string[] {
                "Aminoácidos e nucleotídeos",
                "Glicerol e ácidos graxos",
                "Monossacarídeos e polissacarídeos",
                "Peptídeos e cofatores"
            },
            correctIndex = 1,
            questionNumber = 57,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_057",
            topic = "lipids",
            subtopic = "lipid_composition",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "glycerol", "fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídeos são constituídos principalmente por glicerol e ácidos graxos, moléculas que formam estruturas como os triglicerídeos e diversos outros compostos lipídicos" }
        },

        // QUESTION 058
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual é a principal função dos lipídeos de reserva nos animais?",
            answers = new string[] {
                "Armazenar informações genéticas",
                "Catalisar reações químicas",
                "Armazenar energia a longo prazo",
                "Transportar oxigênio"
            },
            correctIndex = 2,
            questionNumber = 58,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_058",
            topic = "lipids",
            subtopic = "energy_storage",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "triacylglycerols", "long_term_energy" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Nos organismos animais, os lipídios tem a principal função de armazenar a energia a longo prazo. A energia é armazenada principalmente na forma de triacilgliceróis (triglicerídeos), acumulados no tecido adiposo." }
        },

        // QUESTION 059
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O colesterol é classificado como:",
            answers = new string[] {
                "Esteroide",
                "Fosfolipídeo",
                "Glicerídeo",
                "Terpeno"
            },
            correctIndex = 0,
            questionNumber = 59,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_059",
            topic = "lipids",
            subtopic = "sterols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "cholesterol", "steroids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O colesterol é um lipídeo pertencente à classe dos esteróis e desempenha funções fundamentais no organismo animal. Uma de suas funções mais importantes é atuar como precursor dos hormônios esteroides" }
        },

        // QUESTION 060
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os fosfolipídeos são fundamentais porque:",
            answers = new string[] {
                "Atuam como catalisadores",
                "Formam a bicamada das membranas celulares",
                "São hormônios sexuais",
                "Fornecem energia imediata"
            },
            correctIndex = 1,
            questionNumber = 60,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_060",
            topic = "lipids",
            subtopic = "phospholipids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membranes", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os fosfolipídios, são os principais componentes da bicamada das membranas celulares, devido o seu caráter anfipático." }
        },

        // QUESTION 061
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "O que diferencia ácidos graxos saturados de insaturados?",
            answers = new string[] {
                "Presença ou ausência de grupo carboxila",
                "Quantidade de átomos de oxigênio",
                "Presença de ligações duplas entre carbonos",
                "Presença de fósforo na cadeia"
            },
            correctIndex = 2,
            questionNumber = 61,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_061",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "saturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os ácidos graxos são classificados em saturados e insaturados de acordo com a presença ou ausência de ligações duplas entre os átomos de carbono de sua cadeia carbônica.Os ácidos graxos saturados apresentam apenas ligações simples. Já os ácidos graxos insaturados possuem uma ou mais ligações duplas (C=C) na cadeia carbônica." }
        },

        // QUESTION 062
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os óleos vegetais, em temperatura ambiente, geralmente são:",
            answers = new string[] {
                "Sólidos, pois são saturados",
                "Líquidos, pois são insaturados",
                "Gasosos, pois são voláteis",
                "Sólidos, pois contêm esteroides"
            },
            correctIndex = 1,
            questionNumber = 62,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_062",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "vegetable_oils", "melting_point" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os óleos vegetais possuem elevada proporção de ácidos graxos insaturados, que apresentam uma ou mais ligações duplas na cadeia carbônica. Essas ligações criam dobras na molécula, dificultando seu empacotamento e reduzindo as forças de atração entre as cadeias. Como consequência, os óleos apresentam menor ponto de fusão e permanecem líquidos à temperatura ambiente." }
        },

        // QUESTION 063
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os fosfolipídios que compõem a membrana celular possuem uma característica importante. Eles são:",
            answers = new string[] {
                "Totalmente hidrofóbicos",
                "Totalmente hidrofílicos",
                "Anfipáticos",
                "Apolares"
            },
            correctIndex = 2,
            questionNumber = 63,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_063",
            topic = "lipids",
            subtopic = "phospholipids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "amphipathic_molecules", "membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os fosfolipídios são caracterizados por serem anfipáticos, ou seja, possuírem região polar e região apolar" }
        },

        // QUESTION 064
        new Question
        {
           questionDatabankName = "LipidsQuestionDatabase",
           questionText = "Em um fosfolipídio, a região hidrofílica corresponde:",
           answers = new string[] {
               "Às cadeias de ácidos graxos",
               "Ao grupo fosfato",
               "Ao glicerol apenas",
               "Às ligações duplas"
           },
           correctIndex = 1,
           questionNumber = 64,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 1,
           questionInDevelopment = false,
            globalId = "lipids_064",
            topic = "lipids",
            subtopic = "phospholipids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "hydrophilic_head", "phosphate_group" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "A região hidrofílica dos fosfolipídios corresponde ao grupo fosfato, que é carregado negativamente e possui uma molécula de glicerol associada. Sendo capaz de interagir com a água." }
       },

        // QUESTION 065
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os hormônios sexuais (como testosterona e estrógeno) derivam de qual lipídeo?",
            answers = new string[] {
                "Fosfolipídeos",
                "Colesterol",
                "Glicerídeos",
                "Carotenoides"
            },
            correctIndex = 1,
            questionNumber = 65,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "lipids_065",
            topic = "lipids",
            subtopic = "cholesterol",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "steroid_hormones", "sex_hormones" },
            prerequisites = null,
            questionHint = null
        },

        // QUESTION 066
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A principal função dos lipídeos na membrana plasmática é:",
            answers = new string[] {
                "Armazenar glicose",
                "Regular a temperatura do corpo",
                "Garantir a barreira seletiva e a fluidez da membrana",
                "Fornecer aminoácidos essenciais"
            },
            correctIndex = 2,
            questionNumber = 66,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_066",
            topic = "lipids",
            subtopic = "phospholipids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membrane_fluidity", "selective_barrier", "membranes" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os principais lipídios que constituem a membrana plasmática são os fosfolipídios. Sua formação garante a barreira seletiva e a fluidez da membrana na célula" }
        },

        // QUESTION 067
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A manteiga é sólida à temperatura ambiente principalmente devido à predominância de:",
            answers = new string[] {
                "Ácidos graxos insaturados",
                "Ácidos graxos saturados",
                "Fosfolipídios",
                "Colesterol"
            },
            correctIndex = 1,
            questionNumber = 67,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_067",
            topic = "lipids",
            subtopic = "saturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "melting_point", "solid_fats", "butter" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídios que constituem a manteiga são, em geral, ácidos graxos saturados. Sua formação, dada através de ligações simples, permite maior ponto de fusão, se mantendo sólida por mais tempo." }
        },

        // QUESTION 068
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídios são compostos orgânicos caracterizados principalmente por:",
            answers = new string[] {
                "Alta solubilidade em água",
                "Baixa solubilidade em água e solubilidade em solventes orgânicos",
                "Estrutura formada por nucleotídeos",
                "Sempre possuírem função enzimática"
            },
            correctIndex = 1,
            questionNumber = 68,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_068",
            topic = "lipids",
            subtopic = "lipid_properties",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "water_solubility", "organic_solvents" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídios são moléculas formadas principalmente por cadeias de carbono e hidrogênio, o que lhes confere caráter apolar. Por isso, apresentam baixa solubilidade em água e são solúveis em solventes orgânicos, como éter, benzeno e clorofórmio." }
        },

        // QUESTION 069
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os principais componentes de óleos e gorduras são:",
            answers = new string[] {
                "Fosfolipídios",
                "Glicídios",
                "Triglicerídeos",
                "Esteroides"
            },
            correctIndex = 2,
            questionNumber = 69,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_069",
            topic = "lipids",
            subtopic = "triacylglycerols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "oils_and_fats" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os óleos e gorduras são constituídos principalmente por triglicerídeos (triacilgliceróis), moléculas formadas pela ligação de uma molécula de glicerol com três moléculas de ácidos graxos." }
        },

        // QUESTION 070
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Um lipídio formado por glicerol + 3 ácidos graxos é denominado:",
            answers = new string[] {
                "Fosfolipídio",
                "Esteroide",
                "Triglicerídeo",
                "Cerídeo"
            },
            correctIndex = 2,
            questionNumber = 70,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_070",
            topic = "lipids",
            subtopic = "triacylglycerols",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "glycerol", "fatty_acids" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "O prefixo \"Tri\" indica que a molécula é formada por três estruturas semelhantes, no caso, os ácidos graxos. Já o \"glicerídeos\" indica que há, também, uma parte de glicerol na molécula" }
        },

        // QUESTION 071
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os fosfolipídios são importantes porque:",
            answers = new string[] {
                "Formam a parede celular dos vegetais",
                "Atuam como catalisadores",
                "Compõem a membrana plasmática das células",
                "São responsáveis pelo transporte de oxigênio"
            },
            correctIndex = 2,
            questionNumber = 71,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_071",
            topic = "lipids",
            subtopic = "phospholipids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "membranes", "lipid_bilayer" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os fosfolipídios são os principais lipídios que constituem a membrana plasmática das células" }
        },

        // QUESTION 072
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Qual dos lipídios abaixo possui função hormonal?",
            answers = new string[] {
                "Triglicerídeos",
                "Esteroides",
                "Fosfolipídios",
                "Cerídeos"
            },
            correctIndex = 1,
            questionNumber = 72,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_072",
            topic = "lipids",
            subtopic = "steroids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "steroid_hormones", "lipid_functions" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os esteroides, são hormônios sexuais, como testosterona e estrógeno, enzimáticas na molécula de colesterol, principalmente nas gônadas e nas glândulas suprarrenais." }
        },

        // QUESTION 073
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "A principal função dos lipídios de reserva é:",
            answers = new string[] {
                "Fornecer energia de curto prazo",
                "Armazenar energia de longo prazo",
                "Atuar como cofatores enzimáticos",
                "Regular o pH celular"
            },
            correctIndex = 1,
            questionNumber = 73,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_073",
            topic = "lipids",
            subtopic = "energy_storage",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "triacylglycerols", "long_term_energy" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os lipídios de reserva podem ser chamados de triglicerídeos, estes tem a principal função de armazenar energia a longo prazo." }
        },

        // QUESTION 074
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os lipídios que atuam como isolantes térmicos em animais são principalmente:",
            answers = new string[] {
                "Fosfolipídios",
                "Esteroides",
                "Glicídios",
                "Triglicerídeos"
            },
            correctIndex = 3,
            questionNumber = 74,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_074",
            topic = "lipids",
            subtopic = "adipose_tissue",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "triacylglycerols", "thermal_insulation" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os triglicerídeos, pois criam camadas de gordura corporal capazes tanto de armazenar energia quanto de criar isolantes térmicos." }
        },

        // QUESTION 075
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Um exemplo de cera (cerídeo) é:",
            answers = new string[] {
                "Colesterol",
                "Cutina das folhas",
                "Fosfatidilcolina",
                "Amido"
            },
            correctIndex = 1,
            questionNumber = 75,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "lipids_075",
            topic = "lipids",
            subtopic = "waxes",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "cerides", "plant_cuticle" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os cerídeos (ceras) são lipídios formados pela união de ácidos graxos de cadeia longa com álcoois de cadeia longa, por meio de ligações éster. Sua principal função biológica é a proteção e impermeabilização de superfícies expostas ao ambiente. Portanto, são muito presentes na cutina das folhas." }
        },

        // QUESTION 076
        new Question
        {
            questionDatabankName = "LipidsQuestionDatabase",
            questionText = "Os ácidos graxos insaturados diferem dos saturados porque:",
            answers = new string[] {
                "Possuem cadeias ramificadas",
                "Apresentam uma ou mais duplas ligações na cadeia carbônica",
                "Não contêm hidrogênio em sua estrutura",
                "São encontrados apenas em animais"
            },
            correctIndex = 1,
            questionNumber = 76,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "lipids_076",
            topic = "lipids",
            subtopic = "unsaturated_fatty_acids",
            displayName = "Lipídeos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "saturated_fatty_acids", "double_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint { text = "Os ácidos graxos são classificados em saturados e insaturados de acordo com a presença ou ausência de ligações duplas entre os átomos de carbono de sua cadeia carbônica.Os ácidos graxos saturados apresentam apenas ligações simples. Já os ácidos graxos insaturados possuem uma ou mais ligações duplas (C=C) na cadeia carbônica." }
        }
    };

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.lipids;
    public string GetDatabankName()  => "LipidsQuestionDatabase";
    public string GetDisplayName()   => "Lipídeos";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}