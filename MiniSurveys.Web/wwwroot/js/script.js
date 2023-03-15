var slideIndex = 1;

$('.dot').on('click', function () {
    const number = this.id.replace("picture_", '');
    currentSlide(number);
});

$('.prev').on('click', function () {
    plusSlides(-1);
});

$('.next').on('click', function () {
    plusSlides(1);
});

const slides = document.getElementsByClassName("mySlides");


function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}
const surveyList = document.querySelector('#survey-list');

$('input[name=isActive]').change(function () {
    $.ajax({
        type: "GET",
        url: "/Survey/GetSurveyListPartial",
        data: {
            isActive: $('input[name=isActive]:checked').val(),
            stringSearch: $('#search').val(),
        },
        success: function (html) {
            surveyList.innerHTML = html;
        }
    });
});

$('#search').bind('input', function () {
    $.ajax({
        type: "GET",
        url: "/Survey/GetSurveyListPartial",
        data: {
            isActive: $('input[name=isActive]:checked').val(),
            stringSearch: $('#search').val(),
        },
        success: function (html) {
            console.log(html);
            surveyList.innerHTML = html;
        }
    });
});

$(document).ready(function () {

    $('#button_rigth').on('click', function () {
        goToQuestion(true);
    });

    $('#button_left').on('click', function () {
        goToQuestion(false);
    });

    $('#button_save').on('click', function () {
        Save();
    });

    function Save() {
        const answers = Array.from(document.querySelectorAll('.radio-button__input')).map(x => x.checked);

        $.ajax({
            type: "GET",
            url: "/Survey/Save",
            traditional: true,
            data: {
                answers: answers,
            },
            success: function (html) {
                window.location.replace(html);
            }
        });
        event.preventDefault();
    }

    function goToQuestion(is_next) {

        const answers = Array.from(document.querySelectorAll('.radio-button__input')).map(x => x.checked);
        console.log(answers);

        $.ajax({
            type: "GET",
            url: "/Survey/GetQuestion",
            traditional: true,
            data: {
                isNext: is_next,
                answers: answers,
            },
            success: function (html) {
                $('.question').html(html);

                $('#button_rigth').on('click', function () {
                    goToQuestion(true);
                });
                $('#button_left').on('click', function () {
                    goToQuestion(false);
                });

                $('#button_end').on('click', function () {
                    End();
                });

                $('.dot').on('click', function () {
                    const number = this.id.replace("picture_", '');
                    currentSlide(number);
                });

                $('.prev').on('click', function () {
                    plusSlides(-1);
                });

                $('.next').on('click', function () {
                    plusSlides(1);
                });

                $('#button_save').on('click', function () {
                    Save();
                });
            }
        });
        event.preventDefault();
    }

    function End() {
        $.ajax({
            type: "POST",
            url: "/Survey/Survey",
            traditional: true,
            data: {
                test: 'dsad',
            },
            success: function (html) {
                $('.question').html(html);
            }
        });
    }

});
const lockPadding = document.querySelectorAll(".lock-padding");
const body = document.querySelector('body');

let unlock = true;
const timeout = 800;

function popupOpen(curentPopup) {
    if (curentPopup && unlock) {
        const popupActive = document.querySelector('.popup.open-popup');
        if (popupActive) {
            popupClose(popupActive, false);
        } else {
            bodyLock();
        }
        curentPopup.addClass('open-popup');
        curentPopup.bind("click", function (e) {
            if (!e.target.closest('.popup-content')) {
                popupClose(e.target.closest('.popup'));
            }
        })
    }
}

function popupClose(doUnlock = true) {
    if (unlock) {
        const popupActive = document.querySelector('.popup.open-popup');
        popupActive.classList.remove('open-popup');
        if (doUnlock) {
            bodyUnLock();
        }
    }
}

function bodyLock() {
    const lockPaddingValue = window.innerWidth - document.querySelector('.wrapper').offsetWidth + 'px';
    if (lockPadding.length > 0) {
        for (let index = 0; index < lockPadding.length; index++) {
            const el = lockPadding[index];
            el.style.paddingRight = lockPaddingValue;
        }
    }
    body.style.paddingRight = lockPaddingValue;
    body.classList.add('lock');

    unlock = false;
    setTimeout(function () {
        unlock = true;
    }, timeout);
}

function bodyUnLock() {
    setTimeout(function () {
        if (lockPadding.length > 0) {
            for (let index = 0; index < lockPadding.length; index++) {
                const el = lockPadding[index];
                el.style.paddingRight = '0px';
            }
        }
        body.style.paddingRight = '0px';
        body.classList.remove('lock');
    }, timeout);

    unlock = false;
    setTimeout(function () {
        unlock = true;
    }, timeout);
}

document.addEventListener('keydown', function (e) {
    if (e.which === 27) {
        const popupActive = document.querySelector('.popup.open-popup');
        popupClose(popupActive);
    }
});


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

    //prevent default form submit event
    return false;
};
$(document).ready(function () {
    const myChart = document.getElementById('myChart');
    const id = document.querySelector(".result__fillter").id;
    const fillter = document.querySelector(".result__fillter").value;

    if (myChart) {
        $.ajax({
            type: "GET",
            url: "/Survey/GetResult",
            traditional: true,
            data: {
                id: id,
                fillter: fillter
            },
            success: function (model) {
                console.log(model)
                console.log(model.questions[0].answers);
                const data = {
                    labels: model.title,
                    datasets: [{
                        label: "Количество",
                        data: model.questions[0].answers,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(255, 159, 64, 0.2)',
                            'rgba(255, 205, 86, 0.2)',

                        ],
                        borderColor: [
                            'rgb(255, 99, 132)',
                            'rgb(255, 159, 64)',
                            'rgb(255, 205, 86)',
                        ],
                        borderWidth: 1
                    }]
                };

                const ctx = document.getElementById('myChart');

                new Chart(ctx, {
                    type: 'bar',
                    data: data,
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    },
                });
            }
        });
    }

    window.addEventListener('beforeprint', () => {
        myChart.resize(200, 200);
    });
});