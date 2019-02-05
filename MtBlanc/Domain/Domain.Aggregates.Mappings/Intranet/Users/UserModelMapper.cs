using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Domain.Intranet.Users
{
    public class UserModelMapper : IEntityModelMapper
    {
        public ProviderInfo Provider
        {
            get { return ProviderInfo.SqlServer2008; }
        }

        public string AggregateName { get { return nameof(User); } }

        public void Map(DbModelBuilder modelBuilder)
        {
            MapUsers(modelBuilder.Entity<User>());
            MapRoles(modelBuilder.Entity<Role>());
            MapUserSites(modelBuilder.Entity<UserSite>());
        }

        private void MapUsers(EntityTypeConfiguration<User> entity)
        {
            entity.HasKey(p => p.Id).ToTable("Users", "intranet");

            entity.Property(p => p.Id).HasColumnName("ID");

            entity.Property(p => p.Name);

            entity.Property(p => p.Email);

            entity.Property(p => p.SiteId).HasColumnName("FKSiteID");

            entity.HasRequired(p => p.Site).WithMany().HasForeignKey(p => p.SiteId);

            entity.HasMany(e => e.Roles).WithMany().Map(m => m.MapLeftKey("FKUserID").MapRightKey("FKRoleID").ToTable("UserRoles", "intranet"));
        }

        private void MapRoles(EntityTypeConfiguration<Role> entity)
        {
            entity.HasKey(p => p.Id).ToTable("Roles", "intranet");

            entity.Property(p => p.Id).HasColumnName("ID");

            entity.Property(p => p.Name);
        }

        private void MapUserSites(EntityTypeConfiguration<UserSite> entity)
        {
            entity.HasKey(p => p.Id).ToTable("Sites", "site");

            entity.Property(p => p.Id).HasColumnName("ID");

            entity.Property(p => p.Domain);
        }
    }
}
