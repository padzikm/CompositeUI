///<reference path="~/Scripts/_references.js"/>
///<reference path="../../../../Web/Scripts/PubSubCache/networkingEvents.js"/>

var productsChangedSubscriber = function (topic, data) {

    if (data == null || data.length === 0) {
        $("#OrdersService_total").text("Total sum: 0");
        return;
    }

    var params = "";
    for (var i = 0, len = data.length; i < len; ++i) {
        params += "productIds=" + data[i];
        if (i < len - 1)
            params += "&";
    }
    $.ajax({
        method: "GET",
        url: "/OrdersService/PubSubCache/GetTotal",
        data: params,
        success: function (data) {
            var total = data.total;
            $("#OrdersService_total").text("Total sum: " + total);
        }
    });
};

var subscription = $.pubsub('subscribe', productsChanged.name, productsChangedSubscriber);
