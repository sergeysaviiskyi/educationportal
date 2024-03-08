using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DisplayLearnedSkills")]
    internal class DisplayLearnedSkillsCommand : ICommonCommand
    {
        public DisplayService DisplayService;
        public DisplayLearnedSkillsCommand(DisplayService displayService)
        {
            DisplayService = displayService;
        }
        public async Task Execute(CommandContext context)
        {
            await DisplayService.DisplayLearnedSkillsAsync(context.UserId);
        }
    }
}
