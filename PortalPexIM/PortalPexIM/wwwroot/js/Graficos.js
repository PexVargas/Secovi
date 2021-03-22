// Create the chart
Highcharts.chart('Evolutivo', {
    chart: {
        type: 'column'
    },
    title: {
       text: 'Evolutivo de ofertas'
    },
    subtitle: {
        //text: 'Click the columns to view versions. Source: <a href="http://statcounter.com" target="_blank">statcounter.com</a>'
    },
    accessibility: {
        announceNewData: {
            enabled: true
        }
    },
    xAxis: {
        type: 'category'
    },
    yAxis: {
        title: {
            //text: 'Total percent market share'
        }

    },
    legend: {
        enabled: false
    },
    plotOptions: {
        series: {
            borderWidth: 0,
            dataLabels: {
                enabled: true,
                format: '{point.y:.1f}%'
            }
        }
    },

    tooltip: {
        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
    },

    series: [
        {
            name: "Browsers",
            colorByPoint: true,
            data: [
                {
                    name: "Chrome",
                    y: 62.74,
                    drilldown: "Chrome"
                }
          
            ]
        }
    ],
    drilldown: {
        series: [
            {
                name: "Chrome",
                id: "Chrome",
                data: [
                    [
                        "v65.0",
                        0.1
                    ],
                    [
                        "v64.0",
                        1.3
                    ],
                    [
                        "v63.0",
                        53.02
                    ],
                    [
                        "v62.0",
                        1.4
                    ],
                    [
                        "v61.0",
                        0.88
                    ],
                    [
                        "v60.0",
                        0.56
                    ],
                    [
                        "v59.0",
                        0.45
                    ],
                    [
                        "v58.0",
                        0.49
                    ],
                    [
                        "v57.0",
                        0.32
                    ],
                    [
                        "v56.0",
                        0.29
                    ],
                    [
                        "v55.0",
                        0.79
                    ],
                    [
                        "v54.0",
                        0.18
                    ],
                    [
                        "v51.0",
                        0.13
                    ],
                    [
                        "v49.0",
                        2.16
                    ],
                    [
                        "v48.0",
                        0.13
                    ],
                    [
                        "v47.0",
                        0.11
                    ],
                    [
                        "v43.0",
                        0.17
                    ],
                    [
                        "v29.0",
                        0.26
                    ]
                ]
            }
        ]
    }
});

Highcharts.chart('Bairros', {
    chart: {
        type: 'bar'
    },
    title: {
        text: 'Oferta por bairros'
    },
    xAxis: {
        categories: ['Porto Alegre - Centro', 'Porto Alegre - Centro Histórico', 'Canoas - Centro']
    },
    yAxis: {
        min: 0,
        title: {
            //text: 'Total fruit consumption'
        }
    },
    legend: {
        reversed: true
    },
    plotOptions: {
        series: {
            stacking: 'normal'
        }
    },
    series: [{
        name: 'Imóveis',
        data: [5, 3, 4]
    }]
});

Highcharts.chart('Cidades', {
    chart: {
        type: 'bar'
    },
    title: {
        text: 'Oferta por cidades'
    },
    xAxis: {
        categories: ['Porto Alegre', 'São Leopoldo', 'Canoas', 'Caxias do Sul']
    },
    yAxis: {
        min: 0,
        title: {
            //text: 'Total fruit consumption'
        }
    },
    legend: {
        reversed: true
    },
    plotOptions: {
        series: {
            stacking: 'normal'
        }
    },
    series: [{
        name: 'Imóveis',
        data: [5, 3, 4, 10, 1]
    }]
});

Highcharts.chart('Tipo de Imovel', {
    chart: {
        type: 'bar'
    },
    title: {
        text: 'Tipo de Imovel'
    },
    xAxis: {
        categories: ['Porto Alegre - Centro', 'Porto Alegre - Centro Histórico', 'Canoas - Centro']
    },
    yAxis: {
        min: 0,
        title: {
            //text: 'Total fruit consumption'
        }
    },
    legend: {
        reversed: true
    },
    plotOptions: {
        series: {
            stacking: 'normal'
        }
    },
    series: [{
        name: 'Imóveis',
        data: [5, 3, 4]
    }]
});






