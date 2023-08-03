namespace Mango.Services.CouponAPI.Models.Dto
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool IsScucess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
