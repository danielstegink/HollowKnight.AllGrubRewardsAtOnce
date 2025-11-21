using System.Collections.Generic;
using AllGrubRewardsAtOnce.Helpers;
using HutongGames.PlayMaker;
using Modding;
using SFCore.Utils;
using UnityEngine;

namespace AllGrubRewardsAtOnce
{
    public class AllGrubRewardsAtOnce : Mod
    {
        internal static AllGrubRewardsAtOnce Instance;

        public override string GetVersion() => "1.0.0.0";

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            Instance = this;
            On.HutongGames.PlayMaker.Fsm.Awake += ModifyFsm;

            Log("Initialized");
        }

        /// <summary>
        /// Modifies the Grubfather's FSM so that if there are rewards left to give, 
        /// the Final Reward? step goes back to the Activate Reward step
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        private void ModifyFsm(On.HutongGames.PlayMaker.Fsm.orig_Awake orig, HutongGames.PlayMaker.Fsm self)
        { 
            orig(self);

            if (self.Name.Equals("King Control") &&
                self.GameObjectName.Equals("Grub King"))
            {
                FsmState finalReward = self.GetState("Final Reward?");
                SFCore.Utils.FsmUtil.AddTransition(finalReward, "AllGrubRewardsAtOnce", "Activate Reward");

                finalReward.InsertAction(new GiveNextReward(), 0);
            }
        }
    }
}