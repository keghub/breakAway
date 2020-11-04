using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakAway.Entities
{
    public interface IBreakAwayContext
    {
        DbSet<Activity> Activities { get; }
        DbSet<Contact> Contacts { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Destination> Destinations { get; }
        DbSet<Equipment> Equipments { get; }
        DbSet<Event> Events { get; }
        DbSet<Lodging> Lodgings { get; }
        DbSet<Payment> Payments { get; }
        DbSet<Reservation> Reservations { get; }

        int SaveChanges();
    }

    public class BreakAwayContext : DbContext, IBreakAwayContext
    {
        public BreakAwayContext(DbContextOptions<BreakAwayContext> options) : base(options)
        {
            Activities = Set<Activity>();
            Contacts = Set<Contact>();
            Customers = Set<Customer>();
            Destinations = Set<Destination>();
            Equipments = Set<Equipment>();
            Events = Set<Event>();
            Lodgings = Set<Lodging>();
            Payments = Set<Payment>();
            Reservations = Set<Reservation>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Activity> Activities { get; private set; }
        public DbSet<Contact> Contacts { get; private set; }
        public DbSet<Customer> Customers { get; private set; }
        public DbSet<Destination> Destinations { get; private set; }
        public DbSet<Equipment> Equipments { get; private set; }
        public DbSet<Event> Events { get; private set; }
        public DbSet<Lodging> Lodgings { get; private set; }
        public DbSet<Payment> Payments { get; private set; }
        public DbSet<Reservation> Reservations { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityEquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalInfoConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventActivityConfiguration());
            modelBuilder.ApplyConfiguration(new DestinationConfiguration());
            modelBuilder.ApplyConfiguration(new LodgingConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        }

        #region Mappings

        private class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
        {
            public void Configure(EntityTypeBuilder<Reservation> builder)
            {
                builder.ToTable("Reservations");

                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id).HasColumnName("ReservationID");

                builder.Property(e => e.ReservationDate).HasColumnType("DATETIME");

                builder.Property(e => e.RowVersion).IsRowVersion();

                builder.Property(e => e.ContactId).HasColumnName("ContactID");

                builder.Property(e => e.EventId).HasColumnName("EventID");

                builder.HasOne(e => e.Contact).WithMany(c => c.Reservations).HasForeignKey(e => e.ContactId);

                builder.HasOne(e => e.Event).WithMany().HasForeignKey(e => e.EventId);
            }
        }

        private class PaymentConfiguration : IEntityTypeConfiguration<Payment>
        {
            public void Configure(EntityTypeBuilder<Payment> builder)
            {
                builder.ToTable("Payments");

                builder.HasKey(e => e.Id);

                builder.Property(e => e.Id).HasColumnName("PaymentID");

                builder.Property(e => e.PaymentDate).HasColumnName("PaymentDate").HasColumnType("DATETIME");

                builder.Property(e => e.ReservationId).HasColumnName("ReservationID");

                builder.Property(e => e.Amount).HasColumnName("Amount").HasColumnType("MONEY");

                builder.Property(e => e.ModifiedDate).HasColumnType("DATETIME");

                builder.Property(e => e.RowVersion).IsRowVersion();

                builder.HasOne(e => e.Reservation).WithMany(e => e.Payments).HasForeignKey(e => e.ReservationId);
            }
        }

        private class LodgingConfiguration : IEntityTypeConfiguration<Lodging>
        {
            public void Configure(EntityTypeBuilder<Lodging> builder)
            {                
                builder.ToTable("Lodging");

                builder.HasKey(k => k.Id);

                builder.Property(k => k.Id).HasColumnName("LodgingID");

                builder.Property(k => k.Name).HasColumnName("LodgingName");

                builder.Property(k => k.ContactId).HasColumnName("ContactID");

                builder.Property(k => k.LocationId).HasColumnName("LocationID");

                builder.Property(k => k.IsResort).HasColumnName("Resort");

                builder.Property(k => k.ResortChainOwner).HasColumnName("ResortChainOwner");

                builder.Property(k => k.IsLuxuryResort).HasColumnName("LuxuryResort");

                builder.HasOne(k => k.Contact).WithMany().HasForeignKey(k => k.ContactId);
            }
        }

        private class DestinationConfiguration : IEntityTypeConfiguration<Destination>
        {
            public void Configure(EntityTypeBuilder<Destination> builder)
            {
                builder.ToTable("Locations");

                builder.HasKey(k => k.Id);

                builder.Property(k => k.Id).HasColumnName("LocationID");

                builder.Property(k => k.Name).HasColumnName("LocationName");
            }
        }

        private class EventConfiguration : IEntityTypeConfiguration<Event>
        {
            public void Configure(EntityTypeBuilder<Event> builder)
            {
                builder.ToTable("Events");
                
                builder.HasKey(k => k.Id);

                builder.Property(k => k.Id).HasColumnName("EventID");

                builder.Property(k => k.LocationId).HasColumnName("LocationID");

                builder.Property(k => k.LodgingId).HasColumnName("LodgingID");

                builder.Property(k => k.StartDate).HasColumnType("DATETIME");

                builder.Property(k => k.EndDate).HasColumnType("DATETIME");

                builder.Property(k => k.TripCostUSD);

                builder.HasMany(k => k.Activities).WithOne(o => o.Event).HasForeignKey(f => f.EventId);
            }
        }

        private class EventActivityConfiguration : IEntityTypeConfiguration<EventActivity>
        {
            public void Configure(EntityTypeBuilder<EventActivity> builder)
            {
                builder.ToTable("EventActivity");

                builder.HasKey(k => new { k.EventId, k.ActivityId });

                builder.Property(p => p.ActivityId).HasColumnName("ActivityId");

                builder.Property(p => p.EventId).HasColumnName("EventId");

                builder.HasOne(o => o.Activity).WithMany().HasForeignKey(f => f.ActivityId);

                builder.HasOne(o => o.Event).WithMany(m => m.Activities).HasForeignKey(f => f.EventId);
            }
        }

        private class ContactConfiguration : IEntityTypeConfiguration<Contact>
        {
            public void Configure(EntityTypeBuilder<Contact> builder)
            {
                builder.ToTable("Contact");

                builder.HasKey(k => k.Id);

                builder.Property(k => k.Id).HasColumnName("ContactID");

                builder.Property(k => k.FirstName).HasColumnName("FirstName");

                builder.Property(k => k.LastName).HasColumnName("LastName");

                builder.Property(k => k.Title).HasColumnName("Title");

                builder.Property(k => k.AddDate).HasColumnName("AddDate").HasColumnType("DATETIME");

                builder.Property(k => k.ModifiedDate).HasColumnName("ModifiedDate").HasColumnType("DATETIME");

                builder.Property(k => k.RowVersion).IsRowVersion();

                builder.HasMany(k => k.Addresses).WithOne(o => o.Contact).HasForeignKey(k => k.ContactId);

                builder.HasMany(k => k.Reservations).WithOne(o => o.Contact).HasForeignKey(k => k.ContactId);

                builder.Ignore(k => k.FullName);
            }
        }

        private class PersonalInfoConfiguration : IEntityTypeConfiguration<PersonalInfo>
        {
            public void Configure(EntityTypeBuilder<PersonalInfo> builder)
            {
                builder.ToTable("ContactPersonalInfo");

                builder.HasKey(k => k.ContactId);

                builder.Property(p => p.ContactId).HasColumnName("ContactID");
                
                builder.Property(k => k.BirthDate).HasColumnType("DATETIME");

                builder.Property(k => k.HeightInches);

                builder.Property(k => k.WeightPounds);

                builder.Property(k => k.DietaryRestrictions);
            }
        }

        private class CustomerConfiguration : IEntityTypeConfiguration<Customer>
        {
            public void Configure(EntityTypeBuilder<Customer> builder)
            {                
                builder.ToTable("Customers");

                builder.HasKey(k => k.ContactId);

                builder.Property(k => k.CustomerType).HasColumnName("CustomerTypeID");

                builder.Property(k => k.InitialDate).HasColumnType("DATETIME").HasColumnName("InitialDate");

                builder.Property(k => k.PrimaryDestinationId).HasColumnName("PrimaryDesintation");

                builder.Property(k => k.SecondaryDestinationId).HasColumnName("SecondaryDestination");

                builder.Property(k => k.PrimaryActivityId).HasColumnName("PrimaryActivity");

                builder.Property(k => k.SecondaryActivityId).HasColumnName("SecondaryActivity");

                builder.Property(k => k.Notes);

                builder.Property(k => k.CustomerRowVersion).HasColumnName("RowVersion").IsRowVersion().IsConcurrencyToken(false);

                builder.HasOne(k => k.PrimaryActivity).WithMany().HasForeignKey(k => k.PrimaryActivityId);

                builder.HasOne(k => k.SecondaryActivity).WithMany().HasForeignKey(k => k.SecondaryActivityId);

                builder.HasOne(k => k.PrimaryDestination).WithMany().HasForeignKey(k => k.PrimaryDestinationId);

                builder.HasOne(k => k.SecondaryDestination).WithMany().HasForeignKey(k => k.SecondaryDestinationId);

                builder.HasOne(o => o.Contact).WithMany().HasForeignKey(f => f.ContactId);
            }
        }

        private class AddressConfiguration : IEntityTypeConfiguration<Address>
        {
            public void Configure(EntityTypeBuilder<Address> builder)
            {
                builder.ToTable("Address");
                
                builder.HasKey(k => k.Id);

                builder.Property(k => k.Id).HasColumnName("AddressID");

                builder.Property(k => k.Street1).HasColumnName("Street1");

                builder.Property(k => k.Street2).HasColumnName("Street2");

                builder.Property(k => k.City).HasColumnName("City");

                builder.Property(k => k.StateProvince).HasColumnName("StateProvince");

                builder.Property(k => k.CountryRegion).HasColumnName("CountryRegion");

                builder.Property(k => k.PostalCode).HasColumnName("PostalCode");

                builder.Property(k => k.AddressType).HasColumnName("AddressType");

                builder.Property(k => k.ContactId).HasColumnName("ContactID");

                builder.Property(k => k.ModifiedDate).HasColumnType("DATETIME");

                builder.Property(k => k.RowVersion).HasColumnName("TimeStamp").IsRowVersion();

                builder.HasOne(k => k.Contact).WithMany(c => c.Addresses).HasForeignKey(k => k.ContactId);
            }
        }

        private class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
        {
            public void Configure(EntityTypeBuilder<Equipment> builder)
            {
                builder.ToTable("Equipment");

                builder.HasKey(k => k.Id);

                builder.Property(k => k.Id).HasColumnName("EquipmentID");

                builder.Property(k => k.Name).HasColumnName("Equipment");
            }
        }

        private class ActivityEquipmentConfiguration : IEntityTypeConfiguration<ActivityEquipment>
        {
            public void Configure(EntityTypeBuilder<ActivityEquipment> builder)
            {
                builder.ToTable("ActivityEquipment");

                builder.HasKey(k => new { k.ActivityId, k.EquipmentId });

                builder.Property(p => p.ActivityId).HasColumnType("ActivityId");

                builder.Property(p => p.EquipmentId).HasColumnType("EquipmentId");

                builder.HasOne(o => o.Activity).WithMany(m => m.Equipments).HasForeignKey(f => f.ActivityId);

                builder.HasOne(o => o.Equipment).WithMany().HasForeignKey(f => f.EquipmentId);
            }
        }

        private class ActivityConfiguration : IEntityTypeConfiguration<Activity>
        {
            public void Configure(EntityTypeBuilder<Activity> builder)
            {
                builder.ToTable("Activities");
                
                builder.HasKey(k => k.Id);

                builder.Property(k => k.Id).HasColumnName("ActivityID");

                builder.Property(k => k.Name).HasColumnName("Activity");

                builder.Property(k => k.ImagePath).HasColumnName("imagepath");

                builder.Property(k => k.Category).HasColumnName("Category");

                builder.HasMany(k => k.Equipments).WithOne(o => o.Activity).HasForeignKey(f => f.ActivityId);
            }
        }

        #endregion
    }
}