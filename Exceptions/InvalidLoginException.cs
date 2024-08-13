namespace ORMMiniProject.Exceptions;

public class InvalidLoginException:Exception
{
    public InvalidLoginException(string message="Invalid password or email"):base(message)  
    {
        
    }
}
