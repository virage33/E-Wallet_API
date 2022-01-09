using System;
using System.Collections.Generic;
using System.Text;

namespace Ewallet.Models.DTO.ReturnDTO
{
    public class PaginatedListDTO<T>
    {
        public PageMeta MetaData { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginatedListDTO()
        {
            Data = new List<T>();
        }
    }
}
