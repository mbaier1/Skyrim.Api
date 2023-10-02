using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Skyrim.Api.Controllers;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using System.Media;
using System.Net;

namespace Skyrim.Api.Test.Controllers
{
    public class LocationController_Tests
    {
        internal readonly Mock<ILocationDomain> _mockDomain;
        internal readonly LocationsController _locationsController;

        public LocationController_Tests()
        {
            _mockDomain = new Mock<ILocationDomain>();
            _locationsController = new LocationsController(_mockDomain.Object);
        }
    }

    public class GetLocations : LocationController_Tests
    {
        [Fact]
        public async void WithDataInDatabase_ReturnsOkStatusAndLocationsArray()
        {
            // Arrange
            List<Location> locations = new List<Location>
            {
                new City
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.City
                },
                new Town
                {
                    Id = 2,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Town
                },
                new Settlement
                {
                    Id = 3,
                    Name = "Test",
                    Description = "Test",
                    GeographicalDescription = "Test",
                    LocationId = LocationType.Settlement
                }
            };

            var completedTask = Task<Location>.FromResult(locations);
            _mockDomain.Setup(x => x.GetLocation())
                .ReturnsAsync((List<Location>)completedTask.Result);
            var okActionStatusCode = (int)HttpStatusCode.OK;

            // Act
            var result = await _locationsController.GetLocation();
            var responseAsOkActionResult = (OkObjectResult)result.Result;
            var resultValue = responseAsOkActionResult.Value;
            List<object> resultLocations = new List<object>()
            {
                resultValue
            };

            // Assert
            Assert.Equal(okActionStatusCode, responseAsOkActionResult.StatusCode);
            resultLocations[0].Should().Be(locations);
        }

