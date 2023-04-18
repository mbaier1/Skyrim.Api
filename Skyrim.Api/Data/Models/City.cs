﻿using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class City : Location
    {
        public City()
        {
            this.LocationId = LocationType.City;
            this.TypeOfLocation = LocationType.City.GetDisplayName();
        }
    }
}
