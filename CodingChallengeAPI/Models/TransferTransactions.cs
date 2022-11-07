using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CodingChallengeAPI.Models
{
    public class TransferTransactions
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long TransactionsId { get; set; }

        [DefaultValue(typeof(Decimal), "0.00")]
        [Precision(18, 2)]
        public decimal TransferAmount { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ForeignKey("AccountId")]
        public long? TransferFrom { get; set; }
        public long? TransferTo { get; set; }
        //[JsonIgnore]
        //public virtual Account Account { get; set; }



    }
}
