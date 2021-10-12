using System;
using System.Collections.Generic;
using System.Xml;

namespace Faker.DTO
{
    public class TestDto
    {
        public string stringField { get; set; }
        public int intField { get; set; }
        public double doubleField { get; }
        public float floatFiled { get; set; }
        public DateTime dateTimeField { get; set; }
        public List<AnotherTestDto> dtoListPropery { get; set; }
        public bool BooleanFiled;

        public Random nonDtoField;  

        private int privateIntProperty { get; set; }
        
        private bool privateBooleanaField;
    }

    public class AnotherTestDto
    {
        private AnotherTestDto() { }

        public string stringProperty { get; set; }

        public bool boolField;
        public int intField;

        private double privateDoublePropery { get; set; }

        private float privateFloatField;

        public char charField;
    }

    public class DtoWithConstructorWithoutDefaultConstructor
    {
        private string privateStringProperty { get; set; }

        public AnotherTestDto publicDtoField;

        public DtoWithConstructorWithoutDefaultConstructor(string privateStringProperty)
        {
            this.privateStringProperty = privateStringProperty;
        }
    }
    
    public class A
    {
        public List<B> b { get; set; }
    }

    public class B
    {
        public List<C> c { get; set; }
        public List<A> a { get; set; }
    }
    
    public class C
    {
        public List<A> aaaaaa { get; set; }
    }
    
    
}