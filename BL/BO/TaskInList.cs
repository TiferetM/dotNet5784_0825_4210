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

public class TaskInList
{
    public int Id {  get; init; }
    public  string? Description {  get; set; }
    public string? Alias {  get; set; }
    public  BO.Status Status { get; set; }

}
