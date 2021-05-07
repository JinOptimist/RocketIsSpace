$(document).ready(function () {
    var tag = '.employes-list .human-department-info';
    $('.human-department-clickable .temp').click(function () {
        var currentDepartment = $(this).parent('label').find('input');
        var departmentId = currentDepartment.val();
        var url = 'UpdateEmployes?idDepartment=' + departmentId;

        $.getJSON(url)
            .done(function (json) {
                for (var i = 0; i < json.employes.length; i++) {
                    InsertText(tag, json.employes[i].name);
                    InsertText(tag, json.employes[i].surname);
                    InsertText(tag, json.employes[i].specification);
                }
            });
    });
    function InsertText(inputTag, inputText) {
        $(inputTag).text('<div>' + inputText + '</div>');

    };
});