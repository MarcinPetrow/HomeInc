using System.Collections.Generic;
using HomeIncClient.Core.Store;

namespace HomeIncClient.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<Category> SubCategories { get; set; }
    }
}