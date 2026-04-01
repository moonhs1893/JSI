using System.Text;
using X;
using UnityEngine;
using JSI.Scenario;

namespace JSI.Cmd {
    public class JSICmdToSelectSmallestStandingCardByScaleHandle : 
        XLoggableCmd {
        // fields
        JSIStandingCard mSelectedStandingCard = null;

        // private constructor
        private JSICmdToSelectSmallestStandingCardByScaleHandle(XApp app) : 
            base(app) {
            JSIApp jsi = (JSIApp)this.mApp;
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToSelectSmallestStandingCardByScaleHandle cmd = 
                new JSICmdToSelectSmallestStandingCardByScaleHandle(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIEditStandingCardScenario scenario =
                JSIEditStandingCardScenario.getSingleton();
            this.mSelectedStandingCard = 
                scenario.selectStandingCardByScaleHandle();
            scenario.setSelectedStandingCard(this.mSelectedStandingCard);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            return data;
        }
    }
}