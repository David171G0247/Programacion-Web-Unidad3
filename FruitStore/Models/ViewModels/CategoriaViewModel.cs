using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitStore.Models.ViewModels
{
    public class CategoriaViewModel
    {
        public string Nombre { get; set; }
        public IEnumerable<Models.Productos> Productos { get; set; }
    }
}
