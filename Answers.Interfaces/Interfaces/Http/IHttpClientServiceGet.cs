using System.Threading.Tasks;

namespace Answers.Services.Core
{
    public interface IHttpClientServiceGet
    {
        Task<string> GetRequest(string Url);
    }
}