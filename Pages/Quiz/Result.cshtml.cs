using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;

namespace quizzApp.Pages.Quiz;

public class ResultModel : PageModel
{

    private readonly IScoreService _scoreService;

    public ResultModel(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    // auto bind (in GET request) the properties of CurrentScore with the values from the URL
    // e.g. URL = /Result?Id=5&TakerName=John&ScorePercent=85
    [BindProperty(SupportsGet = true)]
    public Models.Score CurrentScore { get; set; }

    public List<Models.Score> AllScores { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (CurrentScore == null || CurrentScore.Id == 0)
        {
            return NotFound();
        }
        
       

        // // Fetch the current score from the database to ensure we have all properties
        // CurrentScore = await _context.Scores.FindAsync(CurrentScore.Id);

        // if (CurrentScore == null)
        // {
        //     return NotFound();
        // }

        
        
        // Fetch all scores from the database order by score desc
        AllScores = await _scoreService.GetAllScoresSortedByScorePercent(ascending: false);

        return Page();
    }
}