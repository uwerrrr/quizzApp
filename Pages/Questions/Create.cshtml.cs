using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Models;

namespace quizzApp.Pages.Questions;

public class Create : PageModel
{
    private readonly AppDbContext _context;
    public Create(AppDbContext context)
    {
        _context = context;
    }
    

    [BindProperty] 
    public Question Question { get; set; }

    [BindProperty] 
    public List<Answer> Answers { get; set; }
    
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        // First, add the new Question
        _context.Questions.Add(Question);
        await _context.SaveChangesAsync(); 

        // Then, link the new Answers to the newly created Question
        foreach (var answer in Answers)
        {
            answer.QuestionId = Question.Id;
            _context.Answers.Add(answer);
        }
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");  // Redirect after successful save
    }
}