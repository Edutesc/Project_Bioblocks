using System.Collections.Generic;
using System.IO;

namespace QuestionSystem
{
    /// <summary>
    /// Helpers para traduzir os caminhos legados de imagem das Question
    /// (ex.: "AnswerImages/IntroductionDB/benzeno", "QuestionImages/...") em storage keys
    /// relativos à raiz "Question" do Firebase Storage no novo layout por tema:
    ///
    ///     Question/&lt;topic&gt;/&lt;filename&gt;.png
    ///
    /// O FirebaseStorageImageRepository tem _root = "Question", então a chave que o
    /// app passa adiante é "&lt;topic&gt;/&lt;filename&gt;" (sem extensão, ela é acrescentada
    /// pelo repositório de Storage).
    ///
    /// Se algum dia o questionImagePath / answer image guardado no Firestore já vier no
    /// formato "&lt;topic&gt;/&lt;filename&gt;", basta este helper retornar `legacyPath` e o
    /// resto do app continua funcionando.
    /// </summary>
    public static class QuestionStorageKeys
    {
        private const string ANSWER_IMAGES_PREFIX   = "AnswerImages/";
        private const string QUESTION_IMAGES_PREFIX = "QuestionImages/";

        /// <summary>
        /// Verifica se uma string parece ser um path de imagem e não texto literal de resposta.
        ///
        /// Reconhece dois formatos:
        ///   1. Legado (C# databases / HardcodedQuestionSource em preview mode):
        ///      "AnswerImages/..." ou "QuestionImages/..."
        ///   2. Storage key (Firestore após migração do UploadQuestionBanksEditor):
        ///      "&lt;topic&gt;/&lt;filename&gt;" — contém "/" mas não começa com os prefixos legados.
        ///
        /// Nota: só é chamado quando isImageAnswer == true, portanto o risco de
        /// um texto de resposta com "/" ser confundido com imagem é irrelevante.
        /// </summary>
        public static bool LooksLikeImagePath(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            // Formato legado
            if (value.StartsWith(ANSWER_IMAGES_PREFIX) || value.StartsWith(QUESTION_IMAGES_PREFIX))
                return true;
            // Formato storage key: "<topic>/<filename>" (contém "/" e não começa com "/")
            int slash = value.IndexOf('/');
            return slash > 0 && slash < value.Length - 1;
        }

        /// <summary>
        /// Resolve a storage key a partir de um path legado (ou já novo) e do topic
        /// da questão. Retorna null se a entrada for vazia ou se não houver topic.
        /// </summary>
        public static string Resolve(string legacyPath, string topic)
        {
            if (string.IsNullOrEmpty(legacyPath)) return null;
            if (string.IsNullOrEmpty(topic))      return null;

            // Já está no novo formato "<topic>/<filename>"? Devolve como está.
            if (!LooksLikeImagePath(legacyPath) && legacyPath.StartsWith(topic + "/"))
                return StripExtension(legacyPath);

            // Caso geral: extrai o filename e prefixa com o topic.
            string fileName = Path.GetFileNameWithoutExtension(legacyPath);
            if (string.IsNullOrEmpty(fileName)) return null;

            return $"{topic}/{fileName}";
        }

        /// <summary>
        /// Enumera todas as storage keys associadas a uma Question — questionImagePath
        /// (se for image question) e cada answer com path de imagem (se for image answer).
        /// </summary>
        public static IEnumerable<string> AllForQuestion(Question q)
        {
            if (q == null) yield break;
            string topic = q.topic;

            if (q.isImageQuestion && !string.IsNullOrEmpty(q.questionImagePath))
            {
                string key = Resolve(q.questionImagePath, topic);
                if (!string.IsNullOrEmpty(key)) yield return key;
            }

            if (q.isImageAnswer && q.answers != null)
            {
                foreach (var answer in q.answers)
                {
                    if (!LooksLikeImagePath(answer)) continue;
                    string key = Resolve(answer, topic);
                    if (!string.IsNullOrEmpty(key)) yield return key;
                }
            }
        }

        private static string StripExtension(string path)
        {
            string ext = Path.GetExtension(path);
            return string.IsNullOrEmpty(ext) ? path : path.Substring(0, path.Length - ext.Length);
        }
    }
}
