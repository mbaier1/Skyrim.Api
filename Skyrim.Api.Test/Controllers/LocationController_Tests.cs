using Microsoft.AspNetCore.Mvc;
using Moq;
using Skyrim.Api.Controllers;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

    public class CreateLocation_AsCity : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsACity_ReturnsCreateAtActionWithCityDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test City",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test Description"
            };

            var city = new City
            {
                Id = 0,
                Name = "Test City",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(city);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCity.TypeOfLocation = (LocationType)cityObject.GetType().GetProperty("TypeOfLocation").GetValue(cityObject, null);
            locationAsCity.GeographicalDescription = (string)cityObject.GetType().GetProperty("GeographicalDescription").GetValue(cityObject, null);
            locationAsCity.Description = (string)cityObject.GetType().GetProperty("Description").GetValue(cityObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(city.Id, locationAsCity.Id);
            Assert.Equal(city.Name, locationAsCity.Name);
            Assert.Equal(city.Description, locationAsCity.Description);
            Assert.Equal(LocationType.City, locationAsCity.TypeOfLocation);
            Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAsAnyLocation_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test City",
                Description = "    ",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test Description"
            };

            var city = new City
            {
                Id = 0,
                Name = "Test City",
                Description = "",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(city);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCity.TypeOfLocation = (LocationType)cityObject.GetType().GetProperty("TypeOfLocation").GetValue(cityObject, null);
            locationAsCity.GeographicalDescription = (string)cityObject.GetType().GetProperty("GeographicalDescription").GetValue(cityObject, null);
            locationAsCity.Description = (string)cityObject.GetType().GetProperty("Description").GetValue(cityObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(city.Name, locationAsCity.Name);
            Assert.Equal(city.Id, locationAsCity.Id);
            Assert.Equal(city.Description, locationAsCity.Description);
            Assert.Equal(LocationType.City, locationAsCity.TypeOfLocation);
            Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAnyLocation_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test City",
                Description = null,
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test Description"
            };

            var city = new City
            {
                Id = 0,
                Name = "Test City",
                Description = null,
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(city);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCity.TypeOfLocation = (LocationType)cityObject.GetType().GetProperty("TypeOfLocation").GetValue(cityObject, null);
            locationAsCity.GeographicalDescription = (string)cityObject.GetType().GetProperty("GeographicalDescription").GetValue(cityObject, null);
            locationAsCity.Description = (string)cityObject.GetType().GetProperty("Description").GetValue(cityObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(city.Id, locationAsCity.Id);
            Assert.Equal(city.Name, locationAsCity.Name);
            Assert.Equal(city.Description, locationAsCity.Description);
            Assert.Equal(LocationType.City, locationAsCity.TypeOfLocation);
            Assert.Equal(city.GeographicalDescription, locationAsCity.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasInvalidModelState_ReturnsBadRequest()
        {
            // Arrange

            CreateLocationDto createLocationDto = new CreateLocationDto();
            _locationsController.ModelState.AddModelError("fakeError", "fakeError");
            var badRequest = (int)HttpStatusCode.BadRequest;


            // Act

            var result = await _locationsController.CreateLocation(createLocationDto);
            var resultAsBadRequest = result.Result as BadRequestResult;

            // Assert

            Assert.Equal(badRequest, resultAsBadRequest.StatusCode);

        }

        [Fact]
        public async void WhenCreateLocationDtoHasInvalidOrNullLocationType_ReturnsBadRequest()
        {
            // Arrange

            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test City",
                GeographicalDescription = "Test Description"
            };
            Location location = null;

            var completedCreateTask = Task<Location>.FromResult(location);
            var badRequest = (int)HttpStatusCode.BadRequest;

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>())).ReturnsAsync((Location)completedCreateTask.Result);

            // Act

            var response = await _locationsController.CreateLocation(createLocationDto);
            var responseAsBadRequest = response.Result as BadRequestResult;


            // Assert

            Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForName_ReturnsBadRequest()
        {
            // Arrange

            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "Test Description"
            };

            Location location = null;
            var completedCreateTask = Task<Location>.FromResult(location);
            var badRequest = (int)HttpStatusCode.BadRequest;

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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

            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.City,
                GeographicalDescription = "        "
            };

            Location location = null;
            var completedCreateTask = Task<Location>.FromResult(location);
            var badRequest = (int)HttpStatusCode.BadRequest;

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
                .ReturnsAsync((Location)completedCreateTask.Result);

            // Act

            var response = await _locationsController.CreateLocation(createLocationDto);
            var responseAsBadRequest = response.Result as BadRequestResult;

            // Assert
            Assert.Equal(badRequest, responseAsBadRequest.StatusCode);
        }
    }
}
