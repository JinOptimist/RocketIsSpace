var wayModule = (function () {

    var drawedVertexes = [];
    var wayIndexer;
    var currentInterval;
    var time = 1000;
    var color = 'black'
    var selector;

    function onClick() {
        var x = $(this).find('.cell').attr('x');
        var y = $(this).find('.cell').attr('y');
        $.get(`/Home/PossibleWays?x=${x}&y=${y}`)
            .done(function (data) {

                SetDefault();
                clearInterval(currentInterval);
                currentInterval = setInterval(function () { draw(data) }, time);

            });
    }


    function draw(data) {

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

    function initialize(_selector, _types, _options) {
        FillSelector(_selector, _types);
        if (_options.color) {
            color = _options.color;
        }
        if (_options.time) {
            time = _options.time;
        }
        $(`${selector}`).click(onClick);
    }

    function FillSelector(_selector, _types) {
        selector = "";
        var valueString = _types.pop();
        var rightPart = valueString == null ? "" : "." + valueString;
        var leftPart = "." + _selector;
        selector = leftPart + rightPart;

        while (_types[0] != null) {
            selector += ",";
            rightPart = "." + _types.pop();
            leftPart = "." + _selector;
            selector += leftPart + rightPart;
        }
    }

    return {
        initialize: initialize
    };

})();