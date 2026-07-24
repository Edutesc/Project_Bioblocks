using System.Collections.Generic;
using QuestionSystem;

public class NucleicAcidsQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;
    
    private List<Question> questions = new List<Question>
    {
        //QUESTION 001
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quem primeiro isolou o ácido nucléico?",
            answers = new string[] { "Watson", "Crick", "Friedrich Miescher", "Chargaff" },
            correctIndex = 2,
            questionNumber = 1,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_001",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_history",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "friedrich_miescher", "nuclein" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a história da identificação dos ácidos nucleicos e as evidências experimentais disponíveis. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 002
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a principal função do RNA na célula?",
            answers = new string[] { 
                "Armazenamento de informação genética", 
                "Síntese de proteínas", 
                "Catálise de reações", 
                "Transporte de íons" 
            },
            correctIndex = 1,
            questionNumber = 2,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_002",
            topic = "nucleicAcids",
            subtopic = "rna_function",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "protein_synthesis", "gene_expression" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as diferentes funções celulares dos RNAs e sua participação na expressão da informação genética. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 003
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais são os três componentes de um nucleotídeo?",
            answers = new string[] { 
                "Açúcar, base, fosfato", 
                "Açúcar, base, aminoácido", 
                "Base, aminoácido, fosfato", 
                "Açúcar, lipídeo, base" 
            },
            correctIndex = 0,
            questionNumber = 3,
            answerType = AnswerType.Text,
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
                text = "Considere a organização química de nucleotídeos, nucleosídeos e seus componentes estruturais. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 004
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual açúcar está presente no RNA?",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 1,
            questionNumber = 4,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_004",
            topic = "nucleicAcids",
            subtopic = "rna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "ribose", "pentose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as características químicas que distinguem a estrutura do RNA daquela encontrada no DNA. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 005
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual açúcar está presente no DNA?",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 0,
            questionNumber = 5,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_005",
            topic = "nucleicAcids",
            subtopic = "dna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "deoxyribose", "pentose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização da dupla hélice, a orientação das fitas e as interações entre seus componentes. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 006
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que são nucleosídeos?",
            answers = new string[] {
                "Açúcar + base",
                "Açúcar + base + fosfato",
                "Base + fosfato",
                "Açúcar + aminoácido"
            },
            correctIndex = 0,
            questionNumber = 6,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_006",
            topic = "nucleicAcids",
            subtopic = "nucleosides",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "pentose", "nitrogenous_base" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a diferença estrutural entre nucleosídeos e nucleotídeos, especialmente a presença de grupos fosfato. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 007
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Identifique a estrutura do nucleosídeo",
            answers = new string[] {
                "AnswerImages/NucleicAcidDB/nucleotideo_di_fosfato",
                "AnswerImages/NucleicAcidDB/nucleotideo_mono_fosfato",
                "AnswerImages/NucleicAcidDB/nucleosideo",
                "AnswerImages/NucleicAcidDB/nucleotideo_tri_fosfato"
            },
            correctIndex = 2,
            questionNumber = 7,
            answerType = AnswerType.Image,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_007",
            topic = "nucleicAcids",
            subtopic = "nucleosides",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "pentose", "nitrogenous_base", "structure_identification" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a diferença estrutural entre nucleosídeos e nucleotídeos, especialmente a presença de grupos fosfato. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 008
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais bases são encontradas no RNA, mas não no DNA?",
            answers = new string[] { "Adenina, guanina", "Citosina, timina", "Uracila", "Timina, uracila" },
            correctIndex = 2,
            questionNumber = 8,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_008",
            topic = "nucleicAcids",
            subtopic = "nitrogenous_bases",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "rna_bases", "uracil", "dna_rna_differences" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a distribuição das bases nitrogenadas em DNA e RNA e suas regras de complementaridade. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 009
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Quais bases são encontradas no DNA, mas não no RNA?",
            answers = new string[] { "Adenina, guanina", "Citosina, timina", "Uracila", "Timina" },
            correctIndex = 3,
            questionNumber = 9,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_009",
            topic = "nucleicAcids",
            subtopic = "nitrogenous_bases",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "dna_bases", "thymine", "dna_rna_differences" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a distribuição das bases nitrogenadas em DNA e RNA e suas regras de complementaridade. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 010
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal dos grupamentos fosfato nos nucleotídeos?",
            answers = new string[] { "Dar caráter básico", "Dar caráter ácido", "Formar ligações peptídicas", "Armazenar energia" },
            correctIndex = 1,
            questionNumber = 10,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_010",
            topic = "nucleicAcids",
            subtopic = "nucleotide_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "phosphate_group", "acidic_character" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização química de nucleotídeos, nucleosídeos e seus componentes estruturais. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 011
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Que tipo de ligação une os nucleotídeos em uma cadeia?",
            answers = new string[] { "Ligação peptídica", "Ligação glicosídica", "Ligação éster", "Ligação fosfodiéster" },
            correctIndex = 3,
            questionNumber = 11,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_011",
            topic = "nucleicAcids",
            subtopic = "phosphodiester_bond",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "nucleotide_polymerization", "nucleic_acid_backbone" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 012
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a orientação das cadeias de DNA em uma dupla hélice?",
            answers = new string[] { "Paralela", "Antiparalela", "Perpendicular", "Aleatória" },
            correctIndex = 1,
            questionNumber = 12,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_012",
            topic = "nucleicAcids",
            subtopic = "dna_double_helix",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "antiparallel_strands", "dna_structure" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 013
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que são pares de bases de Watson-Crick?",
            answers = new string[] {
                "A-T e G-C",
                "A-G e T-C",
                "A-C e G-T",
                "Qualquer combinação de bases."
            },
            correctIndex = 0,
            questionNumber = 13,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_013",
            topic = "nucleicAcids",
            subtopic = "base_pairing",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "watson_crick_pairs", "adenine_thymine", "guanine_cytosine" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as regras quantitativas de complementaridade entre bases em moléculas de dupla fita. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 014
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual tipo de ligação mantém os pares de bases unidos no DNA?",
            answers = new string[] { "Ligação iônica", "Ligação covalente", "Pontes de hidrogênio", "Ligação peptídica" },
            correctIndex = 2,
            questionNumber = 14,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_014",
            topic = "nucleicAcids",
            subtopic = "base_pairing",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "hydrogen_bonds", "dna_double_helix" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as regras quantitativas de complementaridade entre bases em moléculas de dupla fita. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 015
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal do DNA?",
            answers = new string[] { "Transporte de moléculas", "Síntese de proteínas", "Armazenamento de informação genética", "Catálise de reações" },
            correctIndex = 2,
            questionNumber = 15,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_015",
            topic = "nucleicAcids",
            subtopic = "dna_function",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "genetic_information", "information_storage" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estabilidade molecular, armazenamento da informação e transmissão hereditária. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 016
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal do RNA?",
            answers = new string[] { "Transporte de moléculas", "Síntese de proteínas", "Armazenamento de informação genética", "Expressão da informação genética" },
            correctIndex = 3,
            questionNumber = 16,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_016",
            topic = "nucleicAcids",
            subtopic = "rna_function",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "gene_expression", "protein_synthesis" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as diferentes funções celulares dos RNAs e sua participação na expressão da informação genética. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 017
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é desnaturação do DNA?",
            answers = new string[] {
                "Quebra da dupla hélice.",
                "Separação das fitas.",
                "Mudança na seqüência de bases.",
                "Todas as alternativas acima."
            },
            correctIndex = 1,
            questionNumber = 17,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_017",
            topic = "nucleicAcids",
            subtopic = "dna_denaturation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "strand_separation", "hydrogen_bonds" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as alterações estruturais provocadas por temperatura e as formas experimentais de acompanhá-las. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 018
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é renaturação do DNA?",
            answers = new string[] {
                "Formação de novas fitas.",
                "Reassociação das fitas.",
                "Replicação do DNA.",
                "Transcrição do DNA."
            },
            correctIndex = 1,
            questionNumber = 18,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_018",
            topic = "nucleicAcids",
            subtopic = "dna_renaturation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "strand_reassociation", "complementary_base_pairing" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as condições que permitem o reencontro e o pareamento de sequências complementares. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 019
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que causa a desnaturação do DNA?",
            answers = new string[] {
                "Altas temperaturas",
                "Extremos de pH",
                "Ação de enzimas",
                "Todas as alternativas acima"
            },
            correctIndex = 3,
            questionNumber = 19,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_019",
            topic = "nucleicAcids",
            subtopic = "dna_denaturation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "temperature_effects", "ph_effects", "strand_separation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as alterações estruturais provocadas por temperatura e as formas experimentais de acompanhá-las. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 020
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Para que serve a medida de absorvância a 260nm?",
            answers = new string[] {
                "Medida da concentração de proteínas.",
                "Medida da concentração de ácidos nucléicos.",
                "Medida da temperatura de fusão do DNA.",
                "Medida da viscosidade de uma solução."
            },
            correctIndex = 1,
            questionNumber = 20,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_020",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_quantification",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "uv_absorbance", "a260" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 021
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é a regra de Chargaff?",
            answers = new string[] {
                "A = T e G = C",
                "A = G e T = C",
                "A = C e G = T",
                "Não há regra de Chargaff."
            },
            correctIndex = 0,
            questionNumber = 21,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_021",
            topic = "nucleicAcids",
            subtopic = "chargaff_rule",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "base_pairing", "adenine_thymine", "guanine_cytosine" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 022
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Em que tipo de molécula a regra de Chargaff se aplica?",
            answers = new string[] {
                "DNA",
                "RNA",
                "Proteínas",
                "Carboidratos"
            },
            correctIndex = 0,
            questionNumber = 22,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_022",
            topic = "nucleicAcids",
            subtopic = "chargaff_rule",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "dna_structure", "base_composition" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 023
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é o 'fluxo da informação genética'?",
            answers = new string[] {
                "O movimento de íons através da membrana.",
                "A replicação do DNA.",
                "O processo de conversão da informação genética em proteínas.",
                "O transporte de proteínas para o exterior da célula."
            },
            correctIndex = 2,
            questionNumber = 23,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_023",
            topic = "nucleicAcids",
            subtopic = "central_dogma",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "dna", "rna", "protein_synthesis", "gene_expression" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 024
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual tipo de RNA transporta aminoácidos para os ribossomos?",
            answers = new string[] { "tRNA", "rRNA", "mRNA", "snRNA" },
            correctIndex = 0,
            questionNumber = 24,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_024",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "trna", "amino_acid_transport", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 025
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual tipo de RNA faz parte da estrutura dos ribossomos?",
            answers = new string[] { "tRNA", "rRNA", "mRNA", "snRNA" },
            correctIndex = 1,
            questionNumber = 25,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_025",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "rrna", "ribosomes", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 026
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a principal diferença química entre DNA e RNA?",
            answers = new string[] { "Açúcar", "Bases nitrogenadas", "Grupamento fosfato", "Sequência de bases" },
            correctIndex = 0,
            questionNumber = 26,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_026",
            topic = "nucleicAcids",
            subtopic = "dna_rna_differences",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "ribose", "deoxyribose", "sugars" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 027
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a principal diferença na composição de bases entre DNA e RNA?",
            answers = new string[] { "Timina vs. Uracila", "Adenina vs. Guanina", "Citosina vs. Guanina", "Ribose vs. Desoxirribose" },
            correctIndex = 0,
            questionNumber = 27,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_027",
            topic = "nucleicAcids",
            subtopic = "dna_rna_differences",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "thymine", "uracil", "nitrogenous_bases" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 028
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "O que é um códon?",
            answers = new string[] {
                "Uma seqüência de três bases no tRNA.",
                "Uma seqüência de três bases no mRNA.",
                "Uma seqüência de três bases no rRNA.",
                "Uma seqüência de três bases no DNA."
            },
            correctIndex = 1,
            questionNumber = 28,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_028",
            topic = "nucleicAcids",
            subtopic = "genetic_code",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Remember,
            conceptTags = new List<string> { "codon", "mrna", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Recupere as definições essenciais e observe exatamente qual propriedade o enunciado solicita. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 029
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função do anticódon no tRNA?",
            answers = new string[] {
                "Ligar-se ao ribossomo.",
                "Ligar-se ao mRNA.",
                "Ligar-se a proteínas.",
                "Ligar-se ao DNA."
            },
            correctIndex = 1,
            questionNumber = 29,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_029",
            topic = "nucleicAcids",
            subtopic = "genetic_code",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "anticodon", "trna", "mrna", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 030
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual a função principal dos rRNAs?",
            answers = new string[] {
                "Transporte de aminoácidos.",
                "Síntese de proteínas.",
                "Fazem parte da estrutura dos ribossomos.",
                "Catalisam reações."
            },
            correctIndex = 2,
            questionNumber = 30,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_030",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "rrna", "ribosomes", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 031
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Explique qual alternativa reúne, sem incluir componentes estranhos, as três partes estruturais de um nucleotídeo.",
            answers = new string[] { "Açúcar, base, fosfato", "Açúcar, base, aminoácido", "Base, aminoácido, fosfato", "Açúcar, lipídeo, base" },
            correctIndex = 0,
            questionNumber = 31,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_031",
            topic = "nucleicAcids",
            subtopic = "nucleotide_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "pentose", "nitrogenous_base", "phosphate_group" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização química de nucleotídeos, nucleosídeos e seus componentes estruturais. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 032
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Diferencie nucleosídeo de nucleotídeo escolhendo a composição que representa apenas um nucleosídeo.",
            answers = new string[] { "Açúcar + base + fosfato", "Açúcar + base", "Base + fosfato", "Açúcar + aminoácido" },
            correctIndex = 1,
            questionNumber = 32,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_032",
            topic = "nucleicAcids",
            subtopic = "nucleosides",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "pentose", "nitrogenous_base" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a diferença estrutural entre nucleosídeos e nucleotídeos, especialmente a presença de grupos fosfato. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 033
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Associe a denominação ribonucleotídeo ao tipo de pentose presente em sua estrutura.",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 1,
            questionNumber = 33,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_033",
            topic = "nucleicAcids",
            subtopic = "rna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "ribonucleotides", "ribose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as características químicas que distinguem a estrutura do RNA daquela encontrada no DNA. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 034
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Associe a denominação desoxirribonucleotídeo ao tipo de pentose presente em sua estrutura.",
            answers = new string[] { "Desoxirribose", "Ribose", "Glicose", "Frutose" },
            correctIndex = 0,
            questionNumber = 34,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_034",
            topic = "nucleicAcids",
            subtopic = "dna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "deoxyribonucleotides", "deoxyribose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização da dupla hélice, a orientação das fitas e as interações entre seus componentes. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 035
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Reconheça a relação entre monômero e polímero escolhendo a unidade repetitiva que forma os ácidos nucleicos.",
            answers = new string[] {
                "Aminoácidos",
                "Nucleotídeos",
                "Monossacarídeos",
                "Lipídios"
            },
            correctIndex = 1,
            questionNumber = 35,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_035",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_polymers",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "nucleotides", "macromolecules" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 036
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Classifique as duas macromoléculas que constituem os principais tipos de ácidos nucleicos dos seres vivos.",
            answers = new string[] {
                "DNA e RNA",
                "DNA e ATP",
                "RNA e lipídios",
                "DNA e proteínas"
            },
            correctIndex = 0,
            questionNumber = 36,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_036",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "dna", "rna" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 037
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Explique a origem do termo ribonucleico identificando o açúcar característico do RNA.",
            answers = new string[] {
                "Desoxirribose",
                "Glicose",
                "Ribose",
                "Maltose"
            },
            correctIndex = 2,
            questionNumber = 37,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_037",
            topic = "nucleicAcids",
            subtopic = "rna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "ribose", "pentose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as características químicas que distinguem a estrutura do RNA daquela encontrada no DNA. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 038
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Interprete a complementaridade das bases e indique com qual base a adenina se emparelha no DNA.",
            answers = new string[] {
                "Guanina",
                "Citosina",
                "Timina",
                "Uracila"
            },
            correctIndex = 2,
            questionNumber = 38,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_038",
            topic = "nucleicAcids",
            subtopic = "base_pairing",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "adenine_thymine", "dna_structure" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as regras quantitativas de complementaridade entre bases em moléculas de dupla fita. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 039
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Relacione o modelo estrutural da dupla hélice aos pesquisadores que o propuseram.",
            answers = new string[] {
                "Darwin e Lamarck",
                "Watson e Crick",
                "Pasteur e Koch",
                "Franklin e Mendel"
            },
            correctIndex = 1,
            questionNumber = 39,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_039",
            topic = "nucleicAcids",
            subtopic = "dna_double_helix",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "watson_crick", "dna_structure_history" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 040
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Diferencie os tipos de RNA pela função e identifique aquele que leva a informação genética aos ribossomos.",
            answers = new string[] {
                "RNA ribossômico (rRNA)",
                "RNA transportador (tRNA)",
                "RNA mensageiro (mRNA)",
                "RNA nuclear"
            },
            correctIndex = 2,
            questionNumber = 40,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 1,
            questionInDevelopment = false,
            globalId = "nucleicAcids_040",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Understand,
            conceptTags = new List<string> { "mrna", "gene_expression", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Procure explicar a relação apresentada com suas próprias palavras antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 041
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma molécula de RNA foi hidrolisada e uma base que normalmente não integra o DNA foi detectada. Qual base é compatível com o resultado?",
            answers = new string[] {
                "Guanina",
                "Uracila",
                "Adenina",
                "Citosina"
            },
            correctIndex = 1,
            questionNumber = 41,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_041",
            topic = "nucleicAcids",
            subtopic = "nitrogenous_bases",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "uracil", "rna_bases", "thymine" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Lembre-se que o DNA possui Timina, enquanto o RNA a substitui por outra base pirimídica exclusiva. Qual é essa base?",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 042
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Em um experimento, uma fita de DNA serviu de molde para produzir uma molécula complementar de RNA. Como se chama esse processo?",
            answers = new string[] {
                "Tradução",
                "Transcrição",
                "Replicação",
                "Mutação"
            },
            correctIndex = 1,
            questionNumber = 42,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_042",
            topic = "nucleicAcids",
            subtopic = "transcription",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "rna_synthesis", "dna_template", "gene_expression" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a utilização de uma fita molde de DNA para sintetizar RNA complementar. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 043
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma célula precisa acoplar energia à contração muscular. Qual papel do ATP explica sua utilização imediata nesse processo?",
            answers = new string[] {
                "Formar a bicamada lipídica",
                "Ser uma fonte de energia celular",
                "Carregar oxigênio no sangue",
                "Transportar aminoácidos"
            },
            correctIndex = 1,
            questionNumber = 43,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_043",
            topic = "nucleicAcids",
            subtopic = "nucleotide_functions",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "atp", "cellular_energy" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 044
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Durante transporte ativo, a célula consome ATP. Qual propriedade desse nucleotídeo permite sustentar o trabalho celular?",
            answers = new string[] {
                "Formar a bicamada lipídica",
                "Ser uma fonte de energia celular",
                "Carregar oxigênio no sangue",
                "Transportar aminoácidos"
            },
            correctIndex = 1,
            questionNumber = 44,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_044",
            topic = "nucleicAcids",
            subtopic = "nucleotide_functions",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "atp", "cellular_energy" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 045
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Durante a replicação, nucleotídeos complementares precisam ser adicionados à nova fita. Qual enzima realiza essa etapa?",
            answers = new string[] {
                "DNA polimerase",
                "RNA polimerase",
                "Ligase",
                "Transcriptase reversa"
            },
            correctIndex = 0,
            questionNumber = 45,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_045",
            topic = "nucleicAcids",
            subtopic = "dna_replication",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "dna_polymerase", "nucleotide_polymerization" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a orientação das fitas, a complementaridade e as funções coordenadas das enzimas de replicação. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 046
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Ao analisar um polímero com pentose, fosfato e bases nitrogenadas repetidos, qual unidade monomérica deve ser identificada?",
            answers = new string[] {
                "Aminoácidos",
                "Nucleotídeos",
                "Monossacarídeos",
                "Lipídeos"
            },
            correctIndex = 1,
            questionNumber = 46,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_046",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_polymers",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "nucleotides", "macromolecules" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 047
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um pesquisador isolou uma molécula com pentose, base nitrogenada e fosfato. Como essa molécula deve ser classificada?",
            answers = new string[] {
                "Pentose + fosfato + base nitrogenada",
                "Hexose + lipídio + aminoácido",
                "Glicose + fosfato + proteína",
                "Glicerol + base nitrogenada + ácido graxo"
            },
            correctIndex = 0,
            questionNumber = 47,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_047",
            topic = "nucleicAcids",
            subtopic = "nucleotide_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "pentose", "nitrogenous_base", "phosphate_group" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização química de nucleotídeos, nucleosídeos e seus componentes estruturais. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 048
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma amostra contém ribose e uma base que substitui a timina. Qual base deve aparecer na análise?",
            answers = new string[] {
                "Timina",
                "Citosina",
                "Uracila",
                "Adenina"
            },
            correctIndex = 2,
            questionNumber = 48,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_048",
            topic = "nucleicAcids",
            subtopic = "nitrogenous_bases",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "uracil", "rna_bases" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a distribuição das bases nitrogenadas em DNA e RNA e suas regras de complementaridade. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 049
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma amostra de DNA contém uma pirimidina ausente do RNA celular típico. Qual base deve ser detectada?",
            answers = new string[] {
                "Uracila",
                "Adenina",
                "Timina",
                "Guanina"
            },
            correctIndex = 2,
            questionNumber = 49,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_049",
            topic = "nucleicAcids",
            subtopic = "nitrogenous_bases",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "thymine", "dna_bases" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a distribuição das bases nitrogenadas em DNA e RNA e suas regras de complementaridade. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 050
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma imagem mostra duas fitas antiparalelas enroladas e unidas por bases complementares. Qual descrição estrutural se aplica?",
            answers = new string[] {
                "Hélice simples",
                "Tripla hélice",
                "Dupla hélice",
                "Cadeia linear"
            },
            correctIndex = 2,
            questionNumber = 50,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_050",
            topic = "nucleicAcids",
            subtopic = "dna_double_helix",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "dna_structure" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 051
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Após analisar um ácido nucleico estável, encontrou-se uma pentose sem hidroxila no carbono 2'. Qual açúcar está presente?",
            answers = new string[] {
                "Ribose",
                "Desoxirribose",
                "Glicose",
                "Frutose"
            },
            correctIndex = 1,
            questionNumber = 51,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_051",
            topic = "nucleicAcids",
            subtopic = "dna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "deoxyribose", "pentose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização da dupla hélice, a orientação das fitas e as interações entre seus componentes. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 052
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma fita molde de DNA apresenta adenina em determinada posição. Qual base deve ser incorporada na nova fita complementar?",
            answers = new string[] {
                "Guanina",
                "Citosina",
                "Uracila",
                "Timina"
            },
            correctIndex = 3,
            questionNumber = 52,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_052",
            topic = "nucleicAcids",
            subtopic = "base_pairing",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "adenine_thymine", "dna_structure" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as regras quantitativas de complementaridade entre bases em moléculas de dupla fita. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 053
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma molécula recém-transcrita segue do núcleo ao ribossomo levando códons. Qual função descreve esse RNA?",
            answers = new string[] {
                "Formar a estrutura dos ribossomos",
                "Transportar aminoácidos",
                "Levar a informação genética do DNA até os ribossomos",
                "Catalisar reações químicas"
            },
            correctIndex = 2,
            questionNumber = 53,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_053",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "mrna", "gene_expression", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 054
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Durante a tradução, uma molécula reconhece códons e entrega aminoácidos ao ribossomo. Qual é sua função?",
            answers = new string[] {
                "Levar aminoácidos até os ribossomos durante a síntese proteica",
                "Carregar energia química",
                "Armazenar informação genética",
                "Catalisar reações metabólicas"
            },
            correctIndex = 0,
            questionNumber = 54,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_054",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "trna", "amino_acid_transport", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 055
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma célula precisa conservar instruções hereditárias por muitas divisões. Qual função molecular deve ser atribuída ao DNA?",
            answers = new string[] {
                "Atuar como catalisador enzimático",
                "Fornecer energia imediata",
                "Armazenar e transmitir a informação genética",
                "Transportar oxigênio"
            },
            correctIndex = 2,
            questionNumber = 55,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_055",
            topic = "nucleicAcids",
            subtopic = "dna_function",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "genetic_information", "inheritance" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estabilidade molecular, armazenamento da informação e transmissão hereditária. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 056
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma enzima degrada um ácido nucleico em suas unidades repetitivas. Quais unidades serão obtidas?",
            answers = new string[] {
                "Aminoácidos",
                "Monossacarídeos",
                "Nucleotídeos",
                "Ácidos graxos"
            },
            correctIndex = 2,
            questionNumber = 56,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_056",
            topic = "nucleicAcids",
            subtopic = "nucleic_acid_polymers",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "nucleotides", "macromolecules" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 057
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma molécula possui base nitrogenada ligada a uma pentose e a fosfato. Qual alternativa descreve seus componentes?",
            answers = new string[] {
                "Aminoácido, fosfato e água",
                "Açúcar, base nitrogenada e fosfato",
                "Glicerol, ácido graxo e base nitrogenada",
                "Açúcar, lipídio e proteína"
            },
            correctIndex = 1,
            questionNumber = 57,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_057",
            topic = "nucleicAcids",
            subtopic = "nucleotide_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "pentose", "nitrogenous_base", "phosphate_group" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização química de nucleotídeos, nucleosídeos e seus componentes estruturais. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 058
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma análise química detecta pentose sem oxigênio no carbono 2'. A qual açúcar do DNA esse resultado corresponde?",
            answers = new string[] {
                "Glicose",
                "Ribose",
                "Desoxirribose",
                "Galactose"
            },
            correctIndex = 2,
            questionNumber = 58,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_058",
            topic = "nucleicAcids",
            subtopic = "dna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "deoxyribose", "pentose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização da dupla hélice, a orientação das fitas e as interações entre seus componentes. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 059
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma análise química detecta pentose com hidroxila no carbono 2'. A qual açúcar do RNA esse resultado corresponde?",
            answers = new string[] {
                "Glicose",
                "Ribose",
                "Desoxirribose",
                "Maltose"
            },
            correctIndex = 1,
            questionNumber = 59,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_059",
            topic = "nucleicAcids",
            subtopic = "rna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "ribose", "pentose" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as características químicas que distinguem a estrutura do RNA daquela encontrada no DNA. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 060
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma dupla fita de DNA apresenta 30% de adenina. Aplicando a regra de Chargaff, qual porcentagem de timina deve ser esperada?",
            answers = new string[] {
                "20%",
                "30%",
                "40%",
                "70%"
            },
            correctIndex = 1,
            questionNumber = 60,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_060",
            topic = "nucleicAcids",
            subtopic = "chargaff_rule",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Apply,
            conceptTags = new List<string> { "base_pairing", "adenine_thymine", "guanine_cytosine" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Identifique os dados do caso, escolha o princípio pertinente e aplique-o antes de comparar as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 061
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Em uma célula, a molécula X leva códons ao ribossomo, enquanto a molécula Y transporta aminoácidos. Ao comparar X e Y, qual função pertence especificamente a X?",
            answers = new string[] {
                "Transportar aminoácidos",
                "Atuar como catalisador enzimático",
                "Levar a informação do DNA até os ribossomos",
                "Formar a dupla hélice do DNA"
            },
            correctIndex = 2,
            questionNumber = 61,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_061",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "mrna", "gene_expression", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 062
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Durante a tradução, a molécula X apresenta anticódon, enquanto a molécula Y contém códons. Ao comparar X e Y, qual função pertence especificamente a X?",
            answers = new string[] {
                "Levar aminoácidos até o ribossomo durante a síntese de proteínas",
                "Duplicar o DNA",
                "Formar a membrana celular",
                "Produzir energia na respiração"
            },
            correctIndex = 0,
            questionNumber = 62,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_062",
            topic = "nucleicAcids",
            subtopic = "rna_types",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "trna", "amino_acid_transport", "translation" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 063
        new Question {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um texto atribui a Franklin dados de difração, a Chargaff relações entre bases e a dois pesquisadores a proposição do modelo da dupla hélice em 1953. Quem corresponde à última contribuição?",
            answers = new string[] {
                "Darwin e Mendel",
                "Watson e Crick",
                "Franklin e Chargaff",
                "Pauling e Wöhler"
            },
            correctIndex = 1,
            questionNumber = 63,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_063",
            topic = "nucleicAcids",
            subtopic = "dna_double_helix",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string> { "watson_crick", "dna_structure_history" },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 064
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma amostra de DNA dupla fita contém 18% de guanina. Qual porcentagem de adenina é esperada?",
            answers = new string[]
            {
                "18%",
                "36%",
                "32%",
                "64%"
            },
            correctIndex = 2,
            questionNumber = 64,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_064",
            topic = "nucleicAcids",
            subtopic = "base_pairing",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "chargaff",
                "percentage_calculation"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Pela Regra de Chargaff, a quantidade de Citosina é igual à de Guanina (G=C), e Adenina é igual à Timina (A=T). Se G=18%, então C também é 18%. Subtraia a soma (36%) de 100% para descobrir quanto sobra para o par A-T",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 065
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Compare duas amostras de DNA: X tem 40% de GC e Y tem 60% de GC. Qual tende a exigir maior temperatura para desnaturar?",
            answers = new string[]
            {
                "Y, por ter mais pares G-C",
                "X, por ter mais pares A-T",
                "Ambas, por terem o mesmo comprimento",
                "Não é possível relacionar composição e estabilidade"
            },
            correctIndex = 0,
            questionNumber = 65,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_065",
            topic = "nucleicAcids",
            subtopic = "dna_stability",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "gc_content",
                "melting_temperature"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere os fatores químicos e estruturais que alteram a estabilidade da dupla hélice. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 066
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma molécula apresenta uracila, ribose e uma única cadeia. Qual classificação é mais consistente?",
            answers = new string[]
            {
                "DNA",
                "Proteína",
                "Polissacarídeo",
                "RNA"
            },
            correctIndex = 3,
            questionNumber = 66,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_066",
            topic = "nucleicAcids",
            subtopic = "dna_rna_comparison",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "uracil",
                "ribose",
                "single_strand"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as diferenças de açúcar, bases, estabilidade e função entre DNA e RNA. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 067
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Durante a replicação, uma fita é sintetizada continuamente e outra em fragmentos. Qual característica explica essa diferença?",
            answers = new string[]
            {
                "As bases possuem cargas positivas",
                "As fitas molde são antiparalelas",
                "A ligase sintetiza ambas as fitas",
                "O DNA contém ribose"
            },
            correctIndex = 1,
            questionNumber = 67,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_067",
            topic = "nucleicAcids",
            subtopic = "dna_replication",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "antiparallelism",
                "replication_fork"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a orientação das fitas, a complementaridade e as funções coordenadas das enzimas de replicação. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 068
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Após aquecimento, a absorvância de uma solução de DNA a 260 nm aumenta. Qual interpretação é mais adequada?",
            answers = new string[]
            {
                "O DNA foi traduzido",
                "As fitas se separaram e as bases ficaram mais expostas",
                "Os nucleotídeos viraram aminoácidos",
                "O fosfato foi removido"
            },
            correctIndex = 1,
            questionNumber = 68,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_068",
            topic = "nucleicAcids",
            subtopic = "dna_denaturation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "hyperchromic_effect",
                "absorbance"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as alterações estruturais provocadas por temperatura e as formas experimentais de acompanhá-las. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 069
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma mutação altera o anticódon de um RNAt sem mudar o aminoácido ligado a ele. Qual consequência direta é mais provável?",
            answers = new string[]
            {
                "Interrupção da transcrição do DNA",
                "Duplicação do cromossomo",
                "Conversão do RNAt em RNAr",
                "Reconhecimento de um códon diferente"
            },
            correctIndex = 3,
            questionNumber = 69,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_069",
            topic = "nucleicAcids",
            subtopic = "translation",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "anticodon",
                "codon_recognition"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a cooperação entre códons, anticódons, ribossomos e aminoácidos durante a tradução. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 070
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um nucleotídeo perde todos os seus grupos fosfato. Em que tipo de molécula ele se transforma?",
            answers = new string[]
            {
                "Nucleosídeo",
                "Aminoácido",
                "Fosfolipídio",
                "Monossacarídeo"
            },
            correctIndex = 0,
            questionNumber = 70,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_070",
            topic = "nucleicAcids",
            subtopic = "nucleosides",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "phosphate_removal",
                "nucleoside"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Todo nucleotídeo é formado por três partes: grupo fosfato, pentose e base nitrogenada. Se você remover o grupo fosfato, a molécula resultante contendo apenas açúcar e base recebe outro nome.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 071
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma enzima rompe apenas ligações fosfodiéster. Qual parte da molécula de DNA será diretamente fragmentada?",
            answers = new string[]
            {
                "As ligações entre bases complementares",
                "Cada base nitrogenada",
                "O esqueleto açúcar-fosfato",
                "Os anéis das pentoses"
            },
            correctIndex = 2,
            questionNumber = 71,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_071",
            topic = "nucleicAcids",
            subtopic = "nucleotide_bonds",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "phosphodiester_bond",
                "sugar_phosphate_backbone"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as ligações que organizam o esqueleto dos ácidos nucleicos e estabilizam suas estruturas. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 072
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Compare DNA e RNA quanto à estabilidade química. Qual característica contribui para a maior reatividade do RNA?",
            answers = new string[]
            {
                "Timina no lugar de uracila",
                "Dupla hélice obrigatória",
                "Ausência de fosfato",
                "Grupo hidroxila no carbono 2' da ribose"
            },
            correctIndex = 3,
            questionNumber = 72,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_072",
            topic = "nucleicAcids",
            subtopic = "dna_rna_comparison",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "ribose",
                "chemical_stability"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Foque na estrutura da pentose. A principal diferença química que torna o RNA mais instável (reativo) e propenso à hidrólise é a presença de um grupo oxigenado extra no carbono 2.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 073
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma fita de DNA molde é 3'-TAC GGA-5'. Qual sequência de RNA é produzida na transcrição?",
            answers = new string[]
            {
                "5'-UAC GGA-3'",
                "3'-AUG CCU-5'",
                "5'-AUG CCU-3'",
                "5'-ATG CCT-3'"
            },
            correctIndex = 2,
            questionNumber = 73,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_073",
            topic = "nucleicAcids",
            subtopic = "transcription",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "template_strand",
                "rna_sequence"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a utilização de uma fita molde de DNA para sintetizar RNA complementar. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 074
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma molécula tem 120 nucleotídeos, sendo 30 adeninas e 30 timinas. Se for DNA dupla fita, quantos nucleotídeos G e C existem ao todo?",
            answers = new string[]
            {
                "30",
                "60",
                "90",
                "120"
            },
            correctIndex = 1,
            questionNumber = 74,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_074",
            topic = "nucleicAcids",
            subtopic = "base_pairing",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "chargaff",
                "nucleotide_count"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere as regras quantitativas de complementaridade entre bases em moléculas de dupla fita. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 075
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma substância impede a formação de ligações de hidrogênio entre bases do DNA. Qual nível estrutural será afetado primeiro?",
            answers = new string[]
            {
                "União das duas fitas complementares",
                "Ligação entre açúcar e fosfato",
                "Formação dos nucleosídeos",
                "Síntese das bases purínicas"
            },
            correctIndex = 0,
            questionNumber = 75,
            answerType = AnswerType.Text,
            questionType = QuestionType.Text,
            questionImagePath = "",
            questionLevel = 2,
            questionInDevelopment = false,
            globalId = "nucleicAcids_075",
            topic = "nucleicAcids",
            subtopic = "dna_structure",
            displayName = "Ácidos Nucleicos",
            bloomLevel = BloomLevel.Analyze,
            conceptTags = new List<string>
            {
                "hydrogen_bonds",
                "double_helix"
            },
            prerequisites = null,
            questionHint = new QuestionHint
            {
                text = "Considere a organização da dupla hélice, a orientação das fitas e as interações entre seus componentes. Separe os dados relevantes, estabeleça relações entre eles e somente depois compare as alternativas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 076
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um laboratório propõe identificar DNA apenas medindo absorvância a 260 nm. Qual avaliação é mais adequada?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere o que cada técnica realmente mede, sua especificidade e os controles necessários. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 077
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Para armazenar informação genética por longo prazo, um pesquisador escolheria DNA em vez de RNA. Qual justificativa é cientificamente mais sólida?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere os fatores químicos e estruturais que alteram a estabilidade da dupla hélice. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 078
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um aluno afirma que maior conteúdo de GC torna o DNA menos estável. Como avaliar essa afirmação?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere os fatores químicos e estruturais que alteram a estabilidade da dupla hélice. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 079
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma equipe usa apenas a presença de uracila para concluir que uma amostra é RNA. Qual limitação deve ser considerada?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere a relação entre estrutura, propriedades e funções dos ácidos nucleicos. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 080
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual estratégia oferece evidência mais convincente de que duas fitas de DNA são complementares?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere as regras quantitativas de complementaridade entre bases em moléculas de dupla fita. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 081
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um modelo didático representa as duas fitas de DNA paralelas no mesmo sentido. Qual julgamento é adequado?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere a organização da dupla hélice, a orientação das fitas e as interações entre seus componentes. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 082
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Para testar a renaturação do DNA, qual desenho experimental é mais apropriado?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere as condições que permitem o reencontro e o pareamento de sequências complementares. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 083
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um estudante diz que o RNAt determina sozinho a ordem dos aminoácidos. Qual avaliação é mais precisa?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere a cooperação entre códons, anticódons, ribossomos e aminoácidos durante a tradução. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 084
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual explicação melhor sustenta o uso de ATP como fonte imediata de energia celular?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere a estrutura do ATP e o acoplamento entre sua hidrólise e o trabalho celular. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 085
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Uma análise conclui que uma molécula é nucleotídeo porque contém apenas açúcar e base. Como avaliar a conclusão?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere a organização química de nucleotídeos, nucleosídeos e seus componentes estruturais. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 086
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Dois métodos estimam concentração de DNA: absorvância a 260 nm e um ensaio fluorescente específico. Qual escolha é mais defensável em amostra contaminada com RNA?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere o que cada técnica realmente mede, sua especificidade e os controles necessários. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 087
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um livro afirma que todas as moléculas de RNA têm apenas função intermediária. Qual crítica é mais adequada?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere as diferentes funções celulares dos RNAs e sua participação na expressão da informação genética. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 088
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Qual evidência melhor apoia que uma amostra sofreu desnaturação, mas não degradação completa?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere as alterações estruturais provocadas por temperatura e as formas experimentais de acompanhá-las. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 089
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Um pesquisador propõe usar RNA para arquivo genético muito duradouro sem proteção adicional. Qual avaliação é mais adequada?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere as diferenças de açúcar, bases, estabilidade e função entre DNA e RNA. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
                imagePath = null,
                videoUrl = null,
                link = null
            }
        },

        //QUESTION 090
        new Question
        {
            questionDatabankName = "NucleicAcidsQuestionDatabase",
            questionText = "Para explicar a fidelidade da replicação, qual argumento é mais completo?",
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
            questionType = QuestionType.Text,
            questionImagePath = "",
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
                text = "Considere a orientação das fitas, a complementaridade e as funções coordenadas das enzimas de replicação. Julgue cada alternativa pela qualidade da justificativa e pelas limitações das evidências apresentadas. Evite decidir por uma palavra isolada: verifique se toda a afirmação permanece compatível com o enunciado e com o nível de organização molecular discutido.",
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

