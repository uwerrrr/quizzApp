using Microsoft.EntityFrameworkCore;
using quizzApp.Models;

namespace quizzApp.Data;

public class AppDbContext : DbContext


/*
    AppDbContext is a class that inherits from DbContext, which is provided by Entity Framework Core. 
    DbContext represents a session with the database and is used to query and save data.
*/
{   // represent tables in database
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Score> Scores { get; set; }

    // constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  { }
   
    // Configure the model creation and seed initial data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the base method to ensure any default behavior from DbContext is applied
        base.OnModelCreating(modelBuilder);

        // Seeding data for the Question entity
        modelBuilder.Entity<Question>().HasData(
            new Question { Id = 1, Text = "What is the capital of France?" },
            new Question { Id = 2, Text = "Which planet is known as the Red Planet?" },
            new Question { Id = 3, Text = "What is the chemical symbol for water?" },
            new Question { Id = 4, Text = "Who wrote 'The Great Gatsby'?" }, 
            new Question { Id = 5, Text = "How many continents are there?" },
            new Question { Id = 6, Text = "What is the largest mammal?" },
            new Question { Id = 7, Text = "What is the smallest prime number?" },
            new Question { Id = 8, Text = "What is the hardest natural substance?" },
            new Question { Id = 9, Text = "What is the speed of light?" },
            new Question { Id = 10, Text = "Who painted the Mona Lisa?" }
        );

        // Seeding data for the Answer entity
        modelBuilder.Entity<Answer>().HasData(
            // Answers for Question 1
            new Answer { Id = 1, Text = "Paris", IsCorrect = true, QuestionId = 1 },
            new Answer { Id = 2, Text = "London", IsCorrect = false, QuestionId = 1 },
            new Answer { Id = 3, Text = "Berlin", IsCorrect = false, QuestionId = 1 },
            new Answer { Id = 4, Text = "Madrid", IsCorrect = false, QuestionId = 1 },

            // Answers for Question 2
            new Answer { Id = 5, Text = "Mars", IsCorrect = true, QuestionId = 2 },
            new Answer { Id = 6, Text = "Jupiter", IsCorrect = false, QuestionId = 2 },
            new Answer { Id = 7, Text = "Venus", IsCorrect = false, QuestionId = 2 },
            new Answer { Id = 8, Text = "Saturn", IsCorrect = false, QuestionId = 2 },

            // Answers for Question 3
            new Answer { Id = 9, Text = "H2O", IsCorrect = true, QuestionId = 3 },
            new Answer { Id = 10, Text = "O2", IsCorrect = false, QuestionId = 3 },
            new Answer { Id = 11, Text = "CO2", IsCorrect = false, QuestionId = 3 },
            new Answer { Id = 12, Text = "N2", IsCorrect = false, QuestionId = 3 },

            // Answers for Question 4
            new Answer { Id = 13, Text = "F. Scott Fitzgerald", IsCorrect = true, QuestionId = 4 },
            new Answer { Id = 14, Text = "Charles Dickens", IsCorrect = false, QuestionId = 4 },
            new Answer { Id = 15, Text = "Mark Twain", IsCorrect = false, QuestionId = 4 },
            new Answer { Id = 16, Text = "J.K. Rowling", IsCorrect = false, QuestionId = 4 },

            // Answers for Question 5
            new Answer { Id = 17, Text = "7", IsCorrect = true, QuestionId = 5 },
            new Answer { Id = 18, Text = "5", IsCorrect = false, QuestionId = 5 },
            new Answer { Id = 19, Text = "6", IsCorrect = false, QuestionId = 5 },
            new Answer { Id = 20, Text = "8", IsCorrect = false, QuestionId = 5 },

            // Answers for Question 6
            new Answer { Id = 21, Text = "Blue Whale", IsCorrect = true, QuestionId = 6 },
            new Answer { Id = 22, Text = "Elephant", IsCorrect = false, QuestionId = 6 },
            new Answer { Id = 23, Text = "Giraffe", IsCorrect = false, QuestionId = 6 },
            new Answer { Id = 24, Text = "Hippopotamus", IsCorrect = false, QuestionId = 6 },

            // Answers for Question 7
            new Answer { Id = 25, Text = "2", IsCorrect = true, QuestionId = 7 },
            new Answer { Id = 26, Text = "1", IsCorrect = false, QuestionId = 7 },
            new Answer { Id = 27, Text = "3", IsCorrect = false, QuestionId = 7 },
            new Answer { Id = 28, Text = "5", IsCorrect = false, QuestionId = 7 },

            // Answers for Question 8
            new Answer { Id = 29, Text = "Diamond", IsCorrect = true, QuestionId = 8 },
            new Answer { Id = 30, Text = "Iron", IsCorrect = false, QuestionId = 8 },
            new Answer { Id = 31, Text = "Gold", IsCorrect = false, QuestionId = 8 },
            new Answer { Id = 32, Text = "Silver", IsCorrect = false, QuestionId = 8 },

            // Answers for Question 9
            new Answer { Id = 33, Text = "299,792,458 m/s", IsCorrect = true, QuestionId = 9 },
            new Answer { Id = 34, Text = "150,000,000 m/s", IsCorrect = false, QuestionId = 9 },
            new Answer { Id = 35, Text = "1,000,000 m/s", IsCorrect = false, QuestionId = 9 },
            new Answer { Id = 36, Text = "3,000,000 m/s", IsCorrect = false, QuestionId = 9 },

            // Answers for Question 10
            new Answer { Id = 37, Text = "Leonardo da Vinci", IsCorrect = true, QuestionId = 10 },
            new Answer { Id = 38, Text = "Vincent van Gogh", IsCorrect = false, QuestionId = 10 },
            new Answer { Id = 39, Text = "Pablo Picasso", IsCorrect = false, QuestionId = 10 },
            new Answer { Id = 40, Text = "Claude Monet", IsCorrect = false, QuestionId = 10 }
        );
        
        // Seeding data for the Score entity (as an example)
        modelBuilder.Entity<Score>().HasData(
            new Score { Id = 1, TakerName = "John Doe", ScorePercent = 85.5F, DateTaken = DateTime.Now },
            new Score { Id = 2, TakerName = "Jane Smith", ScorePercent = 92.0F, DateTaken = DateTime.Now }
        );
    }    
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder.UseSqlite("Data Source=db/quizzAppDb.db"));
    // }
}