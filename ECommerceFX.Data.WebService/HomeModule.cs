using System;
using Nancy;

namespace ECommerceFX.Data.WebService
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/test"] = p => new TestModel { Id = Guid.NewGuid(), Name = "Just a test model." };
        }
    }

    public class TestModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}