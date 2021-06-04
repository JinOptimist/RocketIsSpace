$(document).ready(function () {
    var tag = '.employes-list .human-department-info';
    $('.human-department-clickable .department-info-block').click(function () {
        var departmentId = $(this).parent('label').find('input').val();
        var url = '/Human/UpdateEmployes?idDepartment=' + departmentId;
        $('.employes-list .human-department-info:not(.clone)').remove();
        $.getJSON(url)
            .done(function (employes) {
                for (var i = 0; i < employes.length; i++) {
                    var block = $('.clone').clone(true).appendTo('.employes-list');
                    block.removeClass('clone undisplayable');
                    InsertText(block, '#name', employes[i].name);
                    InsertText(block, '#surname', employes[i].surname);
                    InsertText(block, '#specification', employes[i].position);
                    InsertText(block, '#salary', employes[i].salaryPerHour);
                }
            });
    });
    function InsertText(block, divToFind, arg) {
        block.find(divToFind).append(arg);
    };
});