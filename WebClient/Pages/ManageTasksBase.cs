using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Abstractions;

namespace WebClient.Pages
{
    // Naming conventions not followed for some existing class members
    public class ManageTasksBase : ComponentBase
    {
        protected List<MemberVm> members = new List<MemberVm>();

        protected List<TaskVm> tasks = new List<TaskVm>();

        protected List<MenuItem> leftMenuItem = new List<MenuItem>();

        public string newTask;

        public bool isLoaded;

        public bool showLister;

        [Inject] public IMemberDataService MemberService { get; set; }

        [Inject] public ITaskDataService TaskDataService { get; set; }

        protected override Task OnInitializedAsync()
        {
            showLister = true;
            isLoaded = true;
            ReloadMenu();
            UpdateTasks();
            return base.OnInitializedAsync();
        }

        public void OnAddTask()
        {
            var task = new TaskVm()
            {
                Id = Guid.NewGuid(),
                IsComplete = false,
                Subject = newTask
            };

            newTask = string.Empty;

            if (MemberService.SelectedMember != null)
            {
                task.AssignedToId = MemberService.SelectedMember.Id;
            }

            TaskDataService.AddTask(task);
            StateHasChanged();
        }

        void UpdateTasks()
        {
            var result = TaskDataService.Tasks;

            tasks = result?.ToList();
        }

        void ReloadMenu()
        {
            foreach (var t in members)
            {
                leftMenuItem.Add(new MenuItem
                {
                    IconColor = t.Avatar,
                    Label = t.FirstName,
                    ReferenceId = t.Id
                });
            }
        }
    }
}