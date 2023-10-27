namespace ShowCaseAPI.WebApi.Model.ResponseHelper
{
    public class ApiResponseViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
