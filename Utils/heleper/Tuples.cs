#pragma warning disable CS0660 
#pragma warning disable CS0661 

using System;
using System.Collections.Generic;
using UnityEngine;

namespace AnilTools
{
    public struct Tuple1<T1> : IEquatable<Tuple1<T1>>
    {
        public T1 value;

        public Tuple1(T1 t1)
        {
            value = t1;
        }

        public bool Equals(Tuple1<T1> other)
        {
            return EqualityComparer<T1>.Default.Equals(value, other.value);
        }

        public static bool operator ==(Tuple1<T1> a, Tuple1<T1> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Tuple1<T1> a, Tuple1<T1> b)
        {
            return !a.Equals(b);
        }
    }
    public struct Tuple2<T1, T2> : IEquatable<Tuple2<T1, T2>>
    {
        public T1 value;
        public T2 value1;

        public Tuple2(T1 t1, T2 t2)
        {
            value = t1;
            value1 = t2;
        }

        public bool Equals(Tuple2<T1, T2> other)
        {
            return EqualityComparer<T1>.Default.Equals(value, other.value) &&
                   EqualityComparer<T2>.Default.Equals(value1, other.value1);
        }

        public static bool operator ==(Tuple2<T1,T2> a, Tuple2<T1, T2> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Tuple2<T1, T2> a, Tuple2<T1, T2> b)
        {
            return !a.Equals(b);
        }

    }
    public struct Tuple3<T1, T2, T3> : IEquatable<Tuple3<T1, T2, T3>>
    {
        public readonly T1 value;
        public readonly T2 value1;
        public readonly T3 value2;

        public Tuple3(T1 t1, T2 t2, T3 t3)
        {
            value = t1;
            value1 = t2;
            value2 = t3;
        }

        public bool Equals(Tuple3<T1, T2, T3> other)
        {
            return EqualityComparer<T1>.Default.Equals(value, other.value) &&
                   EqualityComparer<T2>.Default.Equals(value1, other.value1) &&
                   EqualityComparer<T3>.Default.Equals(value2, other.value2);
        }

        public override int GetHashCode()
        {
            int hashCode = -838867063;
            hashCode = hashCode * -1521134295 + EqualityComparer<T1>.Default.GetHashCode(value);
            hashCode = hashCode * -1521134295 + EqualityComparer<T2>.Default.GetHashCode(value1);
            return hashCode;
        }

        public static bool operator ==(Tuple3<T1, T2, T3> a, Tuple3<T1, T2, T3> b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Tuple3<T1, T2,T3> a, Tuple3<T1, T2,T3> b)
        {
            return !a.Equals(b);
        }
}

    [Serializable]
    public struct OneFloat
    {
        public float value;

        public static bool operator ==(OneFloat a, OneFloat b)
        {
            return a.value == b.value;
        }
        public static bool operator !=(OneFloat a, OneFloat b)
        {
            return a.value != b.value;
        }
        public static bool operator ==(OneFloat a, float b)
        {
            return a.value == b;
        }
        public static bool operator !=(OneFloat a, float b)
        {
            return a.value != b;
        }

        public static float operator +(OneFloat a, OneFloat b)
        {
            return a.value + b.value;
        }
        public static float operator -(OneFloat a, OneFloat b)
        {
            return a.value + b.value;
        }
        public static float operator *(OneFloat a, OneFloat b)
        {
            return a.value * b.value;
        }
        public static float operator /(OneFloat a, OneFloat b)
        {
            return a.value / b.value;
        }
        public static float operator +(OneFloat a, float b)
        {
            return a.value + b;
        }
        public static float operator -(OneFloat a, float b)
        {
            return a.value + b;
        }
        public static float operator *(OneFloat a, float b)
        {
            return a.value * b;
        }
        public static float operator /(OneFloat a, float b)
        {
            return a.value / b;
        }

    }

    [Serializable]
    public struct BVector3
    {
        private static BVector3 temp;
        private static byte TempB;

        public byte x;
        public byte y;
        public byte z;

        public BVector3(byte x, byte y, byte z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public BVector3(Vector3 vector3)
        {
            x = (byte)vector3.x;
            y = (byte)vector3.y;
            z = (byte)vector3.z;
        }


        public readonly static BVector3 zero = new BVector3(0, 0, 0);
        public readonly static BVector3 one = new BVector3(1, 1, 1);
        public readonly static BVector3 up = new BVector3(0, 1, 0);
        public readonly static BVector3 Left = new BVector3(1, 0, 0);
        public readonly static BVector3 Right = new BVector3(0, 0, 1);

        public static bool operator ==(BVector3 a, BVector3 b)
        { 
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }
        public static bool operator !=(BVector3 a, BVector3 b)
        {
            return !Mathf.Approximately(a.x, b.x) && !Mathf.Approximately(a.y, b.y) && !Mathf.Approximately(a.z, b.z);
        }
        public static bool operator ==(BVector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }
        public static bool operator !=(BVector3 a, Vector3 b)
        {
            return !Mathf.Approximately(a.x, b.x) && !Mathf.Approximately(a.y, b.y) && !Mathf.Approximately(a.z, b.z);
        }

        public static BVector3 operator *(BVector3 a, byte b)
        {
            temp = a;
            temp.x *= b;
            temp.y *= b;
            temp.z *= b;

            return temp;
        }

        public static BVector3 operator *(BVector3 a, float b)
        {
            temp = a;
            TempB = (byte)b;

            temp.x *= TempB;
            temp.y *= TempB;
            temp.z *= TempB;

            return temp;
        }

        public static implicit operator Vector3(BVector3 s)
        {
            return new Vector3(s.x,s.y,s.z);
        }
    }

