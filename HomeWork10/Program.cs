using System;
using System.Xml;
using System.Collections.Generic;

namespace HomeWork10
{
    [Serializable]
    public class Payment
    {
        public int payForDay;
        public int countDays;
        public int mulctForDay;
        public int countMulctDays;
        public int sumPay;
        public int mulctPay;
        public int totalPay;
        public static bool IsFullSerial { get; set; }

        public Payment(int payForDay, int countDays, int mulctForDay, int countMulctDays = 0) 
        {
            this.payForDay = payForDay;
            this.countDays = countDays;
            this.mulctForDay = mulctForDay;
            this.countMulctDays = countMulctDays;
            sumPay = payForDay * countDays;
            mulctPay = mulctForDay * countMulctDays;
            totalPay = sumPay + mulctPay;
        }
    }
    internal class Program
    {
        static XmlDocument Serial(in List<Payment> list)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Payments");
            doc.AppendChild(root);

            foreach (Payment pay in list)
            {
                XmlElement employee = doc.CreateElement("employee");
                XmlElement employeePayForDay = doc.CreateElement("payForDay");
                employeePayForDay.InnerText = pay.payForDay.ToString();
                XmlElement employeeCountDays = doc.CreateElement("countDays");
                employeeCountDays.InnerText = pay.countDays.ToString();
                XmlElement employeeMulctForDay = doc.CreateElement("mulctForDay");
                employeeMulctForDay.InnerText = pay.mulctForDay.ToString();
                XmlElement employeeCountMulctDays = doc.CreateElement("countMulctDays");
                employeeCountMulctDays.InnerText = pay.countMulctDays.ToString();
                employee.AppendChild(employeePayForDay);
                employee.AppendChild(employeeCountDays);
                employee.AppendChild(employeeMulctForDay);
                employee.AppendChild(employeeCountMulctDays);

                if (Payment.IsFullSerial == true)
                {
                    XmlElement employeeSumPay = doc.CreateElement("sumPay");
                    employeeSumPay.InnerText = pay.sumPay.ToString();
                    XmlElement employeeMulctPay = doc.CreateElement("mulctPay");
                    employeeMulctPay.InnerText = pay.mulctPay.ToString();
                    XmlElement employeeTotalPay = doc.CreateElement("totalPay");
                    employeeTotalPay.InnerText = pay.totalPay.ToString();
                    employee.AppendChild(employeeSumPay);
                    employee.AppendChild(employeeMulctPay);
                    employee.AppendChild(employeeTotalPay);
                }
                root.AppendChild(employee);
            }
            return doc;
        }
        static void Main(string[] args)
        {
            List<Payment> list = new List<Payment>()
            {
                new Payment( payForDay: 1990, countDays: 21, mulctForDay: 1120),
                new Payment( payForDay: 1050, countDays: 19, mulctForDay: 520, countMulctDays: 3),
                new Payment( payForDay: 1670, countDays: 30, mulctForDay: 810, countMulctDays: 1)
            };

            Payment.IsFullSerial = false;
            XmlDocument document1 = Serial(in list);
            document1.Save("partialRecord.xml");

            Payment.IsFullSerial = true;
            XmlDocument document2 = Serial(in list);
            document2.Save("fullRecord.xml");

            
            
            XmlDocument document = new XmlDocument();
            document.Load("fullRecord.xml");
            XmlElement root = document.DocumentElement;

            List<Payment> newList = new List<Payment>();
            foreach(XmlElement employee in root)
            {
                int payForDay = 0;
                int countDays = 0;
                int mulctForDay = 0;
                int countMulctDays = 0;
                foreach(XmlElement field in employee.ChildNodes)
                {
                    switch (field.Name) 
                    {
                        case "payForDay":
                            payForDay = Convert.ToInt32(field.InnerText); break;
                        case "countDays":
                            countDays = Convert.ToInt32(field.InnerText); break;
                        case "mulctForDay":
                            mulctForDay = Convert.ToInt32(field.InnerText); break;
                        case "countMulctDays":
                            countMulctDays = Convert.ToInt32(field.InnerText); break;
                        default:
                            break;
                    }
                }
                newList.Add(new Payment(payForDay, countDays, mulctForDay, countMulctDays));
            }

            Console.WriteLine();
            Console.WriteLine("Total pay to second employee: " + newList[1].totalPay);
        }
    }
}
