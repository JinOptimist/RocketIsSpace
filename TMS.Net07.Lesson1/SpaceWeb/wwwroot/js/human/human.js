$(document).ready(function () {
    var tag = '.employes-list .human-department-info';
    $('.human-department-clickable .department-info-block').click(function () {
        var departmentId = $(this).parent('label').find('input').val();
        var url = 'UpdateEmployes?idDepartment=' + departmentId;
        $('.employes-list .human-department-info div').remove();
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
        $(inputTag).append('<div>' + inputText + '</div>');

    };
});