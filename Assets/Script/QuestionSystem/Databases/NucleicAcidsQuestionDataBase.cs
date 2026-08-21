using System.Collections.Generic;
using QuestionSystem;

public class NucleicAcidsQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
        //         //QUESTION 001
        //         new Question {
        //             questionDatabankName = "NucleicAcidsQuestionDatabase",
        //             questionText = "Quem primeiro isolou o ácido nucléico?",
        //             answers = new string[] { "Watson", "Crick", "Friedrich Miescher", "Chargaff" },
        //             correctIndex = 2,
        //             questionNumber = 1,
        //             answerType = AnswerType.Text,
        //             questionType = QuestionType.Text,
        //             questionImagePath = "",
        //             questionLevel = 1,
        //             questionInDevelopment = false,
        //             globalId = "nucleicAcids_001",
        //             topic = "nucleicAcids",
        //             subtopic = "nucleic_acid_history",
        //             displayName = "Ácidos Nucleicos",
        //             bloomLevel = BloomLevel.Remember,
        //             conceptTags = new List<string> { "friedrich_miescher", "nuclein" },
        //             prerequisites = null,
        //             questionHint = new QuestionHint
        //             {
        //                 text = "Esta questão recupera um marco anterior ao modelo da dupla hélice. Revise o início da história dos ácidos nucleicos, quando uma substância rica em fósforo foi isolada de núcleos celulares e recebeu o nome de nucleína. Diferencie esse isolamento das contribuições posteriores de Chargaff, Watson e Crick.",
        //                 imagePath = null,
        //                 videoUrl = null,
        //                 link = null
        //             }
        //         },

        //         //QUESTION 002
        //         new Question
        //         {
        //             questionDatabankName = "NucleicAcidsQuestionDatabase",
        //             questionText = "Qual a principal função do RNA na célula?",
        //             answers = new string[] { 
        //                 "Armazenamento de informação genética", 
        //                 "Síntese de proteínas", 
        //                 "Catálise de reações", 
        //                 "Transporte de íons" 
        //             },
        //             correctIndex = 1,
        //             questionNumber = 2,
        //             answerType = AnswerType.Text,
        //             questionType = QuestionType.Text,
        //             questionImagePath = "",
        //             questionLevel = 1,
        //             questionInDevelopment = false,
        //             globalId = "nucleicAcids_002",
        //             topic = "nucleicAcids",
        //             subtopic = "rna_function",
        //             displayName = "Ácidos Nucleicos",
        //             bloomLevel = BloomLevel.Remember,
        //             conceptTags = new List<string> { "protein_synthesis", "gene_expression" },
        //             prerequisites = null,
        //             questionHint = new QuestionHint
        //             {
        //                 text = "Pense no RNA como participante da expressão da informação armazenada no DNA. Embora alguns RNAs tenham atividade catalítica, considere a função que reúne RNAm, RNAt e RNAr em um mesmo processo celular. A alternativa mais abrangente deve representar o papel central desses três tipos de RNA.",
        //                 imagePath = null,
        //                 videoUrl = null,
        //                 link = null
        //             }
    //},

         //QUESTION 003
         new Question {
             questionDatabankName = "NucleicAcidsQuestionDatabase",
             questionText = "Quais são os três componentes de um nucleotídeo?",
             answers = new string[] {
                 "AnswerImages/NucleicAcidDB/NucleicAcidsDB_ImageAnswer003.0",
                 "AnswerImages/NucleicAcidDB/NucleicAcidsDB_ImageAnswer003.1",
                 "AnswerImages/NucleicAcidDB/NucleicAcidsDB_ImageAnswer003.2",
                 "AnswerImages/NucleicAcidDB/NucleicAcidsDB_ImageAnswer003.3"
             },
             correctIndex = 0,
             questionNumber = 3,
             answerType = AnswerType.Image,
             questionType = QuestionType.Text,
             questionImagePath = "",
             questionLevel = 1,
             questionInDevelopment = false,
             globalId = "nucleicAcids_003",
             topic = "nucleicAcids",
             subtopic = "nucleotide_structure",
             displayName = "Ácidos Nucleicos",
             bloomLevel = BloomLevel.Remember,
             conceptTags = new List<string> { "pentose", "nitrogenous_base", "phosphate_group" },
             prerequisites = null,
             questionHint = new QuestionHint
             {
                 text = "Um nucleotídeo possui uma parte orgânica formada por uma pentose ligada a uma base nitrogenada e uma parte que participa das ligações da cadeia. Compare as alternativas procurando exatamente esses três elementos. Não confunda nucleotídeo com nucleosídeo, que não apresenta grupo fosfato.",
                 imagePath = null,
                 videoUrl = null,
                 link = null
             },
//         },

//         //QUESTION 004
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual açúcar está presente no RNA?",
//             answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
//             correctIndex = 1,
//             questionNumber = 4,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_004",
//             topic = "nucleicAcids",
//             subtopic = "rna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "ribose", "pentose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Compare os nomes completos dos dois ácidos nucleicos: ácido ribonucleico e ácido desoxirribonucleico. O prefixo presente em “ribonucleico” indica diretamente a pentose procurada. Glicose e frutose também são açúcares, mas não constituem o esqueleto normal das moléculas celulares de RNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 005
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual açúcar está presente no DNA?",
//             answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
//             correctIndex = 0,
//             questionNumber = 5,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_005",
//             topic = "nucleicAcids",
//             subtopic = "dna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "deoxyribose", "pentose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O próprio nome “ácido desoxirribonucleico” contém uma pista sobre sua pentose. “Desoxi” indica que esse açúcar possui um oxigênio a menos que a pentose encontrada no RNA, especificamente no carbono 2'. Procure a alternativa cujo nome expressa essa diferença química, não uma característica da dupla hélice.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 006
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que são nucleosídeos?",
//             answers = new string[] {
//                 "Açúcar + base",
//                 "Açúcar + base + fosfato",
//                 "Base + fosfato",
//                 "Açúcar + aminoácido"
//             },
//             correctIndex = 0,
//             questionNumber = 6,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_006",
//             topic = "nucleicAcids",
//             subtopic = "nucleosides",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "pentose", "nitrogenous_base" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Compare um nucleosídeo com um nucleotídeo: ambos contêm uma pentose ligada a uma base nitrogenada, mas somente um deles precisa incluir fosfato. Identifique a alternativa que representa a estrutura antes da adição do grupo fosfato. Aminoácidos não fazem parte dessa definição química.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 007
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Identifique a estrutura do nucleosídeo",
//             answers = new string[] {
//                 "AnswerImages/NucleicAcidDB/nucleotideo_di_fosfato",
//                 "AnswerImages/NucleicAcidDB/nucleotideo_mono_fosfato",
//                 "AnswerImages/NucleicAcidDB/nucleosideo",
//                 "AnswerImages/NucleicAcidDB/nucleotideo_tri_fosfato"
//             },
//             correctIndex = 2,
//             questionNumber = 7,
//             answerType = AnswerType.Image,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_007",
//             topic = "nucleicAcids",
//             subtopic = "nucleosides",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "pentose", "nitrogenous_base", "structure_identification" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Observe cada imagem procurando primeiro a pentose e a base nitrogenada. Depois verifique se há um ou mais grupos fosfato ligados ao açúcar. A estrutura pedida é justamente aquela que conserva açúcar e base, mas não apresenta fosfato; as demais correspondem a nucleotídeos fosforilados.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 008
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Quais bases são encontradas no RNA, mas não no DNA?",
//             answers = new string[] { "Adenina, guanina", "Citosina, timina", "Uracila", "Timina, uracila" },
//             correctIndex = 2,
//             questionNumber = 8,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_008",
//             topic = "nucleicAcids",
//             subtopic = "nitrogenous_bases",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "rna_bases", "uracil", "dna_rna_differences" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Monte mentalmente as listas de bases dos dois ácidos nucleicos. Adenina, guanina e citosina aparecem em ambos; a diferença está em uma pirimidina usada pelo RNA no lugar da timina. Procure a alternativa que contém apenas essa base exclusiva, sem acrescentar bases compartilhadas.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 009
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Quais bases são encontradas no DNA, mas não no RNA?",
//             answers = new string[] { "Adenina, guanina", "Citosina, timina", "Uracila", "Timina" },
//             correctIndex = 3,
//             questionNumber = 9,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_009",
//             topic = "nucleicAcids",
//             subtopic = "nitrogenous_bases",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "dna_bases", "thymine", "dna_rna_differences" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Compare as bases do DNA e do RNA, lembrando que adenina, guanina e citosina são compartilhadas. No DNA, uma pirimidina ocupa a posição correspondente à uracila do RNA. Escolha somente essa base exclusiva do DNA; alternativas com citosina incluem indevidamente uma base presente nos dois.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 010
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a função principal dos grupamentos fosfato nos nucleotídeos?",
//             answers = new string[] { "Dar caráter básico", "Dar caráter ácido", "Formar ligações peptídicas", "Armazenar energia" },
//             correctIndex = 1,
//             questionNumber = 10,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_010",
//             topic = "nucleicAcids",
//             subtopic = "nucleotide_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "phosphate_group", "acidic_character" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Os fosfatos ligam a pentose de um nucleotídeo à pentose do seguinte, formando o esqueleto externo da cadeia de ácido nucleico. Pense, portanto, em uma função estrutural de conexão entre monômeros. Não os confunda com as bases nitrogenadas, responsáveis pelo pareamento e pela informação da sequência.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 011
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Que tipo de ligação une os nucleotídeos em uma cadeia?",
//             answers = new string[] { "Ligação peptídica", "Ligação glicosídica", "Ligação éster", "Ligação fosfodiéster" },
//             correctIndex = 3,
//             questionNumber = 11,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_011",
//             topic = "nucleicAcids",
//             subtopic = "phosphodiester_bond",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "nucleotide_polymerization", "nucleic_acid_backbone" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Em uma cadeia de DNA ou RNA, o fosfato estabelece uma ponte entre o carbono 3' de uma pentose e o carbono 5' da seguinte. Recorde o nome da ligação covalente que forma esse esqueleto açúcar-fosfato. Ligações de hidrogênio, ao contrário, atuam entre bases complementares.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 012
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a orientação das cadeias de DNA em uma dupla hélice?",
//             answers = new string[] { "Paralela", "Antiparalela", "Perpendicular", "Aleatória" },
//             correctIndex = 1,
//             questionNumber = 12,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_012",
//             topic = "nucleicAcids",
//             subtopic = "dna_double_helix",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "antiparallel_strands", "dna_structure" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Cada fita possui uma extremidade 5' e outra 3'. Na dupla hélice, quando uma fita é percorrida de 5' para 3', a complementar segue no sentido oposto. Procure o termo que descreve essa orientação inversa, distinguindo-o da forma helicoidal ou do pareamento das bases.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 013
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que são pares de bases de Watson-Crick?",
//             answers = new string[] {
//                 "A-T e G-C",
//                 "A-G e T-C",
//                 "A-C e G-T",
//                 "Qualquer combinação de bases."
//             },
//             correctIndex = 0,
//             questionNumber = 13,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_013",
//             topic = "nucleicAcids",
//             subtopic = "base_pairing",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "watson_crick_pairs", "adenine_thymine", "guanine_cytosine" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Os pares de Watson-Crick seguem uma correspondência específica determinada pela geometria e pelas ligações de hidrogênio. No DNA, uma purina sempre se associa a uma pirimidina: adenina com timina e guanina com citosina. Escolha a alternativa que apresenta exatamente esses dois pareamentos canônicos.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 014
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual tipo de ligação mantém os pares de bases unidos no DNA?",
//             answers = new string[] { "Ligação iônica", "Ligação covalente", "Pontes de hidrogênio", "Ligação peptídica" },
//             correctIndex = 2,
//             questionNumber = 14,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_014",
//             topic = "nucleicAcids",
//             subtopic = "base_pairing",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "hydrogen_bonds", "dna_double_helix" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "As duas fitas do DNA precisam separar-se durante replicação e transcrição sem romper o esqueleto covalente de cada fita. Isso é possível porque as bases complementares são mantidas por interações mais fracas. Identifique essas interações, diferenciando-as das ligações fosfodiéster existentes dentro de cada cadeia.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 015
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a função principal do DNA?",
//             answers = new string[] { "Transporte de moléculas", "Síntese de proteínas", "Armazenamento de informação genética", "Catálise de reações" },
//             correctIndex = 2,
//             questionNumber = 15,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_015",
//             topic = "nucleicAcids",
//             subtopic = "dna_function",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "genetic_information", "information_storage" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Pense na molécula que permanece como arquivo hereditário da célula e cuja sequência pode ser copiada antes da divisão. Sua função principal deve envolver conservação e transmissão de instruções biológicas, não produção imediata de energia nem transporte de moléculas. Relacione estabilidade química com hereditariedade.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 016
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a função principal do RNA?",
//             answers = new string[] { "Transporte de moléculas", "Síntese de proteínas", "Armazenamento de informação genética", "Expressão da informação genética" },
//             correctIndex = 3,
//             questionNumber = 16,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_016",
//             topic = "nucleicAcids",
//             subtopic = "rna_function",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "gene_expression", "protein_synthesis" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Considere em conjunto RNAm, RNAt e RNAr: um leva a mensagem, outro entrega aminoácidos e outro integra o ribossomo. Apesar de existirem RNAs com outras atividades, esses três convergem para um processo central. Escolha a função geral que melhor reúne a atuação desses tipos de RNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 017
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que é desnaturação do DNA?",
//             answers = new string[] {
//                 "Quebra da dupla hélice.",
//                 "Separação das fitas.",
//                 "Mudança na seqüência de bases.",
//                 "Todas as alternativas acima."
//             },
//             correctIndex = 1,
//             questionNumber = 17,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_017",
//             topic = "nucleicAcids",
//             subtopic = "dna_denaturation",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "strand_separation", "hydrogen_bonds" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Na desnaturação, o esqueleto covalente de cada fita permanece, mas as interações entre bases complementares são desfeitas. Imagine o efeito do aquecimento sobre uma dupla hélice: as duas cadeias se afastam sem que o DNA seja necessariamente degradado em nucleotídeos. Procure a descrição dessa perda de estrutura secundária.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 018
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que é renaturação do DNA?",
//             answers = new string[] {
//                 "Formação de novas fitas.",
//                 "Reassociação das fitas.",
//                 "Replicação do DNA.",
//                 "Transcrição do DNA."
//             },
//             correctIndex = 1,
//             questionNumber = 18,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_018",
//             topic = "nucleicAcids",
//             subtopic = "dna_renaturation",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "strand_reassociation", "complementary_base_pairing" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Renaturação é o processo inverso da separação das fitas. Para que ocorra, cadeias com sequências complementares precisam reencontrar-se em condições adequadas e restabelecer o pareamento entre bases. Pense no que acontece durante um resfriamento controlado após a desnaturação, e não em síntese de uma molécula nova.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 019
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que causa a desnaturação do DNA?",
//             answers = new string[] {
//                 "Altas temperaturas",
//                 "Extremos de pH",
//                 "Ação de enzimas",
//                 "Todas as alternativas acima"
//             },
//             correctIndex = 3,
//             questionNumber = 19,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_019",
//             topic = "nucleicAcids",
//             subtopic = "dna_denaturation",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "temperature_effects", "ph_effects", "strand_separation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A dupla hélice depende de interações não covalentes sensíveis às condições do meio. Temperatura elevada ou valores extremos de pH podem desfazer o pareamento das bases sem cortar imediatamente o esqueleto açúcar-fosfato. Procure a alternativa que altera essas interações, e não uma enzima envolvida na síntese proteica.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 020
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Para que serve a medida de absorvância a 260nm?",
//             answers = new string[] {
//                 "Medida da concentração de proteínas.",
//                 "Medida da concentração de ácidos nucléicos.",
//                 "Medida da temperatura de fusão do DNA.",
//                 "Medida da viscosidade de uma solução."
//             },
//             correctIndex = 1,
//             questionNumber = 20,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_020",
//             topic = "nucleicAcids",
//             subtopic = "nucleic_acid_quantification",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "uv_absorbance", "a260" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "As bases nitrogenadas absorvem radiação ultravioleta intensamente próximo de 260 nm. Por isso, a leitura de A260 é usada para estimar a quantidade de ácidos nucleicos em solução e também pode acompanhar desnaturação. Lembre, porém, que essa medida isolada não identifica especificamente DNA em presença de RNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 021
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que é a regra de Chargaff?",
//             answers = new string[] {
//                 "A = T e G = C",
//                 "A = G e T = C",
//                 "A = C e G = T",
//                 "Não há regra de Chargaff."
//             },
//             correctIndex = 0,
//             questionNumber = 21,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_021",
//             topic = "nucleicAcids",
//             subtopic = "chargaff_rule",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "base_pairing", "adenine_thymine", "guanine_cytosine" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Chargaff comparou quantitativamente as bases do DNA e observou relações de igualdade associadas ao pareamento. Em DNA de dupla fita, a quantidade de adenina acompanha a de timina, enquanto guanina acompanha citosina. Procure a alternativa que expressa essas proporções, não o fluxo DNA–RNA–proteína.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 022
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Em que tipo de molécula a regra de Chargaff se aplica?",
//             answers = new string[] {
//                 "DNA",
//                 "RNA",
//                 "Proteínas",
//                 "Carboidratos"
//             },
//             correctIndex = 0,
//             questionNumber = 22,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_022",
//             topic = "nucleicAcids",
//             subtopic = "chargaff_rule",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "dna_structure", "base_composition" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A regra de Chargaff depende da presença de uma fita complementar: cada adenina de uma cadeia corresponde a uma timina na outra, e o mesmo ocorre entre guanina e citosina. Pergunte em qual estrutura essas igualdades globais são necessariamente esperadas, evitando generalizá-las a moléculas de fita simples.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 023
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que é o 'fluxo da informação genética'?",
//             answers = new string[] {
//                 "O movimento de íons através da membrana.",
//                 "A replicação do DNA.",
//                 "O processo de conversão da informação genética em proteínas.",
//                 "O transporte de proteínas para o exterior da célula."
//             },
//             correctIndex = 2,
//             questionNumber = 23,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_023",
//             topic = "nucleicAcids",
//             subtopic = "central_dogma",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "dna", "rna", "protein_synthesis", "gene_expression" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Organize os três processos que conectam material genético e proteína. A replicação copia DNA; a transcrição utiliza DNA para produzir RNA; e a tradução utiliza a mensagem do RNA para formar uma cadeia polipeptídica. Escolha a alternativa que apresenta o sentido geral dessa transferência de informação.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 024
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual tipo de RNA transporta aminoácidos para os ribossomos?",
//             answers = new string[] { "tRNA", "rRNA", "mRNA", "snRNA" },
//             correctIndex = 0,
//             questionNumber = 24,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_024",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "trna", "amino_acid_transport", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Durante a tradução, cada aminoácido precisa chegar ao ribossomo associado a uma molécula que possui anticódon. Esse anticódon reconhece um códon do RNAm, posicionando o aminoácido correto na cadeia nascente. Identifique o tipo de RNA cuja função combina transporte de aminoácido e reconhecimento da mensagem.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 025
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual tipo de RNA faz parte da estrutura dos ribossomos?",
//             answers = new string[] { "tRNA", "rRNA", "mRNA", "snRNA" },
//             correctIndex = 1,
//             questionNumber = 25,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_025",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "rrna", "ribosomes", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O ribossomo não é composto apenas de proteínas. Grande parte de sua estrutura e de sua atividade catalítica depende de um tipo de RNA associado às subunidades ribossômicas. Diferencie-o do RNAm, que carrega códons, e do RNAt, que entrega aminoácidos durante a tradução.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 026
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a principal diferença química entre DNA e RNA?",
//             answers = new string[] { "Açúcar", "Bases nitrogenadas", "Grupamento fosfato", "Sequência de bases" },
//             correctIndex = 0,
//             questionNumber = 26,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_026",
//             topic = "nucleicAcids",
//             subtopic = "dna_rna_differences",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "ribose", "deoxyribose", "sugars" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Observe o carbono 2' das pentoses. A ribose do RNA possui uma hidroxila nessa posição, enquanto a pentose do DNA apresenta apenas hidrogênio, justificando o prefixo “desoxi”. Procure a alternativa que compara essa diferença química específica; número de fitas e função celular não definem a diferença pedida.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 027
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a principal diferença na composição de bases entre DNA e RNA?",
//             answers = new string[] { "Timina vs. Uracila", "Adenina vs. Guanina", "Citosina vs. Guanina", "Ribose vs. Desoxirribose" },
//             correctIndex = 0,
//             questionNumber = 27,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_027",
//             topic = "nucleicAcids",
//             subtopic = "dna_rna_differences",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "thymine", "uracil", "nitrogenous_bases" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Três bases principais são comuns ao DNA e ao RNA. A distinção de composição ocorre porque o DNA utiliza timina, enquanto o RNA normalmente utiliza uracila na posição correspondente. Escolha a alternativa que expressa essa substituição, sem trocar adenina, guanina ou citosina entre as moléculas.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 028
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "O que é um códon?",
//             answers = new string[] {
//                 "Uma seqüência de três bases no tRNA.",
//                 "Uma seqüência de três bases no mRNA.",
//                 "Uma seqüência de três bases no rRNA.",
//                 "Uma seqüência de três bases no DNA."
//             },
//             correctIndex = 1,
//             questionNumber = 28,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_028",
//             topic = "nucleicAcids",
//             subtopic = "genetic_code",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Remember,
//             conceptTags = new List<string> { "codon", "mrna", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Na tradução, o ribossomo lê o RNAm em grupos consecutivos de três nucleotídeos. Cada grupo especifica um aminoácido ou um sinal de início ou término. Procure a alternativa que define essa unidade da mensagem genética, distinguindo-a do anticódon complementar presente no RNAt.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 029
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a função do anticódon no tRNA?",
//             answers = new string[] {
//                 "Ligar-se ao ribossomo.",
//                 "Ligar-se ao mRNA.",
//                 "Ligar-se a proteínas.",
//                 "Ligar-se ao DNA."
//             },
//             correctIndex = 1,
//             questionNumber = 29,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_029",
//             topic = "nucleicAcids",
//             subtopic = "genetic_code",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "anticodon", "trna", "mrna", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O anticódon é uma trinca do RNAt, não do RNAm. Sua sequência complementar permite reconhecer um códon da mensagem e posicionar o RNAt correspondente no ribossomo. Assim, relacione pareamento de bases com a entrega do aminoácido adequado, em vez de atribuir ao anticódon a síntese do RNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 030
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Qual a função principal dos rRNAs?",
//             answers = new string[] {
//                 "Transporte de aminoácidos.",
//                 "Síntese de proteínas.",
//                 "Fazem parte da estrutura dos ribossomos.",
//                 "Catalisam reações."
//             },
//             correctIndex = 2,
//             questionNumber = 30,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_030",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "rrna", "ribosomes", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O RNAr forma, com proteínas, as subunidades do ribossomo e participa diretamente da formação das ligações peptídicas. Sua função vai além de apenas transportar uma mensagem ou um aminoácido. Procure a alternativa que combine papel estrutural e participação catalítica na maquinaria de síntese proteica.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 031
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Explique qual alternativa reúne, sem incluir componentes estranhos, as três partes estruturais de um nucleotídeo.",
//             answers = new string[] { "Açúcar, base, fosfato", "Açúcar, base, aminoácido", "Base, aminoácido, fosfato", "Açúcar, lipídeo, base" },
//             correctIndex = 0,
//             questionNumber = 31,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_031",
//             topic = "nucleicAcids",
//             subtopic = "nucleotide_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "pentose", "nitrogenous_base", "phosphate_group" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Desmonte conceitualmente um nucleotídeo em três partes: uma pentose, uma base nitrogenada e pelo menos um grupo fosfato. Em seguida, verifique cada alternativa à procura desse conjunto completo. Componentes típicos de proteínas ou lipídios, como aminoácidos e ácidos graxos, são elementos estranhos à estrutura solicitada.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 032
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Diferencie nucleosídeo de nucleotídeo escolhendo a composição que representa apenas um nucleosídeo.",
//             answers = new string[] { "Açúcar + base + fosfato", "Açúcar + base", "Base + fosfato", "Açúcar + aminoácido" },
//             correctIndex = 1,
//             questionNumber = 32,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_032",
//             topic = "nucleicAcids",
//             subtopic = "nucleosides",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "pentose", "nitrogenous_base" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Use a presença do fosfato como critério decisivo. Pentose ligada a uma base forma um nucleosídeo; quando um ou mais fosfatos são adicionados, forma-se um nucleotídeo. Escolha a composição anterior à fosforilação, eliminando alternativas que introduzem aminoácidos ou omitem um dos dois componentes essenciais.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 033
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Associe a denominação ribonucleotídeo ao tipo de pentose presente em sua estrutura.",
//             answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
//             correctIndex = 1,
//             questionNumber = 33,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_033",
//             topic = "nucleicAcids",
//             subtopic = "rna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "ribonucleotides", "ribose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A palavra “ribonucleotídeo” deriva da pentose presente no monômero do RNA. Observe que o açúcar procurado conserva uma hidroxila no carbono 2', diferentemente da versão desoxigenada do DNA. Glicose e frutose são carboidratos celulares importantes, mas não dão nome aos ribonucleotídeos.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 034
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Associe a denominação desoxirribonucleotídeo ao tipo de pentose presente em sua estrutura.",
//             answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
//             correctIndex = 0,
//             questionNumber = 34,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_034",
//             topic = "nucleicAcids",
//             subtopic = "dna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "deoxyribonucleotides", "deoxyribose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O prefixo “desoxi” indica a ausência da hidroxila no carbono 2' que existe na ribose. Portanto, associe “desoxirribonucleotídeo” à pentose modificada que caracteriza o DNA. Não use como critério a forma de dupla hélice, pois a pergunta trata da composição química do monômero.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 035
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Reconheça a relação entre monômero e polímero escolhendo a unidade repetitiva que forma os ácidos nucleicos.",
//             answers = new string[] {
//                 "Aminoácidos",
//                 "Nucleotídeos",
//                 "Monossacarídeos",
//                 "Lipídios"
//             },
//             correctIndex = 1,
//             questionNumber = 35,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_035",
//             topic = "nucleicAcids",
//             subtopic = "nucleic_acid_polymers",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "nucleotides", "macromolecules" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Ácidos nucleicos são polímeros porque resultam da repetição de unidades menores ligadas pelo esqueleto açúcar-fosfato. Identifique qual classe de monômero já contém pentose, base nitrogenada e fosfato. Aminoácidos formam proteínas; monossacarídeos formam muitos polissacarídeos; ácidos graxos participam de diversos lipídios celulares.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 036
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Classifique as duas macromoléculas que constituem os principais tipos de ácidos nucleicos dos seres vivos.",
//             answers = new string[] {
//                 "DNA e RNA",
//                 "DNA e ATP",
//                 "RNA e lipídios",
//                 "DNA e proteínas"
//             },
//             correctIndex = 0,
//             questionNumber = 36,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_036",
//             topic = "nucleicAcids",
//             subtopic = "nucleic_acid_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "dna", "rna" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A expressão “ácidos nucleicos” designa duas macromoléculas relacionadas, mas quimicamente distintas pela pentose e por uma das bases. Uma armazena a informação hereditária com maior estabilidade; a outra assume diversos papéis na expressão gênica. Selecione o par que corresponde a essas duas categorias moleculares.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 037
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Explique a origem do termo ribonucleico identificando o açúcar característico do RNA.",
//             answers = new string[] {
//                 "Desoxirribose",
//                 "Glicose",
//                 "Ribose",
//                 "Maltose"
//             },
//             correctIndex = 2,
//             questionNumber = 37,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_037",
//             topic = "nucleicAcids",
//             subtopic = "rna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "ribose", "pentose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O nome “ácido ribonucleico” já aponta para o açúcar presente em seus nucleotídeos. Relacione o radical “ribo” à pentose que possui hidroxila no carbono 2'. Não escolha desoxirribose, pois sua perda de oxigênio nessa posição é justamente a característica que nomeia o DNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 038
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Interprete a complementaridade das bases e indique com qual base a adenina se emparelha no DNA.",
//             answers = new string[] {
//                 "Guanina",
//                 "Citosina",
//                 "Timina",
//                 "Uracila"
//             },
//             correctIndex = 2,
//             questionNumber = 38,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_038",
//             topic = "nucleicAcids",
//             subtopic = "base_pairing",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "adenine_thymine", "dna_structure" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "No DNA, o pareamento mantém uma purina associada a uma pirimidina com geometria constante. A adenina forma duas ligações de hidrogênio com sua parceira canônica. Lembre que uracila ocupa essa função no RNA, enquanto no DNA a base correspondente possui um grupo metil adicional.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 039
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Relacione o modelo estrutural da dupla hélice aos pesquisadores que o propuseram.",
//             answers = new string[] {
//                 "Darwin e Lamarck",
//                 "Watson e Crick",
//                 "Pasteur e Koch",
//                 "Franklin e Mendel"
//             },
//             correctIndex = 1,
//             questionNumber = 39,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_039",
//             topic = "nucleicAcids",
//             subtopic = "dna_double_helix",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "watson_crick", "dna_structure_history" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Separe a obtenção de evidências da proposição do modelo. Rosalind Franklin produziu dados decisivos de difração de raios X, e Chargaff descreveu relações entre bases; em 1953, outros dois pesquisadores integraram essas evidências em um modelo de dupla hélice. Identifique os autores desse modelo.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 040
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Diferencie os tipos de RNA pela função e identifique aquele que leva a informação genética aos ribossomos.",
//             answers = new string[] {
//                 "RNA ribossômico (rRNA)",
//                 "RNA transportador (tRNA)",
//                 "RNA mensageiro (mRNA)",
//                 "RNA nuclear"
//             },
//             correctIndex = 2,
//             questionNumber = 40,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 1,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_040",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Understand,
//             conceptTags = new List<string> { "mrna", "gene_expression", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Procure o RNA cuja sequência contém os códons copiados do DNA durante a transcrição. Essa molécula deixa o núcleo e apresenta a mensagem ao ribossomo. Não a confunda com o RNAt, que reconhece códons por anticódons, nem com o RNAr, componente estrutural e catalítico do ribossomo.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 041
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma molécula de RNA foi hidrolisada e uma base que normalmente não integra o DNA foi detectada. Qual base é compatível com o resultado?",
//             answers = new string[] {
//                 "Guanina",
//                 "Uracila",
//                 "Adenina",
//                 "Citosina"
//             },
//             correctIndex = 1,
//             questionNumber = 41,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_041",
//             topic = "nucleicAcids",
//             subtopic = "nitrogenous_bases",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "uracil", "rna_bases", "thymine" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A hidrólise revelou uma base característica do RNA. Compare a composição dos dois ácidos nucleicos: adenina, guanina e citosina são compartilhadas, mas o RNA emprega uma pirimidina no lugar da timina. A base procurada deve, portanto, ser compatível com ribose e não integrar normalmente o DNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 042
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Em um experimento, uma fita de DNA serviu de molde para produzir uma molécula complementar de RNA. Como se chama esse processo?",
//             answers = new string[] {
//                 "Tradução",
//                 "Transcrição",
//                 "Replicação",
//                 "Mutação"
//             },
//             correctIndex = 1,
//             questionNumber = 42,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_042",
//             topic = "nucleicAcids",
//             subtopic = "transcription",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "rna_synthesis", "dna_template", "gene_expression" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Identifique primeiro o produto e o molde: uma cadeia de RNA está sendo construída a partir de uma fita de DNA. Esse fluxo corresponde a uma etapa da expressão gênica anterior à síntese proteica. Não escolha replicação, que produz DNA, nem tradução, que utiliza RNAm para produzir polipeptídeo.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 043
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma célula precisa acoplar energia à contração muscular. Qual papel do ATP explica sua utilização imediata nesse processo?",
//             answers = new string[] {
//                 "Formar a bicamada lipídica",
//                 "Ser uma fonte de energia celular",
//                 "Carregar oxigênio no sangue",
//                 "Transportar aminoácidos"
//             },
//             correctIndex = 1,
//             questionNumber = 43,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_043",
//             topic = "nucleicAcids",
//             subtopic = "nucleotide_functions",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "atp", "cellular_energy" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O ATP transfere energia quando sua hidrólise é acoplada a uma mudança conformacional das proteínas contráteis. Pense nele como intermediário entre reações que liberam energia e processos que a consomem. A alternativa correta deve explicar uso energético imediato, não composição de membranas, transporte de oxigênio ou entrega de aminoácidos.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 044
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Durante transporte ativo, a célula consome ATP. Qual propriedade desse nucleotídeo permite sustentar o trabalho celular?",
//             answers = new string[] {
//                 "Formar a bicamada lipídica",
//                 "Ser uma fonte de energia celular",
//                 "Carregar oxigênio no sangue",
//                 "Transportar aminoácidos"
//             },
//             correctIndex = 1,
//             questionNumber = 44,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_044",
//             topic = "nucleicAcids",
//             subtopic = "nucleotide_functions",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "atp", "cellular_energy" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "No transporte ativo, a hidrólise de ATP pode alterar a conformação de uma proteína de membrana e impulsionar solutos contra seus gradientes. A propriedade relevante é sua capacidade de acoplar uma reação favorável ao trabalho celular. Não atribua ao ATP funções estruturais de lipídios ou proteínas transportadoras.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 045
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Durante a replicação, nucleotídeos complementares precisam ser adicionados à nova fita. Qual enzima realiza essa etapa?",
//             answers = new string[] {
//                 "DNA polimerase",
//                 "RNA polimerase",
//                 "Ligase",
//                 "Transcriptase reversa"
//             },
//             correctIndex = 0,
//             questionNumber = 45,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_045",
//             topic = "nucleicAcids",
//             subtopic = "dna_replication",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "dna_polymerase", "nucleotide_polymerization" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A enzima procurada lê a fita molde e adiciona desoxirribonucleotídeos complementares à extremidade 3' da nova cadeia. A ligase apenas une fragmentos já sintetizados, enquanto a RNA polimerase produz RNA. Relacione, portanto, o tipo de produto formado ao nome e à função da polimerase adequada.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 046
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Ao analisar um polímero com pentose, fosfato e bases nitrogenadas repetidos, qual unidade monomérica deve ser identificada?",
//             answers = new string[] {
//                 "Aminoácidos",
//                 "Nucleotídeos",
//                 "Monossacarídeos",
//                 "Lipídeos"
//             },
//             correctIndex = 1,
//             questionNumber = 46,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_046",
//             topic = "nucleicAcids",
//             subtopic = "nucleic_acid_polymers",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "nucleotides", "macromolecules" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O polímero descrito possui um esqueleto repetitivo de pentose e fosfato, com uma base ligada a cada açúcar. A unidade que reúne esses três componentes é o monômero dos ácidos nucleicos. Compare com proteínas, polissacarídeos e lipídios, cujos monômeros apresentam composições químicas diferentes.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 047
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Um pesquisador isolou uma molécula com pentose, base nitrogenada e fosfato. Como essa molécula deve ser classificada?",
//             answers = new string[] {
//                 "Pentose + fosfato + base nitrogenada",
//                 "Hexose + lipídio + aminoácido",
//                 "Glicose + fosfato + proteína",
//                 "Glicerol + base nitrogenada + ácido graxo"
//             },
//             correctIndex = 0,
//             questionNumber = 47,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_047",
//             topic = "nucleicAcids",
//             subtopic = "nucleotide_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "pentose", "nitrogenous_base", "phosphate_group" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Classifique a molécula pela soma de seus componentes. Pentose ligada somente à base seria um nucleosídeo; a presença adicional de fosfato caracteriza um nucleotídeo. Entre as alternativas, procure a composição que preserve exatamente esses três grupos e descarte combinações próprias de proteínas, carboidratos ou lipídios.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 048
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma amostra contém ribose e uma base que substitui a timina. Qual base deve aparecer na análise?",
//             answers = new string[] {
//                 "Timina",
//                 "Citosina",
//                 "Uracila",
//                 "Adenina"
//             },
//             correctIndex = 2,
//             questionNumber = 48,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_048",
//             topic = "nucleicAcids",
//             subtopic = "nitrogenous_bases",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "uracil", "rna_bases" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A ribose indica que a amostra provavelmente contém RNA. Nesse ácido nucleico, uma pirimidina substitui a timina utilizada pelo DNA. Adenina e citosina aparecem em ambos e, portanto, não explicam a substituição mencionada. Escolha a base característica que completa coerentemente as duas pistas químicas.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 049
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma amostra de DNA contém uma pirimidina ausente do RNA celular típico. Qual base deve ser detectada?",
//             answers = new string[] {
//                 "Uracila",
//                 "Adenina",
//                 "Timina",
//                 "Guanina"
//             },
//             correctIndex = 2,
//             questionNumber = 49,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_049",
//             topic = "nucleicAcids",
//             subtopic = "nitrogenous_bases",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "thymine", "dna_bases" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A amostra foi identificada como DNA e a pergunta pede uma pirimidina normalmente ausente do RNA. Compare o par diferencial: DNA utiliza uma base metilada onde o RNA utiliza uracila. Adenina e guanina são purinas, e ambas aparecem nos dois tipos de ácido nucleico.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 050
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma imagem mostra duas fitas antiparalelas enroladas e unidas por bases complementares. Qual descrição estrutural se aplica?",
//             answers = new string[] {
//                 "Hélice simples",
//                 "Tripla hélice",
//                 "Dupla hélice",
//                 "Cadeia linear"
//             },
//             correctIndex = 2,
//             questionNumber = 50,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_050",
//             topic = "nucleicAcids",
//             subtopic = "dna_double_helix",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "dna_structure" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Use simultaneamente três pistas visuais: existem duas cadeias, elas percorrem sentidos opostos e se enrolam em torno de um mesmo eixo; bases complementares mantêm as cadeias associadas. Essa organização corresponde ao modelo estrutural clássico do DNA, não a uma hélice simples, tripla ou a duas cadeias lineares.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 051
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Após analisar um ácido nucleico estável, encontrou-se uma pentose sem hidroxila no carbono 2'. Qual açúcar está presente?",
//             answers = new string[] {
//                 "Ribose",
//                 "Desoxirribose",
//                 "Glicose",
//                 "Frutose"
//             },
//             correctIndex = 1,
//             questionNumber = 51,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_051",
//             topic = "nucleicAcids",
//             subtopic = "dna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "deoxyribose", "pentose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A ausência de hidroxila no carbono 2' distingue a pentose do DNA da ribose do RNA e contribui para maior estabilidade química. O termo “desoxi” registra exatamente essa falta de oxigênio. Identifique o açúcar por essa característica molecular, sem recorrer à forma geral do ácido nucleico.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 052
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma fita molde de DNA apresenta adenina em determinada posição. Qual base deve ser incorporada na nova fita complementar?",
//             answers = new string[] {
//                 "Guanina",
//                 "Citosina",
//                 "Uracila",
//                 "Timina"
//             },
//             correctIndex = 3,
//             questionNumber = 52,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_052",
//             topic = "nucleicAcids",
//             subtopic = "base_pairing",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "adenine_thymine", "dna_structure" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Como o produto é uma nova fita de DNA, aplique as regras de pareamento próprias do DNA. Adenina da fita molde orienta a incorporação de sua pirimidina complementar. Uracila seria usada se o produto fosse RNA; aqui, a replicação exige a base equivalente encontrada no DNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 053
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma molécula recém-transcrita segue do núcleo ao ribossomo levando códons. Qual função descreve esse RNA?",
//             answers = new string[] {
//                 "Formar a estrutura dos ribossomos",
//                 "Transportar aminoácidos",
//                 "Levar a informação genética do DNA até os ribossomos",
//                 "Catalisar reações químicas"
//             },
//             correctIndex = 2,
//             questionNumber = 53,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_053",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "mrna", "gene_expression", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A molécula contém códons e leva uma cópia da informação do DNA ao ribossomo. Essas pistas descrevem a função mensageira. Diferencie-a do RNAt, que carrega aminoácidos e possui anticódon, e do RNAr, que forma a estrutura catalítica onde a tradução acontece.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 054
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Durante a tradução, uma molécula reconhece códons e entrega aminoácidos ao ribossomo. Qual é sua função?",
//             answers = new string[] {
//                 "Levar aminoácidos até os ribossomos durante a síntese proteica",
//                 "Carregar energia química",
//                 "Armazenar informação genética",
//                 "Catalisar reações metabólicas"
//             },
//             correctIndex = 0,
//             questionNumber = 54,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_054",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "trna", "amino_acid_transport", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A molécula descrita deve combinar duas propriedades: carregar um aminoácido específico e possuir um anticódon capaz de reconhecer o RNAm. Essa é a função transportadora durante a tradução. Não confunda sua tarefa com levar a mensagem do DNA ou compor estruturalmente o ribossomo.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 055
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma célula precisa conservar instruções hereditárias por muitas divisões. Qual função molecular deve ser atribuída ao DNA?",
//             answers = new string[] {
//                 "Atuar como catalisador enzimático",
//                 "Fornecer energia imediata",
//                 "Armazenar e transmitir a informação genética",
//                 "Transportar oxigênio"
//             },
//             correctIndex = 2,
//             questionNumber = 55,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_055",
//             topic = "nucleicAcids",
//             subtopic = "dna_function",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "genetic_information", "inheritance" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A informação hereditária precisa permanecer estável, ser duplicada antes da divisão e chegar às células descendentes. Relacione essas exigências à sequência de bases do DNA. A alternativa adequada deve incluir armazenamento e transmissão, não energia imediata, catálise metabólica ou transporte de outras moléculas.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 056
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma enzima degrada um ácido nucleico em suas unidades repetitivas. Quais unidades serão obtidas?",
//             answers = new string[] {
//                 "Aminoácidos",
//                 "Monossacarídeos",
//                 "Nucleotídeos",
//                 "Ácidos graxos"
//             },
//             correctIndex = 2,
//             questionNumber = 56,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_056",
//             topic = "nucleicAcids",
//             subtopic = "nucleic_acid_polymers",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "nucleotides", "macromolecules" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Para reconhecer as unidades produzidas, identifique primeiro o monômero do polímero. Cada segmento repetitivo de um ácido nucleico reúne pentose, fosfato e base nitrogenada. A degradação completa das ligações que unem esses segmentos libera essa classe de moléculas, e não monômeros de proteínas, carboidratos ou lipídios.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 057
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma molécula possui base nitrogenada ligada a uma pentose e a fosfato. Qual alternativa descreve seus componentes?",
//             answers = new string[] {
//                 "Aminoácido, fosfato e água",
//                 "Açúcar, base nitrogenada e fosfato",
//                 "Glicerol, ácido graxo e base nitrogenada",
//                 "Açúcar, lipídio e proteína"
//             },
//             correctIndex = 1,
//             questionNumber = 57,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_057",
//             topic = "nucleicAcids",
//             subtopic = "nucleotide_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "pentose", "nitrogenous_base", "phosphate_group" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A presença conjunta de pentose, base nitrogenada e fosfato caracteriza um nucleotídeo completo. Verifique qual alternativa lista precisamente essas três partes. Açúcar e base sem fosfato formariam apenas um nucleosídeo; glicerol e ácidos graxos, por sua vez, pertencem à organização dos lipídios.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 058
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma análise química detecta pentose sem oxigênio no carbono 2'. A qual açúcar do DNA esse resultado corresponde?",
//             answers = new string[] {
//                 "Glicose",
//                 "Ribose",
//                 "Desoxirribose",
//                 "Galactose"
//             },
//             correctIndex = 2,
//             questionNumber = 58,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_058",
//             topic = "nucleicAcids",
//             subtopic = "dna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "deoxyribose", "pentose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Examine especificamente o carbono 2' da pentose: se nele há hidrogênio em vez de hidroxila, falta um oxigênio em relação à ribose. Essa modificação origina o açúcar característico do DNA e explica seu nome. Glicose e galactose não são pentoses dos ácidos nucleicos.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 059
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma análise química detecta pentose com hidroxila no carbono 2'. A qual açúcar do RNA esse resultado corresponde?",
//             answers = new string[] {
//                 "Glicose",
//                 "Ribose",
//                 "Desoxirribose",
//                 "Maltose"
//             },
//             correctIndex = 1,
//             questionNumber = 59,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_059",
//             topic = "nucleicAcids",
//             subtopic = "rna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "ribose", "pentose" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A hidroxila no carbono 2' é a marca química da pentose do RNA. Compare-a com a desoxirribose do DNA, que apresenta hidrogênio nessa posição e, portanto, um oxigênio a menos. Selecione o açúcar que conserva o grupo 2'-OH, não outros carboidratos celulares.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 060
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma dupla fita de DNA apresenta 30% de adenina. Aplicando a regra de Chargaff, qual porcentagem de timina deve ser esperada?",
//             answers = new string[] {
//                 "20%",
//                 "30%",
//                 "40%",
//                 "70%"
//             },
//             correctIndex = 1,
//             questionNumber = 60,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_060",
//             topic = "nucleicAcids",
//             subtopic = "chargaff_rule",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Apply,
//             conceptTags = new List<string> { "base_pairing", "adenine_thymine", "guanine_cytosine" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Em DNA de dupla fita, cada adenina está pareada com uma timina; por isso, suas porcentagens totais são iguais. Aplique diretamente A = T, sem subtrair inicialmente do total. A soma restante será dividida entre guanina e citosina, mas esse cálculo não é necessário para responder.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 061
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Em uma célula, a molécula X leva códons ao ribossomo, enquanto a molécula Y transporta aminoácidos. Ao comparar X e Y, qual função pertence especificamente a X?",
//             answers = new string[] {
//                 "Transportar aminoácidos",
//                 "Atuar como catalisador enzimático",
//                 "Levar a informação do DNA até os ribossomos",
//                 "Formar a dupla hélice do DNA"
//             },
//             correctIndex = 2,
//             questionNumber = 61,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_061",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string> { "mrna", "gene_expression", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A molécula X apresenta códons e segue até o ribossomo, características do RNAm. Já a molécula Y transporta aminoácidos, função do RNAt. Ao comparar as alternativas, selecione somente a atividade exclusiva de X e não uma propriedade geral do RNA ou da tradução como um todo.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 062
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Durante a tradução, a molécula X apresenta anticódon, enquanto a molécula Y contém códons. Ao comparar X e Y, qual função pertence especificamente a X?",
//             answers = new string[] {
//                 "Levar aminoácidos até o ribossomo durante a síntese de proteínas",
//                 "Duplicar o DNA",
//                 "Formar a membrana celular",
//                 "Produzir energia na respiração"
//             },
//             correctIndex = 0,
//             questionNumber = 62,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_062",
//             topic = "nucleicAcids",
//             subtopic = "rna_types",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string> { "trna", "amino_acid_transport", "translation" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O anticódon identifica X como RNAt. Essa trinca reconhece um códon do RNAm no ribossomo, enquanto a outra extremidade do RNAt carrega um aminoácido específico. Relacione essas duas regiões para determinar a função de X, sem atribuir-lhe duplicação do DNA ou produção de energia.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 063
//         new Question {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Um texto atribui a Franklin dados de difração, a Chargaff relações entre bases e a dois pesquisadores a proposição do modelo da dupla hélice em 1953. Quem corresponde à última contribuição?",
//             answers = new string[] {
//                 "Darwin e Mendel",
//                 "Watson e Crick",
//                 "Franklin e Chargaff",
//                 "Pauling e Wöhler"
//             },
//             correctIndex = 1,
//             questionNumber = 63,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_063",
//             topic = "nucleicAcids",
//             subtopic = "dna_double_helix",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string> { "watson_crick", "dna_structure_history" },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Organize as contribuições historicamente: Franklin forneceu padrões de difração que revelavam características helicoidais; Chargaff estabeleceu relações quantitativas entre bases. O modelo publicado em 1953 integrou essas evidências e propôs fitas antiparalelas com pareamento complementar. Procure a dupla de pesquisadores associada especificamente a essa síntese estrutural.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 064
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma amostra de DNA dupla fita contém 18% de guanina. Qual porcentagem de adenina é esperada?",
//             answers = new string[]
//             {
//                 "18%",
//                 "36%",
//                 "32%",
//                 "64%"
//             },
//             correctIndex = 2,
//             questionNumber = 64,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_064",
//             topic = "nucleicAcids",
//             subtopic = "base_pairing",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "chargaff",
//                 "percentage_calculation"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Em DNA de dupla fita, %G = %C. Se a guanina representa 18%, a citosina também representa 18%; juntas, ocupam 36% do total. O restante corresponde a adenina e timina em proporções iguais. Execute essas duas etapas para encontrar a porcentagem de adenina.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 065
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Compare duas amostras de DNA: X tem 40% de GC e Y tem 60% de GC. Qual tende a exigir maior temperatura para desnaturar?",
//             answers = new string[]
//             {
//                 "Y, por ter mais pares G-C",
//                 "X, por ter mais pares A-T",
//                 "Ambas, por terem o mesmo comprimento",
//                 "Não é possível relacionar composição e estabilidade"
//             },
//             correctIndex = 0,
//             questionNumber = 65,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_065",
//             topic = "nucleicAcids",
//             subtopic = "dna_stability",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "gc_content",
//                 "melting_temperature"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Pares G–C estabelecem três ligações de hidrogênio, enquanto pares A–T estabelecem duas. Mantidos semelhantes os demais fatores, maior proporção de GC exige mais energia térmica para separar as fitas. Compare as amostras por essa relação entre composição de bases e estabilidade, não apenas pelo comprimento.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 066
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma molécula apresenta uracila, ribose e uma única cadeia. Qual classificação é mais consistente?",
//             answers = new string[]
//             {
//                 "DNA",
//                 "Proteína",
//                 "Polissacarídeo",
//                 "RNA"
//             },
//             correctIndex = 3,
//             questionNumber = 66,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_066",
//             topic = "nucleicAcids",
//             subtopic = "dna_rna_comparison",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "uracil",
//                 "ribose",
//                 "single_strand"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Use as três características em conjunto. Ribose e uracila são marcadores químicos típicos do RNA, e uma única cadeia é sua organização mais frequente. DNA normalmente apresenta desoxirribose, timina e dupla fita. A classificação deve explicar simultaneamente todos os dados, não apenas um deles.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 067
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Durante a replicação, uma fita é sintetizada continuamente e outra em fragmentos. Qual característica explica essa diferença?",
//             answers = new string[]
//             {
//                 "As bases possuem cargas positivas",
//                 "As fitas molde são antiparalelas",
//                 "A ligase sintetiza ambas as fitas",
//                 "O DNA contém ribose"
//             },
//             correctIndex = 1,
//             questionNumber = 67,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_067",
//             topic = "nucleicAcids",
//             subtopic = "dna_replication",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "antiparallelism",
//                 "replication_fork"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "A DNA polimerase só alonga uma cadeia no sentido 5'→3'. Como as duas fitas molde são antiparalelas, uma pode ser copiada acompanhando continuamente a abertura da forquilha, enquanto a outra precisa ser sintetizada em fragmentos. Procure a alternativa que conecta orientação oposta e síntese descontínua.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 068
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Após aquecimento, a absorvância de uma solução de DNA a 260 nm aumenta. Qual interpretação é mais adequada?",
//             answers = new string[]
//             {
//                 "O DNA foi traduzido",
//                 "As fitas se separaram e as bases ficaram mais expostas",
//                 "Os nucleotídeos viraram aminoácidos",
//                 "O fosfato foi removido"
//             },
//             correctIndex = 1,
//             questionNumber = 68,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_068",
//             topic = "nucleicAcids",
//             subtopic = "dna_denaturation",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "hyperchromic_effect",
//                 "absorbance"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O aumento de A260 após aquecimento é o efeito hipercrômico. Quando as fitas se separam, o empilhamento das bases diminui e elas absorvem mais radiação ultravioleta. Isso indica perda da dupla hélice, não conversão dos nucleotídeos em aminoácidos nem remoção obrigatória do esqueleto fosfodiéster.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 069
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma mutação altera o anticódon de um RNAt sem mudar o aminoácido ligado a ele. Qual consequência direta é mais provável?",
//             answers = new string[]
//             {
//                 "Interrupção da transcrição do DNA",
//                 "Duplicação do cromossomo",
//                 "Conversão do RNAt em RNAr",
//                 "Reconhecimento de um códon diferente"
//             },
//             correctIndex = 3,
//             questionNumber = 69,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_069",
//             topic = "nucleicAcids",
//             subtopic = "translation",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "anticodon",
//                 "codon_recognition"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "O anticódon determina qual códon do RNAm o RNAt consegue reconhecer. Se essa trinca muda, mas o aminoácido ligado permanece o mesmo, o RNAt pode entregar esse aminoácido diante de um códon diferente. Analise a consequência no pareamento durante a tradução, não na replicação ou transcrição.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 070
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Um nucleotídeo perde todos os seus grupos fosfato. Em que tipo de molécula ele se transforma?",
//             answers = new string[]
//             {
//                 "Nucleosídeo",
//                 "Aminoácido",
//                 "Fosfolipídio",
//                 "Monossacarídeo"
//             },
//             correctIndex = 0,
//             questionNumber = 70,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_070",
//             topic = "nucleicAcids",
//             subtopic = "nucleosides",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "phosphate_removal",
//                 "nucleoside"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Remova conceitualmente os fosfatos e observe o que resta: uma pentose ainda ligada a uma base nitrogenada. Essa combinação possui uma denominação própria e não é mais um nucleotídeo. Não a classifique como monossacarídeo, porque o conjunto conserva também a base ligada ao açúcar.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 071
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma enzima rompe apenas ligações fosfodiéster. Qual parte da molécula de DNA será diretamente fragmentada?",
//             answers = new string[]
//             {
//                 "As ligações entre bases complementares",
//                 "Cada base nitrogenada",
//                 "O esqueleto açúcar-fosfato",
//                 "Os anéis das pentoses"
//             },
//             correctIndex = 2,
//             questionNumber = 71,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_071",
//             topic = "nucleicAcids",
//             subtopic = "nucleotide_bonds",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "phosphodiester_bond",
//                 "sugar_phosphate_backbone"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Ligações fosfodiéster conectam o carbono 3' de uma pentose ao fosfato ligado ao carbono 5' da seguinte. Elas formam a continuidade covalente do esqueleto açúcar-fosfato. Uma enzima específica para essas ligações fragmentará cada fita, mas não romperá diretamente as ligações de hidrogênio entre bases complementares.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 072
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Compare DNA e RNA quanto à estabilidade química. Qual característica contribui para a maior reatividade do RNA?",
//             answers = new string[]
//             {
//                 "Timina no lugar de uracila",
//                 "Dupla hélice obrigatória",
//                 "Ausência de fosfato",
//                 "Grupo hidroxila no carbono 2' da ribose"
//             },
//             correctIndex = 3,
//             questionNumber = 72,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_072",
//             topic = "nucleicAcids",
//             subtopic = "dna_rna_comparison",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "ribose",
//                 "chemical_stability"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Compare as pentoses no carbono 2'. O RNA conserva um grupo hidroxila capaz de participar de reações que favorecem a quebra de seu esqueleto fosfodiéster, especialmente em condições alcalinas. O DNA possui hidrogênio nessa posição. Procure a característica que explica quimicamente a maior reatividade do RNA.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 073
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma fita de DNA molde é 3'-TAC GGA-5'. Qual sequência de RNA é produzida na transcrição?",
//             answers = new string[]
//             {
//                 "5'-UAC GGA-3'",
//                 "3'-AUG CCU-5'",
//                 "5'-AUG CCU-3'",
//                 "5'-ATG CCT-3'"
//             },
//             correctIndex = 2,
//             questionNumber = 73,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_073",
//             topic = "nucleicAcids",
//             subtopic = "transcription",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "template_strand",
//                 "rna_sequence"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Leia primeiro a orientação da fita molde: ela está escrita de 3' para 5', portanto o RNA será produzido de 5' para 3'. Faça a complementaridade base a base, substituindo timina por uracila no produto. Só depois compare sequência e direção com cada alternativa.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 074
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma molécula tem 120 nucleotídeos, sendo 30 adeninas e 30 timinas. Se for DNA dupla fita, quantos nucleotídeos G e C existem ao todo?",
//             answers = new string[]
//             {
//                 "30",
//                 "60",
//                 "90",
//                 "120"
//             },
//             correctIndex = 1,
//             questionNumber = 74,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_074",
//             topic = "nucleicAcids",
//             subtopic = "base_pairing",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "chargaff",
//                 "nucleotide_count"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Subtraia do total os nucleotídeos já contabilizados: 30 A + 30 T ocupam 60 dos 120. Os 60 restantes devem ser G ou C. Como a pergunta solicita G e C em conjunto, não é necessário dividir esse restante, embora em dupla fita suas quantidades sejam iguais.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

//         //QUESTION 075
//         new Question
//         {
//             questionDatabankName = "NucleicAcidsQuestionDatabase",
//             questionText = "Uma substância impede a formação de ligações de hidrogênio entre bases do DNA. Qual nível estrutural será afetado primeiro?",
//             answers = new string[]
//             {
//                 "União das duas fitas complementares",
//                 "Ligação entre açúcar e fosfato",
//                 "Formação dos nucleosídeos",
//                 "Síntese das bases purínicas"
//             },
//             correctIndex = 0,
//             questionNumber = 75,
//             answerType = AnswerType.Text,
//             questionType = QuestionType.Text,
//             questionImagePath = "",
//             questionLevel = 2,
//             questionInDevelopment = false,
//             globalId = "nucleicAcids_075",
//             topic = "nucleicAcids",
//             subtopic = "dna_structure",
//             displayName = "Ácidos Nucleicos",
//             bloomLevel = BloomLevel.Analyze,
//             conceptTags = new List<string>
//             {
//                 "hydrogen_bonds",
//                 "double_helix"
//             },
//             prerequisites = null,
//             questionHint = new QuestionHint
//             {
//                 text = "Ligações de hidrogênio conectam bases complementares pertencentes a fitas diferentes. Se sua formação for impedida, a associação entre as duas cadeias e, portanto, a estrutura de dupla hélice será afetada primeiro. O esqueleto açúcar-fosfato permanece, pois depende de ligações covalentes fosfodiéster.",
//                 imagePath = null,
//                 videoUrl = null,
//                 link = null
//             }
//         },

        //QUESTION 076
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "A medida detecta ácidos nucleicos, mas exige controles para distinguir DNA de RNA",
                "O método é totalmente específico para DNA",
                "A medida identifica apenas proteínas",
                "O método determina diretamente a sequência de bases"
            },
            correctIndex = 0,
            questionNumber = 76,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer76",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_076",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_methods",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "absorbance",
                "method_limitations"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A absorbância a 260 nm é produzida pelas bases dos ácidos nucleicos, tanto de DNA quanto de RNA. Assim, uma leitura elevada confirma material que absorve nessa faixa, mas não distingue sozinha as duas moléculas. Avalie a proposta considerando especificidade, possíveis contaminantes e necessidade de controles adicionais.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 077
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "O DNA é sempre fita simples",
                "O DNA não possui fosfato",
                "A desoxirribose favorece maior estabilidade química",
                "O RNA não contém bases nitrogenadas"
            },
            correctIndex = 2,
            questionNumber = 77,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer77",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_077",
            topic = "nucleicAcids",
            subtopic = "dna_stability",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "deoxyribose",
                "information_storage"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A diferença decisiva está no carbono 2' da pentose. O RNA possui uma hidroxila que favorece reações de hidrólise do esqueleto, enquanto o DNA tem hidrogênio nessa posição e é quimicamente menos reativo. Julgue as justificativas pela relação causal entre estrutura do açúcar e conservação prolongada.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 078
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Correta, pois G-C forma uma ligação de hidrogênio",
                "Correta, pois GC elimina fosfatos",
                "Incorreta, pois composição não influencia desnaturação",
                "Incorreta, pois pares G-C geralmente aumentam a estabilidade da dupla hélice"
            },
            correctIndex = 3,
            questionNumber = 78,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer78",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_078",
            topic = "nucleicAcids",
            subtopic = "dna_stability",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "gc_content",
                "critical_reasoning"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Avalie a afirmação comparando as interações dos pares: G–C estabelece três ligações de hidrogênio e A–T, duas. Em condições comparáveis, aumentar GC costuma elevar a temperatura necessária para separar as fitas. A alternativa defensável deve corrigir o sentido da afirmação e apresentar uma justificativa estrutural coerente.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 079
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Uracila prova que a amostra é proteína",
                "Uracila é um forte indício, mas a identificação deve combinar outros dados estruturais",
                "Todo DNA contém grande quantidade de uracila",
                "RNA nunca contém uracila"
            },
            correctIndex = 1,
            questionNumber = 79,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer79",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_079",
            topic = "nucleicAcids",
            subtopic = "dna_rna_identification",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "uracil",
                "evidence_evaluation"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Uracila é característica importante do RNA celular típico, mas uma conclusão robusta não deve depender de um único marcador. Considere possíveis modificações, contaminações ou misturas e procure evidências adicionais, como tipo de pentose e sensibilidade a enzimas específicas. Escolha a avaliação que reconhece o indício sem tratá-lo como prova absoluta.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 080
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Comparar apenas o comprimento",
                "Medir somente a massa total",
                "Verificar pareamento A-T e G-C em posições correspondentes",
                "Confirmar que ambas contêm ribose"
            },
            correctIndex = 2,
            questionNumber = 80,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer80",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_080",
            topic = "nucleicAcids",
            subtopic = "base_pairing",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "complementarity",
                "evidence"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Complementaridade exige correspondência posição por posição, não apenas igualdade de comprimento ou composição global. Alinhe as fitas em sentidos antiparalelos e verifique se cada adenina corresponde a timina e cada guanina a citosina. A melhor evidência deve testar diretamente essa previsão do pareamento canônico ao longo das sequências.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 081
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "O modelo é correto para qualquer DNA",
                "O modelo deve ser corrigido, pois as fitas da dupla hélice são antiparalelas",
                "O sentido das fitas depende da quantidade de uracila",
                "A orientação não se relaciona à estrutura"
            },
            correctIndex = 1,
            questionNumber = 81,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer81",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_081",
            topic = "nucleicAcids",
            subtopic = "dna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "antiparallelism",
                "model_evaluation"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Na dupla hélice, as ligações fosfodiéster conferem uma direção 5'→3' a cada fita, mas as duas direções são opostas. Um modelo que as mostra paralelas no mesmo sentido representa incorretamente a geometria do DNA. Julgue-o pela orientação dos esqueletos, não apenas pelo aspecto helicoidal.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 082
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Separar as fitas por calor e depois resfriar em condições favoráveis ao pareamento",
                "Aquecer continuamente sem resfriar",
                "Adicionar protease e medir glicose",
                "Remover todas as bases nitrogenadas"
            },
            correctIndex = 0,
            questionNumber = 82,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer82",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_082",
            topic = "nucleicAcids",
            subtopic = "dna_renaturation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "experimental_design",
                "base_pairing"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Um teste de renaturação precisa primeiro separar as fitas e depois oferecer condições para que sequências complementares voltem a parear. Considere aquecimento seguido de resfriamento controlado, concentração de sais e tempo adequado. O desenho deve também permitir observar recuperação da estrutura, em vez de manter continuamente as condições desnaturantes.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 083
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Correta, pois o RNAt armazena o gene",
                "Correta, pois o ribossomo não participa",
                "Incorreta, pois aminoácidos não formam proteínas",
                "Incompleta, pois a ordem depende dos códons do RNAm reconhecidos pelos anticódons"
            },
            correctIndex = 3,
            questionNumber = 83,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer83",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_083",
            topic = "nucleicAcids",
            subtopic = "translation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "mrna",
                "trna",
                "reasoning"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O RNAt não estabelece sozinho a sequência proteica. A ordem é codificada nos códons do RNAm; os anticódons dos RNAt reconhecem esses códons e entregam os aminoácidos, enquanto o ribossomo coordena o processo. Escolha a avaliação que distribui corretamente essas funções e identifica a incompletude da afirmação.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 084
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Ele substitui permanentemente o DNA",
                "Sua hidrólise pode ser acoplada a processos que exigem energia",
                "Ele é o único nucleotídeo celular",
                "Suas bases formam ligações peptídicas"
            },
            correctIndex = 1,
            questionNumber = 84,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer84",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_084",
            topic = "nucleicAcids",
            subtopic = "nucleotide_energy",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "atp_hydrolysis",
                "energy_coupling"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A hidrólise de ATP é energeticamente favorável e pode ser acoplada a reações, transporte ou movimentos que, isoladamente, exigiriam energia. O ATP não “libera energia” por simplesmente conter ligações especiais; o efeito depende do balanço da reação e do acoplamento. Procure a explicação que inclua esse mecanismo funcional.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 085
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Correta, pois fosfato é opcional",
                "Correta apenas para DNA",
                "Incorreta, pois nucleotídeos contêm aminoácidos",
                "Incorreta, pois açúcar e base formam um nucleosídeo, e o nucleotídeo inclui fosfato"
            },
            correctIndex = 3,
            questionNumber = 85,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer85",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_085",
            topic = "nucleicAcids",
            subtopic = "nucleotide_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "nucleoside",
                "nucleotide",
                "evaluation"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Açúcar ligado a uma base nitrogenada forma um nucleosídeo. Para receber a classificação de nucleotídeo, a molécula precisa apresentar também pelo menos um grupo fosfato. Avalie a conclusão verificando se todos os componentes definidores estão presentes, e não tratando o fosfato como opcional ou substituindo-o por aminoácido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 086
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Usar apenas A260, que distingue perfeitamente DNA de RNA",
                "Ignorar a contaminação",
                "Preferir o ensaio específico ou combinar métodos e controles",
                "Medir apenas a cor visível"
            },
            correctIndex = 2,
            questionNumber = 86,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer86",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_086",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_methods",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "quantification",
                "specificity"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A260 responde tanto ao DNA quanto ao RNA, portanto a contaminação pode superestimar a concentração de DNA. Um corante com maior seletividade oferece melhor discriminação, e controles ou combinação de métodos fortalecem a estimativa. Julgue a escolha pelo grau de especificidade necessário para essa amostra, não apenas pela rapidez da leitura.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 087
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "A afirmação é ampla demais, pois RNAr, RNAt e RNAs catalíticos exercem outras funções",
                "A afirmação é correta, pois todo RNA vira DNA",
                "A afirmação é correta apenas porque RNA contém timina",
                "A afirmação é falsa porque RNA não participa da síntese proteica"
            },
            correctIndex = 0,
            questionNumber = 87,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer87",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_087",
            topic = "nucleicAcids",
            subtopic = "rna_function",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "rna_diversity",
                "critical_evaluation"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "RNAm atua como intermediário, mas essa função não representa toda a diversidade dos RNAs. RNAt transporta aminoácidos, RNAr integra e catalisa o ribossomo, e outros RNAs regulam genes ou catalisam reações. A crítica adequada deve apresentar contraexemplos funcionais que invalidem a generalização do livro.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 088
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "Desaparecimento irreversível de todos os nucleotídeos",
                "Formação de aminoácidos",
                "Aumento reversível de A260 e recuperação do pareamento após resfriamento",
                "Perda exclusiva dos grupos fosfato"
            },
            correctIndex = 2,
            questionNumber = 88,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer88",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_088",
            topic = "nucleicAcids",
            subtopic = "dna_denaturation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "hyperchromicity",
                "renaturation"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Desnaturação separa as fitas sem necessariamente cortar o esqueleto; degradação completa destrói a integridade das cadeias. Procure um resultado reversível: aumento de A260 durante a separação seguido de recuperação do pareamento após resfriamento. A reversibilidade indica que sequências complementares e esqueletos covalentes permaneceram suficientemente preservados.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 089
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "A escolha é questionável porque o grupo 2'-OH aumenta a suscetibilidade à hidrólise",
                "A escolha é ideal porque a ribose é menos reativa",
                "A escolha é obrigatória porque DNA não armazena informação",
                "Não há diferença química entre DNA e RNA"
            },
            correctIndex = 0,
            questionNumber = 89,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer89",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_089",
            topic = "nucleicAcids",
            subtopic = "dna_rna_comparison",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "rna_stability",
                "design_evaluation"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "O grupo 2'-OH da ribose pode favorecer a hidrólise do esqueleto fosfodiéster, tornando o RNA menos estável que o DNA sem proteção. Para um arquivo duradouro, avalie não apenas a capacidade de armazenar sequência, mas também a resistência química ao longo do tempo e as condições propostas.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 090
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "",
            answers = new string[]
            {
                "A replicação é fiel apenas porque DNA contém fosfato",
                "A complementaridade orienta a incorporação, e mecanismos enzimáticos de revisão reduzem erros",
                "Todas as bases se emparelham igualmente",
                "A ligase escolhe sozinha cada base complementar"
            },
            correctIndex = 1,
            questionNumber = 90,
            answerType = AnswerType.Text,
            questionType = QuestionType.Image,
            questionImagePath = "QuestionImages/NucleicAcidsDB/NucleicAcidsDB_ImageQuestionContainer90",
            questionLevel = 3,
            questionInDevelopment = false,
            globalId = "nucleicAcids_090",
            topic = "nucleicAcids",
            subtopic = "dna_replication",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Evaluate,
            conceptTags = new List<string>
            {
                "complementarity",
                "proofreading",
                "fidelity"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "A complementaridade das bases orienta qual nucleotídeo deve ser incorporado, mas esse mecanismo sozinho não elimina todos os erros. Polimerases também selecionam substratos e podem revisar incorporações incorretas, enquanto reparos posteriores aumentam a fidelidade. A melhor explicação deve combinar a regra estrutural com mecanismos enzimáticos de controle.",
                imagePath = null,
                videoUrl = null,
                link = null
            }

        },

};


         

    public List<Question> GetQuestions() => questions;

    public QuestionSet GetQuestionSetType() => QuestionSet.nucleicAcids;

    public string GetDatabankName()  => "NucleicAcidsQuestionDatabase";
    public string GetDisplayName()   => "Ácidos Nucleicos";

    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
