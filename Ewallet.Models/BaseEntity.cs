using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models
{
    public class BaseEntity
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public  DateTime LastModified { get; set; }
    }
}
