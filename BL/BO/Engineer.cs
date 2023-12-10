using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    int ID { get; init; }
    string? Name { get; set; }
    string? Email { get; set; }
    public EngineerExperience Level { get; init; }
    double Cost { get; set; }
    BO.TaskInEngineer ?Task { get; set; }

}
