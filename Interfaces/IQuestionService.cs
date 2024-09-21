using quizzApp.Models;

namespace quizzApp.Interfaces;

public interface IQuestionService
{
    Task<List<Question>> GetAllQuestionsWithAnswersAsync();
    Task<Question> GetQuestionWithAnswersByIdAsync(int id);
    Task AddQuestionWithAnswerAsync(Question question, List<Answer> answers);
    Task UpdateQuestionWithAnswersAsync(Question question, List<Answer> newAnswers);
    Task RemoveQuestionWithAnswersByIdAsync(int id);
}