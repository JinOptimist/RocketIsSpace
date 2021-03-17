$(document).ready(function (){
    
    $('.popup .cover').click(function(){
        $('.popup').hide(2000);
    });

    $('.rocket .icon.close').click(function(){
        var iconClose = $(this);//Тот по которому кликнули
        iconClose
            .closest('.rocket')
            .hide();
    });

    $('.rocket img').click(function(){
        //Добавляет класс full-size если его не было
        //убирает класс full-size если он был
        $(this).toggleClass('full-size');
    });

    $('.login').click(function(){
        var userName = $('.user-login').val();
        $('.header-text').text(userName);
    });

    $('.header-text').click(function(){
        var headerText = $(this).text();
        $('.user-login').val(headerText);
        
    });
});
