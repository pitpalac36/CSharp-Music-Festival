using System;

namespace Model.validator
{
    [Serializable]
    class ValidationException : Exception
    {
        private new readonly string Message;

        public ValidationException(string message) { Message = message; }

        public string GetMessage() => Message;

    }
}