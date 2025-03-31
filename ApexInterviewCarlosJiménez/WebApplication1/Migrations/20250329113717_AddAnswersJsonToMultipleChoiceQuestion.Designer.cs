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
    [Migration("20250329113717_AddAnswersJsonToMultipleChoiceQuestion")]
    partial class AddAnswersJsonToMultipleChoiceQuestion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Questionnaire<string>", b =>
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

                    b.ToTable("Questionnaires");

                    b.HasDiscriminator<string>("QuestionType").HasValue("Questionnaire<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebApplication1.Models.GradeQuestion", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Questionnaire<string>");

                    b.Property<int>("MaxGrade")
                        .HasColumnType("int");

                    b.Property<int>("MinGrade")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Grade");
                });

            modelBuilder.Entity("WebApplication1.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("WebApplication1.Models.Questionnaire<string>");

                    b.Property<string>("Answers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("MultipleChoice");
                });
#pragma warning restore 612, 618
        }
    }
}
