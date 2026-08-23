using System.Collections.Generic;
using System.Threading.Tasks;
using Edutesc.BioBlocks.Core.Models;

public class FakeFirestoreAssessmentRepository : IFirestoreAssessmentRepository
{
    public AssessmentResult LastSavedResult { get; private set; }
    public AssessmentAttempt LastSavedAttempt { get; private set; }
    public string LastSavedAssessmentId { get; private set; }
    public AssessmentData ActiveAssessmentToReturn { get; set; }
    public Dictionary<string, AssessmentData> AssessmentsDatabase { get; set; } = new Dictionary<string, AssessmentData>();

    public Task<AssessmentData> GetAssessmentAsync(string assessmentId)
    {
        if (AssessmentsDatabase.TryGetValue(assessmentId, out var data))
        {
            return Task.FromResult(data);
        }
        return Task.FromResult(ActiveAssessmentToReturn);
    }

    public Task<AssessmentData> GetActiveAssessmentAsync()
    {
        return Task.FromResult(ActiveAssessmentToReturn);
    }

    public Task SaveAssessmentAttemptAsync(string assessmentId, AssessmentAttempt attempt)
    {
        LastSavedAssessmentId = assessmentId;
        LastSavedAttempt = attempt;
        return Task.CompletedTask;
    }

    public Task SaveAssessmentAsync(AssessmentResult result)
    {
        LastSavedResult = result;
        return Task.CompletedTask;
    }
}
