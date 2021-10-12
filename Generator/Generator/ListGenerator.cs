using System;
using System.Collections;
using System.Collections.Generic;
using BindingFlags = System.Reflection.BindingFlags;

namespace Generator
{
    public class ListGenerator : IListGenerator
    {
        private Type valuesType;

        public ListGenerator(Type type)
        {
            this.valuesType = type;
        }
        public IList generateList()
        {
            var random = new Random();
            var count = random.Next(5) + 5;
            var list = (IList)typeof(List<>).MakeGenericType(valuesType)
                .GetConstructor(BindingFlags.Instance | BindingFlags.Public, new Type[] { })
                .Invoke(new object[] {});
            for (int i = 0; i < count; i++)
            {
                list.Add(null);
            }

            return list;
        }
    }
}