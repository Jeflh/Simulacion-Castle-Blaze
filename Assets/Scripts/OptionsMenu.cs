using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer music;
    [SerializeField] private AudioMixer fx;
    public void setVolumenMusic(float volumen)
    {
        music.SetFloat("Music", volumen);
    }

    public void setVolumenFX(float volumen)
    {
        fx.SetFloat("FX", volumen);
    }

    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("playerName", playerName);
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString("playerName", "Ingresa nombre");
    }
}
