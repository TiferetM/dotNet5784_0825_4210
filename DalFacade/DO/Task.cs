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
    public string?Description{ get; set; } = null;
    public string? Alias { get; set; } = null;
    public bool Milestone { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? SchedulableDate { get; set; }
    public DateTime? DeadLine { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string ?Deliverables { get; set; }
    public string? Remarks { get; set; } = null;
    public int? EngineerId { get; set; }
    public EngineerExperience? ComplexityLevel { get; set; } = null;
    public int NewId { get; set; }
    public DateTime? ForCastDate { get; set; }
    public TimeSpan TimeSpan { get; set; }
    // public int ID { get; set; }
    //  public string? Description { get; set; }

    //param c-tor
    public Task(int myId,string? description,string? myAlias,bool myMilestone,DateTime? createDate,
TimeSpan TimeSpan, DateTime? start,DateTime? schedualDate,DateTime? myDeadLine,DateTime? myComplete,string myDelivrables,
string? myRemarks,int engineerId, EngineerExperience myComplexityLevel)//c-tor
    {
        Id = myId;
        Description = description;
        Alias = myAlias;
        Milestone = myMilestone;
        CreateDate = CreateDate;
        StartDate= start;
        SchedulableDate= schedualDate;
        DeadLine= myDeadLine;
        CompletedDate= myComplete;
        Deliverables= myDelivrables;
        Remarks = myRemarks;
        EngineerId=engineerId;
        ComplexityLevel = myComplexityLevel;
    }
    public Task(int id) { }//empty c-tor

    public Task(int v1, string v2, string myAlias, bool v3, DateTime createdat, DateTime start, DateTime schedudalDate, DateTime deadLine, DateTime complete, string delivrables, string remarks, int? engineerid, EngineerExperience complexityLevel)
    {
        this.CreateDate = createdat;
        this.StartDate = start;
        this.SchedulableDate = schedudalDate;
        DeadLine = deadLine;
        CompletedDate = complete;
        Deliverables = delivrables;
        Remarks = remarks;
        ComplexityLevel = complexityLevel;
    }
}

