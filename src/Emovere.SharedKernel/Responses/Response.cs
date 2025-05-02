using System.Text.Json.Serialization;

namespace Emovere.SharedKernel.Responses
{
    public class Response<TData>
    {
        [JsonIgnore]
        public readonly int Code;

        public const string DEFAULT_ERROR_MESSAGE = "Invalid Operation.";
        public const string DEFAULT_SUCCESS_MESSAGE = "Valid Operation.";

        [JsonConstructor]
        protected Response() => Code = StatusCode.OK_STATUS_CODE;

        protected Response(
            TData? data,
            int code = StatusCode.OK_STATUS_CODE,
            string? message = null,
            List<string>? errors = null)
        {
            Data = data;
            Message = message;
            Errors = errors;
            Code = code;
        }

        public TData? Data { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public bool IsSuccess => Code is >= StatusCode.OK_STATUS_CODE and <= 299;

        public static Response<TData> Success(TData? data, int code = StatusCode.OK_STATUS_CODE, string? message = DEFAULT_SUCCESS_MESSAGE)
            => new(data, code, message);

        public static Response<TData> Failure(List<string> errors, string? message = DEFAULT_ERROR_MESSAGE, int code = StatusCode.BAD_REQUEST_STATUS_CODE)
            => new(default, code, message, errors);
    }
}