	using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Core.Extensions.ModelConversion;
    using Domain.Commands;
    using Domain.Queries;
    using Domain.ViewModel;
    using Microsoft.AspNetCore.Components;
    using WebClient.Abstractions;

    namespace WebClient.Services
{
    public class TaskDataService : ITaskDataService
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<TaskVm> _tasks;

		public IEnumerable<TaskVm> Tasks => _tasks;
        public TaskVm SelectedTask { get; private set; }
        TaskVm ITaskDataService.SelectedTask => SelectedTask;

        public event EventHandler TasksUpdated;
        public event EventHandler<string> UpdateTaskFailed;
        public event EventHandler<string> CreateTaskFailed;


        public TaskDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            _tasks = new List<TaskVm>();
             LoadTasks();
        }

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await _httpClient.PostJsonAsync<CreateTaskCommandResult>("tasks", command);
        }

        private async Task<GetAllTaskQueryResult> GetAllTasks()
        {
            return await _httpClient.GetJsonAsync<GetAllTaskQueryResult>("Tasks/Get-All-Tasks-With-MemberAvatar");
        }

        private async Task<UpdateTaskCommandResult> Update(UpdateTaskCommand command)
        {
            return await _httpClient.PutJsonAsync<UpdateTaskCommandResult>($"tasks/{command.Id}", command);
        }

        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        private async void LoadTasks()
        {
            _tasks = (await GetAllTasks()).Payload;
            TasksUpdated?.Invoke(this, null);
        }

        public async Task ToggleTaskAsync(Guid id)
        {
            foreach (var taskModel in Tasks)
            {
                if (taskModel.Id != id) continue;
                taskModel.IsComplete = !taskModel.IsComplete;
                await UpdateTask(taskModel);
            }
            TasksUpdated?.Invoke(this, null);
        }

        public async void AddTask(TaskVm model)
        {
            var result = await Create(model.ToCreateTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    _tasks = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                CreateTaskFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of members from the server.");
            }

            CreateTaskFailed?.Invoke(this, "Unable to create record.");
        }

        public async Task UpdateTask(TaskVm model)
        {
            var result = await Update(model.ToUpdateTaskCommand());

            Console.WriteLine(JsonSerializer.Serialize(result));

            if (result != null)
            {
                var updatedTaskList = (await GetAllTasks()).Payload;

                if (updatedTaskList != null)
                {
                    _tasks = updatedTaskList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The save was successful, but we can no longer get an updated list of tasks from the server.");
            }
            UpdateTaskFailed?.Invoke(this, "Unable to save changes.");
        }
    }
}