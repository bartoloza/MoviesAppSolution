namespace AppModels
{
    public class MoviceCollection
    {
        public string SearchType { get; set; }
        public string Expression { get; set; }
        public List<SearchResult> Results { get; set; }
        public string ErrorMessage { get; set; }
    }
}
