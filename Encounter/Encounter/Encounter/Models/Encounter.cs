using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Encounter.Models
{
	public class Encounter
	{
        public int CR { get; set; }
        public List<Monster> Monsters { get; set; }
    }
}

