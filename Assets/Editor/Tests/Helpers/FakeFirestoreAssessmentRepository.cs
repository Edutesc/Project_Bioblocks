using System.Threading.Tasks;

public class FakeFirestoreAssessmentRepository : IFirestoreAssessmentRepository
{
    public AssessmentResult LastSavedResult { get; private set; }

    public Task SaveAssessmentAsync(AssessmentResult result)
    {
        LastSavedResult = result;
        return Task.CompletedTask;
    }
}
