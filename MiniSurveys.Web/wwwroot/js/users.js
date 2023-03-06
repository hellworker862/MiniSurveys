

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
        }
    })
};

jQueryAjaxPost = form => {
    const t = new FormData(form);
    console.log(t.get("Name"))
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
            else
                $('.popup-body .popup-content').html(res.html);
        },
        error: function (err) {
            console.log(err)
        }
    })

    return false;
};

jQueryAjaxPostCreate = form => {
    const t = new FormData(form);
    console.log(t.get("Name"))
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
            else
                $('.popup-body .popup-content').html(res.html);
        },
        error: function (err) {
            console.log(err)
        }
    })

    return false;
};

jQueryAjaxDelete = form => {
    if (confirm('Do you want to delete?')) {
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

    //prevent default form submit event
    return false;
};