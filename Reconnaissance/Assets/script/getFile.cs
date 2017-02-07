using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class getFile : MonoBehaviour {
    public static string fileName;

    public void loadfile()
    {
        //存取按鈕名字供scene_controller跳轉
        fileName = this.gameObject.name;
        print(fileName);   //Text為組件,text為內容  不用ToString();       
    }
}
