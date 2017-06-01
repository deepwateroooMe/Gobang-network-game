南行五子棋
===

基于TCP协议的五子棋联机对战平台
---

> 山就在那里

### 逻辑框图

* ![总览](http://xiaoliming96.com/images/gobang/gobang_main.png)  
* ![服务器部分](http://xiaoliming96.com/images/gobang/gobang_server.png)  
* ![客户机部分](http://xiaoliming96.com/images/gobang/gobang_client.png)  


### 待做事项

>* ~~补充逻辑框图~~
>* ~~完成客户端<=>服务器的同步通信~~
>* ~~服务器开线程负责监听多个客户端的连接请求~~
>* ~~完成客户端<=>服务器<=>客户端的同步通信~~
>* 完成聊天系统
>* ~~接住客户机断开时抛出的异常~~
>* 用同步通信实现“下棋”
>* 完成客户端<=>服务器的异步通信
>* 完成客户端<=>服务器<=>客户端的异步通信
>* 完成游戏的gui界面

### 日志

#### 2017-5-28

>* 项目立项
>* 添加了ReadMe文件
>   * 绘制了逻辑框图
>     * 总体部分
>     * 服务器部分
>     * 客户机部分
>   * 更新了待做事项
>   * 更新了日志

#### 2017-5-30

>* 完成了客户端<=>服务器的同步通信
>   * 基于TcpClient与TcpListener封装了TcpHelperClient与TcpHelperServer类
>* 添加了GobangClientTest项目方便测试使用

#### 2017-5-31

>* 完成了客户端<=>服务器<=>客户端的同步多线程通信
>* 重构代码
>   * 重新封装了TcpHelperServer类，令Player类承担了其通信部分的职能
>   * 封装了Player与Game类，在TcpHelperServer类中用队列存储了所有Player与Game的集合
>* 服务器现在分配了多个线程专门执行特定任务
>   * TcpHelperServer类的ListenerThread，GameMakerThread分别负责生成TcpClient并封装成Player，将Player两两分组
>   * Player类的ReaderThread负责监听网络流，读到信息后存入MessageBox
>   * Game类的TalkerThread负责监听MessageBox，将比赛双方的信息分别写入对方的网络流
>* 习惯代替配置（命名规范）
>   * 线程的命名在末尾加上Thread，托管给线程的方法在末尾加上Threadwork，所有线程属于实例变量，在实例化的最后被赋值与启用

#### 2017-5-31

>* 现在当客户端断开连接时，服务端不会崩溃
>   * 作为处理，客户端对应的Player实例中将有一个属性标识该连接已经断开
>   * 如果断开的客户端所对应的Player实例属于一个Game实例，那么Game实例将会被标记为终止，Game中的另一位Player将回到等待队列
>   * 上述所有情况发生时，对应的线程会被Abort();