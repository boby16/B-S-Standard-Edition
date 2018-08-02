(function ($, undefined) { 
     
    $.extend(
        {
            businessManager: {
                init: function () {
                    (typeof businessControl == $.cst.undefined) && $.object.create($.cst.control.businessControl);
                    var _control = arguments.length ? new businessControl(arguments[0]) : new businessControl();
                    _control.init(); return _control;
                }
            },
            animateManager: {
                init: function () {
                    (typeof animatControl == $.cst.undefined) && $.object.create($.cst.control.animatControl);
                    var _control = arguments.length ? new animatControl(arguments[0]) : new animatControl();
                    _control.init(); return _control;
                }
            },
            alert: function () {
                var msgContent = arguments.length ? arguments[0] : $.cst.empty,
                    template = $.template.render($.cst.template.alert, { Msg: msgContent }),
                    obj = $(template),
                    time = arguments.length > 1 ? arguments[1] : 1500;
                $.cls($.cst.mainDiv).append(obj);
                window.setTimeout(function () {
                    obj.addClass($.cst.cls.fadeOut);
                    obj.one($.cst.action.animateEnd, function () { obj.remove(); });
                }, time);
            },
            value: {
                get: function () {
                    if (arguments.length == 1) {
                        var object = $.object.check(arguments[0]),
                            taget = object.object;
                        if (!object.isSucceed) return $.cst.empty;

                        var tagName = taget[0].tagName.toLowerCase(),
                            type = taget.attr($.cst.attr.type) ? taget.attr($.cst.attr.type).toLowerCase() : $.cst.empty,
                            object = null;

                        switch (tagName) {
                            case $.cst.html.input: {
                                switch (type) {
                                    case $.cst.html.radio: {
                                        var _taget = $.template.render($.cst.template.radioYes, { Name: taget.attr($.cst.attr.name) });
                                        return $(_taget).length ? $(_taget).val() : $.cst.empty;
                                    } break;
                                    case $.cst.html.checkbox: {
                                        var _taget = $.template.render($.cst.template.checkboxYes, { Name: taget.attr($.cst.attr.name) }),
                                            _value = [];
                                        $(_taget).each(function () { _value.push($(this).val()) });
                                        return $(_taget).length ? _value.join($.cst.charSplit) : $.cst.empty;
                                    } break;
                                    default: {
                                        var _value = taget.val();
                                        if ((taget.data($.cst.attr.role) && taget.data($.cst.attr.role) == $.cst.control.notice) || (taget.data($.cst.attr.lang) && taget.data($.cst.attr.lang) == $.cst.control.notice)) _value = _value.replace(taget.data($.cst.control.notice), $.cst.empty);
                                        return _value;
                                    } break;
                                }
                            }
                            case $.cst.html.select: return taget.val(); break;
                            case $.cst.html.textarea: {
                                var _value = taget.val();
                                if ((taget.data($.cst.attr.role) && taget.data($.cst.attr.role) == $.cst.control.notice) || (taget.data($.cst.attr.lang) && taget.data($.cst.attr.lang) == $.cst.control.notice)) _value = _value.replace(taget.data($.cst.control.notice), $.cst.empty);
                                return _value;
                            } break;
                            default: return taget.html(); break;
                        }
                    } else return $.cst.empty;
                },
                set: function () {
                    if (arguments.length == 2) {
                        var object = $.object.check(arguments[0]),
                            taget = object.object,
                            flag = false;
                        if (!object.isSucceed) return flag;

                        var tagName = taget[0].tagName.toLowerCase(),
                            type = taget.attr($.cst.attr.type) ? taget.attr($.cst.attr.type).toLowerCase() : $.cst.empty,
                            value = arguments[1],
                            object = null;
                        switch (tagName) {
                            case $.cst.html.input: {
                                switch (type) {
                                    case $.cst.html.radio: {
                                        var _taget = $.template.render($.cst.template.radio, { Name: taget.attr($.cst.attr.name) });
                                        $(_taget).each(function () {
                                            if ($(this).val() == value) { this.checked = true; flag = true; return false; }
                                        });
                                    } break;
                                    case $.cst.html.checkbox: {
                                        var name = taget.attr($.cst.attr.name) ? taget.attr($.cst.attr.name) : $.cst.empty,
                                            value = value.toString().split($.cst.charSplit),
                                            _taget = $.template.render($.cst.template.checkbox, { Name: name });

                                        $(_taget).removeAttr($.cst.attr.checked);

                                        for (; value.length;) {
                                            var _value = value.shift(),
                                                _taget = $.template.render($.cst.template.checkboxNo, { Name: name });

                                            $(_taget).each(function (i) {
                                                if ($(this).val() == _value) { this.checked = true; flag = true; return false; }
                                            });
                                        }
                                    } break;
                                    default: { taget.val(value); flag = true; } break;
                                }
                            } break;
                            case $.cst.html.select: { taget.val(value); flag = true; } break;
                            case $.cst.html.textarea: { taget.val(value); flag = true; } break;
                            default: { taget.html(value); flag = true; } break;
                        }

                        ((taget.data($.cst.attr.role) && taget.data($.cst.attr.role) == $.cst.control.notice) || (taget.data($.cst.attr.lang) && taget.data($.cst.attr.lang) == $.cst.control.notice)) && taget.css({ color: $.cst.empty });

                        return flag;

                    } else return false;
                },
                getJson: function () {
                    var value = $.cst.empty,
                        json = arguments.length ? arguments[0] : null,
                        key = arguments.length > 1 ? arguments[1] : null;
                    try {
                        typeof json == $.cst.string && (json = jQuery.parseJSON(json));

                        json.hasOwnProperty(key) && (value = json[key]);
                    }
                    catch (e) { }
                    finally { return value; }
                },
                setJosn: function () {
                    if (arguments.length == 2) {
                        try {
                            var key = arguments[0], value = typeof arguments[1] == $.cst.string ? jQery.parseJSON(arguments[1]) : arguments[1];

                            value.length && $.each(value, function (i, item) {
                                $.each(item, function (_i, _item) {
                                    var father = $.object.check(key),
                                        target = father.isSucceed ? father.object.find($.template.render($.cst.template.name, { Name: _i })) : null;

                                    target && target.length && i <= target.length && $.value.set(target.eq(i), _item);
                                });
                            });
                            !value.length && $.each(value, function (i, item) { $.value.set(i, item); });
                        }
                        catch (e) { }
                    }
                },
                check: {
                    isEmpty: function () {
                        var value = arguments.length ? (typeof arguments[0] == $cst.string ? arguments[0] : $.value.get(arguments[0])) : $.cst.empty;
                        return !jQuery.trim(value).length ? true : false;
                    }
                }
            },
            dataManager: {
                post: function () {
                    (typeof dataControl == $.cst.undefined) && $.object.create($.cst.control.dataControl);
                    var _control = arguments.length ? new dataControl(arguments[0]) : new dataControl();
                    _control.init(); return _control;
                }
            },
            object: {
                check: function () {
                    var defaultReturn = { isSucceed: false, object: null };
                    if (!arguments.length ||
                        (typeof arguments[0] == $.cst.string && (!$.id(arguments[0]) && !$.cls(arguments[0]))) ||
                        (typeof arguments[0] == $.cst.object && !$(arguments[0]).length)) return defaultReturn;

                    if (typeof arguments[0] == $.cst.string) { defaultReturn.isSucceed = true; defaultReturn.object = $.id(arguments[0]) ? $.id(arguments[0]) : $.cls(arguments[0]); }
                    else if (typeof arguments[0] == $.cst.object) { defaultReturn.isSucceed = true; defaultReturn.object = $(arguments[0]); }
                    return defaultReturn;
                },
                create: function () {
                    var script = document.createElement($.cst.script);
                    script.src = $.cst.url[arguments[0]];
                    $($.cst.html.head).append(script);
                },
                createJson: function () {
                    var father = arguments.length ? $.object.check(arguments[0]) : null,
                        returnJosn = {};
                    if (!father || !father.isSucceed) return returnJosn;
                    father.object.find($.cst.json.flag).each(function () {
                        returnJosn[this.id] = $.value.get(this);
                    });
                    return returnJosn;
                },
                createJsonList: function () {
                    var father = arguments.length ? $.object.check(arguments[0]) : null,
                        fatherName = arguments.length == 2 ? arguments[1] : (arguments.length ? arguments[0] : null),
                        returnJosn = [], returnObj = {}, returnJosnString = [];
                    if (!father || !fatherName || !father.isSucceed) return returnJosn;

                    father.object.find($.cst.json.flagList).each(function () {
                        var _this = $(this),
                            _name = _this.attr($.cst.attr.name),
                            _value = $.value.get(this),
                            flag = true;

                        if (_this.attr($.cst.json.flagFilter) && !_this.attr($.cst.json.flagFilter).length) return true;

                        $.each(returnJosn, function (i, item) {
                            if (!this.hasOwnProperty(_name)) { this[_name] = _value; flag = false; return false; }
                        });

                        if (flag) { var subJson = {}; subJson[_name] = _value; returnJosn.push(subJson); }
                    });


                    $.each(returnJosn, function (i, item) {
                        var _this = this, tempArry = Object.keys(_this), every = [];
                        for (; tempArry.length;) {
                            var _key = tempArry.shift();
                            every.push($.template.render($.cst.template.jsonItem, { Key: _key, Value: _this[_key] }));
                        }
                        returnJosnString.push($.template.render($.cst.template.jsonEveryItem, { Json: every.join($.cst.charSplit) }));
                    });
                    returnObj[fatherName] = $.template.render($.cst.template.jsonArrayItem, { Json: returnJosnString.join($.cst.charSplit) });
                    return returnObj;
                }
            },
            id: function () {
                return arguments.length && $($.cst.charBeginWith + arguments[0]).length ? $($.cst.charBeginWith + arguments[0]) : null;
            },
            cls: function () {
                return arguments.length && $($.cst.charClass + arguments[0]).length ? $($.cst.charClass + arguments[0]) : null;
            },
            iniatial: {
                height: function () {
                    $.cls($.cst.mainDiv).css({ height: $(window).height() });
                }
            },
            cookie: {
                set: function () {
                    if (arguments.length < 2) return false;
                    var _cookieName = arguments[0],
                        _cookieValue = arguments[1];
                    if (arguments.length >= 3)
                        _foreverFlag = arguments[2];
                    if (_foreverFlag == 1)
                        window.localStorage.setItem(_cookieName, _cookieValue);
                    else
                        window.sessionStorage.setItem(_cookieName, _cookieValue);
                    return true;
                }, 
                del: function () {
                    if (arguments.length < 1) return false;
                    var _cookieName = arguments[0];
                    window.localStorage.removeItem(_cookieName);
                    window.sessionStorage.removeItem(_cookieName);
                    return true;
                },
                get: function () {
                    if (arguments.length < 1) return null;
                    var _cookieName = arguments[0],
                        _return = window.localStorage.getItem(_cookieName);
                    if (typeof (_return) == "undefined" || _return == null || _return == "") {
                        _return = window.sessionStorage.getItem(_cookieName);
                    }
                    return _return ? _return : null;
                }
            },
            session: {
                set: function () {
                    if (arguments.length < 2) return false;
                    var _sessionName = arguments[0],
                        _sessionValue = arguments[1];
                    window.sessionStorage.setItem(_sessionName, _sessionValue);
                    return true;
                },
                del: function () {
                    if (arguments.length < 1) return false;
                    var _sessionName = arguments[0];
                    window.sessionStorage.removeItem(_sessionName);
                    return true;
                },
                get: function () {
                    if (arguments.length < 1) return null;
                    var _sessionName = arguments[0],
                        _return = window.sessionStorage.getItem(_sessionName);
                    return _return ? _return : null;
                }
            },
            checkConnection: function () {
                var networkState = navigator.connection.type;
                var states = {};
                states[Connection.UNKNOWN] = 'Unknown connection';
                states[Connection.ETHERNET] = 'Ethernet connection';
                states[Connection.WIFI] = 'WiFi connection';
                states[Connection.CELL_2G] = 'Cell 2G connection';
                states[Connection.CELL_3G] = 'Cell 3G connection';
                states[Connection.CELL_4G] = 'Cell 4G connection';
                states[Connection.CELL] = 'Cell generic connection';
                states[Connection.NONE] = null;
                return states[networkState];
            },
            clearCache: function () {
                var keys = Object.keys($.cst.cache),
                    _key;
                for (; keys.length;) {
                    _key = keys.shift(); $.cookie.del($.cst.cache[_key]);
                }
                $.cookie.del($.cst.cookie.inputValue);
            },
            
            showPopup: function (popupDiv, closeBtn) {
                var bh = $("body").height(),
                    bw = $("body").width(),
                    popDiv = $("#" + popupDiv),
                    closeBtnObj = $("#" + closeBtn);
                popDiv.find(".pop_fullbg").css({
                    height: bh,
                    width: bw,
                    display: "block"
                });
                popDiv.find(".pop_dialog").show();


                popDiv.find(".pop_close").unbind("click");
                popDiv.find(".pop_close").click(function () {
                    popDiv.find(".pop_fullbg,.pop_dialog").hide();
                });
                closeBtnObj.unbind("click");
                closeBtnObj.click(function () {
                    popDiv.find(".pop_fullbg,.pop_dialog").hide();
                });
            },
            cst: {
                action: { click: 'click', touchstart: 'touchstart', touchend: 'touchend', touchmove: 'touchmove', change: 'change', keyup: 'keyup', animateEnd: 'webkitAnimationEnd oAnimationEnd MSAnimationEnd animationend' },
                actionType: { post: 'post', get: 'get' },
                attr: { name: 'name', type: 'type', checked: 'checked', role: 'role', src: 'src', lang: 'lang', outerHTML: 'outerHTML', template: 'template', readonly: 'readonly', className: 'class' },
                boolean: 'boolean',
                br: '<br/>',
                cls: {
                    fadeOut: 'ani fadeOut'
                },
                charBeginWith: '#',
                charSplit: ',',
                charClass: '.',
                control: { animatControl: 'animatControl', checkControl: 'checkControl', dataControl: 'dataControl', notice: 'notice', pageControl: 'pageControl' },
                cookie: {
                    accountId: 'loyalFilialAppAccount',
                    userType: 'loyalFilialAppUserType',
                    token: 'loyalFilialAppToken',
                    orderId: 'loyalFilialOrderId',
                    noticeId: 'loyalFilialNoticeId',
                    vendorName: 'vendorName',
                    refundMoney: 'refundMoney',
                    waitOrderState: 'waitOrderState',
                    noticeCurrent: 'noticeCurrent',
                    backPage: '/Index/Login.html',
                    backAction: 'backAction',
                    backTime: '0',
                    listPosition: 'listPosition',
                    inputValue: 'inputValue',
                    orderState: 'orderState',
                    sleepAction: 'sleepAction',
                    resumeAction: 'resumeAction'
                },
                cache:{
                    messageList: 'cache_messageList',
                    messageOver: 'cache_messageOver',
                    messageIndex: 'cache_messageIndex',
                    notificationList: 'cache_notificationList',
                    notificationOver: 'cache_notificationOver',
                    notificationIndex: 'cache_notificationIndex',
                    indexSeven: 'cache_indexSeven',
                    indexOneMonth: 'cache_indexOneMonth',
                    indexThrMonth: 'cache_indexThrMonth',

                    orderTap: 'cache_orderTap',
                    orderCondtionTap: 'cache_orderCondtionTap',
                    orderAll: 'cache_orderAll',
                    orderAllOver: 'cache_orderAllOver',
                    orderAllIndex: 'cache_orderAllIndex',
                    orderWait: 'cache_orderWait',
                    orderWaitOver: 'cache_orderWaitOver',
                    orderWaitIndex: 'cache_orderWaitIndex',
                    orderCancle: 'cache_orderCancle',
                    orderCancleOver: 'cache_orderCancleOver',
                    orderCancleIndex: 'cache_orderCancleIndex',
                    orderPays: 'cache_orderPays',
                    orderPaysOver: 'cache_orderPaysOver',
                    orderPaysIndex: 'cache_orderPaysIndex',
                    orderDelivory: 'cache_orderDelivory',
                    orderDelivoryOver: 'cache_orderDelivoryOver',
                    orderDelivoryIndex: 'cache_orderDelivoryIndex'
                },
                dataType: { json: 'json' },
                empty: '',
                fun: 'function',
                html: { a: 'a', body: 'body', checkbox: 'checkbox', div: 'div', head: 'head', input: 'input', h4: 'h4', img: 'img', li: 'li',p:'p', radio: 'radio', select: 'select', textarea: 'textarea', table: 'table', tbody: 'tbody', th: 'th', ul: 'ul' },
                json: { flag: '[id]', flagList: '[name]', flagFilter: 'no' },
                mainDiv: 'mainContent',
                nullValue: 'null',
                number: 'number',
                object: 'object',
                page: {
                    index: '/Index/Index.html',
                    login: '/Index/Login.html',
                    orderRank: '/Index/OrderRank.html',
                    more: '/More/More.html',
                    list: '/Order/List.html',
                    messageList: '/More/MessageList.html',
                    detail: '/Order/Detail.html',
                    contact: '/More/Contact.html',
                    messageContent: '/More/MessageContent.html',
                    distribution: '/Order/Distribution.html',
                    refund: '/Order/Refund.html',
                    road: '/More/Road.html',
                    taoRoad: '/Order/Refund.html'
                },
                popup: '.popup',
                postAction: {
                    user: '/User/User.svc/',
                    product: '/Product/Product.svc/',
                    order: '/Order/Order.svc/',
                    message: '/Notification/Notification.svc/'
                },
                script: 'script',
                space: ' ',
                string: 'string',
                template: {
                    radio: 'input:radio[name=${Name}]',
                    radioYes: 'input:radio[name=${Name}]:checked',
                    checkbox: 'input:checkbox[name=${Name}]',
                    checkboxYes: 'input:checkbox[name=${Name}]:checked',
                    checkboxNo: 'input:checkbox[name=${Name}]:not(:checked)',
                    jsonItem: "'${Key}':'${Value}'",
                    jsonEveryItem: "{${Json}}",
                    jsonArrayItem: '[${Json}]',
                    name: '[name=${Name}]',
                    alert: '<div class="error_msg">\
                                <p>${Msg}</p>\
                           </div>'
                },
                undefined: 'undefined',
                url: {
                    dataControl: '../JavaScript/FrameWork/DataControl.js',
                    pageControl: '../JavaScript/FrameWork/PageControl.js',
                    checkControl: '../JavaScript/FrameWork/CheckControl.js',
                    animatControl: '../JavaScript/FrameWork/AnimatControl.js',
                    businessControl: '../JavaScript/Business/BusinessControl.js'
                },
                web: 'http://localhost:5890',
                timeSpan:400,
                webOpen: true,
                version: '1.0.0',
                sleepTime: 30
            },
            template: {
                render: function (pTemplate, pData) {
                    return pTemplate.replace(/\$\{.*?\}/g, function (a, b) {
                        var _key = a.replace(/\$|\{|\}/g, "");
                        return pData.hasOwnProperty(_key) ? pData[_key] : a;
                    });
                }
            }
        }
    );
     
})(jQuery, undefined)