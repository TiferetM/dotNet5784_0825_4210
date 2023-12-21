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


internal class Milestone
{
     int Id { get; set; }
    string? Description { get; set; }
    string? Alias { get; set; }
    DateTime? CreatedAtDate { get; set; }
    BO.Status Status {  get; set; }
    DateTime ? StartDate {  get; set; }
    DateTime? ForecastDate { get; set; }
    DateTime? DeadlineDate { get; set; }
    DateTime? CompleteDate { get; set; }
    double completionPercentage { get; set; }
    string? Remaks { get; set; }
    List<BO.TaskInList> ?Dependencies { get; set; }
    public string[]? HangingList { get; set; }
}
