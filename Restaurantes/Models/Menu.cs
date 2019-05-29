using System;
using System.Collections.Generic;

namespace Restaurantes.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Plato = new HashSet<Plato>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Restaurante { get; set; }
        public int Jornada { get; set; }
        public DateTime? Fecha { get; set; }
        public string Tipo { get; set; }

        public Jornada JornadaNavigation { get; set; }
        public Restaurante RestauranteNavigation { get; set; }
        public ICollection<Plato> Plato { get; set; }
    }
}
