var slideIndex = 1;

const dotLinks = document.querySelectorAll('.dot');
const prevLinks = document.querySelectorAll('.prev');
const nextLinks = document.querySelectorAll('.next');

if(dotLinks.length > 0) {
    for(let index = 0; index < dotLinks.length; index++) {
        const dotLink = dotLinks[index];
        dotLink.addEventListener("click", function(e) {
            const number = this.id.replace("picture",'');
            currentSlide(number);
        });
    }
}

if(nextLinks.length > 0) {
    for(let index = 0; index < nextLinks.length; index++) {
        const nextLink = nextLinks[index];
        nextLink.addEventListener("click", function(e) {
            plusSlides(1);
        });
    }
}

if(prevLinks.length > 0) {
    for(let index = 0; index < prevLinks.length; index++) {
        const prevLink = prevLinks[index];
        prevLink.addEventListener("click", function(e) {
            plusSlides(-1);
        });
    }
}

const slides = document.getElementsByClassName("mySlides");

//if (slides) {
//    showSlides(slideIndex);
//}

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
