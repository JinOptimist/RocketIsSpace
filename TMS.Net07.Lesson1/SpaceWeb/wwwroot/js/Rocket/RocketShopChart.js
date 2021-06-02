$(document).ready(function () {
    init();
    function init() {

        var url = '/RocketShop/OrderChartInfo';
        $.get(url)
            .done(function (answer) {
                drawChart(answer);
            });
    }

    function drawChart(data) {
        const config = {
            type: 'bar',
            data,
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        };
        var myChart = new Chart(
            document.getElementById('order-chart'),
            config
        );
    }
});