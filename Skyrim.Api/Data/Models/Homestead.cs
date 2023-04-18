﻿using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Homestead : Location
    {
        public Homestead()
        {
            this.LocationId = LocationType.Homestead;
            this.TypeOfLocation = LocationType.Homestead.GetDisplayName();
        }
    }
}
