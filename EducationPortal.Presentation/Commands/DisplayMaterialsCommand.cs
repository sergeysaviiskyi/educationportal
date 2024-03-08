using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DisplayMaterials")]
    public class DisplayMaterialsCommand : ICommonCommand
    {
        public DisplayService DisplayService;
        public DisplayMaterialsCommand(DisplayService displayService)
        {
            DisplayService = displayService;
        }
        public async Task Execute(CommandContext context)
        {
            await DisplayService.DisplayMaterialsAsync();
        }
    }
}
