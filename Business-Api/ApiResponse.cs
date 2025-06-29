namespace Business_Api
{
    public class ApiResponse<T>
    {
        public string Response { get; set; }
        public int StatusRequest { get; set; }
        public List<T> DataList { get; set; } = new();

        public ApiResponse(string response, int statusRequest, T data)
        {
            Response = response;
            StatusRequest = statusRequest;
            DataList.Add(data);
        }

        public ApiResponse(string response, int statusRequest)
        {
            Response = response;
            StatusRequest = statusRequest;
        }

        public ApiResponse(string response, int statusRequest, List<T> data)
        {
            Response = response;
            StatusRequest = statusRequest;
            DataList = data;
        }
        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>("success", 200, data);
        }
        public static ApiResponse<T> Success(List<T> data)
        {
            return new ApiResponse<T>("success", 200, data);
        }

        public static ApiResponse<T> Error(string message, int statusCode)
        {
            return new ApiResponse<T>(message, statusCode);
        }

    }
}
