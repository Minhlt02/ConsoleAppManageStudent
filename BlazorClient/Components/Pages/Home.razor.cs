
using AntDesign;
using AutoMapper;
using BlazorClient.DTO;
using Grpc.Core;
using Microsoft.AspNetCore.Components;

using Shared;
using System.Drawing.Printing;
using System.ServiceModel.Channels;
namespace BlazorClient.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public IStudentContract studentContract { get; set; } = null!;

        [Inject]
        public NotificationService Notification { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;

        int pageNumber = 1;
        int pageSize = 10;
        int total;
        private bool isRetry = false;
        private string? SearchKeyword;


        // models
        StudentDTO? student = new StudentDTO();
        SearchStudentDTO? searchStudent = new SearchStudentDTO();
        List<StudentDTO> students = null!;

        bool isCreate = false;
        bool isDetails = false;
        bool visible = false;

        async Task HandlePageIndexChangeAsync(PaginationEventArgs args)
        {
            pageNumber = args.Page;
            await LoadStudentsAsync();
        }

        async Task HandlePageSizeChangeAsync(PaginationEventArgs args)
        {
            pageNumber = 1;
            pageSize = args.PageSize;
            await LoadStudentsAsync();
        }

        void OpenPopup(StudentDTO? students = null, bool isCreate = false, bool isDetails = false)
        {
            this.isCreate = isCreate;
            this.visible = true;
            this.isDetails = isDetails;
            if (!isCreate)
            {
                this.student = students;
            }
        }

        async Task ClosePopupAsync()
        {
            await Task.Run(() =>
            {
                student = new StudentDTO();
                isCreate = false;
                visible = false;
                isDetails = false;
            });
        }

        async Task LoadStudentsAsync()
        {
            var request = Mapper.Map<PaginationRequest>(searchStudent);
            request.PageSize = pageSize;
            request.PageNumber = pageNumber;

            var reply = await studentContract.GetPaginationAsync(request);

            if (reply.listStudents?.Any() == true)
            {
                students = Mapper.Map<List<StudentDTO>>(reply.listStudents);
                total = reply.Count;
                isRetry = false; // reset
            }
            else
            {
                if (pageNumber > 1 && !isRetry)
                {
                    pageNumber--;
                    isRetry = true;
                    await LoadStudentsAsync();
                }
                else
                {
                    students = new();
                    total = 0;
                    isRetry = false;
                    _ = Notification.Open(new NotificationConfig()
                    {
                        Message = "Không có dữ liệu phù hợp",
                        Description = reply.Message ?? "Danh sách sinh viên trống.",
                        NotificationType = NotificationType.Warning
                    });
                    searchStudent = new SearchStudentDTO();
                }
            }
        }

        public async Task DeleteStudentAsync(int id)
        {
            var isDeleted = await studentContract.DeleteStudentAsync(new RequestId { id = id });
            _ = Notification.Open(new NotificationConfig()
            {
                Message = "Success",
                Description = isDeleted.Message ?? "Deleted",
                NotificationType = isDeleted.Success ? NotificationType.Success : NotificationType.Error
            });
            await LoadStudentsAsync();
        }

        public async Task OnSortAsync(MenuItem menuItem)
        {
            switch (menuItem.Key)
            {
                case "sbName":
                    students = students!.OrderBy(s => s.studentName).ToList();
                    break;
                case "sbId":
                    students = students!.OrderBy(s => s.studentCode).ToList();
                    break;
            }
        }

        private async Task SearchStudentAsync()
        {
            int codeInput = int.Parse(SearchKeyword ?? "1");
            if (codeInput != 0)
            {
                try
                {
                    var reply = await studentContract.GetStudentByIdAsync(new RequestId { studentCode = codeInput });

                    if (reply.Student != null)
                    {
                        students = new List<StudentDTO> { Mapper.Map<StudentDTO>(reply.Student) };
                        total = 1;
                    }
                    else
                    {
                        students = new List<StudentDTO>();
                        total = 0;
                        await Notification.Open(new NotificationConfig
                        {
                            Message = "Không tìm thấy sinh viên",
                            Description = reply.Message ?? "Mã sinh viên không hợp lệ",
                            NotificationType = NotificationType.Warning
                        });
                    }
                }
                catch (Exception ex)
                {
                    await Notification.Open(new NotificationConfig
                    {
                        Message = "Lỗi tìm kiếm",
                        Description = ex.Message,
                        NotificationType = NotificationType.Error
                    });
                }
            }
            else
            {
                // Nếu không có mã sinh viên => tải toàn bộ (hoặc theo phân trang hiện tại)
                await LoadStudentsAsync();
            }
        }


        protected override async Task OnInitializedAsync()
        {
            await LoadStudentsAsync();
        }
    }
}
