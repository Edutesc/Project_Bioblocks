using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISRSManagerRepository
{
    Task SaveProgressAsync(string userId, Dictionary<string, SubjectData> userProgress);
    Task<Dictionary<string, SubjectData>> LoadProgressAsync(string userId);
}