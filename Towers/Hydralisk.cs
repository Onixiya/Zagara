namespace Zagara{
    public class Hydralisk:SC2Tower{
        public override string Name=>"ZagaraHydralisk";
        public override bool AddToShop=>false;
        public override Faction TowerFaction=>Faction.Zerg;
		public override bool Upgradable=>false;
		public override bool ShowUpgradeMenu=>false;
        public override string BundleName=>"hydralisk.bundle";
        public override Dictionary<string,Il2CppSystem.Type>Components=>new(){{Name+"-Prefab",Il2CppType.Of<HydraCom>()},{Name+"-UpgradedPrefab",Il2CppType.Of<HydraCom>()}};
        [RegisterTypeInIl2Cpp]
        public class HydraCom:MonoBehaviour{
            public HydraCom(IntPtr ptr):base(ptr){}
            Animator animator;
            System.Random random;
            int normalStand;
            public void Start(){
                random=Zagara.zagaraCom.random;
                animator=GetComponent<Animator>();
                animator.SetFloat("AnimOffset",random.NextSingle());
                AnimationEvent animEvent=new();
                animEvent.functionName="PlayNextStand";
                animEvent.time=2.67f;
                animator.runtimeAnimatorController.animationClips.First(a=>a.name.EndsWith("Stand1")).AddEvent(animEvent);
                PlayAnimation(animator,"Stand1",0);
            }
            public void PlayNextStand(){
                normalStand+=1;
                if(normalStand>6){
                    normalStand=0;
                    PlayAnimation(animator,"Stand"+random.Next(2,6));
                }
            }
            public void PlayAttack(){
                PlayAnimation(animator,"Attack"+random.Next(1,3));
            }
        }
        public override TowerModel[]GenerateTowerModels(){
			return new TowerModel[]{
				Base(),
                HunterKiller()
			};
		}
        public TowerModel Base(){
			TowerModel hydralisk=gameModel.GetTowerFromId("DartMonkey").Clone<TowerModel>();
            hydralisk.mods=new(0);
			hydralisk.name=Name;
			hydralisk.baseId=Name;
			hydralisk.radius=7;
            hydralisk.range=35;
            hydralisk.dontDisplayUpgrades=true;
            hydralisk.display=new(Name+"-Prefab");
			hydralisk.upgrades=new(0);
            hydralisk.portrait=new("Ui["+Name+"-Portrait]");
			List<Model>hydraliskBehav=hydralisk.behaviors.ToList();
            hydraliskBehav.RemoveModel<CreateSoundOnTowerPlaceModel>();
            hydraliskBehav.RemoveModel<CreateSoundOnUpgradeModel>();
            hydraliskBehav.RemoveModel<CreateEffectOnUpgradeModel>();
            hydraliskBehav.Add(SelectedSoundModel.Clone());
			hydraliskBehav.Add(new TowerExpireModel("TowerExpireModel",20,9999,false,false));
            hydraliskBehav.GetModel<DisplayModel>().display=hydralisk.display;
            AttackModel hydraliskAttack=hydraliskBehav.GetModel<AttackModel>();
            hydraliskAttack.range=hydralisk.range;
            hydraliskAttack.name="Spines";
            WeaponModel hydraliskWeapon=hydraliskAttack.weapons[0];
            hydraliskWeapon.rate=0.8f;
            hydraliskWeapon.name="Spines";
            ProjectileModel hydraliskProj=hydraliskWeapon.projectile;
            hydraliskProj.name="Spines";
            hydraliskProj.pierce=3;
            hydraliskProj.CapPierce(hydraliskProj.pierce+2);
            hydraliskProj.display=new("Zagara-SpinesPrefab");
            Il2CppReferenceArray<Model>hydraliskProjBehav=hydraliskProj.behaviors;
            DamageModel hydraliskProjDamage=hydraliskProjBehav.GetModel<DamageModel>();
            hydraliskProjDamage.damage=3;
            hydraliskProjDamage.CapDamage(hydraliskProjDamage.damage+2);
            TravelStraitModel hydraliskProjTravel=hydraliskProjBehav.GetModel<TravelStraitModel>();
            hydraliskProjTravel.Lifespan=0.3f;
            hydraliskProjTravel.Speed=350;
            hydraliskProjBehav.GetModel<DisplayModel>().display=hydraliskProj.display;
            hydralisk.behaviors=hydraliskBehav.ToArray();
            SetSounds(hydralisk,Name+"-Clip",false,true,false,true);
			return hydralisk;
        }
        public TowerModel HunterKiller(){
            TowerModel hydralisk=Base().Clone<TowerModel>();
            hydralisk.name=Name+"-Upgraded";
            hydralisk.range+=5;
            hydralisk.display=new(Name+"-UpgradedPrefab");
            hydralisk.portrait=new("Ui["+Name+"-UpgradedPortrait]");
            Il2CppReferenceArray<Model>hydraliskBehav=hydralisk.behaviors;
            AttackModel hydraliskAttack=hydralisk.behaviors.GetModel<AttackModel>();
            hydraliskAttack.range=hydralisk.range;
            WeaponModel hydraliskWeapon=hydraliskAttack.weapons[0];
            hydraliskWeapon.rate-=0.2f;
            hydraliskWeapon.customStartCooldown=1;
            ProjectileModel hydraliskProj=hydraliskWeapon.projectile;
            hydraliskProj.pierce+=2;
            Il2CppReferenceArray<Model>hydraliskProjBehav=hydraliskProj.behaviors;
            DamageModel hydraliskProjDamage=hydraliskProjBehav.GetModel<DamageModel>();
            hydraliskProjDamage.damage+=2;
            hydraliskProjDamage.CapDamage(hydraliskProjDamage.damage+3);
            TravelStraitModel hydraliskProjTravel=hydraliskProjBehav.GetModel<TravelStraitModel>();
            hydraliskProjTravel.Lifespan+=0.1f;
            hydraliskProjTravel.Speed+=50;
            LocManager.textTable.Add(Name,"Hydralisk");
            LocManager.textTable.Add(hydralisk.name,"Hunter Killer");
            return hydralisk;
        }
        public override void Attack(Weapon weapon){
            weapon.attack.tower.Node.graphic.GetComponent<HydraCom>().PlayAttack();
        }
    }
}