using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum AniPlayMode { PlayOnce, PlayCycle};

public class GIF : MonoBehaviour {
    
    Image player;
    int id = 0;
    
    [System.Serializable]
     
     public class PlayerData
    {
        public string folder = "";
        public Sprite[] textures;
        public AniPlayMode mode;
        public float framesPerSecond = 10.0f;
        
        public float waitTime = 0.0f;
    }
    public PlayerData[] playerRef ;

    void Start()
    {
        foreach (var movie in playerRef)
        {
            movie.textures = Resources.LoadAll<Sprite>(movie.folder);   //讀取Resources資料夾下變數folder資料夾中的所有sprite
        }       
        player = GetComponent<Image>();
        StartCoroutine(PlayOnce(playerRef[id]));   
    }

    private IEnumerator PlayOnce(PlayerData playerRef)
    {
        
        int index = 0;      //1秒鐘幾幀
        
        

        while(index < playerRef.textures.Length)
         {
            yield return new WaitForSeconds(1/ playerRef.framesPerSecond);
            index += 1;
            if (index == playerRef.textures.Length)
            {
                index = 0;
                id += 1;
                NextGIF(id);
                break;
            }
            player.sprite = playerRef.textures[index];

            
          }
         
       

        
    }
    private IEnumerator PlayCycle(PlayerData playerRef)
    {
        int index = 0;      //1秒鐘幾幀
        
        while (index < playerRef.textures.Length)
        {
            yield return new WaitForSeconds(1 / playerRef.framesPerSecond);
            index += 1;
            if (index == playerRef.textures.Length)
            {
                index = 0;                
            }
            player.sprite = playerRef.textures[index];
            
        }
    }
    
    private void NextGIF(int id)
    {
        switch (playerRef[id].mode)                             //判斷皆須放在Update
        {
            case AniPlayMode.PlayOnce:
                StartCoroutine(PlayOnce(playerRef[id]));
                break;
            case AniPlayMode.PlayCycle:
                StartCoroutine(PlayCycle(playerRef[id]));
                break;
            default:
                break;
        }
    }
}
   

