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