// See https://aka.ms/new-console-template for more information

using System;
using Faker.DTO;
using Faker_;

var faker = new Faker_.Faker();
var testDto = faker.Create<TestDto>();
var anotherTestDto = faker.Create<AnotherTestDto>();
var a = faker.Create<A>();
Console.ReadLine();