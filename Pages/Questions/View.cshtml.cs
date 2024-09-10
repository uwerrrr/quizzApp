using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Models;

namespace quizzApp.Pages.Questions;

public class ViewModel : PageModel
{
    // db context
    private readonly AppDbContext _context;
    
    // constructor
    public ViewModel(AppDbContext context)
    {
        _context = context;
    }
    
    public List<Question> Questions { get; set; } = new List<Question>();
    
    public async Task OnGetAsync()
    {
        // Fetch questions and include answers
        Questions = await _context.Questions
            .Include(q => q.Answers)  // Eager load answers for each question
            .ToListAsync();
    }
    
    // delete on post
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        // Finds an entity with the given primary key values
        var question = await _context.Questions.FindAsync(id);
        
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
        
        return RedirectToPage();
    }
}