﻿using EGameCafe.SPA.Enums;
using EGameCafe.SPA.Models;
using EGameCafe.SPA.Services;
using EGameCafe.SPA.Services.ResponseServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EGameCafe.SPA.ViewModels
{
    public class ChatVm : IChatVm , INotifyPropertyChanged
    {
        private readonly IResponseService _responseService;
        private readonly IRepository _repository;

        public ChatVm(IResponseService responseService, IRepository repository)
        {
            _responseService = responseService;
            _repository = repository;

            notification = new NotificationModel();
            item = new GetAllGroups();
        }


        private NotificationModel notification;
        public NotificationModel Notification
        {
            get => notification;
            set
            {
                notification = value;
                OnPropertyChanged();
            }
        }
        public string PageUri { set; get; } = "/groupchat";

        public GetAllGroups item;
        public GetAllGroups Item 
        {
            get => item;
            set
            {
                item = value;
                OnPropertyChanged();
            }
        }

        public GetAllGroupsDto currentGroup;
        public GetAllGroupsDto CurrentGroup 
        {
            get => currentGroup;
            set
            {
                currentGroup = value;
                OnPropertyChanged();
            }
        }

        public ChatViewType ViewType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task HandleGetGroups()
        {
            var groupsResult = await _repository.AuthorizeGetAsync<GetAllGroups>("api/v1/Group/GetAllGroups/0/100/groupname");

            var notifResult = await _responseService.ResponseResultChecker(groupsResult.Result, PageUri, "عملیات با موفقیت انجام شد");

            if (groupsResult.Result.Status != 200)
            {
                Notification = notifResult;
            }
            else
            {
                Item = groupsResult.ResultVm;
            }

            OnPropertyChanged(nameof(Item));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
