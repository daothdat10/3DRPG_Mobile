
using TMPro;
using UnityEngine;
using UnityEngine.UI;



    public class DailyRewardUI : MonoBehaviour
    {
        public bool showRewardName;

        [Header("UI Elements")]
        public TextMeshProUGUI textDay;                
        public TextMeshProUGUI textReward;             
        public Image imageRewardBackground; 
        public Image imageReward;          
        public Color colorClaim;            
        private Color colorUnclaimed;       

        [Header("Internal")]
        public int day;

        [HideInInspector]
        public Reward reward;

        public DailyRewardState state;

        // The States a reward can have
        public enum DailyRewardState
        {
            UNCLAIMED_AVAILABLE,
            UNCLAIMED_UNAVAILABLE,
            CLAIMED
        }

        void Awake()
        {
            colorUnclaimed = imageReward.color;
        }

        public void Initialize()
        {
            textDay.text = string.Format("Day {0}", day.ToString());
            if (reward.reward > 0)
            {
                if (showRewardName)
                {
                    textReward.text = reward.reward + " " + reward.unit;
                }
                else
                {
                    textReward.text = reward.reward.ToString();
                }
            }
            else
            {
                textReward.text = reward.unit.ToString();
            }
            imageReward.sprite = reward.sprite;
        }

        // Refreshes the UI
        public void Refresh()
        {
            switch (state)
            {
                case DailyRewardState.UNCLAIMED_AVAILABLE:
                    imageRewardBackground.color = colorClaim;
                    break;
                case DailyRewardState.UNCLAIMED_UNAVAILABLE:
                    imageRewardBackground.color = colorUnclaimed;
                    break;
                case DailyRewardState.CLAIMED:
                    imageRewardBackground.color = colorClaim;
                    break;
            }
        }
    }
