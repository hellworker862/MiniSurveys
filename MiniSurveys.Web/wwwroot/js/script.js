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
            $(document).ready(function ($) {
                $("#user-number").mask("+7(999) 999-9999");
            });
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
            $(document).ready(function ($) {
                $("#user-number").mask("+7(999) 999-9999");
            });
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
            $(document).ready(function ($) {
                    $("#user-number").mask("+7(999) 999-9999");
            });
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
var pieChart;
var countBarCharts = 0;
var barCharts = [];


$(document).ready(function () {
    const myChart1 = document.getElementById('myChart1');
    CreateChart();

    $(document).on("change", "select", function () {
        $("option[value=" + this.value + "]", this)
            .attr("selected", true).siblings()
            .removeAttr("selected")
    });

    $('#button-apply').on('click', function () {
        CreateChart();
    });

    //$('#button-export').on('click', function () {
    //    ExportToExcel();
    //});

    //function ExportToExcel() {
    //    const id = document.querySelector(".result__fillter").id;
    //    const fillter = $("#combobox").val();

    //    $.ajax({
    //        type: "GET",
    //        url: "/Survey/ExportToExcel",
    //        traditional: true,
    //        data: {
    //            id: id,
    //            fillter: fillter
    //        },
    //        success: function () {
    //        }
    //    });
    //}

    function CreateChart() {
        const id = document.querySelector(".result__fillter").id;
        const fillter = $("#combobox").val();

        if (myChart1) {
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
                    const dataPie = {
                        labels: model.surveyedUsers.map(x => x.title),
                        datasets: [{
                            label: " Количество",
                            data: model.surveyedUsers.map(x => x.value),
                            backgroundColor: [
                                'rgb(237, 28, 36)',
                                'rgb(247, 158, 164)',
                            ],
                            hoverOffset: 20,
                            borderColor: '#E9ECEE',
                            borderWidth: 5,
                            hoverBorderColor: "#E9ECEE",
                        },]
                    };

                    const optionsPie = {
                        plugins: {
                            datalabels: {
                                formatter: (value, ctx) => {
                                    let sum = 0;
                                    let dataArr = ctx.chart.data.datasets[0].data;
                                    dataArr.map(data => {
                                        sum += data;
                                    });
                                    let percentage = (value * 100 / sum).toFixed(1) + "%";
                                    return percentage == "0.0%" ? "" : percentage;
                                },
                                color: '#fff',
                                font: {
                                    family: 'Montserrat',
                                    size: 18,
                                    weight: 'bold',
                                }
                            },
                            title: {
                                display: false,
                            },
                            legend: {
                                position: 'left',
                            }
                        },
                    };

                    const ctx1 = document.getElementById('myChart1');

                    if(pieChart != null) {
                        pieChart.destroy();
                    }

                    pieChart = new Chart(ctx1, {
                        type: 'pie',
                        data: dataPie,
                        plugins: [ChartDataLabels],
                        options: optionsPie,
                    });

                    var barChartsArr = document.querySelectorAll('.barChart-item');
                    var isEmpty = barChartsArr.length == 0;

                    if (isEmpty) {
                        console.log(model.questionResultDatas)
                        for (var i = 0; i < model.questionResultDatas.length; i++) {
                            countBarCharts++;
                            var canv = document.createElement('canvas');
                            canv.id = 'barChart' + countBarCharts;
                            canv.classList += 'barChart-item';
                            document.getElementById('barCharts').appendChild(canv);
                        }

                        barCharts = new Array(countBarCharts);
                        barChartsArr = document.querySelectorAll('.barChart-item');
                    }

                    var currentQuestion = 0;
                    barChartsArr.forEach((item) => {
                        console.log('arr', barCharts);
                        barCharts[currentQuestion] = renderChartBar(item, model.questionResultDatas[currentQuestion], barCharts[currentQuestion], currentQuestion);
                        currentQuestion++;
                    });
                }
            });
        }
    }
});

