// See https://aka.ms/new-console-template for more information

using System;
using Faker.DTO;
using Faker_;

FakerConfig fakerConfig = new FakerConfig();
fakerConfig.Add<TestDto, int, IntTenGenerator>(dto => dto.intField);
var faker = new Faker_.Faker(fakerConfig);
var testDto = faker.Create<TestDto>();
var anotherTestDto = faker.Create<AnotherTestDto>();
var a = faker.Create<A>();
Console.ReadLine();