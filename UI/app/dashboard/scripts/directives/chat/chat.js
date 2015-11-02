'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */
angular.module('MainApp')
	.directive('chat',function(){
		return {
		    templateUrl: '/app/dashboard/scripts/directives/chat/chat.html',
        restrict: 'E',
        replace: true,
    	}
	});


