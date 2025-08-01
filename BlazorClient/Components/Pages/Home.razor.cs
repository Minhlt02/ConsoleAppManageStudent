﻿
using AntDesign;
using AutoMapper;
using BlazorClient.DTO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared;
namespace BlazorClient.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] IStudentContract StudentContract { get; set; }

        [Inject] INotificationService Notice { get; set; }

        [Inject] IMapper Mapper { get; set; }
        [Inject] IClassroomContract ClassroomService { get; set; }
        [Inject] ITeacherContract TeacherService { get; set; }

        [Inject] IJSRuntime JS { get; set; }

        [Parameter] public StudentDTO Student { get; set; } = new();

        // models
        SearchStudentDTO? searchStudent = new SearchStudentDTO();

        List<StudentDTO> Students;
        List<ClassroomDTO> Classrooms = new();
        List<TeacherDTO> Teachers = new();

        IEnumerable<StudentDTO> SelectedRows = [];

        int pageNumber = 1;
        int pageSize = 10;
        int total;
        int? keywordId;
        int? SelectedClassroomID;
        int? SelectedTeacherID;

        string? keyword;
        string? sortBy;
        
        bool isCreate = false;
        bool isDetails = false;
        bool visible = false;
        bool isSearch = false;
        bool isRetry = false;

        async Task HandlePageIndexChangeAsync(PaginationEventArgs args)
        {
            pageNumber = args.Page;
            if (isSearch)
            {
                await LoadStudentsAsync();
            } else
            {
                await SearchStudentAsync();
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
            isCreate = false;
            visible = false;
            isDetails = false;
            await LoadStudentsAsync();
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
                Classrooms = Mapper.Map<List<ClassroomDTO>>(reply.ClassroomList);
            }
        }

        async Task LoadTeachersAsync()
        {
            var reply = await TeacherService.GetAllTeacherAsync(new Shared.Empty());
            if (reply.TeacherList == null)
            {
                await NotificationMessage("Lấy thông tin thất bại", NotificationType.Error);
            }
            else
            {
                Teachers = Mapper.Map<List<TeacherDTO>>(reply.TeacherList);
            }
        }
        async Task LoadStudentsAsync()
        {
            var request = Mapper.Map<PaginationRequest>(searchStudent);
            request.PageSize = pageSize;
            request.PageNumber = pageNumber;
            request.SortBy = sortBy;

            var reply = await StudentContract.GetPaginationAsync(request);

            if (reply.listStudents?.Any() == true)
            {
                Students = Mapper.Map<List<StudentDTO>>(reply.listStudents);
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
                    Students = new();
                    total = 0;
                    isRetry = false;
                    await NotificationMessage("Không tìm thấy danh sách", NotificationType.Warning);
                    searchStudent = new SearchStudentDTO();
                }
            }
        }



        public async Task DeleteStudentAsync(int? id)
        {
            try
            {
                await StudentContract.DeleteStudentAsync(new RequestId { id = id.Value});
                await LoadStudentsAsync();
                await NotificationMessage("Xóa thành công", NotificationType.Success);
            }
            catch (Exception ex)
            {
                await NotificationMessage("Xóa thất bại", NotificationType.Error);
            }
        }

        public async Task DeleteManyStudentAsync()
        {
            try
            {
                var ids = SelectedRows.Select(s => s.Id).ToList();


                await StudentContract.DeleteManyStudentAsync(new RequestId { ids = ids.ToList()});
                await LoadStudentsAsync();
                await NotificationMessage("Xóa thành công", NotificationType.Success);
                SelectedRows = Enumerable.Empty<StudentDTO>();
            }
            catch (Exception ex)
            {
                await NotificationMessage("Xóa thất bại", NotificationType.Error);
                Console.WriteLine(ex.Message);
            }
        }


        public async Task OnSortAsync(MenuItem menuItem)
        {
            switch (menuItem.Key)
            {
                case "sbName":
                    sortBy = "studentName";
                    await LoadStudentsAsync();
                    break;
                case "sbId":
                    sortBy = "id";
                    await LoadStudentsAsync();
                    break;
                case "sbRemove":
                    sortBy = "";
                    await LoadStudentsAsync();
                    break;
                case "":
                    await LoadStudentsAsync();
                    break;
            }
            
        }

        public async Task SearchStudentAsync()
        {
            searchStudent.PageNumber = pageNumber;
            var request = Mapper.Map<PaginationRequest>(searchStudent);
            request.PageSize = pageSize;
            request.PageNumber = pageNumber;
            request.keywordId = keywordId;
            request.keyword = keyword;
            request.classroomId = SelectedClassroomID;
            request.teacherId = SelectedTeacherID;
            isRetry = true;

            var reply = await StudentContract.GetPaginationAsync(request);

            if (reply.listStudents?.Any() == true)
            {
                Students = Mapper.Map<List<StudentDTO>>(reply.listStudents);
                total = reply.Count;
                isRetry = false; // reset
            }
            else
            {
                if (pageNumber > 1 && isRetry)
                {
                    pageNumber--;
                    isRetry = true;
                    searchStudent.PageNumber = pageNumber;
                    await SearchStudentAsync();
                }
                else
                {
                    Students = new();
                    total = 0;
                    isRetry = false;
                    await NotificationMessage("Không tìm thấy danh sách", NotificationType.Warning);
                    searchStudent = new SearchStudentDTO();
                }
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

            var reply = await StudentContract.GetPaginationAsync(request);
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
            Students = Mapper.Map<List<StudentDTO>>(reply.listStudents);
            foreach (var student in Students)
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
            await NotificationMessage("Tải thành công", NotificationType.Success);
            // Gọi JS để tải file
            await JS.InvokeVoidAsync("downloadFileFromBytes", "DanhSachSinhVien.xlsx", base64);
        }

        public async Task NotificationMessage(String message, NotificationType type)
        {
            _ = Notice.Open(new NotificationConfig()
            {
                Message = message,
                NotificationType = type,
            });
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadStudentsAsync();
            await LoadClassroomsAsync();
            await LoadTeachersAsync();
        }
    }
}
