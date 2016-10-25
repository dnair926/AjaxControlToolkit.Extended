
Type.registerNamespace('WebExtenders');

WebExtenders.VisibilityControlType = function () {
    throw Error.invalidOperation();
};
WebExtenders.VisibilityControlType.prototype = {
    DropdownList: 0,
    RadiobuttonList: 1,
    CheckBox: 2,
    Panel: 3,
    TextBox: 4,
    Label: 5,
    LinkButton: 6,
    TableRow: 7,
    TableCell: 8,
    Table: 9,
    CheckBoxList: 10
}
WebExtenders.VisibilityControlType.registerEnum('WebExtenders.VisibilityControlType');
WebExtenders.VisibilityAction = function () {
    throw Error.invalidOperation();
}
WebExtenders.VisibilityAction.prototype = {
    Hide: 0,
    Show: 1
}
WebExtenders.VisibilityAction.registerEnum('WebExtenders.VisibilityAction');

WebExtenders.VisibilityBehavior = function (element) {
    /// <summary>
    /// The VisibilityBehavior allows you to add collapsible sections to your page
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement" domElement="true">
    /// Element to associate the behavior with
    /// </param>
    WebExtenders.VisibilityBehavior.initializeBase(this, [element]);

    // property values
    this._collapsed = false;
    this._disableClientEvent = false;
    this._parentControlType = WebExtenders.VisibilityControlType.DropdownList;
    this._targetControlType = WebExtenders.VisibilityControlType.Panel;
    this._actionOnValueSelected = WebExtenders.VisibilityAction.Show;
    this._parentControlID = null;
    this._focusControlID = null;
    this._valuesToCheck = null;

    //handler delegates
    this._eventHandler = null;
}
WebExtenders.VisibilityBehavior.prototype = {
    initialize: function () {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        WebExtenders.VisibilityBehavior.callBaseMethod(this, 'initialize');

        var element = this.get_element();
        var parentElement = $get(this._parentControlID);

        if (!parentElement) {
            throw Error.argument('ParentControlID', String.format(Sys.Extended.UI.Resources.Visibility_NoControlID, this._parentControlID));
        } else if (parentElement !== null) {
            if (!this._disableClientEvent) {
                this._eventHandler = Function.createDelegate(this, this._toggle);

                switch (this._parentControlType) {
                    case WebExtenders.VisibilityControlType.DropdownList: //DropdownList
                        $addHandler(parentElement, 'change', this._eventHandler);
                        break;
                    case WebExtenders.VisibilityControlType.TextBox: //DropdownList
                        $addHandler(parentElement, 'blur', this._eventHandler);
                        break;
                    case WebExtenders.VisibilityControlType.RadiobuttonList:  //RadiobuttonList
                    case WebExtenders.VisibilityControlType.CheckBox: //CheckBox
                    case WebExtenders.VisibilityControlType.CheckBoxList:
                        $addHandler(parentElement, 'click', this._eventHandler);
                        break;
                }
            }
        }
    },

    dispose: function () {
        /// <summary>
        /// Dispose the behavior
        /// </summary>

        var parentElement = $get(this._parentControlID);

        if ((this._eventHandler) && (parentElement)) {
            switch (this._parentControlType) {
                case WebExtenders.VisibilityControlType.DropdownList:
                    $removeHandler(parentElement, 'change', this._eventHandler);
                    break;
                case WebExtenders.VisibilityControlType.TextBox:
                    $removeHandler(parentElement, 'blur', this._eventHandler);
                    break;
                case WebExtenders.VisibilityControlType.RadiobuttonList:
                case WebExtenders.VisibilityControlType.CheckBox:
                case WebExtenders.VisibilityControlType.CheckBoxList:
                    $removeHandler(parentElement, 'click', this._eventHandler);
                    break;
            }
        }

        WebExtenders.VisibilityBehavior.callBaseMethod(this, 'dispose');
    },

    _doClose: function () {
        /// <summary>
        /// Collapse the panel. Internal function, to close call "collapsePanel".
        /// </summary>
        /// <param name="eventObj" type="Sys.UI.DomEvent" mayBeNull="true" optional="true">
        /// Event Info
        /// </param>

        var eventArgs = new Sys.CancelEventArgs();
        if (eventArgs.get_cancel()) {
            return;
        }

        var e = this.get_element();
        e.style.display = 'none';
    },

    _doOpen: function () {
        /// <summary>
        /// Expand the Panel. Internal function, to close call "expandPanel".
        /// </summary>
        /// <param name="eventObj" type="Sys.UI.DomEvent" mayBeNull="true" optional="true">
        /// Event Info
        /// </param>

        var eventArgs = new Sys.CancelEventArgs();
        if (eventArgs.get_cancel()) {
            return;
        }

        var e = this.get_element();
        e.style.display = '';
        //Set focus to the control specified
        this.setFocus();
    },

    _toggle: function (eventObj) {
        /// <summary>
        /// Event handler to expand or collapse the panel (based on its current state)
        /// Internal function. Please use "togglePanel(eventObj)" to get same functionality.
        /// </summary>
        /// <param name="eventObj" type="Sys.UI.DomEvent" mayBeNull="true" optional="true">
        /// Event Info
        /// </param>

        var clickedOnAssociatedLabel = eventObj.target.tagName === 'LABEL';
        if (clickedOnAssociatedLabel) {
            return;
        }

        this.changeControlMode();
        this.raiseValueChanged(new Sys.EventArgs());
    },

    changeControlMode: function () {
        if (this.isValueSelected()) {
            if (this._actionOnValueSelected === WebExtenders.VisibilityAction.Show) {
                this._collapsed = true;
            } else if (this._actionOnValueSelected === WebExtenders.VisibilityAction.Hide) {
                this._collapsed = false;
            }
        } else {
            if (this._actionOnValueSelected === WebExtenders.VisibilityAction.Show) {
                this._collapsed = false;
            } else if (this._actionOnValueSelected === WebExtenders.VisibilityAction.Hide) {
                this._collapsed = true;
            }
        }

        if (this._collapsed) {
            return this._doOpen();
        } else {
            this.clearControlsHiding();
            return this._doClose();
        }
    },

    clearControlsHiding: function () {
        /// <summary>
        /// Clear Textboxes, Dropdowns, Multiline Textboxes, Checkboxes.
        /// </summary>

        var e = this.get_element();

        switch (this._targetControlType) {
            case WebExtenders.VisibilityControlType.Panel:
            case WebExtenders.VisibilityControlType.Table:
            case WebExtenders.VisibilityControlType.TableRow:
            case WebExtenders.VisibilityControlType.TableCell:
                $commonScripts.clearInputElementsInsideContainer(e);
                break;
            case WebExtenders.VisibilityControlType.DropdownList:
            case WebExtenders.VisibilityControlType.RadiobuttonList:
            case WebExtenders.VisibilityControlType.CheckBox:
            case WebExtenders.VisibilityControlType.TextBox:
                $commonScripts.clearControl(e);
                break;
        }
    },

    setFocus: function () {
        $commonScripts.setFocus(this._focusControlID);
    },

    isValueSelected: function () {
        var valueList = this._valuesToCheck;
        var values = '';
        if (valueList !== null) {
            values = valueList.split(',');
        }
        var valueFound = false;
        var parentElement = $get(this._parentControlID);
        var i = 0;

        if (parentElement !== null) {
            switch (this._parentControlType) {
                case WebExtenders.VisibilityControlType.DropdownList:
                    if (values.length > 0) {
                        for (i = 0; i < values.length; i++) {
                            if (parentElement.value === values[i]) {
                                return true;
                            }
                        }
                    }
                    break;
                case WebExtenders.VisibilityControlType.RadiobuttonList:
                    return this.checkInputNodes(parentElement, "radio", values);
                case WebExtenders.VisibilityControlType.CheckBoxList:
                    return this.checkInputNodes(parentElement, "checkbox", values);
                case WebExtenders.VisibilityControlType.CheckBox:
                    if ((parentElement) && (parentElement.checked)) {
                        return true;
                    }
                    break;
            }
        }
        return false;
    },

    checkInputNodes: function (parentElement, tagName, values) {
        if (values.length === 0) {
            return false;
        }

        var inputNodes = parentElement.getElementsByTagName('INPUT');
        for (i = 0; i < inputNodes.length; i++) {
            if ((!inputNodes[i]) || (inputNodes[i].type !== tagName) || (!inputNodes[i].checked)) {
                continue;
            }

            for (j = 0; j < values.length; j++) {
                if (inputNodes[i].value === values[j]) {
                    return true;
                }
            }
        }
    },

    add_valueChanged: function (handler) {
        /// <summary>
        /// Add an event handler for the valueChanged event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().addHandler('valueChanged', handler);
    },
    remove_valueChanged: function (handler) {
        /// <summary>
        /// Remove an event handler from the valueChanged event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().removeHandler('valueChanged', handler);
    },
    raiseValueChanged: function (eventArgs) {
        /// <summary>
        /// Raise the valueChanged event
        /// </summary>
        /// <param name="eventArgs" type="Sys.Extended.UI.AutoCompleteItemEventArgs" mayBeNull="false">
        /// Event arguments for the itemSelected event
        /// </param>
        /// <returns />

        var handler = this.get_events().getHandler('valueChanged');
        if (handler) {
            handler(this, eventArgs);
            //If the text is used, should HtmlEncode to format all ASCII codes back to normal.
            //            this.get_Value.HtmlEncode();
        }
    },
    get_Collapsed: function () {
        /// <value type="Boolean">
        /// Whether or not the panel is collapsed
        /// </value>
        return this._collapsed;
    },
    set_Collapsed: function (value) {
        // if we're changing values, and we're live, togglePanel.
        if (this.get_isInitialized() && this.get_element() && value !== this._collapsed()) {
            this._toggle();
        }
        else {
            this._collapsed = value;
            this.raisePropertyChanged('Collapsed');
        }
    },
    get_DisableClientEvent: function () {
        /// <value type="Boolean">
        /// Whether or not to disable client actions
        /// </value>
        return this._disableClientEvent;
    },
    set_DisableClientEvent: function (value) {
        if (this._disableClientEvent !== value) {
            this._disableClientEvent = value;
            this.raisePropertyChanged('disableClientEvent');
        }
    },

    get_ParentControlID: function () {
        /// <value type="String">
        /// ID of the control used to collapse the target when clicked
        /// </value>
        return this._parentControlID;
    },
    set_ParentControlID: function (value) {
        if (this._parentControlID !== value) {
            this._parentControlID = value;
            this.raisePropertyChanged('parentControlID');
        }
    },

    get_FocusControlID: function () {
        /// <value type="String">
        /// ID of the control used to collapse the target when clicked
        /// </value>
        return this._focusControlID;
    },
    set_FocusControlID: function (value) {
        if (this._focusControlID !== value) {
            this._focusControlID = value;
            this.raisePropertyChanged('focusControlID');
        }
    },

    get_ValuesToCheck: function () {
        /// <value type="String">
        /// ID of the control used to collapse the target when clicked
        /// </value>
        return this._valuesToCheck;
    },
    set_ValuesToCheck: function (value) {
        if (this._valuesToCheck !== value) {
            this._valuesToCheck = value;
            this.raisePropertyChanged('valuesToCheck');
        }
    },

    get_ActionOnValueSelected: function () {
        /// <value type="String">
        /// ID of the control used to collapse the target when clicked
        /// </value>
        return this._actionOnValueSelected;
    },
    set_ActionOnValueSelected: function (value) {
        if (this._actionOnValueSelected !== value) {
            this._actionOnValueSelected = value;
            this.raisePropertyChanged('actionOnValueSelected');
        }
    },

    get_ParentControlType: function () {
        /// <value type="Sys.Extended.UI.VisibilityExpandDirection">
        /// Type of the Parent Control Type (can be either "DropdownList" or "RadiobuttonList" or "CheckBox")
        /// </value>
        return this._parentControlType;
    },
    set_ParentControlType: function (value) {
        if (this._parentControlType !== value) {
            this._parentControlType = value;
            this.raisePropertyChanged('ParentControlType');
        }
    },

    get_TargetControlType: function () {
        /// <value type="Sys.Extended.UI.VisibilityExpandDirection">
        /// Type of the Parent Control Type (can be either "DropdownList" or "RadiobuttonList" or "CheckBox")
        /// </value>
        return this._targetControlType;
    },
    set_TargetControlType: function (value) {
        if (this._targetControlType !== value) {
            this._targetControlType = value;
            this.raisePropertyChanged('TargetControlType');
        }
    }
}
WebExtenders.VisibilityBehavior.registerClass('WebExtenders.VisibilityBehavior', Sys.Extended.UI.BehaviorBase);