function renderChartBar(ctx, data, barGraph, number) {
    const dataBar = {
        labels: data.answers.map(x => x.title),
        datasets: [{
            label: " Количество",
            data: data.answers.map(x => x.value),
            backgroundColor: [
                '#EC1C24',
            ],
        },]
    };

    const options = {
        plugins: {
            legend: {
                display: false,
                labels: {
                    color: 'rgb(255, 99, 132)'
                }
            },
            title: {
                display: true,
                text: 'Вопрос ' + ++number + '/' + countBarCharts,
                padding: {
                    top: 30,
                    bottom: 5
                },
                position: 'top',
                align: 'start',
                font: {
                    family: 'Montserrat',
                    size: 20,
                    weight: 'bold',
                },
                color: '#0F2232',
            },
            subtitle: {
                display: true,
                text: data.title,
                padding: {
                    top: 0,
                    bottom: 20
                },
                position: 'top',
                align: 'start',
                font: {
                    family: 'Montserrat',
                    size: 16,
                    weight: 'bold',
                },
                color: '#0F2232',
            },
        },
        scales: {
            y: {
                ticks: {
                    stepSize: 1
                }
            }   
        }
    };

    if (barGraph != null) {
        barGraph.destroy();
    }

    barGraph = new Chart(ctx, {
        type: 'bar',
        data: dataBar,
        options: options,
    });

    return barGraph;
}
// наименования id
const question = 'q_';
const answer = '_a_';
const buttonAddMedia = 'addMedia_';
const buttonAddAnswer = 'addAnswer_';
const buttonDeleteMedia = '_deleteMedia_';
const buttonDeleteAnswer = '_deleteAnswer_';
const buttonDeleteQuestion = 'deleteQuestion_';
const mediaList = 'mediaList_';
const answerList = 'answerList_';
const numberQuestion = 'number_q_';
const numberAnswer = '_number_a_';
const numberMedia = '_number_m_';
const signatureMediaAnswer = '_signature_a_m_';
const signatureMedia = '_signature_m_';
const titleAnswer = '_title_a_';
const titleQuestion = 'title_q_';
const typeMediaAnswer = '_type_a_m_';
const typeMedia = '_type_m_';
const linkMediaAnswer = '_link_a_m_';
const linkMedia = '_link_m_';
const isActiveAnswer = '_active_a_';

$("#addQuestion").click(function () {
    const questionCount = $('[id^=' + question + ']').length;
    const questionId = question + questionCount;
    const buttonAddMediaId = buttonAddMedia + questionCount;
    const buttonAddAnswerId = buttonAddAnswer + questionCount;
    const mediaListId = mediaList + questionCount;
    const answerListId = answerList + questionCount;
    const numberQuestionId = numberQuestion + questionCount;
    const titleQuestionId = titleQuestion + questionCount;
    const buttonDeleteQuestionId = buttonDeleteQuestion + questionCount;

    var html = `<div class="create-survey__item" id="${questionId}">
                    <div class="create-survey__item-header-div">
                        <div class=""></div>
                        <div class="create-survey__header">
                            <span id="${numberQuestionId}">Вопрос ${questionCount + 1}</span>
                            <button id="${buttonDeleteQuestionId}">Удалить</button>
                        </div>
                    </div>
                    <div class="create-survey__body">
                        <div class="create-survey__body-header">
                            <div class="create-survey__body-header-title">
                                <span class="create-survey__body-title3">Текст вопроса</span>
                                <input id="${titleQuestionId}" name="Questions[${questionCount}].QuestionTitle" type="text" placeholder="Введите текст вопроса">
                            </div>
                            <div class="create-survey__body-header-download" id="addMedia">
                                <button id="${buttonAddMediaId}">Добавить мультимедия</button>
                            </div>
                            <div class="create-survey__body-header-list" id="${mediaListId}">

                            </div>
                        </div>
                        <div class="create-survey__body-body">
                            <div class="create-survey__body-header-title">
                                <span class="create-survey__body-title3">Варианты ответов</span>
                            </div>
                            <div class="create-survey__body-header-download create-survey__body-header-add-answer">
                                <button id="${buttonAddAnswerId}">Добавить ответ</button>
                            </div>
                            <div class="create-survey__body-header-item-title">
                                <span>№</span>
                                <span>Текст варианта ответа</span>
                            </div>
                            <div class="create-survey__body-body-list" id="${answerListId}">

                            </div>
                        </div>
                    </div>
                </div>`;

    $("#questionList").append(html);
    addAnswer(questionCount);

    $('#' + buttonAddAnswerId).click(function (even) {
        const id = even.target.id.replace(buttonAddAnswer, '');
        addAnswer(id)

        return false;
    });
    $('#' + buttonAddMediaId).click(function (even) {
        const id = even.target.id.replace(buttonAddMedia, '');
        addMedia(id)

        return false;
    });
    $('#' + buttonDeleteQuestionId).click(function (even) {
        const id = even.target.id.replace(buttonDeleteQuestion, '');
        deleteQuestion(id);

        return false;
    });

    return false;
});

