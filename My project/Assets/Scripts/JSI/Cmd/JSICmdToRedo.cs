using System;
using X;

namespace JSI.Cmd {
    public class JSICmdToRedo : XLoggableCmd {
        // private constructor
        private JSICmdToRedo(XApp app) : base(app) {}

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToRedo cmd = new JSICmdToRedo(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            return jsi.getSnapshotMgr().redo();
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
    }
}