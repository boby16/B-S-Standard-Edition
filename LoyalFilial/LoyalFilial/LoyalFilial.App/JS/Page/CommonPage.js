var footerDiv = '<div class="footer">\
        <div style="background-color:rgb(153, 51, 0); height:150px; vertical-align:middle;width:1000px ">\
            <div style="display:inline-block;">\
                <div style="float:left; padding:50px 50px 5px 50px; width:150px;text-align:left;">\
                    <div style="color:white; font-weight:bold">帮助中心</div>\
                    <div style="color:white;padding-top:20px; font-size:14px">平台操作指南</div>\
                </div>\
                <div style="float:left; padding:50px 50px 5px 50px; width:150px;text-align:left;">\
                    <div style="color:white; font-weight:bold">关于车盈</div>\
                    <div style="color:white;padding-top:20px; font-size:14px">关于车盈</div>\
                </div>\
                <div style="float:left; padding:50px 50px 5px 50px; width:250px;text-align:left;">\
                    <div style="color:white; font-weight:bold">关注车盈</div>\
                    <div style="color:white;padding-top:20px; font-size:14px">还没有注册车盈网吗？</div>\
                    <div style="color:white; font-size:14px">赶快 注册，享受车盈网优质服务吧！</div>\
                </div>\
            </div>\
        </div>\
        <div style="font-size:14px; text-align:center">Copyright © 上海有限公司  沪ICP备</div>\
    </div>',
    headerDiv = '<div class="header">\
        <div class="icon">车  盈</div>\
        <div style="float:right; width:auto">\
            <div class="link" style="text-wrap:none" id="divLoginInfo"></div>\
            <div class="link" style="display:none;"><a href="###"><img src="../Images/index_1.png" /><span>询报价管理</span></a></div>\
            <div class="link" style="display:none;"><a href="###"><img src="../Images/index_1.png" /><span>订单管理</span></a></div>\
            <div class="link" style="display:none;"><a href="../MaintainFactory/ReserveMng.html"><img src="../Images/index_1.png" /><span>预约清单</span></a></div>\
            <div class="link"><a href="###"><img src="../Images/index_2.png" /><span>短信充值</span></a></div>\
            <div class="link"><a href="###"><img src="../Images/index_3.png" /><span>个人中心</span></a></div>\
            <div class="link"><a href="###"><img src="../Images/index_4.png" /><span>帮助中心</span></a></div>\
        </div>\
    </div>',
    menuDiv = '<div class="menu">\
        <ul id="ulHeaderMenu">\
            <li><a href="../Home/Index.html" id="Index">首页</a></li>\
            <li><a href="../MaintainFactory/CarPartsSearch.html" id="CarPartsSearch" style="display:none;">汽配快搜</a></li>\
            <li><a href="../MaintainFactory/InquiryMng.html" id="inquiryMng" style="display:none;">询价管理</a></li>\
            <li><a href="../MaintainFactory/PurchaseMng.html" id="purchaseMng" style="display:none;">订单管理</a></li>\
            <li><a href="../MaintainFactory/CustomerMng.html" id="CustomerMng" style="display:none;">客户管理</a></li>\
            <li><a href="../MaintainFactory/MaintainMng.html" id="MaintainMng" style="display:none;">维保管理</a></li>\
            <li><a href="../MaintainFactory/ReserveMng.html" id="ReserveMng" style="display:none;">预约管理</a></li>\
            <li><a href="../CarParts/QuotationMng.html" id="QuotationMng" style="display:none;">报价管理</a></li>\
            <li><a href="../CarParts/OrderMng.html" id="OrderMng" style="display:none;">订单交易</a></li>\
            <li><a href="../CarParts/DeliverMng.html" id="DeliverMng" style="display:none;">发货管理</a></li>\
        </ul>\
    </div>',
    alertDiv = '<div class="mainContent" style="display:none"></div>',
    urlParams = new Object();

