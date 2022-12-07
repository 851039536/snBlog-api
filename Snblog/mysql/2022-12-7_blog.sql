-- MySqlBackup.NET 2.3.5
-- Dump Time: 2022-12-07 17:22:55
-- --------------------------------------
-- Server version 8.0.16 MySQL Community Server - GPL


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of article_tag
-- 

DROP TABLE IF EXISTS `article_tag`;
CREATE TABLE IF NOT EXISTS `article_tag` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标签名称',
  `description` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标签描述',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table article_tag
-- 

/*!40000 ALTER TABLE `article_tag` DISABLE KEYS */;
INSERT INTO `article_tag`(`id`,`name`,`description`) VALUES
(30,'mysql','');
/*!40000 ALTER TABLE `article_tag` ENABLE KEYS */;

-- 
-- Definition of article_type
-- 

DROP TABLE IF EXISTS `article_type`;
CREATE TABLE IF NOT EXISTS `article_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '分类名称',
  `description` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '分类描述',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table article_type
-- 

/*!40000 ALTER TABLE `article_type` DISABLE KEYS */;
INSERT INTO `article_type`(`id`,`name`,`description`) VALUES
(103,'mysql',''),
(104,'csharp','');
/*!40000 ALTER TABLE `article_type` ENABLE KEYS */;

-- 
-- Definition of sn_comments
-- 

DROP TABLE IF EXISTS `sn_comments`;
CREATE TABLE IF NOT EXISTS `sn_comments` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '评论主键',
  `user_id` int(11) NOT NULL COMMENT '用户id',
  `give` int(11) NOT NULL COMMENT '点赞数',
  `text` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '内容',
  `time_create` datetime NOT NULL COMMENT '评论日期',
  `time_modified` datetime NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_comments
-- 

/*!40000 ALTER TABLE `sn_comments` DISABLE KEYS */;

/*!40000 ALTER TABLE `sn_comments` ENABLE KEYS */;

-- 
-- Definition of sn_interface_type
-- 

DROP TABLE IF EXISTS `sn_interface_type`;
CREATE TABLE IF NOT EXISTS `sn_interface_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_interface_type
-- 

/*!40000 ALTER TABLE `sn_interface_type` DISABLE KEYS */;
INSERT INTO `sn_interface_type`(`id`,`name`) VALUES
(1,'header'),
(2,'sidebar');
/*!40000 ALTER TABLE `sn_interface_type` ENABLE KEYS */;

-- 
-- Definition of sn_interface
-- 

DROP TABLE IF EXISTS `sn_interface`;
CREATE TABLE IF NOT EXISTS `sn_interface` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标题',
  `path` varchar(80) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '路径',
  `type_id` int(5) NOT NULL COMMENT '类别',
  `user_id` int(5) NOT NULL COMMENT '用户',
  `identity` tinyint(1) NOT NULL COMMENT '显示隐藏',
  PRIMARY KEY (`id`),
  KEY `type_id` (`type_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `sn_interface_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  CONSTRAINT `type` FOREIGN KEY (`type_id`) REFERENCES `sn_interface_type` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=134 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_interface
-- 

/*!40000 ALTER TABLE `sn_interface` DISABLE KEYS */;
INSERT INTO `sn_interface`(`id`,`title`,`path`,`type_id`,`user_id`,`identity`) VALUES
(1,'博客','/article/column',2,4,1),
(2,'博客页','/qarticle',2,4,1),
(5,'主页','/article/column',1,4,1),
(9,'舔狗日记','/one',2,4,1),
(10,'哔哔视频','/video',2,4,1),
(11,'网站导航','/favorite',2,4,1),
(12,'博客导航','/BlogCircles',2,4,1),
(13,'我的书单','/book',2,4,1),
(14,'聚合搜索','/ListContent',2,4,1),
(17,'图册','/Photo',1,4,1),
(18,'代码块','code',1,4,1),
(19,'后台管理','/Login',2,4,1),
(127,'后台','/Login',1,1,1),
(128,'博客导航','/BlogCircles',2,1,1),
(129,'博客','/tag',2,1,1),
(131,'网站导航','/favorite',2,1,1),
(132,'代码块','code',1,1,1);
/*!40000 ALTER TABLE `sn_interface` ENABLE KEYS */;

-- 
-- Definition of sn_navigation_type
-- 

DROP TABLE IF EXISTS `sn_navigation_type`;
CREATE TABLE IF NOT EXISTS `sn_navigation_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `title` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标题',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_navigation_type
-- 

/*!40000 ALTER TABLE `sn_navigation_type` DISABLE KEYS */;
INSERT INTO `sn_navigation_type`(`id`,`title`) VALUES
(2,'网站'),
(3,'vue'),
(4,'net'),
(5,'JavaScript'),
(7,'收藏'),
(8,'博客圈'),
(9,'css'),
(10,'论坛'),
(11,'导航'),
(13,'文档'),
(14,'工具'),
(15,'学习'),
(16,'mysql'),
(17,'在线刷题'),
(18,'图库'),
(19,'前端框架'),
(20,'markdown'),
(21,'uniapp'),
(22,'efcore'),
(23,'docker'),
(24,'vue组件库'),
(26,'vite'),
(27,'常用工具'),
(28,'wpf'),
(29,'orm'),
(30,'cdn');
/*!40000 ALTER TABLE `sn_navigation_type` ENABLE KEYS */;

-- 
-- Definition of sn_one_type
-- 

DROP TABLE IF EXISTS `sn_one_type`;
CREATE TABLE IF NOT EXISTS `sn_one_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_one_type
-- 

/*!40000 ALTER TABLE `sn_one_type` DISABLE KEYS */;
INSERT INTO `sn_one_type`(`id`,`title`) VALUES
(4,'舔狗日记'),
(5,'毒鸡汤');
/*!40000 ALTER TABLE `sn_one_type` ENABLE KEYS */;

-- 
-- Definition of sn_picture_type
-- 

DROP TABLE IF EXISTS `sn_picture_type`;
CREATE TABLE IF NOT EXISTS `sn_picture_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '分类名称',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_picture_type
-- 

/*!40000 ALTER TABLE `sn_picture_type` DISABLE KEYS */;
INSERT INTO `sn_picture_type`(`id`,`name`) VALUES
(1,'article'),
(2,'User'),
(3,'Video');
/*!40000 ALTER TABLE `sn_picture_type` ENABLE KEYS */;

-- 
-- Definition of sn_picture
-- 

DROP TABLE IF EXISTS `sn_picture`;
CREATE TABLE IF NOT EXISTS `sn_picture` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图床名',
  `img_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片地址',
  `type_id` int(11) NOT NULL COMMENT '分类',
  `user_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `prcture_type_id` (`type_id`),
  KEY `pivture_user_id` (`user_id`),
  CONSTRAINT `pivture_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  CONSTRAINT `prcture_type_id` FOREIGN KEY (`type_id`) REFERENCES `sn_picture_type` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_picture
-- 

/*!40000 ALTER TABLE `sn_picture` DISABLE KEYS */;
INSERT INTO `sn_picture`(`id`,`name`,`img_url`,`type_id`,`user_id`) VALUES
(2,'blog','/img/blog/1.jpg',1,4),
(3,'blog','/img/blog/2.jpg',1,4),
(4,'blog','/img/blog/3.jpg',1,4),
(6,'blog','/img/blog/4.jpg',1,4),
(7,'blog','/img/blog/5.jpg',1,4);
/*!40000 ALTER TABLE `sn_picture` ENABLE KEYS */;

-- 
-- Definition of sn_setblog_type
-- 

DROP TABLE IF EXISTS `sn_setblog_type`;
CREATE TABLE IF NOT EXISTS `sn_setblog_type` (
  `id` int(11) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_setblog_type
-- 

/*!40000 ALTER TABLE `sn_setblog_type` DISABLE KEYS */;
INSERT INTO `sn_setblog_type`(`id`,`name`) VALUES
(0,'后台设置'),
(1,'主页设置');
/*!40000 ALTER TABLE `sn_setblog_type` ENABLE KEYS */;

-- 
-- Definition of sn_software_type
-- 

DROP TABLE IF EXISTS `sn_software_type`;
CREATE TABLE IF NOT EXISTS `sn_software_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_software_type
-- 

/*!40000 ALTER TABLE `sn_software_type` DISABLE KEYS */;

/*!40000 ALTER TABLE `sn_software_type` ENABLE KEYS */;

-- 
-- Definition of sn_software
-- 

DROP TABLE IF EXISTS `sn_software`;
CREATE TABLE IF NOT EXISTS `sn_software` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '标题',
  `img` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '图片路径',
  `type_id` int(11) DEFAULT NULL COMMENT '分类',
  `comment_id` int(255) DEFAULT NULL COMMENT '评论',
  `time_create` datetime DEFAULT NULL COMMENT '时间',
  `time_modified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `software_type_id` (`type_id`),
  CONSTRAINT `software_type_id` FOREIGN KEY (`type_id`) REFERENCES `sn_software_type` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_software
-- 

/*!40000 ALTER TABLE `sn_software` DISABLE KEYS */;

/*!40000 ALTER TABLE `sn_software` ENABLE KEYS */;

-- 
-- Definition of sn_talk_type
-- 

DROP TABLE IF EXISTS `sn_talk_type`;
CREATE TABLE IF NOT EXISTS `sn_talk_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_talk_type
-- 

/*!40000 ALTER TABLE `sn_talk_type` DISABLE KEYS */;
INSERT INTO `sn_talk_type`(`id`,`name`) VALUES
(3,'版本更新'),
(4,'测试'),
(5,'生活');
/*!40000 ALTER TABLE `sn_talk_type` ENABLE KEYS */;

-- 
-- Definition of sn_talk
-- 

DROP TABLE IF EXISTS `sn_talk`;
CREATE TABLE IF NOT EXISTS `sn_talk` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标题',
  `describe` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '简介',
  `text` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '内容',
  `read` int(11) NOT NULL COMMENT '阅读量',
  `give` int(11) NOT NULL COMMENT '点赞',
  `comment_id` int(11) NOT NULL COMMENT '评论',
  `user_id` int(11) NOT NULL COMMENT '用户',
  `type_id` int(11) NOT NULL,
  `time_create` datetime NOT NULL COMMENT '发表时间',
  `time_modified` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `sn_talk_typeId` (`type_id`),
  CONSTRAINT `lalk_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`),
  CONSTRAINT `talk_type_id` FOREIGN KEY (`type_id`) REFERENCES `sn_talk_type` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_talk
-- 

/*!40000 ALTER TABLE `sn_talk` DISABLE KEYS */;
INSERT INTO `sn_talk`(`id`,`title`,`describe`,`text`,`read`,`give`,`comment_id`,`user_id`,`type_id`,`time_create`,`time_modified`) VALUES
(3,'Blog-12.18更新','favorite,博客页面样式更新样式更新,内容页面,时间线页面动态加载组件数据提示框更新为骨架框','<h3>1.样式更新</h3><p><span style=\"color: rgb(68, 68, 68);\">favorite</span>,<span style=\"color: rgb(68, 68, 68);\">博客页面样式更新样式更新</span></p><h3>2.内容页改动</h3><p><span style=\"color: rgb(68, 68, 68);\">内容页面,时间线页面动态加载组件数据提示框更新为骨架框</span></p><h3>3.时间线改动</h3><p><span style=\"color: rgb(68, 68, 68);\">增加one日记动态数据时间线增加跳转到页面详情</span></p>',0,0,0,4,3,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(17,'Blog-12.21更新','','<h3>1.增加页面</h3><p>增加日志详情页面-TalkText.vue, OneSidebar.vue 文章侧边栏界面</p><h3>2.Talk页面改动</h3><p>增加页面跳转至TalkText.vue</p><p>About关于界面增加功能跳转</p><p>One文章页面增加 OneSidebar文章侧边栏</p>',0,0,0,4,3,'2020-12-21 00:00:00','2020-12-18 00:00:00'),
(18,'Blog-12.23更新','优化了Indexs,Talk及侧边栏等页面字体样式及抗齿轮,one页面增加点击弹出详情框及更新了css样式','<h3>1.页面优化</h3><ul><li>优化了Indexs,Talk及侧边栏等页面字体样式及抗齿轮</li><li>one页面增加点击弹出详情框及更新了css样式</li><li>OneSidebar侧边栏时间字段格式优化,增加点击弹出详情页</li><li>Snvodeo视频页面及详情页增加时间字段格式优化</li><li>TalkTest页面头部组件更新</li><li>TimeLine时间组件动态字段更新增加(分类,标签,文章,阅读,字段数)</li><li>Sidebarsn css样式更新</li><li>IndexSidebar 站点信息增加动态更新</li></ul><h3>2.新增</h3><ul><li>增加nprogress顶部加载组件</li><li>增加store状态管理</li><li>增加Transfer文章中转站页面</li></ul><p><br></p><h3>3.页面重构</h3><ul><li>app页面js更改为ts方式重写</li></ul><h3>4.其他优化</h3><ul><li>修复Indextext2页面跳转当前页面不刷新</li><li>封装了内容详情(blogs)css样式</li><li>增加视频图片</li></ul>',0,0,0,4,3,'2020-12-23 00:00:00','2020-12-18 00:00:00'),
(19,'Blog-12.25更新','','<h3>1.页面改动</h3><p>app.vue 删除背景颜色设定</p><p>com.scss 封装line-ome index.css</p><p>增加响应式设定</p><p>导航页面css调整</p><p>收藏页面增加动态数据分类框(之前是静态)</p><p>one侧边栏动态增加字段,文章数量,阅读显示</p><p>删除日志Talk页面顶部信息框,删除侧边栏图标框 Headers.vue 样式调整</p><h3>2.新增内容</h3><p>增加新字体 font.css并应用页面</p><p>增加响应式断点主页面已完成响应式设定</p><p>新增字体文件 新增移动端状态下显示底部导航框bootom</p>',0,0,0,4,3,'2020-12-25 00:00:00','2020-12-18 00:00:00'),
(20,'展望','','<p>是佛挡杀佛的范德萨发</p>',0,0,0,4,3,'2020-12-26 00:00:00','2020-12-18 00:00:00'),
(21,'青春真的结束了吗','','',0,0,0,4,3,'2020-12-26 00:00:00','2020-12-18 00:00:00'),
(22,'Blog正式投入使用','','<p>经过接近一个月时间的项目重构(vue2--&gt;vue3+ts),主要部分功能已经完善。</p><p><strong>以下已完成功能</strong></p><p>主页技术方面的文章阅读</p><p>标签页面方面查找对应的文章进行阅读</p><p>时间线</p><p>导航站</p><p>日志-&gt;只做个人文章展示</p><p><strong>娱乐项</strong></p><p>短文仅供一乐(舔狗日志)</p><p>收藏,博客页面分享各路大神技术博客</p><p><strong>待进行功能</strong></p><p>书单--准备进行</p><p>后台-- 后台系统大部分功能已近完善,基本可以投入生产,但是还有很多细节方面没弄好暂不上线</p><p>留言-- 前端已完成,后台api已完成待导入接口</p><p><strong>待优化功能项</strong></p><p>额!!!!!!! 好像都需要优化</p><p>后续增加一个新页面来追踪自己要做什么,做到什么进度。就叫个人项目进度追踪应该也算是新功能了</p><p><strong>项目架构</strong></p><p>前端项目使用到的技术</p><p><span style=\"color: rgb(136, 136, 136);\">VUE3 -- TS -- Router -- Axios -- Store -- AntDesignVue -- TaiwindCss -- animate.css -- marked</span></p><p>后端项目</p><p>NetCore3.1webApi</p><p><strong>本次更新</strong></p><p>1.增加导航侧边栏 FavSidebar.vue</p><p>2.增加书单页面 Book.vue</p><p>3.增加图片视频展示</p><p>4.删除字体(太大了)</p><p>5.Talk.vue 页面进行重构(想好看点)</p><p><br></p>',0,0,0,4,3,'2020-12-26 00:00:00','2020-12-18 00:00:00');
/*!40000 ALTER TABLE `sn_talk` ENABLE KEYS */;

-- 
-- Definition of sn_user_friends
-- 

DROP TABLE IF EXISTS `sn_user_friends`;
CREATE TABLE IF NOT EXISTS `sn_user_friends` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL COMMENT '用户id',
  `user_friends_id` int(11) DEFAULT NULL COMMENT '好友id',
  `user_note` varchar(20) DEFAULT NULL COMMENT '好友备注',
  `user_status` varchar(20) DEFAULT NULL COMMENT '好友状态',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_user_friends
-- 

/*!40000 ALTER TABLE `sn_user_friends` DISABLE KEYS */;
INSERT INTO `sn_user_friends`(`id`,`user_id`,`user_friends_id`,`user_note`,`user_status`) VALUES
(1,1,2,'小张','在线');
/*!40000 ALTER TABLE `sn_user_friends` ENABLE KEYS */;

-- 
-- Definition of sn_video_type
-- 

DROP TABLE IF EXISTS `sn_video_type`;
CREATE TABLE IF NOT EXISTS `sn_video_type` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_video_type
-- 

/*!40000 ALTER TABLE `sn_video_type` DISABLE KEYS */;
INSERT INTO `sn_video_type`(`id`,`name`) VALUES
(1,'火影'),
(2,'LOL');
/*!40000 ALTER TABLE `sn_video_type` ENABLE KEYS */;

-- 
-- Definition of snippet_label
-- 

DROP TABLE IF EXISTS `snippet_label`;
CREATE TABLE IF NOT EXISTS `snippet_label` (
  `id` int(11) NOT NULL,
  `name` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table snippet_label
-- 

/*!40000 ALTER TABLE `snippet_label` DISABLE KEYS */;
INSERT INTO `snippet_label`(`id`,`name`) VALUES
(1,'测试'),
(2,'transition'),
(3,'slot'),
(4,'component'),
(5,'code'),
(6,'scroll'),
(7,'array'),
(8,'运算符'),
(9,'日期处理');
/*!40000 ALTER TABLE `snippet_label` ENABLE KEYS */;

-- 
-- Definition of snippet_tag
-- 

DROP TABLE IF EXISTS `snippet_tag`;
CREATE TABLE IF NOT EXISTS `snippet_tag` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table snippet_tag
-- 

/*!40000 ALTER TABLE `snippet_tag` DISABLE KEYS */;
INSERT INTO `snippet_tag`(`id`,`name`) VALUES
(1,'插件'),
(2,'基础教程'),
(3,'语法片段'),
(4,'异常解决'),
(6,'编码规范'),
(7,'指令'),
(8,'命名规范');
/*!40000 ALTER TABLE `snippet_tag` ENABLE KEYS */;

-- 
-- Definition of snippet_type
-- 

DROP TABLE IF EXISTS `snippet_type`;
CREATE TABLE IF NOT EXISTS `snippet_type` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table snippet_type
-- 

/*!40000 ALTER TABLE `snippet_type` DISABLE KEYS */;
INSERT INTO `snippet_type`(`id`,`name`) VALUES
(1,'csharp'),
(2,'vue'),
(3,'axios'),
(5,'typescript'),
(6,'javascript'),
(8,'css'),
(9,'vite'),
(10,'scss'),
(11,'eslint'),
(12,'mysql'),
(13,'vscode'),
(14,'windicss'),
(15,'vue-router');
/*!40000 ALTER TABLE `snippet_type` ENABLE KEYS */;

-- 
-- Definition of user
-- 

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ip` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT 'ip地址',
  `name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '账号',
  `email` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '邮箱',
  `pwd` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `photo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '头像',
  `time_create` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '注册时间',
  `time_modified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '更新时间',
  `nickname` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '称呼',
  `brief` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '简介',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table user
-- 

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user`(`id`,`ip`,`name`,`email`,`pwd`,`photo`,`nickname`,`brief`) VALUES
(1,'ip','1','81929392@qq.con','1','','测试号','哈哈'),
(4,'ip','kai','851039536@qq.com','woshishui','..','少年','人生有梦,各自精彩'),
(18,'1','12312','23213','213','131232','3213','213123');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- 
-- Definition of article
-- 

DROP TABLE IF EXISTS `article`;
CREATE TABLE IF NOT EXISTS `article` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标题 ',
  `sketch` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '内容简述',
  `text` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '博客内容',
  `read` smallint(8) NOT NULL COMMENT '阅读次数',
  `give` smallint(8) NOT NULL COMMENT '点赞',
  `img` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片',
  `comment_id` smallint(8) NOT NULL COMMENT '评论',
  `tag_id` int(5) NOT NULL COMMENT '标签外键',
  `type_id` int(5) NOT NULL COMMENT '分类外键',
  `user_id` int(5) NOT NULL COMMENT '用户外键id',
  `time_create` datetime NOT NULL COMMENT '发表时间',
  `time_modified` datetime NOT NULL COMMENT '更新时间',
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `article_labelsId` (`tag_id`),
  KEY `article_sortId` (`type_id`),
  CONSTRAINT `article_ibfk_1` FOREIGN KEY (`tag_id`) REFERENCES `article_tag` (`id`),
  CONSTRAINT `tyoeId` FOREIGN KEY (`type_id`) REFERENCES `article_type` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `userId` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=369 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table article
-- 

/*!40000 ALTER TABLE `article` DISABLE KEYS */;
INSERT INTO `article`(`id`,`name`,`sketch`,`text`,`read`,`give`,`img`,`comment_id`,`tag_id`,`type_id`,`user_id`,`time_create`,`time_modified`) VALUES
(368,'c#使用mysql进行备份还原','数据备份是数据安全的最后一道防线，对于任何数据丢失的场景，备份虽然不一定能恢复百分之百的数据(取决于备份周期)，但至少能将损失降到最低。','> 数据备份是数据安全的最后一道防线，对于任何数据丢失的场景，备份虽然不一定能恢复百分之百的数据(取决于备份周期)，但至少能将损失降到最低。\n\n###  引用dll\n\n使用nuget安装dll程序包\n\n```\nnuget > MySql.Data.dll\nnuget > MySqlbackup.dll\n```\n\n\n\n### 连接静态类\n\n新建一个连接字符串静态类\n\n```csharp\npublic static class mysql\n{\npublic static string constr = \"database=test;Password=密码;user ID=root;server=ip地址\";\npublic static MySqlConnection conn = new MySqlConnection(constr);\n}\n```\n\n\n\n### 数据备份\n\n```csharp\nDialogResult result = MessageBox.Show(\"备份路径默认在当前程序下\", \"提示\", MessageBoxButtons.YesNo, MessageBoxIcon.Question);\nif(result == DialogResult.Yes)\n{\n    string time1 = System.DateTime.Now.ToString(\"d\").Replace(\"/\", \"-\");\n    string file = \".//mysql/\" + time1 + \"_test.sql\";\n    using(MySqlCommand cmd = new MySqlCommand())\n    {\n        using(MySqlBackup mb = new MySqlBackup(cmd))\n        {\n            cmd.Connection = mysql.conn;\n            mysql.conn.Open();\n            mb.ExportToFile(file);\n            mysql.conn.Close();\n            MessageBox.Show(\"已备份\");\n        }\n    }\n}\n```\n\n### 备份还原\n\n```csharp\nstring file = textBox1.Text;\nif(file == \"\")\n{\n    MessageBox.Show(\"不能为空\");\n    return;\n}\nDialogResult result = MessageBox.Show(\"确定还原吗？\", \"还原\", MessageBoxButtons.YesNo, MessageBoxIcon.Question);\nif(result == DialogResult.Yes)\n{\n    try\n    {\n        using(MySqlCommand cmd = new MySqlCommand())\n        {\n            using(MySqlBackup mb = new MySqlBackup(cmd))\n            {\n                cmd.Connection = mysql.conn;\n                mysql.conn.Open();\n                mb.ImportFromFile(file);\n                mysql.conn.Close();\n                MessageBox.Show(\"已还原\");\n            }\n        }\n    }\n    catch(Exception ex)\n    {\n        MessageBox.Show(ex.Message);\n    }\n}\n```\n\n### 定时备份\n\n```csharp\n//winform\ntimer1.Interval = 1000; //代表一秒运行一次\ntimer1.Enabled = true; //启动\n```\n\n利用winform窗体 timer定时器控件\n\n```csharp\nprivate void timer1_Tick(object sender, EventArgs e)\n{\n    if(booql)\n    {\n        booql = false;\n        if(DateTime.Now.Hour == 10 && DateTime.Now.Minute == 00) //时间10点 \n        {\n            string time1 = System.DateTime.Now.ToString(\"d\").Replace(\"/\", \"-\");\n            string file = \".//mysql/\" + time1 + \"_test.sql\";\n            using(MySqlCommand cmd = new MySqlCommand())\n            {\n                using(MySqlBackup mb = new MySqlBackup(cmd))\n                {\n                    cmd.Connection = mysql.conn;\n                    mysql.conn.Open();\n                    mb.ExportToFile(file);\n                    mysql.conn.Close();\n                    MessageBox.Show(\"数据库已自动备份本地\");\n                }\n            }\n        }\n    }\n}\n```\n',2,0,'blog/2.jpg',0,30,104,4,'2022-12-07 16:09:17','2022-12-07 16:09:17');
/*!40000 ALTER TABLE `article` ENABLE KEYS */;

-- 
-- Definition of sn_leave
-- 

DROP TABLE IF EXISTS `sn_leave`;
CREATE TABLE IF NOT EXISTS `sn_leave` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `text` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '留言内容',
  `user_id` int(11) NOT NULL COMMENT '用户外键',
  `time_create` datetime NOT NULL COMMENT '发布时间',
  `time_modified` datetime NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `user_id` (`user_id`),
  CONSTRAINT `user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_leave
-- 

/*!40000 ALTER TABLE `sn_leave` DISABLE KEYS */;
INSERT INTO `sn_leave`(`id`,`text`,`user_id`,`time_create`,`time_modified`) VALUES
(4,'string',4,'2020-12-25 00:00:00','2021-11-02 00:00:00'),
(5,'string',4,'2020-12-25 00:00:00','2021-11-02 00:00:00'),
(6,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(7,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(8,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(9,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(10,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(11,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(12,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(13,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(14,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(15,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(16,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(17,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(18,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(19,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(20,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(21,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(22,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(23,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(24,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(25,'string',4,'2021-04-29 00:00:00','2021-11-02 00:00:00'),
(26,'string',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(27,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(28,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(29,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(30,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(31,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(32,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(33,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(34,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(35,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(36,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(37,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(38,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(39,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(40,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(41,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(42,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(43,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00'),
(44,'jenny',4,'2021-05-03 00:00:00','2021-11-02 00:00:00');
/*!40000 ALTER TABLE `sn_leave` ENABLE KEYS */;

-- 
-- Definition of sn_navigation
-- 

DROP TABLE IF EXISTS `sn_navigation`;
CREATE TABLE IF NOT EXISTS `sn_navigation` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `title` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '导航标题',
  `describe` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标题描述',
  `img` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片路径',
  `url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '链接路径',
  `type_id` int(11) NOT NULL COMMENT '分类',
  `user_id` int(11) NOT NULL COMMENT '用户',
  `time_create` datetime DEFAULT NULL,
  `time_modified` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `nav_user_id` (`user_id`),
  KEY `nav_type_id` (`type_id`),
  CONSTRAINT `nav_type_id` FOREIGN KEY (`type_id`) REFERENCES `sn_navigation_type` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `nav_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=608 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_navigation
-- 

/*!40000 ALTER TABLE `sn_navigation` DISABLE KEYS */;
INSERT INTO `sn_navigation`(`id`,`title`,`describe`,`img`,`url`,`type_id`,`user_id`,`time_create`,`time_modified`) VALUES
(1,'Webpack','Webpack 是当下最热门的前端资源模块化管理和打包工具。它可以将许多松散的模块按照依赖和规则打包成符合生产环境部署的前端资源。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.27/dist/img/webpack.png','https://www.webpackjs.com/',14,4,'2021-11-10 14:45:01','2021-11-11 16:24:54'),
(2,'React','React 起源于 Facebook 的内部项目，是一个用于构建用户界面的 JavaScript 库。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.27/dist/img/react.png','https://reactjs.bootcss.com/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(3,'TypeScript','TypeScript 是由微软开源的编程语言。它是 JavaScript 的一个超集，而且本质上向这个语言添加了可选的静态类型和基于类的面向对象编程。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.27/dist/img/typescript.png','https://typescript.bootcss.com/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(4,'Svelte','Svelte 是构建 Web 应用程序的一种新方法。Svelte 是一个编译器，它将声明性组件转换成高效的 JavaScript 代码，并像做外科手术一样细粒度地更新 DOM。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.27/dist/img/svelte.png','https://www.sveltejs.cn/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(5,'Next.js','Next.js 是一个轻量级的 React 服务端渲染应用框架。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.28/dist/img/nextjs.png','https://www.nextjs.cn/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(6,'Babel','Babel 是一个 JavaScript 编译器。Babel 通过语法转换器支持最新版本的 JavaScript 语法。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.27/dist/img/babeljs.png','https://www.babeljs.cn/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(7,'Node.js','Node.js 是一个基于 Chrome V8 引擎的 JavaScript 运行环境。Node.js 使用了一个事件驱动、非阻塞式 I/O 的模型，使其轻量又高效。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.28/dist/img/nodejs.png','https://www.nodeapp.cn/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(8,'Deno','Deno 是一个简单、现代且安全的 JavaScript 和 TypeScript 运行时，deno 基于 V8 引擎并使用 Rust 编程语言构建。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.28/dist/img/deno.png','https://deno.bootcss.com/',5,4,'2021-11-10 14:45:01','2022-10-18 08:37:50'),
(9,'Yarn','Yarn 是一个快速、可靠、安全的依赖管理工具。是 NPM 的替代品。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.28/dist/img/yarn.png','https://yarn.bootcss.com/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(10,'Yarn v2','Yarn 是一个快速、可靠、安全的依赖管理工具。是 NPM 的替代品。Yarn v2 与 v1 版本有很大的不同，Yarn v2 改进了 CLI 交互、支持 workspace、PnP 等新功能。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.28/dist/img/yarn.png','https://www.yarnpkg.com.cn/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(11,'前端学习路线','好好学习，天天敲代码','https://objtube.github.io/front-end-roadmap/#/','https://objtube.github.io/front-end-roadmap/#/',15,4,'2021-11-10 14:45:01','2021-12-16 14:25:19'),
(12,'Visual Studio','功能完备的集成开发环境 (IDE)，适用于 Android、iOS、Windows、Web 和云 (IDE)','https://img-prod-cms-rt-microsoft-com.akamaized.net/cms/api/am/imageFileData/RE1Mu3b?ver=5c31','https://visualstudio.microsoft.com/zh-hans/downloads/',14,4,'2021-11-10 14:45:01','2022-10-13 10:13:40'),
(14,'现代 JavaScript','以最新的 JavaScript 标准为基准。通过简单但足够详细的内容，为你讲解从基础到高阶的 JavaScript 相关知识。','https://zh.javascript.info/','https://zh.javascript.info/',5,4,'2021-11-10 14:45:01','2022-10-18 08:38:01'),
(15,'Sass中文网','Sass 是一款强化 CSS 的辅助工具，它在 CSS 语法的基础上增加了变量 (variables)、嵌套 (nested rules)、混合 (mixins)、导入 (inline imports) 等高级功能，这些拓展令 CSS 更加强大与优雅。使用 Sass 以及 Sass 的样式库（如 Compass）有助于更好地组织管理样式文件，以及更高效地开发项目。','https://www.sass.hk/docs/','https://www.sass.hk/docs/',9,4,'2021-11-10 14:45:01','2022-10-18 08:37:43'),
(16,'BootstrapVue','我们开始BootstrapVue之旅游，基于全球最流行的Bootstrap V4框架，构建移动优先的响应式门户，在Vue.js前端栈基础上。','http://code.z01.com/bootstrap-vue/docs/','http://code.z01.com/bootstrap-vue/docs/',3,4,'2021-11-10 14:45:01','2022-10-18 08:37:36'),
(17,'前端导航站','前端内容汇总','http://jsdig.com/','http://jsdig.com/',15,4,'2021-11-10 14:45:01','2021-12-16 14:25:48'),
(18,'Md2All排版','Markdown排版利器，支持 \"一键排版\" 、自定义css、80多种代码高亮。\n能让Markdown内容，无需作任何调整就能一键复制到微信公众号、博客园、掘金、知乎、csdn、51cto、wordpress、hexo。。。等平台。','http://md.aclickall.com/','http://md.aclickall.com/',20,4,'2021-11-10 14:45:01','2022-10-18 08:37:29'),
(19,'VuePress','Vue 驱动的静态网站生成器','https://v0.vuepress.vuejs.org/zh/','https://v0.vuepress.vuejs.org/zh/',3,4,'2021-11-10 14:45:01','2022-10-18 08:37:24'),
(20,'Blog.Core','BCVP（Blog.Core & Vue Project）是一个开箱即用的企业级权限管理应用框架。\n采用最新的前后端完全分离技术【 ASP.NET Core Api 3.x + Vue 2.x 】。','http://apk.neters.club/.doc/','http://apk.neters.club/.doc/',4,4,'2021-11-10 14:45:01','2022-10-18 08:37:11'),
(21,'sqlSugar','.NET 4.+ & .NET CORE 高性能 轻量级 ORM框架，众多.NET框架中最容易使用的数据库访问技术','http://www.codeisbug.com/','http://www.codeisbug.com/',16,4,'2021-11-10 14:45:01','2022-11-08 15:54:28'),
(22,'Avue','一个很多骚操作的前端框架\n让数据驱动视图，减去繁琐的操作，更贴近企业级的前端开发组件','https://avuejs.com/','https://avuejs.com/',3,4,'2021-11-10 14:45:01','2022-10-18 08:37:02'),
(24,'Bootstrap ','基于 Bootstrap 样式库精心打造，并且额外增加了 50 多种常用的组件，为您快速开发项目带来非一般的感觉','https://blazor.sdgxgz.com/','https://blazor.sdgxgz.com/',9,4,'2021-11-10 14:45:01','2022-10-18 08:36:53'),
(25,'Editor.md','\n开源在线 Markdown 编辑器','http://editor.md.ipandao.com/','http://editor.md.ipandao.com/',20,4,'2021-11-10 14:45:01','2022-10-18 08:36:44'),
(26,'标签[c#]','','https://stackoom.com/img/logo.png','https://stackoom.com/',4,4,'2021-11-10 14:45:01','2022-10-18 08:36:32'),
(27,'Font Aweso','世界上最流行的ICON图标字体库和CSS工具包','http://www.fontawesome.com.cn/','http://www.fontawesome.com.cn/',9,4,'2021-11-10 14:45:01','2022-10-18 08:35:25'),
(28,'Dotnet9','Donet技术论坛','https://dotnet9.com/','https://dotnet9.com/',8,4,'2021-11-10 14:45:01','2021-11-15 14:30:21'),
(29,'vol.vue','前后端分离\n\n全自动代码生成\n\n支持前端、后台扩展的快速开发框架','http://www.volcore.xyz/','http://www.volcore.xyz/',3,4,'2021-11-10 14:45:01','2022-10-18 08:35:19'),
(30,'jQuery插件库','jQuery插件库','https://www.jq22.com/','https://www.jq22.com/',5,4,'2021-11-10 14:45:01','2022-10-18 08:35:07'),
(31,'Vue.js中文文档','渐进式\nJavaScript 框架','https://vue.docschina.org/','https://vue.docschina.org/',3,4,'2021-11-10 14:45:01','2022-10-18 08:35:02'),
(33,'CSS Fonts','网络安全 CSS 字体堆栈的完整集合。','','https://www.cssfontstack.com/',9,4,'2021-11-10 14:45:01','2022-10-18 08:34:54'),
(34,'CSS速查总表 ','','http://css.cuishifeng.cn/','http://css.cuishifeng.cn/',9,4,'2021-11-10 14:45:01','2022-10-18 08:34:40'),
(35,'爆胎','互联网长方体空间移动师','https://tvax1.sinaimg.cn/square/0084aYsLly1ggmuk8fguaj305k05kq30.jpg','https://itggg.cn/',8,4,'2021-11-10 14:45:01','2022-11-08 15:53:02'),
(36,'青找博客','我们总在生活中与「一瞬的感动」相遇。','https://tva3.sinaimg.cn/square/0084aYsLly1ggmukfjc0uj3068068dfs.jpg','https://www.linguang.me/',8,4,'2021-11-10 14:45:01','2021-11-11 16:24:27'),
(37,'axios中文网','易用、简洁且高效的http库','','http://www.axios-js.com/',3,4,'2021-11-10 14:45:01','2022-10-18 08:34:32'),
(38,'layui','由职业前端倾情打造，面向全层次的前后端开发者，低门槛开箱即用的前端 UI 解决方案','','https://www.layui.com/',5,4,'2021-11-10 14:45:01','2022-10-18 08:34:23'),
(39,'我爱斗图','在线表情包','','https://www.52doutu.cn/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(40,'Quasar Fra','以最短时间构建高性能的VueJS用户界面','','http://www.quasarchs.com/',3,4,'2021-11-10 14:45:01','2022-10-18 08:34:13'),
(41,'Electron','使用 JavaScript，HTML 和 CSS 构建跨平台的桌面应用程序','','https://www.electronjs.org/',24,4,'2021-11-10 14:45:01','2022-10-18 08:34:06'),
(42,'2020年Web前端','新手入门前端，需要学习的基础内容有很多，如下。','','https://www.cnblogs.com/qianguyihao/p/8776837.html',10,4,'2021-11-10 14:45:01','2022-11-08 15:54:12'),
(43,'ZUI','一个基于 Bootstrap 深度定制开源前端实践方案，帮助你快速构建现代跨屏应用。','','https://www.openzui.com/',9,4,'2021-11-10 14:45:01','2022-10-18 09:57:20'),
(44,'Aplayer','Wow, such a beautiful HTML5 music player','','https://aplayer.js.org/#/zh-Hans/',24,4,'2021-11-10 14:45:01','2022-10-18 08:33:50'),
(46,'.NET万能框架','项目基于.NET 4.5构建，语法版本C#6.0，包含日常编程多数的常用封装，可以说是一个万能框架，能够用于任何基于.NET平台的项目当中。','','https://masuit.com/55',4,4,'2021-11-10 14:45:01','2022-10-18 08:33:20'),
(47,'Tailwind Grids','为 Tailwind CSS 项目轻松生成响应式网格。所有生成的类都基于 Tailwind 默认值，只需选择您的设置即可开始使用。','','https://tailwindgrids.com/#/',9,4,'2021-11-10 14:45:01','2022-10-18 09:57:16'),
(48,'CSS Inspiration -- CSS灵感','这里可以让你寻找到使用或者是学习 CSS 的灵感，以分类的形式，展示不同 CSS 属性或者不同的课题使用 CSS 来解决的各种方法。','','https://csscoco.com/inspiration/#/',9,4,'2021-11-10 14:45:01','2022-10-18 08:32:58'),
(49,'小游网','二次元技术宅','https://img.xiaoyou66.com/images/2020/02/20/tTSY.jpg','https://xiaoyou66.com/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(50,'全历史','全历史(Allhistory)以AI知识图谱为核心引擎,通过高度时空化、关联化数据的方式构造及展现数字人文内容,尤其是历史知识。','https://img.xiaoyou66.com/images/2020/02/20/tTSY.jpg','https://www.allhistory.com/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(51,'疯狂去水印','打开短视频APP， 选择要下载的视频，点击右下角分享按钮，在分享弹框中点击“复制链接”','https://douyin.video996.com/img/mp.jpg','https://douyin.video996.com/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(52,'知妖','知妖是一个开放的在线“妖怪”资料库。致力于收集、整理、介绍、分享古人文献中的“妖怪”。我们尽可能地收录古文献中的“妖怪”资料，让更多的人能够完整，系统地了解中国“妖怪”文化。','https://static.cbaigui.com/images/2020/04/loading.jpg!full','https://www.cbaigui.com/',2,4,'2021-11-10 14:45:01','2021-11-10 14:45:13'),
(53,'煎蛋','地球上没有新鲜事','http://img.jandan.net/news/2020/03/2e4024373d26ccd3888e29a6f4152076.jpg!square','http://jandan.net/',10,4,'2021-11-10 14:45:01','2022-10-18 10:02:14'),
(54,'小鸡词典','查网络流行语','https://jikipedia.com/images/logo/logo_full_side.png','https://jikipedia.com/',8,4,'2021-11-10 14:45:01','2021-11-18 13:41:02'),
(55,'网站任意门','你将被传送到完全随机的一个网站，传送到任何一个网站的概率都是相等的。','https://gate.ofo.moe/social/hero-4.jpg','https://gate.ofo.moe/',11,4,'2021-11-10 14:45:01','2022-10-18 10:02:08'),
(56,'Tailwind CSS','Tailwind 是基于 PostCSS 开发的,通过 JavaScript 代码进行配置,这意味着你可以完全发挥真正的编程语言的能力。 Tailwind 不仅仅是一个 CSS 框架,他更是构建设计系统','','https://www.tailwindcss.cn/',9,4,'2021-11-10 14:45:01','2022-10-18 09:57:11'),
(57,'Flexbox网格','基于flex显示属性的网格系统。','','http://flexboxgrid.com/',9,4,'2021-11-10 14:45:01','2022-10-18 09:56:59'),
(58,'purecss','一组小型的自适应CSS模块，您可以在每个Web项目中使用。','https://purecss.io/img/logo_pure@2x.png','https://purecss.io/',9,4,'2021-11-10 14:45:01','2022-10-18 09:56:55'),
(59,'vue-aplayer','A beautiful HTML5 music player for Vue.js','https://aplayer.netlify.app/docs/hero.png','https://aplayer.netlify.app/docs/',24,4,'2021-11-10 14:45:01','2022-10-18 08:32:11'),
(60,'林德熙','微软最具价值专家 Windows Development MVP','https://blog.lindexi.com/img/avatar.png','https://blog.lindexi.com/',8,4,'2021-11-10 14:45:01','2022-10-18 08:32:02'),
(61,'MahApps.Me','MahApps.Metro是一个框架，使开发人员可以轻松地为自己的WPF应用程序整合Metro或Modern UI。','https://mahapps.com/assets/img/oss.png','https://mahapps.com/',4,4,'2021-11-10 14:45:01','2022-10-18 08:31:54'),
(62,'C#入门经典教程','C# 是微软推出的一门面向对象的通用型编程语言，它除了可以开发 PC 软件、网站（借助 ASP.NET）和 APP（基于 Windows Phone），还能作为游戏脚本，编写游戏逻辑。','','http://c.biancheng.net/csharp/',13,4,'2021-11-10 14:45:01','2022-10-18 08:31:34'),
(64,'C# 指南','官方指南','https://docs.microsoft.com/favicon.ico','https://docs.microsoft.com/zh-cn/dotnet/csharp/',13,4,'2021-11-10 14:45:01','2022-10-18 08:31:07'),
(65,'Ant Design Blazor','这里是 Ant Design 的 Blazor 实现，开发和服务于企业级后台产品。','https://raw.githubusercontent.com/ant-design-blazor/ant-design-blazor/master/logo.svg','https://ant-design-blazor.gitee.io/zh-CN/docs/introduce',4,4,'2021-11-10 14:45:01','2022-09-28 11:43:42'),
(66,'mavonEditor','关于\nmavonEditor-基于Vue的markdown编辑器，支持多种个性化功能','https://raw.githubusercontent.com/ant-design-blazor/ant-design-blazor/master/logo.svg','https://github.com/hinesboy/mavonEditor',24,4,'2021-11-10 14:45:01','2022-09-28 11:44:16'),
(68,'VUE粒子','粒子背景的Vue.js组件','','https://vue-particles.netlify.app/',24,4,'2021-11-10 14:45:01','2021-11-18 14:38:44'),
(69,'Element','Element，一套为开发者、设计师和产品经理准备的基于 Vue 2.0 的桌面端组件库','','https://element.eleme.cn/#/zh-CN',19,4,'2021-11-10 14:45:01','2021-11-18 14:38:29'),
(70,'outils','前端业务代码工具库','','https://www.npmjs.com/package/outils',5,4,'2021-11-10 14:45:01','2021-11-15 14:33:28'),
(71,'Anime.js','一个强大的、轻量级的用来制作动画的javascript库','','https://animejs.com/',9,4,'2021-11-10 14:45:01','2022-10-18 09:56:50'),
(72,'Hover.css','CSS hover 悬停效果，可以应用于链接、按钮、图片等等','','http://ianlunn.github.io/Hover/',9,4,'2021-11-10 14:45:01','2022-10-18 09:56:47'),
(73,'Waves','点击波纹效果','','http://fian.my.id/Waves/#examples',9,4,'2021-11-10 14:45:01','2021-11-18 14:37:52'),
(74,'Viewer.js','图片滑动切换展示效果','','https://fengyuanchen.github.io/viewerjs/',5,4,'2021-11-10 14:45:01','2021-11-18 14:38:37'),
(75,'clipboard','复制粘贴插件','','https://clipboardjs.com/',24,4,'2021-11-10 14:45:01','2021-11-18 14:38:02'),
(76,'You-need-to-know-css','作为一名Web开发者，CSS是必备技能之一，我一直以为自己对CSS的掌握已经够用了，直到读Lea Verou的《CSS揭秘》，我发现自己充其量就算个会打CS的选手，书中针对我们常见的网页设计难题从不同的角度提出了多种实用又优雅的解决方案，在这里强烈的推荐给每一位从事前端相关工作和对前端有兴趣的同学，相信你一定会有所收获','https://lhammer.cn/You-need-to-know-css/static/logo.png','https://lhammer.cn/You-need-to-know-css/#/zh-cn/',9,4,'2021-11-10 14:45:01','2021-11-18 14:37:38'),
(77,'Shiro','Shiro，是alphardex平时所做的CSS创意作品集','','https://shiroi.netlify.app/',9,4,'2021-11-10 14:45:01','2021-11-18 14:37:32'),
(78,'今日热榜','每日榜单','https://file.ipadown.com/tophub/assets/images/logo.png','https://tophub.today/',11,4,'2021-11-10 14:45:01','2021-11-18 14:37:25'),
(79,'艾特网','程序员之家','https://iiter.cn/_nuxt/img/f996b71.png','https://iiter.cn/',8,4,'2021-11-10 14:45:01','2021-11-18 14:37:03'),
(80,'PostCSS','是一个用 JavaScript 工具和插件转换 CSS 代码的工具','https://www.postcss.com.cn/postcss.1b20c651.png','https://www.postcss.com.cn/',9,4,'2021-11-10 14:45:01','2022-10-18 09:56:42'),
(81,'Articles','css技巧','','https://css-tricks.com/archives/',9,4,'2021-11-10 14:45:01','2021-11-18 14:37:10'),
(82,'JavaScript 秘密花园','此中文翻译由三生石上独立完成，博客园首发，转载请注明出处。','','https://bonsaiden.github.io/JavaScript-Garden/zh/#intro',5,4,'2021-11-10 14:45:01','2021-11-18 14:35:56'),
(83,'uni-app','uni-app 是一个使用 Vue.js 开发所有前端应用的框架，开发者编写一套代码，可发布到iOS、Android、H5、以及各种小程序（微信/支付宝/百度/头条/QQ/钉钉/淘宝）、快应用等多个平台。','https://vkceyugu.cdn.bspapp.com/VKCEYUGU-uni-app-doc/7c946930-bcf2-11ea-b997-9918a5dda011.png','https://uniapp.dcloud.io/README',21,4,'2021-11-10 14:45:01','2021-11-18 14:37:45'),
(84,'前端小册子','前端学习曲线陡峭，入门容易精通难。后期有瓶颈往往是因为前期基础不扎实。学习一点，掌握一点，貌似慢，实际快。实战是好事，但理论不扎实就着急实战并非是好事。实战帮助更好地理解理论，而不是帮助学习理论。理论学明白，项目才能踏实做，否则，在实战的过程中得到的只是零碎知识点，并没有形成完善的理论体系，收效不大\n好的代码像粥一样，都是用时间熬出来的。小火柴立志要做一名前端工匠\n\n这个小册子是小火柴总结的前端知识结构，方便自己学习，也希望能够帮到更多人\n由于里面许多内容是自己的总结，可能会有错误或纰漏之处，希望不会造成误导，多多交流','https://vkceyugu.cdn.bspapp.com/VKCEYUGU-uni-app-doc/7c946930-bcf2-11ea-b997-9918a5dda011.png','https://xiaohuochai.site/introduce.html',11,4,'2021-11-10 14:45:01','2021-11-18 14:36:36'),
(85,'uView(uni-appUI框架)','uView UI，是uni-app生态最优秀的UI框架，全面的组件和便捷的工具会让您信手拈来，如鱼得水','https://uviewui.com/common/logo.png','https://uviewui.com/',19,4,'2021-11-10 14:45:01','2021-11-18 14:36:53'),
(86,'umy-ui','为开发者准备的基于 Vue 2.0 的桌面端组件库; 流畅渲染表格万级数据','','https://www.umyui.com/',21,4,'2021-11-10 14:45:01','2021-11-18 14:36:30'),
(87,'Element UI表单设计及代码生成器','可将生成的代码直接运行在基于Element的vue项目中；也可导出JSON表单，使用配套的解析器将JSON解析成真实的表单。','','https://jakhuang.github.io/form-generator/#/',19,4,'2021-11-10 14:45:01','2021-11-18 14:36:47'),
(88,'luch-request(uni-app)','基于Promise开发的uni-app跨平台请求库','https://www.quanzhan.co/luch-request/assets/img/logo.jpg','https://www.quanzhan.co/luch-request/',21,4,'2021-11-10 14:45:01','2021-11-18 14:36:22'),
(89,'Entity Framework Core API 参考','欢迎使用 .NET API 浏览器！这个一站式商店，销售 Microsoft 提供的所有基于 .NET 的 API。 在下面的框中键入字词，开始搜索任意托管 API 吧。 可以通过我们的博文详细了解 API 浏览器。','','https://docs.microsoft.com/zh-cn/dotnet/api/?view=efcore-3.1',22,4,'2021-11-10 14:45:01','2022-10-18 08:48:47'),
(90,'Furion','Furion 是 .NET 5 平台下极易入门、极速开发的 Web 应用框架。','https://monksoul.gitee.io/fur/img/logo.png','https://dotnetchina.gitee.io/furion/',4,4,'2021-11-10 14:45:01','2021-11-18 13:40:32'),
(92,'vue-admin-beautiful','是一款基于vue+element-ui的绝佳的中后台前端开发管理框架（基于vue/cli 4 最新版，同时支持电脑，手机，平板）,他同时是拥有100+页面的大型vue前端单页应用','','https://gitee.com/chu1204505056/vue-admin-beautiful/?hmsr=github&hmpl=&hmcu=&hmkw=&hmci=',19,4,'2021-11-10 14:45:01','2021-11-18 13:41:17'),
(93,'Animate中文网','强大的跨平台的预设css3动画库\n内置了很多典型的css3动画，兼容性好使用方便','','http://www.animate.net.cn/',9,4,'2021-11-10 14:45:01','2021-11-18 13:40:25'),
(94,'jQuery API 3.3.1 速查表','速查表','','https://www.94xh.com/index.html',5,4,'2021-11-10 14:45:01','2021-11-18 13:40:08'),
(95,'flv.js(播放器)','flv.js 是一个使用纯JavaScript编写的FLV(HTML5 Flash Video)播放器。','','https://www.bootcdn.cn/flv.js/1.5.0/',5,4,'2021-11-10 14:45:01','2021-11-18 13:39:57'),
(96,'福利汇总','大千世界收集福利分享','','https://www.gifxu.com/',10,4,'2021-11-10 14:45:01','2021-11-18 12:28:16'),
(97,'Animate.css','Just-add-water CSS animations','','https://animate.style/',9,4,'2021-11-10 14:45:01','2021-11-18 10:58:10'),
(98,'Typecho','Typecho博客分享','https://qqdie.com/wp-content/themes/lighthouse/images/typecho.png','https://qqdie.com/links.html',27,4,'2021-11-10 14:45:01','2022-10-13 10:13:24'),
(99,'Umar Hansa的开发人员技巧','开发人员技巧','','https://umaar.com/dev-tips/',15,4,'2021-11-10 14:45:01','2021-11-18 10:57:50'),
(100,'iconfont','阿里巴巴图形库','','https://www.iconfont.cn/',18,4,'2021-11-10 14:45:01','2021-11-18 14:36:05'),
(101,'highlight.js','Web语法突出显示','','https://highlightjs.org/',5,4,'2021-11-10 14:45:01','2021-11-18 10:57:30'),
(102,'MakingCSS','The web tool for generating CSS3 code','','https://makingcss.com/',9,4,'2021-11-10 14:45:01','2021-11-18 10:57:38'),
(103,'CSS生成器','','','https://www.cssportal.com/',9,4,'2021-11-10 14:45:01','2021-11-18 10:37:40'),
(104,'LintCode 领扣','空前强大的  在线编程  训练系统  即刻启程！','','https://www.lintcode.com/',17,4,'2021-11-10 14:45:01','2021-11-18 10:52:12'),
(105,'postcss简介','PostCSS 是一个允许使用 JS 插件转换样式的工具。 这些插件可以检查（lint）你的 CSS，支持 CSS Variables 和 Mixins， 编译尚未被浏览器广泛支持的先进的 CSS 语法，内联图片，以及其它很多优秀的功能。','','https://www.cnblogs.com/aidixie/p/12771985.html',9,4,'2021-11-10 14:45:01','2021-11-18 10:44:28'),
(106,'TypeScript','TypeScript是JavaScript类型的超集，它可以编译成纯JavaScript。\nTypeScript可以在任何浏览器、任何计算机和任何操作系统上运行，并且是开源的。','','https://www.tslang.cn/index.html',5,4,'2021-11-10 14:45:01','2021-11-18 10:44:35'),
(107,'Axios中文文档','Axios 是一个基于 promise 的 HTTP 库，可以用在浏览器和 node.js 中。','','https://www.kancloud.cn/yunye/axios/234845',13,4,'2021-11-10 14:45:01','2021-11-18 10:44:07'),
(108,'关于ASP.NETCore的分享之路','学习路线图\nASP.NET CORE学习指南\n《基础知识掌握部分》\n《部署与组件学习部分》\n《容器化与跨平台部分》','','https://www.cnblogs.com/laozhang-is-phi/p/all-knowledge-for-netcore.html#autoid-2-1-0',15,4,'2021-11-10 14:45:01','2021-11-18 14:36:14'),
(109,'C#之Action和Func的用法','','','https://www.cnblogs.com/LipeiNet/p/4694225.html',7,4,'2021-11-10 14:45:01','2022-10-18 08:29:37'),
(110,'NanUI 界面组件','这是一个开放源代码的 .NET / .NET Core 窗体应用程序（WinForms）界面组件。您可以使用 HTML5 / CSS3 / Javascript 等前端技术来构建您的应用程序界面。主流的Javascript框架，比如Angular, React, Vue都是可以用来构架SPA应用的明智选择。使用 NanUI 界面组件将给您的窗体设计工作带来无限可能。','','https://www.formium.net/',4,4,'2021-11-10 14:45:01','2021-11-18 10:44:13'),
(111,'让你30分钟快速掌握vue 3','经过了漫长的迭代，Vue 3.0终于在上2020-09-18发布了，带了翻天覆地的变化，使用了Typescript 进行了大规模的重构，带来了Composition API RFC版本，类似React Hook 一样的写Vue，可以自定义自己的hook ，让使用者更加的灵活，接下来总结一下vue 3.0  带来的部分新特性。\n\n作者：撒点料儿\n链接：https://juejin.im/post/6887359442354962445\n来源：掘金\n著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。','','https://juejin.im/post/6887359442354962445',15,4,'2021-11-10 14:45:01','2021-11-18 10:39:34'),
(112,'vuepress-theme-vdoing','一款简洁高效的VuePress 知识管理&博客 主题','https://doc.xugaoyi.com/vuepress-theme-vdoing-doc/','https://doc.xugaoyi.com/vuepress-theme-vdoing-doc/',3,4,'2021-11-10 14:45:01','2022-10-18 08:29:26'),
(113,'软件工艺师(bibi)','视频教程','https://doc.xugaoyi.com/vuepress-theme-vdoing-doc/','https://space.bilibili.com/361469957/video',8,4,'2021-11-10 14:45:01','2021-11-18 10:38:51'),
(114,'NetModular','为中小型企业而生的基于.Net Core平台的模块化快速开发解决方案','https://docs.17mkh.com/images/logo.png','https://docs.17mkh.com/',4,4,'2021-11-10 14:45:01','2022-10-18 08:29:19'),
(115,'秦枫鸢梦','花有重开日，人无再少年','https://q1.qlogo.cn/g?b=qq&nk=2013143650&s=100','https://blog.zwying.com/',8,4,'2021-11-10 14:45:01','2021-11-18 10:39:15'),
(116,'Mikutap','','','https://xiabor.com/',8,4,'2021-11-10 14:45:01','2022-10-18 08:29:04'),
(117,'Typora','Typora 是一款支持实时预览的 Markdown 文本编辑器。它有 OS X、Windows、Linux 三个平台的版本，并且由于仍在测试中，是完全免费的。','','https://typora.io/',27,4,'2021-11-10 14:45:01','2022-10-13 10:13:51'),
(119,'ExCSS','一个CSS3解析器C#库','','https://www.ctolib.com/ExCSS.html',4,4,'2021-11-10 14:45:01','2021-11-18 10:39:02'),
(120,'Everything','Everything中文版是一款功能强大，便捷实用的文件搜索软件。','','https://everything.en.softonic.com/',14,4,'2021-11-10 14:45:01','2022-10-18 08:27:59'),
(121,'AnyDesk','远程连接到您的计算机，无论是从办公室的另一层还是世界的另一端。 AnyDesk为IT专业人员和移动用户提供安全可靠的远程桌面连接。','','https://anydesk.com/zhs',14,4,'2021-11-10 14:45:01','2022-10-18 08:27:52'),
(122,'WebStorm','WebStorm 是jetbrains公司旗下一款JavaScript 开发工具。已经被广大中国JS开发者誉为“Web前端开发神器”、“最强大的HTML5编辑器”、“最智能的JavaScript IDE”等。与IntelliJ IDEA同源，继承了IntelliJ IDEA强大的JS部分的功能。','','https://www.jetbrains.com/help/webstorm/installation-guide.html',14,4,'2021-11-10 14:45:01','2021-11-18 10:43:58'),
(124,'XMind','思如泉涌 • 成竹在图','','https://www.xmind.cn/',14,4,'2021-11-10 14:45:01','2021-11-18 10:39:08'),
(125,'Postman','API开发协作平台','','https://www.postman.com/',14,4,'2021-11-10 14:45:01','2021-11-18 10:35:45'),
(126,'AutoHotkey','AutoHotkey 是一个自由、开源的宏生成器和自动化软件工具，它让用户能够自动执行重复性任务。AutoHotkey 可以修改任何应用程序的用户界面（例如，把默认的 Windows 按键控制命令替换为 Emacs 风格）。它是由定制的脚本语言驱动，旨在提供键盘快捷键或热键。——wikipedia','','https://www.autohotkey.com/',14,4,'2021-11-10 14:45:01','2021-11-17 17:10:09'),
(127,'Notepad++','Notepad++ 是在微软视窗环境之下的一个免费的代码编辑器。为了产生小巧且有效率的代码编辑器,这个在GPL许可证下的自由软体开发专案采用 win32 api 和 STL 以 ...','','https://notepad-plus-plus.org/',14,4,'2021-11-10 14:45:01','2021-11-17 17:10:00'),
(128,'字体仓库','免费字体库','','https://www.ziticangku.com/',14,4,'2021-11-10 14:45:01','2021-11-17 17:09:40'),
(129,'You-need-to-know-css','为了以后可以更爽的复制粘贴，笔者把自己的收获和工作中常用的一些CSS小样式总结成这份文档','','https://lhammer.cn/You-need-to-know-css/#/zh-cn/',9,4,'2021-11-10 14:45:01','2021-11-17 17:09:33'),
(130,'CSS Tricks','总结一些常用的 CSS 样式\n记录一些 CSS 的新属性和一点奇技淫巧\n在“动”部分下有些动画并不是 CSS 效果，因为没有地方放置，所以暂时寄存在这里\n尽量少说废话，代码简单易用，方便复制','','http://css-tricks.neatbang.com/',9,4,'2021-11-10 14:45:01','2021-11-17 17:09:22'),
(131,'animista','该项目里面有各种 CSS 实现的效果，还有代码演示，方便直接复制代码，还可以复制压缩后的代码，如果你在找某个 CSS 的效果的话，可以到这里找找看。','','https://animista.net/',9,4,'2021-11-10 14:45:01','2021-11-18 10:35:52'),
(132,'spinkit','汇集了实现各种加载效果的 CSS 代码片段。\n\nSpinKit 仅使用（transform 和 opacity）CSS 动画来创建平滑且易于自定义的动画。','','https://tobiasahlin.com/spinkit/',9,4,'2021-11-10 14:45:01','2022-10-18 08:27:41'),
(133,'Blog.Admin','框架涵盖 VUE 开发中常见的基本知识点，不仅适合初学者入门，同时也适用于企业级别的开发。','','https://vueadmin.neters.club/.doc/',19,4,'2021-11-10 14:45:01','2021-11-17 17:09:47'),
(134,'.NET Core 学习资料精选：入门','主要分享一些.NET Core比较优秀的社区资料和微软官方资料。我进行了知识点归类，让大家可以更清晰的学习.NET Core。\n\n首先感谢资料原作者的贡献。','','https://www.cnblogs.com/heyuquan/p/dotnet-basic-learning-resource.html',15,4,'2021-11-10 14:45:01','2021-11-17 17:05:08'),
(135,'.NET Core 学习资料精选：进阶','主要分享一些.NET Core比较优秀的社区资料和微软官方资料。我进行了知识点归类，让大家可以更清晰的学习.NET Core。\n\n首先感谢资料原作者的贡献。','','https://www.cnblogs.com/heyuquan/p/dotnet-advance-learning-resource.html',15,4,'2021-11-10 14:45:01','2021-11-17 17:09:11'),
(136,'ASP.NET Core on K8S 入门学习系列文章目录','K8S的入门学习放到了2019年的学习列表中，并总结了一些学习笔记和实践总结的文章并汇总在这里，希望对各位园友有帮助！','','https://www.cnblogs.com/edisonchou/',15,4,'2021-11-10 14:45:01','2021-11-18 12:28:04'),
(137,'.NET Core微服务架构学习与实践系列文章目录','拥抱开源，任重而道远！','K8S的入门学习放到了2019年的学习列表中，并总结了一些学习笔记和实践总结的文章并汇总在这里，希望对各位园友有帮助！','https://www.cnblogs.com/edisonchou/p/dotnetcore_microservice_foundation_blogs_index_final.html',15,4,'2021-11-10 14:45:01','2021-11-18 10:43:50'),
(138,'C# 官方语言指南','提供许多有关 C# 语言学习资源、新增功能、概念、操作指南、编程指南和语言参考等。','','https://docs.microsoft.com/zh-cn/dotnet/csharp/',13,4,'2021-11-10 14:45:01','2021-11-17 17:04:51'),
(139,'ASP.NET Core 教程','跨平台的高性能开源框架，用于在 Windows、Mac 或 Linux 上开发基于现代化的 Web 应用程序。','','https://docs.microsoft.com/zh-cn/aspnet/core/?view=aspnetcore-5.0',15,4,'2021-11-10 14:45:01','2021-11-18 10:59:12'),
(140,'EF Core 官方教程','Entity Framework (EF) Core 是轻量化、可扩展、开源和跨平台版的常用数据访问技术。','','https://docs.microsoft.com/zh-cn/ef/core/',13,4,'2021-11-10 14:45:01','2021-11-17 16:56:56'),
(141,'Visual Studio 文档','学习使用强大功能提高开发效率，开发、生成、调试、测试、部署、版本控制、 DevOps 和性能分析','','https://docs.microsoft.com/zh-cn/visualstudio/?view=vs-2019',13,4,'2021-11-10 14:45:01','2021-11-17 16:56:48'),
(142,'.NET 微服务应用程序架构指南','本指南介绍如何使用 .NET Core 和 Docker 容器开发基于微服务的应用程序并对其进行管理。','','https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/',4,4,'2021-11-10 14:45:01','2021-11-18 10:43:30'),
(143,'微软 eShopOnWeb 开源框架','基于 ASP.NET Core 构建的单体分层应用架构，使用 DDD 领域驱动设计程序体系结构和部署模型。','','https://www.cnblogs.com/MrHSR/p/10855824.html',4,4,'2021-11-10 14:45:01','2021-11-17 16:56:23'),
(144,'IdentityServer4中文文档','dentityServer4 是一个免费的开源 OpenID Connect 和 OAuth 2.0 身份认证与授权框架，适用于 ASP.NET Core 平台，IdentityServer4 由 Dominick Baier 和 Brock Allen 两位大神创建和维护，您可以快捷的在应用程序中集成基于令牌的身份验证，单点登录和 API 访问控制，支持非常多的协议实现和可扩展点，IdentityServer4 由 OpenID 基金会正式认证，因此符合规范且可互操作，被微软作为 .NET 基金会项目的一部分，并根据其行为准则运行，虽然这个框架也非常的好，博客也不少，但以下整理的中文文档值得推荐学习。','','https://www.xcode.me/post/6038#',4,4,'2021-11-10 14:45:01','2021-11-17 16:56:30'),
(145,'dnSpy基于.NET的反编译工具','dnSpy是一款基于.NET的反编译与调试工具，开源免费，能够讲.NET开发的Exe和Dll程序集反编译为C#代码，同时支持断点调试和代码二次编辑，如果您只有编译后的程序集，在没有源码的情况下想还原C#源码，dnSpy绝对是首选。','','https://github.com/dnSpy/dnSpy/releases',4,4,'2021-11-10 14:45:01','2021-11-17 17:05:15'),
(146,'Visual Studio 2015','软就放出了VS2015不同版本的离线安装镜像包，支持32位和64位，现在，您就可以下载并安装它','','https://www.xcode.me/post/1916',14,4,'2021-11-10 14:45:01','2021-11-17 16:55:55'),
(147,'微软官方常用系统工具合集','这些小工具原本是为了解决工程师们平常在工作上遇到的各种问题而开发的，之后他们将这些工具集合起来称为 Sysinternals，并免费提供公众下载，其中部分还开源了，一直以来都颇受 IT 界人士的好评。如果把管理员比喻成战士的话，那么 Sysinternals 就是我们手中的良兵利器。熟悉和掌握这些工具，并且对 Windows 的体系有一定的了解，将大幅提高你的电脑维护、应用技能。','','https://www.xcode.me/post/1631',4,4,'2021-11-10 14:45:01','2021-11-17 17:04:59'),
(149,'零度编程','分享编程之美','','https://www.xcode.me/',8,4,'2021-11-10 14:45:01','2021-11-17 16:54:17'),
(150,'技术胖','专注前端开发,每年100集免费视频。','https://blogimages.jspang.com/blogtouxiang1.jpg','http://www.jspang.com/',8,4,'2021-11-10 14:45:01','2021-11-17 16:40:28'),
(151,'网站(Web App)','这里包含了基于Vue.js开发的网站应用程序，包括管理工具、网页游戏、购物社交网站等。','','https://madewith.cn/',3,4,'2021-11-10 14:45:01','2021-11-17 16:40:20'),
(152,'任务协助系统','任何业务场景，您都可以找到合适的方案 PearProject 拥有丰富且灵活的产品研发管理功能，协助您释放产品研发能力，是推动研发进程的强力驱动','','https://home.vilson.xyz/?from=madewith.cn#/',13,4,'2021-11-10 14:45:01','2021-11-17 16:40:10'),
(154,'npm','NPM（node package manager）是 Node.js 世界的包管理器。NPM 可以让 JavaScript 开发者在共享代码、复用代码以及更新共享的代码上更加方便。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/npm.png','https://www.npmjs.cn/',13,4,'2021-11-10 14:45:01','2022-10-18 10:02:47'),
(155,'Lerna','Lerna 是一个管理工具，用于管理包含多个软件包（package）的 JavaScript 项目。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/lernajs.png','https://lernajs.bootcss.com/',5,4,'2021-11-10 14:45:01','2021-11-18 10:36:35'),
(156,'Vue.js','Vue.js - 是一套构建用户界面的渐进式框架。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/vuejs.png','https://vuejs.bootcss.com/guide/',3,4,'2021-11-10 14:45:01','2021-11-17 16:39:56'),
(157,'Nuxt.js','Nuxt.js 是一个基于 Vue.js 的通用应用框架。通过对客户端/服务端基础架构的抽象组织，Nuxt.js 主要关注的是应用的 UI渲染。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/nuxtjs.png','https://www.nuxtjs.cn/',19,4,'2021-11-10 14:45:01','2021-11-18 10:36:01'),
(158,'Parcel','Parcel - 极速、零配置的 web 应用打包工具。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/parcel.png','https://www.parceljs.cn/',13,4,'2021-11-10 14:45:01','2021-11-17 16:54:33'),
(160,'Pro Git','Pro Git 中文版（第二版）是一本详细的 Git 指南，主要介绍了 Git 的使用基础和原理，让你从 Git 初学者成为 Git 专家。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/progit.png','https://www.progit.cn/',13,4,'2021-11-10 14:45:01','2021-11-18 10:36:26'),
(161,'PurgeCSS','PurgeCSS 是一个用来删除未使用的 CSS 代码的工具，能够减小 CSS 文件的体积。例如可以用来减小 Bootstrap 等前端框架的文件体积，提升加载速度。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/purgecss.png','https://www.purgecss.cn/',9,4,'2021-11-10 14:45:01','2021-11-17 16:39:47'),
(162,'Markdown','Markdown 是一种轻量级标记语言，便于人们使用易读易写的纯文本格式编写文档并添加格式元素。Markdown 是 John Gruber 于 2004 年创建的。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/markdown.png','https://www.markdown.xyz/',20,4,'2021-11-10 14:45:01','2021-11-16 16:37:14'),
(163,'ESLint','ESLint 是一个插件化并且可配置的 JavaScript 语法规则和代码风格的检查工具。ESLint 能够帮你轻松写出高质量的 JavaScript 代码。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/eslint.png','https://cn.eslint.org/',3,4,'2021-11-10 14:45:01','2021-11-16 16:37:05'),
(164,'Infima CSS 框架','Infima 是 Facebook 出品的一个 CSS 框架，专为内容驱动型网站而设计，并且内建对暗模式的支持。是 Docusaurus 的姊妹项目。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/infima.png','https://infima.bootcss.com/',9,4,'2021-11-10 14:45:01','2022-10-18 09:56:38'),
(165,'Stylus','Stylus - 富于表现力、健壮、功能丰富的 CSS 预处理语言。','https://cdn.jsdelivr.net/npm/@bootcss/www.bootcss.com@0.0.37/dist/img/stylus.png','https://stylus.bootcss.com/',9,4,'2021-11-10 14:45:01','2021-11-17 16:39:38'),
(166,'懒得勤快','勤于发现,乐于分享','https://git.imweb.io/ldqk/imgbed/raw/master/20190606/5dc7fc1266bfd8109d1ef5e0e7630f2c_2_3_art.png','https://masuit.org/',8,4,'2021-11-10 14:45:01','2022-11-02 16:34:25'),
(167,'爱哔哔','视频解析','','https://www.ibilibili.com/',14,4,'2021-11-10 14:45:01','2021-11-16 16:36:45'),
(168,'果核剥壳','工具分享','https://www.ghpym.com/wp-content/uploads/2019/12/2020logo.png','https://www.ghpym.com/category/all/pcsoft/pccode',14,4,'2021-11-10 14:45:01','2021-11-16 16:36:59'),
(169,'醉秋风','要相信一切都是最好的安排','https://blog.slomoo.cn/slomoo.png','https://blog.slomoo.cn/',8,4,'2021-11-10 14:45:01','2021-11-16 16:36:27'),
(170,'薛定喵君','scan to see more ?','http://tiaocaoer.com/images/site_icon.png','http://tiaocaoer.com/',8,4,'2021-11-10 14:45:01','2021-11-16 16:36:51'),
(171,'舔狗日记','我们是狗,是舔狗','https://we.dog/assets/images/logo.gif','https://we.dog/',10,4,'2021-11-10 14:45:01','2021-11-16 16:35:44'),
(172,'rscss','CSS样式表结构的合理系统。\n一组简单的想法可以指导您构建可维护CSS的过程。','https://we.dog/assets/images/logo.gif','https://rscss.io/index.html',9,4,'2021-11-10 14:45:01','2021-11-17 16:39:20'),
(173,'WEB安全色','WEB安全色','','https://www.bootcss.com/p/websafecolors/',9,4,'2021-11-10 14:45:01','2021-11-16 16:36:00'),
(174,'Adobe Color ','色輪 (或「擷取主題」標籤中的影像)','','https://color.adobe.com/zh/create/color-wheel',9,4,'2021-11-10 14:45:01','2021-11-18 10:36:08'),
(175,'中国色 ','中国色 ','','http://zhongguose.com/',9,4,'2021-11-10 14:45:01','2021-11-16 16:35:52'),
(176,'托盘式 ','利用数百万设计师的知识生成漂亮的调色板。','','https://www.palettable.io/CCCC82',9,4,'2021-11-10 14:45:01','2021-11-17 17:04:42'),
(177,'itmeo','WebGradients 是180个线性渐变的免费集合，您可以将其用作\n网站任何部分的内容背景。轻松复制CSS3跨浏览器代码\n，稍后使用！我们还为每个渐变准备了.PNG版本。\n 作为奖励，还有用于Sketch  ＆  Photoshop的软件包 。','','https://webgradients.com/',9,4,'2021-11-10 14:45:01','2021-11-17 16:56:42'),
(178,'配色表','网页设计常用色彩搭配表','','http://tool.c7sky.com/webcolor/',9,4,'2021-11-10 14:45:01','2021-11-16 16:34:27'),
(179,'avascript粒子动画引擎','avascript粒子动画引擎','','https://drawcall.github.io/Proton/#examples',9,4,'2021-11-10 14:45:01','2021-11-16 16:36:20'),
(180,'Keyframes helps you write better CSS','Dead simple visual tools to help you generate CSS for your projects.','','https://keyframes.app/',9,4,'2021-11-10 14:45:01','2021-11-16 16:35:34'),
(181,'Ant Design Pro','开箱即用的中台前端/设计解决方案','','https://pro.ant.design/index-cn',19,4,'2021-11-10 14:45:01','2021-11-17 16:39:29'),
(182,'Laravel诗词博客','','','https://www.qqphp.com/article',8,4,'2021-11-10 14:45:01','2021-11-16 15:46:38'),
(183,'hexo','快速、简洁且高效的博客框架','','https://hexo.io/zh-cn/',13,4,'2021-11-10 14:45:01','2021-11-16 16:35:21'),
(184,'Gridea',' 是一个静态博客写作客户端，帮助你更容易地构建并管理博客或任何静态站点。','','https://gridea.dev/',13,4,'2021-11-10 14:45:01','2021-11-16 16:35:29'),
(185,'awesome-bookmarks','个人收藏夹','','https://panjiachen.github.io/awesome-bookmarks/',11,4,'2021-11-10 14:45:01','2021-11-18 10:36:18'),
(186,'VueRequest','⚡️ 一个很酷的 Vue3 的请求库','','https://cn.attojs.org/',3,4,'2021-11-10 14:45:01','2021-11-16 16:34:20'),
(187,'牛客网','','','https://www.nowcoder.com/profile/8768562',17,4,'2021-11-10 14:45:01','2021-11-16 15:44:54'),
(188,'TinyPNG','','','https://tinypng.com/',18,4,'2021-11-10 14:45:01','2021-11-16 16:36:09'),
(189,'长征部落格','事 不 三 思 终 有 悔， 人 能 百 忍 自 无 忧。','','https://www.cz5h.com/',8,4,'2021-11-10 14:45:01','2021-11-16 15:44:02'),
(190,'Zidone','莫道君行早 更有早行人','','https://www.aye.ink/',8,4,'2021-11-10 14:45:01','2021-11-16 16:34:12'),
(191,'天涯社区','提供论坛、部落、博客、问答、文学、相册、个人空间等服务。拥有天涯杂谈、娱乐八卦、情感天地等人气栏目,以及关天茶舍、煮酒论史等高端人文论坛。','https://static.tianyaui.com/global/bbs/web/static/images/weixin_code.jpg','https://bbs.tianya.cn/',10,4,'2021-11-10 14:45:01','2021-11-16 15:45:07'),
(192,'胶囊日记','凌晨零点，集体失忆','http://s4.timepill.net/s/w220/topic/74toih.png','http://www.timepill.net/',10,4,'2021-11-10 14:45:01','2021-11-16 15:45:01'),
(193,'码库CTOlib','收集GitHub上的实用dotnet开源项目，并进行分类。每天都有新的库和项目添加到列表中。','https://www.ctolib.com/static/img/getqrcode.jpg','https://www.ctolib.com/',11,4,'2021-11-10 14:45:01','2021-11-17 16:55:45'),
(194,'SegmentFault','每一位开发者都在贡献和更新技术内容，共同参与社区建设，维护社区秩序。\n\n如果你和我们一样有技术理想，并愿意贡献自己的力量，欢迎加入我们。','https://cdn.segmentfault.com/r-d209f51c/static/logo-b.d865fc97.svg','https://segmentfault.com/',10,4,'2021-11-10 14:45:01','2021-11-17 16:55:37'),
(195,'LearnKu','编程者社区','https://cdn.learnku.com/uploads/images/201901/24/1/OyBnfB2vlk.png!/both/44x44','https://learnku.com/',10,4,'2021-11-10 14:45:01','2021-11-16 11:54:14'),
(196,'毒导航','网络资源','https://www.toxic.ltd/wp-content/uploads/2020/04/lang_logo.png','https://www.toxic.ltd/',11,4,'2021-11-10 14:45:01','2021-11-16 15:44:35'),
(197,'tailblocks','tailblocks','','https://tailblocks.cc/',19,4,'2021-11-10 14:45:01','2021-11-16 16:33:56'),
(198,'TailwindCSS 中文网','TailwindCSS 使用教程、TailwindCSS 中文文档及 TailwindCSS 相关资源','https://tailwindchina.com/logo.png','https://tailwindchina.com/',13,4,'2021-11-10 14:45:01','2021-11-16 15:46:22'),
(199,'vue-tailwind.com','针对 TailwindCss 优化的 Lightview 和完全可定制的 Vue 组件集','','https://www.vue-tailwind.com/',19,4,'2021-11-10 14:45:01','2021-11-16 16:34:05'),
(200,'emantic UI','用户界面就是 Web 的语言','','https://semantic-ui.com/',19,4,'2021-11-10 14:45:01','2021-11-16 15:46:31'),
(201,'be-a-professional-programmer','成为专业程序员路上用到的各种优秀资料、神器及框架','','http://tools.stanzhai.site/',13,4,'2021-11-10 14:45:01','2021-11-16 11:39:41'),
(202,'Bulma','现代化的CSS框架','Bulma是一个免费、开源的CSS框架，它提供了易于使用的前端的组件，您可以轻松地组合这些组件来构建响应式Web界面。','https://bulma.zcopy.site/',9,4,'2021-11-10 14:45:01','2022-10-18 09:56:31'),
(203,'PhotoKit','图片编辑器','https://photokit.com/images/editor.min.webp','https://photokit.com/?lang=zh',18,4,'2021-11-10 14:45:01','2021-11-16 14:15:02'),
(204,'V2EX','V2EX 是一个关于分享和探索的地方','','https://www.v2ex.com/',10,4,'2021-11-10 14:45:01','2021-11-16 15:44:16'),
(205,'screensiz','screensiz','','https://screensiz.es/phone',14,4,'2021-11-10 14:45:01','2021-11-17 16:55:28'),
(206,'Vue Router','用 Vue + Vue Router 创建单页应用非常简单：通过 Vue.js，我们已经用组件组成了我们的应用。当加入 Vue Router 时，我们需要做的就是将我们的组件映射到路由上，让 Vue Router 知道在哪里渲染它们。','https://cdn4.buysellads.net/uu/1/3386/1525189943-38523.png','https://next.router.vuejs.org/',3,4,'2021-11-10 14:45:01','2021-11-16 15:44:08'),
(207,'View UI','View UI®，即原先的 iView，是一套基于 Vue.js 的开源 UI 组件库，主要服务于 PC 界面的中后台产品。','https://file.iviewui.com/dist/7dcf5af41fac2e4728549fa7e73d61c5.svg','https://www.iviewui.com/docs/introduce',19,4,'2021-11-10 14:45:01','2021-11-16 15:42:35'),
(208,'it365万能解码器','it365万能解码器，能自动识别各种编码的文本文件，如：GB2312、Big5、UTF-8等。此解码器小巧，但是非常强悍。支持几十种编码，支持自动探测文件编码。一个web网页程序，就能解决文件乱码、编码转换等工作。','https://c10365.now.sh/zan/active.jpg','https://it365.gitlab.io/zh-cn/decode/?81206z',14,4,'2021-11-10 14:45:01','2021-11-16 14:15:09'),
(209,'果糖网net论坛','','https://www.donet5.com/HtmlTemplate2/com_files/logo.png','https://www.donet5.com/',10,4,'2021-11-10 14:45:01','2021-11-16 14:17:04'),
(210,'Vue3 One Piece','下一代web开发方式，更快，更轻，易维护，更多的原生支持','https://static.vue-js.com/6280b990-ff19-11ea-85f6-6fac77c0c9b3.png','https://vue3js.cn/',3,4,'2021-11-10 14:45:01','2021-11-16 14:14:50'),
(211,'vuepress-theme-vdoing','一款简洁高效的VuePress 知识管理&博客 主题','https://cdn.jsdelivr.net/gh/xugaoyi/image_store/blog/20200409124835.png','https://doc.xugaoyi.com/',13,4,'2021-11-10 14:45:01','2021-11-16 15:42:27'),
(212,'TailwindCSS的漂亮扩展。','Tailwind Starter Kit是免费和开源的。它不会更改或从TailwindCSS向已添加的CSS添加任何CSS 。它具有多个HTML元素，并带有ReactJS，Vue和Angular的动态组件。','','https://www.creative-tim.com/learning-lab/tailwind-starter-kit/presentation',19,4,'2021-11-10 14:45:01','2021-11-16 11:57:06'),
(213,'创造者日报','一群创造者','','https://creatorsdaily.com/',10,4,'2021-11-10 14:45:01','2021-11-16 14:13:23'),
(214,'唧唧','','','https://www.jijidown.com/',8,4,'2021-11-10 14:45:01','2021-11-16 14:14:34'),
(215,'OpenJudge','OpenJudge是开放的在线程序评测系统','','http://openjudge.cn/',14,4,'2021-11-10 14:45:01','2021-11-16 14:13:18'),
(217,'万有导航','','','http://wanyouw.com/',11,4,'2021-11-10 14:45:01','2021-11-16 14:13:05'),
(218,'极客阅读','','','https://jikeyuedu.cn/topic/Vue.js',10,4,'2021-11-10 14:45:01','2021-11-16 14:12:59'),
(219,'鱼塘热榜','','','https://mo.fish/',10,4,'2021-11-10 14:45:01','2021-11-16 14:12:52'),
(220,'老张的哲学','','','https://www.cnblogs.com/laozhang-is-phi/',8,4,'2021-11-10 14:45:01','2021-11-16 11:55:03'),
(221,'软件盒子','','','https://www.bsc1011.top/',11,4,'2021-11-10 14:45:01','2021-11-16 14:12:47'),
(222,'编程导航','站长是腾讯全栈开发 & 腾讯云开发高级布道师，欢迎关注他的  微信公众号【程序员鱼皮】，或添加  微信 liyupi66  交流学习 ','','https://www.code-nav.cn/recommend',11,4,'2021-11-10 14:45:01','2021-11-16 14:12:39'),
(223,'手把手教你AspNetCore WebApi：数据验证','小明最近又遇到麻烦了，小红希望对接接口传送的数据进行验证，既然是小红要求，那小明说什么都得满足呀，这还不简单嘛。','','https://www.cnblogs.com/zcqiand/p/13795675.html',15,4,'2021-11-10 14:45:01','2021-11-16 14:12:33'),
(224,'缓存（MemoryCache和Redis）','这几天小明又有烦恼了，系统上线一段时间后，系统性能出现了问题，马老板很生气，叫小明一定要解决这个问题。性能问题一般用什么来解决呢？小明第一时间想到了缓存。','','https://www.cnblogs.com/zcqiand/p/13816732.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:55:11'),
(225,'Autofac一个优秀的.NET IoC框架','Autofac与C#语言的结合非常紧密，并学习它非常的简单，也是.NET领域最为流行的IoC框架之一。','','https://www.cnblogs.com/zcqiand/p/14257650.html',15,4,'2021-11-10 14:45:01','2021-11-16 14:12:27'),
(226,'.net Core 使用AutoMapper','在我们的项目中慢慢的要把数据库的实体模型和视图模型进行分离，防止被人拿到我们表字段。在学校的时候自己只是有将很多数据库模型，写成一个视图模型返回到前台。','','https://www.cnblogs.com/chenxi001/p/11800943.html',15,4,'2021-11-10 14:45:01','2021-11-16 14:12:21'),
(227,'Autofac 框架初识与应用','AutoFac是一个开源的轻量级的依赖注入容器，也是.net下比较流行的实现依赖注入的工具之一。','','https://www.cnblogs.com/i3yuan/archive/2021/04/13/14654547.html',15,4,'2021-11-10 14:45:01','2021-11-16 14:12:15'),
(228,'Vue项目中实现用户登录及token验证','','','https://www.cnblogs.com/web-record/p/9876916.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:56:52'),
(229,'asp.net core 批量依赖注入服务','看园子里netcore的文章都是简单的注入几个服务的例子,在项目中肯定不会一行一行的写注册服务的代码，参考网上，找到一些解决方案，根据自己实际需求进行更改，特记录下来。','','https://www.cnblogs.com/5jia0/archive/2021/04/14/14658642.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:56:46'),
(230,'张鑫旭的个人主页','张鑫旭-鑫空间-鑫生活','','https://www.zhangxinxu.com/',8,4,'2021-11-10 14:45:01','2021-11-16 11:56:41'),
(231,'colorui文档','','','https://www.kancloud.cn/m22543/colorui/1289223',13,4,'2021-11-10 14:45:01','2021-11-16 11:56:35'),
(232,'lucky-canvas','一个基于 Js + Canvas 的【大转盘 & 九宫格】抽奖，致力于为 web 前端提供一个功能强大且专业可靠的组件，只需要通过简单配置即可实现自由化定制，帮助你快速的完成产品需求','','https://100px.net/',24,4,'2021-11-10 14:45:01','2021-11-16 11:56:30'),
(233,'[C#] NAudio 库的各种常见使用方式: 播放 录制 转码 音频可视化','在 NAudio 中, 常用类型有 WaveIn, WaveOut, WaveStream, WaveFileWriter, WaveFileReader, AudioFileReader 以及接口: IWaveProvider, ISampleProvider, IWaveIn, IWavePlayer','','https://www.cnblogs.com/slimenull/p/14735111.html',4,4,'2021-11-10 14:45:01','2021-11-16 11:56:24'),
(234,'.NET之生成数据库全流程','本文主要是回顾下从项目创建到生成数据到数据库(代码优先)的全部过程。采用EFCore作为ORM框架。','','https://www.cnblogs.com/azrng/p/14757769.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:56:16'),
(235,'24K导航','','','https://www.24kdh.com/',11,4,'2021-11-10 14:45:01','2021-11-16 11:54:52'),
(236,'印记中文','','','https://docschina.org/',11,4,'2021-11-10 14:45:01','2021-11-16 11:54:47'),
(237,'NuxtJS ','NuxtJS 让你构建你的下一个 Vue.js 应用程序变得更有信心。这是一个 开源 的框架，让 web 开发变得简单而强大','','https://www.nuxtjs.cn/',3,4,'2021-11-10 14:45:01','2021-11-16 11:54:41'),
(238,'Tailwind CSS文档','开始 Tailwind CSS 之旅\n用最适合您的方式学习 Tailwind','','https://www.tailwindcss.cn/docs',13,4,'2021-11-10 14:45:01','2021-11-16 11:54:36'),
(239,'outils 代码库','','','https://www.npmjs.com/package/outils',11,4,'2021-11-10 14:45:01','2021-11-16 11:54:31'),
(240,'30 seconds of code ','代码片段','','https://www.30secondsofcode.org/',5,4,'2021-11-10 14:45:01','2021-11-16 11:54:24'),
(243,'Pagination.js','分页控件','','http://pagination.js.org/',24,4,'2021-11-10 14:45:01','2021-11-16 11:54:03'),
(244,'Lodash 中文文档','Lodash 是一个一致性、模块化、高性能的 JavaScript 实用工具库。','','https://www.lodashjs.com/',5,4,'2021-11-10 14:45:01','2021-11-16 11:53:57'),
(245,'微信SDK微信SDK','','','https://sdk.weixin.senparc.com/Document',4,4,'2021-11-10 14:45:01','2021-11-16 11:53:52'),
(246,'VUE API 手册','','','https://vue3js.cn/vue-composition-api/',3,4,'2021-11-10 14:45:01','2021-11-16 11:53:47'),
(247,'极客导航','','','https://geekdocs.cn/',11,4,'2021-11-10 14:45:01','2021-11-16 11:47:47'),
(248,'.NET 官方文档','了解如何使用 .NET 在任何使用 C#、F# 和 Visual Basic 的平台上创建应用程序。 浏览 API 引用、代码示例、教程以及其他内容。','','https://docs.microsoft.com/zh-cn/dotnet/',4,4,'2021-11-10 14:45:01','2021-11-16 11:53:42'),
(249,'开源前哨','分享热门、有趣和实用的开源项目～','','https://www.zhihu.com/column/c_1317124962785062912',11,4,'2021-11-10 14:45:01','2021-11-16 11:53:35'),
(250,'DotNet 资源大全中文版','DotNet 资源大全中文版，内容包括：编译器、压缩、应用框架、应用模板、加密、数据库、反编译、IDE、日志、风格指南等。','','https://github.com/jobbole/awesome-dotnet-cn#api',11,4,'2021-11-10 14:45:01','2021-11-16 11:53:29'),
(251,'C#/.NET/.NET Core学习视频汇总（持续更新ing）','之前有很多小伙伴在我的公众号后台留言问有没有C#/.NET/.NET Core这方面相关的视频推荐，我一般都会推荐他们去B站搜索一下。今天刚好有空收集了网上一些比较好的C#/.NET/.NET Core这方面的学习视频，希望能够帮助到有需要的小伙伴们。当然假如你有更好的资源视频推荐可以在我的文章下面留言，开篇之前我要感谢各位小伙伴对【C#/.NET/.NET Core学习、工作、面试指南','','https://www.cnblogs.com/Can-daydayup/p/15046838.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:52:38'),
(256,'MYSQL中数据类型介绍','MySQL的数据类型','1','https://www.cnblogs.com/-xlp/p/8617760.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:53:17'),
(287,'LBO.net','保持饥饿！保持愚蠢！ ->C#在线编辑','','https://www.cnblogs.com/lbonet/',8,4,'2021-11-10 14:45:01','2021-11-16 11:44:54'),
(288,'牛客网','求职之前，先上牛客','','https://www.nowcoder.com/',17,4,'2021-11-10 14:45:01','2021-11-16 11:47:36'),
(289,'GitHub Profile README Generator','简历生成','','https://rahuldkjain.github.io/gh-profile-readme-generator/',14,4,'2021-11-10 14:45:01','2021-11-16 11:53:11'),
(290,'vue3最新学习资料集合，不断更新','vue3最新学习资料集合，不断更新','','https://learnku.com/articles/48928',3,4,'2021-11-10 14:45:01','2021-11-16 11:53:05'),
(292,'wangEditorV5','wangEditorV4\nTypescript 开发的 Web 富文本编辑器， 轻量、简洁、易用、开源免费','','https://www.wangeditor.com/',20,4,'2021-11-10 14:45:01','2022-10-11 13:56:18'),
(293,'最好的uniapp入门实战教程','uniapp是Dcloud公司的产品，是一个跨端开发框架，基于vue.js技术栈。开发者编写一套代码，可发布到iOS、Android、Web（响应式）、以及各种小程序（微信/支付宝/百度/头条/QQ/钉钉/淘宝）、快应用等多个平台。这是它的功能架构图','','https://juejin.cn/post/6899642866693423111#heading-16',15,4,'2021-11-10 14:45:01','2021-11-16 11:47:55'),
(294,'EFCore之详细增删改查','EFCore之详细增删改查','','https://juejin.cn/post/6965727147189075976#heading-31',15,4,'2021-11-10 14:45:01','2021-11-16 11:52:53'),
(295,'NET Core和Blog.Core【老张的哲学】','NET Core和Blog.Core【老张的哲学】','','https://www.yuque.com/docs/share/c58f37a4-677c-4a08-b240-4f7f4088a63b#dlCt7',15,4,'2021-11-10 14:45:01','2021-11-16 11:52:46'),
(296,'Vue3+Vite工程常用工具的接入方法','Vue3+Vite工程常用工具的接入方法','','https://juejin.cn/post/6982476410279460878',15,4,'2021-11-10 14:45:01','2021-11-16 11:48:02'),
(297,'Uni-App从入门到实战-黑马程序员杭州校区出品','Uni-App从入门到实战-黑马程序员杭州校区出品','','https://www.bilibili.com/video/BV1BJ411W7pX?p=40',15,4,'2021-11-10 14:45:01','2021-11-16 11:45:02'),
(298,'dotNet全栈开发','dotNet全栈开发\n.NET Core\\xamarin爱好者、篮球狂热爱好者https://dwz.cn/ppnuFzrZ','','https://blog.csdn.net/kebi007',8,4,'2021-11-10 14:45:01','2021-11-16 11:39:27'),
(299,'什么是 Docker','Docker 最初是 dotCloud 公司创始人 Solomon Hykes (opens new window)在法国期间发起的一个公司内部项目，它是基于 dotCloud 公司多年云服务技术的一次革新，并于 2013 年 3 月以 Apache 2.0 授权协议开源 (opens new window)，主要项目代码在 GitHub (opens new window)上进行维护。Docker 项目后来还加入了 Linux 基金会，并成立推动 开放容器联盟（OCI） (opens new window)。','','https://vuepress.mirror.docker-practice.com/',23,4,'2021-11-10 14:45:01','2021-11-15 14:30:37'),
(300,'编程之家','','','https://www.jb51.cc/netcore/index_2.html',11,4,'2021-11-10 14:45:01','2021-11-16 11:39:18'),
(304,'.NetCore中EFCore的使用整理','EntirtyFramework框架是一个轻量级的可扩展版本的流行实体框架数据访问技术.\n\n其中的.NetCore版本对应EntityFrameworkCore','','https://www.cnblogs.com/tianma3798/p/6835400.html',4,4,'2021-11-10 14:45:01','2021-11-16 11:39:00'),
(305,'【EFCORE笔记】添加数据的多种方案','','','https://www.cnblogs.com/lbonet/p/14599549.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:38:54'),
(306,'ES6 入门教程','《ECMAScript 6 入门教程》是一本开源的 JavaScript 语言教程，全面介绍 ECMAScript 6 新引入的语法特性。','','https://es6.ruanyifeng.com/',15,4,'2021-11-10 14:45:01','2021-11-16 11:38:48'),
(307,'图文','','','https://pixabay.com/',18,4,'2021-11-10 14:45:01','2021-11-16 11:38:41'),
(308,'免费个人图床搭建gitee+PicGo','','','https://www.cnblogs.com/jiba/p/15147616.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:38:35'),
(309,'Moment.js','JavaScript 日期处理类库','','http://momentjs.cn/',5,4,'2021-11-10 14:45:01','2021-11-16 11:38:29'),
(311,'泽泽社长','','','https://zezeshe.com/',8,4,'2021-11-10 14:45:01','2021-11-16 11:29:47'),
(312,'逗比表情包','','','https://www.dbbqb.com/',7,4,'2021-11-10 14:45:01','2021-11-16 11:29:41'),
(313,'两个BT','','','https://www.bttwo.com/',7,4,'2021-11-10 14:45:01','2021-11-16 11:29:36'),
(314,'电影蜜蜂','','','https://www.idybee.com/',7,4,'2021-11-10 14:45:01','2021-11-16 11:29:31'),
(315,'人人影视','','','https://yyets.dmesg.app/home',7,4,'2021-11-10 14:45:01','2021-11-16 11:29:26'),
(316,'努努影视','','','https://www.nunuyy.cc/',7,4,'2021-11-10 14:45:01','2021-11-16 11:29:20'),
(317,'人人美剧','','','https://www.meiju11.com/',7,4,'2021-11-10 14:45:01','2021-11-16 11:29:09'),
(318,'pianku','','','https://www.pianku.li/',7,4,'2021-11-10 14:45:01','2021-11-16 11:29:16'),
(319,'换脸','','','https://myvoiceyourface.com/',14,4,'2021-11-10 14:45:01','2021-11-16 11:28:57'),
(320,'ECMAScript 6 入门','《ECMAScript 6 入门教程》是一本开源的 JavaScript 语言教程，全面介绍 ECMAScript 6 新引入的语法特性。','','https://es6.ruanyifeng.com/',5,4,'2021-11-10 14:45:01','2021-11-16 11:28:42'),
(321,'Windi CSS','下一代工具类 CSS 框架','','https://cn.windicss.org/',19,4,'2021-11-10 14:45:01','2021-11-16 11:28:37'),
(322,'wow.js','滚动时显示动画。非常Animate.css朋友 :-)\n轻松自定义动画设置：样式、延迟、长度、偏移、迭代...','https://www.delac.io/WOW/img/wow-logo.jpg','https://www.delac.io/WOW/',5,4,'2021-11-10 14:45:01','2021-11-16 11:28:27'),
(323,'.Net Core + DDD基础分层 + 项目基本框架 + 个人总结','','','https://www.cnblogs.com/shijiehaiyang/p/14918544.html',4,4,'2021-11-10 14:45:01','2021-11-16 11:28:21'),
(324,'一文梳理CSS必会知识点','','','https://juejin.cn/post/6854573212337078285',15,4,'2021-11-10 14:45:01','2022-10-18 09:55:09'),
(325,'30个你必须熟记的CSS选择器','','','https://code.tutsplus.com/zh-hans/tutorials/the-30-css-selectors-you-must-memorize--net-16048',15,4,'2021-11-10 14:45:01','2022-10-18 09:55:26'),
(326,'深入理解 TypeScript','','','https://jkchao.github.io/typescript-book-chinese/#why',15,4,'2021-11-10 14:45:01','2021-11-16 11:28:03'),
(327,'Vue Patterns CN','有用的Vue模式，技巧，提示和技巧以及有帮助的精选链接。','','https://zyszys.github.io/vue-patterns-cn/',3,4,'2021-11-10 14:45:01','2021-11-16 11:27:57'),
(328,'代码整洁的 JavaScript','','','https://github.com/beginor/clean-code-javascript',5,4,'2021-11-10 14:45:01','2021-11-15 14:33:46'),
(329,'JavaScript 风格指南','','','https://github.com/alivebao/clean-code-js',5,4,'2021-11-10 14:45:01','2021-11-16 11:27:49'),
(330,'typescript基础史上最强学习文章','','','https://juejin.cn/post/7018805943710253086',15,4,'2021-11-10 14:45:01','2021-11-16 11:27:41'),
(331,'CSS Icons','Open-source CSS, SVG and Figma UI Icons\nAvailable in SVG Sprite, styled-components, NPM & API','','https://css.gg/',9,4,'2021-11-10 14:45:01','2021-11-15 14:33:06'),
(332,'发现导航','','','https://www.nav3.cn/#/light',11,4,'2021-11-10 14:45:01','2021-11-15 14:32:33'),
(333,'50个Vue知识点','','','https://juejin.cn/post/6984210440276410399#heading-21',15,4,'2021-11-10 14:45:01','2021-11-16 11:27:19'),
(334,'daisyUI','Tailwind CSS Components','','https://daisyui.com/',19,4,'2021-11-10 14:45:01','2021-11-16 11:27:11'),
(335,'Vue风格指南','这里是官方的 Vue 特有代码的风格指南。如果在工程中使用 Vue，为了回避错误、小纠结和反模式，该指南是份不错的参考。不过我们也不确信风格指南的所有内容对于所有的团队或工程都是理想的。所以根据过去的经验、周围的技术栈、个人价值观做出有意义的偏差是可取的。','','https://cn.vuejs.org/v2/style-guide/index.html',3,4,'2021-11-10 14:45:01','2021-11-15 14:32:25'),
(336,'SqlSugar','NET 开源ORM框架，由果糖大数据科技团队维护和更新 ，开箱即用\n最易上手的ORM框架 ，51Job和Boss直招简历数超过 国外框架 Nhibernate PetaPoco, \n仅次于Dapp','','https://www.donet5.com/',29,4,'2021-11-10 14:45:01','2022-10-18 08:47:54'),
(337,'使用 FluentValidation 实现数据校验、验重','','','https://www.cnblogs.com/zl33842902/p/13514929.html',15,4,'2021-11-10 14:45:01','2021-11-16 11:25:02'),
(338,'c# asp.net core 3.1 自动注入','','','https://www.cnblogs.com/Byboys/p/13744481.html',15,4,'2021-11-10 14:45:01','2021-11-15 14:32:49'),
(339,'.Net Core3.1下Autofac的使用','','','https://blog.csdn.net/sammy520/article/details/114417432',15,4,'2021-11-10 14:45:01','2021-11-15 14:31:12'),
(340,'获取windows 操作系统下的硬件或操作系统信息等','','','https://www.cnblogs.com/pilgrim/p/15115782.html',15,4,'2021-11-10 14:45:01','2021-11-15 14:32:56'),
(341,'pixabay','Stunning free images & royalty free stock','','https://pixabay.com/',18,4,'2021-11-10 14:45:01','2021-11-16 11:20:08'),
(342,'学会这几招,轻松让你的github脱颖而出','','','https://juejin.cn/post/6997070653010477087',15,4,'2021-11-10 14:45:01','2021-11-15 14:32:18'),
(343,'使用模板生成网页/Pdf/Word/Png/Html的简历','','','https://github.com/liangjingkanji/Resume-Template',14,4,'2021-11-10 14:45:01','2021-11-15 14:32:41'),
(344,'ASP.NET Core定时之Quartz.NET使用','Quartz.NET 是一个功能齐全的开源作业调度系统，可用于从最小的应用程序到大型企业系统。\n\nQuartz.NET是纯净的，它是一个.Net程序集，是非常流行的Java作业调度系统Quartz的C#实现。','','https://www.cnblogs.com/LaoPaoEr/p/15129899.html',15,4,'2021-11-10 14:45:01','2021-11-15 14:33:37'),
(345,'C# 实现发送QQ邮箱功能','','','https://www.cnblogs.com/2002-YiZhiYu/p/15118080.html',15,4,'2021-11-10 14:45:01','2021-11-15 14:32:04'),
(346,'.Net Core5.0中Autofac依赖注入整合多层，项目中可直接用','','','https://www.cnblogs.com/wei325/p/15121451.html#autoid-3-0-0',15,4,'2021-11-10 14:45:01','2021-11-15 14:32:12'),
(347,'Fantastic-admin','一款开箱即用的 Vue 中后台管理系统框架','','https://hooray.gitee.io/fantastic-admin/',19,4,'2021-11-10 14:45:01','2021-11-15 14:31:04'),
(348,'vue-element-admin','A magical vue admin','','https://panjiachen.gitee.io/vue-element-admin-site/zh/',19,4,'2021-11-10 14:45:01','2021-11-15 14:31:55'),
(349,'wallhaven','','','https://wallhaven.cc/',18,4,'2021-11-10 14:45:01','2021-11-16 11:19:39'),
(350,'基于vue3实现的vue3-seamless-scroll无缝滚动','','','https://juejin.cn/post/7001831268811800584',24,4,'2021-11-10 14:45:01','2021-11-15 14:30:03'),
(351,'在vite2和Vue3中配置Mockjs _','','','https://www.cnblogs.com/wdyyy/p/mockjs_vite2.html',15,4,'2021-11-10 14:45:01','2021-11-15 14:31:46'),
(352,'Vue 3.0 训练营','','','https://vue3.github.io/vue3-News/',3,4,'2021-11-10 14:45:01','2021-11-15 14:29:50'),
(354,'Quasar ','','','https://quasar.dev/start/pick-quasar-flavour',19,4,'2021-11-10 14:45:01','2021-11-15 14:29:57'),
(355,'jstips','','','https://www.jstips.co/zh_CN/javascript/',5,4,'2021-11-10 14:45:01','2021-11-15 14:29:33'),
(356,'Pinia','状态管理','','https://pinia.esm.dev/',3,4,'2021-11-10 14:45:01','2021-11-15 14:29:25'),
(357,'Vue-H5-Template','使用 Vue3.0+Typescript+Vant 搭建 h5 开发基础模板，并提供通用型的解决方案。','','https://docs.xwhx.top/',15,4,'2021-11-10 14:45:01','2021-11-15 14:29:20'),
(358,'片段生成器','','','https://snippet-generator.app/',27,4,'2021-11-10 14:45:01','2022-10-13 10:15:48'),
(359,'Vben Admin ','一个开箱即用的前端框架','','https://vvbin.cn/doc-next/',19,4,'2021-11-10 14:45:01','2021-11-15 14:29:02'),
(360,'Vue-Mastery学习笔记','','','https://www.yuque.com/nxtt7g/kompdt',15,4,'2021-11-10 14:45:01','2021-11-15 14:28:56'),
(361,'u.tools','新一代效率工具平台\n自由组合丰富插件，打造随手可取的终极神器','','https://u.tools/',27,4,'2021-11-10 14:45:01','2022-10-13 10:13:09'),
(362,'VARLET','面向Vue3的Material风格移动端组件库','','https://varlet.gitee.io/varlet-ui/#/zh-CN/home',19,4,'2021-11-10 14:45:01','2021-11-15 14:28:42'),
(363,'devhints.io','Rico''s cheatsheets','','https://devhints.io/',13,4,'2021-11-10 14:45:01','2021-11-16 11:18:39'),
(364,'TypeScript 4.0 使用手册','TypeScript语言用于大规模应用的JavaScript开发。 ✔️ TypeScript支持类型，是JavaScript的超集且可以编译成纯JavaScript代码。 ✔️ TypeScript兼容所有浏览器，所有宿主环境，所有操作系统。 ✔️ TypeScript是开源的。','','https://www.bookstack.cn/read/TypeScript-4.0-zh/README.md',13,4,'2021-11-10 14:45:01','2021-11-15 14:28:31'),
(365,'Apifox 使用文档','API 文档、API 调试、API Mock、API 自动化测试一体化协作平台，定位 Postman + Swagger + Mock + JMeter。通过一套系统、一份数据，解决多个系统之间的数据同步问题。只要定义好 API 文档，API 调试、API 数据 Mock、API 自动化测试就可以直接使用，无需再次定义；API 文档和 API 开发调试使用同一个工具，API 调试完成后即可保证和 API 文档定义完全一致。高效、及时、准确！','','https://www.apifox.cn/help/',14,4,'2021-11-10 14:45:01','2021-11-15 14:28:26'),
(366,'Snipaste','Snipaste 不只是截图，善用贴图功能将帮助你提升工作效率','','https://docs.snipaste.com/zh-cn/',14,4,'2021-11-10 14:45:01','2021-11-15 14:28:20'),
(367,'【TypeScript】- 一篇够用的TS总结','','','https://alexwjj.github.io/pages/cf42a74e3cc8f/',15,4,'2021-11-10 14:45:01','2021-11-15 14:28:12'),
(368,'柠檬大师的空间站','97程序员一枚，软件工程专业，现居北京，喜欢捣腾，专攻后端，用其他技术打辅助','https://d33wubrfki0l68.cloudfront.net/6657ba50e702d84afb32fe846bed54fba1a77add/827ae/logo.svg','https://leidl.top/',8,4,'2021-11-10 14:45:01','2021-11-15 14:21:59'),
(369,'Loader Gallery','customize and make your own unique loader!','','https://loading.io/spinner/',9,4,'2021-11-10 14:45:01','2021-11-15 14:28:06'),
(370,'程序员导航','','','https://cxy521.com/',11,4,'2021-11-10 14:45:01','2021-11-15 14:17:29'),
(371,'Live Demo','','','https://theoxiong.github.io/vue-search-panel/',24,4,'2021-11-10 14:45:01','2021-11-15 14:17:17'),
(372,'书栈网','','','https://www.bookstack.cn/',10,4,'2021-11-10 14:45:01','2021-11-15 14:16:44'),
(373,'axios','易用、简洁且高效的http库','','http://www.axios-js.com/zh-cn/',3,4,'2021-11-10 14:45:01','2021-11-15 14:16:33'),
(374,'jQuery','','','http://hemin.cn/jq/',19,4,'2021-11-10 14:45:01','2021-11-15 14:16:27'),
(375,'Linux命令大全(手册)','准确，丰富，稳定，在技术之路上为您护航！','','https://www.linuxcool.com/',15,4,'2021-11-10 14:45:01','2021-11-15 14:16:16'),
(376,'JavaScript中的这些骚操作，你都知道吗？','','','https://juejin.cn/post/7007306019307175966',15,4,'2021-11-10 14:45:01','2021-11-15 14:16:10'),
(377,'分享32个JavaScript工作中常用的代码片段','整理一下工作中常用的JavaScript小技巧分享给大家，希望能帮助到各位小伙伴们，在工作中提升开发效率。','','https://segmentfault.com/a/1190000040637925',15,4,'2021-11-10 14:45:01','2021-11-15 14:15:58'),
(378,'Vue3的7种和Vue2的12种组件通信','','','https://juejin.cn/post/6999687348120190983',15,4,'2021-11-10 14:45:01','2021-11-15 14:14:53'),
(379,'v-md-editor','v-md-editor 是基于 Vue 开发的 markdown 编辑器组件','','https://ckang1229.gitee.io/vue-markdown-editor/zh/#%E4%BB%8B%E7%BB%8D',20,4,'2021-11-10 14:45:01','2021-11-15 14:14:18'),
(384,'前端“技师”们强推的效率开发工具汇总','各位程序员“技师”提供的小技巧的汇总。将我们平常累计的一些开发技巧分享给大家，希望能对大家有所帮助','图片链接','https://juejin.cn/post/7021320464836329502#heading-3',15,4,'2021-11-10 14:45:01','2021-11-15 14:14:10'),
(385,'Windows 快捷操作大全','快捷键只介绍能让你成为开发大佬的，类似 Ctrl+C、Ctrl+V 这种大家熟知的，一概省略，咱们只来干货。','....','https://juejin.cn/post/7020574670097219621',15,4,'2021-11-10 14:45:01','2021-11-15 14:14:00'),
(386,'一文让你30分钟快速掌握Vue3','经过了漫长的迭代，Vue 3.0 终于在上 2020-09-18 发布了，带了翻天覆地的变化，使用了 Typescript 进行了大规模的重构，带来了 Composition API RFC 版本，类似 React Hook 一样的写 Vue，可以自定义自己的 hook ，让使用者更加的灵活，接下来总结一下 vue 3.0 带来的部分新特性。','图片链接','https://mp.weixin.qq.com/s/1orWGlOXT2Wn2pJLK6VAIg',15,4,'2021-11-10 14:45:01','2021-11-15 14:13:52'),
(387,'前端进阶之道','针对前端的知识难点进行细致入微的讲解，让你的进阶之路不再崎岖！','图片链接','https://yuchengkai.cn/',15,4,'2021-11-10 14:45:01','2021-11-15 14:13:46'),
(388,'Web 控制台终极指南','一旦掌握了控制台，它将帮助我们更有条理、更快地调试并了解应用程序中发生的一切。所以我会试着用例子总结你需要知道的所有内容','图片链接','https://segmentfault.com/a/1190000040705234',15,4,'2021-11-10 14:45:01','2021-11-15 14:13:36'),
(389,'JSRUN.NET','用代码说话,一惯的风格','图片链接','http://jsrun.net/t',13,4,'2021-11-10 14:45:01','2021-11-15 14:13:27'),
(390,'vue-manage-system','','图片链接','https://github.com/lin-xin/vue-manage-system',24,4,'2021-11-10 14:45:01','2021-11-15 14:13:09'),
(391,'微信Markdown','导航简述','图片链接','https://doocs.gitee.io/md/#/',20,4,'2021-11-10 14:45:01','2021-12-15 15:00:07'),
(392,'cssreference.io','免费的 CSS 视觉指南 通过示例学习：cssreference.io是一个免费的 CSS 视觉指南。它以最流行的属性为特色，并通过插图和动画示例对其进行了解释。','图片链接','https://cssreference.io/',9,4,'2021-11-10 14:45:01','2021-11-15 14:12:53'),
(393,'css-tricks.com','可以包含（在另一个特定 HTML 元素中的特定 HTML 元素）','图片链接','https://css-tricks.com/',9,4,'2021-11-10 14:45:01','2021-11-15 14:12:47'),
(400,'color-ui','简述','图片链接','https://www.color-ui.com/',19,4,'2021-11-10 14:45:01','2021-11-15 14:12:39'),
(402,'codemyui','简述','图片链接','https://codemyui.com/',9,4,'2021-11-10 14:45:01','2021-11-15 14:12:32'),
(403,'学习CSS布局','本站教授的是现在广泛使用于网站布局领域的CSS基础。  我们假设你已经掌握了CSS的选择器、属性和值。并且你可能已经对布局有一定了解，即使亲自去写的话还是会很苦恼。如果你想要从头开始学习HTML和CSS，那么你可以看下这篇教程。不然的话，让我们看看我们是否可以让你在下一个项目少一些烦恼。','图片链接','https://zh.learnlayout.com/',9,4,'2021-11-10 14:45:01','2021-11-15 14:11:34'),
(410,'CSShake','一些 CSS 类 移动你的 DOM！','图片链接','https://elrumordelaluz.github.io/csshake/',9,4,'2021-11-10 14:45:01','2021-11-15 14:11:28'),
(411,'Nuxt3','使用 Vue 3 构建您的下一个应用程序，体验混合渲染、强大的数据获取和新功能。Nuxt 3 是一个开源框架，使 Web 开发变得简单而强大。','图片链接','https://v3.nuxtjs.org/',3,4,'2021-11-10 14:45:01','2021-11-15 14:11:21'),
(412,'Day.js中文网','Day.js是一个极简的JavaScript库，可以为现代浏览器解析、验证、操作和显示日期和时间。','图片链接','https://dayjs.fenxianglu.cn/',5,4,'2021-11-10 14:45:01','2021-11-15 10:17:04'),
(413,'fenxianglu','','图片链接','https://www.fenxianglu.cn/',13,4,'2021-11-10 14:45:01','2021-11-15 10:16:57'),
(414,'Vuex','Vuex 是一个专为 Vue.js 应用程序开发的状态管理模式 + 库。它采用集中式存储管理应用的所有组件的状态，并以相应的规则保证状态以一种可预测的方式发生变化。','图片链接','https://next.vuex.vuejs.org/zh/',3,4,'2021-11-10 14:45:01','2021-11-15 10:16:37'),
(415,'Vue I18n','Vue I18n 是 Vue.js 的国际化插件','图片链接','https://kazupon.github.io/vue-i18n/zh/',24,4,'2021-11-10 14:45:01','2021-11-15 10:16:30'),
(416,'Material Design 框架','Vuetify 是一个纯手工精心打造的 Material 样式的 Vue UI 组件库。 不需要任何设计技能 — 创建叹为观止的应用程序所需的一切都触手可及。','图片链接','https://vuetifyjs.com/zh-Hans/',19,4,'2021-11-10 14:45:01','2021-11-15 10:16:23'),
(417,'Typora+picGo+Gitee搭建图床','Vuetify 是一个纯手工精心打造的 Material 样式的 Vue UI 组件库。 不需要任何设计技能 — 创建叹为观止的应用程序所需的一切都触手可及。','图片链接','https://juejin.cn/post/7011762633691168805',15,4,'2021-11-10 14:45:01','2021-11-15 10:16:14'),
(418,'Axios的封装思想及实践（TS版本）','','图片链接','https://juejin.cn/post/7023006049732919309',15,4,'2021-11-10 14:45:01','2021-11-15 10:16:09'),
(420,'Fect UI -Vue','@fect-ui/vue是根据 Geist-ui/vue作为设计依赖对 vue2 版本进行升级的一个Vue3UI 库。项目基于typescript,拥有更完备的类型提示和对编译器的友好支持, 相较 vue2 版本组件库进行了交互的优化。','图片链接','https://vue.miaya.art/Introduce',19,4,'2021-11-10 14:45:01','2021-11-15 10:15:53'),
(421,'CSS Layout','使用 CSS 制作的流行布局和图案','img','https://csslayout.io/',9,4,'2021-11-10 14:45:01','2021-11-15 10:13:01'),
(422,'vue3-progress','进度条','图片链接','https://vue3-progress-demo.netlify.app/',24,4,'2021-11-10 14:45:01','2021-11-11 16:31:09'),
(423,'webPack转vite所遇到的问题','','img','https://blog.csdn.net/WH_Crx/article/details/118106097',15,4,'2021-11-10 14:45:01','2021-11-15 10:12:49'),
(424,'mind-map','vue 图文','图片链接','https://github.com/jCodeLife/mind-map/',24,4,'2021-11-10 14:45:01','2021-11-15 10:12:41'),
(425,'图像优化','在文件尺寸和质量之间选择完美平衡，并且可获取完整在线预览,您的图像从不会离开您的浏览器。','图片链接','https://zh.recompressor.com/',27,4,'2021-11-10 14:45:01','2022-10-13 10:14:49'),
(426,'Vue Trend','Vue.js Live Demo 的简单、优雅的火花线','img','https://cinwell.com/vue-trend/',24,4,'2021-11-10 14:45:01','2021-11-15 10:12:26'),
(427,'极客猿导航','导航','图片链接','https://nav.geekape.net/',11,4,'2021-11-10 14:45:01','2021-11-11 16:29:35'),
(428,'vue-fullscreen','一个用于将任意页面元素进行全屏切换的vue组件，基于 screenfull.js','图片链接','https://mirari.cc/2017/08/14/%E5%85%A8%E5%B1%8F%E5%88%87%E6%8D%A2%E7%BB%84%E4%BB%B6vue-fullscreen/',24,4,'2021-11-10 14:45:01','2021-11-15 10:12:14'),
(429,'CodePen','CodePen 是一个面向前端设计人员和开发人员的社交开发环境。构建和部署网站，展示您的工作，构建测试用例以学习和调试，并寻找灵感。','图片链接','https://codepen.io/',9,4,'2021-11-10 14:45:01','2021-11-15 10:12:06'),
(430,'GKA','简单的、高效的帧动画生成工具.  使用简单(仅需一行命令) 内置多种图片优化 多类生成模板，支持定制','图片链接','https://gka.js.org/#/',24,4,'2021-11-10 14:45:01','2021-11-15 10:11:54'),
(431,'Sonar','\"Sonar一个Web系统，展现了静态代码扫描的结果，结果是可以自定义的 ,支持多种语言的原理是它的扩展性 \"','图片链接','http://www.sonar.org.cn/',14,4,'2021-11-10 14:45:01','2021-11-15 10:11:18'),
(432,'highcharts','数据可视化','图片链接','http://www.sonar.org.cn/',5,4,'2021-11-10 14:45:01','2021-11-11 16:29:50'),
(433,'chartjs','图表','图片链接','https://www.chartjs.org/',5,4,'2021-11-10 14:45:01','2021-11-11 16:30:31'),
(434,'Apache ECharts','一个基于 JavaScript 的开源可视化图表库','img','https://echarts.apache.org/zh/index.html',5,4,'2021-11-10 14:45:01','2021-11-15 10:10:53'),
(435,'JavaScript Promise迷你书','本书的目的是以目前还在制定中的ECMAScript 6 Promises规范为中心，着重向各位读者介绍JavaScript中对Promise相关技术的支持情况。','img','http://liubin.org/promises-book/#introduction',13,4,'2021-11-10 14:45:01','2021-11-15 10:10:39'),
(436,'EJS','嵌入式 JavaScript 模板。','img','https://ejs.co/#promo',5,4,'2021-11-10 14:45:01','2021-11-15 10:10:14'),
(437,'Redux 中文官网','JS 应用的状态容器，提供可预测的状态管理','图片链接','https://cn.redux.js.org/',5,4,'2021-11-10 14:45:01','2021-11-11 16:30:56'),
(438,'LOCALFORAGE','localForage 是一个 JavaScript 库，通过简单类似 localStorage API 的异步存储来改进你的 Web 应用程序的离线体验。它能存储多种类型的数据，而不仅仅是字符串。','图片链接','https://localforage.docschina.org/#api-getitem',5,4,'2021-11-10 14:45:01','2021-11-11 16:37:43'),
(439,'v-charts','在使用 echarts 生成图表时，经常需要做繁琐的数据类型转化、修改复杂的配置项，v-charts 的出现正是为了解决这个痛点。基于 Vue2.0 和 echarts 封装的 v-charts 图表组件，只需要统一提供一种对前后端都友好的数据格式设置简单的配置项，便可轻松生成常见的图表。','图片链接','https://v-charts.js.org/#/',24,4,'2021-11-10 14:45:01','2021-11-11 16:30:48'),
(440,'pexels','免费图库','图片链接','https://www.pexels.com/zh-cn/',18,4,'2021-11-10 14:45:01','2021-11-11 16:30:40'),
(441,'HELLOGITHUB','分享 GitHub 上 有趣、入门级的开源项目','图片链接','https://hellogithub.com/',13,4,'2021-11-10 14:45:01','2021-11-15 10:09:57'),
(442,'GitHub Corners','Phew, GitHub is over ten years old now... and is unquestionably synonomous with open source. After 10 years, those GitHub ribbons are more than overdue for a cleaner, more modern alternative. This is my take.  By using SVG, these ','图片链接','https://tholman.com/github-corners/',24,4,'2021-11-10 14:45:01','2021-11-11 16:29:15'),
(444,'Vuetable-2','数据表','图片链接','https://www.vuetable.com/#current-version',24,4,'2021-11-10 14:45:01','2021-11-11 16:28:57'),
(445,'v-viewer','vue的图片查看器组件，支持旋转、缩放、缩放等，基于viewer.js','图片链接','https://mirari.cc/v-viewer/',5,4,'2021-11-10 14:45:01','2021-11-11 16:27:33'),
(446,'Vue 3 选框','为你的 Vue 3 应用程序制作的一个简单的动态选取框组件','图片链接','https://vue3-marquee.vercel.app/',24,4,'2021-11-10 14:45:01','2021-11-11 16:25:35'),
(447,' GoGoCode','代码转换从未如此简单 全网最简单易上手，可读性最强的 AST 处理工具！','图片链接','https://gogocode.io/zh',14,4,'2021-11-10 14:45:01','2021-11-11 16:23:36'),
(448,'hammerjs','hammerjs','图片链接','http://hammerjs.github.io/',5,4,'2021-11-10 14:45:01','2021-11-10 14:59:23'),
(462,'Ovilia','','','http://zhangwenli.com/',8,4,'2021-11-15 14:37:19','2021-11-15 14:37:19'),
(463,'carbon','创建和共享源代码的精美图像。 开始在文本区域键入或拖放文件以开始使用。','','https://carbon.now.sh/',14,4,'2021-11-15 14:39:13','2021-11-15 14:39:13'),
(464,'比格张','','','https://bigezhang.com/',11,4,'2021-11-15 14:39:54','2021-11-15 14:39:54'),
(471,'accordionslider','css','','https://accordionslider.com/',9,4,'2021-12-02 15:54:30','2021-12-02 15:54:30'),
(472,'vuex-module-decorators','Typescript/ES7 装饰器使 Vuex 模块变得轻而易举','','https://championswimmer.in/vuex-module-decorators/',3,4,'2021-12-02 15:57:40','2021-12-02 15:57:40'),
(473,'tool.lu','在线工具','','https://tool.lu/',27,4,'2021-12-02 16:09:02','2022-10-13 10:12:18'),
(480,'一条咸鱼与狗的博客','一条咸鱼与狗的博客','','https://purefish.cn/',8,1,'2021-12-03 15:19:28','2021-12-03 15:19:28'),
(481,'图片转码','在线图片转码','','http://www.jsons.cn/img2base64/',18,1,'2021-12-03 15:20:18','2021-12-03 15:20:18'),
(482,'防网易云','','','https://music-player.immortalboy.cn/',8,4,'2021-12-06 11:19:40','2021-12-06 11:19:40'),
(483,'讨厌的CSS','心脏强者和心灵弱者的动画。','','https://tholman.com/obnoxious/',9,4,'2021-12-06 11:22:36','2021-12-06 11:22:36'),
(484,'VueUse','Collection of essential Vue Composition Utilities','','https://vueuse.org/',3,4,'2021-12-06 11:23:45','2021-12-06 11:23:45'),
(485,'Vue.js 的表单验证','熟悉且易于设置的声明式验证 灵活 同步、异步、字段级或表单级验证  使用直观的 API 和较小的占用空间更快地构建更快的表单','','https://vee-validate.logaretm.com/v4/',24,4,'2021-12-06 11:30:47','2021-12-06 11:30:47'),
(486,'v-slot插槽','第十三篇：你会用v-slot插槽？你倒是用啊','','https://juejin.cn/post/7023188569162252295',15,4,'2021-12-06 13:35:36','2021-12-06 13:35:36'),
(487,'Jest','Jest 是一个令人愉快的 JavaScript 测试框架，专注于 简洁明快。','','https://www.jestjs.cn/',3,4,'2021-12-06 13:49:50','2021-12-06 13:49:50'),
(488,'Qui Max','Neumorphic design system for Web','','https://qvant-lab.github.io/qui-max/',24,4,'2021-12-06 13:57:02','2021-12-06 13:57:02'),
(489,'Zepto','Zepto 是一个轻量级的、针对现代高级浏览器的 JavaScript 工具库，它兼容 jQuery 的 API 。 如果你会用 jQuery，那么你就已经会用 Zepto 了。','','https://zeptojs.bootcss.com/',5,4,'2021-12-06 14:02:01','2021-12-06 14:02:01'),
(490,'ANTD PRO VUE','开箱即用的中台前端/设计解决方案','','https://pro.antdv.com/',19,4,'2021-12-06 14:05:18','2021-12-06 14:05:18'),
(491,'getwaves','Make some waves!','','https://getwaves.io/',9,4,'2021-12-06 14:06:48','2021-12-06 14:06:48'),
(492,'Normalize.css','一种现代的、支持 HTML5 的 CSS 重置替代方案','','http://necolas.github.io/normalize.css/',9,4,'2021-12-06 14:07:53','2021-12-06 14:07:53'),
(493,'vue-awesome-swiper','@vuejs 的 Swiper 组件','','https://github.surmon.me/vue-awesome-swiper/',24,4,'2021-12-06 14:13:23','2021-12-06 14:13:23'),
(494,'lru cache','A cache object that deletes the least-recently-used items.','','https://github.com/isaacs/node-lru-cache#readme',3,4,'2021-12-06 14:18:27','2021-12-06 14:18:27'),
(495,'vite-plugin-vue-docs','vite 插件 - 自动生成 vue 组件文档网站。','','https://meetqy.github.io/vite-plugin-vue-docs/#/docs',24,4,'2021-12-06 14:19:40','2021-12-06 14:19:40'),
(496,'制作缩略图','让我们制作缩略图','','http://makethumbnails.com/#options',18,4,'2021-12-06 14:25:34','2021-12-06 14:25:34'),
(497,'c#编程之路','','','https://www.cjavapy.com/75/',13,4,'2021-12-06 14:44:08','2021-12-06 14:44:08'),
(498,'SweetAlert','SweetAlert 使弹出消息变得简单而漂亮。','','https://sweetalert.js.org/',24,4,'2021-12-06 14:49:43','2021-12-06 14:49:43'),
(499,'Docusaurus','Docusaurus 能够帮助你建立并发布 美观的文档网站。','','https://www.docusaurus.cn/docs',13,4,'2021-12-06 14:54:36','2021-12-06 14:54:36'),
(500,'avaScript for','describe','','https://www.javascript.fun/',11,4,'2021-12-06 15:07:29','2021-12-06 15:07:29'),
(501,'bootswatch','An ode to Metro','','https://bootswatch.com/cosmo/',9,4,'2021-12-06 15:10:26','2022-10-18 09:54:14'),
(502,'icons8','describe','','https://icons8.com/',18,4,'2021-12-06 15:13:29','2021-12-06 15:13:29'),
(503,'CSS Grid Generator','CSS Grid Generator','','https://cssgrid-generator.netlify.app/',9,4,'2021-12-06 15:17:41','2021-12-06 15:17:41'),
(504,'listary','Are Clumsy File Management Systems Slowing Down Your Workflow?','','https://www.listary.com/',14,4,'2021-12-06 15:19:29','2021-12-06 15:19:29'),
(505,'Vue CLI','ue.js 开发的标准工具','','https://cli.vuejs.org/zh/',3,4,'2021-12-06 15:34:42','2021-12-06 15:34:42'),
(506,'c#扩展方法的使用','c#扩展方法的使用','','https://blog.csdn.net/liangmengbk/article/details/112393864',15,4,'2021-12-09 15:03:51','2021-12-09 15:03:51'),
(507,'fastgithub','fastgithub是使用dotnet开发的一款github加速器','','https://www.cnblogs.com/kewei/p/15533079.html',14,4,'2021-12-09 15:07:45','2021-12-09 15:07:45'),
(508,'Simple CSS','简单的 CSS媒体查询生成器 为数百种设备生成 CSS 媒体查询，包括众多 ipad 和 iphone 型号、三星、LG 的安卓设备等等。有时您必须针对特定设备，这只是一个令人不快的事实。','','https://simplecss.eu/',9,4,'2021-12-15 15:14:02','2021-12-15 15:14:02'),
(509,'BootCDN','BootCDN 稳定、快速、免费的前端开源项目 CDN 加速服务','','https://www.bootcdn.cn/',30,4,'2021-12-15 15:44:57','2022-10-18 09:53:13'),
(510,'staticfile CDN','CDN加速服务','','https://www.staticfile.org/',3,4,'2021-12-15 15:45:41','2022-10-17 15:58:01'),
(511,'convue','convue 是一个基于 vite 和 vue3 开发的一个 vite 的插件，让你拥有一套快速开发项目的体验，类似于 nuxt 和 umi.js。','','https://ziping-li.github.io/convue/zh/index.html',3,4,'2021-12-15 15:54:36','2021-12-15 15:54:36'),
(512,'G6 图可视化引擎','G6 是一个简单、易用、完备的图可视化引擎，它在高定制能力的基础上，提供了一系列设计优雅、便于使用的图可视化解决方案。能帮助开发者搭建属于自己的图可视化、图分析、或图编辑器应用。','','https://antv-g6.gitee.io/zh',19,4,'2021-12-16 14:24:41','2021-12-16 14:24:41'),
(513,'HTML DOM','with vanilla JavaScript','','https://htmldom.dev/',15,4,'2021-12-16 14:27:03','2021-12-16 14:27:03'),
(514,'rolan','即刻提升你的工作效率','','https://getrolan.com/',14,4,'2021-12-16 14:37:05','2021-12-16 14:37:05'),
(515,'极简插件','describe','','https://chrome.zzzmh.cn/#/index',14,4,'2021-12-16 14:46:51','2021-12-16 14:46:51'),
(516,'百页窗','一款专业的文件管理工具','','https://shutters.160.com/',14,4,'2021-12-16 15:25:10','2021-12-16 15:25:10'),
(517,'F2 移动端可视化引擎','F2 是一个专注于移动端，面向常规统计图表，开箱即用的可视化引擎，完美支持 H5 环境同时兼容多种环境（Node, 小程序），完备的图形语法理论，满足你的各种可视化需求，专业的移动设计指引为你带来最佳的移动端图表体验。','','https://antv-f2.gitee.io/zh',19,4,'2021-12-16 15:26:21','2021-12-16 15:26:21'),
(518,'Chrome插件扩展下载网','Chrome插件扩展下载网','','https://www.extfans.com/',14,4,'2021-12-16 15:29:24','2021-12-16 15:29:24'),
(520,'box-shadow 例子','所有这些 box-shadow 都是使用复制的  (点击这里尝试免费演示）。 使用 CSS Scan，您可以轻松检查或复制任何网站的 CSS。','','https://getcssscan.com/css-box-shadow-examples',9,4,'2021-12-24 13:55:12','2021-12-24 13:55:12'),
(521,'arco.design','智能设计体系 连接轻盈体验 # 全面开源的企业级产品设计系统','','https://arco.design/',19,4,'2021-12-27 09:29:51','2021-12-27 09:29:51'),
(523,'Vue 组合式 API','describe','','https://vue3js.cn/vue-composition-api/',3,4,'2022-03-11 10:03:59','2022-03-11 10:03:59'),
(524,'Chart.js','describe','','https://chartjs.bootcss.com/docs/getting-started/',24,4,'2022-03-11 10:04:58','2022-03-11 10:04:58'),
(525,'Cookie的使用（js-cookie插件）','describe','','https://www.cnblogs.com/star-meteor/p/12881296.html',15,4,'2022-03-11 10:13:20','2022-03-11 10:13:20'),
(526,'PicGo','图片上传、管理新体验','','https://picgo.github.io/PicGo-Doc/',14,4,'2022-03-11 10:37:23','2022-03-11 10:37:23'),
(527,'手册网','','','https://www.shouce.ren/',13,4,'2022-03-11 10:38:56','2022-03-11 10:38:56'),
(528,'VS插件Supercharger的安装','describe','','https://www.cnblogs.com/arxive/p/13513057.html',15,4,'2022-03-11 10:39:50','2022-03-11 10:39:50'),
(529,'Axios HTTP','基于 Axios 拓展的 HTTP 模块','','https://zhengxs2018.github.io/axios-http/',3,4,'2022-03-11 11:00:09','2022-03-11 11:00:09'),
(530,'图像编辑','在线图像编辑','','https://pixlr.com/cn/x/#search',14,4,'2022-03-11 11:01:36','2022-03-11 11:01:36'),
(531,'CSI.JS','CSI.JS是一个特别的前端日志系统，帮你快速重建犯罪现场。','','https://github.com/tnfe/csijs',24,4,'2022-03-11 11:13:54','2022-03-11 11:13:54'),
(532,'SunnyUI','winfrom组件文档','','https://gitee.com/yhuse/SunnyUI/wikis/pages?sort_id=3025093&doc_id=1022550',13,4,'2022-03-11 11:20:21','2022-03-11 11:20:21'),
(533,'LOCALFORAGE','改进的离线存储','','http://localforage.docschina.org/',5,4,'2022-03-23 15:16:03','2022-03-23 15:16:03'),
(534,'vite-plugin-vue-layouts','插件','','https://github.com/JohnCampionJr/vite-plugin-vue-layouts',26,4,'2022-03-23 15:20:51','2022-03-23 15:20:51'),
(535,'vite-plugin-vue-type-imports','使您能够导入类型并在您的defineProps和defineEmits','','https://github.com/wheatjs/vite-plugin-vue-type-imports',26,4,'2022-03-23 15:22:44','2022-03-23 15:22:44'),
(536,'迅速了解ES6~ES12的全部特性','迅速了解ES6~ES12的全部特性','','https://juejin.cn/post/7068935394191998990',15,4,'2022-03-23 15:23:24','2022-03-23 15:23:24'),
(537,'ES6-ES12总结','describe','','https://juejin.cn/post/7012519052841074696',15,4,'2022-03-23 15:24:03','2022-03-23 15:24:03'),
(538,'一文读懂 TypeScript 泛型及应用','一文读懂 TypeScript 泛型及应用','','https://juejin.cn/post/6844904184894980104',15,4,'2022-03-23 15:24:44','2022-03-23 15:24:44'),
(539,'Vue 3 Babel JSX 插件','以 JSX 的方式来编写 Vue 代码','','https://github.com/vuejs/babel-plugin-jsx/blob/dev/packages/babel-plugin-jsx/README-zh_CN.md',26,4,'2022-03-23 15:25:59','2022-03-23 15:25:59'),
(540,'vue插件库','快速查找您想要的内容，多动手，您会发现问题如此简单！','','https://www.vue365.cn/',3,4,'2022-03-23 15:26:49','2022-03-23 15:26:49'),
(541,'.NET Core资料精选','describe','','.https://www.cnblogs.com/heyuquan/p/dotnet-architecture-learning-resource.html',15,4,'2022-03-23 17:01:46','2022-03-23 17:01:46'),
(542,'总结了38个ES6-ES12的开发技巧','describe','','https://juejin.cn/post/6995334897065787422',15,4,'2022-03-23 17:02:14','2022-03-23 17:02:14'),
(543,'软件先锋','describe','','https://soft.macxf.com/',14,4,'2022-03-23 17:02:50','2022-03-23 17:02:50'),
(544,'加速器','describe','','http://101.34.95.10:8081/',14,4,'2022-03-23 17:05:19','2022-03-23 17:05:19'),
(545,'JavaScript工具函数','describe','','https://juejin.cn/post/6844904181761835016',15,4,'2022-03-23 17:05:51','2022-03-23 17:05:51'),
(546,'vite-plugin-pages','使用Vite的 Vue 3 / React 应用程序的基于文件系统的路由','','https://github.com/hannoeru/vite-plugin-pages',26,4,'2022-03-24 10:49:04','2022-03-24 10:49:04'),
(547,'ProComponents','让中后台开发更简单','','https://procomponents.ant.design/',24,4,'2022-03-24 10:51:04','2022-03-24 10:51:04'),
(548,'netcore','describe','','https://www.cnblogs.com/Can-daydayup/p/15046838.html',15,4,'2022-03-24 10:51:51','2022-03-24 10:51:51'),
(549,'Shell脚本编程30分钟入门','describe','','https://github.com/qinjx/30min_guides/blob/master/shell.md',15,4,'2022-03-24 10:52:25','2022-03-24 10:52:25'),
(550,'C#/.NET/.NET Core推荐学习书籍','describe','','https://www.cnblogs.com/Can-daydayup/p/14386782.html',15,4,'2022-03-24 10:54:41','2022-03-24 10:54:41'),
(551,'unplugin-icons','describe','','https://github.com/antfu/unplugin-icons',26,4,'2022-03-24 10:59:15','2022-03-24 10:59:15'),
(552,'github 的使用技巧 ','describe','','https://juejin.cn/post/7069790758022152206',15,4,'2022-03-24 10:59:40','2022-03-24 10:59:40'),
(553,'WLib','WLib是一组对C#.NET和ArcGIS Engine开发常用代码进行封装的基础库和控件库；','','https://github.com/Windr07/WLib',4,4,'2022-03-24 11:02:43','2022-03-24 11:02:43'),
(554,'Chrome 开发者工具','describe','','https://leeon.gitbooks.io/devtools/content/',15,4,'2022-03-24 11:03:39','2022-03-24 11:03:39'),
(555,'接口大全','describe','','https://www.free-api.com/',11,4,'2022-03-24 11:05:56','2022-03-24 11:05:56'),
(556,'理解ASP.NET Core','describe','','https://www.cnblogs.com/xiaoxiaotank/p/15185288.html',15,4,'2022-04-02 10:39:59','2022-04-02 10:39:59'),
(557,'.NET Core中的鉴权授权正确方式(.NET5)','describe','','https://www.cnblogs.com/wei325/p/15575141.html',15,4,'2022-04-02 10:41:23','2022-04-02 10:41:23'),
(558,'iconpark图标','丰富多彩的资源库免费使用','','https://iconpark.oceanengine.com/home',27,4,'2022-04-02 10:46:29','2022-10-17 15:47:17'),
(559,'carbon','代码片段,创建和共享源代码的精美图像。 开始键入或将文件拖放到文本区域中以开始使用。','','https://carbon.now.sh/',27,4,'2022-04-02 10:48:24','2022-10-13 10:10:45'),
(563,'HandyOrg','WPF控件库','','https://handyorg.github.io/',28,4,'2022-10-10 15:55:30','2022-10-18 08:46:08'),
(564,'v-contextmenu','右键菜单','','https://github.com/heynext/v-contextmenu',24,4,'2022-10-11 08:34:48','2022-10-11 08:34:48'),
(565,'Qing''s Blog','describe','','https://www.cnblogs.com/zhaoqingqing/category/527577.html',8,4,'2022-10-11 08:47:05','2022-10-11 08:47:05'),
(566,'Milkdown','插件驱动的所见即所得的Markdown编辑器框架','','https://milkdown.dev/zh-hans/',20,4,'2022-10-11 13:57:11','2022-10-11 13:57:11'),
(567,'css零代码工具箱','本站后续会推出各种前端实用的好工具，杜绝造轮子，每款工具都经过精心打磨，帮助所有程序员提高前端开发效率！','','http://www.lingdaima.com/',27,4,'2022-10-17 10:01:26','2022-10-17 15:40:30'),
(568,'丹枫无迹的博客','码农升级','','https://www.cnblogs.com/gl1573/',8,4,'2022-11-01 14:18:20','2022-11-01 14:18:31'),
(569,'码友网','我们分享C#&.NET/.NET Core及周边文章、问答、教程、资讯等。\n如果你和我们一样热爱.NET程序开发，欢迎加入我们。\n专注.NET，我们是认真的','','https://codedefault.com/',10,4,'2022-11-01 14:19:49','2022-11-01 14:19:49'),
(570,'RestSharp使用教程','RestSharp是一个.NET平台下REST和HTTP API的开源客户端库，支持的平台包括','','https://blog.csdn.net/huyu107/article/details/104502130',15,4,'2022-11-02 16:29:19','2022-11-02 16:29:19'),
(571,'发烧友软件','软件下载','','https://fsylr.com/pc/page/22',14,4,'2022-11-02 16:31:04','2022-11-02 16:31:04'),
(572,'c#使用mysql (MySqlCommand、MySqlDataAdapter、BeginTransaction)','c#使用mysql (MySqlCommand、MySqlDataAdapter、BeginTransaction)','','https://www.cnblogs.com/hepeng-web/p/14654430.html',15,4,'2022-11-02 16:32:58','2022-11-02 16:32:58'),
(573,'C#中的CSV文件读写','C#中的CSV文件读写','','https://www.cnblogs.com/timefiles/p/CsvReadWrite.html',15,4,'2022-11-02 16:33:33','2022-11-02 16:33:33'),
(574,'formkit','vue3组件库','','https://formkit.com/',3,4,'2022-11-02 16:36:06','2022-11-02 16:36:06'),
(575,'gitclone','GitHub缓存加速网站，为开发者服务（使用git2.0+）','','https://gitclone.com/',14,4,'2022-11-02 16:36:50','2022-11-02 16:36:50'),
(576,'Office交流网','','','http://www.office-cn.net/index.html',10,1,'2022-11-07 10:21:25','2022-11-07 10:21:25'),
(577,'USB-Devices','USB-Devices','','https://www.codeproject.com/Articles/60579/A-USB-Library-to-Detect-USB-Devices',14,1,'2022-11-07 13:43:08','2022-11-07 13:43:08'),
(578,'2022年了，我才开始学 typescript ，晚吗？（7.5k字总结）','2022年了，我才开始学 typescript ，晚吗？（7.5k字总结）','','https://juejin.cn/post/7124117404187099172#heading-30',15,4,'2022-11-07 15:34:21','2022-11-07 15:34:21'),
(579,'前端Vuer，请收下这份《Vue3中使用JSX简明语法》','前端Vuer，请收下这份《Vue3中使用JSX简明语法》','','https://juejin.cn/post/7114063575122984973',15,4,'2022-11-07 15:34:52','2022-11-07 15:34:52'),
(580,'vite-plugin-ssr','Like Next.js/Nuxt but as do-one-thing-do-it-well Vite plugin','','https://vite-plugin-ssr.com/',26,4,'2022-11-07 15:35:47','2022-11-07 15:35:47'),
(582,'VueHooks Plus','基础和高级的 hook， 高性能逻辑的抽象封装，满足大量场景','','https://nelsonyong.gitee.io/docs/hooks/',27,1,'2022-11-08 10:22:13','2022-11-08 10:23:30'),
(583,'web-norm','每当接手一个新项目（如果项目中没有配置 eslint husky commitlint）等这些规范的话，就需要自己手动配置一遍，配置多了后我只能来句窝草','','https://github.com/lyh0371/web-norm',3,4,'2022-11-08 18:08:22','2022-11-08 18:08:22'),
(584,'bourbon','scss常用代码块','','https://www.bourbon.io/docs/latest/#padding',9,4,'2022-11-08 18:10:09','2022-11-08 18:10:09'),
(585,'UnoCSS','原子化css','','https://uno.antfu.me/',9,4,'2022-11-09 10:01:00','2022-11-09 10:01:00'),
(586,'ahooks','ahooks 的 vue 实现。\n\n许多 hooks 是从ahooks移植过来的，但是不完全一致。','','https://dewfall123.github.io/ahooks-vue/zh/',3,4,'2022-11-09 14:29:13','2022-11-09 14:29:13'),
(587,'爱给网','图片素材','','https://www.aigei.com/',18,4,'2022-11-09 14:40:56','2022-11-09 14:40:56'),
(588,' css手册','api参考文档','','https://www.apiref.com/css-zh/index.htm',9,4,'2022-11-09 14:47:43','2022-11-09 14:47:43'),
(589,'cdnjs','CDNJS 是一项免费的开源 CDN 服务，受到超过12.5% 的网站的信任，为 每月超过 2000 亿个请求，由 Cloudflare 提供支持','','https://cdnjs.com/',30,4,'2022-11-09 14:48:50','2022-11-09 14:48:50'),
(590,'gitea','一款极易搭建的自助 Git 服务','','https://try.gitea.io/',14,4,'2022-11-09 14:50:00','2022-11-09 14:50:00'),
(591,'vitest','Vitest\n由 Vite 提供支持的极速单元测试框架\n一个 Vite 原生的单元测试框架。非常的快！','','https://cn.vitest.dev/',3,4,'2022-11-09 14:51:10','2022-11-09 14:51:10'),
(592,'zzhack ','嗨！欢迎来到我的应用 zzhack ，这是一个兴趣使然的项目，zzhack 被设计为一个注重信息展示的应用，它是序列化和沉淀我思想的地方。','','https://www.zzhack.fun/about',8,4,'2022-11-09 15:10:56','2022-11-09 15:10:56'),
(593,'milkdown-vue','基于[milkdown](https://github.com/Saul-Mirone/milkdown)的封装，整合了所有常用插件的`Vue3`组件&#x20;\n','','https://cyyjs.github.io/milkdown-vue/',3,4,'2022-11-09 15:22:31','2022-11-09 15:22:31'),
(594,'vexip UI','Vexip UI 提供了一系类开箱即用的组件。\n组件库使用 组合式 Api 编写，并尽可能采用 Vue 传统的方式设计和编写组件，全量的 TypeScript（不是 AnyScript）。','','https://www.vexipui.com/zh-CN',24,4,'2022-11-09 15:24:20','2022-11-09 15:24:20'),
(595,'网道','网道（WangDoc.com）是一个文档网站，提供互联网开发文档，正在建设中。','','https://wangdoc.com/',13,4,'2022-11-09 15:25:25','2022-11-09 15:25:25'),
(596,'SQL之父','快速生成 SQL 和模拟数据，大幅提高开发测试效率！','','https://www.sqlfather.com/',16,4,'2022-11-09 15:49:47','2022-11-09 15:49:47'),
(597,'icia','类似 jQuery 的 dom 操作库。','','https://licia.liriliri.io/docs_cn.html',5,4,'2022-11-09 16:18:35','2022-11-09 16:18:35'),
(598,' ESLint 配置预设','https://github.com/antfu/eslint-config','','https://github.com/antfu/eslint-config',3,4,'2022-11-09 17:10:02','2022-11-09 17:10:02'),
(599,'pinia插件持久化','','','https://seb-l.github.io/pinia-plugin-persist/',3,4,'2022-11-09 17:11:45','2022-11-09 17:11:45'),
(600,'HappyBoot Tiger','一个中后端框架Vite2+Vue3+HappyKit+NaiveUI','','https://doc.happykit.org/',24,4,'2022-11-09 17:13:19','2022-11-09 17:13:19'),
(601,'轮子之王','将平常开发中非常常用的功能做成轮子，减少开发时间，让开发者拥有更多的时间能够摸鱼','','http://it-learning-diary.gitee.io/it-wheels-king-inline-doc/#/README',3,4,'2022-11-09 17:14:30','2022-11-09 17:14:30'),
(602,'alova','MVVM 库的请求场景管理库，它是对请求库的一种武装，而非替代品','','https://github.com/JOU-amjs/alova/blob/main/README-zh.md',3,4,'2022-11-09 17:16:20','2022-11-09 17:16:20'),
(603,'quark-design','Quark 是一款基于 Web Components 的跨框架 UI 组件库。 它可以同时在任意框架或无框架中使用。','','https://quark-design.hellobike.com/#/',19,4,'2022-11-16 08:58:03','2022-11-16 08:58:03'),
(604,'icones.js','vue3常用图标库','','https://icones.js.org/',27,4,'2022-11-18 09:18:25','2022-11-18 09:18:25'),
(605,'Quick Reference','为开发人员分享快速参考备忘清单【速查表】 (主要是方便自己)，在看到 Reference 快速参考备忘单','','http://ref.ecdata.cn/',27,4,'2022-11-18 09:19:34','2022-11-18 09:19:34'),
(606,'tdesign','','','https://tdesign.tencent.com/',24,4,'2022-11-22 15:20:58','2022-11-22 15:20:58'),
(607,'atool99','在线工具','','https://www.atool99.com/',14,4,'2022-11-24 17:04:28','2022-11-24 17:04:28');
/*!40000 ALTER TABLE `sn_navigation` ENABLE KEYS */;

-- 
-- Definition of sn_one
-- 

DROP TABLE IF EXISTS `sn_one`;
CREATE TABLE IF NOT EXISTS `sn_one` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标题',
  `text` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '内容',
  `img` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片',
  `read` int(11) NOT NULL COMMENT '阅读数',
  `give` int(11) NOT NULL COMMENT '点赞',
  `user_id` int(11) NOT NULL COMMENT '作者',
  `comment_id` int(11) unsigned NOT NULL COMMENT '评论',
  `type_id` int(11) NOT NULL COMMENT '分类',
  `time_create` datetime NOT NULL COMMENT '时间',
  `time_modified` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `sn_one_type` (`type_id`),
  KEY `one_user_id` (`user_id`),
  CONSTRAINT `one_type_id` FOREIGN KEY (`type_id`) REFERENCES `sn_one_type` (`id`),
  CONSTRAINT `one_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_one
-- 

/*!40000 ALTER TABLE `sn_one` DISABLE KEYS */;
INSERT INTO `sn_one`(`id`,`title`,`text`,`img`,`read`,`give`,`user_id`,`comment_id`,`type_id`,`time_create`,`time_modified`) VALUES
(8,'vol.001 舔狗日记','没事，你有对象不重要，你可以偶尔回一下我的信息好吗，一天一条也行，让我知道你还在。','321321',8,9,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(9,'vol.002 舔狗日记','时隔30个小时，你终于发了信息给我，你说“宝贝，我想你了。”，我很开心，我终于以为我的舔狗日子到了，可没想到信息发出来两秒都没有，你就撤回了，你说发错了，当我说准备要回没关系的时候，我看见了红色的感叹号。','321321',1,0,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(10,'vol.003 舔狗日记','蒋介石因为宋美龄的一句喜欢梧桐，他便种满了整个南京。而我因为你的一句不喜欢小偷，我便放过了整个上海的电动车。','321321',1,0,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(11,'vol.004 舔狗日记','我今天送了你一支口红，你拿到之后很开心，在他的嘴巴上亲了一下，或许他送你口红的时候，你也会在我的嘴巴上亲一下吧。','321321',4,1,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(12,'vol.005 舔狗日记','别的妹妹叫你打游戏，你让人家语音给你发了句哥哥，你就陪她打一天。我叫你打游戏，你回了我一句 70/h。','321321',6,0,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(13,'vol.006 舔狗日记','今天在楼上窗户上看见你和他在公园里接吻，我看见哭了出来，并打电话给你，想问问你为什么？但你说怎么了，声音是那么好听。于是我说“以后你和他接吻的时候，能不能用我送给你的口红啊？”','321321',5,1,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(14,'vol.007 舔狗日记','今天上班不是太忙，百无聊赖，又翻出了你的相片，看了又看。今天是我认识你的第302天，也是我爱你的第302天，可是这些你并不知道，也许你知道了，也不会在意吧。 此刻的我好想你！','321321',0,0,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(15,'vol.008 舔狗日记','你好像从来没有对我说过晚安，我在我们的聊天记录里搜索了关键字：“晚安”，你说过一次：我早晚安排人弄死你。','321321',0,0,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(16,'vol.009 舔狗日记','她好像从来没有主动说过爱我，我搜索了一下关键字“爱”。在我们的聊天记录里，她只说过一次：爱奇艺会员借我一下。','321321',5,0,4,0,4,'2020-12-18 00:00:00','2020-12-18 00:00:00'),
(17,'vol.010 毒鸡汤','如果人生是一部电影，那你就是，中间弹出来的广告。','string',7,1,4,0,4,'2020-12-23 00:00:00','2020-12-18 00:00:00'),
(20,'vol.011 舔狗日记','现在已经凌晨一点多了，我望着手机屏幕迟迟没有他的消息：你知道吗？我等了一晚上你的消息。他终于回复我了：是我让你等的？','无',5,3,4,0,4,'2020-12-23 00:00:00','2020-12-18 00:00:00'),
(21,'vol.012 舔狗日记','今天你又来我们班看美女了，路过的时候瞥了一眼坐在第一排的我，我就知道你心里还是有我的。啊！真是美好的一天！','无',14,2,4,0,4,'2021-01-03 00:00:00','2020-12-18 00:00:00'),
(22,'vol.013 毒鸡汤','别以为你一无所有，至少你还有丑！','无',11,0,4,0,4,'2021-01-03 00:00:00','2020-12-18 00:00:00'),
(31,'vol.014 舔狗日记','今天打单子赚了56，给你转了52自己留了4块钱。我花两块买了两包泡面，用剩下的两块钱买了一瓶矿泉水，自己烧水泡面吃，而你用那52块钱想都没想的给你别的哥哥买了皮肤。 我太开心了，因为你用上我的钱了，以后我要赚更多的钱给你','无',11,2,4,0,4,'2021-07-20 00:00:00','2020-12-18 00:00:00'),
(33,'vol.015 舔狗日记','今天你说了要和我打电话，我等了一天，马上十二点了才打过来，我有点不高兴就挂了，你骂了句给脸不要脸。我想了一下，哎呀你还会关心我的脸，多么善良的女孩子，我发誓还能再等一天电话','无',7,3,4,0,4,'2021-09-02 00:00:00','2020-12-18 00:00:00'),
(34,'vol.016 舔狗日记','你好像成熟了，你学会隐忍，开始压抑自己对我的感情。这很好……可是我觉得自己被你忽略了……你好像看不见我。这不可能，对吗？','无',12,4,4,0,4,'2021-09-02 00:00:00','2020-12-18 00:00:00');
/*!40000 ALTER TABLE `sn_one` ENABLE KEYS */;

-- 
-- Definition of sn_setblog
-- 

DROP TABLE IF EXISTS `sn_setblog`;
CREATE TABLE IF NOT EXISTS `sn_setblog` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '设置的内容名称',
  `router_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '路由链接',
  `isopen` tinyint(1) NOT NULL COMMENT '是否启用',
  `type_id` int(5) NOT NULL COMMENT '分类',
  `user_id` int(5) NOT NULL COMMENT '关联用户表',
  PRIMARY KEY (`id`),
  KEY `setblog_user_id` (`user_id`),
  KEY `setblog_type_id` (`type_id`),
  CONSTRAINT `setblog_type_id` FOREIGN KEY (`type_id`) REFERENCES `sn_setblog_type` (`id`),
  CONSTRAINT `setblog_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_setblog
-- 

/*!40000 ALTER TABLE `sn_setblog` DISABLE KEYS */;
INSERT INTO `sn_setblog`(`id`,`name`,`router_url`,`isopen`,`type_id`,`user_id`) VALUES
(1,'SetPage','df',0,1,4),
(2,'1','5',1,0,4);
/*!40000 ALTER TABLE `sn_setblog` ENABLE KEYS */;

-- 
-- Definition of sn_user_talk
-- 

DROP TABLE IF EXISTS `sn_user_talk`;
CREATE TABLE IF NOT EXISTS `sn_user_talk` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) DEFAULT NULL,
  `talk_text` varchar(255) DEFAULT NULL COMMENT '说说内容',
  `talk_time` date DEFAULT NULL COMMENT '发表时间',
  `talk_read` int(11) DEFAULT NULL,
  `talk_give` int(11) DEFAULT NULL,
  `comment_id` int(11) DEFAULT NULL COMMENT '评论id',
  PRIMARY KEY (`id`),
  KEY `sn_user_talk_userId` (`user_id`),
  CONSTRAINT `sn_user_talk_userId` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table sn_user_talk
-- 

/*!40000 ALTER TABLE `sn_user_talk` DISABLE KEYS */;
INSERT INTO `sn_user_talk`(`id`,`user_id`,`talk_text`,`talk_time`,`talk_read`,`talk_give`,`comment_id`) VALUES
(12,4,'omorrow is always fresh, with no mistakes in it. 明天始终崭新，无错可言。','2020-10-19 00:00:00',0,0,0),
(13,4,'人生有梦,各自精彩','2020-10-19 00:00:00',0,0,0),
(15,4,'Sometimes you have to sacrifice to do the right thing. 有时候，为了做成正确的事，你必须付出代价。','2020-10-20 00:00:00',0,0,0),
(16,4,'我就是这样,就是和你不一样','2020-10-23 00:00:00',0,0,0),
(17,4,'A brave man never surrenders. 勇者永不屈服。','2020-10-27 00:00:00',0,0,0),
(18,4,'The strongest person is the person who isn''t scared to be alone. 强大的人不会惧怕孤独。','2020-11-11 00:00:00',0,0,0),
(19,4,'Real love is always worth waiting for. 真爱永远值得等待。','2020-11-24 00:00:00',0,0,0),
(20,4,'“嗨，同志！您知道列宁格勒和斯大林格勒在哪吗？我在地图上找不到它。” “没有了，再也没有了，我们失败了，白匪和资本家再一次骑到了我们的头上，如果你要追随那颗红星，去东方吧，穿越第聂伯河，翻过乌拉尔山脉，西伯利亚平原的尽头，那里还燃烧着星星之火。”','2020-12-26 00:00:00',0,0,0);
/*!40000 ALTER TABLE `sn_user_talk` ENABLE KEYS */;

-- 
-- Definition of snippet
-- 

DROP TABLE IF EXISTS `snippet`;
CREATE TABLE IF NOT EXISTS `snippet` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `text` text CHARACTER SET utf8 COLLATE utf8_bin,
  `type_id` int(11) DEFAULT NULL,
  `tag_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `label_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `uid` (`user_id`),
  KEY `tagid` (`tag_id`),
  KEY `typeid` (`type_id`),
  KEY `label` (`label_id`),
  CONSTRAINT `label` FOREIGN KEY (`label_id`) REFERENCES `snippet_label` (`id`),
  CONSTRAINT `tagid` FOREIGN KEY (`tag_id`) REFERENCES `snippet_tag` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `typeid` FOREIGN KEY (`type_id`) REFERENCES `snippet_type` (`id`),
  CONSTRAINT `uid` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=576 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table snippet
-- 

/*!40000 ALTER TABLE `snippet` DISABLE KEYS */;
INSERT INTO `snippet`(`id`,`name`,`text`,`type_id`,`tag_id`,`user_id`,`label_id`) VALUES
(26,'生命周期','| vue3              | 描述                                                         |\n| ----------------- | ------------------------------------------------------------ |\n| setup()           | 开始创建组件之前，在beforeCreate和created之前执行。创建的是data和method |\n| //挂载阶段        |                                                              |\n| onBeforeMount()   | 组件挂载到节点上之前执行的函数。                             |\n| onMounted()       | 组件挂载完成后执行的函数。                                   |\n| //更新            |                                                              |\n| onBeforeUpdate()  | 组件更新之前执行的函数。                                     |\n| onUpdated()       | 组件更新完成之后执行的函数。                                 |\n| //销毁            |                                                              |\n| onBeforeUnmount() | 组件卸载(销毁)之前执行的函数。                               |\n| onUnmounted()     | 组件卸载(销毁)完成后执行的函数                               |\n| onActivated()     | 被包含在 keep-alive 中的组件，会多出两个生命周期钩子函数。被激活时执行。 |\n| onDeactivated()   | 比如从 A 组件，切换到 B 组件，A 组件消失时执行。             |\n| onErrorCaptured() | 当捕获一个来自子孙组件的异常时激活钩子函数                   |',2,2,4,1),
(27,'文本插值({{ msg }})','数据绑定最常见的形式就是使用“Mustache” (双大括号) 语法的文本插值：\n\n```html\n<span>Message: {{ msg }}</span>\n```\n\n使用的是 `Mustache` 语法 (即双大括号)，每次 `msg` 属性更改时它也会同步更新',2,7,4,1),
(28,' AXIOS String contains non ISO-8859-1',' 在头信息中存在中文，需要对其进行编码,使用encodeURIComponent()对中文信息进行编码即可。\n\n``` js\nif (storage.get(store.state.Roles)) {\n    req.headers.Authorization =\n        encodeURIComponent(storage.get(store.state.Roles)) as string\n}\n```',3,4,4,1),
(30,'命名基本约定','\n| **标识符** | **大小写** | **示例**                                 |\n| ---------- | ---------- | ---------------------------------------- |\n| 命名空间   | Pascal     | namespace Com.Techstar.ProductionCenter  |\n| 类型       | Pascal     | public class DevsList                    |\n| 接口       | Pascal     | public interface ITableModel             |\n| 方法       | Pascal     | public void UpdateData()                 |\n| 属性       | Pascal     | Public int Length{…}                     |\n| 事件       | Pascal     | public event EventHandler Changed;       |\n| 私有字段   | Camel      | private string fieldName;                |\n| 非私有字段 | Pascal     | public string FieldName；                |\n| 枚举值     | Pascal     | FileMode{Append}                         |\n| 参数       | Camel      | public void UpdateData(string fieldName) |\n| 局部变量   | Camel      | string fieldName;                        |',1,6,4,1),
(31,'集合类的声明','**声明一个List**\n```c#\nList<string> list = new List<string>();\nlist.Add(\"a一\");\nlist.Add(\"b二\");\n\n//语法糖\nList<string> list = new List<string> {\"def\",\"OK\"};\n```\n\n**遍历集合中的项：**\n\n```c#\nforeach (string item in list)\n{\n  Console.WriteLine(item);\n}\n//语法糖\nlist.ForEach(a => Console.WriteLine(a));\n```\n',1,3,4,1),
(32,'访问修饰符','- public:公有访问，不受任何限制。\n- private:私有访问，只限于本类成员访问。\n- protected:受保护的，只限于本类和子类访问。\n- internal:内部访问，只限于本项目内访问，其他的不能访问\n- protected internal:内部保护访问，只限于本项目或是子类访问，其他的不能访问',1,2,4,1),
(33,'using等于try finally','使用完释放资源，经常要用using，using实质上就是try fiannaly的一个语法糖\n\n```c#\nStreamWriter sw = null;\ntry\n{\n    sw = new StreamWriter(\"d:\\abc.txt\");\n    sw.WriteLine(\"test\");\n}\nfinally {\n    if(sw!= null) sw.Dispose();\n}\n```\n\n简化为：\n\n```c#\nusing (var sw = new StreamWriter(\"d:\\abc.txt\")) {\n    sw.WriteLine(\"test\");\n}\n```\n',1,3,4,1),
(34,'v-html','双大括号将会将数据插值为纯文本，而不是 HTML。若想插入 HTML，你需要使用 v-html 指令：\n``` html\n<p>Using mustaches: {{ rawHtml }}</p>\n<p>Using v-html directive: <span v-html=\"rawHtml\"></span></p>\n```\n双大括号{{}}会将数据解释为纯文本，使用 v-html 指令，将插入 HTML',2,7,4,1),
(35,'v-bind','`v-bind` 指令指示 Vue 将元素的 `id` attribute 与组件的 `dynamicId` property 保持一致。如果绑定的值是 `null` 或者 `undefined`，那么该 attribute 将会从渲染的元素上移除。\n\n```html\n<!-- 完整语法 -->\n<a v-bind:href=\"url\">...</a>\n\n<!-- 缩写 -->\n<a :href=\"url\">...</a>\n\n<!-- 动态参数的缩写 (2.6.0+) -->\n<a :[key]=\"url\"> ... </a>\n```\n',2,7,4,1),
(36,'v-on:click','使用 `v-on` 指令 (简写为 `@`) 来监听 DOM 事件和运行 JavaScript \n#### 基本语法\n```html\n<!-- 完整语法 -->\n<a v-on:click=\"doSomething\">...</a>\n<!-- 缩写 -->\n<a @click=\"doSomething\">...</a>\n<!-- 动态参数的缩写 -->\n<a @[event]=\"doSomething\"> ... </a>\n<!-- 动态的事件名绑定处理函数 -->\n<a v-on:[eventName]=\"doSomething\"> ... </a>\n```\n事件处理器的值：\n1. 内联事件处理器：事件被触发时执行的内联 JavaScript 语句 (与 `onclick` 类似) 。\n2. 方法事件处理器：一个组件的属性名、或对某个方法的访问。',2,7,4,1),
(37,'vite-plugin-restart','[github](https://github.com/antfu/vite-plugin-restart)\n\n通过监听文件修改，自动重启 vite 服务。\n\n最常用的场景就是监听 `vite.config.js` 和 `.env.development` 文件，我们知道，修改 vite 配置文件和环境配置文件，是需要重启 vite 才会生效，通过这个插件，我们将从反复重启中解脱出来。\n\n\nInstall\n\n```bash\nnpm i vite-plugin-restart -D \nyarn add vite-plugin-restart -D\n```\n\nAdd it to `vite.config.js`\n\n```js\n// vite.config.js\nimport ViteRestart from ''vite-plugin-restart''\n\nexport default {\n  plugins: [\n    ViteRestart({\n      restart: [\n        ''my.config.[jt]s'',\n      ]\n    })\n  ],\n};\n```\n',9,1,4,1),
(38,'vite-plugin-tips','[github:](https://github.com/yingpengsha/vite-plugin-tips)\n\n服务器状态提示\n\n Install\n\n``` bash\n$ npm install vite-plugin-tips -D\n```\n\n configuration\n\n``` js\nimport { viteTips } from ''vite-plugin-tips''\n\nexport default {\n  plugins: [\n    viteTips()\n  ]\n}\n```\n\n Options\n\n``` js\ninterface Options {\n  // Whether to enable relevant tips. Default is enabled.\n  connect?: boolean\n  update?: boolean\n  disconnect?: boolean\n}\n```\n\n',9,1,4,1),
(39,'script setup','## script setup\n\nscript setup可以和script同时存在,  script setup中的顶层的导入和变量声明都将自动地在该组件的模板上可用。\n\n``` js\n<script>\n  export const name = 1\n</script>\n\n<script setup>\n  import { ref } from ''vue''\n  const count = ref(0)\n</script>\n```\n',2,3,4,1),
(40,'nextTick','## nextTick\n\n在下次 DOM 更新循环结束之后执行延迟回调。在修改数据之后立即使用这个方法，获取更新后的 DOM\n\n```ts\nimport { nextTick } from ''vue'';\n\nfunction increment() {\n  count.value++\n  nextTick(() => {\n    // 访问更新后的 DOM\n  })\n}\n\n// 还可以使用 async/await\nasync () => {\n    await nextTick()\n    // ....\n}\n```\n',2,3,4,1),
(42,'组件命名','单文件组件名应该始终是**单词大写开头** NewComponent.vue \n\n**子组件命名**\n\n- FooterSection.vue\n- FooterSectionHeading.vue\n\n**没有子组件，尝试加前缀 `the` 来命名**\n\n- TheNavbar.vue\n\n```\n// 反例\ncomponents/\n|- MyButton.vue\n|- VueTable.vue\n\n// 好例子\ncomponents/\n|- BaseButton.vue\n|- BaseTable.vue\n```\n\n\n',2,6,4,1),
(43,'常用命名推荐','**注意**：ad、banner、gg、guanggao 等有机会和广告挂勾的字眠不建议直接用来做ClassName，因为有些浏览器插件（Chrome的广告拦截插件等）会直接过滤这些类名\n\n| ClassName              | 含义                                     |\n| :--------------------- | :--------------------------------------- |\n| about                  | 关于                                     |\n| account                | 账户                                     |\n| arrow                  | 箭头图标                                 |\n| article                | 文章                                     |\n| aside                  | 边栏                                     |\n| audio                  | 音频                                     |\n| avatar                 | 头像                                     |\n| bg,background          | 背景                                     |\n| bar                    | 栏（工具类）                             |\n| branding               | 品牌化                                   |\n| crumb,breadcrumbs      | 面包屑                                   |\n| btn,button             | 按钮                                     |\n| caption                | 标题，说明                               |\n| category               | 分类                                     |\n| chart                  | 图表                                     |\n| clearfix               | 清除浮动                                 |\n| close                  | 关闭                                     |\n| col,column             | 列                                       |\n| comment                | 评论                                     |\n| community              | 社区                                     |\n| container              | 容器                                     |\n| content                | 内容                                     |\n| copyright              | 版权                                     |\n| current                | 当前态，选中态                           |\n| default                | 默认                                     |\n| description            | 描述                                     |\n| details                | 细节                                     |\n| disabled               | 不可用                                   |\n| entry                  | 文章，博文                               |\n| error                  | 错误                                     |\n| even                   | 偶数，常用于多行列表或表格中             |\n| fail                   | 失败（提示）                             |\n| feature                | 专题                                     |\n| fewer                  | 收起                                     |\n| field                  | 用于表单的输入区域                       |\n| figure                 | 图                                       |\n| filter                 | 筛选                                     |\n| first                  | 第一个，常用于列表中                     |\n| footer                 | 页脚                                     |\n| forum                  | 论坛                                     |\n| gallery                | 画廊                                     |\n| group                  | 模块，清除浮动                           |\n| header                 | 页头                                     |\n| help                   | 帮助                                     |\n| hide                   | 隐藏                                     |\n| hightlight             | 高亮                                     |\n| home                   | 主页                                     |\n| icon                   | 图标                                     |\n| info,information       | 信息                                     |\n| last                   | 最后一个，常用于列表中                   |\n| links                  | 链接                                     |\n| login                  | 登录                                     |\n| logout                 | 退出                                     |\n| logo                   | 标志                                     |\n| main                   | 主体                                     |\n| menu                   | 菜单                                     |\n| meta                   | 作者、更新时间等信息栏，一般位于标题之下 |\n| module                 | 模块                                     |\n| more                   | 更多（展开）                             |\n| msg,message            | 消息                                     |\n| nav,navigation         | 导航                                     |\n| next                   | 下一页                                   |\n| nub                    | 小块                                     |\n| odd                    | 奇数，常用于多行列表或表格中             |\n| off                    | 鼠标离开                                 |\n| on                     | 鼠标移过                                 |\n| output                 | 输出                                     |\n| pagination             | 分页                                     |\n| pop,popup              | 弹窗                                     |\n| preview                | 预览                                     |\n| previous               | 上一页                                   |\n| primary                | 主要                                     |\n| progress               | 进度条                                   |\n| promotion              | 促销                                     |\n| rcommd,recommendations | 推荐                                     |\n| reg,register           | 注册                                     |\n| save                   | 保存                                     |\n| search                 | 搜索                                     |\n| secondary              | 次要                                     |\n| section                | 区块                                     |\n| selected               | 已选                                     |\n| share                  | 分享                                     |\n| show                   | 显示                                     |\n| sidebar                | 边栏，侧栏                               |\n| slide                  | 幻灯片，图片切换                         |\n| sort                   | 排序                                     |\n| sub                    | 次级的，子级的                           |\n| submit                 | 提交                                     |\n| subscribe              | 订阅                                     |\n| subtitle               | 副标题                                   |\n| success                | 成功（提示）                             |\n| summary                | 摘要                                     |\n| tab                    | 标签页                                   |\n| table                  | 表格                                     |\n| txt,text               | 文本                                     |\n| thumbnail              | 缩略图                                   |\n| time                   | 时间                                     |\n| tips                   | 提示                                     |\n| title                  | 标题                                     |\n| video                  | 视频                                     |\n| wrap                   | 容器，包，一般用于最外层                 |\n| wrapper                | 容器，包，一般用于最外层                 |',2,6,4,1),
(44,'简介','\n> Typed JavaScript at Any Scale.\n> 添加了类型系统的 JavaScript，适用于任何规模的项目。\n- TypeScript 是一门静态类型、弱类型的语言。\n- TypeScript 是完全兼容 JavaScript 的，它不会修改 JavaScript 运行时的特性。\n\n- TypeScript 可以编译为 JavaScript，然后运行在浏览器、Node.js 等任何能运行 JavaScript 的环境中。\n\n- TypeScript 拥有很多编译选项，类型检查的严格程度由你决定。\n\n- TypeScript 可以和 JavaScript 共存，这意味着 JavaScript 项目能够渐进式的迁移到 TypeScript。\n\n- TypeScript 增强了编辑器（IDE）的功能，提供了代码补全、接口提示、跳转到定义、代码重构等能力。\n\n- [JavaScript 备忘清单](http://ref.ecdata.cn/docs/javascript.html)\n- [TypeScript 官网](https://www.typescriptlang.org/)',5,2,4,1),
(45,'字符串(String)','一个字符系列，使用单引号（**''**）或双引号（**\"**）来表示字符串类型。反引号（**`**）来定义多行文本和内嵌表达式。\n\n```tsx\nlet myName: string = ''Tom'';\nlet myAge: number = 25;\n\n// 模板字符串\nlet sentence: string = `Hello, my name is ${myName}.\nI''ll be ${myAge + 1} years old next month.`;\n```\n',5,2,4,1),
(47,'C#类型转换','| 序号 | 方法 & 描述                                                  |\n| ---- | ------------------------------------------------------------ |\n| 1    | **ToBoolean** 如果可能的话，把类型转换为布尔型。             |\n| 2    | **ToByte** 把类型转换为字节类型。                            |\n| 3    | **ToChar** 如果可能的话，把类型转换为单个 Unicode 字符类型。 |\n| 4    | **ToDateTime** 把类型（整数或字符串类型）转换为 日期-时间 结构。 |\n| 5    | **ToDecimal** 把浮点型或整数类型转换为十进制类型。           |\n| 6    | **ToDouble** 把类型转换为双精度浮点型。                      |\n| 7    | **ToInt16** 把类型转换为 16 位整数类型。                     |\n| 8    | **ToInt32** 把类型转换为 32 位整数类型。                     |\n| 9    | **ToInt64** 把类型转换为 64 位整数类型。                     |\n| 10   | **ToSbyte** 把类型转换为有符号字节类型。                     |\n| 11   | **ToSingle** 把类型转换为小浮点数类型。                      |\n| 12   | **ToString** 把类型转换为字符串类型。                        |\n| 13   | **ToType** 把类型转换为指定类型。                            |\n| 14   | **ToUInt16** 把类型转换为 16 位无符号整数类型。              |\n| 15   | **ToUInt32** 把类型转换为 32 位无符号整数类型。              |\n| 16   | **ToUInt64** 把类型转换为 64 位无符号整数类型。              |',1,2,4,1),
(48,'CSS简介','层叠样式表(英文全称：Cascading Style Sheets)是一种用来表现[HTML](https://baike.baidu.com/item/HTML)（[标准通用标记语言](https://baike.baidu.com/item/标准通用标记语言/6805073)的一个应用）或[XML](https://baike.baidu.com/item/XML)（标准通用标记语言的一个子集）等文件样式的计算机语言。CSS不仅可以静态地修饰网页，还可以配合各种脚本语言动态地对网页各元素进行格式化。 [1] \n\nCSS 能够对网页中元素位置的排版进行像素级精确控制，支持几乎所有的字体字号样式，拥有对网页对象和模型样式编辑的能力。',8,2,4,1),
(49,'代码大小写','样式选择器，属性名，属性值关键字全部使用小写字母书写，属性字符串允许使用大小写。\n',8,6,4,1),
(50,'选择器','选择器根据不同的需求把不同的标签选出来这就是选择器的作用。\nCSS选择器的解析是从上到下，从右向左解析，为了避免对所有元素进行解析\n- 可继承的样式：font-size, font-family, color，ul，li，dl，dt，dd；\n- 不可继承的样式：border, padding, margin, width, height\n\n**优先级**\n\n| 选择器         | 权重值 |\n| :------------- | :----- |\n| !important标识 | 10000  |\n| 行内样式       | 1000   |\n| id选择器       | 100    |\n| 类选择器       | 10     |\n| 标签选择器     | 1      |\n| 通配符 *       | 0      |\n\n> !important>行内样式>ID选择器>类、伪类、属性>元素、伪元素>继承>通配符',8,6,4,1),
(51,'标签选择器','标签选择器是指用HTML标签名称作为选择器，按标签名称分类，为页面中某一类的标签指定统一的CSS样式。\n\n```css\np{\n    color:red;\n }\n\n标签名{\n属性1;属性值1;\n属性2;属性值2;\n......\n}\n```\n\n作用：可以把某一类标签全部选择出来,比如所有的div标签和所有的span标签\n优点：能快速为页面中同类型的标签统一设置样式。\n缺点：不能设计差异化模式，只能选择全部的当前标签。',8,2,4,1),
(52,'类选择器','如果想要差异化选择不同的标签，单独选一个或几个标签，可以使用类选择器。\n\n语法：\n\n```css\n.类名{\n 属性1;属性值1;\n ......\n }\n```\n\n结构需要用class属性来调用class类的意思。\n\n```html\n<div class=\"red\">变颜色</div>\n```\n\n注意：\n\n- 类选择器使用“.”（英文点号）进行标识，后面紧跟类名(自定义，我们自己命名的）。\n- 可以理解为给这个个标签起了一个名字，来表示。\n- 长名称或词组可以使用中横线来为选择器命名。\n- 不要使用纯数字、中文等命名，尽量使用英文字母来表示。\n- 命名要有意义，尽量使别人一眼就知道这个类名的目的。\n- 命名规范：见附件（web前端开发规范手册.doc）\n\n**类选择器口诀：\n\n> 样式点定义 结构类调用 一个或多个 开发最常用\n\n### 类命名规则\n\n| 头       | header            | 菜单     |    menu    |\n| -------- | ----------------- | -------- | :--------: |\n| 内容     | content/container | 子菜单   |  submenu   |\n| 尾       | footer            | 搜索     |   search   |\n| 导航     | nav               | 友情链接 | friendlink |\n| 侧栏     | sidebar           | 页脚     |   footer   |\n| 栏目     | column            | 版权     | copyright  |\n| 标志     | logo              | 广告     |   banner   |\n| 页面主体 | main              | 热点     |    hot     |\n| 新闻     | news              | 下载     |  download  |\n| 子导航   | subnav            |          |            |',8,2,4,1),
(53,'ID选择器','id选择器可以为标有特点id的HTML元素指定特定的样式。HTML元素以id属性来设置ID选择器，CSS中id选择器以“#”来定义。\n\n```html\n<div id=\"warning\">----</div>\n```\n\n```css\n#warning{color:red;}\n\n# id名{\n属性1;属性值1;\n......\n}\n```\n\n注意：id属性只能在每个html文档中出现一次。\n\n口诀：\n\n> 样式自定义，结构id调用，只能调用一次，别人切勿使用。\n\n### ID选择器和类选择器区别\n\n- 类选择器（class）可以有多个class，同时一个class也可以被多个使用。\n- ID选择器好比人的身份证号码，全中国是唯一的，不得重复。\n- ID选择器和类选择器最大的不同在于使用次数上。\n- 类选择器在修改样式中用的最多，ID选择器一般用于页面唯一性的元素上，经常和JAVASCRIPT搭配使用。',8,2,4,1),
(54,'通配符选择器','通配符选择器用“*”定义，它表示选取页面中所有元素（标签）\n\n语法：\n\n```bash\n * {\n属性1；属性值1；\n......\n}\n```\n\n- 通配符选择器不需要调用，自动就给所有的元素使用样式。\n- 特殊情况下才使用，后面讲解使用场景（以下是清除所有的元素标签的内外边距，后期讲）。\n\n```css\n* {\nmargin: 0;\npadding: 0;\n}\n```\n',8,2,4,1),
(55,'属性选择器','```html\n<ul>\n    <li foo>1</li>\n    <li foo=\"abc\">2</li>\n    <li foo=\"abc efj\">3</li>\n    <li foo=\"abcefj\">4</li>\n    <li foo=\"efjabc\">5</li>\n    <li foo=\"ab\">6</li>\n</ul>\n```\n\n```css\n//选择 attribute=value 的所有元素。\n[foo=abc]{\n    background-color:red;\n}\n\n//选择 attribute 属性包含单词 value 的所有元素。\n[foo~=abc]{\n    background-color:red;\n}\n\n//选择其 attribute 属性值以 value 开头的所有元素。类似正则的 ^,以什么开始\n[foo^=abc]{\n    background-color:red;\n}\n\n//选择其 attribute 属性值以 value 结束的所有元素。类似正则的 $,以什么结束\n[foo$=abc]{\n    background-color:red;\n}\n\n//选择其 attribute 属性中包含 value 子串的每个元素。\n[foo*=abc]{\n    background-color:red;\n}\n\n//选择 attribute 属性值以 value 开头的所有元素。\n[foo|=abc]{\n    background-color:red;\n}\n```\n',8,2,4,1),
(57,'子选择器','子元素选择器（子选择器）只能选择作为某元素的最近一级子元素，简单理解就是选亲儿子元素。\n\n```css\n ul>li>p{\n   border: 1px solid red;\n}\n语法：元素1>元素2{样式声明}\n```\n\n上述语法表示选择元素1里面的所有直接后代（子元素）元素2.\n\n注意：\n\n- 元素1和元素2中间用大于号隔开。\n- 元素1是父级元素，2是子级元素，最终选择的是元素2.\n- 元素2必须是亲儿子，其孙子、重孙不归他管，也可以叫亲儿子选择器。',8,2,4,1),
(58,'并集选择器','并集选择器可以选择多组标签，同时为他们定义相同的样式，通常用于集体声明。\n\n并集选择器是各选择器通过英文逗号（，）连接而成，任何形式的选择器都可以作为并集选择器的一部分。\n\n```text\n语法：元素1,元素2{样式声明}\n```\n\n上述语法表示选择元素1和元素2.',8,2,4,1),
(59,'相邻兄弟选择器','```css\n<div>\n    <h1>h1</h1>\n    <p>p1</p>\n    <p>p2</p>\n    <p>p3</p>\n</div>\n\n//选择紧接在 element元素之后的 element 元素。\nh1+p{\n    color:red;\n}\n```\n\n',8,2,4,1),
(60,'一般兄弟选择器','```css\n//选择前面有 element1 元素的每个 elem 元素。\n<div>\n    <h1>h1</h1>\n    <p>p1</p>\n    <p>p2</p>\n    <p>p3</p>\n</div>\n\n h1~p{\n   border: 1px solid red;\n}\n```\n\n',8,2,4,1),
(61,'伪类选择器','伪类选择器用于向某些选择器添加特殊的效果，比如给链接添加特殊效果，或选择第一个，第n个元素。\n\n伪类选择器书写最大的特点是用冒号（：）表示，比如:hover :first-child\n\n因为伪类选择器很多，比如有链接伪类、结构伪类等，所以这里，先给大家讲解常用的链接伪类选择器。\n\n链接伪类选择器\n\n| a:link    | /*选择所有未被访问的链接*/               |\n| --------- | ---------------------------------------- |\n| a:visited | /*选择所有已被访问的链接*/               |\n| a:hover   | /*选择鼠标指针位于其上的链接*/           |\n| a:active  | /*选择活动链接（鼠标按下未弹起的链接）*/ |\n\n注意：\n\n- 为了确保生效，请按照LVHA的循环顺序声明\n\n```text\n:link    :visited-   :hover-    :active \n```\n\n- 记忆法：love hate或者lv包hao\n- 因为a链接在浏览器中具有默认样式，所以我们实际工作中都需要给链接单独指定样式。\n\n链接选择器在实际工作开发中的写法：\n\n```css\n/*a是标签选择器 所有的链接*/\na{\ncolor:gray;\ntext:decoration:none;\n}\n/* :hover是链接伪类选择器 鼠标经过*/\na:hover{\ncolor:red;/*鼠标经过的时候，由原来的灰色变成了红色；*/\ntext-decoration:underline;\n}\n```\n\n#### :focus伪类选择器\n\n：focus伪类选择器用于选取获得焦点的表单元素。\n\n焦点是光标，一般情况下（input）类表单元素才能获取，因此这个选择器也主要针对于表单元素来说。\n\n```css\n语法：input:focus{\nbackgroud-color:yellow;\n}\n```\n',8,2,4,1),
(62,'伪元素选择器','```css\nelement::first-line\n\n//p 元素的第一行发生改变\np:first-line{\n    background-color:yellow;\n}\n\nelement::first-letter\n//直接第一个字符变黄，如果JavaScript做的话，需要获取字符串，再获取第一个字符，再改变其颜色\nh1:first-letter{\n    color:yellow;\n}\n\n//在每个 element 元素的内容之前插入内容。我们更多的可能是当作一个div来用\nelement::before\n\n//在每个element元素的内容之后插入内容。我们可能更多的是用来清除浮动或验证表单提示等其它\nelement::after\n\n//选择被用户选取的元素部分。\n::selection\n```\n',8,2,4,1),
(63,'外边距(margin)','margin 清除周围的（外边框）元素区域。margin 没有背景颜色，是完全透明的。\n\nmargin 可以单独改变元素的上，下，左，右边距，也可以一次改变所有的属性。\n\n| 值       | 说明                                        |\n| :------- | :------------------------------------------ |\n| auto     | 设置浏览器边距。 这样做的结果会依赖于浏览器 |\n| *length* | 定义一个固定的margin（使用像素，pt，em等）  |\n| *%*      | 定义一个使用百分比的边距                    |\n\n#### 边距属性\n\n| 属性          | 描述                                       |\n| :------------ | :----------------------------------------- |\n| margin        | 简写属性。在一个声明中设置所有外边距属性。 |\n| margin-bottom | 设置元素的下外边距。                       |\n| margin-left   | 设置元素的左外边距。                       |\n| margin-right  | 设置元素的右外边距。                       |\n| margin-top    | 设置元素的上外边距。                       |\n\n#### 单边外边距属性\n\n在CSS中，它可以指定不同的侧面不同的边距：\n\n```css\nmargin-top:100px;\nmargin-bottom:100px;\nmargin-right:50px;\nmargin-left:50px;\n```\n\n#### Margin - 简写属性\n\n为了缩短代码，有可能使用一个属性中margin指定的所有边距属性。这就是所谓的简写属性。\n\n所有边距属性的简写属性是 **margin** :\n\n```css\nmargin:100px 50px;\n```\n\nmargin属性可以有一到四个值。\n\n- margin:25px 50px 75px 100px;\n  - 上边距为25px\n  - 右边距为50px\n  - 下边距为75px\n  - 左边距为100px\n- margin:25px 50px 75px;\n  - 上边距为25px\n  - 左右边距为50px\n  - 下边距为75px\n- margin:25px 50px;\n  - 上下边距为25px\n  - 左右边距为50px\n- margin:25px;\n  - 所有的4个边距都是25px\n\n#### 居中显示\n\n```css\nmargin: 0 auto;\n```\n',8,2,4,1),
(64,'padding填充','当元素的 padding（填充）内边距被清除时，所释放的区域将会受到元素背景颜色的填充。\n\n单独使用 padding 属性可以改变上下左右的填充\n\n| 值       | 说明                                |\n| :------- | :---------------------------------- |\n| *length* | 定义一个固定的填充(像素, pt, em,等) |\n| *%*      | 使用百分比值定义一个填充            |\n\n#### 单边内边距属性\n\n在CSS中，它可以指定不同的侧面不同的填充：\n\n```css\npadding-top:25px;\npadding-bottom:25px;\npadding-right:50px;\npadding-left:50px;\n```\n\n#### 简写属性\n\n为了缩短代码，它可以在一个属性中指定的所有填充属性。\n\n这就是所谓的简写属性。所有的填充属性的简写属性是 **padding** :\n\n```css\npadding:25px 50px;\n```\n\nPadding属性，可以有一到四个值。\n\n **padding:25px 50px 75px 100px;**\n\n- 上填充为25px\n\n- 右填充为50px\n\n- 下填充为75px\n\n- 左填充为100px\n\n  **padding:25px 50px 75px;**\n\n- 上填充为25px\n\n- 左右填充为50px\n\n- 下填充为75px\n\n  **padding:25px 50px;**\n\n- 上下填充为25px\n\n- 左右填充为50px\n\n  **padding:25px;**\n\n- 所有的填充都是25px',8,2,4,1),
(65,'属性背景效果(background)','- background-color\n- background-image\n- background-repeat\n- background-attachment\n- background-position\n\n颜色值通常以以下方式定义:\n\n- 十六进制 - 如：\"#ff0000\"\n- RGB - 如：\"rgb(255,0,0)\"\n- 颜色名称 - 如：\"red\"\n',8,2,4,1),
(66,'背景颜色(background)','```css\n background-color: #ffffff;\n```',8,2,4,1),
(67,'背景图像(background)','background-image 属性描述了元素的背景图像.\n\n默认情况下，背景图像进行平铺重复显示，以覆盖整个元素实体.\n\n```css\nbackground: no-repeat center/100% url(\"../img/index.png\");\n```\n',8,2,4,1),
(68,'多个背景图片(background)','```css\nbody {\n  background-image:\n url(https://xxxx.svg),\n url(https://xxxxxx.svg);\n  background-position: center, top;\n  background-repeat: repeat, no-repeat;\n  background-size: contain, cover;\n}\n```\n',8,2,4,1),
(69,'水平方向平铺(background)','```css\nbackground-image:url(''gradient2.png'');\nbackground-repeat:repeat-x;\n```\n',8,2,4,1),
(70,'设置定位与不平铺(background)','让背景图像不影响文本的排版\n如果你不想让图像平铺，你可以使用 background-repeat 属性:\n```css\nbackground-image:url(''img_tree.png'');\nbackground-repeat:no-repeat;\n```\n',8,2,4,1),
(71,'网格背景图像(background)','```html\n<body>\n<div class=\"container\">\n  <div class=\"item_img\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item_img\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item_img\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item_img\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item_img\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item_img\"></div>\n  <div class=\"item\"></div>\n  <div class=\"item_img\"></div>\n  <div class=\"item\"></div>\n</div>\n</body>\n```\n\n```css\nbody {\n margin: 0;\n  padding: 0;\n}\n\n.container {\n  position: absolute;\n  width: 100%;\n  height: 100%;\n  background: black;\n  display: grid;\n  grid-template-columns: 25fr 30fr 40fr 15fr;\n  grid-template-rows: 20fr 45fr 5fr 30fr;\n  grid-gap: 20px;\n  .item_img {\n    background-image: url(''https://images.unsplash.com/photo-1499856871958-5b9627545d1a?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=2207&q=80'');\n  background-repeat: no-repeat;\n  background-position: center;\n  background-attachment: fixed;\n  background-size: cover;\n}\n}\n```\n\n',8,3,4,1),
(72,'全局背景颜色','### 全局背景颜色\n\n```js\nmounted() {\n  document.querySelector(''body'').setAttribute(''style'', ''background-color:#f7f7f7'')\n},\nbeforeDestroy() {\n  document.querySelector(''body'').removeAttribute(''style'')\n}\n```\n\n',2,3,4,1),
(73,'背景属性(background)','| Property              | 描述                                         |\n| :-------------------- | :------------------------------------------- |\n| background            | 简写属性，作用是将背景属性设置在一个声明中。 |\n| background-attachment | 背景图像是否固定或者随着页面的其余部分滚动。 |\n| background-color      | 设置元素的背景颜色。                         |\n| background-image      | 把图像设置为背景。                           |\n| background-position   | 设置背景图像的起始位置。                     |\n| background-repeat     | 设置背景图像是否及如何重复。                 |',8,2,4,1),
(74,'元素隐藏','```css\n/**这个属性只是简单的隐藏某个元素，但是元素占用的空间任然存在；**/\nvisibility: hidden; \n/**属性，设置0可以使一个元素完全透明；**/\nopacity: 0;CSS3\n/**设置一个很大的 left 负值定位，使元素定位在可见区域之外；**/\nposition: absolute; \n/**元素会变得不可见，并且不会再占用文档的空间；**/\ndisplay: none; \n/**将一个元素设置为缩放无限小，元素将不可见，元素原来所在的位置将被保留；**/\ntransform: scale(0); \n```\n\n\n\n\n\n',8,3,4,1),
(75,'虚线效果','```css\n.dotted-line{\n    border: 1px dashed transparent;\n    background: linear-gradient(white,white) padding-box, repeating-linear-gradient(-45deg,#ccc 0, #ccc .25em,white 0,white .75em);\n}\n```\n',8,3,4,1),
(76,'文本超出省略号','**单行文本**\n\n```css\n.single-ellipsis{\n  width: 500px;\n  overflow: hidden;\n  text-overflow: ellipsis;\n  white-space: nowrap;\n}\n```\n\n**多行文本**\n\n```css\n.multiline-ellipsis {\n  display: -webkit-box;\n  word-break: break-all;\n  -webkit-box-orient: vertical;\n  -webkit-line-clamp: 4; //需要显示的行数\n  overflow: hidden;\n  text-overflow: ellipsis;\n}\n```\n',8,3,4,1),
(77,'自动打字','```html\n<div class=\"bruce flex-ct-x\">\n    <div class=\"auto-typing\">Do You Want To Know More About CSS Development Skill</div>\n</div>\n```\n\n```css\n@mixin typing($count: 0, $duration: 0, $delay: 0) {\n    overflow: hidden;\n    border-right: 1px solid transparent;\n    width: #{$count + 1}ch;\n    font-family: Consolas, Monaco, Monospace;\n    white-space: nowrap;\n    animation: typing #{$duration}s steps($count + 1) #{$delay}s backwards, caret 500ms steps(1) #{$delay}s $duration * 2 forwards;\n}\n.auto-typing {\n    font-weight: bold;\n    font-size: 30px;\n    color: #09f;\n    @include typing(52, 5);\n}\n@keyframes typing {\n    from {\n        width: 0;\n    }\n}\n@keyframes caret {\n    50% {\n        border-right-color: currentColor;\n    }\n}\n```\n\n',8,3,4,1),
(78,'渐变色文字','```css\n<h2 class=\"gradient-text\">Gradient text</h2>\n<style>\n.gradient-text {\n  background-image: linear-gradient(90deg, red, blue);\n  background-clip: text;\n  color: transparent;\n}\n</style>\n```\n\n',8,3,4,1),
(79,'清除overflow的滚动条','```css\n.div{\n  overflow: auto\n}\n\n.div::-webkit-scrollbar{\n    display: none;\n}\n```\n\n',8,3,4,1),
(80,'字体属性','CSS使用font-family属性定义文本的字体系列。\n\n```css\np { font-family:\"微软雅黑\";}\ndiv {font-family:Arial,\"Microsoft Yahei\",\"微软雅黑\"；}\n```\n\n注意：\n\n- 各种字体之间必须使用英文状态下的逗号隔开。\n- 一般情况下，如果有空格隔开的多个单词组成的字体，加引号。\n- 尽量使用系统默认自带字体，保证在任何用户的浏览器中都能正确显示。\n- 最常见的几个字体：\n\n```css\nbody {font-family:\"Microdoft Yahei\",tohoma,arial,\"Hiyayinosan GB\";}\n```\n',8,2,4,1),
(81,'字体大小','CSS使用font-size属性定义字体大小\n\n```css\np {font-size:20px;}\n```\n\n注意：\n\n- px（像素）大小是我们网页最常用的单位。\n- 谷歌浏览器默认的文字大小为16px。\n- 不同浏览器可能默认显示的字号大小不一致，我们尽量给一个明确值大小，不要默认大小。\n- 可以给body指定整个页面文字的大小',8,2,4,1),
(82,'字体粗细','CSS使用font-weight属性设置文字字体的粗细。\n\n```css\np {font-weight:normal\\body\\bolder\\light\\number;}\n```\n\n参数：\n\n| normal | 正常字体（相当于number=400）              |\n| ------ | ----------------------------------------- |\n| bold   | 粗体（相当于number=700）                  |\n| bolder | 特粗体（IF5+）                            |\n| light  | 细体（IF5+）                              |\n| number | 100/200/300/400/500/600/700/800/900(IF5+) |\n\nnumber语法：\n\n```css\np {font-weight:400;}\n```\n\n注意：\n\n- 学会加粗标签（比如h和strong等）不加粗，或者其他标签加粗\n- 实际开发时，我们更喜欢用字表示粗细。',8,2,4,1),
(83,'文字模式','CSS使用font-style属性设置文本的网络。\n\n```css\np { font-style: normal; }\n```\n\n| 属性值 | 作用                                                 |\n| ------ | ---------------------------------------------------- |\n| normal | 默认值，浏览器会显示标准的字体样式 font-size：normal |\n| italic | 浏览器会显示斜体的字体样式                           |\n\n注意：平时我们很少给文字加斜体，反而要给斜体标签（em，；）改为不斜体字体。',8,2,4,1),
(84,'字体的复合属性','```css\nbody { font-style font-weight font-size/line-height font-family;}\n```\n\n注意：\n\n- 使用font属性时，必须按上面的语法格式中的顺序书写，不能更换顺序，并且各个属性间以空格隔开。\n- 不需要设置的属性可以忽略（取默认值），但必须保留 font-size和font-family属性，否则font属性将不起作用。',8,2,4,1),
(85,'文本属性','color属性用于定义文本的颜色\n\n```css\ndiv {\ncolor:red;\n    }\n```\n\n注意：开发中最常用的时十六进制。\n\n| 表示           | 属性值                          |\n| -------------- | ------------------------------- |\n| 预定义的颜色值 | red，green，blue，pink          |\n| 十六进制       | #FF0000，#FF6600，#FF29D794     |\n| RGB代码        | rgb（255,0,0）或rgb(100%,0%,0%) |',8,2,4,1),
(86,'文本对齐','text-align属性用于设置元素内文本内容的水平对齐方式。\n\n```css\ndiv {\ntext-align:center;\n}\n```\n\n| 属性值 | 解释             |\n| ------ | ---------------- |\n| left   | 左对齐（默认值） |\n| right  | 右对齐           |\n| center | 居中对齐         |',8,2,4,1),
(87,'文本装饰','text-decoration属性规定添加到文本的修饰，可以给文本添加下划线、删除线、上划线等。\n\n```css\ndiv {\ntext-decoration:underline;\n}\n```\n\n| 属性值       | 解释                            |\n| ------------ | ------------------------------- |\n| none         | 默认，没有装饰线（最常用）      |\n| underline    | 下划线：链接a自带下划线（常用） |\n| overline     | 上划线（几乎不用）              |\n| line-through | 删除线（不常用）                |',8,2,4,1),
(88,'文本缩进','text-indent属性用来指定文本的第一行的缩进，通常是将段落的首行缩进。\n\n```css\ndiv {\ntext-indent:10px/2em;\n}\n```\n\n*:2em代表2个字符',8,2,4,1),
(89,'行间距','line-height属性用于设置行间的距离（行高），可以控制文本的行与行之间的距离。\n\n```css\np { line-height: 26px;}\n```\n\n**:用Faststone chature 软件的标尺测量上一行的行底与下一行的行底之间的距离。',8,2,4,1),
(90,'文本转换','文本转换属性是用来指定在一个文本中的大写和小写字母。\n可用于所有字句变成大写或小写字母，或每个单词的首字母大写。\n```css\np.uppercase {text-transform:uppercase;}\np.lowercase {text-transform:lowercase;} \np.capitalize {text-transform:capitalize;}\n```\n\n',8,2,4,1),
(91,'CSS常用片段','```css\n/* 字体颜色 */\n  color: #888888;\n/* 字体粗细 */\n  font-weight: 400;\n/* 字体大小 */\n  font-size: 14px;\n/* 文字居中 */\n  text-align: center;\n/* 字体行内居中 */\n  line-height: 45px;\n /*字体加深*/\n text-shadow: 0 0 1px rgba(0,0,0,.3);\n /*起始段落空2格*/\n text-indent: 2em;\n```\n\n',8,3,4,1),
(92,'描绘波浪线','```html\n<div class=\"bruce flex-ct-x\">\n    <p class=\"waveline-text\">波浪线文字</p>\n</div>\n```\n\n```css\n@mixin waveline($h, $color: #f66) {\n    position: relative;\n    &::after {\n        position: absolute;\n        left: 0;\n        top: 100%;\n        width: 100%;\n        height: $h;\n        background: linear-gradient(135deg, transparent, transparent 45%, $color, transparent 55%, transparent 100%), linear-gradient(45deg, transparent, transparent 45%, $color, transparent 55%, transparent 100%);\n        background-size: $h * 2 $h * 2;\n        content: \"\";\n    }\n}\n.waveline-text {\n    height: 20px;\n    line-height: 20px;\n    letter-spacing: 10px;\n    @include waveline(10px);\n}\n```\n',8,3,4,1),
(93,'下划线跟随','```html\n<div class=\"bruce flex-ct-x\">\n    <ul class=\"underline-navbar\">\n        <li>11111</li>\n        <li>22222</li>\n        <li>33333</li>\n    </ul>\n</div>\n```\n\n```css\n.underline-navbar {\n    display: flex;\n    li {\n        position: relative;\n        padding: 10px;\n        cursor: pointer;\n        font-size: 20px;\n        color: #09f;\n        transition: all 300ms;\n        &::before {\n            position: absolute;\n            left: 100%;\n            top: 0;\n            border-bottom: 2px solid transparent;\n            width: 0;\n            height: 100%;\n            content: \"\";\n            transition: all 300ms;\n        }\n        &:active {\n            background-color: #09f;\n            color: #fff;\n        }\n        &:hover {\n            &::before {\n                left: 0;\n                top: 0;\n                z-index: -1;\n                border-bottom-color: #09f;\n                width: 100%;\n                transition-delay: 100ms;\n            }\n            & + li::before {\n                left: 0;\n            }\n        }\n    }\n}\n```\n\n\n\n',8,3,4,1),
(94,'透明opacity','### 透明opacity\n\n```css\n opacity: 0.9;\n```\n',8,3,4,1),
(95,'水平垂直居中','**HTML结构如下**\n\n```html\n<div class=\"parent\">\n    <div class=\"child\"></div>\n</div>\n```\n\n#### 使用Flex布局\n\n```css\n.parent {\n    display: flex;\n    justify-content: center;\n    align-items: center;\n}\n```\n\nFlex布局是我日常使用过程中使用最频繁的一种方案，通过很简单的几行代码就可以实现我们想要的布局。\n\n#### 绝对定位\n\n```css\n.parent {\n    position: relative;\n}\n.child {\n    position: absolute;\n    /* top和left是以左上角为坐标原点 */\n    top: 50%;\n    left: 50%;\n    /* transform：translate的属性值为百分数时，是以元素自身为参考 */\n    transform: translate(-50%,-50%);\n}\n\n/* 或者 */\n.child {\n    position: absolute;\n    top: 0;\n    right: 0;\n    bottom: 0;\n    left: 0;\n}\n```\n\n值得说明的是，`top`和`left`是以左上角为坐标原点的，这样就会导致虽然设置为50%，但实际上是实现了左上角这个点的居中，因此需要再移动一下这个元素。而当`transform`的`translate`的属性值为百分数的时候，是以元素自身作为参考的，因此向左和向上移动自身长宽的50%即可实现水平垂直居中。\n\n#### Flex的另一种方法\n\n```css\n.parent {\n    display: flex;\n}\n.child {\n    margin: auto;\n}\n```\n\n#### Grid布局\n\n```css\n.parent {\n    display: grid;\n}\n.child {\n    justify-self: center;\n    align-self: center;\n}\n```\n\nGrid布局也是比较常用的一种方法，一般用在构建整体布局的时候用到的比较多。它有固定和灵活的尺寸，还可以通过名称或网格线把目标放置在指定的位置。\n\n#### Table布局\n\n```css\n.parent {\n    display: table;\n}\n.child {\n    display: table-cell;\n    vertical-align: middle;\n    text-align: center;\n}\n```\n\n',8,3,4,1),
(96,'内部样式表','内部样式表是写到html页面内部，是将所有的CSS代码抽取出来，单独放到一个style标签中。\n\n```css\n<style>\n     div{\n         color:red;\n         font-size: 12px;\n         }\n</style>\n```\n\n- 标签理论上可放在HTML文档的任何地方，但一般会放在文档的head标签中\n- 通过此种方式，可以方便控制当前整个页面中的元素样式设置。\n- 代码结构清晰，但是并没有实现结构与样式完全分离。',8,6,4,1),
(97,'边框样式','边框样式属性指定要显示什么样的边界。\n\nborder-style属性用来定义边框的样式\nnone: 默认无边框\ndotted: 定义一个点线边框\ndashed: 定义一个虚线边框\nsolid: 定义实线边框\ndouble: 定义两个边框。 两个边框的宽度和 border-width 的值相同\ngroove: 定义3D沟槽边框。效果取决于边框的颜色值\nridge: 定义3D脊边框。效果取决于边框的颜色值\ninset:定义一个3D的嵌入边框。效果取决于边框的颜色值\noutset: 定义一个3D突出边框。 效果取决于边框的颜色值\n\n```css\n/* 样式 1px */\nborder-style: ridge;\n```',8,2,4,1),
(98,'边框宽度','通过 border-width 属性为边框指定宽度。\n\n为边框指定宽度有两种方法：可以指定长度值，比如 2px 或 0.1em(单位为 px, pt, cm, em 等)，或者使用 3 个关键字之一，它们分别是 thick 、medium（默认值） 和 thin。\n\n**注意：**CSS 没有定义 3 个关键字的具体宽度，所以一个用户可能把 thick 、medium 和 thin 分别设置为等于 5px、3px 和 2px，而另一个用户则分别设置为 3px、2px 和 1px。\n\n```css\np.one {    \nborder-style:solid; \nborder-width:5px; \n}\np.two {   \nborder-style:solid;   \nborder-width:medium;\n}\n```\n\n',8,2,4,1),
(99,'边框颜色','border-color属性用于设置边框的颜色。\n\n您还可以设置边框的颜色为\"transparent\"。\n**注意：** border-color单独使用是不起作用的，必须得先使用border-style来设置边框样式。\n\n```css\n/* 边框宽度1px 样式 颜色 */\n  border: 1px solid #868686;\n\np.one {  \nborder-style:solid; \nborder-color:red; \n}\np.two {  \nborder-style:solid; \nborder-color:#98bf21;\n}\n```\n\n',8,2,4,1),
(100,'单独设置各边(border)','border-style属性有1-4个值：\n- border-style:dotted solid double dashed;\n  - 上边框 dotted\n  - 右边框 solid\n  - 底边框 double\n  - 左边框 dashed\n- border-style:dotted solid double;\n  - 上边框 dotted\n  - 左、右边框 solid\n  - 底边框 double\n\n可以指定不同的侧面不同的边框：\n\n```css\n/* 上下左右 */\nborder-top-style:dotted; \nborder-right-style:solid;  \nborder-bottom-style:dotted; \nborder-left-style:solid; \n\n/* 多属性 */\nborder-bottom: 1px solid #edeef0;\n```\n',8,2,4,1),
(101,'边框边角弧度','```css\nborder-radius: 5px;\n```\n',8,2,4,1),
(102,'边框的斜线语法','```css\n.border-radius {\n  border-radius: 50px 25px / 25px 50px;\n}\n```',8,3,4,1),
(103,'实线边框','```css\n-webkit-box-shadow: 0 1px 3px rgba(0,0,0,.12), 0 1px 2px rgba(0,0,0,.24);\n box-shadow: 0 1px 3px rgba(0,0,0,.12), 0 1px 2px rgba(0,0,0,.24);\n```\n\n',8,3,4,1),
(104,'悬停放大图片特效','```css\n.img-wrapper img {\n  -webkit-transition: 0.3s linear;\n  transition: 0.3s linear;\n}\n\n.img-wrapper img:hover {\n  transform: scale(1.1);\n}\n```\n\n',8,3,4,1),
(105,'定位(position)','position 属性指定了元素的定位类型。\n\nposition 属性的五个值：\n\nstatic, relative, fixed, absolute, sticky',8,2,4,1),
(106,'static定位','HTML 元素的默认值，即没有定位，遵循正常的文档流对象。\n\n静态定位的元素不会受到 top, bottom, left, right影响。\n\n```css\ndiv.static {    \nposition: static;   \nborder: 3px solid #73AD21; \n}\n```\n',8,2,4,1),
(107,'fixed定位','元素的位置相对于浏览器窗口是固定位置。\n\n即使窗口是滚动的它也不会移动：\n\n```css\np.pos_fixed {   \nposition:fixed;   \ntop:30px;   \nright:5px; \n}\n```\n\n**注意：** Fixed 定位在 IE7 和 IE8 下需要描述 !DOCTYPE 才能支持。\n\nFixed定位使元素的位置与文档流无关，因此不占据空间。\n\nFixed定位的元素和其他元素重叠。',8,2,4,1),
(108,'relative定位','相对定位元素的定位是相对其正常位置。\n\n```css\nh2.pos_left {   \nposition:relative; \nleft:-20px; \n} \nh2.pos_right {\nposition:relative; \nleft:20px; \n}\n```\n\n移动相对定位元素，但它原本所占的空间不会改变。\n\n```css\nh2.pos_top { \nposition:relative;  \ntop:-50px;\n}\n```\n\n相对定位元素经常被用来作为绝对定位元素的容器块。',8,2,4,1),
(109,'absolute定位','绝对定位的元素的位置相对于最近的已定位父元素，如果元素没有已定位的父元素，那么它的位置相对于html\n\n```css\nh2 {   \nposition:absolute;  \nleft:100px;  \ntop:150px; \n}\n```\n\nabsolute 定位使元素的位置与文档流无关，因此不占据空间。\n\nabsolute 定位的元素和其他元素重叠。',8,2,4,1),
(110,'sticky定位','sticky 英文字面意思是粘，粘贴，所以可以把它称之为粘性定位。\n\n**position: sticky;** 基于用户的滚动位置来定位。\n\n粘性定位的元素是依赖于用户的滚动，在 **position:relative** 与 **position:fixed** 定位之间切换。\n\n它的行为就像 **position:relative;** 而当页面滚动超出目标区域时，它的表现就像 **position:fixed;**，它会固定在目标位置。\n\n元素定位表现为在跨越特定阈值前为相对定位，之后为固定定位。\n\n这个特定阈值指的是 top, right, bottom 或 left 之一，换言之，指定 top, right, bottom 或 left 四个阈值其中之一，才可使粘性定位生效。否则其行为与相对定位相同。\n\n**注意:** Internet Explorer, Edge 15 及更早 IE 版本不支持 sticky 定位。 Safari 需要使用 -webkit- prefix (查看以下实例)。\n\n```css\ndiv.sticky {   \nposition: -webkit-sticky; /* Safari */  \nposition: sticky; \ntop: 0;  \nbackground-color: green;   \nborder: 2px solid #4CAF50; \n}\n```\n',8,2,4,1),
(111,'重叠的元素','元素的定位与文档流无关，所以它们可以覆盖页面上的其它元素\n\nz-index属性指定了一个元素的堆叠顺序（哪个元素应该放在前面，或后面）\n\n一个元素可以有正数或负数的堆叠顺序：\n\n```css\nimg {    \nposition:absolute; \nleft:0px;   \ntop:0px;  \nz-index:-1;\n}\n```\n\n具有更高堆叠顺序的元素总是在较低的堆叠顺序元素的前面。\n\n**注意：** 如果两个定位元素重叠，没有指定z - index，最后定位在HTML代码中的元素将被显示在最前面。',8,2,4,1),
(112,'定位属性','\"CSS\" 列中的数字表示哪个CSS(CSS1 或者CSS2)版本定义了该属性。\n\n| 属性       | 说明                                                         | 值                                                           | CSS  |\n| :--------- | :----------------------------------------------------------- | :----------------------------------------------------------- | :--- |\n| bottom     | 定义了定位元素下外边距边界与其包含块下边界之间的偏移。       | auto *length % *inherit                                      | 2    |\n| clip       | 剪辑一个绝对定位的元素                                       | *shape *auto inherit                                         | 2    |\n| cursor     | 显示光标移动到指定的类型                                     | *url* auto crosshair default pointer move e-resize ne-resize nw-resize n-resize se-resize sw-resize s-resize w-resize text wait help | 2    |\n| left       | 定义了定位元素左外边距边界与其包含块左边界之间的偏移。       | auto *length % *inherit                                      | 2    |\n| overflow   | 设置当元素的内容溢出其区域时发生的事情。                     | auto hidden scroll visible inherit                           | 2    |\n| overflow-y | 指定如何处理顶部/底部边缘的内容溢出元素的内容区域            | auto hidden scroll visible no-display no-content             | 2    |\n| overflow-x | 指定如何处理右边/左边边缘的内容溢出元素的内容区域            | auto hidden scroll visible no-display no-content             | 2    |\n| position   | 指定元素的定位类型                                           | absolute fixed relative static inherit                       | 2    |\n| right      | 定义了定位元素右外边距边界与其包含块右边界之间的偏移。       | auto *length % *inherit                                      | 2    |\n| top        | 定义了一个定位元素的上外边距边界与其包含块上边界之间的偏移。 | auto *length % *inherit                                      | 2    |\n| z-index    | 设置元素的堆叠顺序                                           | *number *auto inherit                                        | 2    |',8,2,4,1),
(113,'定位示例','#### 元素固定\n\n```css\n@media (min-height: 500px) {\n    .site-header {\n        position: sticky;\n        top: 0;\n        /*other styles*/\n    }\n}\n```\n\n#### 居中定位\n\n示例1\n\n```c#\n   position: absolute;\n    width: 50%;\n    height: 80%;\n    overflow: auto;\n    margin: auto;\n    position: absolute;\n    top: 0;\n    left: 0;\n    bottom: 0;\n    right: 0;\n```\n\n示例2\n\n```css\n.tag_search {\n\n  @apply relative;\n  height: 50px;\n\n  div {\n    @apply absolute top-1/2 left-1/2;\n    width: 40%;\n    transform: translate(-50%, -50%);\n  }\n}\n```\n\n示例3\n\n@apply居中\n\n```css\n  // 居中\n  @apply top-1/2 left-1/2 absolute;\n  transform: translate(-50%, -50%);\n```\n',8,3,4,1),
(114,'设置全局片段','有些代码片段是需要经常写的，我们在VSCode中我们可以生成一个代码片段，方便我们快速生成。\n\n1.复制自己需要生成代码片段的代码；\n\n2.[snippet-generator.app/](https://link.juejin.cn?target=https%3A%2F%2Fsnippet-generator.app%2F) 在网站中生成代码片段；\n\n3.在VSCode中配置代码片段；\n\n设置 - 用户代码片段 - - 粘贴生成的代码片段\n\n1. 可以直接打开现有代码片段\n2. 可以选择创建全局代码片段\n3. 可以选择创建当前文件夹的代码片段',13,2,4,1),
(116,'Code Spell Checker','检测你写代码时拼写的单词是否正确。\n\n#### 安装方法\n\n1. 在应用商店中搜索Code Spell Checker。\n2. 点击安装，等待安装完成，然后重启VSCode。\n\n#### Add Word to Workspace Dictionaries\n\n（向工作区词典添加单词）\n\n选中要添加的单词–>右击鼠标–>点击这个Add Word to Workspace Dictionaries，这样下次在拼写这个单词的时候便不会出现波浪线的提示。\n\n#### Add Word to User Dictionary\n\n（将单词添加到用户词典）\n操作步骤如上，只需要选中不同的Add Word to User Dictionary功能即可。\n\n#### Ignore Word\n\n（忽略单词）\n\n操作步骤如上，只需要选中不同的Ignore Word功能即可。\n\n\n#### 删除单词解决方案：\n\nAdd Word to Workspace Dictionaries，这就是意味着添加进来了工作区词典，而在VSCode中创建项目时，系统会给我们生成一个.vscode的文件夹，修改settings.json即可\n\n\n\n\nAdd Word to User Dictionary，这就需要我们再我们再VSCode的系统设置中修改。\n\n设置–>用户–>扩展–>找到安装的Code Spell Checker–>打开settings.json，在其中找到图6的内容，在其中修改添加错的单词，我在其中添加的是bindblur单词。\n\n Ignore Word，方法和第一个一样，因为都是在.vscode中的setting.json文件中。\n\n\n#### 总结\n\nAdd Word to Workspace Dictionaries和Ignore Word仅是将word添加到该项目中，如果再新建一个项目以前添加的word将不再在新项目中生效，而Add Word to User Dictionary是将word添加到VSCode的系统中，所以即是新建一个项目也不会提示有波浪线。',13,1,4,1),
(117,'Code Runner','可以让你在 `vscode` 里方便的运行某个文件或者运行选取的某段代码。\n### Bracket Pair Colorizer 2\n\n- 给你的括号用颜色标记一下，这样当你的函数块里面有很多嵌套，就比较容易理清楚。\n\n### Debugger for Chrome\n\n- 我们知道可以在 `chrome` 浏览器中利用开发者工具打断点调试，那么如何在 `vscode` 进行 `debugger` 调试？\n- [在 vscode 中调试 vue](',13,1,4,1),
(118,'Debugger for Chrome','### Debugger for Chrome\n\n- 我们知道可以在 `chrome` 浏览器中利用开发者工具打断点调试，那么如何在 `vscode` 进行 `debugger` 调试？\n- [在 vscode 中调试 vue',13,1,4,1),
(119,'ESLint','- 代码检查及错误提示。\n- 结合 Prettier 插件可以让我们快速格式化代码。',13,1,4,1),
(120,'Prettier','- 格式化代码，规范代码风格。',13,1,4,1),
(121,'int.TryParse()','```csharp\npublic static bool TryParse(string s, out Int32 result);\n```\n\n转换成功返回true,否则返回false\n\n```c#\n//参数：s是要转换的字符，i是转换的结果。执行成功返回true，输出转换成功的值；执行失败返回0\nint.TryParse(string s,out int i)\n```\n',1,3,4,1),
(122,'string转换枚举类型','枚举 字段 = （枚举）Enum.Pares(typeof(枚举)，转换的字段)；\n\n```csharp\n QQ state = (QQ) Enum.Pares(typeof(QQ )，str);\n```\n\n\n\n',1,3,4,1),
(123,'命名规范','- Pascal法：首字母大写，连接词的首字母也都大写\n  →GetInfo\n- Camel：第一个单词的首字母小写\n  →getInfo\n- MENTIONS:\n\n1. 成员变量最好加一个“_”\n2. 接口的名称前加前缀“*I*”',1,6,4,1),
(124,'代码编写规则','- 尽量使用接口\n- 局部变量尽量在最接近的地方使用\n- 不要使用goto系列语句（除非是在跳出深层循环时）\n- 构建和构建一个长的字符串时，一定要使用**StringBuilder**类型\n- switch语句要有**default**来处理意外情况',1,6,4,1),
(125,'问号的演变','表示左边的变量如果为null则值为右边的变量，否则就是左边的变量值\n\n```c#\nstring a = null;\nvar b = a??””;\n```\n\n',1,3,4,1),
(126,'类型实例化','```c#\npublic class Abc\n{\n    public int ID {get; set; }\n    public string Name { get; set; }\n}\n\npublic static void Main(string[] args)\n{\n  var abc = new Abc\n  {\n    ID = 1,\n    Name = \"yukaizhao\",\n  };\n}\n```\n',1,3,4,1),
(127,'匿名类','匿名类在linq to sql或者entity framework中返回查询数据时很好用。\n\n```csharp\nvar a = new {\n    ID = 1,\n    Name=”yukaizhao”,\n};\n```\n',1,3,4,1),
(128,'NULL条件运算符','```csharp\n//使用代码\nCustomer customer = new Customer();\nstring name = customer?.Name;\n\n//编译代码\nCustomer customer = new Customer();\nif (customer != null)\n{\n    string name = customer.Name;\n}\n//和??组合起来使用\nif (customer?.Face()??false)\n还可以两个一起组合来使用\nint? contactNameLen = contact?.Name?.Length; \n```\n\n在对象使用前检查是否为null。如对象空，则赋值给变量为空值，所以例子中需要一个为空的int类型、即int?。如果对象不为空，则调用对象的成员取值，并赋值给变量。',1,3,4,1),
(129,'字符串格式化','String.Format:使用{0}占位符、必须顺序来格式化\n\n```csharp\n    var contactInfo = string.Format(\"Id:{0} Name:{1} EmailAddr:{2} PhoneNum:{3}\", \n    contact.Id, contact.Name, contact.EmailAddress, contact.PhoneNum);\n\n    //新的语法\n    var contactInfo2 = $\"Id:{contact.Id} Name:{contact.Name} EmailAddr:\n    {contact.EmailAddress} PhoneNum:{contact.PhoneNum}\";\n\n    //新格式化方式还支持任何表达式的直接赋值：\n    var contactInfo = $\"Id:{contact.Id} Name:{(contact.Name.Length == 0 ? \"Frank\" : \n    contact.Name)} EmailAddr:{contact.EmailAddress} PhoneNum:{contact.PhoneNum}\";\n```\n',1,3,4,1),
(130,'绑定属性(v-bind)','```html\n<img v-bind:src=\"imgUrl\"/>\n```\n',2,7,4,1),
(131,'动态绑定(v-bind)','在某些情况下，我们属性的名称可能也不是固定的：\n如果属性名称不是固定的，我们可以使用 `:[属性名]=''值''` 的格式来定义\n这种绑定的方式，我们称之为动态绑定属性；\n```js\nconst objectOfAttrs = {\n  id: ''container'',\n  class: ''wrapper''\n}\n//通过不带参数的 v-bind，你可以将它们绑定到单个元素上：\n<div v-bind=\"objectOfAttrs\"></div>\n```\n\n\n',2,7,4,1),
(132,'绑定style(v-bind)','CSS property 名可以用驼峰式 (camelCase) 或短横线分隔 (kebab-case，记得用引号括起来) 来命名\n-  绑定style对象语法\n```\n:style=\"包含css样式的对象\"\n:style=\"{属性名: ''属性值''}\"\n```\n​      1- {color: ''red''} 这里需要注意属性值必须添加'''', 如果没有引号会将red当做变量去数据中寻找\n​      2- { fontSize: ''24px''} ,这里也可以写成烤串命名法，但是需要用''''包起来，否则会报错\n​      3- {fontSize: `${finalFontSize}px`} 也可以写成字符串拼接\n​      4- :style=\"finalStyleObj\" 也可以直接绑定对象\n​      5- 也可以写在方法或者计算属性中\n``` html\n  <!-- <h2 :style=\"{key(属性名):value(属性值)}\">{{massage}}</h2> -->\n  <!-- 这里要加'' ''要不然vue会去解析50px这个变量然后报错 -->\n  :style=\"{fontSize: ''50px''}\">{{massage}}\n  <!-- finalSize当成一个变量在使用 -->\n  :style=\"{fontSize: finalSize}\">{{massage}}\n  <!-- 也可以拼接 -->\n   :style=\"{fontSize: finalSize + ''px'',color:finalColor}\">{{massage}}\n  <!-- 数组语法 -->\n  :style=\"[baseStyle,baseStyle1]\">{{massage}}\n\n\nconst app = new Vue({\n	el: \"#app\",\n	data: {	\n        finalSize: 100,\n        finalColor: ''red'',\n        baseStyle:{color:''red''},\n        baseStyle1:{fontSize:''75px''}\n		}})\n\n```\n\n',2,7,4,1),
(133,'JavaScript表达式','```js\n{{ number + 1 }}\n{{ ok ? ''YES'' : ''NO'' }}\n{{ message.split('''').reverse().join('''') }\n<div :id=\"`list-${id}`\"></div>\n```\n\n这些表达式都会被作为 JavaScript ，以组件为作用域解析执行。\n\n在 vue 模板内，JavaScript 表达式可以被使用在如下场景上：\n\n- 在文本插值中 (双大括号)\n- 在任何 Vue 指令 (以 `v-` 开头的特殊 attribute) attribute 的值中',2,3,4,1),
(134,'调用函数(v-bind)','可以在绑定的表达式中使用一个组件暴露的方法：\n```html\n<span :title=\"toTitleDate(date)\">\n  {{ formatDate(date) }}\n</span>\n```\n\n',2,7,4,1),
(135,'内联事件处理器(v-on:click)','\n```js\nconst count = ref(0)\n```\n```js\n<button @click=\"count++\">Add 1</button>\n<p>Count is: {{ count }}</p>\n```\n\n',2,7,4,1),
(136,'方法事件处理器(v-on:click)','`v-on` 也可以接受一个方法名或对某个方法的调用。\n\n```js\nconst name = ref(''Vue.js'')\n\nfunction greet(event) {\n  alert(`Hello ${name.value}!`)\n  // `event` 是 DOM 原生事件\n  if (event) {\n    alert(event.target.tagName)\n  }\n}\n<!-- `greet` 是上面定义过的方法名 -->\n<button @click=\"greet\">Greet</button>\n```\n\n通过被触发事件的 `event.target.tagName` 访问到该 DOM 元素。',2,7,4,1),
(137,'内联处理器中调用方法(v-on:click)','除了直接绑定方法名，你还可以在内联事件处理器中调用方法。这允许我们向方法传入自定义参数以代替原生事件：\n\n```js\nfunction say(message) {\n  alert(message)\n}\n<button @click=\"say(''hello'')\">Say hello</button>\n<button @click=\"say(''bye'')\">Say bye</button>\n```\n',2,7,4,1),
(138,'内联事件处理器中访问事件参数(v-on:click)','有时需要在内联事件处理器中访问原生 DOM 事件。可以向该处理器方法传入一个特殊的 `$event` 变量，或者使用内联箭头函数：\n```js\nfunction warn(message, event) {\n  // `这里可以访问 DOM 原生事件`\n  if (event) {\n    event.preventDefault()\n  }\n  alert(message)\n}\n```\n```html\n<!-- 使用特殊的 $event 变量 -->\n<button @click=\"warn(''Form cannot be submitted yet.'', $event)\">\n  Submit\n</button>\n\n<!-- 使用内联箭头函数 -->\n<button @click=\"(event) => warn(''Form cannot be submitted yet.'', event)\">\n  Submit\n</button>\n```\n',2,7,4,1),
(139,'多事件处理器(v-on:click)','事件处理程序中可以有多个方法，这些方法由逗号运算符分隔：\n\n```html\n<button @click=\"one($event), two($event)\">Submit</button>\n```\n\n```js\n  one(event) {\n    // 第一个事件处理器逻辑...\n  },\n  two(event) {\n   // 第二个事件处理器逻辑...\n  }\n```\n',2,7,4,1),
(140,'事件修饰符(v-on:click)','在处理事件时调用 `event.preventDefault()` 或 `event.stopPropagation()` 是很常见的。尽管我们可以直接在方法内调用，但如果方法能更专注于数据逻辑而不用去处理 DOM 事件的细节会更好。\n\n```html\n<!-- 单击事件将停止传递 -->\n<a @click.stop=\"doThis\"></a>\n<!-- 提交事件将不再重新加载页面 -->\n<form @submit.prevent=\"onSubmit\"></form>\n<!-- 修饰语可以使用链式书写 -->\n<a @click.stop.prevent=\"doThat\"></a>\n<!-- 也可以只有修饰符 -->\n<form @submit.prevent></form>\n<!-- 添加事件监听器时使用事件捕获模式 -->\n<!-- 即内部元素触发的事件先在此处理，然后才交由内部元素进行处理 -->\n<div v-on:click.capture=\"doThis\">...</div>\n<!-- 仅当 event.target 是元素本身时才会触发事件处理器 -->\n<!-- 例如：事件处理器不来自子元素 -->\n<div @click.self=\"doThat\">...</div>\n<!-- 点击事件将只会触发一次 -->\n<a v-on:click.once=\"doThis\"></a\n    <!-- 滚动事件的默认行为 (即滚动行为) 将会立即触发 -->\n<!-- 而不会等待 `onScroll` 完成  -->\n<!-- 这其中包含 `event.preventDefault()` 的情况 -->\n<div v-on:scroll.passive=\"onScroll\">...</div>\n```\n',2,7,4,1),
(141,'按键修饰符','在监听键盘事件时，我们经常需要检查特定的按键。Vue 允许在 `v-on` 或 `@` 监听按键事件时添加按键修饰符。\n\n```html\n<!-- 仅在 `key` 为 `Enter` 时调用 `vm.submit()` -->\n<input @keyup.enter=\"submit\" />\n\n<!--仅会在 $event.key 为 ''PageDown'' 时调用事件处理。 -->\n<input @keyup.page-down=\"onPageDown\" />\n```\n\n',2,7,4,1),
(142,'.exact修饰符','\n修饰符允许你控制由精确的系统修饰符组合触发的事件\n```html\n<!-- 即使 Alt 或 Shift 被一同按下时也会触发 -->\n<button v-on:click.ctrl=\"onClick\">A</button>\n<!-- 有且只有 Ctrl 被按下的时候才触发 -->\n<button v-on:click.ctrl.exact=\"onCtrlClick\">A</button>\n<!-- 没有任何系统修饰符被按下的时候才触发 -->\n<button v-on:click.exact=\"onClick\">A</button>\n```\n',2,7,4,1),
(143,'系统按键修饰符','你可以使用以下系统按键修饰符来触发鼠标或键盘事件监听器，只有当按键被按下时才会触发。\n- `.ctrl`\n- `.alt`\n- `.shift`\n- `.meta`\n\n```html\n<!-- Alt + Enter -->\n<input @keyup.alt.enter=\"clear\" />\n<!-- Ctrl + 点击 -->\n<div @click.ctrl=\"doSomething\">Do something</div>\n```\n',2,7,4,1),
(144,'事件处理器标注类型','在处理原生 DOM 事件时，应该为我们传递给事件处理器的参数正确地标注类型\n\n```js\nfunction handleChange(event) {\n  // `event` 隐式地标注为 `any` 类型\n  console.log(event.target.value)\n}\n```\n\n```html\n<template>\n  <input type=\"text\" @change=\"handleChange\" />\n</template>\n```\n\n没有类型标注时，这个 `event` 参数会隐式地标注为 `any` 类型。这也会在 `tsconfig.json` 中配置了 `\"strict\": true` 或 `\"noImplicitAny\": true` 时报出一个 TS 错误。因此，建议显式地为事件处理器的参数标注类型。此外，你可能需要显式地强制转换 `event` 上的 property：\n\n```js\nfunction handleChange(event: Event) {\n  console.log((event.target as HTMLInputElement).value)\n}\n```\n',2,7,4,1),
(145,'项目目录对照','``` bash\n.\n├── vue.config.js/                      # webpack 配置文件；\n├── config/                     # 与项目构建相关的常用的配置选项；\n│   ├── index.js                # 主配置文件\n│   ├── dev.env.js              # 开发环境变量\n│   ├── prod.env.js             # 生产环境变量\n│   └── test.env.js             # 测试环境变量\n│\n├── src/\n│   ├── main.js                 # 的入口文件；\n│   ├── assets/                 # 共用的代码以外的资源，如：图片、图标、视频 等；\n│   ├── api/                    # 网络模块，如：接口；\n│   ├── router/                 # 路由模块\n│   ├── I18n/                   # 国际化模块\n│   ├── pages/                  # 单页页面\n│   ├── vuex/                   # 组件共享状态\n│   ├── components/             # 共用的组件；； 这里的存放的组件应该都是展示组件\n│   │   ├── base/               # 基本组件，如：共用的弹窗组件，loading加载组件，提示组件。\n│   │   ├── common/             # 共用的全局组件，封装的导航条，底部组件等等\n│   │   ├── temp/               # 模板组件，如：相同的页面封装成一个组件。\n│   │   ├── UItemp/             # UI组件，如：项目中特定的按钮，消息数字，等等一些样式可以封装成组件的。\n│   ├── common/                 # 共用的资源，如：常用的图片、图标，共用的组件、模块、样式，常量文件等等；\n│   │   ├── compatible/         # 兼容模块，如：适合App和微信各种接口的模块；\n│   │   ├── extension/          # 已有类的扩展模块，如：对 Array 类型进行扩展的模块；\n│   │   ├── libraries/          # 存放自己封装的或者引用的库；\n│   │   ├── tools/              # 自己封装的一些工具\n│   │   ├── constant.js         # 存放js的常量；\n│   │   ├── constant.scss       # 存放scss的常量；\n│   │   └── ...\n│   └── app/                    # 存放项目业务代码；\n│       ├── App.vue             # app 的根组件；\n├── public/                     # 纯静态资源，该目录下的文件不会被webpack处理，该目录会被拷贝到输出目录下；\n├── .babelrc                    # babel 的配置文件\n├── .editorconfig               # 编辑器的配置文件；可配置如缩进、空格、制表类似的参数；\n├── .eslintrc.js                # eslint 的配置文件\n├── .eslintignore               # eslint 的忽略规则\n├── .gitignore                  # git的忽略配置文件\n├── .postcssrc.js               # postcss 的配置文件\n├── CHAGNELOG.md                # 版本更新变更release\n├── index.html                  # HTML模板\n├── package.json                # npm包配置文件，里面定义了项目的npm脚本，依赖包等信息\n└── README.md                   # 项目信息文档\n```\n',2,6,4,1),
(146,'组件使用规范','- 使用时以`v-`开头\n- 命名遵循组件命名规范\n- 推荐使用单标签闭合\n\n``` html\n<v-BaseButton :data=\"data\"/>\n<script>\n export default{\n     components:{\n         \"v-BaseButton\":BaseButton\n     }\n }\n</srcipt\n```\n',2,8,4,1),
(147,'目录命名','**参照项目命名规则，有复数结构时，要采用复数命名法**。例：docs、assets、components、directives、mixins、utils、views。\n- 文件名统一采用小写\n- 特殊缩写名称可大写开头\n\n```\n│   ├── pages/                 \n│   ├── components/\n│   │   ├── UItemp/\n```\n',2,8,4,1),
(148,'命名原则','祖先模块不能出现下划线，除了是全站公用模块，如 `mod_` 系列的命名：\n\n**推荐：**\n```html\n<div class=\"modulename\">\n    <div class=\"modulename_info\">\n        <div class=\"modulename_son\"></div>\n        <div class=\"modulename_son\"></div>\n        ...\n    </div>\n</div>\n\n<!-- 这个是全站公用模块，祖先模块允许直接出现下划线 -->\n<div class=\"mod_info\">\n    <div class=\"mod_info_son\"></div>\n    <div class=\"mod_info_son\"></div>\n    ...        \n</div>\n```\n',2,8,4,1),
(149,'模块命名','全站公共模块：以 `mod_` 开头\n\n```html\n<div class=\"mod_yours\"></div>\n```\n\n业务公共模块：以 `业务名_mod_` 开头\n\n```html\n<div class=\"paipai_mod_yours\"></div>\n```\n',2,8,4,1),
(150,'图片命名','- 图片文件夹一般遵从页面或者模块命名,如：`login/`）\n- 图片不可随意命名，且严禁使用0，1，等数字直接命名图片。\n- 图片命名可遵循：用途+描述，多个单词用下划线隔开，如：`login_icon.png`,`pwd_icon.png`\n- 10k以下图片建议放置`assets/img`下（webpack打包时直接转为base64嵌入）\n- 大图且不常更换的图片放置`public/img`下\n- 可用css编写的样式严禁使用图片\n- 国际化图片，后缀使用简体`-cn`,英文`-en`,繁体`-tw`\n\n```\n│   ├── assets/               \n│   │   ├── img/                          # 图片\n│   │   │    ├── common/                  # 公共图片\n│   │   │    │    ├── default_avatar.png  # 默认头像\n│   │   │    ├── login/                   # 登录模块\n│   │   │    │    ├── login1.png          # 登录模块图片\n│   │   │    │    ├── login_icon-en.png      \n│   │   │    │    ├── login_icon-cn.png     \n│   │   │    │    ├── login_icon-tw.png      \n│   │   │    ├── userInfo/                # 用户中心模块的图片\n```\n',2,8,4,1),
(151,'v-once','- v-once 用于指定元素或者组件只渲染一次\n- 当数据发生变化时，元素或者组件以及其所有的子元素将视为静态内容并且跳过；\n- 该指令可以用于性能优化；\n- 如果是子节点，也是只会渲染一次\n\n```html\n<span v-once>这个将不会改变: {{ msg }}</span>\n```\n',2,7,4,1),
(152,'v-if','用于按条件渲染一个区块。\n`v-if` 是“真实的”按条件渲染，因为它确保了条件区块内的事件监听器和子组件都会在切换时被销毁与重建。\n\n`v-if` 也是**懒加载**的：如果在初次渲染时条件值为 false，则不会做任何事。条件区块会直到条件首次变为 true 时才渲染。\n\n```html\n<h1 v-if=\"awesome\">Vue is awesome!</h1>\n```\n',2,7,4,1),
(153,'v-else','v-else 元素必须跟在一个 v-if 或者 v-else-if 元素后面，否则将不会识别它\n```html\n<h1 v-else>Oh no </h1>\n```\n',2,7,4,1),
(154,'template上的v-if','`v-if` 是一个指令，他必须依附某个元素。但想要切换不只一个元素呢？在这种情况下我们可以在一个 `<template>` 元素上使用 `v-if`，这只是一个不可见的包裹元素，最后渲染的结果并不会包含这个 `<template>` 元素。\n```html\n<template v-if=\"ok\">\n  <h1>Title</h1>\n  <p>Paragraph 1</p>\n  <p>Paragraph 2</p>\n</template>\n```\n`v-else` 和 `v-else-if` 也可以在 `<template>` 上使用。',2,7,4,1),
(155,'v-show','按条件显示一个元素\n\n`v-show` 会在 DOM 渲染中保留该元素；`v-show` 仅切换了该元素上名为 `display` 的CSS 属性。\n`v-show` 不支持在 `<template>` 元素上使用，也没有 `v-else` 来配合。\n\n```html\n<h1 v-show=\"ok\">Hello!</h1>\n```\n',2,7,4,1),
(156,'v-for','v-for的基本格式是 \"item in 数组\nv-for也支持遍历对象，并且支持有一二三个参数\n\n一个参数： \"value in object\";\n二个参数： \"(value, key) in object\";\n三个参数： \"(value, key, index) in object\";\n\nv-for同时也支持数字的遍历',2,2,4,1),
(157,'定义数组渲染列表(v-for)','```js\nconst items = ref([{ message: ''Foo'' }, { message: ''Bar'' }])\n```\n```html\n<li v-for=\"item in items\" :key=\"item.message\">\n    {{ item.message }}\n</li>\n```\n',2,7,4,1),
(158,'可选参数(v-for)','在 `v-for` 块中可以完整地访问父作用域内的属性。`v-for` 也支持使用可选的第二个参数，表示当前项的位置索引。\n\n```js\nconst parentMessage = ref(''Parent'')\nconst items = ref([{ message: ''Foo'' }, { message: ''Bar'' }])\n```\n\n```html\n<li v-for=\"(item, index) in items\">\n    {{ parentMessage }} - {{ index }} - {{ item.message }}\n</li>\n```\n\n',2,7,4,1),
(159,'变量别名解构(v-for)','```html\n<li v-for=\"{ message } in items\">\n  {{ message }}\n</li>\n\n<!-- 有 index 索引时 -->\n<li v-for=\"({ message }, index) in items\">\n  {{ message }} {{ index }}\n</li>\n```\n\n',2,7,4,1),
(160,'用of替代in分隔符(v-for)','```html\n<div v-for=\"item of items\"></div>\n```\n',2,7,4,1),
(161,'对象(v-for)','遍历一个对象的所有属性。\n```vue\n  <li v-for=\"value in myObject\">\n    {{ value }}\n  </li>\n```\n```js\nconst myObject = reactive({\n  title: ''如何在 Vue 中渲染列表'',\n  author: ''王小明'',\n})\n```\n提供第二个的参数为 property 名称 (键名)\n```html\n<li v-for=\"(value, key) in myObject\">\n  {{ key }}: {{ value }}\n</li>\n```\n用第三个参数作为索引\n```html\n<li v-for=\"(value, key, index) in myObject\">\n  {{ index }}. {{ key }}: {{ value }}\n</li>\n```\n唯一key\n```html\n<div v-for=\"item in items\" v-bind:key=\"item.id\">\n  <!-- 内容 -->\n</div>\n```\n\n',2,7,4,1),
(162,'使用值范围(v-for)','`v-for` 也可以接受整数。在这种情况下，它会把模板重复对应次数。\n```html\n<div>\n  <span v-for=\"n in 10\">{{ n }} </span>\n</div>\n```\n 在template使用\n```html\n  <template v-for=\"item in items\">\n    <li>{{ item.msg }}</li>\n    <li class=\"divider\" role=\"presentation\"></li>\n  </template>\n```\n',2,7,4,1),
(163,'组件上使用(v-for)','```html\n<my-component v-for=\"item in items\" :key=\"item.id\">\n</my-component>\n```\n\n但是，这不会自动将任何数据传递给组件，因为组件有自己独立的作用域。为了将迭代后的数据传递到组件中，我们还是应该使用 props：\n\n```html\n<my-component\n  v-for=\"(item, index) in items\"\n  :item=\"item\"\n  :index=\"index\"\n  :key=\"item.id\"\n></my-component>\n```\n\n',2,7,4,1),
(164,'for&if同时使用','当它们处于同一节点，`v-if` 的优先级比 `v-for` 更高，这意味着 `v-if` 将没有权限访问 `v-for` 里的变量，这时，可以把 v-for 移到 <template> 中\n\n```html\n//1\n<template v-for=\"(item, index) in ResultList\" :key=\"index\">\n   <a @click=\"onk(item.path)\" v-if=\"item.identity\">\n    {{item.title}}\n   </a>\n</template>\n//2\n<template v-for=\"todo in todos\" :key=\"todo.name\">\n  <li v-if=\"!todo.isComplete\">\n    {{ todo.name }}\n  </li>\n</template>\n```\n',2,7,4,1),
(165,'v-text','都是用于将数据显示在界面中，但是通常只接受一个string类型\n用于更新元素的 textContent\n```html\n<div v-text=\"message\"></div>\n<div v-text=\"msg\"></div> 等价于 <div>{{msg}}</div>\n```\n',2,7,4,1),
(166,'v-pre','- v-pre用于跳过元素和它的子元素的编译过程，显示原始的Mustache标签\n- 跳过不需要编译的节点，加快编译的速度\n```html\n<h2 v-pre>{{massage}}</h2> <!-- {{massage}} -->\n```\n\n',2,7,4,1),
(167,'v-cloak','保持在元素上直到关联组件实例结束编译； 和 CSS 规则如 [v-cloak] { display: none } 一起用时，这个指令可以隐藏未编译的 Mustache 标签直到组件实 例准备完毕。',2,7,4,1),
(168,'自定义指令','注册一个全局指令 v-focus, 指令的功能在页面加载时，元素获得焦点：\n\n```html\n<div id=\"app\">\n    <p>页面载入时，input 元素自动获取焦点：</p>\n    <input v-focus>\n</div>\n\n<script>\nconst app = Vue.createApp({})\n// 注册一个全局自定义指令 `v-focus`\napp.directive(''focus'', {\n  // 当被绑定的元素挂载到 DOM 中时……\n  mounted(el) {\n    // 聚焦元素\n    el.focus()\n  }\n})\napp.mount(''#app'')\n</script>\n```\n\n我们也可以在实例使用 directives 选项来注册局部指令，这样指令只能在这个实例中使用：\n\n```html\n<div id=\"app\">\n    <p>页面载入时，input 元素自动获取焦点：</p>\n    <input v-focus>\n</div>\n<script>\nconst app = {\n   data() {return {}},\n   directives: {\n      focus: {\n         // 指令的定义\n         mounted(el) {\n            el.focus()\n         }\n      }\n   }\n}\nVue.createApp(app).mount(''#app'')\n```',2,7,4,1),
(169,'循环指定次数(v-for)','```html\n<a-button type=\"primary\" shape=\"round\" @click=\"GetMonth(index)\" \n          v-for=\"index of 12\" :key=\"index\">{{ index }}月\n</a-button>\n```\n\n\n\n',2,7,4,1),
(173,'ces','ces',1,1,4,1),
(174,'获取本日开始时间','```csharp\n/// <summary>\n/// 获取本日开始时间（0点0分0秒）\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetDayStart(this DateTime dateTime)\n{\n  return dateTime.Date;\n}\n```\n\n\n\n',1,3,4,1),
(175,'获取本日结束时间','```csharp\n/// <summary>\n/// 获取本日结束时间（23点59分59秒）\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetDayEnd(this DateTime dateTime)\n{\n  return dateTime.Date.AddDays(1).AddMilliseconds(-1);\n}\n```\n\n\n\n',1,3,4,1),
(176,'获取本周开始时间','```csharp\n/// <summary>\n/// 获取本周开始时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetWeekStart(this DateTime dateTime)\n{\n  return dateTime.AddDays(-(int) dateTime.DayOfWeek + 1).GetDayStart();\n}    \n```\n\n\n\n',1,3,4,1),
(177,'获取本周结束时间','```csharp\n/// <summary>\n/// 获取本周结束时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetWeekEnd(this DateTime dateTime)\n{\n  return dateTime.AddDays(7 - (int) dateTime.DayOfWeek).GetDayEnd();\n}\n```\n\n\n\n',1,3,4,1),
(178,'获取本月开始时间','```csharp\n/// <summary>\n/// 获取本月开始时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetMonthStart(this DateTime dateTime)\n{\n  return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0);\n}\n\n```\n\n\n\n',1,3,4,1),
(179,'获取本月结束时间','```csharp\n /// <summary>\n /// 获取本月结束时间\n /// </summary>\n /// <param name=\"dateTime\"></param>\n /// <returns></returns>\n public static DateTime GetMonthEnd(this DateTime dateTime)\n {\n   return GetMonthStart(dateTime).AddMonths(1).AddMilliseconds(-1);\n }\n```\n\n\n\n',1,3,4,1),
(180,'获取本季度开始时间','```csharp\n/// <summary>\n/// 获取本季度开始时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetSeasonStart(this DateTime dateTime)\n{\n  var time = dateTime.AddMonths(0 - ((dateTime.Month - 1) % 3));\n  return DateTime.Parse(time.AddDays(-time.Day + 1).ToString(\"yyyy/MM/dd 00:00:00\"));\n}\n```\n\n\n\n',1,3,4,1),
(181,'获取本季度结束时间','```csharp\n/// <summary>\n/// 获取本季度结束时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetSeasonEnd(this DateTime dateTime)\n{\n  var time = dateTime.AddMonths((3 - ((dateTime.Month - 1) % 3) - 1));\n  return DateTime.Parse(time.AddMonths(1).AddDays(-time.AddMonths(1).Day + 1).AddDays(-1).ToString(\"yyyy/MM/dd 23:59:59\"));\n}\n```\n\n\n\n',1,3,4,1),
(182,'获取本年开始时间','```csharp\n/// <summary>\n/// 获取本年开始时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetYearStart(this DateTime dateTime)\n{\n  return DateTime.Parse(dateTime.AddDays(-dateTime.DayOfYear + 1).ToString(\"yyyy/MM/dd 00:00:00\"));\n}\n```\n\n\n\n',1,3,4,1),
(183,'获取本年结束时间','```csharp\n/// <summary>\n/// 获取本年结束时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime GetYearEnd(this DateTime dateTime)\n{\n  var time2 = dateTime.AddYears(1);\n  return DateTime.Parse(time2.AddDays(-time2.DayOfYear).ToString(\"yyyy/MM/dd 23:59:59\"));\n}\n```\n\n\n\n',1,3,4,1),
(184,'北京时间转换成unix时间戳','```csharp\n/// <summary>\n/// 北京时间转换成unix时间戳(10位/秒)\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static long BeijingTimeToUnixTimeStamp10(this DateTime dateTime)\n{\n  return(long)(dateTime - new DateTime(1970, 1, 1, 8, 0, 0)).TotalSeconds;\n}\n```\n\n\n\n',1,3,4,1),
(186,'北京时间转换成unix时间戳(13位/毫秒)','```csharp\n/// <summary>\n/// 北京时间转换成unix时间戳(13位/毫秒)\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static long BeijingTimeToUnixTimeStamp13(this DateTime dateTime)\n{\n  return(long)(dateTime - new DateTime(1970, 1, 1, 8, 0, 0)).TotalMilliseconds;\n}\n```\n\n\n\n',1,3,4,1),
(188,'10位unix时间戳转换成北京时间','```csharp\n/// <summary>\n/// 10位unix时间戳转换成北京时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime UnixTimeStamp10ToBeijingTime(this long unixTimeStamp)\n{\n  return new DateTime(1970, 1, 1, 8, 0, 0).AddSeconds(unixTimeStamp);\n}\n```\n\n\n\n',1,3,4,1),
(190,'13位unix时间戳转换成北京时间','```csharp\n/// <summary>\n/// 13位unix时间戳转换成北京时间\n/// </summary>\n/// <param name=\"dateTime\"></param>\n/// <returns></returns>\npublic static DateTime UnixTimeStamp13ToBeijingTime(this long unixTimeStamp)\n{\n  return new DateTime(1970, 1, 1, 8, 0, 0).AddMilliseconds(unixTimeStamp);\n}\n```\n\n\n\n',1,3,4,1),
(192,'当前日期所在月份第一个指定星期几的日期','```csharp\n/// <summary>\n/// 当前日期所在月份第一个指定星期几的日期\n/// </summary>\n/// <param name=\"date\">给定日期</param>\n/// <param name=\"dayOfWeek\">星期几</param>\n/// <returns>所对应的日期</returns>\npublic static DateTime GetFirstWeekDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)\n{\n  var dt = date.GetMonthStart();\n  while(dt.DayOfWeek != dayOfWeek) dt = dt.AddDays(1);\n  return dt;\n}\n```\n\n\n\n',1,3,4,1),
(193,'当前日期所在月份最后1个指定星期几的日期','```csharp\n/// <summary>\n/// 当前日期所在月份最后1个指定星期几的日期\n/// </summary>\n/// <param name=\"date\">给定日期</param>\n/// <param name=\"dayOfWeek\">星期几</param>\n/// <returns>所对应的日期</returns>\npublic static DateTime GetLastWeekDayOfMonth(this DateTime date, DayOfWeek dayOfWeek)\n{\n  var dt = date.GetMonthEnd();\n  while(dt.DayOfWeek != dayOfWeek) dt = dt.AddDays(-1);\n  return dt;\n}\n```\n\n\n\n',1,3,4,1),
(196,'给定日期所在月份共有多少天','```csharp\n/// <summary>\n/// 给定日期所在月份共有多少天\n/// </summary>\n/// <param name=\"date\"></param>\n/// <returns></returns>\npublic static int GetCountDaysOfMonth(this DateTime date)\n{\n  return date.GetMonthEnd().Day;\n}\n```\n\n\n\n',1,3,4,1),
(197,'当前日期与给定日期是否是同一天','```csharp\n/// <summary>\n/// 当前日期与给定日期是否是同一天\n/// </summary>\n/// <param name=\"date\">当前日期</param>\n/// <param name=\"dateToCompare\">给定日期</param>\n/// <returns></returns>\npublic static bool IsDateEqual(this DateTime date, DateTime dateToCompare)\n{\n  return date.Date == dateToCompare.Date;\n}\n```\n\n\n\n',1,3,4,1),
(198,'是否是周未','```csharp\n/// <summary>\n/// 是否是周未\n/// </summary>\n/// <param name=\"date\"></param>\n/// <returns></returns>\npublic static bool IsWeekend(this DateTime date)\n{\n  return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;\n}\n```\n\n\n\n',1,3,4,1),
(199,'是否是工作日','```csharp\n/// <summary>\n/// 是否是工作日\n/// </summary>\n/// <param name=\"date\"></param>\n/// <returns></returns>\npublic static bool IsWeekDay(this DateTime date)\n{\n  return !date.IsWeekend();\n}\n```\n\n\n\n',1,3,4,1),
(200,'判断是否为今天','```csharp\n/// <summary>\n/// 判断是否为今天\n/// </summary>\n/// <param name=\"date\"></param>\n/// <returns></returns>\npublic static bool IsToday(this DateTime date)\n{\n  return date.Date == DateTime.Now.Date;\n}\n```\n\n\n\n',1,3,4,1),
(201,'ShellExecute使用','### ShellExecute\n\n若ShellExecute函数调用成功，则返回值为被执行程序的实例句柄。若返回值小于32，则表示出现错误。\n\n```csharp\n  /// <summary>\n        /// ShellExecute\n        /// </summary>\n        /// <param name=\"hwnd\">指定父窗口句柄:ntPtr.Zero</param>\n        /// <param name=\"lpszOp\">指定要进行的操作:Open</param>\n        /// <param name=\"lpszFile\">指定要打开的文件名|路径</param>\n        /// <param name=\"lpszParams\">指定命令行参数: 0 | \"\"</param>\n        /// <param name=\"lpszDir\">用于指定默认目录:0 | \"\"</param>\n        /// <param name=\"FsShowCmd\">显示模式: 0:隐藏 1~11</param>\n        /// <returns></returns>\n        [DllImport(\"shell32.dll\")]\n        private static extern int ShellExecute(IntPtr hwnd, StringBuilder lpszOp, StringBuilder lpszFile, StringBuilder lpszParams, StringBuilder lpszDir, int FsShowCmd);\n\n\n        /// <summary>\n        /// 打开程序/文件夹\n        /// </summary>\n        /// <param name=\"path\">路径</param>\n        /// <param name=\"FsShow\">显示模式 默认1</param>\n        public static void OpenFile(string path, int FsShow = 1)\n        {\n            ShellExecute(IntPtr.Zero, new StringBuilder(\"Open\"), new StringBuilder(@path), new StringBuilder(\"\"), new StringBuilder(\"\"), FsShow);\n        }\n```\n\n',1,3,4,1),
(202,'WindiCSS','## WindiCSS\n\n[指令 | Windi CSS](https://cn.windicss.org/features/directives.html)\n\n**Windi CSS** 是下一代工具优先的 CSS 框架。\n\n如果你已经熟悉了 [Tailwind CSS](https://tailwindcss.com/docs)，可以把 Windi CSS 看作是**按需供应的** Tailwind 替代方案，它为你提供了更快的加载体验，**完美兼容 Tailwind v2.0**，并且拥有很多额外的酷炫功能',14,2,4,1),
(203,'自动值推导',' Windi CSS 提供自动值推导功能，可以在类名中使用任意值，然后生成相应的样式，用起来比使用标准的 sass 要方便很多。例如：\n\n- `w-192px` 可以自动解析为 `width: 192px;`\n- `w-[25%]` 可以自动解析为 `width: 25%;` ，也相当于 `width: 48px;`\n- `h-12` 可以自动解析为 `height: 3rem;` ，根据当前的根元素的字体大小也相当于 `height: 48px;',14,3,4,1),
(204,'Important前缀','### Important前缀\n\n使用 important 也是非常方便，只需在任意工具类的前面使用 `!` 前缀，使它们变为 `!important`，就可以覆盖之前的样式规则中指定的属性了。\n\n如 `w-192px` 添加 `!` 前缀为 `!w-192px',14,3,4,1),
(205,'Shortcuts','使用相同的工具类合集时，出现很多重复性代码， Shortcuts 特性可以工具类的名字组合在一起定义成一个新的名字，在任何地方使用，就免去了大量重复冗余的代码。\n\n配置文件 `windi.config.ts` 中添加 `shortcuts` 字段：\n\n```typescript\nexport default {\n  theme: {\n    /* ... */\n  },\n  shortcuts: {\n    ''box view'': ''w-[25%] h-12 rounded-md m-4px'',\n  },\n}\n```\n\n就可以把页面代码修改为下面这样，页面展示效果是一样的，但是代码更加简洁明了。\n\n```html\n<div class=\"bg-blue-400 box-view\"></div>\n```',14,3,4,1),
(206,'响应式设计','Windi CSS 中轻松实现响应式设计。将可变修饰（variant）的前缀加到对应的工具类前，比如 `md:`、`lg:`。\n\n> |      | 默认                | 带有 `<` 前缀         | 带有 `@` 前缀                                 |\n> | ---- | ------------------- | --------------------- | --------------------------------------------- |\n> | sm   | (min-width: 640px)  | (max-width: 639.9px)  | (min-width: 640px) and (max-width: 767.9px)   |\n> | md   | (min-width: 768px)  | (max-width: 767.9px)  | (min-width: 768px) and (max-width: 1023.9px)  |\n> | lg   | (min-width: 1024px) | (max-width: 1023.9px) | (min-width: 1024px) and (max-width: 1279.9px) |\n> | xl   | (min-width: 1280px) | (max-width: 1279.9px) | (min-width: 1280px) and (max-width: 1535.9px) |\n> | 2xl  | (min-width: 1536px) | (max-width: 1535.9px) | (min-width: 1536px)                           |\n\n```html\n<div class=\"lg:bg-red-400 md:bg-red-200 sm:bg-red-100 box-view\"></div>\n\n<div class=\"phone:(bg-red-400 text-light-100 p-10) iPad:(p-50 bg-blue-700) lg:(p-100 bg-cyan-800 text-50px)\">\n    响应式\n</div>\n\n <div\n    w:phone=\"bg-red-400 text-light-100 p-10\"\n    w:iPad=\"bg-blue-700 p-50\"\n    w:lg=\"p-100 bg-cyan-800 text-50px\"\n>\n    响应式\n</div>\n\n@screen <xp {\n}\n```\n\n根据业务需求来自定义断点，在 `windi.config.ts` 中配置\n\n```tsx\nimport { defineConfig } from ''windicss/helpers''\n\nexport default defineConfig({\n  theme: {\n    screens: {\n      tablet: ''640px'',\n      laptop: ''1024px'',\n      desktop: ''1280px'',\n    },\n  },\n})\n\n```',14,3,4,1),
(207,'@apply','将 `@apply` 在 style 块中同一行的、一些已存在的工具类上使用，和 Shortcuts 效果差不多，适合抽取成一个通用工具类。\n\n```html\n<style lang=\"scss\" scoped>\n...\n\n.box-view2 {\n  @apply w-[25%] h-12 rounded-md m-4px;\n}\n</style>\n```\n',14,3,4,1),
(208,'@variants','通过把 css 工具类定义包装在 `@variants` 中，用来生成带有一些屏幕可变修饰，状态可变修饰，主题可变修饰的工具类。\n\n```html\n<template>\n    <h3>使用 Windi CSS</h3>\n    <div class=\"flex items-center justify-around bg-teal-100 rounded-md p-4px w-192px\">\n      <div class=\"bg-red-400 box-view2\"></div>\n      <div class=\"bg-green-400 box-view2\"></div>\n      <div class=\"bg-blue-400 box-view2\"></div>\n    </div>\n    <br>\n</template>\n\n<style lang=\"scss\" scoped>\n\n.box-view2 {\n  @apply w-[25%] h-12 rounded-md m-4px;\n}\n\n@variants focus, hover {\n  .box-view2 {\n    @apply bg-red-200;\n  }\n}\n</style>\n```\n',14,3,4,1),
(209,'@screen','`@screen` 媒体查询，通过名称来引用断点，以此来取代通过复制你 CSS 里面的值来实现。\n\n```html\n<style lang=\"scss\" scoped>\n...\n\n.box-view2 {\n  @apply w-[25%] h-12 rounded-md m-4px;\n}\n\n// lg 就是上文响应式设计中的断点 (min-width: 1024px)\n@screen lg {\n  .box-view2 {\n    @apply bg-red-200;\n  }\n}\n\n// 上下效果一样\n@media (min-width: 1024px) {\n  .box-view2{\n    // background-color: rgba(254, 202, 202) = bg-red-200\n    background-color: rgba(254, 202, 202)\n  }\n}\n</style>\n```\n',14,3,4,1),
(210,'@layer','`@layer` 指令用来确认每个 class 的排序。合法的层级为 `基础 (base)`, `组件 (components)` 和 `工具类 (utilities)`。',14,3,4,1),
(211,'theme()','`theme()` 函数可以让我们通过 `.` 运算符来获取想要设置的值。\n\n```html\n<template> \n    <h3>使用 Windi CSS</h3>\n    <div class=\"flex items-center justify-around bg-teal-100 rounded-md p-4px w-192px\">\n      <div class=\"bg-red-400 box-view2 light-red\"></div>\n      <div class=\"bg-green-400 box-view2\"></div>\n      <div class=\"bg-blue-400 box-view2\"></div>\n    </div>\n</template>\n\n<style lang=\"scss\" scoped>\n.box-view2 {\n  @apply w-[25%] h-12 rounded-md m-4px;\n}\n\n.light-red {\n  background-color: theme(\"colors.red.200\");\n}\n</style>\n```\n',14,3,4,1),
(212,'属性化模式','属性化在 Windi CSS 中默认下是可选的，在 `windi.config.ts` 配置中开启，并根据需求使用。\n\n```typescript\nimport { defineConfig } from ''windicss/helpers''\n\nexport default defineConfig({\n  attributify: true,\n})\n```\n\n把上面的代码改为下面的方式，效果和上面讲到的响应式设计是一样的，这样写可以让我们的目录更加清晰。\n\n```html\n<h3>使用 Windi CSS</h3>\n<div class=\"flex items-center justify-around bg-teal-100 rounded-md p-4px w-192px\">\n    <div\n        lg=\"bg-red-400 box-view\"\n        md=\"bg-red-200 box-view\"\n        sm=\"bg-red-100 box-view\"\n      ></div>\n    <div class=\"bg-green-400 box-view\"></div>\n    <div class=\"bg-blue-400 box-view\"></div>\n</div>\n```\n\n如果担心命名冲突，可以在 `windi.config.ts` 配置中通过属性化方式添加自定义前缀：\n\n```html\nimport { defineConfig } from ''windicss/helpers''\n\nexport default defineConfig({\n  attributify: {\n    prefix: ''w:'',\n  },\n})\n<h3>使用 Windi CSS</h3>\n<div class=\"flex items-center justify-around bg-teal-100 rounded-md p-4px w-192px\">\n    <div\n        w:lg=\"bg-red-400 box-view\"\n        w:md=\"bg-red-200 box-view\"\n        w:sm=\"bg-red-100 box-view\"\n      ></div>\n    <div class=\"bg-green-400 box-view\"></div>\n    <div class=\"bg-blue-400 box-view\"></div>\n</div>\n```\n\n',14,3,4,1),
(213,'Plugin插件','[Windi CSS](https://link.juejin.cn?target=https%3A%2F%2Fcn.windicss.org%2Fplugins%2Finterfaces.html) 官方以及社区提供了很多 plugin 插件使用，让我们操作 css 更加方便和高效，而且我们也可以通过 Windi CSS 的接口，来开发自己的插件。\n\n插件同样需要在 Windi CSS 的配置文件 `windi.config.ts` 中引入即可。\n\n```typescript\nexport default {\n  theme: {\n    // ...\n  },\n  // 引入插件\n  plugins: [\n    require(''windicss/plugin/typography''),\n    // ...\n  ],\n}\n```\n',14,1,4,1),
(214,'选择器选择','- 尽量少用通用选择器\n- 不使用 ID 选择器\n- 不使用无具体语义定义的标签选择器\n\n```css\n/* 推荐Class */\n.jdc {}\n.jdc li {}\n.jdc li p{}\n```\n',8,6,4,1),
(215,'属性值引号','css属性值用到引号时，统一用单引号\n\n```css\n/* 推荐 */\n.jdc { \n    font-family: ''Hiragino Sans GB'';\n}\n```\n\n',8,6,4,1),
(216,'属性书写顺序','建议遵循以下顺序：\n\n1. 布局定位属性：display / position / float / clear / visibility / overflow\n2. 自身属性：width / height / margin / padding / border / background\n3. 文本属性：color / font / text-decoration / text-align / vertical-align / white- space / break-word\n4. 其他属性（CSS3）：content / cursor / border-radius / box-shadow / text-shadow / background:linear-gradient …',8,6,4,1),
(217,'CSS注释','### CSS注释\n\n```css\n/*这是个注释*/\np\n{\n    text-align:center;\n    /*这是另一个注释*/\n    color:black;\n    font-family:arial;\n}\n```',8,6,4,1),
(218,'文件信息注释','在样式文件编码声明 `@charset` 语句下面注明页面名称、作者、创建日期等信息\n\n```css\n@charset \"UTF-8\";\n/**\n * @desc File Info\n * @author Author Name\n * @date 2015-10-10\n */\n```\n\n',8,6,4,1),
(219,'常用命名','| **CSS类名**           | **说明**               |\n| --------------------- | ---------------------- |\n| **布局**              |                        |\n| layout                | 布局容器               |\n| wrapper/wrap          | 控制布局宽度的外围容器 |\n| header/head/hd        | 头部/顶部              |\n| main/bd               | 主体部分               |\n| footer/foot/ft        | 底部                   |\n| sidebar               | 侧边栏                 |\n| **容器**              |                        |\n| banner                | 广告栏                 |\n| content               | 内容部分               |\n| copyright             | 版权                   |\n| list                  | 列表                   |\n| menu/submenu          | 菜单/二级菜单          |\n| nav/subnav            | 导航栏/二级导航        |\n| **组件/细节**         |                        |\n| arrow                 | 箭头                   |\n| btn                   | 按钮                   |\n| download              | 下载                   |\n| logo                  | 徽标                   |\n| message/msg           | 信息                   |\n| news                  | 新闻                   |\n| product               | 产品                   |\n| search                | 搜索                   |\n| status                | 状态                   |\n| summary               | 摘要                   |\n| tab                   | 标签页                 |\n| tag                   | 标签                   |\n| text/txt              | 文本                   |\n| tip                   | 提示                   |\n| title/subtitle        | 标题/二级标题          |\n| **尺寸**              |                        |\n| large                 | 大                     |\n| middle                | 中等                   |\n| small                 | 小                     |\n| mini                  | 迷你                   |\n| **位置**              |                        |\n| top/right/bottom/left | 上/右/下/左            |\n| **关系**              |                        |\n| first                 | 第一个                 |\n| last                  | 最后一个               |\n| prev                  | 上一个                 |\n| current               | 当前项                 |\n| next                  | 下一个                 |\n| forward               | 向前                   |\n| back                  | 向后                   |\n| **状态**              |                        |\n| primary               | 主要                   |\n| info                  | 提示信息               |\n| success               | 成功                   |\n| warning               | 一般警告               |\n| danger/error          | 严重警告/错误警告      |\n| link                  | 文字链接               |\n| plain/ghost           | 按钮是否镂空           |\n| light                 | 亮模式                 |\n| dark                  | 暗模式                 |\n| disabled              | 禁用                   |\n| active                | 激活                   |\n| checked               | 选中                   |\n| loading               | 加载中                 |',8,6,4,1),
(220,'选择器使用情况','| 选择器         | 作用                   | 用法及隔开代码                      | 使用情况 |\n| -------------- | ---------------------- | ----------------------------------- | -------- |\n| 后代选择器     | 用来选择后代元素       | 符号是空格 .nav a                   | 较多     |\n| 子代选择器     | 选择最近一级元素       | 符号是大于 .nav>p                   | 较少     |\n| 并集选择器     | 选择某些相同样式的元素 | 符号是逗号 .nav，header             | 较多     |\n| 链接伪类选择器 | 选择不同状态的链接     | 重点记住a{}和a：hover实际开发的写法 | 较多     |\n| ：focus        | 选择获得光标的表单     | input：focus记住这个写法            | 较少     |',8,2,4,1),
(221,'CSS的三种样式表','按照CSS样式书写的位置（或者引入方式）\n\n- 行业样式表（行内式）\n- 内部样式表（嵌入式）\n- 外部样式表（链接式）',8,6,4,1),
(222,'行内样式表','行内样式表是在元素标签内部的style属性中设置CSS样式，适合于修改简单样式。\n\n```css\n<div style=\"color:red; font-size:12px;\">青春不常在，抓紧谈恋爱</div>\n```\n\n- style其实就是标签的属性\n- 在双引号中间，写法要符合CSS规范。\n- 可以控制当前的标签设置样式。\n- 由于书写繁琐，并且没有体现出结构与样式相分离的思想，所以不推荐大量使用，只有对当前元素添加简单样式的时候，可以考虑使用。\n- 使用行内样式表设定CSS，通常也被成为行内式引入。',8,6,4,1),
(223,'外部样式表','实际开发都是外部样式表，适合于样式比较多的情况，核心是：样式单独写到CSS文件中，之后把CSS文件引入到HTML页面中使用。\n\n引入外部样式表分为两步：\n\n- 新建一个后缀名为CSS的样式文件，把所有CSS代码都放入此文件中。\n- 在HTML页面中，使用<link>标签引入这个文件。\n\n```css\nlink rel=\"style sheet\" href\"CSS文件路径\"\n```\n\n',8,2,4,1),
(224,'CSS引入方式总结','| 样式表     | 优点                     | 缺点         | 使用情况   | 控制范围     |\n| ---------- | ------------------------ | ------------ | ---------- | ------------ |\n| 行内样式表 | 书写方便；权重高         | 结构样式混写 | 较少       | 控制一个标签 |\n| 内部样式表 | 部分结构和样式相分离     | 没有彻底分离 | 较多       | 控制一个页面 |\n| 外部样式表 | 完全实现结构和样式相分离 | 需要引入     | 最多(推荐) | 控制多个页面 |',8,2,4,1),
(225,'变量','### 变量\n\n变量用来存储需要在CSS中复用的信息，如颜色和字体,通过$符号去声明一个变量。\n可复用属性尽量抽离为页面变量，易于统一维护\n\n```css\n$color: red;\n.jdc {\n    color: $color;\n    border-color: $color;\n}\n```\n转换全局变量可添加 `!global` 声明：\n```css\n $width: 5em !global;\n```',10,2,4,1),
(226,'混合(mixin)','根据功能定义模块，然后在需要使用的地方通过 `@include` 调用，避免编码时重复输入代码段\n\n```css\n@mixin icon($x:0, $y:0) {\n    background: url(/img/icon.png) no-repeat $x, $y;\n}\n.test {\n   @include icon(-10px, 0);\n}\n```\n查看: [混合(Mixins)](http://ref.ecdata.cn/docs/sass.html#sass-混合mixins)',10,2,4,1),
(227,'占位选择器(%) ','以 `%` 标识定义，通过 `@extend` 调用\n\n```css\n%borderbox {\n    box-sizing: border-box;\n}\n.jdc {\n    @extend %borderbox;\n}\n```\n',10,6,4,1),
(228,'继承(extend)','通过@extend指令在选择器之间复用CSS属性，并且不会产生冗余的代码\n```css\n.jdc_1 {\n    color: red;\n}\n.jdc_2 {\n    @extend .jdc_1;\n}\n```\n\n',10,2,4,1),
(229,'循环(for)','```\n结束值不执行：\n@for 变量 from 开始值 through 结束值 {}\n结束值也执行：\n@for 变量 from 开始值 to 结束值 {}\n```\n\n注意：`#{}` 是连接符，变量连接使用时需要依赖\n\n```scss\n@for $i from 1 through 3 {\n    .jdc_#{$i} {\n        background-position: 0 (-20px) * $i;\n    }\n}\n```',10,2,4,1),
(230,'循环(each)','```scss\n@each $name in list, detail {\n    .jdc_#{$name} {\n        ...\n    }\n}\n@each $name, $color in (list, red), (detail, blue) {\n    .jdc_#{$name} {\n      ...\n    }\n}\n```',10,3,4,1),
(231,'函数(function)','```css\n@function pxToRem($px) {\n    @return $px / 10px * 1rem;\n}\n.jdc {\n    font-size: pxToRem(12px);\n}\n```\n',10,3,4,1),
(232,'运算规范','### 运算规范\n\n运算符之间空出一个空格\n\n```scss\n.jdc {\n    width: 100px - 50px;\n    height: 30px / 5;\n}\n```\n\n单位同时参与运算，所以 10px 不等于 10，乘除运算时需要特别注意\n\n```scss\n// 正确的运算格式\n.jdc {\n    width: 100px - 50px;\n    width: 100px + 50px;\n    width: 100px * 2;\n    width: 100px / 2;\n}\n```\n\n',10,6,4,1),
(233,'居中(flex)','使用 Flexbox 实现子元素的居中效果\n```css\n@mixin center-children {\n    display: flex;\n    justify-content: center;\n    align-items: center;\n}\n```\n\n```css\n.parent {\n    @include center-children;\n}\n```\n\n',10,3,4,1),
(235,'插值(#{})','通过 `#{}` 语句在选择器或属性名中使用变量：\n```css\n$name: foo;\n$attr: border;\np.#{$name} {\n  #{$attr}-color: blue;\n}\n```\n\n编译为\n\n```css\np.foo {\n  border-color: blue; }\n```\n\n',10,3,4,1),
(236,'嵌套(Nesting)','```css\n.container{\n  .left-area{\n    ...\n  }\n}\n```',10,2,4,1),
(237,'父选择器(&)','& 表示自身的意思。\n```css\n.container{\n  &.right-area{\n   background-color : #0000\n }\n}\n```\n\n',10,3,4,1),
(238,'Mixin(混合)','### Mixin(混合)\n\n用来分组那些需要在页面中复用的CSS声明，开发人员可以通过向Mixin传递变量参数来让代码更加灵活，该特性在添加浏览器兼容性前缀的时候非常有用，SASS目前使用@mixin name指令来进行混合操作。\n\n```scss\n@mixin 名字（参数1，参数2，...）\n{\n........样式.......\n}\n\ndiv{\n    @include 名字;  \n}\n```\n\n',10,3,4,1),
(242,'循环(while)','### while循环\n\n``` css\n$gao: 1;\n@while $gao<4 {\n    .div#{$gao}{\n        height: $gao*10px;\n    }\n   $gao : $gao+1;\n}\n```\n\n\n\n',10,3,4,1),
(243,'v-model','在处理表单时，常常需要将表单输入框的内容同步给 JavaScript 中相应的变量。v-model 在表单 `<input>`、`<textarea>` 及 `<select>` 元素上创建双向数据绑定。它会根据控件类型自动选取正确的方法来更新元素。\n#### 基本使用\n```html\n<input v-model=\"text\">\n```\n#### 多行文本\n```html\n<textarea v-model=\"message\"></textarea>\n```\n',2,7,4,1),
(244,'复选框(v-model)','单一的复选框，绑定的是布尔类型值：\n\n```html\n<input type=\"checkbox\" id=\"checkbox\" v-model=\"checked\" />\n<label for=\"checkbox\">{{ checked }}</label>\n```\n',2,7,4,1),
(245,'多个复选框(v-model)','绑定同一个数组：\n```js\nconst checkedNames = ref([])\n```\n```html\n<div>选择的名字有：{{ checkedNames }}</div>\n<input type=\"checkbox\" id=\"jack\" value=\"Jack\" v-model=\"checkedNames\">\n<label for=\"jack\">Jack</label>\n\n<input type=\"checkbox\" id=\"john\" value=\"John\" v-model=\"checkedNames\">\n<label for=\"john\">John</label>\n```\n',2,7,4,1),
(246,'选择框(v-model)','单选时：\n```html\n<div>选择的是：{{ selected }}</div>\n<select v-model=\"selected\">\n  <option disabled value=\"\">请选择</option>\n  <option>A</option>\n  <option>B</option>\n</select\n```\n\n多选时 (绑定到一个数组)：\n\n```html\n<div>选择的是：{{ selected }}</div>\n<select v-model=\"selected\" multiple>\n  <option>A</option>\n  <option>B</option>\n</select>\n```\n\n用 `v-for` 渲染的动态选项：\n\n```html\n<select v-model=\"selected\">\n  <option v-for=\"option in options\" :value=\"option.value\">\n    {{ option.text }}\n  </option>\n</select>\n<div>选择的是：{{ selected }}</div>\n```\n\n```js\nconst selected = ref(''A'')\nconst options = ref([\n  { text: ''One'', value: ''A'' },\n  { text: ''Two'', value: ''B'' },\n])\n```',2,7,4,1),
(247,'值绑定(v-model)','对于单选按钮，复选框和选择器选项，`v-model` 绑定的值通常是静态的字符串 (或者对复选框是布尔值)：\n\n```html\n<!-- `picked` 在被选择时是字符串 \"a\" -->\n<input type=\"radio\" v-model=\"picked\" value=\"a\" />\n\n<!-- `toggle` 只会为 true 或 false -->\n<input type=\"checkbox\" v-model=\"toggle\" />\n\n<!-- `selected` 在第一项被选中时为字符串 \"abc\" -->\n<select v-model=\"selected\">\n  <option value=\"abc\">ABC</option>\n</select\n```\n',2,7,4,1),
(248,'修饰符(v-model)','.lazy：默认情况下，`v-model` 在每次 `input` 事件触发后将输入框的值与数据进行同步。你可以添加 `lazy` 修饰符，从而转为在 `change` 事件_之后_进行同步：\n```html\n<!-- 在 \"change\" 事件后同步更新而不是 \"input\" -->\n<input v-model.lazy=\"msg\" />\n```\n.number：如果想自动将用户的输入值转为数值类型，可以给 `v-model` 添加 `number` 修饰符：\n```html\n<input v-model.number=\"age\" />\n```\n.trim：如果要自动过滤用户输入的首尾空白字符，可以给 `v-model` 添加 `trim` 修饰符：\n```html\n<input v-model.trim=\"msg\" />\n```\n',2,7,4,1),
(249,'vue-tsc','```\n//安装\nnpm i vue-tsc -D\n//用法\nvue-tsc --noEmit && vite build\n```\n\nvue3 命令行类型检查工具基于 IDE 插件 [Volar](https://github.com/johnsoncodehk/volar)。\n\n```\n//类型检查：\nvue-tsc --noEmit\n//构建 dts\nvue-tsc --declaration --emitDeclarationOnly\n```\n',2,1,4,1),
(250,'normalize','统一浏览器的初始样式\n\n```js\n//安装\nnpm install --save normalize.css\n\n//main中引入\nimport ''normalize.css/normalize.css''\n```\n\n',2,1,4,1),
(251,'unplugin-vue-components','自动引入组件\n```\nnpm unplugin-vue-components -D\n```\n```js\n// vite.config.js\nimport Components from ''unplugin-vue-components/vite''\nimport { AntDesignVueResolver } from ''unplugin-vue-components/resolvers''\n  Components({\n    dts: true, // ts支持\n    dirs: [''src/components'', ''src/views''], // 自定义路径按需导入\n    resolvers: [AntDesignVueResolver()] // antd直接使用组件,无需在任何地方导入组件\n  }),\n```\n\n',2,1,4,1),
(252,'${}模版字符串','```JS\nconsole.info(`大家好，我叫${name}，今年${age}岁了`)\n// 等价于\nconsole.info(''大家好，我叫'' + name + ''，今年'' + age + ''岁了'')\nreturn get(`${Api.FY}/${identity}/${type}/${pageindex}/${pagesize}`);\n```',2,3,4,1),
(253,'console.log','console.log({name})取代console.log(''name'', name)\n\n```js\nconsole.log({name})\n```\n\n',2,3,4,1),
(254,'globalProperties','globalProperties\n- 类型：[key: string]: any\n- 默认：undefined\n\n#### 全局使用\n```javascript\n//main.js\nimport { createApp } from ''vue''\nimport App from ''./App.vue''\nconst app = createApp(App)\n// 获取原型\nconst prototype = app.config.globalProperties\n// 绑定参数\nprototype.name = ''Jerry''\n```\n#### 组件内使用\n```js\n<script setup>\n  import { getCurrentInstance } from ''vue''\n  // 获取原型 获取上下文实例，ctx=vue2的this\n  const { proxy } = getCurrentInstance()\n  // 输出\n  console.log(proxy.name)\n</script>\n```\n',2,3,4,1),
(255,'vue3对await的支持','不必再配合 async 就可以直接使用 await 了，这种情况下，组件的 setup 会自动变成 async setup 。\n\n```js\n//vue3\n<script setup>\n  const post = await fetch(''/api'').then(() => {})\n</script>\n```\n\n',2,2,4,1),
(256,'CSS变量注入','```html\n<template>\n  <span>Jerry</span>  \n</template>\n\n<script setup>\n  import { reactive } from ''vue''\n  const state = reactive({\n    color: ''red''\n  })\n</script>\n\n<style scoped>\n  span {\n    // 使用v-bind绑定state中的变量\n    color: v-bind(''state.color'');\n  }  \n</style>\n```',2,3,4,1),
(257,'定义倒计时','```ts\nsetTimeout(async () => {\n   await ConutSort();//方法\n  }, 2000);//间隔2秒\n });\n```\n\n',2,3,4,1),
(258,'is动态组件切换','```html\n  <div>\n    <a href=\"#\" @click.prevent=\"comName = ''login''\">登录</a>\n    <a href=\"#\" @click.prevent=\"comName = ''register''\">注册</a>\n    <a href=\"#\" @click.prevent=\"comName = ''logOut''\">退出</a>\n     <!--  <component></component>来展示对应名称的组件,相当于一个占位符 // :is 属性指定 组件名称 -->\n    <component :is=\"comName\"></component>\n  </div>\n```\n\n',2,3,4,1),
(259,'清除缓存组件','```js\n// beforeRouteLeave()钩子\n// 判断是否要到详情页\n  beforeRouteLeave(to, from, next) {\n      if (to.path === \"/goods_detail\") {\n        from.meta.keepAlive = true;\n      } else {\n        from.meta.keepAlive = false;\n      }\n      next();\n    }\n```\n\n',2,3,4,1),
(260,'传值编码/解码','```js\n//编码\nlet paths = encodeURIComponent(path)\n//解码\nlet paths = decodeURIComponent(path)\n```\n\n',2,3,4,1),
(261,'回调顶部','```html\n <div id=\"backtop\">\n   <p @click=\"backtop\"></p>\n </div>\n```\n\n```js\n  static BackTop() {\n    // eslint-disable-next-line func-names\n    const timer = setInterval(function () {\n      const osTop = document.documentElement.scrollTop || document.body.scrollTop\n      const isPeed = Math.floor(-osTop / 5)\n      document.body.scrollTop = osTop + isPeed\n      document.documentElement.scrollTop = document.body.scrollTop\n      if (osTop === 0) {\n        clearInterval(timer)\n      }\n    }, 30)\n  }\n```',2,3,4,1),
(262,'日期格式化(moment)','`moment` 是一个 `JavaScript` 日期处理类库。\n\n```\nnpm install moment --save\n```\n\n```js\n /**\n   * 日期格式化\n   * @param time\n   */\n  static MomentTime(time: any) {\n    moment(time).format(''YYYY-MM-DD- H:mm:ss'')\n  }\n\n  /**\n   *  日期格式化\n   * @param time\n   */\n  static async MomentTimeList(time: any) {\n    await time.data.forEach((res: any) => {\n      res.timeCreate = moment(res.timeCreate).format(''YYYY-MM-DD- H:mm:ss'')\n    })\n  }\n```\n\n',2,3,4,1),
(263,'随机数(Random)','```js\n  /**\n   * 随机数\n   * @param minNum 最小值\n   * @param maxNum 最大值\n   * @param counts 循环次数\n   * @returns\n   */\n  static Random(minNum: number, maxNum: number, counts: number) {\n    let i = 0\n    let count\n    do {\n      i += 1\n      const res = `${Math.random() * (maxNum - minNum + 1) + minNum}`\n      count = parseInt(res, 10)\n    } while (i < counts)\n    return count\n  }\n```',2,3,4,1),
(264,'vue-router','vue-router是Vue.js官方的路由插件，它和vue.js是深度集成的，适合用于构建单页面应用。vue的单页面应用是基于路由和组件的，路由用于设定访问路径，并将路径和组件映射起来。传统的页面应用，是用一些超链接来实现页面切换和跳转的。在vue-router单页面应用中，则是路径之间的切换，也就是组件的切换。\n\n```\nnpm install vue-router@4\n```\n定义index.ts\n\nmain.ts引入\n\n```js\nimport router from ''./router/index''\nconst app = createApp(App)\napp.use(router)\n```\n',15,2,4,1),
(265,'路由导航流程','1. 导航被触发\n2. 在失活的组件里调用  beforeRouteLeave 守卫\n3. 调用全局 beforeEach 前置守卫\n4. 重用的组件调用 beforeRouteUpdate 守卫（2.2+）\n5. 路由配置调用 beforeEnter\n6. 解析异步路由组件\n7. 在被激活的组件里调用 beforeRouteEnter 守卫\n8. 调用全局的 beforeResolve 守卫（2.5+）\n9. 导航被确认\n10. 调用全局的 afterEach\n11. 触发 DOM 更新\n12. 调用 beforeRouteEnter 守卫中传给 next 的回调函数，创建好的组件实例会作为回调函数的参数传入',15,2,4,1),
(266,'Hash路由模式','hash 历史模式是用 `createWebHashHistory()` 创建的不会制造页面刷新\n\n```tsx\nimport { createRouter, createWebHashHistory } from ''vue-router''\nconst router = createRouter({\n  history: createWebHashHistory(),\n  routes: [\n    //...\n  ],\n})\n```\n',15,2,4,1),
(267,'history路由模式','history:用 `createWebHistory()` 创建 HTML5 模式，推荐使用这个模式不会有历史，不会制造页面刷新\n\n```tsx\nimport { createRouter, createWebHistory } from ''vue-router''\n\nconst router = createRouter({\n  history: createWebHistory(),\n  routes: [\n    //...\n  ],\n})\n```\n\n',15,2,4,1),
(268,'router-link to=\"xxx\"','表示目标路由的链接。 被点击后，会立刻把 to 的值传到 router.push()，这个值可以是字符串或者是描述目标位置的对象。\n\n```html\n<!-- 字符串 -->\n<router-link to=\"home\">Home</router-link>\n\n<!-- 使用 v-bind 的 JS 表达式 -->\n<router-link v-bind:to=\"''home''\">Home</router-link>\n\n<!-- 不写 v-bind 也可以，就像绑定别的属性一样 -->\n<router-link :to=\"''home''\">Home</router-link>\n\n<!-- 同上 -->\n<router-link :to=\"{ path: ''home'' }\">Home</router-link>\n\n<!-- 命名的路由 -->\n<router-link :to=\"{ name: ''user'', params: { userId: 123 }}\">User</router-link>\n\n<!-- 带查询参数，下面的结果为 /register?plan=private -->\n<router-link :to=\"{ path: ''register'', query: { plan: ''private'' }}\">Register</router-link>\n```\n',15,2,4,1),
(269,'replace','设置 replace 属性,点击时，会调用 router.replace() 而不是 router.push()，导航后不会留下 history 记录。\n\n```html\n//声明式\n<router-link :to=\"{ path: ''/abc''}\" replace></router-link>\n\n//编程式\nrouter.replace(...)\n```\n',15,2,4,1),
(270,'append','设置 append 属性后，则在当前 (相对) 路径前添加其路径。例如，我们从 /a 导航到一个相对路径 b，如果没有配置 append，则路径为 /b，如果配了，则为 /a/b\n\n```html\n<router-link :to=\"{ path: ''relative/path''}\" append></router-link>\n```\n\n',15,2,4,1),
(271,'tag','有时候想要 `<router-link>` 渲染成某种标签，例如 `<li>`。 于是我们使用 `tag` prop 类指定何种标签，同样它还是会监听点击，触发导航。\n\n```html\n<router-link to=\"/foo\" tag=\"li\">foo</router-link>\n<!-- 渲染结果 -->\n<li>foo</li>\n```\n\n',15,2,4,1),
(272,'active-class','设置 链接激活时使用的 CSS 类名。可以通过以下代码来替代。\n\n```html\n<style>\n   ._active{\n      background-color : red;\n   }\n</style>\n<p>\n   <router-link v-bind:to = \"{ path: ''/route1''}\" active-class = \"_active\">1</router-link>\n   <router-link v-bind:to = \"{ path: ''/route2''}\" tag = \"span\">2</router-link>\n</p>\n```\n\n',15,2,4,1),
(273,'exact-active-class','配置当链接被精确匹配的时候应该激活的 class。可以通过以下代码来替代。\n\n```html\n<p>\n   <router-link v-bind:to = \"{ path: ''/route1''}\" exact-active-class = \"_active\">Router Link 1</router-link>\n   <router-link v-bind:to = \"{ path: ''/route2''}\" tag = \"span\">Router Link 2</router-link>\n</p>\n```\n',15,2,4,1),
(274,'event','声明可以用来触发导航的事件。可以是一个字符串或是一个包含字符串的数组。\n\n```html\n<router-link v-bind:to = \"{ path: ''/route1''}\" event = \"mouseover\">Router Link 1</router-link>\n```\n\n以上代码设置了 event 为 mouseover ，及在鼠标移动到 Router Link 1 上时导航的 HTML 内容会发生改变。',15,2,4,1),
(275,'router-view','`router-view` 将显示与 url 对应的组件。你可以把它放在任何地方，以适应你的布局。',15,2,4,1),
(276,'useRoute/useRouter','```html\n<script setup>\n  import { useRoute, useRouter } from ''vue-router''\n  const route = useRoute()  // 路由信息\n  console.log(route.query)\n  const router = useRouter()// 路由跳转\n  router.push(''/newPage'')\n</script>\n```\n\n',15,2,4,1),
(277,'路由导航守卫','```html\n<script setup>\n  import { onBeforeRouteLeave, onBeforeRouteUpdate } from ''vue-router''\n\n  // 添加一个导航守卫，在当前组件将要离开时触发。\n  onBeforeRouteLeave((to, from, next) => {\n    next()\n  })\n\n  // 添加一个导航守卫，在当前组件更新时触发。\n  // 在当前路由改变，但是该组件被复用时调用。\n  onBeforeRouteUpdate((to, from, next) => {\n    next()\n  })\n</script>\n```\n\n',15,2,4,1),
(278,'全局前置守卫','在路由跳转前触发，可在执行 next 方法前做一些身份登录验证的逻辑。\n\n```js\nconst router = new createRouter({})\n//to: 即将要进入的目标 用一种标准化的方式\n//from: 当前导航正要离开的路由 用一种标准化的方式\nrouter.beforeEach((to, from, next) => {\n  ...\n  // 必须执行 next 方法来触发路由跳转 \n  next() \n  // 返回 false 以取消导航\n  return false\n})\n```\n\n',15,2,4,1),
(279,'全局解析守卫','与 beforeEach 类似，也是路由跳转前触发，区别是还需在`所有组件内守卫和异步路由组件被解析之后`，也就是在组件内 beforeRouteEnter 之后被调用。\n\n```js\nrouter.beforeResolve((to, from, next) => {\n  ...\n  // 必须执行 next 方法来触发路由跳转 \n  next() \n})\n```\n',15,2,4,1),
(280,'全局后置钩子','和守卫不同的是，这些钩子不会接受 `next` 函数也不会改变导航本身。它们对于分析、更改页面标题、声明页面等辅助功能以及许多其他事情都很有用。\n\n```js\nrouter.afterEach((to, from) => {\n  // ...\n})\n```\n\n',15,2,4,1),
(281,'路由独享守卫','可在路由配置上直接定义 beforeEnter\n\n```js\nconst routes = [\n  {\n    path: ''/users/:id'',\n    component: UserDetails,\n    beforeEnter: (to, from) => {\n      // reject the navigation\n      return false\n    },\n  },\n]\n```\n\n',15,2,4,1),
(282,'组件内的守卫','组件内可直接定义如下路由导航守卫\n\n```js\nconst Foo = {\n  template: `...`,\n  beforeRouteEnter(to, from) {\n    // 不能获取组件实例 this\n    // 当守卫执行前，组件实例还没被创建\n  },\n  beforeRouteUpdate(to, from) {\n    // 当前路由改变，但是组件被复用时调用\n    // 可访问实例 this\n  },\n  beforeRouteLeave(to, from) {\n    // 导航离开组件时被调用\n  }\n}\n```\n\n',15,2,4,1),
(283,'router封装','## router封装index\n\n```js\n//index.js\n// eslint-disable-next-line @typescript-eslint/no-unused-vars\nimport { createRouter, createWebHistory, RouteRecordRaw, _RouteRecordBase } from ''vue-router''\nimport NProgress from ''nprogress''\n\ndeclare module ''vue-router'' {\n  interface _RouteRecordBase {\n    hidden?: boolean | string | number\n  }\n}\nconst routes: RouteRecordRaw[] = [\n   {\n    path: ''/index'',\n    name: ''index'',\n    component: () => {\n      return import(''../views/page/article/Index.vue'')\n    },\n    meta: {\n      keepAlive: true\n    },\n    children: [\n      // 添加子路由\n      {\n        path: ''indexColumn'',\n        name: ''indexColumn'',\n        component: () => {\n          return import(''../views/page/article/components/indexColumn.vue'')\n        }\n      }\n    ]\n  },\n  { path: ''/'', redirect: { name: ''Home'' } },\n]\n\nconst router = createRouter({\n  history: createWebHistory(), //历史模式会制造页面刷新\n  routes\n})\n// 页面切换之前取消上一个路由中未完成的请求\nrouter.beforeEach((_to: any, _from: any, next: () => void) => {\n  NProgress.start()\n  next()\n})\nrouter.afterEach(() => {\n  // 进度条\n  NProgress.done()\n})\nexport default router\n```\n\n',15,3,4,1),
(284,'基础跳转','```js\nimport { useRoute, useRouter } from ''vue-router''\nconst route = useRoute() // 参数的获取  this.$route\nconst router = useRouter() // 方法使用  this.$router\nrouter.push({\n  path: num,\n  query: {\n    t: +new Date()\n  }\n})\n```\n\n',15,2,4,1),
(285,'无参跳转','```js\n// 字符串\nrouter.push(''home'')\n// 对象\nrouter.push({ path: ''home'' })\n```\n\n',15,3,4,1),
(286,'带参跳转','```js\n/**\n * 传值跳转\n * @param path 路径\n * @param value 值\n */\nconst routerId = async (path: string, value: number | string) => {\n  await router.push({\n    path: path,\n    query: {\n      id: value,\n      t: +new Date()\n    }\n  })\n}\n```\n获取跳转过来的参数\n\n```tsx\nimport { useRoute } from ''vue-router''\n const route = useRoute()\n const state = reactive({\n      id: route.query.id,\n })\n```\n',15,3,4,1),
(287,'刷新当前路由','```js\n//+new Date()保证每次点击路由的query项都是不一样的，确保会重新刷新view\nconst routers = async (path: string) => {\n  await router.push({\n    path: path,\n    query: {\n      t: +new Date()\n    }\n  })\n}\n```\n\n',15,3,4,1),
(289,'router.go(n)','这个方法的参数是一个整数，意思是在 history 记录中向前或者后退多少步，类似 `window.history.go(n)`。\n\n```js\n// 在浏览器记录中前进一步，等同于 history.forward()\nrouter.go(1)\n// 后退一步记录，等同于 history.back()\nrouter.go(-1)\n// 前进 3 步记录\nrouter.go(3)\n// 如果 history 记录不够用，那就默默地失败呗\nrouter.go(-100)\nrouter.go(100)\n```\n\n',15,2,4,1),
(290,'子路由','```html\n<a-menu-item key=\"1\" @click=\"Routers(''/Admin-index/ArticleTable'')\">\n文章列表</a-menu-item>\n<router-view></router-view>\n```\n\n路由配置\n\n```tsx\n{\n  path: ''/Admin-index'',\n  name: ''Admin-index'',\n  component: () => import(''@/views/admin/index/index.vue''),\n  children: [   // 添加子路由\n    {\n      path: ''ArticleTable'',\n      name: ''ArticleTable'',\n      component: () => import(''@/views/admin/article/ArticleTable.vue''),\n    },\n  ]\n},\n```\n\n',15,3,4,1),
(291,'配置404页面','\\* 代表通配符，若放在任意路由前，会被先匹配，导致跳转到 404 页面，所以需将如下配置置于最后。\n\n```js\n{\n  path: ''*'',\n  name: ''404''\n  component: () => import(''./404.vue'')  \n}\n```\n\n',15,3,4,1),
(292,'路由对象/属性类型报错','引入 _RouteRecordBase 定义 hidden\n\n```js\nimport {\n    createRouter,\n    createWebHashHistory,\n    RouteRecordRaw,\n    _RouteRecordBase \n} from ''vue-router''\n\ndeclare module ''vue-router''{\n    interface _RouteRecordBase {\n        hidden?: boolean | string | number\n    }\n}\n\nconst routes: Array<RouteRecordRaw> = [\n {\n        path: ''/'',\n        redirect: ''/login'',\n    },\n    {\n        path: ''/login'',\n        name:''login'',\n       	hidden: false,\n        component: () => import(''@/views/login.vue''), // 懒加载组件\n    }\n]\n\n```\n',15,4,4,1),
(293,'常用的命名规范','- `camelCase`（小驼峰式命名法 —— 首字母小写）\n- `PascalCase`（大驼峰式命名法 —— 首字母大写）\n- `kebab-case`（短横线连接式）\n- `Snake`（下划线连接式）\n\n#### 项目名 \n\n全部采用小写方式， 以**短横线**分隔。 例：`my-project-name`。',2,6,4,1),
(294,'CSS文件名','全部采用小写方式， 优先选择单个单词命名，多个单词命名以**短横线**分隔。\n\n```lua\n|- normalize.less\n|- base.less\n|- date-picker.scss\n|- input-number.scss\n```\n\n',2,8,4,1),
(295,'单例组件名','只拥有单个活跃实例的组件应该以 `The` 前缀命名，以示其唯一性。\n\n这不意味着组件只可用于一个单页面，而是*每个页面*只使用一次。这些组件永远不接受任何 prop，因为它们是为你的应用定制的。如果你发现有必要添加 prop，那就表明这实际上是一个可复用的组件，*只是目前*在每个页面里只使用一次。\n\n比如，头部和侧边栏组件几乎在每个页面都会使用，不接受 prop，该组件是专门为该应用所定制的。\n\n```\ncomponents/\n|- TheHeading.vue\n|- TheSidebar.vue\n```\n\n',2,8,4,1),
(296,'基础组件名','> 基础组件：不包含业务，独立、具体功能的基础组件，比如**日期选择器**、**模态框**等。这类组件作为项目的基础控件，会被大量使用，因此组件的 API 进行过高强度的抽象，可以通过不同配置实现不同的功能。\n\n特定样式和约定的基础组件(展示类的、无逻辑的或无状态、不掺杂业务逻辑的组件) 应该全部以一个特定的前缀开头 —— Base。**基础组件在一个页面内可使用多次，在不同页面内也可复用，是高可复用组件。**\n\n```\ncomponents/\n|- BaseButton.vue\n|- BaseTable.vue\n|- BaseIcon.vue\n```\n\n',2,6,4,1),
(297,'业务组件','> 业务组件：它不像基础组件只包含某个功能，而是在业务中被多个页面复用的（具有可复用性），它与基础组件的区别是，业务组件只在当前项目中会用到，不具有通用性，而且会包含一些业务，比如数据请求；而基础组件不含业务，在任何项目中都可以使用，功能单一，比如一个具有数据校验功能的输入框。\n\n**掺杂了复杂业务的组件（拥有自身 `data`、`prop` 的相关处理）即业务组件**应该以 `Custom` 前缀命名。业务组件在一个页面内比如：某个页面内有一个卡片列表，而样式和逻辑跟业务紧密相关的卡片就是业务组件。\n\n```\ncomponents/\n|- CustomCard.vue\n```\n\n\n\n\n\n',2,6,4,1),
(298,'紧密耦合的组件名','**和父组件紧密耦合的子组件应该以父组件名作为前缀命名。**  因为编辑器通常会按字母顺序组织文件，所以这样做可以把相关联的文件排在一起。\n\n```\ncomponents/\n|- TodoList.vue\n|- TodoListItem.vue\n|- TodoListItemButton.vue\n```\n\n\n',2,6,4,1),
(299,'变量命名','- 命名方法：camelCase\n- 命名规范：类型 + 对象描述或属性的方式\n\n``` js\nlet tableTitle = \"LoginTable\"\nlet mySchool = \"我的学校\"\n```\n\n',2,6,4,1),
(300,'常量命名','- 命名方法：全部大写下划线分割\n- 命名规范：使用大写字母和下划线来组合命名，下划线用以分割单词\n\n```javascript\nconst MAX_COUNT = 10\nconst URL = ''http://test.host.com''\n```\n\n',2,6,4,1),
(301,'方法命名','- 命名方法：camelCase\n- 命名规范：统一使用动词或者动词 + 名词形式\n\n```js\n// 1、普通情况下，使用动词 + 名词形式\njumpPage、openCarInfoDialog\n// 2、请求数据方法，以 data 结尾\ngetListData、postFormData\n// 3、单个动词的情况\ninit、refresh\n```\n\n| 动词 | 含义                         | 返回值                                                  |\n| ---- | ---------------------------- | ------------------------------------------------------- |\n| can  | 判断是否可执行某个动作 (权 ) | 函数返回一个布尔值。true：可执行；false：不可执行；     |\n| has  | 判断是否含有某个值           | 函数返回一个布尔值。true：含有此值；false：不含有此值； |\n| is   | 判断是否为某个值             | 函数返回一个布尔值。true：为某个值；false：不为某个值； |\n| get  | 获取某个值                   | 函数返回一个非布尔值                                    |\n| set  | 设置某个值                   | 无返回值、返回是否设置成功或者返回链式对象              |\n\n\n\n',2,6,4,1),
(302,'指令缩写(:@#)','- **用 `:` 表示 `v-bind:`**\n- **用 `@` 表示 `v-on:`**\n- **用 `#` 表示 `v-slot:`**',2,7,4,1),
(303,'img默认设置','#### 让图片元素隐藏\n\n```html\n<img src=\"图片的url地址\" alt=\"图片XX\" onerror=\"this.style.display=''none''\"/>\n1\n```\n\n#### 设置默认图\n\n```html\n<img src=\"图片的url地址\" alt=\"图片XX\" onerror=\"this.src=''默认图片的url地址''\"/>\n1\n```\n\n第二种方式为常用的方法，但当onerror中设置的图片路径也不存在时就会导致浏览器一直加载这个图，造成堆栈溢出错误。\n所以，通常在后面加上 **this.οnerrοr=null;** 保证onerror里的事件只执行一次。所以完整的写法为：\n\n```html\n<img src=\"图片的url地址\" alt=\"图片XX\" onerror=\"this.src=''默认图片的url地址'';this.οnerrοr=null\"/>\n```\n\n',2,3,4,1),
(306,'scss介绍','- [Sass 官方文档](https://sass-lang.com/documentation) *(sass-lang.com)*\n- [Sass 中文文档](https://www.sass.hk/docs/) *(sass.hk)*\n\nSass 是一种 CSS 的预编译语言\n\n```bash\n$ npm install -g sass\n```\n\n在 Node.js 环境中使用 Sass\n\n```bash\n$ sass source/index.scss build/index.css\n$ sass --watch input.scss output.css\n$ sass --watch app/sass:public/css\n```\n\n\n\n',10,2,4,1),
(307,'注释','```\n/*\n 多行注释\n 块注释\n 块注释\n*/\n\n// 单行注释\n```\n\n',10,2,4,1),
(308,'字符串插值','``` css\n$wk: -webkit-;\n.rounded-box {\n  #{$wk}border-radius: 4px;\n}\n```\n\n\n',10,2,4,1),
(309,'模块(片段)','```css\n// _base.scss\n$font-stack:    Helvetica, sans-serif;\n$primary-color: #333;\n注意以下划线开头的 Sass 文件\n\n// styles.scss\n@use ''base'';\n\n.inverse {\n  background-color: base.$primary-color;\n  color: white;\n}\n```\n\n',10,2,4,1),
(310,'@import','```css\n@import ''./other_sass_file'';\n@import ''/code'', ''lists'';\n// 纯 CSS @imports\n@import \"theme.css\";\n@import url(theme);\n```\n\n`.sass` 或 `.sass` 扩展名是可选的。',10,2,4,1),
(311,'混合(Mixins默认值)','```css\n@mixin pad($n: 10px) {\n    padding: $n;\n}\nbody {\n    @include pad(15px);\n}\n```\n',10,3,4,1),
(313,'混合(Mixins默认变量)',' ```css\n$default-padding: 10px;\n@mixin pad($n: $default-padding) {\n  padding: $n;\n}\nbody {\n  @include pad(15px);\n}\n```',10,3,4,1),
(314,'@media','```css\n/* 超小设备 (手机, 600px 以下屏幕设备) */\n@media only screen and (max-width: 600px) {   \n}\n/* 小设备 (平板电脑和大型手机，600 像素及以上) */\n@media only screen and (min-width: 600px) { \n}\n/* 中型设备（平板电脑，768 像素及以上） */\n@media only screen and (min-width: 768px) {\n} \n/* 大型设备（笔记本电脑/台式机，992 像素及以上） */\n@media only screen and (min-width: 992px) {\n} \n/* 超大型设备（大型笔记本电脑和台式机，1200 像素及以上） */\n@media only screen and (min-width: 1200px) {\n}\n```\n',10,3,4,1),
(315,'循环(each嵌套)','\n```scss\n$icons: (\"eye\": \"\\f112\", \"start\": \"\\f12e\");\n\n@each $name, $glyph in $icons {\n  .icon-#{$name}:before {\n    display: inline-block;\n    font-family: \"Icon Font\";\n    content: $glyph;\n  }\n}\n```\n\n',10,3,4,1),
(316,'条件(if)','```css\n@mixin avatar($size, $circle: false) {\n  width: $size;\n  height: $size;\n  @if $circle {\n    border-radius: $size / 2;\n  }\n}\n.square-av {\n  @include avatar(100px, $circle: false);\n}\n.circle-av {\n  @include avatar(100px, $circle: true);\n}\n```',10,3,4,1),
(317,'Map','\n```css\n$map: (key1: value1, key2: value2, key3: value3);\nmap-get($map, key1)\n```\n\n',10,3,4,1),
(318,'基础指令','指令是带有 v- 前缀的特殊属性。\n\n指令用于在表达式的值改变时，将某些行为应用到 DOM 上。',2,2,4,1),
(319,'v-else-if','```html\n<div v-if=\"type === ''A''\">\n  A\n</div>\n<div v-else-if=\"type === ''B''\">\n  B\n</div>\n<div v-else-if=\"type === ''C''\">\n  C\n</div>\n<div v-else>\n  Not A/B/C\n</div>\n```\n',2,7,4,1),
(320,'基础类型','JavaScript 的类型分为两种：原始数据类型（Primitive data types）和对象类型（Object types）。\n\n- 常用：boolean、number、string、array、enum、any、void\n- 不常用：tuple、null、undefined、never',5,2,4,1),
(321,'Boolean','布尔值是最基础的数据类型，在 TypeScript 中，使用 `boolean` 定义布尔值类型：\n\n\n```ts\nlet isDone: boolean = false;\n```\n\n',5,2,4,1),
(322,'Number','双精度 64 位浮点值。它可以用来表示整数和分数。\n\n\n```ts\nlet decLiteral: number = 6;\nlet hexLiteral: number = 0xf00d;\n// ES6 中的二进制表示法\nlet binaryLiteral: number = 0b1010;\n// ES6 中的八进制表示法\nlet octalLiteral: number = 0o744;\nlet notANumber: number = NaN;\nlet infinityNumber: number = Infinity;\n```\n\n',5,2,4,1),
(323,'任意类型(Any)','声明为 any 的变量可以赋予任意类型的值。\n\n```tsx\nlet myFavoriteNumber: any = ''seven'';\nmyFavoriteNumber = 7;\n```\n\n\n\n',5,2,4,1),
(324,'元组[]','数组合并了相同类型的对象，而元组（Tuple）合并了不同类型的对象。\n\n定义一对值分别为 `string` 和 `number` 的元组：\n\n```tsx\nlet x: [string, number];\nx = [''Runoob'', 1];    // 运行正常\nx = [1, ''Runoob''];    // 报错\nconsole.log(x[0]);    // 输出 Runoob\n```\n',5,2,4,1),
(325,'访问元组[]','元组中元素使用索引来访问，第一个元素的索引值为 0，第二个为 1，以此类推第 n 个为 n-1，语法格式如下:\n\n```\ntuple_name[index]\n```\n\n以下实例定义了元组，包含了数字和字符串两种类型的元素：\n\n```tsx\nvar mytuple = [10,\"Runoob\"]; // 创建元组\nconsole.log(mytuple[0]) \nconsole.log(mytuple[1])\n```\n',5,2,4,1),
(326,'元组运算[]','我们可以使用以下两个函数向元组添加新元素或者删除元素：\n\n- push() 向元组添加元素，添加在最后面。\n- pop() 从元组中移除元素（最后一个），并返回移除的元素。\n\n```tsx\nvar mytuple = [10,\"Hello\",\"World\",\"typeScript\"]; \nconsole.log(\"添加前元素个数：\"+mytuple.length)    // 返回元组的大小\n \nmytuple.push(12)                                    // 添加到元组中\nconsole.log(\"添加后元素个数：\"+mytuple.length) \nconsole.log(\"删除前元素个数：\"+mytuple.length) \nconsole.log(mytuple.pop()+\" 元素从元组中删除\") // 删除并返回删除的元素\n        \nconsole.log(\"删除后元素个数：\"+mytuple.length)\n```\n\n',5,2,4,1),
(327,'更新元组[]','元组是可变的，这意味着我们可以对元组进行更新操作：\n\n```js\nvar mytuple = [10, \"Runoob\", \"Taobao\", \"Google\"]; // 创建一个元组\nconsole.log(\"元组的第一个元素为：\" + mytuple[0]) \n \n// 更新元组元素\nmytuple[0] = 121     \nconsole.log(\"元组中的第一个元素更新为：\"+ mytuple[0])\n```\n\n',5,2,4,1),
(328,'解构元组[]','我们也可以把元组元素赋值给变量，如下所示：\n\n``` js\nvar a =[10,\"Runoob\"] \nvar [b,c] = a \nconsole.log( b )    \nconsole.log( c )\n```\n\n',5,2,4,1),
(329,'数组(Array)','Array数组对象是使用单独的变量名来存储一系列的值。\n#### 数组定义\n\n简单的方法使用「类型 + 方括号」来表示数组：\n\n```js\ninterface IItem {\n  id: number;\n  name: string;\n  isGod: boolean;\n}\nconst objectArr: IItem[] = [{ id: 1, name: ''俊劫'', isGod: true }];\n// or\nconst objectArr: Array<IItem> = [{ id: 1, name: ''俊劫'', isGod: true }];\nconst numberArr: number[] = [1, 2, 3];\nconst arr: (number | string)[] = [1, \"string\", 2];\n```\n',5,2,4,1),
(330,'忽略不要的元素(Array)','如果需要从数组或元组中取出某个或某几个特定的元素的话，可以在解构语句中插入额外的逗号，忽略掉不需要的元素：\n\n```tsx\nconst [a, , b] = [1, 5, 10];  // a <- 1, b <- 10\n```\n\n',5,2,4,1),
(331,'数组泛型(Array)','我们也可以使用数组泛型（Array Generic） `Array<elemType>` 来表示数组：\n\n```ts\nlet fibonacci: Array<number> = [1, 1, 2, 3, 5];\nlet arr2:Array<string> = [\"1\",\"2\"]\n```\n',5,2,4,1),
(332,'联合类型数组(Array)','```js\nlet arr:(number | string)[];\n// 表示定义了一个名称叫做arr的数组, \n// 这个数组中将来既可以存储数值类型的数据, 也可以存储字符串类型的数据\narr3 = [1, ''b'', 2, ''c''];\n\nconst arr: (number | string)[] = [1, \"string\", 2];\n```',5,2,4,1),
(333,'交叉&类型(Array)','- 交叉类型：由多个类型组成，用 `&` 连接\n\n```tsx\ninterface Waiter {\n  anjiao: boolean;\n  say: () => {};\n}\n\ninterface Teacher {\n  anjiao: boolean;\n  skill: () => {};\n}\n\n// 交叉类型 \n// 同名类型会进行合并，同名基础类型属性的合并返回：never\n// 同名非基础类型属性可以正常合并\nfunction judgeWho(jishi: Waiter & Teacher) {}\n```',5,2,4,1),
(334,'指定对象成员的数组(Array)','```js\n// interface是接口,后面会讲到\ninterface Arrobj{\n    name:string,\n    age:number\n}\nlet arr3:Arrobj[]=[{name:''jimmy'',age:22}]\n```\n\n',5,2,4,1),
(335,'数组解构(Array)','```js\nlet x: number, let y: number ,let z: number;\nlet five_array = [0,1,2,3,4];\n[x,y,z] = five_array;\n```\n',5,2,4,1),
(336,'数组展开运算符(Array)','```javascript\nlet two_array = [0,1];\nlet five_array = [...two_array,2,3,4];\n```\n\n',5,2,4,1),
(337,'数组循环(Array)','```javascript\nlet colors: string[] = [\"red\", \"green\", \"blue\"];\nfor(let i in colors) {\n  console.log(i);\n}\n```\n',5,2,4,1),
(338,'多维数组(Array)','一个数组的元素可以是另外一个数组，这样就构成了多维数组（Multi-dimensional Array）。\n\n最简单的多维数组是二维数组，定义方式如下：\n\n``` js\nvar arr_name:datatype[][]=[ [val1,val2,val3],[v1,v2,v3] ]\n```\n\n',5,2,4,1),
(339,'数组.concat()','连接两个或更多的数组，并返回结果。\n\n```tsx\nvar alpha = [\"a\", \"b\", \"c\"]; \nvar numeric = [1, 2, 3];\n\nvar alphaNumeric = alpha.concat(numeric); \nconsole.log(\"alphaNumeric : \" + alphaNumeric );    // a,b,c,1,2,3   \n```',5,2,4,1),
(340,'数组.every()','检测数值元素的每个元素是否都符合条件。\n\n```tsx\nfunction isBigEnough(element, index, array) { \n        return (element >= 10); \n}        \nvar passed = [12, 5, 8, 130, 44].every(isBigEnough); \nconsole.log(\"Test Value : \" + passed ); // false\n```\n',5,2,4,1),
(341,'数组.filter()',' 检测数值元素，并返回符合条件所有元素的数组。\n\n```tsx\nfunction isBigEnough(element, index, array) { \n   return (element >= 10); \n}         \nvar passed = [12, 5, 8, 130, 44].filter(isBigEnough); \nconsole.log(\"Test Value : \" + passed ); // 12,130,44\n```\n',5,2,4,1),
(342,'数组.forEach()','数组每个元素都执行一次回调函数。\n\n```tsx\nlet num = [7, 8, 9]; num.forEach(function (value) {\n    console.log(value); });\n`编译成 JavaScript 代码：`\nvar num = [7, 8, 9]; num.forEach(function (value) {  \n    console.log(value);  \n    // 7   8   9 });\n```\n\n',5,2,4,1),
(343,'数组.indexOf()','搜索数组中的元素，并返回它所在的位置。如果搜索不到，返回值 -1，代表没有此项。\n\n```tsx\nvar index = [12, 5, 8, 130, 44].indexOf(8);\nconsole.log(\"index is : \" + index );  // 2\n```\n',5,2,4,1),
(344,'数组.join()','把数组的所有元素放入一个字符串。         \n\n```tsx\nvar arr = new Array(\"Google\",\"Runoob\",\"Taobao\"); \n          \nvar str = arr.join(); \nconsole.log(\"str : \" + str );  // Google,Runoob,Taobao\n          \nvar str = arr.join(\", \"); \nconsole.log(\"str : \" + str );  // Google, Runoob, Taobao\n          \nvar str = arr.join(\" + \"); \nconsole.log(\"str : \" + str );  // Google + Runoob + Taobao\n```',5,2,4,1),
(345,'数组.lastIndexOf()','返回一个指定的字符串值最后出现的位置，在一个字符串中的指定位置从后向前搜索。\n\n```tsx\nvar index = [12, 5, 8, 130, 44].lastIndexOf(8); \nconsole.log(\"index is : \" + index );  // 2\n```\n\n',5,2,4,1),
(346,'数组.map()','通过指定函数处理数组的每个元素，并返回处理后的数组。\n\n```tsx\nvar numbers = [1, 4, 9]; \nvar roots = numbers.map(Math.sqrt); \nconsole.log(\"roots is : \" + roots );  // 1,2,3\n```\n\n',5,2,4,1),
(347,'数组.pop()','删除数组的最后一个元素并返回删除的元素。              \n\n```tsx\nvar numbers = [1, 4, 9]; \n          \nvar element = numbers.pop(); \nconsole.log(\"element is : \" + element );  // 9\n          \nvar element = numbers.pop(); \nconsole.log(\"element is : \" + element );  // 4\n```\n\n',5,2,4,1),
(348,'数组.push()','向数组的末尾添加一个或更多元素，并返回新的长度。      \n\n```tsx\nvar numbers = new Array(1, 4, 9); \nvar length = numbers.push(10); \nconsole.log(\"new numbers is : \" + numbers );  // 1,4,9,10 \nlength = numbers.push(20); \nconsole.log(\"new numbers is : \" + numbers );  // 1,4,9,10,20\n```\n\n',5,2,4,1),
(349,'数组.reduce()','将数组元素计算为一个值（从左到右）。              \n\n```tsx\nvar total = [0, 1, 2, 3].reduce(function(a, b){ return a + b; }); \nconsole.log(\"total is : \" + total );  // 6\n```\n\n',5,2,4,1),
(350,'数组.reduceRight()','将数组元素计算为一个值（从右到左）。        \n\n``` tsx\nvar total = [0, 1, 2, 3].reduceRight(function(a, b){ return a + b; }); \nconsole.log(\"total is : \" + total );  // 6\n```\n\n',5,2,4,1),
(351,'数组.reverse()','反转数组的元素顺序。                             \n\n```tsx\nvar arr = [0, 1, 2, 3].reverse(); \nconsole.log(\"Reversed array is : \" + arr );  // 3,2,1,0\n```\n\n',5,2,4,1),
(352,'数组.shift()','删除并返回数组的第一个元素。                          \n\n```tsx\nvar arr = [10, 1, 2, 3].shift(); \nconsole.log(\"Shifted value is : \" + arr );  // 10\n```',5,2,4,1),
(353,'数组.slice()','选取数组的的一部分，并返回一个新数组。             \n\n```tsx\nvar arr = [\"orange\", \"mango\", \"banana\", \"sugar\", \"tea\"]; \nconsole.log(\"arr.slice( 1, 2) : \" + arr.slice( 1, 2) );  // mango\nconsole.log(\"arr.slice( 1, 3) : \" + arr.slice( 1, 3) );  // mango,banana\n```\n',5,2,4,1),
(354,'数组.some()','检测数组元素中是否有元素符合指定条件。             \n\n```tsx\nfunction isBigEnough(element, index, array) { \n   return (element >= 10);     \n} \nvar retval = [2, 5, 8, 1, 4].some(isBigEnough);\nconsole.log(\"Returned value is : \" + retval );  // false\n          \nvar retval = [12, 5, 8, 1, 4].some(isBigEnough); \nconsole.log(\"Returned value is : \" + retval );  // true\n```\n\n',5,2,4,1),
(355,'数组.sort()','对数组的元素进行排序。                                \n\n```tsx\nvar arr = new Array(\"orange\", \"mango\", \"banana\", \"sugar\"); \nvar sorted = arr.sort(); \nconsole.log(\"Returned string is : \" + sorted );  // banana,mango,orange,sugar\n```',5,2,4,1),
(356,'数组.splice()','从数组中添加或删除元素。                           \n\n```tsx\nvar arr = [\"orange\", \"mango\", \"banana\", \"sugar\", \"tea\"];  \nvar removed = arr.splice(2, 0, \"water\");  \nconsole.log(\"After adding 1: \" + arr );    // orange,mango,water,banana,sugar,tea \nconsole.log(\"removed is: \" + removed); \n          \nremoved = arr.splice(3, 1);  \nconsole.log(\"After removing 1: \" + arr );  // orange,mango,water,sugar,tea \nconsole.log(\"removed is: \" + removed);  // banana\n```\n',5,2,4,1),
(357,'数组.toString()','把数组转换为字符串，并返回结果。                \n\n```tsx\nvar arr = new Array(\"orange\", \"mango\", \"banana\", \"sugar\");         \nvar str = arr.toString(); \nconsole.log(\"Returned string is : \" + str );  // orange,mango,banana,sugar\n```\n',5,2,4,1),
(358,'数组.unshift()','向数组的开头添加一个或更多元素，并返回新的长度。   \n\n```tsx\nvar arr = new Array(\"orange\", \"mango\", \"banana\", \"sugar\"); \nvar length = arr.unshift(\"water\"); \nconsole.log(\"Returned array is : \" + arr );  // water,orange,mango,banana,sugar \nconsole.log(\"Length of the array is : \" + length ); // 5\n```\n',5,2,4,1),
(359,'空值(Void)','JavaScript 没有空值（Void）的概念，在 TypeScript 中，可以用 `void` 表示没有任何返回值的函数：\n\n```ts\nfunction alertName(): void {\n    alert(''My name is Tom'');\n}\n```\n\n声明一个 `void` 类型的变量没有什么用，因为你只能将它赋值为 `undefined` 和 `null`（只在 --strictNullChecks 未指定时）：\n\n```ts\nlet unusable: void = undefined;\n```\n\n',5,2,4,1),
(360,'Null和Undefined','可以使用 `null` 和 `undefined` 来定义这两个原始数据类型：\n\n```ts\nlet u: undefined = undefined;\nlet n: null = null;\n```\n\n与 `void` 的区别是，`undefined` 和 `null` 是所有类型的子类型。也就是说 `undefined` 类型的变量，可以赋值给 `number` 类型的变量：\n\n```ts\n// 这样不会报错\nlet num: number = undefined;\n// 这样也不会报错\nlet u: undefined;\nlet num: number = u;\n```\n而 `void` 类型的变量不能赋值给 `number` 类型的变量：\n\n```ts\nlet u: void;\nlet num: number = u;\n// Type ''void'' is not assignable to type ''number''.\n```',5,2,4,1),
(361,'枚举(Enum)','提高代码可维护性，统一维护某些枚举值，避免 `JiShi === 1`这种魔法数字。`JiShi === JiShiEnum.BLUEJ`这样写，老板一眼就知道我想找谁。\n\n```javascript\n// 初始值默认为 0\nenum JiShiEnum {\n     REDJ,\n     BLUEJ,\n}\n// 设置初始值\nenum JiShiEnum {\n     REDJ = 8,\n     BLUEJ,\n}\nconst jishi: JiShiEnum = JiShiENUM.BLUE\nconsole.log(jishi) // 9\n// 字符串枚举，每个都需要声明\nenum JiShiEnum {\n     REDJ = \"8号\",\n     BLUEJ = \"9号\",\n}\n\n```\n\n默认情况下，初始值为 0，其余的成员会从 1 开始自动增长。',5,2,4,1),
(362,'常数枚举(Enum)','常数枚举与普通枚举的区别是，它会在编译阶段被删除，并且不能包含计算成员。\n\n常数枚举是使用 `const enum` 定义的枚举类型：\n\n```ts\nconst enum Directions {\n    Up,\n    Down,\n    Left,\n    Right\n}\nlet directions = [Directions.Up, Directions.Down, Directions.Left, Directions.Right];\n```\n\n假如包含了计算成员，则会在编译阶段报错：\n\n```ts\nconst enum Color {Red, Green, Blue = \"blue\".length};\n// index.ts(1,38): error TS2474: In ''const'' enum declarations member initializer must be constant expression.\n```\n',5,2,4,1),
(363,'外部枚举(Enum)','`declare` 定义的类型只会用于编译时的检查，编译结果中会被删除。\n\n外部枚举（Ambient Enums）是使用 `declare enum` 定义的枚举类型：\n\n```ts\ndeclare enum Directions {\n    Up,\n    Down,\n    Left,\n    Right\n}\nlet directions = [Directions.Up, Directions.Down, Directions.Left, Directions.Right];\n```\n\n上例的编译结果是：\n\n```js\nvar directions = [Directions.Up, Directions.Down, Directions.Left, Directions.Right];\n```\n\n外部枚举与声明语句一样，常出现在声明文件中。\n\n同时使用 `declare` 和 `const` 也是可以的：\n\n```ts\ndeclare const enum Directions {\n    Up,\n    Down,\n    Left,\n    Right\n}\nlet directions = [Directions.Up, Directions.Down, Directions.Left, Directions.Right];\n```\n\n编译结果：\n```js\nvar directions = [0 /* Up */, 1 /* Down */, 2 /* Left */, 3 /* Right */];\n```\n\n',5,2,4,1),
(364,'Never','`never` 类型表示的是那些永不存在的值的类型。 例如，`never` 类型是那些总是会抛出异常或根本就不会有返回值的函数表达式或箭头函数表达式的返回值类型。\n\n```javascript\n// 返回never的函数必须存在无法达到的终点\nfunction error(message: string): never {\n    throw new Error(message);\n}\n\n// 返回never的函数必须存在无法达到的终点\nfunction infiniteLoop(): never {\n    while (true) {}\n}\n```\n',5,2,4,1),
(365,'类型断言','通过这种方式可以告诉编译器，”相信我，我知道自己在干什么”。类型断言好比其他语言里的类型转换，但是不进行特殊的数据检查和解构。它没有运行时的影响，只是在编译阶段起作用。\n\n类型断言有两种形式：\n\n- “尖括号”语法\n\n```javascript\nlet someValue: any = \"this is a string\";\nlet strLength: number = (<string>someValue).length;\n```\n\n- as 语法\n\n```javascript\nlet someValue: any = \"this is a string\";\nlet strLength: number = (someValue as string).length;\n```\n\n\n\n',5,2,4,1),
(366,'交叉类型(&)','交叉类型是将多个类型合并为一个类型, 表示\"并且\"的关系,用&连接多个类型, 常用于对象合并:\n\n其返回类型既要符合 `T` 类型也要符合 `U` 类型\n\n```tsx\n//1\ninterface A {a:number};\ninterface B {b:string};\n\nconst a:A = {a:1};\nconst b:B = {b:''1''};\nconst ab:A&B = {...a,...b};\n\n//2\ninterface Ant {\n    name: string;\n    weight: number;\n}\n\ninterface Fly {\n    flyHeight: number;\n    speed: number;\n}\n\n// 少了任何一个属性都会报错\nconst flyAnt: Ant & Fly = {\n    name: ''蚂蚁呀嘿'',\n    weight: 0.2,\n    flyHeight: 20,\n    speed: 1,\n};\n```\n',5,2,4,1),
(367,'联合类型(|)','- 联合类型：某个变量可能是多个 interface 中的其中一个，用 `|` 分割\n\n  其返回类型为连接的多个类型中的任意一个\n\n```ts\nlet name: string | number;\nconsole.log(name.toString());\nname = 1;\nconsole.log(name.toFixed(2));\nname = \"hello\";\nconsole.log(name.length);\n```\n',5,2,4,1),
(368,'类型别名','类型别名用来给一个类型起个新名字。\n\n```tsx\ntype Message = string | string[];\n\nlet greet = (message: Message) => {\n  // ...\n};\n}\n```\n上例中，我们使用 `type` 创建类型别名。\n类型别名常用于联合类型',5,2,4,1),
(369,'Unknown类型','就像所有类型都可以赋值给 `any`，所有类型也都可以赋值给 `unknown`。这使得 `unknown` 成为 TypeScript 类型系统的另一种顶级类型（另一种是 `any`）。下面我们来看一下 `unknown` 类型的使用示例：\n\n```typescript\nlet value: unknown;\n\nvalue = true; // OK\nvalue = 42; // OK\nvalue = \"Hello World\"; // OK\nvalue = []; // OK\nvalue = {}; // OK\nvalue = Math.random; // OK\nvalue = null; // OK\nvalue = undefined; // OK\nvalue = new TypeError(); // OK\nvalue = Symbol(\"type\"); // OK\n```\n',5,2,4,1),
(370,'函数声明(Function)','两种常见的定义函数的方式\n+ 函数声明（Function Declaration）\n+ 和函数表达式（Function Expression）：\n\n```tsx\n// 函数声明（Function Declaration）\nfunction sum(x, y) {\n    return x + y;\n}\nasync function clickName(id: number) {\n ...\n}\n// 函数表达式（Function Expression）\nlet mySum = function (x, y) {\n    return x + y;\n};  \n```\n\n一个函数有输入和输出，要在 TypeScript 中对其进行约束，需要把输入和输出都考虑到，其中函数声明的类型定义较简单：\n\n```tsx\nfunction sum(x: number, y: number): number {\n    return x + y;\n}\n```\n',5,2,4,1),
(371,'箭头函数(Function)','### 箭头函数(Function)\n\n在 TypeScript 的类型定义中，`=>` 用来表示函数的定义，左边是输入类型，需要用括号括起来，右边是输出类型。\n\n```tsx\nmyBooks.forEach(() => console.log(''Done reading''));\n\nmyBooks.forEach(title => console.log(title));\n\nmyBooks.forEach((title, idx, arr) => \n  console.log(idx + ''-'' + title);\n);\n\nmyBooks.forEach((title, idx, arr) => {\n  console.log(idx + ''-'' + title);\n});\n```\n\n使用示例\n\n```javascript\n// 未使用箭头函数\nfunction Book() {\n  let self = this;\n  self.publishDate = 2016;\n  setInterval(function() {\n    console.log(self.publishDate);\n  }, 1000);\n}\n\n// 使用箭头函数\nfunction Book() {\n  this.publishDate = 2016;\n  setInterval(() => {\n    console.log(this.publishDate);\n  }, 1000);\n}\n```\n',5,2,4,1),
(372,'参数类型和返回类型(Function)','### 参数类型和返回类型(Function)\n\n```tsx\nfunction createUserId(name: string, id: number): string {\n  return name + id;\n}\n```\n\n',5,2,4,1),
(373,'函数类型(Function)','```tsx\nlet IdGenerator: (chars: string, nums: number) => string;\nfunction createUserId(name: string, id: number): string {\n  return name + id;\n}\nIdGenerator = createUserId;\n```\n',5,2,4,1),
(374,'可选参数及默认参数(Function)','输入多余的（或者少于要求的）参数，是不允许的。那么如何定义可选的参数呢？\n\n与接口中的可选属性类似，用 `?` 表示可选的参数\n可选参数必须接在必需参数后面\n```tsx\n// 可选参数\nfunction createUserId(name: string, age?: number, \n  id: number): string {\n    return name + id;\n}\n\n// 默认参数\nfunction createUserId(name: string = ''Semlinker'', age?: number, \n  id: number): string {\n    return name + id;\n}\n```\n',5,2,4,1),
(375,'剩余参数(Function)','有一种情况，我们不知道要向函数传入多少个参数，这时候我们就可以使用剩余参数来定义。\n\n使用 `...rest` 的方式获取函数中的剩余参数（rest 参数）\n\n```tsx\nfunction push(array, ...items) {\n  items.forEach(function(item) {\n    array.push(item);\n  });\n}\n\nlet a = [];\npush(a, 1, 2, 3);\n```\n\n',5,2,4,1),
(376,' 匿名函数(Function)','### 匿名函数(Function)\n\n匿名函数是一个没有函数名的函数。\n\n匿名函数在程序运行时动态声明，除了没有函数名外，其他的与标准函数一样。\n\n我们可以将匿名函数赋值给一个变量，这种表达式就成为函数表达式。\n\n语法格式如下：\n\n```tsx\nvar res = function( [arguments] ) { ... }\n```\n\n#### 不带参数\n\n```tsx\nvar msg = function() {\nreturn \"hello world\"; \n} \nconsole.log(msg())\n```\n\n#### 带参数\n\n```tsx\nvar res = function(a:number,b:number) {\nreturn a*b;  \n}; \nconsole.log(res(12,2))\n```\n\n#### 匿名函数自调用\n\n匿名函数自调用在函数后使用 () 即可：\n\n```tsx\n(function () {   \nvar x = \"Hello!!\";      \nconsole.log(x)  \n})()\n```\n\n',5,2,4,1),
(377,'重载','重载允许一个函数接受不同数量或类型的参数时，作出不同的处理。\n\n使用重载定义多个 `reverse` 的函数类型：\n\n```ts\nfunction reverse(x: number): number;\nfunction reverse(x: string): string;\nfunction reverse(x: number | string): number | string | void {\n    ///\n}\n```\n\n我们重复定义了多次函数 `reverse`，前几次都是函数定义，最后一次是函数实现。在编辑器的代码提示中，可以正确的看到前两个提示。\n\n注意，TypeScript 会优先从最前面的函数定义开始匹配，所以多个函数定义如果有包含关系，需要优先把精确的定义写在前面。',5,2,4,1),
(378,'类(Class)','在面向对象语言中，类是一种面向对象计算机编程语言的构造，是创建对象的蓝图，描述了所创建的对象共同的属性和方法。\n### 类的概念\n\n这里对类相关的概念做一个简单的介绍。\n\n- 类（Class）：定义了一件事物的抽象特点，包含它的属性和方法\n- 对象（Object）：类的实例，通过 `new` 生成\n- 面向对象（OOP）的三大特性：封装、继承、多态\n- 封装（Encapsulation）：将对数据的操作细节隐藏起来，只暴露对外的接口。外界调用端不需要（也不可能）知道细节，就能通过对外提供的接口来访问该对象，同时也保证了外界无法任意更改对象内部的数据\n- 继承（Inheritance）：子类继承父类，子类除了拥有父类的所有特性外，还有一些更具体的特性\n- 多态（Polymorphism）：由继承而产生了相关的不同的类，对同一个方法可以有不同的响应。比如 `Cat` 和 `Dog` 都继承自 `Animal`，但是分别实现了自己的 `eat` 方法。此时针对某一个实例，我们无需了解它是 `Cat` 还是 `Dog`，就可以直接调用 `eat` 方法，程序会自动判断出来应该如何执行 `eat`\n- 存取器（getter & setter）：用以改变属性的读取和赋值行为\n- 修饰符（Modifiers）：修饰符是一些关键字，用于限定成员或类型的性质。比如 `public` 表示公有属性或方法\n- 抽象类（Abstract Class）：抽象类是供其他类继承的基类，抽象类不允许被实例化。抽象类中的抽象方法必须在子类中被实现\n- 接口（Interfaces）：不同类之间公有的属性或方法，可以抽象成一个接口。接口可以被类实现（implements）。一个类只能继承自另一个类，但是可以实现多个接口',5,2,4,1),
(379,'定义类(Class)','```tsx\nclass Greeter {\n   static cname: string = ''Greeter''; // 静态属性\n   greeting: string; // 成员属行\n\n    //构造函数 会在对象创建时调用\n   constructor(message: string) { // 构造函数 - 执行初始化操作\n     this.greeting = message; //通过 this 向新建的对象中添加属性\n   }\n    static getClassName() { // 静态方法\n      return ''Class name is Greeter'';\n    }\n    greet() { // 成员方法\n      return \"Hello, \" + this.greeting;\n    }\n}\n//定义实例属性   需要通过对象的实例去访问\nlet greeter = new Greeter(\"world\");\n```\n',5,2,4,1),
(380,'类的继承(Class)','使用 `extends` 关键字实现继承，子类中使用 `super` 关键字来调用父类的构造函数和方法。\n\n```tsx\nclass Cat extends Animal {\n  constructor(name) {\n     // 调用父类的 constructor(name)\n    super(name); \n    console.log(this.name);\n  }\n  sayHi() {\n    return ''Meow, '' + super.sayHi(); // 调用父类的 sayHi()\n  }\n}\n\nlet c = new Cat(''Tom''); // Tom\nconsole.log(c.sayHi()); // Meow, My name is Tom\n```\n',5,2,4,1),
(381,'方法重写(Class)','类继承后，子类可以对父类的方法重新定义，这个过程称之为方法的重写。\n\n其中 super 关键字是对父类的直接引用，该关键字可以引用父类的属性和方法。\n\n```tsx\nclass PrinterClass { \n   doPrint():void {\n      console.log(\"父类的 doPrint() 方法。\") \n   } \n} \nclass StringPrinter extends PrinterClass { \n   doPrint():void { \n      super.doPrint() // 调用父类的函数\n      console.log(\"子类的 doPrint()方法。\")\n   } \n}\n```\n',5,2,4,1),
(382,'属性存取器(Class)','- 对于一些不希望被任意修改的属性，可以将其设置为 `private`，直接将其设置为 `private` 将导致无法再通过对象修改其中的属性\n\n使用 getter 和 setter 可以改变属性的赋值和读取行为：\n\n```js\nclass Animal {\n  constructor(name) {\n    this.name = name;\n  }\n  get name() {\n    return ''Jack'';\n  }\n  set name(value) {\n    console.log(''setter: '' + value);\n  }\n}\n\nlet a = new Animal(''Kitty''); // setter: Kitty\na.name = ''Tom''; // setter: Tom\nconsole.log(a.name); // Jack\n```',5,2,4,1),
(383,'静态方法(Class)','使用 `static` 修饰符修饰的方法称为静态方法，它们不需要实例化，而是直接通过类来调用：\n\n```tsx\nclass Animal {\n  static isAnimal(a) {\n    return a instanceof Animal;\n  }\n}\n\nlet a = new Animal(''Jack'');\nAnimal.isAnimal(a); // true\na.isAnimal(a); // TypeError: a.isAnimal is not a function\n```\n',5,2,4,1),
(384,'实例属性(Class)','ES6 中实例的属性只能通过构造函数中的 `this.xxx` 来定义，ES7 提案中可以直接在类里面定义：\n\n```js\nclass Animal {\n  name = ''Jack'';\n\n  constructor() {\n    // ...\n  }\n}\n\nlet a = new Animal();\nconsole.log(a.name); // Jack\n```\n',5,2,4,1),
(385,'静态属性(Class)','ES7 提案中，可以使用 `static` 定义一个静态属性：\n\n```tsx\nclass Animal {\n  static num = 42;\n\n  constructor() {\n    // ...\n  }\n}\n\nconsole.log(Animal.num); // 42\n```\n\n',5,2,4,1),
(386,'类和接口','类可以实现接口，使用关键字 implements，并将 interest 字段作为类的属性使用。\n\n以下实例红 AgriLoan 类实现了 ILoan 接口：\n\n```tsx\ninterface ILoan { \n   interest:number \n} \nclass AgriLoan implements ILoan { \n   interest:number \n   rebate:number \n   constructor(interest:number,rebate:number) { \n      this.interest = interest \n      this.rebate = rebate \n   } \n} \n \nvar obj = new AgriLoan(10,1) \nconsole.log(\"利润为 : \"+obj.interest+\"，抽成为 : \"+obj.rebate )\n```\n\n一个类可以实现多个接口：\n\n```tsx\ninterface Alarm {\n    alert(): void;\n}\ninterface Light {\n    lightOn(): void;\n    lightOff(): void;\n}\n\nclass Car implements Alarm, Light {\n    alert() {\n        console.log(''Car alert'');\n    }\n    lightOn() {\n        console.log(''Car light on'');\n    }\n    lightOff() {\n        console.log(''Car light off'');\n    }\n}\n```\n\n上例中，`Car` 实现了 `Alarm` 和 `Light` 接口，既能报警，也能开关车灯。',5,2,4,1),
(387,'访问修饰符','分别是 `public`、`private` 和 `protected`。\n\n- `public` 修饰的属性或方法是公有的，可以在任何地方被访问到，默认所有的属性和方法都是 `public` 的\n- `private` 修饰的属性或方法是私有的，不能在声明它的类的外部访问\n- `protected` 修饰的属性或方法是受保护的，它和 `private` 类似，区别是它在子类中也是允许被访问的\n\n一些例子：\n\n```tsx\nclass Animal {\n  public name;\n  public constructor(name) {\n    this.name = name;\n  }\n}\n\nlet a = new Animal(''Jack'');\nconsole.log(a.name); // Jack\na.name = ''Tom'';\nconsole.log(a.name); // Tom\n```\n\n上面的例子中，`name` 被设置为了 `public`，所以直接访问实例的 `name` 属性是允许的。',5,2,4,1),
(389,'访问修饰符(private)','很多时候，我们希望有的属性是无法直接存取的，这时候就可以用 `private` 了：\n\n```tsx\nclass Animal {\n  private name;\n  public constructor(name) {\n    this.name = name;\n  }\n}\n\nlet a = new Animal(''Jack'');\nconsole.log(a.name);\na.name = ''Tom'';\n\n// index.ts(9,13): error TS2341: Property ''name'' is private and only accessible within class ''Animal''.\n// index.ts(10,1): error TS2341: Property ''name'' is private and only accessible within class ''Animal''.\n```\n\n需要注意的是，TypeScript 编译之后的代码中，并没有限制 `private` 属性在外部的可访问性。\n\n使用 `private` 修饰的属性或方法，在子类中也是不允许访问的：\n\n```tsx\nclass Animal {\n  private name;\n  public constructor(name) {\n    this.name = name;\n  }\n}\nclass Cat extends Animal {\n  constructor(name) {\n    super(name);\n    console.log(this.name);\n  }\n}\n// index.ts(11,17): error TS2341: Property ''name'' is private and only accessible within class ''Animal''.\n```\n',5,2,4,1),
(390,'访问修饰符(protected)','而如果是用 `protected` 修饰，则允许在子类中访问：\n\n```ts\nclass Animal {\n  protected name;\n  public constructor(name) {\n    this.name = name;\n  }\n}\nclass Cat extends Animal {\n  constructor(name) {\n    super(name);\n    console.log(this.name);\n  }\n}\n```\n\n当构造函数修饰为 `private` 时，该类不允许被继承或者实例化：\n\n```tsx\nclass Animal {\n  public name;\n  private constructor(name) {\n    this.name = name;\n  }\n}\nclass Cat extends Animal {\n  constructor(name) {\n    super(name);\n  }\n}\nlet a = new Animal(''Jack'');\n// index.ts(7,19): TS2675: Cannot extend a class ''Animal''. Class constructor is marked as private.\n// index.ts(13,9): TS2673: Constructor of class ''Animal'' is private and only accessible within the class declaration.\n```\n\n当构造函数修饰为 `protected` 时，该类只允许被继承：\n\n```tsx\nclass Animal {\n  public name;\n  protected constructor(name) {\n    this.name = name;\n  }\n}\nclass Cat extends Animal {\n  constructor(name) {\n    super(name);\n  }\n}\nlet a = new Animal(''Jack'');\n// index.ts(13,9): TS2674: Constructor of class ''Animal'' is protected and only accessible within th\n```\n\n',5,2,4,1),
(391,'参数属性','修饰符和`readonly`还可以使用在构造函数参数中，等同于类中定义该属性同时给该属性赋值，使代码更简洁。\n\n```tsx\nclass Animal {\n  // public name: string;\n  public constructor(public name) {\n    // this.name = name;\n  }\n}\n```\n\n',5,2,4,1),
(392,'只读(readonly)','只读属性关键字，只允许出现在属性声明或索引签名或构造函数中。\n\n```tsx\nclass Animal {\n  readonly name;\n  public constructor(name) {\n    this.name = name;\n  }\n}\n\nlet a = new Animal(''Jack'');\nconsole.log(a.name); // Jack\na.name = ''Tom'';\n\n// index.ts(10,3): TS2540: Cannot assign to ''name'' because it is a read-only property.\n```\n\n注意如果 `readonly` 和其他访问修饰符同时存在的话，需要写在其后面。\n\n```tsx\nclass Animal {\n  // public readonly name;\n  public constructor(public readonly name) {\n    // this.name = name;\n  }\n}\n```\n',5,2,4,1),
(393,'抽象类','`abstract` 用于定义抽象类和其中的抽象方法。\n\n什么是抽象类？\n\n首先，抽象类是不允许被实例化的：\n\n```ts\nabstract class Animal {\n  public name;\n  public constructor(name) {\n    this.name = name;\n  }\n  public abstract sayHi();\n}\n\nlet a = new Animal(''Jack'');\n// index.ts(9,11): error TS2511: Cannot create an instance of the abstract class ''Animal''.\n```\n\n上面的例子中，我们定义了一个抽象类 `Animal`，并且定义了一个抽象方法 `sayHi`。在实例化抽象类的时候报错了。\n\n其次，抽象类中的抽象方法必须被子类实现：\n\n```ts\nabstract class Animal {\n  public name;\n  public constructor(name) {\n    this.name = name;\n  }\n  public abstract sayHi();\n}\n\nclass Cat extends Animal {\n  public eat() {\n    console.log(`${this.name} is eating.`);\n  }\n}\n\nlet cat = new Cat(''Tom'');\n// index.ts(9,7): error TS2515: Non-abstract class ''Cat'' does not implement inherited abstract member ''sayHi'' from class ''Animal''.\n```\n\n上面的例子中，我们定义了一个类 `Cat` 继承了抽象类 `Animal`，但是没有实现抽象方法 `sayHi`，所以编译报错了。\n\n下面是一个正确使用抽象类的例子：\n\n```ts\nabstract class Animal {\n  public name;\n  public constructor(name) {\n    this.name = name;\n  }\n  public abstract sayHi();\n}\nclass Cat extends Animal {\n  public sayHi() {\n    console.log(`Meow, My name is ${this.name}`);\n  }\n}\nlet cat = new Cat(''Tom'');\n```\n\n上面的例子中，我们实现了抽象方法 `sayHi`，编译通过了。',5,2,4,1),
(394,'类的类型(Class)','给类加上 TypeScript 的类型很简单，与接口类似：\n\n```tsX\nclass Animal {\n  name: string;\n  constructor(name: string) {\n    this.name = name;\n  }\n  sayHi(): string {\n    return `My name is ${this.name}`;\n  }\n}\nlet a: Animal = new Animal(''Jack'');\nconsole.log(a.sayHi()); // My name is Jack\n```\n\n',5,2,4,1),
(395,'接口(interface)','interface 接口是一系列抽象方法的声明，是一些方法特征的集合，这些方法都应该是抽象的，需要由具体的类去实现，然后第三方就可以通过这组抽象方法调用，让具体的类执行具体的方法。\n\n### 基础示例\n\n接口一般首字母大写\n\n定义的变量比接口少了一些属性是不允许的\n\n多一些属性也是不允许的\n\n```tsx\ninterface Person {\n    name: string;\n    age: number;\n}\nlet tom: Person = {\n    name: ''Tom'',\n    age: 25\n};\n```\n\n定义了一个接口 `Person`，接着定义了一个变量 `tom`，它的类型是 `Person`。这样，我们就约束了 `tom` 的形状必须和接口 `Person` 一致。',5,2,4,1),
(396,'可选属性(interface)','有时我们希望不要完全匹配一个形状\n\n```ts\ninterface Person {\n    name: string;\n    age?: number;\n}\n\nlet tom: Person = {\n    name: ''Tom''\n};\n\ninterface Person {\n    name: string;\n    age?: number;\n}\nlet tom: Person = {\n    name: ''Tom'',\n    age: 25\n};\n```\n\n可选属性的含义是该属性可以不存在。\n\n这时**仍然不允许添加未定义的属性**',5,2,4,1),
(397,'任意属性(interface)','有时候我们希望一个接口允许有任意的属性，可以使用如下方式：\n\n```ts\ninterface Person {\n    name: string;\n    age?: number;\n    [propName: string]: any;\n}\n\nlet tom: Person = {\n    name: ''Tom'',\n    gender: ''male''\n};\n```\n\n使用 `[propName: string]` 定义了任意属性取 `string` 类型的值。\n\n\n\n一个接口中只能定义一个任意属性。如果接口中有多个类型的属性，则可以在任意属性中使用联合类型：\n\n```ts\ninterface Person {\n    name: string;\n    age?: number;\n    [propName: string]: string | number;\n}\n\nlet tom: Person = {\n    name: ''Tom'',\n    age: 25,\n    gender: ''male''\n};\n```\n\n',5,2,4,1),
(398,'只读属性(interface)','有时候我们希望对象中的一些字段只能在创建的时候被赋值，那么可以用 `readonly` 定义只读属性：\n\n```tsx\ninterface Person {\n    readonly id: number;\n    name: string;\n    age?: number;\n    [propName: string]: any;\n}\n\nlet tom: Person = {\n    id: 89757,\n    name: ''Tom'',\n    gender: ''male''\n};\n\ntom.id = 9527;\n\n// index.ts(14,5): error TS2540: Cannot assign to ''id'' because it is a constant or a read-only property.\n```\n\n上例中，使用 `readonly` 定义的属性 `id` 初始化后，又被赋值了，所以报错了。',5,2,4,1),
(399,'函数类型(interface)','为了使用接口表示函数类型，我们需要给接口定义一个调用签名。 它就像是一个只有参数列表和返回值类型的函数定义。参数列表里的每个参数都需要名字和类型。\n\n```tsx\ninterface SearchFunc {\n  (source: string, subString: string): boolean;\n}\n```\n\n下例展示了如何创建一个函数类型的变量，并将一个同类型的函数赋值给这个变量。\n\n```tsx\nlet mySearch: SearchFunc;\nmySearch = function(source: string, subString: string) {\n  let result = source.search(subString);\n  return result > -1;\n};\n```\n',5,2,4,1),
(400,'可索引的类型(interface)','与使用接口描述函数类型差不多，我们也可以描述那些能够 “通过索引得到” 的类型，比如 `a[10]` 或 `ageMap[\"daniel\"]` 。 可索引类型具有一个_索引签名_，它描述了对象索引的类型，还有相应的索引返回值类型。 让我们看一个例子：\n\n```tsx\ninterface StringArray {\n  [index: number]: string;\n}\n\nlet myArray: StringArray;\nmyArray = [\"Bob\", \"Fred\"];\n\nlet myStr: string = myArray[0];\n```\n\n上面例子里，我们定义了 `StringArray` 接口，它具有索引签名。 这个索引签名表示了当用 `number` 去索引 `StringArray` 时会得到 `string` 类型的返回值。\n\nTypescript 支持两种索引签名：字符串和数字。 可以同时使用两种类型的索引，但是数字索引的返回值必须是字符串索引返回值类型的子类型',5,2,4,1),
(401,'混合类型(interface)','先前我们提过，接口能够描述 JavaScript 里丰富的类型。 因为 JavaScript 其动态灵活的特点，有时你会希望一个对象可以同时具有上面提到的多种类型。\n\n一个例子就是，一个对象可以同时作为函数和对象使用，并带有额外的属性。\n\n```tsx\ninterface Counter {\n  (start: number): string;\n  interval: number;\n  reset(): void;\n}\n\nfunction getCounter(): Counter {\n  let counter = function(start: number) {} as Counter;\n  counter.interval = 123;\n  counter.reset = function() {};\n  return counter;\n}\n\nlet c = getCounter();\nc(10);\nc.reset();\nc.interval = 5.0;\n```\n\n在使用 JavaScript 第三方库的时候，你可能需要像上面那样去完整地定义类型。',5,2,4,1),
(402,'接口继承(interface)','接口继承就是说接口可以通过其他接口来扩展自己。\n\nTypescript 允许接口继承多个接口。\n\n继承使用关键字 **extends**。\n\n单接口继承语法格式：\n\n```\nChild_interface_name extends super_interface_name\n```\n\n多接口继承语法格式：\n\n```\nChild_interface_name extends super_interface1_name, super_interface2_name,…,super_interfaceN_name\n```\n\n继承的各个接口使用逗号 **,** 分隔。',5,2,4,1),
(403,'单继承实例(interface)','```tsx\ninterface Person { \nage:number \n}   \ninterface Musician extends Person { \ninstrument:string \n}  \nvar drummer = <Musician>{};\ndrummer.age = 27  \ndrummer.instrument = \"Drums\"\nconsole.log(\"年龄:  \"+drummer.age) \nconsole.log(\"喜欢的乐器:  \"+drummer.instrument)\n```\n',5,2,4,1),
(404,'多继承实例(interface)','```tsx\ninterface IParent1 { \n    v1:number \n} \ninterface IParent2 { \n    v2:number \n} \ninterface Child extends IParent1, IParent2 { } \nvar Iobj:Child = { v1:12, v2:23} \nconsole.log(\"value 1: \"+Iobj.v1+\" value 2: \"+Iobj.v2)\n```\n',5,2,4,1),
(405,'接口定义函数(interface)','接口不仅可以定义对象, 还可以定义函数:\n\n```tsx\n// 声明接口\ninterface Core {\n    (n:number, s:string):[number,string]\n}\n\n// 声明函数遵循接口定义\nconst core:Core = (a,b)=>{\n    return [a,b];\n}\n```\n',5,2,4,1),
(406,'接口定义类(interface)','先简单看下如何给类定义接口, 后面的课程具体讲类:\n\n```tsx\n// 定义\ninterface Animate {\n    head:number;\n    body:number;\n    foot:number;\n    eat(food:string):void;\n    say(word:string):string;\n}\n```\n\n```tsx\n// implements\nclass Dog implements Animate{\n    head=1;\n    body=1;\n    foot=1;\n    eat(food){\n        console.log(food);\n    }\n    say(word){\n        return word;\n    }\n}\n```\n\n',5,2,4,1),
(407,'类型声明','使用 **类型声明** 来描述一个对象的类型\n\n```tsx\ntype myType = {\n  name: string;\n  age: number;\n};\nconst person1: myType = {\n  name: ''hzw'',\n  age: 18,\n};\n```\n\n类型声明不可以重复写 接口可以重复写,内容会自动合并',5,2,4,1),
(408,'instanceof','instanceof 运算符用于判断对象是否是指定的类型，如果是返回 true，否则返回 false。\n\n```tsx\nclass Person{ } \nvar obj = new Person() \nvar isPerson = obj instanceof Person; \nconsole.log(\"obj 对象是 Person 类实例化来的吗？ \" + isPerson);\n```\n\n',5,2,4,1),
(409,'typeof','typeof 操作符可以用来获取一个变量或对象的类型\n\n```tsx\ninterface Hero {\n  name: string;\n  skill: string;\n}\n\nconst zed: Hero = { name: \"影流之主\", skill: \"影子\" };\ntype LOL = typeof zed; // type LOL = Hero\n```\n\n在上面代码中，我们通过 typeof 操作符获取 zed 变量的类型并赋值给 LOL 类型变量，之后我们就可以使用 LOL 类型\n\n```tsx\nconst ahri: LOL = { name: \"阿狸\", skill: \"魅惑\" };\n```\n',5,2,4,1),
(410,'keyof','keyof 与 Object.keys 略有相似，只不过 keyof 取 interface 的键\n\n```tsx\ninterface Point {\n    x: number;\n    y: number;\n}\n\n// type keys = \"x\" | \"y\"\ntype keys = keyof Point;\n```\n\n用 keyof 可以更好的定义数据类型\n\n```js\nfunction get<T extends object, K extends keyof T>(o: T, name: K): T[K] {\n  return o[name]\n}\n```\n\n',5,2,4,1),
(411,'Partial<T>','将T中所有属性转换为可选属性。返回的类型可以是T的任意子集\n\n```tsx\nexport interface UserModel {\n  name: string;\n  age?: number;\n  sex: number;\n}\n\ntype JUserModel = Partial<UserModel>\n// =\ntype JUserModel = {\n    name?: string | undefined;\n    age?: number | undefined;\n    sex?: number | undefined;\n}\n\n// 源码解析\ntype Partial<T> = { [P in keyof T]?: T[P]; };\n```\n\n',5,3,4,1),
(412,'Required<T>','通过将T的所有属性设置为必选属性来构造一个新的类型。与Partial相反\n\n```tsx\ntype JUserModel2 = Required<UserModel>\n// =\ntype JUserModel2 = {\n    name: string;\n    age: number;\n    sex: number;\n}\n```\n\n',5,3,4,1),
(413,'Readonly<T>','将T中所有属性设置为只读\n\n```tsx\ntype JUserModel3 = Readonly<UserModel>\n\n// =\ntype JUserModel3 = {\n    readonly name: string;\n    readonly age?: number | undefined;\n    readonly sex: number;\n}\n```\n',5,3,4,1),
(414,'Record<K,T>','构造一个类型，该类型具有一组属性K，每个属性的类型为T。可用于将一个类型的属性映射为另一个类型。Record 后面的泛型就是对象键和值的类型。\n\n简单理解：K对应对应的key，T对应对象的value，返回的就是一个声明好的对象\n\n```tsx\ntype TodoProperty = ''title'' | ''description'';\ntype Todo = Record<TodoProperty, string>;\n// =\ntype Todo = {\n    title: string;\n    description: string;\n}\n\ninterface IGirl {\n  name: string;\n  age: number;\n}\n\ntype allGirls = Record<string, IGirl>\n```\n\n',5,3,4,1),
(415,'Pick<T,K>','在一个声明好的对象中，挑选一部分出来组成一个新的声明对象\n\n```tsx\ninterface Todo {\n  title: string;\n  description: string;\n  done: boolean;\n}\ntype TodoBase = Pick<Todo, \"title\" | \"done\">;\n// =\ntype TodoBase = {\n    title: string;\n    done: boolean;\n}\n```\n',5,3,4,1),
(416,'Exclude<T,U>','从T中排除可分配给U的属性，剩余的属性构成新的类型\n\n```tsx\ntype T0 = Exclude<''a'' | ''b'' | ''c'', ''a''>; \n\n// = \n\ntype T0 = \"b\" | \"c\"\n```\n',5,3,4,1),
(417,'Extract<T,U>','从T中抽出可分配给U的属性构成新的类型。与Exclude相反\n\n```tsx\ntype T0 = Extract<''a'' | ''b'' | ''c'', ''a''>; \n\n// = \n\ntype T0 = ''a''\n```\n',5,3,4,1),
(418,'Parameters<T>','返回类型为T的函数的参数类型所组成的数组\n\n```tsx\ntype T0 = Parameters<() => string>;  // []\n\ntype T1 = Parameters<(s: string) => void>;  // [string]\n```\n\n',5,3,4,1),
(419,'ReturnType<T>','function T的返回类型\n\n```tsx\ntype T0 = ReturnType<() => string>;  // string\n\ntype T1 = ReturnType<(s: string) => void>;  // void\n```\n\n',5,3,4,1),
(420,'InstanceType<T>','返回构造函数类型T的实例类型\n\n```tsx\nclass C {\n  x = 0;\n  y = 0;\n}\n\ntype T0 = InstanceType<typeof C>;  // C\n```\n',5,3,4,1),
(421,'字符串字面量类型','### 字符串字面量类型\n\n字符串字面量类型用来约束取值只能是某几个字符串中的一个。\n\n```ts\ntype EventNames = ''click'' | ''scroll'' | ''mousemove'';\nfunction handleEvent(ele: Element, event: EventNames) {\n    // do something\n}\n\nhandleEvent(document.getElementById(''hello''), ''scroll'');  // 没问题\nhandleEvent(document.getElementById(''world''), ''dblclick''); // 报错，event 不能为 ''dblclick''\n\n// index.ts(7,47): error TS2345: Argument of type ''\"dblclick\"'' is not assignable to parameter of type ''EventNames''.\n```\n\n上例中，使用 `type` 定了一个字符串字面量类型 `EventNames`，它只能取三种字符串中的一种。\n\n注意，**类型别名与字符串字面量类型都是使用 `type` 进行定义。**',5,2,4,1),
(422,'泛型(Generics)','泛型（Generics）是指在定义函数、接口或类的时候，不预先指定具体的类型，而在使用的时候再指定类型的一种特性。相比于使用 any 类型，使用泛型来创建可复用的组件要更好，因为泛型会保留参数类型。',5,2,4,1),
(423,'泛型接口(Generics)','```tsx\ninterface GenericIdentityFn<T> {\n    (arg: T): T;\n}\n```\n\n',5,2,4,1),
(424,'泛型类(Generics)','```tsx\nclass GenericNumber<T> {\n    zeroValue: T;\n    add: (x: T, y: T) => T;\n}\n\nlet myGenericNumber = new GenericNumber<number>();\nmyGenericNumber.zeroValue = 0;\nmyGenericNumber.add = function(x, y) { return x + y; };\n```\n\n### 使用示例\n\n```tsx\nfunction createArray<T>(length: number, value: T): Array<T> {\n    let result: T[] = [];\n    for (let i = 0; i < length; i++) {\n        result[i] = value;\n    }\n    return result;\n}\ncreateArray<string>(3, ''x''); // [''x'', ''x'', ''x'']\n\n// T 自定义名称\nfunction myFun<T>(params: T[]) {\n  return params;\n}\nmyFun <string> ([\"123\", \"456\"]);\n\n// 定义多个泛型\nfunction join<T, P>(first: T, second: P) {\n  return `${first}${second}`;\n}\njoin <number, string> (1, \"2\");\n```\n\n上例中，我们在函数名后添加了 <T>，其中 T 用来指代任意输入的类型，在后面的输入 value: T 和输出 Array<T> 中即可使用了\n\n',5,2,4,1),
(425,'多个类型参数(Generics)','定义泛型的时候，可以一次定义多个类型参数：\n\n```tsx\nfunction swap<T, U>(tuple: [T, U]): [U, T] {\n    return [tuple[1], tuple[0]];\n}\nswap([7, ''seven'']); // [''seven'', 7]\n```\n\n上例中，我们定义了一个 `swap` 函数，用来交换输入的元组',5,2,4,1),
(426,'泛型约束(Generics)','在函数内部使用泛型变量的时候，由于事先不知道它是哪种类型，所以不能随意的操作它的属性或方法：\n\n泛型进行约束\n\n```ts\ninterface Lengthwise {\n    length: number;\n}\n\nfunction loggingIdentity<T extends Lengthwise>(arg: T): T {\n    console.log(arg.length);\n    return arg;\n}\n```\n\n上例中，我们使用了 `extends` 约束了泛型 `T` 必须符合接口 `Lengthwise` 的形状，也就是必须包含 `length` 属性。\n\n\n\n多个类型参数之间也可以互相约束：\n\n```ts\nfunction copyFields<T extends U, U>(target: T, source: U): T {\n    for (let id in source) {\n        target[id] = (<T>source)[id];\n    }\n    return target;\n}\nlet x = { a: 1, b: 2, c: 3, d: 4 };\ncopyFields(x, { b: 10, d: 20 });\n```\n\n上例中，我们使用了两个类型参数，其中要求 `T` 继承 `U`，这样就保证了 `U` 上不会出现 `T` 中不存在的字段。',5,2,4,1),
(427,'泛型参数的默认类型(Generics)','TypeScript 2.3 以后，可以为泛型中的类型参数指定默认类型。当使用泛型时没有在代码中直接指定类型参数，从实际值参数中也无法推测出时，这个默认类型就会起作用。\n\n```ts\nfunction createArray<T = string>(length: number, value: T): Array<T> {\n    let result: T[] = [];\n    for (let i = 0; i < length; i++) {\n        result[i] = value;\n    }\n    return result;\n}\n```\n\n',5,2,4,1),
(428,'泛型继承(Generics)','也可以对泛型的范围进行约束\n\n使用 `T extends MyInter` 表示泛型 `T` 必须是 `MyInter` 的子类，不一定非要使用接口类和抽象类同样适用；\n\n```tsx\ninterface MyInter{\n  length: number;\n}\n\nfunction test<T extends MyInter>(arg: T): number{\n  return arg.length;\n}\n```\n',5,2,4,1),
(429,'模块','TypeScript 代码必须使用路径进行导入。这里的路径既可以是相对路径，以 `.` 或 `..` 开头，也可以是从项目根目录开始的绝对路径，如 `root/path/to/file` 。\n\n```tsx\nimport {Symbol1} from ''google3/path/from/root'';\nimport {Symbol2} from ''../parent/file'';\nimport {Symbol3} from ''./sibling'';\n```\n\n',5,2,4,1),
(430,'导出','代码中必须使用具名的导出声明。\n\n```tsx\n// 1\nexport class Foo { ... }\n```\n\n```tsx\n// 2\nconst someVar = 123;\ntype someType = {\n  type: string;\n};\nexport { someVar, someType };\n```\n\n也可以用重命名变量的方式导出：\n\n```tsx\n// foo.ts\nconst someVar = 123;\nexport { someVar as aDifferentName };\n```\n\n#### 导出可见性\n\nTypeScript 不支持限制导出符号的可见性。因此，不要导出不用于模块以外的符号。一般来说，应当尽量减小模块的外部 API 的规模。\n\n#### 可变导出\n\n虽然技术上可以实现，但是可变导出会造成难以理解和调试的代码，尤其是对于在多个模块中经过了多次重新导出的符号。这条规则的一个例子是，不允许使用 `export let` 。\n\n```tsx\n// 不要这样做！\nexport let foo = 3;\n// 在纯 ES6 环境中，变量 foo 是一个可变值，导入了 foo 的代码会观察到它的值在一秒钟之后发生了改变。\n// 在 TypeScript 中，如果 foo 被另一个文件重新导出了，导入该文件的代码则不会观察到变化。\nwindow.setTimeout(() => {\n    foo = 4;\n}, 1000 /* ms */);\n```\n\n如果确实需要允许外部代码对可变值进行访问，应当提供一个显式的取值器。\n\n```tsx\n// 应当这样做！\nlet foo = 3;\nwindow.setTimeout(() => {\n    foo = 4;\n}, 1000 /* ms */);\n// 使用显式的取值器对可变导出进行访问。\nexport function getFoo() { return foo; };\n```\n\n有一种常见的编程情景是，要根据某种特定的条件从两个值中选取其中一个进行导出：先检查条件，然后导出。这种情况下，应当保证模块中的代码执行完毕后，导出的结果就是确定的。\n\n```tsx\nfunction pickApi() {\n    if (useOtherApi()) return OtherApi;\n    return RegularApi;\n}\nexport const SomeApi = pickApi();\n```\n\n',5,2,4,1),
(431,'容器类','不要为了实现命名空间创建含有静态方法或属性的容器类。\n\n```tsx\n// 不要这样做！\nexport class Container {\n    static FOO = 1;\n    static bar() { return 1; }\n}\n```\n\n应当将这些方法和属性设为单独导出的常数和函数。\n\n```tsx\n// 应当这样做！\nexport const FOO = 1;\nexport function bar() { return 1; }\n```\n\n',5,2,4,1),
(432,'导入','在 ES6 和 TypeScript 中，导入语句共有四种变体：\n\n| 导入类型 | 示例                             | 用途                                       |\n| -------- | -------------------------------- | ------------------------------------------ |\n| 模块     | `import * as foo from ''...'';`    | TypeScript 导入方式                        |\n| 解构     | `import {SomeThing} from ''...'';` | TypeScript 导入方式                        |\n| 默认     | `import SomeThing from ''...'';`   | 只用于外部代码的特殊需求                   |\n| 副作用   | `import ''...'';`                  | 只用于加载某些库的副作用（例如自定义元素） |\n\n```tsx\n// 应当这样做！从这两种变体中选择较合适的一种（见下文）。\nimport * as ng from ''@angular/core'';\nimport {Foo} from ''./foo'';\nimport { someVar as aDifferentName } from ''./foo'';\n\n// 只在有需要时使用默认导入。\nimport Button from ''Button'';\n\n// 有时导入某些库是为了其代码执行时的副作用。\nimport ''jasmine'';\nimport ''@polymer/paper-button'';\n```\n\n从其他模块导入后整体导出：\n\n```tsx\nexport * from ''./foo'';\n```\n\n从其他模块导入后，部分导出：\n\n```tsx\nexport { someVar } from ''./foo'';\n```\n\n通过重命名，部分导出从另一个模块导入的项目：\n\n```tsx\nexport { someVar as aDifferentName } from ''./foo'';\n```\n\n#### 选择模块导入还是解构导入？\n\n根据使用场景的不同，模块导入和解构导入分别有其各自的优势。\n\n虽然模块导入语句中出现了通配符 `*` ，但模块导入并不能因此被视为其它语言中的通配符导入。相反，模块导入语句为整个模块提供了一个名称，模块中的所有符号都通过这个名称进行访问，这为代码提供了更好的可读性，同时令模块中的所有符号可以进行自动补全。模块导入减少了导入语句的数量，降低了命名冲突的出现几率，同时还允许为被导入的模块提供一个简洁的名称。在从一个大型 API 中导入多个不同的符号时，模块导入语句尤其有用。\n\n解构导入语句则为每一个被导入的符号提供一个局部的名称，这样在使用被导入的符号时，代码可以更简洁。对那些十分常用的符号，例如 Jasmine 的 `describe` 和 `it` 来说，这一点尤其有用。\n\n```tsx\n// 不要这样做！无意义地使用命名空间中的名称使得导入语句过于冗长。\nimport {TableViewItem, TableViewHeader, TableViewRow, TableViewModel,\nTableViewRenderer} from ''./tableview'';\nlet item: TableViewItem = ...;\n\n// 应当这样做！使用模块作为命名空间。\nimport * as tableview from ''./tableview'';\nlet item: tableview.Item = ...;\n```\n\n#### 重命名导入\n\n在代码中，应当通过使用模块导入或重命名导出解决命名冲突。此外，在需要时，也可以使用重命名导入（例如 `import {SomeThing as SomeOtherThing}` ）。\n\n在以下几种情况下，重命名导入可能较为有用：\n\n1. 避免与其它导入的符号产生命名冲突。\n2. 被导入符号的名称是自动生成的。\n3. 被导入符号的名称不能清晰地描述其自身，需要通过重命名提高代码的可读性，如将 RxJS 的 `from` 函数重命名为 `observableFrom` 。\n\n#### 默认导入／导出\n\n- 使用\n\n  ```tsx\n  export default\n  ```\n\n  - 在一个变量之前（不需要使用 `let/const/var`）；\n  - 在一个函数之前；\n  - 在一个类之前。\n\n```ts\n// some var\nexport default (someVar = 123);\n\n// some function\nexport default function someFunction() {}\n\n// some class\nexport default class someClass {}\n```\n\n- 导入使用 `import someName from ''someModule''` 语法（你可以根据需要为导入命名）：\n\n```ts\nimport someLocalNameForThisFile from ''./foo'';\n```\n\n',5,2,4,1),
(433,'函数的合并','我们可以使用重载定义多个函数类型：\n\n```tsx\nfunction reverse(x: number): number;\nfunction reverse(x: string): string;\nfunction reverse(x: number | string): number | string {\n    if (typeof x === ''number'') {\n        return Number(x.toString().split('''').reverse().join(''''));\n    } else if (typeof x === ''string'') {\n        return x.split('''').reverse().join('''');\n    }\n}\n```\n',5,2,4,1),
(434,'接口的合并','接口中的属性在合并时会简单的合并到一个接口中：\n\n```tsx\ninterface Alarm {\n    price: number;\n}\ninterface Alarm {\n    weight: number;\n}\n```\n\n相当于：\n\n```tsx\ninterface Alarm {\n    price: number;\n    weight: number;\n}\n```\n\n注意，**合并的属性的类型必须是唯一的**：\n\n```tsx\ninterface Alarm {\n    price: number;\n}\ninterface Alarm {\n    price: number;  // 虽然重复了，但是类型都是 `number`，所以不会报错\n    weight: number;\n}\ninterface Alarm {\n    price: number;\n}\ninterface Alarm {\n    price: string;  // 类型不一致，会报错\n    weight: number;\n}\n\n// index.ts(5,3): error TS2403: Subsequent variable declarations must have the same type.  Variable ''price'' must be of type ''number'', but here has type ''string''.\n```\n\n接口中方法的合并，与函数的合并一样：\n\n```tsx\ninterface Alarm {\n    price: number;\n    alert(s: string): string;\n}\ninterface Alarm {\n    weight: number;\n    alert(s: string, n: number): string;\n}\n```\n\n相当于：\n\n```tsx\ninterface Alarm {\n    price: number;\n    weight: number;\n    alert(s: string): string;\n    alert(s: string, n: number): string;\n}\n```\n',5,2,4,1),
(435,'类的合并','类的合并与接口的合并规则一致',5,2,4,1),
(436,'tsconfig.json','### tsconfig.json 的作用\n\n- 用于标识 TypeScript 项目的根路径；\n- 用于配置 TypeScript 编译器；\n- 用于指定编译的文件。\n\n### tsconfig.json 重要字段\n\n- files - 设置要编译的文件的名称；\n- include - 设置需要进行编译的文件，支持路径模式匹配；\n- exclude - 设置无需进行编译的文件，支持路径模式匹配；\n- compilerOptions - 设置与编译流程相关的选项。\n\ncompilerOptions 支持很多选项，常见的有 `baseUrl`、 `target`、`baseUrl`、 `moduleResolution` 和 `lib` 等。\n\n### 文件配置选项\n\n#### 1. include\n\n- 用来指定哪些目录下的ts文件需要被编译\n- 默认值：`[\"**/*\"]`\n- `**` 表示任意目录 \n- `*` 表示任意文件\n\n```js\n//表示根目录下src目录下任意目录任意文件\n\"include\": [\n  \"./src/**/*\" \n],\n```\n\n#### 2. exclude\n\n- 用来指定哪些目录下的 `ts` 文件不需要被编译\n- 默认值：`[\"node_modules\", \"bower_components\", \"jspm_packages\"]`\n\n```js\n//表示不编译根目录下src目录下test目录下任意目录任意文件\n\"exclude\":[\n  \"./src/test/**/*\"  \n],\n```\n\n#### 3. extends\n\n- 用来定义 **被继承的配置文件**\n- 引入后,当前配置文件中会自动包含引入文件中的所有配置信息\n\n```js\n//当前配置文件中会自动包含根目录下base.json中的所有配置信息\n\"extends\": \"./base.json\",\n```\n\n#### 4. files\n\n- 用来指定被编译的文件列表,需要把文件一个个列出来比较麻烦,只有需要编译的文件少时才会用到.\n\n```js\n //只会编译根目录下01目录下的hello.ts\n\"files\": [\"./01/hello.ts\"],\n```\n\n### compilerOptions\n\n- 编译选项是配置文件中非常重要也比较复杂的配置选项\n- 在 `compilerOptions` 中包含多个子选项，用来完成对编译的配置\n\n#### 1. target\n\n- 用来指定 `TS` 被编译为的 `ES` 版本\n- 可选值：`ES3`（**默认**）、`ES5`、`ES6/ES2015`、`ES7/ES2016`、`ES2017`、`ES2018`、`ES2019`、`ES2020`、`ESNext`(**最新版本的ES**)\n\n```js\n//我们所编写的ts代码将会被编译为ES6版本的js代码\n\"compilerOptions\": {\n    \"target\": \"ES6\"\n}\n复制代码\n```\n\n#### 2. module\n\n- 用来指定要使用的模块化的解决方案\n- 可选值：`CommonJS`、`UMD`、`AMD`、`System`、`ES2020`、`ESNext`、`None`\n\n```js\n\"compilerOptions\": {\n    \"module\": \"CommonJS\"\n}\n复制代码\n```\n\n#### 3. lib\n\n- 用来指定项目中要使用的库 一般浏览器情况下不需要设置\n- 可选值：`ES5`、`ES6/ES2015`、`ES7/ES2016`、`ES2017`、`ES2018`、`ES2019`、`ES2020`、`ESNext`、`DOM`、`WebWorker`、`ScriptHost` `......`\n\n```js\n\"compilerOptions\": {\n    \"lib\": [\"ES6\", \"DOM\"],\n}\n复制代码\n```\n\n#### 4. outDir,rootDir\n\n- `outDir` 用来指定编译后文件所在的目录\n- `rootDir` 用来指定代码的根目录\n\n- 默认情况下，编译后的 `js` 文件会和 `ts` 文件位于相同的目录，设置 `outDir` 后可以改变编译后文件的位置\n\n```js\n\"compilerOptions\": {\n    \"outDir\": \"./dist\",\n    \"rootDir\": \"./src\",\n}\n复制代码\n```\n\n#### 5. outFile\n\n- 将编译后的代码合并为一个文件\n- 设置 `outFile` 后,所有的全局作用域中的代码会合并到同一个文件中\n\n- 如果 `module` 制定了 `None`、`System` 或 `AMD` 则会将模块一起合并到文件之中\n\n```js\n\"compilerOptions\": {\n    \"outFile\": \"dist/app.js\"\n}\n复制代码\n```\n\n#### 6.allowJs\n\n- 是否对 `js` 文件进行编译,默认为 `false`\n\n#### 7.checkJs\n\n- 是否检测 `js` 代码是否符合语法规范,默认为 `false`\n\n#### 8.removeComments\n\n- 是否移除注释,默认为 `false`\n\n#### 9.noEmit\n\n- 是否不生成编译后的文件,默认为 `false`\n\n#### 10.noEmitOnError\n\n- 是否不生成编译后的文件(**当出现错误时**),默认为 `false`\n\n```js\n\"compilerOptions\": {\n     \"allowJs\": true,\n     \"checkJs\": true,\n     \"removeComments\": false,\n     \"noEmit\": false,\n     \"noEmitOnError\": true\n}\n```\n\n#### 11.严格检查\n\n- **strict**\n- - 启用所有的严格检查，默认值为 `true`，设置后相当于开启了所有的严格检查\n\n- **alwaysStrict**\n\n- - 总是以严格模式对代码进行编译\n\n- **noImplicitAny**\n\n- - 禁止隐式的 `any` 类型\n\n- **noImplicitThis**\n\n- - 禁止类型不明确的 `this`\n\n- **strictBindCallApply**\n\n- - 严格检查 `bind`、`call` 和 `apply` 的参数列表\n\n- **strictFunctionTypes**\n\n- - 严格检查函数的类型\n\n- **strictNullChecks**\n\n- - 严格的空值检查\n\n- **strictPropertyInitialization**\n\n- - 严格检查属性是否初始化\n\n#### 12.额外检查\n\n- **noFallthroughCasesInSwitch**\n\n- - 检查 `switch` 语句包含正确的 `break`\n\n- **noImplicitReturns**\n\n- - 检查函数没有隐式的返回值\n\n- **noUnusedLocals**\n\n- - 检查未使用的局部变量\n\n- **noUnusedParameters**\n\n- - 检查未使用的参数\n\n- **allowUnreachableCode**\n\n- - 检查不可达代码\n  - 可选值：`true`，忽略不可达代码;`false`，不可达代码将引起错误\n\n',5,2,4,1),
(437,'tsconfig.json示例','```javascript\n{\n  \"compilerOptions\": {\n    \"target\": \"esnext\",\n    \"module\": \"esnext\",\n    \"moduleResolution\": \"node\",\n    \"isolatedModules\": false,\n    \"strict\": true,\n    \"noUnusedLocals\": true,\n    \"noUnusedParameters\": true,\n    \"experimentalDecorators\": true,\n    \"allowSyntheticDefaultImports\": true,\n    \"skipLibCheck\": true,\n    \"types\": [\"vite/client\"],\n    \"baseUrl\": \".\",\n    \"lib\": [\"dom\", \"esnext\"],\n    \"paths\": {\n      \"@/*\": [\"src/*\"],\n      \"@comp/*\": [\"src/components/*\"],\n      \"@api/*\": [\"src/api/*\"],\n      \"@vi/*\": [\"src/views/*\"],\n      \"@h/*\": [\"src/hooks/*\"]\n    }\n  },\n  \"exclude\": [\"node_modules\", \"dist\"]\n}\n\n```\n\n### compilerOptions 选项\n\n```ts\n{\n  \"compilerOptions\": {\n\n    /* 基本选项 */\n    \"target\": \"es5\",                       // 指定 ECMAScript 目标版本: ''ES3'' (default), ''ES5'', ''ES6''/''ES2015'', ''ES2016'', ''ES2017'', or ''ESNEXT''\n    \"module\": \"commonjs\",                  // 指定使用模块: ''commonjs'', ''amd'', ''system'', ''umd'' or ''es2015''\n    \"lib\": [],                             // 指定要包含在编译中的库文件\n    \"allowJs\": true,                       // 允许编译 javascript 文件\n    \"checkJs\": true,                       // 报告 javascript 文件中的错误\n    \"jsx\": \"preserve\",                     // 指定 jsx 代码的生成: ''preserve'', ''react-native'', or ''react''\n    \"declaration\": true,                   // 生成相应的 ''.d.ts'' 文件\n    \"sourceMap\": true,                     // 生成相应的 ''.map'' 文件\n    \"outFile\": \"./\",                       // 将输出文件合并为一个文件\n    \"outDir\": \"./\",                        // 指定输出目录\n    \"rootDir\": \"./\",                       // 用来控制输出目录结构 --outDir.\n    \"removeComments\": true,                // 删除编译后的所有的注释\n    \"noEmit\": true,                        // 不生成输出文件\n    \"importHelpers\": true,                 // 从 tslib 导入辅助工具函数\n    \"isolatedModules\": true,               // 将每个文件做为单独的模块 （与 ''ts.transpileModule'' 类似）.\n\n    /* 严格的类型检查选项 */\n    \"strict\": true,                        // 启用所有严格类型检查选项\n    \"noImplicitAny\": true,                 // 在表达式和声明上有隐含的 any类型时报错\n    \"strictNullChecks\": true,              // 启用严格的 null 检查\n    \"noImplicitThis\": true,                // 当 this 表达式值为 any 类型的时候，生成一个错误\n    \"alwaysStrict\": true,                  // 以严格模式检查每个模块，并在每个文件里加入 ''use strict''\n\n    /* 额外的检查 */\n    \"noUnusedLocals\": true,                // 有未使用的变量时，抛出错误\n    \"noUnusedParameters\": true,            // 有未使用的参数时，抛出错误\n    \"noImplicitReturns\": true,             // 并不是所有函数里的代码都有返回值时，抛出错误\n    \"noFallthroughCasesInSwitch\": true,    // 报告 switch 语句的 fallthrough 错误。（即，不允许 switch 的 case 语句贯穿）\n\n    /* 模块解析选项 */\n    \"moduleResolution\": \"node\",            // 选择模块解析策略： ''node'' (Node.js) or ''classic'' (TypeScript pre-1.6)\n    \"baseUrl\": \"./\",                       // 用于解析非相对模块名称的基目录\n    \"paths\": {},                           // 模块名到基于 baseUrl 的路径映射的列表\n    \"rootDirs\": [],                        // 根文件夹列表，其组合内容表示项目运行时的结构内容\n    \"typeRoots\": [],                       // 包含类型声明的文件列表\n    \"types\": [],                           // 需要包含的类型声明文件名列表\n    \"allowSyntheticDefaultImports\": true,  // 允许从没有设置默认导出的模块中默认导入。\n\n    /* Source Map Options */\n    \"sourceRoot\": \"./\",                    // 指定调试器应该找到 TypeScript 文件而不是源文件的位置\n    \"mapRoot\": \"./\",                       // 指定调试器应该找到映射文件而不是生成文件的位置\n    \"inlineSourceMap\": true,               // 生成单个 soucemaps 文件，而不是将 sourcemaps 生成不同的文件\n    \"inlineSources\": true,                 // 将代码与 sourcemaps 生成到一个文件中，要求同时设置了 --inlineSourceMap 或 --sourceMap 属性\n\n    /* 其他选项 */\n    \"experimentalDecorators\": true,        // 启用装饰器\n    \"emitDecoratorMetadata\": true          // 为装饰器提供元数据的支持\n  }\n}\n```\n\n',5,3,4,1),
(438,'命名规范','| 命名法                                 | 分类                                   |\n| -------------------------------------- | -------------------------------------- |\n| 帕斯卡命名法（ `UpperCamelCase` ）     | 类、接口、类型、枚举、装饰器、类型参数 |\n| 驼峰式命名法（ `lowerCamelCase` ）     | 变量、参数、函数、方法、属性、模块别名 |\n| 全大写下划线命名法（ `CONSTANT_CASE`） | 全局常量、枚举值                       |\n| 私有成员命名法（ `#ident` ）           | 不允许使用                             |\n\n1. 不要使用`I`做为接口名前缀。\n2. 不要为私有属性名添加`_`前缀。\n3. 尽可能使用完整的单词拼写命名。',5,6,4,1),
(439,'导入模块','导入模块的命名空间时使用驼峰命名法（`lowerCamelCase`），文件名则使用蛇形命名法（`snake_case`）。例如：\n\n```ts\nimport * as fooBar from ''./foo_bar'';\n```\n\n',5,3,4,1),
(440,'常量','常量命名（`CONSTANT_CASE`）表示某个值不可被修改。它还可以用于虽然技术上可以实现，但是用户不应当试图修改的值，比如并未进行深度冻结（deep frozen）的值。\n\n```tsx\nconst UNIT_SUFFIXES = {\n    ''milliseconds'': ''ms'',\n    ''seconds'': ''s'',\n};\n// UNIT_SUFFIXES 使用了常量命名，\n// 这意味着用户不应试图修改它，\n// 即使它实际上是一个可变的值。\n```\n\n这里所说的常量，也包括类中的静态只读属性：\n\n```tsx\nclass Foo {\n    private static readonly MY_SPECIAL_NUMBER = 5;\n\n    bar() {\n        return 2 * Foo.MY_SPECIAL_NUMBER;\n    }\n}\n```\n',5,2,4,1),
(441,'组件','1. 1个文件对应一个逻辑组件 （比如：解析器，检查器）。\n2. 不要添加新的文件。\n3. `.generated.*`后缀的文件是自动生成的，不要手动改它。',5,6,4,1),
(442,'类型','1. 不要导出类型/函数，除非你要在不同的组件中共享它。\n2. 不要在全局命名空间内定义类型/值。\n3. 共享的类型应该在`types.ts`里定义。\n4. 在一个文件里，类型定义应该出现在顶部。',5,6,4,1),
(443,'错误提示信息','1. 在句子结尾使用`.`。\n2. 对不确定的实体使用不定冠词。\n3. 确切的实体应该使用名字（变量名，类型名等）\n4. 当创建一条新的规则时，主题应该使用单数形式（比如：An external module cannot...而不是External modules cannot）。\n5. 使用现在时态。',5,6,4,1),
(444,'错误提示信息代码','提示信息被划分类成了一般的区间。如果要新加一个提示信息，在上条代码上加1做为新的代码。\n\n- 1000 语法信息\n- 2000 语言信息\n- 4000 声明生成信息\n- 5000 编译器选项信息\n- 6000 命令行编译器信息\n- 7000 noImplicitAny信息',5,6,4,1),
(445,'普通方法','由于种种原因，我们避免使用一些方法，而使用我们自己定义的。\n\n1. 不使用ECMAScript 5函数；而是使用[core.ts](https://github.com/Microsoft/TypeScript/blob/master/src/compiler/core.ts)这里的。\n2. 不要使用`for..in`语句；而是使用`ts.forEach`，`ts.forEachKey`和`ts.forEachValue`。注意它们之间的区别。\n3. 如果可能的话，尝试使用`ts.forEach`，`ts.map`和`ts.filter`代替循环。\n\n',5,6,4,1),
(446,'用 JSDoc 还是注释？','TypesScript 中有两种类型的注释：\n\nJSDoc `/** ... */` \n\n普通注释 `// ... 或者 /* ... */` 。\n\n- 对于文档，也就是用户应当阅读的注释，使用 `/** JSDoc */` 。\n- 对于实现说明，也就是只和代码本身的实现细节有关的注释，使用 `// 行注释` 。\n\nJSDoc 注释能够为工具（例如编辑器或文档生成器）所识别，而普通注释只能供人阅读。',5,6,4,1),
(447,'对所有导出的顶层模块进行注释','使用 `/** JSDoc */` 注释为代码的用户提供信息。这些注释应当言之有物，切忌仅仅将属性名或参数名重抄一遍。如果代码的审核人认为某个属性或方法的作用不能从它的名字上一目了然地看出来的话，这些属性和方法同样应当使用 `/** JSDoc */` 注释添加说明文档，无论它们是否被导出，是公开还是私有的。',5,6,4,1),
(448,'不要使用@override','不要在 TypeScript 代码中使用 `@override` 注释。 `@override` 并不会被编译器视为强制性约束，这会导致注释与实现上的不一致性。如果纯粹为了文档添加这一注释，反而令人困惑。',5,6,4,1),
(449,'注释必须言之有物','虽然大多数情况下文档对代码十分有益，但对于那些并不用于导出的符号，有时其函数或参数的名称与类型便足以描述自身了。\n\n注释切忌照抄参数类型和参数名，如下面的反面示例：\n\n```tsx\n// 不要这样做！这个注释没有任何有意义的内容。\n/** @param fooBarService Foo 应用的 Bar 服务 */\n```\n\n因此，只有当需要添加额外信息时才使用 `@param` 和 `@return` 注释，其它情况下直接省略即可。\n\n```tsx\n/**\n * 发送 POST 请求，开始煮咖啡\n * @param amountLitres 煮咖啡的量，注意和煮锅的尺寸对应！\n */\nbrew(amountLitres: number, logger: Logger) {\n    // ...\n}\n```\n',5,6,4,1),
(450,'参数属性注释','通过为构造函数的参数添加访问限定符，参数属性同时创建了构造函数参数和类成员。例如，如下的构造函数\n\n```tsx\nclass Foo {\n    constructor(private readonly bar: Bar) { }\n}\n```\n\n为 `Foo` 类创建了 `Bar` 类型的成员 `bar` 。\n\n如果要为这些成员添加文档，应使用 JSDoc 的 `@param` 注释，这样编辑器会在调用构造函数和访问属性时显示对应的文档描述信息。\n\n```tsx\n/** 这个类演示了如何为参数属性添加文档 */\nclass ParamProps {\n    /**\n     * @param percolator 煮咖啡所用的咖啡壶。\n     * @param beans 煮咖啡所用的咖啡豆。\n     */\n    constructor(\n        private readonly percolator: Percolator,\n        private readonly beans: CoffeeBean[]) {}\n}\n/** 这个类演示了如何为普通成员添加文档 */\nclass OrdinaryClass {\n    /** 下次调用 brew() 时所用的咖啡豆。 */\n    nextBean: CoffeeBean;\n\n    constructor(initialBean: CoffeeBean) {\n        this.nextBean = initialBean;\n    }\n}\n```\n',5,6,4,1),
(451,'函数调用注释','如果有需要，可以在函数的调用点使用行内的 `/* 块注释 */` 为参数添加文档，或者使用字面量对象为参数添加名称并在函数声明中进行解构。注释的格式和位置没有明确的规定。\n\n```tsx\n// 使用行内块注释为难以理解的参数添加说明：\nnew Percolator().brew(/* amountLitres= */ 5);\n\n// 或者使用字面量对象为参数命名，并在函数 brew 的声明中将参数解构：\nnew Percolator().brew({amountLitres: 5});\n/** 一个古老的咖啡壶 {@link CoffeeBrewer} */\nexport class Percolator implements CoffeeBrewer {\n    /**\n     * 煮咖啡。\n     * @param amountLitres 煮咖啡的量，注意必须和煮锅的尺寸对应！\n     */\n    brew(amountLitres: number) {\n        // 这个实现煮出来的咖啡味道差极了，不管了。\n        // TODO(b/12345): 优化煮咖啡的过程。\n    }\n}\n```\n',5,6,4,1),
(452,'将文档置于装饰器之前','文档、方法或者属性如果同时具有装饰器（例如 `@Component`）和 JSDoc 注释，应当将 JSDoc 置于装饰器之前。\n\n禁止将 JSDoc 置于装饰器和被装饰的对象之间。\n\n```tsx\n// 不要这样做！JSDoc 被放在装饰器 @Component 和类 FooComponent 中间了！\n@Component({\n    selector: ''foo'',\n    template: ''bar'',\n})\n/** 打印 \"bar\" 的组件。 */\nexport class FooComponent {}\n```\n\n应当将 JSDoc 置于装饰器之前。\n\n```tsx\n/** 打印 \"bar\" 的组件。 */\n@Component({\n    selector: ''foo'',\n    template: ''bar'',\n})\nexport class FooComponent {}\n```\n',5,6,4,1),
(453,'对象解构','```tsx\nlet person = {\n  name: ''Semlinker'',\n  gender: ''male''\n};\n\nlet {name, gender} = person;\n```\n',5,3,4,1),
(454,'对象展开运算符','```tsx\nlet person = {\n  name: ''Semlinker'',\n  gender: ''male'',\n  address: ''Xiamen''\n};\n// 组装对象\nlet personWithAge = {...person, age: 31};\n// 获取除了某些项外的其它项\nlet {name, ...rest} = person;\n```\n',5,3,4,1),
(455,'延时设定','```tsx\nsetTimeout(async () => {\ndataList.show = false\n}, 1200)\n```\n',5,3,4,1),
(456,'布尔型Attribute','```html\n<button :disabled=\"isButtonDisabled\">\n  Button\n</button>\n```\n',2,3,4,1),
(457,'动态参数(v-bind)','```html\n<a v-bind:[attributeName]=\"url\"> ... </a>\n<!-- 简写 -->\n<a :[attributeName]=\"url\"> ... </a>\n这里的 attributeName 会作为一个 JS 表达式被动态执行\n```\n',2,3,4,1),
(458,'什么是CSS','- CSS 指层叠样式表 (**C**ascading **S**tyle **S**heets)\n- 样式定义**如何显示** HTML 元素\n- 样式通常存储在**样式表**中\n- 把样式添加到 HTML 4.0 中，是为了**解决内容与表现分离的问题**\n- **外部样式表**可以极大提高工作效率\n- 外部样式表通常存储在 **CSS 文件**中\n- 多个样式定义可**层叠**为一个',8,2,4,1),
(459,'后代选择器','后代选择器又称为包含选择器，可以选择父元素里面子元素，其写法就是把外层标签写在前面，内层标签写在后面，中间用空格分隔。当标签发生嵌套时，内层标签就成为外层标签的后代。\n\n```CSS\nul li{\n    border: 1px solid red;\n}\n\n语法：元素1 元素2{样式声明}\n```\n\n上述语法表示选择元素1里面的所有元素2（后代元素）。\n\n注意：\n\n- 元素1与元素2中间用空格隔开\n- 元素1是父级，元素2是子级，最终选择是元素2.',8,2,4,1),
(460,'背景透明(background)','```css\nbackground-color: hsla(0,0%,100%,.7);\n```\n\n',8,2,4,1),
(461,'背景渐变','使用方式:\n```css\n//渐变(方向)\nbackground: linear-gradient(to right, rgba(255, 255, 255, 0),#3FB6F7,rgba(255,255,255,0));\n\n//渐变(角度)\nbackground: linear-gradient(88deg, #4DF7BF 0%, rgba(77, 247, 191, 0.26) 12%, rgba(77, 247, 191, 0) 100%);\n```\n',8,2,4,1),
(462,'边框渐变','```css\n.border-grident{\n  margin-top: 20px;\n  width: 200px;\n  height: 200px;\n  border: 4px solid;\n  border-image: linear-gradient(to right, #8f41e9, #578aef) 4;\n}\n```\n',8,3,4,1),
(463,'瀑布流(column)','```html\n      <div class=\"w-full h-800px overflow-auto\">\n        <div class=\"test\">\n          <div v-for=\"(item, index) in rSnippet\" :key=\"index\" class=\"item\">\n            <div class=\"text-xl text-center\">{{ item.name }}</div>\n            <v-md-preview ref=\"preview\" :text=\"item.text\" />\n          </div>\n        </div>\n      </div>\n```\n\n```css\n.test {\n  margin: 0 auto;\n  column-count: 2;\n  column-gap: 10px;\n  counter-reset: count;\n\n  .item {\n    position: relative;\n    margin-bottom: 10px;\n    // page-break-inside: avoid;\n    // -webkit-column-break-inside: avoid;\n  }\n}\n```\n\n这种方式超出宽高度使用 overflow-y-scroll 只能左右滑动',8,3,4,1),
(464,'瀑布流(grid)','超出宽高度使用 overflow-y-scroll 可以上下滑动\n\n```html\n    <div class=\"test mt-4\">\n        <div class=\"bg-warm-gray-600 w-80 h-40\" t2></div>\n        <div class=\"bg-warm-gray-700 w-80 h-40 text2\"></div>\n        <div class=\"bg-slate-500\" w-80 h-50></div>\n        <div class=\"bg-warm-gray-500 w-80 h-50\"></div>\n        <div class=\"bg-warm-gray-800 w-80 h-20\"></div>\n        <div class=\"bg-warm-gray-100 w-80 h-70\"></div>\n        <div class=\"bg-warm-gray-200 w-80 h-40\"></div>\n        <div class=\"bg-warm-gray-500 w-80 h-60\"></div>\n      </div>\n```\n\n```css\n.test {\n  display: grid;\n  grid-gap: 10px;\n\n  /* 可以看到，网格大小，占据位置是需要提前设定的 */\n  grid-template-columns: repeat(3, 1fr);\n  box-sizing: border-box;\n  width: 80%;\n  height: 800px;\n  padding: 10px;\n  background-color: rgb(189 100 100);\n\n  @apply overflow-y-scroll;\n\n  div {\n    /* 避免子元素被分割的 2种方式 */\n    page-break-inside: avoid;\n    -webkit-column-break-inside: avoid;\n  }\n}\n```\n\n',8,3,4,1),
(465,'居中(flex)','```css\n.center-layout {\n	display: flex;\n	justify-content: center; // 内容自适应：上下居中\n	align-items: center; // 子项对齐方式：左右居中\n}\n```\n',8,3,4,1),
(466,'居中(fixed)','```css\n   .fixed-c {\n            height: 80px;\n            width: 90%;\n            position: fixed;\n            margin: auto;\n            bottom: 0; /*底部固定*/\n            left: 0; /*实现div的居中*/\n            right: 0; /*实现div的居中*/\n            text-align: center; /*div的内容居中*/\n            background: red;\n            line-height: 80px; /*垂直居中*/\n          }\n```\n',8,3,4,1),
(467,'平滑滚动','```css\n html {\n  scroll-behavior: smooth;\n}\n```\n',8,3,4,6),
(468,'绝对居中','```css\n.Absolute-Center {\n  margin: auto;\n  position: absolute;\n  top: 0; left: 0; bottom: 0; right: 0;\n}\n```\n',8,3,4,1),
(469,'文字居中','```css\n<div class=\"container\">\n    <div class=\"item\"></div>\n</div>\n\n.container {\n    text-align: center;\n}\n```\n',8,3,4,1),
(470,'块级元素居中','适用于块级元素，其实就是把要居中的子元素的 margin-left、margin-right 都设置为 auto，该方法能让子元素水平居中，但是对浮动元素和绝对定位的元素无效。\n\n```css\n.item {\n    margin: auto;\n}\n```\n',8,3,4,1),
(471,'绝对定位','```css\n.parent {\n    position: relative;\n}\n.child {\n    position: absolute;\n    /* top和left是以左上角为坐标原点 */\n    top: 50%;\n    left: 50%;\n    /* transform：translate的属性值为百分数时，是以元素自身为参考 */\n    transform: translate(-50%,-50%);\n}\n\n/* 或者 */\n.child {\n    position: absolute;\n    top: 0;\n    right: 0;\n    bottom: 0;\n    left: 0;\n}\n```\n\n值得说明的是，`top`和`left`是以左上角为坐标原点的，这样就会导致虽然设置为50%，但实际上是实现了左上角这个点的居中，因此需要再移动一下这个元素。而当`transform`的`translate`的属性值为百分数的时候，是以元素自身作为参考的，因此向左和向上移动自身长宽的50%即可实现水平垂直居中。',8,3,4,1),
(472,'svue3','```json\n  \"vue3+ts+scss\": {\n    \"prefix\": \"svue3\",\n    \"body\": [\n      \"<script lang=\\\"ts\\\" setup></script>\",\n      \"<template>\",\n      \"  <div></div>\",\n      \"</template>\",\n      \"\",\n      \"<style lang=\\\"scss\\\" scoped></style>\"\n    ],\n    \"description\": \"vue3\"\n  }\n```',13,3,4,1),
(473,'smounted','```json\n\"组件挂载完成后执行的函数\": {\n  \"prefix\": \"smounted\",\n  \"body\": [\n    \"onMounted(async () => {})\"\n  ],\n  \"description\": \"组件挂载完成后执行的函数\"\n}\n```\n',13,3,4,1),
(474,'sdefineAsyncComponent','```json\n\"异步组件加载\": {\n  \"prefix\": \"sdefineAsyncComponent\",\n  \"body\": [\n    \"const xxxx= defineAsyncComponent(() => {\",\n    \"  return import(''@/components/editor/xxxx.vue'')\",\n    \"})\",\n    \"<xxxx></xxxx>\"\n  ],\n  \"description\": \"异步组件加载\"\n}\n```\n',13,3,4,1),
(475,'sRefNumber','```json\n\"响应式变量\": {\n  \"prefix\": \"sRefNumber\",\n  \"body\": [\n    \"const rNumber= ref(0)\"\n  ],\n  \"description\": \"响应式变量\"\n}\n```\n',13,3,4,1),
(476,'sInterface','```json\n\"接口定义\": {\n  \"prefix\": \"sInterface \",\n  \"body\": [\n    \"export interface Ixxx {\",\n    \"  id: number\",\n    \"  name: ''''\",\n    \"}\",\n    \"export const xxxForm: Ixxx = reactive({\",\n    \"  id: 0,\",\n    \"  name: ''''\",\n    \"})\"\n  ],\n  \"description\": \"接口定义\"\n}\n```\n',13,3,4,1),
(477,'sDefinePropsStr','```json\n\"vue3中父子传值\": {\n  \"prefix\": \"sDefinePropsStr\",\n  \"body\": [\n    \"defineProps({\",\n    \"  nameStr: {\",\n    \"    type: String,\",\n    \"    required: true\",\n    \"  }\",\n    \"})\",\n    \" <p>{{ nameStr}}</p>\"\n  ],\n  \"description\": \"vue3中父子传值\"\n}\n```\n',13,3,4,1),
(478,'sAxiosAsync','```json\n\"Axios请求\": {\n  \"prefix\": \"sAxiosAsync\",\n  \"body\": [\n    \"await xxx.GetFun(xxx).then((res: any) => {})\"\n  ],\n  \"description\": \"Axios请求\"\n}\n```\n',13,3,4,1),
(479,'sAxiosAll','```json\n\"axios并发执行\": {\n  \"prefix\": \"sAxiosAll\",\n  \"body\": [\n    \"  axios.all([1, 2]).then(\",\n    \"    axios.spread((res: any,res2: any) => {\",\n    \"     //\",\n    \"    })\",\n    \"  )\"\n  ],\n  \"description\": \"axios并发执行\"\n}\n```\n',13,3,4,1),
(480,'sReactive','```json\n\"返回对象的响应式副本\": {\n  \"prefix\": \"sReactive\",\n  \"body\": [\n    \"const rData = reactive({\",\n    \"  id: number,\",\n    \"  name: ''''\",\n    \"})\"\n  ],\n  \"description\": \"返回对象的响应式副本\"\n}\n```\n',13,3,4,1),
(481,'sApply','```json\n\"CSS @apply\": {\n  \"prefix\": \"sApply\",\n  \"body\": [\n    \"@apply ;\"\n  ],\n  \"description\": \"CSS @apply\"\n}\n```\n',13,3,4,1),
(482,'sGetCurrentInstance','```json\n\"获取全局挂载的内容\": {\n  \"prefix\": \"sGetCurrentInstance\",\n  \"body\": [\n    \"const { proxy }: any = getCurrentInstance()\"\n  ],\n  \"description\": \"获取全局挂载的内容\"\n}\n```\n',13,3,4,1),
(483,'sEnun_Api','```json\n\"定义枚举api字段\": {\n  \"prefix\": \"sEnun_Api\",\n  \"body\": [\n    \"enum Api {\",\n    \"  FY = '''',\",\n    \"  SUM = '''',\",\n    \"  BYID = '''',\",\n    \"  CONTAINS = '''',\",\n    \"  ADD = '''',\",\n    \"  UPDATE = '''',\",\n    \"  DELETE = ''''\",\n    \"}\"\n  ],\n  \"description\": \"定义枚举api字段\"\n}\n```\n',13,3,4,1),
(484,'sFunAsync','```json\n\"定义异步方法\": {\n  \"prefix\": \"sFunAsync\",\n  \"body\": [\n    \"const name = async (data: any) => {\",\n    \"  //\",\n    \"}\"\n  ],\n  \"description\": \"定义异步方法\"\n}\n```\n',13,3,4,1),
(485,'sFun','```json\n\"定义方法\": {\n  \"prefix\": \"sFun\",\n  \"body\": [\n    \"const name = (data: any) => {\",\n    \"  //\",\n    \"}\"\n  ],\n  \"description\": \"定义方法\"\n}\n```\n',13,3,4,1),
(486,'sSvg','```json\n\"导入svg图片\": {\n  \"prefix\": \"sSvg\",\n  \"body\": [\n    \"import svg from ''@assets/svg/xxx.svg?component''\",\n    \"<svg />\"\n  ],\n  \"description\": \"导入svg图片\"\n}\n```\n',13,3,4,1),
(487,'sApply--','sApply--\n\n``` json\n\"unocss @apply\": {\n  \"prefix\": \"sApply--\",\n  \"body\": [\n    \"--at-apply:\"\n  ],\n  \"description\": \"unocss @apply\"\n}\n```\n',13,3,4,1),
(488,'跳转新窗口','```ts\n/**\n * @description: 跳转新页面\n * @param {string} url\n * @return {*}\n */\nfunction winUrl(url: string): any {\n  window.open(url)\n}\n\nasync function resolveId(path: string, id: number) {\n  const { href } = resolve(path, id)\n  await winUrl(href)\n}\n```\n',15,3,4,1),
(489,'ref属性','虽然 Vue 的声明性渲染模型为你抽象了大部分对 DOM 的直接操作，但在某些情况下，我们仍然需要直接访问底层 DOM 元素\n\n```html\n<input ref=\"input\">\n```\n\n',2,2,4,1),
(490,'访问模板(ref)','### 访问模板(ref)\n\n利用`ref`函数获取组件中的标签元素。\n\n声明一个同名的 ref：\n\n```html\n<script setup>\nimport { ref, onMounted } from ''vue''\n// 声明一个 ref 来存放该元素的引用\n// 必须和模板 ref 同名\nconst input = ref(null)\nonMounted(() => {\n  input.value.focus()\n})\n</script>\n\n<template>\n  <input ref=\"input\" />\n</template>\n```\n\n注意，你只可以**在组件挂载后**才能访问 ref。如果你想在模板中的表达式上访问 `$refs.input`，在初次渲染时会是 `null`\n\n如果你正试图观察一个模板 ref 的变化，确保考虑到 ref 的值为 `null` 的情况：\n\n```tsx\nwatchEffect(() => {\n  if (input.value) {\n    input.value.focus()\n  } else {\n    // 此时还未挂载，或此元素已经被卸载（例如经过 v-if 控制）\n  }\n})\n```\n\n利用`ref`函数获取组件中的标签元素。\n\n```html\n<template>\n  <input type=\"text\" ref=\"inputRef\">\n</template>\n\n<script>\nimport { onMounted, ref } from ''vue''\nexport default {\n  setup() {\n    // 定义dom元素\n    const inputRef = ref<HTMLElement|null>(null)\n    // 自动获取焦点\n    onMounted(() => {\n      inputRef.value && inputRef.value.focus()\n    })\n\n    return {\n      inputRef\n    }\n  },\n}\n</script>\n```\n\n',2,2,4,1),
(491,'设置窗口高度(ref)','### 设置窗口高度(ref)\n\n**template**\n\n```html\n<template>\n    <div class=\"demo1-container\">\n        <p>通过ref直接拿到dom</p>\n        <div ref=\"sectionRef\" class=\"ref-section\"></div>\n        <button @click=\"higherAction\" class=\"btn\">变高</button>\n    </div>\n</template>\n```\n\n**script setup**\n\n```tsx\nimport {ref} from ''vue''\nconst sectionRef = ref()\nlet height = 100;\nconst higherAction = () => {\n    height += 50;\n    sectionRef.value.style = `height: ${height}px`;\n}\n```\n\n',2,3,4,1),
(492,'将dom引用放到数组中(ref)','### 将dom引用放到数组中(ref)\n\n```html\n<template>\n    <div class=\"demo2-container\">\n        <div class=\"list-section\">\n            <div :ref=\"setRefAction\" @click=\"higherAction(index)\" class=\"list-item\" v-for=\"(item, index) in state.list\" :key=\"index\">\n                <span>{{item}}</span>\n            </div>\n        </div>\n    </div>\n</template>\n```\n\nscript setup\n\n```tsx\nimport { reactive } from ''vue''\nconst state = reactive({\n    list: [1, 2, 3, 4, 5, 6, 7],\n    refList: [] as Array<any>\n})\nconst setRefAction = (el: any) => {\n    state.refList.push(el);\n}\n```\n',2,3,4,1),
(493,'通过子组件emit传递ref','template\n```html\n<template>\n    <div ref=\"cellRef\" @click=\"cellAction\" class=\"cell-item\">\n        <span>{{item}}</span>\n    </div>\n</template>\n```\n\nscript setup\n\n```tsx\nimport {ref} from ''vue'';\n\nconst props = defineProps({\n    item: Number\n})\nconst emit = defineEmits([''cellTap'']);\nconst cellRef = ref();\nconst cellAction = () => {\n    emit(''cellTap'', cellRef.value);\n}\n```\n\n',2,3,4,1),
(494,'v-for中的ref','当 `ref` 在 `v-for` 中使用时，相应的 ref 中包含的值是一个数组，它将在元素被挂载后填充：\n\ntemplate\n\n```html\n<template>\n  <ul>\n    <li v-for=\"item in list\" ref=\"itemRefs\">\n      {{ item }}\n    </li>\n  </ul>\n</template>\n```\n\nscript setup\n\n```tsx\nimport { ref, onMounted } from ''vue''\n\nconst list = ref([\n  /* ... */\n])\nconst itemRefs = ref([])\nonMounted(() => console.log(itemRefs.value))\n```\n\n应该注意的是，ref 数组**不能**保证与源数组相同的顺序。',2,3,4,1),
(495,'函数型ref','`ref` attribute 还可以绑定为函数，每次组件更新时被调用。函数接受该元素引用作为第一个参数：\n\n```html\n<input :ref=\"(el) => { /* assign el to a property or ref */ }\">\n```\n\n如果使用一个动态的 `:ref` 绑定，也可以传一个函数。当元素卸载时，这个 `el` 参数会是 `null`。你当然也可以使用一个方法而不是内联函数。',2,2,4,1),
(496,'组件上的ref','#### 组件上的ref\n\n 也可以被用在一个子组件上\n\nscript setup\n\n```tsx\nimport { ref, onMounted } from ''vue''\nimport Child from ''./Child.vue''\n\nconst child = ref(null)\nonMounted(() => {\n  // child.value 为 <Child /> 这个组件实例\n})\n```\n\n```html\n<template>\n  <Child ref=\"child\" />\n</template>\n```\n\n一个子组件使用的是选项式 API 或没有使用 `<script setup>`，被引用的组件实例和该子组件的 `this` 完全一致，意味着父组件对子组件每个属性和方法都有完全的访问权。这使得在父组件和子组件之间创建紧密耦合的实现细节变得很容易，应该只在绝对需要时才使用组件引用。多数情况下，应该使用标准的 props 和 emit 接口来实现父子组件交互。\n\n有一个例外的情况，使用了 `<script setup>` 的组件时**默认私有**的：一个父组件无法访问到一个使用了 `<script setup>` 的子组件中的任何东西，除非子组件在其中通过 `defineExpose` 宏显式暴露：\n\nscript setup\n\n```tsx\nimport { ref } from ''vue''\nconst a = 1\nconst b = ref(2)\ndefineExpose({\n  a,\n  b\n})\n```\n\n当父组件通过模板 ref 获取到了该组件的实例，得到的实例类型为 `{ a: number, b: number }` (ref 都会自动解套，和一般的实例一样)。',2,2,4,1),
(497,'插槽(slot)','向一个组件传递内容，可使用插槽。通过插槽来分发内容，即插槽作为分发内容的出口。使用`<slot>`作为想要插入内容的占位符。\n\n插槽 `slot` 通常用于两个父子组件之间，最常见的应用就是我们使用一些 `UI` 组件库中的弹窗组件时，弹窗组件的内容是可以让我们自定义的，这就是使用了插槽的原理。\n\n- `slot` 是 `Vue3` 中的内置标签。\n- `slot` 相当于给子组件挖出了一个槽，可以用来填充内容。\n- 父组件中调用子组件时，子组件标签之间的内容元素就是要放置的内容，它会把 `slot` 标签替换掉。',2,2,4,1),
(498,'基本使用(slot)','在子组件放置插槽\n\n子组件**child.vue**\n\n```html\n<template>\n  // 匿名插槽\n  <slot/>\n  // 具名插槽\n  <slot name=''title''/>\n  // 作用域插槽\n  <slot name=\"footer\" :scope=\"state\" />\n</template>\n```\n\n父组件\n\n```html\n<template>\n  <child>\n    // 匿名插槽\n    <span>我是默认插槽</span>\n    // 具名插槽\n    <template #title>\n      <h1>我是具名插槽</h1>\n      <h1>我是具名插槽</h1>\n      <h1>我是具名插槽</h1>\n    </template>\n    // 作用域插槽\n    <template #footer=\"{ scope }\">\n      <footer>作用域插槽——姓名：{{ scope.name }}，年龄{{ scope.age }}</footer>\n    </template>\n  </child> \n</template>\n<script setup>\n  // 引入子组件\n  import child from ''./child.vue''\n</script>\n```\n',2,3,4,1),
(499,'插槽简写(slot)','```html\n<Dialog>\n  <template #header>\n    <div>1 </div>    \n  </template>\n  <template #default>\n    <div>2</div>\n  </template>\n  <template #footer>\n    <div>3</div>\n  </template>\n</Dialog>\n```\n\n',2,3,4,1),
(500,'渲染作用域(slot)','插槽内容可以访问到父组件的数据，因为插槽内容本身也是在父组件模板的一部分。\n\n```html\n<span>{{ message }}</span>\n<FancyButton>{{ message }}</FancyButton>\n```\n\n这里的两个 `{{ message }}` 插值表达式渲染的内容都是一样的。\n\n插槽内容**无法访问**子组件的数据，请牢记一条规则：\n\n> 任何父组件模板中的东西都是被编译到父组件的作用域中；而任何子组件模板中的东西都只被编译到子组件的作用域中',2,2,4,1),
(501,'默认插槽(slot)','### 默认插槽(slot)\n\n经常会遇到外部没有提供任何内容的情况，此时可能会为插槽提供一个默认的内容来渲染。 SubmitButton组件\n\n```html\n<button type=\"submit\">\n  <slot></slot>\n</button>\n```\n\n如果外部没有提供任何插槽内容，我们可能想在 `<button>` 中渲染“提交”这两个字。要让这两个字成为默认内容，需要写在 `<slot>` 标签之间：\n\n```html\n<button type=\"submit\">\n  <slot>\n    提交 <!-- 默认内容 -->\n  </slot>\n</button>\n```\n\n当在父组件中使用 `<submit-button>` 但不提供任何插槽内容：\n\n```html\n<SubmitButton />\n```\n\n那么将渲染出下面这样的 DOM 结构，包含默认的“提交”二字：\n\n```html\n<button type=\"submit\">提交</button>\n```\n\n如果提供了别的内容给插槽,那么渲染的 DOM 中会选择使用提供的插槽内容',2,2,4,1),
(502,'多个插槽(slot)','有时一个组件中可能会有多个插槽的插口。\n\n```html\n<div class=\"container\">\n  <header>\n    <!-- 标题内容放这里 -->\n  </header>\n  <main>\n    <!-- 主要内容放这里 -->\n  </main>\n  <footer>\n    <!-- 底部内容放这里 -->\n  </footer>\n</div>\n```\n\n对于这种场景，`<slot>` 元素可以有一个特殊的 attribute `name`，可以是一个独一无二的标识符，用来区分各个插槽，确定每一处最终会渲染的内容：\n\n```html\n<div class=\"container\">\n  <header>\n    <slot name=\"header\"></slot>\n  </header>\n  <main>\n    <slot></slot>\n  </main>\n  <footer>\n    <slot name=\"footer\"></slot>\n  </footer>\n</div>\n```\n\n没有提供 `name` 的 `<slot>` 插口会隐式地命名为“default”。\n\n在父组件中使用到 `<BaseLayout>` 时，我们需要给各个插槽传入内容，为了模板片段让各入各门、各寻其所。此时就需要用到**具名插槽**了：\n\n要为具名插槽传入内容，我们需要使用一个含 `v-slot` 指令的 `<template>` 元素，并将目标插槽的名字传给该指令：\n\n```html\n<BaseLayout>\n  <template v-slot:header>\n    <!-- header 插槽的内容放这里 -->\n  </template>\n</BaseLayout>\n```\n\n',2,2,4,1),
(503,'插槽简写(slot)','`v-slot` 有对应的简写 `#`，\n\n向 `<BaseLayout>` 传递内容的代码，指令均使用的是缩写形式：\n\n```html\n<BaseLayout>\n  <template #header>\n    <h1>这里是一个页面标题</h1>\n  </template>\n\n  <template #default>\n    <p>一个文章内容的段落</p>\n    <p>另一个段落</p>\n  </template>\n\n  <template #footer>\n    <p>这里有一些联系方式</p>\n  </template>\n</BaseLayout>\n```\n\n',2,2,4,1),
(504,'动态插槽名(slot)','动态指令参数在 `v-slot` 上也是有效的，即可以定义下面这样的动态插槽名：\n\n插槽可以是一个变量名 const name = ref(''header'')\n\n```html\n<base-layout>\n  <template v-slot:[dynamicSlotName]>\n    ...\n  </template>\n\n  <!-- 缩写为 -->\n  <template #[dynamicSlotName]>\n    ...\n  </template>\n</base-layout>\n```\n\n注意这里的表达式和动态指令参数受相同的语法限制',2,2,4,1),
(505,'作用域插槽(slot)','在上面的渲染作用域中我们讨论到，插槽的内容无法访问到子组件的状态。\n\n然而在某些场景下插槽的内容可能想要同时利用父组件域内和子组件域内的数据。要做到这一点，我们需要让子组件将一部分数据在渲染时提供给插槽。\n\n而我们确实也有办法这么做！我们可以像对组件传递 props 那样，向一个插槽的插口上传递 attribute：\n\n```html\n<!-- <MyComponent> 的模板 -->\n<div>\n  <slot :text=\"greetingMessage\" :count=\"1\"></slot>\n</div>\n```\n\n当需要接收插槽 props 时，一般的默认插槽和具名插槽的使用方式有了一些小小的区别。下面我们将会展示是怎样的不同，首先是一个默认插槽，通过子组件标签上的 `v-slot` 指令，直接接收到了一个插槽 props 对象：\n\n```html\n<MyComonent v-slot=\"slotProps\">\n  {{ slotProps.text }} {{ slotProps.count }}\n<MyComponent>\n```\n\n子组件传入插槽的 props 作为了 `v-slot` 指令的值，可以在插槽内的表达式中访问。\n\n可以将作用于插槽类比为一个传入子组件的函数。子组件会将相应的 props 作为参数传给它：\n\n```tsx\nMyComponent({\n  // 类比默认插槽，将其想成一个函数\n  default: (slotProps) => {\n    return `${slotProps.text} ${slotProps.count}`\n  }\n})\n\nfunction MyComponent(slots) {\n  const greetingMessage = ''hello''\n  return (\n    `<div>${\n      // 在插槽函数调用时传入 props\n      slots.default({ text: greetingMessage, count: 1 })\n    }</div>`\n  )\n}\n```\n\n实际上，这已经和作用域插槽的最终的代码编译结果、以及手动地调用渲染函数的方式非常类似了。\n\n`v-slot=\"slotProps\"` 可以类比这里的函数签名，和函数的参数类似，我们也可以在 `v-slot` 使用：\n\n```tsx\n<MyComonent v-slot=\"{ text, count }\">\n  {{ text }} {{ count }}\n<MyComponent>\n```\n\n',2,2,4,1),
(506,'具名作用域插槽(slot)','具名作用域插槽的工作方式也是类似的，插槽 props 可以作为 `v-slot` 指令的值被访问到：`v-`。当使用缩写时是这样：\n\n```html\n<MyComponent>\n  <template #header=\"headerProps\">\n    {{ headerProps }}\n  </template>\n\n  <template #default=\"defaultProps\">\n    {{ defaultProps }}\n  </template>\n\n  <template #footer=\"footerProps\">\n    {{ headerProps }}\n  </template>\n</MyComponent>\n```\n\n向具名插槽中传入 props：\n\n```html\n<slot name=\"header\" message=\"hello\"></slot>\n```\n\n注意插槽上的 `name` 是由 Vue 保留的，不会作为 props 传递给插槽。因此最终 `headerProps` 的结果是 `{ message: ''hello'' }`。',2,2,4,1),
(507,'provide/inject','### provide/inject\n\n提供 和 注入 是很简单理解的\n\n> 实现跨层级组件(祖孙)间通信\n\n在多层嵌套组件中使用，不需要将数据一层一层地向下传递,可以实现跨层级组件通信\n\n **父组件**\n\n```tsx\nconst info = reactive({\n   title: ''Vue3学习''\n })\n//提供的数据名，数据值\nprovide(''info'', info)\n```\n\n**子组件**\n\n```js\n//获取对应数据的值\nconst color = inject(''info'')\n```\n\n',2,2,4,1),
(508,'应用层Provide','要为组件后代供给数据，需要使用到 `provide()`函数：\n\n```tsx\nimport { provide } from ''vue''\nprovide(/* 注入名 */ ''message'', /* 值 */ ''hello!'')\n```\n\n接收两个参数。第一个参数被称为**注入名**，\n\n第二个参数是供给的值，值可以是任意类型，包括响应式的状态，比如一个 ref：\n\n```tsx\nimport { ref, provide } from ''vue''\n\nconst count = ref(0)\nprovide(''key'', count)\n```\n\n供给的响应式状态使后代组件可以由此和供给者建立响应式的联系。',2,2,4,1),
(509,'Inject(注入)','要注入祖先组件供给的数据，需使用 `inject()`函数：\n\n```tsx\nimport { inject } from ''vue''\nconst message = inject(''message'')\n```\n\n如果供给的值是一个 ref，注入进来的就是它本身，而**不会**自动解套。这使得被注入的组件保持了和供给者的响应性链接。\n\n同样的，如果没有使用 `<script setup>`，`inject()` 需要在 `setup()` 同步调用：\n\n```tsx\nimport { inject } from ''vue''\nexport default {\n  setup() {\n    const message = inject(''message'')\n    return { message }\n  }\n}\n```\n\n',2,2,4,1),
(510,'provide/inject标注类型','provide 和 inject 通常会在不同的组件中运行。要正确地为注入的值标记类型， Vue 提供了一个 `InjectionKey` 接口，它是一个继承自 `Symbol` 的泛型类型，可以用来在提供者和消费者之间同步注入值的类型：\n\n```tsx\nimport { provide, inject, InjectionKey } from ''vue''\n\nconst key = Symbol() as InjectionKey<string>\n      \nprovide(key, ''foo'') // 若提供的是非字符串值会导致错误\n\nconst foo = inject(key) // foo 的类型：string | undefined\n```\n\n建议将注入 key 的类型放在一个单独的文件中，这样它就可以被多个组件导入。\n\n当使用字符串注入 key 时，注入值的类型是 `unknown`，需要通过泛型参数显式声明：\n\n```tsx\nconst foo = inject<string>(''foo'') // 类型：string | undefined\n```\n\n注意注入的值仍然可以是 `undefined`，因为无法保证提供者一定会在运行时 provide 这个值。\n\n当提供了一个默认值后，这个 `undefined` 类型就可以被移除：\n\n```tsx\nconst foo = inject<string>(''foo'', ''bar'') // 类型：string\n```\n\n如果你确定该值将始终被提供，则还可以强制转换该值：\n\n```tsx\nconst foo = inject(''foo'') as string\n```\n\n',2,2,4,1),
(511,'provide/inject传递函数','```tsx\n// 获取歌手详情\nconst getArtistDetail = async () => {\n    let params = {\n        id: route.query.id\n    }\n    await artistDetail(params).then(res => {\n        state.list = res.data.data\n        state.artistName = state.list.artist.name\n    })\n}\n// 获取歌手名称，使用 provide 传递给孙子组件使用\nconst getArtistName = () => {\n    return state.artistName\n}\n// provide 传值\nprovide(\"getArtistName\", getArtistName())\n```\n',2,2,4,1),
(512,'动画(Transition)','提供了两个内置组件，可以制作基于状态变化的过渡和动画：\n\n- `<Transition>` 会在一个元素或组件进入和离开 DOM 时应用动画。\n- `<TransitionGroup>` 会在一个元素或组件被插入到 `v-for` 列表中，或是被移动或从其中移除时应用动画。\n\n除了这两个组件，也可以通过其他技术手段来应用动画，如切换 CSS 类或用状态绑定样式来驱动动画。',2,2,4,2),
(513,'数组语法(v-bind)','1- 数组语法：`[''rose'', ''abc'']`\n2- 三元运算： `[''rose'', ''abc'', isActive ? ''active'': '''']`\n3- 数组中添加对象： `[''rose'', ''abc'', isActive ? ''active'': '''', {current: isActive}]`,这里其实写三元有点复杂，所以数组语法中也支持嵌套对象语法。\n\n```html\n<template id=\"my-app\">\n  <div class=\"cos\" :class=\"[''rose'', ''abc'']\">数组语法1</div>\n  <div class=\"cos\" :class=\"[''rose'', ''abc'', isActive ? ''active'': '''']\">\n    三元运算\n  </div>\n  <div\n       class=\"cos\"\n       :class=\"[''rose'', ''abc'', isActive ? ''active'': '''', {current: isActive}]\"\n       >\n    数组中添加对象\n  </div>\n</template>\n```\n\n```tsx\n<script>\n  const App = {\n    template: \"#my-app\",\n    data() {\n      return {\n        isActive: false,\n        classObj: { active: true, rose: true },\n      };\n    },\n  };\n  Vue.createApp(App).mount(\"#app\");\n</script>\n```\n',2,2,4,1),
(514,'绑定一个对象(v-bind)','\n如果我们希望将一个对象的所有属性，绑定到元素上的所有属性，应该怎么做呢？\n非常简单，我们可以直接使用 v-bind 绑定一个 对象；\n这种写法在高阶组件中常用。\n案例：info对象会被拆解成div的各个属性\n\n```html\n<div v-bind=\"cos\">属性直接绑定一个对象</div>\n<!-- \n这里是数据：\ncos: {\n    name: ''wang'',\n    age: 28,\n    job: ''web'',\n},\n-->\n```\n',2,7,4,1),
(515,'认识插槽Slot','- 在开发中经常封装一个个可复用的组件：\n\n- - 前面通过props传递给组件一些数据，让组件来进行展示；\n  - 但是为了让这个组件具备更强的通用性，不能将组件中的内容限制为固定的div、span等等元素；\n  - 比如某种情况下希望组件显示的是一个按钮，某种情况下希望显示的是一张图片；\n  - 所以，应该让使用者可以决定某一块区域到底存放什么内容和元素；',2,2,4,3),
(516,'如何使用插槽slot','- 定义插槽slot：\n\n- - 插槽的使用过程其实是抽取共性、预留不同；\n  - 将共同的元素、内容依然在组件内进行封装；\n  - 同时会将不同的元素使用slot作为占位，让外部决定到底显示什么样的元素；\n\n- 如何使用slot呢？\n\n- - Vue中将 <slot> 元素作为承载分发内容的出口；\n  - 在封装组件中，使用特殊的元素<slot>就可以为封装组件开启一个插槽；\n  - 该插槽插入什么内容取决于父组件如何使用；',2,2,4,3),
(517,'Transition组件','vue提供了 transition 的封装组件，在下列情形中，可以给任何元素和组件添加进入/离开过渡:\n\n- 条件渲染 (使用 v-if)\n- 条件展示 (使用 v-show)\n- 动态组件\n- 组件根节点\n\n基本示例：\n\n```html\n<button @click=\"show = !show\">切换</button>\n<Transition>\n  <p v-if=\"show\">你好！</p>\n</Transition>\n```\n\n```css\n/* 下面我们会解释这些类是做什么的 */\n.v-enter-active,\n.v-leave-active {\n  transition: opacity 0.5s ease;\n}\n.v-enter-from,\n.v-leave-to {\n  opacity: 0;\n}\n```\n\n`<Transition>` 仅支持单个元素或组件作为其插槽内容。如果是一个组件，组件必须仅有一个根元素。\n\n当一个 `<Transition>` 组件中的元素被插入或移除时，会发生下面这些事情：\n\n1. Vue 会自动检测目标元素是否应用了 CSS 过渡或动画。如果是，则一些 CSS 过渡类会在适当的时机被添加和移除。\n2. 如果有作为监听器的 JavaScript 钩子，这些钩子函数会在适当时机被调用。\n3. 如果没有探测到 CSS 过渡或动画、没有提供 JavaScript 钩子，那么 DOM 的插入、删除操作将在浏览器的下一个动画帧上执行。',2,2,4,2),
(518,'基于CSS的过渡','过渡 CSS 类\n\n有 6 个应用于进入与离开过渡效果的 CSS 类。\n\n1. `v-enter-from`：进入动画的起始状态。这个 CSS 类在元素插入之前添加，在元素插入完成后的下一帧移除。\n2. `v-enter-active`：进入动画的生效状态，应用于整个进入动画阶段。这个 CSS 类在元素被插入之前被添加，在过渡/动画完成之后移除。这个类可以用来定义进入动画的持续时间、延迟与速度曲线类型。\n3. `v-enter-to`：进入动画的结束状态。这个 CSS 类在元素插入完成后的下一帧被添加 (也就是 `v-enter-from` 被移除的同时)，在过渡/动画完成之后移除。\n4. `v-leave-from`：离开动画的起始状态，在离开过渡效果被触发时立即添加，在一帧后被移除。\n5. `v-leave-active`：离开动画的生效状态，应用于整个离开动画阶段。在离开过渡效果被触发时立即添加，在过渡/动画完成之后移除。这个类可以用来定义离开动画的持续时间、延迟与速度曲线类型。\n6. `v-leave-to`：离开动画的结束状态。这个 CSS 类在一个离开动画被触发后的下一帧被添加 (也就是 `v-leave-from` 被移除的同时)，在过渡/动画完成之后移除。\n\n`v-enter-active` 和 `v-leave-active` 给了我们为进入和离开动画指定不同速度曲线的能力。',2,2,4,2),
(519,'为过渡命名','可以通过一个 `name` prop 来声明一种过渡：\n\n```html\n<Transition name=\"fade\">\n  ...\n</Transition>\n```\n\n对于一个有名字的过渡，它的过渡相关 CSS 类会以其名字而不是 `v` 作为前缀。举个例子，上面被应用的 CSS 类将会是 `fade-enter-active` 而不是 `v-enter-active`。这个“fade”过渡的 CSS 类将会是这样：\n\n```css\n.fade-enter-active,\n.fade-leave-active {\n  transition: opacity 0.5s ease;\n}\n\n.fade-enter-from,\n.fade-leave-to {\n  opacity: 0;\n}\n```\n',2,2,4,2),
(520,'Transition搭配CSS','`<Transition>` 一般都会搭配原生 CSS 过渡一起使用，正如你在上面的例子中所看到的那样。这个 `transition` CSS 属性是一个简写形式，使我们可以一次定义一个过渡的各个方面，包括需要执行动画的属性、持续时间和速度曲线。\n\n下面是一个更高级的例子，使用不同的持续时间和速度曲线来过渡多个属性：\n\n```html\n<Transition name=\"slide-fade\">\n  <p v-if=\"show\">hello</p>\n</Transition>\n```\n\n```css\n/*\n  进入和离开动画可以使用不同\n  持续时间和速度曲线。\n*/\n.slide-fade-enter-active {\n  transition: all 0.3s ease-out;\n}\n\n.slide-fade-leave-active {\n  transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);\n}\n\n.slide-fade-enter-from,\n.slide-fade-leave-to {\n  transform: translateX(20px);\n  opacity: 0;\n}\n```\n\n\n\n### CSS的animation',2,2,4,2),
(521,'CSS中的Animation','原生 CSS 动画和 CSS trasition 的应用方式基本上是相同的，只有一点不同，那就是 `*-enter-from` 不是在元素插入后立即移除，而是在一个 `animationend` 事件触发时被移除。\n\n对于大多数的 CSS 动画，我们可以简单地在 `*-enter-active` 和 `*-leave-active` 类下面声明它们。下面是一个示例：\n\n```html\n<Transition name=\"bounce\">\n  <p v-if=\"show\" style=\"text-align: center;\">\n    你好！你会看到这里正在跳跃！\n  </p>\n</Transition>\n```\n\n```css\n.bounce-enter-active {\n  animation: bounce-in 0.5s;\n}\n.bounce-leave-active {\n  animation: bounce-in 0.5s reverse;\n}\n@keyframes bounce-in {\n  0% {\n    transform: scale(0);\n  }\n  50% {\n    transform: scale(1.25);\n  }\n  100% {\n    transform: scale(1);\n  }\n}\n```\n',2,2,4,2),
(522,'自定义过渡类','你也可以向 `<Transition>` 传递以下的 props 来指定自定义的过渡类：\n\n- `enter-from-class`\n- `enter-active-class`\n- `enter-to-class`\n- `leave-from-class`\n- `leave-active-class`\n- `leave-to-class`\n\n你传入的这些类会覆盖相应阶段的默认类名。这个功能在你想要在 Vue 的动画机制下集成其他的第三方 CSS 动画库时非常有用，比如Animate.css\n\n```html\n<!-- 假设你已经引入了 Animate.css -->\n<Transition\n  name=\"custom-classes\"\n  enter-active-class=\"animate__animated animate__tada\"\n  leave-active-class=\"animate__animated animate__bounceOutRight\"\n>\n  <p v-if=\"show\">hello</p>\n</Transition>\n```\n',2,2,4,2),
(523,'同时使用Transition和Animation','Vue 需要附加事件侦听器，以便知道过渡何时结束。可以是 `transitionend` 或 `animationend`，这取决于你所应用的 CSS 规则。如果你仅仅使用二者其中之一，Vue 可以自动探测到正确的类型。\n\n然而在某些场景中，你或许想要在同一个元素上同时使用它们两个，举个例子，触发了一个 CSS 动画的同时，由于副作用触发了另一个 CSS 过渡。此时你需要显式地传入 `type` prop 来声明，告诉 Vue 需要关心哪种类型，传入的值是 `animation` 或 `transition`：\n\n```html\n<Transition type=\"animation\">...</Transition>\n```\n',2,2,4,2),
(524,'深层级过渡与显式过渡时间','尽管过渡类仅能应用在 `<Transition>` 的直接子元素上，我们还是可以使用深层级的 CSS 选择器，使深层级的元素发生过渡。\n\n```html\n<Transition name=\"nested\">\n  <div v-if=\"show\" class=\"outer\">\n    <div class=\"inner\">\n      Hello\n    </div>\n  </div>\n</Transition>\n```\n\n```css\n/* 应用于深层级元素的规则 */\n.nested-enter-active .inner,\n.nested-leave-active .inner {\n  transition: all 0.3s ease-in-out;\n}\n\n.nested-enter-from .inner,\n.nested-leave-to .inner {\n  transform: translateX(30px);\n  opacity: 0;\n}\n```\n\n我们甚至可以在深层级的元素上添加一个过渡延迟，这会创建一个交错进入动画序列：\n\n```css\n/* 延迟进入深层级元素以获得交错效果 */\n.nested-enter-active .inner {\n  transition-delay: 0.25s;\n}\n```\n\n然而，这会带来一个小问题。默认情况下，`<Transition>` 组件会通过监听过渡根元素上的**第一个** `transitionend` 或者 `animationend` 事件来尝试自动判断过渡何时结束。而在深层级的过渡中，期望的行为应该是等待所有内部元素的过渡完成。\n\n在这种情况下，你可以通过向 `<Transition>` 组件传入 `duration` prop 来显式指定的过渡持续时间 (以毫秒为单位)。总持续时间应该匹配延迟加上内部元素的过渡持续时间：\n\n```html\n<Transition :duration=\"550\">...</Transition>\n```\n\n如果有必要的话，你也可以用对象的形式传入，分开指定进入和离开所需的时间：\n\n```html\n<Transition :duration=\"{ enter: 500, leave: 800 }\">...</Transition>\n```\n',2,2,4,2),
(525,'JavaScript钩子','你可以通过监听 `<Transition>` 组件事件的方式在过渡过程中挂上钩子函数：\n\n```html\n<Transition\n  @before-enter=\"onBeforeEnter\"\n  @enter=\"onEnter\"\n  @after-enter=\"onAfterEnter\"\n  @enter-cancelled=\"onEnterCancelled\"\n  @before-leave=\"onBeforeLeave\"\n  @leave=\"onLeave\"\n  @after-leave=\"onAfterLeave\"\n  @leave-cancelled=\"onLeaveCancelled\"\n>\n  <!-- ... -->\n</Transition>\n\n```\n\n```tsx\n// 在元素被插入到 DOM 之前被调用\n// 用这个来设置元素的 \"enter-from\" 状态\nfunction onBeforeEnter(el) {},\n\n// 在元素被插入到 DOM 之后的下一帧被调用\n// 用这个来开始进入动画\nfunction onEnter(el, done) {\n  // 调用回调函数 done 表示过渡结束\n  // 如果与 CSS 结合使用，则这个回调是可选参数\n  done()\n}\n\n// 当进入过渡完成时调用。\nfunction onAfterEnter(el) {}\nfunction onEnterCancelled(el) {}\n\n// 在 leave 钩子之前调用\n// 大多数时候，你应该只会用到 leave 钩子\nfunction onBeforeLeave(el) {}\n\n// 在离开过渡开始时调用\n// 用这个来开始离开动画\nfunction onLeave(el, done) {\n  // 调用回调函数 done 表示过渡结束\n  // 如果与 CSS 结合使用，则这个回调是可选参数\n  done()\n}\n\n// 在离开过渡完成、\n// 且元素已从 DOM 中移除时调用\nfunction onAfterLeave(el) {}\n\n// 仅在 v-show 过渡中可用\nfunction leaveCancelled(el) {}\n```\n\n这些钩子可以与 CSS 过渡/动画结合使用，也可以单独使用。\n\n在使用仅由 JavaScript 执行的动画时，最好是添加一个 `:css=\"false\"` prop。这显式地向 Vue 表明跳过 CSS 过渡的自动探测。除了性能稍好一些之外，还可以防止 CSS 规则意外地干扰过渡。\n\n```html\n<Transition\n  ...\n  :css=\"false\"\n>\n  ...\n</Transition>\n```\n\n在有了 `:css=\"false\"` 后，我们就全权自己负责控制什么时候过渡结束了。这种情况下对于 `@enter` 和 `@leave` 钩子来说，回调函数 `done` 就是必须的。否则，钩子将被同步调用，过渡将立即完成。',2,2,4,2),
(526,'可重用过渡','得益于 Vue 的组件系统，过渡是可以被重用的。要创建一个可被重用的过渡，我们需要为 `<Transition>` 组件创建一个包裹组件，并向内传入插槽内容：\n\n```html\n<!-- MyTransitio.vue -->\n<script>\n// JavaScript 钩子逻辑...\n</script>\n\n<template>\n  <!-- 包裹内置的 Transition 组件 -->\n  <Transition\n    name=\"my-transition\"\n    @enter=\"onEnter\"\n    @leave=\"onLeave\">\n    <slot></slot> <!-- 向内传递插槽内容 -->\n  </Transition>\n</tempalte>\n\n<style>\n/*\n  必要的 CSS...\n  注意：避免在这里使用 <style scoped>\n  因为那不会应用到插槽内容上\n*/\n</style>\n```\n\n现在 `MyTransition` 可以在导入后像内置组件那样使用了：\n\n```html\n<MyTransition>\n  <div v-if=\"show\">Hello</div>\n</MyTransition>\n```\n\n',2,2,4,2),
(527,'出现时过渡','如果你想在某个节点初次渲染时应用一个过渡效果，你可以添加 `appear` attribute：\n\n```html\n<Transition appear>\n  ...\n</Transition>\n```\n',2,2,4,2),
(528,'过渡模式','### \n\n在之前的例子中，进入和离开的元素都是在同时开始动画的，并且我们必须将它们设为 `position: absolute` 以避免二者同时存在时出现的布局问题。\n\n然而，在某些场景中这可能不是个好的方案，或者并不能符合行为预期。我们可能想要先执行离开动画，然后在其完成**之后**再执行元素的进入动画。手动编排这样的动画是非常复杂的，好在我们可以通过向 `<Transition>` 传入一个 `mode` prop 来实现这个行为：\n\n```html\n<Transition mode=\"out-in\">\n  ...\n</Transition>\n```\n\n将之前的例子改为 `mode=\"out-in\"` 后是这样：\n\n`<Transition>` 也支持 `mode=\"in-out\"`，虽然这并不常用。',2,2,4,2),
(530,'组件间过渡','`<Transition>` 也可以用在动态组件之间：\n\n```html\n<Transition name=\"fade\" mode=\"out-in\">\n  <component :is=\"activeComponent\"></component>\n</Transition>\n```\n\n',2,2,4,2),
(531,'动态过渡','`<Transition>` 的 props (比如 `name`) 也可以是动态的！这让我们可以根据状态变化动态地应用不同类型的过渡：\n\n```html\n<Transition :name=\"transitionName\">\n  <!-- ... -->\n</Transition>\n```\n\n当你使用 Vue 的过渡类约定规则定义了 CSS 过渡/动画，并想在它们之间切换时，这可能很有用。\n\n你也可以根据你的组件的当前状态在 JavaScript 过渡钩子中应用不同的行为。在此篇的最后，我们可以得出结论，创建动态过渡的终极方式是创建可重用的过渡组件，这些组件接受 prop 来改变过渡的性质。现在在编写动画时，就只有你想不到，没有做不到的了。',1,2,4,2),
(532,'控制动画时长','就是我们不管CSS中的动画和过渡时长，以标签为准。可以绑定属性`<transition :duration=\"1000\">` 来控制时长，意思是1秒后，结束动画和过渡。',2,2,4,2),
(533,'过度&动画的使用','```html\n<template>\n  <div id=\"app\">\n    <router-view v-slot=\"{ Component }\">\n      <transition name=\"fade\">\n        <keep-alive>\n          <component :is=\"Component\" v-if=\"$route.meta.keepAlive\" />\n        </keep-alive>\n      </transition>\n      <transition name=\"fade\">\n        <component :is=\"Component\" v-if=\"!$route.meta.keepAlive\" />\n      </transition>\n    </router-view>\n  </div>\n</template>\n```\n\n```js\n\n<style lang=\"scss\">\n\n/* 可以为进入和离开动画设置不同的持续时间和动画函数 */\n.fade-enter-active {\n  //进入过程\n  animation: fade-in 0.8s cubic-bezier(0.39, 0.575, 0.565, 1) both;\n}\n.fade-leave-active {\n  //离开过程\n  animation: fade-out 0.3s ease-out both;\n}\n//进入开始和离开结束的状态\n.fade-enter-to {\n  opacity: 0;\n}\n//进入开始和离开结束的状态\n.fade-leave-to {\n  opacity: 0;\n}\n\n@keyframes fade-in {\n  0% {\n    opacity: 0;\n  }\n\n  100% {\n    opacity: 1;\n  }\n}\n@keyframes fade-out {\n  0% {\n    opacity: 1;\n  }\n\n  100% {\n    opacity: 0;\n  }\n}\n</style>\n```\n',2,3,4,2),
(534,'动态切换组件','```html\n<template>\n    <div class=\"dynamicComponent\">\n        <ul>\n            <li v-for=\"(item, index) in tabList\" :key=\"index\" @click=\"change(index)\">{{ item.name }}</li>\n        </ul>\n        <!-- is的值是哪个组件的名称就显示哪个组件 -->\n        <component :is=\"state.com\"></component>\n    </div>\n</template>\n```\n\n```tsx\n//<script setup name=\"funcDynamicComponent\">\nimport A from ''./component/A.vue''\nimport B from ''./component/B.vue''\nimport C from ''./component/C.vue''\n\n// 因为 reactive 是响应式数据 proxy 但是组件确不需要响应式，所有需要使用 markRaw 或者 shallowRef 来避免\nconst tabList = reactive([\n    { name: ''A 组件'', com: markRaw(A) },\n    { name: ''B 组件'', com: markRaw(B) },\n    { name: ''C 组件'', com: markRaw(C) },\n])\nconst state = reactive({\n    com: tabList[0].com,\n})\n\n// 切换组件\nconst change = (index) => {\n    state.com = tabList[index].com\n}\n//</script>\n```\n',2,3,4,4),
(535,'动态切换组件2','```html\n<template>\n    <component :is=\"component[state.activeName]\" @closeDialog=\"closeDialog\" />\n</template>\n```\n\n```tsx\n//<script setup>\nimport Dashboard from ''./component/dashboard.vue'' // 登录指示板\nimport Code from ''./component/code.vue'' // 扫码登录\nimport Email from ''./component/email.vue'' // 邮箱登录\n\n// 跟踪自身 .value 变化的 ref，配合 component 使用\nconst component = shallowRef({\n	dashboard: Dashboard,\n	code: Code,\n	email: Email,\n})\n\n// 定义响应式数据\nconst state = reactive({\n	activeName: ''dashboard'', // 登录方式\n})\n//</script>\n```\n\n',2,3,4,4),
(536,'滚动属性','| 属性             | 说明                                                         |\n| ---------------- | ------------------------------------------------------------ |\n| **clientWidth**  | 获取元素可视部分的宽度，即 CSS 的 width 和 padding 属性值之和，元素边框和滚动条不包括在内，也不包含任何可能的滚动区域。 |\n| **clientWidth**  | 获取元素可视部分的高度，即 CSS 的 height 和 padding 属性值之和，元素边框和滚动条不包括在内，也不包含任何可能的滚动区域。 |\n| **offsetWidth**  | 元素在页面中占据的宽度总和，包括 width、padding、border 以及滚动条的宽度。 |\n| **offsetHeight** | 只读属性，返回该元素的像素高度，高度包含该元素的垂直内边距和边框，且是一个整数。 |\n| **scrollWidth**  | 只读属性，是元素内容宽度的一种度量，括由于溢出导致的视图中不可见内容。 |\n| **scrollHeight** | 只读属性，是元素内容宽度的一种度量，括由于溢出导致的视图中不可见内容。 |\n| **scrollTop**    | 可以获取或设置一个元素的内容垂直滚动的像素数。               |\n| **scrollLeft**   | 可以读取或设置元素滚动条到元素左边的距离。                   |\n\n\n',2,3,4,6),
(537,'横向滚动','```js\n// HTML\n<div ref=\"xxxRef\"></div>\n<button @click=\"onScroll(''left'')\"></button>\n<button @click=\"onScroll(''right'')\"></button>\n \n// JS\nimport { ref, nextTick } from ''vue''\nconst xxxRef = ref(null)\nconst onScroll = (type) => {\n  nextTick(() => {\n    const distance = type === ''left'' ? 0 : xxxRef.value.scrollWidth;\n    xxxRef.value.scrollLeft = distance\n  })\n}\n```\n',2,3,4,6),
(538,'竖向滚动','```js\n// HTML\n<div ref=\"xxxRef\"></div>\n<button @click=\"onScroll(''top'')\">顶部</button>\n<button @click=\"onScroll(''bottom'')\">底部</button>\n \n// JS\nimport { ref, nextTick } from ''vue''\nconst xxxRef = ref(null)\nconst onScroll = (type) => {\n  nextTick(() => {\n    const distance = type === ''top'' ? 0 : xxxRef.value.scrollHeight;\n    xxxRef.value.scrollTop = distance\n  })\n}\n```\n',2,3,4,6),
(539,'滑动指定位置','```js\n  <button @click=\"onScroll2(200)\">下滑</button>\n  <button @click=\"onScroll3(200)\">上划</button>\n  <div ref=\"xxxRef\"></div>\n  \n  const onScroll2 = (type: number) => {\n  nextTick(() => {\n    xxxRef.value.scrollTop += type\n  })\n}\nconst onScroll3 = (type: number) => {\n  nextTick(() => {\n    xxxRef.value.scrollTop -= type\n  })\n}\n```\n',2,3,4,6),
(540,'丝滑滚动','``` css\nscroll-behavior: smooth;\n```\n\n\n',2,3,4,6),
(541,'网页文件下载','```tsx\nconst confirm = async (names: string, path: string) => {\n  let fileName = names + path.slice(-4)\n  await common.FileDownload(path).then((res: any) => {\n    // 地址转换\n    let url = window.URL.createObjectURL(res.data)\n    console.log(''%c [ url ]-32'', ''font-size:13px; background:pink; color:#bf2c9f;'', url)\n    // 文件名\n\n    const a = document.createElement(''a'')\n    a.setAttribute(''href'', url)\n    a.setAttribute(''download'', fileName)\n    document.body.append(a)\n    a.click()\n    document.body.removeChild(a)\n  })\n  message.success(''Click on Yes'')\n}\n```\n',2,3,4,5),
(542,'axios方法调用','```tsx\n //常用\n await computer.Get(data).then(r => {\n    rData = r.data\n  })\n  //简化\nrData = await (await computer.Get(data)).data\n```\n\n',3,3,4,5),
(543,'CSS变量注入','```tsx\n<template>\n  <span>变量注入</span>  \n</template>\n<script lang=\"ts\" setup>\n  import { ref } from ''vue''\n  const color = ref(''red'')\n</script>\n<style scoped>\n  span {\n    /* 使用v-bind绑定组件中定义的变量 */\n    color: v-bind(''color'');\n  }  \n</style>\n```\n',2,3,4,5),
(544,'JS隐式类型转换','// 字符串转数字代码对比 \n\n```js\nconst price = parseInt(''32''); //传统方式\nconst price = Number(''32''); //传统方式\nconst price = +''32''; //新方式\n```\n\n// 日期转换为时间戳代码对比 \n\n```js\nconst timeStamp = new Date().getTime(); //传统方式\nconst timeStamp = +new Date(); //新方式\n```\n\n//布尔转数字新方式\n\n```js\nconsole.log(+true); // Return: 1\nconsole.log(+false); // Return: 0\n```\n\n',6,3,4,5),
(545,'动态导入css样式','**dynamicScss.ts**\n\n```ts\nconst SCSS: number = 1\nasync function toScss(nameCss: string) {\n  switch (nameCss) {\n    case ''sAbout'':\n      if (SCSS === 1) {\n        import(''../views/sAbout/index.scss'')\n      }\n      break\n    case ''index'':\n      if (SCSS === 1) {\n        import(''../views/sAbout/index.scss'')\n      }\n  }\n}\nexport default toScss\n```\n\n```tsx\nimport { toScss } from \"../../hooks/dynamicScss\";\nawait toScss(\"sAbout\");\n```\n\n',2,3,4,5),
(546,'封装ts属性','**data.ts**\n\n```tsx\nimport { reactive } from \"vue\";\nexport interface BlogsList {\n  rData: any, // 显示的数据\n  page: number, //页码\n  pagesize: number, //每页条数\n  count: number, //总数\n}\n\nexport const blogsList: BlogsList = reactive({\n  rData: [],\n  page: 1,\n  pagesize: 10,\n  count: 0,\n})\n```\n\n **vue**\n\n```tsx\nimport { blogsList } from \"../Blogs/components/data\";\n```\n',2,3,4,5),
(547,'函数封装',' **index.ts**\n\n```tsx\nexport class blogs {\n\n  static async Getxxx() {\n    await article.GetFyAsync(page,pagesize).then((r: any) => {\n      return rData = r.data;\n    });\n  }\n}\n```\n\n**index.vue**\n\n```ts\nimport { blogs } from \"./index\";\nawait blogs.Getxxx();\n```\n\n',2,3,4,5),
(548,'模板调试','场景:在Vue开发过程中, 经常会遇到template模板渲染时JavaScript变量出错的问题, 此时也许你会通过console.log来进行调试 这时可以在开发环境挂载一个 log 函数\n\n```html\n// main.js\napp.config.globalProperties.$log = window.console.log\n// 组件内部\n<div>{{$log(info)}}</div>\n```\n',2,3,4,5),
(549,'校验数据类型','```js\nexport const typeOf = function(obj) {\n  return Object.prototype.toString.call(obj).slice(8, -1).toLowerCase()\n}\n```\n\n```js\ntypeOf(''树哥'')  // string\ntypeOf([])  // array\ntypeOf(new Date())  // date\ntypeOf(null) // null\ntypeOf(true) // boolean\ntypeOf(() => { }) // function\n```\n\n',6,3,4,5),
(550,'大小写转换','**参数：**\n\n- str 待转换的字符串\n- type 1-全大写 2-全小写 3-首字母大写\n\n```js\nexport const turnCase = (str, type) => {\n  switch (type) {\n    case 1:\n      return str.toUpperCase()\n    case 2:\n      return str.toLowerCase()\n    case 3:\n      //return str[0].toUpperCase() + str.substr(1).toLowerCase() // substr 已不推荐使用\n      return str[0].toUpperCase() + str.substring(1).toLowerCase()\n    default:\n      return str\n  }\n}\n```\n\n```js\nturnCase(''vue'', 1) // VUE\nturnCase(''REACT'', 2) // react\nturnCase(''vue'', 3) // Vue\n```\n',2,3,4,5),
(551,'解析URL参数','```js\nexport const getSearchParams = () => {\n  const searchPar = new URLSearchParams(window.location.search)\n  const paramsObj = {}\n  for (const [key, value] of searchPar.entries()) {\n    paramsObj[key] = value\n  }\n  return paramsObj\n}\n```\n\n```js\n// 假设目前位于 https://****com/index?id=154513&age=18;\ngetSearchParams(); // {id: \"154513\", age: \"18\"}\n```\n',6,3,4,5),
(552,'判断手机是Andoird还是IOS','```js\n/** \n * 1: ios\n * 2: android\n * 3: 其它\n */\nexport const getOSType=() => {\n  let u = navigator.userAgent, app = navigator.appVersion;\n  let isAndroid = u.indexOf(''Android'') > -1 || u.indexOf(''Linux'') > -1;\n  let isIOS = !!u.match(/\\(i[^;]+;( U;)? CPU.+Mac OS X/);\n  if (isIOS) {\n    return 1;\n  }\n  if (isAndroid) {\n    return 2;\n  }\n  return 3;\n}\n```\n',6,3,4,5),
(553,'生成UUID','```js\nfunction uuid() {\n  var temp_url = URL.createObjectURL(new Blob());\n  var uuid = temp_url.toString(); // blob:https://xxx.com/b250d159-e1b6-4a87-9002-885d90033be3\n  URL.revokeObjectURL(temp_url);\n  return uuid.substr(uuid.lastIndexOf(\"/\") + 1);\n}\n```\n',6,3,4,5),
(554,'滚动到页面顶部','```js\nexport const scrollToTop = () => {\n  const height = document.documentElement.scrollTop || document.body.scrollTop;\n  if (height > 0) {\n    window.requestAnimationFrame(scrollToTop);\n    window.scrollTo(0, height - height / 8);\n  }\n}\n```\n',6,3,4,6),
(555,'使用&&替代if','```js\nconst doSometions = () => {}\nconst isTrue = true\nlet temp = ''''\nif(isTrue){\n    doSometings()\n    temp = ''isTrue''\n}\n\n// 替代方案\nisTrue && this.doSometings()\nisTrue && (temp == ''isTrue'')\n```\n',6,3,4,5),
(556,'数组对象根据字段去重','**参数：**\n\n- arr 要去重的数组\n- key 根据去重的字段名\n\n```js\nexport const uniqueArrayObject = (arr = [], key = ''id'') => {\n  if (arr.length === 0) return\n  let list = []\n  const map = {}\n  arr.forEach((item) => {\n    if (!map[item[key]]) {\n      map[item[key]] = item\n    }\n  })\n  list = Object.values(map)\n\n  return list\n}\n```\n\n```js\nconst responseList = [\n    { id: 1, name: ''树哥'' },\n    { id: 2, name: ''黄老爷'' },\n    { id: 3, name: ''张麻子'' },\n    { id: 1, name: ''黄老爷'' },\n    { id: 2, name: ''张麻子'' },\n    { id: 3, name: ''树哥'' },\n    { id: 1, name: ''树哥'' },\n    { id: 2, name: ''黄老爷'' },\n    { id: 3, name: ''张麻子'' },\n]\n\nuniqueArrayObject(responseList, ''id'')\n// [{ id: 1, name: ''树哥'' },{ id: 2, name: ''黄老爷'' },{ id: 3, name: ''张麻子'' }]\n```\n\n',6,3,4,7),
(557,'滚动到元素位置','```js\nexport const smoothScroll = element =>{\n    document.querySelector(element).scrollIntoView({\n        behavior: ''smooth''\n    });\n};\n```\n\n```js\nsmoothScroll(''#target''); // 平滑滚动到 ID 为 target 的元素\n```\n',6,3,4,6),
(559,'三元运算符简化ifelse','```js\n//Longhand \nlet marks = 26; \nlet result; \nif (marks >= 30) {\n   result = ''Pass''; \n} else { \n   result = ''Fail''; \n} \n//Shorthand \nlet result = marks >= 30 ? ''Pass'' : ''Fail'';\n```\n',6,3,4,8),
(560,'||运算符给变量指定默认值','本质是利用了`||`运算符的特点，当前面的表达式的结果转成布尔值为`false`时，则值为后面表达式的结果\n\n短路运算有时候可以用来代替一些比较简单的 `if else`\n\n```js\n//Longhand\nlet imagePath;\nlet path = getImagePath();\n\nif (path !== null && path !== undefined && path !== '''') {\n    imagePath = path;\n} else {\n    imagePath = ''default.jpg'';\n}\n\n//Shorthand\nlet imagePath = getImagePath() || ''default.jpg'';\nlet c = a || b\n```\n\n',6,3,4,8),
(561,'使用字符串模板简化代码','```js\n//Longhand\nconsole.log(''You got a missed call from '' + number + '' at '' + time);\n//Shorthand\nconsole.log(`You got a missed call from ${number} at ${time}`);\n```\n\n',6,3,4,5),
(562,'网页跳转','```tsx\n  //当前窗口跳转\n   self.location.href=url\n  //新窗口跳转\n   window.open(url)\n  //跳转链接 返回上一页\n   window.history.back(-1);\n```\n\n self 指代当前窗口对象，属于window 最上层的对象。\n\n  location.href 指的是某window对象的url的地址\n\n  self.location.href 指当前窗口的url地址，去掉self默认为当前窗口的url地址，一般用于防止外部的引用\n\ntop.location.href:为引用test.html页面url的**父窗口对象的url**',6,3,4,5),
(563,'取最后一位数字','```javascript\nconst num = 12345\nconst num2 = ''54321''\nconsole.log(num%10) // 5\nconsole.log(num2%10) // 1 当然隐式转换也是可以的\n```\n\n',6,3,4,5),
(564,'滚动到页面顶部','```js\nconst goToTop = () => window.scrollTo(0, 0);\ngoToTop();\n```\n',6,3,4,6),
(565,'判断简化','如果有下面的这样的一个判断：\n\n```javascript\nif(a === undefined || a === 10 || a=== 15 || a === null) {\n    //...\n}\n```\n\n就可以使用数组来简化这个判断逻辑：\n\n```javascript\nif([undefined, 10, 15, null].includes(a)) {\n    //...\n}\n```\n\n这样代码就会简洁很多，并且便于扩展，如果还有需要等于a的判断，直接在数组中添加即可。',6,3,4,8),
(566,'一元运算符简化字符串转数字','```js\n//Longhand\nlet total = parseInt(''453'');\nlet average = parseFloat(''42.6'');\n\n//Shorthand\nlet total = +''453'';\nlet average = +''42.6'';\n```\n\n',6,3,4,8),
(567,'清空数组','如果想要清空一个数组，可以将数组的length置于0:\n\n```javascript\nlet array = [\"A\", \"B\", \"C\", \"D\", \"E\", \"F\"]\narray.length = 0 \nconsole.log(array)  // []\n```\n\n',6,3,4,7),
(568,'验证undefined和null','如果有这样一段代码：\n\n```javascript\nif(a === null || a === undefined) {\n    doSomething()\n}\n```\n\n也就是如果需要验证一个值如果等于null或者undefined时，需要执行一个操作时，可以使用空值合并运算符来简化上面的代码：\n\n```javascript\na ?? doSomething()\n```\n',6,3,4,5),
(569,'组元素转化为数字','如果有一个数组，想要把数组中的元素转化为数字，可以使用map方法来实现：\n\n```javascript\nconst array = [''12'', ''1'', ''3.1415'', ''-10.01''];\narray.map(Number);  // [12, 1, 3.1415, -10.01]\n```\n',6,3,4,7),
(570,'检查日期是否有效','该方法用于检测给出的日期是否有效：\n\n```javascript\nconst isDateValid = (...val) => !Number.isNaN(new Date(...val).valueOf());\n\nisDateValid(\"December 17, 1995 03:24:00\");  // true\n```\n',6,3,4,9),
(571,'计算两个日期之间的间隔','该方法用于计算两个日期之间的间隔时间：\n\n```javascript\nconst dayDif = (date1, date2) => Math.ceil(Math.abs(date1.getTime() - date2.getTime()) / 86400000)\n\ndayDif(new Date(\"2021-11-3\"), \n       new Date(\"2022-2-1\"))  // 90\n```\n\n距离过年还有90天~',6,3,4,9),
(572,'查找日期位于一年中的第几天','该方法用于检测给出的日期位于今年的第几天：\n\n```javascript\nconst dayOfYear = (date) => Math.floor((date - new Date(date.getFullYear(), 0, 0)) / 1000 / 60 / 60 / 24);\n\ndayOfYear(new Date());   // 307\n```\n\n2021年已经过去300多天了~',6,3,4,9),
(573,'全局挂载','```tsx\n//main\nimport axios from ''./api/axios''\nconst app = createApp(App);\n// 全局ctx(this) 上挂载 $axios\napp.config.globalProperties.$api = axios\n\n//vue\nconst { proxy }: any = getCurrentInstance() //获取上下文实例，ctx=vue2的this\n proxy.$api\n```\n',2,3,4,5),
(574,'简化consolelog','\n\n进行调试时书写很多console.log()就会比较麻烦，使用以下形式来简化：\n\n```javascript\nconst c = console.log.bind(document) \nc(996) \nc(\"hello world\")\n```\n',6,3,4,5),
(575,'生成长度为N的数组','\n```js\n// 生成长度为100的数组\nconst arrN = [...Array(100).keys()]\n// [0,1,2,3,...,99]\n```\n',6,3,4,7);
/*!40000 ALTER TABLE `snippet` ENABLE KEYS */;

-- 
-- Definition of video
-- 

DROP TABLE IF EXISTS `video`;
CREATE TABLE IF NOT EXISTS `video` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '标题',
  `img` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '图片',
  `url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '链接路径',
  `type_id` int(11) NOT NULL COMMENT '分类',
  `user_id` int(11) NOT NULL,
  `time_create` datetime NOT NULL COMMENT '时间',
  `time_modified` datetime NOT NULL,
  PRIMARY KEY (`id`),
  KEY `video_type_id` (`type_id`),
  KEY `video_user_id` (`user_id`),
  CONSTRAINT `video_type_id` FOREIGN KEY (`type_id`) REFERENCES `sn_video_type` (`id`),
  CONSTRAINT `video_user_id` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table video
-- 

/*!40000 ALTER TABLE `video` DISABLE KEYS */;
INSERT INTO `video`(`id`,`name`,`img`,`url`,`type_id`,`user_id`,`time_create`,`time_modified`) VALUES
(25,'我们的故事，英雄联盟！','https://s1.ax1x.com/2020/11/11/BX2acT.png','//player.bilibili.com/player.html?aid=45028887&bvid=BV1zb411b7JK&cid=78863318&page=1',1,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(26,'故事开始的地方——为你的本命献上弹幕吧！','https://s1.ax1x.com/2020/11/11/BX2N90.jpg','//player.bilibili.com/player.html?aid=25180028&bvid=BV18s411j7CL&cid=42677566&page=1',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(27,'用战斗来祭奠这个世界 ！！','https://s1.ax1x.com/2020/11/11/BX2BB4.jpg','//player.bilibili.com/player.html?aid=1328701&bvid=BV1px411N7Yd&cid=2015358&page=1',1,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(28,'前方高能！让世界感受这场视觉盛宴吧！','https://s1.ax1x.com/2020/11/11/BX20uF.jpg','//player.bilibili.com/player.html?aid=50331935&bvid=BV1x441187u5&cid=92865323&page=1',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(29,'这才是忍者世界的巅峰战力！','https://s1.ax1x.com/2020/11/11/BX2U3V.jpg','//player.bilibili.com/player.html?aid=71840112&bvid=BV1HE41167kR&cid=124483833&page=1',1,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(30,'这个故事 还没有完结','https://s1.ax1x.com/2020/11/11/BX2JNn.jpg','//player.bilibili.com/player.html?aid=66382748&bvid=BV1J441117u7&cid=115130259&page=1',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(31,'感受国服配音的魅力吧！ 你的热血从未结霜！！','https://s1.ax1x.com/2020/11/11/BX2Yhq.jpg','//player.bilibili.com/player.html?aid=18767799&bvid=BV1WW411e7wq&cid=30610512&page=1',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(32,'敌人虽众，一击皆斩！','https://s1.ax1x.com/2020/11/11/BX2JNn.jpg','//player.bilibili.com/player.html?aid=22446917&bvid=BV1SW41157WM&cid=37190046&page=1',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(33,'【英雄联盟/CG/燃向】 召唤师 如果没有你们 何为英雄 何为联盟','https://s3.ax1x.com/2020/11/13/Dpmoee.jpg','//player.bilibili.com/player.html?aid=78147108&bvid=BV1fJ411v7Q6&cid=133666781&page=1\" scrolling=\"no\" border=\"0\" frameborder=\"no\" framespacing=\"0\" allowfullscreen=\"true\"',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(34,'【超燃巨作/视听盛宴】我...已被这优雅蒙蔽了双眼... 「英雄联盟系列混剪」','https://s3.ax1x.com/2020/11/13/DpnyX8.jpg','//player.bilibili.com/player.html?aid=49445129&bvid=BV1gb411j7r4&cid=86578090&page=1\" scrolling=\"no\" border=\"0\" frameborder=\"no\" framespacing=\"0\" allowfullscreen=\"true\"',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(35,'那不屈的嘶吼和永不低头的信念使我们迎难而上','https://s3.ax1x.com/2020/11/13/Dputg0.png','//player.bilibili.com/player.html?aid=49661297&bvid=BV1ib41157zQ&cid=86951456&page=1\" scrolling=\"no\" border=\"0\" frameborder=\"no\" framespacing=\"0\" allowfullscreen=\"true\"',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(36,'这就是英雄联盟的魅力！','https://s3.ax1x.com/2020/11/13/DpKDL8.jpg','//player.bilibili.com/player.html?aid=47199816&bvid=BV1nb41147HD&cid=82660090&page=1\" scrolling=\"no\" border=\"0\" frameborder=\"no\" framespacing=\"0\" allowfullscreen=\"true\"',2,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(37,'【火影忍者百万填词】一袋米要扛几楼','https://s3.ax1x.com/2020/11/13/DpQAN4.jpg','//player.bilibili.com/player.html?aid=56970467&bvid=BV1Px411d7em&cid=99503508&page=1\" scrolling=\"no\" border=\"0\" frameborder=\"no\" framespacing=\"0\" allowfullscreen=\"true\"',1,4,'2020-07-28 00:00:00','2020-07-28 00:00:00'),
(38,'『忍び的时代真的结束了吗？』让鸡皮疙瘩和肾上腺素','https://s3.ax1x.com/2020/11/13/DpQxaD.png','//player.bilibili.com/player.html?aid=81848177&bvid=BV1HJ411j771&cid=140045131&page=1\" scrolling=\"no\" border=\"0\" frameborder=\"no\" framespacing=\"0\" allowfullscreen=\"true\"',1,4,'2020-07-28 00:00:00','2020-07-28 00:00:00');
/*!40000 ALTER TABLE `video` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2022-12-07 17:22:55
-- Total time: 0:0:0:0:260 (d:h:m:s:ms)
