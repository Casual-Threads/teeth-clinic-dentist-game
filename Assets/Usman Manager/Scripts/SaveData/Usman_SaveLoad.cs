using UnityEngine;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class Usman_SaveLoad
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "SavedGame.json");
    public static void SaveProgress()
    {
        SaveData CheckSave = new SaveData(SaveData.Instance.RemoveAds, SaveData.Instance.LevelsUnlocked, SaveData.Instance.EventsUnlocked, SaveData.Instance.Coins,
        SaveData.Instance.isSound, SaveData.Instance.isMusic, SaveData.Instance.isVibration, SaveData.Instance.isRightControls, SaveData.Instance.Players, SaveData.Instance.ModeProps,
        SaveData.Instance.PartyModeElements, SaveData.Instance.IndianModeElements, SaveData.Instance.CasualModeElements);
        string saveDataString = JsonUtility.ToJson(CheckSave, true);
        SaveData.Instance.hashOfSaveData = HashGenerator(saveDataString);
        string saveDataHashed = JsonUtility.ToJson(SaveData.Instance, true);
        File.WriteAllText(filePath, saveDataHashed);
    }

    public static void LoadProgress()
    {
        if (File.Exists(filePath))
        {
            string fileContent = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(fileContent, SaveData.Instance);
            Debug.Log("Game Load Successful --> " + filePath);
        }
        else
        {
            Debug.Log("New Game Creation Successful --> " + filePath);
            SaveProgress();
        }
    }

    public static string HashGenerator(string saveContent)
    {
        SHA256Managed crypt = new SHA256Managed();
        string hash = string.Empty;
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(saveContent), 0, Encoding.UTF8.GetByteCount(saveContent));
        foreach (byte bit in crypto)
        {
            hash += bit.ToString("x2");
        }
        return hash;
    }

    public static void DeleteProgress()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
