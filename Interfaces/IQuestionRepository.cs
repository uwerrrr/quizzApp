using quizzApp.Models;

namespace quizzApp.Interfaces;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllQuestionsWithAnswersAsync();
    Task<Question> GetQuestionWithAnswersByIdAsync(int questionId);
    Task<Question> GetQuestionByIdAsync(int questionId);
    Task<bool> IsQuestionExistsAsync(int questionId);
    Task AddQuestionAsync(Question question);
    Task UpdateQuestionAsync(Question question);
    Task RemoveQuestionAsync(Question question);
}