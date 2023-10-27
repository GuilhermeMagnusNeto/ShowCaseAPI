using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowCaseAPI.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Domain.Entities
{
    [Table(name: "users")]
    public class User : Entity
    {
        [Column("PASSWORD_HASH")]
        public string PasswordHash { get; set; }

        [Column("PASSWORD_SALT")]
        public string PasswordSalt { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("EMAIL_CONFIRMED")]
        public bool EmailConfirmed { get; set; }

        //TODO: SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoudEnd, LockoudEnabled, AcessFailedCount, Token
    }

    public static class UserDbBuilder
    {
        public static EntityTypeBuilder<TEntity> ConfigureUnique<TEntity>(this EntityTypeBuilder<TEntity> self, ModelBuilder modelBuilder) where TEntity : User
        {
            self.HasKey(x => x.Id);
            self.HasIndex(x => x.Id).IsUnique(); 
            self.Property(x => x.Deleted).HasDefaultValue(false);

            self.HasIndex(x => x.Email).IsUnique();
            self.Property(x => x.EmailConfirmed).HasDefaultValue(false);
            self.Property(x => x.PasswordHash).IsRequired();
            self.Property(x => x.PasswordSalt).IsRequired();
            self.Property(x => x.Email).IsRequired();
            self.Property(x => x.EmailConfirmed).IsRequired();

            return self;
        }
    }
}
