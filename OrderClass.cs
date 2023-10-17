using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    public class OrderClass
    {
        private String senderId;
        private int cardNumber;
        private String receiverId;
        private int quantity;
        private double unitPrice;

        public OrderClass(String senderId, String receiverId, int cardNumber,  int quantity, double unitPrice)
        {
            this.senderId = senderId;           // sender identity
            this.cardNumber = cardNumber;       // credit card number
            this.receiverId = receiverId;       // receiver identity
            this.quantity = quantity;           // number of parking spaces ordered
            this.unitPrice = unitPrice;         // price per parking space
        }

        public String getSenderId() { return senderId; }
        public int getCardNumber() { return cardNumber; }
        public String getReceiverId() { return receiverId; }
        public int getQuantity() { return quantity; }
        public double getUnitPrice() { return unitPrice; }
        public void setSenderId(String id) { senderId = id; }
        public void setCardNumber(int num) { cardNumber = num; }
        public void setReceiverId(String id) { receiverId = id; }
        public void setQuantity(int q) { quantity = q; }
        public void setUnitPrice(double price) { unitPrice = price; }
    }
}