using UnityEngine;

namespace BioBlocks.Tests
{
    public class TestManager : MonoBehaviour
    {
        private TestSession _currentSession;

        private async void SubmitAnswers(string testId, string userId)
        {
            TestSubmission submission = new TestSubmission
            {
                TestId = testId,
                UserId = userId,
                Answers = _currentSession.GetAnswers()
            };

            // try
            // {
            //     var testService = AppContext.GetService<ITestService>();
            //     await testService.SubmitSubmissionAsync(submission);
            //     Debug.Log("Prova enviada com sucesso!");
            // }
            // catch (System.Exception ex)
            // {
            //     Debug.LogError($"Erro ao enviar a prova: {ex.Message}");
            // }
        }
    }
}
