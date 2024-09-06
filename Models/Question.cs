namespace quizzApp.Models;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    
    public int QuizId { get; set; } // Foreign key property
    public Quiz Quiz { get; set; } // Reference navigation to principal
    
    public ICollection<Answer> Answers { get; set; } // Collection navigation containing dependents
}