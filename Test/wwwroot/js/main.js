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
    var xValues;
    var y1Values;
    var y2Values;
    //các biến hiển thị
    var xShow = [];
    var y1Show = [];
    var y2Show = [];
    // Hàm vẽ biểu đồ
    function addData(index,y1data,y2data) {
        for (var i = 0; i < xValues.length; i++) {
            if (y1Show[i] > 0 && y2Show[i] > 0) {
                y1Show[i] = 0;
                y2Show[i] = 0;
                break;
            }
            if (i == index) {
                y1Show[i] = y1data;
                y2Show[i] = y2data;
            }
        }
    }
    function drawChart() {
        var canvas = $("#worldwide-sales");
        var canvasDoughnut = $("#doughnut-chart");
        // Kiểm tra nếu đã có biểu đồ được vẽ trên canvas trước đó
        if (window.myChart1) {
            window.myChart1.destroy(); // Xóa biểu đồ cũ đi
            console.log("Đã chạy tới đây")
        }
        // Kiểm tra xem phần tử canvas có tồn tại trong DOM không
        if (canvas.length) {
            // Nếu phần tử tồn tại, thực hiện các hành động cho biểu đồ bar chart
            xValues = canvas.data("x").split(",");
            console.log(xValues[0]);
            for (var i = 0; i < xValues.length; i++) {
                y1Show.push(0);
                y2Show.push(0);
            }
            y1Values = canvas.data("y1-values").split(",");
            y2Values = canvas.data("y2-values").split(",");
            var titles = ["Total Last Year", "Total to Present"];
            var titleIndex = 0;
            // Tạo đối tượng dữ liệu cho biểu đồ bar chart
            var chartData = {
                labels: xValues,
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
            window.myChart1 = new Chart(ctx1, {
                type: "bar",
                data: chartData,
                options: {
                    responsive: true
                }
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
    }

    // Gọi hàm vẽ biểu đồ khi trang được load
    $(document).ready(function () {
        drawChart();
    });

    // Gọi hàm vẽ biểu đồ khi click vào một item
    document.querySelectorAll('.nav-item.dropdown, .nav-item.p-4').forEach(function (navItem) {
        navItem.querySelectorAll('.dropdown-item, button').forEach(function (item) {
            console.log("Click")
            item.addEventListener('click', function (e) {
                e.preventDefault();
                var index = -1;
                index = parseInt(this.getAttribute('data-index')); // Lấy index từ thuộc tính data-index
                if (index >= 0) {
                    addData(index, y1Values[index], y2Values[index])
                    drawChart(); // Gọi lại hàm vẽ biểu đồ sau khi click
                }
            });
        });
    });
})(jQuery);

