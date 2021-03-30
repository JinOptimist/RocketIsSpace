$(document).ready(function () {
    $('[name=UserName]').change(function () {
        var name = $(this).val();

        var url = '/User/IsUserExist?name=' + name;
        
        $.get(url).done(function (answer) {
            //Когда придёт ответ
            console.log("answer = " + answer);
        });
    });

});