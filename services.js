if (!datalus.services.addresses) {
    datalus.services.addresses = {};
}

datalus.services.addresses.createNewAddress = function (formData, onSuccess, onError) {
    var url = "/api/Addresses/";
    var settings = {
        cache: false
        , contentType: "application/json; charset=UTF-8"
         , data: JSON.stringify(formData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };
    $.ajax(url, settings);
}

datalus.services.addresses.updateAddress = function (valueId, formData, onSuccess, onError) {
    var url = "/api/Addresses/" + valueId;
    var settings = {
        cache: false
        , contentType: "application/json; charset=UTF-8"
         , data: JSON.stringify(formData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };
    $.ajax(url, settings);
}

datalus.services.addresses.getAddressById = function (IdValue, onSuccess, onError) {
    var url = "/api/addresses/" + IdValue;
    var settings = {
        cache: false
        , contentType: "application/json; charset=UTF-8"  
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, settings);
}

datalus.services.addresses.onDeleteAddress = function (IdValue, onSuccess, onError) {
    var url = "/api/addresses/" + IdValue;
    var settings = {
        cache: false
        , contentType: "application/json; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };
    $.ajax(url, settings);
}

datalus.services.addresses.getAllAddresses = function (onSuccess, onError) {
    var url = "/api/Addresses/";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, settings);
}

datalus.services.addresses.getCountries = function (onSuccess, onError) {
    var url = "/api/addresses/countries";
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, settings);
}

datalus.services.addresses.getStatesForCountry = function (countryId, onSuccess, onError) {
    var url = "/api/addresses/StateProvinces/country/" + countryId;
    var settings = {
        cache: false
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };
    $.ajax(url, settings);
}
