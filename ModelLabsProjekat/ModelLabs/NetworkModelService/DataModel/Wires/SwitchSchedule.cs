using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.LoadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SwitchSchedule:SeasonDayTypeSchedule
    {
        private long switchh = 0;
        public SwitchSchedule(long globalId) : base(globalId)
        {

        }

        public long Switchh
        {
            get
            {
                return switchh;
            }
            set
            {
                switchh = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SwitchSchedule x = (SwitchSchedule)obj;
                return (x.switchh == this.switchh);
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

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.SWSCH_SWITCH:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWSCH_SWITCH:
                    property.SetValue(switchh);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWSCH_SWITCH:
                    switchh = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (switchh != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.SWSCH_SWITCH] = new List<long>();
                references[ModelCode.SWSCH_SWITCH].Add(switchh);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
