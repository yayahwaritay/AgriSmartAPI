using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgriSmartAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgroforestryGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Advice = table.Column<string>(type: "text", nullable: false),
                    Benefits = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgroforestryGuides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CropInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    InsuranceProvider = table.Column<string>(type: "text", nullable: false),
                    CoverageDetails = table.Column<string>(type: "text", nullable: false),
                    Premium = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropInsurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpertQAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    ExpertId = table.Column<string>(type: "text", nullable: false),
                    AnsweredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertQAs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmerTrainingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Location = table.Column<string>(type: "text", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: false),
                    PartnerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmerTrainingSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmProductivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    CropYield = table.Column<double>(type: "double precision", nullable: false),
                    InputUsage = table.Column<double>(type: "double precision", nullable: false),
                    ProfitMargin = table.Column<double>(type: "double precision", nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmProductivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    AreaInAcres = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FertilizerPesticideCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    FarmSize = table.Column<double>(type: "double precision", nullable: false),
                    CropType = table.Column<string>(type: "text", nullable: false),
                    FertilizerAmount = table.Column<double>(type: "double precision", nullable: false),
                    PesticideAmount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerPesticideCalculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForumPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Replies = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CooperativeId = table.Column<string>(type: "text", nullable: false),
                    CropName = table.Column<string>(type: "text", nullable: false),
                    TotalQuantity = table.Column<double>(type: "double precision", nullable: false),
                    NegotiatedPrice = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    SupplierId = table.Column<string>(type: "text", nullable: false),
                    Product = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    DeliveryLocation = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProductType = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    Reviews = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Region = table.Column<string>(type: "text", nullable: false),
                    CropType = table.Column<string>(type: "text", nullable: false),
                    LocalizedContent = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketplaceListings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<string>(type: "text", nullable: false),
                    CropName = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false),
                    AskingPrice = table.Column<double>(type: "double precision", nullable: false),
                    BuyerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketplaceListings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CropName = table.Column<string>(type: "text", nullable: false),
                    MarketLocation = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Microloans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    InstitutionName = table.Column<string>(type: "text", nullable: false),
                    LoanAmount = table.Column<double>(type: "double precision", nullable: false),
                    InterestRate = table.Column<double>(type: "double precision", nullable: false),
                    LoanPurpose = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Microloans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganicFarmingGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CertificationProcess = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganicFarmingGuides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partnerships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartnerName = table.Column<string>(type: "text", nullable: false),
                    PartnerType = table.Column<string>(type: "text", nullable: false),
                    SupportType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partnerships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoilType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RecommendedCrops = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PredictionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilPredictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SustainabilityReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    EnvironmentalImpact = table.Column<string>(type: "text", nullable: false),
                    Recommendations = table.Column<string>(type: "text", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SustainabilityReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VirtualFieldSchools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    FacilitatorId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualFieldSchools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterManagements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<string>(type: "text", nullable: false),
                    CropType = table.Column<string>(type: "text", nullable: false),
                    IrrigationSchedule = table.Column<string>(type: "text", nullable: false),
                    ConservationTechniques = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterManagements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PlantingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CareSchedule = table.Column<string>(type: "text", nullable: true),
                    HarvestSchedule = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crops_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PestDiagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FarmerId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Diagnosis = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TreatmentRecommendation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PestDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PestDiagnoses_Users_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crops_UserId",
                table: "Crops",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PestDiagnoses_FarmerId",
                table: "PestDiagnoses",
                column: "FarmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgroforestryGuides");

            migrationBuilder.DropTable(
                name: "CropInsurances");

            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "ExpertQAs");

            migrationBuilder.DropTable(
                name: "FarmerTrainingSessions");

            migrationBuilder.DropTable(
                name: "FarmProductivities");

            migrationBuilder.DropTable(
                name: "FarmSizes");

            migrationBuilder.DropTable(
                name: "FertilizerPesticideCalculations");

            migrationBuilder.DropTable(
                name: "ForumPosts");

            migrationBuilder.DropTable(
                name: "GroupSales");

            migrationBuilder.DropTable(
                name: "InputOrders");

            migrationBuilder.DropTable(
                name: "InputSuppliers");

            migrationBuilder.DropTable(
                name: "LocalizationContents");

            migrationBuilder.DropTable(
                name: "MarketplaceListings");

            migrationBuilder.DropTable(
                name: "MarketPrices");

            migrationBuilder.DropTable(
                name: "Microloans");

            migrationBuilder.DropTable(
                name: "OrganicFarmingGuides");

            migrationBuilder.DropTable(
                name: "Partnerships");

            migrationBuilder.DropTable(
                name: "PestDiagnoses");

            migrationBuilder.DropTable(
                name: "SoilPredictions");

            migrationBuilder.DropTable(
                name: "SustainabilityReports");

            migrationBuilder.DropTable(
                name: "TrainingContents");

            migrationBuilder.DropTable(
                name: "VirtualFieldSchools");

            migrationBuilder.DropTable(
                name: "WaterManagements");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
