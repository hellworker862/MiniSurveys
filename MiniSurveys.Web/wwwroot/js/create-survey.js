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
const validateMedia = '_v_m_';

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
                                <input id="${titleQuestionId}" name="Questions[${questionCount}].QuestionTitle" data-val="true" data-val-required="Не указан текст вопроса" type="text" placeholder="Введите текст вопроса">
                            </div>
                            <div class="create-survey__body-header-title-validation">
                                <span class="field-validation-valid" data-valmsg-for="Questions[${questionCount}].QuestionTitle" data-valmsg-replace="true"></span>
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
    updateValidatorForm();
    

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

$('[id*=' + isActiveAnswer + ']').change(function (even) {
    const idArr = even.target.id.split(isActiveAnswer);
    changeIsActive(idArr);

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
                            <input id="${titleAnswerId}" name="Questions[${id}].Answers[${countAnswers}].AnswerTitle" data-val="true" data-val-required="Не указан текст ответа" type="text" placeholder="Введите текст ответа">
                            <button><img id="${buttonDeleteAnswerId}" src="../img/ilusha.svg" alt=""></button>
                        </div>
                        <div class="create-survey__body-body-item-body-answer-validation">
                            <span class="field-validation-valid" data-valmsg-for="Questions[${id}].Answers[${countAnswers}].AnswerTitle" data-valmsg-replace="true"></span>
                        </div>
                        <div class="create-survey__body-body-item-body-link">
                            <input id="${signatureAnswerId}" name="Questions[${id}].Answers[${countAnswers}].SignatureMedia" data-val="false" data-val-required="Не указана подпись мультимедиа" type="text" placeholder="Подпись мультимедия">
                            <select id="${typeMediaAnswerId}" name="Questions[${id}].Answers[${countAnswers}].SelectedTypeMedia.Value">
                                <option value="1">Картинка</option>
                                <option value="2">Видео</option>
                            </select>
                            <input id="${linkMediaAnswerId}" name="Questions[${id}].Answers[${countAnswers}].LinkMedia" data-val="false" data-val-required="Не указана ссылка на мультимедиа" type="text" placeholder="Вставьте ссылку">
                            <div class="create-survey__body-body-item-body-checkbox">
                                <label>Использовать?</label>
                                <input id="${isActiveAnswerId}" name="Questions[${id}].Answers[${countAnswers}].IsActive" type="checkbox" value="true">
                            </div>
                        </div>
                        <div class="create-survey__body-body-item-body-link-validation">
                            <span asp-validation-for="Questions[${id}].Answers[${countAnswers}].SignatureMedia"></span>
                            <label></label>
                            <span asp-validation-for="Questions[${id}].Answers[${countAnswers}].LinkMedia"></span>
                            <div></div>
                        </div>
                    </div>
                </div>`;

    $('#' + answerList + id).append(html);
    $('#' + buttonDeleteAnswerId).click(function (even) {
        const idArr = even.target.id.split(buttonDeleteAnswer);
        deleteAnswer(idArr);

        return false;
    });
    $('#' + isActiveAnswerId).change(function (even) {
        const idArr = even.target.id.split(isActiveAnswer);
        changeIsActive(idArr);

        return false;
    });
    updateValidatorForm();
}

function addMedia(id) {
    const countMedias = $('[id*=' + id + numberMedia + ']').length;
    const numberMediaId = id + numberMedia + countMedias;
    const signatureMediaId = id + signatureMedia + countMedias;
    const typeMediaId = id + typeMedia + countMedias;
    const linkMediaId = id + linkMedia + countMedias;
    const buttonDeleteMediaId = id + buttonDeleteMedia + countMedias;
    const validateMediaId = id + validateMedia + countMedias;

    var html = `<div id="${numberMediaId}" class="create-survey__body-header-item">
                    <input id="${signatureMediaId}" name="Questions[${id}].Medias[${countMedias}].Signature" data-val="true" data-val-required="Не указана подпись мультимедия" type="text" placeholder="Подпись мультимедия">
                    <select id="${typeMediaId}" name="Questions[${id}].Medias[${countMedias}].SelectedType.Value">
                        <option value="1">Картинка</option>
                        <option value="2">Видео</option>                    
                    </select>
                    <input id="${linkMediaId}" name="Questions[${id}].Medias[${countMedias}].Link" data-val="true" data-val-required="Укажите сcылку на мультимедия" type="text" placeholder="Вставьте ссылку">
                    <button><img id="${buttonDeleteMediaId}" src="../img/ilusha.svg" alt=""></button>
                </div>
                <div id="${validateMediaId}" class="create-survey__body-header-item-validation">
                    <span class="field-validation-valid" data-valmsg-for="Questions[${id}].Medias[${countMedias}].Signature" data-valmsg-replace="true"></span>
                    <label></label>                      
                    <span class="field-validation-valid" data-valmsg-for="Questions[${id}].Medias[${countMedias}].Link" data-valmsg-replace="true"></span>
                    <div></div>
                </div>`;

    $('#' + mediaList + id).append(html);
    $('#' + buttonDeleteMediaId).click(function (even) {
        const idArr = even.target.id.split(buttonDeleteMedia);
        deleteMedia(idArr);

        return false;
    });
    updateValidatorForm();
}

function deleteQuestion(id) {
    $('#' + question + id).remove();
    $(`input[name^="Questions[${id}].Answers["]`).remove();
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
        $(`span[data-valmsg-for="Questions[${i}].QuestionTitle"]`).attr('data-valmsg-for', `Questions[${(i - 1)}].QuestionTitle`);
        const mediaCount = $('[id^=' + i + numberMedia + ']').length;

        for (var j = 0; j < mediaCount; j++) {
            $('#' + i + numberMedia + j).attr('id', (i - 1) + numberMedia + j);
            $('#' + i + buttonDeleteMedia + j).attr('id', (i - 1) + buttonDeleteMedia + j);
            $('#' + i + signatureMedia + j).attr('id', (i - 1) + signatureMedia + j).attr('name', `Questions[${i - 1}].Medias[${j}].Signature`);
            $('#' + i + typeMedia + j).attr('id', (i - 1) + typeMedia + j).attr('name', `Questions[${i - 1}].Medias[${j}].SelectedType.Value`);
            $('#' + i + linkMedia + j).attr('id', (i - 1) + linkMedia + j).attr('name', `Questions[${i - 1}].Medias[${j}].Link`);
            $('#' + i + validateMedia + j).attr('id', (i - 1) + validateMedia + j);
            $(`span[data-valmsg-for="Questions[${i}].Medias[${j}].Signature"]`).attr('data-valmsg-for', `Questions[${i - 1}].Medias[${j}].Signature`);
            $(`span[data-valmsg-for="Questions[${i}].Medias[${j}].Link"]`).attr('data-valmsg-for', `Questions[${i - 1}].Medias[${j}].Link`);
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
            $(`span[data-valmsg-for="Questions[${i}].Answers[${j}].LinkMedia"]`).attr('data-valmsg-for', `Questions[${i - 1}].Answers[${j}].LinkMedia`);
            $(`span[data-valmsg-for="Questions[${i}].Answers[${j}].SignatureMedia"]`).attr('data-valmsg-for', `Questions[${i - 1}].Answers[${j}].SignatureMedia`);
            $(`span[data-valmsg-for="Questions[${i}].Answers[${j}].AnswerTitle"]`).attr('data-valmsg-for', `Questions[${i - 1}].Answers[${j}].AnswerTitle`);
        }
    }
}

function deleteMedia(idArr) {
    $('#' + idArr[0] + numberMedia + idArr[1]).remove();
    $('#' + idArr[0] + validateMedia + idArr[1]).remove();
    const mediaCount = $('[id^=' + idArr[0] + numberMedia + ']').length;
    const index = Number(idArr[1]) + 1;

    for (var i = index; i <= mediaCount; i++) {
        $('#' + idArr[0] + numberMedia + i).attr('id', idArr[0] + numberMedia + (i - 1));
        $('#' + idArr[0] + buttonDeleteMedia + i).attr('id', idArr[0] + buttonDeleteMedia + (i - 1));
        $('#' + idArr[0] + signatureMedia + i).attr('id', idArr[0] + signatureMedia + (i - 1)).attr('name', `Questions[${idArr[0]}].Medias[${i - 1}].Signature`);
        $('#' + idArr[0] + typeMedia + i).attr('id', idArr[0] + typeMedia + (i - 1)).attr('name', `Questions[${idArr[0]}].Medias[${i - 1}].SelectedType.Value`);
        $('#' + idArr[0] + linkMedia + i).attr('id', idArr[0] + linkMedia + (i - 1)).attr('name', `Questions[${idArr[0]}].Medias[${i - 1}].Link`);
        $('#' + idArr[0] + validateMedia + i).attr('id', idArr[0] + validateMedia + (i - 1));
        $(`span[data-valmsg-for="Questions[${idArr[0]}].Medias[${i}].Signature"]`).attr('data-valmsg-for', `Questions[${idArr[0]}].Medias[${i - 1}].Signature`);
        $(`span[data-valmsg-for="Questions[${idArr[0]}].Medias[${i}].Link"]`).attr('data-valmsg-for', `Questions[${idArr[0]}].Medias[${i - 1}].Link`);
    }
}

function deleteAnswer(idArr) {
    $('#' + idArr[0] + answer + idArr[1]).remove();
    $(`input[name="Questions[${idArr[0]}].Answers[${idArr[1]}].IsActive"]`).remove();
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
        $(`input[name="Questions[${idArr[0]}].Answers[${i}].IsActive"]`).attr('name', `Questions[${idArr[0]}].Answers[${i - 1}].IsActive`);
        $(`span[data-valmsg-for="Questions[${idArr[0]}].Answers[${i}].LinkMedia"]`).attr('data-valmsg-for', `Questions[${idArr[0]}].Answers[${i - 1}].LinkMedia`);
        $(`span[data-valmsg-for="Questions[${idArr[0]}].Answers[${i}].SignatureMedia"]`).attr('data-valmsg-for', `Questions[${idArr[0]}].Answers[${i - 1}].SignatureMedia`);
        $(`span[data-valmsg-for="Questions[${idArr[0]}].Answers[${i}].AnswerTitle"]`).attr('data-valmsg-for', `Questions[${idArr[0]}].Answers[${i - 1}].AnswerTitle`);
    }
}

function updateValidatorForm() {
    var form = $("form")
        .removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
}

function changeIsActive(idArr) {
    const changebox = $('#' + idArr[0] + isActiveAnswer + idArr[1]);

    if (changebox.is(':checked')) {
        $('#' + idArr[0] + signatureMediaAnswer + idArr[1]).attr('data-val', 'true');
        $('#' + idArr[0] + linkMediaAnswer + idArr[1]).attr('data-val', 'true');
    }
    else {
        $('#' + idArr[0] + signatureMediaAnswer + idArr[1]).attr('data-val', 'false').removeClass('input-validation-error');
        $('#' + idArr[0] + linkMediaAnswer + idArr[1]).attr('data-val', 'false').removeClass('input-validation-error');
        $(`span[data-valmsg-for="Questions[${idArr[0]}].Answers[${idArr[1]}].LinkMedia"]`).removeClass('field-validation-error').addClass('field-validation-valid').html('');
        $(`span[data-valmsg-for="Questions[${idArr[0]}].Answers[${idArr[1]}].SignatureMedia"]`).removeClass('field-validation-error').addClass('field-validation-valid').html('');
    }
    updateValidatorForm();
}