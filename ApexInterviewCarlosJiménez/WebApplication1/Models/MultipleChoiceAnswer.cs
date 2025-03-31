using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionnaireAPI.Models
{
    public class MultipleChoiceAnswer : Answer
    {
        [Key]  // Ensure Id is marked as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
        public int Id { get; set; }

        [Required]  // Ensures IndexNumber cannot be null
        public int IndexNumber { get; set; }

        [Required]  // Ensures SelectedAnswers cannot be null
        public string SelectedAnswers { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
