namespace FirstAPI.Exceptions
{
    public class DuplicateIdentifierException :Exception
    {
        public DuplicateIdentifierException() : base("The identifier already exists.")
        {
        }
        public DuplicateIdentifierException(string msg) : base(msg)
        {
        }
    }
}
