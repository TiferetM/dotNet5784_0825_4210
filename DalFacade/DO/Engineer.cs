namespace DO;
using System.Xml.Linq;
/// <summary>
/// an intety that has :ID number, a Name, an email, a Engineer Level and the Cost;
/// </summary>
/// <param name="Id">Personal unique ID and its a primary key</param>
/// <param name="Name">Private Name of the engineer</param>
/// <param name="email"></param>
/// <param name="level"></param>
/// <param name="cost"></param>
public record Engineer
{
    //titi
    #region characters
     public int Id { get; set; }
    string? Name;
    string? email;
   public EngineerExperience level { get; set; }
    #endregion
    public Engineer(int myId,string myName,string myEmail, EngineerExperience level)  // ctor 
    {
        Id = myId;
        Name = myName;
        email = myEmail;
    }
    public Engineer() { }//empty c-tor
}
