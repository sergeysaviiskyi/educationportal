using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation
{
    public class CommandProcessor
    {
        public Dictionary<string, ICommonCommand> commands = new Dictionary<string, ICommonCommand>();
        public CommandContext Context { get; set; }
        public CommandProcessor (CommandContext context)
        {
            Context = context;
        }
        public void SetCommand(string name, ICommonCommand command)
        {
            commands.Add(name, command);
        }
        public async Task Execute(string name)
        {
            await commands[name].Execute(Context);
        }
    }
}