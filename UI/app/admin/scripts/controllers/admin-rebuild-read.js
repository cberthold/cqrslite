'use strict';
/**
 * @ngdoc function
 * @name adminModule.controller:CustomerAddController
 * @description
 * # CustomerAddController
 * Controller of the adminModule
 */
angular.module('adminModule')
  .controller('AdminRebuildReadController', ['$scope', 'AdminResource', 'rfc4122', '$state', function ($scope, AdminResource, rfc4122, $state) {
      var vm = this;

      vm.rebuild = function () {

          //vm.data.Id = rfc4122.v4();

          var query = AdminResource.rebuildReadDb().$promise
            .then(function (result) {
                console.log(result);
                alert('ok');
            },
            // on failure...
            function (errorMsg) {
                alert('wa wa wa');
                console.log('Something went wrong: ' + errorMsg);
            });
      }
  }]);