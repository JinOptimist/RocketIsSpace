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
        var employeId = $(this).attr('id');
        var url = '/Human/GetEmloyeAccrualsInfo?employeId=' + employeId;
        $.get(url)
            .done(function (data) {
                $('#modal-salary-count').modal('show');
                $('#hidden-id input').attr('value', data.employeId);
                $('#input-date input').attr('min', RegexResult(dateMonthRegex, data.dateFrom));
                $('#input-date input').attr('max', RegexResult(dateMonthRegex, data.dateTo));

                $("#noAccrualsMonths option").remove();

                $.each(data.noAccrualsDates, function (i, item) {
                    $("#noAccrualsMonths").append($("<option>").attr('value', RegexResult(dateMonthRegex, item)));
                });
            });
    });

    $('[name="btn-payment"]').click(function () {
        var employeId = $(this).attr('id');
        var url = '/Human/GetEmployePaymentInfo?employeId=' + employeId;
        $.get(url)
            .done(function (data) {
                $('#hidden-payment-id input').attr('value', data.employeId);
                $('#input-payment-date input').attr('value', RegexResult(dateRegex, data.date));
                $('#input-payment-not-payed').text(ToLocaleWithDecimals(data.notPayed));
                $('#hidden-payment-account-from input').attr('value', data.departmentAccountNumber);
                $('#hidden-payment-account-to input').attr('value', data.accountNumber);

                $('#modal-payment').modal('show');
            });
    });

    $('#input-date input').change(function () {
        var date = $('#input-date input').val();
        var id = $('#hidden-id input').val();
        url = `/Human/ChangeDate?date=${date}&employeId=${id}`;
        $.get(url)
            .done(function (data) {
                $('#input-amount input').attr('value', ToLocaleWithDecimals(data));
            });
    });
});