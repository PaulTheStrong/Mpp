using System;
using System.Text;

namespace Generator
{
    public class StringGenerator : IGenerator
    {
        public object generateValue()
        {
            var random = new Random();
            int length = random.Next(20) + 10;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append((char) ('0' + random.Next(60)));
            }
            return sb.ToString();
        }
        public Type getGeneratedType()
        {
            return typeof(string);
        }
    }
}