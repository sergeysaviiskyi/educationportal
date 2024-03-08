using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation
{
    public class CommandNameAttribute : Attribute
    {
        public string Name { get; set; }
        public CommandNameAttribute(string name)
        {
            Name = name;
        }
    }
}
