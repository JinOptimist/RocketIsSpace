$(document).ready(function () {
    init();
    function init() {

        var url = '/Bank/BankCurrensyChartInfo';
        $.get(url)
            .done(function (answer) {
                if (answer) {
                    drawChart(answer);
                }
            });
    }

    function drawChart(data) {
        const config = {
            type: 'bar',
            data,
            options: {}
        };
        var myChart = new Chart(
            document.getElementById('myChartRate'),
            config
        );
    }
});
