$(document).ready(function () {
    $('.login').click(function () {
        var userName = $('.user-login').val();//Взять текст из инпута
        $('.button-login').text(userName);//Положть новый текст в тэг
    });
    $('.header-text').click(function () {
        var headerText = $(this).text();//Взять текст из тэга
        $('.user-login').val(headerText);//положить текст в инпут
    });


    $('.rocket .icon.close').click(removeRocket);
    function removeRocket() {
        var iconClose = $(this);//Тот по которому кликнули
        iconClose
            .closest('.rocket')//найти среди родителей
            .hide();//Спрятать тэг
    }

});
