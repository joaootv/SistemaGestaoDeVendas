using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Dominio
{
    [Table("ItemVenda")]
    public class ItemVenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double preco { get; set; }

        [Display(Name = "Produto")]
        public int produtoID { get; set; }
        [Display(Name = "Produto")]
        public Produto produto { get; set; }


        [Display(Name = "Venda")]
        public int vendaID { get; set; }
        [Display(Name = "Venda")]
        public Venda venda { get; set; }


        [Display(Name = "Total")]
        [NotMapped]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public virtual double subTotal
        {
            get { return (this.quantidade * this.preco); }
        }
    }
}
