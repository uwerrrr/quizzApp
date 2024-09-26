using quizzApp.Models;

namespace quizzApp.Interfaces;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllQuestionsWithAnswersAsync();
    Task<Question> GetQuestionWithAnswersByIdAsync(int questionId);
    Task<List<Question>> GetQuestionListWithAnswersByIdListAsync(List<int> idList);
    Task<bool> IsQuestionExistsAsync(int questionId);
    Task AddQuestionAsync(Question question);
    Task UpdateQuestionAsync(Question question);
    Task RemoveQuestionAsync(Question question);
}