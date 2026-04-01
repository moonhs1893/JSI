using System.Collections.Generic;
using UnityEngine;

namespace JSI.Geom {
    public class JSIRect3D : JSIGeom3D {
        // fields 
        private readonly float mWidth = float.NaN;
        public float getWidth() {
            return this.mWidth;
        }
        private readonly float mHeight = float.NaN;
        public float getHeight() {
            return this.mHeight;
        }
        private readonly Vector3 mPos = Vector3.zero;
        public Vector3 getPos() {
            return this.mPos;
        }
        private readonly Quaternion mRot = Quaternion.identity;
        public Quaternion getRot() {
            return this.mRot;
        }

        // constructor
        public JSIRect3D(float width, float height, Vector3 pos, 
            Quaternion rot) {
            this.mWidth = width;
            this.mHeight = height;
            this.mPos = pos;
            this.mRot = rot;
        }

        // methods
        public Vector3 calcNormalDir() {
            return this.mRot * Vector3.forward;
        }
        public Vector3 calcWidthDir() {
            return this.mRot * Vector3.right;
        }
        public Vector3 calcHeightDir() {
            return this.mRot * Vector3.up;
        }

        public List<Vector3> calcPts() {
            Vector3 wDir = this.calcWidthDir();
            Vector3 hDir = this.calcHeightDir();
            List<Vector3> pts = new List<Vector3>();
            pts.Add(this.mPos +
                wDir * (+this.mWidth) / 2f + hDir * (+this.mHeight) / 2f);
            pts.Add(this.mPos +
                wDir * (-this.mWidth) / 2f + hDir * (+this.mHeight) / 2f);
            pts.Add(this.mPos +
                wDir * (-this.mWidth) / 2f + hDir * (-this.mHeight) / 2f);
            pts.Add(this.mPos +
                wDir * (+this.mWidth) / 2f + hDir * (-this.mHeight) / 2f);
            return pts;
        }

        public Mesh calcMesh() {
            Mesh mesh = new Mesh();
            mesh.vertices = this.calcPts().ToArray();
            mesh.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
            return mesh;
        }
     }
}