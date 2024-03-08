
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DeleteCourse")]
    public class DeleteCourseCommand : ICommonCommand
    {
        private ICourseService _courseService;
        public DeleteCourseCommand(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task Execute(CommandContext context)
        {
            await _courseService.RemoveAsync(context.CourseId);
        }
    }
}
