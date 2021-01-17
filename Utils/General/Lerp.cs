

using UnityEngine;

namespace AnilTools
{
	using static Mathmatic;

    public static class Lerp
    {
		public static float Smooth(MoveType moveType,ref float x , float speed)
		{
            switch (moveType)
            {
                case MoveType.fastToSlow:
					x = 1.0f - x;
					x = Mathf.Pow(x, 2) * speed;
					x = 1.0f - x;
					return Max(ref x);
                case MoveType.slowToFast:

					x = Mathf.Pow(x, 2) * speed;
					
					return Max(ref x);
            }
			return x;
        }
		
		public static Vector3 LerpPercentage(this Vector3 StartPos, Vector3 targetPos,float percentage)
        {
            percentage = Mathf.Clamp01(percentage);

            Vector3 startToFinish = StartPos - targetPos;

            return StartPos + startToFinish * percentage;
        }
    }
}
/*
case LeanEase.Smooth:
{
	x = x * x * (3.0f - 2.0f * x);
}
break;

case LeanEase.Accelerate:
{
	x *= x;
}
break;

case LeanEase.Decelerate:
{
	x = 1.0f - x;
	x *= x;
	x = 1.0f - x;
}
break;

case LeanEase.Elastic:
{
	var angle   = x * Mathf.PI * 4.0f;
	var weightA = 1.0f - Mathf.Pow(x, 0.125f);
	var weightB = 1.0f - Mathf.Pow(1.0f - x, 8.0f);

	x = Mathf.LerpUnclamped(0.0f, 1.0f - Mathf.Cos(angle) * weightA, weightB);
}
break;

case LeanEase.Back:
{
	x = 1.0f - x;
	x = x * x * x - x * Mathf.Sin(x * Mathf.PI);
	x = 1.0f - x;
}
break;

case LeanEase.Bounce:
{
	if (x < (4f/11f))
	{
		x = (121f/16f) * x * x;
	}
	else if (x < (8f/11f))
	{
		x = (121f/16f) * (x - (6f/11f)) * (x - (6f/11f)) + 0.75f;
	}
	else if (x < (10f/11f))
	{
		x = (121f/16f) * (x - (9f/11f)) * (x - (9f/11f)) + (15f/16f);
	}
	else
	{
		x = (121f/16f) * (x - (21f/22f)) * (x - (21f/22f)) + (63f/64f);
	}
}
break; 
*/