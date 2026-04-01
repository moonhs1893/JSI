using System.Collections.Generic;
using UnityEngine;

namespace JSI {
    public class JSIPenMarkMgr {
        // constants
        public static readonly int MAX_NUM_PEN_MARKS = 10;

        // fields
        private List<JSIPenMark> mPenMarks = null;
        public List<JSIPenMark> getPenMarks() {
            return this.mPenMarks;
        }

        // constructor
        public JSIPenMarkMgr() {
            this.mPenMarks = new List<JSIPenMark>();
        }

        public void addPenMark(JSIPenMark penMark) {
            this.mPenMarks.Add(penMark);
            if (this.mPenMarks.Count > JSIPenMarkMgr.MAX_NUM_PEN_MARKS) {
                this.mPenMarks.RemoveAt(0);
                Debug.Assert(
                    this.mPenMarks.Count <= JSIPenMarkMgr.MAX_NUM_PEN_MARKS);
            }
        }

        public JSIPenMark getLastPenMark() {
            int size = this.mPenMarks.Count;
            if (size == 0) {
                return null;
            } else {
                return this.mPenMarks[size - 1];
            }
        }

        public JSIPenMark getRecentPenMark(int i) {
            int size = this.mPenMarks.Count;
            int index = size - 1 - i;
            if (index < 0 || index >= size) {
                return null;
            } else {
                return this.mPenMarks[index];
            }
        }

        public bool penDown(Vector2 pt) {
            JSIPenMark penMark = new JSIPenMark(pt);
            this.addPenMark(penMark);
            return true;
        }

        public bool penDrag(Vector2 pt) {
            JSIPenMark penMark = this.getLastPenMark();
            if (penMark != null && penMark.addPt(pt)) {
                return true;
            } else {
                return false;
            }
        }

        public bool penUp(Vector2 pt) {
            return true;
        }

    }
}