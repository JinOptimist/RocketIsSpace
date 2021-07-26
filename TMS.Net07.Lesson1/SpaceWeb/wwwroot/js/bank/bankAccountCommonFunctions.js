function FrozenAccountCheck() {
    var isFrozen = $('.active-account.frozen-status').val();
    console.log(isFrozen);
    if (isFrozen == "true") {
        if ($('.button.freeze .unfreeze-account.text').attr('class').includes('hide')) {
            $('.button.freeze .text').toggleClass('hide');
        }
    }
    if (isFrozen == "false") {
        if ($('.button.freeze .freeze-account.text').attr('class').includes('hide')) {
            $('.button.freeze .text').toggleClass('hide');
        }
    }
}

function GetActiveAccount() {

    var obj = {
        index: $('.active-account.index').val() - 0,
        id: $('.active-account.id').val() - 0,
        isFrozen: $('.active-account.frozen-status').val()
    }

    return obj;
}

function SetActiveAccount(activeAccount) {

    $('.active-account.index').val(activeAccount.index);
    $('.active-account.id').val(activeAccount.id);
    $('.active-account.frozen-status').val(activeAccount.isFrozen);

    $('.account-to-remove').val(activeAccount.id);
}

function ActualizeActiveAccount(index) {
    var id = $(`.account-info-container.${index}`).find('.info.id').val() - 0;

    var isFrozen = $(`.account-info-container.${index}`).find('.info.frozen-status').text();

    var obj = {
        index: index,
        id: id,
        isFrozen: isFrozen
    }

    return obj;
}

function GetActiveAccountByIndex(index){

    var obj = {
        index: index,
        id: $(`.account-info-container.${index} .info.id`).val() - 0,
        isFrozen: $(`.account-info-container.${index} .info.frozen-status`).text()
    }

    return obj;
}

function ChangeFrozenStatus(activeAccount) {
    $(`.${activeAccount.index} .frozen-status`).text(activeAccount.isFrozen);
}