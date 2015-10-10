'use strict';
/**
 * @ngdoc function
 * @name customersModule.controller:CustomerAddController
 * @description
 * # CustomerAddController
 * Controller of the customersModule
 */
angular.module('customersModule')
  .controller('CustomerAddController', function ($scope) {
      var vm = this;
      vm.name = "input name";
      vm.address1 = "";
      vm.address2 = "";
      vm.city = "";
      vm.state = "";
      vm.zipcode = "";


      $scope.reset = function()
      {
          $scope.$setPristine();
          vm.name = "reset customer";
      }
});