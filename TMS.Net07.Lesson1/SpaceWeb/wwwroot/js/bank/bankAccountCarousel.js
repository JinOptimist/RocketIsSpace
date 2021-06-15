var time = 800;

var blockHeight = 117;

var windowCapacity = 4;

var easingType = 'swing';

$(document).ready(function () {
    var isAnimationActive = false;

    var activeAccIndex = GetActiveAccount();

    var totalAccounts = GetTotalAccounts();

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

    $('.bank-account-list a').click(function (evt) {
        var activeAccIndex = $(this).find('.account-index').text() - 0;
        console.log(activeAccIndex);
        if (isAnimationActive) {
            return;
        }

        SetPosition(activeAccIndex, time);

        SetActiveAccount(activeAccIndex);

        evt.preventDefault();
    })
});

function GetActiveAccount() {
    return $('.active-account').val() - 0;
}

function GetTotalAccounts() {
    return $('.total-accounts').text() - 0;
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
                duration: time,
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
        $('.menu.left .bank-account').animate(
            {
                'progress': 100
            },
            {
                duration: time/2,
                step: function (progress) {
                    var s = 1 / 100 * progress;
                    $('.menu.left .bank-account.bordered')
                        .css('border', `8px rgba(255, 255, 255, ${1 - s}) double`);
                        
                    $(`.menu.left .bank-account.${index}`).css('border', `8px rgba(255, 255, 255, ${s}) double`);
                },
                queue: false,
                easing: easingType,
                complete: function () {
                    $('.menu.left .bank-account').css('progress', 0);
                    $('.menu.left .bank-account.bordered').toggleClass('bordered');
                    $(`.menu.left .bank-account.${index}`).toggleClass('bordered');
                }
            }
        );

        //$('.menu.left .bank-account').css('background-color', 'rgba(0, 185, 229, 0.5)');

        //$(`.bank-account.${index}`).css('background-color', 'rgba(0, 185, 229)');
    }
}

function ChangeActiveAccount(index, time) {
    $('.account-carousel').animate({
        'margin-left': (-1 * index * 430)
    }, { duration: time, easing: easingType, queue: false });
}