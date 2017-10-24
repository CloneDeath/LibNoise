namespace LibNoise.Renderer
{
    /// <summary>
    ///     Well-known colors.
    /// </summary>
    public static class Colors
	{
	    /// <summary>
	    ///     Create a black color.
	    /// </summary>
	    public static Color Black => new Color(0, 0, 0, 255);

	    /// <summary>
	    ///     Create a white color.
	    /// </summary>
	    public static Color White => new Color(255, 255, 255, 255);

	    /// <summary>
	    ///     Create a solid red color.
	    /// </summary>
	    public static Color Red => new Color(255, 0, 0, 255);

	    /// <summary>
	    ///     Create a solid green color.
	    /// </summary>
	    public static Color Green => new Color(0, 255, 0, 255);

	    /// <summary>
	    ///     Create a solid blue color.
	    /// </summary>
	    public static Color Blue => new Color(0, 0, 255, 255);

	    /// <summary>
	    ///     Create a transparent color.
	    /// </summary>
	    public static Color Transparent => new Color(0, 0, 0, 0);
	}
}