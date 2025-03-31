﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250331085424_RenameQuestionnaireToQuestion")]
    partial class RenameQuestionnaireToQuestion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerIndex")
                        .HasColumnType("int");

                    b.Property<int>("QuestionnaireIndex")
                        .HasColumnType("int");

                    b.Property<string>("answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("WebApplication1.Models.Question<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("QuestionType")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Question");

                    b.HasDiscriminator<string>("QuestionType").HasValue("Question<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebApplication1.Models.GradeQuestion", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Question<string>");

                    b.Property<int>("MaxGrade")
                        .HasColumnType("int");

                    b.Property<int>("MinGrade")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Grade");
                });

            modelBuilder.Entity("WebApplication1.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Question<string>");

                    b.Property<string>("Answers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("MultipleChoice");
                });
#pragma warning restore 612, 618
        }
    }
}
