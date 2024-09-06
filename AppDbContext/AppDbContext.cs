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
    // public DbSet<Quiz> Quizzes { get; set; }

    // constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  { }
   
    // Configure the model creation and seed initial data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the base method to ensure any default behavior from DbContext is applied
        base.OnModelCreating(modelBuilder);
        
        // // Seed data for the Quiz entity
        // modelBuilder.Entity<Quiz>().HasData(
        //     new Quiz
        //     {
        //         Id = 1,                     
        //         Title = "Geography Quiz"     
        //     }
        // );

        // Seed data for the Question entity
        modelBuilder.Entity<Question>().HasData(
            new Question
            {
                Id = 1,                     // Primary key for the question
                Text = "What is the capital of France?", // Question text
                // QuizId = 1 // Foreign key to reference the Quiz
            }
        );

        // Seed data for the Answer entity
        modelBuilder.Entity<Answer>().HasData(
            new Answer
            {
                Id = 1,                     
                Text = "Paris",             
                IsCorrect = true,           
                QuestionId = 1    // Foreign key referencing the Question entity
            },
            new Answer
            {
                Id = 2,
                Text = "London",
                IsCorrect = false,
                QuestionId = 1
            },
            new Answer
            {
                Id = 3,
                Text = "Rome",
                IsCorrect = false,
                QuestionId = 1
            }
        );
    }    
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder.UseSqlite("Data Source=db/quizzAppDb.db"));
    // }
}