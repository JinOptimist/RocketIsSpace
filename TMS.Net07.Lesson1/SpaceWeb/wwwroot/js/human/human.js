$(document).ready(function () {
    $('.human-department-clickable').click(function () {
        var currentDepartment = $(this).parent('label').find('input');
        console.log('s');
    });
});