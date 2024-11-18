
using UnityEngine;
using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;



    
    public class DailyRewards : DailyRewardsCore<DailyRewards>
    {
        public List<Reward> rewards;        
        public DateTime lastRewardTime;     
        public int availableReward;         
        public int lastReward;              
        public bool keepOpen = true;        

        // Delegates
        public delegate void OnClaimPrize(int day);                 
        public OnClaimPrize onClaimPrize;

        // Needed Constants
        private const string LAST_REWARD_TIME = "LastRewardTime";
        private const string LAST_REWARD = "LastReward";
        private const string DEBUG_TIME = "DebugTime";
        private const string FMT = "O";

        public TimeSpan debugTime;         

        void Start()
        {
            
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            base.InitializeDate();

            if (base.isErrorConnect) 
			{
                if (onInitialize != null)
                    onInitialize(true, base.errorMessage);
			}
            else 
			{
                LoadDebugTime();
                
                CheckRewards();

                if(onInitialize!=null)
                    onInitialize();
			}
        }

        protected override void OnApplicationPause(bool pauseStatus)
        {
            base.OnApplicationPause(pauseStatus);
            CheckRewards();
        }

        public TimeSpan GetTimeDifference()
        {
            TimeSpan difference = (lastRewardTime - now);
            difference = difference.Subtract(debugTime);
            return difference.Add(new TimeSpan(0, 24, 0, 0));
        }

        private void LoadDebugTime ()
        {
            int debugHours = PlayerPrefs.GetInt(GetDebugTimeKey(), 0);
            debugTime = new TimeSpan(debugHours, 0, 0);
        }

        
        public void CheckRewards()
        {
            string lastClaimedTimeStr = PlayerPrefs.GetString(GetLastRewardTimeKey());
            lastReward = PlayerPrefs.GetInt(GetLastRewardKey());

            
            if (!string.IsNullOrEmpty(lastClaimedTimeStr))
            {
                lastRewardTime = DateTime.ParseExact(lastClaimedTimeStr, FMT, CultureInfo.InvariantCulture);

                
                DateTime advancedTime = now.AddHours(debugTime.TotalHours);

                TimeSpan diff = advancedTime - lastRewardTime;
                Debug.Log(" Last claim was " + (long)diff.TotalHours + " hours ago.");

                int days = (int)(Math.Abs(diff.TotalHours) / 24);
                if (days == 0)
                {
                    
                    availableReward = 0;
                    return;
                }

                
                if (days >= 1 && days < 2)
                {
                    
                    if (lastReward == rewards.Count)
                    {
                        availableReward = 1;
                        lastReward = 0;
                        return;
                    }

                    availableReward = lastReward + 1;

                    Debug.Log(" Player can claim prize " + availableReward);
                    return;
                }

                if (days >= 2)
                {
                    
                    availableReward = 1;
                    lastReward = 0;
                    Debug.Log(" Prize reset ");
                }
            }
            else
            {
                
                availableReward = 1;
            }
        }

        
        public void ClaimPrize()
        {
            if (availableReward > 0)
            {
                // Delegate
                if (onClaimPrize != null)
                    onClaimPrize(availableReward);

                Debug.Log(" Reward [" + rewards[availableReward - 1] + "] Claimed!");
                PlayerPrefs.SetInt(GetLastRewardKey(), availableReward);

                
                string lastClaimedStr = now.AddHours(debugTime.TotalHours).ToString(FMT);
                PlayerPrefs.SetString(GetLastRewardTimeKey(), lastClaimedStr);
                PlayerPrefs.SetInt(GetDebugTimeKey(), (int)debugTime.TotalHours);
            }
            else if (availableReward == 0)
            {
                Debug.LogError("Error! The player is trying to claim the same reward twice.");
            }

            CheckRewards();
        }

        
        private string GetLastRewardKey()
        {
            return LAST_REWARD;
        }

        
        private string GetLastRewardTimeKey()
        {
        	return LAST_REWARD_TIME;
        }

        
        private string GetDebugTimeKey()
        {
            return DEBUG_TIME;
        }

        
        public Reward GetReward(int day)
        {
            return rewards[day - 1];
        }

        
        public void Reset()
        {
            PlayerPrefs.DeleteKey(GetLastRewardKey());
            PlayerPrefs.DeleteKey(GetLastRewardTimeKey());
            PlayerPrefs.DeleteKey(GetDebugTimeKey());
        }
    }
