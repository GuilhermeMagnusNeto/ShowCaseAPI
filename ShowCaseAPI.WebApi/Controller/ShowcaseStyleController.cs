using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.ShowcaseStyle;
using ShowCaseAPI.WebApi.Model.Template;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowcaseStyleController : ControllerBase
    {
        private readonly IShowcaseStyleRepository _showcaseStyleRepository;
        private readonly IShowcaseRepository _showcaseRepository;
        private readonly ITemplateRepository _templateRepository;

        public ShowcaseStyleController(IShowcaseStyleRepository showcaseStyleRepository, IShowcaseRepository showcaseRepository, ITemplateRepository templateRepository)
        {
            _showcaseStyleRepository = showcaseStyleRepository;
            _showcaseRepository = showcaseRepository;
            _templateRepository = templateRepository;
        }

        [HttpGet("GetStyleByShowcaseId/{showCaseId}")]
        public async Task<IActionResult> GetByIdAsync(Guid showCaseId)
        {
            try
            {
                var result = _showcaseStyleRepository.Query().FirstOrDefault(x => !x.Deleted && x.ShowcaseId == showCaseId);
                if (result == null)
                {
                    return ResponseHelper.BadRequest("Nenhum style encontrado!");
                }

                var template = await _templateRepository.GetById(result.TemplateId);
                if (template == null)
                {
                    ResponseHelper.BadRequest("Template não encontrado");
                }

                return ResponseHelper.Success(new ShowcaseStyleViewModel
                {
                    Id = result.Id,
                    TemplateId= template.Id,
                    TemplateName = template.Name,
                    BackgroundColorCode = result.BackgroundColorCode,
                    ShowProductValue = result.ShowProductValue,
                    ShowStoreLogo = result.ShowStoreLogo,
                    RedirectLink = result.RedirectLink
                });
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpGet("GetAllTemplates")]
        public async Task<IActionResult> GetAllTemplates()
        {
            try
            {
                var result = (await _templateRepository.GetAll()).Select(x => new TemplateViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

                return ResponseHelper.Success(result);
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostShowcaseStyleViewModel vm)
        {
            try
            {
                var store = await _showcaseRepository.GetById(vm.ShowcaseId);
                if (store == null)
                {
                    ResponseHelper.BadRequest("Loja não encontrada");
                }

                var template = await _templateRepository.GetById(vm.TemplateId);
                if (template == null)
                {
                    ResponseHelper.BadRequest("template não encontrado");
                }

                var style = new ShowcaseStyle()
                {
                    ShowcaseId = vm.ShowcaseId,
                    TemplateId = vm.TemplateId
                };

                var result = await _showcaseStyleRepository.Insert(style);
                if (result > 0)
                {
                    return ResponseHelper.Success(new ShowcaseStyleViewModel
                    {
                        Id = style.Id,
                        TemplateId = template.Id,
                        TemplateName = template.Name,
                        BackgroundColorCode = style.BackgroundColorCode,
                        ShowProductValue = style.ShowProductValue,
                        ShowStoreLogo = style.ShowStoreLogo,
                        RedirectLink = style.RedirectLink
                    });
                }
                return ResponseHelper.BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PutShowcaseStyleViewModel vm)
        {
            try
            {
                var style = await _showcaseStyleRepository.GetById(vm.ShowcaseStyleId);
                if (style == null)
                {
                    return ResponseHelper.BadRequest("Nenhum style encontrado!");
                };

                var template = await _templateRepository.GetById(vm.TemplateId);
                if (template == null)
                {
                    ResponseHelper.BadRequest("nenhum template não encontrado");
                }

                style.TemplateId = vm.TemplateId;
                style.BackgroundColorCode = vm.BackgroundColorCode;
                style.ShowProductValue = vm.ShowProductValue;
                style.ShowStoreLogo = vm.ShowStoreLogo;
                style.RedirectLink = vm.RedirectLink;

                var result = await _showcaseStyleRepository.Update(style);
                if (result > 0)
                {
                    return ResponseHelper.Success();
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }
    }
}
