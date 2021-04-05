using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BnrCodingExercise.Core.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public int UserId { get; set; }

        //public User User { get; set; }

    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Expertise { get; set; }

        //public IEnumerable<Post> Posts { get; set; }
    }
}
