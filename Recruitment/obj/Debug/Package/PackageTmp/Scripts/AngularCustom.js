// Defining angularjs module
var app = angular.module('recruitmentModule', []);

app.directive('fileUpload', function () {
    return {
        scope: true,        //create a new scope
        link: function (scope, el, attrs) {
            el.bind('change', function (event) {
                var files = event.target.files;
                //iterate files since 'multiple' may be specified on the element
                for (var i = 0; i < files.length; i++) {
                    //emit event upward
                    scope.$emit("fileSelected", { file: files[i] });
                }
            });
        }
    };
});

// Defining angularjs Controller and injecting CandidateService
app.controller('recruitmentCtrl', function ($scope, $http, CandidateService) {

    $scope.candidateData = null;
    // Fetching records from the factory created at the bottom of the script file
    CandidateService.GetAllRecords().then(function (d) {
        $scope.candidateData = d.data; // Success
    }, function () {
        alert('Error Occured !!!'); // Failed
    });

    // Add item
    $scope.add = function (data) {
        $scope.Candidate = {
            id: 0,
            name: '',
            sur_name: '',
            position: '',
            curriculum: ''
        };
    }

    //an array of files selected
    $scope.files = [];

    //listen for the file selected event
    $scope.$on("fileSelected", function (event, args) {
        $scope.$apply(function () {
            //add the file object to the scope's files collection
            $scope.files.push(args.file);
        });
    });

    // Reset data
    $scope.clear = function () {
        //$scope.Candidate.id = 0;
        //$scope.Candidate.name = '';
        //$scope.Candidate.sur_name = '';
        //$scope.Candidate.position = '';
        //$scope.Candidate.curriculum = '';
        $scope.Candidate = null;
        angular.forEach(
            angular.element("input[type='file']"),
            function (inputElem) {
                angular.element(inputElem).val(null);
        });
    }

    //Add New Item
    $scope.save = function () {
        if ($scope.Candidate.name != "" &&
       $scope.Candidate.sur_name != "" && $scope.Candidate.position != "" && $scope.files.length > 0) {
            // call Http request using $http
            $http({
                method: 'POST',
                url: '/api/Candidate/Add/',
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("model", angular.toJson(data.model));
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                },
                data: { model: $scope.Candidate, files: $scope.files }
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.candidateData.push(response.data.ExceptionMessage);
                $scope.clear();
                alert("Candidate Added Successfully !!!");
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Edit item
    $scope.edit = function (data) {
        $scope.Candidate = { id: data.id, name: data.name, sur_name: data.sur_name, position: data.position, curriculum: data.curriculum };
    }

    // Cancel form
    $scope.cancel = function () {
        $scope.clear();
    }

    // Update item
    $scope.update = function () {
        if ($scope.Candidate.name != "" &&
       $scope.Candidate.sur_name != "" && $scope.Candidate.position != "") {
            $http({
                method: 'POST',
                url: '/api/Candidate/Edit/',
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("model", angular.toJson(data.model));
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                },
                data: { model: $scope.Candidate, files: $scope.files }
            }).then(function successCallback(response) {
                $scope.candidateData = response.data;
                $scope.clear();
                alert("Candidate Updated Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };

    // Delete item
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: '/api/Candidate/Delete/' + $scope.candidateData[index].id,
        }).then(function successCallback(response) {
            $scope.candidateData.splice(index, 1);
            alert("Candidate Deleted Successfully !!!");
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
    };

});

// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.

app.factory('CandidateService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('/api/Candidate/GetAll');
    }
    return fac;
});
