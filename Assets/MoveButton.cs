using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public InputField console;

    public string moveKey;
    public Button moveButton;
    public GameObject moveObject;

    private void Start()
    {
        UpdateButton();
        moveButton.onClick.AddListener(ToggleActive);
    }

    private void Update()
    {
        if (!console.isFocused)
        {
            if (Input.GetKeyDown(moveKey))
                ToggleActive();
        }
    }

    public void Init()
    {
        UpdateButton();
        moveButton.onClick.AddListener(ToggleActive);
    }

    public void ToggleActive()
    {
        moveObject.SetActive(!moveObject.activeSelf);
        UpdateButton();
    }
    private void UpdateButton()
    {
        moveButton.image.color = moveObject.activeSelf ? Color.gray : Color.white;
    }
}
