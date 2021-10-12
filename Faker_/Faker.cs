using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Faker.DTO;
using Generator;

namespace Faker_
{

    public class Faker
    {
        private Dictionary<Type, IGenerator> Generators = new Dictionary<Type, IGenerator>
        {
            {typeof(int), new IntGenerator()},
            {typeof(float), new FloatGenerator()},
            {typeof(double), new DoubleGenerator()},
            {typeof(bool), new BoolGenerator()},
            {
                typeof(DateTime),
                LoadPlugin("E:/Univer/5sem/spp/Faker/Faker/DateGenerator/bin/Debug/net6.0/DateGenerator.dll",
                    typeof(DateTime))
            },
            {
                typeof(string),
                LoadPlugin("E:/Univer/5sem/spp/Faker/Faker/StringGenerator/bin/Debug/net6.0/StringGenerator.dll",
                    typeof(string))
            },
        };

        private HashSet<Type> dtoTypes = new HashSet<Type>
        {
            typeof(A), typeof(B), typeof(C), typeof(TestDto),
            typeof(AnotherTestDto), typeof(DtoWithConstructorWithoutDefaultConstructor)
        };

        private Dictionary<Type, object> activeDto = new Dictionary<Type, object>();

        private FakerConfig fakerConfig;
       
        public Faker(FakerConfig fakerConfig)
        {
            this.fakerConfig = fakerConfig;
        }

        private static IGenerator LoadPlugin(string path, Type generatorType)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterfaces().Contains(typeof(IGenerator)))
                {
                    var instance = (IGenerator) Activator.CreateInstance(type);
                    if (instance.getGeneratedType() == generatorType)
                    {
                        return instance;
                    }
                }
            }

            return null;
        }

        public object? Create(Type type)
        {
            object result = null;
            if (Generators.ContainsKey(type))
            {
                return Generators[type].generateValue();
            }

            if (dtoTypes.Contains(type))
            {
                result = CreateDto(type);
            }

            if (type.GetInterfaces().Contains(typeof(IList)))
            {
                var listValueType = type.GenericTypeArguments[0];
                IList list = new ListGenerator(listValueType).generateList();
                for (int i = 0; i < list.Count; i++)
                {
                    if (activeDto.ContainsKey(listValueType) && activeDto[listValueType] != null)
                    {
                        list[i] = activeDto[listValueType];
                    }
                    else
                    {
                        list[i] = Create(listValueType);
                    }
                }

                result = list;
            }

            return result;
        }

        private object CreateDto(Type type)
        {
            object result;
            if (!activeDto.ContainsKey(type))
            {
                activeDto.Add(type, null);
            }

            if (activeDto[type] == null)
            {
                var instance = CreateInstance(type);
                activeDto[type] = instance;
                CreateProperties(type, instance);
                CreateFields(type, instance);
                foreach (var memberInfo in type.GetMembers())
                {
                    foreach (var pair in fakerConfig.CustomGenerators)
                    {
                        if (pair.Key.Name.ToLower().Equals(memberInfo.Name.ToLower()))
                        {
                            var generateValue = pair.Value.generateValue();
                            if ((memberInfo.MemberType & MemberTypes.Field) != 0)
                            {
                                ((FieldInfo)memberInfo).SetValue(instance, generateValue);
                            } 
                            else if ((memberInfo.MemberType & MemberTypes.Property) != 0)
                            {
                                ((PropertyInfo)memberInfo).SetValue(instance, generateValue);    
                            }
                        }
                    }
                }
                result = instance;
            }
            else
            {
                result = activeDto[type];
            }

            activeDto[type] = null;

            return result;
        }

        private object CreateInstance(Type type)
        {
            ConstructorInfo[] publicConstructors = type.GetConstructors(BindingFlags.Public
                                                                        | BindingFlags.Instance);
            ConstructorInfo constructorInfo;
            if (publicConstructors.Length == 0)
            {
                constructorInfo = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
            }
            else
            {
                constructorInfo = publicConstructors[0];
            }

            ParameterInfo[] constructorParametersInfo = constructorInfo.GetParameters();
            var length = constructorParametersInfo.Length;
            var parameters = new object[length];
            for (int i = 0; i < length; i++)
            {
                parameters[i] = Create(constructorParametersInfo[i].ParameterType);
            }

            object instance = constructorInfo.Invoke(parameters);
            return instance;
        }

        private void CreateFields(Type type, object instance)
        {
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                var fieldValue = Create(fieldType);
                field.SetValue(instance, fieldValue);
            }
        }

        private void CreateProperties(Type type, object instance)
        {
            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite)
                {
                    var propertyType = property.PropertyType;
                    var propertyValue = Create(propertyType);
                    property.SetValue(instance, propertyValue);
                }
            }
        }

        public T? Create<T>()
        {
            Type type = typeof(T);
            return (T) Create(type);
        }
    }
}

