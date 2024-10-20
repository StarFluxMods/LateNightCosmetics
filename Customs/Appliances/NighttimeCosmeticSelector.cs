using System.Collections.Generic;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using UnityEngine;

namespace LateNightCosmetics.Customs.Appliances
{
    public class NighttimeCosmeticSelector : CustomAppliance
    {
        public override string UniqueNameID => "NighttimeCosmeticSelector";
        public override GameObject Prefab => ((Appliance)GDOUtils.GetExistingGDO(ApplianceReferences.BedroomOutfitSelector)).Prefab;

        public override List<IApplianceProperty> Properties => new List<IApplianceProperty>
        {
            new CImmovable(),
            new CFixedRotation(),
            new CDestroyApplianceAtDay
            {
                HideBin = true
            },
            new CTriggerPlayerSpecificUI(),
            new CCosmeticSelector()
        };
    }
}