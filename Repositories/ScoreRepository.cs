using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Repositories;

public class ScoreRepository: IScoreRepository
{
    //DbContext
    private readonly AppDbContext _context;

    public ScoreRepository(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    
    // add score
    public async Task AddScoreAsync(Score score)
    {
        _context.Scores.Add(score);
        await _context.SaveChangesAsync();
    }
    
    // Fetch scores sorted by ScorePercent ascending
    public async Task<List<Score>> GetAllScoresAscendingByScorePercentAsync()
    {
        return await _context.Scores
            .OrderBy(s => s.ScorePercent)
            .ToListAsync();
    }

    // Fetch scores sorted by ScorePercent descending
    public async Task<List<Score>> GetAllScoresDescendingByScorePercentAsync()
    {
        return await _context.Scores
            .OrderByDescending(s => s.ScorePercent)
            .ToListAsync();
    }
    
}