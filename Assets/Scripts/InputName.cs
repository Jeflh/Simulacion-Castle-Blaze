using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputName : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputPlayerName;
    [SerializeField] private DatabaseManager databaseManager;
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private GameObject successMessage;

    void Start()
    {
        inputPlayerName.text = PlayerPrefs.GetString("playerName", "Ingresa nombre");
    }

    public void InputEnter()
    {
        List<string> allNames = databaseManager.GetAllNames();
        string playerName = inputPlayerName.text;

        if (allNames.Contains(playerName))
        {
            errorMessage.SetActive(true);
            StartCoroutine(HideSuccessMessage(2f));
        }
        else
        {
            PlayerPrefs.SetString("playerName", playerName);
            successMessage.SetActive(true);
            // Desactivar el mensaje de éxito después de 2 segundos
            StartCoroutine(HideSuccessMessage(2f));
        }
    }

    private IEnumerator HideSuccessMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        successMessage.SetActive(false);
        errorMessage.SetActive(false);
    }
}
