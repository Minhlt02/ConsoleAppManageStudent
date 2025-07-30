using AntDesign;
using AutoMapper;
using BlazorClient.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
            await ReloadStudents.InvokeAsync();
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
                    await NotificationMessage("Thêm thành công", NotificationType.Success);
                }
                else
                {
                    await NotificationMessage("Thêm thất bại", NotificationType.Error);
                }
            }
            else
            {
                reply = await StudentService.UpdateStudentAsync(student);
                if (reply.Success)
                {
                    await NotificationMessage("Cập nhật thành công", NotificationType.Success);
                }
                else
                {
                    await NotificationMessage("Cập nhật thất bại", NotificationType.Error);
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
                await NotificationMessage("Lấy thông tin thất bại", NotificationType.Error);
            }
            else
            {
                classrooms = Mapper.Map<List<ClassroomDTO>>(reply.ClassroomList);
            }
        }

        public async Task NotificationMessage(String message, NotificationType type)
        {
            _ = _notice.Open(new NotificationConfig()
            {
                Message = message,
                NotificationType = type,
            });
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadClassroomsAsync();
        }
    }
}
