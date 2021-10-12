using System;
using Generator;

namespace Faker_
{
    public class IntTenGenerator : IGenerator
    {
        public object generateValue()
        {
            return -1;
        }

        public Type getGeneratedType()
        {
            return typeof(int);
        }
    }
}