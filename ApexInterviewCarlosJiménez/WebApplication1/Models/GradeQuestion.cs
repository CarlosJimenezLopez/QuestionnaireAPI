namespace QuestionnaireAPI.Models
{
    public class GradeQuestion : Question<string>
    {
        public int MaxGrade { get; set; }
        public int MinGrade { get; set; }

        public override bool CheckAnswers(string grade)
        {
            int answerGrade;
            if (int.TryParse(grade, out answerGrade))
            {
                return answerGrade >= MinGrade && answerGrade <= MaxGrade;
            }
            return false;
        }
    }

}