$('[id*=' + buttonDeleteQuestion + ']').click(function (even) {
    const id = even.target.id.replace(buttonDeleteQuestion, '');
    deleteQuestion(id);

    return false;
});
$('[id*=' + buttonAddAnswer + ']').click(function (even) {
    const id = even.target.id.replace(buttonAddAnswer, '');
    addAnswer(id);

    return false;
});
$('[id*=' + buttonAddMedia + ']').click(function (even) {
    const id = even.target.id.replace(buttonAddMedia, '');
    addMedia(id);

    return false;
});
$('[id*=' + buttonDeleteMedia + ']').click(function (even) {
    const idArr = even.target.id.split(buttonDeleteMedia);
    deleteMedia(idArr);

    return false;
});
$('[id*=' + buttonDeleteAnswer + ']').click(function (even) {
    const idArr = even.target.id.split(buttonDeleteAnswer);
    deleteAnswer(idArr);

    return false;
});

function addAnswer(id) {
    const countAnswers = $('[id*=' + id + numberAnswer + ']').length;
    const answerId = id + answer + countAnswers;
    const numberAnswerId = id + numberAnswer + countAnswers;
    const buttonDeleteAnswerId = id + buttonDeleteAnswer + countAnswers;
    const titleAnswerId = id + titleAnswer + countAnswers;
    const signatureAnswerId = id + signatureMediaAnswer + countAnswers;
    const typeMediaAnswerId = id + typeMediaAnswer + countAnswers;
    const linkMediaAnswerId = id + linkMediaAnswer + countAnswers;
    const isActiveAnswerId = id + isActiveAnswer + countAnswers;

    var html = `<div id="${answerId}" class="create-survey__body-body-item">
                    <div class="create-survey__body-body-item-body">
                        <div class="create-survey__body-body-item-body-answer">
                            <span id="${numberAnswerId}">${countAnswers + 1}</span>
                            <input id="${titleAnswerId}" name="Questions[${id}].Answers[${countAnswers}].AnswerTitle" type="text" placeholder="Введите текст ответа">
                            <span class="field-validation-valid" data-valmsg-for="Questions[${id}].Answers[${countAnswers}].AnswerTitle" data-valmsg-replace="true"></span>
                            <button><img id="${buttonDeleteAnswerId}" src="../img/ilusha.svg" alt=""></button>
                        </div>
                        <div class="create-survey__body-body-item-body-link">
                            <input id="${signatureAnswerId}" name="Questions[${id}].Answers[${countAnswers}].SignatureMedia" type="text" placeholder="Подпись мультимедия">
                            <select id="${typeMediaAnswerId}" name="Questions[${id}].Answers[${countAnswers}].SelectedTypeMedia.Value">
                                <option value="1">Картинка</option>
                                <option value="2">Видео</option>
                            </select>
                            <input id="${linkMediaAnswerId}" name="Questions[${id}].Answers[${countAnswers}].LinkMedia" type="text" placeholder="Вставьте ссылку">
                            <div class="create-survey__body-body-item-body-checkbox">
                                <label>Использовать?</label>
                                <input id="${isActiveAnswerId}" name="Questions[${id}].Answers[${countAnswers}].IsActive" type="checkbox" value="true">
                            </div>
                        </div>
                    </div>
                </div>`;

    $('#' + answerList + id).append(html);
    $('#' + buttonDeleteAnswerId).click(function (even) {
        const idArr = even.target.id.split(buttonDeleteAnswer);
        deleteAnswer(idArr);

        return false;
    });
}

