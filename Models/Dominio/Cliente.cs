using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Dominio
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int id { get; set; }

        [StringLength(35, ErrorMessage = "Tamanho nome do cliente inválido", MinimumLength = 5)]
        [Required(ErrorMessage = "Campo Nome Cliente é obrigatório")]
        [Display(Name = "Nome do Cliente")]
        public string nome { get; set; }

        [Display(Name = "CPF")]
        [StringLength(14, ErrorMessage = "Não aceita CPF com mais de 14 dígitos")]
        [Remote("ValidarCPF", "Clientes", ErrorMessage = "CPF Inválido!!!")]
        public string cpf { get; set; }

        [StringLength(50, ErrorMessage = "Tamanho de endereço inválido - 50")]
        [Required(ErrorMessage = "Campo Endereço é obrigatório")]
        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [StringLength(25, ErrorMessage = "Tamanho de nome do bairro inválido - 25")]
        [Required(ErrorMessage = "Campo Bairro é obrigatório")]
        [Display(Name = "Bairro")]
        public string bairro { get; set; }

        [StringLength(25, ErrorMessage = "Tamanho de nome do município inválido - 25")]
        [Required(ErrorMessage = "Campo Município é obrigatório")]
        [Display(Name = "Cidade")]
        public string cidade { get; set; }

        [StringLength(2, ErrorMessage = "Tamanho de UF inválido - 2", MinimumLength = 2)]
        [Required(ErrorMessage = "Campo UF é obrigatório")]
        [Display(Name = "UF")]
        public string uf { get; set; }

        [Display(Name = "Telefone")]
        [StringLength(15, ErrorMessage = "Telefone maior que 15 caracteres")]
        [Required(ErrorMessage = "Campo Telefone é obrigatório")]
        public string telefone { get; set; }

        [Display(Name = "E-Mail")]
        [StringLength(35, ErrorMessage = "E-Mail maior que 35 caracteres")]
        [RegularExpression("^[a-zA-Z0-9_+-]+[a-zA-Z0-9._+-]*[a-zA-Z0-9_+-]+@[a-zA-Z0-9_+-]+[a-zA-Z0-9._+-]*[.]{1,1}[a-zA-Z]{2,}$", ErrorMessage = "Email invalido")]
        public string email { get; set; }

        public ICollection<Venda> vendas { get; set; }
    }
}
