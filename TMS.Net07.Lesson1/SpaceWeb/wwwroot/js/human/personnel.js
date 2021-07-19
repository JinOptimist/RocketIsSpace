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
        var url = '/api/HumanApi/GetEmloyeAccrualsInfo?employeId=' + employeId;
        $.get(url)
            .done(function (data) {
                $('#modal-salary-count').modal('show');
                $('#hidden-accrual-id input').attr('value', data.employeId);
                $('#input-accrual-date input').attr('min', RegexResult(dateMonthRegex, data.dateFrom));
                $('#input-accrual-date input').attr('max', RegexResult(dateMonthRegex, data.dateTo));

                $("#noAccrualsMonths option").remove();

                $.each(data.noAccrualsDates, function (i, item) {
                    $("#noAccrualsMonths").append($("<option>").attr('value', RegexResult(dateMonthRegex, item)));
                });
            });
    });

    $('[name="btn-payment"]').click(function () {
        var employeId = $(this).attr('id');
        var url = '/api/HumanApi/GetEmployePaymentInfo?employeId=' + employeId;
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

    $('#input-accrual-date input').change(function () {
        var date = $('#input-accrual-date input').val();
        var id = $('#hidden-accrual-id input').val();
        url = `/api/HumanApi/ChangeDate?date=${date}&employeId=${id}`;
        $.get(url)
            .done(function (data) {
                $('#input-accrual-amount input').attr('value', ToLocaleWithDecimals(data));
            });
    });

    function SetAjaxForSubmit(action, tag) {
        $.ajax({
            url: `/Human/${action}`,
            type: 'post',
            data: {
                EmployeId: $(`#hidden-${tag}-id input`).val(),
                Amount: $(`#input-${tag}-amount input`).val(),
                Date: $(`#input-${tag}-date input`).val()
            },
            success: function (result) {
                $(`.${tag}-message`).contents().filter(function () {
                    return (this.nodeType == 3);
                }).remove();
                $(`.${tag}-message`).append(result);
            }
        });
    }

    $('#submit-payment').click(SetAjaxForSubmit('SavePayment', 'payment'));
    $('#submit-accrual').click(SetAjaxForSubmit('SaveAccrual', 'accrual'));

});