function addMedia(id) {
    const countMedias = $('[id*=' + id + numberMedia + ']').length;
    const numberMediaId = id + numberMedia + countMedias;
    const signatureMediaId = id + signatureMedia + countMedias;
    const typeMediaId = id + typeMedia + countMedias;
    const linkMediaId = id + linkMedia + countMedias;
    const buttonDeleteMediaId = id + buttonDeleteMedia + countMedias;

    var html = `<div id="${numberMediaId}" class="create-survey__body-header-item">
                    <input id="${signatureMediaId}" name="Questions[${id}].Medias[${countMedias}].Signature" type="text" placeholder="Подпись мультимедия">
                    <select id="${typeMediaId}" name="Questions[${id}].Medias[${countMedias}].SelectedType.Value">
                        <option value="1">Картинка</option>
                        <option value="2">Видео</option>                    
                    </select>
                    <input id="${linkMediaId}" name="Questions[${id}].Medias[${countMedias}].Link" type="text" placeholder="Вставьте ссылку">
                    <button><img id="${buttonDeleteMediaId}" src="../img/ilusha.svg" alt=""></button>
                </div>`;

    $('#' + mediaList + id).append(html);
    $('#' + buttonDeleteMediaId).click(function (even) {
        const idArr = even.target.id.split(buttonDeleteMedia);
        deleteMedia(idArr);

        return false;
    });
}

function deleteQuestion(id) {
    $('#' + question + id).remove();
    const questionCount = $('[id^=' + question + ']').length;

    for (var i = Number(id) + 1; i <= questionCount; i++) {
        $('#' + question + i).attr('id', question + (i - 1));
        $('#' + numberQuestion + i).attr('id', numberQuestion + (i - 1)).text('Вопрос ' + i);
        $('#' + buttonDeleteQuestion + i).attr('id', buttonDeleteQuestion + (i - 1));
        $('#' + buttonAddMedia + i).attr('id', buttonAddMedia + (i - 1));
        $('#' + buttonAddAnswer + i).attr('id', buttonAddAnswer + (i - 1));
        $('#' + titleQuestion + i).attr('id', titleQuestion + (i - 1)).attr('name', `Questions[${(i - 1)}].QuestionTitle`);
        $('#' + mediaList + i).attr('id', mediaList + (i - 1));
        $('#' + answerList + i).attr('id', answerList + (i - 1));
        const mediaCount = $('[id^=' + i + numberMedia + ']').length;

        for (var j = 0; j < mediaCount; j++) {
            $('#' + i + numberMedia + j).attr('id', (i - 1) + numberMedia + j);
            $('#' + i + buttonDeleteMedia + j).attr('id', (i - 1) + buttonDeleteMedia + j);
            $('#' + i + signatureMedia + j).attr('id', (i - 1) + signatureMedia + j).attr('name', `Questions[${i - 1}].Medias[${j}].Signature`);
            $('#' + i + typeMedia + j).attr('id', (i - 1) + typeMedia + j).attr('name', `Questions[${i - 1}].Medias[${j}].SelectedType.Value`);
            $('#' + i + linkMedia + j).attr('id', (i - 1) + linkMedia + j).attr('name', `Questions[${i - 1}].Medias[${j}].Link`);
        }
        const answerCount = $('[id^=' + i + numberAnswer + ']').length;

        for (var j = 0; j < answerCount; j++) {
            $('#' + i + answer + j).attr('id', (i - 1) + answer + j);
            $('#' + i + numberAnswer + j).attr('id', (i - 1) + numberAnswer + j);
            $('#' + i + buttonDeleteAnswer + j).attr('id', (i - 1) + buttonDeleteAnswer + j);
            $('#' + i + titleAnswer + j).attr('id', (i - 1) + titleAnswer + j).attr('name', `Questions[${i - 1}].Answers[${j}].AnswerTitle`);
            $('#' + i + signatureMediaAnswer + j).attr('id', (i - 1) + signatureMediaAnswer + j).attr('name', `Questions[${i - 1}].Answers[${j}].SignatureMedia`);
            $('#' + i + typeMediaAnswer + j).attr('id', (i - 1) + typeMediaAnswer + j).attr('name', `Questions[${i - 1}].Answers[${j}].SelectedTypeMedia.Value`);
            $('#' + i + linkMediaAnswer + j).attr('id', (i - 1) + linkMediaAnswer + j).attr('name', `Questions[${i - 1}].Answers[${j}].LinkMedia`);
            $('#' + i + isActiveAnswer + j).attr('id', (i - 1) + isActiveAnswer + j).attr('name', `Questions[${i - 1}].Answers[${j}].IsActive`);
        }
    }
}

