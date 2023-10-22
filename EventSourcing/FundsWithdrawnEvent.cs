using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public class FundsWithdrawnEvent
    {
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"EventType: FundsWithdrawnEvent, Amount: {Amount}";
        }
    }
}
