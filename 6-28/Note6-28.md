#Unity时间的概念
对于引擎来说，他们的时间一般都是为了计算帧率（每秒钟的绘画次数）；

##Time类
__概念：__</br>
引擎里只能拥有一个时间总计数器，他可以向你提供时间的概念，所以我在`Unity`中只能直接使用 `Time`类，他的方法全是静态的，所以你没有必要去实例话一个`Time`类对象；

* `deltaTime`: </br>
只读的，他返回上一帧的执行时间(Update + LateUpdate) 加上 更新函数的休息时间的时间总和；

* `fixedDeltaTime`:</br>
只读的，他是用来返回 FixedUpdate函数的执行时间和休息时间的时间总和;

* `fixedTime` : </br>
只读的，返回我们的Unity引擎的 Time Manager 里面设定的Fixed Timestep时间；

* `frameCount` : </br>
只读的，用来返回从你游戏执行开始到你调用他时一共执行了多少帧；

* `time` : </br>
只读的，返回从你游戏开始一共经历多少秒， frameCount / time 获取平均帧率；

* `timeScale`: </br>
可以读可以写，控制引擎的时间计数的速度，1表示正常，小于1表示慢速，大于1 表示加速播放，等于0 游戏暂停；

  >注意：比如 ：`timeScale = 2`; 那么你的`time`立刻变成2倍的时间增加率；

  >注意：`timeScale = 0`; 表示`time`暂停了计数，但是你所有的`Update`和`LateUpdate`都还是正常工作的，这里只有`FixedUpdate`会停止工作；

