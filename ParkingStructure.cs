using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    public class ParkingStructure
    {
        int id = 0;
        static Random rng = new Random();
        public static event priceCutEvent priceCut;
        private static int parkingPrice = 10;
        private static int numPriceCuts = 0;

        public Int32 getPrice()
        {
            return parkingPrice;
        }

        public static void changePrice(int price)
        {
            if (price < parkingPrice)   // if price is less than previously set price
            {
                if (priceCut != null)
                {
                    priceCut(Thread.CurrentThread.Name, price);    // send priceCut event to parkingAgents
                    numPriceCuts++;
                }
            }
            parkingPrice = price;
        }

        public void PricingModel()
        {
            while (numPriceCuts < 20)
            {
                Thread.Sleep(2000);
                int p = rng.Next(10, 40);
                Console.WriteLine("New price is {0}", p);
                ParkingStructure.changePrice(p);
                //numPriceCuts++;
            }
            Program.isRunning = false;
        }

        // orderCreated event handler  
        public void startOrder()
        {
            // get order from multiCellBuffer
            String cell = Program.multiCellBuffer.getOneCell();
            OrderClass order = getOrderString(cell);
            Thread thread = new Thread(() => OrderProcessing.processOrder(order));
            thread.Start();
        }

        public OrderClass getOrderString(String cell)
        {
            // setOneCell(senderId + "->" + receiverId + ": $" + unitPrice + "x" + quantity + " :: " + card);
            OrderClass order;
            String senderId = "";
            String receiverId = "";
            String unitPrice = "";
            String quantity = "";
            String card = "";

            int i = 0;

            while (i < cell.Length && cell[i] != '-')
            {
                senderId += cell[i];
                i++;
            }
            i += 2;
            
            while (i < cell.Length && cell[i] != ':')
            {
                receiverId += cell[i];
                i++;
            }
            i += 3;

            while (i < cell.Length && cell[i] != 'x')
            {
                unitPrice += cell[i];
                i++;
            }
            i++;

            while (i < cell.Length && cell[i] != ':')
            {
                quantity += cell[i];
                i++;
            }
            i += 2;

            while (i < cell.Length)
            {
                card += cell[i];
                i++;
            }

            order = new OrderClass(senderId, receiverId, Convert.ToInt32(card), Convert.ToInt32(quantity), Convert.ToInt32(unitPrice));

            return order;
        }
    }
}
