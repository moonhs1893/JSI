using System.Text;

namespace X { 

    /**
     *
     * @author seokhyung
     */
    public class XCmdToChangeScene : XLoggableCmd {
        // fields
        private XScene mFromScene = null;
        private XScene mToScene = null;
        private XScene mReturnScene = null;

        // private contructor
        private XCmdToChangeScene(XApp app, XScene toScene, 
            XScene returnScene) : base(app) {
            this.mFromScene = app.getScenarioMgr().getCurScene();
            this.mToScene = toScene;
            this.mReturnScene = returnScene;
        }

        // static method to construct and execute this command
        public static bool execute(XApp app, XScene toScene, 
            XScene returnScene) {
            XCmdToChangeScene cmd = new XCmdToChangeScene(app, toScene, 
                returnScene);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            this.mToScene.setReturnScene(this.mReturnScene);
            this.mApp.getScenarioMgr().setCurScene(this.mToScene);
            return true;
        }

        // protected override string createLog() {
        //     StringBuilder sb = new StringBuilder();
        //     sb.Append(this.GetType().Name).Append("\t");
        //     sb.Append(this.mFromScene.GetType().Name).Append("\t");
        //     XScene curScene = this.mApp.getScenarioMgr().getCurScene();
        //     sb.Append(curScene.GetType().Name).Append("\t");
        //     if (this.mReturnScene == null) {
        //         sb.Append("null");
        //     } else {
        //         sb.Append(curScene.getReturnScene().GetType().Name);
        //     }
        //     return sb.ToString();
        // }
        protected override XJson createLogData() {
            XJson data = new XJson();
            string fromScene = this.mFromScene.getName();
            string toScene = this.mToScene.getName();
            string returnScene = null;
            if (this.mReturnScene != null) {
                returnScene = this.mReturnScene.getName();
            }
            data.addMember("fromScene", fromScene);
            data.addMember("toScene", toScene);
            data.addMember("returnScene", returnScene);
            return data;
        }
    }
}
