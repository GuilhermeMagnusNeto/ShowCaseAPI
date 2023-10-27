using System.ComponentModel.DataAnnotations.Schema;

namespace ShowCaseAPI.WebApi.Model.Template
{
    public class TemplateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
