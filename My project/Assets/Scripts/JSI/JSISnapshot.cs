using System;
using System.Collections.Generic;
using JSI.File;

namespace JSI {
    public class JSISnapshot : JSISerializableAppData {
        // constants
        private static readonly double SMALL_AMOUNT = 0.0001; // 0.1mm

        // fields
        private JSISnapshot mPrevSnapshot = null;
        public JSISnapshot getPrevSnapshot() {
            return this.mPrevSnapshot;
        }
        public void setPrevSnapshot(JSISnapshot prevSnapshot) {
            this.mPrevSnapshot = prevSnapshot;
        }
        private JSISnapshot mNextSnapshot = null;
        public JSISnapshot getNextSnapshot() {
            return this.mNextSnapshot;
        }
        public void setNextSnapshot(JSISnapshot nextSnapshot) {
            this.mNextSnapshot = nextSnapshot;
        }

        // constructor
        public JSISnapshot(JSIApp jsi) : base(new JSIAppData(
            DateTime.Now,
            jsi.getPerspCameraPerson().getEye(),
            jsi.getPerspCameraPerson().getView(),
            JSIPerspCameraPerson.FOV,
            jsi.getStandingCardMgr().getStandingCards())) {

        }

        // methods
        public bool checkSnapshotIsSameAs(JSISnapshot s2) {
            JSISnapshot s1 = this;

            // consideration 1: is it something we don't want to lose?
            // consideration 2: is it a result of deliberate action?
            // consideration 3: should changing it destroy undo history?

            return
                this.checkListsOfStandingCardsAreSame(s1.standingCards,
                    s2.standingCards);
        }

        private bool checkFloatsAreSame(float f1, float f2) {
            return Math.Abs(f1 - f2) < JSISnapshot.SMALL_AMOUNT;
        }

        private bool checkVector3sAreSame(JSISerializableVector3 v1,
            JSISerializableVector3 v2) {

            return this.checkFloatsAreSame(v1.x, v2.x) &&
                this.checkFloatsAreSame(v1.y, v2.y) &&
                this.checkFloatsAreSame(v1.z, v2.z);
        }

        private bool checkQuaternionsAreSame(JSISerializableQuaternion q1,
            JSISerializableQuaternion q2) {

            return this.checkFloatsAreSame(q1.x, q2.x) &&
                this.checkFloatsAreSame(q1.y, q2.y) &&
                this.checkFloatsAreSame(q1.z, q2.z) &&
                this.checkFloatsAreSame(q1.w, q2.w);
        }

        private bool checkStandingCardsAreSame(JSISerializableStandingCard sc1,
            JSISerializableStandingCard sc2) {

            // assume that id and ptCurve3Ds are immutable so assume they are
            // same if ids are same
            return sc1.id == sc2.id &&
                this.checkFloatsAreSame(sc1.width, sc2.width) &&
                this.checkFloatsAreSame(sc1.height, sc2.height) &&
                this.checkVector3sAreSame(sc1.pos, sc2.pos) &&
                this.checkQuaternionsAreSame(sc1.rot, sc2.rot);
        }

        private bool checkListsOfStandingCardsAreSame(
            List<JSISerializableStandingCard> l1,
            List<JSISerializableStandingCard> l2) {

            if (l1.Count != l2.Count) {
                return false;
            }

            foreach (JSISerializableStandingCard sc1 in l1) {
                bool sameStandingCardExists = false;
                foreach (JSISerializableStandingCard sc2 in l2) {
                    sameStandingCardExists = sameStandingCardExists ||
                        this.checkStandingCardsAreSame(sc1, sc2);
                    if (sameStandingCardExists) {
                        break;
                    }
                }

                if (!sameStandingCardExists) {
                    return false;
                }
            }

            return true;
        }

        public bool containsStandingCard(JSISerializableStandingCard sSc1) {
            foreach (JSISerializableStandingCard sSc2 in this.standingCards) {
                if (this.checkStandingCardsAreSame(sSc1, sSc2)) {
                    return true;
                }
            }
            return false;
        }
    }
}