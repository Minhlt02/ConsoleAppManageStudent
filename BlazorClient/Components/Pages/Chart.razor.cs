using AntDesign;
using AntDesign.Charts;
using AutoMapper;
using BlazorClient.DTO;
using Microsoft.AspNetCore.Components;
using Shared;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorClient.Components.Pages
{
    public partial class Chart : ComponentBase
    {
        [Inject]
        public IStudentContract StudentContract { get; set; } = null!;

        [Inject]
        public NotificationService Notification { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;
        IChartComponent chartStudentAge = null!;

        List<StudentAgeDTO> dataStudentAge = null!;

        bool isFirstRender = true;

        // config charts
        ColumnConfig config1 = null!;
        async Task LoadStudentAgeAsync(int id = 1)
        {
            var reply = await StudentContract.GetStudentAgeChartAsync(new RequestId { id = id });
            dataStudentAge = Mapper.Map<List<StudentAgeDTO>>(reply.ChartData);
            if (!isFirstRender)
            {
                await chartStudentAge.ChangeData(dataStudentAge);
            }
        }

        void Config()
        {
            config1 = new ColumnConfig
            {
                AutoFit = true,
                Padding = new[] { 40, 40, 40, 40 },
                XField = "age",
                YField = "count",
                Meta = new
                {
                    Count = new
                    {
                        Alias = "Count"
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

        protected override async Task OnInitializedAsync()
        {
            Config();
            await LoadStudentAgeAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await chartStudentAge.ChangeData(dataStudentAge);
            isFirstRender = false;
        }
    }
}
