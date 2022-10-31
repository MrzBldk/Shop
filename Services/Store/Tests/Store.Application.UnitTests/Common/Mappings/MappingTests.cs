using AutoMapper;
using Store.Application.Common.Mappings;
using Store.Application.Stores.Queries;
using Store.Domain.Entities;
using System.Runtime.Serialization;

namespace Store.Application.UnitTests.Common.Mappings
{
    [TestFixture]
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(Domain.Entities.Store), typeof(StoreDTO))]
        [TestCase(typeof(StoreSection), typeof(StoreSectionDTO))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type)!;

            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
