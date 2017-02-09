using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Collections.Generic;
using System.IO;

public class reciprocal : MonoBehaviour {
    private string reset;
    public static int resetTime;
    public exist socket;
    Image uiElement;
    bool playfinish;
    
    public static bool coolingDown = true;
    public float waitTime = 30.0f;


    void Awake()
    {
       
        //xmlpath
        string filepath = Application.dataPath + "/StreamingAssets/setting.xml";
        XmlDocument xmlDoc = new XmlDocument();
        if (File.Exists(filepath))
        {
            Debug.Log("exist");
            xmlDoc.Load(filepath);
            XmlNodeList reset = xmlDoc.GetElementsByTagName("reset");
            XmlNodeList resetTime = xmlDoc.GetElementsByTagName("resetTime");

            foreach (XmlNode resetInfo in reset)
            {
                if (resetInfo.InnerText == "true")
                {
                    coolingDown = true;
                }
                else
                    coolingDown = false;

            }
            //boundrate
            foreach (XmlNode resetTimeInfo in resetTime)
            {
                Debug.Log("INFOREACH");
                waitTime = int.Parse(resetTimeInfo.InnerText);
                Debug.Log(waitTime);
            }
        }
        else
        {
            Debug.Log("file not find");
        }
    }

    void Start()
    {
        uiElement = this.GetComponent<Image>();
        
        playfinish = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            //更新UI型變動畫
            uiElement.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            if(uiElement.fillAmount == 0 && playfinish == true)
            {
                socket.go_title();
                playfinish = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                uiElement.fillAmount = 1.0f;
                playfinish = true;
            }
        }
    }
   
}
