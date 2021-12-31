$(document).ready(function () {

    $('#customerInvoiceTableBody tr').on("click","td", function(){
        $('#customerInvoiceTableBody').find('.table-active').removeClass('table-active');
        $(this).parent("tr").addClass("table-active");
        var inv_num = $(this).parent("tr").find("td:first").text();
        getInvoiceData(inv_num);
    });


    $('#prod_typeFilter').on("change", function(){
        prodFilter.prod_type = this.value;
        getProductData(prodFilter);
    });

    $('#prod_baseFilter').on("change", function(){
        prodFilter.prod_base = this.value;
        getProductData(prodFilter);

    });

    $('#prod_categoryFilter').on("change", function(){
        prodFilter.prod_category = this.value;
        getProductData(prodFilter);

    });


    collapseTR();

    
});

$(document).ajaxComplete(function () {
    $('td:first-child').on("click", function(){
        console.log($(this).text());
        $(this).parent().find('td:not(:eq(0))').toggle();
    })
})

$(window).on('resize', function(){
    getProductData(prodFilter);
});


var prodFilter = {
    prod_type: 'All',
    prod_base: 'All',
    prod_category: 'All'
}


function getInvoiceData(inv_num) {
    $.ajax({
        url: '/api/invoices/' + inv_num,
        type: 'GET',
        dataType: "json",
        success: function (res) {
            buildInvoiceDataTable(JSON.parse(JSON.stringify(res)));
        }
    })
}

function buildInvoiceDataTable(ajaxRes){
    $("#lineItemTBody tr").remove();
    var qtySum = 0;
    var priceSum = 0;
    for(var i = 0; i < ajaxRes.length; i++){
        
        var newRow = '<tr>' +
        '<td>' + ajaxRes[i].line_num + '</td>' +
        '<td>' + ajaxRes[i].prod_sku + '</td>' +
        '<td>' + ajaxRes[i].line_qty + '</td>' +
        '<td>' + ajaxRes[i].line_price + '</td>' +
        '<td>' + (ajaxRes[i].line_qty * ajaxRes[i].line_price).toFixed(2) +
        '</td></tr>';
        qtySum += ajaxRes[i].line_qty;
        priceSum += ajaxRes[i].line_price * ajaxRes[i].line_qty;
        $('#lineItemTBody').append(newRow);
    }
    var lastRow = '<tr> ' +
    '<td><b> Totals: </b> </td>' +
    '<td></td>' +
    '<td><b>' + qtySum + '</b></td>' +
    '<td></td>' +
    '<td><b>' + priceSum.toFixed(2); + '</b></td>' +
    '</tr>';
    $('#lineItemTBody').append(lastRow);
}

function getProductData(prodFilter){
    var urlString = "/api/products?"
    var keys = Object.keys(prodFilter);
    for(var i = 0; i < keys.length; i++){
        var key = keys[i];
        urlString += key + "=" + prodFilter[key] + "&";
    }
    urlString = urlString.slice(0,-1);
    urlString = encodeURI(urlString);

    $.ajax({
        url: urlString,
        type: 'GET',
        dataType: 'json',
        success: function(res){
            buildProductDataTable(res);
            collapseTR();
        }
    })

}

function buildProductDataTable(ajaxRes){
    $('#productTableBody tr').remove();
    for(var i=0; i < ajaxRes.length; i++){
        var newRow = '<tr><td data-label="Product SKU: ">' +
        ajaxRes[i].prod_sku + '&nbsp <i class="fas fa-caret-down"></i></td><td data-label="Description: ">' +
        ajaxRes[i].prod_descript + '</td><td data-label="Type: ">' +
        ajaxRes[i].prod_type + '</td><td data-label="Base: ">' +
        ajaxRes[i].prod_base + '</td><td data-label="Category: ">' +
        ajaxRes[i].prod_category + '</td><td data-label="Price: ">' +
        ajaxRes[i].prod_price + '</td><td data-label="QTY on Hand: ">' +
        ajaxRes[i].prod_qoh + '</td></tr>' +
        $('#productTableBody').append(newRow);
    }
}

function collapseTR() {
    let windowSize = $(window).width();
    if(windowSize <= 480) {
        $("tr").find('td:not(:eq(0))').toggle();
    }
}