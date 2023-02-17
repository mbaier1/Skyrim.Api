﻿using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Fort : Location
    {
        public Fort()
        {
            this.TypeOfLocation = LocationType.Fort;
        }
    }
}
