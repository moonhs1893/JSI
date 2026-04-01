using System;
using X;

namespace JSI.Cmd {
    public class JSICmdToUndo : XLoggableCmd {
        // fields
        // ...

        // private constructor
        private JSICmdToUndo(XApp app) : base(app) {}

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToUndo cmd = new JSICmdToUndo(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            return jsi.getSnapshotMgr().undo();
        }

        protected override XJson createLogData() {
            JSIApp jsi = (JSIApp)this.mApp;
            XJson data = new XJson();
            // data.addMember("remainingUndoCount", JSICmdToUndo.
            //     calcRemainingUndoCount(jsi));
            // data.addMember("remainingRedoCount", JSICmdToUndo.
            //     calcRemainingRedoCount(jsi));
            return data;
        }

        // public methods
        public static int calcRemainingUndoCount(JSIApp jsi) {
            JSISnapshot ss = jsi.getSnapshotMgr().getCurSnapshot();
            int remainingUndoCount = 0;
            while (ss.getPrevSnapshot() != null) {
                remainingUndoCount++;
                ss = ss.getPrevSnapshot();
            }
            return remainingUndoCount;
        }
        public static int calcRemainingRedoCount(JSIApp jsi) {
            JSISnapshot ss = jsi.getSnapshotMgr().getCurSnapshot();
            int remainingRedoCount = 0;
            while (ss.getNextSnapshot() != null) {
                remainingRedoCount++;
                ss = ss.getNextSnapshot();
            }
            return remainingRedoCount;
        }
    }
}