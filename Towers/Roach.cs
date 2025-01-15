using MelonLoader.Utils;

namespace Zagara{
    public class Roach:SC2Tower{
        public override string Name=>"ZagaraRoach";
        public override bool AddToShop=>false;
        public override Faction TowerFaction=>Faction.Zerg;
		public override bool Upgradable=>false;
		public override bool ShowUpgradeMenu=>false;
        public override string BundleName=>"roach.bundle";
        public override Dictionary<string,Il2CppSystem.Type>Components=>new(){{Name+"-Prefab",Il2CppType.Of<RoachCom>()}};
        [RegisterTypeInIl2Cpp]
        public class RoachCom:MonoBehaviour{
            public RoachCom(IntPtr ptr):base(ptr){}
            Animator animator;
            System.Random random;
            int normalStand=0;
            public void Start(){
                random=Zagara.zagaraCom.random;
                animator=GetComponent<Animator>();
                animator.SetFloat("AnimOffset",random.NextSingle());
                AnimationEvent animEvent=new();
                animEvent.functionName="PlayNextStand";
                animEvent.time=1.99f;
                animator.runtimeAnimatorController.animationClips.First(a=>a.name.EndsWith("Stand")).AddEvent(animEvent);
                PlayAnimation(animator,"Stand1",0);
            }
            public void PlayNextStand(){
                normalStand++;
                if(normalStand>15){
                    normalStand=0;
                    PlayAnimation(animator,"Stand"+random.Next(1,3));
                }
            }
        }
		public override TowerModel[]GenerateTowerModels(){
			return new TowerModel[]{
				Base()
			};
		}
        public TowerModel Base(){
			TowerModel roach=gameModel.GetTowerFromId("DartMonkey").Clone<TowerModel>();
            roach.mods=new(0);
			roach.name=Name;
			roach.baseId=Name;
			roach.radius=7;
            roach.range=25;
            roach.dontDisplayUpgrades=true;
            roach.display=new(Name+"-Prefab");
			roach.upgrades=new(0);
            roach.portrait=new("Ui["+Name+"-Portrait]");
			List<Model>roachBehav=roach.behaviors.ToList();
            roachBehav.RemoveModel<CreateSoundOnTowerPlaceModel>();
            roachBehav.RemoveModel<CreateSoundOnUpgradeModel>();
            roachBehav.Add(SelectedSoundModel.Clone());
			roachBehav.Add(new TowerExpireModel("TowerExpireModel",20,9999,false,false));
            roachBehav.GetModel<DisplayModel>().display=roach.display;
            AttackModel roachAttack=roachBehav.GetModel<AttackModel>();
            roachAttack.range=roach.range;
            roachAttack.name="Acid";
            WeaponModel roachWeapon=roachAttack.weapons[0];
            roachWeapon.rate=1;
            roachWeapon.name="Acid";
            ProjectileModel roachProj=roachWeapon.projectile;
            roachProj.name="Acid";
            roachProj.pierce=1;
            roachProj.CapPierce(roachProj.pierce);
            roachProj.display=new(Name+"-AcidPrefab");
            Il2CppReferenceArray<Model>roachProjBehav=roachProj.behaviors;
            DamageModel roachProjDamage=roachProjBehav.GetModel<DamageModel>();
            roachProjDamage.damage=7;
            roachProjDamage.CapDamage(roachProjDamage.damage+2);
            roachProjDamage.immuneBloonProperties=BloonProperties.Purple;
            TravelStraitModel roachProjTravel=roachProjBehav.GetModel<TravelStraitModel>();
            roachProjTravel.Lifespan=0.5f;
            roachProjTravel.Speed=250;
            roachProjBehav.GetModel<DisplayModel>().display=roachProj.display;
			roach.behaviors=roachBehav.ToArray();
            SetSounds(roach,Name+"-Clip",false,true,false,true);
            LocManager.textTable.Add(Name,"Roach");
			return roach;
        }
        public override void Attack(Weapon weapon){
            PlayAnimation(weapon.attack.tower.Node.graphic.GetComponent<Animator>(),"Attack");
        }
    }
}