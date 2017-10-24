namespace LibNoise.Renderer
{
    /// <summary>
    /// class for an 16bit-heightmap renderer
    /// </summary>
    public class Heightmap16Renderer : AbstractHeightmapRenderer
    {
        #region Fields

        /// <summary>
        /// The destination heightmap
        /// </summary>
        protected Heightmap16 _heightmap;

        #endregion

        #region Accessors

        /// <summary>
        /// Gets or sets the destination heightmap
        /// </summary>
        public Heightmap16 Heightmap
        {
            get { return _heightmap; }
            set { _heightmap = value; }
        }

        #endregion

        #region Ctor/Dtor

        #endregion

        #region internal

        /// <summary>
        /// Sets the new size for the target heightmap.
        /// 
        /// </summary>
        /// <param name="width">width The new width for the heightmap</param>
        /// <param name="height">height The new height for the heightmap</param>
        protected override void SetHeightmapSize(int width, int height)
        {
            _heightmap.SetSize(width, height);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool CheckHeightmap()
        {
            return _heightmap != null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="source"></param>
        /// <param name="boundDiff"></param>
        protected override void RenderHeight(int x, int y, float source, float boundDiff)
        {
            ushort elevation;

            if (source <= _lowerHeightBound)
                elevation = ushort.MinValue;
            else if (source >= _upperHeightBound)
                elevation = ushort.MaxValue;
            else
                elevation = (ushort) (((source - _lowerHeightBound)/boundDiff)*65535.0f);

            _heightmap.SetValue(x, y, elevation);
        }

        #endregion
    }
}
