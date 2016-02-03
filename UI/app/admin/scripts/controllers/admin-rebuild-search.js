'use strict';
/**
 * @ngdoc function
 * @name adminModule.controller:CustomerAddController
 * @description
 * # CustomerAddController
 * Controller of the adminModule
 */
angular.module('adminModule')
  .controller('AdminRebuildSearchController', ['$scope', 'AdminResource', 'rfc4122', '$state', '$stateParams', function ($scope, AdminResource, rfc4122, $state, $stateParams) {
      var vm = this;

      vm.rebuild = function () {

          //vm.data.Id = rfc4122.v4();

          var query = AdminResource.rebuildSearchDb().$promise
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