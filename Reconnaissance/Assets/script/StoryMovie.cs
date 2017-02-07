#if UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_5
#define UNITY_FEATURE_UGUI
#endif

using UnityEngine;
#if UNITY_FEATURE_UGUI
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;
using RenderHeads.Media.AVProVideo;

//-----------------------------------------------------------------------------
// Copyright 2015-2016 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProWindowsMedia.Demos
{
	public class StoryMovie: MonoBehaviour 
	{
        public AVProWindowsMediaMovie _movie;
        public AVProWindowsMediaMovie _movieB;
        public string _folder;
        public List<string> _filenames;

        public GameObject movie;
       
        private AVProWindowsMediaMovie[] _movies;
        private int _moviePlayIndex;
        private int _movieLoadIndex;
        public  int _index = -1;
        private bool _loadSuccess = true;
        private int _playItemIndex = -1;

        public AVProWindowsMediaMovie PlayingMovie { get { return _movies[_moviePlayIndex]; } }
        public AVProWindowsMediaMovie LoadingMovie { get { return _movies[_movieLoadIndex]; } }
        
        IEnumerator NextMovie()
        {            
            
            if (_filenames.Count > 0)
            {
                _index += 1;
                if(_index >= _filenames.Count)
                {
                    _index = _filenames.Count - 1;
                }
            }
            

            print(_index);
            LoadingMovie._folder = _folder;
            LoadingMovie._filename = _filenames[_index];
            LoadingMovie._useStreamingAssetsPath = true;
            LoadingMovie._playOnStart = true;
            _loadSuccess = LoadingMovie.LoadMovie(true);
            if(_loadSuccess == true)
            {
                yield return 0;
            }
            _playItemIndex = _index;            
            _moviePlayIndex = (_moviePlayIndex + 1) % 2;
            _movieLoadIndex = (_movieLoadIndex + 1) % 2;
        }
        
        
        public void Next()
        {
            StartCoroutine(NextMovie());
                               
        }
        public void Previous()
        {
            
            if (_index > 0)
            {
                
                _index -= 2;

                StartCoroutine(NextMovie());
            }
            else
            {
               
                _movie.MovieInstance.Rewind();
                this.gameObject.SetActive(false);
                movie.SetActive(false);
                
            }
            
        }
        
        public void OnRewindButton()
		{
			if( _movie )
			{
				_movie.MovieInstance.Rewind();
			}
		}

        public void jump(int id)
        {
            _index = id;            
            StartCoroutine(NextMovie());
        }

        void Start()
		{
            DirectoryInfo dirInfo = new DirectoryInfo(Application.dataPath + "/StreamingAssets/" + _folder);

            foreach (var file in dirInfo.GetFiles("*.mp4"))
            {
                _filenames.Add(file.Name);
                print(Path.GetFileName(file.Name));
            }

            _movie._loop = true;
            _movieB._loop = true;
            _movies = new AVProWindowsMediaMovie[2];
            _movies[0] = _movie;
            _movies[1] = _movieB;
            _moviePlayIndex = 0;
            _movieLoadIndex = 1;
            _movie._volume = 1.0f;
            
            StartCoroutine(NextMovie());
        }		
	}
}
#endif