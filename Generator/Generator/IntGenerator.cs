using System;

namespace Generator
{
    public class IntGenerator : IGenerator
    {
        private const int MaxValue = 256;
        
        public object generateValue()
        {
            return new Random().Next(MaxValue);
        }
        public Type getGeneratedType()
        {
            return typeof(int);
        }
    }
}