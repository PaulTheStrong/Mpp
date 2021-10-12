using System;

namespace Generator
{
    public class FloatGenerator : IGenerator
    {
        public object generateValue()
        {
            return (float)new Random().NextDouble() * 256f;
        }
        public Type getGeneratedType()
        {
            return typeof(float);
        }
    }
}