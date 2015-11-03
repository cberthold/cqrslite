'use strict';
/**
 * @ngdoc function
 * @name customersModule.services:customerResource
 * @description
 * # customerResource
 * customerResource of the customersModule
 */

angular.module('customersModuleProviders')
    .factory('CustomerResource', ['CustomerApiConfig', function (CustomerApiConfig) {

        var endpointResource = CustomerApiConfig.getResource('customer');


        return endpointResource;
    }]);

    