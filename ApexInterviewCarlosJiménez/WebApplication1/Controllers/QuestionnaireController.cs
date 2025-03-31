using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Controllers
{
    [ApiController]
    [Route("questionnaire")]
    public class QuestionnaireController: ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public QuestionnaireController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{questionnaireId}/answers")]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAnswers(int questionnaireId)
        {
            try
            {
                // Obtener todas las respuestas del cuestionario dado
                var answers = await _context.Answers
                    .Where(a => a.QuestionnaireIndex == questionnaireId)
                    .ToListAsync();

                if (answers == null || !answers.Any())
                {
                    return NotFound(new { message = "No answers found for this questionnaire." });
                }

                var output = new List<QuestionnaireOutput>();

                foreach (var answer in answers)
                {
                    var question = await _context.Question
           .FirstOrDefaultAsync(q => q.Id == answer.AnswerIndex);


                    if (!answers.Any())
                    {
                        return NotFound(new { message = "Bad request" });
                    }

                    else
                    {
                        output.Add(new QuestionnaireOutput
                        {

                            QuestionText = question.Text,
                            AnswerText = answer.answer
                        }
                        );
                    }
                }

                return Ok(output);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new { message = "Database error occurred", details = dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }

    public class QuestionnaireOutput()
    {
        public string QuestionText { get; set; }

        public string AnswerText { get; set; }
    }

    public class AnswerDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
    }





}

