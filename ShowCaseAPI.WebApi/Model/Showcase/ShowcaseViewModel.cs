namespace ShowCaseAPI.WebApi.Model.Showcase
{
    public class ShowcaseViewModel
    {
        public Guid Id { get; set; }
        public string ExclusiveCode { get; set; }
        public string Name { get; set; }
        public Guid StoreId { get; set; }
    }
}
