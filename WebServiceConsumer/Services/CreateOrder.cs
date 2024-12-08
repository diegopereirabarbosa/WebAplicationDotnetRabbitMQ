using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebServiceConsumer.Models;

namespace WebServiceConsumer.Services
{
    public class CreateOrderService : ICreateOrderService
    {
        public void CreateOrder( int fromId, int toId, string content)
        {
            Pedido pedido = JsonConvert.DeserializeObject<Pedido>(content);

            using (GestaoVendasContext _context = new GestaoVendasContext()) {
                try
                {
                    _context.Pedidos.Add(pedido);
                    _context.SaveChanges();
                }
                catch (Exception ex) {

                }

            }
        } 
    }
}
