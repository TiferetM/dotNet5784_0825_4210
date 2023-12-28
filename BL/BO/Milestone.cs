using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BO;
///<summary>
///<param name="Id">Personal unique ID and its a primary key</param>
///<param name="Description"></param>
///<param name="Alias"></param>
///<param name="CreatedAtDate">Production date</param>
///<param name="Status"></param>
///<param name = "StartDate" >Start Date</param >
///<param name = "ForecastDate" >Estimated completion date</param >
///<param name="DeadlineDate">Final date for completion</param>
///<param name="CompleteDate">Actual end date</param>
///<param name="completionPercentage">progress percentage</param>
///<param name="Remaks"></param>
///<param name = "Dependencies" >Dependency list (task-in-list type)</param >
///</summary>


public class Milestone
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreatedAtDate { get; set; }
    public BO.Status Status {  get; set; }
    public DateTime ? StartDate {  get; set; }
    public DateTime? ForecastDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public DateTime? CompleteDate { get; set; }
    public double completionPercentage { get; set; }
    public string? Remarks { get; set; }
    public List<BO.TaskInList> ?Dependencies { get; set; }
    public string[]? HangingList { get; set; }
}
