using System.Threading.Tasks;
using Discount.GRPC.Protos;
using static Discount.GRPC.Protos.DiscountProtoService;

namespace Basket.API.GRPCServices
{
    public class DiscountGRPCService
    {
        private readonly DiscountProtoServiceClient _discountProtoService;

        public DiscountGRPCService(DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            return await _discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = productName });
        }
    }
}
