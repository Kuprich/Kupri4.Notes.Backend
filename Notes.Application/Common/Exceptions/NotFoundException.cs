using System;

namespace Notes.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"\"{name}\"({key}) not found") { }
    }
}
