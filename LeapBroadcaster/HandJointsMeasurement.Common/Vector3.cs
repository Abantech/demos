using System;
using System.Runtime.Serialization;

namespace HandJointsMeasurement
{
    /// <summary>
    /// Serializable wrapper around System.Numerics.Vector3
    /// </summary>
    [DataContract]
    public class Vector3
    {
        /// <summary>
        /// Gets or sets the x coordinate.
        /// </summary>
        /// <value>
        /// The x coordinate.
        /// </value>
        [DataMember]
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the y coordinate.
        /// </summary>
        /// <value>
        /// The y coordinate.
        /// </value>
        [DataMember]
        public float Y { get; set; }

        /// <summary>
        /// Gets or sets the z coordinate.
        /// </summary>
        /// <value>
        /// The z coordinate.
        /// </value>
        [DataMember]
        public float Z { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> class.
        /// </summary>
        public Vector3() : this(0, 0, 0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Finds the angle between two vectors.
        /// 
        /// Calculations according to http://www.wikihow.com/Find-the-Angle-Between-Two-Vectors
        /// </summary>
        /// <param name="v1">The first vector</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The angle between the two vectors</returns>
        public float AngleTo(Vector3 v)
        {
            var v1 = this.ConvertToNumerics();
            var v2 = v.ConvertToNumerics();
            return Convert.ToInt64(Math.Acos(System.Numerics.Vector3.Dot(v1, v2) / (v1.Length() * v2.Length())));
        }

        /// <summary>
        /// Converts vector to <see cref="System.Numerics.Vector3"/>.
        /// </summary>
        /// <returns></returns>
        public System.Numerics.Vector3 ConvertToNumerics()
        {
            return new System.Numerics.Vector3(this.X, this.Y, this.Z);
        }

        /// <summary>
        /// Creates vector from <see cref="System.Numerics.Vector3"/>.
        /// </summary>
        /// <returns></returns>
        public static Vector3 ConvertFromNumerics(System.Numerics.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return ConvertFromNumerics(v1.ConvertToNumerics() - v2.ConvertToNumerics());
        }
    }
}
