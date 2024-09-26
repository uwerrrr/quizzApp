using quizzApp.Models;

namespace quizzApp.Interfaces;

public interface IQuestionService
{
    Task<List<Question>> GetAllQuestionsWithAnswersAsync();
    Task<Question> GetQuestionWithAnswersByIdAsync(int id);
    Task<List<Question>> GetQuestionListWithAnswersByIdListAsync(List<int> idList);
    Task AddQuestionWithAnswerAsync(Question question, List<Answer> answers);
    Task UpdateQuestionWithAnswersAsync(Question question);
    Task RemoveQuestionWithAnswersByIdAsync(int id);
}