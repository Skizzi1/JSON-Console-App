using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using ConsumeJson.Context;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ConsumeJson
{
    class Program
    {
        static void Main(string[] args)
        {

            // The goal here is to read the json link and bulk insert to the database.
             
            IEnumerable<Comment> readComment = ReadComments().Result;

            // If the list is not Null read through the entire list perform bulk insert and use EntityFramework.
            if (readComment != null)
            {
                foreach (var item in readComment)
                {
                    Console.WriteLine(item.postId +"\t\t"+ item.name + "\t\t"+  item.email + "\t\t"  + item.body);

                    CommentContext commentContext = new CommentContext();
                    Comment comment = new Comment();

                    comment.postId = item.postId;
                    comment.name = item.name;
                    comment.email = item.email;
                    comment.body = item.body;
                     
                    commentContext.Comments.Add(comment);
                    commentContext.SaveChanges();

                    Console.WriteLine("Successfully saved to the database!");
                    Console.WriteLine();

                }
            }
            else
            {
                // If the list is null an error message will appear.
                Console.WriteLine("Error: Something went wrong!");
            }


            Console.ReadLine();

        }



        // my list of comments
        public static async Task<IEnumerable<Comment>> ReadComments()
        {

            IEnumerable<Comment> commentList = new List<Comment>();

            using (HttpClient http = new HttpClient())
            {
                 
                http.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await http.GetAsync("comments");
                  
                if (response.IsSuccessStatusCode)
                {
                    var responseTask = response.Content.ReadAsStringAsync().Result;
                    commentList = JsonConvert.DeserializeObject<IEnumerable<Comment>>(responseTask);

                }
                else
                {
                    Console.WriteLine("Something went wrong.");
                }

            }

            return commentList;
        }


    }
}
