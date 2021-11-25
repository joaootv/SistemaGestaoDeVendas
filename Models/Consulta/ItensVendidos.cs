using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Consulta
{
    public class ItensVendidos
    {
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Cliente")]
        public string cliente { get; set; }
        [Display(Name = "Venda")]
        public int venda { get; set; }
        [Display(Name = "Data Venda")]
        public DateTime data { get; set; }
        [Display(Name = "Produto")]
        public string itemVendas { get; set; }
        [Display(Name = "Qtde")]
        public float quantidade { get; set; }
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public float valor { get; set; }
        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public float total { get; set; }

    }
}
