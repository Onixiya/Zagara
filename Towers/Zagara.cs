using Il2CppAssets.Scripts.Data.Global;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Audio;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using Il2CppNinjaKiwi.GUTS.Models.ContentBrowser;
using MelonLoader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;
namespace Zagara{
    public class Zagara:SC2Tower{
        public override string Name=>"Zagara";
        public override Faction TowerFaction=>Faction.Zerg;
		public override bool ShowUpgradeMenu=>false;
		public override bool AddToShop=>false;
		public override bool Hero=>true;
		public override int Order=>2;
        public override Dictionary<string,Il2CppSystem.Type>Components=>new(){{Name+"-Prefab",Il2CppType.Of<ZagaraCom>()},
            {Name+"-BanelingRollPrefab",Il2CppType.Of<ZagaraBaneUltraCom>()},{Name+"-BanelingWalkPrefab",Il2CppType.Of<ZagaraBaneUltraCom>()},
            {Name+"-ZerglingPrefab",Il2CppType.Of<ZagaraZerglingCom>()},{Name+"-UltraliskPrefab",Il2CppType.Of<ZagaraBaneUltraCom>()}};
        public static ZagaraCom zagaraCom; //this will 100% break if you use that infinite heroes thing but idgaf, performance is better
        [RegisterTypeInIl2Cpp]
        public class ZagaraCom:MonoBehaviour{
            public ZagaraCom(IntPtr ptr):base(ptr){}
            public Animator animator;
            public System.Random random=new();
            public string stateName="";
            public int dropPods=0;
            int normalStand;
            public void Start(){
                zagaraCom=this;
                animator=GetComponent<Animator>();
                animator.SetFloat("AnimOffset",zagaraCom.random.NextSingle());
                AnimationEvent animEvent=new();
                animEvent.functionName="PlayNextStand";
                animEvent.time=1.99f;
                animator.runtimeAnimatorController.animationClips.First(a=>a.name.EndsWith("Stand1")).AddEvent(animEvent);
                PlayAnimation(animator,"Stand1",0);
            }
            public void PlayNextStand(){
                normalStand+=1;
                if(normalStand>6){
                    normalStand=0;
                    PlayAnimation(animator,"Stand"+random.Next(2,4));
                }
            }
            public void Update(){
                stateName=animator.GetCurrentStateName(0);
                if(dropPods>=20){
                    dropPods=0;
                    PlayAnimation(animator,"AbilityEnd");
                }
            }
        }
        [RegisterTypeInIl2Cpp]
        public class ZagaraBaneUltraCom:MonoBehaviour{
            public ZagaraBaneUltraCom(IntPtr ptr):base(ptr){}
            public void Start(){
                Animator anim=GetComponent<Animator>();
                anim.SetFloat("AnimOffset",zagaraCom.random.NextSingle());
                PlayAnimation(anim,"Move",0);
            }
        }
        [RegisterTypeInIl2Cpp]
        public class ZagaraZerglingCom:MonoBehaviour{
            public ZagaraZerglingCom(IntPtr ptr):base(ptr){}
            Animator animator;
            int normalStand;
            public void Start(){
                animator=GetComponent<Animator>();
                animator.SetFloat("AnimOffset",zagaraCom.random.NextSingle());
                AnimationEvent animEvent=new();
                animEvent.functionName="PlayNextWalk";
                animEvent.time=1.99f;
                animator.runtimeAnimatorController.animationClips.First(a=>a.name.EndsWith("Walk1")).AddEvent(animEvent);
                PlayAnimation(animator,"Walk1",0);
            }
            public void PlayNextWalk(){
                normalStand+=1;
                if(normalStand>6){
                    normalStand=0;
                    PlayAnimation(animator,"Walk"+zagaraCom.random.Next(1,3));
                }
            }
        }
        public override TowerModel[]GenerateTowerModels(){
            return new TowerModel[]{
                Base(),
				Level2(),
				Level3(),
				Level4(),
				Level5(),
				Level6(),
				Level7(),
				Level8(),
				Level9(),
				Level10(),
				Level11(),
				Level12(),
				Level13(),
				Level14(),
				Level15(),
				Level16(),
				Level17(),
				Level18(),
				Level19(),
				Level20()
            };
		}
        public static string BuffName="Zagara-RateMassFrenzy";
		public override UpgradeModel[]GenerateUpgradeModels(){
			List<UpgradeModel>upgradeList=new(){new(Name+" Level 1",0,205,new(),0,1,0,"",Name+" Level 1")};
			for(int i=1;i<21;i++){
				if(i>9){
					upgradeList.Add(new(Name+" Level "+i,0,(int)System.Math.Round(upgradeList.Last().xpCost*1.05),new(),0,i-1,0,"",Name+" Level "+i));
				}else{
					upgradeList.Add(new(Name+" Level "+i,0,(int)System.Math.Round(upgradeList.Last().xpCost*1.35),new(),0,i-1,0,"",Name+" Level "+i));
				}
			}
			return upgradeList.ToArray();
		}
		public override HeroDetailsModel GenerateHeroDetails(){
            LocManager.textTable.Add(Name,Name);
			LocManager.textTable.Add(Name+" Description","Current commander and leader of the Zerg, stand in her way and feel the fury of the Swarm");
            LocManager.textTable.Add(Name+" Short Description","Overqueen");
			LocManager.textTable.Add(Name+" Level 1 Description","Hits bloons with razer sharp talons");
			LocManager.textTable.Add(Name+" Level 2 Description","Talons reach slightly further");
			LocManager.textTable.Add(Name+" Level 3 Description","Increases damage dealt and pierce");
			LocManager.textTable.Add(Name+" Level 4 Description","Attacks faster");
			LocManager.textTable.Add(Name+" Level 5 Description","Attack becomes ranged, shooting sharp spines at any nearby target");
			LocManager.textTable.Add(Name+" Level 6 Description","Spines hit harder doing more damage and pierce");
			LocManager.textTable.Add(Name+" Level 7 Description","Banelings: Spawns 2 banelings to head towards her target");
			LocManager.textTable.Add(Name+" Level 8 Description","Spines are grooved, allowing them to travel further and faster");
			LocManager.textTable.Add(Name+" Level 9 Description","Banelings deal more damage and in a larger area");
			LocManager.textTable.Add(Name+" Level 10 Description","Shoots 3 spines in a arc dealing more damage");
			LocManager.textTable.Add(Name+" Level 11 Description","Decreases the cooldown on Banelings");
			LocManager.textTable.Add(Name+" Level 12 Description","Banelings now roll, increasing their speed and spawns 2 more");
			LocManager.textTable.Add(Name+" Level 13 Description","Mass Frenzy: Enrages all Zerg on the map, increasing their attack rate and damage");
			LocManager.textTable.Add(Name+" Level 14 Description","Attack range increased, reaching even further targets");
			LocManager.textTable.Add(Name+" Level 15 Description","Banelings deal more damage to their main target in addition to spawning 2 more");
			LocManager.textTable.Add(Name+" Level 16 Description","Spines do even more damage and travel further");
			LocManager.textTable.Add(Name+" Level 17 Description","Spawn Hunter Killers: Spawns in 3 temporary upgraded Hydralisks");
			LocManager.textTable.Add(Name+" Level 18 Description","Mass Frenzy lasts longer and increases pierce");
			LocManager.textTable.Add(Name+" Level 19 Description","Spawn Hunter Killers spawns three more and decreases all ability cooldowns");
			LocManager.textTable.Add(Name+" Level 20 Description","Summon Drop Pods: Summons Drop Pods all around the map, dealing damage and spawning random Zerg");
            //8 zerglings, 4 hydralisks, 4 roaches, 1 ultralisk
            List<BuffIndicatorModel>buffs=gameModel.buffIndicatorModels.ToList();
            BuffIndicatorModel buff=new BuffIndicatorModel(BuffName,BuffName,BuffName,false,0,false);
            buffs.Add(buff);
            gameModel.buffIndicatorModels=buffs.ToArray();
            BuffIconSprite icon=new(){buffId=BuffName,icon=new("Ui["+Name+"-MassFrenzyIcon]")};
            GameData.Instance.buffIconSprites.buffIconSprites.Add(icon);
            return new(Name+"",0,20,1,0,0,false);
		}
		public override SkinData HeroSkin(){
			Il2CppSystem.Collections.Generic.List<StorePortraits>portraits=new();
			portraits.Add(new(){asset=new("Ui["+Name+"-Portrait]"),levelTxt="1"});
			SkinData skin=ScriptableObject.CreateInstance<SkinData>();
			skin.name=Name;
			skin.skinName=Name+" Short Description";
			skin.description=Name+" Description";
			skin.baseTowerName=Name;
			skin.mmCost=0;
			skin.icon=new("Ui["+Name+"-Button]");
			skin.iconSquare=new("Ui["+Name+"-HeroIcon]");
			skin.isDefaultTowerSkin=true;
			skin.textMaterialId="Ezili";
			skin.StorePortraitsContainer=new(){
				items=portraits
			};
			skin.unlockedVoiceSound=new(Name+"-Birth");
			skin.unlockedEventSound=skin.unlockedVoiceSound;
            skin.SwapSpriteContainer=new(){items=new()};
			SkinData gameSkin=GameData.Instance.skinsData.SkinList.items.First(a=>a.name=="Ezili");
			skin.backgroundBanner=gameSkin.backgroundBanner;
			skin.fontMaterial=gameSkin.fontMaterial;
			skin.backgroundColourTintOverride=gameSkin.backgroundColourTintOverride;
			return skin;
		}
        public TowerModel Base(){
			TowerModel zagara=gameModel.GetTowerFromId("DartMonkey").Clone<TowerModel>();
            zagara.mods=new(0);
			zagara.baseId=Name;
			zagara.name=Name;
			zagara.portrait=new("Ui["+Name+"-Portrait]");
			zagara.icon=new("Ui["+Name+"-Icon]");
			zagara.cost=650;
            zagara.radius=18;
            zagara.range=37;
			zagara.towerSet=TowerSet.Hero;
            zagara.display=new(Name+"-Prefab");
			zagara.tier=1;
			zagara.tiers=new(new[]{1,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 2",Name+" 2")};
			zagara.appliedUpgrades=new(0);
			List<Model>zagaraBehav=zagara.behaviors.ToList();
            zagaraBehav.Add(SelectedSoundModel.Clone());
			DisplayModel zagaraDisplay=zagaraBehav.GetModel<DisplayModel>();
            zagaraDisplay.display=new(zagara.display.guidRef);
			zagaraBehav.Add(new HeroModel("HeroModel",1,1));
            zagaraBehav.RemoveModel<AttackModel>();
            AttackModel zagaraAttack=gameModel.GetTowerFromId("SniperMonkey").behaviors.GetModel<AttackModel>().Clone<AttackModel>();
            zagaraAttack.name="Talons";
            zagaraAttack.range=zagara.range;
            WeaponModel zagaraWeapon=zagaraAttack.weapons[0];
            zagaraWeapon.name="Talons";
            zagaraWeapon.rate=1.25f;
            zagaraWeapon.behaviors=null;
            ProjectileModel zagaraProj=zagaraWeapon.projectile;
            zagaraProj.radius=2;
            zagaraProj.pierce=5;
            zagaraProj.CapPierce(zagaraProj.pierce+3);
            zagaraProj.display=new("");
            List<FilterModel>filters=zagaraProj.filters.ToList();
            filters.Remove(filters.First(a=>a.GetIl2CppType()==Il2CppType.Of<FilterAllExceptTargetModel>()));
            zagaraProj.filters=filters.ToArray();
            Il2CppReferenceArray<Model>zagaraProjBehav=zagaraProj.behaviors;
            zagaraProjBehav.GetModel<DisplayModel>().display=zagaraProj.display;
            zagaraProjBehav.GetModel<ProjectileFilterModel>().filters=zagaraProj.filters;
            DamageModel zagaraProjDamage=zagaraProjBehav.GetModel<DamageModel>();
            zagaraProjDamage.damage=3;
            zagaraProjDamage.CapDamage(zagaraProjDamage.damage+2);
            zagaraBehav.Add(zagaraAttack);
			zagara.behaviors=zagaraBehav.ToArray();
            SetSounds(zagara,Name+"-",true,true,true,false);
			return zagara;
        }
		public TowerModel Level2(){
			TowerModel zagara=Base().Clone<TowerModel>();
			zagara.name=Name+" 2";
			zagara.tier=2;
			zagara.tiers=new(new[]{2,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 3",Name+" 3")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 2");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            zagara.range+=5;
            zagara.behaviors.GetModel<AttackModel>().range=zagara.range;
			return zagara;
		}
		public TowerModel Level3(){
			TowerModel zagara=Level2().Clone<TowerModel>();
			zagara.name=Name+" 3";
			zagara.tier=3;
			zagara.tiers=new(new[]{3,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 4",Name+" 4")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 3");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            ProjectileModel zagaraProj=zagara.behaviors.GetModel<AttackModel>().weapons[0].projectile;
            zagaraProj.pierce+=2;
            zagaraProj.CapPierce(zagaraProj.pierce+3);
            DamageModel zagaraProjDamage=zagaraProj.behaviors.GetModel<DamageModel>();
            zagaraProjDamage.damage+=2;
            zagaraProjDamage.CapDamage(zagaraProjDamage.damage+2);
			return zagara;
		}
		public TowerModel Level4(){
			TowerModel zagara=Level3().Clone<TowerModel>();
			zagara.name=Name+" 4";
			zagara.tier=4;
			zagara.tiers=new(new[]{4,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 5",Name+" 5")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 4");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            zagara.behaviors.GetModel<AttackModel>().weapons[0].rate=1.05f;
			return zagara;
		}
		public TowerModel Level5(){
			TowerModel zagara=Level4().Clone<TowerModel>();
			zagara.name=Name+" 5";
			zagara.tier=5;
			zagara.tiers=new(new[]{5,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 6",Name+" 6")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 5");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            zagara.range=60;
            List<Model>zagaraBehav=zagara.behaviors.ToList();
            zagaraBehav.RemoveModel<AttackModel>();
            AttackModel zagaraAttack=gameModel.GetTowerFromId("DartMonkey").behaviors.GetModel<AttackModel>().Clone<AttackModel>();
            zagaraAttack.range=zagara.range;
            zagaraAttack.name="Spines";
            WeaponModel zagaraWeapon=zagaraAttack.weapons[0];
            zagaraWeapon.Rate=1;
            zagaraWeapon.name="Spines";
            ProjectileModel zagaraProj=zagaraWeapon.projectile;
            zagaraProj.pierce=4;
            zagaraProj.CapPierce(zagaraProj.pierce+3);
            zagaraProj.display=new(Name+"-SpinesPrefab");
            Il2CppReferenceArray<Model>zagaraProjBehav=zagaraProj.behaviors;
            zagaraProjBehav.GetModel<DisplayModel>().display=zagaraProj.display;
            zagaraProjBehav.GetModel<TravelStraitModel>().Speed=400;
            DamageModel zagaraProjDamage=zagaraProjBehav.GetModel<DamageModel>();
            zagaraProjDamage.damage=4;
            zagaraProjDamage.CapDamage(zagaraProjDamage.damage+2);
            zagaraBehav.Add(zagaraAttack);
            zagara.behaviors=zagaraBehav.ToArray();
			return zagara;
		}
		public TowerModel Level6(){
			TowerModel zagara=Level5().Clone<TowerModel>();
			zagara.name=Name+" 6";
			zagara.tier=6;
			zagara.tiers=new(new[]{6,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 7",Name+" 7")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 6");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            ProjectileModel zagaraProj=zagara.behaviors.GetModel<AttackModel>().weapons[0].projectile;
            zagaraProj.pierce+=2;
            zagaraProj.CapPierce(zagaraProj.pierce+3);
            Il2CppReferenceArray<Model>zagaraProjBehav=zagaraProj.behaviors;
            DamageModel zagaraProjDamage=zagaraProjBehav.GetModel<DamageModel>();
            zagaraProjDamage.damage+=2;
            zagaraProjDamage.CapDamage(zagaraProjDamage.damage+2);
            TravelStraitModel zagaraProjTravel=zagaraProjBehav.GetModel<TravelStraitModel>();
            zagaraProjTravel.Lifespan+=0.25f;
            zagaraProjTravel.Speed+=50;
			return zagara;
		}
		public TowerModel Level7(){
			TowerModel zagara=Level6().Clone<TowerModel>();
			zagara.name=Name+" 7";
			zagara.tier=7;
			zagara.tiers=new(new[]{7,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 8",Name+" 8")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 7");
            zagara.appliedUpgrades=appliedUpgrades.ToArray();
            AbilityModel banelings=BlankAbilityModel.Clone<AbilityModel>();
            banelings.name="Banelings";
            banelings.displayName=banelings.name;
            banelings.addedViaUpgrade=appliedUpgrades.Last();
            banelings.Cooldown=30;
            banelings.startOffCooldown=true;
            banelings.icon=new("Ui["+Name+"-BanelingsIcon]");
            List<Model>banelingsBehav=banelings.behaviors.ToList();
            banelingsBehav.Add(new CreateSoundOnAbilityModel("BanelingsSound",null,new("Banelings1",new(Name+"-Banelings1")),new("Banelings2",new(Name+"-Banelings2"))));
            AttackModel banelingsAttack=gameModel.GetTowerFromId("BombShooter").behaviors.GetModel<AttackModel>().Clone<AttackModel>();
            banelingsAttack.range=zagara.range+10;
            banelingsAttack.name="Banelings";
            List<Model>banelingsAttackBehav=banelingsAttack.behaviors.ToList();
            banelingsAttack.behaviors=banelingsAttackBehav.ToArray();
            WeaponModel banelingsWeapon=banelingsAttack.weapons[0];
            banelingsWeapon.name="Banelings";
            banelingsWeapon.Rate=0.1f;
            ProjectileModel banelingsProj=banelingsWeapon.projectile;
            banelingsProj.name="Banelings";
            banelingsProj.display=new(Name+"-BanelingWalkPrefab");
            List<Model>banelingsProjBehav=banelingsProj.behaviors.ToList();
            banelingsProjBehav.RemoveModel<CreateEffectOnContactModel>();
            banelingsProjBehav.GetModel<DisplayModel>().display=banelingsProj.display;
            CreateSoundOnProjectileCollisionModel banelingsProjSoundOnCol=banelingsProjBehav.GetModel<CreateSoundOnProjectileCollisionModel>();
            banelingsProjSoundOnCol.sound1=new("BanelingExplosion1",new(Name+"-BanelingExplosionLowVol1"));
            banelingsProjSoundOnCol.sound2=new("BanelingExplosion2",new(Name+"-BanelingExplosionLowVol2"));
            banelingsProjSoundOnCol.sound3=new("BanelingExplosion3",new(Name+"-BanelingExplosionLowVol3"));
            banelingsProjSoundOnCol.sound4=new("BanelingExplosion4",new(Name+"-BanelingExplosionLowVol4"));
            banelingsProjSoundOnCol.sound5=new("BanelingExplosion5",new(Name+"-BanelingExplosionLowVol5"));
            TravelStraitModel banelingsTravel=banelingsProjBehav.GetModel<TravelStraitModel>();
            banelingsTravel.Speed=50;
            banelingsTravel.Lifespan=1.5f;
            banelingsProj.behaviors=banelingsProjBehav.ToArray();
            ActivateAttackModel banelingsActivateAttack=new("Banelings",0.101f,true,new[]{banelingsAttack},true,true,false,false,false,false);
            banelingsBehav.Add(banelingsActivateAttack);
            banelings.behaviors=banelingsBehav.ToArray();
            List<Model>zagaraBehav=zagara.behaviors.ToList();
            zagaraBehav.Add(banelings);
            zagara.behaviors=zagaraBehav.ToArray();
			return zagara;
		}
		public TowerModel Level8(){
			TowerModel zagara=Level7().Clone<TowerModel>();
			zagara.name=Name+" 8";
			zagara.tier=8;
			zagara.tiers=new(new[]{8,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 9",Name+" 9")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 8");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            ProjectileModel zagaraProj=zagara.behaviors.GetModel<AttackModel>().weapons[0].projectile;
            zagaraProj.pierce+=2;
            zagaraProj.CapPierce(zagaraProj.pierce+3);
            zagaraProj.behaviors.GetModel<TravelStraitModel>().Speed+=50;
			return zagara;
		}
		public TowerModel Level9(){
			TowerModel zagara=Level8().Clone<TowerModel>();
			zagara.name=Name+" 9";
			zagara.tier=9;
			zagara.tiers=new(new[]{9,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 10",Name+" 10")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 9");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            ProjectileModel banelingProjExplosion=zagara.behaviors.GetModel<AbilityModel>().behaviors.GetModel<ActivateAttackModel>().
                attacks[0].weapons[0].projectile.behaviors.GetModel<CreateProjectileOnContactModel>().projectile;
            banelingProjExplosion.radius+=5;
            DamageModel banelingProjExplosionDamage=banelingProjExplosion.behaviors.GetModel<DamageModel>();
            banelingProjExplosionDamage.damage=5;
            banelingProjExplosionDamage.CapDamage(banelingProjExplosionDamage.damage);
			return zagara;
		}
		public TowerModel Level10(){
			TowerModel zagara=Level9().Clone<TowerModel>();
			zagara.name=Name+" 10";
			zagara.tier=10;
			zagara.tiers=new(new[]{10,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 11",Name+" 11")};
            List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 10");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            WeaponModel zagaraWeapon=zagara.behaviors.GetModel<AttackModel>().weapons[0];
            zagaraWeapon.emission=new ArcEmissionModel(Name+"ArcEmission",3,0,15,null,false,false);
            DamageModel zagaraProjDamage=zagaraWeapon.projectile.behaviors.GetModel<DamageModel>();
            zagaraProjDamage.damage+=3;
            zagaraProjDamage.CapDamage(zagaraProjDamage.damage+2);
			return zagara;
		}
		public TowerModel Level11(){
			TowerModel zagara=Level10().Clone<TowerModel>();
			zagara.name=Name+" 11";
			zagara.tier=11;
			zagara.tiers=new(new[]{11,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 12",Name+" 12")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 11");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            zagara.behaviors.GetModel<AbilityModel>().Cooldown-=5;
			return zagara;
		}
		public TowerModel Level12(){
			TowerModel zagara=Level11().Clone<TowerModel>();
			zagara.name=Name+" 12";
			zagara.tier=12;
			zagara.tiers=new(new[]{12,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 13",Name+" 13")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 12");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            ActivateAttackModel banelingsActivateAttack=zagara.behaviors.GetModel<AbilityModel>().behaviors.GetModel<ActivateAttackModel>();
            banelingsActivateAttack.Lifespan=0.301f;
            WeaponModel banelingsWeapon=banelingsActivateAttack.attacks[0].weapons[0];
            ProjectileModel banelingsProj=banelingsWeapon.projectile;
            banelingsProj.display=new(Name+"-BanelingsRollPrefab");
            Il2CppReferenceArray<Model>banelingsProjBehav=banelingsProj.behaviors;
            banelingsProjBehav.GetModel<DisplayModel>().display=banelingsProj.display;
            banelingsProjBehav.GetModel<TravelStraitModel>().speed+=30;
			return zagara;
		}
		public TowerModel Level13(){
			TowerModel zagara=Level12().Clone<TowerModel>();
			zagara.name=Name+" 13";
			zagara.tier=13;
			zagara.tiers=new(new[]{13,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 14",Name+" 14")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 13");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            AbilityModel massFrenzy=BlankAbilityModel.Clone<AbilityModel>();
            massFrenzy.name="MassFrenzy";
            massFrenzy.displayName="Mass Frenzy";
            massFrenzy.displayName=massFrenzy.name;
            massFrenzy.addedViaUpgrade=appliedUpgrades.Last();
            massFrenzy.Cooldown=40;
            massFrenzy.startOffCooldown=true;
            massFrenzy.icon=new("Ui["+Name+"-MassFrenzyIcon]");
            List<Model>massFrenzyBehav=massFrenzy.behaviors.ToList();
            massFrenzyBehav.Add(new CreateSoundOnAbilityModel("MassFrenzySound",null,new("MassFrenzy1",new(Name+"-MassFrenzy1")),new("MassFrenzy2",new(Name+"-MassFrenzy2"))));
            massFrenzyBehav.Add(new ActivateRateSupportZoneModel(BuffName,BuffName,true,0.75f,999999,999999,false,10,
                new("DisplayModel",new("Zagara-MassFrenzyEffectPrefab"),0,DisplayCategory.Buff),BuffName,BuffName,null,false));
            massFrenzyBehav.Add(new ActivateTowerDamageSupportZoneModel(Name+"-DamageMassFrenzy",Name+"-DamageMassFrenzy",true,4,999999,999999,true,10,
                null,null,null,0,false,false,null));
            massFrenzy.behaviors=massFrenzyBehav.ToArray();
            List<Model>zagaraBehav=zagara.behaviors.ToList();
            zagaraBehav.Add(massFrenzy);
            zagara.behaviors=zagaraBehav.ToArray();
			return zagara;
		}
		public TowerModel Level14(){
			TowerModel zagara=Level13().Clone<TowerModel>();
			zagara.name=Name+" 14";
			zagara.tier=14;
			zagara.tiers=new(new[]{14,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 15",Name+" 15")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 14");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            zagara.range+=15;
            Il2CppReferenceArray<Model>zagaraBehav=zagara.behaviors;
            zagaraBehav.GetModel<AttackModel>().range=zagara.range;
            zagaraBehav.GetModel<AbilityModel>("Banelings").behaviors.GetModel<ActivateAttackModel>().attacks[0].range=zagara.range+10;
			return zagara;
		}
		public TowerModel Level15(){
			TowerModel zagara=Level14().Clone<TowerModel>();
			zagara.name=Name+" 15";
			zagara.tier=15;
			zagara.tiers=new(new[]{15,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 16",Name+" 16")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 15");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            ActivateAttackModel banelingsActivateAttack=zagara.behaviors.GetModel<AbilityModel>("Banelings").behaviors.GetModel<ActivateAttackModel>();
            banelingsActivateAttack.lifespan=0.501f;
            ProjectileModel banelingsProj=banelingsActivateAttack.attacks[0].weapons[0].projectile;
            List<Model>banelingsProjBehav=banelingsProj.behaviors.ToList();
            banelingsProjBehav.Add(new DamageModel("BanelingDamage",20,999999,true,false,false,BloonProperties.Purple,BloonProperties.Purple,false));
            banelingsProj.behaviors=banelingsProjBehav.ToArray();
			return zagara;
		}
		public TowerModel Level16(){
			TowerModel zagara=Level15().Clone<TowerModel>();
			zagara.name=Name+" 16";
			zagara.tier=16;
			zagara.tiers=new(new[]{16,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 17",Name+" 17")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 16");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            Il2CppReferenceArray<Model>zagaraProjBehav=zagara.behaviors.GetModel<AttackModel>().weapons[0].projectile.behaviors;
            DamageModel zagaraProjDamage=zagaraProjBehav.GetModel<DamageModel>();
            zagaraProjDamage.damage+=4;
            zagaraProjDamage.CapDamage(zagaraProjDamage.damage+5);
            zagaraProjBehav.GetModel<TravelStraitModel>().Lifespan+=0.5f;
			return zagara;
		}
		public TowerModel Level17(){
			TowerModel zagara=Level16().Clone().Cast<TowerModel>();
			zagara.name=Name+" 17";
			zagara.tier=17;
			zagara.tiers=new(new[]{17,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 18",Name+" 18")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 17");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            AbilityModel hunterKillers=BlankAbilityModel.Clone<AbilityModel>();
            hunterKillers.name="HunterKillers";
            hunterKillers.addedViaUpgrade=zagara.appliedUpgrades.Last();
            hunterKillers.displayName="Spawn Hunter Killers";
            hunterKillers.icon=new("Ui["+Name+"-HunterKillersIcon]");
            hunterKillers.Cooldown=30;
            hunterKillers.startOffCooldown=true;
            List<Model>hunterKillersBehav=hunterKillers.behaviors.ToList();
            hunterKillersBehav.Add(new CreateSoundOnAbilityModel("HunterKillersSound",null,
                new("HunterKillers1",new(Name+"-HunterKillers1")),new("HunterKillers2",new(Name+"-HunterKillers2"))));
            AttackModel hunterKillersAttack=CreateTowerAttackModel.Clone<AttackModel>();
            hunterKillersAttack.name="HunterKillers";
            hunterKillersAttack.range=40;
            hunterKillersAttack.targetProvider=new RandomPositionModel("HunterKillersRandom",20,60,7,false,7,true,false,"Land",true,false,20,"Hero");
            hunterKillersAttack.behaviors[0]=hunterKillersAttack.targetProvider;
            WeaponModel hunterKillersWeapon=hunterKillersAttack.weapons[0];
            hunterKillersWeapon.name="HunterKillers";
            hunterKillersWeapon.Rate=0.1f;
            ProjectileModel hunterKillersProj=hunterKillersWeapon.projectile;
            hunterKillersProj.name="HunterKillers";
            hunterKillersProj.ignorePierceExhaustion=true;
            hunterKillersProj.pierce=99999;
            Il2CppReferenceArray<Model>hunterKillersProjBehav=hunterKillersProj.behaviors;
            hunterKillersProjBehav.GetModel<ArriveAtTargetModel>().expireOnArrival=true;
            DisplayModel hunterKillersProjDisplay=hunterKillersProjBehav.GetModel<DisplayModel>();
            hunterKillersProjDisplay.positionOffset=new(0,0,0);
            hunterKillersProjDisplay.delayedReveal=0;
            hunterKillersProjBehav.GetModel<CreateTowerModel>().tower=TowerTypes[Name+"Hydralisk"].TowerModels[1];
            hunterKillersBehav.Add(new ActivateAttackModel("HunterKillers",0.201f,true,new[]{hunterKillersAttack},false,false,false,false,false,false));
            hunterKillers.behaviors=hunterKillersBehav.ToArray();
            List<Model>zagaraBehav=zagara.behaviors.ToList();
            zagaraBehav.Add(hunterKillers);
            zagara.behaviors=zagaraBehav.ToArray();
			return zagara;
		}
		public TowerModel Level18(){
			TowerModel zagara=Level17().Clone<TowerModel>();
			zagara.name=Name+" 18";
			zagara.tier=18;
			zagara.tiers=new(new[]{18,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 19",Name+" 19")};
            List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 18");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            AbilityModel massFrenzy=zagara.behaviors.GetModel<AbilityModel>("MassFrenzy");
            List<Model>massFrenzyBehav=massFrenzy.behaviors.ToList();
            massFrenzyBehav.Add(new ActivatePierceSupportZoneModel(Name+"-PierceMassFrenzy",Name+"-PierceMassFrenzy",true,10,999999,999999,true,15,null,null,null,null,false));
            massFrenzyBehav.GetModel<ActivateRateSupportZoneModel>().lifespan+=5;
            massFrenzyBehav.GetModel<ActivateTowerDamageSupportZoneModel>().lifespan+=5;
            massFrenzy.behaviors=massFrenzyBehav.ToArray();
			return zagara;
		}
		public TowerModel Level19(){
			TowerModel zagara=Level18().Clone<TowerModel>();
			zagara.name=Name+" 19";
			zagara.tier=19;
			zagara.tiers=new(new[]{19,0,0});
			zagara.upgrades=new UpgradePathModel[]{new(Name+" Level 20",Name+" 20")};
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 19");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            foreach(AbilityModel ability in zagara.behaviors.GetModels<AbilityModel>()){
                ability.cooldown-=7.5f;
                if(ability.name.Contains("HunterKillers")){
                    ability.behaviors.GetModel<ActivateAttackModel>().Lifespan=0.501f;
                }
            }
			return zagara;
		}
		public TowerModel Level20(){
			TowerModel zagara=Level19().Clone<TowerModel>();
			zagara.name=Name+" 20";
			zagara.tier=20;
			zagara.tiers=new(new[]{20,0,0});
			zagara.upgrades=new(0);
			List<string>appliedUpgrades=zagara.appliedUpgrades.ToList();
			appliedUpgrades.Add(Name+" Level 20");
			zagara.appliedUpgrades=appliedUpgrades.ToArray();
            AbilityModel dropPods=BlankAbilityModel.Clone<AbilityModel>();
            dropPods.name="DropPods";
            dropPods.cooldown=1;//50;
            dropPods.displayName="Summon Drop Pods";
            dropPods.icon=new("Ui["+Name+"-DropPodsIcon]");
            dropPods.canActivateBetweenRounds=false;
            dropPods.maxActivationsPerRound=1;
            List<Model>dropPodsBehav=dropPods.behaviors.ToList();
            dropPodsBehav.Add(new CreateSoundOnAbilityModel("DropPodsSound",null,new("DropPods1",new(Name+"-DropPods1")),new("DropPods2",new(Name+"-DropPods2"))));
            AttackModel dropPodsAttack=CreateTowerAttackModel.Clone<AttackModel>();
            dropPodsAttack.range=999999;
            dropPodsAttack.name="DropPods";
            dropPodsAttack.addsToSharedGrid=false;
            dropPodsAttack.targetProvider=new RandomPositionModel("DropPodsRandom",15,150,7,false,7,true,false,"Land",true,false,20,"Hero");
            dropPodsAttack.behaviors[0]=dropPodsAttack.targetProvider;
            WeaponModel dropPodsWeapon=dropPodsAttack.weapons[0];
            dropPodsWeapon.name="DropPods";
            dropPodsWeapon.Rate=0.31f;
            dropPodsWeapon.behaviors=new[]{(new CreateSoundOnProjectileCreatedModel("DropPods",new("Fall1",new("Zagara-DropPodFall1")),new("Fall2",new("Zagara-DropPodFall2")),
                new("Fall3",new("Zagara-DropPodFall3")),new("Fall1",new("Zagara-DropPodFall1")),new("Fall2",new("Zagara-DropPodFall2")),"DropPods"))};
            ProjectileModel dropPodsProj=dropPodsWeapon.projectile;
            dropPodsProj.name="DropPod";
            dropPodsProj.display=new(Name+"-DropPodFallPrefab");
            List<Model>dropPodsProjBehav=dropPodsProj.behaviors.ToList();
            dropPodsProjBehav.Add(new CreateSoundOnProjectileExpireModel("DropPods",new("Explode1",new("Zagara-DropPodExplode1")),new("Explode2",new("Zagara-DropPodExplode2")),
                new("Explode3",new("Zagara-DropPodExplode3")),new("Explode1",new("Zagara-DropPodExplode1")),new("Explode2",new("Zagara-DropPodExplode2"))));
            DisplayModel dropPodsProjDisplay=dropPodsProjBehav.GetModel<DisplayModel>();
            dropPodsProjDisplay.delayedReveal=0;
            dropPodsProjDisplay.display=dropPodsProj.display;
            dropPodsProjDisplay.positionOffset=new(0,0,0);
            dropPodsProjBehav.GetModel<AgeModel>().Lifespan=2.25f;
            dropPodsProjBehav.GetModel<CreateTowerModel>().tower=TowerTypes["ZagaraDropPod"].TowerModels[0];
            dropPodsProj.behaviors=dropPodsProjBehav.ToArray();
            dropPodsBehav.Add(new ActivateAttackModel("DropPods",6.2f,true,new[]{dropPodsAttack},false,false,false,false,false,false));
            dropPods.behaviors=dropPodsBehav.ToArray();
            List<Model>zagaraBehav=zagara.behaviors.ToList();
            zagaraBehav.Add(dropPods);
            zagara.behaviors=zagaraBehav.ToArray();
			return zagara;
		}
        public override void Attack(Weapon weapon){
            switch(weapon.weaponModel.name){
                case"WeaponModel_Talons":
                    PlayAnimation(zagaraCom.animator,"MeleeAttack");
                    break;
                case"WeaponModel_Spines":
                    PlayAnimation(zagaraCom.animator,"RangedAttack");
                    break;
                case"WeaponModel_DropPods":
                    zagaraCom.dropPods+=1;
                    Log(zagaraCom.dropPods);
                    break;
            }
        }
        public override bool Ability(string name,Tower tower){
            if(!zagaraCom.stateName.Contains("Ability")){
                switch(name){
                    case"AbilityModel_Banelings":
                        PlayAnimation(zagaraCom.animator,"Ability2");
                        return true;
                    case"AbilityModel_MassFrenzy":
                        PlayAnimation(zagaraCom.animator,"Ability1");
                        return true;
                    case"AbilityModel_DropPods":
                        zagaraCom.dropPods=0;
                        PlayAnimation(zagaraCom.animator,"AbilityStart");
                        return true;
                    case"AbilityModel_HunterKillers":
                        return true;
                    default:
                        return false;
                }
            }
            return true;
        }
        public override bool AttackTargetFilter(Attack attack){
            if(zagaraCom.stateName.Contains("Ability")||zagaraCom.dropPods>0){
                return false;
            }
            return true;
        }
        [HarmonyPatch(typeof(ActivateTowerSupportZone),nameof(ActivateTowerSupportZone.CheckBehavior))]
        public class ActivateTowerSupportZoneCheckBehavior_Patch{
            public static bool Prefix(ActivateTowerSupportZone __instance,Tower tower,ref bool __result){
                string id=tower.towerModel.baseId;
                if(__instance.Model.name.Contains("MassFrenzy")){
                    if(TowerTypes.ContainsKey(id)&&TowerTypes[id].TowerFaction==Faction.Zerg){
                        __result=true;
                        return false;
                    }
                    __result=false;
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(CreateSoundOnProjectileCreated),nameof(CreateSoundOnProjectileCreated.PlayProjectileSound))]
        public class CreateSoundOnProjectileCreatedPlayProjectileSound_Patch{
            public static bool Prefix(ref CreateSoundOnProjectileCreated __instance){
                if(__instance.weapon.weaponModel.name.Contains("DropPods")){
                    AudioClipReference clip=null;
                    switch(__instance.Sim.syncedRandom.Next(0,5)){
                        case 0:
                            clip=__instance.createSoundOnProjectileCreatedModel.sound1.assetId;
                            break;
                        case 1:
                            clip=__instance.createSoundOnProjectileCreatedModel.sound2.assetId;
                            break;
                        case 2:
                            clip=__instance.createSoundOnProjectileCreatedModel.sound3.assetId;
                            break;
                        case 3:
                            clip=__instance.createSoundOnProjectileCreatedModel.sound4.assetId;
                            break;
                        case 4:
                            clip=__instance.createSoundOnProjectileCreatedModel.sound5.assetId;
                            break;
                    }
                    __instance.Sim.audioLimiterManager.PlaySound(clip,false,false,"__22",0.1f,"DropPodsExplode","DropPodsExplode",25,25,-1,0);
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(CreateSoundOnProjectileExpire),nameof(CreateSoundOnProjectileExpire.PlayProjectileExpireSound))]
        public class CreateSoundOnProjectileExpirePlayProjectileSound{
            public static bool Prefix(ref CreateSoundOnProjectileExpire __instance){
                if(__instance.projectile.projectileModel.name.Contains("DropPod")){
                    __instance.Sim.audioLimiterManager.PlaySound(__instance.sounds[__instance.Sim.syncedRandom.Next(0,5)],false,false,"__22",1f,"DropPodsFall","DropPodsFall",25,25,-1,0);
                    return false;
                }
                return true;
            }
        }
    }
}