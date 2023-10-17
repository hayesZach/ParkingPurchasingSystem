using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    
    public class OrderProcessing
    {
        public static Random rng = new Random();
        public static event orderProcessedEvent orderProcessed;
        public static void processOrder(OrderClass order)
        {
            if (!isValidCard(order.getCardNumber()))
            {
                Console.WriteLine("{0} is an invalid card number, please enter a number between 5000 and 7000!", order.getCardNumber());
                return;
            }
            else
            {
                int tax = rng.Next(8, 12);      // generate random tax percentage between 8-12
                int locale = rng.Next(2, 8);    // generate random location charge between 2-8
                double taxValue = tax * 0.01 + 1.0;     // convert tax from percentage to decimal
                int total = Convert.ToInt32((tax * order.getUnitPrice() * order.getQuantity()) + locale);
                orderProcessed(order.getSenderId(), total, order.getUnitPrice(), order.getQuantity());  // trigger orderProcessed event (notify PrakingAgent)
            }
        }
        private static bool isValidCard(int cardNumber)
        {
            if (cardNumber >= 5000 && cardNumber <= 7000) return true;
            else return false;
        }
    }
}
