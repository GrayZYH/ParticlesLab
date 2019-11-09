using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RenderHeads.Media.AVProVideo;

#if NETFX_CORE
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using System.Threading.Tasks;
#endif

//-----------------------------------------------------------------------------
// Copyright 2015-2017 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProVideo.Demos
{
	/// <summary>
	/// Demonstration of how to use StorageFiles with AVProVideo in UWP builds
	/// The code is put behind NETFX_CORE macros as it is only valid in UWP
	/// This example loads a video picked by the user after clicking the Open Video File button
	/// </summary>
	public class NativeMediaOpen : MonoBehaviour
	{
		public MediaPlayer player;
#if NETFX_CORE
		private IRandomAccessStreamWithContentType _ras;
		private FileOpenPicker _picker;
		private string _pickedFileName;
		private StorageFile file = null;
#endif
		// Use this for initialization
		void Start()
		{
#if NETFX_CORE
			_picker = new FileOpenPicker();
			_picker.ViewMode = PickerViewMode.Thumbnail;
			_picker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
			_picker.FileTypeFilter.Add("*");
#endif
		}

#if NETFX_CORE
		void LoadFile()
		{
			//loads file on UI thread (note you can also use PickSingleFileAndContinue for better compatibility)
			UnityEngine.WSA.Application.InvokeOnUIThread(async () => file = await _picker.PickSingleFileAsync(), true);
		}

		private async Task<bool> ReadFile()
		{
			//reads file into RandomAccessStream
			_ras = await file.OpenReadAsync();
			_pickedFileName = file.Name;

			return true;
		}

		private async void LoadFileFromCameraRoll()
		{
			StorageFolder f = KnownFolders.CameraRoll;
			file = await f.GetFileAsync("myvideo.mp4");
		}
#endif

		void OnGUI()
		{
			if (GUILayout.Button("Open Video File"))
			{
#if NETFX_CORE
				LoadFile();
#endif
			}

			if(player != null)
			{
				GUILayout.Label("Currently Playing: " + player.m_VideoPath);
			}
			
		}

		private void Update()
		{
#if NETFX_CORE
			// if file has been loaded, read it into randomaccessstream and send to AVProVideo
			if(file != null)
			{
				// loading file, and then waiting for the async task to complete so we know the RandomAccessStream is valid and can be sent to AVProVideo
				var tsk = ReadFile();

				while (!tsk.IsCompleted) ;
				file = null;
				// Sends random access stream to AVProVideo. The URL sent is for metadata purposes
				player.OpenVideoFromStream(_ras, _pickedFileName, true);
			}
#endif
		}
	}
}

