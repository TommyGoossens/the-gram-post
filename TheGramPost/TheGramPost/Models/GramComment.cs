using System;

namespace TheGramPost.Models
{
    public class GramComment
    {
        public long Id { get; set; }
        public long PlacedByUserId { get; set; }
        public string Comment { get; set; }
        public long IsReplyOnMessage { get; set; }
        public DateTime Date { get; set; }
    }
}