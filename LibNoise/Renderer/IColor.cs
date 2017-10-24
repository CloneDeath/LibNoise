namespace LibNoise.Renderer
{
    /// <summary>
    /// Interface for a portable color structure.
    /// </summary>
    public interface IColor
    {
        /// <summary>
        /// The alpha channel
        /// </summary>
        byte Alpha { get; set; }

        /// <summary>
        /// The blue channel
        /// </summary>
        byte Blue { get; set; }

        /// <summary>
        /// The green channel
        /// </summary>
        byte Green { get; set; }

        /// <summary>
        /// The red channel
        /// </summary>
        byte Red { get; set; }
    }
}
