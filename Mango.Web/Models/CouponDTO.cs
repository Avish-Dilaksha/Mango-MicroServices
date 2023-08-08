namespace Mango.Web.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public string? couponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
