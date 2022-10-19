using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using server.Service;
using DBContext;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using shared.Models;
using client;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddTransient<Forumservice>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// giga shoutout to my teacher for teaching me the boilerplate ways
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//dumb practices for dumb people like me :) dont put ur constring in ur code
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<dbContext>(opt =>
        opt.UseNpgsql("Host=ella.db.elephantsql.com;Database=qgcutkgu;Username=qgcutkgu;Password=DVV-YnGW4UxuLtH-sdDJvFsAa1fxigjF"));
var app = builder.Build();
app.UseCors(AllowSomeStuff);
builder.Services.AddRazorPages();
     
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
    
       
app.UseBlazorFrameworkFiles(); 
app.UseHttpsRedirection();

//GET 
//if working correctly it will show the 50 latest posts 
app.MapGet("api/Frontpage", (Forumservice service) =>
{
    return service.getFrontPage();
});
//will show a given post
app.MapGet("api/Post/{id}", (Forumservice service, int id) =>
{
  return service.getPost(id);
});
//will show the comments for a given post(note the "id" parameter is the post-id attacthed to the comment, and not the comment-id)
app.MapGet("api/comment/{id}", (Forumservice service,int id) =>
{
  return service.getComments(id);
});
//POST
//Post method for comments
app.MapPost("api/comment", (Forumservice service, CommentData comment) =>
{
    service.postComment(comment);
});
//Post method for posts
app.MapPost("/api/post/", (Forumservice service, PostData post) =>
{
    service.postPost(post);
});
//PUT
//like for comments
app.MapPut("/api/Comment/like/{id}", (Forumservice service, int id)=>
{
service.LikeComment(id);
});
//dislike for comments
app.MapPut("/api/Comment/dislike/{id}", (Forumservice service, int id)=>
{
service.DislikeComment(id);
});
//like for posts
app.MapPut("/api/post/like/{id}", (Forumservice service,int id)=>
{
service.LikePost(id);
});
//dislike for post
app.MapPut("/api/post/dislike/{id}", (Forumservice service,int id)=>
{
service.DislikePost(id);
});

app.Run();

