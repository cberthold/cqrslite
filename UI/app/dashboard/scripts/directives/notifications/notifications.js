'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('MainApp')
	.directive('notifications',function(){
		return {
		    templateUrl: '/app/dashboard/scripts/directives/notifications/notifications.html',
        restrict: 'E',
        replace: true,
    	}
	});


