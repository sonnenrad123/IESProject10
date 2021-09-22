using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitSymbol value1Unit;

        public BasicIntervalSchedule(long globalId) : base(globalId)
        {

        }

        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }

        public UnitSymbol Value1Unit
        {
            get
            {
                return value1Unit;
            }
            set
            {
                value1Unit = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                BasicIntervalSchedule x = (BasicIntervalSchedule)obj;
                return ((x.startTime == this.startTime) && (x.value1Unit == this.value1Unit));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.BIS_STIME:
                case ModelCode.BIS_VALUNIT:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.BIS_STIME:
                    prop.SetValue(startTime);
                    break;

                case ModelCode.BIS_VALUNIT:
                    prop.SetValue((short)value1Unit);
                    break;
                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.BIS_STIME:
                    startTime = property.AsDateTime();
                    break;

                case ModelCode.BIS_VALUNIT:
                    value1Unit = (UnitSymbol)property.AsEnum();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess implementation
    }
}
