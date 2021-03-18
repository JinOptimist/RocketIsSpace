$(document).ready(function (){
    
    $('.popup .cover').click(function(){
        $('.popup').hide(2000);
    });

    $('.rocket .icon.close').click(function(){
        var iconClose = $(this);//Тот по которому кликнули
        iconClose
            .closest('.rocket')//найти среди родителей
            .hide();//Спрятать тэг
    });

    $('.rocket img').click(function(){
        //Добавляет класс full-size если его не было
        //убирает класс full-size если он был
        $(this).toggleClass('full-size');
    });

    $('.login').click(function(){
        var userName = $('.user-login').val();//Взять текст из инпута
        $('.header-text').text(userName);//Положть новый текст в тэг
    });

    $('.header-text').click(function(){
        var headerText = $(this).text();//Взять текст из тэга
        $('.user-login').val(headerText);//положить текст в инпут
    });

    $('.view .img-name').click(function(){
        $(this)
            .closest('.view')
            .hide();

        $(this)
            .closest('.name')
            .find('.edit')
            .show();
        var newName = $('.name-input').val();
        $('.img-name').text(newName);
    });
});
