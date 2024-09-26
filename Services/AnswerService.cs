using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Services;

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;

    public AnswerService(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task AddAnswersAsync(List<Answer> answers)
    {
        await _answerRepository.AddAnswersAsync(answers);
    }

    public async Task RemoveAnswersByQuestionIdAsync(int questionId)
    {
        await _answerRepository.RemoveAnswersByQuestionIdAsync(questionId);
    }

    public async Task<List<Answer>> GetAnswersByQuestionIdAsync(int questionId)
    {
        return await _answerRepository.GetAnswersByQuestionIdAsync(questionId);
    }
    
}