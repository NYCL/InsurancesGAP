$(function () {
    $('#Policy_CoveragePercentage').on('input', function () {
        $('#CoveragePercentage').html($(this).val() + "%");
    });
    $('.policy-validation').submit(function (e) {
        var $this = $(this);
        $this.find('#CreatePolicy').text('Enviando...').prop('disabled', true);
        e.preventDefault();
        e.stopPropagation();
        $(this).addClass('was-validated');
        if (this.checkValidity() !== false) {
            var data = {
                "Name": $('#Policy_Name').val(),
                "Description": $('#Policy_Description').val(),
                "PolicyCoverageTypes": $('#Policy_PolicyCoverageTypes').val().map(item => { return { "CoverageTypeId": item } }),
                "CoveragePercentage": Number($('#Policy_CoveragePercentage').val()),
                "StartDate": $('#Policy_StartDate').val(),
                "CoverageMonths": Number($('#Policy_CoverageMonths').val()),
                "Price": Number($('#Policy_Price').val()),
                "RiskTypeId": Number($('#Policy_RiskTypeId').val()),
                "__RequestVerificationToken": $('[name=__RequestVerificationToken]').val()
            };
            console.log(data);
            
            $.ajax({
                url: $this.attr('action'),
                method: $this.attr('method'),
                data: JSON.stringify(data),
                contentType: "application/json",
                cache: false
            })
            .done(function (response) {
                alert("success");
                console.log(response);
            })
            .fail(function (jqXHR, textStatus) {
                if ($this.data('response')) {
                    var alertbox = '<div class="alert alert-danger alert-dismissible fade show" role="alert">';

                    if (jqXHR.responseJSON.errors) {
                        for (var property in jqXHR.responseJSON.errors) {
                            alertbox = alertbox + '<strong>' + property + '</strong>' + jqXHR.responseJSON.errors[property] + '<br/>';
                        }
                    } else if (jqXHR.responseJSON) {
                        for (var property in jqXHR.responseJSON) {
                            alertbox = alertbox + '<strong>' + property + '</strong>' + jqXHR.responseJSON[property] + '<br/>';
                        }
                    }

                    alertbox = alertbox +
                            '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
                                '<span aria-hidden="true">&times;</span>' +
                            '</button>' +
                        '</div>';
                    $($this.data('response')).append(alertbox);
                }

                console.log(jqXHR);
                console.log(textStatus)
            });
            $this.find('#CreatePolicy').text('Crear').prop('disabled', false);
            console.log('send');
        }
    });
})