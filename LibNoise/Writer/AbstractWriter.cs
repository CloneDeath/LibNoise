using System;
using System.IO;

namespace LibNoise.Writer
{
    /// <summary>
    ///     Abstract base class for all writer classes
    /// </summary>
    public abstract class AbstractWriter
	{
		#region Accessors

	    /// <summary>
	    ///     Gets or sets the name of the file to write.
	    /// </summary>
	    public string Filename
		{
			get => _filename;
			set => _filename = value;
		}

		#endregion

		#region Interaction

	    /// <summary>
	    ///     Writes the destination content
	    /// </summary>
	    public abstract void WriteFile();

		#endregion

		#region Fields

	    /// <summary>
	    ///     the name of the file to write.
	    /// </summary>
	    protected string _filename;

	    /// <summary>
	    ///     A binary writer
	    /// </summary>
	    protected BinaryWriter _writer;

		#endregion

		#region internal

	    /// <summary>
	    ///     Create a new BinaryWriter
	    /// </summary>
	    protected void OpenFile()
		{
			if (_writer != null)
				return; // Should throw exception ?

			if (File.Exists(_filename))
				try
				{
					File.Delete(_filename);
				}
				catch (Exception e)
				{
					throw new IOException("Unable to delete destination file", e);
				}

			BufferedStream stream;

			try
			{
				stream = new BufferedStream(new FileStream(_filename, FileMode.Create));
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

		#endregion
	}
}