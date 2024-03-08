using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation
{
    public class CommandContext
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int MaterialId { get; set; }
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }
    }
}
