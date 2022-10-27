using Store.Application.Common.Interfaces;

namespace Store.Infrasructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
