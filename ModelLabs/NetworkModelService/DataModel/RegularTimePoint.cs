using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class RegularTimePoint : IdentifiedObject
    {
        private int sequenceNumber;
        private float value1;
        private float value2;
        private long intervalScheduleIIS = 0;

        public int SequenceNumber { get => sequenceNumber; set => sequenceNumber = value; }
        public float Value1 { get => value1; set => value1 = value; }
        public float Value2 { get => value2; set => value2 = value; }
        public long IntervalScheduleIIS { get => intervalScheduleIIS; set => intervalScheduleIIS = value; }

        public RegularTimePoint(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegularTimePoint irrTP = (RegularTimePoint)obj;
                return ((irrTP.sequenceNumber == this.sequenceNumber) &&
                        (irrTP.value1 == this.value1) &&
                        (irrTP.value2 == this.value2) &&
                        (irrTP.intervalScheduleIIS == this.intervalScheduleIIS));

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
                case ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER:
                case ModelCode.REGULARTIMEPOINT_VALUE1:
                case ModelCode.REGULARTIMEPOINT_VALUE2:
                case ModelCode.REGULARTIMEPOINT_REGULARINTERVALSCHEDULE:

                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {


                case ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER:
                    property.SetValue(sequenceNumber);
                    break;

                case ModelCode.REGULARTIMEPOINT_VALUE1:
                    property.SetValue(value1);
                    break;

                case ModelCode.REGULARTIMEPOINT_VALUE2:
                    property.SetValue(value2);
                    break;

                case ModelCode.REGULARTIMEPOINT_REGULARINTERVALSCHEDULE:
                    property.SetValue(intervalScheduleIIS);
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


                case ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER:
                    sequenceNumber = property.AsInt();
                    break;

                case ModelCode.REGULARTIMEPOINT_VALUE1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.REGULARTIMEPOINT_VALUE2:
                    value2 = property.AsFloat();
                    break;

                case ModelCode.REGULARTIMEPOINT_REGULARINTERVALSCHEDULE:
                    intervalScheduleIIS = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }



        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (intervalScheduleIIS != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.REGULARTIMEPOINT_REGULARINTERVALSCHEDULE] = new List<long>() { intervalScheduleIIS };
            }

            base.GetReferences(references, refType);
        }



    }
}
