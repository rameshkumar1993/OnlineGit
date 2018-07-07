

function notifySuccess(msg, title)
{
    type = ['', 'info', 'success', 'warning', 'danger', 'rose', 'primary'];

    color = Math.floor((Math.random() * 6) + 1);

    $.notify({
        message: title + ' ' + msg

    }, {
        type: 'success',
        timer: 5000,
        placement: {
            from: 'top',
            align: 'right'
        }
    });
}

function notifyError(msg, title) {
    type = ['', 'info', 'success', 'warning', 'danger', 'rose', 'primary'];

    color = Math.floor((Math.random() * 6) + 1);

    $.notify({
        message: title + ' ' + msg

    }, {
        type: 'danger',
        timer: 5000,
        placement: {
            from: 'top',
            align: 'right'
        }
    });
}






