using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Mapping
{
    public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
    {
        public void Configure(EntityTypeBuilder<UserEvent> builder)
        {
            builder.HasKey(t => new { t.UserID, t.EventID});
            builder.HasOne(e => e.Event).WithMany(e => e.UsersGoing).HasForeignKey(e => e.EventID);
            builder.HasOne(e => e.User).WithMany(e => e.Events).HasForeignKey(e => e.UserID);
        }
    }
}
