namespace Webshop.Tools.APIAccess
{
    public interface ISearchServiceClient<PostType, ResultType>
    {
        public Task<ResultType> Post(string endpoint, PostType Payload);
    }
}
