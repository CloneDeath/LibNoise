namespace LibNoise.Transformer
{
    using System;

    /// <summary>
    /// Noise module that rotates the input value around the origin before
    /// returning the output value from a source module.
    ///
    /// The GetValue() method rotates the coordinates of the input value
    /// around the origin before returning the output value from the source
    /// module.  To set the rotation angles, call the SetAngles() method.  To
    /// set the rotation angle around the individual x, y, or z axes,
    /// set the XAngle, YAngle or ZAngle properties,
    /// respectively.
    ///
    /// The coordinate system of the input value is assumed to be
    /// "left-handed" (x increases to the right, y increases upward,
    /// and z increases inward.)
    /// </summary>
    public class RotatePoint : TransformerModule, IModule3D
    {
        #region Connstant

        /// <summary>
        /// Default x rotation angle for the RotatePoint noise module.
        /// </summary>
        public const float DefaultRotateX = 0.0f;

        /// <summary>
        /// Default y rotation angle for the RotatePoint noise module.
        /// </summary>
        public const float DefaultRotateY = 0.0f;

        /// <summary>
        /// Default z rotation angle for the RotatePoint noise module.
        /// </summary>
        public const float DefaultRotateZ = 0.0f;

        #endregion

        #region Fields

        /// <summary>
        /// The source input module
        /// </summary>
        private IModule _sourceModule;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _x1Matrix;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _x2Matrix;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _x3Matrix;

        /// <summary>
        /// x rotation angle applied to the input value, in degrees.
        /// </summary>
        private float _xAngle;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _y1Matrix;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _y2Matrix;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _y3Matrix;

        /// <summary>
        /// y rotation angle applied to the input value, in degrees.
        /// </summary>
        private float _yAngle;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _z1Matrix;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _z2Matrix;

        /// <summary>
        /// An entry within the 3x3 rotation matrix used for rotating the
        /// input value.
        /// </summary>
        private float _z3Matrix;

        /// <summary>
        /// z rotation angle applied to the input value, in degrees.
        /// </summary>
        private float _zAngle;

        #endregion

        #region Accessors

        /// <summary>
        /// Gets or sets the source module
        /// </summary>
        public IModule SourceModule
        {
            get { return _sourceModule; }
            set { _sourceModule = value; }
        }

        /// <summary>
        /// Gets or sets the x rotation angle applied to the input value, in degrees.
        /// </summary>
        public float XAngle
        {
            get { return _xAngle; }
            set { SetAngles(value, _yAngle, _zAngle); }
        }

        /// <summary>
        /// Gets or sets the y rotation angle applied to the input value, in degrees.
        /// </summary>
        public float YAngle
        {
            get { return _yAngle; }
            set { SetAngles(_xAngle, value, _zAngle); }
        }

        /// <summary>
        /// Gets or sets the z rotation angle applied to the input value, in degrees.
        /// </summary>
        public float ZAngle
        {
            get { return _zAngle; }
            set { SetAngles(_xAngle, _yAngle, value); }
        }

        #endregion

        #region Ctor/Dtor

        /// <summary>
        /// Create a new noise module with default values
        /// </summary>
        public RotatePoint()
        {
        }

        /// <summary>
        /// Create a new noise module with given values
        /// </summary>
        /// <param name="source">the source module</param>
        public RotatePoint(IModule source)
        {
            _sourceModule = source;
        }

        /// <summary>
        /// Create a new noise module with the given values
        /// </summary>
        /// <param name="source">The input source module</param>
        /// <param name="xAngle">the x rotation angle applied to the input value, in degrees.</param>
        /// <param name="yAngle">the y rotation angle applied to the input value, in degrees.</param>
        /// <param name="zAngle">the z rotation angle applied to the input value, in degrees.</param>
        public RotatePoint(IModule source, float xAngle, float yAngle, float zAngle)
        {
            _sourceModule = source;
            SetAngles(xAngle, yAngle, zAngle);
        }

        #endregion

        #region Interaction

        /// <summary>
        /// Sets the rotation angles around all three axes to apply to the
        /// input value.
        /// </summary>
        /// <param name="xAngle">the x rotation angle applied to the input value, in degrees.</param>
        /// <param name="yAngle">the y rotation angle applied to the input value, in degrees.</param>
        /// <param name="zAngle">the z rotation angle applied to the input value, in degrees.</param>
        public void SetAngles(float xAngle, float yAngle, float zAngle)
        {
            float xCos, yCos, zCos, xSin, ySin, zSin;

            xCos = (float) Math.Cos(xAngle*Libnoise.Deg2Rad);
            yCos = (float) Math.Cos(yAngle*Libnoise.Deg2Rad);
            zCos = (float) Math.Cos(zAngle*Libnoise.Deg2Rad);
            xSin = (float) Math.Sin(xAngle*Libnoise.Deg2Rad);
            ySin = (float) Math.Sin(yAngle*Libnoise.Deg2Rad);
            zSin = (float) Math.Sin(zAngle*Libnoise.Deg2Rad);

            _x1Matrix = ySin*xSin*zSin + yCos*zCos;
            _y1Matrix = xCos*zSin;
            _z1Matrix = ySin*zCos - yCos*xSin*zSin;

            _x2Matrix = ySin*xSin*zCos - yCos*zSin;
            _y2Matrix = xCos*zCos;
            _z2Matrix = -yCos*xSin*zCos - ySin*zSin;

            _x3Matrix = -ySin*xCos;
            _y3Matrix = xSin;
            _z3Matrix = yCos*xCos;

            _xAngle = xAngle;
            _yAngle = yAngle;
            _zAngle = zAngle;
        }

        #endregion

        #region IModule3D Members

        /// <summary>
        /// Generates an output value given the coordinates of the specified input value.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public float GetValue(float x, float y, float z)
        {
            float nx = (_x1Matrix*x) + (_y1Matrix*y) + (_z1Matrix*z);
            float ny = (_x2Matrix*x) + (_y2Matrix*y) + (_z2Matrix*z);
            float nz = (_x3Matrix*x) + (_y3Matrix*y) + (_z3Matrix*z);

            return ((IModule3D) _sourceModule).GetValue(nx, ny, nz);
        }

        #endregion
    }
}
