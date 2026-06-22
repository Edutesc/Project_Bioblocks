using System.Collections.Generic;
using QuestionSystem;

public interface ISRSManager
{
    void ScheduleRevision(string subjectid);
    void SynctoFirestorm();
    
}