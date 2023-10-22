using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class FundsDepositedEvent
    {
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"EventType: FundsDepositedEvent, Amount: {Amount}";
        }
    }
}
