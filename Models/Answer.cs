namespace quizzApp.Models;

public class Answer
{
    public int Id { get; set; }
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int QuestionId { get; set; } // Foreign key property
    public Question Question { get; set; } // Reference navigation to principal
}