    [Serializable]
    public struct SVector3
    {
        private static SVector3 temp;
        private static sbyte tempS;

        public sbyte x;
        public sbyte y;
        public sbyte z;

        public SVector3(sbyte x, sbyte y, sbyte z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public SVector3(Vector3 vector3)
        {
            x = (sbyte)vector3.x;
            y = (sbyte)vector3.y;
            z = (sbyte)vector3.z;
        }


        public readonly static SVector3 zero = new SVector3(0, 0, 0);
        public readonly static SVector3 one = new SVector3(1, 1, 1);
        public readonly static SVector3 up = new SVector3(0, 1, 0);
        public readonly static SVector3 Left = new SVector3(1, 0, 0);
        public readonly static SVector3 Right = new SVector3(0, 0, 1);
        public readonly static SVector3 Back = new SVector3(0, 0, -1);
        public readonly static SVector3 Down = new SVector3(0, -1, 0);

        public static bool operator ==(SVector3 a, SVector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }
        public static bool operator !=(SVector3 a, SVector3 b)
        {
            return !Mathf.Approximately(a.x, b.x) && !Mathf.Approximately(a.y, b.y) && !Mathf.Approximately(a.z, b.z);
        }
        public static bool operator ==(SVector3 a, Vector3 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y) && Mathf.Approximately(a.z, b.z);
        }
        public static bool operator !=(SVector3 a, Vector3 b)
        {
            return !Mathf.Approximately(a.x, b.x) && !Mathf.Approximately(a.y, b.y) && !Mathf.Approximately(a.z, b.z);
        }

        public static SVector3 operator *(SVector3 a, sbyte b)
        {
            temp = a;
            temp.x *= b;
            temp.y *= b;
            temp.z *= b;

            return temp;
        }
        public static SVector3 operator *(SVector3 a, float b)
        {
            temp = a;
            tempS = (sbyte)b;

            temp.x *= tempS;
            temp.y *= tempS;
            temp.z *= tempS;

            return temp;
        }

        public static SVector3 operator +(SVector3 a, sbyte b)
        {
            temp = a;
            temp.x += b;
            temp.y += b;
            temp.z += b;

            return temp;
        }

        public static SVector3 operator +(SVector3 a, float b)
        {
            temp = a;
            tempS = (sbyte)b;

            temp.x += tempS;
            temp.y += tempS;
            temp.z += tempS;

            return temp;
        }

        public static implicit operator Vector3(SVector3 s)
        {
            return new Vector3(s.x, s.y, s.z);
        }
    }

    [Serializable]
    public struct SVector2
    {
        private static SVector2 temp;
        private static sbyte tempS;

        public sbyte x;
        public sbyte y;

        public SVector2(sbyte x, sbyte y)
        {
            this.x = x;
            this.y = y;
        }

        public SVector2(Vector2 vector3)
        {
            x = (sbyte)vector3.x;
            y = (sbyte)vector3.y;
        }


        public readonly static SVector2 zero = new SVector2(0, 0);
        public readonly static SVector2 one = new SVector2(1, 1);
        public readonly static SVector2 up = new SVector2(0, 1);
        public readonly static SVector2 Left = new SVector2(1, 0);
        public readonly static SVector2 Down = new SVector2(0, -1);

        public static bool operator ==(SVector2 a, SVector2 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }
        public static bool operator !=(SVector2 a, SVector2 b)
        {
            return !Mathf.Approximately(a.x, b.x) && !Mathf.Approximately(a.y, b.y);
        }
        public static bool operator ==(SVector2 a, Vector2 b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }
        public static bool operator !=(SVector2 a, Vector2 b)
        {
            return !Mathf.Approximately(a.x, b.x) && !Mathf.Approximately(a.y, b.y);
        }

        public static SVector2 operator *(SVector2 a, sbyte b)
        {
            temp = a;
            temp.x *= b;
            temp.y *= b;

            return temp;
        }

        public static SVector2 operator *(SVector2 a, float b)
        {
            temp = a;
            tempS = (sbyte)b;

            temp.x *= tempS;
            temp.y *= tempS;

            return temp;
        }

        public static implicit operator Vector2(SVector2 s)
        {
            return new Vector3(s.x, s.y);
        }
    }

}
