using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsAdminCreated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    CCCD = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoucherType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandCategory", x => new { x.BrandId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BrandCategory_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Keyword = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeywordHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentStatus = table.Column<string>(type: "text", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    ShopName = table.Column<string>(type: "text", nullable: false),
                    ShippingAddress = table.Column<string>(type: "text", nullable: false),
                    ShopAddress = table.Column<string>(type: "text", nullable: false),
                    BusinessModelId = table.Column<int>(type: "integer", nullable: false),
                    BusinessRegistrationCertificateUrl = table.Column<string>(type: "text", nullable: false),
                    TaxCode = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seller_BusinessModel_BusinessModelId",
                        column: x => x.BusinessModelId,
                        principalTable: "BusinessModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seller_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    ShopName = table.Column<string>(type: "text", nullable: false),
                    ShippingAddress = table.Column<string>(type: "text", nullable: false),
                    ShopAddress = table.Column<string>(type: "text", nullable: false),
                    BusinessModelId = table.Column<int>(type: "integer", nullable: false),
                    BusinessRegistrationCertificateUrl = table.Column<string>(type: "text", nullable: false),
                    TaxCode = table.Column<string>(type: "text", nullable: false),
                    RejectReason = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerApplication_BusinessModel_BusinessModelId",
                        column: x => x.BusinessModelId,
                        principalTable: "BusinessModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerApplication_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    DiscountAmount = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingMail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    SellerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingMail_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gadget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gadget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gadget_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gadget_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gadget_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GadgetRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    GadgetName = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GadgetRequest_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    BannerUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerPage_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecificationDefinition_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationDefinition_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    VoucherTypeId = table.Column<int>(type: "integer", nullable: false),
                    DiscountTypeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    MinAmount = table.Column<int>(type: "integer", nullable: false),
                    MaxAmount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voucher_DiscountType_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voucher_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Voucher_VoucherType_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalTable: "VoucherType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingMailApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mail = table.Column<string>(type: "text", nullable: false),
                    SellerApplicationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingMailApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingMailApplication_SellerApplication_SellerApplicationId",
                        column: x => x.SellerApplicationId,
                        principalTable: "SellerApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartGadget",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    GadgetId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartGadget", x => new { x.CartId, x.GadgetId });
                    table.ForeignKey(
                        name: "FK_CartGadget_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartGadget_Gadget_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteGadget",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GadgetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteGadget", x => new { x.UserId, x.GadgetId });
                    table.ForeignKey(
                        name: "FK_FavoriteGadget_Gadget_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteGadget_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GadgetImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GadgetId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GadgetImage_Gadget_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GadgetInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDetailId = table.Column<int>(type: "integer", nullable: false),
                    GadgetId = table.Column<int>(type: "integer", nullable: false),
                    GadgetName = table.Column<string>(type: "text", nullable: false),
                    GadgetPrice = table.Column<int>(type: "integer", nullable: false),
                    DiscountAmount = table.Column<int>(type: "integer", nullable: true),
                    GadgetQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GadgetInformation_Gadget_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GadgetInformation_OrderDetail_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GadgetId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchHistory_Gadget_GadgetId",
                        column: x => x.GadgetId,
                        principalTable: "Gadget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SearchHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GadgetRequestBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GadgetRequestId = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: true),
                    BrandName = table.Column<string>(type: "text", nullable: true),
                    LogoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetRequestBrand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GadgetRequestBrand_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GadgetRequestBrand_GadgetRequest_GadgetRequestId",
                        column: x => x.GadgetRequestId,
                        principalTable: "GadgetRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GadgetRequestCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GadgetRequestId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    CategoryName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetRequestCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GadgetRequestCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GadgetRequestCategory_GadgetRequest_GadgetRequestId",
                        column: x => x.GadgetRequestId,
                        principalTable: "GadgetRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerPageImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerPageId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerPageImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerPageImage_SellerPage_SellerPageId",
                        column: x => x.SellerPageId,
                        principalTable: "SellerPage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpecificationDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    SellerId = table.Column<int>(type: "integer", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: false),
                    SpecificationUnitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecificationValue_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecificationValue_SpecificationDefinition_SpecificationDef~",
                        column: x => x.SpecificationDefinitionId,
                        principalTable: "SpecificationDefinition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationValue_SpecificationUnit_SpecificationUnitId",
                        column: x => x.SpecificationUnitId,
                        principalTable: "SpecificationUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherUser",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherUser", x => new { x.VoucherId, x.UserId });
                    table.ForeignKey(
                        name: "FK_VoucherUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherUser_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GadgetRequestSpecification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GadgetRequestId = table.Column<int>(type: "integer", nullable: false),
                    SpecificationDefinitionId = table.Column<int>(type: "integer", nullable: true),
                    SpecificationDefinitionName = table.Column<string>(type: "text", nullable: true),
                    SpecificationValueId = table.Column<int>(type: "integer", nullable: true),
                    SpecificationValueValue = table.Column<string>(type: "text", nullable: true),
                    SpecificationUnitId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetRequestSpecification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GadgetRequestSpecification_GadgetRequest_GadgetRequestId",
                        column: x => x.GadgetRequestId,
                        principalTable: "GadgetRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GadgetRequestSpecification_SpecificationDefinition_Specific~",
                        column: x => x.SpecificationDefinitionId,
                        principalTable: "SpecificationDefinition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GadgetRequestSpecification_SpecificationUnit_SpecificationU~",
                        column: x => x.SpecificationUnitId,
                        principalTable: "SpecificationUnit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GadgetRequestSpecification_SpecificationValue_Specification~",
                        column: x => x.SpecificationValueId,
                        principalTable: "SpecificationValue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingMail_SellerId",
                table: "BillingMail",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingMailApplication_SellerApplicationId",
                table: "BillingMailApplication",
                column: "SellerApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandCategory_CategoryId",
                table: "BrandCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartGadget_GadgetId",
                table: "CartGadget",
                column: "GadgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteGadget_GadgetId",
                table: "FavoriteGadget",
                column: "GadgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Gadget_BrandId",
                table: "Gadget",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Gadget_CategoryId",
                table: "Gadget",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Gadget_SellerId",
                table: "Gadget",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetImage_GadgetId",
                table: "GadgetImage",
                column: "GadgetId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetInformation_GadgetId",
                table: "GadgetInformation",
                column: "GadgetId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetInformation_OrderDetailId",
                table: "GadgetInformation",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequest_SellerId",
                table: "GadgetRequest",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestBrand_BrandId",
                table: "GadgetRequestBrand",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestBrand_GadgetRequestId",
                table: "GadgetRequestBrand",
                column: "GadgetRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestCategory_CategoryId",
                table: "GadgetRequestCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestCategory_GadgetRequestId",
                table: "GadgetRequestCategory",
                column: "GadgetRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestSpecification_GadgetRequestId",
                table: "GadgetRequestSpecification",
                column: "GadgetRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestSpecification_SpecificationDefinitionId",
                table: "GadgetRequestSpecification",
                column: "SpecificationDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestSpecification_SpecificationUnitId",
                table: "GadgetRequestSpecification",
                column: "SpecificationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetRequestSpecification_SpecificationValueId",
                table: "GadgetRequestSpecification",
                column: "SpecificationValueId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordHistory_UserId",
                table: "KeywordHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistory_GadgetId",
                table: "SearchHistory",
                column: "GadgetId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistory_UserId",
                table: "SearchHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_BusinessModelId",
                table: "Seller",
                column: "BusinessModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_UserId",
                table: "Seller",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerApplication_BusinessModelId",
                table: "SellerApplication",
                column: "BusinessModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerApplication_UserId",
                table: "SellerApplication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerPage_SellerId",
                table: "SellerPage",
                column: "SellerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerPageImage_SellerPageId",
                table: "SellerPageImage",
                column: "SellerPageId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationDefinition_CategoryId",
                table: "SpecificationDefinition",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationDefinition_SellerId",
                table: "SpecificationDefinition",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationValue_SellerId",
                table: "SpecificationValue",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationValue_SpecificationDefinitionId",
                table: "SpecificationValue",
                column: "SpecificationDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationValue_SpecificationUnitId",
                table: "SpecificationValue",
                column: "SpecificationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_DiscountTypeId",
                table: "Voucher",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_SellerId",
                table: "Voucher",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_VoucherTypeId",
                table: "Voucher",
                column: "VoucherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUser_UserId",
                table: "VoucherUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingMail");

            migrationBuilder.DropTable(
                name: "BillingMailApplication");

            migrationBuilder.DropTable(
                name: "BrandCategory");

            migrationBuilder.DropTable(
                name: "CartGadget");

            migrationBuilder.DropTable(
                name: "FavoriteGadget");

            migrationBuilder.DropTable(
                name: "GadgetImage");

            migrationBuilder.DropTable(
                name: "GadgetInformation");

            migrationBuilder.DropTable(
                name: "GadgetRequestBrand");

            migrationBuilder.DropTable(
                name: "GadgetRequestCategory");

            migrationBuilder.DropTable(
                name: "GadgetRequestSpecification");

            migrationBuilder.DropTable(
                name: "KeywordHistory");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "SearchHistory");

            migrationBuilder.DropTable(
                name: "SellerPageImage");

            migrationBuilder.DropTable(
                name: "VoucherUser");

            migrationBuilder.DropTable(
                name: "SellerApplication");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "GadgetRequest");

            migrationBuilder.DropTable(
                name: "SpecificationValue");

            migrationBuilder.DropTable(
                name: "Gadget");

            migrationBuilder.DropTable(
                name: "SellerPage");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "SpecificationDefinition");

            migrationBuilder.DropTable(
                name: "SpecificationUnit");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "DiscountType");

            migrationBuilder.DropTable(
                name: "VoucherType");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropTable(
                name: "BusinessModel");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
