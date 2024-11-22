using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Итоговый_проект__Продвинутый_уровень_
{

    // Абстрактный класс для доставки
    abstract class Delivery
    {
        public string Address { get; set; }
        public abstract void Deliver();
    }

    // Статический класс для управления заказами
    static class OrderManager
    {
        public static void ProcessOrder<TDelivery>(Order<TDelivery> order) where TDelivery : Delivery
        {
            Console.WriteLine($"Обработка заказа #{order.Number}: {order.Description}");
            order.Delivery.Deliver();
        }
    }

    // Доставка на дом
    class HomeDelivery : Delivery
    {
        public string CourierName { get; set; }

        public HomeDelivery(string address, string courierName)
        {
            Address = address;
            CourierName = courierName;
        }

        public override void Deliver()
        {
            Console.WriteLine($"Доставка на дом по адресу {Address} курьером {CourierName}");
        }
    }

    // Доставка в пункт выдачи
    class PickPointDelivery : Delivery
    {
        public string PickupPoint { get; set; }

        public PickPointDelivery(string address, string pickupPoint)
        {
            Address = address;
            PickupPoint = pickupPoint;
        }

        public override void Deliver()
        {
            Console.WriteLine($"Доставка в пункт выдачи: {PickupPoint}, по адресу {Address}");
        }
    }

    // Класс заказа
    class Order<TDelivery> where TDelivery : Delivery
    {
        public TDelivery Delivery { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }

        public Order(TDelivery delivery, int number, string description)
        {
            Delivery = delivery;
            Number = number;
            Description = description;
        }

        public void DisplayAddress()
        {
            Console.WriteLine($"Адрес доставки: {Delivery.Address}");
        }

        public void ChangeAddress(string newAddress)
        {
            Delivery.Address = newAddress;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var homeOrder = new Order<HomeDelivery>(
                new HomeDelivery("ул. Ленина, 12", "Иван"),
                1,
                "Товар 1"
            );

            var pickPointOrder = new Order<PickPointDelivery>(
                new PickPointDelivery("ул. Мира, 34", "Пункт 1"),
                2,
                "Товар 2"
            );

            // Использование статического метода для обработки заказов
            OrderManager.ProcessOrder(homeOrder);
            OrderManager.ProcessOrder(pickPointOrder);

            homeOrder.ChangeAddress("ул. Пушкина, 15");
            homeOrder.DisplayAddress();
        }
    }

}
