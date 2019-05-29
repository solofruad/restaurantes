using System;
using System.Collections.Generic;

namespace Restaurantes.Models
{
    public partial class Jornada
    {
        public Jornada()
        {
            Menu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Menu> Menu { get; set; }
    }
}
