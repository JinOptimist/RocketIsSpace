$(document).ready(function () {
    var regex = /\d+-\d+/;

    $('[value="Начисление"]').click(function () {
        var Id = $(this).attr('id');
        var url = '/Human/GetEmloyeAccrualsInfo?id=' + Id;
        $.get(url)
            .done(function (data) {
                console.log(data);
                $('#modal-salary-count').modal('show');
                var minMonth = regex.exec(data.inviteDate);
                var maxMonth = regex.exec(data.limitDate)
                $('#input-date').find('input').attr('min', minMonth[0]);
                $('#input-date').find('input').attr('max', maxMonth[0]);
                $('#IdEmploye').attr('value', data.idEmploye);

                $("#noAccrualsMonths option").remove();

                $.each(data.noAccrualsDates, function (i, item) {
                    var a = i;
                    var result = regex.exec(item);
                    var v = result[0];
                    $("#noAccrualsMonths").append($("<option>").attr('value', v));
                });
            });
    });
});