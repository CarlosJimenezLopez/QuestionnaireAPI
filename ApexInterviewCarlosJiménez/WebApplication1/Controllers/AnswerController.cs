using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Controllers
{
    [ApiController]
    [Route("answers")]
    public class AnswerController: ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public AnswerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllAnswers")]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAllAnswers()
        {
            try
            {
                var answers = await _context.Answers
                    .ToListAsync();

                if (!answers.Any())
                {
                    return NotFound(new { message = "No answers found for this index." });
                }

                return Ok(answers);
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

        [HttpPost("createAnswer")]
        public async Task<ActionResult<Answer>> PostAnswer([FromBody] Answer answer)
        {
            try
            {
                if (answer == null)
                {
                    return BadRequest(new { message = "Invalid answer data." });
                }
                var question = await _context.Question
           .FirstOrDefaultAsync(q => q.Id == answer.AnswerIndex);

                if (question == null)
                {
                    return BadRequest(new { message = "The associated question does not exist." });
                }

                var answerAux = await _context.Answers
                    .FirstOrDefaultAsync(a => a.AnswerIndex == answer.AnswerIndex && a.QuestionnaireIndex == answer.QuestionnaireIndex);

                if (answerAux != null)
                {
                    return BadRequest(new { message = "The answer was already submitted." });
                }

                if (!question.CheckAnswers(answer.answer))
                {
                    return BadRequest(new { message = "The answer is not correctly formulated." });
                }



                _context.Answers.Add(answer);
                await _context.SaveChangesAsync();

                return (answer);
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


        [HttpGet("getAllAnswersToQuestion/{questionId}")]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersToQuestion(int questionId)
        {
            try
            {
                var answers = await _context.Answers
                    .Where(a => a.AnswerIndex == questionId)
                    .ToListAsync();

                if (!answers.Any())
                {
                    return NotFound(new { message = "No answers found for this index." });
                }

                return Ok(answers);
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



        [HttpDelete]
        [Route("deleteAllAnswers")]
        public IActionResult DeleteAllQuestions()
        {
            try
            {
                _context.Answers.RemoveRange(_context.Answers);
                _context.SaveChanges();

                return Ok(new { message = "Answers deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error", details = ex.Message });
            }
        }



    }
}
