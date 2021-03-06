// <auto-generated />
using System;
using CarSharingDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarSharingDAL.Migrations
{
    [DbContext(typeof(CarSharingContext))]
    partial class CarSharingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarSharingDAL.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("NumberPlate")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("YearOfProduction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FromId")
                        .HasColumnType("int");

                    b.Property<int?>("RideId")
                        .HasColumnType("int");

                    b.Property<int>("ToId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromId");

                    b.HasIndex("RideId");

                    b.HasIndex("ToId");

                    b.HasIndex("UserId");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("CarSharingDAL.Entities.PathInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<int?>("RideId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RideId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityName = "Brno",
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CityName = "Praha",
                            Time = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Pictures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Url = "urlnaobrazok1"
                        },
                        new
                        {
                            Id = 2,
                            Url = "urlnaobrazok2"
                        },
                        new
                        {
                            Id = 3,
                            Url = "urlnaobrazok3"
                        });
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<int?>("PassengerId")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<int?>("RideId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PassengerId");

                    b.HasIndex("RideId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Ride", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("DriverId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("CarSharingDAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("PictureId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("PictureId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Kamil",
                            Password = "heslo",
                            PictureId = 1,
                            Surname = "Fňukal",
                            UserName = "user1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Patrícia",
                            Password = "bla",
                            PictureId = 2,
                            Surname = "Andicsová",
                            UserName = "patkajesaman"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dalibor",
                            Password = "ahoj",
                            PictureId = 3,
                            Surname = "Pantlík",
                            UserName = "dalibor"
                        });
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Car", b =>
                {
                    b.HasOne("CarSharingDAL.Entities.User", null)
                        .WithMany("Cars")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Passenger", b =>
                {
                    b.HasOne("CarSharingDAL.Entities.PathInfo", "From")
                        .WithMany()
                        .HasForeignKey("FromId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarSharingDAL.Entities.Ride", null)
                        .WithMany("Passengers")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CarSharingDAL.Entities.PathInfo", "To")
                        .WithMany()
                        .HasForeignKey("ToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarSharingDAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CarSharingDAL.Entities.PathInfo", b =>
                {
                    b.HasOne("CarSharingDAL.Entities.Ride", null)
                        .WithMany("Cities")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Picture", b =>
                {
                    b.HasOne("CarSharingDAL.Entities.Car", null)
                        .WithMany("Pictures")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Rating", b =>
                {
                    b.HasOne("CarSharingDAL.Entities.Passenger", null)
                        .WithMany("Ratings")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CarSharingDAL.Entities.Ride", null)
                        .WithMany("Ratings")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CarSharingDAL.Entities.Ride", b =>
                {
                    b.HasOne("CarSharingDAL.Entities.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CarSharingDAL.Entities.User", "Driver")
                        .WithMany("Rides")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CarSharingDAL.Entities.User", b =>
                {
                    b.HasOne("CarSharingDAL.Entities.Picture", "UserPicture")
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
