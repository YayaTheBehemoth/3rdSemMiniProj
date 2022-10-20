using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using shared.Models;
namespace client.Service;
    public class HttpService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public HttpService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }
    //GET
    public async Task<Post[]> GetPosts()
    {   string url = "api/Frontpage";
        //string url = $"{baseAPI}/api/Frontpage";
        return await http.GetFromJsonAsync<Post[]>(url);
    }

    public async Task<Post> GetPost(int id)
    {
        string url = $"{baseAPI}/api/post/{id}/";
        return await http.GetFromJsonAsync<Post>(url);
    }

     public async Task<Comment[]> GetComments(int id)
    {
        string url = $"{baseAPI}/api/comment/{id}/";
        return await http.GetFromJsonAsync<Comment[]>(url);
    }
    
    //POST
       public async void postPost(PostData post)
    {
        string url = $"{baseAPI}/api/post/";
         await http.PostAsJsonAsync(url, post);
    }

      public async void postComment(CommentData comment)
    {
        string url = $"{baseAPI}/api/comment";
        await http.PostAsJsonAsync(url, comment);
    }
    
    //PUT
         public async void likePost(int id)
    {
        string url = $"{baseAPI}/api/post/like/{id}";
        await http.PutAsJsonAsync(url, "");
    }

         public async void dislikePost(int id)
    {
        string url = $"{baseAPI}/api/post/dislike/{id}";
        await http.PutAsJsonAsync(url, "");
    }

         public async void likeComment(int id)
    {
        string url = $"{baseAPI}/api/Comment/like/{id}";
        await http.PutAsJsonAsync(url, "");
    }
        public async void dislikeComment(int id)
    {
        string url = $"{baseAPI}/api/Comment/dislike/{id}";
       await http.PutAsJsonAsync(url, "");
    }


//some very cool code that deserializes httpresonses into JSON, i assume its for easier bugfixing but since im not sure i just leave this out lmfao    
/*
    public async Task<Comment> CreateComment(string content, int postId, int userId)
    {
        string url = $"{baseAPI}posts/{postId}/comments";
     
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { content, userId });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Comment? newComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        return newComment;
    }


    public async Task<Post> UpvotePost(int id)
    {
        string url = $"{baseAPI}posts/{id}/upvote/";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Post? updatedPost = JsonSerializer.Deserialize<Post>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return updatedPost;
    }
    */

}
