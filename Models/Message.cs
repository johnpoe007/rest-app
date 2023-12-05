public class Message{
    public string content {get; set;}
    public Embed[] embeds {get; set;}
    public Attachment[] attachments {get; set;}

    public class Embed{
        public string title {get; set;}
        public string description {get; set;}
        public int color {get; set;}
        public Author author {get; set;}
        public Image image {get; set;}
    }

    public class Author{
        public string name {get; set;}
    }

    public class Image{
        public string url {get; set;}
    }

    public class Attachment{
        public string filename {get; set;}
        public string content {get; set;}
    }

    public Message() { }   

    public Message(string content, Embed[] embeds, Attachment[] attachments){
        this.content = content;
        this.embeds = embeds;
        this.attachments = attachments;
    }
}