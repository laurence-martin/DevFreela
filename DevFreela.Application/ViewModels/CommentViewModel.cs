namespace DevFreela.Application.ViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel(string content, string user)
        {
            Content = content;
            User = user;
        }

        public string Content { get; private set; }
        public string User { get; private set; }
    }
}
