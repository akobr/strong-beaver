using GalaSoft.MvvmLight;
using StrongBeaver.Core.Model;
using System;
using System.Text;
using SQLite;

namespace StrongBeaver.Showroom.Model
{
    public class ExemplaryItem : ObservableObject, IComplexStoreItem<ExemplaryItem>
    {
        private static int idCounter = 0;
        private static readonly Random randomity = new Random();

        public ExemplaryItem()
        {
            Id = idCounter++;
            Text = GenerateRandomText();
            Decimal = GenerateRandomDecimal();
        }

        [PrimaryKey]
        public int Id { get; set; }

        public string Text { get; set; }

        public double Decimal { get; set; }

        public void Initialise()
        {

        }

        public void Update(ExemplaryItem newValue)
        {
            Text = newValue.Text;
            Decimal = newValue.Decimal;
        }

        public void Dispose()
        {

        }

        public override string ToString()
        {
            return $"Id={Id};{Environment.NewLine}Text={Text};{Environment.NewLine}Decimal={Decimal};";
        }

        private string GenerateRandomText()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                builder.Append((char)randomity.Next(33, 126));
            }

            return builder.ToString();
        }

        private double GenerateRandomDecimal()
        {
            return randomity.Next() + randomity.NextDouble();
        }
    }
}
