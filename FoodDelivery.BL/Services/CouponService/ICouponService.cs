using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;

namespace FoodDelivery.BL.Services.CouponService;

public interface ICouponService : ICrudService<Coupon, Guid, CouponGetDto, CouponCreateDto, CouponUpdateDto>
{
    public Task<IEnumerable<CouponGetDto>> QueryAsync(QueryDto<CouponGetDto> queryDto);
}
