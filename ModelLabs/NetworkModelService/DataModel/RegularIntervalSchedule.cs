using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class RegularIntervalSchedule : BasicIntervalSchedule
    {
        private DateTime endTime;
        private float timeStep;
        private List<long> timePoints = new List<long>();

        public DateTime EndTime { get => endTime; set => endTime = value; }

        public float TimeStep { get => timeStep; set => timeStep = value; }

        public RegularIntervalSchedule(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegularIntervalSchedule ris = (RegularIntervalSchedule)obj;
                return (ris.endTime == this.endTime) && (ris.timeStep == this.timeStep) && (CompareHelper.CompareLists(ris.timePoints, this.timePoints));
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
                case ModelCode.REGULARINTERVALSCHEDULE_REGULARTIMEPOINTS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    property.SetValue(endTime);
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
                    property.SetValue(timeStep);
                    break;
                case ModelCode.REGULARINTERVALSCHEDULE_REGULARTIMEPOINTS:
                    property.SetValue(timePoints);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced
        {
            get
            {
                return timePoints.Count > 0 || base.IsReferenced;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    endTime = property.AsDateTime();
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP:
                    timeStep = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARTIMEPOINT_REGULARINTERVALSCHEDULE:
                    timePoints.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }



        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (timePoints != null && timePoints.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULARINTERVALSCHEDULE_REGULARTIMEPOINTS] = timePoints.GetRange(0, timePoints.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARTIMEPOINT_REGULARINTERVALSCHEDULE:

                    if (timePoints.Contains(globalId))
                    {
                        timePoints.Remove(globalId);
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
    }

}
