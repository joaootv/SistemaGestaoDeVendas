using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestaoDeVendas.Models.Dominio
{
    public class ViewModel
    {
            public Venda Venda { get; set; }
            public IEnumerable<ItemVenda>  ItemVenda { get; set; }
        }
    }
