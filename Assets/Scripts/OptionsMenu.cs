using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer music;
    [SerializeField] private AudioMixer fx;

    public Toggle JoystickToggle;

    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider musicSlider;

    private const string fxKey = "FXVolume";
    private const string musicKey = "MusicVolume";


    public void Start()
    {
        try
        {
            // Recuperar valores de volumen al iniciar el juego
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                float musicVolume = PlayerPrefs.GetFloat("MusicVolume");
                music.SetFloat("Music", musicVolume);
            }

            if (PlayerPrefs.HasKey("FXVolume"))
            {
                float fxVolume = PlayerPrefs.GetFloat("FXVolume");
                fx.SetFloat("FX", fxVolume);
            }

            fxSlider.value = getVolumenFX();
            musicSlider.value = getVolumenMusic();

            // Cargar el valor de la preferencia "joystickPreference"
            int joystickPref = PlayerPrefs.GetInt("joystickPreference", 0);

            // Actualizar el estado del toggle según el valor de la preferencia
            JoystickToggle.isOn = joystickPref == 1;
        }
        catch
        {

        }
    }

    public void setVolumenFX(float volumen)
    {
        fx.SetFloat("FX", volumen);
        PlayerPrefs.SetFloat("FXVolume", volumen);
    }

    public void setVolumenMusic(float volumen)
    {
        music.SetFloat("Music", volumen);
        PlayerPrefs.SetFloat("MusicVolume", volumen);
    }

    public float getVolumenFX()
    {
        float fxVolume = 0f;
        fx.GetFloat("FX", out fxVolume);
        return fxVolume;
    }

    public float getVolumenMusic()
    {
        float musicVolume = 0f;
        music.GetFloat("Music", out musicVolume);
        return musicVolume;
    }


    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("playerName", playerName);
    }

    public string LoadPlayerName()
    {
        return PlayerPrefs.GetString("playerName", "Ingresa nombre");
    }

    public void SetJoystickPreference()
    {
        Toggle myToggle = GameObject.Find("JoystickToggle").GetComponent<Toggle>();
        int option = myToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("joystickPreference", option);
    }

    public int LoadJoystickPreference()
    {
        return PlayerPrefs.GetInt("joystickPreference", 0);
    }
}
