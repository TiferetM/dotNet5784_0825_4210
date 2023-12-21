using DO;


namespace BO;
///<summary>
///<param name="Id">Personal unique ID and its a primary key</param>
///<param name="Name"></param>
///<param name="Email"></param>
///<param name="Level">Engineer level</param>
///<param name="Cost">cost per hour</param>
///<param name = "Task" >If present, the ID and alias of the current task</param >
///</summary>




public class Engineer
{
    public int ID { get; init; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public EngineerExperience Level { get; init; }
    public double Cost { get; set; }
    public BO.TaskInEngineer? Task { get; set; }
    public override string ToString() => Tools.ToStringProperty(this);

}
