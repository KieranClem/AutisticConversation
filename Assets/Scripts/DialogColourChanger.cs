using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogColourChanger : MonoBehaviour
{
    public Image dialogBox;
    private Color defaultColour;
    public Color PlayerTextBoxColour;
    public Color ConversationPartnerTextBoxColour;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultColour = dialogBox.color;
    }

    public void ChangeColourToDefault()
    {
        dialogBox.color = defaultColour;
    }

    public void ChangeColourToPlayer()
    {
        dialogBox.color = PlayerTextBoxColour;
    }

    public void ChangeColourToConversationPartner()
    {
        dialogBox.color = ConversationPartnerTextBoxColour;
    }
}
