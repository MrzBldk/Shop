using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Commands.SetCancelledStatus
{
    public interface ISetCancelledStatusUseCase
    {
        public Task Execute(Guid orderId);
    }
}
