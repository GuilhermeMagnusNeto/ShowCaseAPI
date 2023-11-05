namespace ShowCaseAPI.WebApi.Model.Product
{
    public class PutProductViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
        public string? SKU { get; set; }
        public string? Description { get; set; }
    }
}
