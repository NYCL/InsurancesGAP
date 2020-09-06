$(function () {
    // Range display
    $('[name="Policy.CoveragePercentage"]').on('input', function () {
        console.log('range set');
        $(this).siblings('.CoveragePercentage').html($(this).val() + "%");
    });

    // Submit via ajax using api (Create, Edit)
    $('.policy-validation').submit(function (e) {
        var $this = $(this);
        var text = $this.find('[type=submit]').text();
        $this.find('[type=submit]').text('Enviando...').prop('disabled', true);
        e.preventDefault();
        e.stopPropagation();
        $this.addClass('was-validated');
        if (this.checkValidity() !== false) {
            // Model Arrange
            var data = {
                "ID": $this.find('#Policy_ID').val(),
                "Name": $this.find('#Policy_Name').val(),
                "Description": $this.find('#Policy_Description').val(),
                "PolicyCoverageTypes": $this.find('#Policy_PolicyCoverageTypes').val().map(item => { return { "CoverageTypeId": item, "PolicyId": $this.find('#Policy_ID').val() } }),
                "CoveragePercentage": Number($this.find('#Policy_CoveragePercentage').val()),
                "StartDate": $this.find('#Policy_StartDate').val(),
                "CoverageMonths": Number($this.find('#Policy_CoverageMonths').val()),
                "Price": Number($this.find('#Policy_Price').val()),
                "RiskTypeId": Number($this.find('#Policy_RiskTypeId').val()),
                "__RequestVerificationToken": $this.find('[name=__RequestVerificationToken]').val()
            };
            console.log(data);

            $.ajax({
                url: $this.attr('action') + '/' + $this.data('id')??'',
                method: $this.attr('method'),
                data: JSON.stringify(data),
                contentType: "application/json",
                cache: false
            }).done(function (response) {
                $this.trigger('reset');
                $this.removeClass('was-validated');
                $this.closest('.modal').modal('hide');
                $('#list-tab').trigger('shown.bs.tab');
                console.log(response);
            }).fail(function (jqXHR, textStatus) {
                if ($this.data('response')) {
                    var alertbox = '<div class="alert alert-danger alert-dismissible fade show" role="alert">';
                    console.log(jqXHR);

                    if (jqXHR.responseJSON !== undefined) {
                        if (jqXHR.responseJSON.errors !== undefined) {
                            for (var property in jqXHR.responseJSON.errors) {
                                alertbox = alertbox + '<strong>' + property + '</strong> ' + jqXHR.responseJSON.errors[property] + '<br/>';
                            }
                        } else {
                            for (var property in jqXHR.responseJSON) {
                                alertbox = alertbox + '<strong>' + property + '</strong> ' + jqXHR.responseJSON[property] + '<br/>';
                            }
                        }
                    } else if (jqXHR.responseText) {
                        alertbox = alertbox + '<strong>Error: </strong> ' + jqXHR.responseText + '<br/>';
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
            console.log('send');
        }
        $this.find('[type=submit]').text(text).prop('disabled', false);
    });

    // Submit via ajax using api (Delete)
    $('.delete-form').submit(function (e) {
        var $this = $(this);
        var text = $this.find('[type=submit]').text();
        $this.find('[type=submit]').text('Enviando...').prop('disabled', true);
        e.preventDefault();
        e.stopPropagation();

        $.ajax({
            url: $this.attr('action') + '/' + $this.data('id'),
            method: $this.attr('method'),
            contentType: "application/json",
            cache: false
        }).done(function (response) {
            $this.closest('.modal').modal('hide');
            $('#list-tab').trigger('shown.bs.tab');
            console.log(response);
        }).fail(function (jqXHR, textStatus) {
            if ($this.data('response')) {
                var alertbox = '<div class="alert alert-danger alert-dismissible fade show" role="alert">';
                console.log(jqXHR);

                if (jqXHR.responseJSON !== undefined) {
                    if (jqXHR.responseJSON.errors !== undefined) {
                        for (var property in jqXHR.responseJSON.errors) {
                            alertbox = alertbox + '<strong>' + property + '</strong> ' + jqXHR.responseJSON.errors[property] + '<br/>';
                        }
                    } else {
                        for (var property in jqXHR.responseJSON) {
                            alertbox = alertbox + '<strong>' + property + '</strong> ' + jqXHR.responseJSON[property] + '<br/>';
                        }
                    }
                } else if (jqXHR.responseText) {
                    alertbox = alertbox + '<strong>Error: </strong> ' + jqXHR.responseText + '<br/>';
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
        console.log('send');
        $this.find('[type=submit]').text(text).prop('disabled', false);
    });

    var jqxhr = null;
    $('#list-tab').on('shown.bs.tab', function (e) {
        if (jqxhr === null) {
            jqxhr = $.ajax({
                url: '/api/Policies',
                method: 'GET',
                contentType: "application/json",
                cache: false
            }).done(function (response) {
                var table = $('#policies-table').find('tbody');
                table.empty();
                for (var i = 0; i < response.length; i++) {
                    var row = $('<tr data-id="' + response[i].id + '">');
                    row.append('<td>' + response[i].name + '</td>');
                    row.append('<td>' + response[i].policyCoverageTypes.map(item => item.coverageType.name) + '</td>');
                    row.append('<td>' + response[i].coveragePercentage + '%</td>');
                    row.append('<td>' + response[i].coverageMonths + '</td>');
                    row.append('<td>' + response[i].riskType.name + '</td>');
                    row.append('<td>' + (response[i].customer ? response[i].customer.name : '-') + '</td>');
                    row.append('<td>' +
                        '<button type="button" asp-action="Edit" data-id="' + response[i].id + '" class="btn btn-outline-primary btn-sm" data-toggle="modal" data-target="#editModal">Editar</button>&nbsp;' +
                        '<button type="button" asp-action="Delete" data-id="' + response[i].id + '" class="btn btn-outline-danger btn-sm" data-toggle="modal" data-target="#deleteModal">Eliminar</button>' +
                        '</td>');
                    table.append(row);
                }
            }).fail(function (jqXHR, textStatus) {
                var alertbox = '<div class="alert alert-danger alert-dismissible fade show" role="alert">';
                alertbox = alertbox + '<strong>Error (' + jqXHR.status + '): </strong>' + textStatus + '<br/>';
                alertbox = alertbox +
                    '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
                    '<span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>';
                $('#list').prepend(alertbox);
                console.log(jqXHR);
                console.log(textStatus)
            }).always(function () {
                jqxhr = null;
                $('#actions').prop('disabled', true);
            });
        }
    });

    $('#policies-table tbody').on('click', 'tr', function () {
        $(this).toggleClass('table-active');
        var selected = $('#policies-table tbody').find('.table-active').length;
        if (selected > 0) {
            $('#actions').prop('disabled', false);
        } else {
            $('#actions').prop('disabled', true);
        }
    });

    $('#asignModal').on('show.bs.modal', function () {
        var selected = $('#policies-table tbody').find('.table-active').length;
        $('.selections').text(selected);
    });

    $('#Customers').change(function () {
        if ($(this).val()) {
            $('#assign').prop('disabled', false);
        } else {
            $('#assign').prop('disabled', true);
        }
    });

    $('#cancel').click(function () {
        $('#policies-table tbody').find('.table-active').each(function () {
            $.ajax({
                url: '/api/Policies/' + $(this).data('id') + '/',
                method: 'PATCH',
                contentType: "application/json",
                cache: false
            }).done(function (response) {
                $('#list-tab').trigger('shown.bs.tab');
                $('#asignModal').modal('hide');
            });
        });
    });

    $('#assign').click(function () {
        $('#policies-table tbody').find('.table-active').each(function () {
            $.ajax({
                url: '/api/Policies/' + $(this).data('id') + '/' + $('#Customers').val(),
                method: 'PATCH',
                contentType: "application/json",
                cache: false
            }).done(function (response) {
                $('#list-tab').trigger('shown.bs.tab');
                $('#asignModal').modal('hide');
            });
        });
    });

    $('#editModal').on('show.bs.modal', function (event) {
        var id = $(event.relatedTarget).data('id');
        var modal = $(this);
        modal.find('form').data('id', id);
        if (jqxhr === null) {
            jqxhr = $.ajax({
                url: '/api/Policies/' + id,
                method: 'GET',
                contentType: "application/json",
                cache: false
            }).done(function (response) {
                modal.find('#Policy_ID').val(response.id);
                modal.find('#Policy_Name').val(response.name);
                modal.find('#Policy_Description').val(response.description);
                $.each(response.policyCoverageTypes, function (i, e) {
                    console.log("option[value='" + e.coverageTypeId + "']");
                    modal.find('#Policy_PolicyCoverageTypes').find("option[value='" + e.coverageTypeId + "']").prop("selected", true);
                });
                modal.find('#Policy_CoveragePercentage').val(response.coveragePercentage).trigger('input');
                var startDate = new Date(response.startDate);
                console.log(startDate);
                var day = ("0" + startDate.getDate()).slice(-2);
                var month = ("0" + (startDate.getMonth() + 1)).slice(-2);
                var date = startDate.getFullYear() + "-" + (month) + "-" + (day);
                modal.find('#Policy_StartDate').val(date);
                modal.find('#Policy_CoverageMonths').val(response.coverageMonths);
                modal.find('#Policy_Price').val(response.price);
                modal.find('#Policy_RiskTypeId').val(response.riskTypeId);
                console.log(response);
            }).fail(function (jqXHR, textStatus) {
                var alertbox = '<div class="alert alert-danger alert-dismissible fade show" role="alert">';
                alertbox = alertbox + '<strong>Error (' + jqXHR.status + '): </strong>' + textStatus + '<br/>';
                alertbox = alertbox +
                    '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
                    '<span aria-hidden="true">&times;</span>' +
                    '</button>' +
                    '</div>';
                modal.find('.modal-body').prepend(alertbox);
                console.log(jqXHR);
                console.log(textStatus);
            }).always(function () {
                jqxhr = null;
            });
        }
    });

    $('#deleteModal').on('show.bs.modal', function (event) {
        var id = $(event.relatedTarget).data('id');
        var modal = $(this);
        modal.find('form').data('id', id);
        modal.find('#Policy_ID').val(id);
    });

    $('#editModal').on('hidden.bs.modal', function (event) {
        $(this).find('form').removeClass('was-validated').trigger('reset');
    });
})