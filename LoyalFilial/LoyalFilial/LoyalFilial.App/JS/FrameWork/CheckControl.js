var checkControl = function () {
    this.template = {
        common: '<div class="error_msg">${Contents}</div>',
        input: 'input[name=${Name}]',
        popup: '<div class="error_msg" style="padding-left:${Left}px">${Contents}</div>'
    },
    this.emptyAttr = 'msg',
    this.class = {
        parentDefault: 'type_in_box',
        parentError: 'error',
        itemError: '.error_msg',
        parentAttr: 'fatherClass'
    },
    this.emptyAttrs = '[msg]',
    this.rulesAttr = 'rule',
    this.rulesMsgAttr = 'msgRule',
    this.controls = ['address', 'calendar'],
    this.checkFather = this.checkFather || null;
};

checkControl.prototype = {
    constructor: checkControl,
    init:function(){
        if (this.checkFather && $.object.check(this.checkFather).isSucceed) {
            this.target = $.object.check(this.checkFather).object;
            return this.check();
        }
        return false;
    },
    check: function () {
        var _this = this,
            _flag = true;
        this.target.find(_this.emptyAttrs).each(function (i) {
            var thisItem=$(this),
                everyItem = _this.checkItemEmpty(thisItem);
            !everyItem.isSucceed && _this.addErrorClass(thisItem, everyItem.errorMsg) && _this.bindEvent(thisItem, this.tagName.toLowerCase());

            !everyItem.isSucceed && _flag && (_flag = false);

            !i && thisItem.focus();
        });

        return _flag;
    },
    checkItemEmpty: function () {
        var tagert = arguments[0],
            value = $.value.get(tagert),
            empty = $.value.check.isEmpty(value),
            defaultReturn = { isSucceed: false, errorMsg: $.cst.empty };

        empty && (defaultReturn.errorMsg = tagert.attr(this.emptyAttr));

        !empty && typeof tagert.attr(_this.rulesAttr) != $.cst.string && (defaultReturn.isSucceed = true);

        !empty && typeof tagert.attr(_this.rulesAttr) == $.cst.string && (defaultReturn = this.checkItemRule(tagert.attr(_this.rulesAttr), value,tagert));

        return defaultReturn;
    },
    checkItemRule: function () {
        var rule = arguments[0],
            value = arguments[1],
            tagert=arguments[2],
            defaultReturn = { isSucceed: true, errorMsg: $.cst.empty };

        !this.getRegex(rule).test(value) && (defaultReturn.isSucceed = false, defaultReturn.errorMsg = tagert.attr(this.rulesMsgAttr));

        return defaultReturn;         
    },
    bindEvent: function () {
        var _this = this,
            target = arguments[0],
            tagName = arguments[1],
            type = target.attr($.cst.attr.type),
            control = target.data($.cst.attr.role);

        for (; this.controls.length;) {
            if (control && control.toLowerCase() == this.controls.shift()) {
                target.bind($.cst.action.change, function () {
                    var thisItem = $(this),
                    defaultReturn = _this.checkItemEmpty(target);
                    defaultReturn.isSucceed && _this.removeErrorClass(target);
                    !defaultReturn.isSucceed && _this.addErrorClass(target, defaultReturn.errorMsg);
                });
                return;
            }
        }

        if (tagName == $.cst.html.select || typeof target.attr($.cst.html.readonly) == $.cst.string) {
            target.bind($.cst.action.change, function () {
                var thisItem = $(this),
                defaultReturn = _this.checkItemEmpty(target);
                defaultReturn.isSucceed && _this.removeErrorClass(target);
                !defaultReturn.isSucceed && _this.addErrorClass(target, defaultReturn.errorMsg);
            });
            return;
        }

        if (tagName == $.cst.html.input && (type == $.cst.html.radio || type == $.cst.html.checkbox)) {
            $($.template.render(_this.template.input, { Name: target.attr($.cst.attr.name) })).each(function () {
                $(this).bind($.cst.action.click, function () {
                    var thisItem = $(this),
                    defaultReturn = _this.checkItemEmpty(target);
                    defaultReturn.isSucceed && _this.removeErrorClass(target);
                    !defaultReturn.isSucceed && _this.addErrorClass(target, defaultReturn.errorMsg);
                });
            });
            return;
        }

        target.bind($.cst.action.keyup, function () {
            var thisItem = $(this),
                defaultReturn = _this.checkItemEmpty(thisItem);
            defaultReturn.isSucceed && _this.removeErrorClass(thisItem);
            !defaultReturn.isSucceed && _this.addErrorClass(thisItem, defaultReturn.errorMsg);
        });
    },
    addErrorClass: function () {
        var _this = this,
            target = arguments[0],
            errorMsg = arguments[1],
            className = (typeof target.attr(_this.class.parentAttr) == $.cst.string) ? target.attr(_this.class.parentAttr) : _this.class.parentDefault,
            parent = target.closest($.cst.charClass + className),
            popup = parent.length ? parent.closest($.cst.popup) : null,
            error = parent.length ? parent.find(_this.class.itemError) : null,
            template = parent.length && popup && popup.length ? $.template.render(_this.template.popup, { Contents: errorMsg, Left: (target.offset().left - popup.offset().left - 15) }) : $.cst.empty;

        parent.length && popup && !popup.length && (template = $.template.render(_this.template.common, { Contents: errorMsg }));

        error.length && error.remove();

        !error.length && parent.addClass(_this.class.parentError);

        parent.length && parent.append(template);
    },
    removeErrorClass: function () {
        var target = arguments[0],
            className = (typeof target.attr(_this.class.parentAttr) == $.cst.string) ? target.attr(_this.class.parentAttr) : _this.class.parentDefault,
            parent = target.closest($.cst.charClass + className),
            item = parent.length ? parent.find(_this.class.itemError) : null;
        parent.length && parent.removeClass(_this.class.parentError);
        item.length && father.remove();
    },
    getRegex: function (nameRegex) {
        switch (nameRegex.toLowerCase()) {
            case "int": return /^\d{0,10}$/;
            case "float": return /^\d{0,10}(\.\d{0,4}){0,1}$/;
            case "mail": return /^\s*([A-Za-z0-9_-]+(\.\w+)*@(\w+\.)+\w{2,5})\s*$/;
            case "date": return /^\d{4}\-\d{1,2}-\d{1,2}$/;
            case "telephone": return /^[-|\d]{3,15}$/;
            case "mobile": return /^1\d{10}$/;
            default: return eval(nameRegex.toLowerCase());
        }
    }
}