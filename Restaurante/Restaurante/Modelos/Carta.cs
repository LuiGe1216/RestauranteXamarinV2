using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurante.Modelos
{
    public class Carta
    {
        public int Id { get; set; }
        public string categoria { get; set; }
        public string Comida { get; set; }

        public string descripcion { get; set; }

        public string precio { get; set; }

        public Sede Sede { get; set; }
        [ForeignKey("SedeForeignKey")]
        public int IdSede { get; set; }
    }
}
