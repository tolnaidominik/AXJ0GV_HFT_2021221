﻿using AXJ0GV_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXJ0GV_HFT_2021221.Data
{
    public class DogDbContext : DbContext
    {
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Dog> Dogs { get; set; }
        public virtual DbSet<Injection> Injections { get; set; }

        public DogDbContext()
        {
            Database.EnsureCreated();
        }
        public DogDbContext(DbContextOptions<DogDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // "The primary key value needs to be specified even if it's usually generated by the database. It will be used to detect data changes between migrations"
            Owner Kritya = new Owner()
            {
                Id = 1,
                Name = "Kristóf",
                IdentityCardNumber = "ASD123",
                Sex = Sex.Male
            };
            Owner Doma = new Owner()
            {
                Id = 2,
                Name = "Dominik",
                IdentityCardNumber = "ASD321",
                Sex = Sex.Male
            };
            Owner Tubi = new Owner()
            {
                Id = 3,
                Name = "Eszter",
                IdentityCardNumber = "DSA321",
                Sex = Sex.Female
            };
            Injection Bordetella = new Injection()
            {
                Id = 1,
                Name = InjectionName.Bordetella_Bronchiseptica,
                Commonness = Commonness.Once,
            };
            Injection Distemper = new Injection()
            {
                Id = 2,
                Name = InjectionName.Canine_Distemper,
                Commonness = Commonness.Half_year,
            };
            Injection Hepatitis = new Injection()
            {
                Id = 3,
                Name = InjectionName.Canine_Hepatitis,
                Commonness = Commonness.Yearly,
            };
            Dog Pug = new Dog()
            {
                Id = 1,
                Name = "Áfi",
                Sex = Sex.Female,
                Species = "Pug",
                OwnerID = Doma.Id,
                InjectionID = Hepatitis.Id
            };
            
            modelBuilder.Entity<Dog>(entity =>
            {
                entity.HasOne(x => x.Owner)
                    .WithMany(x => x.Dogs)
                    .HasForeignKey(dog => dog.OwnerID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Dog>(entity =>
            {
                entity.HasOne(x => x.Injection)
                    .WithMany(x => x.Dogs)
                    .HasForeignKey(dog => dog.InjectionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Owner>().HasData(Kritya, Doma, Tubi);
            modelBuilder.Entity<Dog>().HasData(Pug);
            modelBuilder.Entity<Injection>().HasData(Bordetella, Distemper, Hepatitis);
        }
    }
}
