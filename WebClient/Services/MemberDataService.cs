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
    public class MemberDataService : IMemberDataService
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<MemberVm> _members;
        public MemberDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            _members = new List<MemberVm>();
            LoadMembers();
        }

        public IEnumerable<MemberVm> Members => _members;
        public MemberVm SelectedMember { get; private set; }

        public event EventHandler MembersChanged;
        public event EventHandler<string> UpdateMemberFailed;
        public event EventHandler<string> CreateMemberFailed;
        public event EventHandler SelectedMemberChanged;

        private async void LoadMembers()
        {
            _members = (await GetAllMembers()).Payload;
            MembersChanged?.Invoke(this, null);
        }

        private async Task<CreateMemberCommandResult> Create(CreateMemberCommand command)
        {
            return await _httpClient.PostJsonAsync<CreateMemberCommandResult>("members", command);
        }

        private async Task<GetAllMembersQueryResult> GetAllMembers()
        {
            return await _httpClient.GetJsonAsync<GetAllMembersQueryResult>("members");
        }

        private async Task<UpdateMemberCommandResult> Update(UpdateMemberCommand command)
        {
            return await _httpClient.PutJsonAsync<UpdateMemberCommandResult>($"members/{command.Id}", command);
        }

        public async Task UpdateMember(MemberVm model)
        {
            var result = await Update(model.ToUpdateMemberCommand());

            Console.WriteLine(JsonSerializer.Serialize(result));

            if(result != null)
            {
                var updatedList = (await GetAllMembers()).Payload;

                if(updatedList != null)
                {
                    _members = updatedList;
                    MembersChanged?.Invoke(this, null);
                    return;
                }
                UpdateMemberFailed?.Invoke(this, "The save was successful, but we can no longer get an updated list of members from the server.");
            }

            UpdateMemberFailed?.Invoke(this, "Unable to save changes.");
        }

        public async Task CreateMember(MemberVm model)
        {
            var result = await Create(model.ToCreateMemberCommand());
            if (result != null)
            {
                var updatedList = (await GetAllMembers()).Payload;

                if (updatedList != null)
                {
                    _members = updatedList;
                    MembersChanged?.Invoke(this, null);
                    return;
                }
                CreateMemberFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of members from the server.");
            }

            CreateMemberFailed?.Invoke(this, "Unable to create record.");
        }

        public void SelectMember(Guid id)
        {
            if (_members.All(memberVm => memberVm.Id != id)) return;
            {
                SelectedMember = _members.SingleOrDefault(memberVm => memberVm.Id == id);
                SelectedMemberChanged?.Invoke(this, null);
            }
        }

        public void SelectNullMember()
        {
            SelectedMember = null;
            SelectedMemberChanged?.Invoke(this, null);
        }
    }
}
