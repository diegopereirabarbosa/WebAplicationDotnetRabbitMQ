using System;
using System.Collections.Generic;

namespace WebApiSendOrder.Models;

public partial class Iten
{

    public string Produto { get; set; } = null!;

    public int Quantidade { get; set; }

    public decimal? Preco { get; set; }

    public int CodigoPedido { get; set; }

}
