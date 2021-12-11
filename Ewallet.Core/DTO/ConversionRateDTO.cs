using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ewallet.Core.DTO
{
    public class ConversionRateDTO
    {
        [JsonProperty("", NullValueHandling = NullValueHandling.Ignore)]
        public double ConvertTo { get; set; }
        [JsonProperty("PHP_USD", NullValueHandling = NullValueHandling.Ignore)]
        public double ConvertFrom { get; set; }

    }
}

