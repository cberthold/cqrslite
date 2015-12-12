'use strict';
/**
 * @ngdoc function
 * @name customersModule.services:customerResource
 * @description
 * # customerResource
 * customerResource of the customersModule
 */
angular.module('customersModuleProviders')

.provider('CustomerApiConfig', function () {


    var provider = this;

    this.apiUri = "http://localhost/api/";

    this.setApiUri = function (uri) {
        provider.apiUri = uri;
    };

    this.getEndpointUri = function () {
        var uri = provider.apiUri;

        if (uri.match("/" + "$") != "/") {
            uri += "/";
        }

        return uri;
    };

    this.getEndpointAddressNameOnlyUri = function (endpointName) {
        var uri = this.getEndpointUri();
        var endpointAddress = uri + endpointName;

        return endpointAddress;
    };

    this.getEndpointAddress = function (endpointName) {

        var uri = this.getEndpointAddressNameOnlyUri(endpointName);
        var endpointAddress = uri + "/:id";

        return endpointAddress;

    };


    return {
        setApiUri: provider.setApiUri,

        $get: ['$resource', function ($resource) {
            return {
                getResource: function (resourceName, paramDefaults, actions) {

                    var urlPattern = provider.getEndpointAddress(resourceName);

                    return $resource(urlPattern, paramDefaults, actions);
                },
                getEndpointUri: provider.getEndpointUri,
                getEndpointAddressNameOnlyUri: provider.getEndpointAddressNameOnlyUri,
                getEndpointAddress: provider.getEndpointAddress
            };
        }]



    };

});

