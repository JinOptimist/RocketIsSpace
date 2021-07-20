$(document).ready(function () {

    var drawedVertexes = [];
    var wayIndexer;
    var currentInterval;
    var time = 1000;
    var color = 'black'

    $('.maze-way').click(function () {
        var x = $(this).find('.cell').attr('x');
        var y = $(this).find('.cell').attr('y');
        $.get(`/Home/PossibleWays?x=${x}&y=${y}`)
            .done(function (data) {

                SetDefault();
                clearInterval(currentInterval);
                currentInterval = setInterval(function () { testFunc(data) }, time);
                
            });
    });

    function testFunc(data) {

        SetDefaultVertexes();

        for (var j = 0; j < data[wayIndexer].cells.length; j++) {
            var pointX = data[wayIndexer].cells[j].x;
            var pointY = data[wayIndexer].cells[j].y;
            var vertex = $(`.cell[x=${pointX}][y=${pointY}]`)
            vertex.parent().css('background', color);
            drawedVertexes.push(vertex);
        }

        wayIndexer++;
        if (wayIndexer >= data.length) {
            wayIndexer = 0;
        }

    }

    function SetDefault() {
        wayIndexer = 0;
        SetDefaultVertexes();
    }

    function SetDefaultVertexes() {
        for (var i = drawedVertexes.length - 1; i >= 0; i--) {
            drawedVertexes[i].parent().css('background', '');
            drawedVertexes.pop();
        }
    }

});