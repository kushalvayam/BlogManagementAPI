using BlogManagementAPI.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace BlogManagementAPI.Service
{
    public class BlogService
    {
        private readonly string _filePath = "blogposts.json";

        public List<BlogPost> GetAllPosts()
        {
            if (!File.Exists(_filePath))
            {
                return new List<BlogPost>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<BlogPost>>(json) ?? new List<BlogPost>();
        }

        public BlogPost GetPost(int id)
        {
            var posts = GetAllPosts();
            return posts.FirstOrDefault(p => p.Id == id);
        }

        public void AddPost(BlogPost post)
        {
            var posts = GetAllPosts();
            post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
            post.CreatedDate = DateTime.UtcNow; // Set creation time
            posts.Add(post);
            SavePosts(posts);
        }

        public void UpdatePost(BlogPost post)
        {
            var posts = GetAllPosts();
            var existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost != null)
            {
                existingPost.Created = post.Created;
                existingPost.Text = post.Text;
                SavePosts(posts);
            }
        }

        public void DeletePost(int id)
        {
            var posts = GetAllPosts();
            var postToRemove = posts.FirstOrDefault(p => p.Id == id);
            if (postToRemove != null)
            {
                posts.Remove(postToRemove);
                SavePosts(posts);
            }
        }

        private void SavePosts(List<BlogPost> posts)
        {
            var json = JsonConvert.SerializeObject(posts, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
