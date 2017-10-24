namespace LibNoise.Renderer
{
    /// A delegate to a callback function used by the Renderer classes.
    ///
    /// The renderer method calls this callback function each
    /// time it fills a row of the target struct.
    ///
    /// This callback function has a single integer parameter that contains
    /// a count of the rows that have been completed.  It returns void.
    public delegate void RendererCallback(int row);

    /// <summary>
    /// Abstract base class for a renderer
    /// </summary>
    public abstract class AbstractRenderer
    {
        #region Fields

        /// <summary>
        /// The callback function that Render() calls each time it fills a
        /// row of the image.
        /// </summary>
        protected RendererCallback _callBack;

        /// <summary>
        /// The source noise map that contains the coherent-noise values.
        /// </summary>
        protected IMap2D<float> _noiseMap;

        #endregion

        #region Accessors

        /// <summary>
        /// Gets or sets the source noise map
        /// </summary>
        public IMap2D<float> NoiseMap
        {
            get { return _noiseMap; }
            set { _noiseMap = value; }
        }

        /// <summary>
        /// Gets or sets the callback function
        /// </summary>
        public RendererCallback CallBack
        {
            get { return _callBack; }
            set { _callBack = value; }
        }

        #endregion

        #region Interaction

        /// <summary>
        /// Renders the destination image using the contents of the source
        /// noise map.
        ///
        /// @pre NoiseMap has been defined.
        /// @pre Image has been defined.
        ///
        /// @post The original contents of the destination image is destroyed.
        ///
        /// @throw ArgumentException See the preconditions.
        /// </summary>
        public abstract void Render();

        #endregion
    }
}
