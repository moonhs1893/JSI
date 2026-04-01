using System.Collections.Generic;
using UnityEngine;

namespace JSI.Geom {
    public class JSIPolyline3D : JSIGeom3D {
        // fields
        private readonly List<Vector3> mPts = null;
        public List<Vector3> getPts() {
            return this.mPts;
        }

        // constructor 
        public JSIPolyline3D(List<Vector3> pts) {
            this.mPts = pts;
        }

        // utility methods
        public Vector3 calcCentroid() {
            Vector3 centroid = Vector3.zero;
            int num = this.mPts.Count;
            foreach (Vector3 pt in this.mPts) {
                centroid += pt;
            }
            centroid /= (float)num;
            return centroid;
        }

        public float calcMaxDevFrom(Vector3 fromPt) {
            float maxDev = float.NegativeInfinity;
            foreach (Vector3 pt in this.mPts) {
                float dev = Vector3.Distance(pt, fromPt);
                if (dev > maxDev) {
                    maxDev = dev;
                }
            }
            return maxDev;
        }
    }
}