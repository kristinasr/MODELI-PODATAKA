using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class OutageSchedule : IrregularIntervalSchedule
    {
        private List<long> powerSystemResources = new List<long>();
        private List<long> switchingOperations = new List<long>();

        public OutageSchedule(long globalId) : base(globalId)
        {
        }

        //DVE LISTE COMPARE
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                OutageSchedule oS = (OutageSchedule)obj;
                return (CompareHelper.CompareLists(oS.powerSystemResources, this.powerSystemResources, true) &&
                  CompareHelper.CompareLists(oS.switchingOperations, this.switchingOperations, true));
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
                case ModelCode.OUTAGESCHEDULE_PSRS:
                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {

                case ModelCode.OUTAGESCHEDULE_PSRS:
                    prop.SetValue(powerSystemResources);
                    break;
                case ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS:
                    prop.SetValue(switchingOperations);
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
                default:
                    base.SetProperty(property);
                    break;
            }
        }


        public override bool IsReferenced
        {
            get
            {
                return powerSystemResources.Count > 0 || switchingOperations.Count > 0 || base.IsReferenced;
            }
        }

        public List<long> PowerSystemResources { get => powerSystemResources; set => powerSystemResources = value; }
        public List<long> SwitchingOperations { get => switchingOperations; set => switchingOperations = value; }



        //?? dve liste
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (powerSystemResources != null && powerSystemResources.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.OUTAGESCHEDULE_PSRS] = powerSystemResources.GetRange(0, powerSystemResources.Count);
            }

            if (switchingOperations != null && switchingOperations.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.OUTAGESCHEDULE_SWITCHINGOPERATIONS] = powerSystemResources.GetRange(0, switchingOperations.Count);
            }

            base.GetReferences(references, refType);
        }


        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.PSR_OUTAGESCHEDULE:
                    powerSystemResources.Add(globalId);
                    break;
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                    switchingOperations.Add(globalId);
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
                case ModelCode.PSR_OUTAGESCHEDULE:

                    if (powerSystemResources.Contains(globalId))
                    {
                        powerSystemResources.Remove(globalId);
                    }

                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:

                    if (switchingOperations.Contains(globalId))
                    {
                        switchingOperations.Remove(globalId);
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
