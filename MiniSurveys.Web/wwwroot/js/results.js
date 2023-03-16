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

    function CreateChart() {
        const id = document.querySelector(".result__fillter").id;
        const fillter = $("#combobox").val();
        console.log(fillter);

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
                                    size: 18,
                                    weight: 'bold',
                                }
                            },
                            title: {
                                display: true,
                                text: 'Количество опрошенных',
                                padding: {
                                    top: 30,
                                    bottom: 0
                                },
                                position: 'top',
                                align: 'start',
                                font: {
                                    size: 20,
                                    weight: 'bold',
                                }
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
            maxBarThickness: 100,
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
                    size: 20,
                    weight: 'bold',
                }
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
                    size: 16,
                    weight: 'bold',
                }
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