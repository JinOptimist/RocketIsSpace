var time = 800;

$(document).ready(function () {
    var isAnimationActive = false;

    var activeAccIndex = GetActiveAccount();

    var totalAccounts = $('.total-accounts').text() - 0;

    SetPosition(activeAccIndex, 0);


    $('.next-btn').click(function () {
        if (isAnimationActive) {
            return;
        }
        if (activeAccIndex == (totalAccounts-1)) {
            activeAccIndex = 0;
        }
        else {
            activeAccIndex++;
        }

        SetPosition(activeAccIndex, time);

        SetActiveAccount(activeAccIndex);
    });

    $('.prev-btn').click(function () {
        if (isAnimationActive) {
            return;
        }
        if (activeAccIndex == 0) {
            activeAccIndex = (totalAccounts - 1);
        }
        else {
            activeAccIndex--;
        }

        SetPosition(activeAccIndex, time);

        SetActiveAccount(activeAccIndex);
    });
    
});

function GetActiveAccount() {
    return $('.active-account').val() - 0;
}

function SetPosition(index, time) {
    isAnimationActive = true;

    $('.account-carousel').animate({
        'margin-left': (-1 * index * 430)
    }, { duration: time, easing: 'swing', queue: false });

    $('.menu.left .bank-account').animate({
        borderWidth: 0,
    }, {
        duration: time / 4,
        queue: false
    });

    $(`.bank-account.${index}`).animate({
        borderWidth: 8,
    }, {
            duration: time / 4,
            queue: false,
        });

    //ScrollLeftMenu(index, time);

    console.log(IsVivible(index));

    isAnimationActive = false;
}

function SetActiveAccount(index) {
    $('.active-account').val(index);
}

function ScrollLeftMenu(index, time) {

    var currentPosition = $('.bank-account-list').scrollTop() - 0;

    var nextPosition = index * 100;

    if (index > 2) {
        
        nextPosition = 100 * (index - 2);
    }
    else {
        nextPosition = 0;
    }



    //$('.bank-account-list').animate(
    //    {
    //        'progress' : 100
    //    },
    //    {
    //        duration: time,
    //        step: function (progress) {
    //            var s = currentPosition + nextPosition/100*progress;
    //                console.log(s);
    //                $('.bank-account-list').scrollTop(s);
    //            },
    //        complete: function () {
    //            $('.bank-account-list').css('progress', 0)
    //        }
    //    }
    //);

    function IsVivible(index) {

        var blockHeight = 186;

        var windowCapacity = 4;

        var blockPosition = blockHeight + index * blockHeight;

        var windowPosition = $('.bank-account-list').scrollTop();

        var visibleZone = windowPosition + windowCapacity * blockHeight;

        if (blockPosition > windowPosition && blockPosition < visibleZone) {
            return true;
        }
        else {
            return false;
        }
    }
}