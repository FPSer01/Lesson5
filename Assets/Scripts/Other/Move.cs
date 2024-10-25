using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public struct VectorXYZ 
	{
		public float x;
		public float y;
		public float z;
		
		public float vectorLen() 
		{
			return new Vector3(x, y, z).magnitude;
		}
		
		public Vector3 norm() 
		{
			return new Vector3(x, y, z).normalized;
		}
		
		public float dot(Vector3 vec) 
		{
			return Vector3.Dot(new Vector3(x, y, z), vec);
		}
		
		public Vector3 mult(Vector3 vec) 
		{
			return Vector3.Cross(new Vector3(x, y, z), vec);
		}
	}
}
