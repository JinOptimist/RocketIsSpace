$(document).ready(function () {
    $('.login').click(function () {
        var userName = $('.user-login').val();//����� ����� �� ������
        $('.button-login').text(userName);//������� ����� ����� � ���
    });
    $('.header-text').click(function () {
        var headerText = $(this).text();//����� ����� �� ����
        $('.user-login').val(headerText);//�������� ����� � �����
    });


    $('.rocket .icon.close').click(removeRocket);
    function removeRocket() {
        var iconClose = $(this);//��� �� �������� ��������
        iconClose
            .closest('.rocket')//����� ����� ���������
            .hide();//�������� ���
    }

});