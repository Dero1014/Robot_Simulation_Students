using System;
using UnityEngine;
using UnityEngine.UI;

public class ToolToggles : MonoBehaviour
{
    public InputField console;

    [Space(10)]
    public ScreenButton inspector;
    public ScreenButton spawn;
    public ScreenButton moveTool;

    private ScreenButton[] buttons;

    private void Start()
    {
        buttons = new[] { inspector, spawn, moveTool };
        foreach (ScreenButton button in buttons)
            button.Init();
    }

    private void Update()
    {
        if (!console.isFocused)
        {
            foreach (ScreenButton button in buttons)
            {
                if (Input.GetKeyDown(button.key))
                    button.ToggleActive();
            }
        }
    }


    [Serializable]
    public class ScreenButton
    {
        public string key;
        public Button button;
        public GameObject obj;

        public void Init()
        {
            UpdateButton();
            button.onClick.AddListener(ToggleActive);
        }

        public void ToggleActive()
        {
            obj.SetActive(!obj.activeSelf);
            UpdateButton();
        }
        private void UpdateButton()
        {
            button.image.color = obj.activeSelf ? Color.gray : Color.white;
        }
    }
}
