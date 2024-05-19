namespace EtiqaFreelancerDataAPI.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null;
        public object Data { get; set; }

        public ApiResponse(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
