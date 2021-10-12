using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generator;

namespace Faker_
{ 
    public class FakerConfig
    {
        public Dictionary<MemberInfo, IGenerator> CustomGenerators{ get; private set; }

        public FakerConfig()
        {
            CustomGenerators = new Dictionary<MemberInfo, IGenerator>();
        }

        public void Add<DtoType, MemberType, GeneratorType>(Expression<Func<DtoType, MemberType>> expression) 
            where DtoType : class
            where GeneratorType : IGenerator
        {
            Expression expressionBody = expression.Body;
            if (expressionBody.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Invalid expression");
            }

            IGenerator generator = (IGenerator)Activator.CreateInstance(typeof(GeneratorType));
            if (generator.getGeneratedType() != typeof(MemberType))
            {
                throw new ArgumentException("Invalid generator");
            }

            CustomGenerators.Add(((MemberExpression)expressionBody).Member, generator);
        }
    }
}
