

using System.Net;

namespace Events.Contracts.Wrappers
{
    public class Response
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

        public Response()
        {
        }

        public Response(bool succeeded = true, string message = null)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(bool succeeded)
        {
            Succeeded = succeeded;
            Message = Succeeded ? "Operation has been completed successfully." : "Operation has been failed!";
        }

        public static implicit operator Response(bool successded)
        {
            return new Response(successded);
        }

        public static implicit operator Response(string message)
        {
            return new Response(message);
        }
    }
    public class Response<T> : Response
    {
        public T Data { get; set; }
        public byte[] ByteData { get; set; }

        public Response() { }

        public Response(T data, bool succeeded = true, string message = null) :
            base(succeeded, message)
        {
            Data = data;
        }

        public Response(string message) : base(message) { }

        public Response(bool succeeded) : base(succeeded) { }

        public static implicit operator Response<T>(T data)
        {
            return new Response<T>(data);
        }
    }
}
