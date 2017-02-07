using UnityEngine;
using System.Collections;

public class cursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void Update()
    {
        Cursor.visible = false;
        //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    
}
