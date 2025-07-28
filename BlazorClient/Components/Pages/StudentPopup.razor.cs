using AntDesign;
using AutoMapper;
using BlazorClient.DTO;
using Microsoft.AspNetCore.Components;
using Shared;

namespace BlazorClient.Components.Pages
{
    public partial class StudentPopup : ComponentBase
    {
        [Parameter]
        public StudentDTO Student { get; set; } = null!;

        [Parameter]
        public bool IsCreate { get; set; }

        [Parameter]
        public bool Visible { get; set; }

        [Parameter]
        public bool IsDetails { get; set; }

        [Parameter]
        public EventCallback ReloadStudents { get; set; }

        [Parameter]
        public EventCallback OnClose { get; set; }

        [Inject]
        IStudentContract StudentService { get; set; } = null!;

        [Inject]
        IMapper Mapper { get; set; } = null!;

        [Inject]
        NotificationService Notification { get; set; } = null!;


        async Task ClosePopupAsync()
        {
            IsDetails = false;
            IsCreate = false;
            Visible = false;
            await OnClose.InvokeAsync();
        }


        async Task CreateOrUpdateAsync()
        {
            var student = Mapper.Map<StudentProfile>(Student);
            OperationReply reply = new OperationReply();
            if (IsCreate)
            {
                reply = await StudentService.AddStudentAsync(student);
            }
            else
            {
                reply = await StudentService.UpdateStudentAsync(student);
            }


            if (reply.Success)
            {
                await Notification.Open(new NotificationConfig()
                {
                    Message = "Success",
                    Description = reply.Message,
                    NotificationType = NotificationType.Success
                });
            }
            else
            {
                await Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            await ReloadStudents.InvokeAsync();
            await ClosePopupAsync();
        }

        Task NotificationMessage(string? message, bool isSuccess)
        {
            _ = Notification.Open(new NotificationConfig()
            {
                Message = "Success",
                Description = message != null ? message : IsCreate ? "Created" : "Updated",
                NotificationType = isSuccess ? NotificationType.Success : NotificationType.Error
            });
            return Task.CompletedTask;
        }

    }
}
