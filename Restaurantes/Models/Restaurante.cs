using System;
using System.Collections.Generic;

namespace Restaurantes.Models
{
    public partial class Restaurante
    {
        public Restaurante()
        {
            Menu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Horario { get; set; }
        public int Principal { get; set; }
        public int? Capacidad { get; set; }

        public ICollection<Menu> Menu { get; set; }
    }
}
