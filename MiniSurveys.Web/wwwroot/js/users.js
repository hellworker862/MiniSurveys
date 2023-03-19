const usersList = document.querySelector('#users-id');

$('#search-users').bind('input', function () {
    $.ajax({
        type: "GET",
        url: "/Admin/GetUsersBySearchString",
        data: {
            searchString: $('#search-users').val(),
        },
        success: function (html) {
            console.log(html);
            usersList.innerHTML = html;
        }
    });
});

showInPopup = (url) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('.popup-body .popup-content').html(res);
            popupOpen($('.popup'));
            $("#user-number").mask("+7(999) 999-9999");
        }
    })
};

jQueryAjaxPost = form => {
    $.ajax({
        type: 'POST',
        url: "/Admin/UserEditPartial/",
        data: new FormData(form),
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.isValid) {
                $('#users-id').html(res.html)
                $('.popup-body .popup-content').html('');
                popupClose();
            }
        },
        error: function (err) {
            $('.popup-body .popup-content').html(err.responseText);
            $("#user-number").mask("+7(999) 999-9999");
        }
    })

    return false;
};

jQueryAjaxPostCreate = form => {
    $.ajax({
        type: 'POST',
        url: "/Admin/UserCreatePartial/",
        data: new FormData(form),
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.isValid) {
                $('#users-id').html(res.html)
                $('.popup-body .popup-content').html('');
                popupClose();
            }
        },
        error: function (err) {
            $('.popup-body .popup-content').html(err.responseText);
            $("#user-number").mask("+7(999) 999-9999");
        }
    })

    return false;
};

jQueryAjaxDelete = form => {
    if (confirm('Вы действительно хотите удалить?')) {
        try {
            $.ajax({
                type: 'POST',
                url: "/Admin/UserDelete/",
                data: { userName: form },
                success: function (res) {
                    $('#users-id').html(res.html)
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    return false;
};