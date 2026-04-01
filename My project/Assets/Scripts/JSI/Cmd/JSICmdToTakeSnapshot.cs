using X;

namespace JSI.Cmd {
    public class JSICmdToTakeSnapshot : XLoggableCmd {
        // fields
        // ...

        // private constructor
        private JSICmdToTakeSnapshot(XApp app) : base(app) {
            // JSIApp jsi = (JSIApp)this.mApp;
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToTakeSnapshot cmd = new JSICmdToTakeSnapshot(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            return jsi.getSnapshotMgr().takeSnapshot();
        }

        protected override XJson createLogData() {
            JSIApp jsi = (JSIApp)this.mApp;
            XJson data = new XJson();
            data.addMember("remainingUndoCount", JSICmdToUndo.
                calcRemainingUndoCount(jsi));
            data.addMember("remainingRedoCount", JSICmdToUndo.
                calcRemainingRedoCount(jsi));
            return data;
        }
    }
}