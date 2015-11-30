/// <reference path="_references.js" />

jQuery.validator.unobtrusive.adapters.addBool("futuredate");

jQuery.validator.addMethod("futuredate", function(value, element, params) {
    if (value === null || value === "")
        return true;
    var date = new Date(value);
    var now = new Date();
    return date.getDate() >= now.getDate();
});