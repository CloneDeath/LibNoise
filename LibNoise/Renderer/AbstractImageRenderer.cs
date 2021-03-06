﻿namespace LibNoise.Renderer
{
    /// <summary>
    ///     Abstract base class for an image renderer
    /// </summary>
    public abstract class AbstractImageRenderer : AbstractRenderer
	{
		/// <summary>
	    ///     The destination image
	    /// </summary>
	    protected IMap2D<IColor> _image;

		/// <summary>
	    ///     Gets or sets the destination image
	    /// </summary>
	    public IMap2D<IColor> Image
		{
			get => _image;
			set => _image = value;
		}
	}
}