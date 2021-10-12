using System;

namespace Generator
{
    public interface IGenerator
    {
        object generateValue();
        Type getGeneratedType();
    }
}