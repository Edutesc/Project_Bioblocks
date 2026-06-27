using System.Collections.Generic;
using QuestionSystem;

public class AcidBaseBufferQuestionDatabase : IQuestionDatabase
{
    private bool databaseInDevelopment = false;

    private List<Question> questions = new List<Question>
    {
        //// QUESTION 001
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Segundo Arrhenius, o que caracteriza um ácido?",
        //    answers = new string[] {
        //        "Libera íons H+ em solução aquosa.",
        //        "Recebe prótons (H+) em solução aquosa.",
        //        "Libera íons OH- em solução aquosa.",
        //        "Recebe íons OH- em solução aquosa."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 1,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_001",
        //    topic = "acidsBase",
        //    subtopic = "arrhenius_theory",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "acids", "aqueous_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Segundo Arrhenius, a definição foca na água: um ácido é aquela substância química que, quando dissolvida em solução aquosa, sofre ionização e libera íons H+ (ou hidroxônio, H3O+)." }
        //},

        //// QUESTION 002
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Segundo Arrhenius, o que caracteriza uma base?",
        //    answers = new string[] {
        //        "Libera íons H+ em solução aquosa.",
        //        "Recebe prótons (H+) em solução aquosa.",
        //        "Libera íons OH- em solução aquosa.",
        //        "Recebe íons OH- em solução aquosa."
        //    },
        //    correctIndex = 2,
        //    questionNumber = 2,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_002",
        //    topic = "acidsBase",
        //    subtopic = "arrhenius_theory",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "bases", "aqueous_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Para Arrhenius, o caráter básico depende de quem doa hidroxilas. Uma base é qualquer composto que se dissocia em água liberando o ânion OH- (hidroxila)." }
        //},

        //// QUESTION 003
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "De acordo com Brønsted-Lowry, o que é um ácido?",
        //    answers = new string[] {
        //        "Doador de prótons (H+).",
        //        "Receptor de prótons (H+).",
        //        "Doador de íons OH-. ",
        //        "Receptor de íons OH-."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 3,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_003",
        //    topic = "acidsBase",
        //    subtopic = "bronsted_lowry_theory",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "acids", "proton_transfer" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Brønsted e Lowry expandiram a teoria para fora da água. Para eles, um ácido é qualquer molécula ou íon capaz de doar um próton (H+) para outra espécie química durante a reação." }
        //},

        //// QUESTION 004
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "De acordo com Brønsted-Lowry, o que é uma base?",
        //    answers = new string[] {
        //        "Doador de prótons (H+).",
        //        "Receptor de prótons (H+).",
        //        "Doador de íons OH-. ",
        //        "Receptor de íons OH-."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 4,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_004",
        //    topic = "acidsBase",
        //    subtopic = "bronsted_lowry_theory",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "bases", "proton_transfer" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Na visão de Brønsted-Lowry, a base não precisa ter OH-. Ela é definida como qualquer substância química que atua como receptora de um próton (H+) doado por um ácido." }
        //},

        //// QUESTION 005
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A água pode atuar como:",
        //    answers = new string[] {
        //        "Apenas ácido.",
        //        "Apenas base.",
        //        "Tanto ácido quanto base.",
        //        "Nem ácido nem base."
        //    },
        //    correctIndex = 2,
        //    questionNumber = 5,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_005",
        //    topic = "acidsBase",
        //    subtopic = "amphoteric_water",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "bronsted_lowry_theory", "proton_transfer" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Substâncias anfóteras, ou anfotéricas, têm um comportamento duplo. A água, por exemplo, pode doar um H+ agindo como ácido, ou receber um H+ agindo como base, dependendo do pH do meio." }
        //},

        //// QUESTION 006
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O que é a base conjugada do HCl?",
        //    answers = new string[] {
        //        "H^+",
        //        "Cl^-",
        //        "H2O",
        //        "OH^-"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 6,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_006",
        //    topic = "acidsBase",
        //    subtopic = "conjugate_acid_base_pairs",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "bronsted_lowry_theory", "proton_transfer" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O conceito de pares conjugados é fundamental. Quando um ácido (como o HCl) perde seu próton (H+), a espécie resultante (Cl-) tem a capacidade de receber o próton de volta, atuando como base conjugada." }
        //},

        // QUESTION 007
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A figura mostra NH3 recebendo um próton (H^+). Qual espécie corresponde ao ácido conjugado da amônia?",
        //    answers = new string[] {
        //        "H^+",
        //        "OH^-",
        //        "NH4^+",
        //        "NH2^-"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 7,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_007",
        //    topic = "acidsBase",
        //    subtopic = "conjugate_acid_base_pairs",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Analyze,
        //    conceptTags = new List<string> { "bronsted_lowry_theory", "proton_transfer" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Observe o caminho inverso: quando uma base reage e captura um próton (H+), a nova estrutura formada fica protonada e ganha a capacidade de doar esse próton, tornando-se o ácido conjugado." }
        //},

        //// QUESTION 008
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um ácido forte em solução aquosa:",
        //    answers = new string[] {
        //        "Se dissocia parcialmente.",
        //        "Se dissocia completamente.",
        //        "Não se dissocia.",
        //        "Forma ligações de hidrogênio."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 8,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_008",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_strength",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "acid_dissociation" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Um ácido forte (como o HCl ou H2SO4) é aquele que se desintegra completamente na água. Isso significa que quase 100% de suas moléculas sofrem ionização imediata, liberando muitos íons H+." }
        //},

        //// QUESTION 009
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um ácido fraco em solução aquosa:",
        //    answers = new string[] {
        //        "Se dissocia completamente.",
        //        "Se dissocia parcialmente.",
        //        "Não se dissocia.",
        //        "Forma ligações iônicas."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 9,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_009",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_strength",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "acid_dissociation", "equilibrium_constants" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Diferente dos ácidos fortes, os ácidos fracos não se ionizam por completo. Eles estabelecem um equilíbrio dinâmico, onde a maior parte da molécula continua intacta (não dissociada) na solução." }
        //},

        //// QUESTION 010
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A constante de equilíbrio (Keq) de uma reação indica:",
        //    answers = new string[] {
        //        "A velocidade da reação.",
        //        "A proporção de reagentes e produtos no equilíbrio.",
        //        "A energia de ativação da reação.",
        //        "A concentração dos reagentes."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 10,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_010",
        //    topic = "acidsBase",
        //    subtopic = "equilibrium_constants",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "chemical_equilibrium" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A constante de equilíbrio (Keq) quantifica o estado final de uma reação reversível. Ela nos dá a exata proporção matemática entre as concentrações dos produtos e as dos reagentes quando a reação se estabiliza." }
        //},

        // QUESTION 011
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A figura mostra uma reação com Keq < 1. O que isso indica sobre a composição no equilíbrio?",
        //    answers = new string[] {
        //        "Há mais reagentes do que produtos.",
        //        "Há mais produtos do que reagentes.",
        //        "Reagentes e produtos estão em quantidades iguais.",
        //        "A reação ocorre apenas no sentido direto."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 11,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_011",
        //    topic = "acidsBase",
        //    subtopic = "chemical_equilibrium",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Analyze,
        //    conceptTags = new List<string> { "equilibrium_constants" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Se a constante Keq é menor que 1, significa que o numerador (produtos) é menor que o denominador (reagentes). Portanto, o equilíbrio tende fortemente à esquerda, não ocorrendo dissociação total." }
        //},

        // QUESTION 012
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A figura mostra uma reação com Keq > 1. O que isso indica sobre a composição no equilíbrio?",
        //    answers = new string[] {
        //        "Há mais reagentes do que produtos.",
        //        "Há mais produtos do que reagentes.",
        //        "Reagentes e produtos estão em quantidades iguais.",
        //        "A reação ocorre apenas no sentido inverso."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 12,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_012",
        //    topic = "acidsBase",
        //    subtopic = "chemical_equilibrium",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Analyze,
        //    conceptTags = new List<string> { "equilibrium_constants" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Se Keq é bem maior que 1, a reação é muito favorável no sentido direto. Isso indica que quase todos os reagentes foram convertidos e o sistema se estabiliza com alta concentração de produtos." }
        //},

        // QUESTION 013
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A figura mostra a expressão de Ka para a dissociação de um ácido fraco. O que o valor de Ka permite analisar?",
        //    answers = new string[] {
        //        "A força da base.",
        //        "A força do ácido.",
        //        "A velocidade de uma reação ácida.",
        //        "A velocidade de uma reação básica."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 13,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_013",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_strength",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Analyze,
        //    conceptTags = new List<string> { "ka_pka", "equilibrium_constants" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A constante de acidez (Ka) é a medida oficial da força de um ácido. Trata-se da constante de equilíbrio aplicada especificamente para a reação de dissociação do ácido em água." }
        //},

        // QUESTION 014
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A figura mostra a expressão de Kb para a reação de uma base fraca com água. O que o valor de Kb permite analisar?",
        //    answers = new string[] {
        //        "A força da base.",
        //        "A força do ácido.",
        //        "A velocidade de uma reação ácida.",
        //        "A velocidade de uma reação básica."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 14,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_014",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_strength",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Analyze,
        //    conceptTags = new List<string> { "base_strength", "equilibrium_constants" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Assim como o Ka mede ácidos, o Kb (constante de basicidade) mede bases fracas. Ele reflete a constante de equilíbrio na reação em que a base captura um próton da água gerando OH-." }
        //},

        //// QUESTION 015
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um ácido fraco tem um valor de Ka:",
        //    answers = new string[] {
        //        "Alto",
        //        "Baixo",
        //        "Próximo a 1",
        //        "Próximo a 0"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 15,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_015",
        //    topic = "acidsBase",
        //    subtopic = "ka_pka",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "acid_base_strength", "equilibrium_constants" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Um Ka muito baixo indica que o numerador (produtos iônicos dissociados) é minúsculo. Isso comprova experimentalmente que poucas moléculas se ionizaram, característica de um ácido fraco." }
        //},
        //// QUESTION 016
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O pKa de um ácido é definido como:",
        //    answers = new string[] {
        //        "log Ka",
        //        "-log Ka",
        //        "1/Ka",
        //        "10/Ka"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 16,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_016",
        //    topic = "acidsBase",
        //    subtopic = "ka_pka",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "acid_base_strength", "ph_calculations" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Por uma questão matemática para evitar usar potências de 10 negativas, definimos o pKa como sendo o logaritmo negativo da constante de dissociação ácida: pKa = -log Ka." }
        //},
        //// QUESTION 017
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um ácido com um pKa baixo é:",
        //    answers = new string[] {
        //        "Fraco",
        //        "Forte",
        //        "De força moderada",
        //        "Indeterminado"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 17,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_017",
        //    topic = "acidsBase",
        //    subtopic = "ka_pka",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "acid_base_strength" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O 'p' indica o uso de logaritmo negativo. Por conta dessa inversão matemática, quanto mais alto for o valor do Ka (ácido mais forte), menor e mais negativo será o valor numérico do pKa." }
        //},
        //// QUESTION 018
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um ácido com um pKa alto é:",
        //    answers = new string[] {
        //        "Forte",
        //        "Fraco",
        //        "De força moderada",
        //        "Indeterminado"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 18,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_018",
        //    topic = "acidsBase",
        //    subtopic = "ka_pka",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "acid_base_strength" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Um valor alto de pKa significa que o Ka daquele ácido é muito pequeno (ex: 10^-8). Logo, um pKa alto indica que o ácido não gosta de doar seu próton, sendo classificado como fraco." }
        //},
        //// QUESTION 019
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A equação de Henderson-Hasselbalch relaciona:",
        //    answers = new string[] {
        //        "pH, pKa e a razão entre base conjugada e ácido.",
        //        "pH, pKa e a concentração de íons H+",
        //        "pH, pOH e a concentração de íons OH-",
        //        "pKa, pKb e a concentração de íons H+"
        //    },
        //    correctIndex = 0,
        //    questionNumber = 19,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_019",
        //    topic = "acidsBase",
        //    subtopic = "henderson_hasselbalch",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "buffers", "ka_pka", "ph_calculations" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A famosa equação de Henderson-Hasselbalch é usada justamente para cálculos de sistemas tampão. Ela correlaciona o pH do ambiente com o pKa do ácido e a proporção entre a forma dissociada e não dissociada." }
        //},
        //// QUESTION 020
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Em uma solução-tampão, o pH permanece relativamente constante porque:",
        //    answers = new string[] {
        //        "O ácido se dissocia completamente.",
        //        "A base se dissocia completamente.",
        //        "Há um equilíbrio entre ácido e sua base conjugada.",
        //        "Não há interações entre o ácido e a base."
        //    },
        //    correctIndex = 2,
        //    questionNumber = 20,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_020",
        //    topic = "acidsBase",
        //    subtopic = "buffers",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "conjugate_acid_base_pairs", "chemical_equilibrium" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "As soluções-tampão resistem a variações de pH. Elas funcionam por conter tanto um ácido fraco (que neutraliza OH- intruso) quanto sua base conjugada (que neutraliza H+ intruso) em altas concentrações." }
        //},
        //// QUESTION 021
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A faixa de tamponamento de uma solução-tampão é:",
        //    answers = new string[] {
        //        "Muito menor que o pKa.",
        //        "Igual ao pKa.",
        //        "Aproximadamente ± 1 unidade de pH em relação ao pKa.",
        //        "Muito maior que o pKa."
        //    },
        //    correctIndex = 2,
        //    questionNumber = 21,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_021",
        //    topic = "acidsBase",
        //    subtopic = "buffers",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "ka_pka", "henderson_hasselbalch" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Um tampão funciona melhor quando a concentração de ácido é igual à de base, momento em que o pH é igual ao pKa. Na prática, a faixa de proteção tamponante é de +1 ou -1 unidade em volta do pKa." }
        //},
        //// QUESTION 022
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O pH do sangue é mantido constante principalmente pelo sistema tampão:",
        //    answers = new string[] {
        //        "Fosfato",
        //        "Acetato",
        //        "Bicarbonato",
        //        "Tris"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 22,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_022",
        //    topic = "acidsBase",
        //    subtopic = "blood_buffer_system",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "buffers", "bicarbonate_buffer", "acid_base_homeostasis" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O plasma humano precisa de uma resposta rápida. Esse papel recai sobre o tampão bicarbonato (H2CO3 / HCO3-), pois o ácido carbônico pode ser rapidamente convertido e expelido como gás carbônico pelos pulmões." }
        //},
        //// QUESTION 023
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O que acontece com o pH do sangue durante o exercício intenso?",
        //    answers = new string[] {
        //        "Aumenta.",
        //        "Diminui.",
        //        "Permanece constante.",
        //        "Varia de forma imprevisível."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 23,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_023",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_homeostasis",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "blood_buffer_system", "lactic_acid" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Em atividades anaeróbicas intensas, as células musculares lançam ácido lático na circulação. A dissociação desse ácido gera uma carga massiva de H+, o que derruba o pH do sangue (acidose)." }
        //},
        //// QUESTION 024
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Como o corpo responde à diminuição do pH sangüíneo durante o exercício?",
        //    answers = new string[] {
        //        "Diminui a taxa respiratória.",
        //        "Aumenta a taxa respiratória.",
        //        "Mantém a taxa respiratória constante.",
        //        "Para de respirar."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 24,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_024",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_homeostasis",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "blood_buffer_system", "respiratory_compensation" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O CO2 é o equivalente gasoso do ácido. Quando você respira mais rápido (hiperventilação), o corpo 'sopra' o CO2 embora. Isso desloca o equilíbrio, consome os íons H+ sobrando e faz o pH subir de volta." }
        //},
        //// QUESTION 025
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O que é pH?",
        //    answers = new string[] {
        //        "Uma medida da concentração de OH-",
        //        "Uma medida da concentração de H+",
        //        "Uma medida da temperatura",
        //        "Uma medida da pressão"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 25,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_025",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "hydrogen_ion_concentration" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O termo pH significa 'potencial Hidrogeniônico'. É a régua universal da química que mede a concentração exata de íons de hidrogênio livres em uma solução aquosa." }
        //},
        //// QUESTION 026
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma solução com pH 3 é:",
        //    answers = new string[] {
        //        "Neutra",
        //        "Básica",
        //        "Ácida",
        //        "Tampão"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 26,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_026",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "acids" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Lembrando da escala padrão em água a 25 °C (de 0 a 14), o ponto central neutro é 7. Qualquer valor inferior a isso representa uma abundância de íons H+, caracterizando um ambiente ácido." }
        //},
        //// QUESTION 027
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma solução com pH 11 é:",
        //    answers = new string[] {
        //        "Ácida",
        //        "Neutra",
        //        "Básica",
        //        "Tampão"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 27,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_027",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "bases" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Na mesma escala de 0 a 14, se o pH for superior a 7, os íons hidroxila (OH-) estão em maior número que os de hidrogênio. Isso define quimicamente uma solução básica ou alcalina." }
        //},
        //// QUESTION 028
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma solução com pH 7 é:",
        //    answers = new string[] {
        //        "Ácida",
        //        "Neutra",
        //        "Básica",
        //        "Tampão"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 28,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_028",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "neutral_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O pH é 7 (neutro) quando o sistema atinge o equilíbrio perfeito, em que as concentrações molares de hidrogênio (H+) e hidroxila (OH-) estão em quantidades rigorosamente iguais na água." }
        //},
        //// QUESTION 029
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O pOH de uma solução é uma medida de:",
        //    answers = new string[] {
        //        "Concentração de H+",
        //        "Concentração de OH-",
        //        "Acidez",
        //        "Basicidade"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 29,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_029",
        //    topic = "acidsBase",
        //    subtopic = "poh_kw",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "hydroxide_ion_concentration" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A mesma lógica logarítmica se aplica ao OH-. O pOH (potencial hidroxiliônico) expressa a acidez do lado das bases, sendo uma medida direta da concentração de íons OH-." }
        //},
        //// QUESTION 030
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A relação entre pH e pOH é:",
        //    answers = new string[] {
        //        "pH + pOH = 0",
        //        "pH + pOH = 7",
        //        "pH + pOH = 14",
        //        "pH + pOH = 21"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 30,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_030",
        //    topic = "acidsBase",
        //    subtopic = "poh_kw",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "ph_scale", "ph_calculations" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Na autoionização da água sob temperatura de 25 °C, existe uma relação matemática fechada de Kw: a soma numérica entre o pH e o pOH de qualquer solução será sempre e obrigatoriamente 14." }
        //},
        // QUESTION 031
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Qual é o valor de Kw, o produto iônico da água, a 25 °C?",
        //    answers = new string[] {
        //        "10^{-7} ",
        //        "10^{-14} ",
        //        "10^{0} ",
        //        "10^{14} "
        //    },
        //    correctIndex = 1,
        //    questionNumber = 31,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_031",
        //    topic = "acidsBase",
        //    subtopic = "poh_kw",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "water_autoionization" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A constante Kw representa o produto iônico da água. Na temperatura de 25 °C, a multiplicação das concentrações [H+] x [OH-] gera sempre o valor constante de 10 elevado a -14." }
        //},
        // //QUESTION 032
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Em água pura a 25 °C, se Kw = 1×10^-14 e [H^+] = [OH^-], qual é a concentração de H^+?",
        //    answers = new string[] {
        //        "10^{-14} M",
        //        "10^{-7} M",
        //        "10^{0} M",
        //        "10^{7} M"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 32,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_032",
        //    topic = "acidsBase",
        //    subtopic = "ph_calculations",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "ph_scale", "hydrogen_ion_concentration" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Se a solução for pura e neutra, [H+] e [OH-] devem ser idênticos. Como o produto deles dá 10^-14, a única possibilidade matemática é que cada um seja igual a 10^-7 molar." }
        //},
        //// QUESTION 033
        ////new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Em água pura a 25 °C, se Kw = 1×10^-14 e [H^+] = [OH^-], qual é a concentração de OH^-?",
        //    answers = new string[] {
        //        "10^{-14} M",
        //        "10^{-7} M",
        //        "10^{0} M",
        //        "10^{7} M"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 33,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_033",
        //    topic = "acidsBase",
        //    subtopic = "poh_kw",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "hydroxide_ion_concentration", "water_autoionization" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O mesmo raciocínio do H+ se aplica ao ânion hidroxila: na neutralidade pura a 25 °C, não há predomínio, e a concentração de OH- será de 10^-7 mol por litro." }
        //},
        //// QUESTION 034
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Qual a fórmula para calcular o pH?",
        //    answers = new string[] {
        //        "pH = log[H^+]",
        //        "pH = -log[H^+]",
        //        "pH = log[OH^-]",
        //        "pH = -log[OH^-]"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 34,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_034",
        //    topic = "acidsBase",
        //    subtopic = "ph_calculations",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "ph_scale", "hydrogen_ion_concentration" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A definição estrita e oficial do pH introduzida por Sørensen é a base do logaritmo decimal: pH = -log10[H+], onde os colchetes indicam a concentração molar do hidrogênio." }
        //},
        //// QUESTION 035
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Qual a fórmula para calcular o pOH?",
        //    answers = new string[] {
        //        "pOH = -log[OH^-]",
        //        "pOH = log[OH^-]",
        //        "pOH = -log[OH^+]",
        //        "pOH = log[OH^+]"
        //    },
        //    correctIndex = 0,
        //    questionNumber = 35,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_035",
        //    topic = "acidsBase",
        //    subtopic = "ph_calculations",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "poh_kw", "hydroxide_ion_concentration" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Igualmente à definição de Sørensen para o ácido, o pOH é calculado aplicando-se o logaritmo negativo na base 10 sobre a concentração dos íons hidroxila: pOH = -log10[OH-]." }
        //},
        // QUESTION 036
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Na escala didática usual de pH entre 0 e 14, qual valor representa a extremidade mais ácida?",
        //    answers = new string[] {
        //        "0",
        //        "7",
        //        "14",
        //        "-14"
        //    },
        //    correctIndex = 0,
        //    questionNumber = 36,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_036",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string>(),
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Se usarmos concentrações molares de referência (como 1 mol/L para ácidos fortes), a escala vai bater no limite teórico inferior de 0 para a acidez máxima rotineira." }
        //},
        //// QUESTION 037
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Na escala didática usual de pH entre 0 e 14, qual valor representa a extremidade mais básica?",
        //    answers = new string[] {
        //        "0",
        //        "7",
        //        "14",
        //        "-14"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 37,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_037",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string>(),
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Na outra ponta dessa escala de referência usual baseada em 1 mol/L (ex: hidróxido de sódio concentrado), o pH máximo da basicidade rotineira atingirá o topo marcando 14." }
        //},
        //// QUESTION 038
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma solução aquosa a 25 °C apresenta [H+] = 1×10^-7 M. Qual é o pH dessa solução?",
        //    answers = new string[] {
        //        "0",
        //        "7",
        //        "14",
        //        "Variavel"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 38,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_038",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "neutral_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O número 7 é o marco exato no meio da escala logarítmica e atesta a neutralidade para soluções aquosas comuns submetidas a 25 °C de temperatura ambiente." }
        //},
        //// QUESTION 039
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um peagâmetro indica pH 6,2 para uma amostra aquosa a 25 °C. Como essa solução deve ser classificada?",
        //    answers = new string[] {
        //        "Neutra",
        //        "Básica",
        //        "Ácida",
        //        "Tampão"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 39,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_039",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "acids" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Para facilitar o dia a dia laboratorial: viu um valor medido no peagômetro que cravou abaixo de 7? Não tenha dúvida de que há excesso de prótons e a solução é ácida." }
        //},
        // QUESTION 040
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um peagâmetro indica pH 8,5 para uma amostra aquosa a 25 °C. Como essa solução deve ser classificada?",
        //    answers = new string[] {
        //        "Ácida",
        //        "Neutra",
        //        "Básica",
        //        "Tampão"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 40,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_040",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "bases" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Da mesma forma, qualquer leitura eletrônica que entregue um número superior a 7 atesta um excesso de hidroxilas (OH-), revelando que o meio está sob domínio básico." }
        //},
        //// QUESTION 041
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O processo de neutralização envolve:",
        //    answers = new string[] {
        //        "A adição de um ácido a uma base.",
        //        "A adição de uma base a um ácido.",
        //        "A reação entre um ácido e uma base, resultando em água e um sal.",
        //        "Todas as alternativas anteriores."
        //    },
        //    correctIndex = 2,
        //    questionNumber = 41,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_041",
        //    topic = "acidsBase",
        //    subtopic = "neutralization",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "acids", "bases", "salts" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "As reações de neutralização são aquelas que anulam a agressividade das partes envolvidas: os H+ do ácido encontram os OH- da base, gerando moléculas de água e um sal dissolvido." }
        //},
        // QUESTION 042
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Durante uma titulação ácido-base, o ponto de equivalência é atingido quando:",
        //    answers = new string[] {
        //        "A quantidade estequiométrica de ácido e base reagiu.",
        //        "O pH é sempre igual a 0.",
        //        "O pH é sempre igual a 7.",
        //        "O pH é sempre igual a 14."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 42,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_042",
        //    topic = "acidsBase",
        //    subtopic = "titration",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "neutralization", "stoichiometry" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Em uma titulação, a técnica exige gotejar cuidadosamente um titulante. Quando chegamos ao exato ponto de equivalência, significa que todos os mols da base anularam exatamente os mols do ácido original." }
        //},
        //// QUESTION 043
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um indicador de pH é uma substância que:",
        //    answers = new string[] {
        //        "Muda de cor em um determinado intervalo de pH.",
        //        "Muda de cor em qualquer pH.",
        //        "Mantém o pH constante.",
        //        "Neutraliza ácidos e bases."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 43,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_043",
        //    topic = "acidsBase",
        //    subtopic = "indicators",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "ph_scale" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Os indicadores (como fenolftaleína) não são enfeites. Eles são ácidos ou bases orgânicas gigantes e fracas, cujas estruturas moleculares dobram e mudam de cor radicalmente ao ganharem ou perderem um H+." }
        //},
        //// QUESTION 044
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O que é uma solução-tampão?",
        //    answers = new string[] {
        //        "Uma solução que resiste a mudanças de temperatura.",
        //        "Uma solução que resiste a mudanças de pressão.",
        //        "Uma solução que resiste a mudanças de pH.",
        //        "Uma solução que resiste a mudanças de volume."
        //    },
        //    correctIndex = 2,
        //    questionNumber = 44,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_044",
        //    topic = "acidsBase",
        //    subtopic = "buffers",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "ph_scale" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Um sistema tampão atua como uma 'esponja' química. Se você adicionar ácido, ele absorve. Se adicionar base, ele doa H+. Isso resulta em uma forte resistência contra mudanças bruscas de pH." }
        //},
        //// QUESTION 045
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma solução-tampão é tipicamente composta de:",
        //    answers = new string[] {
        //        "Um ácido forte e uma base forte.",
        //        "Um ácido fraco e sua base conjugada.",
        //        "Um ácido forte e sua base conjugada.",
        //        "Um ácido fraco e uma base forte."
        //    },
        //    correctIndex = 1,
        //    questionNumber = 45,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_045",
        //    topic = "acidsBase",
        //    subtopic = "buffers",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "conjugate_acid_base_pairs", "weak_acids" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Você não pode montar um tampão misturando ácido forte, pois ele reage sem retorno. A química do tampão exige moléculas reversíveis, ou seja, um ácido ou base fraca combinado com seu sal conjugado." }
        //},
        //// QUESTION 046
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A capacidade de tamponamento de uma solução-tampão é máxima em:",
        //    answers = new string[] {
        //        "pH = 0",
        //        "pH = 7",
        //        "pH = pKa",
        //        "pH = 14"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 46,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_046",
        //    topic = "acidsBase",
        //    subtopic = "buffers",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "ka_pka", "henderson_hasselbalch" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Analise a equação: pH = pKa + log([A-]/[HA]). Se as concentrações do ácido e da base forem iguais, o termo log se torna log(1), que vale zero. O resultado é o pico da eficiência: pH igual ao pKa." }
        //},
        // QUESTION 047
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Um tampão com pKa = 6,8 será usado em uma solução que deve permanecer em pH 8,5. Qual avaliação é mais adequada?",
        //    answers = new string[] {
        //        "Está fora da faixa ideal e terá baixa eficiência.",
        //        "Está exatamente no ponto de máxima eficiência.",
        //        "Funcionará melhor quanto mais distante estiver do pKa.",
        //        "Será eficiente apenas se o ácido for forte."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 47,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_047",
        //    topic = "acidsBase",
        //    subtopic = "buffers",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Analyze,
        //    conceptTags = new List<string> { "ka_pka", "henderson_hasselbalch" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Enquanto o bicarbonato toma conta do sangue extracelular, o interior de nossas células usa o sistema tampão fosfato (HPO4 / H2PO4). Seu pKa próximo a 6.8 o torna ideal para a proteção do citosol." }
        //},
        //// QUESTION 048
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Qual a principal função do sistema tampão do sangue?",
        //    answers = new string[] {
        //        "Regular a temperatura corporal",
        //        "Manter o pH do sangue constante",
        //        "Regular a pressão sanguínea",
        //        "Transportar oxigênio"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 48,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_048",
        //    topic = "acidsBase",
        //    subtopic = "blood_buffer_system",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "buffers", "acid_base_homeostasis" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A ventilação excessiva dos pulmões funciona como uma bomba sugadora de CO2. Removendo esse componente, o ácido carbônico se converte de volta, o H+ sanguíneo desaparece, e o pH sobe na alcalose." }
        //},
        // QUESTION 049
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "A reação CO2 + H2O ⇌ H2CO3 ⇌ H^+ + HCO3^- representa principalmente qual sistema tampão do sangue?",
        //    answers = new string[] {
        //        "Fosfato",
        //        "Acetato",
        //        "Bicarbonato",
        //        "Hemoglobina"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 49,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 3,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_049",
        //    topic = "acidsBase",
        //    subtopic = "blood_buffer_system",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Analyze,
        //    conceptTags = new List<string> { "buffers" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Se ocorre uma hipoventilação pulmonar (asma grave, por exemplo), o CO2 se acumula e força a água a criar ácido carbônico no sangue. O banho de novos íons H+ abaixa o pH gerando acidose." }
        //},
        //// QUESTION 050
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Durante o exercício intenso, o aumento da produção de ácido lático causa:",
        //    answers = new string[] {
        //        "Aumento do pH do sangue",
        //        "Diminuição do pH do sangue",
        //        "Aumento da taxa respiratória",
        //        "Diminuição da taxa respiratória"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 50,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_050",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_homeostasis",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "blood_buffer_system", "lactic_acid", "ph_scale" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Acidose metabólica é sistêmica e não tem culpa primária no pulmão. Ela ocorre quando perdemos as reservas de bicarbonato nos rins, ou quando produzimos muito lixo ácido no metabolismo." }
        //},
        //// QUESTION 051
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Segundo Arrhenius, um ácido é toda substância que, em solução aquosa libera:",
        //    answers = new string[] {
        //        "OH^⁻",
        //        "H^⁺ (prótons)",
        //        "Na^⁺",
        //        "Cl^⁻"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 51,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_051",
        //    topic = "acidsBase",
        //    subtopic = "arrhenius_theory",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "acids", "aqueous_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Já a alcalose metabólica pode ocorrer após crises de vômito severas, onde o estômago ejeta todo o seu conteúdo ácido, deixando o resto do organismo subitamente rico em excessos de bases." }
        //},
        //// QUESTION 052
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Segundo Arrhenius, uma base é toda substância que em solução aquosa libera:",
        //    answers = new string[] {
        //        "H⁺",
        //        "OH⁻",
        //        "CO₂",
        //        "O₂"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 52,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_052",
        //    topic = "acidsBase",
        //    subtopic = "arrhenius_theory",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "bases", "aqueous_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Termodinâmica importa: as constantes de dissociação (pKa, Ka, Kw) foram padronizadas na literatura. Qualquer variação de temperatura muda esses números. A maioria das tabelas crava o padrão em 25 °C." }
        //},
        // QUESTION 053
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma solução apresenta [H^+] = 1×10^-4 M. Qual é o pH dessa solução?",
        //    answers = new string[] {
        //        "3",
        //        "4",
        //        "7",
        //        "10"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 53,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_053",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "hydrogen_ion_concentration" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Aqui é pura matemática. Se a concentração [H+] for igual a 10^(-4) mol/L, usando a fórmula pH = -log[H+], o próprio expoente nos dará o número 4 diretamente como pH." }
        //},
        //// QUESTION 054
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma amostra apresenta pH = 5. Como ela deve ser classificada?",
        //    answers = new string[] {
        //        "Neutra",
        //        "Ácida",
        //        "Básica",
        //        "Isotônica"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 54,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_054",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "acids" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Fórmula reversa: como pH é o logaritmo negativo da concentração mola, para um pH batendo a marca de 9, a verdadeira concentração em mols de hidrogênio livres será 10 elevado a -9 M." }
        //},
        //// QUESTION 055
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma amostra apresenta pH = 9. Como ela deve ser classificada?",
        //    answers = new string[] {
        //        "Ácida",
        //        "Neutra",
        //        "Básica",
        //        "Saturada"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 55,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_055",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "bases" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "A escala de pH não é linear, ela é logarítmica (potências de 10). Portanto, pular uma unidade (ex: de 5 para 6) significa que os íons reduziram exatamente em uma escala de 10 vezes." }
        //},
        //// QUESTION 056
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Uma solução a 25 °C tem pOH = 8. Qual é seu pH?",
        //    answers = new string[] {
        //        "4",
        //        "6",
        //        "7",
        //        "8"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 56,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_056",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "neutral_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Lembre-se da regra de ouro do equilíbrio: pH + pOH = 14. Se você obteve pOH = 8, então a conta 14 - 8 te mostrará imediatamente que o pH real daquela mistura será 6." }
        //},
        //// QUESTION 057
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Após pequena adição de HCl a uma solução, o pH quase não se altera. Qual função do tampão foi observada?",
        //    answers = new string[] {
        //        "Regular a temperatura corporal",
        //        "Transportar oxigênio",
        //        "Manter o pH estável",
        //        "Produzir energia imediata"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 57,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_057",
        //    topic = "acidsBase",
        //    subtopic = "buffers",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "acid_base_homeostasis" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Um ácido forte derrama 100% dos seus mols como íons de H+. Se a solução era de 0,1 Molar, o sistema recebe 10^-1 de hidrogênio livres. O cálculo do log negativo te dá pH = 1." }
        //},
        //// QUESTION 058
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Qual par ácido/base conjugado é mais adequado para representar o tampão bicarbonato do sangue?",
        //    answers = new string[] {
        //        "Glicose/Insulina",
        //        "Hemoglobina/O₂",
        //        "H₂CO₃/HCO₃^⁻ (ácido carbônico/bicarbonato)",
        //        "DNA/RNA"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 58,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 2,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_058",
        //    topic = "acidsBase",
        //    subtopic = "blood_buffer_system",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "buffers", "bicarbonate_buffer" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "NaOH joga bases fortíssimas. 0,01 Molar gera 10^-2 mols de OH-. Isso reflete um pOH igual a 2. Para fechar o ciclo de 14, subtraímos esse valor e confirmamos um pH altamente básico de 12." }
        //},
        //// QUESTION 059
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Por que uma solução com alta concentração de OH^- é classificada como básica?",
        //    answers = new string[] {
        //        "Porque há predomínio de OH^- em relação a H^+.",
        //        "Porque todo OH^- é um ácido forte.",
        //        "Porque o pH necessariamente é igual a 7.",
        //        "Porque não há íons na solução."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 59,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_059",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "bases", "hydroxide_ion_concentration" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O comportamento neutro se altera! Em temperaturas muito mais quentes, as moléculas da água tremem e sofrem maior autoionização (aumenta o Kw), fazendo o ponto da neutralidade cair para algo como 6.1." }
        //},
        //// QUESTION 060
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Por que indicadores como fenolftaleína ou papel de tornassol são úteis em testes ácido-base?",
        //    answers = new string[] {
        //        "Porque mudam de cor em faixas específicas de pH.",
        //        "Porque neutralizam qualquer ácido.",
        //        "Porque mantêm o pH constante.",
        //        "Porque impedem a dissociação da água."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 60,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_060",
        //    topic = "acidsBase",
        //    subtopic = "indicators",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "ph_scale", "acids", "bases" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Ter proporções iguais (pH = pKa) gera eficácia, mas não diz o limite. A verdadeira capacidade de resistir (quanto ácido posso jogar?) dependerá das concentrações absolutas em mols dos componentes." }
        //},
        //// QUESTION 061
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Qual substância abaixo, ao se dissolver em água, libera OH- e pode ser classificada como base de Arrhenius?",
        //    answers = new string[] {
        //        "HCl",
        //        "NaOH",
        //        "CO₂",
        //        "H₂SO₄"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 61,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_061",
        //    topic = "acidsBase",
        //    subtopic = "arrhenius_theory",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "bases", "aqueous_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Para fazer do bicarbonato um tampão genial, o sangue possui os glóbulos vermelhos lotados com uma das enzimas mais rápidas que a natureza inventou: a anidrase carbônica." }
        //},
        //// QUESTION 062
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Na reação NH4^+ ⇌ NH3 + H^+, como é classificado o par NH4^+/NH3?",
        //    answers = new string[] {
        //        "Par conjugado",
        //        "Par isotópico",
        //        "Par redox",
        //        "Par covalente"
        //    },
        //    correctIndex = 0,
        //    questionNumber = 62,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_062",
        //    topic = "acidsBase",
        //    subtopic = "conjugate_acid_base_pairs",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Apply,
        //    conceptTags = new List<string> { "bronsted_lowry_theory" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Os rins são a resposta lenta e teimosa do corpo. Eles compensam anomalias metabólicas e respiratórias longas secretando H+ diretamente na urina e conservando valioso bicarbonato de volta ao sangue." }
        //},
        //// QUESTION 063
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Qual o pH de uma solução neutra a 25 °C?",
        //    answers = new string[] {
        //        "0",
        //        "7",
        //        "14",
        //        "10"
        //    },
        //    correctIndex = 1,
        //    questionNumber = 63,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_063",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "neutral_solutions" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Os pulmões, ao invés de excretarem íons líquidos, resolvem a acidez soprando-a para o ambiente na forma de gás carbônico. É o ajuste de curto prazo mais potente que temos." }
        //},
        //// QUESTION 064
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Se o pOH de uma solução a 25 °C é 5, qual é seu pH?",
        //    answers = new string[] {
        //        "5",
        //        "7",
        //        "9",
        //        "11"
        //    },
        //    correctIndex = 2,
        //    questionNumber = 64,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_064",
        //    topic = "acidsBase",
        //    subtopic = "ph_calculations",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "ph_scale" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Enquanto o corpo luta para estabilizar o plasma alcalino em ~7.4, toda a porcaria ácida não volátil tem que ir para algum lugar. Por isso, as reações renais forçam a urina a ser tipicamente mais ácida (pH ~6)." }
        //},
        //// QUESTION 065
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Por que o HCl é classificado como ácido forte em água?",
        //    answers = new string[] {
        //        "Porque se ioniza quase completamente.",
        //        "Porque recebe H^+ da água.",
        //        "Porque libera OH^-.",
        //        " Porque tem pH sempre igual a 7."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 65,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_065",
        //    topic = "acidsBase",
        //    subtopic = "acid_base_strength",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "strong_acids", "arrhenius_theory" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Um ácido poliprótico possui múltiplos hidrogênios desprendíveis em sua fórmula (como o fosfórico H3PO4). Eles se ionizam em estágios e cada molécula ejetada de H+ apresenta um pKa exclusivo." }
        //},
        //// QUESTION 066
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "Por que uma solução com pH = 12 apresenta caráter básico?",
        //    answers = new string[] {
        //        "Porque pH maior que 7 indica predomínio relativo de OH^-.",
        //        "Porque pH maior que 7 indica excesso de H^+. ",
        //        "Porque pH 12 é neutro a 25 °C.",
        //        "Porque pH não se relaciona com acidez."
        //    },
        //    correctIndex = 0,
        //    questionNumber = 66,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_066",
        //    topic = "acidsBase",
        //    subtopic = "ph_scale",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Understand,
        //    conceptTags = new List<string> { "bases" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "Quanto mais você tira prótons de um ácido poliprótico, mais a carga negativa da molécula segura os prótons remanescentes. Ou seja, ionizações seguintes requerem muito mais força, logo seu pKa aumenta muito." }
        //},
        //// QUESTION 067
        //new Question
        //{
        //    questionDatabankName = "AcidBaseBufferQuestionDatabase",
        //    questionText = "O produto iônico da água a 25 °C (Kw) é:",
        //    answers = new string[] {
        //        "1 × 10⁻¹⁴",
        //        "1 × 10⁻⁷",
        //        "1 × 10⁻¹",
        //        "1 × 10⁻¹⁰"
        //    },
        //    correctIndex = 0,
        //    questionNumber = 67,
        //    answerType = AnswerType.Text,
        //    questionType = QuestionType.Text,
        //    questionImagePath = "",
        //    questionLevel = 1,
        //    questionInDevelopment = false,
        //    globalId = "acidsBase_067",
        //    topic = "acidsBase",
        //    subtopic = "poh_kw",
        //    displayName = "Ácidos, Bases e Tampões",
        //    bloomLevel = BloomLevel.Remember,
        //    conceptTags = new List<string> { "water_autoionization" },
        //    prerequisites = null,
        //    questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        //}
        // QUESTION 068
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Você precisa preparar um tampão para uma enzima que atua melhor em pH 7,4. Qual opção é mais adequada?",
           answers = new string[] {
               "Ácido acético/acetato, pKa 4,8",
               "Fosfato, pKa próximo de 7,2",
               "Amônia/amônio, pKa 9,2",
               "HCl/Cl^-, ácido forte"
           },
           correctIndex = 1,
           questionNumber = 68,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_068",
           topic = "acidsBase",
           subtopic = "buffers",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 069
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Em uma titulação, a curva apresenta região de tamponamento antes do ponto de equivalência e pH de equivalência maior que 7. Que tipo de sistema é mais compatível com essa curva?",
           answers = new string[] {
               "Ácido forte titulado com base forte",
               "Ácido fraco titulado com base forte",
               "Base forte titulada com ácido forte",
               "Água pura titulada com água"
           },
           correctIndex = 1,
           questionNumber = 69,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_069",
           topic = "acidsBase",
           subtopic = "titration",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 070
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Um paciente apresenta pH sanguíneo baixo e concentração elevada de CO2. Qual interpretação é mais coerente?",
           answers = new string[] {
               "Acidose respiratória por retenção de CO2",
               "Alcalose respiratória por perda de CO2",
               "Acidose metabólica por perda de bicarbonato",
               "Alcalose metabólica por excesso de bicarbonato"
           },
           correctIndex = 0,
           questionNumber = 70,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_070",
           topic = "acidsBase",
           subtopic = "acid_base_homeostasis",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 071
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Durante exercício intenso, há aumento de H^+ no sangue. Qual resposta ajuda a deslocar o equilíbrio do tampão bicarbonato para reduzir a acidez?",
           answers = new string[] {
               "Aumentar a ventilação para eliminar CO2",
               "Diminuir a ventilação para reter CO2 ",
               "Bloquear a formação de bicarbonato ",
               "Impedir a saída de CO2 pelos pulmões"
           },
           correctIndex = 0,
           questionNumber = 71,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_071",
           topic = "acidsBase",
           subtopic = "acid_base_homeostasis",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
    };  //// QUESTION 072
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Em um tampão, o pH medido é igual ao pKa do ácido fraco. O que se pode concluir sobre [A^-]/[HA]?",
           answers = new string[] {
               "Não há ácido fraco no sistema",
               "A razão é 10",
               "A razão é 0,1",
               "A razão é 1"
           },
           correctIndex = 3,
           questionNumber = 72,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_072",
           topic = "acidsBase",
           subtopic = "henderson_hasselbalch",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 073
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Ao adicionar pequena quantidade de ácido forte a um tampão HA/A-, qual componente reage diretamente com o H+ adicionado?",
           answers = new string[] {
               "A base conjugada A- ",
               "O ácido fraco HA",
               " A água como produto final exclusivo",
               "O sal neutro sem participação ácido-base"
           },
           correctIndex = 0,
           questionNumber = 73,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_073",
           topic = "acidsBase",
           subtopic = "buffers",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 074
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Se a ventilação aumenta e mais CO2 é eliminado, qual tendência é esperada para o equilíbrio CO2 + H2O ⇌ H2CO3 ⇌ H^+ + HCO3^-?",
           answers = new string[] {
               "Aumento de H+ e diminuição do pH",
               "Diminuição de H+ e aumento do pH ",
               "Conversão completa de bicarbonato em glicose",
               "Ausência de efeito sobre o pH"
           },
           correctIndex = 1,
           questionNumber = 74,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_074",
           topic = "acidsBase",
           subtopic = "blood_buffer_system",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 075
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Três ácidos têm pKa 3,8; 7,2; e 9,5. Qual deles é o mais forte?",
           answers = new string[] {
               "O de pKa 3,8 ",
               "O de pKa 7,2",
               "O de pKa 9,5",
               "Todos têm a mesma força"
           },
           correctIndex = 0,
           questionNumber = 75,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 2,
           questionInDevelopment = false,
           globalId = "acidsBase_075",
           topic = "acidsBase",
           subtopic = "blood_buffer_system",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Analyze,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 076
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Qual escolha de tampão é mais defensável para um experimento enzimático em pH 7,2?",
           answers = new string[] {
               "Um ácido forte sem base conjugada",
               "Um tampão com pKa 2,0, pois será mais ácido",
               "Um tampão com pKa próximo de 7,2",
               "Água pura, pois sempre mantém pH constante"
           },
           correctIndex = 2,
           questionNumber = 76,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_076",
           topic = "acidsBase",
           subtopic = "buffers",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        // QUESTION 077
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Um estudante afirma: 'toda solução neutra tem pH exatamente 7'. Qual avaliação é mais correta?",
           answers = new string[] {
               "A afirmação é sempre correta em qualquer temperatura.",
               "A afirmação é uma aproximação válida para água a 25 °C, mas não é universal.",
               "A afirmação é falsa porque neutralidade significa pH 0.",
               "A afirmação é falsa porque neutralidade significa pH 14."
           },
           correctIndex = 1,
           questionNumber = 77,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_077",
           topic = "acidsBase",
           subtopic = "ph_scale",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 078
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "O pH sanguíneo de 7,4 deve ser interpretado como:",
           answers = new string[] {
               "Levemente básico em relação à neutralidade da água a 25 °C, mas fisiológico.",
               "Fortemente ácido e incompatível com a vida.",
               "Neutro porque todo fluido corporal tem pH 7.",
               "Sem relação com concentração de H+."
           },
           correctIndex = 0,
           questionNumber = 78,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_078",
           topic = "acidsBase",
           subtopic = "acid_base_homeostasis",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 079
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Um aluno diz que 'quanto maior o pKa, mais forte é o ácido'. Como você avaliaria essa afirmação?",
           answers = new string[] {
               "Correta apenas para bases fortes.",
               "Correta: pKa e força ácida aumentam juntos.",
               "Incorreta: ácidos mais fortes têm menor pKa.",
               "A afirmação é falsa porque neutralidade significa pH 14."
           },
           correctIndex = 2,
           questionNumber = 79,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_079",
           topic = "acidsBase",
           subtopic = "ka_pka",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 080
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Em uma titulação cujo ponto final ocorre em faixa básica, qual decisão é mais adequada?",
           answers = new string[] {
               "Escolher qualquer indicador, pois todos viram em pH 7.",
               "Evitar indicadores, pois eles neutralizam a solução.",
               "Escolher um indicador cuja viragem ocorra na faixa de pH do ponto final.",
               "Usar apenas indicadores que mudam em pH 0."
           },
           correctIndex = 2,
           questionNumber = 80,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_080",
           topic = "acidsBase",
           subtopic = "indicators",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 081
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "A mistura HCl/Cl^- é uma boa solução-tampão? Qual avaliação é mais adequada?",
           answers = new string[] {
               "Sim, porque todo ácido com seu ânion forma bom tampão.",
               "Sim, porque HCl mantém pH constante em qualquer concentração.",
               "Não, porque Cl- é sempre ácido forte.",
               "Não, porque HCl é ácido forte e Cl- tem basicidade conjugada desprezível. "
           },
           correctIndex = 3,
           questionNumber = 81,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_081",
           topic = "acidsBase",
           subtopic = "buffers",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 082
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Um colega usa pH + pOH = 14 sem especificar temperatura. Qual correção é mais adequada?",
           answers = new string[] {
               "A relação é sempre 14 em qualquer solvente e temperatura.",
               "A relação correta é sempre pH + pOH = 7.",
               "A relação 14 é a forma usual para soluções aquosas a 25 °C.",
               " A relação não tem ligação com Kw."
           },
           correctIndex = 2,
           questionNumber = 82,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_082",
           topic = "acidsBase",
           subtopic = "poh_kw",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 083
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Ao diluir uma solução ácida forte, o pH aumenta de 2 para 3. Qual interpretação é mais adequada?",
           answers = new string[] {
               "A concentração de H^+ aumentou 10 vezes.",
               "A concentração de H^+ diminuiu 10 vezes. ",
               "A solução se tornou necessariamente neutra.",
               "O pH aumentou de forma linear com [H^+]."
           },
           correctIndex = 1,
           questionNumber = 83,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_083",
           topic = "acidsBase",
           subtopic = "ph_scale",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 084
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Um paciente tem pH baixo e CO2 elevado por hipoventilação. Qual intervenção fisiológica seria mais diretamente compensatória a curto prazo?",
           answers = new string[] {
               "Reduzir a ventilação para acumular CO2.",
               "Aumentar a ventilação para eliminar CO2.",
               "Adicionar ácido lático ao sangue.",
               "Bloquear o sistema bicarbonato."
           },
           correctIndex = 1,
           questionNumber = 84,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_084",
           topic = "acidsBase",
           subtopic = "acid_base_homeostasis",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 085
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Após vômitos intensos, há perda de HCl gástrico. Qual avaliação ácido-base é mais provável?",
           answers = new string[] {
               "Tendência à acidose respiratória.",
               "Tendência à acidose por excesso de CO2.",
               "Nenhuma alteração possível no pH.",
               "Tendência à alcalose metabólica. "
           },
           correctIndex = 3,
           questionNumber = 85,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_085",
           topic = "acidsBase",
           subtopic = "acid_base_homeostasis",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 086
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Qual argumento melhor justifica a eficiência do tampão bicarbonato no sangue?",
           answers = new string[] {
               " Ele funciona porque bicarbonato é ácido forte.",
               "Ele independe da ventilação.",
               "Ele está acoplado à eliminação de CO2 pelos pulmões.",
               "Ele atua apenas em pH 0."
           },
           correctIndex = 2,
           questionNumber = 86,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_086",
           topic = "acidsBase",
           subtopic = "blood_buffer_system",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 087
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Dois tampões têm pH = pKa, mas um contém 0,1 M de HA/A- e outro 0,001 M. Qual deve resistir melhor à adição de ácido?",
           answers = new string[] {
               "O de 0,001 M, por menor concentração.",
               "O de 0,1 M, por maior capacidade tamponante. ",
               "Ambos resistem igualmente a qualquer quantidade de ácido.",
               "Nenhum, pois pH = pKa elimina o tampão."
           },
           correctIndex = 1,
           questionNumber = 87,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_087",
           topic = "acidsBase",
           subtopic = "buffers",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 088
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Ao escolher um tampão para pH 5,0, qual critério é mais adequado?",
           answers = new string[] {
               "Selecionar o ácido com menor pKa possível.",
               "Selecionar sempre um ácido forte.",
               " Ignorar pKa e usar apenas a cor do indicador.",
               "Selecionar um ácido fraco com pKa próximo de 5,0."
           },
           correctIndex = 3,
           questionNumber = 88,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_088",
           topic = "acidsBase",
           subtopic = "henderson_hasselbalch",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 089
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Qual justificativa é cientificamente melhor para classificar uma solução de pH 2 como ácida?",
           answers = new string[] {
               "Ela tem sabor azedo, portanto é ácido por definição.",
               "Ela tem pH menor que 14, portanto toda solução assim é ácida.",
               "Ela tem concentração de H+ maior que uma solução neutra. ",
               "Ela não contém água."
           },
           correctIndex = 2,
           questionNumber = 89,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_089",
           topic = "acidsBase",
           subtopic = "ph_scale",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        }
        QUESTION 090
        new Question
        {
           questionDatabankName = "AcidBaseBufferQuestionDatabase",
           questionText = "Um aluno afirma que todo ponto de equivalência em titulação ácido-base ocorre em pH 7. Qual avaliação é correta?",
           answers = new string[] {
               "Correta para qualquer titulação ácido-base.",
               "Incorreta: o pH no ponto de equivalência depende da força do ácido e da base.",
               "Correta apenas quando há indicador colorido.",
               "Incorreta porque ponto de equivalência sempre ocorre em pH 0."
           },
           correctIndex = 1,
           questionNumber = 90,
           answerType = AnswerType.Text,
           questionType = QuestionType.Text,
           questionImagePath = "",
           questionLevel = 3,
           questionInDevelopment = false,
           globalId = "acidsBase_090",
           topic = "titration",
           subtopic = "ph_scale",
           displayName = "Ácidos, Bases e Tampões",
           bloomLevel = BloomLevel.Evaluate,
           conceptTags = new List<string> { "water_autoionization" },
           prerequisites = null,
           questionHint = new QuestionHint { text = "O Produto Iônico, apelidado de Kw (onde o w significa water), é a constante universal da autoionização da água sob 25 °C. Ele dita as regras e sempre cravou a marca estática de 1 x 10^-14." }
        },
};

    public List<Question> GetQuestions() => questions;
    public QuestionSet GetQuestionSetType() => QuestionSet.acidsBase;
    public string GetDatabankName() => "AcidBaseBufferQuestionDatabase";
    public string GetDisplayName() => "Ácidos, Bases e Tampões";
    public bool IsDatabaseInDevelopment() => databaseInDevelopment;
}
