namespace DVotingBackendApp.exceptions
{
    public class InvalidCandidateException : Exception
    {
        public InvalidCandidateException(string message) : base(message) { }
    }
}
