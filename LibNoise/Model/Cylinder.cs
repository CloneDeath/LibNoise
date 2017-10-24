namespace LibNoise.Model
{
    using System;

    /// <summary>
    /// Model that defines the surface of a cylinder.
    ///
    /// This model returns an output value from a noise module given the
    /// coordinates of an input value located on the surface of a cylinder.
    ///
    /// To generate an output value, pass the (angle, height) coordinates of
    /// an input value to the GetValue() method.
    ///
    /// This model is useful for creating:
    /// - seamless textures that can be mapped onto a cylinder
    ///
    /// This cylinder has a radius of 1.0 unit and has infinite height.  It is
    /// oriented along the y axis.  Its center is located at the origin.
    /// </summary>
    public class Cylinder : AbstractModel
    {
        #region Ctor/Dtor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Cylinder()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="module">The noise module that is used to generate the output values.</param>
        public Cylinder(IModule3D module)
            : base(module)
        {
        }

        #endregion

        #region Interaction

        /// <summary>
        /// Returns the output value from the noise module given the
        /// (angle, height) coordinates of the specified input value located
        /// on the surface of the cylinder.
        ///
        /// This cylinder has a radius of 1.0 unit and has infinite height.
        /// It is oriented along the y axis.  Its center is located at the
        /// origin.
        /// </summary>
        /// <param name="angle">The angle around the cylinder's center, in degrees.</param>
        /// <param name="height">The height along the y axis.</param>
        /// <returns>The output value from the noise module.</returns>
        public float GetValue(float angle, float height)
        {
            var x = (float) Math.Cos(angle*Libnoise.Deg2Rad);
            float y = height;
            var z = (float) Math.Sin(angle*Libnoise.Deg2Rad);

            return ((IModule3D) PSourceModule).GetValue(x, y, z);
        }

        #endregion
    }
}
