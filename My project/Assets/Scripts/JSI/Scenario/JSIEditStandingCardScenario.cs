using JSI.AppObject;
using JSI.Geom;
using System.Collections.Generic;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario : XScenario {
        // singleton pattern 
        private static JSIEditStandingCardScenario mSingleton = null;
        public static JSIEditStandingCardScenario getSingleton() {
            Debug.Assert(JSIEditStandingCardScenario.mSingleton != null);
            return JSIEditStandingCardScenario.mSingleton;
        }
        public static JSIEditStandingCardScenario createSingleton(XApp app) {
            Debug.Assert(JSIEditStandingCardScenario.mSingleton == null);
            JSIEditStandingCardScenario.mSingleton = 
                new JSIEditStandingCardScenario(app);
            return JSIEditStandingCardScenario.mSingleton;
        }
        private JSIEditStandingCardScenario(XApp app) : base(app) {
            this.setInitScene(this.mScenes[0]);
        }

        protected override void addScenes() {
            this.addScene(JSIEditStandingCardScenario.RotateStandingCardScene.
                createSingleton(this));
            this.addScene(JSIEditStandingCardScenario.MoveStandingCardScene.
                createSingleton(this));
            this.addScene(JSIEditStandingCardScenario.ScaleStandingCardScene.
                createSingleton(this));
            this.addScene(JSIEditStandingCardScenario.MoveLineInStandingCardScene.
                createSingleton(this));
        }

        // fields
        private JSIStandingCard mSelectedStandingCard = null;
        public JSIStandingCard getSelectedStandingCard() {
            return this.mSelectedStandingCard;
        }
        public void setSelectedStandingCard(JSIStandingCard sc) {
            this.mSelectedStandingCard = sc;
        }

        // methods
        public JSIStandingCard selectStandingCardByStand() {
            JSIApp jsi = (JSIApp)this.mApp;
            List<JSIStandingCard> hitStandingCards =
                new List<JSIStandingCard>();
            foreach (JSIStandingCard sc in 
                jsi.getStandingCardMgr().getStandingCards()) {
                if (jsi.getCursor().hits(sc.getStand())) {
                    hitStandingCards.Add(sc);
                }
            }

            if (hitStandingCards.Count == 0) {
                return null;
            }

            // find and return the smallest standing card among the hit ones.
            float minWidth = Mathf.Infinity;
            JSIStandingCard smallestStandingCard = null;
            foreach (JSIStandingCard sc in hitStandingCards) {
                JSIAppRect3D card = sc.getCard();
                JSIRect3D rect = (JSIRect3D)card.getGeom();
                if (rect.getWidth() < minWidth) {
                    smallestStandingCard = sc;
                    minWidth = rect.getWidth();
                }
            }

            return smallestStandingCard;
        }

        public JSIStandingCard selectStandingCardByScaleHandle() {
            JSIApp jsi = (JSIApp)this.mApp;
            List<JSIStandingCard> hitStandingCards =
                new List<JSIStandingCard>();
            foreach (JSIStandingCard sc in
                jsi.getStandingCardMgr().getStandingCards()) {
                if (jsi.getCursor().hits(sc.getScaleHandle())) {
                    hitStandingCards.Add(sc);
                }
            }

            if (hitStandingCards.Count == 0) {
                return null;
            }

            // find and return the smallest standing card among the hit ones.
            float minWidth = Mathf.Infinity;
            JSIStandingCard smallestStandingCard = null;
            foreach (JSIStandingCard sc in hitStandingCards) {
                JSIAppRect3D card = sc.getCard();
                JSIRect3D rect = (JSIRect3D)card.getGeom();
                if (rect.getWidth() < minWidth) {
                    smallestStandingCard = sc;
                    minWidth = rect.getWidth();
                }
            }

            return smallestStandingCard;
        }
    }
}