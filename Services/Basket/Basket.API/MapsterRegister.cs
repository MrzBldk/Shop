using Basket.DAL.Entities;
using Basket.DAL.Repositories;
using Mapster;
using System.Reflection;

namespace Basket.API
{
    public class MappingRegister : ICodeGenerationRegister
    {
        public void Register(CodeGenerationConfig config)
        {
            config.AdaptTwoWays("[name]Dto", MapType.Map)
                 .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(RedisBasketRepository)), "Basket.DAL.Entities")
                 .ShallowCopyForSameType(true);

            config.GenerateMapper("[name]Mapper")
                .ForType<CustomerBasket>();
        }
    }
}
