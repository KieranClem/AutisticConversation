using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustDialogBoxSize : MonoBehaviour
{
    public float NormalTextBoxWidth = 1500;
    public float NormatTextBoxHeight = 335;
    public float SmallTextBoxWidth;
    public float SmallTextBoxHeight;

    public void MakeTextBoxNormal(RectTransform textBox)
    {
        textBox.sizeDelta = new Vector2(NormalTextBoxWidth, NormatTextBoxHeight);
    }

    public void MakeTextBoxSmall(RectTransform textBox)
    {
        textBox.sizeDelta = new Vector2(SmallTextBoxWidth, SmallTextBoxHeight);
    }
}
