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
            Assert.Equal(clearing.Id, locationAsCave.Id);
            Assert.Equal(clearing.Name, locationAsCave.Name);
            Assert.Equal(clearing.Description, locationAsCave.Description);
            Assert.Equal(clearing.TypeOfLocation, locationAsCave.TypeOfLocation);
            Assert.Equal(clearing.GeographicalDescription, locationAsCave.GeographicalDescription);
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
}
