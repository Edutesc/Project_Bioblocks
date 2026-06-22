using System.Collections.Generic;

public interface ISRSManager
{
    void ScheduleRevision(string subjectid);
    Task SynctoFirestorm(string userID);
    
}