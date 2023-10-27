using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.Showcase;
using ShowCaseAPI.WebApi.Model.ShowcaseStyle;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

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

        [HttpGet("GetStyleByShowcaseId/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = _showcaseStyleRepository.Query().FirstOrDefault(x => !x.Deleted && x.ShowcaseId == id);
                if (result == null)
                {
                    return NotFound("Nenhum style encontrado!");
                }

                return Ok(new ShowcaseStyleViewModel
                {
                    Id = result.Id,
                    TemplateId= result.TemplateId,
                    TemplateName = result.Template.Name,
                    BackgroundColorCode = result.BackgroundColorCode,
                    ShowProductValue = result.ShowProductValue,
                    ShowStoreLogo = result.ShowStoreLogo
                });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
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
                    BadRequest("Loja não encontrada");
                }

                var template = await _templateRepository.GetById(vm.TemplateId);
                if (template == null)
                {
                    BadRequest("template não encontrado");
                }

                var style = new ShowcaseStyle()
                {
                    ShowcaseId = vm.ShowcaseId,
                    TemplateId = vm.TemplateId
                };

                var result = await _showcaseStyleRepository.Insert(style);
                if (result > 0)
                {
                    return Ok("Style da vitrine criada com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a criação do style da vitrine.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PutShowcaseStyleViewModel vm)
        {
            try
            {
                var style = await _showcaseStyleRepository.GetById(vm.Id);
                if (style == null)
                {
                    return BadRequest("Nenhum style encontrado!");
                };

                var template = await _templateRepository.GetById(vm.TemplateId);
                if (template == null)
                {
                    BadRequest("template não encontrado");
                }

                style.TemplateId = vm.TemplateId;
                style.BackgroundColorCode = vm.BackgroundColorCode;
                style.ShowProductValue = vm.ShowProductValue;
                style.ShowStoreLogo = vm.ShowStoreLogo;

                var result = await _showcaseStyleRepository.Update(style);
                if (result > 0)
                {
                    return Ok("Style da vitrine atualizado com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a atualização do style vitrine.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
