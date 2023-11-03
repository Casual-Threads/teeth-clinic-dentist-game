using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerProps
{
    public string playerName;
    public int playerHealth;
    public int playerDamage;
    public int playerRange;
    public bool isLocked = true;
}

[System.Serializable]
public class Modesprops
{
    public bool isLocked;
}



[System.Serializable]
public class SaveData
{

    public static SaveData instance;
    public static SaveData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveData();
            }
            return instance;
        }
    }
    public bool RemoveAds = false;
    //public int vsMode;
    public int LevelsUnlocked = 1;
    public int levelIndex = 0;
    public int EventsUnlocked = 0;
    public int SelectedAvatar = 0;
    public string ProfileName;
    public bool ProfileCreated = false;
    public bool isRateUs = true;
    public bool isIndicationOff = false;
    public bool isSound = true, isMusic = true, isVibration = true, isRightControls = true;
    public int Coins = 3000;
    public List<PlayerProps> Players = new List<PlayerProps>();
    public List<Modesprops> ModeProps = new List<Modesprops>();
    public string hashOfSaveData;

    //Constructor to save actual GameData
    public SaveData() { }

    //Constructor to check any tampering with the SaveData
    public SaveData(bool ads, int levelsUnlocked, int eventsUnlocked, int coins, bool soundOn, bool musicOn, bool vibrationOn, bool rightControls, List<PlayerProps> _players, List<Modesprops> _modeProps)
    {
        RemoveAds = ads;
        LevelsUnlocked = levelsUnlocked;
        EventsUnlocked = eventsUnlocked;
        Coins = coins;
        isSound = soundOn;
        isMusic = musicOn;
        isVibration = vibrationOn;
        isRightControls = rightControls;
        Players = _players;
        ModeProps = _modeProps;

    }
}