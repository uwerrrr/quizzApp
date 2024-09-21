using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerService _answerService;

    public QuestionService(IQuestionRepository questionRepository, IAnswerService answerService)
    {
        _questionRepository = questionRepository;
        _answerService = answerService;
    }

    // get all quesitons with answers
    public async Task<List<Question>> GetAllQuestionsWithAnswersAsync()
    {
        return await _questionRepository.GetAllQuestionsWithAnswersAsync();
    }
    
    public async Task<Question> GetQuestionWithAnswersByIdAsync(int id)
    {
        return await _questionRepository.GetQuestionWithAnswersByIdAsync(id);
    }

    public async Task AddQuestionWithAnswerAsync(Question question, List<Answer> answers)
    {
        await _questionRepository.AddQuestionAsync(question);
        foreach (var answer in answers)
        {
            answer.QuestionId = question.Id;
        }
        await _answerService.AddAnswersAsync(answers);
    }
    
    public async Task UpdateQuestionWithAnswersAsync(Question question, List<Answer> newAnswers)
    {
        await _answerService.RemoveAnswersByQuestionIdAsync(question.Id);
        await _questionRepository.UpdateQuestionAsync(question);
        await _answerService.AddAnswersAsync(newAnswers);
    }
    
    public async Task RemoveQuestionWithAnswersByIdAsync(int id)
    {
        // Finds an entity with the given primary key values
        var question = await _questionRepository.GetQuestionByIdAsync(id);

        if (question != null)
        {
            // Remove answers using the AnswerService
            await _answerService.RemoveAnswersByQuestionIdAsync(id);

            // Remove the question
            await _questionRepository.RemoveQuestionAsync(question);
        }

    }
    
}