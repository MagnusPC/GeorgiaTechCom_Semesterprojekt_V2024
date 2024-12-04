using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Tools.TempSearchLib
{
    public interface ITempSearchRepos
    {
        Task CreateAsync(Object obj);
    }
}
