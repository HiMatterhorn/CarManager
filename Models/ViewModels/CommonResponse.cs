using AmiFlota.Enums;

namespace AmiFlota.Models.ViewModels
{
    public class CommonResponse<T>
    {
        public ResponseResult Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public CommonResponse(T data)
        {
            Data = data;
            Status = ResponseResult.Success;
        }

        public CommonResponse(ResponseResult result = ResponseResult.Failure, string message = null)
        {
            Status = result;
            Message = message;
        }
    }
}
