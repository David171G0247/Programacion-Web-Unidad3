using System;
using System.Collections.Generic;

namespace RazasPerros.Models
{
    public partial class Estadisticasraza
    {
        public uint Id { get; set; }
        public uint NivelEnergia { get; set; }
        public uint FacilidadEntrenamiento { get; set; }
        public uint EjercicioObligatorio { get; set; }
        public uint AmistadDesconocidos { get; set; }
        public uint AmistadPerros { get; set; }
        public uint NecesidadCepillado { get; set; }

        public Razas IdNavigation { get; set; }
    }
}
