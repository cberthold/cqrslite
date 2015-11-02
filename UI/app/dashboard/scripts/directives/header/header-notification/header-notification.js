'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('MainApp')
	.directive('headerNotification',function(){
		return {
		    templateUrl: '/app/dashboard/scripts/directives/header/header-notification/header-notification.html',
        restrict: 'E',
        replace: true,
    	}
	});


