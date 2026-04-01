using System.Collections.Generic;
using UnityEngine;

namespace JSI.Geom {
    public class JSICircle3D : JSIGeom3D {
        // fields
        private readonly float mRadius = float.NaN;
        public float getRadius() {
            return this.mRadius;
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
        public JSICircle3D(float radius, Vector3 pos, Quaternion rot) {
            this.mRadius = radius;
            this.mPos = pos;
            this.mRot = rot;
        }

        // methods
        public Vector3 calcNormalDir() {
            return this.mRot * Vector3.forward;
        }
        public Vector3 calcXDir() {
            return this.mRot * Vector3.right;
        }
        public Vector3 calcYDir() {
            return this.mRot * Vector3.up;
        }
        public List<Vector3> calcPts(int sideNum) {
            float dtheta = 2f * Mathf.PI / (float)sideNum; // in radian
            Vector3 xDir = this.calcXDir();
            Vector3 yDir = this.calcYDir();
            List<Vector3> pts = new List<Vector3>();
            for (int i = 0; i < sideNum + 1; i++) { // the last point is the 
                                                    // starting point. 
                Vector3 pt = this.mPos +
                    xDir * this.mRadius * Mathf.Cos((float)i * dtheta) +
                    yDir * this.mRadius * Mathf.Sin((float)i * dtheta);
                pts.Add(pt);
            }
            return pts;
        }
        public Mesh calcMesh(int sideNum) {
            // the second to last vertex is the starting point. 
            // the last vertex is its center. 
            List<Vector3> vs = this.calcPts(sideNum);
            vs.Add(this.mPos);

            int[] ts = new int[3 * sideNum];
            for (int i = 0; i < sideNum; i++) {
                int j = 3 * i;
                ts[j] = i;
                ts[j + 1] = i + 1;
                ts[j + 2] = sideNum + 1;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = vs.ToArray();
            mesh.triangles = ts;
            return mesh;
        }
    }
}
