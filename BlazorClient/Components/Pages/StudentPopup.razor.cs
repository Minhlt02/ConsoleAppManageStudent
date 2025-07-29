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
        IStudentContract StudentService { get; set; }

        [Inject]
        IClassroomContract ClassroomService { get; set; }

        [Inject]
        IMapper Mapper { get; set; }

        [Inject]
        INotificationService _notice { get; set; }

        List<ClassroomDTO> classrooms = new List<ClassroomDTO>();


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
                if (reply.Success)
                {
                    _ = _notice.Open(new NotificationConfig()
                    {
                        Message = "Thêm thành công",
                        Description = reply.Message,
                        NotificationType = NotificationType.Success
                    });
                }
                else
                {
                    _ = _notice.Open(new NotificationConfig()
                    {
                        Message = "Thêm thất bại",
                        Description = reply.Message,
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                reply = await StudentService.UpdateStudentAsync(student);
                if (reply.Success)
                {
                    _ = _notice.Open(new NotificationConfig()
                    {
                        Message = "Cập nhật thành công",
                        Description = reply.Message,
                        NotificationType = NotificationType.Success
                    });
                }
                else
                {
                    _ = _notice.Open(new NotificationConfig()
                    {
                        Message = "Cập nhật thất bại",
                        Description = reply.Message,
                        NotificationType = NotificationType.Error
                    });
                }
            }

            await ReloadStudents.InvokeAsync();
            await ClosePopupAsync();

        }

        async Task LoadClassroomsAsync()
        {
            var reply = await ClassroomService.GetAllClassroomAsync(new Shared.Empty());
            if (reply.ClassroomList == null)
            {
                _ = _notice.Open(new NotificationConfig()
                {
                    Message = "Lấy thông tin thất bại",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                classrooms = Mapper.Map<List<ClassroomDTO>>(reply.ClassroomList);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadClassroomsAsync();
        }
    }
}
