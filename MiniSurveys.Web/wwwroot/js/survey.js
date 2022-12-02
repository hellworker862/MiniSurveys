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