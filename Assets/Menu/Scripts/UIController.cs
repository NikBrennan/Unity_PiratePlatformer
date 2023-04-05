using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public UIDocument mainDoc;
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    public VisualTreeAsset settingsButtonsTemplate;
    public VisualElement settingsButtons;
    public VisualElement buttonsBox;

    Resolution[] resolutions;
    Display[] displays;


    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        displays = Display.displays;

        var doc = GetComponent<UIDocument>().rootVisualElement;
        playButton = doc.Q<Button>("play-button");
        settingsButton = doc.Q<Button>("settings-button");
        quitButton = doc.Q<Button>("quit-button");
        buttonsBox = doc.Q<VisualElement>("Main");

        playButton.clicked += PlayButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        quitButton.clicked += QuitButtonPressed;

        settingsButtons = settingsButtonsTemplate.CloneTree();
        Button backButton = settingsButtons.Q<Button>("back-button");

        List<string> options1 = new List<string>();
        int currentRes = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            options1.Add(resolutions[i].width + " x " + resolutions[i].height);
            if(resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        List<string> options2 = new List<string>();
        for (int i = 0; i < displays.Length; i++)
        {
            options2.Add(displays[i].ToString());
        }


        var dropdown1 = new DropdownField("Resolution", options1, currentRes);
        dropdown1.RegisterValueChangedCallback(SetResolution);
        settingsButtons.Add(dropdown1);

        var dropdown2 = new DropdownField("Display Devices", options2, 0);
        dropdown2.RegisterValueChangedCallback(SetDisplay);
        settingsButtons.Add(dropdown2);

        backButton.clicked += BackButtonPressed;

        
    }

    void SetResolution(ChangeEvent<string> evt)
    {
        string newRes = evt.newValue;
        for(int i = 0; i < resolutions.Length; i++)
        {
            if(newRes == resolutions[i].width + " x " + resolutions[i].height)
            {
                Screen.SetResolution(resolutions[i].width, resolutions[i].height, Screen.fullScreen);
                break;
            }
        }
    }

    void SetDisplay(ChangeEvent<string> evt)
    {
        string newDisplay = evt.newValue;
        for (int i = 0; i < displays.Length; i++)
        {
            if (newDisplay == displays[i].ToString())
            {
                
                Display.displays[i].Activate();
                break;
            }
        }
    }

    void PlayButtonPressed()
    {
        //Load the game scene
        SceneManager.LoadScene("LevelOneScene");
    }

    void SettingsButtonPressed()
    {
        buttonsBox.Clear();
        buttonsBox.Add(settingsButtons);
    }

    void BackButtonPressed()
    {
        buttonsBox.Clear();
        buttonsBox.Add(playButton);
        buttonsBox.Add(settingsButton);
        buttonsBox.Add(quitButton);
    }

    void QuitButtonPressed()
    {
        //Exit the application
        Debug.Log("The application will now close...");
        Application.Quit();
    }


}
