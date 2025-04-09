using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ErpDbContext1 : DbContext
    {
        public ErpDbContext1(DbContextOptions<ErpDbContext1> options) : base(options)
        {
        }

        public virtual DbSet<cdCurrAcc> cdCurrAcc { get; set; }
        public virtual DbSet<cdCurrAccDesc> cdCurrAccDesc { get; set; }
        public virtual DbSet<cdPromotionGroupDesc> cdPromotionGroupDesc { get; set; }
        public virtual DbSet<prCustomerVendorAccount> prCustomerVendorAccount { get; set; }
        public virtual DbSet<prCurrAccDefault> prCurrAccDefault { get; set; }
        public virtual DbSet<prCurrAccPostalAddress> prCurrAccPostalAddress { get; set; }
        public virtual DbSet<CustomerListDto> CustomerList { get; set; }
        public virtual DbSet<cdPriceGroup> cdPriceGroup { get; set; }
        public virtual DbSet<cdPriceGroupDesc> cdPriceGroupDesc { get; set; }
        public virtual DbSet<bsCustomerType> bsCustomerType { get; set; }
        public virtual DbSet<bsCustomerTypeDesc> bsCustomerTypeDesc { get; set; }
        public virtual DbSet<cdCustomerDiscountGr> cdCustomerDiscountGr { get; set; }
        public virtual DbSet<cdCustomerDiscountGrDesc> cdCustomerDiscountGrDesc { get; set; }
        public virtual DbSet<cdCustomerPaymentPlanGr> cdCustomerPaymentPlanGr { get; set; }
        public virtual DbSet<cdCustomerPaymentPlanGrDesc> cdCustomerPaymentPlanGrDesc { get; set; }
        public virtual DbSet<cdOffice> cdOffice { get; set; }
        public virtual DbSet<cdOfficeDesc> cdOfficeDesc { get; set; }
        public virtual DbSet<dfOfficeDefault> dfOfficeDefault { get; set; }
        public virtual DbSet<cdCityDesc> cdCityDesc { get; set; }
        public virtual DbSet<cdDistrictDesc> cdDistrictDesc { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // cdCurrAcc
            modelBuilder.Entity<cdCurrAcc>(entity =>
            {
                entity.HasKey(e => new { e.CurrAccTypeCode, e.CurrAccCode });
                entity.ToTable("cdCurrAcc");

                entity.HasOne(d => d.PromotionGroup)
                    .WithMany()
                    .HasForeignKey(d => d.PromotionGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // cdCurrAccDesc
            modelBuilder.Entity<cdCurrAccDesc>(entity =>
            {
                entity.HasKey(e => new { e.CurrAccTypeCode, e.CurrAccCode, e.LangCode });
                entity.ToTable("cdCurrAccDesc");

                entity.HasOne(d => d.CurrAcc)
                    .WithOne(p => p.CurrAccDesc)
                    .HasForeignKey<cdCurrAccDesc>(d => new { d.CurrAccTypeCode, d.CurrAccCode })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // cdPromotionGroup
            modelBuilder.Entity<cdPromotionGroup>(entity =>
            {
                entity.HasKey(e => e.PromotionGroupCode);
                entity.ToTable("cdPromotionGroup");
            });

            // cdPromotionGroupDesc
            modelBuilder.Entity<cdPromotionGroupDesc>(entity =>
            {
                entity.HasKey(e => new { e.PromotionGroupCode, e.LangCode });
                entity.ToTable("cdPromotionGroupDesc");

                entity.HasOne(d => d.PromotionGroup)
                    .WithMany()
                    .HasForeignKey(d => d.PromotionGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.Property(e => e.LangCode).HasDefaultValue("TR");

                entity.HasQueryFilter(e => e.LangCode == "TR");
            });

            // prCustomerVendorAccount
            modelBuilder.Entity<prCustomerVendorAccount>(entity =>
            {
                entity.HasKey(e => new { e.CurrAccTypeCode, e.CurrAccCode });
                entity.ToTable("prCustomerVendorAccount");

                entity.HasOne(d => d.CurrAcc)
                    .WithOne(p => p.CustomerVendorAccount)
                    .HasForeignKey<prCustomerVendorAccount>(d => new { d.CurrAccTypeCode, d.CurrAccCode })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // prCurrAccDefault
            modelBuilder.Entity<prCurrAccDefault>(entity =>
            {
                entity.HasKey(e => new { e.CurrAccTypeCode, e.CurrAccCode });
                entity.ToTable("prCurrAccDefault");

                entity.HasOne(d => d.CurrAcc)
                    .WithOne(p => p.CurrAccDefault)
                    .HasForeignKey<prCurrAccDefault>(d => new { d.CurrAccTypeCode, d.CurrAccCode })
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PostalAddress)
                    .WithOne(p => p.CurrAccDefault)
                    .HasForeignKey<prCurrAccDefault>(d => d.PostalAddressID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // prCurrAccPostalAddress
            modelBuilder.Entity<prCurrAccPostalAddress>(entity =>
            {
                entity.HasKey(e => e.PostalAddressID);
                entity.ToTable("prCurrAccPostalAddress");
            });

            // cdPriceGroup
            modelBuilder.Entity<cdPriceGroup>(entity =>
            {
                entity.HasKey(e => e.PriceGroupCode);
                entity.ToTable("cdPriceGroup");
            });

            // cdPriceGroupDesc
            modelBuilder.Entity<cdPriceGroupDesc>(entity =>
            {
                entity.HasKey(e => new { e.PriceGroupCode, e.LangCode });
                entity.ToTable("cdPriceGroupDesc");

                entity.HasOne(d => d.PriceGroup)
                    .WithOne(p => p.PriceGroupDesc)
                    .HasForeignKey<cdPriceGroupDesc>(d => d.PriceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // bsCustomerType
            modelBuilder.Entity<bsCustomerType>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeCode);
                entity.ToTable("bsCustomerType");
            });

            // bsCustomerTypeDesc
            modelBuilder.Entity<bsCustomerTypeDesc>(entity =>
            {
                entity.HasKey(e => new { e.CustomerTypeCode, e.LangCode });
                entity.ToTable("bsCustomerTypeDesc");

                entity.HasOne(d => d.CustomerType)
                    .WithOne(p => p.CustomerTypeDesc)
                    .HasForeignKey<bsCustomerTypeDesc>(d => d.CustomerTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // cdCustomerDiscountGr
            modelBuilder.Entity<cdCustomerDiscountGr>(entity =>
            {
                entity.HasKey(e => e.CustomerDiscountGrCode);
                entity.ToTable("cdCustomerDiscountGr");
            });

            // cdCustomerDiscountGrDesc
            modelBuilder.Entity<cdCustomerDiscountGrDesc>(entity =>
            {
                entity.HasKey(e => new { e.CustomerDiscountGrCode, e.LangCode });
                entity.ToTable("cdCustomerDiscountGrDesc");

                entity.HasOne(d => d.CustomerDiscountGr)
                    .WithOne(p => p.CustomerDiscountGrDesc)
                    .HasForeignKey<cdCustomerDiscountGrDesc>(d => d.CustomerDiscountGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // cdCustomerPaymentPlanGr
            modelBuilder.Entity<cdCustomerPaymentPlanGr>(entity =>
            {
                entity.HasKey(e => e.CustomerPaymentPlanGrCode);
                entity.ToTable("cdCustomerPaymentPlanGr");
            });

            // cdCustomerPaymentPlanGrDesc
            modelBuilder.Entity<cdCustomerPaymentPlanGrDesc>(entity =>
            {
                entity.HasKey(e => new { e.CustomerPaymentPlanGrCode, e.LangCode });
                entity.ToTable("cdCustomerPaymentPlanGrDesc");

                entity.HasOne(d => d.CustomerPaymentPlanGr)
                    .WithOne(p => p.CustomerPaymentPlanGrDesc)
                    .HasForeignKey<cdCustomerPaymentPlanGrDesc>(d => d.CustomerPaymentPlanGrCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // cdOffice
            modelBuilder.Entity<cdOffice>(entity =>
            {
                entity.HasKey(e => e.OfficeCode);
                entity.ToTable("cdOffice");
            });

            // cdOfficeDesc
            modelBuilder.Entity<cdOfficeDesc>(entity =>
            {
                entity.HasKey(e => new { e.OfficeCode, e.LangCode });
                entity.ToTable("cdOfficeDesc");

                entity.HasOne(d => d.Office)
                    .WithOne(p => p.OfficeDesc)
                    .HasForeignKey<cdOfficeDesc>(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // dfOfficeDefault
            modelBuilder.Entity<dfOfficeDefault>(entity =>
            {
                entity.HasKey(e => e.OfficeCode);
                entity.ToTable("dfOfficeDefault");

                entity.HasOne(d => d.Office)
                    .WithOne()
                    .HasForeignKey<dfOfficeDefault>(d => d.OfficeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Country)
                    .WithMany()
                    .HasForeignKey(d => d.CountryCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.State)
                    .WithMany()
                    .HasForeignKey(d => d.StateCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.City)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.District)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PriceGroup)
                    .WithMany()
                    .HasForeignKey(d => d.PriceGroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // cdCityDesc
            modelBuilder.Entity<cdCityDesc>(entity =>
            {
                entity.HasKey(e => new { e.CityCode, e.LangCode });
                entity.ToTable("cdCityDesc");

                entity.HasOne(d => d.City)
                    .WithMany()
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // cdDistrictDesc
            modelBuilder.Entity<cdDistrictDesc>(entity =>
            {
                entity.HasKey(e => new { e.DistrictCode, e.LangCode });
                entity.ToTable("cdDistrictDesc");

                entity.HasOne(d => d.District)
                    .WithMany()
                    .HasForeignKey(d => d.DistrictCode)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // CustomerListDto (View/Query result)
            modelBuilder.Entity<CustomerListDto>()
                .HasNoKey()
                .ToView(null);
        }
    }
} 