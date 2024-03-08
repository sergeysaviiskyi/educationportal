global using EducationPortal.Application.Interfaces.Services;
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("AddMaterialToCourse")]
    internal class AddMaterialToCourseCommand : ICommonCommand
    {
        private ICourseService _courseService;
        public AddMaterialToCourseCommand(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task Execute(CommandContext context)
        {
            await _courseService.AddMaterialAsync(context.CourseId, context.MaterialId);
        }
    }
}
