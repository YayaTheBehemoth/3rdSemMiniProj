namespace shared.Models {
    public class Comment {
        public int CommentId {get; set;}
        public string Content {get; set;}
        public string Author{get; set;}
        public int Likes {get; set;}
        public int Dislikes {get; set;}
        public DateTime Date {get; set;}
        public int Post_Id {get; set;}
    }
}