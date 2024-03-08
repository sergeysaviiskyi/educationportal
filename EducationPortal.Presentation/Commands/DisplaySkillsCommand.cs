using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DisplaySkills")]
    public class DisplaySkillsCommand : ICommonCommand      
    {
        public DisplayService DisplayService;
        public DisplaySkillsCommand(DisplayService displayService)
        {
            DisplayService = displayService;
        }
        public async Task Execute(CommandContext context)
        {
            await DisplayService.DisplaySkillsAsync();
        }
    }
}
