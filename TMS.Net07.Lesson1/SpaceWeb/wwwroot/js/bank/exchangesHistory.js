$(document).ready(function () {
    init();
    function init() {

        var url = '/Bank/ExchangesHistoryChartInfo';
        $.get(url)
            .done(function (answer) {
                drawChart(answer);
            });
    }

    function drawChart(data) {
        const config = {
            type: 'line',
            data,
            options: {}
        };
        var myChart = new Chart(
            document.getElementById('exchangesHistoryChart'),
            config
        );
    }
});
