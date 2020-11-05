﻿using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUsers> Users { get; set; }

        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<UserLike>()
                .HasKey(key =>  new {key.SourceUserId,key.LikedUserId });
            
            builder.Entity<UserLike>()
                .HasOne(source => source.SourceUser)
                .WithMany(liked => liked.LikedUsers)
                .HasForeignKey(fk => fk.SourceUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserLike>()
                .HasOne(source => source.LikedUser)
                .WithMany(liked => liked.LikedByUsers)
                .HasForeignKey(fk => fk.LikedUserId)
                .OnDelete(DeleteBehavior.NoAction);

        }


    }
}
