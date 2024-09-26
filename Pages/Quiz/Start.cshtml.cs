using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;
using quizzApp.Models;
using quizzApp.Services;

namespace quizzApp.Pages.Quiz
{
    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        
    }
    

    public class StartModel : PageModel
    {
        // services
        private readonly IQuestionService _questionService;
        private readonly IScoreService _scoreService;

        // constructor
        public StartModel(IQuestionService questionService, IScoreService scoreService)
        {
            _questionService = questionService;
            _scoreService = scoreService;
        }

    
        [BindProperty]
        public List<Question> Questions { get; set; }

        [BindProperty] 
        public List<UserAnswer> UserAnswerList { get; set; } = new List<UserAnswer>();
        
        [BindProperty]
        public Models.Score CurrentScore { get; set; }
        
           
        public int NumberOfQuestions { get; set; }
        public int TimeLimitInSeconds { get; set; } // Time limit in seconds
        
        public async Task OnGetAsync()
        {
            // Fetch all questions with their answers
            var allQuestions = await _questionService.GetAllQuestionsWithAnswersAsync();

            // Shuffle questions
            var allQuestionsArr = allQuestions.ToArray();
            Random.Shared.Shuffle(allQuestionsArr);
            Questions = allQuestionsArr.ToList();

            // Shuffle answers for each question
            foreach (var question in Questions)
            {
                var answersArr = question.Answers.ToArray();
                Random.Shared.Shuffle(answersArr);
                question.Answers = answersArr.ToList();
            }
            
            // time limit = (NumberOfQuestions + 2) minutes
            NumberOfQuestions = Questions.Count;
            TimeLimitInSeconds = (NumberOfQuestions + 2) * 60;
            
            // Initialize UserAnswerList
            UserAnswerList = Questions.Select(q => new UserAnswer { QuestionId = q.Id }).ToList();
            
            // Set default value for the user's name
            if (string.IsNullOrEmpty(CurrentScore?.TakerName))
            {
                CurrentScore = new Models.Score { TakerName = "Anonymous" };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Console.WriteLine("UserAnswerList Count: " + UserAnswerList.Count);
            foreach (var answer in UserAnswerList)
            {
                Console.WriteLine($"Question ID: {answer.QuestionId}, Answer ID: {answer.AnswerId}");
            }
            
            Console.WriteLine("Questions.Count: " + Questions.Count);
            
            CurrentScore.ScorePercent = await CalculateScore();

            Console.WriteLine($"Score: {CurrentScore.ScorePercent}%");
            
            // save current UTC time to CurrentScore.DateTaken
            CurrentScore.DateTaken = DateTime.UtcNow;
            

            // Save user's score to the database
            await _scoreService.AddScoreAsync(CurrentScore);
            Console.WriteLine("CurrentScore.Id: " + CurrentScore.Id);
            
            
            
            // Redirect to the results page with the user's TakerName and ScorePercent
            return RedirectToPage("./Result", CurrentScore);
        }

        
        // calculate score function
        private async Task<float> CalculateScore()
        {
            int correctAnswersCount = 0;
            
            // Get the list of question IDs from UserAnswerList
            var answeredQuestionIds = UserAnswerList.Select(ua => ua.QuestionId).ToList();

            // Fetch only the questions that are in UserAnswerList
            var dbQuestions = await _questionService.GetQuestionListWithAnswersByIdListAsync(answeredQuestionIds);
            

            foreach (var userAnswer in UserAnswerList)
            {
                // get answered question
                var dbQuestion = dbQuestions.FirstOrDefault(q => q.Id == userAnswer.QuestionId);
                if (dbQuestion != null)
                {
                    // get correct answer of answered question & compare
                    var correctAnswer = dbQuestion.Answers.FirstOrDefault(a => a.IsCorrect);
                    if (correctAnswer != null && userAnswer.AnswerId == correctAnswer.Id)
                    {
                        correctAnswersCount++;
                    }
                }
            }

            return (float)Math.Round((correctAnswersCount / (float)dbQuestions.Count) * 100, 1);
        }
    }
}
