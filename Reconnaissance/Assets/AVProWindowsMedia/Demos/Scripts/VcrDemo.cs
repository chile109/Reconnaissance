#if UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_5
#define UNITY_FEATURE_UGUI
#endif

using UnityEngine;
#if UNITY_FEATURE_UGUI
using UnityEngine.UI;
using System.Collections;
using RenderHeads.Media.AVProVideo;

//-----------------------------------------------------------------------------
// Copyright 2015-2016 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProWindowsMedia.Demos
{
	public class VcrDemo : MonoBehaviour 
	{
		public AVProWindowsMediaMovie _movie;

		public Slider		_videoSeekSlider;
		private float		_setVideoSeekSliderValue;
		private bool		_wasPlayingOnScrub;


		public void OnPlayButton()
		{
			if( _movie )
			{
				_movie.Play();
				
			}
		}
		public void OnPauseButton()
		{
			if( _movie )
			{
				_movie.Pause();
				
			}
		}

		public void OnVideoSeekSlider()
		{
			if (_movie && _videoSeekSlider && _videoSeekSlider.value != _setVideoSeekSliderValue)
			{
				_movie.MovieInstance.PositionSeconds = (_videoSeekSlider.value * _movie.MovieInstance.DurationSeconds);
			}
		}
		public void OnVideoSliderDown()
		{
			if( _movie )
			{
				_wasPlayingOnScrub = _movie.MovieInstance.IsPlaying;
				if( _wasPlayingOnScrub )
				{
					_movie.Pause();
					
				}
				OnVideoSeekSlider();
			}
		}
		public void OnVideoSliderUp()
		{
			if( _movie && _wasPlayingOnScrub )
			{
				_movie.Play();
				_wasPlayingOnScrub = false;

				
			}			
		}

		

		public void OnRewindButton()
		{
			if( _movie )
			{
				_movie.MovieInstance.Rewind();
			}
		}

        public void OnAudioVolumeMute()
        {

            _movie._volume = 0.0f;

        }
        public void OnAudioOn()
        {

            _movie._volume = 1.0f;

        }


        void Update()
		{
			if (_movie && _movie.MovieInstance != null)
			{
				float time = _movie.MovieInstance.PositionSeconds;
				float d = time / _movie.MovieInstance.DurationSeconds;
				_setVideoSeekSliderValue = d;
				_videoSeekSlider.value = d;
			}
		}

		
	}
}
#endif