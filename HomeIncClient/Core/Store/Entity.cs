using System;
using System.ComponentModel.DataAnnotations;

namespace HomeIncClient.Core.Store
{
    public class Entity
    {
        public Entity()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsDeleted = false;
        }

        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}