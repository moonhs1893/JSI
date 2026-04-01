using System.Collections.Generic;
using UnityEngine;

namespace JSI.Geom {
    public class JSICircle2D : JSIGeom2D {
        // fields
        private readonly float mRadius = float.NaN;
        public float getRadius() {
            return this.mRadius;
        }
        private readonly Vector2 mPos = Vector2.zero;
        public Vector2 getPos() {
            return this.mPos;
        }
        private readonly Quaternion mRot = Quaternion.identity;
        public Quaternion getRot() {
            return this.mRot;
        }

        // constructor
        public JSICircle2D(float radius, Vector2 pos, Quaternion rot) {
            this.mRadius = radius;
            this.mPos = pos;
            this.mRot = rot;
        }

        // methods
        //public Vector3 calcNormalDir() {
        //    return this.mRot * Vector3.forward;
        //}
        //public Vector3 calcXDir() {
        //    return this.mRot * Vector3.right;
        //}
        //public Vector3 calcYDir() {
        //    return this.mRot * Vector3.up;
        //}
        public List<Vector2> calcPts(int sideNum) {
            float dtheta = 2f * Mathf.PI / (float)sideNum; // in radian
            List<Vector2> pts = new List<Vector2>();
            for (int i = 0; i < sideNum + 1; i++) { // the last point is the 
                                                    // starting point. 
                Vector2 pt = this.mPos + new Vector2(
                    this.mRadius * Mathf.Cos((float)i * dtheta),
                    this.mRadius * Mathf.Sin((float)i * dtheta));
                pts.Add(pt);
            }
            return pts;
        }
        public Mesh calcMesh(int sideNum) {
            // the second to last vertex is the starting point. 
            // the last vertex is its center. 
            List<Vector3> vs = new List<Vector3>();
            foreach (Vector2 pt2D in this.calcPts(sideNum)) {
                //Vector3 pt3D = new Vector3(pt2D.x, pt2D.y, 0f);
                vs.Add((Vector3)pt2D);
            }
            vs.Add((Vector3)this.mPos);

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
