using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enum;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres(){
            var customer = new Customer("Maximilliam Araujo", "maximillian.alex@gmail.com");
            var order = new Order(customer, 0, null);

            Assert.AreEqual(8, order.Number.Length);
        }
    }
}