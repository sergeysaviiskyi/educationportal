
using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("LearnLesson")]
    public class LearnLessonCommand : ICommonCommand
    {
        private ICourseService _courseService;
        public LearnLessonCommand(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task Execute(CommandContext context)
        {
            await _courseService.LearnLessonAsync(context.UserId, context.CourseId);
        }
    }
}
