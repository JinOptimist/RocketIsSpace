$(document).ready(function () {
    var tag = '.employes-list .human-department-info';
    $('.human-department-clickable .department-info-block').click(function () {
        var departmentId = $(this).parent('label').find('input').val();
        var url = 'UpdateEmployes?idDepartment=' + departmentId;
        $('.employes-list .human-department-info:not(.clone)').remove();
        $.getJSON(url)
            .done(function (json) {
                console.log(json);
                for (var i = 0; i < json.employes.length; i++) {
                    var block = $('.clone').clone().appendTo('.employes-list');
                    block.removeClass('clone undisplayable');
                    InsertText(block, '#name', json.employes[i].name);
                    InsertText(block, '#surname', json.employes[i].surname);
                    InsertText(block, '#specification', json.employes[i].specification);
                    InsertText(block, '#salary', json.employes[i].salaryPerHour);
                }
            });
    });
    function InsertText(block, divToFind, arg) {
        block.find(divToFind).append(arg);
    };
});