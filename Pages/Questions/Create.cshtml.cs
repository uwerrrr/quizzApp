using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Pages.Questions;

public class Create : PageModel
{
    // private readonly AppDbContext _context;
    // public Create(AppDbContext context)
    // {
    //     _context = context;
    // }
    
    // service
    private readonly IQuestionService _questionService;
    public Create(IQuestionService questionService)
    {
        _questionService = questionService;
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

        try
        {
            await _questionService.AddQuestionWithAnswerAsync(Question, Answers);
            return RedirectToPage("./View");
        }

        catch (Exception ex)
        {
            var errorMessage = !string.IsNullOrWhiteSpace(ex.Message)
                ? ex.Message
                : "An error occurred while creating the question.";
            ModelState.AddModelError(string.Empty, errorMessage);


            return Page();
        }
    }
}