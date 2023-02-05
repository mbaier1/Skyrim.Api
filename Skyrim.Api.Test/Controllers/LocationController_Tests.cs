using Microsoft.AspNetCore.Mvc;
using Moq;
using Skyrim.Api.Controllers;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;
using Skyrim.Api.Data.Models;
using Skyrim.Api.Domain.DTOs;
using Skyrim.Api.Domain.Interfaces;
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
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAsACity_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
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
        public async void WhenCreateLocationDtoHasNullForDescriptionAsACity_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
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

    public class CreateLocation_AsTown : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsATown_ReturnsCreateAtActionWithTownDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Town",
                TypeOfLocation = LocationType.Town,
                GeographicalDescription = "Test Description"
            };

            var town = new Town
            {
                Id = 0,
                Name = "Test Town",
                TypeOfLocation = LocationType.Town,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(town);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsTown.TypeOfLocation = (LocationType)townObject.GetType().GetProperty("TypeOfLocation").GetValue(townObject, null);
            locationAsTown.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
            locationAsTown.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(town.Id, locationAsTown.Id);
            Assert.Equal(town.Name, locationAsTown.Name);
            Assert.Equal(town.Description, locationAsTown.Description);
            Assert.Equal(LocationType.Town, locationAsTown.TypeOfLocation);
            Assert.Equal(town.GeographicalDescription, locationAsTown.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionATown_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Town",
                Description = "    ",
                TypeOfLocation = LocationType.Town,
                GeographicalDescription = "Test Description"
            };

            var town = new Town
            {
                Id = 0,
                Name = "Test Town",
                Description = "",
                TypeOfLocation = LocationType.Town,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(town);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsTown.TypeOfLocation = (LocationType)townObject.GetType().GetProperty("TypeOfLocation").GetValue(townObject, null);
            locationAsTown.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
            locationAsTown.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(town.Name, locationAsTown.Name);
            Assert.Equal(town.Id, locationAsTown.Id);
            Assert.Equal(town.Description, locationAsTown.Description);
            Assert.Equal(LocationType.Town, locationAsTown.TypeOfLocation);
            Assert.Equal(town.GeographicalDescription, locationAsTown.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsATown_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Town",
                Description = null,
                TypeOfLocation = LocationType.Town,
                GeographicalDescription = "Test Description"
            };

            var town = new Town
            {
                Id = 0,
                Name = "Test Town",
                Description = null,
                TypeOfLocation = LocationType.Town,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(town);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsTown.TypeOfLocation = (LocationType)townObject.GetType().GetProperty("TypeOfLocation").GetValue(townObject, null);
            locationAsTown.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
            locationAsTown.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(town.Id, locationAsTown.Id);
            Assert.Equal(town.Name, locationAsTown.Name);
            Assert.Equal(town.Description, locationAsTown.Description);
            Assert.Equal(LocationType.Town, locationAsTown.TypeOfLocation);
            Assert.Equal(town.GeographicalDescription, locationAsTown.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsATown_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Town,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsATown_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Town,
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

    public class CreateLocation_AsHomestead : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAHomestead_ReturnsCreateAtActionWithTownDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Homestead",
                TypeOfLocation = LocationType.Homestead,
                GeographicalDescription = "Test Description"
            };

            var homestead = new Homestead
            {
                Id = 0,
                Name = "Test Homestead",
                TypeOfLocation = LocationType.Homestead,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(homestead);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsHomestead.TypeOfLocation = (LocationType)townObject.GetType().GetProperty("TypeOfLocation").GetValue(townObject, null);
            locationAsHomestead.GeographicalDescription = (string)townObject.GetType().GetProperty("GeographicalDescription").GetValue(townObject, null);
            locationAsHomestead.Description = (string)townObject.GetType().GetProperty("Description").GetValue(townObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(homestead.Id, locationAsHomestead.Id);
            Assert.Equal(homestead.Name, locationAsHomestead.Name);
            Assert.Equal(homestead.Description, locationAsHomestead.Description);
            Assert.Equal(LocationType.Homestead, locationAsHomestead.TypeOfLocation);
            Assert.Equal(homestead.GeographicalDescription, locationAsHomestead.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAHomestead_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Homestead",
                Description = "    ",
                TypeOfLocation = LocationType.Homestead,
                GeographicalDescription = "Test Description"
            };

            var homestead = new Homestead
            {
                Id = 0,
                Name = "Test homestead",
                Description = "",
                TypeOfLocation = LocationType.Homestead,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(homestead);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsHomestead.TypeOfLocation = (LocationType)homesteadObject.GetType().GetProperty("TypeOfLocation").GetValue(homesteadObject, null);
            locationAsHomestead.GeographicalDescription = (string)homesteadObject.GetType().GetProperty("GeographicalDescription").GetValue(homesteadObject, null);
            locationAsHomestead.Description = (string)homesteadObject.GetType().GetProperty("Description").GetValue(homesteadObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(homestead.Name, locationAsHomestead.Name);
            Assert.Equal(homestead.Id, locationAsHomestead.Id);
            Assert.Equal(homestead.Description, locationAsHomestead.Description);
            Assert.Equal(LocationType.Homestead, locationAsHomestead.TypeOfLocation);
            Assert.Equal(homestead.GeographicalDescription, locationAsHomestead.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAHomestead_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Homestead",
                Description = null,
                TypeOfLocation = LocationType.Homestead,
                GeographicalDescription = "Test Description"
            };

            var homestead = new Homestead
            {
                Id = 0,
                Name = "Test Homestead",
                Description = null,
                TypeOfLocation = LocationType.Homestead,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(homestead);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsHomestead.TypeOfLocation = (LocationType)homesteadObject.GetType().GetProperty("TypeOfLocation").GetValue(homesteadObject, null);
            locationAsHomestead.GeographicalDescription = (string)homesteadObject.GetType().GetProperty("GeographicalDescription").GetValue(homesteadObject, null);
            locationAsHomestead.Description = (string)homesteadObject.GetType().GetProperty("Description").GetValue(homesteadObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(homestead.Id, locationAsHomestead.Id);
            Assert.Equal(homestead.Name, locationAsHomestead.Name);
            Assert.Equal(homestead.Description, locationAsHomestead.Description);
            Assert.Equal(LocationType.Homestead, locationAsHomestead.TypeOfLocation);
            Assert.Equal(homestead.GeographicalDescription, locationAsHomestead.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAHomestead_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Homestead,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsATown_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Homestead,
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
