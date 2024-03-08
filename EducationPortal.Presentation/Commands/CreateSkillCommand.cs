
using EducationPortal.Application.Services;
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("CreateSkill")]
    public class CreateSkillCommand : ICommonCommand
    {
        private ISkillService _skillService;
        public CreateSkillCommand(ISkillService skillService)
        {
            _skillService = skillService;
        }
        public async Task Execute(CommandContext context)
        {
            await _skillService.CreateAsync(context.Name);
        }

    }
}
