using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain.Core.Sites
{
    public class SiteModelMapper : IEntityModelMapper
    {

        public ProviderInfo Provider
        {
            get { return ProviderInfo.SqlServer2008; }
        }

        public string AggregateName { get { return nameof(Site); } }

        public void Map(DbModelBuilder modelBuilder)
        {
            MapSite(modelBuilder.Entity<Site>());
            MapSiteTypes(modelBuilder.Entity<SiteType>());
        }

        private void MapSite(EntityTypeConfiguration<Site> entity)
        {
            entity.HasKey(p=>p.Id).ToTable("Sites","site");

            entity.Property(p => p.Id).HasColumnName("ID");

            entity.Property(p => p.Domain)
                .HasColumnName("Domain")
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(p => p.IsActive);

            entity.Property(p => p.TypeId).HasColumnName("FKTypeID");

            entity.HasRequired(p => p.Type).WithMany().HasForeignKey(p => p.TypeId);

        }

        private void MapSiteTypes(EntityTypeConfiguration<SiteType> entity)
        {
            entity.HasKey(p=>p.Id).ToTable("SiteTypes","site");

            entity.Property(p => p.Id).HasColumnName("ID");

            entity.Property(p => p.Name);

            entity.Property(p => p.RequiresWww);
        }

    }
}
