using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class SwitchingOperation : IdentifiedObject
    {
        private SwitchState newState;
        private DateTime operationTime;
        private long outageSchedule = 0;
        private List<long> switches = new List<long>();

        public SwitchState NewState { get => newState; set => newState = value; }
        public DateTime OperationTime { get => operationTime; set => operationTime = value; }
        public long OutageSchedule { get => outageSchedule; set => outageSchedule = value; }
        public List<long> Switches { get => switches; set => switches = value; }

        public SwitchingOperation(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object x)
        {
            if (base.Equals(x))
            {
                SwitchingOperation temp = (SwitchingOperation)x;

                return ((temp.newState == this.newState) && (temp.operationTime == this.operationTime) && (temp.outageSchedule == this.outageSchedule) && CompareHelper.CompareLists(temp.switches, this.switches));


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

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {

                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                case ModelCode.SWITCHINGOPERATION_SWITCHES:
                case ModelCode.SWITCHINGOPERATION_OPERATIONTIME:
                case ModelCode.SWITCHINGOPERATION_NEWSTATE:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {


                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                    property.SetValue(outageSchedule);
                    break;

                case ModelCode.SWITCHINGOPERATION_SWITCHES:
                    property.SetValue(switches);
                    break;

                case ModelCode.SWITCHINGOPERATION_OPERATIONTIME:
                    property.SetValue(operationTime);
                    break;
                case ModelCode.SWITCHINGOPERATION_NEWSTATE:
                    property.SetValue((short)newState);
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
                case ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE:
                    outageSchedule = property.AsReference();
                    break;

                case ModelCode.SWITCHINGOPERATION_OPERATIONTIME:
                    operationTime = property.AsDateTime();
                    break;
                case ModelCode.SWITCHINGOPERATION_NEWSTATE:
                    newState = (SwitchState)property.AsEnum();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }


        public override bool IsReferenced
        {
            get
            {
                return switches.Count > 0 || base.IsReferenced;
            }
        }




        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    switches.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        //long i lista

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (outageSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCHINGOPERATION_OUTAGESCHEDULE] = new List<long>() { outageSchedule };
            }

            if (switches != null && switches.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.SWITCHINGOPERATION_SWITCHES] = switches.GetRange(0, switches.Count);
            }

            base.GetReferences(references, refType);
        }


        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.SWITCH_SWITCHINGOPERATION:

                    if (switches.Contains(globalId))
                    {
                        switches.Remove(globalId);
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
