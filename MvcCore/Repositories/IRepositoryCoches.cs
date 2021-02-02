using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public interface IRepositoryCoches
    {
        List<Coche> GetCoches();
    }
}
