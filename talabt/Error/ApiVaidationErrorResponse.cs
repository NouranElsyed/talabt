namespace talabt.Error
{
    public class ApiVaidationErrorResponse : ApiErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public ApiVaidationErrorResponse() : base(400 )
        {
        }
    }
}
