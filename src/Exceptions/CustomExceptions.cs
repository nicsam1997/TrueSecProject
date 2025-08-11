namespace TrueSecProject.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when an attempt to create a resource fails because it already exists.
    /// </summary>
    public class DuplicateException : Exception
    {
        public DuplicateException(string message) : base(message) { }

    }

    public class InvalidStructureException : Exception
    {
        public InvalidStructureException(string message) : base(message) { }
    }
}