using UnityEditor;
using UnityEngine;

public class ResetSaveData{
	[MenuItem("Window/MyMenu - Usman Framework/Reset Save Data %#r")]
	private static void ResetSave (){				
		Reset ();
	}

	[MenuItem("Window/MyMenu - Usman Framework/Open Save File %#o")]
	private static void OpenSave (){
		Application.OpenURL (Application.persistentDataPath);
	}

	public static void Reset(){
		Usman_SaveLoad.DeleteProgress();
		EditorUtility.DisplayDialog("MyMenu - Usman Framework",
			"Save data reset successfull !", 
			"Ok");
	}
}

