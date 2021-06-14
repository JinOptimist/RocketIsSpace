$(document).ready(function () {
    var time = 800;

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
        "margin-left": (-1 * index * 430)
    }, time);
    isAnimationActive = false;
}

function SetActiveAccount(index) {
    $('.active-account').val(index);
}