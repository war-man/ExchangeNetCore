// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(document).ready(function () {

});

var connection = new signalR.HubConnectionBuilder().withUrl(signalrUrl + "/OrderBookHub", {
    skipNegotiation: true,
    transport: signalR.HttpTransportType.WebSockets
}).build();

function submitOrder() {
    let orderData = {
        type: parseInt($("input:radio[name ='type']:checked").val()),
        price: parseFloat($("#price").val()),
        amount: parseFloat($("#amount").val())
    };
    $.ajax({
        type: "POST",
        url: apiUrl + "/Order",
        data: JSON.stringify(orderData),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            console.log(res);

            connection.invoke("LoadOrderBook").catch(function (err) {
                return console.error(err.toString());
            });
        }
    });
}

connection.on("ReceiveOrderBook", function (orderBook) {
    console.log(orderBook);
    let data = JSON.parse(orderBook);
    console.log(data);
    $('#orderbook-sell').DataTable({
        destroy: true,
        searching: false,
        paging: false,
        info: false,
        data: data.askOrders,
        columns: [
            { data: 'price' },
            { data: 'amount' }, 
            { data: 'total' }
        ]
    });
    $('#orderbook-buy').DataTable({
        destroy: true,
        searching: false,
        paging: false,
        info: false,
        data: data.bidOrders,
        columns: [
            { data: 'price' },
            { data: 'amount' },
            { data: 'total' }
        ]
    });
});

connection.start().then(function () {
    connection.invoke("LoadOrderBook").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});
