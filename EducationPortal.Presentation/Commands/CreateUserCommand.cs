
using EducationPortal.Application.Services;
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("CreateUser")]
    public class CreateUserCommand : ICommonCommand
    {
        private IUserService _userService;
        public CreateUserCommand(IUserService userService)
        {
            _userService = userService;
        }
        public async Task Execute(CommandContext context)
        {
            //await _userService.CreateAsync(context.Name);
        }

    }
}