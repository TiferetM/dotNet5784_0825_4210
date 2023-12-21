namespace BO;


[Serializable]
public class BLDoesNotExistException : Exception//ברירת מחדל
{
    public BLDoesNotExistException(string? message) : base(message) { }
}

public class BLAlreadyExistsException : Exception// קיים כבר
{
    public BLAlreadyExistsException(string? message, DO.DalAlreadyExistsException ex) : base(message) { }
}
public class BLDeletionImpossible : Exception// מחיקה
{
    public BLDeletionImpossible(string? message) : base(message) { }
}
public class BLXMLFileLoadCreateException : Exception//הקצאה
{
    public BLXMLFileLoadCreateException(string? message) : base(message) { }
}
