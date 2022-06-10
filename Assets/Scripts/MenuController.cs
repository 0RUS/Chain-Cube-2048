using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public TextAsset textJSON;
    public TextMeshProUGUI buttonText;
    [System.Serializable]
    public class Language
    {
        public string name;
        public string start;    
    }

    [System.Serializable]
    public class LanguageList
    {
        public Language[] languages;
    }

    public LanguageList languageList = new LanguageList();
    // Start is called before the first frame update
    void Start()
    {
        languageList = JsonUtility.FromJson<LanguageList>(textJSON.text);
        Debug.Log(languageList);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeLanguege(string language)
    {
        foreach (Language l in languageList.languages)
        {
            if (l.name == language)
            {
                buttonText.text = l.start;
                break;
            }
        }
    }
}
