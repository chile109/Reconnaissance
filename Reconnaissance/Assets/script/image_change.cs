using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;


public class image_change : MonoBehaviour {
    public List<Sprite> contentSprite = new List<Sprite>();
    private Image img;
    // Use this for initialization
    void Start () {
        
        img = this.GetComponent<Image>();

        Sprite[] tex = Resources.LoadAll<Sprite>(getFile.fileName);

        
        foreach (Sprite spr in tex)
        {
            contentSprite.Add(spr);
        }

        // Unload them - no longer needed
        /*foreach (Sprite spr in tex)
        {
            Resources.UnloadAsset(spr);
        }*/
    }


    void Update()
    {
        tramsform_img(socket_console.rader_index);
    }
    public void tramsform_img(int i)
    {
        img.sprite = contentSprite[i]; 
    }
}
