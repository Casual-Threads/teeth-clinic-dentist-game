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

//blush, bouqet, braclete, dress, earing, eyebrow, eyelashes, eyeshade, facemask, hair, handbag, lanz, lips, shoes;
[System.Serializable]
public class PartyModeElements
{
    public List<bool> blush = new List<bool>();
    public List<bool> bouqet = new List<bool>();
    public List<bool> braclete = new List<bool>();
    public List<bool> dress = new List<bool>();
    public List<bool> earing = new List<bool>();
    public List<bool> eyebrow = new List<bool>();
    public List<bool> eyelashes = new List<bool>();
    public List<bool> eyeshade = new List<bool>();
    public List<bool> facemask = new List<bool>();
    public List<bool> hair = new List<bool>();
    public List<bool> handbag = new List<bool>();
    public List<bool> lanz = new List<bool>();
    public List<bool> lips = new List<bool>();
    public List<bool> shoes = new List<bool>();
}

[System.Serializable]
public class IndianModeElements
{
    public List<bool> blush = new List<bool>();
    public List<bool> necklace = new List<bool>();
    public List<bool> bangle = new List<bool>();
    public List<bool> dress = new List<bool>();
    public List<bool> earing = new List<bool>();
    public List<bool> eyebrow = new List<bool>();
    public List<bool> eyelashes = new List<bool>();
    public List<bool> eyeshade = new List<bool>();
    public List<bool> bindi = new List<bool>();
    public List<bool> hair = new List<bool>();
    public List<bool> handbag = new List<bool>();
    public List<bool> lanz = new List<bool>();
    public List<bool> lips = new List<bool>();
    public List<bool> shoes = new List<bool>();
}

[System.Serializable]
public class CasualModeElements
{
    public List<bool> blush = new List<bool>();
    public List<bool> necklace = new List<bool>();
    public List<bool> bracelet = new List<bool>();
    public List<bool> dress = new List<bool>();
    public List<bool> earing = new List<bool>();
    public List<bool> eyebrow = new List<bool>();
    public List<bool> eyelashes = new List<bool>();
    public List<bool> eyeshade = new List<bool>();
    public List<bool> hair = new List<bool>();
    public List<bool> handbag = new List<bool>();
    public List<bool> lanz = new List<bool>();
    public List<bool> lips = new List<bool>();
    public List<bool> shoes = new List<bool>();
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
    public bool isSound = true, isMusic = true, isVibration = true, isRightControls = true;
    public int Coins = 3000;
    public List<PlayerProps> Players = new List<PlayerProps>();
    public List<Modesprops> ModeProps = new List<Modesprops>();
    public PartyModeElements PartyModeElements = new PartyModeElements();
    public IndianModeElements IndianModeElements = new IndianModeElements();
    public CasualModeElements CasualModeElements = new CasualModeElements();
    public string hashOfSaveData;

    //Constructor to save actual GameData
    public SaveData() { }

    //Constructor to check any tampering with the SaveData
    public SaveData(bool ads, int levelsUnlocked, int eventsUnlocked, int coins, bool soundOn, bool musicOn, bool vibrationOn, bool rightControls, List<PlayerProps> _players,
                    List<Modesprops> _modeProps, PartyModeElements _PartyModeElements, IndianModeElements _IndianModeElements, CasualModeElements _CasualModeElements)
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
        PartyModeElements = _PartyModeElements;
        IndianModeElements = _IndianModeElements;
        CasualModeElements = _CasualModeElements;
    }
}