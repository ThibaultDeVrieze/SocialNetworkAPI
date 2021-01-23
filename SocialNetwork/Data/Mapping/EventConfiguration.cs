using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Data.Mapping
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.EventID);
            builder.HasMany(e => e.Messages);
            builder.HasOne(e => e.Image);
            builder.HasMany(e => e.UsersGoing).WithOne(e => e.Event);
            builder.HasOne(e => e.Location);
            builder.HasOne(e => e.Founder);
        }
    }
}
