namespace Zagara{
    public class DropPod:SC2Tower{
        public override string Name=>"ZagaraDropPod";
        public override bool AddToShop=>false;
        public override Faction TowerFaction=>Faction.Misc;
		public override bool Upgradable=>false;
		public override bool ShowUpgradeMenu=>false;
        public override bool HasBundle=>false;
        public override int Order=>1;
        public override Dictionary<string,Il2CppSystem.Type>Components=>new(){{"Zagara-DropPodStandPrefab",Il2CppType.Of<DropPodCom>()}};
        public static Dictionary<ObjectId,DropPodCom>DropPodComs=new();
        [RegisterTypeInIl2Cpp]
        public class DropPodCom:MonoBehaviour{
            public DropPodCom(IntPtr ptr):base(ptr){}
            public int unitsSpawned=0;
            public Unit UnitSpawned;
            public Tower tower;
            public void Start(){
                UnitSpawned=(Unit)zagaraCom.random.Next(0,4);
            }
            public Dictionary<Unit,int>Units=new(){{Unit.Zergling,1},{Unit.Hydralisk,4},{Unit.Roach,4},{Unit.Ultralisk,8}};
            public enum Unit{
                Zergling,Hydralisk,Roach,Ultralisk
            }
            public void OnDestroy(){
                DropPodComs.Remove(tower.Id);
            }
        }
        public override TowerModel[]GenerateTowerModels(){
			return new TowerModel[]{
				Base()
			};
		}
        public TowerModel Base(){
			TowerModel dropPod=gameModel.GetTowerFromId("DartMonkey").Clone<TowerModel>();
            dropPod.mods=new(0);
			dropPod.name=Name;
			dropPod.baseId=Name;
			dropPod.radius=10;
            dropPod.range=30;
            dropPod.dontDisplayUpgrades=true;
            dropPod.display=new("Zagara-DropPodStandPrefab");
			dropPod.upgrades=new(0);
            dropPod.ignoreTowerForSelection=true;
            List<Model>dropPodBehav=dropPod.behaviors.ToList();
            dropPodBehav.RemoveModel<AttackModel>();
            dropPodBehav.Add(new TowerExpireModel("DropPod",8,999999,false,false));
            dropPodBehav.RemoveModel<CreateSoundOnUpgradeModel>();
            dropPodBehav.RemoveModel<CreateEffectOnUpgradeModel>();
            dropPodBehav.RemoveModel<CreateSoundOnTowerPlaceModel>();
            dropPodBehav.GetModel<DisplayModel>().display=dropPod.display;
            AttackModel dropPodHydra=CreateTowerAttackModel.Clone<AttackModel>();
            dropPodHydra.name="Hydralisk";
            dropPodHydra.targetProvider=new RandomPositionModel("DropPodRandom",5,35,7,false,7,true,false,"Land",true,false,0,"Hero");
            dropPodHydra.behaviors[0]=dropPodHydra.targetProvider;
            WeaponModel dropPodHydraWeapon=dropPodHydra.weapons[0];
            dropPodHydraWeapon.name="Hydralisk";
            dropPodHydraWeapon.Rate=0.01f;
            ProjectileModel dropPodHydraProj=dropPodHydraWeapon.projectile;
            dropPodHydraProj.name="Hydralisk";
            Il2CppReferenceArray<Model>dropPodHydraProjBehav=dropPodHydraProj.behaviors;
            dropPodHydraProjBehav.GetModel<CreateTowerModel>().tower=TowerTypes["ZagaraHydralisk"].TowerModels[0];
            dropPodHydraProjBehav.GetModel<ArriveAtTargetModel>().expireOnArrival=true;
            AttackModel dropPodRoach=dropPodHydra.Clone<AttackModel>();
            dropPodRoach.name="Roach";
            WeaponModel dropPodRoachWeapon=dropPodRoach.weapons[0];
            dropPodRoachWeapon.name="Roach";
            ProjectileModel dropPodRoachProj=dropPodRoachWeapon.projectile;
            dropPodRoachProj.name="Roach";
            Il2CppReferenceArray<Model>dropPodRoachProjBehav=dropPodRoachProj.behaviors;
            dropPodRoachProjBehav.GetModel<CreateTowerModel>().tower=TowerTypes["ZagaraRoach"].TowerModels[0];
            dropPodRoachProjBehav.GetModel<ArriveAtTargetModel>().expireOnArrival=true;
            Il2CppReferenceArray<Model>necroBehav=gameModel.GetTowerFromId("WizardMonkey-004").behaviors;
            NecromancerZoneModel necroZone=necroBehav.GetModelClone<NecromancerZoneModel>();
            necroZone.attackUsedForRangeModel.range=999;
            AttackModel dropPodZergling=necroBehav.GetModelClone<AttackModel>("Necromancer");
            dropPodZergling.name="Zergling";
            dropPodZergling.range=dropPod.range;
            WeaponModel dropPodZerglingWeapon=dropPodZergling.weapons[0];
            dropPodZerglingWeapon.rate=0.001f;
            dropPodZerglingWeapon.name="Zergling";
            NecromancerEmissionModel dropPodZerglingEmission=dropPodZerglingWeapon.emission.Cast<NecromancerEmissionModel>();
            dropPodZerglingEmission.maxPiercePerBloon=5;
            dropPodZerglingEmission.maxRbeSpawnedPerSecond=1;
            ProjectileModel dropPodZerglingProj=dropPodZerglingWeapon.projectile;
            dropPodZerglingProj.name="Zergling";
            dropPodZerglingProj.display=new("Zagara-ZerglingPrefab");
            dropPodZerglingProj.radius=7;
            dropPodZerglingProj.pierce=5;
            dropPodZerglingProj.CapPierce(dropPodZerglingProj.pierce);
            Il2CppReferenceArray<Model>dropPodZerglingProjBehav=dropPodZerglingProj.behaviors;
            TravelAlongPathModel dropPodZerglingProjTravel=dropPodZerglingProjBehav.GetModel<TravelAlongPathModel>();
            dropPodZerglingProjTravel.lifespan=99999;
            dropPodZerglingProjTravel.speed=50;
            dropPodZerglingProjTravel.disableRotateWithPathDirection=false;
            DamageModel dropPodZerglingProjDamage=dropPodZerglingProj.behaviors.GetModel<DamageModel>();
            dropPodZerglingProjDamage.damage=5;
            dropPodZerglingProjDamage.CapDamage(dropPodZerglingProjDamage.damage);
            AttackModel dropPodUltralisk=necroBehav.GetModelClone<AttackModel>("Necromancer");
            dropPodUltralisk.name="Ultralisk";
            dropPodUltralisk.range=dropPod.range;
            WeaponModel dropPodUltraliskWeapon=dropPodUltralisk.weapons[0];
            dropPodUltraliskWeapon.rate=0.001f;
            dropPodUltraliskWeapon.name="Ultralisk";
            NecromancerEmissionModel dropPodUltraliskEmission=dropPodUltraliskWeapon.emission.Cast<NecromancerEmissionModel>();
            dropPodUltraliskEmission.maxPiercePerBloon=20;
            dropPodUltraliskEmission.maxRbeSpawnedPerSecond=1;
            ProjectileModel dropPodUltraliskProj=dropPodUltraliskWeapon.projectile;
            dropPodUltraliskProj.name="Ultralisk";
            dropPodUltraliskProj.display=new("Zagara-UltraliskPrefab");
            dropPodUltraliskProj.radius=15;
            dropPodUltraliskProj.pierce=20;
            dropPodUltraliskProj.CapPierce(dropPodUltraliskProj.pierce);
            Il2CppReferenceArray<Model>dropPodUltraliskProjBehav=dropPodUltraliskProj.behaviors;
            TravelAlongPathModel dropPodUltraliskProjTravel=dropPodUltraliskProjBehav.GetModel<TravelAlongPathModel>();
            dropPodUltraliskProjTravel.lifespan=99999;
            dropPodUltraliskProjTravel.speed=25;
            dropPodUltraliskProjTravel.disableRotateWithPathDirection=false;
            DamageModel dropPodUltraliskProjDamage=dropPodUltraliskProj.behaviors.GetModel<DamageModel>();
            dropPodUltraliskProjDamage.damage=15;
            dropPodUltraliskProjDamage.CapDamage(dropPodUltraliskProjDamage.damage);
            dropPodBehav.Add(necroZone);
            dropPodBehav.Add(dropPodZergling);
            dropPodBehav.Add(dropPodRoach);
            dropPodBehav.Add(dropPodHydra);
            dropPodBehav.Add(dropPodUltralisk);
            dropPod.behaviors=dropPodBehav.ToArray();
			return dropPod;
        }
        public override bool PreAttack(Weapon weapon){
            Tower tower=weapon.attack.tower;
            ObjectId id=tower.Id;
            UnityDisplayNode node=tower.Node.graphic;
            if(node!=null){
                DropPodCom com;
                if(!DropPodComs.ContainsKey(id)){
                    com=node.GetComponent<DropPodCom>();
                    com.tower=tower;
                    DropPodComs.Add(id,com);
                }
                com=DropPodComs[id];
                if(weapon.weaponModel.name=="WeaponModel_"+com.UnitSpawned.ToString()&&com.unitsSpawned<8){
                    com.unitsSpawned+=com.Units[com.UnitSpawned];
                    return true;
                }
            }
            return false;
        }
        [HarmonyPatch(typeof(NecroData),nameof(NecroData.RbePool))]
        public class NecroDataRbePool_Patch{
            public static bool Prefix(ref NecroData __instance,ref int __result){
                if(__instance.tower.towerModel.baseId=="ZagaraDropPod"){
                    __result=500;
                    return false;
                }
                return true;
            }
        }
    }
}