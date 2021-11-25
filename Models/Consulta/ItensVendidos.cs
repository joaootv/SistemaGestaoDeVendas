using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Consulta
{
    public class ItensVendidos
    {
        public int id { get; set; }
        public string cliente { get; set; }
        public int venda { get; set; }
        public DateTime data { get; set; }
        public string itemVendas { get; set; }
        public float quantidade { get; set; }
        public float valor { get; set; }
        public float total { get; set; }

    }
}
