﻿using Microsoft.OpenApi.Extensions;
using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Shipwreck : Location
    {
        public Shipwreck()
        {
            this.LocationId = LocationType.Shipwreck;
            this.TypeOfLocation = LocationType.Shipwreck.GetDisplayName();
        }
    }
}
