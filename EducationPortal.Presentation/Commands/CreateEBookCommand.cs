
using EducationPortal.Application.Services;
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("CreateEBook")]
    public class CreateEBookCommand : ICommonCommand
    {
        private IMaterialService _materialService;
        public CreateEBookCommand(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        public async Task Execute(CommandContext context)
        {
            await _materialService.CreateEBookAsync(context.Name, context.Author);
        }

    }
}
