var businessControl = function (opt) {
    $.extend(this, opt);

    this.notice = this.notice || $.cst.empty;
    this.actionName = this.actionName || $.cst.empty;
    this.afterFun = this.afterFun || null;
}

businessControl.prototype = {
    constructor: businessControl,
    init: function () {

        this.actionName == this.cst.msg && this.receiveNotice();
        this.actionName == this.cst.scn && this.screenPrint();

    },
    receiveNotice: function () {

    },
    screenPrint: function () {         
        var _this = this;
        cloudSky.zBar.scan({}, function (msg) {
            _this.afterFun && typeof _this.afterFun == $.cst.fun && _this.afterFun(msg);

        }, function () {
            var contents = arguments.length ? arguments[0] : $.cst.empty; 
        });
    },
    cst: {
        msg: 1,
        scn: 2
    }
}