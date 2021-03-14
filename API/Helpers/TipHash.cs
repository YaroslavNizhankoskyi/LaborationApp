using API.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class TipHash : Tip
    {
        private readonly IDataProtector _protector;

        public TipHash(IDataProtectionProvider provider, Tip tip) : base(tip)
        {
            _protector = provider.CreateProtector("API.Helpers.TipHash");
        }


        public string Protect()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(HealthFactorId);
            sb.Append(LaborFactorId);
            sb.Append(MentalFactorId);
            sb.Append(SleepFactorId);

            FactorHash = _protector.Protect(sb.ToString());
            return FactorHash;
        }
    }
}
