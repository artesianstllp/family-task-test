using Domain.ViewModel;

namespace WebClient.Abstractions
{
    public interface IDragAndDropService
    {
        void StartDrag(TaskVm model);
    }
}
