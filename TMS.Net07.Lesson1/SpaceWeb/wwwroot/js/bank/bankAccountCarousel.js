var time = 800;

var blockHeight = 117;

var windowCapacity = 4;

var easingType = 'swing';

var marginLeft = 65;

var cardWidth = 430;

$(document).ready(function () {
    var isAnimationActive = false;

    var activeAccount = GetActiveAccount();

    var totalAccounts = GetTotalAccounts();

    SetPosition(activeAccount, 0);

    $('.next-btn').click(function () {
        if (isAnimationActive) {
            return;
        }
        if (activeAccount.index == (totalAccounts - 1)) {
            activeAccount.index = 0;
        }
        else {
            activeAccount.index++;
        }

        AnimateAndSetAccount(activeAccount);
    });

    $('.prev-btn').click(function () {
        if (isAnimationActive) {
            return;
        }
        if (activeAccount.index == 0) {
            activeAccount.index = (totalAccounts - 1);
        }
        else {
            activeAccount.index--;
        }

        AnimateAndSetAccount(activeAccount);

    });

    $('.bank-account-list .account-link').click(function (evt) {
        activeAccount.index = $(this).find('.account-index').text() - 0;
        console.log(activeAccount.index);
        if (isAnimationActive) {
            return;
        }

        AnimateAndSetAccount(activeAccount);

        evt.preventDefault();
    })
});

function AnimateAndSetAccount(activeAccount) {

    var newActiveAccount = GetActiveAccountByIndex(activeAccount.index);

    SetPosition(newActiveAccount, time);

    SetActiveAccount(newActiveAccount);

    FrozenAccountCheck();
}

function GetTotalAccounts() {
    return $('.total-accounts').text() - 0;
}

function SetPosition(activeAccount, time) {

    isAnimationActive = true;

    AnimateActiveAccount(activeAccount, time);

    ScrollLeftMenu(activeAccount, time);

    isAnimationActive = false;
}

function ScrollLeftMenu(activeAccount, time) {

    var blockPosition = blockHeight + activeAccount.index * blockHeight;

    var windowPosition = $('.bank-account-list').scrollTop();

    var visibleZone = (windowPosition + windowCapacity * blockHeight) * 0.9;

    var nextPosition = {};

    var delta = {};

    SetAccountListVisualChange(activeAccount);

    if (IsVisible()) {
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

    function IsVisible() {

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

    function SetAccountListVisualChange(activeAccount) {
        $('.menu.left .bank-account').animate(
            {
                'progress': 100
            },
            {
                duration: time/2,
                step: function (progress) {
                    var s = 1 / 100 * progress;
                    $('.menu.left .bank-account.bordered .account-info')
                        .css('border', `8px rgba(255, 255, 255, ${1 - s}) double`);
                        
                    $(`.menu.left .bank-account.${activeAccount.index} .account-info`).css('border', `8px rgba(255, 255, 255, ${s}) double`);
                },
                queue: false,
                easing: easingType,
                complete: function () {
                    $('.menu.left .bank-account').css('progress', 0);
                    $('.menu.left .bank-account.bordered').toggleClass('bordered');
                    $(`.menu.left .bank-account.${activeAccount.index}`).toggleClass('bordered');
                }
            }
        );
    }
}

function AnimateActiveAccount(activeAccount, time) {
    $('.account-carousel').animate({
        'margin-left': (marginLeft + (-1 * activeAccount.index * cardWidth))
    }, { duration: time, easing: easingType, queue: false });
}