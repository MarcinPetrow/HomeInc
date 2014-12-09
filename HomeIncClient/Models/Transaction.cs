using System.ComponentModel.DataAnnotations.Schema;
using HomeIncClient.Core.Store;

namespace HomeIncClient.Models
{
    public class Transaction : Entity
    {
        [ForeignKey("CategoryId")] public Category Category;
        public int CategoryId;
        public string Description { get; set; }
        public double Value { get; set; }
    }
}