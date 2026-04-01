using System;
using System.Linq;
using UnityEngine;

namespace JSI {
    public static class JSIUtil {
        // constants
        public static readonly Vector2 VECTOR2_NAN =
            new Vector2(float.NaN, float.NaN);
        public static readonly Vector3 VECTOR3_NAN =
            new Vector3(float.NaN, float.NaN, float.NaN);
        public static readonly Quaternion QUATERNION_NAN =
            new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);

        // different Random objects created in small amount of time
        // will produce same random numbers when Next() is called
        // so, make sure Next() is called on the same Random object
        private static readonly System.Random random = new System.Random();
        private static readonly char[] ID_CHARS =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz-".
            ToArray();
        private static readonly int ID_LENGTH = 21;

        // methods
        public static void createDebugSphere(Vector3 pt) {
            GameObject debugSphere = GameObject.CreatePrimitive(
                PrimitiveType.Sphere);
            debugSphere.name = "DebugSphere";
            debugSphere.transform.position = pt;
            debugSphere.transform.localScale = 0.05f * Vector3.one;
            debugSphere.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        public static string createId() {
            char[] idChars = new char[ID_LENGTH];
            for (int i = 0; i < ID_LENGTH; i++) {
                idChars[i] = ID_CHARS[random.Next(0, ID_CHARS.Length)];
            }
            return new string(idChars);
        }
    }
}