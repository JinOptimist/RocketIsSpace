$(document).ready(function () {
    var tag = '.employes-list .human-department-info';
    $('.human-department-clickable .department-info-block').click(function () {
        var departmentId = $(this).parent('label').find('input').val();
        var url = 'UpdateEmployes?idDepartment=' + departmentId;
        $('.employes-list .human-department-info:not(.clone)').remove();
        $.getJSON(url)
            .done(function (json) {
                for (var i = 0; i < json.employes.length; i++) {
                      Test();
                //    InsertText(tag, json.employes[i].name);
                //    InsertText(tag, json.employes[i].surname);
                //    InsertText(tag, json.employes[i].specification);
                }
            });
    });
    function Test() {
        var t = $('.clone').clone().appendTo('.employes-list');
        t.removeClass('clone undisplayable');
    };
});