// Decompile from assembly: Assembly-CSharp.dll

using System;
using UnityEngine;

namespace MoreMountains.NiceVibrations
{
	public static class MMVibrationManager
	{
		public static long LightDuration = 20L;

		public static long MediumDuration = 40L;

		public static long HeavyDuration = 80L;

		public static int LightAmplitude = 40;

		public static int MediumAmplitude = 120;

		public static int HeavyAmplitude = 255;

		private static int _sdkVersion = -1;

		private static long[] _lightimpactPattern = new long[]
		{
			0L,
			MMVibrationManager.LightDuration
		};

		private static int[] _lightimpactPatternAmplitude = new int[]
		{
			0,
			MMVibrationManager.LightAmplitude
		};

		private static long[] _mediumimpactPattern = new long[]
		{
			0L,
			MMVibrationManager.MediumDuration
		};

		private static int[] _mediumimpactPatternAmplitude = new int[]
		{
			0,
			MMVibrationManager.MediumAmplitude
		};

		private static long[] _HeavyimpactPattern = new long[]
		{
			0L,
			MMVibrationManager.HeavyDuration
		};

		private static int[] _HeavyimpactPatternAmplitude = new int[]
		{
			0,
			MMVibrationManager.HeavyAmplitude
		};

		private static long[] _successPattern = new long[]
		{
			0L,
			MMVibrationManager.LightDuration,
			MMVibrationManager.LightDuration,
			MMVibrationManager.HeavyDuration
		};

		private static int[] _successPatternAmplitude = new int[]
		{
			0,
			MMVibrationManager.LightAmplitude,
			0,
			MMVibrationManager.HeavyAmplitude
		};

		private static long[] _warningPattern = new long[]
		{
			0L,
			MMVibrationManager.HeavyDuration,
			MMVibrationManager.LightDuration,
			MMVibrationManager.MediumDuration
		};

		private static int[] _warningPatternAmplitude = new int[]
		{
			0,
			MMVibrationManager.HeavyAmplitude,
			0,
			MMVibrationManager.MediumAmplitude
		};

		private static long[] _failurePattern = new long[]
		{
			0L,
			MMVibrationManager.MediumDuration,
			MMVibrationManager.LightDuration,
			MMVibrationManager.MediumDuration,
			MMVibrationManager.LightDuration,
			MMVibrationManager.HeavyDuration,
			MMVibrationManager.LightDuration,
			MMVibrationManager.LightDuration
		};

		private static int[] _failurePatternAmplitude = new int[]
		{
			0,
			MMVibrationManager.MediumAmplitude,
			0,
			MMVibrationManager.MediumAmplitude,
			0,
			MMVibrationManager.HeavyAmplitude,
			0,
			MMVibrationManager.LightAmplitude
		};

		private static AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

		private static AndroidJavaObject CurrentActivity = MMVibrationManager.UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

		private static AndroidJavaObject AndroidVibrator = MMVibrationManager.CurrentActivity.Call<AndroidJavaObject>("getSystemService", new object[]
		{
			"vibrator"
		});

		private static AndroidJavaClass VibrationEffectClass;

		private static AndroidJavaObject VibrationEffect;

		private static int DefaultAmplitude;

		private static IntPtr AndroidVibrateMethodRawClass = AndroidJNIHelper.GetMethodID(MMVibrationManager.AndroidVibrator.GetRawClass(), "vibrate", "(J)V", false);

		private static jvalue[] AndroidVibrateMethodRawClassParameters = new jvalue[1];

		private static bool iOSHapticsInitialized = false;

		public static bool Android()
		{
			return true;
		}

		public static bool iOS()
		{
			return false;
		}

		public static void Vibrate()
		{
			if (MMVibrationManager.Android())
			{
				MMVibrationManager.AndroidVibrate(MMVibrationManager.MediumDuration);
			}
			else if (MMVibrationManager.iOS())
			{
				MMVibrationManager.iOSTriggerHaptics(HapticTypes.MediumImpact);
			}
		}

		public static void Haptic(HapticTypes type, bool defaultToRegularVibrate = false)
		{
			if (defaultToRegularVibrate)
			{
				Handheld.Vibrate();
				return;
			}
			if (MMVibrationManager.Android())
			{
				switch (type)
				{
				case HapticTypes.Selection:
					MMVibrationManager.AndroidVibrate(MMVibrationManager.LightDuration, MMVibrationManager.LightAmplitude);
					break;
				case HapticTypes.Success:
					MMVibrationManager.AndroidVibrate(MMVibrationManager._successPattern, MMVibrationManager._successPatternAmplitude, -1);
					break;
				case HapticTypes.Warning:
					MMVibrationManager.AndroidVibrate(MMVibrationManager._warningPattern, MMVibrationManager._warningPatternAmplitude, -1);
					break;
				case HapticTypes.Failure:
					MMVibrationManager.AndroidVibrate(MMVibrationManager._failurePattern, MMVibrationManager._failurePatternAmplitude, -1);
					break;
				case HapticTypes.LightImpact:
					MMVibrationManager.AndroidVibrate(MMVibrationManager._lightimpactPattern, MMVibrationManager._lightimpactPatternAmplitude, -1);
					break;
				case HapticTypes.MediumImpact:
					MMVibrationManager.AndroidVibrate(MMVibrationManager._mediumimpactPattern, MMVibrationManager._mediumimpactPatternAmplitude, -1);
					break;
				case HapticTypes.HeavyImpact:
					MMVibrationManager.AndroidVibrate(MMVibrationManager._HeavyimpactPattern, MMVibrationManager._HeavyimpactPatternAmplitude, -1);
					break;
				}
			}
			else if (MMVibrationManager.iOS())
			{
				MMVibrationManager.iOSTriggerHaptics(type);
			}
		}

