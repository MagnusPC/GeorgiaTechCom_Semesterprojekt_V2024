﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Catalog.Domain.AggregateRoots;

namespace Webshop.Tools.TempSearchLib
{
    public interface ITempSearchRepos
    {
        Task CreateAsync(Product entity);
    }
}
