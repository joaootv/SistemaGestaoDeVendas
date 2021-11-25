using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Consulta
{
    public class VendaGrp
    {
        [Display(Name = "ID")]
        public int id { get; set; }
        public int ano { get; set; }
        public int mes { get; set; }
        [Display(Name = "Cliente")]
        public string cliente { get; set; }
        [Display(Name = "Qtde de Produtos")]
        public int qtdeProduto { get; set; }
        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public float total { get; set; }
    }
}
