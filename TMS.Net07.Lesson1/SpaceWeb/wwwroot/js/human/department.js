﻿$(document).ready(function () {

    function setter() {
        return {
            trigger: 'click',
            speed: 1000
        }
    };

    var tag = '.employes-list .human-department-employee';

    $('.human-department-clickable .department-info-block').click(function () {
        var departmentId = $(this).parent('label').find('input').val();
        var url = '/api/HumanApi/UpdateEmployes?departmentId=' + departmentId;
        $('.employes-list .human-department-employee:not(.clone)').remove();
        $.getJSON(url)
            .done(function (employes) {
                for (var i = 0; i < employes.length; i++) {
                    var block = $('.clone').clone().appendTo('.employes-list');
                    InsertText(block, '#name', employes[i].name);
                    InsertText(block, '#surname', employes[i].surname);
                    InsertText(block, '#specification', employes[i].position);
                    InsertText(block, '#salary', employes[i].salaryPerHour.toFixed(2));
                    InsertImage(block, '#image', employes[i].avatarUrl);
                    block.flip(setter());
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

    $('.flip-flop').flip(setter());

});