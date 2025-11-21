using HutongGames.PlayMaker;

namespace AllGrubRewardsAtOnce.Helpers
{
    /// <summary>
    /// During the Final Reward? step, if there are still rewards to give, send the AllGrubRewardsAtOnce event to 
    /// go back to the Activate Reward step
    /// </summary>
    public class GiveNextReward : FsmStateAction
    {
        public override void OnEnter()
        {
            if (PlayerData.instance.GetInt("grubRewards") < PlayerData.instance.GetInt("grubsCollected"))
            {
                base.Fsm.Event("AllGrubRewardsAtOnce");
            }

            // If we don't need to do any special logic, Finish the step and move on to the regular process
            Finish();
        }
    }
}