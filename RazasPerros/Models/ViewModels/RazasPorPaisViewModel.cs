using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazasPerros.Models.ViewModels
{
    public class RazasPorPaisViewModel
    {
        public string Pais { get; set; }
        public IEnumerable<Models.Razas> Razas { get; set; }
    }
}
