using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class RegulatingControl:PowerSystemResource
    {
        private bool discrete;
        private RegulatingControlModeKind mode;
        private PhaseCode monitoredPhase;
        private float targetRange;
        private float targetValue;
        private List<long> regulationSchedules = new List<long>();

        public RegulatingControl(long globalId) : base(globalId)
        {

        }

        public bool Discrete
        {
            get
            {
                return discrete;
            }
            set
            {
                discrete = value;
            }
        }

        public RegulatingControlModeKind Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        public PhaseCode MonitoredPhase
        {
            get
            {
                return monitoredPhase;
            }
            set
            {
                monitoredPhase = value;
            }
        }

        public float TargetRange
        {
            get
            {
                return targetRange;
            }
            set
            {
                targetRange = value;
            }
        }

        public float TargetValue
        {
            get
            {
                return targetValue;
            }
            set
            {
                targetValue = value;
            }
        }

        public List<long> RegulationSchedules
        {
            get
            {
                return regulationSchedules;
            }
            set
            {
                regulationSchedules = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegulatingControl x = (RegulatingControl)obj;
                return ((x.discrete == this.discrete) &&
                        (x.mode == this.mode) &&
                        (x.monitoredPhase == this.monitoredPhase) &&
                        (x.targetRange == this.targetRange) &&
                        (x.targetValue == this.targetValue) &&
                        (CompareHelper.CompareLists(x.regulationSchedules, this.regulationSchedules)));
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
                case ModelCode.REGCONTROL_DISCRETE:
                case ModelCode.REGCONTROL_MODE:
                case ModelCode.REGCONTROL_MONITORPHASE:
                case ModelCode.REGCONTROL_TARGETR:
                case ModelCode.REGCONTROL_TARGETV:
                case ModelCode.REGCONTROL_REGSCHS:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.REGCONTROL_DISCRETE:
                    prop.SetValue(discrete);
                    break;
                case ModelCode.REGCONTROL_MODE:
                    prop.SetValue((short)mode);
                    break;
                case ModelCode.REGCONTROL_MONITORPHASE:
                    prop.SetValue((short)monitoredPhase);
                    break;
                case ModelCode.REGCONTROL_TARGETR:
                    prop.SetValue(targetRange);
                    break;
                case ModelCode.REGCONTROL_TARGETV:
                    prop.SetValue(targetValue);
                    break;
                case ModelCode.REGCONTROL_REGSCHS:
                    prop.SetValue(regulationSchedules);
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
                case ModelCode.REGCONTROL_DISCRETE:
                    discrete = property.AsBool();
                    break;
                case ModelCode.REGCONTROL_MODE:
                    mode = (RegulatingControlModeKind)property.AsEnum();
                    break;
                case ModelCode.REGCONTROL_MONITORPHASE:
                    monitoredPhase = (PhaseCode)property.AsEnum();
                    break;
                case ModelCode.REGCONTROL_TARGETR:
                    targetRange = property.AsFloat();
                    break;
                case ModelCode.REGCONTROL_TARGETV:
                    targetValue = property.AsFloat();
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
                return regulationSchedules.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (regulationSchedules != null && regulationSchedules.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGCONTROL_REGSCHS] = regulationSchedules.GetRange(0, regulationSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGSCH_REGCONTROL:
                    regulationSchedules.Add(globalId);
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
                case ModelCode.REGSCH_REGCONTROL:

                    if (regulationSchedules.Contains(globalId))
                    {
                        regulationSchedules.Remove(globalId);
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
