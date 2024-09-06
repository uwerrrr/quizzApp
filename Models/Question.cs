using System.ComponentModel.DataAnnotations;

namespace quizzApp.Models;

public class Question
{

    public int Id { get; set; }
    
    public string Text { get; set; }
    
    public ICollection<Answer>? Answers { get; set; } // Collection navigation containing dependents
}