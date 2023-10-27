namespace ShowCaseAPI.WebApi.Model.Store
{
    public class PostStoreViewModel
    {
        public string Name { get; set; }
        public IFormFile? StoreLogo { get; set; }
        public Guid UserId { get; set; }
    }
}
