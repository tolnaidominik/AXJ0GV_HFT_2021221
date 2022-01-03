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
                Price = 1000
            };
            Injection Distemper = new Injection()
            {
                Id = 2,
                Name = InjectionName.Canine_Distemper,
                Commonness = Commonness.Half_year,
                Price = 2000
            };
            Injection Hepatitis = new Injection()
            {
                Id = 3,
                Name = InjectionName.Canine_Hepatitis,
                Commonness = Commonness.Yearly,
                Price = 10000
            };
            Injection Parainfluenza = new Injection()
            {
                Id = 4,
                Name = InjectionName.Canine_Parainfluenza,
                Commonness = Commonness.Once,
                Price = 100000
            };
            Injection Heartworm = new Injection()
            {
                Id = 5,
                Name = InjectionName.Heartworm,
                Commonness = Commonness.Yearly,
                Price = 30000
            };
            Injection Leptospirosis = new Injection()
            {
                Id = 6,
                Name = InjectionName.Leptospirosis,
                Commonness = Commonness.Half_year,
                Price = 30020
            };
            Injection Parvovirus = new Injection()
            {
                Id = 7,
                Name = InjectionName.Parvovirus,
                Commonness = Commonness.Yearly,
                Price = 55500
            };
            Injection Rabies = new Injection()
            {
                Id = 8,
                Name = InjectionName.Rabies,
                Commonness = Commonness.Yearly,
                Price = 55500
            };
            Dog Afi = new Dog()
            {
                Id = 1,
                Name = "Áfi",
                Sex = Sex.Female,
                Species = "Pug",
                OwnerID = Doma.Id,
                InjectionID = Hepatitis.Id
            };
            Dog Mogyi = new Dog()
            {
                Id = 2,
                Name = "Mogyoró",
                Sex = Sex.Female,
                Species = "Yorki",
                OwnerID = Tubi.Id,
                InjectionID = Distemper.Id
            };
            
            modelBuilder.Entity<Dog>(entity =>
            {
                entity.HasOne(x => x.Owner)
                    .WithMany(x => x.Dogs)
                    .HasForeignKey(dog => dog.OwnerID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Dog>(entity =>
            {
                entity.HasOne(x => x.Injection)
                    .WithMany(x => x.Dogs)
                    .HasForeignKey(dog => dog.InjectionID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Owner>().HasData(Kritya, Doma, Tubi);
            modelBuilder.Entity<Dog>().HasData(Afi,Mogyi);
            modelBuilder.Entity<Injection>().HasData(Bordetella, Distemper, Hepatitis, Parainfluenza, Heartworm, Leptospirosis, Parvovirus, Rabies);
        }
    }
}
