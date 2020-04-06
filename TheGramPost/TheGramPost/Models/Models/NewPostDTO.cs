using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TheGramPost.Models.Models
{
    public class NewPostDTO
    {
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
        public string UserUID { get; set; }

        public NewPostDTO(IFormCollection form)
        {
            Image = form.Files[0];
            Description = form["description"];
            try
            {
                DatePosted = DateTime.Parse(form["datePosted"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            UserUID = form["userUID"];

        }
    }
}