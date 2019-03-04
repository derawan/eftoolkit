using System;


namespace GTX
{
    public sealed class ParseException : Exception
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        int _position;

        public ParseException(string message, int position)
            : base(message)
        {
            _position = position;
        }

        public int Position
        {
            get { return _position; }
        }

        public override string ToString()
        {
            return string.Format(Res.ParseExceptionFormat, Message, _position);
        }
    }
}

