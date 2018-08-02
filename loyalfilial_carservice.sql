/*
Navicat MySQL Data Transfer

Source Server         : yzb
Source Server Version : 50624
Source Host           : localhost:3306
Source Database       : loyalfilial_carservice

Target Server Type    : MYSQL
Target Server Version : 50624
File Encoding         : 65001

Date: 2015-09-05 22:39:31
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for auth_authcode
-- ----------------------------
DROP TABLE IF EXISTS `auth_authcode`;
CREATE TABLE `auth_authcode` (
  `MobileNo` bigint(20) NOT NULL,
  `AuthCode` int(11) NOT NULL,
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `State` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`MobileNo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for auth_login
-- ----------------------------
DROP TABLE IF EXISTS `auth_login`;
CREATE TABLE `auth_login` (
  `UserId` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '用户Id',
  `LoginIP` varchar(50) COLLATE utf8_bin NOT NULL COMMENT 'IP',
  `LoginTime` datetime NOT NULL COMMENT '登录时间',
  `ExpiredDate` datetime NOT NULL COMMENT '过期时间',
  `Token` varchar(200) COLLATE utf8_bin NOT NULL COMMENT '令牌',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for auth_loginlog
-- ----------------------------
DROP TABLE IF EXISTS `auth_loginlog`;
CREATE TABLE `auth_loginlog` (
  `LoginId` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '登录Id',
  `UserId` varchar(50) COLLATE utf8_bin NOT NULL,
  `LoginIP` varchar(50) COLLATE utf8_bin NOT NULL,
  `LoginTime` datetime NOT NULL COMMENT '登录时间',
  `LoginType` varchar(10) COLLATE utf8_bin NOT NULL COMMENT '登录or登出',
  PRIMARY KEY (`LoginId`)
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for auth_user
-- ----------------------------
DROP TABLE IF EXISTS `auth_user`;
CREATE TABLE `auth_user` (
  `UserId` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '用户Id',
  `MobileNo` bigint(20) NOT NULL COMMENT '手机号',
  `UserName` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '用户名',
  `Password` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '密码',
  `State` int(11) NOT NULL DEFAULT '1' COMMENT '状态',
  `ReTryTimes` int(11) NOT NULL COMMENT '重试次数(5次机会）',
  `UserType` int(11) NOT NULL COMMENT '用户类型（1：维修厂；2：汽配商；3：车主）',
  `TargetId` int(11) DEFAULT NULL COMMENT '汽修厂ID、汽配商ID',
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '注册时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for b_carparts
-- ----------------------------
DROP TABLE IF EXISTS `b_carparts`;
CREATE TABLE `b_carparts` (
  `CarPartsId` int(11) NOT NULL AUTO_INCREMENT COMMENT '汽修厂Id',
  `CarPartsName` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '汽修厂名称',
  `CityId` int(11) NOT NULL DEFAULT '1' COMMENT '城市Id',
  `CityName` varchar(50) COLLATE utf8_bin NOT NULL DEFAULT '上海' COMMENT '城市名称',
  `Address` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '地址',
  `TelNo1` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '电话',
  `TelNo2` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '电话',
  `TelNo3` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '电话',
  `FaxNo` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `MobileNo1` bigint(20) NOT NULL COMMENT '手机',
  `MobileNo2` bigint(20) DEFAULT NULL COMMENT '手机',
  `ContactName` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '联系人',
  `MainServices` varchar(200) COLLATE utf8_bin NOT NULL COMMENT '主营',
  `Alliance` varchar(100) COLLATE utf8_bin DEFAULT NULL COMMENT '联盟',
  `State` int(11) NOT NULL DEFAULT '1' COMMENT '状态（1:有效;0:无效）',
  `CreateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '新建时间',
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`CarPartsId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for b_city
-- ----------------------------
DROP TABLE IF EXISTS `b_city`;
CREATE TABLE `b_city` (
  `CityId` int(11) NOT NULL AUTO_INCREMENT COMMENT '城市Id',
  `CityName` varchar(255) COLLATE utf8_bin NOT NULL COMMENT '城市名称',
  `State` int(11) NOT NULL DEFAULT '1' COMMENT '状态',
  PRIMARY KEY (`CityId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for b_mainserviceitem
-- ----------------------------
DROP TABLE IF EXISTS `b_mainserviceitem`;
CREATE TABLE `b_mainserviceitem` (
  `MainServiceItemId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主营id',
  `ItemName` varchar(20) COLLATE utf8_bin NOT NULL COMMENT '主营',
  `State` int(11) NOT NULL DEFAULT '1' COMMENT '状态',
  PRIMARY KEY (`MainServiceItemId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for b_maintainfactory
-- ----------------------------
DROP TABLE IF EXISTS `b_maintainfactory`;
CREATE TABLE `b_maintainfactory` (
  `MaintainFactoryId` int(11) NOT NULL AUTO_INCREMENT COMMENT '汽修厂Id',
  `MaintainName` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '汽修厂名称',
  `CityId` int(11) NOT NULL DEFAULT '1' COMMENT '城市Id',
  `CityName` varchar(50) COLLATE utf8_bin NOT NULL DEFAULT '上海' COMMENT '城市名称',
  `Address` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '地址',
  `TelNo` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '电话',
  `MobileNo` bigint(20) NOT NULL COMMENT '手机',
  `ContactName` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '联系人',
  `MainServices` varchar(200) COLLATE utf8_bin NOT NULL COMMENT '主营',
  `Alliance` varchar(100) COLLATE utf8_bin DEFAULT NULL COMMENT '联盟',
  `State` int(11) NOT NULL DEFAULT '1' COMMENT '状态（1:有效;0:无效）',
  `SMSCount` int(11) NOT NULL DEFAULT '0' COMMENT '短信数量',
  `CreateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '新建时间',
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`MaintainFactoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for carservice_log
-- ----------------------------
DROP TABLE IF EXISTS `carservice_log`;
CREATE TABLE `carservice_log` (
  `LogId` bigint(20) NOT NULL AUTO_INCREMENT,
  `Level` varchar(10) COLLATE utf8_bin NOT NULL DEFAULT '',
  `Ip` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `LogTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Title` varchar(100) COLLATE utf8_bin NOT NULL DEFAULT '',
  `Message` varchar(1000) COLLATE utf8_bin NOT NULL DEFAULT '',
  `Exception` varchar(2000) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`LogId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for favorites
-- ----------------------------
DROP TABLE IF EXISTS `favorites`;
CREATE TABLE `favorites` (
  `FavoriteId` int(11) NOT NULL AUTO_INCREMENT COMMENT '收藏Id',
  `UserId` varchar(50) COLLATE utf8_bin NOT NULL,
  `TargetType` int(11) NOT NULL COMMENT '收藏目标类型（1：汽配商、2：汽修厂、3：车主 ）',
  `TargetId` int(11) NOT NULL COMMENT '收藏目标Id(汽配商Id、汽修厂Id、车主Id)',
  `FavoriteTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '收藏时间',
  PRIMARY KEY (`FavoriteId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for f_customer
-- ----------------------------
DROP TABLE IF EXISTS `f_customer`;
CREATE TABLE `f_customer` (
  `MFCustomerId` int(11) NOT NULL AUTO_INCREMENT COMMENT '供应商客户ID',
  `CustormerName` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '客户姓名',
  `MaintainFactoryId` int(11) DEFAULT NULL COMMENT '维修厂Id',
  `CustomerId` int(11) DEFAULT NULL COMMENT '客户Id',
  `Address` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '住址',
  `CarBrand` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '品牌',
  `CarType` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '车型',
  `PlateNO` varchar(10) COLLATE utf8_bin NOT NULL COMMENT '车牌号',
  `MobileNo` bigint(20) NOT NULL COMMENT '手机号',
  `VIN` varchar(50) COLLATE utf8_bin NOT NULL COMMENT '车架码',
  `BirthDay` datetime DEFAULT NULL COMMENT '生日',
  `State` int(11) NOT NULL COMMENT '状态：1：正式客户，0：潜在客户;',
  `Remark` varchar(1000) COLLATE utf8_bin DEFAULT NULL COMMENT '备注',
  `CreateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`MFCustomerId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for f_inquiry
-- ----------------------------
DROP TABLE IF EXISTS `f_inquiry`;
CREATE TABLE `f_inquiry` (
  `InquiryId` int(11) NOT NULL AUTO_INCREMENT COMMENT '询价单Id',
  `InquiryDate` datetime NOT NULL COMMENT '询价日期',
  `MaintainFactoryId` int(11) NOT NULL COMMENT '汽修厂Id',
  `CarBrand` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '品牌',
  `CarType` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '车型',
  `VIN` varchar(50) COLLATE utf8_bin DEFAULT NULL COMMENT '车架码',
  `MaintainListImage` blob COMMENT '维修清单(图片)',
  `PartsImage` blob COMMENT '配件清单(图片)',
  `PartsName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '配件名称',
  `Quantity` decimal(10,0) NOT NULL COMMENT '数量',
  `Remark` varchar(500) COLLATE utf8_bin DEFAULT NULL COMMENT '描述',
  `Alliance` varchar(100) COLLATE utf8_bin DEFAULT NULL COMMENT '联盟',
  `CarPartsId` varchar(255) COLLATE utf8_bin DEFAULT NULL COMMENT '指定汽配商Id',
  `PlanDeliveryDate` datetime NOT NULL COMMENT '要求交货日期',
  `State` int(11) NOT NULL DEFAULT '0' COMMENT '状态（-1：作废；0：制作中；1：有效；2：已成交）',
  `IsAnonymity` int(11) NOT NULL DEFAULT '0' COMMENT '是否匿名发布（1：是；0：否）',
  `ToCarPartsType` int(11) NOT NULL DEFAULT '0' COMMENT '可见汽配商类型（1：指定供应商；2：收藏供应商）',
  `PurchaseId` int(11) NOT NULL DEFAULT '0' COMMENT '采购单Id',
  `CreateUser` varchar(50) COLLATE utf8_bin NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`InquiryId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for f_maintain
-- ----------------------------
DROP TABLE IF EXISTS `f_maintain`;
CREATE TABLE `f_maintain` (
  `MFMaintainId` int(11) NOT NULL AUTO_INCREMENT COMMENT '维保记录Id',
  `MFCustomerId` int(11) NOT NULL COMMENT '汽修厂客户Id',
  `MaintainFactoryId` int(11) NOT NULL COMMENT '汽修厂Id',
  `ServiceItem` varchar(500) COLLATE utf8_bin NOT NULL COMMENT '维保项目',
  `RemindDate` datetime NOT NULL COMMENT '提醒时间',
  `MaintainDate` datetime NOT NULL COMMENT '维保时间',
  `State` int(11) NOT NULL DEFAULT '1' COMMENT '状态',
  `CreateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`MFMaintainId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for f_purchase
-- ----------------------------
DROP TABLE IF EXISTS `f_purchase`;
CREATE TABLE `f_purchase` (
  `PurchaseId` int(11) NOT NULL AUTO_INCREMENT COMMENT '报价单Id',
  `PurchaseDate` datetime NOT NULL COMMENT '报价日期',
  `MaintainFactoryId` int(11) NOT NULL COMMENT '汽修厂Id',
  `CarPartsId` int(11) NOT NULL COMMENT '汽配Id',
  `QuotationId` int(11) NOT NULL COMMENT '询价单Id',
  `InquiryId` int(11) NOT NULL,
  `PartsName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '配件清单',
  `Quantity` decimal(10,0) NOT NULL COMMENT '数量',
  `Price` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '单价',
  `Amount` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '总价',
  `Remark` varchar(500) COLLATE utf8_bin DEFAULT NULL COMMENT '备注',
  `State` int(11) NOT NULL,
  `DeliverId` int(11) NOT NULL DEFAULT '0' COMMENT '发货单Id',
  `CreateUser` varchar(50) COLLATE utf8_bin NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`PurchaseId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for f_reserved
-- ----------------------------
DROP TABLE IF EXISTS `f_reserved`;
CREATE TABLE `f_reserved` (
  `MFReserveId` int(11) NOT NULL AUTO_INCREMENT COMMENT '预约Id',
  `MFCustomerId` int(11) NOT NULL COMMENT '维修厂的客户Id',
  `MaintainFactoryId` int(11) NOT NULL COMMENT '维修厂Id',
  `ServiceItem` varchar(500) COLLATE utf8_bin NOT NULL COMMENT '保养项目',
  `ReservedDate` datetime NOT NULL COMMENT '预约日期',
  `MobileNo` bigint(11) NOT NULL COMMENT '预约手机号',
  `State` int(11) NOT NULL DEFAULT '1' COMMENT '状态（-3：维修厂拒绝；-2：客户取消；-1：预约失败；0：预约中；1：预约成功；2：维保完成）',
  `ReserveType` int(11) DEFAULT NULL COMMENT '预约方式（1：电话预约；2：短信预约；3：网站预约；4：QQ预约；5：微信预约）',
  `Remark` varchar(500) COLLATE utf8_bin DEFAULT NULL COMMENT '备注',
  `FeedBack` varchar(500) COLLATE utf8_bin DEFAULT NULL COMMENT '服务反馈',
  `CreateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`MFReserveId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for f_smssendlog
-- ----------------------------
DROP TABLE IF EXISTS `f_smssendlog`;
CREATE TABLE `f_smssendlog` (
  `SendId` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '发送Id',
  `SendTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发送时间',
  `MobileNo` bigint(11) NOT NULL,
  `Content` varchar(500) COLLATE utf8_bin NOT NULL COMMENT '发送内容',
  `ReceivedId` varchar(100) COLLATE utf8_bin DEFAULT NULL COMMENT '对方Id',
  `ReceivedResult` varchar(500) COLLATE utf8_bin DEFAULT NULL,
  `State` int(11) NOT NULL DEFAULT '0' COMMENT '发送状态（0,：待发送；1：发送成功；-1：发送失败）',
  PRIMARY KEY (`SendId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for p_deliver
-- ----------------------------
DROP TABLE IF EXISTS `p_deliver`;
CREATE TABLE `p_deliver` (
  `DeliverId` int(11) NOT NULL AUTO_INCREMENT COMMENT '报价单Id',
  `DeliverDate` datetime NOT NULL COMMENT '报价日期',
  `CarPartsId` int(11) NOT NULL COMMENT '汽配商Id',
  `QuotationId` int(11) NOT NULL COMMENT '询价单Id',
  `PurchaseId` int(11) NOT NULL DEFAULT '0' COMMENT '采购单号',
  `PartsName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '配件清单',
  `Quantity` decimal(10,0) NOT NULL COMMENT '数量',
  `Price` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '单价',
  `Amount` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '总价',
  `Remark` varchar(500) COLLATE utf8_bin DEFAULT NULL COMMENT '备注',
  `State` int(11) NOT NULL,
  `ActualDeliverID` varchar(50) COLLATE utf8_bin NOT NULL DEFAULT '' COMMENT '发货单号',
  `DeliverReturnId` int(11) NOT NULL DEFAULT '0' COMMENT '发货退回单Id',
  `CreateUser` varchar(50) COLLATE utf8_bin NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`DeliverId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for p_deliverreturn
-- ----------------------------
DROP TABLE IF EXISTS `p_deliverreturn`;
CREATE TABLE `p_deliverreturn` (
  `DeliverReturnId` int(11) NOT NULL AUTO_INCREMENT COMMENT '报价单Id',
  `DeliverReturnDate` datetime NOT NULL COMMENT '报价日期',
  `CarPartsId` int(11) NOT NULL COMMENT '汽配商Id',
  `DeliverId` int(11) NOT NULL COMMENT '询价单Id',
  `PurchaseId` int(11) NOT NULL DEFAULT '0' COMMENT '采购单号',
  `PartsName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '配件清单',
  `Quantity` decimal(10,0) NOT NULL COMMENT '数量',
  `Price` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '单价',
  `Amount` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '总价',
  `Remark` varchar(500) COLLATE utf8_bin DEFAULT NULL COMMENT '备注',
  `State` int(11) NOT NULL,
  `CreateUser` varchar(50) COLLATE utf8_bin NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`DeliverReturnId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
-- Table structure for p_quotation
-- ----------------------------
DROP TABLE IF EXISTS `p_quotation`;
CREATE TABLE `p_quotation` (
  `QuotationId` int(11) NOT NULL AUTO_INCREMENT COMMENT '报价单Id',
  `QuotationDate` datetime NOT NULL COMMENT '报价日期',
  `CarPartsId` int(11) NOT NULL COMMENT '汽配商Id',
  `InquiryId` int(11) NOT NULL COMMENT '询价单Id',
  `PartsName` varchar(100) COLLATE utf8_bin NOT NULL COMMENT '配件清单',
  `Quantity` decimal(10,0) NOT NULL COMMENT '数量',
  `Price` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '单价',
  `Amount` decimal(10,0) NOT NULL DEFAULT '0' COMMENT '总价',
  `Remark` varchar(500) COLLATE utf8_bin DEFAULT NULL COMMENT '备注',
  `State` int(11) NOT NULL,
  `PurchaseId` int(11) NOT NULL DEFAULT '0' COMMENT '采购单Id',
  `CreateUser` varchar(50) COLLATE utf8_bin NOT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateUser` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`QuotationId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
