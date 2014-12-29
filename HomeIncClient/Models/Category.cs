using HomeIncClient.Core.Store;
using System.Collections.Generic;

namespace HomeIncClient.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public ICollection<Category> SubCategories { get; set; }
    }
}