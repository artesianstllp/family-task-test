using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using WebClient.Abstractions;

namespace WebClient.Services
{
    public class DragAndDropService: IDragAndDropService
    {
        public DragAndDropService()
        {

        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        public TaskVm Data { get; set; }

        public void StartDrag(TaskVm taskdata)
        {
            Data = taskdata;
        }
      

    }
}