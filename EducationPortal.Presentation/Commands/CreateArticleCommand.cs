using EducationPortal.Application.Services;
using EducationPortal.Domain.Entities.Materials;
using EducationPortal.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Presentation.Commands
{
    [CommandName("CreateArticle")]
    public class CreateArticleCommand : ICommonCommand
    {
        private IMaterialService _materialService { get; set; }
        public CreateArticleCommand(IMaterialService materialService)
        {
            _materialService = materialService;
        }
        //Check return
        public  Task Execute(CommandContext context)
        {
            return _materialService.CreateArticleAsync(context.Name);
        }

    }
}