using Dto.DachBoard;

namespace Api.Repositories.DachRepository
{
    public interface IServiceVisualizationRepository
    {
        List<RequestData> GetRequestsByTime();
        List<ErrorData> GetErrorsByTime();
    }
}
