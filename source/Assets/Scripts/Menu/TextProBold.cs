using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextProBold : MonoBehaviour
{
    private TMP_Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setUnderline(bool underline)
    {
        if (underline)
            textComponent.fontStyle = FontStyles.Bold | FontStyles.Underline;
        else
            textComponent.fontStyle = FontStyles.Bold;
    }
}
