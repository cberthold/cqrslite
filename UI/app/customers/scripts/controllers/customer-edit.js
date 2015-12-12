'use strict';
/**
 * @ngdoc function
 * @name customersModule.controller:CustomerAddController
 * @description
 * # CustomerAddController
 * Controller of the customersModule
 */
angular.module('customersModule')
  .controller('CustomerEditController', ['$scope', 'CustomerResource', 'rfc4122', '$state', '$stateParams', function ($scope, CustomerResource, rfc4122, $state, $stateParams) {
      var vm = this;

      var data = {};
      vm.data = data;

      var availableActions = [
          { description: 'Edit', action: 'edit' },
          { description: 'Delete', action: 'delete' }
      ];
      
      var original = {};

      CustomerResource.get({ id: $stateParams.id }, function (list) {
          original = angular.copy(vm.data);
          vm.data = list;
      });

      vm.getAvailableActions = function (item) {
          return availableActions;
      };

      vm.actionSelected = function (item, actionItem) {

          var action = actionItem.action;

          switch(action)
          {
              case 'edit':
                  vm.editCustomer(item);
                  break;
              case 'delete':
                  vm.deleteCustomer(item);
                  break;
              default:
                  alert('unknown action: ' + action);
                  break;
          }
     };

      

      vm.reset = function () {
          vm.data = angular.copy(original);
          vm.form1.$setPristine();
      }

      vm.update = function () {
          var queryParms = { id: vm.data.Id };
          var queryData = vm.data;
          var query = CustomerResource.update(queryParms, queryData).$promise
            .then(function (result) {
                console.log(result);
                $state.transitionTo('customers.list');
            },
            // on failure...
            function (errorMsg) {
                console.log('Something went wrong: ' + errorMsg);
            });
      }

  }]);