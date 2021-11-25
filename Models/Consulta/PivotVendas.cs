using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Consulta
{
    public class PivotVendas
    {
        [Display(Name = "ID")]
        public int id { get; set; }
        [Display(Name = "Ano")]
        public int ano { get; set; }
        [Display(Name = "Jan")]
        public int mes1 { get; set; }
        [Display(Name = "Fev")]
        public int mes2 { get; set; }
        [Display(Name = "Mar")]
        public int mes3 { get; set; }
        [Display(Name = "Abr")]
        public int mes4 { get; set; }
        [Display(Name = "Mai")]
        public int mes5 { get; set; }
        [Display(Name = "Jun")]
        public int mes6 { get; set; }
        [Display(Name = "Jul")]
        public int mes7 { get; set; }
        [Display(Name = "Ago")]
        public int mes8 { get; set; }
        [Display(Name = "Set")]
        public int mes9 { get; set; }
        [Display(Name = "Out")]
        public int mes10 { get; set; }
        [Display(Name = "Nov")]
        public int mes11 { get; set; }
        [Display(Name = "Dez")]
        public int mes12 { get; set; }
    }
}
