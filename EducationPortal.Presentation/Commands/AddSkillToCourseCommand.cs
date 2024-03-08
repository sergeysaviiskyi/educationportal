
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("AddSkillToCourse")]
    internal class AddSkillToCourseCommand : ICommonCommand
    {
        private ICourseService _courseService;
        public AddSkillToCourseCommand(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task Execute(CommandContext context)
        {
            await _courseService.AddSkillAsync(context.CourseId, context.SkillId);
        }
    }
}
