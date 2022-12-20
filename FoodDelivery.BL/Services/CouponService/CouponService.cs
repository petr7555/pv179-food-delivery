using AutoMapper;
using FoodDelivery.BL.DTOs;
using FoodDelivery.BL.DTOs.Coupon;
using FoodDelivery.BL.QueryObject;
using FoodDelivery.BL.Services.CrudService;
using FoodDelivery.DAL.EntityFramework.Models;
using FoodDelivery.Infrastructure.UnitOfWork;

namespace FoodDelivery.BL.Services.CouponService;

public class CouponService : CrudService<Coupon, Guid, CouponGetDto, CouponCreateDto, CouponUpdateDto>,
    ICouponService
{
    private readonly IQueryObject<CouponGetDto, Coupon> _queryObject;

    public CouponService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<CouponGetDto, Coupon> queryObject) :
        base(unitOfWork.CouponRepository, mapper)
    {
        _queryObject = queryObject;
    }

    public async Task<IEnumerable<CouponGetDto>> QueryAsync(QueryDto<CouponGetDto> queryDto)
    {
        return await _queryObject.ExecuteAsync(queryDto);
    }
}
