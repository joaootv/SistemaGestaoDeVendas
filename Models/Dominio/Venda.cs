using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Dominio
{
    public enum Status { Aberta, Finalizada }


    [Table("Venda")]
    public class Venda
    {
        public enum Status { Aberta, Finalizada }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Cód.")]
        public int id { get; set; }


        [Display(Name = "Cliente")]
        public int clienteID { get; set; }
        [Display(Name = "Cliente")]
        public Cliente cliente { get; set; }

        [Display(Name = "Data da Venda")]

        public DateTime dataVenda { get; set; }

        [Display(Name = "Status")]
        public Status status { get; set; }

        public ICollection<ItemVenda> itemVendas { get; set; }


        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public  float total { get; set; }
          
    }
}