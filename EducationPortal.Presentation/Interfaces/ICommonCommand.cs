using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Interfaces
{
    public interface ICommonCommand
    {
        public Task Execute(CommandContext context);
    }
}
