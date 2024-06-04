using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProductCatalogChallenge.Application.Interfaces
{
    public interface IQueryHandler<TQuery,TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
