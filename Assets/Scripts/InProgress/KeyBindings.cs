using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindings : MonoBehaviour
{
    public Dictionary<string, KeyCode> inputs = new Dictionary<string, KeyCode>();
    public Text Jump, Attack, Interact, Dash, Forward, Backwards;
    [SerializeField] GameObject ChangeInstructionPannel;
    GameObject currentKey;

    // Start is called before the first frame update
    void Start()
    {
        inputs.Add("Jump", KeyCode.Z);
        inputs.Add("Attack", KeyCode.X);
        inputs.Add("Interact", KeyCode.A);
        inputs.Add("Dash", KeyCode.C);
        inputs.Add("Forward", KeyCode.RightArrow);
        inputs.Add("Backward", KeyCode.LeftArrow);

        Jump.text = inputs["Jump"].ToString();
        Attack.text = inputs["Attack"].ToString();
        Interact.text = inputs["Interact"].ToString();
        Dash.text = inputs["Dash"].ToString();
        Forward.text = inputs["Forward"].ToString();
        Backwards.text = inputs["Backward"].ToString();
    }

    private void OnGUI()
    {
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

    public void clickedToChange(GameObject thisIsClicked)
    {
        currentKey = thisIsClicked;
        ChangeInstructionPannel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
