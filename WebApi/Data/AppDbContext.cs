using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.Data.Entities;

namespace WebApi.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<BillingMail> BillingMails { get; set; }
    public DbSet<BillingMailApplication> BillingMailApplications { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<BrandCategory> BrandCategories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartGadget> CartGadgets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<DiscountType> DiscountTypes { get; set; }
    public DbSet<FavoriteGadget> FavoriteGadgets { get; set; }
    public DbSet<Gadget> Gadgets { get; set; }
    public DbSet<GadgetImage> GadgetImages { get; set; }
    public DbSet<GadgetInformation> GadgetInformation { get; set; }
    public DbSet<GadgetRequest> GadgetRequests { get; set; }
    public DbSet<GadgetRequestBrand> GadgetRequestBrands { get; set; }
    public DbSet<GadgetRequestCategory> GadgetRequestCategories { get; set; }
    public DbSet<GadgetRequestSpecification> GadgetRequestSpecifications { get; set; }
    public DbSet<KeywordHistory> KeywordHistories { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<SearchHistory> SearchHistories { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<SellerApplication> SellerApplications { get; set; }
    public DbSet<SellerPage> SellerPages { get; set; }
    public DbSet<SellerPageImage> SellerPageImages { get; set; }
    public DbSet<SpecificationDefinition> SpecificationDefinitions { get; set; }
    public DbSet<SpecificationUnit> SpecificationUnits { get; set; }
    public DbSet<SpecificationValue> SpecificationValues { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserVerify> UserVerify { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<VoucherType> VoucherTypes { get; set; }
    public DbSet<VoucherUser> VoucherUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
