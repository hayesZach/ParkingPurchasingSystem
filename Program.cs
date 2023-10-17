using System;
using System.Threading;

namespace Project_2
{
    public delegate void priceCutEvent(String senderId, double unitPrice);
    public delegate void orderProcessedEvent(String senderId, int total, double unitPrice, int quantity);
    public delegate void orderCreatedEvent();

    public class Program
    {
        public static MultiCellBuffer multiCellBuffer;
        public static Thread[] agents;
        public static bool isRunning = true;
        static void Main(string[] args)
        {
            ParkingStructure parking = new ParkingStructure();
            ParkingAgent parkingAgent = new ParkingAgent();

            // create MultiCellBuffer
            multiCellBuffer = new MultiCellBuffer();

            Thread structure = new Thread(new ThreadStart(parking.PricingModel));
            structure.Start();
            structure.Name = "1";

            // subscribe to events
            ParkingStructure.priceCut += new priceCutEvent(parkingAgent.parkingOnSale);     // run parkingOnSale when priceCut
            ParkingAgent.orderCreated += new orderCreatedEvent(parking.startOrder);         // startOrder when orderCreated
            OrderProcessing.orderProcessed += new orderProcessedEvent(parkingAgent.orderProcessed);

            agents = new Thread[5];
            for (int i = 0; i < 1; i++)
            {
                agents[i] = new Thread(new ThreadStart(parkingAgent.agentFunc));
                agents[i].Name = (i + 1).ToString();
                agents[i].Start();
            }
        }
    }
}
