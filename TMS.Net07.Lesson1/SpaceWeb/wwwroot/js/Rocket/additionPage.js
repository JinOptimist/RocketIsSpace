$(document).ready(function () {

    $('.circle').hover(function () {
        $(this).children()
            .toggleClass('material-icons-outlined')
            .toggleClass('material-icons');
    });
});
