using System;
using BepInEx;
using EntityStates.BeetleQueenMonster;
using EntityStates.GolemMonster;
using IL.EntityStates.GolemMonster;
using IL.RoR2;
using R2API;
using RoR2;
using UnityEngine;
using RoR2.Artifacts;
using JetBrains.Annotations;
using EntityStates.Engi.EngiWeapon;
using UnityEngine.Networking;

namespace JSMods_1
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.JSMods.Mod1","JS Test Mod Numero Uno","1.0.0")]

    public class Mod1 : BaseUnityPlugin
    {


        public void Awake()
        {




            RoR2.Chat.AddMessage("Loaded Mod1: Currently Doing Nothing");
      


            On.EntityStates.GolemMonster.ChargeLaser.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.ChargeLaser.baseDuration = 0.5f;
               
                orig(self);
            };

            On.RoR2.Run.BeginStage += (orig, self) =>
            {
                for (int i = 0; i < RoR2.Run.instance.stageClearCount; i++)
                {
                    RoR2.TeleporterInteraction.instance.AddShrineStack();
                }
                orig(self);
            };

            On.RoR2.CharacterMaster.Respawn += (orig, self, footPosition, rotation, tryToGroundSafely) =>
            {


                

               return orig(self, footPosition, rotation, tryToGroundSafely);
               
            };

            On.EntityStates.Huntress.ArrowRain.OnEnter += (orig, self) =>
            {
                //code that should be run
                EntityStates.Huntress.HuntressWeapon.ThrowGlaive.glaiveTravelSpeed += 5;
                EntityStates.Huntress.HuntressWeapon.ThrowGlaive.maxBounceCount += 1;
                EntityStates.Huntress.HuntressWeapon.ThrowGlaive.damageCoefficient *= 1.1f;
                orig(self);
            };

            RoR2.GlobalEventManager.onCharacterDeathGlobal += GlobalEventManager_onCharacterDeathGlobal;
        }


        private void spawnMonster(string monsterName, int eliteIndex, RoR2.CharacterBody charBody)
        {

            var transform = charBody.transform;

            GameObject gameObject = RoR2.MasterCatalog.FindMasterPrefab(monsterName);
            GameObject bodyPrefab = gameObject.GetComponent<RoR2.CharacterMaster>().bodyPrefab;
            GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, transform.position, Quaternion.identity);
            RoR2.CharacterMaster component = gameObject2.GetComponent<RoR2.CharacterMaster>();
            NetworkServer.Spawn(gameObject2);
            component.SpawnBody(bodyPrefab, transform.position, Quaternion.identity);

            RoR2.EliteIndex enumFromPartial = Utilities.GetEnumFromPartial<RoR2.EliteIndex>(eliteIndex.ToString());
            component.inventory.SetEquipmentIndex(RoR2.EliteCatalog.GetEliteDef(enumFromPartial).eliteEquipmentIndex);
            component.inventory.GiveItem(ItemIndex.BoostHp, Mathf.RoundToInt((Utilities.GetTierDef(enumFromPartial).healthBoostCoefficient - 1f) * 10f));
            component.inventory.GiveItem(ItemIndex.BoostDamage, Mathf.RoundToInt((Utilities.GetTierDef(enumFromPartial).damageBoostCoefficient - 1f) * 10f));
            component.teamIndex = RoR2.TeamIndex.Monster;



        }

        //when enemy dies, spawn copy of it

        private void GlobalEventManager_onCharacterDeathGlobal(RoR2.DamageReport report)
        {
            System.Random rnd = new System.Random();
            int Odds = rnd.Next(1, 5);
            if(Odds == 3)
            {
                spawnMonster(Utilities.Enemies.LemurianMaster.ToString(), 1, report.victimBody);
                RoR2.Chat.AddMessage(report.victim.ToString());
                RoR2.Chat.AddMessage(report.victimBody.name);
                RoR2.Chat.AddMessage(report.victimMaster.name);
            }
        }

    }
}


