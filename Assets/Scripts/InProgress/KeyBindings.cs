using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindings : MonoBehaviour
{
    public Dictionary<string, KeyCode> inputs = new Dictionary<string, KeyCode>();          //Creating dictionary of the input keys
    public Text Jump, Attack, Interact, Dash, Forward, Backwards;                           //The texts shown inside the button (the key binding is shown)
    [SerializeField] GameObject ChangeInstructionPannel;                                    //The instruction pannel
    GameObject currentKey;                                                                  //The button which is selected to change the keybind off

    // Start is called before the first frame update
    void Start()
    {
        //Adding the lable in the dictionary and its meaaning(what it will return)
        inputs.Add("Jump",(KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Z")));
        inputs.Add("Attack",(KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "X")));
        inputs.Add("Interact",(KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "A")));
        inputs.Add("Dash", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Dash", "C")));
        inputs.Add("Forward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "RightArrow")));
        inputs.Add("Backward", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "LeftArrow")));

        //Setting the text in the buttons to show the key name of that keybinding
        Jump.text = inputs["Jump"].ToString();
        Attack.text = inputs["Attack"].ToString();
        Interact.text = inputs["Interact"].ToString();
        Dash.text = inputs["Dash"].ToString();
        Forward.text = inputs["Forward"].ToString();
        Backwards.text = inputs["Backward"].ToString();
    }

    private void OnGUI()
    {
        //Checking if the button is click if so then changing the keybinding
        if (currentKey != null)
        {
            Event e = Event.current;
            if(e.isKey)
            {
                inputs[currentKey.name] = e.keyCode;
                currentKey.GetComponentInChildren<Text>().text = e.keyCode.ToString();
                ChangeInstructionPannel.SetActive(false);
                currentKey = null;
            }
        }
    }


    //Function that will set the current key to have a value to cahnge too and shows instruction pannal
    public void clickedToChange(GameObject thisIsClicked)
    {
        currentKey = thisIsClicked;
        ChangeInstructionPannel.SetActive(true);
    }

    //Function to save the changes in keyBindings
    public void SaveKeyBindings()
    {
        //looping through the current set keybindings and aving playerPrefs
        foreach(var key in inputs)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
