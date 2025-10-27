namespace FirstAPI.Exceptions
{
    public class CollectionEmptyException :Exception
    {
        public CollectionEmptyException() : base("The collection is empty.")
        {
        }
        public CollectionEmptyException(string msg) : base(msg)
        {
        }
    }
}
