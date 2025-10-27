namespace FirstAPI.Exceptions
{
    public class InvalidIdentifierException :Exception
    {
        public InvalidIdentifierException() : base("The provided identifier is invalid.")
        {
        }
        public InvalidIdentifierException(string msg) : base(msg)
        {
        }
    }
}
