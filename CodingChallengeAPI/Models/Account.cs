using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CodingChallengeAPI.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long AccountId { get; set; }
 
        [DefaultValue("")]
        public string AccountName { get; set; }

        [DefaultValue("")]
        public string IBAN { get; set; }

        [JsonIgnore]
        public virtual Outstanding? Outstanding { get; set; }
        [JsonIgnore]
        public virtual List<Deposit>? Deposits { get; set; }
        //[JsonIgnore]
        //public virtual List<TransferTransactions>? TransferTransactions { get; set; }

    }
}
