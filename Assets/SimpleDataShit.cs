using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SimpleDataShit : MonoBehaviour
{
    #region Singleton

    //-------------------------------------------------------------
    public static SimpleDataShit Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // transform.SetParent(null);
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-------------------------------------------------------------

    #endregion

    public AudioData collectDrawGemSound;
    public TextMeshProUGUI drawGemCount;
    const string GEM_KEY_SAVE = nameof(GEM_KEY_SAVE);
    int gems;

    void Start()
    {
        Debug.LogWarning("SHIT SCRIPT");
        gems = PlayerPrefs.GetInt(GEM_KEY_SAVE, 0);
        RefreshText();
    }


    public void AddGem()
    {
        AudioManager.Instance.PlaySound(collectDrawGemSound);
        gems++;
        drawGemCount.transform.DOScale(1.3f, 0.15f)
            .OnComplete(()
                => drawGemCount.transform.DOScale(1, 0.15f));
        RefreshText();
        Save();
    }

    void RefreshText()
    {
        drawGemCount.text = gems.ToString();
    }

    void Save()
    {
        PlayerPrefs.SetInt(GEM_KEY_SAVE, gems);
    }
}