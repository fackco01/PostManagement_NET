using Assignment3.Model;

namespace Assignment3.ApplicationContext
{
    public class DbInit
    {
        public static void AppPostIntit(PostAppContext ctx)
        {
            var User = new AppUsers[]
            {
                new AppUsers{ UserID = 1 , FullName="Ho Trong Duan", Address= "Da Nang", Email="duan@gmail.com", Password="1234"},
                new AppUsers{ UserID = 2 ,FullName="Nguyen Van A", Address= "Da Nang", Email="NVA@gmail.com", Password="1234"}
            };

            ctx.Users.AddRange(User);
            ctx.SaveChanges();

            var Category = new PostCategories[]
            {
                new PostCategories{CategoryID = 1 ,CategoryName="Tour", CategoryDescription="Touch Grass"},
                new PostCategories{CategoryID = 2 ,CategoryName="Other", CategoryDescription="Other Post"}
            };

            ctx.Categories.AddRange(Category);
            ctx.SaveChanges();


            var Post = new Posts[]
            {
                new Posts{ UserID = 1, CategoryID = 1, CreatedDate= DateTime.Now, UpdatedDate= DateTime.Now, Content = "Hello Everyone", PublishStatus = true, Title= "Example 1" },
                new Posts{ UserID = 1, CategoryID = 1, CreatedDate= DateTime.Now, UpdatedDate= DateTime.Now, Content = "Hello Everyone 2", PublishStatus = true, Title= "Example 2" },
                new Posts{ UserID = 1, CategoryID = 2, CreatedDate= DateTime.Now, UpdatedDate= DateTime.Now, Content = "Hello Everyone 3", PublishStatus = true, Title= "Hello" },
                new Posts{ UserID = 2, CategoryID = 1, CreatedDate= DateTime.Now, UpdatedDate= DateTime.Now, Content = "Hello Everyone 4", PublishStatus = true, Title= "Example 4" },
                new Posts{ UserID = 2, CategoryID = 1, CreatedDate= DateTime.Now, UpdatedDate= DateTime.Now, Content = "Hello Everyone 5", PublishStatus = true, Title= "Post Gameplay" },
            };

            ctx.Posts.AddRange(Post);
            ctx.SaveChanges();
        }
    }
}
