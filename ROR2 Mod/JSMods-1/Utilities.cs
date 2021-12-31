using System;
using System.Collections.Generic;
using System.Text;
using RoR2;
using UnityEngine;
using System.Reflection;
using R2API.Utils;

namespace JSMods_1
{
    class Utilities
    {
        public static TEnum GetEnumFromPartial<TEnum>(string name)
        {
            TEnum[] array = (TEnum[])Enum.GetValues(typeof(TEnum));
            int num;
            if (!int.TryParse(name, out num))
            {
                foreach (TEnum tenum in array)
                {
                    if (Enum.GetName(typeof(TEnum), tenum).ToUpper().Contains(name.ToUpper()))
                    {
                        return tenum;
                    }
                }
                return default(TEnum);
            }
            if (num >= array.Length)
            {
                return default(TEnum);
            }
            return array[num];
        }

        internal static CombatDirector.EliteTierDef GetTierDef(EliteIndex index)
        {
            int num = 0;
            CombatDirector.EliteTierDef[] fieldValue = Reflection.GetFieldValue<CombatDirector.EliteTierDef[]>(typeof(CombatDirector), "eliteTiers");
            if (index > EliteIndex.None && index < EliteIndex.Count)
            {
                for (int i = 0; i < fieldValue.Length; i++)
                {
                    for (int j = 0; j < fieldValue[i].eliteTypes.Length; j++)
                    {
                        if (fieldValue[i].eliteTypes[j] == index)
                        {
                            num = i;
                        }
                    }
                }
            }
            return fieldValue[num];
        }

        public static void spawnMonster(string monsterName, int eliteIndex, RoR2.CharacterBody charBody)
        {

            var transform = charBody.transform;

            GameObject gameObject = RoR2.MasterCatalog.FindMasterPrefab(monsterName);
            GameObject bodyPrefab = gameObject.GetComponent<RoR2.CharacterMaster>().bodyPrefab;
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
            RoR2.CharacterMaster component = gameObject2.GetComponent<RoR2.CharacterMaster>();
            component.SpawnBody(bodyPrefab, transform.position, Quaternion.identity);

            RoR2.EliteIndex enumFromPartial = GetEnumFromPartial<RoR2.EliteIndex>(eliteIndex.ToString());
            component.inventory.SetEquipmentIndex(RoR2.EliteCatalog.GetEliteDef(enumFromPartial).eliteEquipmentIndex);
            component.inventory.GiveItem(ItemIndex.BoostHp, Mathf.RoundToInt((GetTierDef(enumFromPartial).healthBoostCoefficient - 1f) * 10f));
            component.inventory.GiveItem(ItemIndex.BoostDamage, Mathf.RoundToInt((GetTierDef(enumFromPartial).damageBoostCoefficient - 1f) * 10f));
            component.teamIndex = RoR2.TeamIndex.Monster;

        }

        public enum Enemies
        {
            AncientWispMaster,
            ArchWispMaster,
            ArtifactShellMaster,
            BeetleCrystalMaster,
            BeetleGuardAllyMaster,
            BeetleGuardMaster,
            BeetleGuardMasterCrystal,
            BeetleMaster,
            BeetleQueenMaster,
            BellMaster,
            BisonMaster,
            BrotherGlassMaster,
            BrotherHauntMaster,
            BrotherHurtMaster,
            BrotherMaster,
            CaptainMonsterMaster,
            ClayBossMaster,
            ClayBruiserMaster,
            ClaymanMaster,
            CommandoMonsterMaster,
            CrocoMonsterMaster,
            Drone1Master,
            Drone2Master,
            DroneBackupMaster,
            DroneMissileMaster,
            ElectricWormMaster,
            EmergencyDroneMaster,
            EngiBeamTurretMaster,
            EngiMonsterMaster,
            EngiTurretMaster,
            EngiWalkerTurretMaster,
            EquipmentDroneMaster,
            FlameDroneMaster,
            GolemMaster,
            GrandparentMaster,
            GravekeeperMaster,
            GreaterWispMaster,
            HermitCrabMaster,
            HuntressMonsterMaster,
            ImpBossMaster,
            ImpMaster,
            JellyfishMaster,
            LemurianBruiserMaster,
            LemurianBruiserMasterFire,
            LemurianBruiserMasterHaunted,
            LemurianBruiserMasterIce,
            LemurianBruiserMasterPoison,
            LemurianMaster,
            LoaderMonsterMaster,
            LunarGolemMaster,
            LunarWispMaster,
            MageMonsterMaster,
            MagmaWormMaster,
            MegaDroneMaster,
            MercMonsterMaster,
            MiniMushroomMaster,
            NullifierMaster,
            ParentMaster,
            ParentPodMaster,
            RoboBallBossMaster,
            RoboBallMiniMaster,
            ScavLunar1Master,
            ScavLunar2Master,
            ScavLunar3Master,
            ScavLunar4Master,
            ScavMaster,
            ShopkeeperMaster,
            SquidTurretMaster,
            SuperRoboBallBossMaster,
            TitanGoldAllyMaster,
            TitanGoldMaster,
            TitanMaster,
            ToolbotMonsterMaster,
            TreebotMonsterMaster,
            Turret1Master,
            UrchinTurretMaster,
            VagrantMaster,
            VultureMaster,
            WispMaster,
            WispSoulMaster
        }
    }
}
