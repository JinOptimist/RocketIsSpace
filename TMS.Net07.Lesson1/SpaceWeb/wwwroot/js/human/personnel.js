$(document).ready(function () {
    var regex = /\d+-\d+/;

    $('[name="btn-accrual"]').click(function () {
        var Id = $(this).attr('id');
        var url = '/Human/GetEmloyeAccrualsInfo?id=' + Id;
        $.get(url)
            .done(function (data) {
                $('#modal-salary-count').modal('show');
                var minMonth = regex.exec(data.inviteDate);
                var maxMonth = regex.exec(data.limitDate);
                $('#input-date').find('input').attr('min', minMonth[0]);
                $('#input-date').find('input').attr('max', maxMonth[0]);
                $('#IdEmploye').attr('value', data.idEmploye);

                $("#noAccrualsMonths option").remove();

                $.each(data.noAccrualsDates, function (i, item) {
                    var result = regex.exec(item);
                    $("#noAccrualsMonths").append($("<option>").attr('value', result[0]));
                });
            });
    });

    $('[name="btn-payment"]').click(function () {
        var Id = $(this).attr('id');
        var url = '/Human/GetEmployePaymentInfo?id=' + Id;
        $.get(url)
        $('#modal-payment').modal('show');
    });

    $('#input-date input').change(function () {
        var date = $('#input-date input').val();
        var id = $('#hidden-id input').val();
        url = `/Human/ChangeDate?date=${date}&IdEmploye=${id}`;
        console.log(date);
        $.get(url)
            .done(function (data) {
                var input = $('#input-amount').find('input');
                input.attr('value', data);
            });
    });
});