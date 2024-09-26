using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Repositories;
public class QuestionRepository: IQuestionRepository
// public class QuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetAllQuestionsWithAnswersAsync()
    {
        return await _context.Questions
            .Include(q => q.Answers) // Eager load answers for each question
            .ToListAsync();
    }
    
    public async Task<Question> GetQuestionWithAnswersByIdAsync(int questionId)
    {
        return await _context.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == questionId);
    }
    
    // get question list with its answers by given id list
    public async Task<List<Question>> GetQuestionListWithAnswersByIdListAsync(List<int> idList)
    {
        return await _context.Questions
            .Where(q => idList.Contains(q.Id))
            .Include(q => q.Answers)
            .ToListAsync();
    }
    
    
    public async Task<bool> IsQuestionExistsAsync(int questionId)
    {
        return await _context.Questions.AnyAsync(q => q.Id == questionId);
    }
    
    public async Task AddQuestionAsync(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateQuestionAsync(Question question)
    {
        // _context.Attach(question).State = EntityState.Modified;
        _context.Update(question);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveQuestionAsync(Question question)
    {
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
    }
    
}
