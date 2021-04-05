using BnrCodingExercise.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BnrCodingExercise.Infrastructure
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed data
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Node is awesome", Body = "Node.js is a JavaScript runtime built on Chrome's V8 JavaScript engine.", UserId = 1 },
                new Post { Id = 2, Title = "Spring Boot is cooler", Body = "Spring Boot makes it easy to create stand-alone, production-grade Spring based Applications that you can \"just run\".", UserId = 1 },
                new Post { Id = 3, Title = "Go is faster", Body = "Go is an open source programming language that makes it easy to build simple, reliable, and efficient software.", UserId = 3 },
                new Post { Id = 4, Title = "'What about me?' -Rails", Body = "Ruby on Rails makes it much easier and more fun. It includes everything you need to build fantastic applications, and you can learn it with the support of our large, friendly community.", UserId = 3 },
                new Post { Id = 5, Title = ".NET has the gravy", Body = ".NET enables engineers to develop blazing fast web services with ease, utilizing tools developed by Microsoft!", UserId = 4 }
                );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Ryan Dahl", Expertise = "Node", Email = "node4lyfe@example.com" },
                new User { Id = 2, Name = "Rob Pike", Expertise = "Go", Email = "gofarther@example.com" },
                new User { Id = 3, Name = "DHH", Expertise = "Rails", Email = "magic@example.com" },
                new User { Id = 4, Name = "John Watkins", Expertise = ".NET", Email = "jwats@example.com" }
                );
        }
    }
}
