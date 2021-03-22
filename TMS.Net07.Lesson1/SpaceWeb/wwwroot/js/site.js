$(document).ready(function () {
    $('.popup .cover').click(function () {
        $('.popup').hide(2000);
    });

    $('.rocket .icon.close').click(removeRocket);
    function removeRocket() {
        var iconClose = $(this);//Тот по которому кликнули
        iconClose
            .closest('.rocket')//найти среди родителей
            .hide();//Спрятать тэг
    }

    $('.rocket img').click(function () {
        //Добавляет класс full-size если его не было
        //убирает класс full-size если он был
        $(this).toggleClass('full-size');
    });

    $('.login').click(function () {
        var userName = $('.user-login').val();//Взять текст из инпута
        $('.header-text').text(userName);//Положть новый текст в тэг
    });

    $('.header-text').click(function () {
        var headerText = $(this).text();//Взять текст из тэга
        $('.user-login').val(headerText);//положить текст в инпут
    });

    $('.content').on('click', '.rocket .current-name,.rocket .icon.ok', function () {
        $(this)
            .closest('.name')
            .find('.view,.edit')
            .toggleClass('hide');
    });


    $('.content').on('click', '.rocket .current-name', function () {
        var currentName = $(this).text();
        $(this)
            .closest('.name')
            .find('.new-name')
            .val(currentName);
    });

    $('.content').on('click', '.rocket .icon.ok', onIconOk);
    $('.rocket .icon.ok').click(onIconOk);
    function onIconOk() {
        var newName = $(this)
            .closest('.edit')
            .find('.new-name')
            .val();
        $(this)
            .closest('.name')
            .find('.current-name')
            .text(newName);
    }

    $('.add-rocket .icon.add').click(function () {
        var name = $('.add-rocket .new-name').val();
        var url = $('.add-rocket .new-image').val();
        var copy = $('.rocket.template').clone();
        copy
            .find('.current-name')
            .text(name);

        copy
            .find('.image-container img')
            .attr('src', url);

        copy.removeClass('template');

        copy.find('.icon.close').click(removeRocket);

        $('.content').append(copy);
    });

    $('[name=visible-remove]').change(function () {
        var val = $('[name=visible-remove]:checked').val();
        var isVisible = val == '1';
        $('.icon.close').toggle(isVisible);
    });
});
