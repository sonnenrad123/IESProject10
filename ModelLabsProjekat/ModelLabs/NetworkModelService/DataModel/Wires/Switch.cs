using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch:ConductingEquipment
    {
        private bool normalOpen;
        private float ratedCurrent;
        private bool retained;
        private int switchOnCount;
        private DateTime switchOnDate;
        private List<long> switchSchedules = new List<long>();

        public Switch(long globalId)
            :base(globalId)
        {

        }

        public bool NormalOpen
        {
            get
            {
                return normalOpen;
            }

            set
            {
                normalOpen = value;
            }
        }

        public float RatedCurrent
        {
            get
            {
                return ratedCurrent;
            }

            set
            {
                ratedCurrent = value;
            }
        }

        public bool Retained
        {
            get
            {
                return retained;
            }

            set
            {
                retained = value;
            }
        }

        public int SwitchOnCount
        {
            get
            {
                return switchOnCount;
            }
            set
            {
                switchOnCount = value;
            }
        }

        public DateTime SwitchOnDate
        {
            get
            {
                return switchOnDate;
            }
            set
            {
                switchOnDate = value;
            }
        }

        public List<long> SwitchSchedules
        {
            get
            {
                return switchSchedules;
            }
            set
            {
                switchSchedules = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Switch x = (Switch)obj;
                return ((x.normalOpen == this.normalOpen) &&
                        (x.ratedCurrent == this.ratedCurrent) &&
                        (x.retained == this.retained) &&
                        (x.switchOnCount == this.switchOnCount) && 
                        (x.switchOnDate == this.switchOnDate) && CompareHelper.CompareLists(x.switchSchedules,this.switchSchedules));
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
                case ModelCode.SW_NORMOPEN:
                case ModelCode.SW_RATEDCURR:
                case ModelCode.SW_RETAINED:
                case ModelCode.SW_SWONCOUNT:
                case ModelCode.SW_SWONDATE:
                case ModelCode.SW_SWSCHS:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SW_NORMOPEN:
                    property.SetValue(normalOpen);
                    break;
                case ModelCode.SW_RATEDCURR:
                    property.SetValue(ratedCurrent);
                    break;
                case ModelCode.SW_RETAINED:
                    property.SetValue(retained);
                    break;
                case ModelCode.SW_SWONCOUNT:
                    property.SetValue(switchOnCount);
                    break;
                case ModelCode.SW_SWONDATE:
                    property.SetValue(switchOnDate);
                    break;
                case ModelCode.SW_SWSCHS:
                    property.SetValue(switchSchedules);
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
                case ModelCode.SW_NORMOPEN:
                    normalOpen = property.AsBool();
                    break;
                case ModelCode.SW_RATEDCURR:
                    ratedCurrent = property.AsFloat();
                    break;
                case ModelCode.SW_RETAINED:
                    retained = property.AsBool();
                    break;
                case ModelCode.SW_SWONCOUNT:
                    switchOnCount = property.AsInt();
                    break;
                case ModelCode.SW_SWONDATE:
                    switchOnDate = property.AsDateTime();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess implementation

        #region IReference implementation
        public override bool IsReferenced
        {
            get
            {
                return switchSchedules.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (switchSchedules != null && switchSchedules.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SW_SWSCHS] = switchSchedules.GetRange(0, switchSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWSCH_SWITCH:
                    switchSchedules.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWSCH_SWITCH:

                    if (switchSchedules.Contains(globalId))
                    {
                        switchSchedules.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation
    }
}
