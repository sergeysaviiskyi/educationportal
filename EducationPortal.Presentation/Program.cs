using System;
using EducationPortal.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Design;
namespace EducationPortal.Presentation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var context = new CommandContext();
            var processor = new CommandProcessor(context);
            var manger = new Manager(processor);
            await manger.StartUpAsync();
        }
    }
}