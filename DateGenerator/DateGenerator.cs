using System;

namespace Generator
{
    public class DateGenerator : IGenerator
    {
        public object generateValue()
        {
            var random = new Random();
            return new DateTime(
                random.Next(25) + 2000, random.Next(12),
                random.Next(29), random.Next(12), 
                random.Next(60), random.Next(60));
        }

        public Type getGeneratedType()
        {
            return typeof(DateTime);
        }
    }
}