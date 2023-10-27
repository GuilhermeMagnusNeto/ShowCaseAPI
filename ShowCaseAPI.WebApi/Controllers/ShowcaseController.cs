using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.Showcase;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowcaseController : ControllerBase
    {
        private readonly IShowcaseRepository _showcaseRepository;
        private readonly IStoreRepository _storeRepository;
        public ShowcaseController(IShowcaseRepository showcaseRepository, IStoreRepository storeRepository)
        {
            _showcaseRepository = showcaseRepository;
            _storeRepository = storeRepository;
        }


        [HttpGet("GetShowcaseById/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var result = await _showcaseRepository.GetById(id);
                if (result == null)
                {
                    return NotFound("Nenhum produto encontrado!");
                }

                return Ok(new ShowcaseViewModel
                {
                    Id = result.Id,
                    ExclusiveCode = result.ExclusiveCode,
                    Name = result.Name,
                    StoreId = result.StoreId
                });
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpGet("GetAllShowcasesByStoreId/{storeId}")]
        public async Task<IActionResult> GetAllByStoreId(Guid storeId)
        {
            try
            {
                var result = (await _showcaseRepository.GetShowcasesByStoreId(storeId)).Select(x => new ShowcaseViewModel
                {
                    Id = x.Id,
                    ExclusiveCode = x.ExclusiveCode,
                    Name = x.Name,
                    StoreId = x.StoreId
                }).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PostShowcaseViewModel vm)
        {
            try
            {
                var store = await _storeRepository.GetById(vm.StoreId);
                if (store == null)
                {
                    BadRequest("Loja não encontrada");
                }

                var exclusiveCode = await _showcaseRepository.GenerateExclusiveCode();
                var showcase = new Showcase()
                {
                    ExclusiveCode = exclusiveCode,
                    Name = vm.Name,
                    StoreId = vm.StoreId
                };

                var result = await _showcaseRepository.Insert(showcase);
                if (result > 0)
                {
                    return Ok("Vitrine criada com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a criação da vitrine.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] PutShowcaseViewModel vm)
        {
            try
            {
                var showcase = await _showcaseRepository.GetById(vm.Id);
                if (showcase == null)
                {
                    return BadRequest("Nenhuma vitrine encontrado!");
                };

                showcase.Name = vm.Name;

                var result = await _showcaseRepository.Update(showcase);
                if (result > 0)
                {
                    return Ok("Vitrine atualizado com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante a atualização da vitrine.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var showcase = await _showcaseRepository.GetById(id);
                if (showcase == null)
                {
                    return BadRequest("Nenhuma vitrine encontrado!");
                };

                var result = await _showcaseRepository.Delete(id);
                if (result > 0)
                {
                    return Ok("Vitrine excluida com sucesso!");
                }

                return BadRequest("Ocorreu um erro durante a exclusão da vitrine.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
