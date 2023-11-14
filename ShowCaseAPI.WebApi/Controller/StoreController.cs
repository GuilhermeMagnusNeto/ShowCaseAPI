using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.Showcase;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IConfiguration _config;
        public StoreController(IStoreRepository storeRepository, IUserRepository userRepository, IStoreProductRepository storeProductRepository, IConfiguration config)
        {
            _storeRepository = storeRepository;
            _userRepository = userRepository;
            _storeProductRepository = storeProductRepository;
            _config = config;
        }


        [HttpGet("GetById/{storeId}")]
        public async Task<IActionResult> GetByIdAsync(Guid storeId)
        {
            try
            {
                var result = await _storeRepository.GetById(storeId);
                if (result == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma loja encontrado!");
                }
                return ResponseHelper.Success(new StoreViewModel
                {
                    Id = result.Id,
                    Name = result.Name,
                    UrlStoreLogo = result.StoreLogo != null ? _config.GetValue<string>("BlobStorageUrl") + result.StoreLogo : null
                });
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpGet("GetAllStoresByUserId/{storeId}")]
        public async Task<IActionResult> GetAllStoresByUserId(Guid storeId)
        {
            try
            {
                var result = _storeRepository.Query().Where(x => !x.Deleted && x.UserId == storeId).Select(x => new StoreViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlStoreLogo = x.StoreLogo != null ? _config.GetValue<string>("BlobStorageUrl") + x.StoreLogo : null
                }).ToList();

                return ResponseHelper.Success(result);
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] PostStoreViewModel vm)
        {
            try
            {
                var user = await _userRepository.GetById(vm.UserId);
                if (user == null)
                {
                    return ResponseHelper.BadRequest("Usuário não encontrado!");
                }
                var existeName = _storeRepository.Query().Where(x => !x.Deleted && x.UserId == user.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return ResponseHelper.BadRequest("Você já tem uma loja com esse nome!");
                }

                var store = new Store()
                {
                    Name = vm.Name,
                    UserId = user.Id
                };
                var result = await _storeRepository.Insert(store);
                if (result > 0)
                {
                    if (vm.StoreLogo != null)
                    {
                        //Blob Storage authentication
                        StorageCredentials credencials = new StorageCredentials(BlobInstance.AccessName, BlobInstance.AccessKey);
                        CloudStorageAccount account = new CloudStorageAccount(credencials, useHttps: true);
                        CloudBlobClient client = account.CreateCloudBlobClient();
                        CloudBlobContainer container = client.GetContainerReference("globalshowcase");

                        //Upload the file to Blob Storage                            
                        using (var stream = vm.StoreLogo.OpenReadStream())
                        {
                            var fileEx = vm.StoreLogo.FileName.Substring(vm.StoreLogo.FileName.Length - 5).Split(".");
                            string fileName = Guid.NewGuid().ToString() + "." + fileEx[1];
                            string fileSend = Path.Combine($"Store-{store.Id.ToString()}", $"Logo-{fileName}");

                            //Upload the file to Blob Storage   
                            CloudBlockBlob cblob = container.GetBlockBlobReference(fileSend);
                            var response = container.ExistsAsync().Result;
                            if (!response)
                            {
                                return BadRequest("Container not found!");
                            }
                            cblob.UploadFromStreamAsync(stream).Wait();

                            store.StoreLogo = fileSend.Replace("\\", "/");
                            var create = await _storeRepository.Update(store);
                            if (create > 0)
                            {
                                return ResponseHelper.Success(new StoreViewModel
                                {
                                    Id = store.Id,
                                    Name = store.Name,
                                    UrlStoreLogo = store.StoreLogo != null ? _config.GetValue<string>("BlobStorageUrl") + store.StoreLogo : null
                                });
                            }
                            return ResponseHelper.BadRequest();
                        }
                    }

                    return ResponseHelper.Success(new StoreViewModel
                    {
                        Id = store.Id,
                        Name = store.Name,
                        UrlStoreLogo = store.StoreLogo != null ? _config.GetValue<string>("BlobStorageUrl") + store.StoreLogo : null
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
        public async Task<IActionResult> PutAsync([FromForm] PutStoreViewModel vm)
        {
            try
            {
                var store = await _storeRepository.GetById(vm.StoreId);
                if (store == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma loja encontrado!");
                };
                var user = await _userRepository.GetById(store.UserId);
                if (user == null)
                {
                    return ResponseHelper.BadRequest("Usuário não encontrado!");
                }
                var existeName = _storeRepository.Query().Where(x => !x.Deleted && x.UserId == user.Id && x.Id != store.Id).Any(x => x.Name.ToUpper() == vm.Name.ToUpper());
                if (existeName)
                {
                    return ResponseHelper.BadRequest("Você já tem uma loja com esse nome!");
                }
                store.Name = vm.Name;

                var result = await _storeRepository.Update(store);
                if (result > 0)
                {
                    if (vm.StoreLogo != null)
                    {
                        //Blob Storage authentication
                        StorageCredentials credencials = new StorageCredentials(BlobInstance.AccessName, BlobInstance.AccessKey);
                        CloudStorageAccount account = new CloudStorageAccount(credencials, useHttps: true);
                        CloudBlobClient client = account.CreateCloudBlobClient();
                        CloudBlobContainer container = client.GetContainerReference("globalshowcase");

                        //Upload the file to Blob Storage                            
                        using (var stream = vm.StoreLogo.OpenReadStream())
                        {
                            var fileEx = vm.StoreLogo.FileName.Substring(vm.StoreLogo.FileName.Length - 5).Split(".");
                            string fileName = Guid.NewGuid().ToString() + "." + fileEx[1];
                            string fileSend = Path.Combine($"Store-{store.Id.ToString()}", $"Logo-{fileName}");

                            //Upload the file to Blob Storage   
                            CloudBlockBlob cblob = container.GetBlockBlobReference(fileSend);
                            var response = container.ExistsAsync().Result;
                            if (!response)
                            {
                                return BadRequest("Container not found!");
                            }
                            cblob.UploadFromStreamAsync(stream).Wait();

                            store.StoreLogo = fileSend.Replace("\\", "/");
                            var create = await _storeRepository.Update(store);
                            if (create > 0)
                            {
                                return ResponseHelper.Success(new StoreViewModel
                                {
                                    Id = store.Id,
                                    Name = store.Name,
                                    UrlStoreLogo = store.StoreLogo != null ? _config.GetValue<string>("BlobStorageUrl") + store.StoreLogo : null
                                });
                            }
                            return ResponseHelper.BadRequest();
                        }
                    }
                    return ResponseHelper.Success(new StoreViewModel
                    {
                        Id = store.Id,
                        Name = store.Name,
                        UrlStoreLogo = store.StoreLogo != null ? _config.GetValue<string>("BlobStorageUrl") + store.StoreLogo : null
                    });
                }
                return ResponseHelper.BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpDelete("{storeId}")]
        public async Task<IActionResult> DeleteAsync(Guid storeId)
        {
            try
            {
                var store = await _storeRepository.GetById(storeId);
                if (store == null)
                {
                    return ResponseHelper.BadRequest("Nenhuma loja encontrado!");
                };

                var products = await _storeProductRepository.GetProductsByStoreId(store.Id);
                foreach (var product in products)
                {
                    var productResult = await _storeProductRepository.Delete(product.Id);
                    if (productResult <= 0)
                    {
                        return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão dos produtos da loja.");
                    }
                }

                var resultStore = await _storeRepository.Delete(storeId);
                if (resultStore <= 0)
                {
                    return ResponseHelper.BadRequest("Ocorreu um erro durante a exclusão da loja.");
                }

                return ResponseHelper.Success();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }
    }
}
