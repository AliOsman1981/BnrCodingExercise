using BnrCodingExercise.Core.Entities;
using System.Linq;

namespace BnrCodingExercise.Infrastructure
{
    public static class Seeder
    {
        public static void SeedData(AppDataContext appDataContext)
        {
            var shouldSave = false;
            if (!appDataContext.Posts.Any())
            {
                shouldSave = true;
                appDataContext.Posts.AddRange(
                    new Post { Title = "Node is awesome", Body = "Node.js is a JavaScript runtime built on Chrome's V8 JavaScript engine.", UserId = 1 },
                    new Post { Title = "Spring Boot is cooler", Body = "Spring Boot makes it easy to create stand-alone, production-grade Spring based Applications that you can \"just run\".", UserId = 1 },
                    new Post { Title = "Go is faster", Body = "Go is an open source programming language that makes it easy to build simple, reliable, and efficient software.", UserId = 3 },
                    new Post { Title = "'What about me?' -Rails", Body = "Ruby on Rails makes it much easier and more fun. It includes everything you need to build fantastic applications, and you can learn it with the support of our large, friendly community.", UserId = 3 },
                    new Post { Title = ".NET has the gravy", Body = ".NET enables engineers to develop blazing fast web services with ease, utilizing tools developed by Microsoft!", UserId = 4 });
            }

            if (!appDataContext.Users.Any())
            {
                shouldSave = true;
                appDataContext.Users.AddRange(
                    new User { Name = "Ryan Dahl", Expertise = "Node", Email = "node4lyfe@example.com" },
                    new User { Name = "Rob Pike", Expertise = "Go", Email = "gofarther@example.com" },
                    new User { Name = "DHH", Expertise = "Rails", Email = "magic@example.com" },
                    new User { Name = "John Watkins", Expertise = ".NET", Email = "jwats@example.com" }
                );
            }

            if (shouldSave)
                appDataContext.SaveChanges();
        }
    }
}
