using UnityEngine;

public class PlayerPrefManager : MonoBehaviour
{
    public static int IsLevelUnlocked
    {
        get
        {
            if (!PlayerPrefs.HasKey(Constants.LEVEL_UNLOCK_KEY))
            {
                PlayerPrefs.SetInt(Constants.LEVEL_UNLOCK_KEY, 0);
                PlayerPrefs.Save();
            }
            return PlayerPrefs.GetInt(Constants.LEVEL_UNLOCK_KEY);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.LEVEL_UNLOCK_KEY, value);
            PlayerPrefs.Save();
        }
    }
}
