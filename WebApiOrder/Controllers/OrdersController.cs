using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiOrder.Models;

namespace WebApiOrder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        //Rotorna Valor total do pedido
        [HttpGet]
        [Route("GetTotalValueOrder/{codigoPedido}")]
        public async Task<ActionResult<decimal>> GetTotalValueOrder([FromServices] GestaoVendasContext _context, int codigoPedido)
        {
            try
            {

                decimal totalOrder = (decimal) _context.Itens.Where(a => a.CodigoPedido == codigoPedido).Sum(a => a.Preco);
             
                return totalOrder;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Quantidade de Pedidos por Cliente
        [HttpGet]
        [Route("GetAmountOrdersByCustomer/{codigoCliente}")]
        public async Task<ActionResult<IEnumerable>> GetAmountOrdersByCustomer([FromServices] GestaoVendasContext _context, int codigoCliente)
        {
            try
            {
                Cliente customer = _context.Clientes.Find(codigoCliente); 
                int amountReturn = _context.Pedidos.Where(a=> a.CodigoCliente == codigoCliente).Count();

                return Ok( new
                {
                    CustomerName = customer.NomeRazaoSocial,
                    AmountOrders = amountReturn,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Lista de pedidos realizados por cliente
        [HttpGet]
        [Route("GetOrdersByCustomer/{codigoCliente}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetOrdersByCustomer([FromServices] GestaoVendasContext _context, int codigoCliente)
        {
            try
            {
                return await _context.Pedidos.AsNoTracking().Include(x=>x.Itens).Where(a=>a.CodigoCliente == codigoCliente).ToListAsync();
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
