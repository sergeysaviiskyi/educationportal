
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("StartLearningNewCourse")]
    public class StartLearningNewCourseCommand : ICommonCommand
    {
        private ICourseService _courseService;
        public StartLearningNewCourseCommand(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task Execute(CommandContext context)
        {
            await _courseService.StartLearningNewCourseAsync(context.UserId, context.CourseId);
        }
    }
}
