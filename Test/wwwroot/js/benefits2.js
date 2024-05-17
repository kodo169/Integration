(function ($) {
    "use strict";
    var canvasDoughnut1 = $("#doughnut-chart1");
    if (canvasDoughnut1.length) {
        var doughnutLabels1 = canvasDoughnut1.data("labels");
        var doughnutValues1 = canvasDoughnut1.data("values");

        var doughnutData1 = {
            labels: doughnutLabels1,
            datasets: [{
                backgroundColor: [
                    "rgba(255, 99, 132, 0.7)",
                    "rgba(255, 99, 132, 0.6)",
                    "rgba(255, 99, 132, 0.5)",
                    "rgba(255, 99, 132, 0.4)",
                    "rgba(255, 99, 132, 0.3)"
                ],
                data: doughnutValues1
            }]
        };

        var ctxDoughnut1 = canvasDoughnut1.get(0).getContext("2d");
        new Chart(ctxDoughnut1, {
            type: "doughnut",
            data: doughnutData1,
            options: {
                responsive: true
            }
        });
    }
})(jQuery);