namespace DO;
///<summary>
///<param name="Id">Personal unique ID and its a primary key</param>
///<param name="description"></param>description of the task
///<param name="Alias"></param>short name
///<param name="Milestone"></param>○	אם קיים, מזהה אבן-דרך שהמשימה שייכת
///<param name="createdat"></param>
///<param name = "start" ></param >
///<param name="schedudalDate"></param>
///<param name="DeadLine"></param>
///<param name="Complete"></param>
///<param name="Delivrables"></param>
///<param name = "Remarks" ></param >
///<param name = "Engineerid" ></param >
///<param name = "ComplexityLevel" ></param >
public record Task
{
    public int Id { get; set; }
    public string? description{ get; set; } = null;
    public string? Alias { get; set; } = null;
    public bool Milestone { get; set; }
    public DateTime createdat { get; set; }
    public DateTime start { get; set; }
    DateTime schedudalDate;
    DateTime DeadLine;
    DateTime Complete;
    string Delivrables;
    string? Remarks= null;
    int Engineerid;
    EngineerExperience? ComplexityLevel = null;
    public int newId;

    //param c-tor
    public Task(int myId,string? mydescription,string? myAlias,bool myMilestone,DateTime mycreatedat,
DateTime mystart,DateTime myschedudalDate,DateTime myDeadLine,DateTime myComplete,string myDelivrables,
string? myRemarks,int myEngineerid, EngineerExperience myComplexityLevel)//c-tor
    {
        Id = myId;
        description = mydescription;
        Alias = myAlias;
        Milestone = myMilestone;
        createdat = mycreatedat;
        start= mystart;
        schedudalDate= myschedudalDate;
        DeadLine= myDeadLine;
        Complete= myComplete;
        Delivrables= myDelivrables;
        Remarks = myRemarks;
        Engineerid=myEngineerid;
        ComplexityLevel = myComplexityLevel;
    }
    public Task() { }//empty c-tor
}
