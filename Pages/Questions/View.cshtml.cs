using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using quizzApp.Data;
using quizzApp.Interfaces;
using quizzApp.Models;

namespace quizzApp.Pages.Questions;

public class ViewModel : PageModel
{
    // service
    private readonly IQuestionService _questionService;
    
    // constructor
    public ViewModel(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    
    public List<Question> Questions { get; set; } = new List<Question>();
    
    public async Task OnGetAsync()
    {
        // Fetch questions and its answers
        Questions = await _questionService.GetAllQuestionsWithAnswersAsync();
    }
    
    // delete on post
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {   
        // remove questions and its answers
        await _questionService.RemoveQuestionWithAnswersByIdAsync(id);
        
        // reload page
        return RedirectToPage();
    }
}