
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("DeleteUser")]
    public class DeleteUserCommand : ICommonCommand
    {
        private IUserService _userService;
        public DeleteUserCommand(IUserService userService)
        {
            _userService = userService;
        }
        public async Task Execute(CommandContext context)
        {
            await _userService.RemoveAsync(context.UserId);
        }
    }
}
