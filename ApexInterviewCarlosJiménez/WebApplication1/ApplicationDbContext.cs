using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Question<string>> Question { get; set; }
    public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
    public DbSet<GradeQuestion> GradeQuestions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Questionnaire> Questionnaires { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Question<string>>().HasDiscriminator<string>("QuestionType")
            .HasValue<MultipleChoiceQuestion>("MultipleChoice")
            .HasValue<GradeQuestion>("Grade");
    }
}