var commonPageJsMng = commonPageJsMng || {
    initHeader: function () {
        document.writeln(headerDiv);
        commonPageJsMng.initJs();
        commonPageJsMng.initLoginInfo();
    },
    initHeaderMenu: function () {
        document.writeln(headerDiv);
        document.writeln(menuDiv);
        commonPageJsMng.initJs();
        commonPageJsMng.initLoginInfo();

        $("#ulHeaderMenu").find("li").each(function () {
            if ($(this).find("a").eq(0).prop("href").indexOf(location.pathname) > 0)
                $(this).addClass("cur");
            else
                $(this).removeClass("cur");
        });
    },
    initFooter: function () {
        document.writeln(footerDiv);
    },
    initJs: function () {
        commonPageJsMng.includeJS("mainJs", "../JS/FrameWork/Main.js");
        commonPageJsMng.includeJS("dataJs", "../JS/FrameWork/DataControl.js");
        commonPageJsMng.includeJS("checkJs", "../JS/FrameWork/CheckControl.js");
        document.writeln(alertDiv);
    },
    initLoginInfo: function () {
        var accountidCk = $.cookie.get($.cst.cookie.accountId),
            userTypeCk = $.cookie.get($.cst.cookie.userType),
            userTypeText = "";
        if (userTypeCk == 1)
            userTypeText = "汽修厂用户";
        else if (userTypeCk == 2)
            userTypeText = "汽配商用户";
        if (accountidCk != null && typeof (accountidCk) != "undefined" && accountidCk > 0) {
            $("#divLoginInfo").html('您好！' + userTypeText + '【' + accountidCk
               + '】 | <a href="javascript:void(0);" onclick="commonPageJsMng.loginOut()"><img style="height:16px;width:16px" src="../Images/loginOut.png" /><span>退出</span></a>');

            if (userTypeCk == 1) {
                $("#CarPartsSearch").show();
                $("#inquiryMng").show();
                $("#purchaseMng").show();
                $("#CustomerMng").show();
                $("#MaintainMng").show();
                $("#ReserveMng").show();

                $("#QuotationMng").hide();
                $("#OrderMng").hide();
                $("#DeliverMng").hide();
            }
            else if (userTypeCk == 2) {
                $("#CarPartsSearch").hide();
                $("#inquiryMng").hide();
                $("#purchaseMng").hide();
                $("#CustomerMng").hide();
                $("#MaintainMng").hide();
                $("#ReserveMng").hide();

                $("#QuotationMng").show();
                $("#OrderMng").show();
                $("#DeliverMng").show();
            }
        }
        else
            $("#divLoginInfo").html("请登录/注册");
    },
    loginOut: function () {
        $.dataManager.post({
            dataJson: {
                AccountId: $.cookie.get($.cst.cookie.accountId)
            },
            postUrl: "/Auth/Auth.svc/LoginOut",
            needLoading: false,
            afterFun: function (msg) {
                $.cookie.del($.cst.cookie.accountId);
                $.cookie.del($.cst.cookie.userType);
                $.cookie.del($.cst.cookie.token);
                location.href = "../Home/Index.html";
            }
        });
    },
    includeJS: function (sId, fileUrl) {
        if (!$("#" + sId) || $("#" + sId).length == 0) {
            var oHead = $('head').eq(0);
            var oScript = document.createElement("script");
            oScript.id = sId;
            oScript.src = fileUrl;
            oHead.append(oScript);
        }
    },
    initSelect: function () {
        $('.son_ul').hide(); //初始ul隐藏
        $('.select_box span').hover(function(){ //鼠标移动函数
            $(this).parent().find('ul.son_ul').slideDown();  //找到ul.son_ul显示
            $(this).parent().find('li').hover(
                function () { $(this).addClass('hover'); },
                function () { $(this).removeClass('hover'); }
            ); //li的hover效果
            $(this).parent().hover(
                function () { },
                function () { $(this).parent().find("ul.son_ul").slideUp(); }
            );
        },function(){}
        );
        $('ul.son_ul li').click(function(){
            $(this).parents('li').find('span').html($(this).html());
            $(this).parents('li').find('ul').slideUp();
        });
    },
    initUrlParam: function () {
        var args = new Object();
        var query = location.search.substring(1);
        var pairs = query.split("&");  

        for (var i = 0; i < pairs.length; i++) {
            var pos = pairs[i].indexOf('=');
            if (pos == -1) continue;
            var argname = pairs[i].substring(0, pos);
            var value = pairs[i].substring(pos + 1);
            args[argname] = unescape(value);
        }
        return args;
    },
    GetUrlParms:function(parm){
        if(typeof(urlParams[parm])!="undefined") {
            return urlParams[parm];
        }
        else{
            urlParams = commonPageJsMng.initUrlParam();
            if (typeof (urlParams[parm]) != "undefined") {
                return urlParams[parm];
            }
            else return "";
        }
    },
    checkLoginInfo: function () {
        $.dataManager.post({
            dataJson: {
                AccountId: $.cookie.get($.cst.cookie.accountId),
                Token: $.cookie.get($.cst.cookie.token)
            },
            postUrl: "/Auth/Auth.svc/CheckLoginUser",
            needLoading: false,
            afterFun: function (msg) {
                if (msg != null && msg != "null" && typeof (msg) != "undefined") {
                    if (msg.IsSuccess) {
                        return true;
                    }
                }
                location.href = "../Home/Index.html";
            }
        });
    }
}