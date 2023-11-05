namespace ShowCaseAPI.WebApi.Model.ShowcaseProduct
{
    public class PutShowcaseProductViewModel
    {
        public Guid ShowcaseId { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}
