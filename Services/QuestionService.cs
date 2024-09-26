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
    
    // get a question with its answers by id
    public async Task<Question> GetQuestionWithAnswersByIdAsync(int id)
    {
        return await _questionRepository.GetQuestionWithAnswersByIdAsync(id);
    }
    
    // get question list with its answers by given id list
    public async Task<List<Question>> GetQuestionListWithAnswersByIdListAsync(List<int> idList)
    {
        return await _questionRepository.GetQuestionListWithAnswersByIdListAsync(idList);
    }

    // add a question with its answers
    public async Task AddQuestionWithAnswerAsync(Question question, List<Answer> answers)
    {
        await _questionRepository.AddQuestionAsync(question);
        foreach (var answer in answers)
        {
            answer.QuestionId = question.Id;
        }
        await _answerService.AddAnswersAsync(answers);
    }
    
    // update a question with its answers
    public async Task UpdateQuestionWithAnswersAsync(Question questionToUpdate)
    {
        var existingQuestion = await GetQuestionWithAnswersByIdAsync(questionToUpdate.Id);
        if (existingQuestion == null)
        {
            throw new Exception($"Question with id {questionToUpdate.Id} does not exist");
        }
        
        // Update the question text
        existingQuestion.Text = questionToUpdate.Text;

        // Update the answers
        for (int i = 0; i < existingQuestion.Answers.Count; i++)
        {
            existingQuestion.Answers[i].Text = questionToUpdate.Answers[i].Text;
            existingQuestion.Answers[i].IsCorrect = questionToUpdate.Answers[i].IsCorrect;
            
        }

        // Save changes via the repository
        await _questionRepository.UpdateQuestionAsync(existingQuestion);
        
    }

    // remove a question with its answers
    public async Task RemoveQuestionWithAnswersByIdAsync(int id)
    {
        // Finds an entity with the given primary key values
        var question = await _questionRepository.GetQuestionWithAnswersByIdAsync(id);

        if (question != null)
        {
            // Remove answers using the AnswerService
            await _answerService.RemoveAnswersByQuestionIdAsync(id);

            // Remove the question
            await _questionRepository.RemoveQuestionAsync(question);
        }

    }
    
}