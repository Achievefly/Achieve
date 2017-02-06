/*
    @author:fanyuepan
    @createtime:2015-05-15
    @description:chathub client js 
    @v1.0
*/
(function ($) {
    var csHub = {
        option: {
            serverUrl: '',//singalr服务器url
            receiveCallBack: function (result) {
                //返回消息总入口
                console.log(result);
                switch (result.msgtype) {
                    case csHub.messageType.system:
                        //处理系统消息
                        chat.handleSystemMsg(result);
                        break;
                    case csHub.messageType.custom:
                        chat.handleCustomMsg(result);
                        break;
                }
            }
        },
        proxy: {
            proxyCS: null,//singalr客户端代理类
        },
        messageType: {
            system:1,
            custom:2
        },
        //client
        client: {
            init: function () {
                //客户端 receiveMessage 方法
                _this.proxy.proxyCS.client.receiveMessage = function (result) {
                    _this.option.receiveCallBack(result);
                };
            }
        },
        init: function (option) {
            $.extend(_this.option, option);
            _this.server.init();//服务端代码初始化  
            _this.client.init();//客户端代码初始化

        },
        //server
        server: {
            //server初始化
            init: function () {
                this.connect();
                _this.proxy.proxyCS.client.clientOnConnectedCallBack = this.connectCallBack;
            },
            //连接服务器
            connect: function () {
                $.connection.hub.url = _this.option.serverUrl;
                _this.proxy.proxyCS = $.connection.csHub;
                $.connection.hub.start({ jsonp: true }).done(function () {
                    //连接服务器
                    //TODO处理聊天界面之前的逻辑
                    console.log('连接成功');
                }).fail(function () {
                    console.log("连接失败");
                });
            },
            //单人聊天
            ctoc: function (sid, rid,t) {
                //调用hub的clientToClient方法
                 if (!chat.isConnected(rid,t)) {
                   
                    //如果没有连接过，进行连接
                    console.log("用户 " + rid + "没有连接过...");
                    if (t == 'one') {
                        //一对一聊天连接服务器
                        _this.proxy.proxyCS.server.clientToClient(sid, rid);
                    } else {
                        //一对多（群组）聊天连接服务器
                        _this.proxy.proxyCS.server.clientToGroup(sid, rid);
                    }
                } else {
                    console.log("用户 " + rid + "已经连接过了，不需要连接了...");
                }
            },
            //发送，增加最后一个参数t， one ，group 群聊还是单体聊天
            send: function (msg, userid, username, userphoto, rid, t) {
                var obj = {
                    msg: msg,
                    fromuser: {
                        userid: userid,
                        username: username,
                        photo: userphoto
                    },
                    touser: {
                        userid: rid
                    }
                };
                switch (t) {
                    case 'one':
                        this.ctocsend(obj);
                        break;
                    case 'group':
                        this.ctogsend(obj);
                        break;
                    default:
                        alert('无效的消息类型');
                }
            },
            //单独
            ctocsend: function (sendObj) {
                _this.proxy.proxyCS.server.clientSendMsgToClient(sendObj);
            },
            //群组
            ctogsend: function (sendObj) {
                _this.proxy.proxyCS.server.clientSendMsgToGroup(sendObj);
            },
            //连接成功之后回调
            connectCallBack: function (result) {
                console.log(result);
            }
        }
    };
    var _this = csHub;
    //聊天信息处理
    var chat = {
        cache: {},
        cacheGroup:{},
        handleSystemMsg: function (result) {
            if (result.other.t == 'one') {
                this.cache[result.other.receiveid] = "ok";//代表我已经和当前聊天人已经连接上了，下次点击没必要再次连接
            } else {
                this.cacheGroup[result.other.receiveid] = "ok";
            }
            //然后在这里处理历史记录 2016-3-7
            if (result.other.history && result.other.history.length) {
                $(result.other.history).each(function (i,item) {
                    //追加消息
                    console.log(item);
                    chat.handleCustomMsg(item);//每一个item就是一条消息，这里格式是通用的，所以，直接调用 handleCustomMsg方法就可以了。
                });
            }
        },
        handleCustomMsg: function (result) {
            var log = {};
            //接收人
            var keys = result.other.t + result.touser.userid;
            //发送人
            var keys1 = result.other.t + result.fromuser.userid;
            //这里一定要注意，这个keys是会变的，也就是说，如果只取一个的话，会造成 log.imarea[0]为undefined的情况，至于为什么会变，看看代码好好思考一下吧
            log.imarea = $('#layim_area' + keys);//layim_areaone0
            if (!log.imarea.length) {
                log.imarea = $('#layim_area' + keys1);//layim_areaone0
            }
            if (!log.imarea.length) {
                //这里只有在连接过一次之后，并且关闭消息框才会提示
                if (result.touser.userid == hubConfig.currentUser.id) {
                    alert("您收到消息啦...");
                }
            }
            //拼接html模板
            log.html = function (param, type) {
                return '<li class="' + (type === 'me' ? 'layim_chateme' : '') + '">'
                    + '<div class="layim_chatuser">'
                        + function () {
                            if (type === 'me') {
                                return '<span class="layim_chattime">' + param.time + '</span>'
                                       + '<span class="layim_chatname">' + param.name + '</span>'
                                       + '<img src="' + param.face + '" >';
                            } else {
                                return '<img src="' + param.face + '" >'
                                       + '<span class="layim_chatname">' + param.name + '</span>'
                                       + '<span class="layim_chattime">' + param.time + '</span>';
                            }
                        }()
                    + '</div>'
                    + '<div class="layim_chatsay">' + param.content + '<em class="layim_zero"></em></div>'
                + '</li>';
            };
            //上述代码还是layim里的代码，只不过拼接html的时候，参数采用signalR返回的参数
            var type = result.fromuser.userid == currentUser.id ? "me" : "";//如果发送人的id==当前用户的id，那么这条消息类型为me
            //拼接html 直接调用layim里的代码
            log.imarea.append(log.html({
                time: result.addtime,
                name: result.fromuser.username,
                face: result.fromuser.photo,
                content: replace_em(result.msg)
            }, type));
            //滚动条处理
            if (log.imarea.length) {
                log.imarea.scrollTop(log.imarea[0].scrollHeight);
            }
        },
        isConnected: function (rid, t) {
            ///单人聊天
            if (t == 'one') {
                console.log(this.cache);
                /*2016-3-7修改
                  增加聊天框的判断，如果聊天框被关闭了，需要重新连接，用来读取历史记录
                */
                var chatbox = $('#layim_areaone' + rid);
                return this.cache[rid] === "ok" && chatbox.length > 0;
            }
            //群组聊天
            if (t == 'group') {
                return this.cacheGroup[rid] === "ok";
            }
        }
    };
    _this.chat = chat;
    window.csClient = _this;
})($);

