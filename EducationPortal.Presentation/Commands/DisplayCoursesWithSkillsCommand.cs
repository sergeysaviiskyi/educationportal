using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DisplayCoursesWithSkills")]
    public class DisplayCoursesWithSkillsCommand : ICommonCommand
    {
        public DisplayService DisplayService;
        public DisplayCoursesWithSkillsCommand(DisplayService displayService)
        {
            DisplayService = displayService;
        }
        public async Task Execute(CommandContext context)
        {
            await DisplayService.DisplayCoursesWithSkillsAsync();
        }
    }
}
