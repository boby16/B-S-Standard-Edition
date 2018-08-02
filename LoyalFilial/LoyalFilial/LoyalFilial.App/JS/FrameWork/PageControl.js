var pageControl = function (opt) {

    $.extend(this, opt);
    this.total = this.total || 0,
    this.index = this.index || 1,
    this.action = this.action || null,
    this.viewNum = this.viewNum || 5,
    this.page = this.page || null,
    this.pageNum = this.pageNum || 30,
    this.pageTotal = this.pageTotal || 0,
    this.pageOldNum = this.pageOldNum || this.pageTotal,
    this.pageTemplate = '<div class="console_box clear">\
                            <ul class="page_console border_nor radius shadow">\
                                <li class="prev">共 <span class="c_blue">${Total}</span> 条记录</li>\
                                <li class="page_select"><label>每页显示</label><select><option ' + (this.pageNum == 10 ? 'selected' : '') + '>10</option><option ' + (this.pageNum == 30 ? 'selected' : '') + '>30</option><option ' + (this.pageNum == 50 ? 'selected' : '') + '>50</option><option ' + (this.pageNum == 100 ? 'selected' : '') + '>100</option></select></li>\
                            </ul>\
                            <ul class="console border_nor radius shadow">\
                                ${PageDetail}\
                            </ul>\
                        </div>',
    this.dataAttr = { type: 'type', prev: 'prev', prev_list: 'prev_list', next: 'next', next_list: 'next_list', cur: '.cur a' },
    this.pageTemplateDetail = {
        prev: '<li class=\"prev\"><a href=\"javascript:void(0);\" data-type=\"prev\">上一页</a></li>',
        prevDelete: '<li><a href=\"javascript:void(0);\">1</a></li><li><a data-type=\"prev_list\" href=\"javascript:void(0);\">···</a></li>',
        nextDelete: '<li><a data-type=\"next_list\" href=\"javascript:void(0);\">···</a></li><li><a href=\"javascript:void(0);\">${pageTotal}</a></li>',
        next: '<li><a data-type=\"next\" href=\"javascript:void(0);\">下一页</a></li>',
        page: '<li><a href=\"javascript:void(0);\">${index}</a></li>',
        pageCurrent: '<li class=\"cur\"><a>${index}</a></li>'
    };
};

pageControl.prototype = {
    constructor: pageControl,
    init: function () {
        this.pageTotal = this.total % this.pageNum > 0 ? parseInt((this.total / this.pageNum), 10) + 1 : parseInt((this.total / this.pageNum), 10);
        if (this.page && $.object.check(this.page).isSucceed) {
            this.target = $.object.check(this.page).object;
            this.bindEvent();
            this.viewPage();
        }
    },
    bindEvent: function () {
        var _this = this;
        this.target.unbind($.cst.action.click);
        this.target.bind($.cst.action.click, function (e) {
            var e = e || window.event,
                target = e.target || e.srcElement;
            if (target.tagName.toLowerCase() === $.cst.html.a) {                
                var type = $(target).data(_this.dataAttr.type);
                switch (type) {
                    case _this.dataAttr.prev: _this.index = parseInt(_this.index, 10) - 1; break;
                    case _this.dataAttr.next: _this.index = parseInt(_this.index, 10) + 1; break;
                    case _this.dataAttr.prev_list: {
                        var pIndex = _this.index,
                            dnum = _this.viewNum % 2,
                            hnum = (_this.viewNum - dnum) / 2,
                            center = pIndex - (pIndex - dnum) % hnum,
                            start = Math.max(1, center - hnum + 1 - dnum);
                        _this.index = start - 1;
                    }; break;
                    case _this.dataAttr.next_list: {
                        var pIndex = _this.index,
                            dnum = _this.viewNum % 2,
                            hnum = (_this.viewNum - dnum) / 2,
                            center = pIndex - (pIndex - dnum) % hnum,
                            start = Math.max(1, center - hnum + 1 - dnum),
                            end = Math.min(_this.pageTotal, start + _this.viewNum - 1);
                        _this.index = end + 1;
                    }; break;
                    default: { if (parseInt(target.innerHTML, 10) != _this.index) { _this.index = parseInt(target.innerHTML, 10) } else return false; }; break;
                }
                _this.setPageByIndex();
            }            
        });

        this.target.unbind($.cst.action.change);
        this.target.bind($.cst.action.change, function (e) {
            var e = e || window.event,
                target = e.target || e.srcElement;
            if (target.tagName.toLowerCase() === $.cst.html.select) {
                _this.setpageNum($(target).val());
            }
        });
    },
    setpageNum: function (pNum) {
        this.pageOldNum = this.pageNum;
        this.pageNum = pNum;
        this.pageTotal = this.total % this.pageNum > 0 ? parseInt(this.total / this.pageNum) + 1 : parseInt(this.total / this.pageNum);
        this.index = 1;
        this.setPageByIndex();
    },
    setPageByIndex: function () {
        if (this.action) {
            this.action.pageIndex = this.index;
            this.action.pageSize = this.pageNum;
            this.action.confirmAction = false;
            var result = this.action.confirm();
            if (typeof result == $.cst.boolean && !result) {
                this.index = this.target.find(this.dataAttr.cur).html();

                this.target.find($.cst.html.select).val(this.pageOldNum);
                this.pageNum = this.pageOldNum;
                this.pageTotal = this.total % this.pageNum > 0 ? parseInt((this.total / this.pageNum), 10) + 1 : parseInt((this.total / this.pageNum), 10);
            }
        }
        this.viewPage();
    },
    viewPage: function () {
        var dnum = this.viewNum % 2,
            hnum = (this.viewNum - dnum) / 2,
            center = this.index - (this.index - dnum) % hnum,
            start = Math.max(1, center - hnum + 1 - dnum),
            end = Math.min(this.pageTotal, start + this.viewNum - 1),
            pageDetail = $.cst.empty,
            pageList = [], _this = this;

        _this.index > 1 && (pageDetail += _this.pageTemplateDetail.prev);
        start > 1 && (pageDetail += _this.pageTemplateDetail.prevDelete);

        for (; start < _this.index; start++) pageList.push($.template.render(_this.pageTemplateDetail.page, { index: start }));
        pageList.push($.template.render(_this.pageTemplateDetail.pageCurrent, { index: _this.index }));
        for (var i = (_this.index + 1) ; i <= end; i++) pageList.push($.template.render(_this.pageTemplateDetail.page, { index: i }));
        (pageDetail += pageList.join($.cst.empty));

        end < _this.pageTotal && (pageDetail += $.template.render(_this.pageTemplateDetail.nextDelete, { pageTotal: _this.pageTotal }));
        _this.index < _this.pageTotal && (pageDetail += _this.pageTemplateDetail.next);

        $.value.set(this.target, $.template.render(_this.pageTemplate, { Total: _this.total, PageDetail: pageDetail }));
    }
};