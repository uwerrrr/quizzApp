using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Pages.Questions;

public class Edit : PageModel
{
    // db context
    private readonly AppDbContext _context;

    // service
    private readonly IQuestionService _questionService;
    
    // constructor
    public Edit(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [BindProperty] 
    public Question Question { get; set; }


    // load the question and its answers for editing
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        
        // search existing question and related answers in db
        Question = await _questionService.GetQuestionWithAnswersByIdAsync(id.Value);
        

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
        
        
        try
        {
            // Update using the service
            await _questionService.UpdateQuestionWithAnswersAsync(Question);
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., question not found)
            var errorMessage = !string.IsNullOrWhiteSpace(ex.Message) ? ex.Message : "An error occurred while updating the question.";
            ModelState.AddModelError(string.Empty, errorMessage);
            return Page();
        }
        
        return RedirectToPage("./View");

    }
}