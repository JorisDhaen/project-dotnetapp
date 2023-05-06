using Domain;

namespace Tests
{
    public class OrderTest
    {
        OrderStatus status;
        Order order;
        ISet<OrderItem> orders;
        public OrderTest() {
            OrderItem ordit = new OrderItem("11111", 5, 50, new Product("1111", "testProduct", "test test", 10, 30, 5.5, 6.6, 5.5));
            OrderItem ordit2 = new OrderItem("22222", 5, 50, new Product("2222", "testProduct2", "test test", 10, 30, 5.5, 6.6, 5.5));
            OrderItem ordit3 = new OrderItem("33333", 5, 50, new Product("3333", "testProduct3", "test test", 10, 30, 5.5, 6.6, 5.5));
            orders = new HashSet<OrderItem>();
            status = new OrderStatus();
            order = new Order();
            orders.Add(ordit);
            orders.Add(ordit2);
            orders.Add(ordit3);
            order.Items.Add(ordit);
            order.Items.Add(ordit2);
            order.Items.Add(ordit3);
            order.ChangeStatus(status);
         }

        [Fact]
        public void StatusTest()
        {
            Assert.Equal(order._orderStatus, status);
        }

        [Fact]
        public void ItemsTest()
        {
            Assert.Equal(order.Items, orders);
        }
    }
}
