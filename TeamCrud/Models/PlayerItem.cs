using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamCrud.Models
{
    public class PlayerItem
    {
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}