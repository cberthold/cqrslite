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

    this.getEndpointAddress = function (endpointName) {

        var uri = provider.apiUri;

        if (uri.match("/" + "$") != "/") {
            uri += "/";
        }

        var endpointAddress = uri + endpointName + "/:id";

        return endpointAddress;

    };


    return {
        setApiUri: provider.setApiUri,

        $get: ['$resource', function ($resource) {
            return {
                getResource: function (resourceName, paramDefaults, actions) {

                    var urlPattern = provider.getEndpointAddress(resourceName);

                    return $resource(urlPattern, paramDefaults, actions);
                }
            };
        }]



    };

});

