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

internal class Task
{
      int ID {  get; init; }
      string ? Description { get; set; }
      string? Alias { get; set; }
      DateTime? CreatedAtDate { get; set; }
      BO.Status Status { get; set; }
      BO.MilestoneTask ?Milestone { get; set; }
      DateTime? BaselineStartDate { get; set; }
      DateTime? StartDate {  get; set; }
      DateTime? ScheduledStartDate { get; set; }
      DateTime? ForecastDate { get; set; }
      DateTime? DeadlineDate { get; set; }
      DateTime? CompleteDate { get; set; }
      string ?Deliverables { get; set; }
      string ?Remarks { get; set; }
      BO.EngineerInTask? Engineer {  get; set; }
      public EngineerExperience CopmlexityLevel { get; set; }








}
