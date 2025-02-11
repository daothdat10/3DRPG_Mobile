
using System;
using System.Globalization;
using UnityEngine;
using System.Collections;


    
    public abstract class DailyRewardsCore<T> : MonoBehaviour where T : DailyRewardsCore<T>
    {
        public bool isSingleton = true;                         

        public string errorMessage;                            
        public bool isErrorConnect;                             
        public DateTime now;                                    

        public int maxRetries = 3;                              

        public delegate void OnInitialize(bool error = false, string errorMessage = ""); 
        public OnInitialize onInitialize;

        public bool isInitialized = false;
		private static T _instance;

        
        public void InitializeDate()
        {
	        now = DateTime.Now;
	        isInitialized = true;
        }

        public void RefreshTime ()
        {
            now = DateTime.Now;
        }

		public static T instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();
					if (_instance == null)
					{
						GameObject obj = new GameObject();
						obj.hideFlags = HideFlags.HideAndDontSave;
						_instance = obj.AddComponent<T>();
					}
				}
				
				return _instance;
			}
		}

        
        public virtual void TickTime()
        {
            if (!isInitialized)
				return;

            now = now.AddSeconds(Time.unscaledDeltaTime);
        }

        public string GetFormattedTime (TimeSpan span)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
        }

        protected virtual void Awake()
        {
			if (isSingleton)
				DontDestroyOnLoad(this.gameObject);
			
			if (_instance == null)
				_instance = this as T;
			else
				Destroy(gameObject);
        }

        protected virtual void OnApplicationPause(bool pauseStatus)
        {
            if(!pauseStatus)
            {
                RefreshTime();
            }                
        }
    }
