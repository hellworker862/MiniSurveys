﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniSurveys.Domain.Data;

#nullable disable

namespace MiniSurveys.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MediaId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionId = 1,
                            Title = "😀"
                        },
                        new
                        {
                            Id = 2,
                            QuestionId = 1,
                            Title = "😐"
                        },
                        new
                        {
                            Id = 3,
                            QuestionId = 1,
                            Title = "😴"
                        },
                        new
                        {
                            Id = 4,
                            QuestionId = 1,
                            Title = "😡"
                        },
                        new
                        {
                            Id = 5,
                            QuestionId = 1,
                            Title = "😭"
                        },
                        new
                        {
                            Id = 6,
                            MediaId = 4,
                            QuestionId = 2,
                            Title = "С коллегами"
                        },
                        new
                        {
                            Id = 7,
                            QuestionId = 2,
                            Title = "Один"
                        },
                        new
                        {
                            Id = 8,
                            QuestionId = 2,
                            Title = "С семьей"
                        },
                        new
                        {
                            Id = 9,
                            QuestionId = 2,
                            Title = "С друзьями"
                        },
                        new
                        {
                            Id = 10,
                            QuestionId = 3,
                            Title = "Очень хорошо"
                        },
                        new
                        {
                            Id = 11,
                            QuestionId = 3,
                            Title = "Хорошо"
                        },
                        new
                        {
                            Id = 12,
                            QuestionId = 3,
                            Title = "Удовлетворительно"
                        },
                        new
                        {
                            Id = 13,
                            QuestionId = 3,
                            Title = "Мне он не нравится"
                        },
                        new
                        {
                            Id = 14,
                            QuestionId = 4,
                            Title = "Ответ 1"
                        },
                        new
                        {
                            Id = 15,
                            QuestionId = 4,
                            Title = "Ответ 2"
                        },
                        new
                        {
                            Id = 16,
                            QuestionId = 4,
                            Title = "Ответ 3"
                        },
                        new
                        {
                            Id = 17,
                            QuestionId = 5,
                            Title = "Ответ 1"
                        },
                        new
                        {
                            Id = 18,
                            QuestionId = 5,
                            Title = "Ответ 2"
                        },
                        new
                        {
                            Id = 19,
                            QuestionId = 5,
                            Title = "Ответ 3"
                        });
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Кадровая служба"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Отдел разработки"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Отдел тестирования"
                        });
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Medias");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            QuestionId = 3,
                            Title = "Ёжик в тумане. Мультфильм HD (1975г.)",
                            Type = (byte)2,
                            Url = "<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/6gS1Bp4LZLc\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>"
                        },
                        new
                        {
                            Id = 2,
                            QuestionId = 3,
                            Title = "Фото ёжика 1",
                            Type = (byte)1,
                            Url = "https://proprikol.ru/wp-content/uploads/2021/12/kartinki-ezhika-na-avu-32.jpg"
                        },
                        new
                        {
                            Id = 3,
                            QuestionId = 3,
                            Title = "Фото ёжика 2",
                            Type = (byte)1,
                            Url = "https://bugaga.ru/uploads/posts/2016-04/1461680876_ezhik-vampir-11.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Title = "С коллегами",
                            Type = (byte)1,
                            Url = "https://www.b17.ru/foto/uploaded/upl_1637170559_396979_p74dn.jpg"
                        });
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(-1);

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = 1,
                            SurveyId = 1,
                            Title = "Как прошел ваш рабочий день?"
                        },
                        new
                        {
                            Id = 2,
                            Number = 2,
                            SurveyId = 1,
                            Title = "С кем вы обычно ходите обедать?"
                        },
                        new
                        {
                            Id = 3,
                            Number = 3,
                            SurveyId = 1,
                            Title = "Как вы относитесь к своему начальнику?"
                        },
                        new
                        {
                            Id = 4,
                            Number = 1,
                            SurveyId = 2,
                            Title = "Как прошел ваш рабочий день?"
                        },
                        new
                        {
                            Id = 5,
                            Number = 2,
                            SurveyId = 2,
                            Title = "Как прошел ваш рабочий день?"
                        });
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.HasIndex("UserId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsQuestionOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("StartTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 3, 9, 12, 25, 27, 886, DateTimeKind.Local).AddTicks(3269));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Surveys");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EndTime = new DateTime(2023, 3, 11, 12, 25, 27, 884, DateTimeKind.Local).AddTicks(8630),
                            IsQuestionOrder = false,
                            StartTime = new DateTime(2023, 3, 9, 12, 25, 27, 884, DateTimeKind.Local).AddTicks(8616),
                            Title = "Тест 1"
                        },
                        new
                        {
                            Id = 2,
                            EndTime = new DateTime(2023, 3, 13, 14, 25, 27, 884, DateTimeKind.Local).AddTicks(8639),
                            IsQuestionOrder = false,
                            StartTime = new DateTime(2023, 3, 11, 14, 25, 27, 884, DateTimeKind.Local).AddTicks(8638),
                            Title = "Тест 2"
                        },
                        new
                        {
                            Id = 3,
                            EndTime = new DateTime(2023, 3, 7, 10, 25, 27, 884, DateTimeKind.Local).AddTicks(8642),
                            IsQuestionOrder = false,
                            StartTime = new DateTime(2023, 3, 5, 10, 25, 27, 884, DateTimeKind.Local).AddTicks(8641),
                            Title = "Тест 0"
                        });
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("HrefAvatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniSurveys.Domain.Modals.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Answer", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.Media", "Media")
                        .WithMany()
                        .HasForeignKey("MediaId");

                    b.HasOne("MiniSurveys.Domain.Modals.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Media");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Media", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.Question", "Question")
                        .WithMany("Media")
                        .HasForeignKey("QuestionId");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Question", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Result", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniSurveys.Domain.Modals.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.User", b =>
                {
                    b.HasOne("MiniSurveys.Domain.Modals.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Media");
                });

            modelBuilder.Entity("MiniSurveys.Domain.Modals.Survey", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
