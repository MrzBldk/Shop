using NSubstitute;
using Ordering.Application.Commands.AddOrderItem;
using Ordering.Application.Commands.CreateOrder;
using Ordering.Application.Commands.SetShippedStatus;
using Ordering.Application.Repositories;
using Ordering.Domain;
using Ordering.Domain.ValueObjects;

namespace Ordering.UseCases.Tests
{
    [TestFixture]
    public class OrderTest
    {

        private IOrderWriteOnlyRepository _orderWriteOnlyRepository;
        private IOrderReadOnlyRepository _orderReadOnlyRepository;

        [SetUp]
        public void Setup()
        {
            _orderReadOnlyRepository = Substitute.For<IOrderReadOnlyRepository>();
            _orderWriteOnlyRepository = Substitute.For<IOrderWriteOnlyRepository>();
        }

        [Test]
        [TestCase("a", "a", "a", "a", "a")]
        [TestCase("b", "b", "b", "b", "b")]
        [TestCase("c", "c", "c", "c", "c")]
        public async Task Create_Valid_Order(string street, string city, string state, string country, string zipCode)
        {
            var expectedAdress = $"{country}, {state}, {city}, {street}, {zipCode}";

            CreateOrderUseCase useCase = new(_orderWriteOnlyRepository);
            Address address = new(street, city, state, country, zipCode);
            CreateOrderResult result = await useCase.Execute(address);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Order.Id, Is.Not.EqualTo(Guid.Empty));
                Assert.That(result.Order.Address, Is.EqualTo(expectedAdress));
                Assert.That(result.Order.OrderDate, Is.InRange(DateTime.Now.AddMinutes(-1), DateTime.Now));
            });
        }

        private static readonly object[] _sourceLists =
            {
                new object[] {1m, "a", 1 },
                new object[] {2m, "b", 2 }
            };

        [Test]
        [TestCaseSource(nameof(_sourceLists))]
        public async Task Add_Valid_OrderItem(decimal price, string name, int units)
        {            
            _orderReadOnlyRepository.Get(Arg.Any<Guid>()).Returns(new Domain.Orders.Order(new ("a", "a", "a", "a", "a")));
            AddOrderItemUseCase useCase = new(_orderReadOnlyRepository, _orderWriteOnlyRepository);
            AddOrderItemResult result = await useCase.Execute(Guid.NewGuid(), price, name, units);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Order.Items, Has.Count.EqualTo(1));
                Assert.That(result.Order.Items[0].Name, Is.EqualTo(name));
                Assert.That(result.Order.Items[0].UnitPrice, Is.EqualTo(price));
                Assert.That(result.Order.Items[0].Units, Is.EqualTo(units));
            });
        }

        [Test]
        public async Task Change_Status_From_Submitted_To_Shipped_Throws()
        {
            _orderReadOnlyRepository.Get(Arg.Any<Guid>()).Returns(new Domain.Orders.Order(new("a", "a", "a", "a", "a")));
            SetShippedStatusUseCase useCase = new(_orderReadOnlyRepository, _orderWriteOnlyRepository);
            var exception = Assert.ThrowsAsync<DomainException>(async () => await useCase.Execute(Guid.NewGuid()));
            Assert.That(exception.Message, Is.EqualTo("Is not possible to change the order status from submitted to shipped."));
        }
    }
}
