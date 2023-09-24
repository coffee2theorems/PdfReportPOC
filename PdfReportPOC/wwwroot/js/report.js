
$(document).ready(function () {
    generateReport();
});

function generateReport() {
    getRevenueReport();
    getUserRoleReport();
}

function getRevenueReport() {
    $.ajax({
        url: '/Report?handler=RevenueReport',
        type: 'GET',
    }).done(function (dataJson) {
        initRevenueGraph(dataJson.data, dataJson.categories);
    });
}

function getUserRoleReport() {
    $.ajax({
        url: '/Report?handler=UserRolesReport',
        type: 'GET',
    }).done(function (dataJson) {
        initUserRolesPieChart(dataJson.data, dataJson.categories);
    });
}

function initUserRolesPieChart(data, categories) {
    let options = {
        series: data,
        labels: categories,
        chart: {
            type: 'donut'
        },
        plotOptions: {
            pie: {
                size: 65,
                donut: {
                    size: '55%'
                }
            }
        },
        dataLabels: {
            enabled: true,
            formatter: function (val) {
                
                return val.toFixed(2) + "%"
            }
        }
    }

    let userDonutElements = document.getElementsByClassName('UserRolePieChar');
    for(let element of userDonutElements){
        let revenueGraph = new ApexCharts(element, options);

        setTimeout(function () {
            revenueGraph.render();
        }, 200);
    }
}

function initRevenueGraph(data, categories) {
    let options = {
        series: [{
            name: 'data',
            data: data
        }],
        labels: categories,
        chart: {
            type: 'bar',
            height: 350,
            toolbar: {
                show: false
            }
        }, plotOptions: {
            bar: {
                borderRadius: 8,
                distributed: false,
                barHeight: 50,
                dataLabels: {
                    position: 'top',
                }
            }
        },
        dataLabels: {
            enabled: true,
            textAnchor: 'middle',
            offsetY: -25,
            // formatter: function (val) {
            //     val = Math.floor(val)
            //     var Format = wNumb({
            //         prefix: '$',
            //         thousand: ','
            //     })
            //     return Format.to(val);
            // },
            style: {
                fontSize: '12px',
                fontWeight: '600',
                align: 'center',
                colors: ['#000']
            },
        },
        legend: {
            show: false
        },
        xaxis: {
            categories: categories,
            labels: {
                style: {
                    fontSize: '12px',
                    align: 'left'
                }
            },
            axisBorder: {
                show: true
            }
        },
        yaxis: {
            labels: {
            //     formatter: function (val) {
            //         var Format = wNumb({
            //             prefix: '$',
            //             thousand: ','
            //         })
            //         return Format.to(val);
            //     },
                style: {
                    fontSize: '12px',
                },
                offsetY: 0,
                align: 'left'
            }
        }
        // tooltip: {
        //     style: {
        //         fontSize: '12px'
        //     },
        //     // y: {
        //     //     formatter: function (val) {
        //     //         var Format = wNumb({
        //     //             prefix: '$',
        //     //             thousand: ','
        //     //         })
        //     //         return Format.to(val);
        //     //     },
        //     // }
        // }
    };
    let revenueGraphElements = document.getElementsByClassName('RevenueByProductGraph');
     for(let element of revenueGraphElements){
        let revenueGraph = new ApexCharts(element, options);

        setTimeout(function () {
            revenueGraph.render();
        }, 200);
    }
}

function getPDF(){
    //const { jsPDF } = window.jspdf;
    var HTML_Width = $(".canvas_div_pdf").width();
    var HTML_Height = $(".canvas_div_pdf").height();
    var top_left_margin = 15;
    var PDF_Width = HTML_Width+(top_left_margin*2);
    var PDF_Height = (PDF_Width*1.5)+(top_left_margin*2);
    var canvas_image_width = HTML_Width;
    var canvas_image_height = HTML_Height;

    var totalPDFPages = Math.ceil(HTML_Height/PDF_Height)-1;


    html2canvas($(".canvas_div_pdf")[0],{allowTaint:true}).then(function(canvas) {
        canvas.getContext('2d');

        console.log(canvas.height+"  "+canvas.width);


        var imgData = canvas.toDataURL("image/jpeg", 1.0);
        var pdf = new jsPDF('p', 'pt',  [PDF_Width, PDF_Height]);
        pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin,canvas_image_width,canvas_image_height);


        for (var i = 1; i <= totalPDFPages; i++) {
            pdf.addPage(PDF_Width, PDF_Height);
            pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height*i)+(top_left_margin*4),canvas_image_width,canvas_image_height);
        }

        pdf.save("HTML-Document.pdf");
    });
};
