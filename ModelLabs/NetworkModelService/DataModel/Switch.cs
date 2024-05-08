using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel
{
    public class Switch : ConductingEquipment
    {

        private long switchingOperation = 0;

        public long SwitchingOperation { get => switchingOperation; set => switchingOperation = value; }

        public Switch(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Switch s = (Switch)obj;
                return ((this.switchingOperation == s.switchingOperation));
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

                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {

                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    property.SetValue(switchingOperation);
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

                case ModelCode.SWITCH_SWITCHINGOPERATION:
                    switchingOperation = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }


        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (switchingOperation != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.SWITCH_SWITCHINGOPERATION] = new List<long>() { switchingOperation };
            }

            base.GetReferences(references, refType);
        }



    }
}
