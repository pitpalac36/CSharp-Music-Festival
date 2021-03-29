
namespace csharpMusicFestival.validator
{
    class ValidationException
    {
        private string message;

        public string getMessage() { return message; }

        public ValidationException(string message) { this.message = message; }
    }
}
