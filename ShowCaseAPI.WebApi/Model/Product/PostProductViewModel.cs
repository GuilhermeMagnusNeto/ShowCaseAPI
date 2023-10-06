namespace ShowCaseAPI.WebApi.Model.Product
{
    public class PutProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
    }
}
