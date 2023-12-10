using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<summary>
///<param name="Id">Personal unique ID and its a primary key</param>
///<param name="Description"></param>
///<param name="Alias"></param>
///<param name="Status"></param>
///</summary>

namespace BO;

internal class TaskInList
{
    int ID {  get; init; }
    string? Description {  get; set; }
    string? Alias {  get; set; }
    BO.Status Status { get; set; }

}
