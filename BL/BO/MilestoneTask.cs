using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<summary>
///<param name="Id">Personal unique ID and its a primary key</param>
///<param name="Alias"></param>
///</summary>

namespace BO;

public class MilestoneTask
{
    public  int ID { get; set; }
    public string? Alias {  get; set; }
}
