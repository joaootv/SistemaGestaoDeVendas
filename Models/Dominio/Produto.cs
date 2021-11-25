using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Dominio
{
    public enum Categoria { Calça, Camisa, Camiseta, Moletom, Boné, Calçado, Body, Chapéu, Outros }

    [Table("Produto")]
    public class Produto
    {
        public enum Categoria { Calça, Camisa, Camiseta, Moletom, Boné, Calçado, Body, Chapéu, Outros }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [StringLength(35, ErrorMessage = "Tamanho nome do produto inválido", MinimumLength = 5)]
        [Required(ErrorMessage = "Campo Nome Produto é obrigatório")]
        [Display(Name = "Nome do Produto")]
        public string nome { get; set; }


        [Display(Name = "Categoria")]
        public Categoria categoria { get; set; }


        [Display(Name = "Tamanho")]
        [Required(ErrorMessage = "Campo Tamanho é obrigatório")]
        public string tamanho { get; set; }

        [Display(Name = "Preço")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public float preco { get; set; }

        [Display(Name = "Quantidade")]
        public int quantidade { get; set; }

        [Display(Name = "Valor Estoque")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [NotMapped]
        public virtual float valorEstoque
        {
            get { return (float)(this.quantidade * this.preco); }
        }

        [NotMapped]
        public string nomeTam { 
            get { return nome+" "+tamanho; } 
        }

        public ICollection<ItemVenda> itemVendas { get; set; }
    }
}
