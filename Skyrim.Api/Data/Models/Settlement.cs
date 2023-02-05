﻿using Skyrim.Api.Data.AbstractModels;
using Skyrim.Api.Data.Enums;

namespace Skyrim.Api.Data.Models
{
    public class Settlement : Location
    {
        public Settlement()
        {
            this.TypeOfLocation = LocationType.Settlement;
        }
    }
}
