using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Models;

namespace quizzApp.Pages.Quiz;

public class StartModel : PageModel
{
    // db context
    private readonly AppDbContext _context;
    
    // constructor
    public StartModel(AppDbContext context)
    {
        _context = context;
    }
    [BindProperty]
    public List<Question> Questions { get; set; }

    [BindProperty]
    public List<int> UserAnswerIdList { get; set; }

    public int Score { get; set; }

    public async Task OnGetAsync()
    {
        Questions = await _context.Questions
            .Include(q => q.Answers)
            .ToListAsync();

        // UserAnswerIdList = new List<int>(new int[Questions.Count]);
        UserAnswerIdList = new List<int>(Questions.Count);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Questions = await _context.Questions
            .Include(q => q.Answers)
            .ToListAsync();

        Score = CalculateScore();
        
        Console.WriteLine(Score);

        // Here you could save the user's score to the database if needed
        // For example:
        // var userScore = new UserScore { Score = Score, UserId = GetCurrentUserId() };
        // _context.UserScores.Add(userScore);
        // await _context.SaveChangesAsync();

        return Page();
    }

    private int CalculateScore()
    {
        int score = 0;
        for (int i = 0; i < Questions.Count; i++)
        {
            if (i < UserAnswerIdList.Count)
            {
                var correctAnswer = Questions[i].Answers.FirstOrDefault(a => a.IsCorrect);
                if (correctAnswer != null && UserAnswerIdList[i] == correctAnswer.Id)
                {
                    score++;
                }
            }
        }
        return score;
    }
}


