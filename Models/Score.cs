using System.ComponentModel.DataAnnotations;

namespace quizzApp.Models;

public class Score
{
    [Key]
    public int Id { get; set; }
    public string TakerName { get; set; }
    public float ScorePercent { get; set; }
    public DateTime DateTaken { get; set; }
}