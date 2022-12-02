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

$(document).ready(function(){

$('#button_rigth').on('click', function () {
    goToQuestion(true);
});

$('#button_left').on('click', function () {
    goToQuestion(false);
});

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
        }
    });
    event.preventDefault();
}
});