using UnityEngine;
using UnityEngine.UI;

namespace BioBlocks.Tests
{
    public class TestAvailabilityChecker : MonoBehaviour
    {
        [SerializeField] private Button _startTestButton;
        private ITestService _testService;

        private async void Start()
        {
            _startTestButton.gameObject.SetActive(false);

            // var authService = AppContext.GetService<IAuthService>();
            // if (!IsAuthorizedDomain(authService.CurrentUser.Email)) return;
            
            // _testService = AppContext.GetService<ITestService>();
            // var activeTests = await _testService.GetActiveTestsAsync();

            // if (activeTests.Count > 0)
            // {
            //     string currentUserId = authService.CurrentUser.Id;
            //     TestConfig currentTest = activeTests[0];

            //     bool alreadyTaken = await _testService.HasUserTakenTestAsync(currentTest.Id, currentUserId);
                
            //     if (!alreadyTaken)
            //     {
            //         _startTestButton.gameObject.SetActive(true);
            //         _startTestButton.onClick.AddListener(() => StartTest(currentTest));
            //     }
            // }
        }

        private void StartTest(TestConfig config)
        {
            // Inicia cena do teste
        }
    }
}
