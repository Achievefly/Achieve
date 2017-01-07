

function Tab() {

    var config = {
        loadingIndex: 0,
        max: 15,
        current: -1,
        notcloseClass: 'content-main',
        activeClass: 'active'
    };

    function getTabIndex() {
        if (config.current < config.max) {
            config.current++;
        }
        return config.current;
    }
    function sumWidth(WidthObjList) {
        var width = 0;
        $(WidthObjList).each(function () {
            width += $(this).outerWidth(true)
        });
        return width
    };
    function getCurrentTab() {
        return $('#J_mainTabs a.' + config.activeClass);
    }
    function getTab(url) {
        return $('#J_mainTabs a[href="' + url + '"]');
    }
    function getIframe(url) {
        return $('#J_mainContent iframe[src="' + url + '"]');
    }
    function getTabHtml(param) {
        var html = '<a href="' + param.url + '" class="active J_menuTab" data-id="' + param.url + '">' + param.title;
        if (param.close) html += '<i class="fa fa-refresh"></i>&nbsp;<i class="fa fa-times-circle"></i>';
        html += '</a>';
        return html;
    }
    function getIframeHtml(dataId,url) {
        var html = '<iframe id="iframe' + dataId + '" class="NFine_iframe J_content active" width="100%" height="100%" frameborder="0" src="' + url + '" seamless></iframe>';
        return html;
    }
    function tabCheck(tag) {
        var param = getParam(tag);
        var url = param.url;

        if ($.trim(url).length == 0) return false

        var active = config.activeClass;
        var tab = getTab(url);
        var content = getIframe(url);
        if (tab.length) {
            if (!tab.hasClass(active)) {
                $(tab).addClass(active).siblings().removeClass(active);
            }
            if (content.length) {
                $(content).addClass(active).show().siblings().hide();
            }
        }
        else {
            tabCreate(param);
        }
        return false
    };
    function tabCreate(param) {
        var index = getTabIndex();
        var tabs = $('#J_mainTabs .page-tabs-content');

        tabs.find('a').removeClass(config.activeClass);
        tabs.append(getTabHtml(param));
        $('#J_mainContent .J_content').removeClass('active').hide();

        loading(true);
        var content = $(getIframeHtml(param.dataId, param.url));
        $('#J_mainContent').append(content);
        content.load(function () {
            loading(false);
        }).show();
    };
    function moveLeft() {
        var tabMarginLeft = Math.abs(parseInt($("#J_mainTabs .page-tabs-content").css("margin-left")));
        var otherWidth = sumWidth($('#J_mainTabs').children().not(".J_menuTabs"));
        var tabZoneWidth = $('#J_mainTabs').outerWidth(true) - otherWidth;
        var px = 0;
        if ($("#J_mainTabs .page-tabs-content").width() < tabZoneWidth) {
            return false
        } else {
            var tabs = $("#J_mainTabs .J_menuTab:first");
            var menuTabs = 0;
            while ((menuTabs + $(tabs).outerWidth(true)) <= tabMarginLeft) {
                menuTabs += $(tabs).outerWidth(true);
                tabs = $(tabs).next()
            }
            menuTabs = 0;
            if (sumWidth($(tabs).prevAll()) > tabZoneWidth) {
                while ((menuTabs + $(tabs).outerWidth(true)) < (tabZoneWidth) && tabs.length > 0) {
                    menuTabs += $(tabs).outerWidth(true);
                    tabs = $(tabs).prev()
                }
                px = sumWidth($(tabs).prevAll())
            }
        }
        $("#J_mainTabs .page-tabs-content").animate({
            marginLeft: 0 - px + "px"
        }, "fast")
    }
    function moveRight() {
        var tabMarginLeft = Math.abs(parseInt($("#J_mainTabs .page-tabs-content").css("margin-left")));
        var otherWidth = sumWidth($('#J_mainTabs').children().not(".J_menuTabs"));
        var tabZoneWidth = $('#J_mainTabs').outerWidth(true) - otherWidth;
        var px = 0;
        if ($("#J_mainTabs .page-tabs-content").width() < tabZoneWidth) {
            return false
        } else {
            var tabs = $("#J_mainTabs .J_menuTab:first");
            var menuTabs = 0;
            while ((menuTabs + $(tabs).outerWidth(true)) <= tabMarginLeft) {
                menuTabs += $(tabs).outerWidth(true);
                tabs = $(tabs).next()
            }
            menuTabs = 0;
            while ((menuTabs + $(tabs).outerWidth(true)) < (tabZoneWidth) && tabs.length > 0) {
                menuTabs += $(tabs).outerWidth(true);
                tabs = $(tabs).next()
            }
            px = sumWidth($(tabs).prevAll());
            if (px > 0) {
                $("#J_mainTabs .page-tabs-content").animate({
                    marginLeft: 0 - px + "px"
                }, "fast")
            }
        }
    }
    function tabClose(tag) {
        var active = config.activeClass;
        var current = tag.parents('.J_menuTab');
        var url = current.attr('href');
        if (current.hasClass(active)) {
            var next = current.next();
            var prev = current.prev();
            if (next.length > 0) {
                next.addClass(active);
            }
            else if (prev.length > 0) {
                prev.addClass(active);
            }
        }
        var content = getIframe(url);
        content.remove();
        current.remove();
        tabCheck(getCurrentTab());
        return false
    }
    function tabCloseAll(all) {
        var as = $("#J_mainTabs .page-tabs-content a").not('.' + config.notcloseClass);
        if (!all) as = as.not('.' + config.activeClass);
        as.each(function () {
            var tab = $(this);
            url = tab.attr('href');
            tab.remove();
            var content = getIframe(url);
            content.remove();
        });
        $("#J_mainTabs .page-tabs-content").css("margin-left", "0");
        var tab = $('#J_mainTabs .page-tabs-content a:last');
        tabCheck(tab);
    }
    function tabEnable() {
        if (!$(this).hasClass("active")) {
            tabCheck($(this));
        }
        return false;
    }
    function getParam(tag) {
        var href = tag.attr('href');
        var id = tag.attr('data-id');
        var param = {
            close: true,
            dataId:id,
            url: href,
            title: $.trim(tag.text())
        };
        return param;
    }
    //function loading(flag) {
    //    if (flag) {
    //        config.loadingIndex = layer.load(1);
    //    }
    //    else {
    //        layer.close(config.loadingIndex);
    //    }
    //}
    function loading(flag) {
        var currentId = $('.page-tabs-content').find('.active').attr('data-id');
        var target = $('.NFine_iframe[data-id="' + currentId + '"]');
        var url = target.attr('src');
        $.loading(flag);
        target.attr('src', url).load(function () {
            $.loading(flag);
        });
    }
    function init() {
        $(".J_menuItem").on("click", function () {
            tabCheck($(this));
            return false;
        });
        $("#J_mainTabs .J_tabReloadCurrent").on("click", function () {
            var tab = getCurrentTab();
            var url = tab.attr('href');
            var content = getIframe(url);
            loading(true);
            content.attr('src', url).load(function () {
                loading(false);
            });
        });
        $("#J_mainTabs .J_tabCloseCurrent").on("click", function () {
            var tab = $("#J_mainTabs .J_menuTab.active i");
            tabClose(tab);
        });
        $("#J_mainTabs .J_menuTabs").on("click", ".J_menuTab .fa-times-circle", function () {
            tabClose($(this));
            return false;
        });
        $("#J_mainTabs .J_menuTabs").on("click", ".J_menuTab .fa-refresh", function () {
            var tab = getCurrentTab();
            var url = tab.attr('href');
            var content = getIframe(url);
            loading(true);
            content.attr('src', url).load(function () {
                loading(false);
            });
        });
        $("#J_mainTabs .J_tabCloseOther").on("click", function () {
            tabCloseAll(false);
        });
        $("#J_mainTabs .J_menuTabs").on("click", ".J_menuTab", tabEnable);
        $("#J_mainTabs .J_tabLeft").on("click", moveLeft);
        $("#J_mainTabs .J_tabRight").on("click", moveRight);
        $("#J_mainTabs .J_tabCloseAll").on("click", function () {
            tabCloseAll(true);
        })
    }

    init();
}

$(function () {
    var tab = new Tab();
});
