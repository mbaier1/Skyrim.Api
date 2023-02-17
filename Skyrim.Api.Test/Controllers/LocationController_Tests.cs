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
            Assert.Equal(city.TypeOfLocation, locationAsCity.TypeOfLocation);
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
            Assert.Equal(city.TypeOfLocation, locationAsCity.TypeOfLocation);
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
            Assert.Equal(city.TypeOfLocation, locationAsCity.TypeOfLocation);
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
            Assert.Equal(town.TypeOfLocation, locationAsTown.TypeOfLocation);
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
            Assert.Equal(town.TypeOfLocation, locationAsTown.TypeOfLocation);
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
            Assert.Equal(town.TypeOfLocation, locationAsTown.TypeOfLocation);
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
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAHomestead_ReturnsCreateAtActionWithHomesteadDetails()
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
            Assert.Equal(homestead.TypeOfLocation, locationAsHomestead.TypeOfLocation);
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
                Name = "Test Homestead",
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
            Assert.Equal(homestead.TypeOfLocation, locationAsHomestead.TypeOfLocation);
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
            Assert.Equal(homestead.TypeOfLocation, locationAsHomestead.TypeOfLocation);
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

    public class CreateLocation_AsSettlement : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsASettlement_ReturnsCreateAtActionWithSettlementDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Settlement",
                TypeOfLocation = LocationType.Settlement,
                GeographicalDescription = "Test Description"
            };

            var settlement = new Settlement
            {
                Id = 0,
                Name = "Test Settlement",
                TypeOfLocation = LocationType.Settlement,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(settlement);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsSettlement.TypeOfLocation = (LocationType)settlementObject.GetType().GetProperty("TypeOfLocation").GetValue(settlementObject, null);
            locationAsSettlement.GeographicalDescription = (string)settlementObject.GetType().GetProperty("GeographicalDescription").GetValue(settlementObject, null);
            locationAsSettlement.Description = (string)settlementObject.GetType().GetProperty("Description").GetValue(settlementObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(settlement.Id, locationAsSettlement.Id);
            Assert.Equal(settlement.Name, locationAsSettlement.Name);
            Assert.Equal(settlement.Description, locationAsSettlement.Description);
            Assert.Equal(settlement.TypeOfLocation, locationAsSettlement.TypeOfLocation);
            Assert.Equal(settlement.GeographicalDescription, locationAsSettlement.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionASettlement_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Settlement",
                Description = "    ",
                TypeOfLocation = LocationType.Settlement,
                GeographicalDescription = "Test Description"
            };

            var settlement = new Settlement
            {
                Id = 0,
                Name = "Test Settlement",
                Description = "",
                TypeOfLocation = LocationType.Settlement,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(settlement);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsSettlement.TypeOfLocation = (LocationType)settlementObject.GetType().GetProperty("TypeOfLocation").GetValue(settlementObject, null);
            locationAsSettlement.GeographicalDescription = (string)settlementObject.GetType().GetProperty("GeographicalDescription").GetValue(settlementObject, null);
            locationAsSettlement.Description = (string)settlementObject.GetType().GetProperty("Description").GetValue(settlementObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(settlement.Name, locationAsSettlement.Name);
            Assert.Equal(settlement.Id, locationAsSettlement.Id);
            Assert.Equal(settlement.Description, locationAsSettlement.Description);
            Assert.Equal(settlement.TypeOfLocation, locationAsSettlement.TypeOfLocation);
            Assert.Equal(settlement.GeographicalDescription, locationAsSettlement.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsASettlement_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Settlement",
                Description = null,
                TypeOfLocation = LocationType.Settlement,
                GeographicalDescription = "Test Description"
            };

            var settlement = new Settlement
            {
                Id = 0,
                Name = "Test Settlement",
                Description = null,
                TypeOfLocation = LocationType.Settlement,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(settlement);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsSettlement.TypeOfLocation = (LocationType)settlementObject.GetType().GetProperty("TypeOfLocation").GetValue(settlementObject, null);
            locationAsSettlement.GeographicalDescription = (string)settlementObject.GetType().GetProperty("GeographicalDescription").GetValue(settlementObject, null);
            locationAsSettlement.Description = (string)settlementObject.GetType().GetProperty("Description").GetValue(settlementObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(settlement.Id, locationAsSettlement.Id);
            Assert.Equal(settlement.Name, locationAsSettlement.Name);
            Assert.Equal(settlement.Description, locationAsSettlement.Description);
            Assert.Equal(settlement.TypeOfLocation, locationAsSettlement.TypeOfLocation);
            Assert.Equal(settlement.GeographicalDescription, locationAsSettlement.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsASettlement_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Settlement,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsASettlement_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Settlement,
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

    public class CreateLocation_AsDaedricShrine : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADaedricShrine_ReturnsCreateAtActionWithDaedricShrineDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DaedricShrine",
                TypeOfLocation = LocationType.DaedricShrine,
                GeographicalDescription = "Test Description"
            };

            var daedricShrine = new DaedricShrine
            {
                Id = 0,
                Name = "Test DaedricShrine",
                TypeOfLocation = LocationType.DaedricShrine,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(daedricShrine);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDaedricShrine.TypeOfLocation = (LocationType)daedricShrineObject.GetType().GetProperty("TypeOfLocation").GetValue(daedricShrineObject, null);
            locationAsDaedricShrine.GeographicalDescription = (string)daedricShrineObject.GetType().GetProperty("GeographicalDescription").GetValue(daedricShrineObject, null);
            locationAsDaedricShrine.Description = (string)daedricShrineObject.GetType().GetProperty("Description").GetValue(daedricShrineObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(daedricShrine.Id, locationAsDaedricShrine.Id);
            Assert.Equal(daedricShrine.Name, locationAsDaedricShrine.Name);
            Assert.Equal(daedricShrine.Description, locationAsDaedricShrine.Description);
            Assert.Equal(daedricShrine.TypeOfLocation, locationAsDaedricShrine.TypeOfLocation);
            Assert.Equal(daedricShrine.GeographicalDescription, locationAsDaedricShrine.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADaedricShrine_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DaedricShrine",
                Description = "    ",
                TypeOfLocation = LocationType.DaedricShrine,
                GeographicalDescription = "Test Description"
            };

            var daedricShrine = new DaedricShrine
            {
                Id = 0,
                Name = "Test DaedricShrine",
                Description = "",
                TypeOfLocation = LocationType.DaedricShrine,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(daedricShrine);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDaedricShrine.TypeOfLocation = (LocationType)daedricShrineObject.GetType().GetProperty("TypeOfLocation").GetValue(daedricShrineObject, null);
            locationAsDaedricShrine.GeographicalDescription = (string)daedricShrineObject.GetType().GetProperty("GeographicalDescription").GetValue(daedricShrineObject, null);
            locationAsDaedricShrine.Description = (string)daedricShrineObject.GetType().GetProperty("Description").GetValue(daedricShrineObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(daedricShrine.Name, locationAsDaedricShrine.Name);
            Assert.Equal(daedricShrine.Id, locationAsDaedricShrine.Id);
            Assert.Equal(daedricShrine.Description, locationAsDaedricShrine.Description);
            Assert.Equal(daedricShrine.TypeOfLocation, locationAsDaedricShrine.TypeOfLocation);
            Assert.Equal(daedricShrine.GeographicalDescription, locationAsDaedricShrine.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsADaedricShrine_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DaedricShrine",
                Description = null,
                TypeOfLocation = LocationType.DaedricShrine,
                GeographicalDescription = "Test Description"
            };

            var daedricShrine = new DaedricShrine
            {
                Id = 0,
                Name = "Test DaedricShrine",
                Description = null,
                TypeOfLocation = LocationType.DaedricShrine,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(daedricShrine);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDaedricShrine.TypeOfLocation = (LocationType)daedricShrineObject.GetType().GetProperty("TypeOfLocation").GetValue(daedricShrineObject, null);
            locationAsDaedricShrine.GeographicalDescription = (string)daedricShrineObject.GetType().GetProperty("GeographicalDescription").GetValue(daedricShrineObject, null);
            locationAsDaedricShrine.Description = (string)daedricShrineObject.GetType().GetProperty("Description").GetValue(daedricShrineObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(daedricShrine.Id, locationAsDaedricShrine.Id);
            Assert.Equal(daedricShrine.Name, locationAsDaedricShrine.Name);
            Assert.Equal(daedricShrine.Description, locationAsDaedricShrine.Description);
            Assert.Equal(daedricShrine.TypeOfLocation, locationAsDaedricShrine.TypeOfLocation);
            Assert.Equal(daedricShrine.GeographicalDescription, locationAsDaedricShrine.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADaedricShrine_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.DaedricShrine,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADaedricShrine_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.DaedricShrine,
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

    public class CreateLocation_AsStandingStone : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAStandingStone_ReturnsCreateAtActionWithStandingStoneDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test StandingStone",
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test Description"
            };

            var standingStone = new StandingStone
            {
                Id = 0,
                Name = "Test StandingStone",
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(standingStone);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsStandingStone.TypeOfLocation = (LocationType)standingStoneObject.GetType().GetProperty("TypeOfLocation").GetValue(standingStoneObject, null);
            locationAsStandingStone.GeographicalDescription = (string)standingStoneObject.GetType().GetProperty("GeographicalDescription").GetValue(standingStoneObject, null);
            locationAsStandingStone.Description = (string)standingStoneObject.GetType().GetProperty("Description").GetValue(standingStoneObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(standingStone.Id, locationAsStandingStone.Id);
            Assert.Equal(standingStone.Name, locationAsStandingStone.Name);
            Assert.Equal(standingStone.Description, locationAsStandingStone.Description);
            Assert.Equal(standingStone.TypeOfLocation, locationAsStandingStone.TypeOfLocation);
            Assert.Equal(standingStone.GeographicalDescription, locationAsStandingStone.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAStandingStone_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test StandingStone",
                Description = "    ",
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test Description"
            };

            var standingStone = new StandingStone
            {
                Id = 0,
                Name = "Test StandingStone",
                Description = "",
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(standingStone);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsStandingStone.TypeOfLocation = (LocationType)standingStoneObject.GetType().GetProperty("TypeOfLocation").GetValue(standingStoneObject, null);
            locationAsStandingStone.GeographicalDescription = (string)standingStoneObject.GetType().GetProperty("GeographicalDescription").GetValue(standingStoneObject, null);
            locationAsStandingStone.Description = (string)standingStoneObject.GetType().GetProperty("Description").GetValue(standingStoneObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(standingStone.Name, locationAsStandingStone.Name);
            Assert.Equal(standingStone.Id, locationAsStandingStone.Id);
            Assert.Equal(standingStone.Description, locationAsStandingStone.Description);
            Assert.Equal(standingStone.TypeOfLocation, locationAsStandingStone.TypeOfLocation);
            Assert.Equal(standingStone.GeographicalDescription, locationAsStandingStone.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAStandingStone_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test StandingStone",
                Description = null,
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test Description"
            };

            var standingStone = new StandingStone
            {
                Id = 0,
                Name = "Test StandingStone",
                Description = null,
                TypeOfLocation = LocationType.StandingStone,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(standingStone);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsStandingStone.TypeOfLocation = (LocationType)standingStoneObject.GetType().GetProperty("TypeOfLocation").GetValue(standingStoneObject, null);
            locationAsStandingStone.GeographicalDescription = (string)standingStoneObject.GetType().GetProperty("GeographicalDescription").GetValue(standingStoneObject, null);
            locationAsStandingStone.Description = (string)standingStoneObject.GetType().GetProperty("Description").GetValue(standingStoneObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(standingStone.Id, locationAsStandingStone.Id);
            Assert.Equal(standingStone.Name, locationAsStandingStone.Name);
            Assert.Equal(standingStone.Description, locationAsStandingStone.Description);
            Assert.Equal(standingStone.TypeOfLocation, locationAsStandingStone.TypeOfLocation);
            Assert.Equal(standingStone.GeographicalDescription, locationAsStandingStone.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAStandingStone_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.StandingStone,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAStandingStone_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.StandingStone,
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

    public class CreateLocation_AsLandmark : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsALandmark_ReturnsCreateAtActionWithLandmarkDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Landmark",
                TypeOfLocation = LocationType.Landmark,
                GeographicalDescription = "Test Description"
            };

            var landmark = new Landmark
            {
                Id = 0,
                Name = "Test Landmark",
                TypeOfLocation = LocationType.Landmark,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(landmark);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsLandmark.TypeOfLocation = (LocationType)landmarkObject.GetType().GetProperty("TypeOfLocation").GetValue(landmarkObject, null);
            locationAsLandmark.GeographicalDescription = (string)landmarkObject.GetType().GetProperty("GeographicalDescription").GetValue(landmarkObject, null);
            locationAsLandmark.Description = (string)landmarkObject.GetType().GetProperty("Description").GetValue(landmarkObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(landmark.Id, locationAsLandmark.Id);
            Assert.Equal(landmark.Name, locationAsLandmark.Name);
            Assert.Equal(landmark.Description, locationAsLandmark.Description);
            Assert.Equal(landmark.TypeOfLocation, locationAsLandmark.TypeOfLocation);
            Assert.Equal(landmark.GeographicalDescription, locationAsLandmark.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionALandmark_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Landmark",
                Description = "    ",
                TypeOfLocation = LocationType.Landmark,
                GeographicalDescription = "Test Description"
            };

            var landmark = new Landmark
            {
                Id = 0,
                Name = "Test Landmark",
                Description = "",
                TypeOfLocation = LocationType.Landmark,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(landmark);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsLandmark.TypeOfLocation = (LocationType)landmarkObject.GetType().GetProperty("TypeOfLocation").GetValue(landmarkObject, null);
            locationAsLandmark.GeographicalDescription = (string)landmarkObject.GetType().GetProperty("GeographicalDescription").GetValue(landmarkObject, null);
            locationAsLandmark.Description = (string)landmarkObject.GetType().GetProperty("Description").GetValue(landmarkObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(landmark.Name, locationAsLandmark.Name);
            Assert.Equal(landmark.Id, locationAsLandmark.Id);
            Assert.Equal(landmark.Description, locationAsLandmark.Description);
            Assert.Equal(landmark.TypeOfLocation, locationAsLandmark.TypeOfLocation);
            Assert.Equal(landmark.GeographicalDescription, locationAsLandmark.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsALandmark_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Landmark",
                Description = null,
                TypeOfLocation = LocationType.Landmark,
                GeographicalDescription = "Test Description"
            };

            var landmark = new Landmark
            {
                Id = 0,
                Name = "Test Landmark",
                Description = null,
                TypeOfLocation = LocationType.Landmark,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(landmark);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsLandmark.TypeOfLocation = (LocationType)landmarkObject.GetType().GetProperty("TypeOfLocation").GetValue(landmarkObject, null);
            locationAsLandmark.GeographicalDescription = (string)landmarkObject.GetType().GetProperty("GeographicalDescription").GetValue(landmarkObject, null);
            locationAsLandmark.Description = (string)landmarkObject.GetType().GetProperty("Description").GetValue(landmarkObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(landmark.Id, locationAsLandmark.Id);
            Assert.Equal(landmark.Name, locationAsLandmark.Name);
            Assert.Equal(landmark.Description, locationAsLandmark.Description);
            Assert.Equal(landmark.TypeOfLocation, locationAsLandmark.TypeOfLocation);
            Assert.Equal(landmark.GeographicalDescription, locationAsLandmark.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsALandmark_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Landmark,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsALandmark_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Landmark,
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

    public class CreateLocation_AsCamp : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsACamp_ReturnsCreateAtActionWithCampDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Camp",
                TypeOfLocation = LocationType.Camp,
                GeographicalDescription = "Test Description"
            };

            var camp = new Camp
            {
                Id = 0,
                Name = "Test Camp",
                TypeOfLocation = LocationType.Camp,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(camp);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCamp.TypeOfLocation = (LocationType)campObject.GetType().GetProperty("TypeOfLocation").GetValue(campObject, null);
            locationAsCamp.GeographicalDescription = (string)campObject.GetType().GetProperty("GeographicalDescription").GetValue(campObject, null);
            locationAsCamp.Description = (string)campObject.GetType().GetProperty("Description").GetValue(campObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(camp.Id, locationAsCamp.Id);
            Assert.Equal(camp.Name, locationAsCamp.Name);
            Assert.Equal(camp.Description, locationAsCamp.Description);
            Assert.Equal(camp.TypeOfLocation, locationAsCamp.TypeOfLocation);
            Assert.Equal(camp.GeographicalDescription, locationAsCamp.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionACamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Camp",
                Description = "    ",
                TypeOfLocation = LocationType.Camp,
                GeographicalDescription = "Test Description"
            };

            var camp = new Camp
            {
                Id = 0,
                Name = "Test Camp",
                Description = "",
                TypeOfLocation = LocationType.Camp,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(camp);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCamp.TypeOfLocation = (LocationType)campObject.GetType().GetProperty("TypeOfLocation").GetValue(campObject, null);
            locationAsCamp.GeographicalDescription = (string)campObject.GetType().GetProperty("GeographicalDescription").GetValue(campObject, null);
            locationAsCamp.Description = (string)campObject.GetType().GetProperty("Description").GetValue(campObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(camp.Name, locationAsCamp.Name);
            Assert.Equal(camp.Id, locationAsCamp.Id);
            Assert.Equal(camp.Description, locationAsCamp.Description);
            Assert.Equal(camp.TypeOfLocation, locationAsCamp.TypeOfLocation);
            Assert.Equal(camp.GeographicalDescription, locationAsCamp.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsACamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Camp",
                Description = null,
                TypeOfLocation = LocationType.Camp,
                GeographicalDescription = "Test Description"
            };

            var camp = new Camp
            {
                Id = 0,
                Name = "Test Camp",
                Description = null,
                TypeOfLocation = LocationType.Camp,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(camp);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCamp.TypeOfLocation = (LocationType)campObject.GetType().GetProperty("TypeOfLocation").GetValue(campObject, null);
            locationAsCamp.GeographicalDescription = (string)campObject.GetType().GetProperty("GeographicalDescription").GetValue(campObject, null);
            locationAsCamp.Description = (string)campObject.GetType().GetProperty("Description").GetValue(campObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(camp.Id, locationAsCamp.Id);
            Assert.Equal(camp.Name, locationAsCamp.Name);
            Assert.Equal(camp.Description, locationAsCamp.Description);
            Assert.Equal(camp.TypeOfLocation, locationAsCamp.TypeOfLocation);
            Assert.Equal(camp.GeographicalDescription, locationAsCamp.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsACamp_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Camp,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsACamp_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Camp,
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

    public class CreateLocation_AsCave : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsACave_ReturnsCreateAtActionWithCaveDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Cave",
                TypeOfLocation = LocationType.Cave,
                GeographicalDescription = "Test Description"
            };

            var cave = new Cave
            {
                Id = 0,
                Name = "Test Cave",
                TypeOfLocation = LocationType.Cave,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(cave);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCave.TypeOfLocation = (LocationType)caveObject.GetType().GetProperty("TypeOfLocation").GetValue(caveObject, null);
            locationAsCave.GeographicalDescription = (string)caveObject.GetType().GetProperty("GeographicalDescription").GetValue(caveObject, null);
            locationAsCave.Description = (string)caveObject.GetType().GetProperty("Description").GetValue(caveObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(cave.Id, locationAsCave.Id);
            Assert.Equal(cave.Name, locationAsCave.Name);
            Assert.Equal(cave.Description, locationAsCave.Description);
            Assert.Equal(cave.TypeOfLocation, locationAsCave.TypeOfLocation);
            Assert.Equal(cave.GeographicalDescription, locationAsCave.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionACave_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Cave",
                Description = "    ",
                TypeOfLocation = LocationType.Cave,
                GeographicalDescription = "Test Description"
            };

            var cave = new Cave
            {
                Id = 0,
                Name = "Test Cave",
                Description = "",
                TypeOfLocation = LocationType.Cave,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(cave);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCave.TypeOfLocation = (LocationType)caveObject.GetType().GetProperty("TypeOfLocation").GetValue(caveObject, null);
            locationAsCave.GeographicalDescription = (string)caveObject.GetType().GetProperty("GeographicalDescription").GetValue(caveObject, null);
            locationAsCave.Description = (string)caveObject.GetType().GetProperty("Description").GetValue(caveObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(cave.Name, locationAsCave.Name);
            Assert.Equal(cave.Id, locationAsCave.Id);
            Assert.Equal(cave.Description, locationAsCave.Description);
            Assert.Equal(cave.TypeOfLocation, locationAsCave.TypeOfLocation);
            Assert.Equal(cave.GeographicalDescription, locationAsCave.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsACave_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Cave",
                Description = null,
                TypeOfLocation = LocationType.Cave,
                GeographicalDescription = "Test Description"
            };

            var cave = new Cave
            {
                Id = 0,
                Name = "Test Cave",
                Description = null,
                TypeOfLocation = LocationType.Cave,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(cave);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsCave.TypeOfLocation = (LocationType)caveObject.GetType().GetProperty("TypeOfLocation").GetValue(caveObject, null);
            locationAsCave.GeographicalDescription = (string)caveObject.GetType().GetProperty("GeographicalDescription").GetValue(caveObject, null);
            locationAsCave.Description = (string)caveObject.GetType().GetProperty("Description").GetValue(caveObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(cave.Id, locationAsCave.Id);
            Assert.Equal(cave.Name, locationAsCave.Name);
            Assert.Equal(cave.Description, locationAsCave.Description);
            Assert.Equal(cave.TypeOfLocation, locationAsCave.TypeOfLocation);
            Assert.Equal(cave.GeographicalDescription, locationAsCave.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsACave_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Cave,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsACave_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Cave,
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

    public class CreateLocation_AsClearing : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAClearing_ReturnsCreateAtActionWithClearingDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Clearing",
                TypeOfLocation = LocationType.Clearing,
                GeographicalDescription = "Test Description"
            };

            var clearing = new Clearing
            {
                Id = 0,
                Name = "Test Clearing",
                TypeOfLocation = LocationType.Clearing,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(clearing);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsClearing.TypeOfLocation = (LocationType)clearingObject.GetType().GetProperty("TypeOfLocation").GetValue(clearingObject, null);
            locationAsClearing.GeographicalDescription = (string)clearingObject.GetType().GetProperty("GeographicalDescription").GetValue(clearingObject, null);
            locationAsClearing.Description = (string)clearingObject.GetType().GetProperty("Description").GetValue(clearingObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(clearing.Id, locationAsClearing.Id);
            Assert.Equal(clearing.Name, locationAsClearing.Name);
            Assert.Equal(clearing.Description, locationAsClearing.Description);
            Assert.Equal(clearing.TypeOfLocation, locationAsClearing.TypeOfLocation);
            Assert.Equal(clearing.GeographicalDescription, locationAsClearing.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAClearing_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Clearing",
                Description = "    ",
                TypeOfLocation = LocationType.Clearing,
                GeographicalDescription = "Test Description"
            };

            var clearing = new Clearing
            {
                Id = 0,
                Name = "Test Clearing",
                Description = "",
                TypeOfLocation = LocationType.Clearing,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(clearing);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsClearing.TypeOfLocation = (LocationType)clearingObject.GetType().GetProperty("TypeOfLocation").GetValue(clearingObject, null);
            locationAsClearing.GeographicalDescription = (string)clearingObject.GetType().GetProperty("GeographicalDescription").GetValue(clearingObject, null);
            locationAsClearing.Description = (string)clearingObject.GetType().GetProperty("Description").GetValue(clearingObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(clearing.Name, locationAsClearing.Name);
            Assert.Equal(clearing.Id, locationAsClearing.Id);
            Assert.Equal(clearing.Description, locationAsClearing.Description);
            Assert.Equal(clearing.TypeOfLocation, locationAsClearing.TypeOfLocation);
            Assert.Equal(clearing.GeographicalDescription, locationAsClearing.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAClearing_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Clearing",
                Description = null,
                TypeOfLocation = LocationType.Clearing,
                GeographicalDescription = "Test Description"
            };

            var clearing = new Clearing
            {
                Id = 0,
                Name = "Test Clearing",
                Description = null,
                TypeOfLocation = LocationType.Clearing,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(clearing);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsClearing.TypeOfLocation = (LocationType)clearingObject.GetType().GetProperty("TypeOfLocation").GetValue(clearingObject, null);
            locationAsClearing.GeographicalDescription = (string)clearingObject.GetType().GetProperty("GeographicalDescription").GetValue(clearingObject, null);
            locationAsClearing.Description = (string)clearingObject.GetType().GetProperty("Description").GetValue(clearingObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(clearing.Id, locationAsClearing.Id);
            Assert.Equal(clearing.Name, locationAsClearing.Name);
            Assert.Equal(clearing.Description, locationAsClearing.Description);
            Assert.Equal(clearing.TypeOfLocation, locationAsClearing.TypeOfLocation);
            Assert.Equal(clearing.GeographicalDescription, locationAsClearing.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAClearing_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Clearing,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAClearing_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Clearing,
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

    public class CreateLocation_AsDock : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADock_ReturnsCreateAtActionWithDockDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Dock",
                TypeOfLocation = LocationType.Dock,
                GeographicalDescription = "Test Description"
            };

            var dock = new Dock
            {
                Id = 0,
                Name = "Test Dock",
                TypeOfLocation = LocationType.Dock,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dock);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDock.TypeOfLocation = (LocationType)dockObject.GetType().GetProperty("TypeOfLocation").GetValue(dockObject, null);
            locationAsDock.GeographicalDescription = (string)dockObject.GetType().GetProperty("GeographicalDescription").GetValue(dockObject, null);
            locationAsDock.Description = (string)dockObject.GetType().GetProperty("Description").GetValue(dockObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dock.Id, locationAsDock.Id);
            Assert.Equal(dock.Name, locationAsDock.Name);
            Assert.Equal(dock.Description, locationAsDock.Description);
            Assert.Equal(dock.TypeOfLocation, locationAsDock.TypeOfLocation);
            Assert.Equal(dock.GeographicalDescription, locationAsDock.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADock_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Dock",
                Description = "    ",
                TypeOfLocation = LocationType.Dock,
                GeographicalDescription = "Test Description"
            };

            var dock = new Dock
            {
                Id = 0,
                Name = "Test Dock",
                Description = "",
                TypeOfLocation = LocationType.Dock,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dock);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDock.TypeOfLocation = (LocationType)dockObject.GetType().GetProperty("TypeOfLocation").GetValue(dockObject, null);
            locationAsDock.GeographicalDescription = (string)dockObject.GetType().GetProperty("GeographicalDescription").GetValue(dockObject, null);
            locationAsDock.Description = (string)dockObject.GetType().GetProperty("Description").GetValue(dockObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dock.Name, locationAsDock.Name);
            Assert.Equal(dock.Id, locationAsDock.Id);
            Assert.Equal(dock.Description, locationAsDock.Description);
            Assert.Equal(dock.TypeOfLocation, locationAsDock.TypeOfLocation);
            Assert.Equal(dock.GeographicalDescription, locationAsDock.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsADock_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Dock",
                Description = null,
                TypeOfLocation = LocationType.Dock,
                GeographicalDescription = "Test Description"
            };

            var dock = new Dock
            {
                Id = 0,
                Name = "Test Dock",
                Description = null,
                TypeOfLocation = LocationType.Dock,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dock);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDock.TypeOfLocation = (LocationType)dockObject.GetType().GetProperty("TypeOfLocation").GetValue(dockObject, null);
            locationAsDock.GeographicalDescription = (string)dockObject.GetType().GetProperty("GeographicalDescription").GetValue(dockObject, null);
            locationAsDock.Description = (string)dockObject.GetType().GetProperty("Description").GetValue(dockObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dock.Id, locationAsDock.Id);
            Assert.Equal(dock.Name, locationAsDock.Name);
            Assert.Equal(dock.Description, locationAsDock.Description);
            Assert.Equal(dock.TypeOfLocation, locationAsDock.TypeOfLocation);
            Assert.Equal(dock.GeographicalDescription, locationAsDock.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADock_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Dock,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADock_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Dock,
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

    public class CreateLocation_AsDragonLair : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADragonLair_ReturnsCreateAtActionWithDragonLairDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DragonLair",
                TypeOfLocation = LocationType.DragonLair,
                GeographicalDescription = "Test Description"
            };

            var dragonLair = new DragonLair
            {
                Id = 0,
                Name = "Test DragonLair",
                TypeOfLocation = LocationType.DragonLair,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dragonLair);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDragonLair.TypeOfLocation = (LocationType)dragonLairObject.GetType().GetProperty("TypeOfLocation").GetValue(dragonLairObject, null);
            locationAsDragonLair.GeographicalDescription = (string)dragonLairObject.GetType().GetProperty("GeographicalDescription").GetValue(dragonLairObject, null);
            locationAsDragonLair.Description = (string)dragonLairObject.GetType().GetProperty("Description").GetValue(dragonLairObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dragonLair.Id, locationAsDragonLair.Id);
            Assert.Equal(dragonLair.Name, locationAsDragonLair.Name);
            Assert.Equal(dragonLair.Description, locationAsDragonLair.Description);
            Assert.Equal(dragonLair.TypeOfLocation, locationAsDragonLair.TypeOfLocation);
            Assert.Equal(dragonLair.GeographicalDescription, locationAsDragonLair.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADragonLair_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DragonLair",
                Description = "    ",
                TypeOfLocation = LocationType.DragonLair,
                GeographicalDescription = "Test Description"
            };

            var dragonLair = new DragonLair
            {
                Id = 0,
                Name = "Test DragonLair",
                Description = "",
                TypeOfLocation = LocationType.DragonLair,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dragonLair);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDragonLair.TypeOfLocation = (LocationType)dragonLairObject.GetType().GetProperty("TypeOfLocation").GetValue(dragonLairObject, null);
            locationAsDragonLair.GeographicalDescription = (string)dragonLairObject.GetType().GetProperty("GeographicalDescription").GetValue(dragonLairObject, null);
            locationAsDragonLair.Description = (string)dragonLairObject.GetType().GetProperty("Description").GetValue(dragonLairObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dragonLair.Name, locationAsDragonLair.Name);
            Assert.Equal(dragonLair.Id, locationAsDragonLair.Id);
            Assert.Equal(dragonLair.Description, locationAsDragonLair.Description);
            Assert.Equal(dragonLair.TypeOfLocation, locationAsDragonLair.TypeOfLocation);
            Assert.Equal(dragonLair.GeographicalDescription, locationAsDragonLair.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsADragonLair_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DragonLair",
                Description = null,
                TypeOfLocation = LocationType.DragonLair,
                GeographicalDescription = "Test Description"
            };

            var dragonLair = new DragonLair
            {
                Id = 0,
                Name = "Test DragonLair",
                Description = null,
                TypeOfLocation = LocationType.DragonLair,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dragonLair);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDragonLair.TypeOfLocation = (LocationType)dragonLairObject.GetType().GetProperty("TypeOfLocation").GetValue(dragonLairObject, null);
            locationAsDragonLair.GeographicalDescription = (string)dragonLairObject.GetType().GetProperty("GeographicalDescription").GetValue(dragonLairObject, null);
            locationAsDragonLair.Description = (string)dragonLairObject.GetType().GetProperty("Description").GetValue(dragonLairObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dragonLair.Id, locationAsDragonLair.Id);
            Assert.Equal(dragonLair.Name, locationAsDragonLair.Name);
            Assert.Equal(dragonLair.Description, locationAsDragonLair.Description);
            Assert.Equal(dragonLair.TypeOfLocation, locationAsDragonLair.TypeOfLocation);
            Assert.Equal(dragonLair.GeographicalDescription, locationAsDragonLair.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADragonLair_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.DragonLair,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADragonLair_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.DragonLair,
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

    public class CreateLocation_AsDwarvenRuin : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsADwarvenRuin_ReturnsCreateAtActionWithDwarvenRuinDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DwarvenRuin",
                TypeOfLocation = LocationType.DwarvenRuin,
                GeographicalDescription = "Test Description"
            };

            var dwarvenRuin = new DwarvenRuin
            {
                Id = 0,
                Name = "Test DwarvenRuin",
                TypeOfLocation = LocationType.DwarvenRuin,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dwarvenRuin);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDwarvenRuin.TypeOfLocation = (LocationType)dwarvenRuinObject.GetType().GetProperty("TypeOfLocation").GetValue(dwarvenRuinObject, null);
            locationAsDwarvenRuin.GeographicalDescription = (string)dwarvenRuinObject.GetType().GetProperty("GeographicalDescription").GetValue(dwarvenRuinObject, null);
            locationAsDwarvenRuin.Description = (string)dwarvenRuinObject.GetType().GetProperty("Description").GetValue(dwarvenRuinObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dwarvenRuin.Id, locationAsDwarvenRuin.Id);
            Assert.Equal(dwarvenRuin.Name, locationAsDwarvenRuin.Name);
            Assert.Equal(dwarvenRuin.Description, locationAsDwarvenRuin.Description);
            Assert.Equal(dwarvenRuin.TypeOfLocation, locationAsDwarvenRuin.TypeOfLocation);
            Assert.Equal(dwarvenRuin.GeographicalDescription, locationAsDwarvenRuin.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionADwarvenRuin_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DwarvenRuin",
                Description = "    ",
                TypeOfLocation = LocationType.DwarvenRuin,
                GeographicalDescription = "Test Description"
            };

            var dwarvenRuin = new DwarvenRuin
            {
                Id = 0,
                Name = "Test DwarvenRuin",
                Description = "",
                TypeOfLocation = LocationType.DwarvenRuin,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dwarvenRuin);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDwarvenRuin.TypeOfLocation = (LocationType)dwarvenRuinObject.GetType().GetProperty("TypeOfLocation").GetValue(dwarvenRuinObject, null);
            locationAsDwarvenRuin.GeographicalDescription = (string)dwarvenRuinObject.GetType().GetProperty("GeographicalDescription").GetValue(dwarvenRuinObject, null);
            locationAsDwarvenRuin.Description = (string)dwarvenRuinObject.GetType().GetProperty("Description").GetValue(dwarvenRuinObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dwarvenRuin.Name, locationAsDwarvenRuin.Name);
            Assert.Equal(dwarvenRuin.Id, locationAsDwarvenRuin.Id);
            Assert.Equal(dwarvenRuin.Description, locationAsDwarvenRuin.Description);
            Assert.Equal(dwarvenRuin.TypeOfLocation, locationAsDwarvenRuin.TypeOfLocation);
            Assert.Equal(dwarvenRuin.GeographicalDescription, locationAsDwarvenRuin.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsADwarvenRuin_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test DwarvenRuin",
                Description = null,
                TypeOfLocation = LocationType.DwarvenRuin,
                GeographicalDescription = "Test Description"
            };

            var dwarvenRuin = new DwarvenRuin
            {
                Id = 0,
                Name = "Test DwarvenRuin",
                Description = null,
                TypeOfLocation = LocationType.DwarvenRuin,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(dwarvenRuin);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsDwarvenRuin.TypeOfLocation = (LocationType)dwarvenRuinObject.GetType().GetProperty("TypeOfLocation").GetValue(dwarvenRuinObject, null);
            locationAsDwarvenRuin.GeographicalDescription = (string)dwarvenRuinObject.GetType().GetProperty("GeographicalDescription").GetValue(dwarvenRuinObject, null);
            locationAsDwarvenRuin.Description = (string)dwarvenRuinObject.GetType().GetProperty("Description").GetValue(dwarvenRuinObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(dwarvenRuin.Id, locationAsDwarvenRuin.Id);
            Assert.Equal(dwarvenRuin.Name, locationAsDwarvenRuin.Name);
            Assert.Equal(dwarvenRuin.Description, locationAsDwarvenRuin.Description);
            Assert.Equal(dwarvenRuin.TypeOfLocation, locationAsDwarvenRuin.TypeOfLocation);
            Assert.Equal(dwarvenRuin.GeographicalDescription, locationAsDwarvenRuin.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsADwarvenRuin_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.DwarvenRuin,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsADwarvenRuin_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.DwarvenRuin,
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

    public class CreateLocation_AsFarm : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAFarm_ReturnsCreateAtActionWithFarmDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test grove",
                TypeOfLocation = LocationType.Farm,
                GeographicalDescription = "Test Description"
            };

            var farm = new Farm
            {
                Id = 0,
                Name = "Test Farm",
                TypeOfLocation = LocationType.Farm,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(farm);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsFarm.TypeOfLocation = (LocationType)farmObject.GetType().GetProperty("TypeOfLocation").GetValue(farmObject, null);
            locationAsFarm.GeographicalDescription = (string)farmObject.GetType().GetProperty("GeographicalDescription").GetValue(farmObject, null);
            locationAsFarm.Description = (string)farmObject.GetType().GetProperty("Description").GetValue(farmObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(farm.Id, locationAsFarm.Id);
            Assert.Equal(farm.Name, locationAsFarm.Name);
            Assert.Equal(farm.Description, locationAsFarm.Description);
            Assert.Equal(farm.TypeOfLocation, locationAsFarm.TypeOfLocation);
            Assert.Equal(farm.GeographicalDescription, locationAsFarm.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAFarm_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Farm",
                Description = "    ",
                TypeOfLocation = LocationType.Farm,
                GeographicalDescription = "Test Description"
            };

            var farm = new Farm
            {
                Id = 0,
                Name = "Test Farm",
                Description = "",
                TypeOfLocation = LocationType.Farm,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(farm);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsFarm.TypeOfLocation = (LocationType)farmObject.GetType().GetProperty("TypeOfLocation").GetValue(farmObject, null);
            locationAsFarm.GeographicalDescription = (string)farmObject.GetType().GetProperty("GeographicalDescription").GetValue(farmObject, null);
            locationAsFarm.Description = (string)farmObject.GetType().GetProperty("Description").GetValue(farmObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(farm.Name, locationAsFarm.Name);
            Assert.Equal(farm.Id, locationAsFarm.Id);
            Assert.Equal(farm.Description, locationAsFarm.Description);
            Assert.Equal(farm.TypeOfLocation, locationAsFarm.TypeOfLocation);
            Assert.Equal(farm.GeographicalDescription, locationAsFarm.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAFarm_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Farm",
                Description = null,
                TypeOfLocation = LocationType.Farm,
                GeographicalDescription = "Test Description"
            };

            var farm = new Farm
            {
                Id = 0,
                Name = "Test Farm",
                Description = null,
                TypeOfLocation = LocationType.Farm,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(farm);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsFarm.TypeOfLocation = (LocationType)farmObject.GetType().GetProperty("TypeOfLocation").GetValue(farmObject, null);
            locationAsFarm.GeographicalDescription = (string)farmObject.GetType().GetProperty("GeographicalDescription").GetValue(farmObject, null);
            locationAsFarm.Description = (string)farmObject.GetType().GetProperty("Description").GetValue(farmObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(farm.Id, locationAsFarm.Id);
            Assert.Equal(farm.Name, locationAsFarm.Name);
            Assert.Equal(farm.Description, locationAsFarm.Description);
            Assert.Equal(farm.TypeOfLocation, locationAsFarm.TypeOfLocation);
            Assert.Equal(farm.GeographicalDescription, locationAsFarm.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAFarm_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Farm,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAFarm_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Farm,
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

    public class CreateLocation_AsFort : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAFort_ReturnsCreateAtActionWithFortDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Fort",
                TypeOfLocation = LocationType.Fort,
                GeographicalDescription = "Test Description"
            };

            var fort = new Fort
            {
                Id = 0,
                Name = "Test Fort",
                TypeOfLocation = LocationType.Fort,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(fort);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsFort.TypeOfLocation = (LocationType)fortObject.GetType().GetProperty("TypeOfLocation").GetValue(fortObject, null);
            locationAsFort.GeographicalDescription = (string)fortObject.GetType().GetProperty("GeographicalDescription").GetValue(fortObject, null);
            locationAsFort.Description = (string)fortObject.GetType().GetProperty("Description").GetValue(fortObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(fort.Id, locationAsFort.Id);
            Assert.Equal(fort.Name, locationAsFort.Name);
            Assert.Equal(fort.Description, locationAsFort.Description);
            Assert.Equal(fort.TypeOfLocation, locationAsFort.TypeOfLocation);
            Assert.Equal(fort.GeographicalDescription, locationAsFort.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAFort_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Fort",
                Description = "    ",
                TypeOfLocation = LocationType.Fort,
                GeographicalDescription = "Test Description"
            };

            var fort = new Fort
            {
                Id = 0,
                Name = "Test Fort",
                Description = "",
                TypeOfLocation = LocationType.Fort,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(fort);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsFort.TypeOfLocation = (LocationType)fortObject.GetType().GetProperty("TypeOfLocation").GetValue(fortObject, null);
            locationAsFort.GeographicalDescription = (string)fortObject.GetType().GetProperty("GeographicalDescription").GetValue(fortObject, null);
            locationAsFort.Description = (string)fortObject.GetType().GetProperty("Description").GetValue(fortObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(fort.Name, locationAsFort.Name);
            Assert.Equal(fort.Id, locationAsFort.Id);
            Assert.Equal(fort.Description, locationAsFort.Description);
            Assert.Equal(fort.TypeOfLocation, locationAsFort.TypeOfLocation);
            Assert.Equal(fort.GeographicalDescription, locationAsFort.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAFort_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Fort",
                Description = null,
                TypeOfLocation = LocationType.Fort,
                GeographicalDescription = "Test Description"
            };

            var fort = new Fort
            {
                Id = 0,
                Name = "Test Fort",
                Description = null,
                TypeOfLocation = LocationType.Fort,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(fort);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsFort.TypeOfLocation = (LocationType)fortObject.GetType().GetProperty("TypeOfLocation").GetValue(fortObject, null);
            locationAsFort.GeographicalDescription = (string)fortObject.GetType().GetProperty("GeographicalDescription").GetValue(fortObject, null);
            locationAsFort.Description = (string)fortObject.GetType().GetProperty("Description").GetValue(fortObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(fort.Id, locationAsFort.Id);
            Assert.Equal(fort.Name, locationAsFort.Name);
            Assert.Equal(fort.Description, locationAsFort.Description);
            Assert.Equal(fort.TypeOfLocation, locationAsFort.TypeOfLocation);
            Assert.Equal(fort.GeographicalDescription, locationAsFort.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAFort_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Fort,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAFort_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Fort,
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

    public class CreateLocation_AsGiantCamp : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAGiantCamp_ReturnsCreateAtActionWithGiantCampDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test GiantCamp",
                TypeOfLocation = LocationType.GiantCamp,
                GeographicalDescription = "Test Description"
            };

            var giantCamp = new GiantCamp
            {
                Id = 0,
                Name = "Test GiantCamp",
                TypeOfLocation = LocationType.GiantCamp,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(giantCamp);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsGiantCamp.TypeOfLocation = (LocationType)giantCampObject.GetType().GetProperty("TypeOfLocation").GetValue(giantCampObject, null);
            locationAsGiantCamp.GeographicalDescription = (string)giantCampObject.GetType().GetProperty("GeographicalDescription").GetValue(giantCampObject, null);
            locationAsGiantCamp.Description = (string)giantCampObject.GetType().GetProperty("Description").GetValue(giantCampObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(giantCamp.Id, locationAsGiantCamp.Id);
            Assert.Equal(giantCamp.Name, locationAsGiantCamp.Name);
            Assert.Equal(giantCamp.Description, locationAsGiantCamp.Description);
            Assert.Equal(giantCamp.TypeOfLocation, locationAsGiantCamp.TypeOfLocation);
            Assert.Equal(giantCamp.GeographicalDescription, locationAsGiantCamp.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAGiantCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test GiantCamp",
                Description = "    ",
                TypeOfLocation = LocationType.GiantCamp,
                GeographicalDescription = "Test Description"
            };

            var giantCamp = new GiantCamp
            {
                Id = 0,
                Name = "Test GiantCamp",
                Description = "",
                TypeOfLocation = LocationType.GiantCamp,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(giantCamp);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsGiantCamp.TypeOfLocation = (LocationType)giantCampObject.GetType().GetProperty("TypeOfLocation").GetValue(giantCampObject, null);
            locationAsGiantCamp.GeographicalDescription = (string)giantCampObject.GetType().GetProperty("GeographicalDescription").GetValue(giantCampObject, null);
            locationAsGiantCamp.Description = (string)giantCampObject.GetType().GetProperty("Description").GetValue(giantCampObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(giantCamp.Name, locationAsGiantCamp.Name);
            Assert.Equal(giantCamp.Id, locationAsGiantCamp.Id);
            Assert.Equal(giantCamp.Description, locationAsGiantCamp.Description);
            Assert.Equal(giantCamp.TypeOfLocation, locationAsGiantCamp.TypeOfLocation);
            Assert.Equal(giantCamp.GeographicalDescription, locationAsGiantCamp.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAGiantCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test GiantCamp",
                Description = null,
                TypeOfLocation = LocationType.GiantCamp,
                GeographicalDescription = "Test Description"
            };

            var giantCamp = new GiantCamp
            {
                Id = 0,
                Name = "Test GiantCamp",
                Description = null,
                TypeOfLocation = LocationType.GiantCamp,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(giantCamp);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsGiantCamp.TypeOfLocation = (LocationType)giantCampObject.GetType().GetProperty("TypeOfLocation").GetValue(giantCampObject, null);
            locationAsGiantCamp.GeographicalDescription = (string)giantCampObject.GetType().GetProperty("GeographicalDescription").GetValue(giantCampObject, null);
            locationAsGiantCamp.Description = (string)giantCampObject.GetType().GetProperty("Description").GetValue(giantCampObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(giantCamp.Id, locationAsGiantCamp.Id);
            Assert.Equal(giantCamp.Name, locationAsGiantCamp.Name);
            Assert.Equal(giantCamp.Description, locationAsGiantCamp.Description);
            Assert.Equal(giantCamp.TypeOfLocation, locationAsGiantCamp.TypeOfLocation);
            Assert.Equal(giantCamp.GeographicalDescription, locationAsGiantCamp.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAGiantCamp_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.GiantCamp,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAGiantCamp_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.GiantCamp,
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

    public class CreateLocation_AsGrove : LocationController_Tests
    {
        [Fact]
        public async void WhenCreateLocationDtoHasRequiredValidPropertiesAsAGrove_ReturnsCreateAtActionWithGroveDetails()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Grove",
                TypeOfLocation = LocationType.Grove,
                GeographicalDescription = "Test Description"
            };

            var grove = new Grove
            {
                Id = 0,
                Name = "Test Grove",
                TypeOfLocation = LocationType.Grove,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(grove);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsGrove.TypeOfLocation = (LocationType)groveObject.GetType().GetProperty("TypeOfLocation").GetValue(groveObject, null);
            locationAsGrove.GeographicalDescription = (string)groveObject.GetType().GetProperty("GeographicalDescription").GetValue(groveObject, null);
            locationAsGrove.Description = (string)groveObject.GetType().GetProperty("Description").GetValue(groveObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(grove.Id, locationAsGrove.Id);
            Assert.Equal(grove.Name, locationAsGrove.Name);
            Assert.Equal(grove.Description, locationAsGrove.Description);
            Assert.Equal(grove.TypeOfLocation, locationAsGrove.TypeOfLocation);
            Assert.Equal(grove.GeographicalDescription, locationAsGrove.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasEmptySpacesForDescriptionAGrove_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Grove",
                Description = "    ",
                TypeOfLocation = LocationType.Grove,
                GeographicalDescription = "Test Description"
            };

            var grove = new Grove
            {
                Id = 0,
                Name = "Test Grove",
                Description = "",
                TypeOfLocation = LocationType.Grove,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(grove);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsGrove.TypeOfLocation = (LocationType)groveObject.GetType().GetProperty("TypeOfLocation").GetValue(groveObject, null);
            locationAsGrove.GeographicalDescription = (string)groveObject.GetType().GetProperty("GeographicalDescription").GetValue(groveObject, null);
            locationAsGrove.Description = (string)groveObject.GetType().GetProperty("Description").GetValue(groveObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(grove.Name, locationAsGrove.Name);
            Assert.Equal(grove.Id, locationAsGrove.Id);
            Assert.Equal(grove.Description, locationAsGrove.Description);
            Assert.Equal(grove.TypeOfLocation, locationAsGrove.TypeOfLocation);
            Assert.Equal(grove.GeographicalDescription, locationAsGrove.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullForDescriptionAsAGiantCamp_ReturnsCreatedAtActionWithLocationDetailsWithEmptyDescription()
        {
            // Arrange

            var createLocationDto = new CreateLocationDto
            {
                Name = "Test Grove",
                Description = null,
                TypeOfLocation = LocationType.Grove,
                GeographicalDescription = "Test Description"
            };

            var grove = new Grove
            {
                Id = 0,
                Name = "Test Grove",
                Description = null,
                TypeOfLocation = LocationType.Grove,
                GeographicalDescription = "Test Description"
            };

            var completedCreateTask = Task<Location>.FromResult(grove);

            _mockDomain.Setup(x => x.CreateLocation(It.IsAny<CreateLocationDto>()))
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
            locationAsGrove.TypeOfLocation = (LocationType)groveObject.GetType().GetProperty("TypeOfLocation").GetValue(groveObject, null);
            locationAsGrove.GeographicalDescription = (string)groveObject.GetType().GetProperty("GeographicalDescription").GetValue(groveObject, null);
            locationAsGrove.Description = (string)groveObject.GetType().GetProperty("Description").GetValue(groveObject, null);

            // Assert

            Assert.Equal(createdAtActionStatusCode, responseAsCreateAsActionResult.StatusCode);
            Assert.Equal(grove.Id, locationAsGrove.Id);
            Assert.Equal(grove.Name, locationAsGrove.Name);
            Assert.Equal(grove.Description, locationAsGrove.Description);
            Assert.Equal(grove.TypeOfLocation, locationAsGrove.TypeOfLocation);
            Assert.Equal(grove.GeographicalDescription, locationAsGrove.GeographicalDescription);
        }

        [Fact]
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForNameAsAGrove_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "      ",
                Description = "Test",
                TypeOfLocation = LocationType.Grove,
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
        public async void WhenCreateLocationDtoHasNullOrWhiteSpaceForGeogrpahicalDescriptionAsAGrove_ReturnsBadRequest()
        {
            // Arrange
            CreateLocationDto createLocationDto = new CreateLocationDto
            {
                Name = "Test",
                Description = "Test",
                TypeOfLocation = LocationType.Grove,
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
