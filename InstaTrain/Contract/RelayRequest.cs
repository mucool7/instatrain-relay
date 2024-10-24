namespace InstaTrain.Contract
{
    public class RelayRequest
    {
        public object? Data { get; set; }
        public Dictionary<string,string>? Headers { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
    }
}
