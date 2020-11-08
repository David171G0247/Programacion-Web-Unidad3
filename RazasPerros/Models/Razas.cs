using System;
using System.Collections.Generic;

namespace RazasPerros.Models
{
    public partial class Razas
    {
        public uint Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string OtrosNombres { get; set; }
        public int IdPais { get; set; }
        public float PesoMin { get; set; }
        public float PesoMax { get; set; }
        public float AlturaMin { get; set; }
        public float AlturaMax { get; set; }
        public uint EsperanzaVida { get; set; }

        public Paises IdPaisNavigation { get; set; }
        public Caracteristicasfisicas Caracteristicasfisicas { get; set; }
        public Estadisticasraza Estadisticasraza { get; set; }
    }
}
