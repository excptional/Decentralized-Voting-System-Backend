namespace DVotingBackendApp.exceptions
{
    public class InvalidVoterException : Exception
    {
        public InvalidVoterException(string message) : base(message) { }
    }
}
