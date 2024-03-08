using EducationPortal.Application.Interfaces;
using EducationPortal.Presentation.Interfaces;
using EducationPortal.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DisplayUserInfo")]
    public class DisplayUserInfoCommand : ICommonCommand
    {
        public DisplayService DisplayService;
        public DisplayUserInfoCommand(DisplayService displayService)
        {
            DisplayService = displayService;
        }
        public async Task Execute(CommandContext context)
        {
            await DisplayService.DisplayUserInfoAsync(context.UserId);
        }
    }
}
