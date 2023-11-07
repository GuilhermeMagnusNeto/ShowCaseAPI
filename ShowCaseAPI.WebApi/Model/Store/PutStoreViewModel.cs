namespace ShowCaseAPI.WebApi.Model.Store
{
    public class PutStoreViewModel
    {
        public Guid storeId { get; set; }
        public string Name { get; set; }
        public IFormFile? StoreLogo { get; set; }
    }
}
