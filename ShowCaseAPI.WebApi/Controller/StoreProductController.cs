using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.Store;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StoreProductController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IConfiguration _config;

        public StoreProductController(IStoreProductRepository storeProductRepository, IStoreRepository storeRepository, IConfiguration config)
        {
            _storeProductRepository = storeProductRepository;
            _storeRepository = storeRepository;
            _config = config;
        }


        [HttpGet("GetProductById/{productId}")]
        public async Task<IActionResult> GetByIdAsync(Guid productId)
        {
            try
            {
                var result = await _storeProductRepository.GetById(productId);
                if (result == null)
                {
                    return ResponseHelper.BadRequest("Nenhum produto encontrado!");
                }

                return ResponseHelper.Success(new ProductViewModel
                {
                    Id = result.Id,
                    StoreId = result.StoreId,
                    Name = result.Name,
                    Value = result.Value,
                    SKU = result.SKU,
                    Description = result.Description,
                    UrlProductPicture = result.ProductPicture != null? _config.GetValue<string>("BlobStorageUrl") + result.ProductPicture : null
                });
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpGet("GetAllProductsByStoreId/{storeId}")]
        public async Task<IActionResult> GetAllByStoreId(Guid storeId)
        {
            try
            {
                var result = (await _storeProductRepository.GetProductsByStoreId(storeId)).Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    StoreId = x.StoreId,
                    Name = x.Name,
                    Value = x.Value,
                    SKU = x.SKU,
                    Description = x.Description,
                    UrlProductPicture = x.ProductPicture != null ? _config.GetValue<string>("BlobStorageUrl") + x.ProductPicture : null
                }).ToList();
                return ResponseHelper.Success(result);
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> PostAsync([FromForm] PostProductViewModel vm)
        {
            try
            {
                var store = await _storeRepository.GetById(vm.StoreId);
                if (store == null)
                {
                    ResponseHelper.BadRequest("Loja não encontrada");
                }

                if (vm.SKU != null)
                {
                    var products = await _storeProductRepository.GetProductsByStoreId(vm.StoreId);
                    var existSKU = products.Any(x => x.SKU != null && x.SKU.ToUpper() == vm.SKU.ToUpper());
                    if (existSKU)
                    {
                        return ResponseHelper.BadRequest("Você já tem um produto com esse código SKU registrado para esta loja!");
                    }
                }

                var product = new StoreProduct()
                {
                    StoreId = vm.StoreId,
                    Name = vm.Name,
                    Value = vm.Value,
                    SKU = vm.SKU,
                    Description = vm.Description
                };

                var result = await _storeProductRepository.Insert(product);
                if (result > 0)
                {
                    if (vm.ProductPicture != null)
                    {
                        //Blob Storage authentication
                        StorageCredentials credencials = new StorageCredentials(BlobInstance.AccessName, BlobInstance.AccessKey);
                        CloudStorageAccount account = new CloudStorageAccount(credencials, useHttps: true);
                        CloudBlobClient client = account.CreateCloudBlobClient();
                        CloudBlobContainer container = client.GetContainerReference("globalshowcase");

                        //Upload the file to Blob Storage                            
                        using (var stream = vm.ProductPicture.OpenReadStream())
                        {
                            var fileEx = vm.ProductPicture.FileName.Substring(vm.ProductPicture.FileName.Length - 5).Split(".");
                            string fileName = product.Id.ToString() + "." + fileEx[1];
                            string fileSend = Path.Combine($"Store-{product.StoreId.ToString()}", "Products", $"Product-{fileName}");

                            //Upload the file to Blob Storage   
                            CloudBlockBlob cblob = container.GetBlockBlobReference(fileSend);
                            var response = container.ExistsAsync().Result;
                            if (!response)
                            {
                                return BadRequest("Container not found!");
                            }
                            cblob.UploadFromStreamAsync(stream).Wait();

                            product.ProductPicture = fileSend.Replace("\\", "/");
                            var create = await _storeProductRepository.Update(product);
                            if (create > 0)
                            {
                                return ResponseHelper.Success();
                            }
                        }

                        return ResponseHelper.Success();
                    }
                }
                return ResponseHelper.BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPut("EditProduct")]
        public async Task<IActionResult> PutAsync([FromForm] PutProductViewModel vm)
        {
            try
            {
                var product = await _storeProductRepository.GetById(vm.ProductId);
                if (product == null)
                {
                    return ResponseHelper.BadRequest("Nenhum produto encontrado!");
                };

                if (vm.SKU != null)
                {
                    var products = await _storeProductRepository.GetProductsByStoreId(product.StoreId);
                    var existSKU = products.Where(x => x.Id != product.Id).Any(x => x.SKU != null && x.SKU.ToUpper() == vm.SKU.ToUpper());
                    if (existSKU)
                    {
                        return ResponseHelper.BadRequest("Você já tem um produto com esse código SKU registrado para esta loja!");
                    }
                }

                product.Name = vm.Name;
                product.SKU = vm.SKU;
                product.Description = vm.Description;
                product.Value = vm.Value;
                if (vm.ProductPicture != null)
                {
                    //Blob Storage authentication
                    StorageCredentials credencials = new StorageCredentials(BlobInstance.AccessName, BlobInstance.AccessKey);
                    CloudStorageAccount account = new CloudStorageAccount(credencials, useHttps: true);
                    CloudBlobClient client = account.CreateCloudBlobClient();
                    CloudBlobContainer container = client.GetContainerReference("globalshowcase");

                    //Upload the file to Blob Storage                            
                    using (var stream = vm.ProductPicture.OpenReadStream())
                    {
                        var fileEx = vm.ProductPicture.FileName.Substring(vm.ProductPicture.FileName.Length - 5).Split(".");
                        string fileName = product.Id.ToString() + "." + fileEx[1];
                        string fileSend = Path.Combine($"Store-{product.StoreId.ToString()}", "Products", $"Product-{fileName}");

                        //Upload the file to Blob Storage   
                        CloudBlockBlob cblob = container.GetBlockBlobReference(fileSend);
                        var response = container.ExistsAsync().Result;
                        if (!response)
                        {
                            return BadRequest("Container not found!");
                        }
                        cblob.UploadFromStreamAsync(stream).Wait();
                        product.ProductPicture = fileSend.Replace("\\", "/");
                    }
                }

                var result = await _storeProductRepository.Update(product);
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

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteAsync(Guid productId)
        {
            try
            {
                var product = await _storeProductRepository.GetById(productId);
                if (product == null)
                {
                    return ResponseHelper.BadRequest("Nenhum produto encontrado!");
                };

                var result = await _storeProductRepository.Delete(productId);
                if (result > 0)
                {
                    return ResponseHelper.Success();
                }

                return ResponseHelper.BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }
    }
}
