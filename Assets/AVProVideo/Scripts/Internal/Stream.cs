//-----------------------------------------------------------------------------
// Copyright 2015-2017 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RenderHeads.Media.AVProVideo
{
	public abstract class Stream
	{
		public struct Chunk
		{
			public string name;
		}

		public abstract int Width { get; }

		public abstract int Height { get; }

		public abstract int Bandwidth { get; }

		public abstract string URL { get; }

		public abstract List<Chunk> GetAllChunks();

		public abstract List<Chunk> GetChunks();

		public abstract List<Stream> GetAllStreams();

		public abstract List<Stream> GetStreams();
	}
}
	
