using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models
{
    public class BeneficiaryData
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string WalletOrAccountAddress { get; set; }
      
    }
}
