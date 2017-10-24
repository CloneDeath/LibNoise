using System;
using System.IO;
using LibNoise.Renderer;

namespace LibNoise.Writer
{
    /// <summary>
    ///     Heightmap writer class, raw format.
    /// </summary>
    public class Heightmap32RawWriter : AbstractWriter
	{
		#region Fields

	    /// <summary>
	    ///     The heightmap to write
	    /// </summary>
	    protected Heightmap32 _heightmap;

		#endregion

		#region Accessors

	    /// <summary>
	    ///     Gets or sets the heightmap to write
	    /// </summary>
	    public Heightmap32 Heightmap
		{
			get => _heightmap;
			set => _heightmap = value;
		}

		#endregion

		#region Interaction

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

			var buffer = _heightmap.Share();

			try
			{
				// ... Raw format ...
				for (var i = 0; i < buffer.Length; i++)
					_writer.Write(buffer[i]);
			}
			catch (Exception e)
			{
				throw new IOException("Unknown IO exception", e);
			}

			CloseFile();
		}

		#endregion

		#region Ctor/Dtor

		#endregion
	}
}