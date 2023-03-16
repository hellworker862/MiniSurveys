var pieChart;


$(document).ready(function () {
    const myChart = document.getElementById('myChart');
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
                    const data = {
                        labels: [model.allUsers.title, model.testedUsers.title],
                        datasets: [{
                            label: " Количество",
                            data: [model.allUsers.value, model.testedUsers.value],
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

                    const options = {
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

                    const ctx = document.getElementById('myChart');

                    if(pieChart != null) {
                        pieChart.destroy();
                    }

                    pieChart = new Chart(ctx, {
                        type: 'pie',
                        data: data,
                        plugins: [ChartDataLabels],
                        options: options,
                    });
                }
            });
        }
    }
});