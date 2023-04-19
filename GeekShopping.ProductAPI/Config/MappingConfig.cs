using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Config
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            #region ModelToDomain
            CreateMap<ProductVO, Product>();
            #endregion
            #region DomainToModel
            CreateMap<Product, ProductVO>();
            #endregion
        }
    }
}
