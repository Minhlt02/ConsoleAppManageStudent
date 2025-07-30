using AntDesign;
using AntDesign.Charts;
using AutoMapper;
using BlazorClient.DTO;
using Microsoft.AspNetCore.Components;
using Shared;

namespace BlazorClient.Components.Pages
{
    public partial class Chart : ComponentBase
    {
        [Inject]
        public IStudentContract StudentContract { get; set; }

        [Inject]
        public IClassroomContract ClassroomService { get; set; }

        [Inject]
        ITeacherContract TeacherService { get; set; }

        [Inject]
        public INotificationService _notice { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }
        IChartComponent chartStudentAge;

        List<StudentChartDTO> dataStudentAge;

        IChartComponent chartStudentCount;

        List<StudentChartDTO> dataStudentCount;
        IChartComponent chartStudentCountOfTeacher;

        List<StudentChartDTO> dataStudentCountOfTeacher;

        List<TeacherDTO> teachers = new List<TeacherDTO>();
        List<ClassroomDTO> classrooms = new List<ClassroomDTO>();
        private int SelectedClassroomID;
        private int SelectedTeacherID;

        bool isFirstRender = true;

        // config charts
        PieConfig configPie;
        ColumnConfig configCount;
        ColumnConfig configCountOfTeacher;
        private async Task OnClassroomChanged(int id)
        {
            SelectedClassroomID = id;
            await LoadStudentCountAsync();
        }

        private async Task OnTeacherChanged(int id)
        {
            SelectedTeacherID = id;
            await LoadStudentCountOfTeacherAsync();
        }
        async Task LoadStudentAgeAsync(int id = 1)
        {
            var reply = await StudentContract.GetStudentAgeChartAsync(new RequestId { id = id });
            dataStudentAge = Mapper.Map<List<StudentChartDTO>>(reply.ChartData);
            if (!isFirstRender)
            {
                await chartStudentAge.ChangeData(dataStudentAge);
            }
        }



        async Task LoadStudentCountAsync()
        {
            var reply = await StudentContract.GetStudentCountAsync(new RequestId { id = SelectedClassroomID });
            dataStudentCount = Mapper.Map<List<StudentChartDTO>>(reply.ChartData);
            if (!isFirstRender)
            {
                await chartStudentCount.ChangeData(dataStudentCount);
            }
        }

        async Task LoadStudentCountOfTeacherAsync()
        {
            var reply = await StudentContract.GetStudentCountOfTeacherAsync(new RequestId { id = SelectedTeacherID });
            dataStudentCountOfTeacher = Mapper.Map<List<StudentChartDTO>>(reply.ChartData);
            if (!isFirstRender)
            {
                await chartStudentCountOfTeacher.ChangeData(dataStudentCountOfTeacher);
            }
        }

        void Config()
        {
            configPie = new PieConfig
            {
                Radius = 0.8,
                AngleField = "count",
                ColorField = "age",
                Label = new PieLabelConfig
                {
                    Visible = true,
                    Type = "spider"
                }
            };

            configCount = new ColumnConfig
            {
                AutoFit = true,
                Padding = new[] { 40, 40, 40, 40 },
                XField = "className",
                YField = "count",
                Meta = new
                {
                    Count = new
                    {
                        Alias = "Số lượng học sinh"
                    }
                },
                Label = new ColumnViewConfigLabel
                {
                    Visible = true,
                    Style = new TextStyle
                    {
                        FontSize = 12,
                        FontWeight = 600,
                        Opacity = 0.6,
                    }
                }
            };

            configCountOfTeacher = new ColumnConfig
            {
                AutoFit = true,
                Padding = new[] { 40, 40, 40, 40 },
                XField = "teacherName",
                YField = "count",
                Meta = new
                {
                    Count = new
                    {
                        Alias = "Số lượng học sinh"
                    }
                },
                Label = new ColumnViewConfigLabel
                {
                    Visible = true,
                    Style = new TextStyle
                    {
                        FontSize = 12,
                        FontWeight = 600,
                        Opacity = 0.6,
                    }
                }
            };

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

        protected override async Task OnInitializedAsync()
        {
            Config();
            await LoadClassroomsAsync();
            await LoadTeachersAsync();
            await LoadStudentAgeAsync();
            await LoadStudentCountAsync();
            await LoadStudentCountOfTeacherAsync();


        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadStudentAgeAsync();
                chartStudentAge?.ChangeData(dataStudentAge);
                await LoadStudentCountAsync();
                chartStudentCount?.ChangeData(dataStudentCount);
                await LoadStudentCountOfTeacherAsync();
                chartStudentCountOfTeacher?.ChangeData(dataStudentCountOfTeacher);
            }
            
            isFirstRender = false;
        }
    }
}
