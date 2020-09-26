﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test_auntefication.Models
{
    public interface ITabacosRepository
    {
        IQueryable<Tabaco> Tabacos { get; }
        void AddTabaco(Tabaco tb);
    }
}
