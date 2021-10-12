using System;

namespace Generator
{
    public class DoubleGenerator : IGenerator
    {
        public object generateValue()
        {
            return new Random().NextDouble() * 256;
        }
        
        public Type getGeneratedType()
        {
            return typeof(Double);
        }
    }
}