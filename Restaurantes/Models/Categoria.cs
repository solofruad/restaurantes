using System;
using System.Collections.Generic;

namespace Restaurantes.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Plato = new HashSet<Plato>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<Plato> Plato { get; set; }
    }
}
