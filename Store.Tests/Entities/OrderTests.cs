using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enum;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("Maximilliam Araujo", "maximillian.alex@gmail.com");
        private readonly Product _product = new Product("Relógio", 100, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
        {
            var order = new Order(_customer, 0, null);

            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_seu_status_deve_ser_aguardando_pagamento()
        {
            var order = new Order(_customer, 10, null);

            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pagamento_status_pedido_deve_ser_aguardando_entrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);
            order.Pay(100);

            Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_cancelado_seu_status_deve_ser_cancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();

            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_sem_um_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null, 10);

            Assert.AreEqual(order.Items.Count, 0);
        }
    }
}