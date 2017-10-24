using System;
using System.IO;

namespace LibNoise.Writer
{
    /// <summary>
    ///     Abstract base class for all writer classes
    /// </summary>
    public abstract class AbstractWriter
	{
		/// <summary>
		///     the name of the file to write.
		/// </summary>
	    public string Filename { get; set; }

		/// <summary>
	    ///     Writes the destination content
	    /// </summary>
	    public abstract void WriteFile();

		/// <summary>
	    ///     A binary writer
	    /// </summary>
	    protected BinaryWriter _writer;

		/// <summary>
	    ///     Create a new BinaryWriter
	    /// </summary>
	    protected void OpenFile()
		{
			if (_writer != null)
				return; // Should throw exception ?

			if (File.Exists(Filename))
				File.Delete(Filename);

			BufferedStream stream;

			try
			{
				stream = new BufferedStream(new FileStream(Filename, FileMode.Create));
			}
			catch (Exception e)
			{
				throw new IOException("Unable to create destination file", e);
			}

			_writer = new BinaryWriter(stream);
		}


	    /// <summary>
	    ///     Release a BinaryWriter previously opened
	    /// </summary>
	    protected void CloseFile()
		{
			try
			{
				_writer.Flush();
				_writer.Close();
				_writer = null;
			}
			catch (Exception e)
			{
				throw new IOException("Unable to release stream", e);
			}
		}
	}
}