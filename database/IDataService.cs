using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudWeb.database
{
    public interface IDataService
    {
        Task<List<Item>> GetAllItemsAsync();

    }
}