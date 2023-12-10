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

internal class MilestoneTask
{
    int ID { get; set; }
    string? Alias {  get; set; }
}
