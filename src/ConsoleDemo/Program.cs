using Swagger.Doc;
using System;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc= CovertXmlToSwaggerDoc.GetSwagger("172.8.8.116", "v1");

            Console.WriteLine("Hello World!");
        }
    }
}
