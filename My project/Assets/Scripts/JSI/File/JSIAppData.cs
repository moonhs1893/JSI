using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSI {
    public class JSIAppData {
        // fields
        private DateTime mSavedTime;
        public DateTime getSavedTime() {
            return this.mSavedTime;
        }
        private Vector3 mEye = JSIUtil.VECTOR3_NAN;
        public Vector3 getEye() {
            return this.mEye;
        }
        private Vector3 mView = JSIUtil.VECTOR3_NAN;
        public Vector3 getView() {
            return this.mView;
        }
        private float mFov = float.NaN;
        public float getFov() {
            return this.mFov;
        }
        private List<JSIStandingCard> mStandingCards = null;
        public List<JSIStandingCard> getStandingCards() {
            return this.mStandingCards;
        }

        // constructor
        public JSIAppData(DateTime savedTime, Vector3 eye, Vector3 view,
            float fov,List<JSIStandingCard> standingCards) {

            this.mSavedTime = savedTime;
            this.mFov = fov;
            this.mEye = eye;
            this.mView = view;
            this.mStandingCards = standingCards;
        }
    }
}