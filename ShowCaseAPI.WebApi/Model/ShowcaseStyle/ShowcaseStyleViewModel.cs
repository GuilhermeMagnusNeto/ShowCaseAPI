namespace ShowCaseAPI.WebApi.Model.ShowcaseStyle
{
    public class ShowcaseStyleViewModel
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string BackgroundColorCode { get; set; }
        public bool ShowProductValue { get; set; }
        public bool ShowStoreLogo { get; set; }
    }
}
