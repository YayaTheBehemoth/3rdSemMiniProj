namespace shared.Models {
    public class Post {
        public int PostId {get; set;}
        public string Title {get; set;}
        public string Content {get; set;}
         public string Author{get; set;}
        public int Likes {get; set;}
        public int Dislikes {get; set;}
          public DateTime Date {get; set;}

    }
}