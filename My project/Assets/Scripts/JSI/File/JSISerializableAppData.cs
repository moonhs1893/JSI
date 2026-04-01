using System;
using System.Collections.Generic;
using System.Globalization;

/*
see JSON official documentation
https://json.org/json-en.html

{
    "savedTime": "#"
    "eye": { "x": #, "y": #, "z": # },
    "view": { "x": #, "y": #, "z": # },
    "fov": #,
    "standingCards": [{
        "width": #,
        "height": #,
        "pos": { "x": #, "y": #, "z": # },
        "rot": { "x": #, "y": #, "z": #, "w": # },
        "ptCurve3Ds": [
            {
                "pts": [{ "x": #, "y": #, "z": # }, ...],
                "width": #,
                "color": { "r": #, "g": #, "b": #, "a": # }
            },
            ...
        ]
    }, ...]
}
*/
namespace JSI.File {
    [Serializable]
    public class JSISerializableAppData {
        // fields
        public string savedTime = string.Empty;
        public JSISerializableVector3 eye = null;
        public JSISerializableVector3 view = null;
        public float fov = float.NaN;
        public List<JSISerializableStandingCard> standingCards = null;

        // constructor
        public JSISerializableAppData(JSIAppData ad) {
            this.savedTime = ad.getSavedTime().ToString("o"); // ISO 8601 format
            // see https://docs.microsoft.com/en-us/dotnet/standard/base-types/
            // standard-date-and-time-format-strings
            this.eye = new JSISerializableVector3(ad.getEye());
            this.view = new JSISerializableVector3(ad.getView());
            this.fov = ad.getFov();
            this.standingCards = new List<JSISerializableStandingCard>();
            foreach (JSIStandingCard standingCard in ad.getStandingCards()) {
                JSISerializableStandingCard sStandingCard =
                    new JSISerializableStandingCard(standingCard);
                this.standingCards.Add(sStandingCard);
            }
        }

        // methods
        public JSIAppData toAppData() {
            List<JSIStandingCard> standingCards = new List<JSIStandingCard>();
            foreach(JSISerializableStandingCard serialStandingCard in
                this.standingCards) {

                JSIStandingCard sc = serialStandingCard.toStandingCard();
                standingCards.Add(sc);
            }
            return new JSIAppData(DateTime.Parse(this.savedTime), this.eye.
                toVector3(), this.view.toVector3(), this.fov, standingCards);
        }
    }
}