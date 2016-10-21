Type.registerNamespace('WebExtenders');

WebExtenders.CustomTargetControlType = function () {
    /// <summary>
    /// The CustomTargetControlType enumeration describes the type of parent control
    /// </summary>
    /// <field name="DropdownList" type="Number" integer="true" />
    /// <field name="RadiobuttonList" type="Number" integer="true" />
    /// <field name="CheckBox" type="Number" integer="true" />
    /// <field name="TextBox" type="Number" integer="true" />
    throw Error.invalidOperation();
}
WebExtenders.CustomTargetControlType.prototype = {
    DropdownList: 0,
    RadiobuttonList: 1,
    CheckBox: 2,
    TextBox: 3,
    CheckBoxList: 4
}
WebExtenders.CustomTargetControlType.registerEnum('WebExtenders.CustomTargetControlType');

WebExtenders.CustomBehavior = function (element) {
    /// <summary>
    /// The CustomBehavior allows you to add collapsible sections to your page
    /// </summary>
    /// <param name="element" type="Sys.UI.DomElement" domElement="true">
    /// Element to associate the behavior with
    /// </param>
    WebExtenders.CustomBehavior.initializeBase(this, [element]);

    // property values
    this._targetControlType = WebExtenders.CustomTargetControlType.DropdownList;

    //handler delegates
    this._eventHandler = null;
}
WebExtenders.CustomBehavior.prototype = {
    initialize: function () {
        /// <summary>
        /// Initialize the behavior
        /// </summary>
        WebExtenders.CustomBehavior.callBaseMethod(this, 'initialize');

        var element = this.get_element();

        // setup all of our handlers.
        this._eventHandler = Function.createDelegate(this, this.userActionEvent);

        switch (this._targetControlType) {
            case WebExtenders.CustomTargetControlType.DropdownList:
                $addHandler(element, 'change', this._eventHandler);
                break;
            case WebExtenders.CustomTargetControlType.TextBox:
                $addHandler(element, 'blur', this._eventHandler);
                break;
            case WebExtenders.CustomTargetControlType.RadiobuttonList:
            case WebExtenders.CustomTargetControlType.CheckBox:
            case WebExtenders.CustomTargetControlType.CheckBoxList:
                $addHandler(element, 'click', this._eventHandler);
                break;
        }

        this.onInit();
    },

    dispose: function () {
        /// <summary>
        /// Dispose the behavior
        /// </summary>

        var element = this.get_element();

        if (this._eventHandler) {
            switch (this._targetControlType) {
                case WebExtenders.CustomTargetControlType.DropdownList:
                    $removeHandler(element, 'change', this._eventHandler);
                    break;
                case WebExtenders.CustomTargetControlType.TextBox:
                    $removeHandler(element, 'blur', this._eventHandler);
                    break;
                case WebExtenders.CustomTargetControlType.RadiobuttonList:
                case WebExtenders.CustomTargetControlType.CheckBox:
                case WebExtenders.CustomTargetControlType.CheckBoxList:
                    $removeHandler(element, 'click', this._eventHandler);
                    break;
            }
        }

        WebExtenders.CustomBehavior.callBaseMethod(this, 'dispose');
    },

    onInit: function () {
        var eventArgs = new Sys.CancelEventArgs();
        this.raisepageInit(eventArgs);
        if (eventArgs.get_cancel()) {
            return;
        }
    },

    userActionEvent: function (eventObj) {
        if (eventObj.target.tagName === 'LABEL') {
            return;
        }

        var eventArgs = new Sys.CancelEventArgs();
        this.raiseuserAction(eventArgs);
        if (eventArgs.get_cancel()) {
            return;
        }
    },

    get_TargetControlType: function () {
        /// <value type="String">
        /// ID of the control used to collapse the target when clicked
        /// </value>
        return this._targetControlType;
    },
    set_TargetControlType: function (value) {
        if (this._targetControlType !== value) {
            this._targetControlType = value;
            this.raisePropertyChanged('targetControlType');
        }
    },
    add_pageInit: function (handler) {
        /// <summary>
        /// Add an event handler for the collapsing event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().addHandler('pageInit', handler);
    },
    remove_pageInit: function (handler) {
        /// <summary>
        /// Remove an event handler from the collapsing event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().removeHandler('pageInit', handler);
    },
    raisepageInit: function (eventArgs) {
        /// <summary>
        /// Raise the collapsing event
        /// </summary>
        /// <param name="eventArgs" type="Sys.CancelEventArgs" mayBeNull="false">
        /// Event arguments for the collapsing event
        /// </param>
        /// <returns />

        var handler = this.get_events().getHandler('pageInit');
        if (handler) {
            handler(this, eventArgs);
        }
    },
    add_userAction: function (handler) {
        /// <summary>
        /// Add an event handler for the collapsing event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().addHandler('userAction', handler);
    },
    remove_userAction: function (handler) {
        /// <summary>
        /// Remove an event handler from the collapsing event
        /// </summary>
        /// <param name="handler" type="Function" mayBeNull="false">
        /// Event handler
        /// </param>
        /// <returns />
        this.get_events().removeHandler('userAction', handler);
    },
    raiseuserAction: function (eventArgs) {
        /// <summary>
        /// Raise the collapsing event
        /// </summary>
        /// <param name="eventArgs" type="Sys.CancelEventArgs" mayBeNull="false">
        /// Event arguments for the collapsing event
        /// </param>
        /// <returns />

        var handler = this.get_events().getHandler('userAction');
        if (handler) {
            handler(this, eventArgs);
        }
    }
}
WebExtenders.CustomBehavior.registerClass('WebExtenders.CustomBehavior', Sys.Extended.UI.BehaviorBase);