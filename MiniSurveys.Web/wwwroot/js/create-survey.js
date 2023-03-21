$("#addQuestion").click(function () {
    $.ajax({
        type: "GET",
        url: "/Admin/BlankQuestion",
        cache: false,
        success: function (html)
        {
            const questionCount = $('[id*=q_]').length + 1;
            const addMediaId = 'addMedia_' + questionCount;
            const mediaListId = 'mediaList_' + questionCount;
            const addAnswerId = 'addAnswer_' + questionCount;
            const answerListId = 'answerList_' + questionCount;

            $("#questionList").append(html);
            $('#_q_').attr('id', 'q_' + questionCount == null ? 1 : questionCount);
            $("#_number_q_").attr('id', 'number_q_' + questionCount).text('Вопрос ' + questionCount);
            $("#addMedia_").attr('id', addMediaId);
            $("#mediaList_").attr('id', mediaListId);
            $("#addAnswer_").attr('id', addAnswerId);
            $("#answerList_").attr('id', answerListId);

            $('#' + addAnswerId).click(function (even) {
                $.ajax({
                    type: "GET",
                    url: "/Admin/BlankAnswer",
                    cache: false,
                    success: function (html)
                    {
                        const id = even.target.id.replace('addAnswer_');
                        $('#' + answerListId).append(html);
                        const countAnswers = $('[id*=number_a_' + id + ']').length + 1;
                        $('#_number_a_').text(countAnswers);
                        $('#_number_a_').attr('id', 'number_a_' + id + '_' + countAnswers);
                    }
                });
                return false;
            });

            $('#' + addMediaId).click(function () {
                $.ajax({
                    type: "GET",
                    url: "/Admin/BlankMedia",
                    cache: false,
                    success: function (html) { $('#' + mediaListId).append(html); }
                });
                return false;
            });
        }
    });
    return false;
});



$("#addQuestion").click();
