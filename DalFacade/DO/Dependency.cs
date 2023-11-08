namespace DO;
/// <summary>
/// an intety that has Id, dependencyTask and DependenceOnTask
/// </summary>
/// <param name="Id">Personal unique ID and its a primary key</param>
/// <param name="DependenceTask"></param>
/// <param name="DependenceOnTask"></param>
public record Dependency
{
    public int Id { get; set; }
    int DependenceTask;
    int DependenceOnTask;

    public Dependency(int myId, int myDependenceTask, int myDependenceOnTask)  // ctor 
    {
        #region character
        Id = myId;
        DependenceTask= myDependenceTask;
        DependenceOnTask= myDependenceOnTask;
        #endregion
    }
    public Dependency() { }
}