		public static void AndroidVibrate(long milliseconds)
		{
			if (!MMVibrationManager.Android())
			{
				return;
			}
			MMVibrationManager.AndroidVibrateMethodRawClassParameters[0].j = milliseconds;
			AndroidJNI.CallVoidMethod(MMVibrationManager.AndroidVibrator.GetRawObject(), MMVibrationManager.AndroidVibrateMethodRawClass, MMVibrationManager.AndroidVibrateMethodRawClassParameters);
		}

		public static void AndroidVibrate(long milliseconds, int amplitude)
		{
			if (!MMVibrationManager.Android())
			{
				return;
			}
			if (MMVibrationManager.AndroidSDKVersion() < 26)
			{
				MMVibrationManager.AndroidVibrate(milliseconds);
			}
			else
			{
				MMVibrationManager.VibrationEffectClassInitialization();
				MMVibrationManager.VibrationEffect = MMVibrationManager.VibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", new object[]
				{
					milliseconds,
					amplitude
				});
				MMVibrationManager.AndroidVibrator.Call("vibrate", new object[]
				{
					MMVibrationManager.VibrationEffect
				});
			}
		}

		public static void AndroidVibrate(long[] pattern, int repeat)
		{
			if (!MMVibrationManager.Android())
			{
				return;
			}
			if (MMVibrationManager.AndroidSDKVersion() < 26)
			{
				MMVibrationManager.AndroidVibrator.Call("vibrate", new object[]
				{
					pattern,
					repeat
				});
			}
			else
			{
				MMVibrationManager.VibrationEffectClassInitialization();
				MMVibrationManager.VibrationEffect = MMVibrationManager.VibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", new object[]
				{
					pattern,
					repeat
				});
				MMVibrationManager.AndroidVibrator.Call("vibrate", new object[]
				{
					MMVibrationManager.VibrationEffect
				});
			}
		}

		public static void AndroidVibrate(long[] pattern, int[] amplitudes, int repeat)
		{
			if (!MMVibrationManager.Android())
			{
				return;
			}
			if (MMVibrationManager.AndroidSDKVersion() < 26)
			{
				MMVibrationManager.AndroidVibrator.Call("vibrate", new object[]
				{
					pattern,
					repeat
				});
			}
			else
			{
				MMVibrationManager.VibrationEffectClassInitialization();
				MMVibrationManager.VibrationEffect = MMVibrationManager.VibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", new object[]
				{
					pattern,
					amplitudes,
					repeat
				});
				MMVibrationManager.AndroidVibrator.Call("vibrate", new object[]
				{
					MMVibrationManager.VibrationEffect
				});
			}
		}

		public static void AndroidCancelVibrations()
		{
			if (!MMVibrationManager.Android())
			{
				return;
			}
			MMVibrationManager.AndroidVibrator.Call("cancel", new object[0]);
		}

		private static void VibrationEffectClassInitialization()
		{
			if (MMVibrationManager.VibrationEffectClass == null)
			{
				MMVibrationManager.VibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
			}
		}

		public static int AndroidSDKVersion()
		{
			if (MMVibrationManager._sdkVersion == -1)
			{
				int num = int.Parse(SystemInfo.operatingSystem.Substring(SystemInfo.operatingSystem.IndexOf("-") + 1, 3));
				MMVibrationManager._sdkVersion = num;
				return num;
			}
			return MMVibrationManager._sdkVersion;
		}

		private static void InstantiateFeedbackGenerators()
		{
		}

		private static void ReleaseFeedbackGenerators()
		{
		}

		private static void SelectionHaptic()
		{
		}

		private static void SuccessHaptic()
		{
		}

		private static void WarningHaptic()
		{
		}

		private static void FailureHaptic()
		{
		}

		private static void LightImpactHaptic()
		{
		}

		private static void MediumImpactHaptic()
		{
		}

		private static void HeavyImpactHaptic()
		{
		}

		public static void iOSInitializeHaptics()
		{
			if (!MMVibrationManager.iOS())
			{
				return;
			}
			MMVibrationManager.InstantiateFeedbackGenerators();
			MMVibrationManager.iOSHapticsInitialized = true;
		}

		public static void iOSReleaseHaptics()
		{
			if (!MMVibrationManager.iOS())
			{
				return;
			}
			MMVibrationManager.ReleaseFeedbackGenerators();
		}

		public static bool HapticsSupported()
		{
			return false;
		}

		public static void iOSTriggerHaptics(HapticTypes type)
		{
			if (!MMVibrationManager.iOS())
			{
				return;
			}
			if (!MMVibrationManager.iOSHapticsInitialized)
			{
				MMVibrationManager.iOSInitializeHaptics();
			}
			if (MMVibrationManager.HapticsSupported())
			{
				switch (type)
				{
				case HapticTypes.Selection:
					MMVibrationManager.SelectionHaptic();
					break;
				case HapticTypes.Success:
					MMVibrationManager.SuccessHaptic();
					break;
				case HapticTypes.Warning:
					MMVibrationManager.WarningHaptic();
					break;
				case HapticTypes.Failure:
					MMVibrationManager.FailureHaptic();
					break;
				case HapticTypes.LightImpact:
					MMVibrationManager.LightImpactHaptic();
					break;
				case HapticTypes.MediumImpact:
					MMVibrationManager.MediumImpactHaptic();
					break;
				case HapticTypes.HeavyImpact:
					MMVibrationManager.HeavyImpactHaptic();
					break;
				}
			}
		}

		public static string iOSSDKVersion()
		{
			return null;
		}
	}
}
