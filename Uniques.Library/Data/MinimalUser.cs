﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniques.Library.Data
{
    public class MinimalUser
    {
        public int Id { get; set; }

        public string Loginname { get; set; }

        public string Displayname { get; set; }

        public DateTime LastAction { get; set; }

        public bool IsAdmin { get; set; }
    }
}
