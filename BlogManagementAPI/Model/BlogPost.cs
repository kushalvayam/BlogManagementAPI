using System;
namespace BlogManagementAPI.Model
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Created { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
