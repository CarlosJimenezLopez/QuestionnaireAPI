using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace QuestionnaireAPI.Models
{
    public class MultipleChoiceQuestion : Question<string>
    {
        public string Answers { get; set; }

        [NotMapped]
        public List<string> AnswerList
        {
            get => string.IsNullOrEmpty(Answers) ? new List<string>() : Answers.Split(' ').ToList();
            set => Answers = string.Join(" ", value);
        }

        public override bool CheckAnswers(string answers)
        {
            return AnswerList.Contains(answers);
        }
    }
}
