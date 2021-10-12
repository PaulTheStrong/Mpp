using System;

namespace Generator
{
    public class BoolGenerator : IGenerator
    {
        public object generateValue()
        {
            return new Random().Next(2) == 1;
        }
        public Type getGeneratedType()
        {
            return typeof(bool);
        }
    }
}