using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Domain.Entities
{
    public class Entity
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }

        [Column("UPDATED_AT")]
        public DateTime UpdatedAt { get; set; }

        [Column("DELETED")]
        public bool Deleted { get; set; }

        //TODO: ChangedByUserId in all tables
        //[Column("CHANGED_BY_USER_ID")]
        //public Guid ChangedByUserId { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Deleted = false;
        }
    }
}
