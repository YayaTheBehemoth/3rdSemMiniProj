using DBContext;
using shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace server.Service {
    public class Forumservice {
        private dbContext db {get;}
        public Forumservice (dbContext db){
            this.db = db;
        }

        public List<Post> getFrontPage(){
            return db.Posts.OrderBy(e => e.Date).Take(50).ToList();
        }

        public Post getPost (int id){
            return db.Posts.FirstOrDefault(p => p.PostId == id);
        }  

       
        public List<Comment> getComments (int id){
            return db.Comments.Where(p => p.Post_Id == id).ToList();
        }  

        public void postPost(PostData post){
            db.Posts.Add(new Post{Title = post.Title, Content = post.Content, Author = post.Author, Likes = 0, Dislikes = 0, Date = DateTime.Now});
            db.SaveChanges();
           
        }


        public void postComment(CommentData postComment){
            db.Comments.Add(new Comment{Content = postComment.Content, Author = postComment.Author, Likes = 0, Dislikes = 0, Date = DateTime.Now, Post_Id = postComment.Post_Id});
            db.SaveChanges();
           
        }

        public void LikePost(int id){
            var postToLike = db.Posts.FirstOrDefault(p => p.PostId == id);
            postToLike.Likes += 1;
            db.SaveChanges();
        }

         public void DislikePost(int id){
            var postToDislike = db.Posts.FirstOrDefault(p => p.PostId == id);
            postToDislike.Dislikes += 1;
            db.SaveChanges();
        }


        public void LikeComment(int id){
            var commentToLike = db.Comments.FirstOrDefault(p => p.CommentId == id);
            commentToLike.Likes += 1;
            db.SaveChanges();
        }

         public void DislikeComment(int id){
            var commentToDislike = db.Comments.FirstOrDefault(p => p.CommentId == id);
            commentToDislike.Dislikes += 1;
            db.SaveChanges();
        }
        
    }
}