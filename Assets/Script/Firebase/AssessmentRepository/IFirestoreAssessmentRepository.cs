using System.Threading.Tasks;

public interface IFirestoreAssessmentRepository
{
    Task SaveAssessmentAsync(AssessmentResult result);
}
