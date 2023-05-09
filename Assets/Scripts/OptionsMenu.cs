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

    private void Awake()
    {
        try
        {
            // Cargar el valor de la preferencia "joystickPreference"
            int joystickPref = PlayerPrefs.GetInt("joystickPreference", 0);

            // Actualizar el estado del toggle según el valor de la preferencia
            JoystickToggle.isOn = joystickPref == 1;
        }
        catch
        {

        }
    }


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
