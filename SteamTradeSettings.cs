using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamTrader
{
    public class SteamTradeSettings
    {
        public Decimal MinimumPrice { get; set; }

        public Decimal MaximumPrice { get; set; }

        public Decimal SellPriceMultiplier { get; set; }

        public Decimal SpecialSellPriceMultiplier { get; set; }

        public Decimal ConstantPriceSubtractor { get; set; }

        public int DelayMilliseconds { get; set; }

        public IList<String> IgnoredItems { get; set; }

        public Trader.Strategy Strategy { get; set; }

    }
}
