using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Models;

namespace quizzApp.Pages.Questions;

public class Edit : PageModel
{
    // db context
    private readonly AppDbContext _context;

    // constructor
    public Edit(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty] 
    public Question Question { get; set; }


    // load the question and its answers for editing
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        
        // search existing question and related answers in db
        Question = await _context.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);


        if (Question == null) return NotFound();

        Console.WriteLine(Question.Answers[0]);
        return Page();
    }

    // form submission for question edits
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        // Remove existing answers
        _context.Answers.RemoveRange(_context.Answers.Where(a => a.QuestionId == Question.Id));

        // Upate question and answer db
        _context.Attach(Question).State = EntityState.Modified;
            /*EF Core is tracking an entity (like Question), it also automatically tracks its related entities (like Answers) if they are included in the model
            _context.Attach(Question):
            Tracks the Question entity.
            EntityState.Modified:
            Signale EF Core to update it in the database*/

        try
        {
            // Save the changes to the database
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // If a concurrency exception occurs, check if the question still exists
            if (!_context.Questions.Any(q => q.Id == Question.Id))
            {
                // If the question is no longer found, return a 404 Not Found response
                return NotFound();
            }
            else
            {
                // If the question still exists, re-throw the exception to handle it further
                throw;
            }
        }
        
        return RedirectToPage("./View");

    }
}