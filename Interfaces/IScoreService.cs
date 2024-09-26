using quizzApp.Models;

namespace quizzApp.Interfaces;

public interface IScoreService
{
    Task AddScoreAsync(Score score);
    Task<List<Score>> GetAllScoresSortedByScorePercent(bool ascending);
}