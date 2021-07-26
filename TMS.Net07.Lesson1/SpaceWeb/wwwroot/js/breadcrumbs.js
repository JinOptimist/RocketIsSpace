$(document).ready(function () {
    var cookieCount = 5;
    for (var i = cookieCount; i > 0; i--) {
        var indexPrev = i - 1;
        var cookieVal = readCookie('cookie' + indexPrev);
        createCookie('cookie' + i, cookieVal);
    }

    var href = document.location.href;
    var title = document.title || "?";
    var json = JSON.stringify({ href, title });
    createCookie('cookie0', json);

    for (var i = 0; i < cookieCount; i++) {
        var json = readCookie('cookie' + i);
        var cookieObj = JSON.parse(json);
        if (cookieObj) {
            var link = $('<a>');
            link.attr('href', cookieObj.href);
            link.addClass('breadcrumb');
            link.text(cookieObj.title);
            $(".breadcrumb-container").append(link);
        }
    }
});

var expiredDay = 1;

function createCookie(name, value) {
    if (expiredDay) {
        var date = new Date();
        date.setTime(date.getTime() + (expiredDay * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

// "guestLang=En; test=smile; c2=fun; guestLang=Ru"
