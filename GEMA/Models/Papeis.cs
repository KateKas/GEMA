﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    public class Papeis
    {
        public Papeis()
        {
            this.Pessoas = new HashSet<Pessoas>();
        }

        public int Id { get; set; }

        public string Papel { get; set; }

        public virtual ICollection<Pessoas> Pessoas { get; set; }
    }
}