using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CodingChallengeAPI.Models
{
    public class Deposit
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long DepositId { get; set; }

        [DefaultValue(typeof(Decimal), "0.00")]
        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [DefaultValue(typeof(Decimal), "0.00")]
        [JsonIgnore]
        [Precision(18, 2)]
        public decimal FeeAmount { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [ForeignKey("Account")]
        public long? AccountId { get; set; }
        [JsonIgnore]
        public virtual Account? Account { get; set; }

    }
}
