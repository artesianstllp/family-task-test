using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using WebClient.Abstractions;

namespace WebClient.Pages
{
    public class MembersBase : ComponentBase
    {
        protected List<MemberVm> members = new List<MemberVm>();
        protected List<MenuItem> leftMenuItem = new List<MenuItem>();

        protected bool showCreator;
        protected bool isLoaded;

        [Inject]
        public IMemberDataService MemberDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UpdateMembers();
            ReloadMenu();

            MemberDataService.MembersChanged += MemberDataService_MembersChanged;
            showCreator = true;
            isLoaded = true;
        }

        private void MemberDataService_MembersChanged(object sender, EventArgs e)
        {
            UpdateMembers();
            ReloadMenu();

            showCreator = true;
            isLoaded = true;

            StateHasChanged();
        }

        void UpdateMembers()
        {
            var result = MemberDataService.Members;

            if (result.Any())
            {
                members = result.ToList();
            }
        }

        void ReloadMenu()
        {
            for (int i = 0; i < members.Count; i++)
            {
                leftMenuItem.Add(new MenuItem
                {
                    IconColor = members[i].Avatar,
                    Label = members[i].FirstName,
                    ReferenceId = members[i].Id
                });
            }
        }

        // Naming conventions not followed for this existing method
        protected void onAddItem()
        {
            showCreator = true;
            StateHasChanged();
        }

        // Naming conventions not followed for this existing method
        protected void onMemberAdd(MemberVm familyMember)
        {
            MemberDataService.CreateMember(familyMember);
        }

    }
}
