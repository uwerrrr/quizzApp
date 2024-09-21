using quizzApp.Models;

namespace quizzApp.Interfaces;

public interface IAnswerRepository
{
    Task<List<Answer>> GetAnswersByQuestionIdAsync(int questionId);
    Task AddAnswersAsync(List<Answer> answers);
    Task RemoveAnswersByQuestionIdAsync(int questionId);
    
}