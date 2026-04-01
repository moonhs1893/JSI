using JSI.AppObject;
using UnityEngine;
using X;

namespace JSI {
    public class JSIApp : XApp {
        // fields
        private JSIPerspCameraPerson mPerspCameraPerson = null;
        public JSIPerspCameraPerson getPerspCameraPerson() {
            return this.mPerspCameraPerson;
        }
        private JSIOrthoCameraPerson mOrthoCameraPerson = null;
        public JSIOrthoCameraPerson getOrthoCameraPerson() {
            return this.mOrthoCameraPerson;
        }
        private JSIGrid mGrid = null;
        private XLogMgr mLogMgr = null;
        public override XLogMgr getLogMgr() {
            return this.mLogMgr;
        }
        private JSIPenMarkMgr mPenMarkMgr = null;
        public JSIPenMarkMgr getPenMarkMgr() {
            return this.mPenMarkMgr;
        }
        private JSIPtCurve2DMgr mPtCurve2DMgr = null;
        public JSIPtCurve2DMgr getPtCurve2DMgr() {
            return this.mPtCurve2DMgr;
        }
        private JSIStandingCardMgr mStandingCardMgr = null;
        public JSIStandingCardMgr getStandingCardMgr() {
            return this.mStandingCardMgr;
        }
        private JSISnapshotMgr mSnapshotMgr = null;
        public JSISnapshotMgr getSnapshotMgr() {
            return this.mSnapshotMgr;
        }
        private XScenarioMgr mScenarioMgr = null;
        public override XScenarioMgr getScenarioMgr() {
            return this.mScenarioMgr;
        }

        private JSIKeyEventSource mKeyEventSource = null;
        private JSIMouseEventSource mMouseEventSource = null;
        private JSIEventListener mEventListener = null;

        private JSICursor2D mCursor = null;
        public JSICursor2D getCursor() {
            return this.mCursor;
        }

        private void Start() {
            // Unity physics options
            Physics.queriesHitBackfaces = true;

            this.mPerspCameraPerson = new JSIPerspCameraPerson();
            this.mOrthoCameraPerson = new JSIOrthoCameraPerson();
            this.mGrid = new JSIGrid();

            //new JSIAppRect3D("TestRect3D", 1f, 2f, 
            //    new Color(0.5f, 0f, 0f, 0.5f));
            //new JSIAppCircle3D("TestCircle3D", 1f, 
            //    new Color(0.5f, 0f, 0f, 0.5f));
            //new JSIStandingCard("TestStandingCard", 1f, 2f,
            //    new Vector3(0f, 1f, 0f), Quaternion.identity, null);

            this.mPenMarkMgr = new JSIPenMarkMgr();
            this.mPtCurve2DMgr = new JSIPtCurve2DMgr();
            this.mStandingCardMgr = new JSIStandingCardMgr();
            this.mSnapshotMgr = new JSISnapshotMgr(this);
            this.mScenarioMgr = new JSIScenarioMgr(this);
            this.mLogMgr = new XLogMgr();
            this.mLogMgr.setPrintOn(true);

            this.mKeyEventSource = new JSIKeyEventSource();
            this.mMouseEventSource = new JSIMouseEventSource();
            this.mEventListener = new JSIEventListener(this);
            this.mCursor = new JSICursor2D(this);

            this.mKeyEventSource.setEventListener(this.mEventListener);
            this.mMouseEventSource.setEventListener(this.mEventListener);
        }
        private void Update() {
            this.mOrthoCameraPerson.update();
            this.mKeyEventSource.update();
            this.mMouseEventSource.update();
        }
    }
}