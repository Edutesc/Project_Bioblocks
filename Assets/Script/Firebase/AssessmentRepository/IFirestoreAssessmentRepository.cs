using System.Threading.Tasks;
using Edutesc.BioBlocks.Core.Models;

public interface IFirestoreAssessmentRepository
{
    Task<AssessmentData> GetAssessmentAsync(string assessmentId);
    Task<AssessmentData> GetActiveAssessmentAsync();
    Task SaveAssessmentAttemptAsync(string assessmentId, AssessmentAttempt attempt);
    Task SaveAssessmentAsync(AssessmentResult result);
}
