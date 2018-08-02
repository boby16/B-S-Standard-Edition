var animatControl = function (opt) {
    $.extend(this, opt);
     
    this.mask = {
        defaultClass: 'mask',
        fadeIn: 'ani fadeIn',
        fadeOut: 'fadeOut',
        object: $.cst.charClass + 'mask'
    }
    this.actionEnd = 'webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend';
    this.defaultClass = this.defaultClass || $.cst.empty;

    this.action = this.action || null;
    this.direction = this.direction || null;
    this.target = this.target || null;
    this.trigger = this.trigger || null;
    this.nextPage = this.nextPage || null;
    this.prevPage = this.prevPage || null;
    this.loadAll = this.loadAll || null;
    this.noPage = this.noPage || null;
    if (this.useLastDirection == null) this.useLastDirection = false;
    else this.useLastDirection = true;
    if (this.maskShow == null) this.maskShow = true;
    this.afterFun = this.afterFun || null;
    this.enable = true;
}

animatControl.prototype = {
    constructor: animatControl,
    init: function () {
        var _this = this;
        typeof _this.target == $.cst.object && $(_this.target).length && (_this.target = $(_this.target));
        typeof _this.trigger == $.cst.object && $(_this.trigger).length && (_this.trigger = $(_this.trigger));
        typeof _this.target == $.cst.string && $.object.check(_this.target).isSucceed && (_this.target = $.object.check(_this.target).object);
        typeof _this.trigger == $.cst.string && $.object.check(_this.trigger).isSucceed && (_this.trigger = $.object.check(_this.trigger).object);


        if (typeof _this.target != $.cst.object || !_this.target.length) { alert(_this.cst.info.error.iniationControl); return; }
        //////////////////////////////////////////////////////////////////////////////iniational 
        _this.target.attr($.cst.attr.className) && (!this.defaultClass.length && (this.defaultClass = _this.target.attr($.cst.attr.className)));
        //////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////bind event
        this.action == this.cst.action.open &&
        _this.trigger &&
        (
        _this.trigger.off($.cst.action.touchstart),
        _this.trigger.on($.cst.action.touchstart, function (e) { _this.fadeDown(_this.target, _this.direction); }));

        this.action == this.cst.action.open &&
        !_this.trigger && _this.fadeDown(_this.target, _this.direction);
        ///////////////////////////////////////

        ///////////////////////////////////////bind event
        this.action == this.cst.action.close &&
        _this.trigger &&
        (
        _this.trigger.off($.cst.action.touchstart),
        _this.trigger.on($.cst.action.touchstart, function (e) { _this.fadeUp(_this.target, _this.direction); }));

        this.action == this.cst.action.close &&
        !_this.trigger && _this.fadeUp(_this.target, _this.direction);
        ///////////////////////////////////////

        ///////////////////////////////////////bind event
        !this.direction &&
        _this.trigger &&
        (
        _this.trigger.off($.cst.action.touchstart),
        _this.trigger.on($.cst.action.touchstart, function (e) { _this.fade(_this.target, _this.prevPage ? _this.cst.className.dropOutRight : _this.cst.className.dropOutLeft, _this.prevPage ? _this.prevPage : _this.nextPage); }));
        
        !this.direction &&
        !_this.trigger &&
        _this.fade(_this.target, _this.prevPage ? _this.cst.className.right : _this.cst.className.left, _this.prevPage ? _this.prevPage : _this.nextPage);

        ///////////////////////////////////////
    },
    fadeDown: function () {
        var _target = arguments[0], _class = arguments[1],
            _mask = this.mask, _this = this;
        _mask.object = $(_mask.object);

        
        /////////////////////////////////////////////////////////mask and target add flash

        var _nowClass = _target.get(0).className.match(_class);
        if (_nowClass)
        {
            _this.fadeUp(_target, _this.direction == _this.cst.className.down ? _this.cst.className.up : 'ani fadeOutDown');
            return;
        }

        if (_mask.object.length && _mask.object.get(0).className.match(_this.mask.fadeIn))//current mask show and prevent other mask
            return;
        
        (_target.removeClass(),
        _target.addClass(_this.defaultClass + $.cst.space + _class),
        this.maskShow && _mask.object.removeClass(),
        this.maskShow && _mask.object.addClass(_mask.defaultClass + $.cst.space + _mask.fadeIn));
        
        _mask.object.off($.cst.action.touchend);
        _mask.object.on($.cst.action.touchend, function () {
            _this.fadeUp(_target, _this.direction == _this.cst.className.down ? _this.cst.className.up : 'ani fadeOutDown');
        });

        /////////////////////////////////////////////////////////

        _this.afterFun && typeof _this.afterFun == $.cst.fun && _this.afterFun();
    },
    fadeUp: function () {

        var _target = arguments[0], _class = arguments[1],
             _mask = this.mask, _this = this;
        _mask.object = $(_mask.object);
        /////////////////////////////////////////////////////////mask and target add flash
            (_target.addClass(_class), _mask.object.addClass(_mask.fadeOut));
        /////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////add event when flash over
        _target.one(_this.actionEnd, function () {
            _target.removeClass();
            _target.addClass(_this.defaultClass);
            _target.off($.cst.action.actionEnd);

            _mask.object.removeClass();
            _mask.object.addClass(_mask.defaultClass);
        });

        
        /////////////////////////////////////////////////////////

        _this.afterFun && typeof _this.afterFun == $.cst.fun && _this.afterFun();
    },
    fade: function () {
        if (!this.enable)
            return;
        var _target = arguments[0], _class = arguments[1], _page = arguments[2],
            _this = this,
            _father = _target.closest(_this.cst.className.main),
            _newClass = _this.prevPage ? _this.cst.className.dropInLeft : _this.cst.className.dropInRight;
            _this.enable = false;

        /////////////////////////////////////////////////////////ready page
        !_this.noPage && (_father.addClass(_class), _father.css({ zIndex: 100 }));
        /////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// file entrance
        $.cst.webOpen && !_this.noPage && $.get('..' + _page + '?' + $.cst.version).success(function (content) {
            /////////////////////////////////////////////////////////after success
            var  actionTarget = $(content);
            /////////////////////////////////////////////////////////ready

            content && content.length && actionTarget.addClass(_this.cst.className.active);
            //_this.useLastDirection && result && result.length && actionTarget.addClass(_this.cst.className.active);
            //!_this.useLastDirection && result && result.length && (actionTarget.addClass(_this.cst.className.active), actionTarget.addClass(_newClass));
            /////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////add dom
            !_this.loadAll && actionTarget.length && $($.cst.html.body).append(actionTarget);
            _this.loadAll && actionTarget.length && $.each(actionTarget, function () {                
                if (this.className && this.className.match($.cst.mainDiv)) {
                    $($.cst.html.body).append($(this));
                    return false;
                }
            });
            /////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////add event when flash over
            _father.one(_this.actionEnd, function () { _target.off($.cst.action.touchstart); _father.remove(); $.iniatial.height(); _this.enable = true; });
            /////////////////////////////////////////////////////////
        });

        !$.cst.webOpen && _this.getFile(_page, _target, _father);
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////ready close
        _this.noPage && _target.addClass(_this.cst.className.close);
        _this.noPage && _target.one(_this.actionEnd, function () { $(this).removeClass(_this.cst.className.close); $(this).hide(); });
        /////////////////////////////////////////////////////////

        _this.afterFun && typeof _this.afterFun == $.cst.fun && _this.afterFun();
    },
    getFile: function () {
        var fileName = arguments.length ? arguments[0] : $.cst.empty,
            isAndroid = device.platform && device.platform.toLowerCase() == "android" ? true : false,
            _this = this, _target = arguments[1], _father = arguments[2];

        if (!$.cookie.get($.cst.cookie.backPage)) {
            var _null = [];
            _null.push($.cst.page.login);
            $.cookie.set($.cst.cookie.backPage, _null.join(','));
        }
        else {
            var _curr = $.cookie.get($.cst.cookie.backPage),
                _curr = _curr.split(',');
            _curr.push(fileName);
            $.cookie.set($.cst.cookie.backPage, _curr.join(','));
        }

            !isAndroid && window.resolveLocalFileSystemURL(cordova.file.applicationDirectory + "www" + fileName, function (fileEntry) {
                fileEntry.file(function (file) {
                    var reader = new FileReader();
                    reader.onloadend = function (e) {
                        var content = this.result, actionTarget = $(content);

                        /////////////////////////////////////////////////////////ready
                        content && content.length && actionTarget.addClass(_this.cst.className.active);
                        /////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////add dom
                        !_this.loadAll && actionTarget.length && $($.cst.html.body).append(actionTarget);
                        _this.loadAll && actionTarget.length && $.each(actionTarget, function () {
                            if (this.className && this.className.match($.cst.mainDiv)) {
                                $($.cst.html.body).append($(this));
                                return false;
                            }
                        });
                        /////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////add event when flash over
                        _father.one(_this.actionEnd, function () { _target.off($.cst.action.touchstart); _father.remove(); $.iniatial.height(); _this.enable = true; });
                        ////////////////////////////////////
                    }
                    reader.readAsText(file);
                });
            }, function (error) { alert('faile!'); });


        if (isAndroid) {
            content = window._cordovaNative.readAssetString("www" + fileName);

            /////////////////////////////////////////////////////////after success
            var actionTarget = $(content);
            /////////////////////////////////////////////////////////ready

            content && content.length && actionTarget.addClass(_this.cst.className.active);
            /////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////add dom
            !_this.loadAll && actionTarget.length && $($.cst.html.body).append(actionTarget);
            _this.loadAll && actionTarget.length && $.each(actionTarget, function () {
                if (this.className && this.className.match($.cst.mainDiv)) {
                    $($.cst.html.body).append($(this));
                    return false;
                }
            });
            /////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////add event when flash over
            _father.one(_this.actionEnd, function () { _target.off($.cst.action.touchstart); _father.remove(); $.iniatial.height(); _this.enable = true; });
            /////////////////////////////////////////////////////////
        }
    },
    cst: {
        fadeDown: 'fadeDown',
        fadeUp: 'fadeUp',
        fadeLeft: 'fadeLeft',
        fadeRight: 'fadeRight',
        className: {
            down: 'ani fadeInDown',
            up: 'ani fadeOutUp',
            left: 'ani dropLeft',
            dropInRight: 'ani dropInRight',
            dropOutLeft: 'ani dropOutLeft',
            right: 'ani dropRight',
            dropInLeft: 'ani dropInLeft',
            dropOutRight: 'ani dropOutRight',
            maskFade: '',
            main: '.mainContent',
            active: 'active',
            close: 'ani close'
        },
        action: { click: 'click', open: 1, close: 2 },
        info: {
            error: {
                iniationControl: 'iniational control failed!'
            }
        },
        regular: {
            body: /[<]body[>][\w\W]+[</]body[>]/ig
        }
    }
}
 

function gotFile(fileEntry) {

    fileEntry.file(function (file) {
        var reader = new FileReader();

        reader.onloadend = function (e) {
            alert(this.result);
        }

        reader.readAsText(file);
    });

}