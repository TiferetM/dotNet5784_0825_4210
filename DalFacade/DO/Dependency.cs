
using System.Xml.Linq;

namespace DO;
/// <summary>
/// an intety that has Id, dependencyTask and DependenceOnTask
/// </summary>
/// <param name="Id">Personal unique ID and its a primary key</param>
/// <param name="DependenceTask"></param>
/// <param name="DependenceOnTask"></param>
public class Dependency
{
    int Id;
    int DependenceTask;
    int DependenceOnTask;

    public Dependency(int myId, int myDependenceTask, int myDependenceOnTask)  // ctor 
    {
        Id = myId;
         DependenceTask= myDependenceTask;
         DependenceOnTask= myDependenceOnTask;
    }
    public Dependency() { }
}
