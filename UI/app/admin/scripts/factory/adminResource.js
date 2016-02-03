'use strict';
/**
 * @ngdoc function
 * @name adminsModule.services:adminResource
 * @description
 * # adminResource
 * adminResource of the adminsModule
 */

angular.module('adminModuleProviders')
    .factory('AdminResource', ['AdminApiConfig', function (AdminApiConfig) {

        var getUrl = AdminApiConfig.getEndpointAddress('admin');
        var rebuildReadUrl = getUrl + '/rebuildReadDb';
        var rebuildSearchUrl = getUrl + '/rebuildSearchDb';

        var paramDefaults = null;
        var actions = {
            'rebuildReadDb': { method: 'GET', url: rebuildReadUrl },
            'rebuildSearchDb': { method: 'GET', url: rebuildSearchUrl }
        };

        var endpointResource = AdminApiConfig.getResource('admin', paramDefaults, actions);

        return endpointResource;
    }]);

    