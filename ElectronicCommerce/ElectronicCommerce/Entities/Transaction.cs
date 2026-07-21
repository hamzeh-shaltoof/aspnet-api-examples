using ElectronicCommerce.Entities;
using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Enums;
using ElectronicCommerce.Helper;

namespace ElectronicCommerce.Entities
{
    public class Transaction : BaseEntity
    {
        public Transaction() 
        {
            // Move Code ?!
            ProviderTransactionId = TransactionHelper.GetRandomPrefix() + Guid.NewGuid().ToString().Substring(0,8);
            TransactionCode = $"TRX-{Guid.NewGuid().ToString().Substring(0,6)}";
        }
       
        public string TransactionCode { get; set; } = null!;
        public string ProviderTransactionId { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal? Fee { get; set; }
        public decimal? NetAmount { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType Type { get; set; }
        public PaymentMethod Method { get; set; }

        public string Currency { get; set; } = null!;
        public int PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;
    }
}



