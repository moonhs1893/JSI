using UnityEngine;
using X;

namespace JSI {
    public abstract class JSIScene : XScene {
        // constructor
        protected JSIScene(XScenario scenario) : base(scenario) {
        }

        // methods
        public abstract void handleKeyDown(KeyCode kc);
        public abstract void handleKeyUp(KeyCode kc);
        public abstract void handlePenDown(Vector2 pt);
        public abstract void handlePenDrag(Vector2 pt);
        public abstract void handlePenUp(Vector2 pt);
    }
}