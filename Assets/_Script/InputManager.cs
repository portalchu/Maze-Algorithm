using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputField_Width;
    public TMP_InputField inputField_Vertical;
    public TMP_InputField text;

    // Start is called before the first frame update
    void Start()
    {
        inputField_Width = GameObject.Find("Width").GetComponent<TMP_InputField>();
        inputField_Vertical = GameObject.Find("Vertical").GetComponent<TMP_InputField>();

        if (inputField_Width == null) Debug.Log("no Width");
        if (inputField_Vertical == null) Debug.Log("no Vertical");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
