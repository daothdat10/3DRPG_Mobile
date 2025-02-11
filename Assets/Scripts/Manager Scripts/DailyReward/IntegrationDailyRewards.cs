
using UnityEngine;



public class IntegrationDailyRewards : MonoBehaviour
{
    void OnEnable()
    {
        DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }

    void OnDisable()
    {
		DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

  
    public void OnClaimPrizeDailyRewards(int day)
    {
       
		Reward myReward = DailyRewards.instance.GetReward(day);

        
        print(myReward.unit);   // This is your reward Unit name
        print(myReward.reward); // This is your reward count

		var rewardsCount = PlayerPrefs.GetInt ("MY_REWARD_KEY", 0);
		rewardsCount += myReward.reward;

		PlayerPrefs.SetInt ("MY_REWARD_KEY", rewardsCount);
		PlayerPrefs.Save ();
    }
}