using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO.InputDTO
{
    public class DebitDTO
    {
        public decimal Amount { get; set; }
        public string CurrencyId { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string FinancialInstitutionType { get; set; }
        public string InstitutionName { get; set; }
        public string BeneficiaryName { get;set; }
    }
}
