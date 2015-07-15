using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitbit.API.Model.Common.Enums.Profile
{
    public enum Gender
    { 
        MALE,
        FEMALE,
        NA
    }
}

namespace Fitbit.API.Model.Common.Enums.UnitSystems
{
    public enum Locale
    {
        en_US, 
        fr_FR, 
        de_DE, 
        es_ES, 
        en_GB, 
        en_AU, 
        en_NZ, 
        ja_JP
    }

    public enum UnitSystem
    {
        US,
        UK,
        METRIC
    }

    public enum Duration
    {
        milliseconds
    }

    public enum Elevation
    {
        feet,
        meters
    }

    public enum Distance
    {
        miles,
        kilometers
    }

    public enum Height
    {
        inches,
        centimeters
    }

    public enum Weight
    {
        pounds,
        stone,
        kilograms
    }

    public enum Measurements
    {
        inches,
        centimeters
    }

    public enum Liquids
    {
        fl_oz,
        milliliters
    }

    public enum BloodGlucose
    {
        mg_dL,
        mmol_l
    }
}

namespace Fitbit.API.Model.Common.Enums.Activities
{
    public enum AccessLevel
    { 
        PUBLIC,
        PRIVATE
    }

    public enum DistanceUnit
    {
        Steps,
        Miles,
        Kilometers
    }
}
