using System.Collections.Generic;
using UnityEngine;

namespace JSI {
    public class JSIPenMark {
        // constants 
        public static readonly double MIN_DIST_BTWN_PTS = 1f;

        // fields
        private List<Vector2> mPts = null;
        public List<Vector2> getPts() {
            return this.mPts;
        }
        
        // constructor
        public JSIPenMark(Vector2 pt) {
            this.mPts = new List<Vector2>();
            this.mPts.Add(pt);
        }

        // methods
        public bool addPt(Vector2 pt) {
            Debug.Assert(this.mPts.Count > 0);
            Vector2 lastPt = this.getLastPt();
            if (Vector2.Distance(lastPt, pt) < JSIPenMark.MIN_DIST_BTWN_PTS) {
                return false;
            } else {
                this.mPts.Add(pt);
                return true;
            }
        }

        public Vector2 getFirst() {
            return this.mPts[0];
        }

        public Vector2 getLastPt() {
            int size = this.mPts.Count;
            Debug.Assert(size > 0);
            return this.mPts[size - 1];
        }

        public Vector2 getRecentPt(int i) {
            int size = this.mPts.Count;
            int index = size - 1 - i;
            Debug.Assert(index >= 0 && index < size);
            return this.mPts[index];
        }
    }
}