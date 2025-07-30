
using AntDesign;
using AntDesign.TableModels;
using AutoMapper;
using BlazorClient.DTO;
using ClosedXML.Excel;
using Grpc.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
namespace BlazorClient.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public IStudentContract studentContract { get; set; } = null!;

        [Inject]
        public INotificationService _notice { get; set; }

        [Inject]
        public IMapper Mapper { get; set; } = null!;
        [Inject]
        IClassroomContract ClassroomService { get; set; }
        [Inject]
        ITeacherContract TeacherService { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public StudentDTO Student { get; set; } = new();

        // models
        SearchStudentDTO? searchStudent = new SearchStudentDTO();
        List<StudentDTO> students = null!;
        IEnumerable<StudentDTO> _selectedRows = [];
        List<ClassroomDTO> classrooms = new List<ClassroomDTO>();
        List<TeacherDTO> teachers = new List<TeacherDTO>();


        int pageNumber = 1;
        int pageSize = 10;
        int total;
        string? sortBy;
        private bool isRetry = false;
        private string? keyword;
        private int? SelectedClassroomID;
        private int? SelectedTeacherID;

        bool isCreate = false;
        bool isDetails = false;
        bool visible = false;
        bool isSort = false;

        async Task HandlePageIndexChangeAsync(PaginationEventArgs args)
        {
            pageNumber = args.Page;
            if (isSort == true)
            {
                await LoadSortStudentsAsync();
            } else
            {
                await LoadStudentsAsync();
            }
            
        }

        async Task HandlePageSizeChangeAsync(PaginationEventArgs args)
        {
            await LoadStudentsAsync();
            return;
                
        }

        void OpenPopup(StudentDTO? students = null, bool isCreate = false, bool isDetails = false)
        {
            this.isCreate = isCreate;
            this.visible = true;
            this.isDetails = isDetails;
            if (!isCreate)
            {
                this.Student = students;
            }
        }

        async Task ClosePopupAsync()
        {
            await Task.Run(() =>
            {
                Student = new StudentDTO();
                isCreate = false;
                visible = false;
                isDetails = false;
            });
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

        async Task LoadTeachersAsync()
        {
            var reply = await TeacherService.GetAllTeacherAsync(new Shared.Empty());
            if (reply.TeacherList == null)
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
                teachers = Mapper.Map<List<TeacherDTO>>(reply.TeacherList);
            }
        }

        async Task ExportExcelAsync()
        {
            var request = Mapper.Map<PaginationRequest>(searchStudent);
            request.PageSize = int.MaxValue;
            request.PageNumber = 1;
            request.keyword = keyword;
            request.classroomId = SelectedClassroomID;
            request.teacherId = SelectedTeacherID;

            var reply = await studentContract.GetPaginationAsync(request);
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Students");

            worksheet.Cell(1, 1).Value = "Mã SV";
            worksheet.Cell(1, 2).Value = "Tên";
            worksheet.Cell(1, 3).Value = "Ngày sinh";
            worksheet.Cell(1, 4).Value = "Địa chỉ";
            worksheet.Cell(1, 5).Value = "Mã lớp học";

            worksheet.Column(1).Width = 10;
            worksheet.Column(2).Width = 30;
            worksheet.Column(3).Width = 15;
            worksheet.Column(4).Width = 50;
            worksheet.Column(5).Width = 10;

            worksheet.Column(3).Style.DateFormat.Format = "dd/MM/yyyy";
            for (int col = 1; col <= 5; col++)
            {
                worksheet.Column(col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Column(col).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }
            int i = 0;
            students = Mapper.Map<List<StudentDTO>>(reply.listStudents);
            foreach (var student in students)
            {
                if (student == null) continue;

                worksheet.Cell(i + 2, 1).Value = student.Id;
                worksheet.Cell(i + 2, 2).Value = student.studentName;
                worksheet.Cell(i + 2, 3).Value = student.studentBirthday;
                worksheet.Cell(i + 2, 4).Value = student.studentAddress;
                worksheet.Cell(i + 2, 5).Value = student.classroomID;
                i++;
            }


            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            var fileBytes = stream.ToArray();
            var base64 = Convert.ToBase64String(fileBytes);
            _ = _notice.Open(new NotificationConfig()
            {
                Message = "Tải thành công",
                NotificationType = NotificationType.Success,
            });
            // Gọi JS để tải file
            await JS.InvokeVoidAsync("downloadFileFromBytes", "DanhSachSinhVien.xlsx", base64);
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
                    await _notice.Open(new NotificationConfig()
                    {
                        Message = "Không có dữ liệu phù hợp",
                        Description = reply.Message ?? "Danh sách sinh viên trống.",
                        NotificationType = NotificationType.Warning
                    });
                    searchStudent = new SearchStudentDTO();
                }
            }
        }



        public async Task DeleteStudentAsync(int? id)
        {
            try
            {
                await studentContract.DeleteStudentAsync(new RequestId { id = id.Value});
                await LoadStudentsAsync();
                _ = _notice.Open(new NotificationConfig()
                {
                    Message = "Xóa thành công",
                    Description = "Đã xóa sinh viên.",
                    Duration = 2,
                    NotificationType = NotificationType.Warning
                });
            }
            catch (Exception ex)
            {
                _ = _notice.Open(new NotificationConfig()
                {
                    Message = "Xóa thất bại",
                    Description = ex.Message,
                    NotificationType = NotificationType.Error
                });
            }
        }

        async Task LoadSortStudentsAsync()
        {
            var request = Mapper.Map<PaginationRequest>(searchStudent);
            request.PageSize = pageSize;
            request.PageNumber = pageNumber;
            request.SortBy = sortBy;

            var reply = await studentContract.GetPaginationSortAsync(request);

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
                    await LoadSortStudentsAsync();
                }
                else
                {
                    students = new();
                    total = 0;
                    isRetry = false;
                    await _notice.Open(new NotificationConfig()
                    {
                        Message = "Không có dữ liệu phù hợp",
                        Description = reply.Message ?? "Danh sách sinh viên trống.",
                        NotificationType = NotificationType.Warning
                    });
                    searchStudent = new SearchStudentDTO();
                }
            }
        }

        public async Task OnSortAsync(MenuItem menuItem)
        {
            switch (menuItem.Key)
            {
                case "sbName":
                    sortBy = "studentName";
                    await LoadSortStudentsAsync();
                    isSort = true;
                    break;
                case "sbId":
                    sortBy = "studentCode";
                    await LoadSortStudentsAsync();
                    isSort = true;
                    break;
                case "sbRemove":
                    await LoadStudentsAsync();
                    isSort = false;
                    break;
            }
            
        }

        public async Task SearchStudentAsync()
        {
            var request = Mapper.Map<PaginationRequest>(searchStudent);
            request.PageSize = pageSize;
            request.PageNumber = pageNumber;
            request.keyword = keyword;
            request.classroomId = SelectedClassroomID;
            request.teacherId = SelectedTeacherID;

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
                    await SearchStudentAsync();
                }
                else
                {
                    students = new();
                    total = 0;
                    isRetry = false;
                    _= _notice.Open(new NotificationConfig()
                    {
                        Message = "Không có dữ liệu phù hợp",
                        Description = reply.Message ?? "Danh sách sinh viên trống.",
                        NotificationType = NotificationType.Warning
                    });
                    searchStudent = new SearchStudentDTO();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadStudentsAsync();
            await LoadClassroomsAsync();
            await LoadTeachersAsync();
        }
    }
}
