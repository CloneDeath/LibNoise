namespace LibNoise
{
    /// <summary>
    /// 
    /// </summary>
    public enum NoisePrimitive : byte
    {
        Constant = 1,
        Spheres = 2,
        Cylinders = 3,
        BevinsValue = 4,
        BevinsGradient = 5,
        //ClassicPerlin = 6,
        ImprovedPerlin = 7,
        SimplexPerlin = 8
    }
}
