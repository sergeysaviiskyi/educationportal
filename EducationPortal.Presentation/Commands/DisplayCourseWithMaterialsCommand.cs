using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DisplayCourseWithMaterials")]
    internal class DisplayCourseWithMaterialsCommand : ICommonCommand
    {
        public DisplayService DisplayService;
        public DisplayCourseWithMaterialsCommand(DisplayService displayService)
        {
            DisplayService = displayService;
        }
        public async Task Execute(CommandContext context)
        {
            await DisplayService.DisplayCourseWithMaterialsAsync(context.CourseId);
        }
    }
}
