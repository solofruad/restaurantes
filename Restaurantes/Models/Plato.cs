using System;
using System.Collections.Generic;

namespace Restaurantes.Models
{
    public partial class Plato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Menu { get; set; }
        public int Categoria { get; set; }

        public Categoria CategoriaNavigation { get; set; }
        public Menu MenuNavigation { get; set; }
    }
}
