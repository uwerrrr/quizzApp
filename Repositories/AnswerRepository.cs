using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Repositories;

public class AnswerRepository: IAnswerRepository
{
    private readonly AppDbContext _context;

    public AnswerRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAnswersAsync(List<Answer> answers)
    {
        _context.Answers.AddRange(answers);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveAnswersByQuestionIdAsync(int questionId)
    {
        _context.Answers
            .RemoveRange(_context.Answers
                .Where(a => a.QuestionId == questionId));
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Answer>> GetAnswersByQuestionIdAsync(int questionId)
    {
        return await _context.Answers
            .Where(a => a.QuestionId == questionId)
            .ToListAsync();
    }
    
}