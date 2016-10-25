Type.registerNamespace('WebExtenders');

WebExtenders._CommonScripts = function () {
    // The _CommonScripts class contains functionality utilized across a number
    // of controls (but not universally)
    // You should not create new instances of _CommonScripts.  Instead you should use the shared instance CommonToolkitScripts (or WebExtenders.CommonToolkitScripts).
}
WebExtenders._CommonScripts.prototype = {
    clearInputElementsInsideContainer: function (container) {
        var tagsToClear = ['INPUT', 'SELECT', 'TEXTAREA'],
            tagsCount = tagsToClear.length,
            elements = null,
            elementCount = 0,
            element = null,
            i = 0,
            j = 0;

        if (tagsCount === 0) {
            return;
        }

        for (i = 0; i < tagsCount; i++) {
            elements = container.getElementsByTagName(tagsToClear[i]);
            if (typeof elements === "undefined" && elements !== null) {
                continue;
            }

            elementCount = elements.length;
            for (j = 0; j < elementCount; j++) {
                element = elements[j];
                this.clearControl(element);
            }
        }
    },

    tryFireEvent: function (element, eventName, properties) {
        /// <summary>
        /// Attempts to fire a DOM event on an element
        /// </summary>
        /// <param name="element" type="Sys.UI.DomElement">The element to fire the event</param>
        /// <param name="eventName" type="String">The name of the event to fire (without an 'on' prefix)</param>
        /// <param name="properties" type="Object">Properties to add to the event</param>
        /// <returns type="Boolean">True if the event was successfully fired, otherwise false</returns>

        try {
            var def = $common.__DOMEvents[eventName];
            if (def) {
                var e = document.createEvent(def.eventGroup);
                def.init(e, properties || {});
                element.dispatchEvent(e);
                return true;
            }

        } catch (e) {
        }
        return false;
    },

    clearControl: function (control) {
        switch (control.tagName.toLowerCase()) {
            case 'input':
                switch (control.type.toLowerCase()) {
                    case 'checkbox':
                        if (control.checked) {
                            //control.checked = false;
                            this.raiseControlEvent(control);
                        }
                        break;
                    case 'radio':
                        //Uncheck the control, and raise the click event. 
                        //These controls have click event handlers which do not run automatically
                        if (control.checked) {
                            control.checked = false;
                            this.raiseControlEvent(control);
                        }
                        break;
                    case 'text':
                    case 'password':
                    case 'hidden':
                        //Just clear the value. No need to raise any event. 
                        //If this control is getting cleared that means some other control was clicked or changed 
                        //and the focus shifted to that control and the blur event of this contol will run automatically.
                        if (control.value.trim() !== '') {
                            control.value = '';
                        }
                        break;
                }
                break;
            case 'textarea':
                //Just clear the value. No need to raise any event. 
                //If this control is getting cleared that means some other control was clicked or changed 
                //and the focus shifted to that control and the blur event of this contol will run automatically.
                if (control.value.trim() !== '') {
                    control.value = '';
                }
                break;
            case 'select':
                //Select the first item from the list. No need to raise any event. 
                //The "change" event of this control will run automatically after the index is changed to the first item.
                //Should not touch the dropdowns on the calendar extender, it will cause the calendar to show. (CalendarExtender was modified to add Year and Month dropdowns)
                if ((control.id.indexOf("_monthSelect") === -1) && (control.id.indexOf("_yearSelect") === -1) && (control.selectedIndex !== 0)) {
                    control.selectedIndex = 0;
                    this.raiseControlEvent(control);
                }
                break;
        }
    },

    raiseControlEvent: function (control) {
        switch (control.tagName.toLowerCase()) {
            case 'input':
                switch (control.type.toLowerCase()) {
                    case 'checkbox':
                        this.tryFireEvent(control, "click");
                        break;
                    case 'radio':
                        // If Visibility Behavior is attached to an ASP.NET Radio Button list, the click event is associated to the parent container (ul (Layout=OrderedList/UnorderedList)/span (Layout=Flow) or table (Layout=Table)).
                        // so when radio button is unchecked inside a hidden container, click event has to be raised on the parent element.
                        var parentElementId = this.getParentElementId(control.id);
                        var parentElement = $get(parentElementId);
                        if (parentElement) {
                            this.tryFireEvent(parentElement, "click");
                        } else {
                            this.tryFireEvent(control, "click");
                        }
                        break;
                    case 'text':
                    case 'password':
                        this.tryFireEvent(control, "blur");
                        break;
                }
                break;
            case 'textarea':
                this.tryFireEvent(control, "blur");
                break;
            case 'select':
                this.tryFireEvent(control, "change");
                break;
        }
    },

    getParentElementId: function (id) {
        if (!id) {
            return null;
        }
        
        var regex = /^(\w+)\_\d{1,}$/g,
            matches = regex.exec(id);
            
        return matches.length > 1 ? matches[1] : null;
    },

    setFocus: function (elementIdBeingFocused) {
        if (elementIdBeingFocused === null) {
            return;
        }

        var control = $get(elementIdBeingFocused);
        if (!control || !control.focus) {
            return;
        }

        control.focus();
    },
}

CommonToolkitScripts = WebExtenders.CommonScripts = new WebExtenders._CommonScripts();
$commonScripts = CommonToolkitScripts;
