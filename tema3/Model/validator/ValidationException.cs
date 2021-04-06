using System;

namespace csharpMusicFestival.validator
{
    class ValidationException : Exception
    {
        private new readonly string Message;

        public ValidationException(string message) { Message = message; }

        public string getMessage() => Message;

    }
}