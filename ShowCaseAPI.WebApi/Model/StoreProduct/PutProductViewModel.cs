namespace ShowCaseAPI.WebApi.Model.Product
{
    public class PostProductViewModel
    {
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
        public string? SKU { get; set; }
        public string? Description { get; set; }
    }
}
