
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DeleteMaterial")]
    public class DeleteMaterialCommand : ICommonCommand
    {
        private IMaterialService _materialService;
        public DeleteMaterialCommand(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        public async Task Execute(CommandContext context)
        {
            await _materialService.RemoveAsync(context.MaterialId);
        }
    }
}
