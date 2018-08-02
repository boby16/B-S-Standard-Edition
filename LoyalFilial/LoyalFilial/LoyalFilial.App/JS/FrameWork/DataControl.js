var dataControl = function (opt) {
    $.extend(this, opt);
    this.postId = this.postId || null,
    this.postUrl = this.postUrl || null,
    this.dataJson = this.dataJson || null,
    this.triggerId = this.triggerId || null,
    this.checkFather = this.checkFather || null,
    this.page = this.page || null,
    this.pageSize = this.pageSize || 10,
    this.pageIndex = this.pageIndex || 1,
    this.confirmAction = this.confirmAction || true,
    this.afterFun = this.afterFun || null,
    this.loadObject = this.loadObject || null;
    if (this.needLoop == null) this.needLoop = true;
    if (this.needLoading == null) this.needLoading = true;
};
var dataControlQuenen = [];

dataControl.prototype = {
    constructor: dataControl,
    init: function () {
        var _this = this,
            _trigger = $.object.check(_this.triggerId);
        _trigger.isSucceed && _trigger.object.unbind($.cst.action.click) && _trigger.object.bind($.cst.action.click, function () {

            _this.page && (_this.confirmAction = true, _this.pageIndex = 1);

            _this.confirm();
        });
        !_trigger.isSucceed && _this.confirm();
    },
    confirm: function () {
        
        var _this = this,
            currentPost = null;
        if (!$.cst.webOpen && !$.checkConnection()) {
            $.alert(_this.cst.info.theNetConnection); return;
        }        

        (typeof checkControl == $.cst.undefined) && $.object.create($.cst.control.checkControl);

        if (this.checkFather != null && $.id(this.checkFather) != null && !$.checkValidationByFather(this.checkFather).IsSucceed) { return false; }

        if (this.checkFather && (typeof checkControl == $.cst.undefined) && $.object.create($.cst.control.checkControl)) {
            var _controlCheck = new checkFather({ checkFather: _this.checkFather });
            if (!_controlCheck.init()) return false;
        }

        var _data = this.page ? { pageSize: _this.pageSize, pageIndex: _this.pageIndex } : {};
        _this.postId && $.each(_this.get.jsonFromPostId(_this.postId, _this), function (i, item) { _data[i] = item; });
        _this.dataJson && typeof _this.dataJson == $.cst.object && $.each(_this.dataJson, function (i, item) { if (!_data.hasOwnProperty(i)) _data[i] = item; });
        _this.dataJson && typeof _this.dataJson == $.cst.string && $.each(_this.get.jsonFromData(_this.dataJson), function (i, item) { if (!_data.hasOwnProperty(i)) _data[i] = item; });

        this.postUrl &&
        (currentPost = $.ajax({
            type: $.cst.actionType.post,
            data: _data,
            url: $.cst.web + _this.postUrl,
            dataType: _this.cst.josn.jsonp,
            jsonpCallback: _this.cst.josn.jsonpFunction,
            timeout: 20000,
            complete: function (XMLHttpRequest, status) {
                debugger;
                if (status == 'timeout' || status == 'error') {
                    _this.needLoading && _this.clearLoadHtml();
                    $.alert(_this.cst.info.theNetConnection);
                    currentPost && currentPost.abort();
                }                
            },
            cache: false,
            beforeSend: function () {
                _this.needLoading && !dataControlQuenen.length && _this.creatLoadHtml();
                dataControlQuenen.push(_this);
            },
            success: function () {
                _this.needLoading && _this.clearLoadHtml();

                if (arguments.length) {
                    var msg = arguments[0];

                    if (msg.Code && msg.Code == -99) {
                        $.alert(_this.cst.info.theSameAccount);
                        var __t = window.setTimeout(function () {
                            $.animateManager.init({ target: $.cst.mainDiv, nextPage: $.cst.page.login, loadAll: true });
                            window.clearTimeout(__t);
                        }, 3000);
                        return;
                    }

                    if (_this.needLoop) {
                        try {
                            typeof msg == $.cst.string ? _this.set(jQuery.parseJSON(jQuery.parseJSON(msg).Message)) : _this.set(jQuery.parseJSON(msg.Message));
                        } catch (e) { }
                    }

                    _this.afterFun && typeof _this.afterFun == $.cst.fun && _this.afterFun(msg);

                    if (_this.page && $.object.check(_this.page).isSucceed) {

                        (typeof pageControl == $.cst.undefined) && $.object.create($.cst.control.pageControl);

                        var total = $.value.getJson(msg, _this.cst.josn.recordCount),
                            pageCount = Math.ceil(total / _this.pageSize),
                            para = { total: total, pageNum: _this.pageSize, action: _this, index: (_this.pageIndex > pageCount ? pageCount : _this.pageIndex), page: _this.page },
                            _pageControl = new pageControl(para);

                        _pageControl.init();
                    }
                }
                dataControlQuenen.shift();
            },
            error: function (data) {
                debugger;
                _this.needLoading && _this.clearLoadHtml();
                $.alert(_this.cst.info.theNetConnection);
                dataControlQuenen.shift();                
            }
        }));

    },
    set: function (json, father) {
        var _this = this;
        $.each(json, function (i, item) {
            switch (typeof i) {

                case $.cst.number: {
                    var _loop = $.object.check(father);
                    if (_loop.isSucceed) {
                        var tagName = _loop.object.get(0).tagName.toLowerCase(),
                            _loopItem = _loop.object.find(_this.cst.subItem[tagName]).eq(i),
                            _html = _loopItem.html(), _key = _html.match(eval(_this.cst.regDataAll)), _item = this;
                        
                        for (; _key && _key.length;) {
                            var __key = _key.shift().replace(eval(_this.cst.begin), $.cst.empty);
                            _html = _html.replace(eval($.template.render(_this.cst.regData, { Data: __key })), _item[__key]);
                        }

                        $.value.set(_loopItem, _html);

                        _loopItem.find(_this.cst.subRole.role).each(function () { $.value.set(this, _item[$(this).data($.cst.attr.role)]); });
                    }
                }; break;
                case $.cst.string: {
                    if (typeof item == $.cst.object) {
                        var _target = $.object.check(i),
                            _loopFlag = false;

                        _target.isSucceed && !$.cookie.get($.cst.attr.template + _target.object.get(0).id) && _this.get.template(i, _this) && $.cookie.set($.cst.attr.template + _target.object.get(0).id, _this.get.template(i, _this));

                        if (_target.isSucceed && $.cookie.get($.cst.attr.template + _target.object.get(0).id) && _this.get.templateClear(_target.object, _this)) {
                            for (var j = 0; j < item.length; j++) _target.object.append($.cookie.get($.cst.attr.template + _target.object.get(0).id));

                            !item.length && _this.get.templateNone(_target.object, _this);

                            _loopFlag = true;
                        }
                        _target.isSucceed && _loopFlag && item.length && _this.set(item, i);
                        _target.isSucceed && !_loopFlag && $.value.setJosn(i, item);
                    }
                    else typeof item != $.cst.object && $.value.set(i, item);
                }; break;
            }
        });
    },
    get: {
        jsonFromPostId: function () {
            var idArray = arguments[0].split($.cst.charSplit),
                valueArray = {},
                _this = arguments[1];

            for (; idArray.length;) {
                var _idArray = idArray.shift().split(_this.cst.find),
                    _sonSet = _idArray.pop();
                (_sonSet == _this.cst.all) && $.each($.object.createJson(_idArray.pop()), function (i, item) { if (!valueArray.hasOwnProperty(i)) valueArray[i] = item; });
                (_sonSet != _this.cst.all) && $.object.check(_sonSet).isSucceed && (valueArray[_sonSet] = $.value.get(_sonSet));
            }
            return valueArray;
        },
        jsonFromData: function () {
            try {
                var data = jQuery.parseJSON(arguments[0]), tempJson = {};
                $.each(data, function (i, item) {
                    tempJson[i] = item;
                });
                return tempJson;
            }
            catch (e) {
                return {};
            }
        },
        template: function () {
            var target = $.object.check(arguments[0]);

            if (!target.isSucceed) return $.cst.empty;

            var target = target.object,
                tagName = target.get(0).tagName.toLowerCase(),
                _this = arguments[1],
                _return = $.cst.empty;
            _this.cst.subItem.hasOwnProperty(tagName) && (_return = target.find(_this.cst.subItem[tagName]).first().show());

            _return && _return.length && (_return = _return.prop($.cst.attr.outerHTML));
            return _return;
        },
        templateClear: function () {
            var target = arguments[0],
                _this = arguments[1],
                tagName = target.get(0).tagName.toLowerCase(),
                subItem = target.find(_this.cst.subItem[tagName]);
            if (subItem.length) { subItem.remove(); return true; }
            return false;
        },
        templateNone: function () {
            var target = arguments[0],
                _this = arguments[1],
                tagName = target.get(0).tagName.toLowerCase();
            switch (tagName) {
                case $.cst.html.table: target.append($.template.render(_this.cst.subItemNone.table, { colspan: target.find(_this.cst.subItemHead.table).length })); break;
                case $.cst.html.ul: target.append(_this.cst.subItemNone.ul); break;
                case $.cst.html.div: target.append(_this.cst.subItemNone.div); break;
            }
        }
    },
    creatLoadHtml: function () {
        var filter = document.createElement($.cst.html.div);
        $(filter).find($.cst.html.img).css({
            top: ($(window).height() / 2),
            left: ($(window).width() / 2)
        });
        $(filter).append($(this.cst.template.mask).show());
        document.body.appendChild(filter);
        this.loadObject = filter;
    },
    clearLoadHtml: function () {
        var _this = this,
            _actual = $(this.loadObject).children().first();
        this.loadObject && (_actual.addClass(_this.cst.fadeOut), _actual.one(_this.cst.actionEnd, function () {
            $(this).parent().remove();
        }));
    },    
    cst: {
        find: '->',
        all: '*',
        fadeOut: 'fadeOut',
        begin: '/\\$/g',
        subItem: { table: 'tbody tr', ul: 'li', div: 'div[class="contact clear"]' },
        subItemHead: { table: 'thead th' },
        subItemNone: { table: '<tr><td colspan="${colspan}" align="center"  style="font-size:1.4rem;">对不起，未查找到任何相关数据！</td></tr>', ul: '<li style="text-align:center;font-size:1.4rem;">对不起，未查找到任何相关数据！</li>', div: '<div class="contact clear" style="text-align:center;font-size:1.4rem;">对不起，未查找到任何相关数据！</div>' },
        subRole: { role: '[data-role]' },
        regData: '/\\$${Data}\\$/g',
        regDataAll: '/\\$[^\\$]+(?:\\$)/g',
        josn: {
            recordCount: 'RecordCount',
            jsonpFunction: 'josnpCallbackFunctionInWCF',
            jsonp: 'jsonp'
        },
        template: {
            mask: '<div class="mask loading ani fadeIn"><img src="../Images/loading.gif" /></div>'
        },
        actionEnd: 'webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend',
        info: {
            theSameAccount: '此帐号已在别处登录！',
            theNetConnection: '网络不给力,请稍候再试！'
        }
    }
};

function josnpCallbackFunctionInWCF() { }