function deleteMedia(idArr) {
    $('#' + idArr[0] + numberMedia + idArr[1]).remove();
    const mediaCount = $('[id^=' + idArr[0] + numberMedia + ']').length;
    const index = Number(idArr[1]) + 1;

    for (var i = index; i <= mediaCount; i++) {
        $('#' + idArr[0] + numberMedia + i).attr('id', idArr[0] + numberMedia + (i - 1));
        $('#' + idArr[0] + buttonDeleteMedia + i).attr('id', idArr[0] + buttonDeleteMedia + (i - 1));
        $('#' + idArr[0] + signatureMedia + i).attr('id', idArr[0] + signatureMedia + (i - 1)).attr('name', `Questions[${idArr[0]}].Medias[${i - 1}].Signature`);
        $('#' + idArr[0] + typeMedia + i).attr('id', idArr[0] + typeMedia + (i - 1)).attr('name', `Questions[${idArr[0]}].Medias[${i - 1}].SelectedType.Value`);
        $('#' + idArr[0] + linkMedia + i).attr('id', idArr[0] + linkMedia + (i - 1)).attr('name', `Questions[${idArr[0]}].Medias[${i - 1}].Link`);
    }
}

function deleteAnswer(idArr) {
    $('#' + idArr[0] + answer + idArr[1]).remove();
    const answerCount = $('[id^=' + idArr[0] + numberAnswer + ']').length;
    const index = Number(idArr[1]) + 1;

    for (var i = index; i <= answerCount; i++) {
        $('#' + idArr[0] + answer + i).attr('id', idArr[0] + answer + (i - 1));
        $('#' + idArr[0] + numberAnswer + i).attr('id', idArr[0] + numberAnswer + (i - 1)).text(i);
        $('#' + idArr[0] + buttonDeleteAnswer + i).attr('id', (idArr[0]) + buttonDeleteAnswer + (i - 1));
        $('#' + idArr[0] + titleAnswer + i).attr('id', (idArr[0]) + titleAnswer + (i - 1)).attr('name', `Questions[${idArr[0]}].Answers[${i - 1}].AnswerTitle`);
        $('#' + idArr[0] + signatureMediaAnswer + i).attr('id', (idArr[0]) + signatureMediaAnswer + (i - 1)).attr('name', `Questions[${idArr[0]}].Answers[${i - 1}].SignatureMedia`);
        $('#' + idArr[0] + typeMediaAnswer + i).attr('id', (idArr[0]) + typeMediaAnswer + (i - 1)).attr('name', `Questions[${idArr[0]}].Answers[${i - 1}].SelectedTypeMedia.Value`);
        $('#' + idArr[0] + linkMediaAnswer + i).attr('id', (idArr[0]) + linkMediaAnswer + (i - 1)).attr('name', `Questions[${idArr[0]}].Answers[${i - 1}].LinkMedia`);
        $('#' + idArr[0] + isActiveAnswer + i).attr('id', (idArr[0]) + isActiveAnswer + (i - 1)).attr('name', `Questions[${idArr[0]}].Answers[${i - 1}].IsActive`);
    }
}