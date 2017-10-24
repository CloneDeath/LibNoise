using System;
using System.IO;
using LibNoise.Renderer;

namespace LibNoise.Writer
{
    /// <summary>
    ///     Heightmap writer class, raw format.
    /// </summary>
    public class Heightmap8RawWriter : AbstractWriter
	{
		/// <summary>
	    ///     The heightmap to write
	    /// </summary>
	    protected Heightmap8 _heightmap;

		/// <summary>
	    ///     Gets or sets the heightmap to write
	    /// </summary>
	    public Heightmap8 Heightmap
		{
			get => _heightmap;
			set => _heightmap = value;
		}

		/// <summary>
	    ///     Writes the contents of the heightmap into the file.
	    ///     @throw IOException An I/O exception occurred.
	    ///     Possibly the file could not be written.
	    /// </summary>
	    /// <param name="heightmap"></param>
	    public override void WriteFile()
		{
			if (_heightmap == null)
				throw new ArgumentException("An heightmap must be provided");

			OpenFile();

			try
			{
				// ... Raw format ...
				_writer.Write(_heightmap.Share());
			}
			catch (Exception e)
			{
				throw new IOException("Unknown IO exception", e);
			}

			CloseFile();
		}
	}
}