using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DisplayFinishedCourses")]
    public class DisplayFinishedCoursesCommand : ICommonCommand
    {
        public DisplayService DisplayService;
        public DisplayFinishedCoursesCommand(DisplayService displayService)
        {
            DisplayService = displayService;
        }
        public async Task Execute(CommandContext context)
        {
            await DisplayService.DisplayFinishedCoursesAsync(context.UserId);
        }
    }
}
