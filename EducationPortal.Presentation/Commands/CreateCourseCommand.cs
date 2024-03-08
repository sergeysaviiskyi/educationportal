
using EducationPortal.Application.Services;
using EducationPortal.Domain.Entities;
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("CreateCourse")]
    public class CreateCourseCommand : ICommonCommand
    {
        private ICourseService _courseService;
        public CreateCourseCommand(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task Execute(CommandContext context)
        {
             await _courseService.CreateAsync(context.Name, context.Description);
        }
    }
}
