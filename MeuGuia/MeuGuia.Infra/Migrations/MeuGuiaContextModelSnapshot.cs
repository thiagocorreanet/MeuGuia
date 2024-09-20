﻿// <auto-generated />
using System;
using MeuGuia.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeuGuia.Infra.Migrations
{
    [DbContext(typeof(MeuGuiaContext))]
    partial class MeuGuiaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeuGuia.Domain.Entitie.AuditProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AuditProcessId")
                        .HasColumnOrder(1)
                        .HasComment("Chave primária da auditoria.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AffectedColumns")
                        .HasColumnType("VARCHAR(MAX)")
                        .HasColumnName("AffectedColumns")
                        .HasColumnOrder(6)
                        .HasComment("Colunas que foram afetadas");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME2")
                        .HasColumnName("CreationDate")
                        .HasColumnOrder(8)
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("Data de criação da empresa");

                    b.Property<DateTime>("ModificationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ModificationDate")
                        .HasColumnOrder(9)
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("Data da última atualização da empresa");

                    b.Property<string>("NewValues")
                        .HasColumnType("VARCHAR(MAX)")
                        .HasColumnName("NewValues")
                        .HasColumnOrder(5)
                        .HasComment("Novo valor");

                    b.Property<string>("OldValues")
                        .HasColumnType("VARCHAR(MAX)")
                        .HasColumnName("OldValues")
                        .HasColumnOrder(4)
                        .HasComment("Valor antigo");

                    b.Property<string>("PrimaryKey")
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("PrimaryKey")
                        .HasColumnOrder(7)
                        .HasComment("Primary key da tabela");

                    b.Property<string>("Table")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("Table")
                        .HasColumnOrder(3)
                        .HasComment("Tabela na qual vai ser exibida na auditoria");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("Type")
                        .HasColumnOrder(2)
                        .HasComment("Tipo da auditoria");

                    b.HasKey("Id")
                        .HasName("PK_AUDITSPROCESS");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("IX_AUDITSPROCESS_USERID");

                    b.ToTable("AuditsProcess", (string)null);
                });

            modelBuilder.Entity("MeuGuia.Domain.Entitie.IdentityUserClaimCustom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("MeuGuia.Domain.Entitie.IdentityUserCustom", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MeuGuia.Domain.Entitie.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PermissionId")
                        .HasColumnOrder(1)
                        .HasComment("Chave primária da permissão.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AspNetUserClaimId")
                        .HasColumnType("INT")
                        .HasColumnName("AspNetUserClaimId")
                        .HasColumnOrder(3)
                        .HasComment("Chave primária da tabela AspNetUserClaims");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME2")
                        .HasColumnName("CreationDate")
                        .HasColumnOrder(7)
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("Data de criação do registro");

                    b.Property<DateTime>("ModificationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ModificationDate")
                        .HasColumnOrder(8)
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("Data da última atualização do registro");

                    b.Property<string>("Page")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("Page")
                        .HasColumnOrder(5)
                        .HasComment("Nome da página que o usuário terá acesso.");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("Role")
                        .HasColumnOrder(6)
                        .HasComment("Nome da permissão do usuário.");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("Token")
                        .HasColumnOrder(2)
                        .HasComment("Token da tabela");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)")
                        .HasColumnName("UserId")
                        .HasColumnOrder(4)
                        .HasComment("Chave do usuário.");

                    b.HasKey("Id")
                        .HasName("PK_PERMISSIONS");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("IX_PERMISSION_ID");

                    b.ToTable("Permissions", (string)null);
                });

            modelBuilder.Entity("MeuGuia.Domain.Entitie.Revenue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RevenueId")
                        .HasColumnOrder(1)
                        .HasComment("Chave primária da receita.");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME2")
                        .HasColumnName("CreationDate")
                        .HasColumnOrder(4)
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("Data de criação do registro");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("Description")
                        .HasColumnOrder(2)
                        .HasComment("Descrição da receita.");

                    b.Property<DateTime>("ModificationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("ModificationDate")
                        .HasColumnOrder(5)
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("Data da última atualização do registro");

                    b.Property<decimal>("Value")
                        .HasColumnType("DECIMAL(18, 2)")
                        .HasColumnName("Value")
                        .HasColumnOrder(3)
                        .HasComment("Valor da receita.");

                    b.HasKey("Id")
                        .HasName("PK_REVENUES");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("IX_REVENUE_ID");

                    b.ToTable("Revenues", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MeuGuia.Domain.Entitie.IdentityUserClaimCustom", b =>
                {
                    b.HasOne("MeuGuia.Domain.Entitie.IdentityUserCustom", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MeuGuia.Domain.Entitie.IdentityUserCustom", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeuGuia.Domain.Entitie.IdentityUserCustom", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MeuGuia.Domain.Entitie.IdentityUserCustom", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
