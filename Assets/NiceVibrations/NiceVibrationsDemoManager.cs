// Decompile from assembly: Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.NiceVibrations
{
	public class NiceVibrationsDemoManager : MonoBehaviour
	{
		public static NiceVibrationsDemoManager Instance;

		//private void Awake()
		//{

		//}
		//public Text DebugTextBox;
		protected string _debugString;

		protected string _platformString;

		protected const string _CURRENTVERSION = "1.5";

		protected virtual void Awake ()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
			#if UNITY_EDITOR
			return;
            #endif
            MMVibrationManager.iOSInitializeHaptics();
			
		}

        protected virtual void Start ()
		{
            #if UNITY_EDITOR
            return;
            #endif
            this.DisplayInformation();
        }

        protected virtual void DisplayInformation ()
		{
			if (MMVibrationManager.Android ()) {
				this._platformString = "API version " + MMVibrationManager.AndroidSDKVersion ().ToString ();
			} else if (MMVibrationManager.iOS ()) {
				this._platformString = "iOS " + MMVibrationManager.iOSSDKVersion ();
			} else {
				this._platformString = Application.platform + ", not supported by Nice Vibrations for now.";
			}
			//this.DebugTextBox.text = "Platform : " + this._platformString + "\n Nice Vibrations v1.5";
		}

		protected virtual void OnDisable ()
		{
            #if UNITY_EDITOR
            return;
             #endif 

            MMVibrationManager.iOSReleaseHaptics ();          
           
        }

        public virtual void TriggerDefault ()
		{
			Handheld.Vibrate ();
		}

		public virtual void TriggerVibrate ()
		{
            #if UNITY_EDITOR
            return;
            #endif
            MMVibrationManager.Vibrate ();
		}

		public virtual void TriggerSelection ()
		{
			MMVibrationManager.Haptic (HapticTypes.Selection, false);
		}

		public virtual void TriggerSuccess ()
		{
			MMVibrationManager.Haptic (HapticTypes.Success, false);
		}

		public virtual void TriggerWarning ()
		{
			MMVibrationManager.Haptic (HapticTypes.Warning, false);
		}

		public virtual void TriggerFailure ()
		{
			MMVibrationManager.Haptic (HapticTypes.Failure, false);
		}

		public virtual void TriggerLightImpact ()
		{
			MMVibrationManager.Haptic (HapticTypes.LightImpact, false);
		}

		public virtual void TriggerMediumImpact ()
		{
			MMVibrationManager.Haptic (HapticTypes.MediumImpact, false);
		}

		public virtual void TriggerHeavyImpact ()
		{
			MMVibrationManager.Haptic (HapticTypes.HeavyImpact, false);
		}
	}
}
