namespace JSI.AppObject {
    public abstract class JSIAppObject3D : JSIAppObject {
        // contructor
        public JSIAppObject3D(string name) : base(name) {
            this.mGameObject.layer = 0; // default layer.
        }
    }
}