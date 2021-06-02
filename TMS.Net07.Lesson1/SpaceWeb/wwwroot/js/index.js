$(document).ready(function () {
    init();
    function init() {

        var url = '/Home/AccountChartInfo';
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
            document.getElementById('myChart'),
            config
        );
    }
});
