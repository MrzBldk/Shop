using Basket.API.Controllers;
using Basket.API.Models;
using Basket.DAL.Entities;
using Basket.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Basket.UnitTests
{
    [TestFixture]
    public class BasketWebApiTests
    {
        private Mock<IBasketRepository> _basketRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _basketRepositoryMock = new();
        }

        [Test]
        public async Task Get_customer_basket_success()
        {
            //Arrange
            var fakeCustomerId = "1";
            var fakeCustomerBasket = GetCustomerBasketFake(fakeCustomerId);

            _basketRepositoryMock.Setup(x => x.GetBasketAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeCustomerBasket));

            //Act
            BasketController basketController = new(_basketRepositoryMock.Object);
            var actionResult = await basketController.GetBasketByIdAsync(fakeCustomerId);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That((actionResult.Result as OkObjectResult).StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(fakeCustomerId, Is.EqualTo((((ObjectResult)actionResult.Result).Value as CustomerBasketDto).BuyerId));
            });
        }

        [Test]
        public async Task Post_customer_basket_success()
        {
            //Arrange
            var fakeCustomerId = "1";
            var fakeCustomerBasket = GetCustomerBasketFake(fakeCustomerId);

            _basketRepositoryMock.Setup(x => x.UpdateBasketAsync(It.IsAny<CustomerBasket>()))
                .Returns(Task.FromResult(fakeCustomerBasket));

            //Act
            var basketController = new BasketController(_basketRepositoryMock.Object);
            var actionResult = await basketController.UpdateBasketAsync(fakeCustomerBasket.AdaptToDto());

            Console.WriteLine(actionResult);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That((actionResult.Result as OkObjectResult).StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(fakeCustomerId, Is.EqualTo((((ObjectResult)actionResult.Result).Value as CustomerBasketDto).BuyerId));
            });
        }

        private CustomerBasket GetCustomerBasketFake(string fakeCustomerId)
        {
            return new CustomerBasket(fakeCustomerId)
            {
                Items = new List<BasketItem>()
                {
                    new BasketItem()
                    {
                        Quantity = 1
                    }
                }
            };
        }
    }
}
