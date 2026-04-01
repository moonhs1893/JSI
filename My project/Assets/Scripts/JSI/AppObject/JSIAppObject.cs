using System.Collections.Generic;
using UnityEngine;

namespace JSI.AppObject {
    public abstract class JSIAppObject {
        // fields
        protected GameObject mGameObject = null;
        public GameObject getGameObject() {
            return this.mGameObject;
        }
        protected List<JSIAppObject> mChildren = null;
        public List<JSIAppObject> getChildren() {
            return this.mChildren;
        }

        // constructor
        public JSIAppObject(string name) {
            this.mGameObject = new GameObject(name);
            this.mChildren = new List<JSIAppObject>();
            this.addComponents();
        }

        // methods
        protected abstract void addComponents();
        public void addChild(JSIAppObject child) {
            this.mChildren.Add(child);
            GameObject childGameObject = child.getGameObject();

            Vector3 localPos = childGameObject.transform.localPosition;
            Quaternion localRot = childGameObject.transform.localRotation;
            Vector3 localScale = childGameObject.transform.localScale;

            childGameObject.transform.parent = this.mGameObject.transform;

            childGameObject.transform.localPosition = localPos;
            childGameObject.transform.localRotation = localRot;
            childGameObject.transform.localScale = localScale;
        }
        public void removeChild(JSIAppObject child) {
            this.mChildren.Remove(child);
            GameObject childGameObject = child.getGameObject();

            Vector3 localPos = childGameObject.transform.localPosition;
            Quaternion localRot = childGameObject.transform.localRotation;
            Vector3 localScale = childGameObject.transform.localScale;

            childGameObject.transform.parent = null;

            childGameObject.transform.localPosition = localPos;
            childGameObject.transform.localRotation = localRot;
            childGameObject.transform.localScale = localScale;
        }
        public void destroyGameObject() {
            GameObject.Destroy(this.mGameObject);
            foreach (JSIAppObject child in this.mChildren) {
                child.destroyGameObject();
            }
        }
    }
}