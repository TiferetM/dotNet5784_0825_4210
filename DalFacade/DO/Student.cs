
namespace DO;


/// <summary>
/// Student Entity represents a student with all its props
/// </summary>
/// <param name="Id">Personal unique ID of the student (as in national id card)</param>
/// <param name="Name">Private Name of the student</param>
/// <param name="Alias"></param>
/// <param name="IsActive"></param>
/// <param name="BirthDate"></param>
public record Student
(
        int Id,
        string? Name = null,
        string? Alias = null,
        bool IsActive = false,
        DateTime? BirthDate = null
)
{
    public Student() : this(0) { } //empty ctor for stage 3

    /// <summary>
    /// RegistrationDate - registration date of the current student record
    /// </summary>
    public DateTime RegistrationDate => DateTime.Now; //get only
}
