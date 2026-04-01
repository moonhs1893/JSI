using JSI.AppObject;
using System.Collections.Generic;
using UnityEngine;

namespace JSI {
    public class JSIPtCurve2DMgr {
        // constants
        public static readonly float PT_CURVE_WIDTH = 2f;
        public static readonly Color PT_CURVE_COLOR = Color.blue;

        // fields
        private JSIAppPolyline2D mCurPtCurve2D = null;
        public JSIAppPolyline2D getCurPtCurve2D() {
            return this.mCurPtCurve2D;
        }
        public void setCurPtCurve2D(JSIAppPolyline2D ptCurve2D) {
            this.mCurPtCurve2D = ptCurve2D;
        }
        private List<JSIAppPolyline2D> mPtCurve2Ds = null;
        public List<JSIAppPolyline2D> getPtCurve2Ds() {
            return this.mPtCurve2Ds;
        }

        // constructor
        public JSIPtCurve2DMgr() {
            this.mPtCurve2Ds = new List<JSIAppPolyline2D>();
        }
    }
}