﻿using Ewallet.Models;
using Ewallet.Models.DTO.ReturnDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ewallet.Commons
{
    public class PagedList<T>
    {
        public static PageMeta CreatePageMetaData(int page, int perPage, int total)
        {
            var total_pages = total % perPage == 0 ? total / perPage : total / perPage + 1;
            return new PageMeta
            {
                Page = page,
                PerPage = perPage,
                Total = total,
                TotalPages = total_pages
            };
        } 

        public static PaginatedListDTO<T> Paginate(List<T> source, int page, int perPage)
        {
            page = page < 1 ? 1 : page;
            var paginatedList = source.Skip((page - 1) * perPage).Take(perPage);
            var pageMeta = CreatePageMetaData(page, perPage, source.Count);

            return new PaginatedListDTO<T>
            {
                MetaData = pageMeta,
                Data = paginatedList,
            };
        }
    }
}
