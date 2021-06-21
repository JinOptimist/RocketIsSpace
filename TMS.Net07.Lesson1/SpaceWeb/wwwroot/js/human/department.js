$(document).ready(function () {

    function setter() {
        return {
            trigger: 'click',
            speed: 1000
        }
    };

    var tag = '.employes-list .human-department-employee';

    $('.human-department-info').click(function () {
        //var departmentId = $(this).parent('label').find('input').val();
        var departmentId = $(this).find('#department_Id').val();
        var url = '/Human/UpdateEmployes?idDepartment=' + departmentId;
        $('.employes-list .human-department-employee:not(.clone)').remove();
        $.getJSON(url)
            .done(function (employes) {
                console.log(employes);
                for (var i = 0; i < employes.length; i++) {
                    var block = $('.clone').clone().appendTo('.employes-list');
                    InsertText(block, '#name', employes[i].name);
                    InsertText(block, '#surname', employes[i].surname);
                    InsertText(block, '#specification', employes[i].position);
                    InsertText(block, '#salary', employes[i].salaryPerHour);
                    InsertImage(block, '#image', employes[i].avatarUrl);
                    //block.flip(setter());
                    block.removeClass('clone undisplayable');
                }
            });
    });

    function InsertText(block, divToFind, arg) {
        block.find(divToFind).append(arg);
    };

    function InsertImage(block, divToFind, avatarUrl) {
        block.find(divToFind).attr('src', avatarUrl);
    };

   // $('.flip-flop').flip(setter());

});