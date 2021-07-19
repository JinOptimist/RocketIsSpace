$(document).ready(function () {

    init();

    function init() {

        var url = '/api/HumanApi/GetGraph';
        $.get(url)
            .done(function (answer) {
                drawChart(answer);
            });
    };

    function drawChart(data) {
        const config = {
            type: 'bar',
            data,
            options: {}
        };
        var myChart = new Chart(
            document.getElementById('myGraph'),
            config
        );
    };
});