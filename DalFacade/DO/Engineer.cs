using System.Xml.Linq;

namespace DO;
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
    #region characters
     public int Id { get; set; }
    string? Name;
    string? email;
   public EngineerExperience level { get; set; }
    #endregion
    public Engineer(int myId,string myName,string myEmail,int myCost)  // ctor 
    {
        Id = myId;
        Name = myName;
        email = myEmail;
    }
    public Engineer() { }//empty c-tor
}
