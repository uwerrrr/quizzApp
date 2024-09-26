using quizzApp.Interfaces;
using quizzApp.Models;
using quizzApp.Repositories;

namespace quizzApp.Services;

public class ScoreService : IScoreService
{
    private readonly IScoreRepository _scoreRepo;

    public ScoreService(IScoreRepository scoreRepo)
    {
        _scoreRepo = scoreRepo;
    }

    public async Task AddScoreAsync(Score score)
    {
        await _scoreRepo.AddScoreAsync(score);
    }

    // get all scores sorted by ScorePercent
    public async Task<List<Score>> GetAllScoresSortedByScorePercent(bool ascending)
    {
        if (ascending)
            return await _scoreRepo.GetAllScoresAscendingByScorePercentAsync();
        else
            return await _scoreRepo.GetAllScoresDescendingByScorePercentAsync();
    }
}