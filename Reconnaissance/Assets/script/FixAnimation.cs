using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FixAnimation : MonoBehaviour {
    Image uiElement;
    // Use this for initialization
    void Start () {
        uiElement = this.GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {

        //Bug:傳統影片不支援遮罩UI變化，須加上此段程式碼
        uiElement.enabled = false;
        uiElement.enabled = true;

    }
}
