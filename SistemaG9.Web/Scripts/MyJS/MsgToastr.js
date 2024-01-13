$(function () {
    var displayMessage = function (message, msgType) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        if (msgType === 'Sucesso') {
            toastr.success(message);
        }
        if (msgType === 'Info') {
            toastr.info(message);
        }
        if (msgType === 'Aguarde') {
            toastr.warning(message);
        }
        if (msgType === 'Erro') {
            toastr.error(message);
        }
    };

    if ($('#success').val()) {
        displayMessage($('#success').val(), 'Sucesso');
    }
    if ($('#info').val()) {
        displayMessage($('#info').val(), 'Info');
    }
    if ($('#warning').val()) {
        displayMessage($('#warning').val(), 'Aguarde');
    }
    if ($('#error').val()) {
        displayMessage($('#error').val(), 'Erro');
    }
});