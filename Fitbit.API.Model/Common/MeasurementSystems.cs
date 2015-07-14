using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitbit.API.Model.Common.Enums.UnitSystems;

namespace Fitbit.API.Model.Common.MeasurementSystems
{
    public abstract class AMeasurementSystem
    {
        public Duration duration { get { return Duration.milliseconds; } }
        public abstract Distance distance { get; }
        public abstract Elevation elevation { get; }
        public abstract Height height { get; }
        public abstract Weight weight { get; }
        public abstract Measurements measurements { get; }
        public abstract Liquids liquids { get; }
        public abstract BloodGlucose bloodGlucose { get; }
    }

    public class US : AMeasurementSystem
    {
        public override Distance distance { get { return Distance.miles; } }
        public override Elevation elevation { get { return Elevation.feet; } }
        public override Height height { get { return Height.inches; } }
        public override Weight weight { get { return Weight.pounds; } }
        public override Measurements measurements { get { return Measurements.inches; } }
        public override Liquids liquids { get { return Liquids.fl_oz; } }
        public override BloodGlucose bloodGlucose { get { return BloodGlucose.mg_dL; } }
    }

    public class UK : METRIC
    {
        public override Weight weight { get { return Weight.stone; } }
    }

    public class METRIC : AMeasurementSystem
    {
        public override Distance distance { get { return Distance.kilometers; } }
        public override Elevation elevation { get { return Elevation.meters; } }
        public override Height height { get { return Height.centimeters; } }
        public override Weight weight { get { return Weight.kilograms; } }
        public override Measurements measurements { get { return Measurements.centimeters; } }
        public override Liquids liquids { get { return Liquids.milliliters; } }
        public override BloodGlucose bloodGlucose { get { return BloodGlucose.mmol_l; } }
    }
}