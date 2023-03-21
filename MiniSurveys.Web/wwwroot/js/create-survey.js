// наименования id
const buttonAddMedia = 'addMedia_';
const buttonAddAnswer = 'addAnswer_';
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
    const questionCount = $('[id^=q_]').length;
    const buttonAddMediaId = buttonAddMedia + questionCount;
    const buttonAddAnswerId = buttonAddAnswer + questionCount;
    const mediaListId = mediaList + questionCount;
    const answerListId = answerList + questionCount;
    const numberQuestionId = numberQuestion + questionCount;
    const titleQuestionId = titleQuestion + questionCount;

    var html = `<div class="create-survey__item" id="q_${questionCount}">
                    <div class="create-survey__item-header-div">
                        <div class=""></div>
                        <div class="create-survey__header">
                            <span id="${numberQuestionId}">Вопрос ${questionCount + 1}</span>
                            <button>Удалить</button>
                        </div>
                    </div>
                    <div class="create-survey__body">
                        <div class="create-survey__body-header">
                            <div class="create-survey__body-header-title">
                                <span class="create-survey__body-title3">Текст вопроса</span>
                                <input id="${titleQuestionId}" name="Questions[${questionCount}].Title" type="text" placeholder="Введите текст вопроса">
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

    return false;
});


//    /////другой вариант//////

//    //$.ajax({
//    //    type: "GET",
//    //    url: "/Admin/BlankQuestion",
//    //    cache: false,
//    //    success: function (html)
//    //    {
//    //        const questionCount = $('[id*=q_]').length + 1;
//    //        const addMediaId = 'addMedia_' + questionCount;
//    //        const mediaListId = 'mediaList_' + questionCount;
//    //        const addAnswerId = 'addAnswer_' + questionCount;
//    //        const answerListId = 'answerList_' + questionCount;

//    //        $("#questionList").append(html);
//    //        $('#_q_').attr('id', 'q_' + questionCount == null ? 1 : questionCount);
//    //        $("#_number_q_").attr('id', 'number_q_' + questionCount).text('Вопрос ' + questionCount);
//    //        $("#addMedia_").attr('id', addMediaId);
//    //        $("#mediaList_").attr('id', mediaListId);
//    //        $("#addAnswer_").attr('id', addAnswerId);
//    //        $("#answerList_").attr('id', answerListId);

//    //        $('#' + addAnswerId).click(function (even) {
//    //            $.ajax({
//    //                type: "GET",
//    //                url: "/Admin/BlankAnswer",
//    //                cache: false,
//    //                success: function (html)
//    //                {
//    //                    const id = even.target.id.replace('addAnswer_');
//    //                    $('#' + answerListId).append(html);
//    //                    const countAnswers = $('[id*=number_a_' + id + ']').length + 1;
//    //                    $('#_number_a_').text(countAnswers);
//    //                    $('#_number_a_').attr('id', 'number_a_' + id + '_' + countAnswers);
//    //                }
//    //            });
//    //            return false;
//    //        });

//    //        $('#' + addMediaId).click(function () {
//    //            $.ajax({
//    //                type: "GET",
//    //                url: "/Admin/BlankMedia",
//    //                cache: false,
//    //                success: function (html) { $('#' + mediaListId).append(html); }
//    //            });
//    //            return false;
//    //        });
//    //    }
//    //});
//    //return false;
//});

function addAnswer(id) {
    const countAnswers = $('[id*=' + id + numberAnswer + ']').length;
    const numberAnswerId = id + numberAnswer + countAnswers;
    const titleAnswerId = id + titleAnswer + countAnswers;
    const signatureAnswerId = id + signatureMediaAnswer + countAnswers;
    const typeMediaAnswerId = id + typeMediaAnswer + countAnswers;
    const linkMediaAnswerId = id + linkMediaAnswer + countAnswers;
    const isActiveAnswerId = id + isActiveAnswer + countAnswers;

    var html = `<div class="create-survey__body-body-item">
                    <div class="create-survey__body-body-item-body">
                        <div class="create-survey__body-body-item-body-answer">
                            <span id="${numberAnswerId}">${countAnswers + 1}</span>
                            <input id="${titleAnswerId}" name="Questions[${id}].Answers[${countAnswers}].Title" type="text" placeholder="Введите текст ответа">
                            <button><img src="../img/ilusha.svg" alt=""></button>
                        </div>
                        <div class="create-survey__body-body-item-body-link">
                            <input id="${signatureAnswerId}" name="Questions[${id}].Answers[${countAnswers}].Media.Signature" type="text" placeholder="Подпись мультимедия">
                            <select id="${typeMediaAnswerId}" name="Questions[${id}].Answers[${countAnswers}].Media.SelectedType.Value">
                                <option value="1">Картинка</option>
                                <option value="2">Видео</option>
                            </select>
                            <input id="${linkMediaAnswerId}" name="Questions[${id}].Answers[${countAnswers}].Media.Link" type="text" placeholder="Вставьте ссылку">
                            <div class="create-survey__body-body-item-body-checkbox">
                                <label>Использовать?</label>
                                <input id="${isActiveAnswerId}" name="Questions[${id}].Answers[${countAnswers}].IsActive" type="checkbox" value="true">
                            </div>
                        </div>
                    </div>
                </div>`;

    $('#' + answerList + id).append(html);
}

function addMedia(id) {
    const countMedias = $('[id*=' + id + numberMedia + ']').length;
    const numberMediaId = id + numberMedia + countMedias;
    const signatureMediaId = id + signatureMedia + countMedias;
    const typeMediaId = id + typeMedia + countMedias;
    const linkMediaId = id + linkMedia + countMedias;

    var html = `<div id="${numberMediaId}" class="create-survey__body-header-item">
                    <input id="${signatureMediaId}" name="Questions[${id}].Medias[${countMedias}].Signature" type="text" placeholder="Подпись мультимедия">
                    <select id="${typeMediaId}" name="Questions[${id}].Medias[${countMedias}].SelectedType.Value">
                        <option value="1">Картинка</option>
                        <option value="2">Видео</option>                    
                    </select>
                    <input id="${linkMediaId}" name="Questions[${id}].Medias[${countMedias}].Link" type="text" placeholder="Вставьте ссылку">
                    <button><img src="../img/ilusha.svg" alt=""></button>
                </div>`;

    $('#' + mediaList + id).append(html);
}

$("#addQuestion").click();