        [Fact]
        public async void WithNoDataInDatabaseOrIfErrorHappens_ReturnsNotFoundAndNull()
        {
            // Arrange
            List<Location> locations = (List<Location>)null;

            var completedTask = Task<Location>.FromResult(locations);

            _mockDomain.Setup(x => x.GetLocation())
                .ReturnsAsync((IEnumerable<Location>)completedTask.Result);

            // Act
            var result = await _locationsController.GetLocation();

            var responseAsNotFoundActionResult = (NotFoundResult)result.Value;

            // Assert
            Assert.Equal("Microsoft.AspNetCore.Mvc.NotFoundResult", result.Result.ToString());
            Assert.Null(responseAsNotFoundActionResult);
        }
    }

    public class GetLocation : LocationController_Tests
    {
        [Fact]
        public async void WithValidId_ReturnsOkStatusCodeAndExpectedLocation()
        {
            // Arrange

            int id = 1;

            var city = new City
            {
                Id = 1,
                Name = "Test",
                LocationId = LocationType.City,
                GeographicalDescription = "Test"
            };

            var completedTask = Task<Location>.FromResult(city);

            _mockDomain.Setup(x => x.GetLocation(It.IsAny<int>()))
                .ReturnsAsync((Location)completedTask.Result);

            var okActionStatusCode = (int)HttpStatusCode.OK;
            var cityObject = new Object();
            var locationAsCity = new City();

            // Act

            var result = await _locationsController.GetLocation(id);
            var responseAsOkActionResult = (OkObjectResult)result.Result;
            cityObject = responseAsOkActionResult.Value;

            locationAsCity.Id = (int)cityObject.GetType().GetProperty("Id").GetValue(cityObject, null);
            locationAsCity.Name = (string)cityObject.GetType().GetProperty("Name").GetValue(cityObject, null);
            locationAsCity.LocationId = (LocationType)cityObject.GetType().GetProperty("LocationId").GetValue(cityObject, null);
            locationAsCity.GeographicalDescription = (string)cityObject.GetType().GetProperty("GeographicalDescription").GetValue(cityObject, null);

            // Assert

            Assert.Equal(okActionStatusCode, responseAsOkActionResult.StatusCode);
            Assert.Equal(city.Id, locationAsCity.Id);
            Assert.Equal(city.Name, locationAsCity.Name);
            Assert.Equal(city.LocationId, locationAsCity.LocationId);
            Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
        }

        [Fact]
        public async void WithInvalidId_ReturnsExpectedNotFoundActionResultAndNull()
        {
            // Arrange
            int id = 1;

            City city = null;

            var completedTask = Task<Location>.FromResult(city);

            _mockDomain.Setup(x => x.GetLocation(It.IsAny<int>()))
                .ReturnsAsync((Location)completedTask.Result);

            // Act
            var result = await _locationsController.GetLocation(id);

            // Assert
            Assert.Equal("Microsoft.AspNetCore.Mvc.NotFoundResult", result.Result.ToString());
            Assert.Null(result.Value);
        }
    }

    public class UpdateLocation : LocationController_Tests
    {
        [Fact]
        public async void WithValidIdAndValidChangedData_ReturnsUpdatedLocationAndOk()
        {
            // Arrange
            int id = 1;
            var locationDto = new LocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test"
            };

            var city = new City
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test"
            };

            var completedTask = Task<Location>.FromResult(city);
            var locationAsCity = new City();
            var createdAtActionStatusCode = (int)HttpStatusCode.Created;
            _mockDomain.Setup(x => x.UpdateLocation(It.IsAny<int>(), It.IsAny<LocationDto>()))
                .ReturnsAsync((Location)completedTask.Result);

            // Act
            var result = await _locationsController.UpdateLocation(id, locationDto);
            var responseAsCreateAsActionResult = (CreatedAtActionResult)result.Result;
            var resultValue = responseAsCreateAsActionResult.Value;

            locationAsCity.Id = (int)resultValue.GetType().GetProperty("Id").GetValue(resultValue, null);
            locationAsCity.Name = (string)resultValue.GetType().GetProperty("Name").GetValue(resultValue, null);
            locationAsCity.LocationId = (LocationType)resultValue.GetType().GetProperty("LocationId").GetValue(resultValue, null);
            locationAsCity.GeographicalDescription = (string)resultValue.GetType().GetProperty("GeographicalDescription").GetValue(resultValue, null);

            // Assert
            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(city.Id, locationAsCity.Id);
            Assert.Equal(city.Name, locationAsCity.Name);
            Assert.Equal(city.LocationId, locationAsCity.LocationId);
            Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
        }

        [Fact]
        public async void WithInvalidIdAndValidChangedData_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            var locationDto = new LocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test"
            };

            var city = (City)null;

            var completedTask = Task<Location>.FromResult(city);
            var locationAsCity = new City();
            var badRequestActionStatusCode = (int)HttpStatusCode.BadRequest;
            _mockDomain.Setup(x => x.UpdateLocation(It.IsAny<int>(), It.IsAny<LocationDto>()))
                .ReturnsAsync((Location)completedTask.Result);

            // Act
            var result = await _locationsController.UpdateLocation(id, locationDto);
            var responseAsBadRequestResult = (BadRequestResult)result.Result;

            // Assert
            Assert.Null(result.Value);
            Assert.Equal(badRequestActionStatusCode, responseAsBadRequestResult.StatusCode);
        }

        [Fact]
        public async void WithValidIdAndInvalidChangedData_ReturnsBadRequest()
        {
            // Arrange
            int id = 1;
            var locationDto = new LocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = null
            };

            var city = (City)null;

            var completedTask = Task<Location>.FromResult(city);
            var locationAsCity = new City();
            var badRequestActionStatusCode = (int)HttpStatusCode.BadRequest;
            _mockDomain.Setup(x => x.UpdateLocation(It.IsAny<int>(), It.IsAny<LocationDto>()))
                .ReturnsAsync((Location)completedTask.Result);

            // Act
            var result = await _locationsController.UpdateLocation(id, locationDto);
            var responseAsBadRequestResult = (BadRequestResult)result.Result;

            // Assert
            Assert.Null(result.Value);
            Assert.Equal(badRequestActionStatusCode, responseAsBadRequestResult.StatusCode);
        }

        [Fact]
        public async void WithWrongIdAndValidChangedData_UpdatesWrongLocation()
        {
            // Arrange
            int id = 2;
            var locationDto = new LocationDto
            {
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test",
            };

            var city = new City
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
                GeographicalDescription = "Test"
            };

            var completedTask = Task<Location>.FromResult(city);
            var locationAsCity = new City();
            var createdAtActionStatusCode = (int)HttpStatusCode.Created;
            _mockDomain.Setup(x => x.UpdateLocation(It.IsAny<int>(), It.IsAny<LocationDto>()))
                .ReturnsAsync((Location)completedTask.Result);

            // Act
            var result = await _locationsController.UpdateLocation(id, locationDto);
            var responseAsCreateAsActionResult = (CreatedAtActionResult)result.Result;
            var resultValue = responseAsCreateAsActionResult.Value;

            locationAsCity.Id = (int)resultValue.GetType().GetProperty("Id").GetValue(resultValue, null);
            locationAsCity.Name = (string)resultValue.GetType().GetProperty("Name").GetValue(resultValue, null);
            locationAsCity.LocationId = (LocationType)resultValue.GetType().GetProperty("LocationId").GetValue(resultValue, null);
            locationAsCity.GeographicalDescription = (string)resultValue.GetType().GetProperty("GeographicalDescription").GetValue(resultValue, null);

            // Assert
            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(city.Id, locationAsCity.Id);
            Assert.Equal(city.Name, locationAsCity.Name);
            Assert.Equal(city.LocationId, locationAsCity.LocationId);
            Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
        }

        [Fact]
        public async void WithBadModelState_ReturnBadRequest()
        {
            // Arrange
            var id = 1;
            LocationDto locationDto = new LocationDto();
            _locationsController.ModelState.AddModelError("fakeError", "fakeError");
            var badRequest = (int)HttpStatusCode.BadRequest;

            // Act
            var result = await _locationsController.UpdateLocation(id, locationDto);
            var resultAsBadRequest = result.Result as BadRequestResult;

            // Assert
            Assert.Equal(badRequest, resultAsBadRequest.StatusCode);
        }

    }

    public class CreateLocation : LocationController_Tests
    {
        [Fact]
        public async void WhenLocationDtoHasInvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            LocationDto locationDto = new LocationDto();
            _locationsController.ModelState.AddModelError("fakeError", "fakeError");
            var badRequest = (int)HttpStatusCode.BadRequest;

            // Act
            var result = await _locationsController.CreateLocation(locationDto);
            var resultAsBadRequest = result.Result as BadRequestResult;

            // Assert
            Assert.Equal(badRequest, resultAsBadRequest.StatusCode);
        }

        [Fact]
        public async void WhenLocationDtoHasInvalidOrNullLocationType_ReturnsBadRequest()
        {
            // Arrange

            LocationDto locationDto = new LocationDto
            {
                Name = "Test City",
                GeographicalDescription = "Test Description"
            };
            Location location = null;

            var completedCreateTask = Task<Location>.FromResult(location);
            var badRequest = (int)HttpStatusCode.BadRequest;

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>())).ReturnsAsync((Location)completedCreateTask.Result);

            // Act

            var response = await _locationsController.CreateLocation(locationDto);
            var responseAsBadRequest = response.Result as BadRequestResult;


            // Assert

            Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
        }

        public class CreateLocation_AsCity : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsACity_ReturnsCreateAtActionWithCityDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test City",
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test Description"
                };

                var city = new City
                {
                    Id = 0,
                    Name = "Test City",
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(city);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var cityObject = new object();
                var locationAsCity = new City();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                cityObject = responseAsCreateAsActionResult.Value;

                locationAsCity.Id = (int)cityObject.GetType().GetProperty("Id").GetValue(cityObject, null);
                locationAsCity.Name = (string)cityObject.GetType().GetProperty("Name").GetValue(cityObject, null);
                locationAsCity.LocationId = (LocationType)cityObject.GetType().GetProperty("LocationId").GetValue(cityObject, null);
                locationAsCity.GeographicalDescription = (string)cityObject.GetType().GetProperty("GeographicalDescription").GetValue(cityObject, null);
                locationAsCity.Description = (string)cityObject.GetType().GetProperty("Description").GetValue(cityObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(city.Id, locationAsCity.Id);
                Assert.Equal(city.Name, locationAsCity.Name);
                Assert.Equal(city.Description, locationAsCity.Description);
                Assert.Equal(city.LocationId, locationAsCity.LocationId);
                Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAsACity_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test City",
                    Description = "    ",
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test Description"
                };

                var city = new City
                {
                    Id = 0,
                    Name = "Test City",
                    Description = "",
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(city);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var cityObject = new object();
                var locationAsCity = new City();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                cityObject = responseAsCreateAsActionResult.Value;

                locationAsCity.Id = (int)cityObject.GetType().GetProperty("Id").GetValue(cityObject, null);
                locationAsCity.Name = (string)cityObject.GetType().GetProperty("Name").GetValue(cityObject, null);
                locationAsCity.LocationId = (LocationType)cityObject.GetType().GetProperty("LocationId").GetValue(cityObject, null);
                locationAsCity.GeographicalDescription = (string)cityObject.GetType().GetProperty("GeographicalDescription").GetValue(cityObject, null);
                locationAsCity.Description = (string)cityObject.GetType().GetProperty("Description").GetValue(cityObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(city.Name, locationAsCity.Name);
                Assert.Equal(city.Id, locationAsCity.Id);
                Assert.Equal(city.Description, locationAsCity.Description);
                Assert.Equal(city.LocationId, locationAsCity.LocationId);
                Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsACity_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test City",
                    Description = null,
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test Description"
                };

                var city = new City
                {
                    Id = 0,
                    Name = "Test City",
                    Description = null,
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(city);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var cityObject = new object();
                var locationAsCity = new City();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                cityObject = responseAsCreateAsActionResult.Value;

                locationAsCity.Id = (int)cityObject.GetType().GetProperty("Id").GetValue(cityObject, null);
                locationAsCity.Name = (string)cityObject.GetType().GetProperty("Name").GetValue(cityObject, null);
                locationAsCity.LocationId = (LocationType)cityObject.GetType().GetProperty("LocationId").GetValue(cityObject, null);
                locationAsCity.GeographicalDescription = (string)cityObject.GetType().GetProperty("GeographicalDescription").GetValue(cityObject, null);
                locationAsCity.Description = (string)cityObject.GetType().GetProperty("Description").GetValue(cityObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(city.Id, locationAsCity.Id);
                Assert.Equal(city.Name, locationAsCity.Name);
                Assert.Equal(city.Description, locationAsCity.Description);
                Assert.Equal(city.LocationId, locationAsCity.LocationId);
                Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForName_ReturnsBadRequest()
            {
                // Arrange

                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.City,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);

            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescription_ReturnsBadRequest()
            {
                // Arrange

                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.City,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsTown : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsATown_ReturnsCreateAtActionWithTownDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Town",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test Description"
                };

                var town = new Town
                {
                    Id = 0,
                    Name = "Test Town",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(town);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var townObject = new object();
                var locationAsTown = new Town();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                townObject = responseAsCreateAsActionResult.Value;

                locationAsTown.Id = (int)townObject.GetType().GetProperty("Id").GetValue(townObject, null);
                locationAsTown.Name = (string)townObject.GetType().GetProperty("Name").GetValue(townObject, null);
                locationAsTown.LocationId = (LocationType)townObject.GetType().GetProperty("LocationId").GetValue(townObject, null);
                locationAsTown.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
                locationAsTown.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(town.Id, locationAsTown.Id);
                Assert.Equal(town.Name, locationAsTown.Name);
                Assert.Equal(town.Description, locationAsTown.Description);
                Assert.Equal(town.LocationId, locationAsTown.LocationId);
                Assert.Equal(town.GeographicalDescription, locationAsTown.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionATown_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Town",
                    Description = "    ",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test Description"
                };

                var town = new Town
                {
                    Id = 0,
                    Name = "Test Town",
                    Description = "",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(town);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var townObject = new object();
                var locationAsTown = new Town();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                townObject = responseAsCreateAsActionResult.Value;

                locationAsTown.Id = (int)townObject.GetType().GetProperty("Id").GetValue(townObject, null);
                locationAsTown.Name = (string)townObject.GetType().GetProperty("Name").GetValue(townObject, null);
                locationAsTown.LocationId = (LocationType)townObject.GetType().GetProperty("LocationId").GetValue(townObject, null);
                locationAsTown.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
                locationAsTown.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(town.Name, locationAsTown.Name);
                Assert.Equal(town.Id, locationAsTown.Id);
                Assert.Equal(town.Description, locationAsTown.Description);
                Assert.Equal(town.LocationId, locationAsTown.LocationId);
                Assert.Equal(town.GeographicalDescription, locationAsTown.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsATown_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Town",
                    Description = null,
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test Description"
                };

                var town = new Town
                {
                    Id = 0,
                    Name = "Test Town",
                    Description = null,
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(town);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var townObject = new object();
                var locationAsTown = new Town();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                townObject = responseAsCreateAsActionResult.Value;

                locationAsTown.Id = (int)townObject.GetType().GetProperty("Id").GetValue(townObject, null);
                locationAsTown.Name = (string)townObject.GetType().GetProperty("Name").GetValue(townObject, null);
                locationAsTown.LocationId = (LocationType)townObject.GetType().GetProperty("LocationId").GetValue(townObject, null);
                locationAsTown.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
                locationAsTown.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(town.Id, locationAsTown.Id);
                Assert.Equal(town.Name, locationAsTown.Name);
                Assert.Equal(town.Description, locationAsTown.Description);
                Assert.Equal(town.LocationId, locationAsTown.LocationId);
                Assert.Equal(town.GeographicalDescription, locationAsTown.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsATown_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsATown_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Town,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsHomestead : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAHomestead_ReturnsCreateAtActionWithHomesteadDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Homestead",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test Description"
                };

                var homestead = new Homestead
                {
                    Id = 0,
                    Name = "Test Homestead",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(homestead);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var townObject = new object();
                var locationAsHomestead = new Homestead();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                townObject = responseAsCreateAsActionResult.Value;

                locationAsHomestead.Id = (int)townObject.GetType().GetProperty("Id").GetValue(townObject, null);
                locationAsHomestead.Name = (string)townObject.GetType().GetProperty("Name").GetValue(townObject, null);
                locationAsHomestead.LocationId = (LocationType)townObject.GetType().GetProperty("LocationId").GetValue(townObject, null);
                locationAsHomestead.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
                locationAsHomestead.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(homestead.Id, locationAsHomestead.Id);
                Assert.Equal(homestead.Name, locationAsHomestead.Name);
                Assert.Equal(homestead.Description, locationAsHomestead.Description);
                Assert.Equal(homestead.LocationId, locationAsHomestead.LocationId);
                Assert.Equal(homestead.GeographicalDescription, locationAsHomestead.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAHomestead_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Homestead",
                    Description = "    ",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test Description"
                };

                var homestead = new Homestead
                {
                    Id = 0,
                    Name = "Test Homestead",
                    Description = "",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(homestead);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var homesteadObject = new object();
                var locationAsHomestead = new Homestead();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                homesteadObject = responseAsCreateAsActionResult.Value;

                locationAsHomestead.Id = (int)homesteadObject.GetType().GetProperty("Id").GetValue(homesteadObject, null);
                locationAsHomestead.Name = (string)homesteadObject.GetType().GetProperty("Name").GetValue(homesteadObject, null);
                locationAsHomestead.LocationId = (LocationType)homesteadObject.GetType().GetProperty("LocationId").GetValue(homesteadObject, null);
                locationAsHomestead.GeographicalDescription = (string)homesteadObject.GetType().GetProperty("GeographicalDescription").GetValue(homesteadObject, null);
                locationAsHomestead.Description = (string)homesteadObject.GetType().GetProperty("Description").GetValue(homesteadObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(homestead.Name, locationAsHomestead.Name);
                Assert.Equal(homestead.Id, locationAsHomestead.Id);
                Assert.Equal(homestead.Description, locationAsHomestead.Description);
                Assert.Equal(homestead.LocationId, locationAsHomestead.LocationId);
                Assert.Equal(homestead.GeographicalDescription, locationAsHomestead.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAHomestead_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Homestead",
                    Description = null,
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test Description"
                };

                var homestead = new Homestead
                {
                    Id = 0,
                    Name = "Test Homestead",
                    Description = null,
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(homestead);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var homesteadObject = new object();
                var locationAsHomestead = new Homestead();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                homesteadObject = responseAsCreateAsActionResult.Value;

                locationAsHomestead.Id = (int)homesteadObject.GetType().GetProperty("Id").GetValue(homesteadObject, null);
                locationAsHomestead.Name = (string)homesteadObject.GetType().GetProperty("Name").GetValue(homesteadObject, null);
                locationAsHomestead.LocationId = (LocationType)homesteadObject.GetType().GetProperty("LocationId").GetValue(homesteadObject, null);
                locationAsHomestead.GeographicalDescription = (string)homesteadObject.GetType().GetProperty("GeographicalDescription").GetValue(homesteadObject, null);
                locationAsHomestead.Description = (string)homesteadObject.GetType().GetProperty("Description").GetValue(homesteadObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(homestead.Id, locationAsHomestead.Id);
                Assert.Equal(homestead.Name, locationAsHomestead.Name);
                Assert.Equal(homestead.Description, locationAsHomestead.Description);
                Assert.Equal(homestead.LocationId, locationAsHomestead.LocationId);
                Assert.Equal(homestead.GeographicalDescription, locationAsHomestead.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAHomestead_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsATown_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Homestead,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsSettlement : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsASettlement_ReturnsCreateAtActionWithSettlementDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Settlement",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test Description"
                };

                var settlement = new Settlement
                {
                    Id = 0,
                    Name = "Test Settlement",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(settlement);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var settlementObject = new object();
                var locationAsSettlement = new Settlement();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                settlementObject = responseAsCreateAsActionResult.Value;

                locationAsSettlement.Id = (int)settlementObject.GetType().GetProperty("Id").GetValue(settlementObject, null);
                locationAsSettlement.Name = (string)settlementObject.GetType().GetProperty("Name").GetValue(settlementObject, null);
                locationAsSettlement.LocationId = (LocationType)settlementObject.GetType().GetProperty("LocationId").GetValue(settlementObject, null);
                locationAsSettlement.GeographicalDescription = (string)settlementObject.GetType().GetProperty("GeographicalDescription").GetValue(settlementObject, null);
                locationAsSettlement.Description = (string)settlementObject.GetType().GetProperty("Description").GetValue(settlementObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(settlement.Id, locationAsSettlement.Id);
                Assert.Equal(settlement.Name, locationAsSettlement.Name);
                Assert.Equal(settlement.Description, locationAsSettlement.Description);
                Assert.Equal(settlement.LocationId, locationAsSettlement.LocationId);
                Assert.Equal(settlement.GeographicalDescription, locationAsSettlement.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionASettlement_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Settlement",
                    Description = "    ",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test Description"
                };

                var settlement = new Settlement
                {
                    Id = 0,
                    Name = "Test Settlement",
                    Description = "",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(settlement);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var settlementObject = new object();
                var locationAsSettlement = new Settlement();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                settlementObject = responseAsCreateAsActionResult.Value;

                locationAsSettlement.Id = (int)settlementObject.GetType().GetProperty("Id").GetValue(settlementObject, null);
                locationAsSettlement.Name = (string)settlementObject.GetType().GetProperty("Name").GetValue(settlementObject, null);
                locationAsSettlement.LocationId = (LocationType)settlementObject.GetType().GetProperty("LocationId").GetValue(settlementObject, null);
                locationAsSettlement.GeographicalDescription = (string)settlementObject.GetType().GetProperty("GeographicalDescription").GetValue(settlementObject, null);
                locationAsSettlement.Description = (string)settlementObject.GetType().GetProperty("Description").GetValue(settlementObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(settlement.Name, locationAsSettlement.Name);
                Assert.Equal(settlement.Id, locationAsSettlement.Id);
                Assert.Equal(settlement.Description, locationAsSettlement.Description);
                Assert.Equal(settlement.LocationId, locationAsSettlement.LocationId);
                Assert.Equal(settlement.GeographicalDescription, locationAsSettlement.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsASettlement_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Settlement",
                    Description = null,
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test Description"
                };

                var settlement = new Settlement
                {
                    Id = 0,
                    Name = "Test Settlement",
                    Description = null,
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(settlement);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var settlementObject = new object();
                var locationAsSettlement = new Settlement();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                settlementObject = responseAsCreateAsActionResult.Value;

                locationAsSettlement.Id = (int)settlementObject.GetType().GetProperty("Id").GetValue(settlementObject, null);
                locationAsSettlement.Name = (string)settlementObject.GetType().GetProperty("Name").GetValue(settlementObject, null);
                locationAsSettlement.LocationId = (LocationType)settlementObject.GetType().GetProperty("LocationId").GetValue(settlementObject, null);
                locationAsSettlement.GeographicalDescription = (string)settlementObject.GetType().GetProperty("GeographicalDescription").GetValue(settlementObject, null);
                locationAsSettlement.Description = (string)settlementObject.GetType().GetProperty("Description").GetValue(settlementObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(settlement.Id, locationAsSettlement.Id);
                Assert.Equal(settlement.Name, locationAsSettlement.Name);
                Assert.Equal(settlement.Description, locationAsSettlement.Description);
                Assert.Equal(settlement.LocationId, locationAsSettlement.LocationId);
                Assert.Equal(settlement.GeographicalDescription, locationAsSettlement.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsASettlement_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsASettlement_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Settlement,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsDaedricShrine : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADaedricShrine_ReturnsCreateAtActionWithDaedricShrineDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DaedricShrine",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test Description"
                };

                var daedricShrine = new DaedricShrine
                {
                    Id = 0,
                    Name = "Test DaedricShrine",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(daedricShrine);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var daedricShrineObject = new object();
                var locationAsDaedricShrine = new DaedricShrine();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                daedricShrineObject = responseAsCreateAsActionResult.Value;

                locationAsDaedricShrine.Id = (int)daedricShrineObject.GetType().GetProperty("Id").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.Name = (string)daedricShrineObject.GetType().GetProperty("Name").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.LocationId = (LocationType)daedricShrineObject.GetType().GetProperty("LocationId").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.GeographicalDescription = (string)daedricShrineObject.GetType().GetProperty("GeographicalDescription").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.Description = (string)daedricShrineObject.GetType().GetProperty("Description").GetValue(daedricShrineObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(daedricShrine.Id, locationAsDaedricShrine.Id);
                Assert.Equal(daedricShrine.Name, locationAsDaedricShrine.Name);
                Assert.Equal(daedricShrine.Description, locationAsDaedricShrine.Description);
                Assert.Equal(daedricShrine.LocationId, locationAsDaedricShrine.LocationId);
                Assert.Equal(daedricShrine.GeographicalDescription, locationAsDaedricShrine.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADaedricShrine_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DaedricShrine",
                    Description = "    ",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test Description"
                };

                var daedricShrine = new DaedricShrine
                {
                    Id = 0,
                    Name = "Test DaedricShrine",
                    Description = "",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(daedricShrine);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var daedricShrineObject = new object();
                var locationAsDaedricShrine = new DaedricShrine();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                daedricShrineObject = responseAsCreateAsActionResult.Value;

                locationAsDaedricShrine.Id = (int)daedricShrineObject.GetType().GetProperty("Id").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.Name = (string)daedricShrineObject.GetType().GetProperty("Name").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.LocationId = (LocationType)daedricShrineObject.GetType().GetProperty("LocationId").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.GeographicalDescription = (string)daedricShrineObject.GetType().GetProperty("GeographicalDescription").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.Description = (string)daedricShrineObject.GetType().GetProperty("Description").GetValue(daedricShrineObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(daedricShrine.Name, locationAsDaedricShrine.Name);
                Assert.Equal(daedricShrine.Id, locationAsDaedricShrine.Id);
                Assert.Equal(daedricShrine.Description, locationAsDaedricShrine.Description);
                Assert.Equal(daedricShrine.LocationId, locationAsDaedricShrine.LocationId);
                Assert.Equal(daedricShrine.GeographicalDescription, locationAsDaedricShrine.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsADaedricShrine_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DaedricShrine",
                    Description = null,
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test Description"
                };

                var daedricShrine = new DaedricShrine
                {
                    Id = 0,
                    Name = "Test DaedricShrine",
                    Description = null,
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(daedricShrine);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var daedricShrineObject = new object();
                var locationAsDaedricShrine = new DaedricShrine();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                daedricShrineObject = responseAsCreateAsActionResult.Value;

                locationAsDaedricShrine.Id = (int)daedricShrineObject.GetType().GetProperty("Id").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.Name = (string)daedricShrineObject.GetType().GetProperty("Name").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.LocationId = (LocationType)daedricShrineObject.GetType().GetProperty("LocationId").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.GeographicalDescription = (string)daedricShrineObject.GetType().GetProperty("GeographicalDescription").GetValue(daedricShrineObject, null);
                locationAsDaedricShrine.Description = (string)daedricShrineObject.GetType().GetProperty("Description").GetValue(daedricShrineObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(daedricShrine.Id, locationAsDaedricShrine.Id);
                Assert.Equal(daedricShrine.Name, locationAsDaedricShrine.Name);
                Assert.Equal(daedricShrine.Description, locationAsDaedricShrine.Description);
                Assert.Equal(daedricShrine.LocationId, locationAsDaedricShrine.LocationId);
                Assert.Equal(daedricShrine.GeographicalDescription, locationAsDaedricShrine.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADaedricShrine_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADaedricShrine_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DaedricShrine,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsStandingStone : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAStandingStone_ReturnsCreateAtActionWithStandingStoneDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test StandingStone",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test Description"
                };

                var standingStone = new StandingStone
                {
                    Id = 0,
                    Name = "Test StandingStone",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(standingStone);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var standingStoneObject = new object();
                var locationAsStandingStone = new StandingStone();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                standingStoneObject = responseAsCreateAsActionResult.Value;

                locationAsStandingStone.Id = (int)standingStoneObject.GetType().GetProperty("Id").GetValue(standingStoneObject, null);
                locationAsStandingStone.Name = (string)standingStoneObject.GetType().GetProperty("Name").GetValue(standingStoneObject, null);
                locationAsStandingStone.LocationId = (LocationType)standingStoneObject.GetType().GetProperty("LocationId").GetValue(standingStoneObject, null);
                locationAsStandingStone.GeographicalDescription = (string)standingStoneObject.GetType().GetProperty("GeographicalDescription").GetValue(standingStoneObject, null);
                locationAsStandingStone.Description = (string)standingStoneObject.GetType().GetProperty("Description").GetValue(standingStoneObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(standingStone.Id, locationAsStandingStone.Id);
                Assert.Equal(standingStone.Name, locationAsStandingStone.Name);
                Assert.Equal(standingStone.Description, locationAsStandingStone.Description);
                Assert.Equal(standingStone.LocationId, locationAsStandingStone.LocationId);
                Assert.Equal(standingStone.GeographicalDescription, locationAsStandingStone.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAStandingStone_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test StandingStone",
                    Description = "    ",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test Description"
                };

                var standingStone = new StandingStone
                {
                    Id = 0,
                    Name = "Test StandingStone",
                    Description = "",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(standingStone);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var standingStoneObject = new object();
                var locationAsStandingStone = new StandingStone();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                standingStoneObject = responseAsCreateAsActionResult.Value;

                locationAsStandingStone.Id = (int)standingStoneObject.GetType().GetProperty("Id").GetValue(standingStoneObject, null);
                locationAsStandingStone.Name = (string)standingStoneObject.GetType().GetProperty("Name").GetValue(standingStoneObject, null);
                locationAsStandingStone.LocationId = (LocationType)standingStoneObject.GetType().GetProperty("LocationId").GetValue(standingStoneObject, null);
                locationAsStandingStone.GeographicalDescription = (string)standingStoneObject.GetType().GetProperty("GeographicalDescription").GetValue(standingStoneObject, null);
                locationAsStandingStone.Description = (string)standingStoneObject.GetType().GetProperty("Description").GetValue(standingStoneObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(standingStone.Name, locationAsStandingStone.Name);
                Assert.Equal(standingStone.Id, locationAsStandingStone.Id);
                Assert.Equal(standingStone.Description, locationAsStandingStone.Description);
                Assert.Equal(standingStone.LocationId, locationAsStandingStone.LocationId);
                Assert.Equal(standingStone.GeographicalDescription, locationAsStandingStone.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAStandingStone_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test StandingStone",
                    Description = null,
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test Description"
                };

                var standingStone = new StandingStone
                {
                    Id = 0,
                    Name = "Test StandingStone",
                    Description = null,
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(standingStone);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var standingStoneObject = new object();
                var locationAsStandingStone = new StandingStone();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                standingStoneObject = responseAsCreateAsActionResult.Value;

                locationAsStandingStone.Id = (int)standingStoneObject.GetType().GetProperty("Id").GetValue(standingStoneObject, null);
                locationAsStandingStone.Name = (string)standingStoneObject.GetType().GetProperty("Name").GetValue(standingStoneObject, null);
                locationAsStandingStone.LocationId = (LocationType)standingStoneObject.GetType().GetProperty("LocationId").GetValue(standingStoneObject, null);
                locationAsStandingStone.GeographicalDescription = (string)standingStoneObject.GetType().GetProperty("GeographicalDescription").GetValue(standingStoneObject, null);
                locationAsStandingStone.Description = (string)standingStoneObject.GetType().GetProperty("Description").GetValue(standingStoneObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(standingStone.Id, locationAsStandingStone.Id);
                Assert.Equal(standingStone.Name, locationAsStandingStone.Name);
                Assert.Equal(standingStone.Description, locationAsStandingStone.Description);
                Assert.Equal(standingStone.LocationId, locationAsStandingStone.LocationId);
                Assert.Equal(standingStone.GeographicalDescription, locationAsStandingStone.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAStandingStone_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAStandingStone_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.StandingStone,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsLandmark : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsALandmark_ReturnsCreateAtActionWithLandmarkDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Landmark",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test Description"
                };

                var landmark = new Landmark
                {
                    Id = 0,
                    Name = "Test Landmark",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(landmark);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var landmarkObject = new object();
                var locationAsLandmark = new Landmark();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                landmarkObject = responseAsCreateAsActionResult.Value;

                locationAsLandmark.Id = (int)landmarkObject.GetType().GetProperty("Id").GetValue(landmarkObject, null);
                locationAsLandmark.Name = (string)landmarkObject.GetType().GetProperty("Name").GetValue(landmarkObject, null);
                locationAsLandmark.LocationId = (LocationType)landmarkObject.GetType().GetProperty("LocationId").GetValue(landmarkObject, null);
                locationAsLandmark.GeographicalDescription = (string)landmarkObject.GetType().GetProperty("GeographicalDescription").GetValue(landmarkObject, null);
                locationAsLandmark.Description = (string)landmarkObject.GetType().GetProperty("Description").GetValue(landmarkObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(landmark.Id, locationAsLandmark.Id);
                Assert.Equal(landmark.Name, locationAsLandmark.Name);
                Assert.Equal(landmark.Description, locationAsLandmark.Description);
                Assert.Equal(landmark.LocationId, locationAsLandmark.LocationId);
                Assert.Equal(landmark.GeographicalDescription, locationAsLandmark.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionALandmark_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Landmark",
                    Description = "    ",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test Description"
                };

                var landmark = new Landmark
                {
                    Id = 0,
                    Name = "Test Landmark",
                    Description = "",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(landmark);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var landmarkObject = new object();
                var locationAsLandmark = new Landmark();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                landmarkObject = responseAsCreateAsActionResult.Value;

                locationAsLandmark.Id = (int)landmarkObject.GetType().GetProperty("Id").GetValue(landmarkObject, null);
                locationAsLandmark.Name = (string)landmarkObject.GetType().GetProperty("Name").GetValue(landmarkObject, null);
                locationAsLandmark.LocationId = (LocationType)landmarkObject.GetType().GetProperty("LocationId").GetValue(landmarkObject, null);
                locationAsLandmark.GeographicalDescription = (string)landmarkObject.GetType().GetProperty("GeographicalDescription").GetValue(landmarkObject, null);
                locationAsLandmark.Description = (string)landmarkObject.GetType().GetProperty("Description").GetValue(landmarkObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(landmark.Name, locationAsLandmark.Name);
                Assert.Equal(landmark.Id, locationAsLandmark.Id);
                Assert.Equal(landmark.Description, locationAsLandmark.Description);
                Assert.Equal(landmark.LocationId, locationAsLandmark.LocationId);
                Assert.Equal(landmark.GeographicalDescription, locationAsLandmark.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsALandmark_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Landmark",
                    Description = null,
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test Description"
                };

                var landmark = new Landmark
                {
                    Id = 0,
                    Name = "Test Landmark",
                    Description = null,
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(landmark);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var landmarkObject = new object();
                var locationAsLandmark = new Landmark();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                landmarkObject = responseAsCreateAsActionResult.Value;

                locationAsLandmark.Id = (int)landmarkObject.GetType().GetProperty("Id").GetValue(landmarkObject, null);
                locationAsLandmark.Name = (string)landmarkObject.GetType().GetProperty("Name").GetValue(landmarkObject, null);
                locationAsLandmark.LocationId = (LocationType)landmarkObject.GetType().GetProperty("LocationId").GetValue(landmarkObject, null);
                locationAsLandmark.GeographicalDescription = (string)landmarkObject.GetType().GetProperty("GeographicalDescription").GetValue(landmarkObject, null);
                locationAsLandmark.Description = (string)landmarkObject.GetType().GetProperty("Description").GetValue(landmarkObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(landmark.Id, locationAsLandmark.Id);
                Assert.Equal(landmark.Name, locationAsLandmark.Name);
                Assert.Equal(landmark.Description, locationAsLandmark.Description);
                Assert.Equal(landmark.LocationId, locationAsLandmark.LocationId);
                Assert.Equal(landmark.GeographicalDescription, locationAsLandmark.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsALandmark_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsALandmark_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Landmark,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsCamp : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsACamp_ReturnsCreateAtActionWithCampDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Camp",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test Description"
                };

                var camp = new Camp
                {
                    Id = 0,
                    Name = "Test Camp",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(camp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var campObject = new object();
                var locationAsCamp = new Camp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                campObject = responseAsCreateAsActionResult.Value;

                locationAsCamp.Id = (int)campObject.GetType().GetProperty("Id").GetValue(campObject, null);
                locationAsCamp.Name = (string)campObject.GetType().GetProperty("Name").GetValue(campObject, null);
                locationAsCamp.LocationId = (LocationType)campObject.GetType().GetProperty("LocationId").GetValue(campObject, null);
                locationAsCamp.GeographicalDescription = (string)campObject.GetType().GetProperty("GeographicalDescription").GetValue(campObject, null);
                locationAsCamp.Description = (string)campObject.GetType().GetProperty("Description").GetValue(campObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(camp.Id, locationAsCamp.Id);
                Assert.Equal(camp.Name, locationAsCamp.Name);
                Assert.Equal(camp.Description, locationAsCamp.Description);
                Assert.Equal(camp.LocationId, locationAsCamp.LocationId);
                Assert.Equal(camp.GeographicalDescription, locationAsCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionACamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Camp",
                    Description = "    ",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test Description"
                };

                var camp = new Camp
                {
                    Id = 0,
                    Name = "Test Camp",
                    Description = "",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(camp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var campObject = new object();
                var locationAsCamp = new Camp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                campObject = responseAsCreateAsActionResult.Value;

                locationAsCamp.Id = (int)campObject.GetType().GetProperty("Id").GetValue(campObject, null);
                locationAsCamp.Name = (string)campObject.GetType().GetProperty("Name").GetValue(campObject, null);
                locationAsCamp.LocationId = (LocationType)campObject.GetType().GetProperty("LocationId").GetValue(campObject, null);
                locationAsCamp.GeographicalDescription = (string)campObject.GetType().GetProperty("GeographicalDescription").GetValue(campObject, null);
                locationAsCamp.Description = (string)campObject.GetType().GetProperty("Description").GetValue(campObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(camp.Name, locationAsCamp.Name);
                Assert.Equal(camp.Id, locationAsCamp.Id);
                Assert.Equal(camp.Description, locationAsCamp.Description);
                Assert.Equal(camp.LocationId, locationAsCamp.LocationId);
                Assert.Equal(camp.GeographicalDescription, locationAsCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsACamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Camp",
                    Description = null,
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test Description"
                };

                var camp = new Camp
                {
                    Id = 0,
                    Name = "Test Camp",
                    Description = null,
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(camp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var campObject = new object();
                var locationAsCamp = new Camp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                campObject = responseAsCreateAsActionResult.Value;

                locationAsCamp.Id = (int)campObject.GetType().GetProperty("Id").GetValue(campObject, null);
                locationAsCamp.Name = (string)campObject.GetType().GetProperty("Name").GetValue(campObject, null);
                locationAsCamp.LocationId = (LocationType)campObject.GetType().GetProperty("LocationId").GetValue(campObject, null);
                locationAsCamp.GeographicalDescription = (string)campObject.GetType().GetProperty("GeographicalDescription").GetValue(campObject, null);
                locationAsCamp.Description = (string)campObject.GetType().GetProperty("Description").GetValue(campObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(camp.Id, locationAsCamp.Id);
                Assert.Equal(camp.Name, locationAsCamp.Name);
                Assert.Equal(camp.Description, locationAsCamp.Description);
                Assert.Equal(camp.LocationId, locationAsCamp.LocationId);
                Assert.Equal(camp.GeographicalDescription, locationAsCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsACamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsACamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Camp,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsCave : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsACave_ReturnsCreateAtActionWithCaveDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Cave",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test Description"
                };

                var cave = new Cave
                {
                    Id = 0,
                    Name = "Test Cave",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(cave);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var caveObject = new object();
                var locationAsCave = new Cave();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                caveObject = responseAsCreateAsActionResult.Value;

                locationAsCave.Id = (int)caveObject.GetType().GetProperty("Id").GetValue(caveObject, null);
                locationAsCave.Name = (string)caveObject.GetType().GetProperty("Name").GetValue(caveObject, null);
                locationAsCave.LocationId = (LocationType)caveObject.GetType().GetProperty("LocationId").GetValue(caveObject, null);
                locationAsCave.GeographicalDescription = (string)caveObject.GetType().GetProperty("GeographicalDescription").GetValue(caveObject, null);
                locationAsCave.Description = (string)caveObject.GetType().GetProperty("Description").GetValue(caveObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(cave.Id, locationAsCave.Id);
                Assert.Equal(cave.Name, locationAsCave.Name);
                Assert.Equal(cave.Description, locationAsCave.Description);
                Assert.Equal(cave.LocationId, locationAsCave.LocationId);
                Assert.Equal(cave.GeographicalDescription, locationAsCave.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionACave_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Cave",
                    Description = "    ",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test Description"
                };

                var cave = new Cave
                {
                    Id = 0,
                    Name = "Test Cave",
                    Description = "",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(cave);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var caveObject = new object();
                var locationAsCave = new Cave();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                caveObject = responseAsCreateAsActionResult.Value;

                locationAsCave.Id = (int)caveObject.GetType().GetProperty("Id").GetValue(caveObject, null);
                locationAsCave.Name = (string)caveObject.GetType().GetProperty("Name").GetValue(caveObject, null);
                locationAsCave.LocationId = (LocationType)caveObject.GetType().GetProperty("LocationId").GetValue(caveObject, null);
                locationAsCave.GeographicalDescription = (string)caveObject.GetType().GetProperty("GeographicalDescription").GetValue(caveObject, null);
                locationAsCave.Description = (string)caveObject.GetType().GetProperty("Description").GetValue(caveObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(cave.Name, locationAsCave.Name);
                Assert.Equal(cave.Id, locationAsCave.Id);
                Assert.Equal(cave.Description, locationAsCave.Description);
                Assert.Equal(cave.LocationId, locationAsCave.LocationId);
                Assert.Equal(cave.GeographicalDescription, locationAsCave.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsACave_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Cave",
                    Description = null,
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test Description"
                };

                var cave = new Cave
                {
                    Id = 0,
                    Name = "Test Cave",
                    Description = null,
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(cave);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var caveObject = new object();
                var locationAsCave = new Cave();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                caveObject = responseAsCreateAsActionResult.Value;

                locationAsCave.Id = (int)caveObject.GetType().GetProperty("Id").GetValue(caveObject, null);
                locationAsCave.Name = (string)caveObject.GetType().GetProperty("Name").GetValue(caveObject, null);
                locationAsCave.LocationId = (LocationType)caveObject.GetType().GetProperty("LocationId").GetValue(caveObject, null);
                locationAsCave.GeographicalDescription = (string)caveObject.GetType().GetProperty("GeographicalDescription").GetValue(caveObject, null);
                locationAsCave.Description = (string)caveObject.GetType().GetProperty("Description").GetValue(caveObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(cave.Id, locationAsCave.Id);
                Assert.Equal(cave.Name, locationAsCave.Name);
                Assert.Equal(cave.Description, locationAsCave.Description);
                Assert.Equal(cave.LocationId, locationAsCave.LocationId);
                Assert.Equal(cave.GeographicalDescription, locationAsCave.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsACave_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsACave_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Cave,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsClearing : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAClearing_ReturnsCreateAtActionWithClearingDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Clearing",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test Description"
                };

                var clearing = new Clearing
                {
                    Id = 0,
                    Name = "Test Clearing",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(clearing);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var clearingObject = new object();
                var locationAsClearing = new Clearing();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                clearingObject = responseAsCreateAsActionResult.Value;

                locationAsClearing.Id = (int)clearingObject.GetType().GetProperty("Id").GetValue(clearingObject, null);
                locationAsClearing.Name = (string)clearingObject.GetType().GetProperty("Name").GetValue(clearingObject, null);
                locationAsClearing.LocationId = (LocationType)clearingObject.GetType().GetProperty("LocationId").GetValue(clearingObject, null);
                locationAsClearing.GeographicalDescription = (string)clearingObject.GetType().GetProperty("GeographicalDescription").GetValue(clearingObject, null);
                locationAsClearing.Description = (string)clearingObject.GetType().GetProperty("Description").GetValue(clearingObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(clearing.Id, locationAsClearing.Id);
                Assert.Equal(clearing.Name, locationAsClearing.Name);
                Assert.Equal(clearing.Description, locationAsClearing.Description);
                Assert.Equal(clearing.LocationId, locationAsClearing.LocationId);
                Assert.Equal(clearing.GeographicalDescription, locationAsClearing.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAClearing_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Clearing",
                    Description = "    ",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test Description"
                };

                var clearing = new Clearing
                {
                    Id = 0,
                    Name = "Test Clearing",
                    Description = "",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(clearing);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var clearingObject = new object();
                var locationAsClearing = new Clearing();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                clearingObject = responseAsCreateAsActionResult.Value;

                locationAsClearing.Id = (int)clearingObject.GetType().GetProperty("Id").GetValue(clearingObject, null);
                locationAsClearing.Name = (string)clearingObject.GetType().GetProperty("Name").GetValue(clearingObject, null);
                locationAsClearing.LocationId = (LocationType)clearingObject.GetType().GetProperty("LocationId").GetValue(clearingObject, null);
                locationAsClearing.GeographicalDescription = (string)clearingObject.GetType().GetProperty("GeographicalDescription").GetValue(clearingObject, null);
                locationAsClearing.Description = (string)clearingObject.GetType().GetProperty("Description").GetValue(clearingObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(clearing.Name, locationAsClearing.Name);
                Assert.Equal(clearing.Id, locationAsClearing.Id);
                Assert.Equal(clearing.Description, locationAsClearing.Description);
                Assert.Equal(clearing.LocationId, locationAsClearing.LocationId);
                Assert.Equal(clearing.GeographicalDescription, locationAsClearing.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAClearing_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Clearing",
                    Description = null,
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test Description"
                };

                var clearing = new Clearing
                {
                    Id = 0,
                    Name = "Test Clearing",
                    Description = null,
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(clearing);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var clearingObject = new object();
                var locationAsClearing = new Clearing();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                clearingObject = responseAsCreateAsActionResult.Value;

                locationAsClearing.Id = (int)clearingObject.GetType().GetProperty("Id").GetValue(clearingObject, null);
                locationAsClearing.Name = (string)clearingObject.GetType().GetProperty("Name").GetValue(clearingObject, null);
                locationAsClearing.LocationId = (LocationType)clearingObject.GetType().GetProperty("LocationId").GetValue(clearingObject, null);
                locationAsClearing.GeographicalDescription = (string)clearingObject.GetType().GetProperty("GeographicalDescription").GetValue(clearingObject, null);
                locationAsClearing.Description = (string)clearingObject.GetType().GetProperty("Description").GetValue(clearingObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(clearing.Id, locationAsClearing.Id);
                Assert.Equal(clearing.Name, locationAsClearing.Name);
                Assert.Equal(clearing.Description, locationAsClearing.Description);
                Assert.Equal(clearing.LocationId, locationAsClearing.LocationId);
                Assert.Equal(clearing.GeographicalDescription, locationAsClearing.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAClearing_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAClearing_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Clearing,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsDock : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADock_ReturnsCreateAtActionWithDockDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Dock",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test Description"
                };

                var dock = new Dock
                {
                    Id = 0,
                    Name = "Test Dock",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dock);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dockObject = new object();
                var locationAsDock = new Dock();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dockObject = responseAsCreateAsActionResult.Value;

                locationAsDock.Id = (int)dockObject.GetType().GetProperty("Id").GetValue(dockObject, null);
                locationAsDock.Name = (string)dockObject.GetType().GetProperty("Name").GetValue(dockObject, null);
                locationAsDock.LocationId = (LocationType)dockObject.GetType().GetProperty("LocationId").GetValue(dockObject, null);
                locationAsDock.GeographicalDescription = (string)dockObject.GetType().GetProperty("GeographicalDescription").GetValue(dockObject, null);
                locationAsDock.Description = (string)dockObject.GetType().GetProperty("Description").GetValue(dockObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dock.Id, locationAsDock.Id);
                Assert.Equal(dock.Name, locationAsDock.Name);
                Assert.Equal(dock.Description, locationAsDock.Description);
                Assert.Equal(dock.LocationId, locationAsDock.LocationId);
                Assert.Equal(dock.GeographicalDescription, locationAsDock.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADock_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Dock",
                    Description = "    ",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test Description"
                };

                var dock = new Dock
                {
                    Id = 0,
                    Name = "Test Dock",
                    Description = "",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dock);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dockObject = new object();
                var locationAsDock = new Dock();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dockObject = responseAsCreateAsActionResult.Value;

                locationAsDock.Id = (int)dockObject.GetType().GetProperty("Id").GetValue(dockObject, null);
                locationAsDock.Name = (string)dockObject.GetType().GetProperty("Name").GetValue(dockObject, null);
                locationAsDock.LocationId = (LocationType)dockObject.GetType().GetProperty("LocationId").GetValue(dockObject, null);
                locationAsDock.GeographicalDescription = (string)dockObject.GetType().GetProperty("GeographicalDescription").GetValue(dockObject, null);
                locationAsDock.Description = (string)dockObject.GetType().GetProperty("Description").GetValue(dockObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dock.Name, locationAsDock.Name);
                Assert.Equal(dock.Id, locationAsDock.Id);
                Assert.Equal(dock.Description, locationAsDock.Description);
                Assert.Equal(dock.LocationId, locationAsDock.LocationId);
                Assert.Equal(dock.GeographicalDescription, locationAsDock.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsADock_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Dock",
                    Description = null,
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test Description"
                };

                var dock = new Dock
                {
                    Id = 0,
                    Name = "Test Dock",
                    Description = null,
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dock);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dockObject = new object();
                var locationAsDock = new Dock();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dockObject = responseAsCreateAsActionResult.Value;

                locationAsDock.Id = (int)dockObject.GetType().GetProperty("Id").GetValue(dockObject, null);
                locationAsDock.Name = (string)dockObject.GetType().GetProperty("Name").GetValue(dockObject, null);
                locationAsDock.LocationId = (LocationType)dockObject.GetType().GetProperty("LocationId").GetValue(dockObject, null);
                locationAsDock.GeographicalDescription = (string)dockObject.GetType().GetProperty("GeographicalDescription").GetValue(dockObject, null);
                locationAsDock.Description = (string)dockObject.GetType().GetProperty("Description").GetValue(dockObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dock.Id, locationAsDock.Id);
                Assert.Equal(dock.Name, locationAsDock.Name);
                Assert.Equal(dock.Description, locationAsDock.Description);
                Assert.Equal(dock.LocationId, locationAsDock.LocationId);
                Assert.Equal(dock.GeographicalDescription, locationAsDock.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADock_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADock_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Dock,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsDragonLair : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADragonLair_ReturnsCreateAtActionWithDragonLairDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DragonLair",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test Description"
                };

                var dragonLair = new DragonLair
                {
                    Id = 0,
                    Name = "Test DragonLair",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dragonLair);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dragonLairObject = new object();
                var locationAsDragonLair = new DragonLair();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dragonLairObject = responseAsCreateAsActionResult.Value;

                locationAsDragonLair.Id = (int)dragonLairObject.GetType().GetProperty("Id").GetValue(dragonLairObject, null);
                locationAsDragonLair.Name = (string)dragonLairObject.GetType().GetProperty("Name").GetValue(dragonLairObject, null);
                locationAsDragonLair.LocationId = (LocationType)dragonLairObject.GetType().GetProperty("LocationId").GetValue(dragonLairObject, null);
                locationAsDragonLair.GeographicalDescription = (string)dragonLairObject.GetType().GetProperty("GeographicalDescription").GetValue(dragonLairObject, null);
                locationAsDragonLair.Description = (string)dragonLairObject.GetType().GetProperty("Description").GetValue(dragonLairObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dragonLair.Id, locationAsDragonLair.Id);
                Assert.Equal(dragonLair.Name, locationAsDragonLair.Name);
                Assert.Equal(dragonLair.Description, locationAsDragonLair.Description);
                Assert.Equal(dragonLair.LocationId, locationAsDragonLair.LocationId);
                Assert.Equal(dragonLair.GeographicalDescription, locationAsDragonLair.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADragonLair_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DragonLair",
                    Description = "    ",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test Description"
                };

                var dragonLair = new DragonLair
                {
                    Id = 0,
                    Name = "Test DragonLair",
                    Description = "",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dragonLair);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dragonLairObject = new object();
                var locationAsDragonLair = new DragonLair();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dragonLairObject = responseAsCreateAsActionResult.Value;

                locationAsDragonLair.Id = (int)dragonLairObject.GetType().GetProperty("Id").GetValue(dragonLairObject, null);
                locationAsDragonLair.Name = (string)dragonLairObject.GetType().GetProperty("Name").GetValue(dragonLairObject, null);
                locationAsDragonLair.LocationId = (LocationType)dragonLairObject.GetType().GetProperty("LocationId").GetValue(dragonLairObject, null);
                locationAsDragonLair.GeographicalDescription = (string)dragonLairObject.GetType().GetProperty("GeographicalDescription").GetValue(dragonLairObject, null);
                locationAsDragonLair.Description = (string)dragonLairObject.GetType().GetProperty("Description").GetValue(dragonLairObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dragonLair.Name, locationAsDragonLair.Name);
                Assert.Equal(dragonLair.Id, locationAsDragonLair.Id);
                Assert.Equal(dragonLair.Description, locationAsDragonLair.Description);
                Assert.Equal(dragonLair.LocationId, locationAsDragonLair.LocationId);
                Assert.Equal(dragonLair.GeographicalDescription, locationAsDragonLair.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsADragonLair_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DragonLair",
                    Description = null,
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test Description"
                };

                var dragonLair = new DragonLair
                {
                    Id = 0,
                    Name = "Test DragonLair",
                    Description = null,
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dragonLair);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dragonLairObject = new object();
                var locationAsDragonLair = new DragonLair();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dragonLairObject = responseAsCreateAsActionResult.Value;

                locationAsDragonLair.Id = (int)dragonLairObject.GetType().GetProperty("Id").GetValue(dragonLairObject, null);
                locationAsDragonLair.Name = (string)dragonLairObject.GetType().GetProperty("Name").GetValue(dragonLairObject, null);
                locationAsDragonLair.LocationId = (LocationType)dragonLairObject.GetType().GetProperty("LocationId").GetValue(dragonLairObject, null);
                locationAsDragonLair.GeographicalDescription = (string)dragonLairObject.GetType().GetProperty("GeographicalDescription").GetValue(dragonLairObject, null);
                locationAsDragonLair.Description = (string)dragonLairObject.GetType().GetProperty("Description").GetValue(dragonLairObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dragonLair.Id, locationAsDragonLair.Id);
                Assert.Equal(dragonLair.Name, locationAsDragonLair.Name);
                Assert.Equal(dragonLair.Description, locationAsDragonLair.Description);
                Assert.Equal(dragonLair.LocationId, locationAsDragonLair.LocationId);
                Assert.Equal(dragonLair.GeographicalDescription, locationAsDragonLair.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADragonLair_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADragonLair_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DragonLair,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsDwarvenRuin : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADwarvenRuin_ReturnsCreateAtActionWithDwarvenRuinDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DwarvenRuin",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test Description"
                };

                var dwarvenRuin = new DwarvenRuin
                {
                    Id = 0,
                    Name = "Test DwarvenRuin",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dwarvenRuin);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dwarvenRuinObject = new object();
                var locationAsDwarvenRuin = new DwarvenRuin();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dwarvenRuinObject = responseAsCreateAsActionResult.Value;

                locationAsDwarvenRuin.Id = (int)dwarvenRuinObject.GetType().GetProperty("Id").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.Name = (string)dwarvenRuinObject.GetType().GetProperty("Name").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.LocationId = (LocationType)dwarvenRuinObject.GetType().GetProperty("LocationId").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.GeographicalDescription = (string)dwarvenRuinObject.GetType().GetProperty("GeographicalDescription").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.Description = (string)dwarvenRuinObject.GetType().GetProperty("Description").GetValue(dwarvenRuinObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dwarvenRuin.Id, locationAsDwarvenRuin.Id);
                Assert.Equal(dwarvenRuin.Name, locationAsDwarvenRuin.Name);
                Assert.Equal(dwarvenRuin.Description, locationAsDwarvenRuin.Description);
                Assert.Equal(dwarvenRuin.LocationId, locationAsDwarvenRuin.LocationId);
                Assert.Equal(dwarvenRuin.GeographicalDescription, locationAsDwarvenRuin.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADwarvenRuin_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DwarvenRuin",
                    Description = "    ",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test Description"
                };

                var dwarvenRuin = new DwarvenRuin
                {
                    Id = 0,
                    Name = "Test DwarvenRuin",
                    Description = "",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dwarvenRuin);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dwarvenRuinObject = new object();
                var locationAsDwarvenRuin = new DwarvenRuin();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dwarvenRuinObject = responseAsCreateAsActionResult.Value;

                locationAsDwarvenRuin.Id = (int)dwarvenRuinObject.GetType().GetProperty("Id").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.Name = (string)dwarvenRuinObject.GetType().GetProperty("Name").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.LocationId = (LocationType)dwarvenRuinObject.GetType().GetProperty("LocationId").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.GeographicalDescription = (string)dwarvenRuinObject.GetType().GetProperty("GeographicalDescription").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.Description = (string)dwarvenRuinObject.GetType().GetProperty("Description").GetValue(dwarvenRuinObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dwarvenRuin.Name, locationAsDwarvenRuin.Name);
                Assert.Equal(dwarvenRuin.Id, locationAsDwarvenRuin.Id);
                Assert.Equal(dwarvenRuin.Description, locationAsDwarvenRuin.Description);
                Assert.Equal(dwarvenRuin.LocationId, locationAsDwarvenRuin.LocationId);
                Assert.Equal(dwarvenRuin.GeographicalDescription, locationAsDwarvenRuin.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsADwarvenRuin_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test DwarvenRuin",
                    Description = null,
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test Description"
                };

                var dwarvenRuin = new DwarvenRuin
                {
                    Id = 0,
                    Name = "Test DwarvenRuin",
                    Description = null,
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(dwarvenRuin);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var dwarvenRuinObject = new object();
                var locationAsDwarvenRuin = new DwarvenRuin();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                dwarvenRuinObject = responseAsCreateAsActionResult.Value;

                locationAsDwarvenRuin.Id = (int)dwarvenRuinObject.GetType().GetProperty("Id").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.Name = (string)dwarvenRuinObject.GetType().GetProperty("Name").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.LocationId = (LocationType)dwarvenRuinObject.GetType().GetProperty("LocationId").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.GeographicalDescription = (string)dwarvenRuinObject.GetType().GetProperty("GeographicalDescription").GetValue(dwarvenRuinObject, null);
                locationAsDwarvenRuin.Description = (string)dwarvenRuinObject.GetType().GetProperty("Description").GetValue(dwarvenRuinObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(dwarvenRuin.Id, locationAsDwarvenRuin.Id);
                Assert.Equal(dwarvenRuin.Name, locationAsDwarvenRuin.Name);
                Assert.Equal(dwarvenRuin.Description, locationAsDwarvenRuin.Description);
                Assert.Equal(dwarvenRuin.LocationId, locationAsDwarvenRuin.LocationId);
                Assert.Equal(dwarvenRuin.GeographicalDescription, locationAsDwarvenRuin.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADwarvenRuin_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADwarvenRuin_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.DwarvenRuin,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsFarm : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAFarm_ReturnsCreateAtActionWithFarmDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test ship",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test Description"
                };

                var farm = new Farm
                {
                    Id = 0,
                    Name = "Test Farm",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(farm);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var farmObject = new object();
                var locationAsFarm = new Farm();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                farmObject = responseAsCreateAsActionResult.Value;

                locationAsFarm.Id = (int)farmObject.GetType().GetProperty("Id").GetValue(farmObject, null);
                locationAsFarm.Name = (string)farmObject.GetType().GetProperty("Name").GetValue(farmObject, null);
                locationAsFarm.LocationId = (LocationType)farmObject.GetType().GetProperty("LocationId").GetValue(farmObject, null);
                locationAsFarm.GeographicalDescription = (string)farmObject.GetType().GetProperty("GeographicalDescription").GetValue(farmObject, null);
                locationAsFarm.Description = (string)farmObject.GetType().GetProperty("Description").GetValue(farmObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(farm.Id, locationAsFarm.Id);
                Assert.Equal(farm.Name, locationAsFarm.Name);
                Assert.Equal(farm.Description, locationAsFarm.Description);
                Assert.Equal(farm.LocationId, locationAsFarm.LocationId);
                Assert.Equal(farm.GeographicalDescription, locationAsFarm.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAFarm_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Farm",
                    Description = "    ",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test Description"
                };

                var farm = new Farm
                {
                    Id = 0,
                    Name = "Test Farm",
                    Description = "",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(farm);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var farmObject = new object();
                var locationAsFarm = new Farm();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                farmObject = responseAsCreateAsActionResult.Value;

                locationAsFarm.Id = (int)farmObject.GetType().GetProperty("Id").GetValue(farmObject, null);
                locationAsFarm.Name = (string)farmObject.GetType().GetProperty("Name").GetValue(farmObject, null);
                locationAsFarm.LocationId = (LocationType)farmObject.GetType().GetProperty("LocationId").GetValue(farmObject, null);
                locationAsFarm.GeographicalDescription = (string)farmObject.GetType().GetProperty("GeographicalDescription").GetValue(farmObject, null);
                locationAsFarm.Description = (string)farmObject.GetType().GetProperty("Description").GetValue(farmObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(farm.Name, locationAsFarm.Name);
                Assert.Equal(farm.Id, locationAsFarm.Id);
                Assert.Equal(farm.Description, locationAsFarm.Description);
                Assert.Equal(farm.LocationId, locationAsFarm.LocationId);
                Assert.Equal(farm.GeographicalDescription, locationAsFarm.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAFarm_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Farm",
                    Description = null,
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test Description"
                };

                var farm = new Farm
                {
                    Id = 0,
                    Name = "Test Farm",
                    Description = null,
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(farm);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var farmObject = new object();
                var locationAsFarm = new Farm();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                farmObject = responseAsCreateAsActionResult.Value;

                locationAsFarm.Id = (int)farmObject.GetType().GetProperty("Id").GetValue(farmObject, null);
                locationAsFarm.Name = (string)farmObject.GetType().GetProperty("Name").GetValue(farmObject, null);
                locationAsFarm.LocationId = (LocationType)farmObject.GetType().GetProperty("LocationId").GetValue(farmObject, null);
                locationAsFarm.GeographicalDescription = (string)farmObject.GetType().GetProperty("GeographicalDescription").GetValue(farmObject, null);
                locationAsFarm.Description = (string)farmObject.GetType().GetProperty("Description").GetValue(farmObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(farm.Id, locationAsFarm.Id);
                Assert.Equal(farm.Name, locationAsFarm.Name);
                Assert.Equal(farm.Description, locationAsFarm.Description);
                Assert.Equal(farm.LocationId, locationAsFarm.LocationId);
                Assert.Equal(farm.GeographicalDescription, locationAsFarm.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAFarm_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAFarm_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Farm,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsFort : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAFort_ReturnsCreateAtActionWithFortDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Fort",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test Description"
                };

                var fort = new Fort
                {
                    Id = 0,
                    Name = "Test Fort",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(fort);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var fortObject = new object();
                var locationAsFort = new Fort();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                fortObject = responseAsCreateAsActionResult.Value;

                locationAsFort.Id = (int)fortObject.GetType().GetProperty("Id").GetValue(fortObject, null);
                locationAsFort.Name = (string)fortObject.GetType().GetProperty("Name").GetValue(fortObject, null);
                locationAsFort.LocationId = (LocationType)fortObject.GetType().GetProperty("LocationId").GetValue(fortObject, null);
                locationAsFort.GeographicalDescription = (string)fortObject.GetType().GetProperty("GeographicalDescription").GetValue(fortObject, null);
                locationAsFort.Description = (string)fortObject.GetType().GetProperty("Description").GetValue(fortObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(fort.Id, locationAsFort.Id);
                Assert.Equal(fort.Name, locationAsFort.Name);
                Assert.Equal(fort.Description, locationAsFort.Description);
                Assert.Equal(fort.LocationId, locationAsFort.LocationId);
                Assert.Equal(fort.GeographicalDescription, locationAsFort.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAFort_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Fort",
                    Description = "    ",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test Description"
                };

                var fort = new Fort
                {
                    Id = 0,
                    Name = "Test Fort",
                    Description = "",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(fort);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var fortObject = new object();
                var locationAsFort = new Fort();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                fortObject = responseAsCreateAsActionResult.Value;

                locationAsFort.Id = (int)fortObject.GetType().GetProperty("Id").GetValue(fortObject, null);
                locationAsFort.Name = (string)fortObject.GetType().GetProperty("Name").GetValue(fortObject, null);
                locationAsFort.LocationId = (LocationType)fortObject.GetType().GetProperty("LocationId").GetValue(fortObject, null);
                locationAsFort.GeographicalDescription = (string)fortObject.GetType().GetProperty("GeographicalDescription").GetValue(fortObject, null);
                locationAsFort.Description = (string)fortObject.GetType().GetProperty("Description").GetValue(fortObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(fort.Name, locationAsFort.Name);
                Assert.Equal(fort.Id, locationAsFort.Id);
                Assert.Equal(fort.Description, locationAsFort.Description);
                Assert.Equal(fort.LocationId, locationAsFort.LocationId);
                Assert.Equal(fort.GeographicalDescription, locationAsFort.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAFort_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Fort",
                    Description = null,
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test Description"
                };

                var fort = new Fort
                {
                    Id = 0,
                    Name = "Test Fort",
                    Description = null,
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(fort);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var fortObject = new object();
                var locationAsFort = new Fort();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                fortObject = responseAsCreateAsActionResult.Value;

                locationAsFort.Id = (int)fortObject.GetType().GetProperty("Id").GetValue(fortObject, null);
                locationAsFort.Name = (string)fortObject.GetType().GetProperty("Name").GetValue(fortObject, null);
                locationAsFort.LocationId = (LocationType)fortObject.GetType().GetProperty("LocationId").GetValue(fortObject, null);
                locationAsFort.GeographicalDescription = (string)fortObject.GetType().GetProperty("GeographicalDescription").GetValue(fortObject, null);
                locationAsFort.Description = (string)fortObject.GetType().GetProperty("Description").GetValue(fortObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(fort.Id, locationAsFort.Id);
                Assert.Equal(fort.Name, locationAsFort.Name);
                Assert.Equal(fort.Description, locationAsFort.Description);
                Assert.Equal(fort.LocationId, locationAsFort.LocationId);
                Assert.Equal(fort.GeographicalDescription, locationAsFort.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAFort_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAFort_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Fort,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsGiantCamp : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAGiantCamp_ReturnsCreateAtActionWithGiantCampDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test GiantCamp",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test Description"
                };

                var giantCamp = new GiantCamp
                {
                    Id = 0,
                    Name = "Test GiantCamp",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(giantCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var giantCampObject = new object();
                var locationAsGiantCamp = new GiantCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                giantCampObject = responseAsCreateAsActionResult.Value;

                locationAsGiantCamp.Id = (int)giantCampObject.GetType().GetProperty("Id").GetValue(giantCampObject, null);
                locationAsGiantCamp.Name = (string)giantCampObject.GetType().GetProperty("Name").GetValue(giantCampObject, null);
                locationAsGiantCamp.LocationId = (LocationType)giantCampObject.GetType().GetProperty("LocationId").GetValue(giantCampObject, null);
                locationAsGiantCamp.GeographicalDescription = (string)giantCampObject.GetType().GetProperty("GeographicalDescription").GetValue(giantCampObject, null);
                locationAsGiantCamp.Description = (string)giantCampObject.GetType().GetProperty("Description").GetValue(giantCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(giantCamp.Id, locationAsGiantCamp.Id);
                Assert.Equal(giantCamp.Name, locationAsGiantCamp.Name);
                Assert.Equal(giantCamp.Description, locationAsGiantCamp.Description);
                Assert.Equal(giantCamp.LocationId, locationAsGiantCamp.LocationId);
                Assert.Equal(giantCamp.GeographicalDescription, locationAsGiantCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAGiantCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test GiantCamp",
                    Description = "    ",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test Description"
                };

                var giantCamp = new GiantCamp
                {
                    Id = 0,
                    Name = "Test GiantCamp",
                    Description = "",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(giantCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var giantCampObject = new object();
                var locationAsGiantCamp = new Fort();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                giantCampObject = responseAsCreateAsActionResult.Value;

                locationAsGiantCamp.Id = (int)giantCampObject.GetType().GetProperty("Id").GetValue(giantCampObject, null);
                locationAsGiantCamp.Name = (string)giantCampObject.GetType().GetProperty("Name").GetValue(giantCampObject, null);
                locationAsGiantCamp.LocationId = (LocationType)giantCampObject.GetType().GetProperty("LocationId").GetValue(giantCampObject, null);
                locationAsGiantCamp.GeographicalDescription = (string)giantCampObject.GetType().GetProperty("GeographicalDescription").GetValue(giantCampObject, null);
                locationAsGiantCamp.Description = (string)giantCampObject.GetType().GetProperty("Description").GetValue(giantCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(giantCamp.Name, locationAsGiantCamp.Name);
                Assert.Equal(giantCamp.Id, locationAsGiantCamp.Id);
                Assert.Equal(giantCamp.Description, locationAsGiantCamp.Description);
                Assert.Equal(giantCamp.LocationId, locationAsGiantCamp.LocationId);
                Assert.Equal(giantCamp.GeographicalDescription, locationAsGiantCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAGiantCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test GiantCamp",
                    Description = null,
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test Description"
                };

                var giantCamp = new GiantCamp
                {
                    Id = 0,
                    Name = "Test GiantCamp",
                    Description = null,
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(giantCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var giantCampObject = new object();
                var locationAsGiantCamp = new GiantCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                giantCampObject = responseAsCreateAsActionResult.Value;

                locationAsGiantCamp.Id = (int)giantCampObject.GetType().GetProperty("Id").GetValue(giantCampObject, null);
                locationAsGiantCamp.Name = (string)giantCampObject.GetType().GetProperty("Name").GetValue(giantCampObject, null);
                locationAsGiantCamp.LocationId = (LocationType)giantCampObject.GetType().GetProperty("LocationId").GetValue(giantCampObject, null);
                locationAsGiantCamp.GeographicalDescription = (string)giantCampObject.GetType().GetProperty("GeographicalDescription").GetValue(giantCampObject, null);
                locationAsGiantCamp.Description = (string)giantCampObject.GetType().GetProperty("Description").GetValue(giantCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(giantCamp.Id, locationAsGiantCamp.Id);
                Assert.Equal(giantCamp.Name, locationAsGiantCamp.Name);
                Assert.Equal(giantCamp.Description, locationAsGiantCamp.Description);
                Assert.Equal(giantCamp.LocationId, locationAsGiantCamp.LocationId);
                Assert.Equal(giantCamp.GeographicalDescription, locationAsGiantCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAGiantCamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAGiantCamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.GiantCamp,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsGrove : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAGrove_ReturnsCreateAtActionWithGroveDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Grove",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test Description"
                };

                var grove = new Grove
                {
                    Id = 0,
                    Name = "Test Grove",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(grove);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var groveObject = new object();
                var locationAsGrove = new Grove();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                groveObject = responseAsCreateAsActionResult.Value;

                locationAsGrove.Id = (int)groveObject.GetType().GetProperty("Id").GetValue(groveObject, null);
                locationAsGrove.Name = (string)groveObject.GetType().GetProperty("Name").GetValue(groveObject, null);
                locationAsGrove.LocationId = (LocationType)groveObject.GetType().GetProperty("LocationId").GetValue(groveObject, null);
                locationAsGrove.GeographicalDescription = (string)groveObject.GetType().GetProperty("GeographicalDescription").GetValue(groveObject, null);
                locationAsGrove.Description = (string)groveObject.GetType().GetProperty("Description").GetValue(groveObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(grove.Id, locationAsGrove.Id);
                Assert.Equal(grove.Name, locationAsGrove.Name);
                Assert.Equal(grove.Description, locationAsGrove.Description);
                Assert.Equal(grove.LocationId, locationAsGrove.LocationId);
                Assert.Equal(grove.GeographicalDescription, locationAsGrove.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAGrove_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Grove",
                    Description = "    ",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test Description"
                };

                var grove = new Grove
                {
                    Id = 0,
                    Name = "Test Grove",
                    Description = "",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(grove);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var groveObject = new object();
                var locationAsGrove = new Grove();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                groveObject = responseAsCreateAsActionResult.Value;

                locationAsGrove.Id = (int)groveObject.GetType().GetProperty("Id").GetValue(groveObject, null);
                locationAsGrove.Name = (string)groveObject.GetType().GetProperty("Name").GetValue(groveObject, null);
                locationAsGrove.LocationId = (LocationType)groveObject.GetType().GetProperty("LocationId").GetValue(groveObject, null);
                locationAsGrove.GeographicalDescription = (string)groveObject.GetType().GetProperty("GeographicalDescription").GetValue(groveObject, null);
                locationAsGrove.Description = (string)groveObject.GetType().GetProperty("Description").GetValue(groveObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(grove.Name, locationAsGrove.Name);
                Assert.Equal(grove.Id, locationAsGrove.Id);
                Assert.Equal(grove.Description, locationAsGrove.Description);
                Assert.Equal(grove.LocationId, locationAsGrove.LocationId);
                Assert.Equal(grove.GeographicalDescription, locationAsGrove.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAGrove_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Grove",
                    Description = null,
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test Description"
                };

                var grove = new Grove
                {
                    Id = 0,
                    Name = "Test Grove",
                    Description = null,
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(grove);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var groveObject = new object();
                var locationAsGrove = new Grove();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                groveObject = responseAsCreateAsActionResult.Value;

                locationAsGrove.Id = (int)groveObject.GetType().GetProperty("Id").GetValue(groveObject, null);
                locationAsGrove.Name = (string)groveObject.GetType().GetProperty("Name").GetValue(groveObject, null);
                locationAsGrove.LocationId = (LocationType)groveObject.GetType().GetProperty("LocationId").GetValue(groveObject, null);
                locationAsGrove.GeographicalDescription = (string)groveObject.GetType().GetProperty("GeographicalDescription").GetValue(groveObject, null);
                locationAsGrove.Description = (string)groveObject.GetType().GetProperty("Description").GetValue(groveObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(grove.Id, locationAsGrove.Id);
                Assert.Equal(grove.Name, locationAsGrove.Name);
                Assert.Equal(grove.Description, locationAsGrove.Description);
                Assert.Equal(grove.LocationId, locationAsGrove.LocationId);
                Assert.Equal(grove.GeographicalDescription, locationAsGrove.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAGrove_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAGrove_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Grove,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsImperialCamp : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAnImperialCamp_ReturnsCreateAtActionWithImperialCampDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test ImperialCamp",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test Description"
                };

                var imperialCamp = new ImperialCamp
                {
                    Id = 0,
                    Name = "Test ImperialCamp",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(imperialCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var imperialCampObject = new object();
                var locationAsImperialCamp = new ImperialCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                imperialCampObject = responseAsCreateAsActionResult.Value;

                locationAsImperialCamp.Id = (int)imperialCampObject.GetType().GetProperty("Id").GetValue(imperialCampObject, null);
                locationAsImperialCamp.Name = (string)imperialCampObject.GetType().GetProperty("Name").GetValue(imperialCampObject, null);
                locationAsImperialCamp.LocationId = (LocationType)imperialCampObject.GetType().GetProperty("LocationId").GetValue(imperialCampObject, null);
                locationAsImperialCamp.GeographicalDescription = (string)imperialCampObject.GetType().GetProperty("GeographicalDescription").GetValue(imperialCampObject, null);
                locationAsImperialCamp.Description = (string)imperialCampObject.GetType().GetProperty("Description").GetValue(imperialCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(imperialCamp.Id, locationAsImperialCamp.Id);
                Assert.Equal(imperialCamp.Name, locationAsImperialCamp.Name);
                Assert.Equal(imperialCamp.Description, locationAsImperialCamp.Description);
                Assert.Equal(imperialCamp.LocationId, locationAsImperialCamp.LocationId);
                Assert.Equal(imperialCamp.GeographicalDescription, locationAsImperialCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAImperialCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test ImperialCamp",
                    Description = "    ",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test Description"
                };

                var imperialCamp = new ImperialCamp
                {
                    Id = 0,
                    Name = "Test ImperialCamp",
                    Description = "",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(imperialCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var imperialCampObject = new object();
                var locationAsImperialCamp = new ImperialCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                imperialCampObject = responseAsCreateAsActionResult.Value;

                locationAsImperialCamp.Id = (int)imperialCampObject.GetType().GetProperty("Id").GetValue(imperialCampObject, null);
                locationAsImperialCamp.Name = (string)imperialCampObject.GetType().GetProperty("Name").GetValue(imperialCampObject, null);
                locationAsImperialCamp.LocationId = (LocationType)imperialCampObject.GetType().GetProperty("LocationId").GetValue(imperialCampObject, null);
                locationAsImperialCamp.GeographicalDescription = (string)imperialCampObject.GetType().GetProperty("GeographicalDescription").GetValue(imperialCampObject, null);
                locationAsImperialCamp.Description = (string)imperialCampObject.GetType().GetProperty("Description").GetValue(imperialCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(imperialCamp.Name, locationAsImperialCamp.Name);
                Assert.Equal(imperialCamp.Id, locationAsImperialCamp.Id);
                Assert.Equal(imperialCamp.Description, locationAsImperialCamp.Description);
                Assert.Equal(imperialCamp.LocationId, locationAsImperialCamp.LocationId);
                Assert.Equal(imperialCamp.GeographicalDescription, locationAsImperialCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAImperialCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test ImperialCamp",
                    Description = null,
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test Description"
                };

                var imperialCamp = new ImperialCamp
                {
                    Id = 0,
                    Name = "Test ImperialCamp",
                    Description = null,
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(imperialCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var imperialCampObject = new object();
                var locationAsImperialCamp = new ImperialCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                imperialCampObject = responseAsCreateAsActionResult.Value;

                locationAsImperialCamp.Id = (int)imperialCampObject.GetType().GetProperty("Id").GetValue(imperialCampObject, null);
                locationAsImperialCamp.Name = (string)imperialCampObject.GetType().GetProperty("Name").GetValue(imperialCampObject, null);
                locationAsImperialCamp.LocationId = (LocationType)imperialCampObject.GetType().GetProperty("LocationId").GetValue(imperialCampObject, null);
                locationAsImperialCamp.GeographicalDescription = (string)imperialCampObject.GetType().GetProperty("GeographicalDescription").GetValue(imperialCampObject, null);
                locationAsImperialCamp.Description = (string)imperialCampObject.GetType().GetProperty("Description").GetValue(imperialCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(imperialCamp.Id, locationAsImperialCamp.Id);
                Assert.Equal(imperialCamp.Name, locationAsImperialCamp.Name);
                Assert.Equal(imperialCamp.Description, locationAsImperialCamp.Description);
                Assert.Equal(imperialCamp.LocationId, locationAsImperialCamp.LocationId);
                Assert.Equal(imperialCamp.GeographicalDescription, locationAsImperialCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAImperialCamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAImperialCamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.ImperialCamp,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsLightHouse : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsALightHouse_ReturnsCreateAtActionWithLightHouseDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test LightHouse",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test Description"
                };

                var lightHouse = new LightHouse
                {
                    Id = 0,
                    Name = "Test LightHouse",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(lightHouse);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var lightHouseObject = new object();
                var locationAsLightHouse = new LightHouse();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                lightHouseObject = responseAsCreateAsActionResult.Value;

                locationAsLightHouse.Id = (int)lightHouseObject.GetType().GetProperty("Id").GetValue(lightHouseObject, null);
                locationAsLightHouse.Name = (string)lightHouseObject.GetType().GetProperty("Name").GetValue(lightHouseObject, null);
                locationAsLightHouse.LocationId = (LocationType)lightHouseObject.GetType().GetProperty("LocationId").GetValue(lightHouseObject, null);
                locationAsLightHouse.GeographicalDescription = (string)lightHouseObject.GetType().GetProperty("GeographicalDescription").GetValue(lightHouseObject, null);
                locationAsLightHouse.Description = (string)lightHouseObject.GetType().GetProperty("Description").GetValue(lightHouseObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(lightHouse.Id, locationAsLightHouse.Id);
                Assert.Equal(lightHouse.Name, locationAsLightHouse.Name);
                Assert.Equal(lightHouse.Description, locationAsLightHouse.Description);
                Assert.Equal(lightHouse.LocationId, locationAsLightHouse.LocationId);
                Assert.Equal(lightHouse.GeographicalDescription, locationAsLightHouse.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionALightHouse_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test LightHouse",
                    Description = "    ",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test Description"
                };

                var lightHouse = new LightHouse
                {
                    Id = 0,
                    Name = "Test LightHouse",
                    Description = "",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(lightHouse);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var lightHouseObject = new object();
                var locationAsLightHouse = new LightHouse();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                lightHouseObject = responseAsCreateAsActionResult.Value;

                locationAsLightHouse.Id = (int)lightHouseObject.GetType().GetProperty("Id").GetValue(lightHouseObject, null);
                locationAsLightHouse.Name = (string)lightHouseObject.GetType().GetProperty("Name").GetValue(lightHouseObject, null);
                locationAsLightHouse.LocationId = (LocationType)lightHouseObject.GetType().GetProperty("LocationId").GetValue(lightHouseObject, null);
                locationAsLightHouse.GeographicalDescription = (string)lightHouseObject.GetType().GetProperty("GeographicalDescription").GetValue(lightHouseObject, null);
                locationAsLightHouse.Description = (string)lightHouseObject.GetType().GetProperty("Description").GetValue(lightHouseObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(lightHouse.Name, locationAsLightHouse.Name);
                Assert.Equal(lightHouse.Id, locationAsLightHouse.Id);
                Assert.Equal(lightHouse.Description, locationAsLightHouse.Description);
                Assert.Equal(lightHouse.LocationId, locationAsLightHouse.LocationId);
                Assert.Equal(lightHouse.GeographicalDescription, locationAsLightHouse.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsALightHouse_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test LightHouse",
                    Description = null,
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test Description"
                };

                var lightHouse = new LightHouse
                {
                    Id = 0,
                    Name = "Test LightHouse",
                    Description = null,
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(lightHouse);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var lightHouseObject = new object();
                var locationAsLightHouse = new LightHouse();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                lightHouseObject = responseAsCreateAsActionResult.Value;

                locationAsLightHouse.Id = (int)lightHouseObject.GetType().GetProperty("Id").GetValue(lightHouseObject, null);
                locationAsLightHouse.Name = (string)lightHouseObject.GetType().GetProperty("Name").GetValue(lightHouseObject, null);
                locationAsLightHouse.LocationId = (LocationType)lightHouseObject.GetType().GetProperty("LocationId").GetValue(lightHouseObject, null);
                locationAsLightHouse.GeographicalDescription = (string)lightHouseObject.GetType().GetProperty("GeographicalDescription").GetValue(lightHouseObject, null);
                locationAsLightHouse.Description = (string)lightHouseObject.GetType().GetProperty("Description").GetValue(lightHouseObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(lightHouse.Id, locationAsLightHouse.Id);
                Assert.Equal(lightHouse.Name, locationAsLightHouse.Name);
                Assert.Equal(lightHouse.Description, locationAsLightHouse.Description);
                Assert.Equal(lightHouse.LocationId, locationAsLightHouse.LocationId);
                Assert.Equal(lightHouse.GeographicalDescription, locationAsLightHouse.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsALightHouse_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsALightHouse_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.LightHouse,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsMine : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAMine_ReturnsCreateAtActionWithMineDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Mine",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test Description"
                };

                var mine = new Mine
                {
                    Id = 0,
                    Name = "Test Mine",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(mine);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var mineObject = new object();
                var locationAsMine = new Mine();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                mineObject = responseAsCreateAsActionResult.Value;

                locationAsMine.Id = (int)mineObject.GetType().GetProperty("Id").GetValue(mineObject, null);
                locationAsMine.Name = (string)mineObject.GetType().GetProperty("Name").GetValue(mineObject, null);
                locationAsMine.LocationId = (LocationType)mineObject.GetType().GetProperty("LocationId").GetValue(mineObject, null);
                locationAsMine.GeographicalDescription = (string)mineObject.GetType().GetProperty("GeographicalDescription").GetValue(mineObject, null);
                locationAsMine.Description = (string)mineObject.GetType().GetProperty("Description").GetValue(mineObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(mine.Id, locationAsMine.Id);
                Assert.Equal(mine.Name, locationAsMine.Name);
                Assert.Equal(mine.Description, locationAsMine.Description);
                Assert.Equal(mine.LocationId, locationAsMine.LocationId);
                Assert.Equal(mine.GeographicalDescription, locationAsMine.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAMine_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Mine",
                    Description = "    ",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test Description"
                };

                var mine = new Mine
                {
                    Id = 0,
                    Name = "Test Mine",
                    Description = "",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(mine);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var mineObject = new object();
                var locationAsMine = new Mine();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                mineObject = responseAsCreateAsActionResult.Value;

                locationAsMine.Id = (int)mineObject.GetType().GetProperty("Id").GetValue(mineObject, null);
                locationAsMine.Name = (string)mineObject.GetType().GetProperty("Name").GetValue(mineObject, null);
                locationAsMine.LocationId = (LocationType)mineObject.GetType().GetProperty("LocationId").GetValue(mineObject, null);
                locationAsMine.GeographicalDescription = (string)mineObject.GetType().GetProperty("GeographicalDescription").GetValue(mineObject, null);
                locationAsMine.Description = (string)mineObject.GetType().GetProperty("Description").GetValue(mineObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(mine.Name, locationAsMine.Name);
                Assert.Equal(mine.Id, locationAsMine.Id);
                Assert.Equal(mine.Description, locationAsMine.Description);
                Assert.Equal(mine.LocationId, locationAsMine.LocationId);
                Assert.Equal(mine.GeographicalDescription, locationAsMine.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAMine_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Mine",
                    Description = null,
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test Description"
                };

                var mine = new Mine
                {
                    Id = 0,
                    Name = "Test Mine",
                    Description = null,
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(mine);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var mineObject = new object();
                var locationAsMine = new Mine();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                mineObject = responseAsCreateAsActionResult.Value;

                locationAsMine.Id = (int)mineObject.GetType().GetProperty("Id").GetValue(mineObject, null);
                locationAsMine.Name = (string)mineObject.GetType().GetProperty("Name").GetValue(mineObject, null);
                locationAsMine.LocationId = (LocationType)mineObject.GetType().GetProperty("LocationId").GetValue(mineObject, null);
                locationAsMine.GeographicalDescription = (string)mineObject.GetType().GetProperty("GeographicalDescription").GetValue(mineObject, null);
                locationAsMine.Description = (string)mineObject.GetType().GetProperty("Description").GetValue(mineObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(mine.Id, locationAsMine.Id);
                Assert.Equal(mine.Name, locationAsMine.Name);
                Assert.Equal(mine.Description, locationAsMine.Description);
                Assert.Equal(mine.LocationId, locationAsMine.LocationId);
                Assert.Equal(mine.GeographicalDescription, locationAsMine.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAMine_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAMine_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Mine,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsNordicTower : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsANordicTower_ReturnsCreateAtActionWithNordicTowerDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test NordicTower",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test Description"
                };

                var nordicTower = new NordicTower
                {
                    Id = 0,
                    Name = "Test NordicTower",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(nordicTower);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var nordicTowerObject = new object();
                var locationAsNordicTower = new NordicTower();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                nordicTowerObject = responseAsCreateAsActionResult.Value;

                locationAsNordicTower.Id = (int)nordicTowerObject.GetType().GetProperty("Id").GetValue(nordicTowerObject, null);
                locationAsNordicTower.Name = (string)nordicTowerObject.GetType().GetProperty("Name").GetValue(nordicTowerObject, null);
                locationAsNordicTower.LocationId = (LocationType)nordicTowerObject.GetType().GetProperty("LocationId").GetValue(nordicTowerObject, null);
                locationAsNordicTower.GeographicalDescription = (string)nordicTowerObject.GetType().GetProperty("GeographicalDescription").GetValue(nordicTowerObject, null);
                locationAsNordicTower.Description = (string)nordicTowerObject.GetType().GetProperty("Description").GetValue(nordicTowerObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(nordicTower.Id, locationAsNordicTower.Id);
                Assert.Equal(nordicTower.Name, locationAsNordicTower.Name);
                Assert.Equal(nordicTower.Description, locationAsNordicTower.Description);
                Assert.Equal(nordicTower.LocationId, locationAsNordicTower.LocationId);
                Assert.Equal(nordicTower.GeographicalDescription, locationAsNordicTower.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionANordicTower_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test NordicTower",
                    Description = "    ",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test Description"
                };

                var nordicTower = new NordicTower
                {
                    Id = 0,
                    Name = "Test NordicTower",
                    Description = "",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(nordicTower);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var nordicTowerObject = new object();
                var locationAsNordicTower = new NordicTower();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                nordicTowerObject = responseAsCreateAsActionResult.Value;

                locationAsNordicTower.Id = (int)nordicTowerObject.GetType().GetProperty("Id").GetValue(nordicTowerObject, null);
                locationAsNordicTower.Name = (string)nordicTowerObject.GetType().GetProperty("Name").GetValue(nordicTowerObject, null);
                locationAsNordicTower.LocationId = (LocationType)nordicTowerObject.GetType().GetProperty("LocationId").GetValue(nordicTowerObject, null);
                locationAsNordicTower.GeographicalDescription = (string)nordicTowerObject.GetType().GetProperty("GeographicalDescription").GetValue(nordicTowerObject, null);
                locationAsNordicTower.Description = (string)nordicTowerObject.GetType().GetProperty("Description").GetValue(nordicTowerObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(nordicTower.Name, locationAsNordicTower.Name);
                Assert.Equal(nordicTower.Id, locationAsNordicTower.Id);
                Assert.Equal(nordicTower.Description, locationAsNordicTower.Description);
                Assert.Equal(nordicTower.LocationId, locationAsNordicTower.LocationId);
                Assert.Equal(nordicTower.GeographicalDescription, locationAsNordicTower.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsANordicTower_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test NordicTower",
                    Description = null,
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test Description"
                };

                var nordicTower = new NordicTower
                {
                    Id = 0,
                    Name = "Test NordicTower",
                    Description = null,
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(nordicTower);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var nordicTowerObject = new object();
                var locationAsNordicTower = new NordicTower();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                nordicTowerObject = responseAsCreateAsActionResult.Value;

                locationAsNordicTower.Id = (int)nordicTowerObject.GetType().GetProperty("Id").GetValue(nordicTowerObject, null);
                locationAsNordicTower.Name = (string)nordicTowerObject.GetType().GetProperty("Name").GetValue(nordicTowerObject, null);
                locationAsNordicTower.LocationId = (LocationType)nordicTowerObject.GetType().GetProperty("LocationId").GetValue(nordicTowerObject, null);
                locationAsNordicTower.GeographicalDescription = (string)nordicTowerObject.GetType().GetProperty("GeographicalDescription").GetValue(nordicTowerObject, null);
                locationAsNordicTower.Description = (string)nordicTowerObject.GetType().GetProperty("Description").GetValue(nordicTowerObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(nordicTower.Id, locationAsNordicTower.Id);
                Assert.Equal(nordicTower.Name, locationAsNordicTower.Name);
                Assert.Equal(nordicTower.Description, locationAsNordicTower.Description);
                Assert.Equal(nordicTower.LocationId, locationAsNordicTower.LocationId);
                Assert.Equal(nordicTower.GeographicalDescription, locationAsNordicTower.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsANordicTower_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsANordicTowerReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.NordicTower,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsOrcStronghold : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAnOrcStronghold_ReturnsCreateAtActionWithOrcStrongholdDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test OrcStronghold",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test Description"
                };

                var orcStronghold = new OrcStronghold
                {
                    Id = 0,
                    Name = "Test OrcStronghold",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(orcStronghold);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var orcStrongholdObject = new object();
                var locationAsOrcStronghold = new OrcStronghold();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                orcStrongholdObject = responseAsCreateAsActionResult.Value;

                locationAsOrcStronghold.Id = (int)orcStrongholdObject.GetType().GetProperty("Id").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.Name = (string)orcStrongholdObject.GetType().GetProperty("Name").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.LocationId = (LocationType)orcStrongholdObject.GetType().GetProperty("LocationId").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.GeographicalDescription = (string)orcStrongholdObject.GetType().GetProperty("GeographicalDescription").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.Description = (string)orcStrongholdObject.GetType().GetProperty("Description").GetValue(orcStrongholdObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(orcStronghold.Id, locationAsOrcStronghold.Id);
                Assert.Equal(orcStronghold.Name, locationAsOrcStronghold.Name);
                Assert.Equal(orcStronghold.Description, locationAsOrcStronghold.Description);
                Assert.Equal(orcStronghold.LocationId, locationAsOrcStronghold.LocationId);
                Assert.Equal(orcStronghold.GeographicalDescription, locationAsOrcStronghold.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAOrcStronghold_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test OrcStronghold",
                    Description = "    ",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test Description"
                };

                var orcStronghold = new OrcStronghold
                {
                    Id = 0,
                    Name = "Test OrcStronghold",
                    Description = "",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(orcStronghold);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var orcStrongholdObject = new object();
                var locationAsOrcStronghold = new OrcStronghold();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                orcStrongholdObject = responseAsCreateAsActionResult.Value;

                locationAsOrcStronghold.Id = (int)orcStrongholdObject.GetType().GetProperty("Id").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.Name = (string)orcStrongholdObject.GetType().GetProperty("Name").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.LocationId = (LocationType)orcStrongholdObject.GetType().GetProperty("LocationId").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.GeographicalDescription = (string)orcStrongholdObject.GetType().GetProperty("GeographicalDescription").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.Description = (string)orcStrongholdObject.GetType().GetProperty("Description").GetValue(orcStrongholdObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(orcStronghold.Name, locationAsOrcStronghold.Name);
                Assert.Equal(orcStronghold.Id, locationAsOrcStronghold.Id);
                Assert.Equal(orcStronghold.Description, locationAsOrcStronghold.Description);
                Assert.Equal(orcStronghold.LocationId, locationAsOrcStronghold.LocationId);
                Assert.Equal(orcStronghold.GeographicalDescription, locationAsOrcStronghold.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAnOrcStronghold_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test OrcStronghold",
                    Description = null,
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test Description"
                };

                var orcStronghold = new OrcStronghold
                {
                    Id = 0,
                    Name = "Test OrcStronghold",
                    Description = null,
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(orcStronghold);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var orcStrongholdObject = new object();
                var locationAsOrcStronghold = new OrcStronghold();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                orcStrongholdObject = responseAsCreateAsActionResult.Value;

                locationAsOrcStronghold.Id = (int)orcStrongholdObject.GetType().GetProperty("Id").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.Name = (string)orcStrongholdObject.GetType().GetProperty("Name").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.LocationId = (LocationType)orcStrongholdObject.GetType().GetProperty("LocationId").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.GeographicalDescription = (string)orcStrongholdObject.GetType().GetProperty("GeographicalDescription").GetValue(orcStrongholdObject, null);
                locationAsOrcStronghold.Description = (string)orcStrongholdObject.GetType().GetProperty("Description").GetValue(orcStrongholdObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(orcStronghold.Id, locationAsOrcStronghold.Id);
                Assert.Equal(orcStronghold.Name, locationAsOrcStronghold.Name);
                Assert.Equal(orcStronghold.Description, locationAsOrcStronghold.Description);
                Assert.Equal(orcStronghold.LocationId, locationAsOrcStronghold.LocationId);
                Assert.Equal(orcStronghold.GeographicalDescription, locationAsOrcStronghold.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAnOrcStronghold_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAOrcStronghold_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.OrcStronghold,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsPass : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAPass_ReturnsCreateAtActionWithPassDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Pass",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test Description"
                };

                var pass = new Pass
                {
                    Id = 0,
                    Name = "Test Pass",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(pass);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var passObject = new object();
                var locationAsPass = new Pass();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                passObject = responseAsCreateAsActionResult.Value;

                locationAsPass.Id = (int)passObject.GetType().GetProperty("Id").GetValue(passObject, null);
                locationAsPass.Name = (string)passObject.GetType().GetProperty("Name").GetValue(passObject, null);
                locationAsPass.LocationId = (LocationType)passObject.GetType().GetProperty("LocationId").GetValue(passObject, null);
                locationAsPass.GeographicalDescription = (string)passObject.GetType().GetProperty("GeographicalDescription").GetValue(passObject, null);
                locationAsPass.Description = (string)passObject.GetType().GetProperty("Description").GetValue(passObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(pass.Id, locationAsPass.Id);
                Assert.Equal(pass.Name, locationAsPass.Name);
                Assert.Equal(pass.Description, locationAsPass.Description);
                Assert.Equal(pass.LocationId, locationAsPass.LocationId);
                Assert.Equal(pass.GeographicalDescription, locationAsPass.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAPass_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Pass",
                    Description = "    ",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test Description"
                };

                var pass = new Pass
                {
                    Id = 0,
                    Name = "Test Pass",
                    Description = "",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(pass);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var passObject = new object();
                var locationAsPass = new Pass();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                passObject = responseAsCreateAsActionResult.Value;

                locationAsPass.Id = (int)passObject.GetType().GetProperty("Id").GetValue(passObject, null);
                locationAsPass.Name = (string)passObject.GetType().GetProperty("Name").GetValue(passObject, null);
                locationAsPass.LocationId = (LocationType)passObject.GetType().GetProperty("LocationId").GetValue(passObject, null);
                locationAsPass.GeographicalDescription = (string)passObject.GetType().GetProperty("GeographicalDescription").GetValue(passObject, null);
                locationAsPass.Description = (string)passObject.GetType().GetProperty("Description").GetValue(passObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(pass.Name, locationAsPass.Name);
                Assert.Equal(pass.Id, locationAsPass.Id);
                Assert.Equal(pass.Description, locationAsPass.Description);
                Assert.Equal(pass.LocationId, locationAsPass.LocationId);
                Assert.Equal(pass.GeographicalDescription, locationAsPass.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAPass_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Pass",
                    Description = null,
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test Description"
                };

                var pass = new Pass
                {
                    Id = 0,
                    Name = "Test Pass",
                    Description = null,
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(pass);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var passObject = new object();
                var locationAsPass = new Pass();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                passObject = responseAsCreateAsActionResult.Value;

                locationAsPass.Id = (int)passObject.GetType().GetProperty("Id").GetValue(passObject, null);
                locationAsPass.Name = (string)passObject.GetType().GetProperty("Name").GetValue(passObject, null);
                locationAsPass.LocationId = (LocationType)passObject.GetType().GetProperty("LocationId").GetValue(passObject, null);
                locationAsPass.GeographicalDescription = (string)passObject.GetType().GetProperty("GeographicalDescription").GetValue(passObject, null);
                locationAsPass.Description = (string)passObject.GetType().GetProperty("Description").GetValue(passObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(pass.Id, locationAsPass.Id);
                Assert.Equal(pass.Name, locationAsPass.Name);
                Assert.Equal(pass.Description, locationAsPass.Description);
                Assert.Equal(pass.LocationId, locationAsPass.LocationId);
                Assert.Equal(pass.GeographicalDescription, locationAsPass.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAPass_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAPass_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Pass,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsRuin : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsARuin_ReturnsCreateAtActionWithRuinDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Ruin",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test Description"
                };

                var ruin = new Ruin
                {
                    Id = 0,
                    Name = "Test Ruin",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(ruin);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var ruinObject = new object();
                var locationAsRuin = new Ruin();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                ruinObject = responseAsCreateAsActionResult.Value;

                locationAsRuin.Id = (int)ruinObject.GetType().GetProperty("Id").GetValue(ruinObject, null);
                locationAsRuin.Name = (string)ruinObject.GetType().GetProperty("Name").GetValue(ruinObject, null);
                locationAsRuin.LocationId = (LocationType)ruinObject.GetType().GetProperty("LocationId").GetValue(ruinObject, null);
                locationAsRuin.GeographicalDescription = (string)ruinObject.GetType().GetProperty("GeographicalDescription").GetValue(ruinObject, null);
                locationAsRuin.Description = (string)ruinObject.GetType().GetProperty("Description").GetValue(ruinObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(ruin.Id, locationAsRuin.Id);
                Assert.Equal(ruin.Name, locationAsRuin.Name);
                Assert.Equal(ruin.Description, locationAsRuin.Description);
                Assert.Equal(ruin.LocationId, locationAsRuin.LocationId);
                Assert.Equal(ruin.GeographicalDescription, locationAsRuin.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionARuin_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Ruin",
                    Description = "    ",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test Description"
                };

                var ruin = new Ruin
                {
                    Id = 0,
                    Name = "Test Ruin",
                    Description = "",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(ruin);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var ruinObject = new object();
                var locationAsRuin = new Ruin();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                ruinObject = responseAsCreateAsActionResult.Value;

                locationAsRuin.Id = (int)ruinObject.GetType().GetProperty("Id").GetValue(ruinObject, null);
                locationAsRuin.Name = (string)ruinObject.GetType().GetProperty("Name").GetValue(ruinObject, null);
                locationAsRuin.LocationId = (LocationType)ruinObject.GetType().GetProperty("LocationId").GetValue(ruinObject, null);
                locationAsRuin.GeographicalDescription = (string)ruinObject.GetType().GetProperty("GeographicalDescription").GetValue(ruinObject, null);
                locationAsRuin.Description = (string)ruinObject.GetType().GetProperty("Description").GetValue(ruinObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(ruin.Name, locationAsRuin.Name);
                Assert.Equal(ruin.Id, locationAsRuin.Id);
                Assert.Equal(ruin.Description, locationAsRuin.Description);
                Assert.Equal(ruin.LocationId, locationAsRuin.LocationId);
                Assert.Equal(ruin.GeographicalDescription, locationAsRuin.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsARuin_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Ruin",
                    Description = null,
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test Description"
                };

                var ruin = new Ruin
                {
                    Id = 0,
                    Name = "Test Ruin",
                    Description = null,
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(ruin);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var ruinObject = new object();
                var locationAsRuin = new Ruin();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                ruinObject = responseAsCreateAsActionResult.Value;

                locationAsRuin.Id = (int)ruinObject.GetType().GetProperty("Id").GetValue(ruinObject, null);
                locationAsRuin.Name = (string)ruinObject.GetType().GetProperty("Name").GetValue(ruinObject, null);
                locationAsRuin.LocationId = (LocationType)ruinObject.GetType().GetProperty("LocationId").GetValue(ruinObject, null);
                locationAsRuin.GeographicalDescription = (string)ruinObject.GetType().GetProperty("GeographicalDescription").GetValue(ruinObject, null);
                locationAsRuin.Description = (string)ruinObject.GetType().GetProperty("Description").GetValue(ruinObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(ruin.Id, locationAsRuin.Id);
                Assert.Equal(ruin.Name, locationAsRuin.Name);
                Assert.Equal(ruin.Description, locationAsRuin.Description);
                Assert.Equal(ruin.LocationId, locationAsRuin.LocationId);
                Assert.Equal(ruin.GeographicalDescription, locationAsRuin.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsARuin_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsARuin_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Ruin,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsShack : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAShack_ReturnsCreateAtActionWithShackDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Shack",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test Description"
                };

                var shack = new Shack
                {
                    Id = 0,
                    Name = "Test Shack",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(shack);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shackObject = new object();
                var locationAsShack = new Shack();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shackObject = responseAsCreateAsActionResult.Value;

                locationAsShack.Id = (int)shackObject.GetType().GetProperty("Id").GetValue(shackObject, null);
                locationAsShack.Name = (string)shackObject.GetType().GetProperty("Name").GetValue(shackObject, null);
                locationAsShack.LocationId = (LocationType)shackObject.GetType().GetProperty("LocationId").GetValue(shackObject, null);
                locationAsShack.GeographicalDescription = (string)shackObject.GetType().GetProperty("GeographicalDescription").GetValue(shackObject, null);
                locationAsShack.Description = (string)shackObject.GetType().GetProperty("Description").GetValue(shackObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(shack.Id, locationAsShack.Id);
                Assert.Equal(shack.Name, locationAsShack.Name);
                Assert.Equal(shack.Description, locationAsShack.Description);
                Assert.Equal(shack.LocationId, locationAsShack.LocationId);
                Assert.Equal(shack.GeographicalDescription, locationAsShack.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAShack_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Shack",
                    Description = "    ",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test Description"
                };

                var shack = new Shack
                {
                    Id = 0,
                    Name = "Test Shack",
                    Description = "",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(shack);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var ShackObject = new object();
                var locationAsShack = new Shack();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                ShackObject = responseAsCreateAsActionResult.Value;

                locationAsShack.Id = (int)ShackObject.GetType().GetProperty("Id").GetValue(ShackObject, null);
                locationAsShack.Name = (string)ShackObject.GetType().GetProperty("Name").GetValue(ShackObject, null);
                locationAsShack.LocationId = (LocationType)ShackObject.GetType().GetProperty("LocationId").GetValue(ShackObject, null);
                locationAsShack.GeographicalDescription = (string)ShackObject.GetType().GetProperty("GeographicalDescription").GetValue(ShackObject, null);
                locationAsShack.Description = (string)ShackObject.GetType().GetProperty("Description").GetValue(ShackObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(shack.Name, locationAsShack.Name);
                Assert.Equal(shack.Id, locationAsShack.Id);
                Assert.Equal(shack.Description, locationAsShack.Description);
                Assert.Equal(shack.LocationId, locationAsShack.LocationId);
                Assert.Equal(shack.GeographicalDescription, locationAsShack.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAShack_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Shack",
                    Description = null,
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test Description"
                };

                var shack = new Shack
                {
                    Id = 0,
                    Name = "Test Shack",
                    Description = null,
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(shack);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shackObject = new object();
                var locationAsShack = new Shack();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shackObject = responseAsCreateAsActionResult.Value;

                locationAsShack.Id = (int)shackObject.GetType().GetProperty("Id").GetValue(shackObject, null);
                locationAsShack.Name = (string)shackObject.GetType().GetProperty("Name").GetValue(shackObject, null);
                locationAsShack.LocationId = (LocationType)shackObject.GetType().GetProperty("LocationId").GetValue(shackObject, null);
                locationAsShack.GeographicalDescription = (string)shackObject.GetType().GetProperty("GeographicalDescription").GetValue(shackObject, null);
                locationAsShack.Description = (string)shackObject.GetType().GetProperty("Description").GetValue(shackObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(shack.Id, locationAsShack.Id);
                Assert.Equal(shack.Name, locationAsShack.Name);
                Assert.Equal(shack.Description, locationAsShack.Description);
                Assert.Equal(shack.LocationId, locationAsShack.LocationId);
                Assert.Equal(shack.GeographicalDescription, locationAsShack.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAShack_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAShack_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Shack,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsShip : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAShip_ReturnsCreateAtActionWithShipDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Ship",
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "Test Description"
                };

                var ship = new Ship
                {
                    Id = 0,
                    Name = "Test Shipwreck",
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(ship);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shipObject = new object();
                var locationAsShip = new Ship();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shipObject = responseAsCreateAsActionResult.Value;

                locationAsShip.Id = (int)shipObject.GetType().GetProperty("Id").GetValue(shipObject, null);
                locationAsShip.Name = (string)shipObject.GetType().GetProperty("Name").GetValue(shipObject, null);
                locationAsShip.LocationId = (LocationType)shipObject.GetType().GetProperty("LocationId").GetValue(shipObject, null);
                locationAsShip.GeographicalDescription = (string)shipObject.GetType().GetProperty("GeographicalDescription").GetValue(shipObject, null);
                locationAsShip.Description = (string)shipObject.GetType().GetProperty("Description").GetValue(shipObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(ship.Id, locationAsShip.Id);
                Assert.Equal(ship.Name, locationAsShip.Name);
                Assert.Equal(ship.Description, locationAsShip.Description);
                Assert.Equal(ship.LocationId, locationAsShip.LocationId);
                Assert.Equal(ship.GeographicalDescription, locationAsShip.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAShip_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Ship",
                    Description = "    ",
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "Test Description"
                };

                var ship = new Ship
                {
                    Id = 0,
                    Name = "Test Shipwreck",
                    Description = "",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(ship);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shipObject = new object();
                var locationAsShip = new Ship();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shipObject = responseAsCreateAsActionResult.Value;

                locationAsShip.Id = (int)shipObject.GetType().GetProperty("Id").GetValue(shipObject, null);
                locationAsShip.Name = (string)shipObject.GetType().GetProperty("Name").GetValue(shipObject, null);
                locationAsShip.LocationId = (LocationType)shipObject.GetType().GetProperty("LocationId").GetValue(shipObject, null);
                locationAsShip.GeographicalDescription = (string)shipObject.GetType().GetProperty("GeographicalDescription").GetValue(shipObject, null);
                locationAsShip.Description = (string)shipObject.GetType().GetProperty("Description").GetValue(shipObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(ship.Name, locationAsShip.Name);
                Assert.Equal(ship.Id, locationAsShip.Id);
                Assert.Equal(ship.Description, locationAsShip.Description);
                Assert.Equal(ship.LocationId, locationAsShip.LocationId);
                Assert.Equal(ship.GeographicalDescription, locationAsShip.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAShip_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Ship",
                    Description = null,
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "Test Description"
                };

                var ship = new Ship
                {
                    Id = 0,
                    Name = "Test Shipwreck",
                    Description = null,
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(ship);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shipObject = new object();
                var locationAsShip = new Ship();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shipObject = responseAsCreateAsActionResult.Value;

                locationAsShip.Id = (int)shipObject.GetType().GetProperty("Id").GetValue(shipObject, null);
                locationAsShip.Name = (string)shipObject.GetType().GetProperty("Name").GetValue(shipObject, null);
                locationAsShip.LocationId = (LocationType)shipObject.GetType().GetProperty("LocationId").GetValue(shipObject, null);
                locationAsShip.GeographicalDescription = (string)shipObject.GetType().GetProperty("GeographicalDescription").GetValue(shipObject, null);
                locationAsShip.Description = (string)shipObject.GetType().GetProperty("Description").GetValue(shipObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(ship.Id, locationAsShip.Id);
                Assert.Equal(ship.Name, locationAsShip.Name);
                Assert.Equal(ship.Description, locationAsShip.Description);
                Assert.Equal(ship.LocationId, locationAsShip.LocationId);
                Assert.Equal(ship.GeographicalDescription, locationAsShip.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAShip_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAShip_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Ship,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsShipwreck : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAShipwreck_ReturnsCreateAtActionWithShipwreckDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Shipwreck",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var shipwreck = new Shipwreck
                {
                    Id = 0,
                    Name = "Test Shipwreck",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(shipwreck);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shipwreckObject = new object();
                var locationAsShipwreck = new Shipwreck();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shipwreckObject = responseAsCreateAsActionResult.Value;

                locationAsShipwreck.Id = (int)shipwreckObject.GetType().GetProperty("Id").GetValue(shipwreckObject, null);
                locationAsShipwreck.Name = (string)shipwreckObject.GetType().GetProperty("Name").GetValue(shipwreckObject, null);
                locationAsShipwreck.LocationId = (LocationType)shipwreckObject.GetType().GetProperty("LocationId").GetValue(shipwreckObject, null);
                locationAsShipwreck.GeographicalDescription = (string)shipwreckObject.GetType().GetProperty("GeographicalDescription").GetValue(shipwreckObject, null);
                locationAsShipwreck.Description = (string)shipwreckObject.GetType().GetProperty("Description").GetValue(shipwreckObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(shipwreck.Id, locationAsShipwreck.Id);
                Assert.Equal(shipwreck.Name, locationAsShipwreck.Name);
                Assert.Equal(shipwreck.Description, locationAsShipwreck.Description);
                Assert.Equal(shipwreck.LocationId, locationAsShipwreck.LocationId);
                Assert.Equal(shipwreck.GeographicalDescription, locationAsShipwreck.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAShipwreck_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Shipwreck",
                    Description = "    ",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var shipwreck = new Shipwreck
                {
                    Id = 0,
                    Name = "Test Shipwreck",
                    Description = "",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(shipwreck);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shipwreckObject = new object();
                var locationAsShipwreck = new Shipwreck();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shipwreckObject = responseAsCreateAsActionResult.Value;

                locationAsShipwreck.Id = (int)shipwreckObject.GetType().GetProperty("Id").GetValue(shipwreckObject, null);
                locationAsShipwreck.Name = (string)shipwreckObject.GetType().GetProperty("Name").GetValue(shipwreckObject, null);
                locationAsShipwreck.LocationId = (LocationType)shipwreckObject.GetType().GetProperty("LocationId").GetValue(shipwreckObject, null);
                locationAsShipwreck.GeographicalDescription = (string)shipwreckObject.GetType().GetProperty("GeographicalDescription").GetValue(shipwreckObject, null);
                locationAsShipwreck.Description = (string)shipwreckObject.GetType().GetProperty("Description").GetValue(shipwreckObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(shipwreck.Name, locationAsShipwreck.Name);
                Assert.Equal(shipwreck.Id, locationAsShipwreck.Id);
                Assert.Equal(shipwreck.Description, locationAsShipwreck.Description);
                Assert.Equal(shipwreck.LocationId, locationAsShipwreck.LocationId);
                Assert.Equal(shipwreck.GeographicalDescription, locationAsShipwreck.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAShipwreck_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Shipwreck",
                    Description = null,
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var shipwreck = new Shipwreck
                {
                    Id = 0,
                    Name = "Test Shipwreck",
                    Description = null,
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(shipwreck);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var shipwreckObject = new object();
                var locationAsShipwreck = new Shipwreck();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                shipwreckObject = responseAsCreateAsActionResult.Value;

                locationAsShipwreck.Id = (int)shipwreckObject.GetType().GetProperty("Id").GetValue(shipwreckObject, null);
                locationAsShipwreck.Name = (string)shipwreckObject.GetType().GetProperty("Name").GetValue(shipwreckObject, null);
                locationAsShipwreck.LocationId = (LocationType)shipwreckObject.GetType().GetProperty("LocationId").GetValue(shipwreckObject, null);
                locationAsShipwreck.GeographicalDescription = (string)shipwreckObject.GetType().GetProperty("GeographicalDescription").GetValue(shipwreckObject, null);
                locationAsShipwreck.Description = (string)shipwreckObject.GetType().GetProperty("Description").GetValue(shipwreckObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(shipwreck.Id, locationAsShipwreck.Id);
                Assert.Equal(shipwreck.Name, locationAsShipwreck.Name);
                Assert.Equal(shipwreck.Description, locationAsShipwreck.Description);
                Assert.Equal(shipwreck.LocationId, locationAsShipwreck.LocationId);
                Assert.Equal(shipwreck.GeographicalDescription, locationAsShipwreck.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAShipwreck_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAShipwreck_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Shipwreck,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsStable : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAStable_ReturnsCreateAtActionWithStableDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Stable",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test Description"
                };

                var stable = new Stable
                {
                    Id = 0,
                    Name = "Test Stable",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(stable);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var stableObject = new object();
                var locationAsStable = new Stable();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                stableObject = responseAsCreateAsActionResult.Value;

                locationAsStable.Id = (int)stableObject.GetType().GetProperty("Id").GetValue(stableObject, null);
                locationAsStable.Name = (string)stableObject.GetType().GetProperty("Name").GetValue(stableObject, null);
                locationAsStable.LocationId = (LocationType)stableObject.GetType().GetProperty("LocationId").GetValue(stableObject, null);
                locationAsStable.GeographicalDescription = (string)stableObject.GetType().GetProperty("GeographicalDescription").GetValue(stableObject, null);
                locationAsStable.Description = (string)stableObject.GetType().GetProperty("Description").GetValue(stableObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(stable.Id, locationAsStable.Id);
                Assert.Equal(stable.Name, locationAsStable.Name);
                Assert.Equal(stable.Description, locationAsStable.Description);
                Assert.Equal(stable.LocationId, locationAsStable.LocationId);
                Assert.Equal(stable.GeographicalDescription, locationAsStable.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAStable_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Stable",
                    Description = "    ",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test Description"
                };

                var stable = new Stable
                {
                    Id = 0,
                    Name = "Test Stable",
                    Description = "",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(stable);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var stableObject = new object();
                var locationAsStable = new Stable();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                stableObject = responseAsCreateAsActionResult.Value;

                locationAsStable.Id = (int)stableObject.GetType().GetProperty("Id").GetValue(stableObject, null);
                locationAsStable.Name = (string)stableObject.GetType().GetProperty("Name").GetValue(stableObject, null);
                locationAsStable.LocationId = (LocationType)stableObject.GetType().GetProperty("LocationId").GetValue(stableObject, null);
                locationAsStable.GeographicalDescription = (string)stableObject.GetType().GetProperty("GeographicalDescription").GetValue(stableObject, null);
                locationAsStable.Description = (string)stableObject.GetType().GetProperty("Description").GetValue(stableObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(stable.Name, locationAsStable.Name);
                Assert.Equal(stable.Id, locationAsStable.Id);
                Assert.Equal(stable.Description, locationAsStable.Description);
                Assert.Equal(stable.LocationId, locationAsStable.LocationId);
                Assert.Equal(stable.GeographicalDescription, locationAsStable.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAStable_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Stable",
                    Description = null,
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test Description"
                };

                var stable = new Stable
                {
                    Id = 0,
                    Name = "Test Stable",
                    Description = null,
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(stable);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var stableObject = new object();
                var locationAsStable = new Stable();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                stableObject = responseAsCreateAsActionResult.Value;

                locationAsStable.Id = (int)stableObject.GetType().GetProperty("Id").GetValue(stableObject, null);
                locationAsStable.Name = (string)stableObject.GetType().GetProperty("Name").GetValue(stableObject, null);
                locationAsStable.LocationId = (LocationType)stableObject.GetType().GetProperty("LocationId").GetValue(stableObject, null);
                locationAsStable.GeographicalDescription = (string)stableObject.GetType().GetProperty("GeographicalDescription").GetValue(stableObject, null);
                locationAsStable.Description = (string)stableObject.GetType().GetProperty("Description").GetValue(stableObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(stable.Id, locationAsStable.Id);
                Assert.Equal(stable.Name, locationAsStable.Name);
                Assert.Equal(stable.Description, locationAsStable.Description);
                Assert.Equal(stable.LocationId, locationAsStable.LocationId);
                Assert.Equal(stable.GeographicalDescription, locationAsStable.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAStable_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAStable_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Stable,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsStormcloakCamp : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAStormcloakCamp_ReturnsCreateAtActionWithStormcloakCampDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test StormcloakCamp",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test Description"
                };

                var stormcloakCamp = new StormcloakCamp
                {
                    Id = 0,
                    Name = "Test StormcloakCamp",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(stormcloakCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var stormcloakCampObject = new object();
                var locationAsStormcloakCamp = new StormcloakCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                stormcloakCampObject = responseAsCreateAsActionResult.Value;

                locationAsStormcloakCamp.Id = (int)stormcloakCampObject.GetType().GetProperty("Id").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.Name = (string)stormcloakCampObject.GetType().GetProperty("Name").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.LocationId = (LocationType)stormcloakCampObject.GetType().GetProperty("LocationId").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.GeographicalDescription = (string)stormcloakCampObject.GetType().GetProperty("GeographicalDescription").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.Description = (string)stormcloakCampObject.GetType().GetProperty("Description").GetValue(stormcloakCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(stormcloakCamp.Id, locationAsStormcloakCamp.Id);
                Assert.Equal(stormcloakCamp.Name, locationAsStormcloakCamp.Name);
                Assert.Equal(stormcloakCamp.Description, locationAsStormcloakCamp.Description);
                Assert.Equal(stormcloakCamp.LocationId, locationAsStormcloakCamp.LocationId);
                Assert.Equal(stormcloakCamp.GeographicalDescription, locationAsStormcloakCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAStormcloakCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test StormcloakCamp",
                    Description = "    ",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test Description"
                };

                var stormcloakCamp = new StormcloakCamp
                {
                    Id = 0,
                    Name = "Test StormcloakCamp",
                    Description = "",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(stormcloakCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var stormcloakCampObject = new object();
                var locationAsStormcloakCamp = new StormcloakCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                stormcloakCampObject = responseAsCreateAsActionResult.Value;

                locationAsStormcloakCamp.Id = (int)stormcloakCampObject.GetType().GetProperty("Id").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.Name = (string)stormcloakCampObject.GetType().GetProperty("Name").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.LocationId = (LocationType)stormcloakCampObject.GetType().GetProperty("LocationId").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.GeographicalDescription = (string)stormcloakCampObject.GetType().GetProperty("GeographicalDescription").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.Description = (string)stormcloakCampObject.GetType().GetProperty("Description").GetValue(stormcloakCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(stormcloakCamp.Name, locationAsStormcloakCamp.Name);
                Assert.Equal(stormcloakCamp.Id, locationAsStormcloakCamp.Id);
                Assert.Equal(stormcloakCamp.Description, locationAsStormcloakCamp.Description);
                Assert.Equal(stormcloakCamp.LocationId, locationAsStormcloakCamp.LocationId);
                Assert.Equal(stormcloakCamp.GeographicalDescription, locationAsStormcloakCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAStormcloakCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test StormcloakCamp",
                    Description = null,
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test Description"
                };

                var stormcloakCamp = new StormcloakCamp
                {
                    Id = 0,
                    Name = "Test StormcloakCamp",
                    Description = null,
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(stormcloakCamp);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var stormcloakCampObject = new object();
                var locationAsStormcloakCamp = new StormcloakCamp();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                stormcloakCampObject = responseAsCreateAsActionResult.Value;

                locationAsStormcloakCamp.Id = (int)stormcloakCampObject.GetType().GetProperty("Id").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.Name = (string)stormcloakCampObject.GetType().GetProperty("Name").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.LocationId = (LocationType)stormcloakCampObject.GetType().GetProperty("LocationId").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.GeographicalDescription = (string)stormcloakCampObject.GetType().GetProperty("GeographicalDescription").GetValue(stormcloakCampObject, null);
                locationAsStormcloakCamp.Description = (string)stormcloakCampObject.GetType().GetProperty("Description").GetValue(stormcloakCampObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(stormcloakCamp.Id, locationAsStormcloakCamp.Id);
                Assert.Equal(stormcloakCamp.Name, locationAsStormcloakCamp.Name);
                Assert.Equal(stormcloakCamp.Description, locationAsStormcloakCamp.Description);
                Assert.Equal(stormcloakCamp.LocationId, locationAsStormcloakCamp.LocationId);
                Assert.Equal(stormcloakCamp.GeographicalDescription, locationAsStormcloakCamp.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAStormcloakCamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAStormcloakCamp_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.StormcloakCamp,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsTomb : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsATomb_ReturnsCreateAtActionWithTombDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Tomb",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test Description"
                };

                var tomb = new Tomb
                {
                    Id = 0,
                    Name = "Test Tomb",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(tomb);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var tombObject = new object();
                var locationAsTomb = new Tomb();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                tombObject = responseAsCreateAsActionResult.Value;

                locationAsTomb.Id = (int)tombObject.GetType().GetProperty("Id").GetValue(tombObject, null);
                locationAsTomb.Name = (string)tombObject.GetType().GetProperty("Name").GetValue(tombObject, null);
                locationAsTomb.LocationId = (LocationType)tombObject.GetType().GetProperty("LocationId").GetValue(tombObject, null);
                locationAsTomb.GeographicalDescription = (string)tombObject.GetType().GetProperty("GeographicalDescription").GetValue(tombObject, null);
                locationAsTomb.Description = (string)tombObject.GetType().GetProperty("Description").GetValue(tombObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(tomb.Id, locationAsTomb.Id);
                Assert.Equal(tomb.Name, locationAsTomb.Name);
                Assert.Equal(tomb.Description, locationAsTomb.Description);
                Assert.Equal(tomb.LocationId, locationAsTomb.LocationId);
                Assert.Equal(tomb.GeographicalDescription, locationAsTomb.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionATomb_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Tomb",
                    Description = "    ",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test Description"
                };

                var tomb = new Tomb
                {
                    Id = 0,
                    Name = "Test Tomb",
                    Description = "",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(tomb);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var tombObject = new object();
                var locationAsTomb = new Tomb();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                tombObject = responseAsCreateAsActionResult.Value;

                locationAsTomb.Id = (int)tombObject.GetType().GetProperty("Id").GetValue(tombObject, null);
                locationAsTomb.Name = (string)tombObject.GetType().GetProperty("Name").GetValue(tombObject, null);
                locationAsTomb.LocationId = (LocationType)tombObject.GetType().GetProperty("LocationId").GetValue(tombObject, null);
                locationAsTomb.GeographicalDescription = (string)tombObject.GetType().GetProperty("GeographicalDescription").GetValue(tombObject, null);
                locationAsTomb.Description = (string)tombObject.GetType().GetProperty("Description").GetValue(tombObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(tomb.Name, locationAsTomb.Name);
                Assert.Equal(tomb.Id, locationAsTomb.Id);
                Assert.Equal(tomb.Description, locationAsTomb.Description);
                Assert.Equal(tomb.LocationId, locationAsTomb.LocationId);
                Assert.Equal(tomb.GeographicalDescription, locationAsTomb.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsATomb_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Tomb",
                    Description = null,
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test Description"
                };

                var tomb = new Tomb
                {
                    Id = 0,
                    Name = "Test Tomb",
                    Description = null,
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(tomb);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var tombObject = new object();
                var locationAsTomb = new Tomb();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                tombObject = responseAsCreateAsActionResult.Value;

                locationAsTomb.Id = (int)tombObject.GetType().GetProperty("Id").GetValue(tombObject, null);
                locationAsTomb.Name = (string)tombObject.GetType().GetProperty("Name").GetValue(tombObject, null);
                locationAsTomb.LocationId = (LocationType)tombObject.GetType().GetProperty("LocationId").GetValue(tombObject, null);
                locationAsTomb.GeographicalDescription = (string)tombObject.GetType().GetProperty("GeographicalDescription").GetValue(tombObject, null);
                locationAsTomb.Description = (string)tombObject.GetType().GetProperty("Description").GetValue(tombObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(tomb.Id, locationAsTomb.Id);
                Assert.Equal(tomb.Name, locationAsTomb.Name);
                Assert.Equal(tomb.Description, locationAsTomb.Description);
                Assert.Equal(tomb.LocationId, locationAsTomb.LocationId);
                Assert.Equal(tomb.GeographicalDescription, locationAsTomb.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsATomb_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsATomb_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Tomb,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsWatchtower : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAWatchtower_ReturnsCreateAtActionWithWatchtowerDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Watchtower",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test Watchtower"
                };

                var watchtower = new Watchtower
                {
                    Id = 0,
                    Name = "Test Watchtower",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(watchtower);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var watchtowerObject = new object();
                var locationAsWatchtower = new Watchtower();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                watchtowerObject = responseAsCreateAsActionResult.Value;

                locationAsWatchtower.Id = (int)watchtowerObject.GetType().GetProperty("Id").GetValue(watchtowerObject, null);
                locationAsWatchtower.Name = (string)watchtowerObject.GetType().GetProperty("Name").GetValue(watchtowerObject, null);
                locationAsWatchtower.LocationId = (LocationType)watchtowerObject.GetType().GetProperty("LocationId").GetValue(watchtowerObject, null);
                locationAsWatchtower.GeographicalDescription = (string)watchtowerObject.GetType().GetProperty("GeographicalDescription").GetValue(watchtowerObject, null);
                locationAsWatchtower.Description = (string)watchtowerObject.GetType().GetProperty("Description").GetValue(watchtowerObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(watchtower.Id, locationAsWatchtower.Id);
                Assert.Equal(watchtower.Name, locationAsWatchtower.Name);
                Assert.Equal(watchtower.Description, locationAsWatchtower.Description);
                Assert.Equal(watchtower.LocationId, locationAsWatchtower.LocationId);
                Assert.Equal(watchtower.GeographicalDescription, locationAsWatchtower.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAWatchtower_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Watchtower",
                    Description = "    ",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test Description"
                };

                var watchtower = new Watchtower
                {
                    Id = 0,
                    Name = "Test Watchtower",
                    Description = "",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(watchtower);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var watchtowerObject = new object();
                var locationAsWatchtower = new Watchtower();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                watchtowerObject = responseAsCreateAsActionResult.Value;

                locationAsWatchtower.Id = (int)watchtowerObject.GetType().GetProperty("Id").GetValue(watchtowerObject, null);
                locationAsWatchtower.Name = (string)watchtowerObject.GetType().GetProperty("Name").GetValue(watchtowerObject, null);
                locationAsWatchtower.LocationId = (LocationType)watchtowerObject.GetType().GetProperty("LocationId").GetValue(watchtowerObject, null);
                locationAsWatchtower.GeographicalDescription = (string)watchtowerObject.GetType().GetProperty("GeographicalDescription").GetValue(watchtowerObject, null);
                locationAsWatchtower.Description = (string)watchtowerObject.GetType().GetProperty("Description").GetValue(watchtowerObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(watchtower.Name, locationAsWatchtower.Name);
                Assert.Equal(watchtower.Id, locationAsWatchtower.Id);
                Assert.Equal(watchtower.Description, locationAsWatchtower.Description);
                Assert.Equal(watchtower.LocationId, locationAsWatchtower.LocationId);
                Assert.Equal(watchtower.GeographicalDescription, locationAsWatchtower.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAWatchtower_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Watchtower",
                    Description = null,
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test Description"
                };

                var watchtower = new Watchtower
                {
                    Id = 0,
                    Name = "Test Watchtower",
                    Description = null,
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(watchtower);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var watchtowerObject = new object();
                var locationAsWatchtower = new Watchtower();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                watchtowerObject = responseAsCreateAsActionResult.Value;

                locationAsWatchtower.Id = (int)watchtowerObject.GetType().GetProperty("Id").GetValue(watchtowerObject, null);
                locationAsWatchtower.Name = (string)watchtowerObject.GetType().GetProperty("Name").GetValue(watchtowerObject, null);
                locationAsWatchtower.LocationId = (LocationType)watchtowerObject.GetType().GetProperty("LocationId").GetValue(watchtowerObject, null);
                locationAsWatchtower.GeographicalDescription = (string)watchtowerObject.GetType().GetProperty("GeographicalDescription").GetValue(watchtowerObject, null);
                locationAsWatchtower.Description = (string)watchtowerObject.GetType().GetProperty("Description").GetValue(watchtowerObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(watchtower.Id, locationAsWatchtower.Id);
                Assert.Equal(watchtower.Name, locationAsWatchtower.Name);
                Assert.Equal(watchtower.Description, locationAsWatchtower.Description);
                Assert.Equal(watchtower.LocationId, locationAsWatchtower.LocationId);
                Assert.Equal(watchtower.GeographicalDescription, locationAsWatchtower.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAWatchtower_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAWatchtower_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Watchtower,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsWheatMill : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAWheatMill_ReturnsCreateAtActionWithWheatMillDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test WheatMill",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test WheatMill"
                };

                var wheatMill = new WheatMill
                {
                    Id = 0,
                    Name = "Test WheatMill",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(wheatMill);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var wheatMillObject = new object();
                var locationAsWheatMill = new WheatMill();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                wheatMillObject = responseAsCreateAsActionResult.Value;

                locationAsWheatMill.Id = (int)wheatMillObject.GetType().GetProperty("Id").GetValue(wheatMillObject, null);
                locationAsWheatMill.Name = (string)wheatMillObject.GetType().GetProperty("Name").GetValue(wheatMillObject, null);
                locationAsWheatMill.LocationId = (LocationType)wheatMillObject.GetType().GetProperty("LocationId").GetValue(wheatMillObject, null);
                locationAsWheatMill.GeographicalDescription = (string)wheatMillObject.GetType().GetProperty("GeographicalDescription").GetValue(wheatMillObject, null);
                locationAsWheatMill.Description = (string)wheatMillObject.GetType().GetProperty("Description").GetValue(wheatMillObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(wheatMill.Id, locationAsWheatMill.Id);
                Assert.Equal(wheatMill.Name, locationAsWheatMill.Name);
                Assert.Equal(wheatMill.Description, locationAsWheatMill.Description);
                Assert.Equal(wheatMill.LocationId, locationAsWheatMill.LocationId);
                Assert.Equal(wheatMill.GeographicalDescription, locationAsWheatMill.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAWheatMill_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test WheatMill",
                    Description = "    ",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test Description"
                };

                var wheatMill = new WheatMill
                {
                    Id = 0,
                    Name = "Test WheatMill",
                    Description = "",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(wheatMill);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var wheatMillObject = new object();
                var locationAsWheatMill = new WheatMill();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                wheatMillObject = responseAsCreateAsActionResult.Value;

                locationAsWheatMill.Id = (int)wheatMillObject.GetType().GetProperty("Id").GetValue(wheatMillObject, null);
                locationAsWheatMill.Name = (string)wheatMillObject.GetType().GetProperty("Name").GetValue(wheatMillObject, null);
                locationAsWheatMill.LocationId = (LocationType)wheatMillObject.GetType().GetProperty("LocationId").GetValue(wheatMillObject, null);
                locationAsWheatMill.GeographicalDescription = (string)wheatMillObject.GetType().GetProperty("GeographicalDescription").GetValue(wheatMillObject, null);
                locationAsWheatMill.Description = (string)wheatMillObject.GetType().GetProperty("Description").GetValue(wheatMillObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(wheatMill.Name, locationAsWheatMill.Name);
                Assert.Equal(wheatMill.Id, locationAsWheatMill.Id);
                Assert.Equal(wheatMill.Description, locationAsWheatMill.Description);
                Assert.Equal(wheatMill.LocationId, locationAsWheatMill.LocationId);
                Assert.Equal(wheatMill.GeographicalDescription, locationAsWheatMill.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAWheatMill_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test WheatMill",
                    Description = null,
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test Description"
                };

                var wheatMill = new WheatMill
                {
                    Id = 0,
                    Name = "Test WheatMill",
                    Description = null,
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(wheatMill);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var wheatMillObject = new object();
                var locationAsWheatMill = new WheatMill();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                wheatMillObject = responseAsCreateAsActionResult.Value;

                locationAsWheatMill.Id = (int)wheatMillObject.GetType().GetProperty("Id").GetValue(wheatMillObject, null);
                locationAsWheatMill.Name = (string)wheatMillObject.GetType().GetProperty("Name").GetValue(wheatMillObject, null);
                locationAsWheatMill.LocationId = (LocationType)wheatMillObject.GetType().GetProperty("LocationId").GetValue(wheatMillObject, null);
                locationAsWheatMill.GeographicalDescription = (string)wheatMillObject.GetType().GetProperty("GeographicalDescription").GetValue(wheatMillObject, null);
                locationAsWheatMill.Description = (string)wheatMillObject.GetType().GetProperty("Description").GetValue(wheatMillObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(wheatMill.Id, locationAsWheatMill.Id);
                Assert.Equal(wheatMill.Name, locationAsWheatMill.Name);
                Assert.Equal(wheatMill.Description, locationAsWheatMill.Description);
                Assert.Equal(wheatMill.LocationId, locationAsWheatMill.LocationId);
                Assert.Equal(wheatMill.GeographicalDescription, locationAsWheatMill.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAWheatMill_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAWheatMill_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.WheatMill,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsLumberMill : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsALumberMill_ReturnsCreateAtActionWithLumberMillDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test LumberMill",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test LumberMill"
                };

                var lumberMill = new LumberMill
                {
                    Id = 0,
                    Name = "Test LumberMill",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(lumberMill);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var lumberMillObject = new object();
                var locationAsLumberMill = new LumberMill();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                lumberMillObject = responseAsCreateAsActionResult.Value;

                locationAsLumberMill.Id = (int)lumberMillObject.GetType().GetProperty("Id").GetValue(lumberMillObject, null);
                locationAsLumberMill.Name = (string)lumberMillObject.GetType().GetProperty("Name").GetValue(lumberMillObject, null);
                locationAsLumberMill.LocationId = (LocationType)lumberMillObject.GetType().GetProperty("LocationId").GetValue(lumberMillObject, null);
                locationAsLumberMill.GeographicalDescription = (string)lumberMillObject.GetType().GetProperty("GeographicalDescription").GetValue(lumberMillObject, null);
                locationAsLumberMill.Description = (string)lumberMillObject.GetType().GetProperty("Description").GetValue(lumberMillObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(lumberMill.Id, locationAsLumberMill.Id);
                Assert.Equal(lumberMill.Name, locationAsLumberMill.Name);
                Assert.Equal(lumberMill.Description, locationAsLumberMill.Description);
                Assert.Equal(lumberMill.LocationId, locationAsLumberMill.LocationId);
                Assert.Equal(lumberMill.GeographicalDescription, locationAsLumberMill.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionALumberMill_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test LumberMill",
                    Description = "    ",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test Description"
                };

                var lumberMill = new LumberMill
                {
                    Id = 0,
                    Name = "Test LumberMill",
                    Description = "",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(lumberMill);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var lumberMillObject = new object();
                var locationAsLumberMill = new LumberMill();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                lumberMillObject = responseAsCreateAsActionResult.Value;

                locationAsLumberMill.Id = (int)lumberMillObject.GetType().GetProperty("Id").GetValue(lumberMillObject, null);
                locationAsLumberMill.Name = (string)lumberMillObject.GetType().GetProperty("Name").GetValue(lumberMillObject, null);
                locationAsLumberMill.LocationId = (LocationType)lumberMillObject.GetType().GetProperty("LocationId").GetValue(lumberMillObject, null);
                locationAsLumberMill.GeographicalDescription = (string)lumberMillObject.GetType().GetProperty("GeographicalDescription").GetValue(lumberMillObject, null);
                locationAsLumberMill.Description = (string)lumberMillObject.GetType().GetProperty("Description").GetValue(lumberMillObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(lumberMill.Name, locationAsLumberMill.Name);
                Assert.Equal(lumberMill.Id, locationAsLumberMill.Id);
                Assert.Equal(lumberMill.Description, locationAsLumberMill.Description);
                Assert.Equal(lumberMill.LocationId, locationAsLumberMill.LocationId);
                Assert.Equal(lumberMill.GeographicalDescription, locationAsLumberMill.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsALumberMill_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test LumberMill",
                    Description = null,
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test Description"
                };

                var lumberMill = new LumberMill
                {
                    Id = 0,
                    Name = "Test LumberMill",
                    Description = null,
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(lumberMill);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var lumberMillObject = new object();
                var locationAsLumberMill = new LumberMill();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                lumberMillObject = responseAsCreateAsActionResult.Value;

                locationAsLumberMill.Id = (int)lumberMillObject.GetType().GetProperty("Id").GetValue(lumberMillObject, null);
                locationAsLumberMill.Name = (string)lumberMillObject.GetType().GetProperty("Name").GetValue(lumberMillObject, null);
                locationAsLumberMill.LocationId = (LocationType)lumberMillObject.GetType().GetProperty("LocationId").GetValue(lumberMillObject, null);
                locationAsLumberMill.GeographicalDescription = (string)lumberMillObject.GetType().GetProperty("GeographicalDescription").GetValue(lumberMillObject, null);
                locationAsLumberMill.Description = (string)lumberMillObject.GetType().GetProperty("Description").GetValue(lumberMillObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(lumberMill.Id, locationAsLumberMill.Id);
                Assert.Equal(lumberMill.Name, locationAsLumberMill.Name);
                Assert.Equal(lumberMill.Description, locationAsLumberMill.Description);
                Assert.Equal(lumberMill.LocationId, locationAsLumberMill.LocationId);
                Assert.Equal(lumberMill.GeographicalDescription, locationAsLumberMill.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsALumberMill_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsALumberMill_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.LumberMill,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsBodyOfWater : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsABodyOfWater_ReturnsCreateAtActionWithBodyOfWaterDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test BodyOfWater",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test BodyOfWater"
                };

                var bodyOfWater = new BodyOfWater
                {
                    Id = 0,
                    Name = "Test BodyOfWater",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(bodyOfWater);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var bodyOfWaterObject = new object();
                var locationAsBodyOfWater = new BodyOfWater();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                bodyOfWaterObject = responseAsCreateAsActionResult.Value;

                locationAsBodyOfWater.Id = (int)bodyOfWaterObject.GetType().GetProperty("Id").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.Name = (string)bodyOfWaterObject.GetType().GetProperty("Name").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.LocationId = (LocationType)bodyOfWaterObject.GetType().GetProperty("LocationId").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.GeographicalDescription = (string)bodyOfWaterObject.GetType().GetProperty("GeographicalDescription").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.Description = (string)bodyOfWaterObject.GetType().GetProperty("Description").GetValue(bodyOfWaterObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(bodyOfWater.Id, locationAsBodyOfWater.Id);
                Assert.Equal(bodyOfWater.Name, locationAsBodyOfWater.Name);
                Assert.Equal(bodyOfWater.Description, locationAsBodyOfWater.Description);
                Assert.Equal(bodyOfWater.LocationId, locationAsBodyOfWater.LocationId);
                Assert.Equal(bodyOfWater.GeographicalDescription, locationAsBodyOfWater.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionABodyOfWater_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test BodyOfWater",
                    Description = "    ",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test Description"
                };

                var bodyOfWater = new BodyOfWater
                {
                    Id = 0,
                    Name = "Test BodyOfWater",
                    Description = "",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(bodyOfWater);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var bodyOfWaterObject = new object();
                var locationAsBodyOfWater = new BodyOfWater();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                bodyOfWaterObject = responseAsCreateAsActionResult.Value;

                locationAsBodyOfWater.Id = (int)bodyOfWaterObject.GetType().GetProperty("Id").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.Name = (string)bodyOfWaterObject.GetType().GetProperty("Name").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.LocationId = (LocationType)bodyOfWaterObject.GetType().GetProperty("LocationId").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.GeographicalDescription = (string)bodyOfWaterObject.GetType().GetProperty("GeographicalDescription").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.Description = (string)bodyOfWaterObject.GetType().GetProperty("Description").GetValue(bodyOfWaterObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(bodyOfWater.Name, locationAsBodyOfWater.Name);
                Assert.Equal(bodyOfWater.Id, locationAsBodyOfWater.Id);
                Assert.Equal(bodyOfWater.Description, locationAsBodyOfWater.Description);
                Assert.Equal(bodyOfWater.LocationId, locationAsBodyOfWater.LocationId);
                Assert.Equal(bodyOfWater.GeographicalDescription, locationAsBodyOfWater.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsABodyOfWater_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test BodyOfWater",
                    Description = null,
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test Description"
                };

                var bodyOfWater = new BodyOfWater
                {
                    Id = 0,
                    Name = "Test BodyOfWater",
                    Description = null,
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(bodyOfWater);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var bodyOfWaterObject = new object();
                var locationAsBodyOfWater = new BodyOfWater();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                bodyOfWaterObject = responseAsCreateAsActionResult.Value;

                locationAsBodyOfWater.Id = (int)bodyOfWaterObject.GetType().GetProperty("Id").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.Name = (string)bodyOfWaterObject.GetType().GetProperty("Name").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.LocationId = (LocationType)bodyOfWaterObject.GetType().GetProperty("LocationId").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.GeographicalDescription = (string)bodyOfWaterObject.GetType().GetProperty("GeographicalDescription").GetValue(bodyOfWaterObject, null);
                locationAsBodyOfWater.Description = (string)bodyOfWaterObject.GetType().GetProperty("Description").GetValue(bodyOfWaterObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(bodyOfWater.Id, locationAsBodyOfWater.Id);
                Assert.Equal(bodyOfWater.Name, locationAsBodyOfWater.Name);
                Assert.Equal(bodyOfWater.Description, locationAsBodyOfWater.Description);
                Assert.Equal(bodyOfWater.LocationId, locationAsBodyOfWater.LocationId);
                Assert.Equal(bodyOfWater.GeographicalDescription, locationAsBodyOfWater.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsABodyOfWater_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsABodyOfWater_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.BodyOfWater,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsInnOrTavern : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAnInnOrTavern_ReturnsCreateAtActionWithInnOrTavernDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test InnOrTavern",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test InnOrTavern"
                };

                var innOrTavern = new InnOrTavern
                {
                    Id = 0,
                    Name = "Test InnOrTavern",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(innOrTavern);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var InnOrTavernObject = new object();
                var locationAsInnOrTavern = new InnOrTavern();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                InnOrTavernObject = responseAsCreateAsActionResult.Value;

                locationAsInnOrTavern.Id = (int)InnOrTavernObject.GetType().GetProperty("Id").GetValue(InnOrTavernObject, null);
                locationAsInnOrTavern.Name = (string)InnOrTavernObject.GetType().GetProperty("Name").GetValue(InnOrTavernObject, null);
                locationAsInnOrTavern.LocationId = (LocationType)InnOrTavernObject.GetType().GetProperty("LocationId").GetValue(InnOrTavernObject, null);
                locationAsInnOrTavern.GeographicalDescription = (string)InnOrTavernObject.GetType().GetProperty("GeographicalDescription").GetValue(InnOrTavernObject, null);
                locationAsInnOrTavern.Description = (string)InnOrTavernObject.GetType().GetProperty("Description").GetValue(InnOrTavernObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(innOrTavern.Id, locationAsInnOrTavern.Id);
                Assert.Equal(innOrTavern.Name, locationAsInnOrTavern.Name);
                Assert.Equal(innOrTavern.Description, locationAsInnOrTavern.Description);
                Assert.Equal(innOrTavern.LocationId, locationAsInnOrTavern.LocationId);
                Assert.Equal(innOrTavern.GeographicalDescription, locationAsInnOrTavern.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAnInnOrTavern_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test InnOrTavern",
                    Description = "    ",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test Description"
                };

                var innOrTavern = new InnOrTavern
                {
                    Id = 0,
                    Name = "Test InnOrTavern",
                    Description = "",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(innOrTavern);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var innOrTavernObject = new object();
                var locationAsInnOrTavern = new InnOrTavern();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                innOrTavernObject = responseAsCreateAsActionResult.Value;

                locationAsInnOrTavern.Id = (int)innOrTavernObject.GetType().GetProperty("Id").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.Name = (string)innOrTavernObject.GetType().GetProperty("Name").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.LocationId = (LocationType)innOrTavernObject.GetType().GetProperty("LocationId").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.GeographicalDescription = (string)innOrTavernObject.GetType().GetProperty("GeographicalDescription").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.Description = (string)innOrTavernObject.GetType().GetProperty("Description").GetValue(innOrTavernObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(innOrTavern.Name, locationAsInnOrTavern.Name);
                Assert.Equal(innOrTavern.Id, locationAsInnOrTavern.Id);
                Assert.Equal(innOrTavern.Description, locationAsInnOrTavern.Description);
                Assert.Equal(innOrTavern.LocationId, locationAsInnOrTavern.LocationId);
                Assert.Equal(innOrTavern.GeographicalDescription, locationAsInnOrTavern.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAnInnOrTavern_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test InnOrTavern",
                    Description = null,
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test Description"
                };

                var innOrTavern = new InnOrTavern
                {
                    Id = 0,
                    Name = "Test InnOrTavern",
                    Description = null,
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(innOrTavern);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var innOrTavernObject = new object();
                var locationAsInnOrTavern = new InnOrTavern();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                innOrTavernObject = responseAsCreateAsActionResult.Value;

                locationAsInnOrTavern.Id = (int)innOrTavernObject.GetType().GetProperty("Id").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.Name = (string)innOrTavernObject.GetType().GetProperty("Name").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.LocationId = (LocationType)innOrTavernObject.GetType().GetProperty("LocationId").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.GeographicalDescription = (string)innOrTavernObject.GetType().GetProperty("GeographicalDescription").GetValue(innOrTavernObject, null);
                locationAsInnOrTavern.Description = (string)innOrTavernObject.GetType().GetProperty("Description").GetValue(innOrTavernObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(innOrTavern.Id, locationAsInnOrTavern.Id);
                Assert.Equal(innOrTavern.Name, locationAsInnOrTavern.Name);
                Assert.Equal(innOrTavern.Description, locationAsInnOrTavern.Description);
                Assert.Equal(innOrTavern.LocationId, locationAsInnOrTavern.LocationId);
                Assert.Equal(innOrTavern.GeographicalDescription, locationAsInnOrTavern.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAnInnOrTavern_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAnInnOrTavern_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.InnOrTavern,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsTemple : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsATemple_ReturnsCreateAtActionWithTempleDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Temple",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test Temple"
                };

                var temple = new Temple
                {
                    Id = 0,
                    Name = "Test Temple",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(temple);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var templeObject = new object();
                var locationAsTemple = new Temple();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                templeObject = responseAsCreateAsActionResult.Value;

                locationAsTemple.Id = (int)templeObject.GetType().GetProperty("Id").GetValue(templeObject, null);
                locationAsTemple.Name = (string)templeObject.GetType().GetProperty("Name").GetValue(templeObject, null);
                locationAsTemple.LocationId = (LocationType)templeObject.GetType().GetProperty("LocationId").GetValue(templeObject, null);
                locationAsTemple.GeographicalDescription = (string)templeObject.GetType().GetProperty("GeographicalDescription").GetValue(templeObject, null);
                locationAsTemple.Description = (string)templeObject.GetType().GetProperty("Description").GetValue(templeObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(temple.Id, locationAsTemple.Id);
                Assert.Equal(temple.Name, locationAsTemple.Name);
                Assert.Equal(temple.Description, locationAsTemple.Description);
                Assert.Equal(temple.LocationId, locationAsTemple.LocationId);
                Assert.Equal(temple.GeographicalDescription, locationAsTemple.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionATemple_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Temple",
                    Description = "    ",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test Description"
                };

                var temple = new Temple
                {
                    Id = 0,
                    Name = "Test Temple",
                    Description = "",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(temple);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var templeObject = new object();
                var locationAsTemple = new Temple();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                templeObject = responseAsCreateAsActionResult.Value;

                locationAsTemple.Id = (int)templeObject.GetType().GetProperty("Id").GetValue(templeObject, null);
                locationAsTemple.Name = (string)templeObject.GetType().GetProperty("Name").GetValue(templeObject, null);
                locationAsTemple.LocationId = (LocationType)templeObject.GetType().GetProperty("LocationId").GetValue(templeObject, null);
                locationAsTemple.GeographicalDescription = (string)templeObject.GetType().GetProperty("GeographicalDescription").GetValue(templeObject, null);
                locationAsTemple.Description = (string)templeObject.GetType().GetProperty("Description").GetValue(templeObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(temple.Name, locationAsTemple.Name);
                Assert.Equal(temple.Id, locationAsTemple.Id);
                Assert.Equal(temple.Description, locationAsTemple.Description);
                Assert.Equal(temple.LocationId, locationAsTemple.LocationId);
                Assert.Equal(temple.GeographicalDescription, locationAsTemple.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsATemple_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Temple",
                    Description = null,
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test Description"
                };

                var temple = new Temple
                {
                    Id = 0,
                    Name = "Test Temple",
                    Description = null,
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(temple);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var templeObject = new object();
                var locationAsTemple = new Temple();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                templeObject = responseAsCreateAsActionResult.Value;

                locationAsTemple.Id = (int)templeObject.GetType().GetProperty("Id").GetValue(templeObject, null);
                locationAsTemple.Name = (string)templeObject.GetType().GetProperty("Name").GetValue(templeObject, null);
                locationAsTemple.LocationId = (LocationType)templeObject.GetType().GetProperty("LocationId").GetValue(templeObject, null);
                locationAsTemple.GeographicalDescription = (string)templeObject.GetType().GetProperty("GeographicalDescription").GetValue(templeObject, null);
                locationAsTemple.Description = (string)templeObject.GetType().GetProperty("Description").GetValue(templeObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(temple.Id, locationAsTemple.Id);
                Assert.Equal(temple.Name, locationAsTemple.Name);
                Assert.Equal(temple.Description, locationAsTemple.Description);
                Assert.Equal(temple.LocationId, locationAsTemple.LocationId);
                Assert.Equal(temple.GeographicalDescription, locationAsTemple.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsATemple_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsATemple_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Temple,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsWordWall : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAWordWall_ReturnsCreateAtActionWithWordWallDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test WordWall",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test WordWall"
                };

                var wordWall = new WordWall
                {
                    Id = 0,
                    Name = "Test WordWall",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(wordWall);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var wordWallObject = new object();
                var locationAsWordWall = new WordWall();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                wordWallObject = responseAsCreateAsActionResult.Value;

                locationAsWordWall.Id = (int)wordWallObject.GetType().GetProperty("Id").GetValue(wordWallObject, null);
                locationAsWordWall.Name = (string)wordWallObject.GetType().GetProperty("Name").GetValue(wordWallObject, null);
                locationAsWordWall.LocationId = (LocationType)wordWallObject.GetType().GetProperty("LocationId").GetValue(wordWallObject, null);
                locationAsWordWall.GeographicalDescription = (string)wordWallObject.GetType().GetProperty("GeographicalDescription").GetValue(wordWallObject, null);
                locationAsWordWall.Description = (string)wordWallObject.GetType().GetProperty("Description").GetValue(wordWallObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(wordWall.Id, locationAsWordWall.Id);
                Assert.Equal(wordWall.Name, locationAsWordWall.Name);
                Assert.Equal(wordWall.Description, locationAsWordWall.Description);
                Assert.Equal(wordWall.LocationId, locationAsWordWall.LocationId);
                Assert.Equal(wordWall.GeographicalDescription, locationAsWordWall.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAWordWall_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test WordWall",
                    Description = "    ",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test Description"
                };

                var wordWall = new WordWall
                {
                    Id = 0,
                    Name = "Test WordWall",
                    Description = "",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(wordWall);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var wordWallObject = new object();
                var locationAsWordWall = new WordWall();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                wordWallObject = responseAsCreateAsActionResult.Value;

                locationAsWordWall.Id = (int)wordWallObject.GetType().GetProperty("Id").GetValue(wordWallObject, null);
                locationAsWordWall.Name = (string)wordWallObject.GetType().GetProperty("Name").GetValue(wordWallObject, null);
                locationAsWordWall.LocationId = (LocationType)wordWallObject.GetType().GetProperty("LocationId").GetValue(wordWallObject, null);
                locationAsWordWall.GeographicalDescription = (string)wordWallObject.GetType().GetProperty("GeographicalDescription").GetValue(wordWallObject, null);
                locationAsWordWall.Description = (string)wordWallObject.GetType().GetProperty("Description").GetValue(wordWallObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(wordWall.Name, locationAsWordWall.Name);
                Assert.Equal(wordWall.Id, locationAsWordWall.Id);
                Assert.Equal(wordWall.Description, locationAsWordWall.Description);
                Assert.Equal(wordWall.LocationId, locationAsWordWall.LocationId);
                Assert.Equal(wordWall.GeographicalDescription, locationAsWordWall.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAWordWall_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test WordWall",
                    Description = null,
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test Description"
                };

                var wordWall = new WordWall
                {
                    Id = 0,
                    Name = "Test WordWall",
                    Description = null,
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(wordWall);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var wordWallObject = new object();
                var locationAsWordWall = new WordWall();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                wordWallObject = responseAsCreateAsActionResult.Value;

                locationAsWordWall.Id = (int)wordWallObject.GetType().GetProperty("Id").GetValue(wordWallObject, null);
                locationAsWordWall.Name = (string)wordWallObject.GetType().GetProperty("Name").GetValue(wordWallObject, null);
                locationAsWordWall.LocationId = (LocationType)wordWallObject.GetType().GetProperty("LocationId").GetValue(wordWallObject, null);
                locationAsWordWall.GeographicalDescription = (string)wordWallObject.GetType().GetProperty("GeographicalDescription").GetValue(wordWallObject, null);
                locationAsWordWall.Description = (string)wordWallObject.GetType().GetProperty("Description").GetValue(wordWallObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(wordWall.Id, locationAsWordWall.Id);
                Assert.Equal(wordWall.Name, locationAsWordWall.Name);
                Assert.Equal(wordWall.Description, locationAsWordWall.Description);
                Assert.Equal(wordWall.LocationId, locationAsWordWall.LocationId);
                Assert.Equal(wordWall.GeographicalDescription, locationAsWordWall.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAWordWall_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAWordWall_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.WordWall,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsCastle : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsACastle_ReturnsCreateAtActionWithCastleDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Castle",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test Castle"
                };

                var castle = new Data.Models.Castle
                {
                    Id = 0,
                    Name = "Test Castle",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(castle);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var castleObject = new object();
                var locationAsCastle = new Data.Models.Castle();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                castleObject = responseAsCreateAsActionResult.Value;

                locationAsCastle.Id = (int)castleObject.GetType().GetProperty("Id").GetValue(castleObject, null);
                locationAsCastle.Name = (string)castleObject.GetType().GetProperty("Name").GetValue(castleObject, null);
                locationAsCastle.LocationId = (LocationType)castleObject.GetType().GetProperty("LocationId").GetValue(castleObject, null);
                locationAsCastle.GeographicalDescription = (string)castleObject.GetType().GetProperty("GeographicalDescription").GetValue(castleObject, null);
                locationAsCastle.Description = (string)castleObject.GetType().GetProperty("Description").GetValue(castleObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(castle.Id, locationAsCastle.Id);
                Assert.Equal(castle.Name, locationAsCastle.Name);
                Assert.Equal(castle.Description, locationAsCastle.Description);
                Assert.Equal(castle.LocationId, locationAsCastle.LocationId);
                Assert.Equal(castle.GeographicalDescription, locationAsCastle.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionACastle_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Castle",
                    Description = "    ",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test Description"
                };

                var castle = new Data.Models.Castle
                {
                    Id = 0,
                    Name = "Test Castle",
                    Description = "",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(castle);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var castleObject = new object();
                var locationAsCastle = new Data.Models.Castle();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                castleObject = responseAsCreateAsActionResult.Value;

                locationAsCastle.Id = (int)castleObject.GetType().GetProperty("Id").GetValue(castleObject, null);
                locationAsCastle.Name = (string)castleObject.GetType().GetProperty("Name").GetValue(castleObject, null);
                locationAsCastle.LocationId = (LocationType)castleObject.GetType().GetProperty("LocationId").GetValue(castleObject, null);
                locationAsCastle.GeographicalDescription = (string)castleObject.GetType().GetProperty("GeographicalDescription").GetValue(castleObject, null);
                locationAsCastle.Description = (string)castleObject.GetType().GetProperty("Description").GetValue(castleObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(castle.Name, locationAsCastle.Name);
                Assert.Equal(castle.Id, locationAsCastle.Id);
                Assert.Equal(castle.Description, locationAsCastle.Description);
                Assert.Equal(castle.LocationId, locationAsCastle.LocationId);
                Assert.Equal(castle.GeographicalDescription, locationAsCastle.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsACastle_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test Castle",
                    Description = null,
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test Description"
                };

                var castle = new Data.Models.Castle
                {
                    Id = 0,
                    Name = "Test Castle",
                    Description = null,
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(castle);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var castleObject = new object();
                var locationAsCastle = new Data.Models.Castle();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                castleObject = responseAsCreateAsActionResult.Value;

                locationAsCastle.Id = (int)castleObject.GetType().GetProperty("Id").GetValue(castleObject, null);
                locationAsCastle.Name = (string)castleObject.GetType().GetProperty("Name").GetValue(castleObject, null);
                locationAsCastle.LocationId = (LocationType)castleObject.GetType().GetProperty("LocationId").GetValue(castleObject, null);
                locationAsCastle.GeographicalDescription = (string)castleObject.GetType().GetProperty("GeographicalDescription").GetValue(castleObject, null);
                locationAsCastle.Description = (string)castleObject.GetType().GetProperty("Description").GetValue(castleObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(castle.Id, locationAsCastle.Id);
                Assert.Equal(castle.Name, locationAsCastle.Name);
                Assert.Equal(castle.Description, locationAsCastle.Description);
                Assert.Equal(castle.LocationId, locationAsCastle.LocationId);
                Assert.Equal(castle.GeographicalDescription, locationAsCastle.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsACastle_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsACastle_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.Castle,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsGuildHeadquarter : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAGuildHeadquarter_ReturnsCreateAtActionWithGuildHeadquarterDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test GuildHeadquarter",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test GuildHeadquarter"
                };

                var guildHeadquarter = new GuildHeadquarter
                {
                    Id = 0,
                    Name = "Test GuildHeadquarter",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(guildHeadquarter);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var guildHeadquarterObject = new object();
                var locationAsGuildHeadquarter = new GuildHeadquarter();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                guildHeadquarterObject = responseAsCreateAsActionResult.Value;

                locationAsGuildHeadquarter.Id = (int)guildHeadquarterObject.GetType().GetProperty("Id").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.Name = (string)guildHeadquarterObject.GetType().GetProperty("Name").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.LocationId = (LocationType)guildHeadquarterObject.GetType().GetProperty("LocationId").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.GeographicalDescription = (string)guildHeadquarterObject.GetType().GetProperty("GeographicalDescription").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.Description = (string)guildHeadquarterObject.GetType().GetProperty("Description").GetValue(guildHeadquarterObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(guildHeadquarter.Id, locationAsGuildHeadquarter.Id);
                Assert.Equal(guildHeadquarter.Name, locationAsGuildHeadquarter.Name);
                Assert.Equal(guildHeadquarter.Description, locationAsGuildHeadquarter.Description);
                Assert.Equal(guildHeadquarter.LocationId, locationAsGuildHeadquarter.LocationId);
                Assert.Equal(guildHeadquarter.GeographicalDescription, locationAsGuildHeadquarter.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAGuildHeadquarter_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test GuildHeadquarter",
                    Description = "    ",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test Description"
                };

                var guildHeadquarter = new GuildHeadquarter
                {
                    Id = 0,
                    Name = "Test GuildHeadquarter",
                    Description = "",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(guildHeadquarter);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var guildHeadquarterObject = new object();
                var locationAsGuildHeadquarter = new GuildHeadquarter();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                guildHeadquarterObject = responseAsCreateAsActionResult.Value;

                locationAsGuildHeadquarter.Id = (int)guildHeadquarterObject.GetType().GetProperty("Id").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.Name = (string)guildHeadquarterObject.GetType().GetProperty("Name").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.LocationId = (LocationType)guildHeadquarterObject.GetType().GetProperty("LocationId").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.GeographicalDescription = (string)guildHeadquarterObject.GetType().GetProperty("GeographicalDescription").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.Description = (string)guildHeadquarterObject.GetType().GetProperty("Description").GetValue(guildHeadquarterObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(guildHeadquarter.Name, locationAsGuildHeadquarter.Name);
                Assert.Equal(guildHeadquarter.Id, locationAsGuildHeadquarter.Id);
                Assert.Equal(guildHeadquarter.Description, locationAsGuildHeadquarter.Description);
                Assert.Equal(guildHeadquarter.LocationId, locationAsGuildHeadquarter.LocationId);
                Assert.Equal(guildHeadquarter.GeographicalDescription, locationAsGuildHeadquarter.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAGuildHeadquarter_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test GuildHeadquarter",
                    Description = null,
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test Description"
                };

                var guildHeadquarter = new GuildHeadquarter
                {
                    Id = 0,
                    Name = "Test GuildHeadquarter",
                    Description = null,
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(guildHeadquarter);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var guildHeadquarterObject = new object();
                var locationAsGuildHeadquarter = new GuildHeadquarter();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                guildHeadquarterObject = responseAsCreateAsActionResult.Value;

                locationAsGuildHeadquarter.Id = (int)guildHeadquarterObject.GetType().GetProperty("Id").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.Name = (string)guildHeadquarterObject.GetType().GetProperty("Name").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.LocationId = (LocationType)guildHeadquarterObject.GetType().GetProperty("LocationId").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.GeographicalDescription = (string)guildHeadquarterObject.GetType().GetProperty("GeographicalDescription").GetValue(guildHeadquarterObject, null);
                locationAsGuildHeadquarter.Description = (string)guildHeadquarterObject.GetType().GetProperty("Description").GetValue(guildHeadquarterObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(guildHeadquarter.Id, locationAsGuildHeadquarter.Id);
                Assert.Equal(guildHeadquarter.Name, locationAsGuildHeadquarter.Name);
                Assert.Equal(guildHeadquarter.Description, locationAsGuildHeadquarter.Description);
                Assert.Equal(guildHeadquarter.LocationId, locationAsGuildHeadquarter.LocationId);
                Assert.Equal(guildHeadquarter.GeographicalDescription, locationAsGuildHeadquarter.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAGuildHeadquarter_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAGuildHeadquarter_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.GuildHeadquarter,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }

        public class CreateLocation_AsUnmarkedLocation : LocationController_Tests
        {
            [Fact]
            public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAUnmarkedLocation_ReturnsCreateAtActionWithGuildUnmarkedLocationDetails()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test UnmarkedLocation",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test UnmarkedLocation"
                };

                var unmarkedLocation = new UnmarkedLocation
                {
                    Id = 0,
                    Name = "Test UnmarkedLocation",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(unmarkedLocation);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var unmarkedLocationObject = new object();
                var locationAsUnmarkedLocation = new UnmarkedLocation();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                unmarkedLocationObject = responseAsCreateAsActionResult.Value;

                locationAsUnmarkedLocation.Id = (int)unmarkedLocationObject.GetType().GetProperty("Id").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.Name = (string)unmarkedLocationObject.GetType().GetProperty("Name").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.LocationId = (LocationType)unmarkedLocationObject.GetType().GetProperty("LocationId").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.GeographicalDescription = (string)unmarkedLocationObject.GetType().GetProperty("GeographicalDescription").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.Description = (string)unmarkedLocationObject.GetType().GetProperty("Description").GetValue(unmarkedLocationObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(unmarkedLocation.Id, locationAsUnmarkedLocation.Id);
                Assert.Equal(unmarkedLocation.Name, locationAsUnmarkedLocation.Name);
                Assert.Equal(unmarkedLocation.Description, locationAsUnmarkedLocation.Description);
                Assert.Equal(unmarkedLocation.LocationId, locationAsUnmarkedLocation.LocationId);
                Assert.Equal(unmarkedLocation.GeographicalDescription, locationAsUnmarkedLocation.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAUnmarkedLocation_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test UnmarkedLocation",
                    Description = "    ",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test Description"
                };

                var unmarkedLocation = new UnmarkedLocation
                {
                    Id = 0,
                    Name = "Test UnmarkedLocation",
                    Description = "",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(unmarkedLocation);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var unmarkedLocationObject = new object();
                var locationAsUnmarkedLocation = new UnmarkedLocation();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                unmarkedLocationObject = responseAsCreateAsActionResult.Value;

                locationAsUnmarkedLocation.Id = (int)unmarkedLocationObject.GetType().GetProperty("Id").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.Name = (string)unmarkedLocationObject.GetType().GetProperty("Name").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.LocationId = (LocationType)unmarkedLocationObject.GetType().GetProperty("LocationId").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.GeographicalDescription = (string)unmarkedLocationObject.GetType().GetProperty("GeographicalDescription").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.Description = (string)unmarkedLocationObject.GetType().GetProperty("Description").GetValue(unmarkedLocationObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(unmarkedLocation.Name, locationAsUnmarkedLocation.Name);
                Assert.Equal(unmarkedLocation.Id, locationAsUnmarkedLocation.Id);
                Assert.Equal(unmarkedLocation.Description, locationAsUnmarkedLocation.Description);
                Assert.Equal(unmarkedLocation.LocationId, locationAsUnmarkedLocation.LocationId);
                Assert.Equal(unmarkedLocation.GeographicalDescription, locationAsUnmarkedLocation.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullForDescriptionAsAUnmarkedLocation_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
            {
                // Arrange

                var createLocationDto = new LocationDto
                {
                    Name = "Test UnmarkedLocation",
                    Description = null,
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test Description"
                };

                var unmarkedLocation = new UnmarkedLocation
                {
                    Id = 0,
                    Name = "Test UnmarkedLocation",
                    Description = null,
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test Description"
                };

                var completedCreateTask = Task<Location>.FromResult(unmarkedLocation);

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                var createdAtActionStatusCode = (int)HttpStatusCode.Created;
                var unmarkedLocationObject = new object();
                var locationAsUnmarkedLocation = new UnmarkedLocation();

                // Act

                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsCreateAsActionResult = (CreatedAtActionResult)response.Result;
                unmarkedLocationObject = responseAsCreateAsActionResult.Value;

                locationAsUnmarkedLocation.Id = (int)unmarkedLocationObject.GetType().GetProperty("Id").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.Name = (string)unmarkedLocationObject.GetType().GetProperty("Name").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.LocationId = (LocationType)unmarkedLocationObject.GetType().GetProperty("LocationId").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.GeographicalDescription = (string)unmarkedLocationObject.GetType().GetProperty("GeographicalDescription").GetValue(unmarkedLocationObject, null);
                locationAsUnmarkedLocation.Description = (string)unmarkedLocationObject.GetType().GetProperty("Description").GetValue(unmarkedLocationObject, null);

                // Assert

                Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
                Assert.Equal(unmarkedLocation.Id, locationAsUnmarkedLocation.Id);
                Assert.Equal(unmarkedLocation.Name, locationAsUnmarkedLocation.Name);
                Assert.Equal(unmarkedLocation.Description, locationAsUnmarkedLocation.Description);
                Assert.Equal(unmarkedLocation.LocationId, locationAsUnmarkedLocation.LocationId);
                Assert.Equal(unmarkedLocation.GeographicalDescription, locationAsUnmarkedLocation.GeographicalDescription);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAUnmarkedLocation_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "      ",
                    Description = "Test",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "Test Description"
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }

            [Fact]
            public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAUnmarkedLocation_ReturnsBadRequest()
            {
                // Arrange
                LocationDto createLocationDto = new LocationDto
                {
                    Name = "Test",
                    Description = "Test",
                    LocationId = LocationType.UnmarkedLocation,
                    GeographicalDescription = "        "
                };

                Location location = null;
                var completedCreateTask = Task<Location>.FromResult(location);
                var badRequest = (int)HttpStatusCode.BadRequest;

                _mockDomain.Setup(x => x.CreateLocation(It.IsAny<LocationDto>()))
                    .ReturnsAsync((Location)completedCreateTask.Result);

                // Act
                var response = await _locationsController.CreateLocation(createLocationDto);
                var responseAsBadRequest = response.Result as BadRequestResult;

                // Assert
                Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
            }
        }
    }

    public class DeleteLocation : LocationController_Tests
    {
        [Fact]
        public async void WhenLocationExists_DeletesLocationAndReturnsOk()
        {
            // Arrange
            var id = 1;
            var isSuccessful = true;

            var completedTask = Task<bool>.FromResult(isSuccessful);

            _mockDomain.Setup(x => x.DeleteLocation(It.IsAny<int>()))
                .ReturnsAsync((bool)completedTask.Result);

            var okActionStatusCode = (int)HttpStatusCode.OK;

            // Act

            var result = await _locationsController.DeleteLocation(id);
            var resultAsOkResult = (OkResult)result.Result;


            // Assert

            Assert.Equal(okActionStatusCode, resultAsOkResult.StatusCode);
        }

        [Fact]
        public async void WhenLocationDoesNotExistOrErrorOccurs_ReturnsBadRequest()
        {
            // Arrange
            bool isSuccessful = false;
            int id = 1;

            var completedTask = Task<bool>.FromResult(isSuccessful);

            _mockDomain.Setup(x => x.DeleteLocation(It.IsAny<int>()))
                .ReturnsAsync((bool)completedTask.Result);

            var badRequestActionStatusCode = (int)HttpStatusCode.BadRequest;

            // Act

            var result = await _locationsController.DeleteLocation(id);
            var resultAsBadRequestResult = (BadRequestResult)result.Result;


            // Assert

            Assert.Equal(badRequestActionStatusCode, resultAsBadRequestResult.StatusCode);
        }
    }
}
