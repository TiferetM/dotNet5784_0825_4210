using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<summary>
///<param name="Id">Personal unique ID and its a primary key</param>
///<param name="Description"></param>
///<param name="Alias"></param>
///<param name="CreatedAtDate">Production date</param>
///<param name="Status"></param>
///<param name="Milestone">Related Milestone (ID and Nickname)</param>
///<param name="BaselineStartDate"></param>
///<param name = "StartDate" >Start Date</param >
///<param name="ScheduledStartDate"></param>
///<param name = "ForecastDate" >Estimated completion date</param >
///<param name="DeadlineDate">Final date for completion</param>
///<param name="CompleteDate">Actual end date</param>
///<param name="Deliverables">Product (a string describing the product)</param>
///<param name="Remaks"></param>
///<param name = "Engineer" >If available, the ID and name of the engineer assigned to the task</param >
///<param name="copmlexityLevel">Difficulty</param>
///</summary>



namespace BO;
public class Task
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public string? Alias { get; set; }
        public DateTime? CreatedAtDate { get; set; }
        public BO.Status? Status { get; set; }
        public BO.MilestoneTask? Milestone { get; set; }
        public DateTime? BaselineStartDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ScheduledStartDate { get; set; }
        public DateTime? ForecastDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string? Deliverables { get; set; }
        public string? Remarks { get; set; }
        public BO.EngineerInTask? Engineer { get; set; }
        public EngineerExperience? ComplexityLevel { get; set; }
        public List<TaskInList>? Dependencies { get; set; }
}

