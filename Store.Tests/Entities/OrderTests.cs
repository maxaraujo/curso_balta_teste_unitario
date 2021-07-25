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

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_com_quantidade_igual_ou_menor_que_zero_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 0);
            order.AddItem(_product, -1);

            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_valido_seu_total_deve_ser_210()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 2);

            Assert.AreEqual(order.Total(), 210);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_210()
        {
            Discount discount = new Discount(10, DateTime.Now.AddDays(-5));
            var order = new Order(_customer, 10, discount);
            order.AddItem(_product, 2);

            Assert.AreEqual(order.Total(), 210);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_200()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 2);

            Assert.AreEqual(order.Total(), 200);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_invalido_o_valor_do_pedido_deve_ser_100()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);

            Assert.AreEqual(order.Total(), 100);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_190()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(_product, 2);

            Assert.AreEqual(order.Total(), 190);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_uma_taxa_de_entrega_de_10_o_valor_pedido_deve_ser_110()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 1);

            Assert.AreEqual(order.Total(), 110);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
        {
            var order = new Order(null, 0, null);

            Assert.AreEqual(order.Valid, false);
        }
    }
}