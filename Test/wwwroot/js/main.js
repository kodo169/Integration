﻿(function ($) {
    "use strict";

    // Hàm để cập nhật dữ liệu của biểu đồ với các giá trị từ mảng selectedValues
    function updateChartWithSelectedValues(selectedValues) {
        var chartData = []; // Bắt đầu với các giá trị mặc định

        // Thêm các giá trị từ mảng selectedValues vào biểu đồ
        selectedValues.forEach(function (value) {
            chartData.push(value);
        });

        // Cập nhật dữ liệu cho biểu đồ
        worldwideSalesChart.data.labels = chartData;
        worldwideSalesChart.update();
    }
    
    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();
    
    
    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({scrollTop: 0}, 1500, 'easeInOutExpo');
        return false;
    });


    // Sidebar Toggler
    $('.sidebar-toggler').click(function () {
        $('.sidebar, .content').toggleClass("open");
        return false;
    });


    // Progress Bar
    $('.pg-bar').waypoint(function () {
        $('.progress .progress-bar').each(function () {
            $(this).css("width", $(this).attr("aria-valuenow") + '%');
        });
    }, {offset: '80%'});


    // Calender
    $('#calender').datetimepicker({
        inline: true,
        format: 'L'
    });


    // Testimonials carousel
    $(".testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        items: 1,
        dots: true,
        loop: true,
        nav : false
    });

    // Truy xuất canvas và các giá trị từ HTML
    // Lấy phần tử canvas từ HTML
    var canvas = $("#worldwide-sales");

    // Kiểm tra xem phần tử canvas có tồn tại trong DOM không
    if (canvas.length) {
        // Nếu phần tử tồn tại, thực hiện các hành động cho biểu đồ bar chart
        var y1Values = canvas.data("y1");
        var y2Values = canvas.data("y2");

        // Tạo đối tượng dữ liệu cho biểu đồ bar chart
        var chartData = {
            labels: [].concat(selectedValues),
            datasets: [{
                label: "Period",
                data: y1Values,
                backgroundColor: "rgba(0, 156, 255, .7)"
            },
            {
                label: "Far",
                data: y2Values,
                backgroundColor: "rgba(0, 156, 255, .5)"
            }
            ]
        };

        // Tạo biểu đồ bar chart
        var ctx1 = canvas.get(0).getContext("2d");
        var myChart1 = new Chart(ctx1, {
            type: "bar",
            data: chartData,
            options: {
                responsive: true
            }
        });
    } else {
        // Nếu phần tử không tồn tại, thực hiện các hành động cho biểu đồ doughnut chart
        var canvasDoughnut = $("#doughnut-chart");
        var doughnutLabels = canvasDoughnut.data("labels");
        var doughnutValues = canvasDoughnut.data("values");

        // Tạo đối tượng dữ liệu cho biểu đồ
        var doughnutData = {
            labels: doughnutLabels,
            datasets: [{
                backgroundColor: [
                    "rgba(0, 156, 255, .7)",
                    "rgba(0, 156, 255, .6)",
                    "rgba(0, 156, 255, .5)",
                    "rgba(0, 156, 255, .4)",
                    "rgba(0, 156, 255, .3)"
                ],
                data: doughnutValues
            }]
        };

        // Tạo biểu đồ doughnut
        var ctxDoughnut = canvasDoughnut.get(0).getContext("2d");
        var myDoughnutChart = new Chart(ctxDoughnut, {
            type: "doughnut",
            data: doughnutData,
            options: {
                responsive: true
            }
        });
    }

    
})(jQuery);

