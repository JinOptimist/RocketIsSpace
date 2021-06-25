$(document).ready(function () {
    var dateMonthRegex = /\d+-\d+/;
    var dateRegex = /\d+-\d+-\d+/;

    function RegexResult(regex, data) {
        var result = regex.exec(data);
        return result[0];
    };

    function ToLocaleWithDecimals(number) {
        return parseFloat(number.toFixed(2)).toLocaleString();
    };

    $('[name="btn-accrual"]').click(function () {
        var Id = $(this).attr('id');
        var url = '/Human/GetEmloyeAccrualsInfo?id=' + Id;
        $.get(url)
            .done(function (data) {
                $('#modal-salary-count').modal('show');
                $('#hidden-id input').attr('value', data.idEmploye);
                $('#input-date input').attr('min', RegexResult(dateMonthRegex, data.inviteDate));
                $('#input-date input').attr('max', RegexResult(dateMonthRegex, data.limitDate));

                $("#noAccrualsMonths option").remove();

                $.each(data.noAccrualsDates, function (i, item) {
                    $("#noAccrualsMonths").append($("<option>").attr('value', RegexResult(dateMonthRegex, item)));
                });
            });
    });

    $('[name="btn-payment"]').click(function () {
        var Id = $(this).attr('id');
        var url = '/Human/GetEmployePaymentInfo?id=' + Id;
        $.get(url)
            .done(function (data) {
                $('#hidden-payment-id input').attr('value', data.idEmploye);
                $('#input-payment-date input').attr('value', RegexResult(dateRegex, data.date));
                $('#input-payment-not-payed input').attr('value', ToLocaleWithDecimals(data.notPayed));
                $('#modal-payment').modal('show');
                console.log(data);
            });
    });

    $('#input-date input').change(function () {
        var date = $('#input-date input').val();
        var id = $('#hidden-id input').val();
        url = `/Human/ChangeDate?date=${date}&IdEmploye=${id}`;
        $.get(url)
            .done(function (data) {
                $('#input-amount input').attr('value', ToLocaleWithDecimals(data));
            });
    });
});