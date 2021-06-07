$(document).ready(function () {
    init();
    function init() {

        var url = '/Bank/AccountsChartInfoDraw';
        $.get(url)
            .done(function (answer) {
                drawChart(answer);
            });
    }

    function drawChart(data) {
        const config = {
            type: 'bar',
            data,
            options: {}
        };
        var myChart = new Chart(
            document.getElementById('myChart'),
            config
        );
    }
});
