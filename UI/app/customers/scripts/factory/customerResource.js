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

        var getUrl = CustomerApiConfig.getEndpointAddress('customer');
        var listUrl = CustomerApiConfig.getEndpointAddressNameOnlyUri('customer');
        var activateUrl = getUrl+'/activate';

        var paramDefaults = null;
        var actions = {
            'list': { method: 'GET', isArray: true, url: listUrl },
            'get': { method: 'GET', url: getUrl },
            'update': { method: 'PUT', url: getUrl },
            'deactivate': { method: 'DELETE', url: getUrl },
            'activate': { method: 'GET', url: activateUrl }
        };

        var endpointResource = CustomerApiConfig.getResource('customer', paramDefaults, actions);

        return endpointResource;
    }]);

    