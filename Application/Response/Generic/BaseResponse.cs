namespace Application.Response.Generic
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public BaseResponse() { }

        public BaseResponse(T response)
        {
            Data = response;
        }
    }
}
