var time = 800;

var blockHeight = 117;

var windowCapacity = 4;

var easingType = 'linear';

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

    ChangeActiveAccount(index, time);

    ScrollLeftMenu(index, time);

    isAnimationActive = false;
}

function SetActiveAccount(index) {
    $('.active-account').val(index);
}

function ScrollLeftMenu(index, time) {

    var blockPosition = blockHeight + index * blockHeight;

    var windowPosition = $('.bank-account-list').scrollTop();

    var visibleZone = (windowPosition + windowCapacity * blockHeight) * 0.9;

    var nextPosition = {};

    var delta = {};

    SetAccountListVisualChange(index);

    if (IsVisible(index)) {
        return;
    }
    else if (blockPosition < windowPosition) {
        nextPosition = blockPosition;
    }
    else if (blockPosition > visibleZone) {
        nextPosition = blockPosition - ((windowCapacity - 1) * blockHeight);
    }

    delta = nextPosition - windowPosition;

    AnimateAccountList(windowPosition, delta);

    function IsVisible(index) {

        if (blockPosition >= windowPosition && blockPosition <= visibleZone) {
            console.log('visible');
            return true;
        }
        else {
            console.log('not visible');
            return false;
        }
}

    function AnimateAccountList(windowPosition, delta) {
        $('.bank-account-list').animate(
            {
                'progress': 100
            },
            {
                duration: time/3*2,
                step: function (progress) {
                    var s = windowPosition + delta / 100 * progress;
                    $('.bank-account-list').scrollTop(s);
                },
                queue: false,
                easing: easingType,
                complete: function () {

                    $('.bank-account-list').css('progress', 0)
                }
            }
        );
    }

    function SetAccountListVisualChange(index) {
        //$('.menu.left .bank-account').animate({
        //    //borderWidth: 0,
        //}, {
        //    duration: time / 4,
        //    queue: false
        //});

        $('.menu.left .bank-account').css('background-color', 'rgba(0, 185, 229, 0.5)');

        //background - color: rgba(0, 185, 229, 0.7);
        //$(`.bank-account.${index}`).animate({
        //    //borderWidth: 8,
        //}, {
        //        duration: time / 4,
        //        queue: false,
        //    });

        $(`.bank-account.${index}`).css('background-color', 'rgba(0, 185, 229)');
    }
}

function ChangeActiveAccount(index, time) {
    $('.account-carousel').animate({
        'margin-left': (-1 * index * 430)
    }, { duration: time, easing: easingType, queue: false });
}