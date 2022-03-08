using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField]
    Text messageLable;

    // Start is called before the first frame update
    void Start()
    {
        messageLable.text = MasterText.GetMasterText("dialog_language");    
    }
}
