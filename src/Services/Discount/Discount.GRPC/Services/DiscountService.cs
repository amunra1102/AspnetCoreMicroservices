using System.Threading.Tasks;
using AutoMapper;
using Discount.GRPC.Entities;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using static Discount.GRPC.Protos.DiscountProtoService;

namespace Discount.GRPC.Services
{
    public class DiscountService : DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (null == coupon)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }

            _logger.LogInformation($"Discount is retrieved for ProductName: {coupon.ProductName}, Amount: {coupon.Amount}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.CreateDiscount(coupon);
            _logger.LogInformation($"Discount is successfully created. ProductName: {coupon.ProductName}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation($"Discount is successfully updated. ProductName: {coupon.ProductName}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountReponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);

            return new DeleteDiscountReponse
            {
                Success = deleted
            };
        }
    }
}
