using JSI.Scenario;
using X;

namespace JSI {
    public class JSIScenarioMgr : XScenarioMgr {
        // constructor
        public JSIScenarioMgr(XApp app) : base(app) { 
        }

        protected override void addScenarios() {
            JSIApp jsi = (JSIApp)this.mApp;
            this.addScenario(JSIDefaultScenario.createSingleton(jsi));
            this.addScenario(JSINavigateScenario.createSingleton(jsi));
            this.addScenario(JSIEditStandingCardScenario.createSingleton(jsi));
            this.addScenario(JSIDrawScenario.createSingleton(jsi));
        }

        protected override void setInitCurScene() {
            this.setCurScene(JSIDefaultScenario.ReadyScene.getSingleton());
        }
    }
}