using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    public class ParkingAgent
    {
        public Random rng = new Random();
        public static event orderCreatedEvent orderCreated;
        public void agentFunc()
        {
            ParkingStructure parking = new ParkingStructure();
            while (Program.isRunning)
            {
                Thread.Sleep(500);
                int p = parking.getPrice();
                Console.WriteLine("Structure {0}: parking spaces on sale: as low as ${1} each!", Thread.CurrentThread.Name, p);
            }
        }

        // priceCut event handler
        public void parkingOnSale(String senderId, double unitPrice)
        {
            Console.WriteLine("Thread {0}: parking spaces on sale: as low as ${1} each!", Thread.CurrentThread.Name, unitPrice);
            createOrder(senderId, unitPrice);
        }

        private void createOrder(String senderId, double unitPrice)
        {
            int card = rng.Next(5000, 7000);
            int quantity = rng.Next(5, 50);     // # of spaces to purchase
            int receiver = rng.Next(1, 10);

            OrderClass order = new OrderClass(senderId, receiver.ToString(), card, quantity, unitPrice);
            Program.multiCellBuffer.setOneCell(senderId + "->" + receiver.ToString() + ": $" + unitPrice + "x" + quantity + " :: " + card);
            orderCreated();     // trigger event (notify subscribers)
        }

        // orderProcessed event handler (event triggered by OrderProcessing class)
        public void orderProcessed(String senderId, int total, double unitPrice, int quantity)
        {
            Console.WriteLine("Order from " + senderId + "\tTotal: " + total + "\t Quantity: " + quantity + " has been processed");
        }
    }
}
