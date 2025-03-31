using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionController: ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getAllQuestions")]
        public IActionResult GetQuestions()
        {
            try
            {
                var multipleChoiceQuestions = _context.MultipleChoiceQuestions.ToList()
                    .Select(mcq => new
                    {
                        Id = mcq.Id,
                        Text = mcq.Text,
                        Answers = mcq.AnswerList
                    });

                var gradeQuestions = _context.GradeQuestions.ToList()
                    .Select(gq => new
                    {
                        Id = gq.Id,
                        Text = gq.Text,
                        MinGrade = gq.MinGrade,
                        MaxGrade = gq.MaxGrade
                    });

                var allQuestions = multipleChoiceQuestions.Cast<object>().Concat(gradeQuestions.Cast<object>()).ToList();

                return Ok(allQuestions);
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

        [HttpGet]
        [Route("getAllMultipleChoiceQuestions")]
        public IActionResult GetAllMultipleChoiceQuestions()
        {
            try
            {
                var questions = _context.MultipleChoiceQuestions.ToList();
                return Ok(questions);
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

        [HttpPost]
        [Route("createMultipleChoiceQuestion")]
        public IActionResult CreateMultipleChoiceQuestion([FromBody] MultipleChoiceQuestion question)
        {
            try
            {
                _context.MultipleChoiceQuestions.Add(question);
                _context.SaveChanges();

                return Ok(question);
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

        [HttpGet]
        [Route("getAllGradeQuestions")]
        public IActionResult GetAlGradeQuestions()
        {
            var questions = _context.GradeQuestions.ToList();
            return Ok(questions);
        }

        [HttpPost]
        [Route("createGradeQuestion")]
        public IActionResult CreateMultipleChoiceQuestion([FromBody] GradeQuestion question)
        {
            try
            {
                _context.GradeQuestions.Add(question);
                _context.SaveChanges();

                return Ok(question);
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
        [Route("deleteAllQuestions")]
        public IActionResult DeleteAllQuestions()
        {
            try
            {
                _context.MultipleChoiceQuestions.RemoveRange(_context.MultipleChoiceQuestions);
                _context.GradeQuestions.RemoveRange(_context.GradeQuestions);
                _context.SaveChanges();

                return Ok(new { message = "All answers were removed successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error", details = ex.Message });
            }
        }


    }

}

