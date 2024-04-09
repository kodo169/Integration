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
    function isXDataInXShow(xdata) {
        return xShow.includes(xdata);
    }
    function getXDataIndexInXShow(xdata) {
        var index = xShow.indexOf(xdata);
        if (index !== -1) {
            return { found: true, index: index };
        } else {
            return { found: false, index: -1 };
        }
    }
    function addDataWithEthnicity(index_X, y1data, y2data, xdata) {
        var deleteIndex = getXDataIndexInXShow(xdata).index;
        if (getXDataIndexInXShow(xdata).found == true) {
            xShow.splice(deleteIndex, 1);
            y1Show.splice(deleteIndex, 1);
            y2Show.splice(deleteIndex, 1);
            return;
        }
        else {
            xShow[index_X] = xdata;
            y1Show[index_X] = y1data;
            y2Show[index_X] = y2data;
            return;
        }
    }
    function addData(index, y1data, y2data) {
        if (y1Show[index] > 0 && y2Show[index] > 0) {
            y1Show[index] = 0;
            y2Show[index] = 0;
            return;
        }
        else {
            y1Show[index] = y1data;
            y2Show[index] = y2data;
        }
    }
    document.querySelectorAll('.nav-item.dropdown, .nav-item.p-4').forEach(function (navItem) {
        navItem.querySelectorAll('.dropdown-item, button').forEach(function (item) {
            item.addEventListener('click', function (e) {
                e.preventDefault();
                var index = -1;
                index = parseInt(this.getAttribute('data-index')); // Lấy index từ thuộc tính data-index
                if (index >= 5) {
                    addDataWithEthnicity(xShow.length,sumdataY1[index], sumdataY2[index], sumdataX[index]);
                    drawChart();
                    return;
                }
                else {
                    addData(index, sumdataY1[index], sumdataY2[index]);
                    drawChart();
                }
            });
        });
    });
    /*$(document).ready(function () {
        drawChart();
    });
    // start biến earning
    var xValues;
    var y1Values;
    var y2Values;
    var xShow = [];
    var y1Show = [];
    var y2Show = [];
    var xEthnicityValues;
    var sumdataX = [];
    var sumdataY1 = [];
    var sumdataY2 = [];
    // end biến earning
    var checkdata = false;
    var checkdataXshow = false;
    function drawChart() {
        var canvas = $("#worldwide-sales");
        var canvasDoughnut = $("#doughnut-chart");
        if (window.myChart1) {
            window.myChart1.destroy(); // Xóa biểu đồ cũ đi
        }
        // Kiểm tra xem phần tử canvas có tồn tại trong DOM không
        if (canvas.length) {
            // dữ liệu cho earnings

            xValues = canvas.data("x").split(",");
            var xShow_intermediate = canvas.data("x").split(",");
            y1Values = canvas.data("y1-values").split(",");
            y2Values = canvas.data("y2-values").split(",");
            xEthnicityValues = canvas.data("ethnicity").split(",");

            sumdataX = xValues.concat(xEthnicityValues);
            sumdataY1 = [].concat(y1Values);
            sumdataY2 = [].concat(y2Values);

            if (checkdataXshow == false) {
                xShow = xShow.concat(xShow_intermediate);
                checkdataXshow = true;
            }
            if (checkdata == false) {
                for (var i = 0; i < sumdataX.length; i++) {
                    y1Show.push(0);
                    y2Show.push(0);
                }
                checkdata = true;
            }

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
    }*/
})(jQuery);

