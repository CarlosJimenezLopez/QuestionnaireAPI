using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireAPI.Models
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AnswerIndex { get; set; }
        public int QuestionnaireIndex { get; set; }

        public string answer { get; set; }

    }
}
