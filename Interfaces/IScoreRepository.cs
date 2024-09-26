using quizzApp.Models;

namespace quizzApp.Interfaces;

public interface IScoreRepository
{
    Task AddScoreAsync(Score score);
    Task<List<Score>> GetAllScoresAscendingByScorePercentAsync();
    Task<List<Score>> GetAllScoresDescendingByScorePercentAsync();
}