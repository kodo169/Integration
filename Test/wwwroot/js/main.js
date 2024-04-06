(function ($) {
    "use strict";

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
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
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
    }, { offset: '80%' });

    // Calendar
    $('#calendar').datetimepicker({
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
        nav: false
    });
    // Truy xuất canvas và các giá trị từ HTML
    // Lấy phần tử canvas từ HTML
    $(document).ready(function () {
        var canvas = $("#worldwide-sales");
        var canvasDoughnut = $("#doughnut-chart");
        // Kiểm tra xem phần tử canvas có tồn tại trong DOM không
        if (canvas.length) {
            // Nếu phần tử tồn tại, thực hiện các hành động cho biểu đồ bar chart
            var xValues = canvas.data("x").split(",");
            var y1Values = canvas.data("y1-values").split(","); 
            var y2Values = canvas.data("y2-values").split(","); 
            //Các biến dùng cho click
            var xData = [];
            var y1Data = [];
            var y2Data = [];
            //Các biến mặc định 
            var xBase = [" "];
            var y1Base = [0];
            var y2Base = [0];
            //các biến hiển thị
            var xShow = [" "];
            var y1Show = [0];
            var y2Show = [0];
            var titles = ["Total Last Year", "Total to Present"];
            var titleIndex = 0;
            // Tạo đối tượng dữ liệu cho biểu đồ bar chart
            var chartData = {
                labels: xShow,
                datasets: [{
                    label: titles[titleIndex],
                    data: y1Show,
                    backgroundColor: "rgba(0, 156, 255, .7)"
                },
                {
                    label: titles[titleIndex + 1],
                    data: y2Show,
                    backgroundColor: "rgba(0, 156, 255, .5)"
                }]
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

            document.querySelectorAll('.nav-item.dropdown').forEach(function (dropdown) {
                dropdown.querySelectorAll('.dropdown-item').forEach(function (item) {
                    item.addEventListener('click', function (e) {
                        e.preventDefault();
                        var index = -1;
                        index = parseInt(this.getAttribute('data-index')); // Lấy index từ thuộc tính data-index
                        if (index >= 0 && index < canvas.data("x").length) {
                            xData.push(xValues[index])
                            y1Data.push(y1Values[index])
                            y2Data.push(y2Values[index])
                            // làm hàm check 
                            xShow = xBase.concat(xData)
                            y1Show = y1Base.concat(y1Data)
                            y2Show = y2Base.concat(y2Data)
                            console.log(xShow)
                            console.log(y1Show)
                            console.log(y2Show)
                        }
                    });
                });
            });

        } else {
            // Nếu phần tử không tồn tại, thực hiện các hành động cho biểu đồ doughnut chart
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
    });
})(jQuery);
