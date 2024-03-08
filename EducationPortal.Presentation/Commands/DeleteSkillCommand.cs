
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DeleteSkill")]
    public class DeleteSkillCommand : ICommonCommand
    {
        private ISkillService _skillService;
        public DeleteSkillCommand(ISkillService skillService)
        {
            _skillService = skillService;
        }
        public async Task Execute(CommandContext context)
        {
            await _skillService.RemoveAsync(context.SkillId);
        }
    }
}
