using System;

namespace Core
{
    public class NotFoundException<TIdentifier> : Exception
    {
        public NotFoundException(string objectType, TIdentifier id)
        {
            ObjectType = objectType;
            Id = id;
        }

        public string ObjectType { get; set; }

        public TIdentifier Id { get; set; }
    }
}