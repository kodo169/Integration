(function ($) {
        "use strict";
        var canvasDoughnut = $("#doughnut-chart");
        if (canvasDoughnut.length) {
            var doughnutLabels = canvasDoughnut.data("labels");
            var doughnutValues = canvasDoughnut.data("values");

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

            var ctxDoughnut = canvasDoughnut.get(0).getContext("2d");
            var myDoughnutChart = new Chart(ctxDoughnut, {
                type: "doughnut",
                data: doughnutData,
                options: {
                    responsive: true
                }
            });
        }

    // Additional functions for the doughnut chart can be added here if needed

})(jQuery);
