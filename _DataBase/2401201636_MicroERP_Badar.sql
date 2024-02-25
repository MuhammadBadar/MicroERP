/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80020
 Source Host           : localhost:3306
 Source Schema         : qamsofterp

 Target Server Type    : MySQL
 Target Server Version : 80020
 File Encoding         : 65001

 Date: 20/02/2024 16:36:31
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for att_attendance
-- ----------------------------
DROP TABLE IF EXISTS `att_attendance`;
CREATE TABLE `att_attendance`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `SchDayId` int NULL DEFAULT NULL,
  `DayStartTime` datetime NULL DEFAULT NULL,
  `DayEndTime` datetime NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `ModifiedBy` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of att_attendance
-- ----------------------------

-- ----------------------------
-- Table structure for ctl_clients
-- ----------------------------
DROP TABLE IF EXISTS `ctl_clients`;
CREATE TABLE `ctl_clients`  (
  `Id` int NOT NULL,
  `ModuleIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CountryId` int NULL DEFAULT NULL,
  `CityId` int NULL DEFAULT NULL,
  `CategoryId` int NULL DEFAULT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ClientName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Contact` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Owner` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `RelevantPerson` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_clients
-- ----------------------------
INSERT INTO `ctl_clients` VALUES (1, '1001004', 0, 0, 1051001, '3bba76e4-9e83-4ac2-9ef1-1b26e2e300d5', 'Client_1', NULL, '07678689789', '.', NULL, '2023-09-22 00:57:09', 0, 0, '2023-09-22 00:57:09', b'1');
INSERT INTO `ctl_clients` VALUES (2, '1001003,1001004', NULL, NULL, 1051001, 'd0f71c3a-0192-4446-a594-68b4c75cba90', 'Client_2', NULL, '9789890809', '.', NULL, '2023-09-22 00:57:23', 0, 0, '2023-09-22 00:57:23', b'1');
INSERT INTO `ctl_clients` VALUES (3, '1001003', 1015004, 1016011, 1051002, '8d4b78f3-a98b-4c48-80be-7435514a3ccd', 'QAMSOFT', 'Fatima Heights near Noble Hospital Rehman pura , Ichra ,Lahore', '03234027206', 'Muhammad Badar', 'Sumaira Ameer', '2023-09-22 11:17:09', 0, 0, '2023-09-22 11:17:09', b'1');
INSERT INTO `ctl_clients` VALUES (4, '1001003,1001004', 0, 0, 1051001, '41d895c7-ffc3-4ff2-a0f2-e305e4a21802', 'Client_3', 'address', '0978798797', 'OWNER', NULL, '2023-09-29 09:13:47', 0, 0, '2023-09-29 09:13:47', b'1');
INSERT INTO `ctl_clients` VALUES (5, '1001003,1001004', NULL, NULL, 1051001, 'c7f53801-a301-437a-8627-6c8962646923', 'CLT_4', '..', '078687879676', '...', NULL, '2023-09-30 16:51:50', 0, 0, '2023-09-30 16:51:50', b'1');

-- ----------------------------
-- Table structure for ctl_customer
-- ----------------------------
DROP TABLE IF EXISTS `ctl_customer`;
CREATE TABLE `ctl_customer`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `AccId` int NULL DEFAULT NULL,
  `CountryId` int NULL DEFAULT NULL,
  `CityId` int NULL DEFAULT NULL,
  `SupplierId` int NULL DEFAULT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Phone` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Address` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Region` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `SendEmail` bit(1) NULL DEFAULT NULL,
  `IsSupplier` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_customer
-- ----------------------------

-- ----------------------------
-- Table structure for ctl_enumline
-- ----------------------------
DROP TABLE IF EXISTS `ctl_enumline`;
CREATE TABLE `ctl_enumline`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `ModuleId` int UNSIGNED NULL DEFAULT NULL,
  `EnumTypeId` int NOT NULL,
  `ParentId` int NULL DEFAULT NULL,
  `LevelId` int NULL DEFAULT NULL,
  `KeyCode` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `AccountCode` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Description` varchar(3000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsSystemDefined` bit(1) NULL DEFAULT NULL,
  `IstAccountLevel` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_enumline
-- ----------------------------
INSERT INTO `ctl_enumline` VALUES (1001001, 0, 0, 1001, 0, 0, '/client', NULL, 'CLT', 'CLT', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001001, 1, 1001004, 1001, 0, 0, 'Radio Button_Yes.', NULL, 'Yes.', 'Yes.', NULL, b'0', b'0', '2023-09-27 02:08:18', 0, '2023-09-27 02:08:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001002, 0, 0, 1001, 0, 0, '/security', NULL, 'SEC', 'SEC', NULL, b'1', b'0', '2023-09-25 12:25:00', 0, '2023-09-25 12:25:00', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001002, 1, 1001004, 1001, 0, 0, 'No', NULL, 'No', 'No', NULL, b'0', b'0', '2023-09-27 02:08:18', 0, '2023-09-27 02:08:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001003, 0, 0, 1001, 0, 0, '/account', NULL, 'GL', 'GL', NULL, b'0', b'0', '2023-09-25 12:25:14', 0, '2023-09-25 12:25:14', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001004, 0, 0, 1001, 0, 0, '/pms', NULL, 'PMS', 'PMS', NULL, b'0', b'0', '2023-09-25 12:25:26', 0, '2023-09-25 12:25:26', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001005, 0, 0, 1001, 0, 0, '/settings/manageSetting', NULL, 'Settings', 'Settings', NULL, b'1', b'0', '2023-09-27 00:39:11', 0, '2023-09-27 00:39:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001006, 0, 0, 1001, 0, 0, '/task', NULL, 'Task', 'Task', NULL, b'1', b'0', '2024-02-14 02:12:51', 0, '2024-02-14 02:12:51', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001007, 0, 0, 1001, 0, 0, '/att/att', NULL, 'Attendance', 'Attendance', NULL, b'1', b'0', '2024-02-14 02:18:32', 0, '2024-02-14 02:18:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001008, 0, 0, 1001, 0, 0, '/sch/sch/Schedule', NULL, 'Schedule', 'Schedule', NULL, b'1', b'0', '2024-02-14 02:23:09', 0, '2024-02-14 02:23:09', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1001009, 0, 0, 1001, 0, 0, '/lms/lms/Vocabulary', NULL, 'Vocabulary', 'Vocabulary', NULL, b'1', b'0', '2024-02-14 02:24:12', 0, '2024-02-14 02:24:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002001, 0, 0, 1002, 1001001, 0, '/client/List/cltList', NULL, 'Clients', 'Clients', NULL, b'1', b'0', '2023-09-25 12:43:00', 0, '2023-09-25 12:43:00', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002002, 0, 0, 1002, 1001002, 0, '/security/security/users?moduleId=0', NULL, 'Manage Users', 'Manage Users', NULL, b'1', b'0', '2023-09-25 12:43:56', 0, '2023-09-25 12:43:56', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002003, 0, 0, 1002, 1001002, 0, 'security/security/roles?type=1054', NULL, 'Manage Roles', 'Manage Roles', NULL, b'1', b'0', '2023-09-25 12:44:32', 0, '2023-09-25 12:44:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002004, 0, 0, 1002, 1001003, 0, '/account/accounts', NULL, 'Accounts', 'Accounts', NULL, b'1', b'0', '2023-09-25 12:46:32', 0, '2023-09-25 12:46:32', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002005, 0, 0, 1002, 1001003, 0, '/account/product', NULL, 'Product', 'Product', NULL, b'1', b'0', '2023-09-25 12:48:48', 0, '2023-09-25 12:48:48', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002006, 0, 0, 1002, 1001003, 0, '/account/tax', NULL, 'Tax', 'Tax', NULL, b'1', b'0', '2023-09-25 12:49:43', 0, '2023-09-25 12:49:43', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002007, 0, 0, 1002, 1001004, 0, '/pms/appt/appmntList', NULL, 'Appointments', 'Appointments', NULL, b'1', b'0', '2023-09-25 12:59:35', 0, '2023-09-25 12:59:35', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002008, 0, 0, 1002, 1001004, 0, '/pms/doctor/doctorList', NULL, 'Doctor', 'Doctor', NULL, b'1', b'0', '2023-09-25 13:01:15', 0, '2023-09-25 13:01:15', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002009, 0, 0, 1002, 1001004, 0, '/pms/patient', NULL, 'Patient', 'Patient', NULL, b'1', b'0', '2023-09-25 13:01:43', 0, '2023-09-25 13:01:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002010, 0, 0, 1002, 1001004, 0, '/pms/rx', NULL, 'Patient Visits', 'Patient Visits', NULL, b'1', b'0', '2023-09-25 13:02:49', 0, '2023-09-25 13:02:49', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002011, 0, 0, 1002, 1001003, 0, '/account/stakeHolder', NULL, 'Stake Holders', 'Stake Holders', NULL, b'1', b'0', '2023-09-25 13:25:00', 0, '2023-09-25 13:25:00', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002012, 0, 0, 1002, 1001003, 0, '/account/settings/manageSetting?type=0&moduleId=1001003', NULL, 'GL Settings', 'GL Settings', NULL, b'1', b'0', '2023-09-26 14:48:55', 0, '2023-09-26 14:48:55', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002013, 0, 0, 1002, 1001004, 0, '/pms/settings/manageSetting?type=0&moduleId=1001004', NULL, 'PMS Settings', 'PMS Settings', NULL, b'1', b'0', '2023-09-26 14:55:30', 0, '2023-09-26 14:55:30', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002014, 0, 0, 1002, 1001003, 0, '/account/security', NULL, 'Security', 'Security', NULL, b'1', b'0', '2023-09-27 11:46:04', 0, '2023-09-27 11:46:04', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002015, 0, 0, 1002, 1001004, 0, '/pms/security', NULL, 'Security', 'Security', NULL, b'1', b'0', '2023-09-27 11:46:32', 0, '2023-09-27 11:46:32', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1002016, 0, 0, 1002, 1001004, 0, '/pms/pms/manageMedicine', NULL, 'Manage Medicine', 'Manage Medicine', NULL, b'1', b'0', '2023-09-27 17:14:38', 0, '2023-09-27 17:14:38', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002017, 0, 0, 1002, 1001004, 0, '/pms/staff/staffList', NULL, 'Staff', 'Staff', NULL, b'1', b'0', '2023-10-09 14:20:12', 0, '2023-10-09 14:20:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002018, 0, 0, 1002, 1001003, 0, '/account/accounts/accountList	', NULL, 'Chart of Account', 'Chart of Account', NULL, b'1', b'0', '2023-10-09 23:00:41', 0, '2023-10-09 23:00:41', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002019, 0, 0, 1002, 1001003, 0, '/account/accounts/vchType', NULL, 'Manage Voucher Type', 'Manage Voucher Type', NULL, b'1', b'0', '2023-10-09 23:03:02', 0, '2023-10-09 23:03:02', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002020, 0, 0, 1002, 1001003, 0, '/account/accounts/vchList ', NULL, 'Voucher', 'Voucher', NULL, b'1', b'0', '2023-10-09 23:03:43', 0, '2023-10-09 23:03:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002021, 0, 0, 1002, 1001002, 0, 'security/security/perms', NULL, 'Manage Permissions', 'Manage Permissions', NULL, b'1', b'0', '2023-10-24 09:35:46', 0, '2023-10-24 09:35:46', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002022, 0, 0, 1002, 1001002, 0, 'security/security/users', NULL, 'Manage User', 'Manage User', NULL, b'1', b'0', '2024-02-13 17:28:20', 0, '2024-02-13 17:28:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002023, 0, 0, 1002, 1001006, 0, '/task/task/taskList', NULL, 'Task List', 'Task List', NULL, b'1', b'0', '2024-02-14 02:13:49', 0, '2024-02-14 02:13:49', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002024, 0, 0, 1002, 1001007, 0, '/att/att/attReport', NULL, 'Summary Report', 'Summary Report', NULL, b'1', b'0', '2024-02-14 02:19:39', 0, '2024-02-14 02:19:39', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1002025, 0, 0, 1002, 1001007, 0, '/att/att/attDetReport', NULL, 'Detail Report', 'Detail Report', NULL, b'1', b'0', '2024-02-14 02:20:15', 0, '2024-02-14 02:20:15', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1003001, 0, 0, 1003, 1002009, 0, '/pms/patient/patientList', NULL, 'Patients', 'Patients', NULL, b'1', b'0', '2023-09-25 13:04:37', 0, '2023-09-25 13:04:37', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1003001, 3, 1001004, 1003, 0, 0, 'A+', NULL, 'A+', 'A+', NULL, b'0', b'0', '2023-09-27 02:36:05', 0, '2023-09-27 02:36:05', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1003002, 0, 0, 1003, 1002009, 0, '/pms/patient/patFieldList', NULL, 'Patient Parameters', 'Patient Parameters', NULL, b'1', b'0', '2023-09-25 13:05:08', 0, '2023-09-25 13:05:08', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1003002, 3, 1001004, 1003, 0, 0, 'B+', NULL, 'B+', 'B+', NULL, b'0', b'0', '2023-09-27 02:36:05', 0, '2023-09-27 02:36:05', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1003003, 0, 0, 1003, 1002010, 0, '/pms/rx/rxList', NULL, 'Patient Visit', 'Patient Visit', NULL, b'1', b'0', '2023-09-25 13:11:15', 0, '2023-09-25 13:11:15', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1003004, 0, 0, 1003, 1002010, 0, '/pms/rx/rxMedExtraFieldsList', NULL, 'Patient Visit Parameters', 'Patient Visit Parameters', NULL, b'1', b'0', '2023-09-25 13:12:32', 0, '2023-09-25 13:12:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1003005, 0, 0, 1003, 1002004, 0, '/account/accounts/accountList', NULL, 'Chart of Accounts', 'Chart of Accounts', NULL, b'1', b'0', '2023-09-25 13:17:42', 0, '2023-09-25 13:17:42', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003006, 0, 0, 1003, 1002004, 0, '/account/accounts/vchList', NULL, 'Voucher ', 'Voucher ', NULL, b'1', b'0', '2023-09-25 13:18:28', 0, '2023-09-25 13:18:28', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003007, 0, 0, 1003, 1002004, 0, '/account/accounts/salesList', NULL, 'Sales', 'Sales', NULL, b'1', b'0', '2023-09-25 13:20:15', 0, '2023-09-25 13:20:15', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003008, 0, 0, 1003, 1002005, 0, '/account/product/proList', NULL, 'Products', 'Products', NULL, b'1', b'0', '2023-09-25 13:26:43', 0, '2023-09-25 13:26:43', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003009, 0, 0, 1003, 1002005, 0, '/account/product/attValues', NULL, 'Product Attributes', 'Product Attributes', NULL, b'1', b'0', '2023-09-25 13:27:29', 0, '2023-09-25 13:27:29', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003010, 0, 0, 1003, 1002005, 0, '/account/product/uom', NULL, 'UOM', 'UOM', NULL, b'1', b'0', '2023-09-25 13:28:13', 0, '2023-09-25 13:28:13', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003011, 0, 0, 1003, 1002005, 0, '/account/product/uomConvrn', NULL, 'UOM Converson', 'UOM Converson', NULL, b'1', b'0', '2023-09-25 13:28:46', 0, '2023-09-25 13:28:46', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003012, 0, 0, 1003, 1002005, 0, '/account/product/docExtras', NULL, 'Doc Extras', 'Doc Extras', NULL, b'1', b'0', '2023-09-25 13:29:27', 0, '2023-09-25 13:29:27', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003013, 0, 0, 1003, 1002006, 0, '/account/tax/taxes', NULL, 'Manage Taxes', 'Manage Taxes', NULL, b'1', b'0', '2023-09-25 13:30:02', 0, '2023-09-25 13:30:02', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003014, 0, 0, 1003, 1002006, 0, '/account/tax/proTaxes', NULL, 'Product Taxes', 'Product Taxes', NULL, b'1', b'0', '2023-09-25 13:30:33', 0, '2023-09-25 13:30:33', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003015, 0, 0, 1003, 1002011, 0, '/account/stakeHolder/customer', NULL, 'Customer', 'Customer', NULL, b'1', b'0', '2023-09-25 13:31:11', 0, '2023-09-25 13:31:11', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003016, 0, 0, 1003, 1002011, 0, '/account/stakeHolder/supplier', NULL, 'Supplier', 'Supplier', NULL, b'1', b'0', '2023-09-25 13:31:37', 0, '2023-09-25 13:31:37', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003017, 0, 0, 1003, 1002014, 0, '/account/security/security/users?moduleId=1001003', NULL, 'Manage User', 'Manage User', NULL, b'1', b'0', '2023-09-27 11:48:26', 0, '2023-09-27 11:48:26', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003018, 0, 0, 1003, 1002014, 0, '/account/security/catalog/enumLine?type=1054&module=1001003', NULL, 'Manage Roles', 'Manage Roles', NULL, b'1', b'0', '2023-09-27 11:50:26', 0, '2023-09-27 11:50:26', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003019, 0, 0, 1003, 1002015, 0, '/pms/security/security/users?moduleId=1001004', NULL, 'Manage User', 'Manage User', NULL, b'1', b'0', '2023-09-27 11:53:11', 0, '2023-09-27 11:53:11', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1003020, 0, 0, 1003, 1002015, 0, '/pms/security/catalog/enumLine?type=1054&module=1001004', NULL, 'Manage Roles', 'Manage Roles', NULL, b'1', b'0', '2023-09-27 11:53:37', 0, '2023-09-27 11:53:37', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1004001, 0, 1001003, 1004, 0, 1006, '10', '10', 'Fixed Assets', NULL, NULL, b'0', b'0', '2023-08-21 10:59:17', 0, '2023-08-21 10:59:17', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004001, 1, 1001004, 1004, 0, 0, 'Yes', NULL, 'Yes', 'Yes', NULL, b'0', b'0', '2023-10-06 09:00:44', 0, '2023-10-06 09:00:44', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004001, 2, 1001003, 1004, 0, 1006, '18', '18', 'Current Assets', NULL, NULL, b'0', b'0', '2023-10-09 23:54:42', 0, '2023-10-09 23:54:42', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004001, 3, 1001003, 1004, 0, 1006, '10', '10', 'Fixed Assets', NULL, NULL, b'0', b'0', '2023-10-09 23:42:24', 0, '2023-10-09 23:42:24', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004002, 0, 1001003, 1004, 0, 1006, '18', '18', 'Current Assets', NULL, NULL, b'0', b'0', '2023-08-21 10:59:33', 0, '2023-08-21 10:59:33', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004002, 1, 1001004, 1004, 0, 0, 'No', NULL, 'No', 'No', NULL, b'0', b'0', '2023-10-06 09:00:44', 0, '2023-10-06 09:00:44', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004002, 2, 1001003, 1004, 1004001, 1007, '180', '18-180', 'Stock & Stores', NULL, NULL, b'0', b'0', '2023-10-10 01:32:49', 0, '2023-10-10 01:32:49', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004002, 3, 1001003, 1004, 1004001, 1007, '100', '10-100', 'Fixed Assets-Tangible', NULL, NULL, b'0', b'0', '2023-10-09 23:43:20', 0, '2023-10-09 23:43:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004003, 0, 1001003, 1004, 0, 1006, '41', '41', 'Capital & Reserves', NULL, NULL, b'0', b'0', '2023-08-21 10:59:57', 0, '2023-08-21 10:59:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004003, 2, 1001003, 1004, 1004002, 1008, '1801', '18-180-1801', 'Stock in Trade', NULL, NULL, b'0', b'0', '2023-10-10 01:33:11', 0, '2023-10-10 01:33:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004003, 3, 1001003, 1004, 1004002, 1008, '1001', '10-100-1001', 'Building - Office', NULL, NULL, b'0', b'0', '2023-10-09 23:44:35', 0, '2023-10-09 23:44:35', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004004, 0, 1001003, 1004, 0, 1006, '61', '61', 'Current Liabilities', NULL, NULL, b'0', b'0', '2023-08-21 11:00:14', 0, '2023-08-21 11:00:14', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004004, 2, 1001003, 1004, 1004001, 1007, '350', '18-350', 'Tax withholding', NULL, NULL, b'0', b'0', '2023-10-10 01:54:00', 0, '2023-10-10 01:54:00', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004004, 3, 1001003, 1004, 1004002, 1008, '1002', '10-100-1002', 'Building - Factory', NULL, NULL, b'0', b'0', '2023-10-09 23:45:02', 0, '2023-10-09 23:45:02', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004005, 0, 1001003, 1004, 0, 1006, '71', '71', 'Total Revenue', NULL, NULL, b'0', b'0', '2023-08-21 11:00:29', 0, '2023-08-21 11:00:29', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004005, 2, 1001003, 1004, 1004004, 1008, '3501', '18-350-3501', 'Tax deduction - Customers', NULL, NULL, b'0', b'0', '2023-10-10 01:54:22', 0, '2023-10-10 01:54:22', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004005, 3, 1001003, 1004, 1004002, 1008, '1003', '10-100-1003', 'Plant & Machinery', NULL, NULL, b'0', b'0', '2023-10-09 23:53:14', 0, '2023-10-09 23:53:14', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004006, 0, 1001003, 1004, 0, 1006, '75', '75', 'Cost of Goods Sold', NULL, NULL, b'0', b'0', '2023-08-21 11:00:48', 0, '2023-08-21 11:00:48', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004006, 3, 1001003, 1004, 1004002, 1008, '1004', '10-100-1004', 'Electrical Appliances', NULL, NULL, b'0', b'0', '2023-10-09 23:57:43', 0, '2023-10-09 23:57:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004007, 0, 1001003, 1004, 0, 1006, '84', '84', 'Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:01:10', 0, '2023-08-21 11:01:10', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004008, 0, 1001003, 1004, 1004001, 1007, '100', '10-100', 'Fixed Assets-Tangible', NULL, NULL, b'0', b'0', '2023-08-21 11:01:43', 0, '2023-08-21 11:01:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004009, 0, 1001003, 1004, 1004002, 1007, '180', '18-180', 'Stock & Stores', NULL, NULL, b'0', b'0', '2023-08-21 11:02:45', 0, '2023-08-21 11:02:45', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004010, 0, 1001003, 1004, 1004002, 1007, '190', '18-190', 'Customers', NULL, NULL, b'0', b'0', '2023-08-21 11:02:56', 0, '2023-08-21 11:02:56', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004011, 0, 1001003, 1004, 1004002, 1007, '250', '18-250', 'Advances, Deposits & Prepayments', NULL, NULL, b'0', b'0', '2023-08-21 11:03:31', 0, '2023-08-21 11:03:31', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004012, 0, 1001003, 1004, 1004002, 1007, '350', '18-350', 'Tax withholding', NULL, NULL, b'0', b'0', '2023-08-21 11:03:52', 0, '2023-08-21 11:03:52', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004013, 0, 1001003, 1004, 1004002, 1007, '360', '18-360', 'Cash & Bank Balances', NULL, NULL, b'0', b'0', '2023-08-21 11:04:08', 0, '2023-08-21 11:04:08', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004014, 0, 1001003, 1004, 1004003, 1007, '410', '41-410', 'Capital', NULL, NULL, b'0', b'0', '2023-08-21 11:04:29', 0, '2023-08-21 11:04:29', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004015, 0, 1001003, 1004, 1004004, 1007, '620', '61-620', 'Suppliers', NULL, NULL, b'0', b'0', '2023-08-21 11:04:58', 0, '2023-08-21 11:04:58', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004016, 0, 1001003, 1004, 1004004, 1007, '679', '61-679', 'Tax Payable', NULL, NULL, b'0', b'0', '2023-08-21 11:05:25', 0, '2023-08-21 11:05:25', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004017, 0, 1001003, 1004, 1004005, 1007, '710', '71-710', 'Revenue', NULL, NULL, b'0', b'0', '2023-08-21 11:05:46', 0, '2023-08-21 11:05:46', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004018, 0, 1001003, 1004, 1004005, 1007, '740', '71-740', 'Discount', NULL, NULL, b'0', b'0', '2023-08-21 11:06:06', 0, '2023-08-21 11:06:06', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004019, 0, 1001003, 1004, 1004005, 1007, '741', '71-741', 'Bulk Discount', NULL, NULL, b'0', b'0', '2023-08-21 11:06:25', 0, '2023-08-21 11:06:25', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004020, 0, 1001003, 1004, 1004005, 1007, '742', '71-742', 'Bulk Discount', NULL, NULL, b'0', b'0', '2023-08-21 11:06:50', 0, '2023-08-21 11:06:50', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004021, 0, 1001003, 1004, 1004006, 1007, '750', '75-750', 'Material Cost', NULL, NULL, b'0', b'0', '2023-08-21 11:07:32', 0, '2023-08-21 11:07:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004022, 0, 1001003, 1004, 1004007, 1007, '840', '84-840', 'Administrative Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:08:01', 0, '2023-08-21 11:08:01', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004023, 0, 1001003, 1004, 1004007, 1007, '850', '84-850', 'Selling Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:08:22', 0, '2023-08-21 11:08:22', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004024, 0, 1001003, 1004, 1004008, 1008, '1001', '10-100-1001', 'Building - Office', NULL, NULL, b'0', b'0', '2023-08-21 11:08:59', 0, '2023-08-21 11:08:59', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004025, 0, 1001003, 1004, 1004008, 1008, '1002', '10-100-1002', 'Building - Factory', NULL, NULL, b'0', b'0', '2023-08-21 11:09:13', 0, '2023-08-21 11:09:13', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004026, 0, 1001003, 1004, 1004008, 1008, '1003', '10-100-1003', 'Plant & Machinery', NULL, NULL, b'0', b'0', '2023-08-21 11:09:28', 0, '2023-08-21 11:09:28', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004027, 0, 1001003, 1004, 1004008, 1008, '1004', '10-100-1004', 'Electrical Appliances', NULL, NULL, b'0', b'0', '2023-08-21 11:09:43', 0, '2023-08-21 11:09:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004028, 0, 1001003, 1004, 1004009, 1008, '1801', '18-180-1801', 'Stock in Trade', NULL, NULL, b'0', b'0', '2023-08-21 11:10:12', 0, '2023-08-21 11:10:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004029, 0, 1001003, 1004, 1004010, 1008, '1901', '18-190-1901', 'Raheem Enterprises', NULL, NULL, b'0', b'0', '2023-08-21 11:10:37', 0, '2023-08-21 11:10:37', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004030, 0, 1001003, 1004, 1004010, 1008, '1902', '18-190-1902', 'Nasir Sattar', NULL, NULL, b'0', b'0', '2023-08-21 11:10:54', 0, '2023-08-21 11:10:54', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004031, 0, 1001003, 1004, 1004010, 1008, '1903', '18-190-1903', 'Pervaiz Ashraf', NULL, NULL, b'0', b'0', '2023-08-21 11:11:12', 0, '2023-08-21 11:11:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004032, 0, 1001003, 1004, 1004011, 1008, '2501', '18-250-2501', 'Farooq Ahmad', NULL, NULL, b'0', b'0', '2023-08-21 11:11:33', 0, '2023-08-21 11:11:33', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004033, 0, 1001003, 1004, 1004011, 1008, '2502', '18-250-2502', 'Jawad Haider', NULL, NULL, b'0', b'0', '2023-08-21 11:11:51', 0, '2023-08-21 11:11:51', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004034, 0, 1001003, 1004, 1004011, 1008, '2503', '18-250-2503', 'Riaz', NULL, NULL, b'0', b'0', '2023-08-21 11:12:07', 0, '2023-08-21 11:12:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004035, 0, 1001003, 1004, 1004012, 1008, '3501', '18-350-3501', 'Tax deduction - Customers', NULL, NULL, b'0', b'0', '2023-08-21 11:12:27', 0, '2023-08-21 11:12:27', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004036, 0, 1001003, 1004, 1004012, 1008, '3502', '18-350-3502', 'Tax deduction - Imports', NULL, NULL, b'0', b'0', '2023-08-21 11:12:43', 0, '2023-08-21 11:12:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004037, 0, 1001003, 1004, 1004012, 1008, '3503', '18-350-3503', 'Tax deduction - Banks', NULL, NULL, b'0', b'0', '2023-08-21 11:13:00', 0, '2023-08-21 11:13:00', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004038, 0, 1001003, 1004, 1004013, 1008, '3601', '18-360-3601', 'Cash in Hand', NULL, NULL, b'0', b'0', '2023-08-21 11:13:20', 0, '2023-08-21 11:13:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004039, 0, 1001003, 1004, 1004013, 1008, '3602', '18-360-3602', 'Cheques', NULL, NULL, b'0', b'0', '2023-08-21 11:13:51', 0, '2023-08-21 11:13:51', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004040, 0, 1001003, 1004, 1004013, 1008, '3603', '18-360-3603', 'Petty Cash', NULL, NULL, b'0', b'0', '2023-08-21 11:14:16', 0, '2023-08-21 11:14:16', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004041, 0, 1001003, 1004, 1004013, 1008, '3701', '18-360-3701', 'MCB', NULL, NULL, b'0', b'0', '2023-08-21 11:14:31', 0, '2023-08-21 11:14:31', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004042, 0, 1001003, 1004, 1004013, 1008, '3702', '18-360-3702', 'HBL', NULL, NULL, b'0', b'0', '2023-08-21 11:14:52', 0, '2023-08-21 11:14:52', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004043, 0, 1001003, 1004, 1004014, 1008, '4101', '41-410-4101', 'Unpproprited Profit', NULL, NULL, b'0', b'0', '2023-08-21 11:15:13', 0, '2023-08-21 11:15:13', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004044, 0, 1001003, 1004, 1004015, 1008, '6201', '61-620-6201', 'Haji Iqbal', NULL, NULL, b'0', b'0', '2023-08-21 11:15:32', 0, '2023-08-21 11:15:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004045, 0, 1001003, 1004, 1004015, 1008, '6202', '61-620-6202', 'Saith Mumtaz', NULL, NULL, b'0', b'0', '2023-08-21 11:16:01', 0, '2023-08-21 11:16:01', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004046, 0, 1001003, 1004, 1004015, 1008, '6203', '61-620-6203', 'Friends Engineering', NULL, NULL, b'0', b'0', '2023-08-21 11:16:23', 0, '2023-08-21 11:16:23', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004047, 0, 1001003, 1004, 1004016, 1008, '6791', '61-679-6791', 'Sales tax - Payable', NULL, NULL, b'0', b'0', '2023-08-21 11:16:44', 0, '2023-08-21 11:16:44', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004048, 0, 1001003, 1004, 1004016, 1008, '6792', '61-679-6792', 'Sales tax Ret - Payable', NULL, NULL, b'0', b'0', '2023-08-21 11:17:07', 0, '2023-08-21 11:17:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004049, 0, 1001003, 1004, 1004016, 1008, '6801', '61-679-6801', 'Sales tax - Further Tax', NULL, NULL, b'0', b'0', '2023-08-21 11:17:26', 0, '2023-08-21 11:17:26', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004050, 0, 1001003, 1004, 1004016, 1008, '6901', '61-679-6901', 'Withholding - Income tax', NULL, NULL, b'0', b'0', '2023-08-21 11:17:48', 0, '2023-08-21 11:17:48', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004051, 0, 1001003, 1004, 1004017, 1008, '7101', '71-710-7101', 'Sales - Small Packing', NULL, NULL, b'0', b'0', '2023-08-21 11:18:11', 0, '2023-08-21 11:18:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004052, 0, 1001003, 1004, 1004017, 1008, '7102', '71-710-7102', 'Sales - Large Packing', NULL, NULL, b'0', b'0', '2023-08-21 11:18:32', 0, '2023-08-21 11:18:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004053, 0, 1001003, 1004, 1004017, 1008, '7106', '71-710-7106', 'Commission', NULL, NULL, b'0', b'0', '2023-08-21 11:19:12', 0, '2023-08-21 11:19:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004054, 0, 1001003, 1004, 1004017, 1008, '7201', '71-710-7201', 'Charges to customers', NULL, NULL, b'0', b'0', '2023-08-21 11:19:31', 0, '2023-08-21 11:19:31', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004055, 0, 1001003, 1004, 1004018, 1008, '7401', '71-740-7401', 'Discount on Sales', NULL, NULL, b'0', b'0', '2023-08-21 11:19:52', 0, '2023-08-21 11:19:52', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004056, 0, 1001003, 1004, 1004019, 1008, '7411', '71-741-7411', 'Bulk Discount on Sales', NULL, NULL, b'0', b'0', '2023-08-21 11:20:12', 0, '2023-08-21 11:20:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004057, 0, 1001003, 1004, 1004020, 1008, '7421', '71-742-7421', 'Bulk Discount on Sales', NULL, NULL, b'0', b'0', '2023-08-21 11:21:24', 0, '2023-08-21 11:21:24', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004058, 0, 1001003, 1004, 1004021, 1008, '7501', '75-750-7501', 'Purchases', NULL, NULL, b'0', b'0', '2023-08-21 11:21:48', 0, '2023-08-21 11:21:48', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004059, 0, 1001003, 1004, 1004021, 1008, '7701', '75-750-7701', 'Discount on Purchases', NULL, NULL, b'0', b'0', '2023-08-21 11:22:13', 0, '2023-08-21 11:22:13', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004060, 0, 1001003, 1004, 1004022, 1008, '8401', '84-840-8401', 'Printing & Stationery', NULL, NULL, b'0', b'0', '2023-08-21 11:22:43', 0, '2023-08-21 11:22:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004061, 0, 1001003, 1004, 1004022, 1008, '8402', '84-840-8402', 'Travelling & Conveyance', NULL, NULL, b'0', b'0', '2023-08-21 11:23:01', 0, '2023-08-21 11:23:01', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004062, 0, 1001003, 1004, 1004022, 1008, '8403', '84-840-8403', 'Vehicle Running & Maintenance', NULL, NULL, b'0', b'0', '2023-08-21 11:23:19', 0, '2023-08-21 11:23:19', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004063, 0, 1001003, 1004, 1004022, 1008, '8404', '84-840-8404', 'Repair & Maintenance', NULL, NULL, b'0', b'0', '2023-08-21 11:23:43', 0, '2023-08-21 11:23:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004064, 0, 1001003, 1004, 1004022, 1008, '8405', '84-840-8405', 'Bilty Exp', NULL, NULL, b'0', b'0', '2023-08-21 11:24:01', 0, '2023-08-21 11:24:01', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004065, 0, 1001003, 1004, 1004023, 1008, '8501', '84-850-8501', 'Sales Promotion Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:24:20', 0, '2023-08-21 11:24:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004066, 0, 1001003, 1004, 1004023, 1008, '8502', '84-850-8502', 'Uniform Exp', NULL, NULL, b'0', b'0', '2023-08-21 11:24:36', 0, '2023-08-21 11:24:36', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004067, 0, 1001003, 1004, 1004023, 1008, '8503', '84-850-8503', 'Packing Charges', NULL, NULL, b'0', b'0', '2023-08-21 11:24:58', 0, '2023-08-21 11:24:58', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1004068, 0, 1001003, 1004, 1004023, 1008, '8504', '84-850-8504', 'Freight Charges', NULL, NULL, b'0', b'0', '2023-08-21 11:25:24', 0, '2023-08-21 11:25:24', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1007001, 1, 1001004, 1007, 0, 0, 'A+', NULL, 'A+', 'A+', NULL, b'0', b'0', '2023-11-02 11:57:20', 0, '2023-11-02 11:57:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1007002, 1, 1001004, 1007, 0, 0, 'B+', NULL, 'B+', 'B+', NULL, b'0', b'0', '2023-11-02 11:57:20', 0, '2023-11-02 11:57:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1008001, 1, 1001004, 1008, 0, 0, 'A+', NULL, 'A+', 'A+', NULL, b'0', b'0', '2023-11-02 12:06:55', 0, '2023-11-02 12:06:55', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1008002, 1, 1001004, 1008, 0, 0, 'B+', NULL, 'B+', 'B+', NULL, b'0', b'0', '2023-11-02 12:06:55', 0, '2023-11-02 12:06:55', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1009001, 0, 0, 1009, 0, 0, 'Vendor', NULL, 'Aaftab', NULL, NULL, b'0', b'0', '2023-04-06 01:40:04', 0, '2023-04-06 01:40:04', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1009002, 0, 0, 1009, 0, 0, 'Vendor_Hasan', NULL, 'Hasan', NULL, NULL, b'0', b'0', '2023-04-06 01:40:18', 0, '2023-04-06 01:40:18', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1010001, 0, 0, 1010, 0, 0, 'Salesman_Murtaza', NULL, 'Murtaza', NULL, NULL, b'0', b'0', '2023-04-06 01:40:27', 0, '2023-04-06 01:40:27', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1010002, 0, 0, 1010, 0, 0, 'Salesman_Humain', NULL, 'Humain', NULL, NULL, b'0', b'0', '2023-04-06 01:41:10', 0, '2023-04-06 01:41:10', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1010003, 0, 0, 1010, 0, 0, 'Salesman_Ali', NULL, 'Ali', NULL, NULL, b'0', b'0', '2023-04-06 01:41:24', 0, '2023-04-06 01:41:24', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1011001, 0, 0, 1011, 0, 0, 'Godown_Godown_1', NULL, 'Godown 1', NULL, NULL, b'0', b'0', '2023-04-06 01:41:48', 0, '2023-04-06 01:41:48', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1011001, 1, 1001004, 1011, 0, 0, '0-100', NULL, '0-100', '0-100', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1011002, 0, 0, 1011, 0, 0, 'Godown_Godown_2', NULL, 'Godown 2', NULL, NULL, b'0', b'0', '2023-04-06 01:41:57', 0, '2023-04-06 01:41:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1011002, 1, 1001004, 1011, 0, 0, '101-200', NULL, '101-200', '101-200', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1011003, 1, 1001004, 1011, 0, 0, '201-300', NULL, '201-300', '201-300', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1011004, 1, 1001004, 1011, 0, 0, '301-400_Above', NULL, '301-400 Above', '301-400 Above', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1012001, 0, 0, 1012, 0, 0, 'Product_Containers', NULL, 'Containers', NULL, NULL, b'0', b'0', '2023-04-06 01:46:17', 0, '2023-04-06 01:46:17', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1012002, 0, 0, 1012, 0, 0, 'Product_Stationary_Products', NULL, 'Stationary Products', NULL, NULL, b'0', b'0', '2023-04-06 01:46:33', 0, '2023-04-06 01:46:33', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1012003, 0, 0, 1012, 0, 0, 'Product_Wood_Preservatives', NULL, 'Wood Preservatives', NULL, NULL, b'0', b'0', '2023-04-06 01:47:06', 0, '2023-04-06 01:47:06', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1012004, 0, 0, 1012, 0, 0, 'Product_Computing_Infrastructure', NULL, 'Computing Infrastructure', NULL, NULL, b'0', b'0', '2023-04-06 01:49:16', 0, '2023-04-06 01:49:16', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1013001, 0, 0, 1013, 0, 0, 'Voucher_Types_Bank_Receipt_Voucher', NULL, 'Bank Receipt Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:02', 0, '2023-04-07 19:00:02', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1013002, 0, 0, 1013, 0, 0, 'Voucher_Types_Bank_Payment_Voucher', NULL, 'Bank Payment Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:23', 0, '2023-04-07 19:00:23', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1013003, 0, 0, 1013, 0, 0, 'Voucher_Types_Cash_Receipt_Voucher', NULL, 'Cash Receipt Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:39', 0, '2023-04-07 19:00:39', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1013004, 0, 0, 1013, 0, 0, 'Voucher_Types_Cash_Payment_Voucher', NULL, 'Cash Payment Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:52', 0, '2023-04-07 19:00:52', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1013005, 0, 0, 1013, 0, 0, 'Voucher_Types__Journal_Voucher', NULL, ' Journal Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:01:06', 0, '2023-04-07 19:01:06', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1014001, 0, 0, 1014, 0, 0, 'Status_Draft', NULL, 'Draft', NULL, NULL, b'1', b'0', '2023-04-08 18:15:11', 0, '2023-04-08 18:15:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1014002, 0, 0, 1014, 0, 0, 'Status_UnPosted', NULL, 'UnPosted', NULL, NULL, b'1', b'0', '2023-04-08 18:15:27', 0, '2023-04-08 18:15:27', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1014003, 0, 0, 1014, 0, 0, 'Status_Posted', NULL, 'Posted', NULL, NULL, b'1', b'0', '2023-04-08 18:15:35', 0, '2023-04-08 18:15:35', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1015001, 0, 0, 1015, 0, 0, 'Country_America', NULL, 'America', NULL, NULL, b'1', b'0', '2023-05-03 15:43:28', 0, '2023-05-03 15:43:28', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1015001, 1, 1001004, 1015, 0, 0, 'Pakistan', NULL, 'Pakistan', 'Pakistan', NULL, b'0', b'0', '2023-10-05 12:49:59', 0, '2023-10-05 12:49:59', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1015001, 3, 1001004, 1015, 0, 0, 'Pakistan', NULL, 'Pakistan', 'Pakistan', NULL, b'0', b'0', '2023-10-05 13:15:05', 0, '2023-10-05 13:15:05', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1015002, 0, 0, 1015, 0, 0, 'Country_Canada', NULL, 'Canada', NULL, NULL, b'1', b'0', '2023-05-03 15:43:40', 0, '2023-05-03 15:43:40', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1015002, 1, 1001004, 1015, 0, 0, 'India', NULL, 'India', 'India', NULL, b'0', b'0', '2023-10-05 12:58:50', 0, '2023-10-05 12:58:50', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1015003, 0, 0, 1015, 0, 0, 'Country_India', NULL, 'India', NULL, NULL, b'1', b'0', '2023-05-03 15:43:49', 0, '2023-05-03 15:43:49', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1015004, 0, 0, 1015, 0, 0, 'Country_Pakistan', NULL, 'Pakistan', 'Pakistan', NULL, b'1', b'0', '2023-05-03 15:44:32', 0, '2023-05-03 15:44:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016001, 0, 0, 1016, 1015001, 0, 'Austin...', NULL, 'Austin...', 'Austin...', NULL, b'1', b'0', '2023-05-03 15:45:25', 0, '2023-05-03 15:45:25', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016001, 1, 1001004, 1016, 1015001, 0, 'Lahore', NULL, 'Lahore', 'Lahore', NULL, b'0', b'0', '2023-10-05 12:50:28', 0, '2023-10-05 12:50:28', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016001, 3, 1001004, 1016, 1015001, 0, 'Lahore', NULL, 'Lahore', 'Lahore', NULL, b'0', b'0', '2023-10-05 13:15:15', 0, '2023-10-05 13:15:15', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016002, 0, 0, 1016, 1015001, 0, 'City_Chichago', NULL, 'Chichago', NULL, NULL, b'1', b'0', '2023-05-03 15:46:03', 0, '2023-05-03 15:46:03', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016002, 1, 1001004, 1016, 1015002, 0, 'Mombai', NULL, 'Mombai', 'Mombai', NULL, b'0', b'0', '2023-10-05 12:59:08', 0, '2023-10-05 12:59:08', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016003, 0, 0, 1016, 1015001, 0, 'City_New_York', NULL, 'New York', NULL, NULL, b'1', b'0', '2023-05-03 15:46:20', 0, '2023-05-03 15:46:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016004, 0, 0, 1016, 1015002, 0, 'City_Ottawa', NULL, 'Ottawa', NULL, NULL, b'1', b'0', '2023-05-03 15:47:14', 0, '2023-05-03 15:47:14', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016005, 0, 0, 1016, 1015002, 0, 'City_Toronto', NULL, 'Toronto', NULL, NULL, b'1', b'0', '2023-05-03 15:47:27', 0, '2023-05-03 15:47:27', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016006, 0, 0, 1016, 1015003, 0, 'City_Mumbai', NULL, 'Mumbai', NULL, NULL, b'1', b'0', '2023-05-03 15:47:51', 0, '2023-05-03 15:47:51', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016007, 0, 0, 1016, 1015003, 0, 'City_Kolkata', NULL, 'Kolkata', NULL, NULL, b'1', b'0', '2023-05-03 15:48:10', 0, '2023-05-03 15:48:10', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016008, 0, 0, 1016, 1015003, 0, 'City_Jaipur', NULL, 'Jaipur', NULL, NULL, b'1', b'0', '2023-05-03 15:48:28', 0, '2023-05-03 15:48:28', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016009, 0, 0, 1016, 1015003, 0, 'City_Chennal', NULL, 'Chennal', NULL, NULL, b'1', b'0', '2023-05-03 15:48:46', 0, '2023-05-03 15:48:46', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016010, 0, 0, 1016, 1015003, 0, 'City_Tawang', NULL, 'Tawang', NULL, NULL, b'1', b'0', '2023-05-03 15:49:18', 0, '2023-05-03 15:49:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016011, 0, 0, 1016, 1015004, 0, 'City_Lahore', NULL, 'Lahore', NULL, NULL, b'1', b'0', '2023-05-03 15:49:32', 0, '2023-05-03 15:49:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016012, 0, 0, 1016, 1015004, 0, 'City_Karachi', NULL, 'Karachi', NULL, NULL, b'1', b'0', '2023-05-03 15:49:45', 0, '2023-05-03 15:49:45', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016013, 0, 0, 1016, 1015004, 0, 'City_Islamabad', NULL, 'Islamabad', NULL, NULL, b'1', b'0', '2023-05-03 15:50:11', 0, '2023-05-03 15:50:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016014, 0, 0, 1016, 1015004, 0, 'City_Faisalabad', NULL, 'Faisalabad', NULL, NULL, b'1', b'0', '2023-05-03 15:50:39', 0, '2023-05-03 15:50:39', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016015, 0, 0, 1016, 1015004, 0, 'City_Multan', NULL, 'Multan', NULL, NULL, b'1', b'0', '2023-05-03 15:50:50', 0, '2023-05-03 15:50:50', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016016, 0, 0, 1016, 1015004, 0, 'City_Hyderabad', NULL, 'Hyderabad', NULL, NULL, b'1', b'0', '2023-05-03 15:51:20', 0, '2023-05-03 15:51:20', 0, b'0');
INSERT INTO `ctl_enumline` VALUES (1016018, 0, 0, 1016, 0, 0, 'City_Quetta', NULL, 'Quetta', NULL, NULL, b'0', b'0', '2023-06-26 17:24:24', 0, '2023-06-26 17:24:24', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016019, 0, 0, 1016, 1015001, 0, 'Hyderabad', NULL, 'Hyderabad', 'Hyderabad', NULL, b'0', b'0', '2024-02-13 17:13:11', 0, '2024-02-13 17:13:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1016020, 0, 0, 1016, 1015001, 0, 'Hyderabad...', NULL, 'Hyderabad...', 'Hyderabad...', NULL, b'0', b'0', '2024-02-13 17:14:12', 0, '2024-02-13 17:14:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1017001, 0, 0, 1017, 0, 0, '', NULL, 'Color', NULL, NULL, b'0', b'0', '2023-07-17 19:33:17', 0, '2023-07-17 19:33:17', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1017002, 0, 0, 1017, 0, 0, '', NULL, 'Brand', NULL, NULL, b'0', b'0', '2023-07-17 19:33:21', 0, '2023-07-17 19:33:21', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1017003, 0, 0, 1017, 0, 0, '', NULL, 'Size', NULL, NULL, b'0', b'0', '2023-07-17 19:33:26', 0, '2023-07-17 19:33:26', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1017004, 0, 0, 1017, 0, 0, '', NULL, 'Legs', NULL, NULL, b'0', b'0', '2023-07-17 19:33:30', 0, '2023-07-17 19:33:30', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1017005, 0, 0, 1017, 0, 0, '', NULL, 'Appearance', NULL, NULL, b'0', b'0', '2023-08-21 11:49:38', 0, '2023-08-21 11:49:38', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018001, 0, 0, 1018, 1017001, 0, 'Black', NULL, 'Black', NULL, NULL, b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018002, 0, 0, 1018, 1017001, 0, 'Brown', NULL, 'Brown', NULL, NULL, b'0', b'0', '2023-07-17 19:49:18', 0, '2023-07-17 19:49:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018003, 0, 0, 1018, 1017001, 0, 'Blue', NULL, 'Blue', NULL, NULL, b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018004, 0, 0, 1018, 1017001, 0, 'Red', NULL, 'Red', NULL, NULL, b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018005, 0, 0, 1018, 1017001, 0, 'White', NULL, 'White', NULL, 'des', b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018006, 0, 0, 1018, 1017003, 0, 'L', NULL, 'Large', NULL, NULL, b'0', b'0', '2023-07-17 20:23:23', 0, '2023-07-17 20:23:23', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018007, 0, 0, 1018, 1017003, 0, 'M', NULL, 'Medium', NULL, NULL, b'0', b'0', '2023-07-17 20:23:31', 0, '2023-07-17 20:23:31', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018008, 0, 0, 1018, 1017003, 0, 'S', NULL, 'Small', NULL, NULL, b'0', b'0', '2023-07-17 20:23:40', 0, '2023-07-17 20:23:40', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018009, 0, 0, 1018, 1017004, 0, 'Steel', NULL, 'Steel', NULL, NULL, b'0', b'0', '2023-07-18 01:16:46', 0, '2023-07-18 01:16:46', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018010, 0, 0, 1018, 1017004, 0, 'Platinum', NULL, 'Platinum', NULL, NULL, b'0', b'0', '2023-07-18 01:16:57', 0, '2023-07-18 01:16:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018011, 0, 0, 1018, 1017002, 0, 'Hoid', NULL, 'Hoid', NULL, NULL, b'0', b'0', '2023-07-21 03:06:06', 0, '2023-07-21 03:06:06', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018012, 0, 0, 1018, 1017002, 0, 'Furniture Hub', NULL, 'Furniture Hub', NULL, NULL, b'0', b'0', '2023-07-21 03:06:19', 0, '2023-07-21 03:06:19', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018013, 0, 0, 1018, 1017002, 0, 'Index Furniture', NULL, 'Index Furniture', NULL, NULL, b'0', b'0', '2023-07-21 03:06:32', 0, '2023-07-21 03:06:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018014, 0, 0, 1018, 1017005, 0, 'Granulated_Sugar', NULL, 'Granulated Sugar', NULL, NULL, b'0', b'0', '2023-08-21 11:50:09', 0, '2023-08-21 11:50:09', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1018015, 0, 0, 1018, 1017005, 0, 'Powdered_Sugar', NULL, 'Powdered Sugar', NULL, NULL, b'0', b'0', '2023-08-21 11:50:22', 0, '2023-08-21 11:50:22', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1019001, 0, 0, 1019, 0, 0, '', NULL, 'Kg', NULL, NULL, b'0', b'0', '2023-08-21 09:42:36', 0, '2023-08-21 09:42:36', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1019002, 0, 0, 1019, 0, 0, '', NULL, 'Box', NULL, NULL, b'0', b'0', '2023-08-21 09:42:42', 0, '2023-08-21 09:42:42', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1019003, 0, 0, 1019, 0, 0, '', NULL, 'Carton', NULL, NULL, b'0', b'0', '2023-08-21 09:42:48', 0, '2023-08-21 09:42:48', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1019004, 0, 0, 1019, 0, 0, '', NULL, 'Bag', NULL, NULL, b'0', b'0', '2023-08-21 09:43:03', 0, '2023-08-21 09:43:03', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1019005, 0, 0, 1019, 0, 0, '', NULL, 'Packet', NULL, NULL, b'0', b'0', '2023-08-21 09:43:10', 0, '2023-08-21 09:43:10', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1020001, 0, 0, 1020, 0, 0, 'UOMTypes_Sale_UOM', NULL, 'Sale UOM', 'Sale UOM', NULL, b'1', b'0', '2023-08-21 09:46:48', 0, '2023-08-21 09:46:48', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1020002, 0, 0, 1020, 0, 0, 'UOMTypes_Purchase_UOM', NULL, 'Purchase UOM', 'Purchase UOM', NULL, b'1', b'0', '2023-08-21 09:47:09', 0, '2023-08-21 09:47:09', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1020003, 0, 0, 1020, 0, 0, 'UOMTypes_Inventory_UOM', NULL, 'Inventory UOM', 'Inventory UOM', NULL, b'1', b'0', '2023-08-21 09:47:31', 0, '2023-08-21 09:47:31', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1022001, 0, 0, 1022, 0, 0, 'Tax_GST', NULL, 'GST', 'GST', NULL, b'1', b'0', '2023-08-21 09:59:07', 0, '2023-08-21 09:59:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1022002, 0, 0, 1022, 0, 0, 'Tax_General_Sale_Tax', NULL, 'General Sale Tax', 'General Sale Tax', NULL, b'0', b'0', '2023-08-21 09:59:43', 0, '2023-08-21 09:59:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1023001, 0, 0, 1023, 0, 0, 'Freight_By_Road_Freight', NULL, 'By Road Freight', 'By Road Freight', NULL, b'1', b'0', '2023-08-21 10:01:42', 0, '2023-08-21 10:01:42', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1023002, 0, 0, 1023, 0, 0, 'Freight_Ocean_Freight', NULL, 'Ocean Freight', 'Ocean Freight', NULL, b'0', b'0', '2023-08-21 10:02:04', 0, '2023-08-21 10:02:04', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1023003, 0, 0, 1023, 0, 0, 'Freight_By_Rail_Freight', NULL, 'By Rail Freight', 'By Rail Freight', NULL, b'1', b'0', '2023-08-21 10:02:28', 0, '2023-08-21 10:02:28', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1023004, 0, 0, 1023, 0, 0, 'Freight_By_Air_Freight', NULL, 'By Air Freight', 'By Air Freight', NULL, b'1', b'0', '2023-08-21 10:02:46', 0, '2023-08-21 10:02:46', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1024001, 0, 0, 1024, 0, 0, 'Discount_Invoice_Discount', NULL, 'Invoice Discount', 'Invoice Discount', NULL, b'1', b'0', '2023-08-21 10:00:16', 0, '2023-08-21 10:00:16', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1024002, 0, 0, 1024, 0, 0, 'Discount_Line_Discount', NULL, 'Line Discount', 'Line Discount', NULL, b'1', b'0', '2023-08-21 10:00:34', 0, '2023-08-21 10:00:34', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1025001, 0, 0, 1025, 0, 0, 'DocExtrasIncDecTypes_Increament', NULL, 'Increament', 'Increament', NULL, b'1', b'0', '2023-08-21 10:04:07', 0, '2023-08-21 10:04:07', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1025002, 0, 0, 1025, 0, 0, 'DocExtrasIncDecTypes_Decreament', NULL, 'Decreament', 'Decreament', NULL, b'1', b'0', '2023-08-21 10:04:23', 0, '2023-08-21 10:04:23', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1026001, 0, 0, 1026, 0, 0, 'DocExtraFormulas_Formula_1', NULL, 'Formula 1', 'Formula 1', NULL, b'1', b'0', '2023-08-21 10:04:39', 0, '2023-08-21 10:04:39', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1026002, 0, 0, 1026, 0, 0, 'DocExtraFormulas_Formula_2', NULL, 'Formula 2', 'Formula 2', NULL, b'1', b'0', '2023-08-21 10:04:50', 0, '2023-08-21 10:04:50', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1028001, 0, 0, 1028, 0, 0, 'Product_Taxes_GST_Sale_Rate', NULL, 'GST Sale Rate', 'GST Sale Rate', NULL, b'1', b'0', '2023-08-21 10:10:06', 0, '2023-08-21 10:10:06', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1028002, 0, 0, 1028, 0, 0, 'Product_Taxes_GST_Purchase_Rate', NULL, 'GST Purchase Rate', 'GST Purchase Rate', NULL, b'1', b'0', '2023-08-21 10:10:27', 0, '2023-08-21 10:10:27', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1028003, 0, 0, 1028, 0, 0, 'Product_Taxes_Wht_Rate', NULL, 'Wht Rate', 'Wht Rate', NULL, b'1', b'0', '2023-08-21 10:10:47', 0, '2023-08-21 10:10:47', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1028004, 0, 0, 1028, 0, 0, 'Product_Taxes_F.Tax_Rate', NULL, 'F.Tax Rate', 'F.Tax Rate', NULL, b'1', b'0', '2023-08-21 10:11:19', 0, '2023-08-21 10:11:19', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1028005, 0, 0, 1028, 0, 0, 'Product_Taxes_Gst_Retail_Rate', NULL, 'Gst Retail Rate', 'Gst Retail Rate', NULL, b'1', b'0', '2023-08-21 10:11:43', 0, '2023-08-21 10:11:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1029001, 0, 0, 1029, 0, 0, 'Item_Types_Small_Packing', NULL, 'Small Packing', 'Small Packing', NULL, b'1', b'0', '2023-08-21 10:19:01', 0, '2023-08-21 10:19:01', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1029002, 0, 0, 1029, 0, 0, 'Item_Types_Large_Paking', NULL, 'Large Packing', 'Large Packing', NULL, b'1', b'0', '2023-08-21 10:19:17', 0, '2023-08-21 10:19:17', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1030001, 0, 0, 1030, 0, 0, 'Documents_Sale_Order', NULL, 'Sale Order', 'Sale Order', NULL, b'1', b'0', '2023-08-21 10:22:30', 0, '2023-08-21 10:22:30', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1030002, 0, 0, 1030, 0, 0, 'Documents_Sale_Invoice', NULL, 'Sale Invoice', 'Sale Invoice', NULL, b'1', b'0', '2023-08-21 10:22:46', 0, '2023-08-21 10:22:46', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1030003, 0, 0, 1030, 0, 0, 'Documents_Purchase_Order', NULL, 'Purchase Order', 'Purchase Order', NULL, b'1', b'0', '2023-08-21 10:23:06', 0, '2023-08-21 10:23:06', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1030004, 0, 0, 1030, 0, 0, 'Documents_Purchase_Invoice', NULL, 'Purchase Invoice', 'Purchase Invoice', NULL, b'1', b'0', '2023-08-21 10:23:22', 0, '2023-08-21 10:23:22', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031001, 0, 0, 1031, 1030001, 0, 'Accounts_Gross_Sale', NULL, 'Gross Sale', 'Gross Sale', '1034001', b'1', b'0', '2023-08-21 10:25:26', 0, '2023-08-21 10:25:26', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031002, 0, 0, 1031, 1030001, 0, 'Accounts_Sale_Discount', NULL, 'Sale Discount', '1004055', '1034002', b'1', b'0', '2023-08-21 10:30:26', 0, '2023-08-21 10:30:26', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031003, 0, 0, 1031, 1030001, 0, 'Accounts_Bulk_Discount', NULL, 'Bulk Discount', '1004056', '1034002', b'1', b'0', '2023-08-21 10:30:56', 0, '2023-08-21 10:30:56', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031004, 0, 0, 1031, 1030001, 0, 'Accounts_Qty_Discount', NULL, 'Qty Discount', '1004057', '1034002', b'1', b'0', '2023-08-21 10:31:19', 0, '2023-08-21 10:31:19', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031005, 0, 0, 1031, 1030001, 0, 'Accounts_Sale_GST', NULL, 'Sale GST', '1004047', '1034001', b'1', b'0', '2023-08-21 10:31:44', 0, '2023-08-21 10:31:44', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031006, 0, 0, 1031, 1030001, 0, 'Accounts_Sale_GST_Ret', NULL, 'Sale GST Ret', '1004048', '1034001', b'1', b'0', '2023-08-21 10:32:09', 0, '2023-08-21 10:32:09', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031007, 0, 0, 1031, 1030001, 0, 'Accounts_Further_TAX', NULL, 'Further TAX', '1004049', '1034001', b'1', b'0', '2023-08-21 10:33:08', 0, '2023-08-21 10:33:08', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031008, 0, 0, 1031, 1030001, 0, 'Accounts_Withholding', NULL, 'Withholding', '1004049', '1043001', b'1', b'0', '2023-08-21 10:33:59', 0, '2023-08-21 10:33:59', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031009, 0, 0, 1031, 1030001, 0, 'Accounts_Charges_Add', NULL, 'Charges Add', '1004054', '1034001', b'1', b'0', '2023-08-21 10:34:42', 0, '2023-08-21 10:34:42', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031010, 0, 0, 1031, 1030001, 0, 'Accounts_Charges_Less', NULL, 'Charges Less', '1004054', '1034002', b'1', b'0', '2023-08-21 10:35:25', 0, '2023-08-21 10:35:25', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031011, 0, 0, 1031, 1030001, 0, 'Accounts_Packing_Charges', NULL, 'Packing Charges', '1004067', '1034001', b'1', b'0', '2023-08-21 10:35:54', 0, '2023-08-21 10:35:54', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031012, 0, 0, 1031, 1030001, 0, 'Accounts_Freight_Charges', NULL, 'Freight Charges', '1004068', '1034002', b'1', b'0', '2023-08-21 10:36:21', 0, '2023-08-21 10:36:21', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1031013, 0, 0, 1031, 1030001, 0, 'Accounts_Invoice_Discount', NULL, 'Invoice Discount', '1004055', '1034002', b'1', b'0', '2023-08-21 10:36:56', 0, '2023-08-21 10:36:56', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1032001, 0, 0, 1032, 1031001, 0, 'Large_Packing_Gross_Sale_(Large_Packing)', NULL, 'Gross Sale (Large Packing)', '1004052', NULL, b'1', b'0', '2023-08-21 10:26:14', 0, '2023-08-21 10:26:14', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1033001, 0, 0, 1033, 1031001, 0, 'Small_Packing_Gross_Sale_(Small_Packing)', NULL, 'Gross Sale (Small Packing)', '1004051', NULL, b'1', b'0', '2023-08-21 10:27:01', 0, '2023-08-21 10:27:01', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1034001, 0, 0, 1034, 0, 0, 'Transaction_Types_Credit', NULL, 'Credit', 'Credit', NULL, b'1', b'0', '2023-08-21 10:27:38', 0, '2023-08-21 10:27:38', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1034002, 0, 0, 1034, 0, 0, 'Transaction_Types_Debit', NULL, 'Debit', 'Debit', NULL, b'1', b'0', '2023-08-21 10:27:48', 0, '2023-08-21 10:27:48', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1035001, 0, 0, 1035, 0, 0, 'Gender_Male', NULL, 'Male', 'Male', NULL, b'1', b'0', '2023-08-30 09:29:13', 0, '2023-08-30 09:29:13', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1035002, 0, 0, 1035, 0, 0, 'Gender_Female', NULL, 'Female', 'Female', NULL, b'1', b'0', '2023-08-30 09:29:23', 0, '2023-08-30 09:29:23', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1035003, 0, 0, 1035, 0, 0, 'Gender_Other', NULL, 'Other', 'Other', NULL, b'1', b'0', '2023-08-30 09:29:32', 0, '2023-08-30 09:29:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036001, 0, 0, 1036, 1016011, 0, 'Areas_Defense', NULL, 'Defense', 'Defense', NULL, b'0', b'0', '2023-08-30 12:14:29', 0, '2023-08-30 12:14:29', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036001, 1, 1001004, 1036, 1016001, 0, 'Iqbal_Town', NULL, 'Iqbal Town', 'Iqbal Town', NULL, b'0', b'0', '2023-10-05 12:52:13', 0, '2023-10-05 12:52:13', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036001, 3, 1001004, 1036, 1016001, 0, 'Shalimar_Town', NULL, 'Shalimar Town', 'Shalimar Town', NULL, b'0', b'0', '2023-10-05 13:15:43', 0, '2023-10-05 13:15:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036002, 0, 0, 1036, 1016012, 0, 'Areas_Karachi_Central', NULL, 'Karachi Central', 'Karachi Central', NULL, b'1', b'0', '2023-08-30 12:15:16', 0, '2023-08-30 12:15:16', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036002, 1, 1001004, 1036, 1016002, 0, 'Eksar_Colony', NULL, 'Eksar Colony', 'Eksar Colony', NULL, b'0', b'0', '2023-10-05 12:59:19', 0, '2023-10-05 12:59:19', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036003, 0, 0, 1036, 1016011, 0, 'Areas_Johar_Town', NULL, 'Johar Town', 'Johar Town', NULL, b'1', b'0', '2023-09-01 18:42:41', 0, '2023-09-01 18:42:41', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036004, 0, 0, 1036, 1016011, 0, 'Areas_Aziz_Abad', NULL, 'Aziz Abad', 'Aziz Abad', NULL, b'1', b'0', '2023-09-03 15:24:35', 0, '2023-09-03 15:24:35', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036005, 0, 0, 1036, 1016011, 0, 'Areas_Qaisar', NULL, 'Qaisar', 'Qaisar', NULL, b'1', b'0', '2023-09-03 15:24:54', 0, '2023-09-03 15:24:54', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1036006, 0, 0, 1036, 1016011, 0, 'Areas_Dhair', NULL, 'Dhair', 'Dhair', NULL, b'1', b'0', '2023-09-03 15:25:20', 0, '2023-09-03 15:25:20', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1037001, 0, 0, 1037, 0, 0, 'Field_Types_Text_Box', NULL, 'Text Box', 'Text Box', NULL, b'1', b'0', '2023-08-30 19:20:18', 0, '2023-08-30 19:20:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1037002, 0, 0, 1037, 0, 0, 'Field_Types_Text_Area', NULL, 'Text Area', 'Text Area', NULL, b'1', b'0', '2023-08-30 19:20:28', 0, '2023-08-30 19:20:28', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1037003, 0, 0, 1037, 0, 0, 'Field_Types_Drop_Down', NULL, 'Drop Down', 'Drop Down', NULL, b'1', b'0', '2023-08-30 19:20:39', 0, '2023-08-30 19:20:39', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1037004, 0, 0, 1037, 0, 0, 'Field_Types_Radio_Button', NULL, 'Radio Button', 'Radio Button', NULL, b'1', b'0', '2023-08-30 19:20:50', 0, '2023-08-30 19:20:50', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1037005, 0, 0, 1037, 0, 0, 'Field_Types_Check_Box', NULL, 'Check Box', 'Check Box', NULL, b'1', b'0', '2023-08-30 19:21:08', 0, '2023-08-30 19:21:08', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1047001, 1, 1001004, 1047, 0, 0, 'Before.....', NULL, 'Before.....', 'Before.....', NULL, b'0', b'0', '2023-09-27 10:12:18', 0, '2023-09-27 10:12:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1047001, 3, 1001004, 1047, 0, 0, '_Before.', NULL, 'Before', 'Before.', NULL, b'0', b'0', '2023-09-27 10:12:18', 0, '2023-09-27 10:12:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1047002, 1, 1001004, 1047, 0, 0, 'After', NULL, 'After', 'After', NULL, b'0', b'0', '2023-11-09 15:42:58', 0, '2023-11-09 15:42:58', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1047002, 3, 1001004, 1047, 0, 0, '', NULL, 'After', NULL, NULL, b'0', b'0', '2023-09-27 09:55:57', 0, '2023-09-27 09:55:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1048001, 1, 1001004, 1048, 0, 0, '', NULL, 'میٹھی چیزوں سے پرہیز کریں', NULL, NULL, b'0', b'0', '2023-09-27 10:22:00', 0, '2023-09-27 10:22:00', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1048001, 3, 1001004, 1048, 0, 0, '', NULL, 'باقاعدگی سے ورزش کریں', NULL, NULL, b'0', b'0', '2023-09-27 10:21:18', 0, '2023-09-27 10:21:18', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1048002, 1, 1001004, 1048, 0, 0, 'باقاعدگی_سے_ورزش_کریں', NULL, 'باقاعدگی سے ورزش کریں', 'باقاعدگی سے ورزش کریں', NULL, b'0', b'0', '2023-10-31 01:22:03', 0, '2023-10-31 01:22:03', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1048003, 1, 1001004, 1048, 0, 0, 'پھل_اور_سبزیاں_کھائیں', NULL, 'پھل اور سبزیاں کھائیں', 'پھل اور سبزیاں کھائیں', NULL, b'0', b'0', '2023-10-31 01:22:16', 0, '2023-10-31 01:22:16', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1049001, 1, 1001004, 1049, 0, 0, 'RCB', NULL, 'RCB', 'RCB', NULL, b'0', b'0', '2023-10-11 23:27:38', 0, '2023-10-11 23:27:38', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1049001, 3, 1001004, 1049, 0, 0, '', NULL, 'RCB', NULL, NULL, b'0', b'0', '2023-09-27 10:31:57', 0, '2023-09-27 10:31:57', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1049002, 1, 1001004, 1049, 0, 0, 'CBC', NULL, 'CBC', 'CBC', NULL, b'0', b'0', '2023-11-02 12:12:11', 0, '2023-11-02 12:12:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1049002, 3, 1001004, 1049, 0, 0, 'XRAY', NULL, 'XRAY', 'XRAY', NULL, b'0', b'0', '2023-10-02 22:46:19', 0, '2023-10-02 22:46:19', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1050001, 1, 1001004, 1050, 0, 0, 'remarks', NULL, 'remarks', 'remarks', NULL, b'0', b'0', '2023-10-02 12:37:24', 0, '2023-10-02 12:37:24', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1050001, 3, 1001004, 1050, 0, 0, 'Remarks.....', NULL, 'Remarks.....', 'Remarks.....', NULL, b'0', b'0', '2023-10-02 22:53:37', 0, '2023-10-02 22:53:37', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1050002, 1, 1001004, 1050, 0, 0, 'Lorem_ipsum_dolor_sit_amet,_consectetur_adipiscing_elit,_sed_do_eiusst_laborum.', NULL, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusst laborum.', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusst laborum.', NULL, b'0', b'0', '2023-11-27 20:07:09', 0, '2023-11-27 20:07:09', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1051001, 0, 0, 1051, 0, 0, '', NULL, 'Clinic', NULL, NULL, b'0', b'0', '2023-09-27 11:07:23', 0, '2023-09-27 11:07:23', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1051002, 0, 0, 1051, 0, 0, 'Software_Development', NULL, 'Software Development', 'Software Development', NULL, b'0', b'0', '2023-09-27 11:07:34', 0, '2023-09-27 11:07:34', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1051003, 0, 0, 1051, 0, 0, 'Business', NULL, 'Business', 'Business', NULL, b'0', b'0', '2023-12-02 15:16:32', 0, '2023-12-02 15:16:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1052001, 1, 1001004, 1052, 0, 0, 'Painless', NULL, 'Painless', 'Painless', NULL, b'0', b'0', '2023-10-02 22:38:43', 0, '2023-10-02 22:38:43', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1052001, 3, 1001004, 1052, 0, 0, 'Antibiotic', NULL, 'Antibiotic', 'Antibiotic', NULL, b'0', b'0', '2023-10-02 22:03:29', 0, '2023-10-02 22:03:29', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1052002, 1, 1001004, 1052, 0, 0, 'Sulpha', NULL, 'Sulpha', 'Sulpha', NULL, b'0', b'0', '2023-11-09 15:41:49', 0, '2023-11-09 15:41:49', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1052002, 3, 1001004, 1052, 0, 0, 'Sulpha', NULL, 'Sulpha', 'Sulpha', NULL, b'0', b'0', '2023-10-02 22:24:11', 0, '2023-10-02 22:24:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1053001, 1, 1001004, 1053, 0, 0, 'Venus', NULL, 'Venus', 'Venus', NULL, b'0', b'0', '2023-10-02 22:38:23', 0, '2023-10-02 22:38:23', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1053001, 3, 1001004, 1053, 0, 0, 'Glitz', NULL, 'Glitz', 'Glitz', NULL, b'0', b'0', '2023-10-02 22:03:14', 0, '2023-10-02 22:03:14', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1053002, 1, 1001004, 1053, 0, 0, 'Glaxo', NULL, 'Glaxo', 'Glaxo', NULL, b'0', b'0', '2023-11-09 15:41:32', 0, '2023-11-09 15:41:32', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1053002, 3, 1001004, 1053, 0, 0, 'Glaxo', NULL, 'Glaxo', 'Glaxo', NULL, b'0', b'0', '2023-10-02 22:23:45', 0, '2023-10-02 22:23:45', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1054001, 0, 0, 1054, 0, 0, 'Roles_Super_Admin', NULL, 'Super Admin', 'Super Admin', NULL, b'1', b'0', '2023-09-27 11:27:12', 0, '2023-09-27 11:27:12', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1054002, 0, 0, 1054, 0, 0, 'Client_Admin', NULL, 'Client Admin', 'Client Admin', NULL, b'1', b'0', '2023-09-27 11:27:29', 0, '2023-09-27 11:27:29', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1054003, 0, 1001004, 1054, 0, 0, 'Doctor', NULL, 'Doctor', 'Doctor', NULL, b'1', b'0', '2023-10-09 12:36:58', 0, '2023-10-09 12:36:58', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1054004, 0, 1001004, 1054, 0, 0, 'Staff', NULL, 'Staff', 'Staff', NULL, b'1', b'0', '2023-10-09 12:43:11', 0, '2023-10-09 12:43:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1055001, 0, 0, 1055, 0, 0, 'AppStatus_Waiting', NULL, 'Waiting', 'Waiting', NULL, b'1', b'0', '2023-10-17 11:43:31', 0, '2023-10-17 11:43:31', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1055002, 0, 0, 1055, 0, 0, 'AppStatus', NULL, 'Due', 'Due', NULL, b'1', b'0', '2023-10-17 11:44:11', 0, '2023-10-17 11:44:11', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1055003, 0, 0, 1055, 0, 0, 'AppStatus_Cancled', NULL, 'Canceled', 'Canceled', NULL, b'1', b'0', '2023-10-17 11:44:22', 0, '2023-10-17 11:44:22', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1055004, 0, 0, 1055, 0, 0, 'AppStatus_Closed', NULL, 'Closed', 'Closed', NULL, b'1', b'0', '2023-10-17 11:44:31', 0, '2023-10-17 11:44:31', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1056001, 0, 0, 1056, 0, 0, 'Permissions_Deny', NULL, 'Deny', 'Deny', NULL, b'1', b'0', '2023-10-24 09:47:36', 0, '2023-10-24 09:47:36', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1056002, 0, 0, 1056, 0, 0, 'Permissions_View_Only', NULL, 'View Only', 'View Only', NULL, b'1', b'0', '2023-10-24 09:47:51', 0, '2023-10-24 09:47:51', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1056003, 0, 0, 1056, 0, 0, 'Permissions_Full_Access', NULL, 'Full Access', 'Full Access', NULL, b'1', b'0', '2023-10-24 09:48:05', 0, '2023-10-24 09:48:05', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1057001, 0, 0, 1057, 0, 0, 'BP_Statuses', NULL, 'Low', 'Low', NULL, b'0', b'0', '2023-12-04 19:31:54', 0, '2023-12-04 19:31:54', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1057002, 0, 0, 1057, 0, 0, 'BP_Statuses_High', NULL, 'High', 'High', NULL, b'0', b'0', '2023-12-04 19:32:02', 0, '2023-12-04 19:32:02', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1058001, 0, 0, 1058, 0, 0, 'Input_Types_number', NULL, 'number', 'number', NULL, b'0', b'0', '2023-12-05 16:57:03', 0, '2023-12-05 16:57:03', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1058002, 0, 0, 1058, 0, 0, 'Input_Types_text', NULL, 'text', 'text', NULL, b'0', b'0', '2023-12-05 16:57:14', 0, '2023-12-05 16:57:14', 0, b'1');
INSERT INTO `ctl_enumline` VALUES (1058003, 0, 0, 1058, 0, 0, 'Input_Types_email', NULL, 'email', 'email', NULL, b'0', b'0', '2023-12-05 17:31:11', 0, '2023-12-05 17:31:11', 0, b'0');

-- ----------------------------
-- Table structure for ctl_enums
-- ----------------------------
DROP TABLE IF EXISTS `ctl_enums`;
CREATE TABLE `ctl_enums`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `ModuleId` int NULL DEFAULT NULL,
  `ParentId` int NULL DEFAULT NULL,
  `KeyCode` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` varchar(3000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsSystemDefined` bit(1) NULL DEFAULT NULL,
  `IstAccountLevel` bit(1) NULL DEFAULT NULL,
  `IsRequired` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_enums
-- ----------------------------
INSERT INTO `ctl_enums` VALUES (1001, 0, 0, 0, 'Modules', 'Modules', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1001, 1, 1001004, 1045, 'Radio Button', 'Smoking Status', NULL, b'0', b'0', b'1', '2023-09-27 02:08:18', 0, '2023-09-27 02:08:18', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1001, 3, 1001004, 1045, 'Text Box', 'Email', NULL, b'0', b'0', b'0', '2023-09-27 02:27:45', 0, '2023-09-27 02:27:45', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1002, 0, 0, 1001, 'Menu', 'Menu', NULL, b'1', b'0', b'0', '2023-03-31 19:59:43', 0, '2023-03-31 19:59:43', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1002, 1, 1001004, 1038, 'Text Area', 'Medical History..', NULL, b'0', b'0', b'0', '2023-09-27 02:36:53', 0, '2023-09-27 02:36:53', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1002, 3, 1001004, 1038, 'Text Box', 'Email', NULL, b'0', b'0', b'0', '2023-09-27 02:35:38', 0, '2023-09-27 02:35:38', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1003, 0, 0, 1002, 'Sub_Menu', 'Sub Menu', NULL, b'1', b'0', b'0', '2023-04-03 20:38:32', 0, '2023-04-03 20:38:32', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1003, 3, 1001004, 1038, 'Drop Down', 'Blood Group', NULL, b'0', b'0', b'0', '2023-09-27 02:36:05', 0, '2023-09-27 02:36:05', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1004, 0, 1001003, 0, 'Chart_of_Account', 'Chart of Account', NULL, b'1', b'0', b'0', '2023-04-03 21:24:13', 0, '2023-04-03 21:24:13', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1004, 1, 1001004, 1045, 'Radio Button', 'Hepatitis', NULL, b'0', b'0', b'0', '2023-10-06 09:00:44', 0, '2023-10-06 09:00:44', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1005, 0, 1001003, 0, 'Levels', 'Levels', NULL, b'1', b'0', b'0', '2023-04-03 21:24:20', 0, '2023-04-03 21:24:20', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1005, 1, 1001004, 1038, 'Text Box', 'Email', 'text', b'0', b'0', b'1', '2023-10-06 09:10:14', 0, '2023-10-06 09:10:14', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1006, 0, 1001003, 1005, 'Main', 'Main', NULL, b'1', b'1', b'0', '2023-04-03 22:52:08', 0, '2023-04-03 22:52:08', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1006, 1, 1001004, 1045, 'Text Area', 'Details ', NULL, b'0', b'0', b'0', '2023-10-31 01:38:43', 0, '2023-10-31 01:38:43', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1007, 0, 1001003, 1006, 'Group', 'Group', NULL, b'1', b'1', b'0', '2023-04-03 22:52:17', 0, '2023-04-03 22:52:17', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1007, 1, 1001004, 1038, 'Drop Down', 'Blood Group', NULL, b'0', b'0', b'0', '2023-11-02 11:57:20', 0, '2023-11-02 11:57:20', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1008, 0, 1001003, 1007, 'Detail', 'Detail', NULL, b'1', b'1', b'0', '2023-04-03 22:52:28', 0, '2023-04-03 22:52:28', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1008, 1, 1001004, 1045, 'Drop Down', 'Blood Group', NULL, b'0', b'0', b'0', '2023-11-02 12:06:55', 0, '2023-11-02 12:06:55', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1009, 0, 0, 0, 'Vendor', 'Vendor', NULL, b'1', b'0', b'0', '2023-04-06 01:38:02', 0, '2023-04-06 01:38:02', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1009, 1, 1001004, 1045, 'Check Box', 'Is Suger Patient', NULL, b'0', b'0', b'0', '2023-11-02 12:07:14', 0, '2023-11-02 12:07:14', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1010, 0, 1001003, 0, 'Salesman', 'Salesman', NULL, b'1', b'0', b'0', '2023-04-06 01:38:18', 0, '2023-04-06 01:38:18', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1010, 1, 1001004, 1045, 'Text Box', 'ECG', 'text', b'0', b'0', b'1', '2023-11-02 12:08:20', 0, '2023-11-02 12:08:20', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1011, 0, 1001003, 0, 'Godown', 'Godown', NULL, b'1', b'0', b'0', '2023-04-06 01:38:30', 0, '2023-04-06 01:38:30', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1011, 1, 1001004, 1045, 'Drop Down', 'Platelets Count', NULL, b'0', b'0', b'1', '2023-12-02 15:55:45', 0, '2023-12-02 15:55:45', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1012, 0, 0, 0, 'Product', 'Product', NULL, b'1', b'0', b'0', '2023-04-06 01:39:29', 0, '2023-04-06 01:39:29', 0, b'0');
INSERT INTO `ctl_enums` VALUES (1013, 0, 1001003, 0, 'Voucher_Types', 'Voucher Types', NULL, b'1', b'0', b'0', '2023-04-07 18:59:39', 0, '2023-04-07 18:59:39', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1014, 0, 1001003, 0, 'Status', 'Status', NULL, b'1', b'0', b'0', '2023-04-08 18:15:00', 0, '2023-04-08 18:15:00', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1015, 0, 0, 0, 'Country', 'Country', NULL, b'1', b'0', b'0', '2023-05-03 15:06:56', 0, '2023-05-03 15:06:56', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1016, 0, 0, 1015, 'City', 'City', NULL, b'1', b'0', b'0', '2023-05-03 15:08:16', 0, '2023-05-03 15:08:16', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1017, 0, 1001003, 0, 'Attributes', 'Attributes', NULL, b'1', b'0', b'0', '2023-07-17 19:15:32', 0, '2023-07-17 19:15:32', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1018, 0, 1001003, 1017, 'AttributeValues', 'AttributeValues', NULL, b'1', b'0', b'0', '2023-07-17 19:15:43', 0, '2023-07-17 19:15:43', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1019, 0, 1001003, 0, 'UOM', 'UOM', NULL, b'1', b'0', b'0', '2023-07-25 11:27:47', 0, '2023-07-25 11:27:47', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1020, 0, 1001003, 0, 'UOMTypes', 'UOMTypes', NULL, b'1', b'0', b'0', '2023-07-26 00:10:30', 0, '2023-07-26 00:10:30', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1021, 0, 1001003, 0, 'Document_Extra_Types', 'Document Extra Types', NULL, b'1', b'0', b'0', '2023-08-01 11:42:01', 0, '2023-08-01 11:42:01', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1022, 0, 1001003, 1021, 'Tax', 'Tax', NULL, b'1', b'0', b'0', '2023-08-01 11:42:22', 0, '2023-08-01 11:42:22', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1023, 0, 1001003, 1021, 'Freight', 'Freight', NULL, b'1', b'0', b'0', '2023-08-01 11:42:55', 0, '2023-08-01 11:42:55', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1024, 0, 1001003, 1021, 'Discount', 'Discount', NULL, b'1', b'0', b'0', '2023-08-01 11:43:08', 0, '2023-08-01 11:43:08', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1025, 0, 1001003, 0, 'DocExtrasIncDecTypes', 'DocExtrasIncDecTypes', NULL, b'1', b'0', b'0', '2023-08-01 12:22:58', 0, '2023-08-01 12:22:58', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1026, 0, 1001003, 0, 'DocExtraFormulas', 'DocExtraFormulas', NULL, b'1', b'0', b'0', '2023-08-01 12:23:18', 0, '2023-08-01 12:23:18', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1027, 0, 1001003, 0, 'UOM_Types', 'UOM Types', NULL, b'1', b'0', b'0', '2023-08-01 12:49:27', 0, '2023-08-01 12:49:27', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1028, 0, 1001003, 0, 'Product_Taxes', 'Product Taxes', NULL, b'1', b'0', b'0', '2023-08-08 19:28:31', 0, '2023-08-08 19:28:31', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1029, 0, 1001003, 0, 'Item_Types', 'Item Types', NULL, b'1', b'0', b'0', '2023-08-15 20:22:54', 0, '2023-08-15 20:22:54', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1030, 0, 1001003, 0, 'Documents', 'Documents', NULL, b'1', b'0', b'0', '2023-08-17 12:21:21', 0, '2023-08-17 12:21:21', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1031, 0, 1001003, 1030, 'Accounts', 'Accounts', NULL, b'1', b'0', b'0', '2023-08-17 16:24:52', 0, '2023-08-17 16:24:52', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1032, 0, 1001003, 1031, 'Large_Packing', 'Large Packing', NULL, b'1', b'0', b'0', '2023-08-17 16:25:25', 0, '2023-08-17 16:25:25', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1033, 0, 1001003, 1031, 'Small_Packing', 'Small Packing', NULL, b'1', b'0', b'0', '2023-08-17 16:25:36', 0, '2023-08-17 16:25:36', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1034, 0, 1001003, 0, 'Transaction_Types', 'Transaction Types', NULL, b'1', b'0', b'0', '2023-08-19 00:55:47', 0, '2023-08-19 00:55:47', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1035, 0, 0, 0, 'Gender', 'Gender', NULL, b'1', b'0', b'0', '2023-08-30 09:29:00', 0, '2023-08-30 09:29:00', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1036, 0, 0, 1016, 'Areas', 'Areas', NULL, b'1', b'0', b'0', '2023-08-30 12:14:03', 0, '2023-08-30 12:14:03', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1037, 0, 0, 0, 'Field_Types', 'Field Types', NULL, b'1', b'0', b'0', '2023-08-30 19:19:54', 0, '2023-08-30 19:19:54', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1038, 0, 1001004, 0, 'Patient_Extra_Fields', 'Patient Extra Fields', NULL, b'1', b'0', b'0', '2023-08-30 22:44:41', 0, '2023-08-30 22:44:41', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1045, 0, 1001004, 0, 'RxMeddicine_Extra_Fields', 'RxMeddicine Extra Fields', NULL, b'1', b'0', b'0', '2023-09-01 12:29:37', 0, '2023-09-01 12:29:37', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1047, 0, 1001004, 0, 'Meal_Relations', 'Meal Relations', NULL, b'1', b'0', b'0', '2023-09-01 15:54:46', 0, '2023-09-01 15:54:46', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1048, 0, 1001004, 0, 'Precautions', 'Precautions', NULL, b'0', b'0', b'0', '2023-09-27 01:30:49', 0, '2023-09-27 01:30:49', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1049, 0, 1001004, 0, 'Report_Categories', 'Report Categories', NULL, b'0', b'0', b'0', '2023-09-27 01:30:58', 0, '2023-09-27 01:30:58', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1050, 0, 1001004, 0, 'Med_Remarks', 'Med Remarks', NULL, b'0', b'0', b'0', '2023-09-27 01:31:20', 0, '2023-09-27 01:31:20', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1051, 0, 1001004, 0, 'Client_Categories', 'Client Categories', NULL, b'0', b'0', b'0', '2023-09-27 01:31:29', 0, '2023-09-27 01:31:29', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1052, 0, 1001004, 0, 'Med_Categories', 'Med Categories', NULL, b'0', b'0', b'0', '2023-09-27 01:32:26', 0, '2023-09-27 01:32:26', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1053, 0, 1001004, 0, 'Manufacturer', 'Manufacturer', NULL, b'0', b'0', b'0', '2023-09-27 01:32:53', 0, '2023-09-27 01:32:53', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1054, 0, 0, 0, 'Roles', 'Roles', NULL, b'1', b'0', b'0', '2023-09-27 11:26:33', 0, '2023-09-27 11:26:33', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1055, 0, 0, 0, 'AppStatus', 'AppStatus', NULL, b'1', b'0', b'0', '2023-10-17 11:38:21', 0, '2023-10-17 11:38:21', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1056, 0, 0, 0, 'Permissions', 'Permissions', NULL, b'1', b'0', b'0', '2023-10-24 09:47:24', 0, '2023-10-24 09:47:24', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1057, 0, 1001004, 0, 'BP_Statuses', 'BP Statuses', NULL, b'0', b'0', b'0', '2023-12-04 19:30:54', 0, '2023-12-04 19:30:54', 0, b'1');
INSERT INTO `ctl_enums` VALUES (1058, 0, 0, 0, 'Input_Types', 'Input Types', NULL, b'0', b'0', b'0', '2023-12-05 16:56:53', 0, '2023-12-05 16:56:53', 0, b'1');

-- ----------------------------
-- Table structure for ctl_item
-- ----------------------------
DROP TABLE IF EXISTS `ctl_item`;
CREATE TABLE `ctl_item`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `ModuleId` int NULL DEFAULT NULL,
  `TypeId` int NULL DEFAULT NULL,
  `VendorId` int NULL DEFAULT NULL,
  `ManufacturersId` int NULL DEFAULT NULL,
  `CategoryId` int NULL DEFAULT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `PurRate` double NULL DEFAULT NULL,
  `SaleRate` double NULL DEFAULT NULL,
  `Conversion` double NULL DEFAULT NULL,
  `GstSaleRate` double NULL DEFAULT NULL,
  `GstPurRate` double NULL DEFAULT NULL,
  `SaleStRate` double NULL DEFAULT NULL,
  `PurStRate` double NULL DEFAULT NULL,
  `PurUnits` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `SaleUnits` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ExtraRate` double NULL DEFAULT NULL,
  `RetailRate` double NULL DEFAULT NULL,
  `PrMazdoori` double NULL DEFAULT NULL,
  `UnitPrice` double NULL DEFAULT NULL,
  `UnitsInStock` double NULL DEFAULT NULL,
  `Formula` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Remarks` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_item
-- ----------------------------
INSERT INTO `ctl_item` VALUES (1, 1, 1001004, 0, 0, 1053001, 1052001, 'Zorpent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Trizex', NULL, '2023-10-02 22:39:24', 0, '2023-10-02 22:39:24', 0, b'1');
INSERT INTO `ctl_item` VALUES (1, 3, 1001004, 0, 0, 1053001, 1052001, 'Calpol', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Epzicom', NULL, '2023-10-02 22:19:45', 0, '2023-10-02 22:19:45', 0, b'1');
INSERT INTO `ctl_item` VALUES (2, 0, 0, 1029001, 0, NULL, NULL, 'Containers', 100, 100, 100, 100, 10, NULL, NULL, '1', '2,5', 100, 0, NULL, 200.98765, 100, NULL, NULL, '2023-05-03 16:00:17', 0, '2023-05-03 16:00:17', 0, b'1');
INSERT INTO `ctl_item` VALUES (2, 1, 1001004, 0, 0, 1053001, 1052001, 'Calpol', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '.', NULL, '2023-11-09 15:40:56', 0, '2023-11-09 15:40:56', 0, b'1');
INSERT INTO `ctl_item` VALUES (2, 3, 1001004, 0, 0, 1053002, 1052002, 'Qalsan D', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Triumpq', NULL, '2023-10-02 22:19:45', 0, '2023-10-02 22:19:45', 0, b'1');
INSERT INTO `ctl_item` VALUES (3, 0, NULL, 1029002, 0, NULL, NULL, 'Stationary Products', 200, 300, 10, NULL, NULL, 100, 9, '1', '2', 40, 250, NULL, 300, 100, NULL, NULL, '2023-05-03 16:00:46', 0, '2023-05-03 16:00:46', 0, b'0');
INSERT INTO `ctl_item` VALUES (3, 1, 1001004, 0, 0, 1053002, 1052002, 'Qalsan D', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Triumpq', NULL, '2023-11-09 15:42:01', 0, '2023-11-09 15:42:01', 0, b'1');
INSERT INTO `ctl_item` VALUES (4, 0, 0, 1029001, 0, NULL, NULL, 'Wood Preservatives', 86, 100, 10, 540, 3, NULL, NULL, '1', '5', 90, 100, NULL, 900, 100, NULL, NULL, '2023-05-03 16:01:08', 0, '2023-05-03 16:01:08', 0, b'1');
INSERT INTO `ctl_item` VALUES (5, 0, 0, 1029001, 0, NULL, NULL, 'Computing Infrastructure', 210, 120, 8, NULL, NULL, 90, 9, '9', '2', 890, 250, NULL, 700, 100, NULL, NULL, '2023-05-03 16:02:02', 0, '2023-05-03 16:02:02', 0, b'1');
INSERT INTO `ctl_item` VALUES (6, 0, 0, 1029002, 0, NULL, NULL, 'Sugar', 100, 100, NULL, NULL, NULL, NULL, NULL, NULL, '1,2,5', NULL, 90, NULL, NULL, NULL, NULL, NULL, '2023-08-21 11:49:19', 0, '2023-08-21 11:49:19', 0, b'0');
INSERT INTO `ctl_item` VALUES (7, 0, NULL, 1029002, 0, NULL, NULL, 'Rice', 100, 100, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 80, NULL, NULL, NULL, NULL, NULL, '2023-08-21 12:14:11', 0, '2023-08-21 12:14:11', 0, b'0');

-- ----------------------------
-- Table structure for ctl_logevent
-- ----------------------------
DROP TABLE IF EXISTS `ctl_logevent`;
CREATE TABLE `ctl_logevent`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `InTime` datetime NULL DEFAULT NULL,
  `OutTime` datetime NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `Message` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  CONSTRAINT `CTL_logevent_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sec_aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_logevent
-- ----------------------------

-- ----------------------------
-- Table structure for ctl_smtpcredentials
-- ----------------------------
DROP TABLE IF EXISTS `ctl_smtpcredentials`;
CREATE TABLE `ctl_smtpcredentials`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `Server` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Port` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `UserName` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Password` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_smtpcredentials
-- ----------------------------
INSERT INTO `ctl_smtpcredentials` VALUES (1, 1, 'smtp.gmail.com', '587', 'bintameer212@gmail.com', 'sbrettzymokvcmnq', '2023-10-11 22:08:50', 0, 0, '2023-10-11 22:08:50', b'0');

-- ----------------------------
-- Table structure for ctl_supplier
-- ----------------------------
DROP TABLE IF EXISTS `ctl_supplier`;
CREATE TABLE `ctl_supplier`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `AccId` int NULL DEFAULT NULL,
  `CountryId` int NULL DEFAULT NULL,
  `CityId` int NULL DEFAULT NULL,
  `CustomerId` int NULL DEFAULT NULL,
  `DiscRate` double NULL DEFAULT NULL,
  `CompanyName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ContactName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Address` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Phone` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsCustomer` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_supplier
-- ----------------------------

-- ----------------------------
-- Table structure for ctl_uomconversion
-- ----------------------------
DROP TABLE IF EXISTS `ctl_uomconversion`;
CREATE TABLE `ctl_uomconversion`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UOMId` int NULL DEFAULT NULL,
  `ConvertedUOMId` int NULL DEFAULT NULL,
  `IsBaseUnit` bit(1) NULL DEFAULT NULL,
  `Qty` int NULL DEFAULT NULL,
  `Multiplier` double NULL DEFAULT NULL,
  `DisplayUOM` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ctl_uomconversion
-- ----------------------------

-- ----------------------------
-- Table structure for kas_voucher
-- ----------------------------
DROP TABLE IF EXISTS `kas_voucher`;
CREATE TABLE `kas_voucher`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `VchTypeId` int NULL DEFAULT NULL,
  `VendorId` int NULL DEFAULT NULL,
  `SalesmanId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `GodownId` int NULL DEFAULT NULL,
  `ApprovedById` int NULL DEFAULT NULL,
  `StatusId` int NULL DEFAULT NULL,
  `VchDate` datetime NULL DEFAULT NULL,
  `VchNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `InvNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `DocNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `DocDate` datetime NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `IsPosted` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of kas_voucher
-- ----------------------------
INSERT INTO `kas_voucher` VALUES (1, 2, 1, 0, 'N/A', 0, 0, 1014001, '2023-10-31 16:20:03', 'TV1', NULL, NULL, NULL, NULL, b'0', '2023-10-31 21:20:05', 0, '2023-10-31 21:20:05', 0, b'1');
INSERT INTO `kas_voucher` VALUES (2, 2, 1, 0, 'N/A', 0, 0, 1014001, '2023-10-31 16:23:57', 'TV2', NULL, NULL, NULL, NULL, b'0', '2023-10-31 21:25:05', 0, '2023-10-31 21:25:05', 0, b'1');
INSERT INTO `kas_voucher` VALUES (3, 2, 2, 0, 'N/A', 0, 0, 1014001, '2023-10-31 16:23:57', 'BRV1', NULL, NULL, NULL, NULL, b'0', '2023-10-31 21:25:05', 0, '2023-10-31 21:25:05', 0, b'1');

-- ----------------------------
-- Table structure for kas_voucherdetail
-- ----------------------------
DROP TABLE IF EXISTS `kas_voucherdetail`;
CREATE TABLE `kas_voucherdetail`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `VchId` int NOT NULL,
  `AcId` int NULL DEFAULT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `BillId` int NULL DEFAULT NULL,
  `Debit` double NULL DEFAULT NULL,
  `Credit` double NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Qty` int NULL DEFAULT NULL,
  `Rate` int NULL DEFAULT NULL,
  `IsDefaultDrCr` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `VchId`, `ClientId`) USING BTREE,
  INDEX `vchDetail_fbk_1_idx`(`VchId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of kas_voucherdetail
-- ----------------------------
INSERT INTO `kas_voucherdetail` VALUES (1, 2, 1, 1004003, 0, 0, 0, 800, NULL, 1, 0, b'1', '2023-10-31 21:20:05', 0, '2023-10-31 21:20:05', 0, b'1');
INSERT INTO `kas_voucherdetail` VALUES (1, 2, 2, 1004003, 0, 0, 0, 0, NULL, 1, 0, b'1', '2023-10-31 21:25:05', 0, '2023-10-31 21:25:05', 0, b'1');
INSERT INTO `kas_voucherdetail` VALUES (1, 2, 3, 1004005, 0, 0, 0, 0, NULL, 1, 0, b'1', '2023-10-31 21:25:10', 0, '2023-10-31 21:25:10', 0, b'1');
INSERT INTO `kas_voucherdetail` VALUES (2, 2, 1, 1004003, 0, 0, 800, 0, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 1, 0, b'0', '2023-11-27 21:01:01', 0, '2023-11-27 21:01:01', 0, b'1');

-- ----------------------------
-- Table structure for kas_vouchertype
-- ----------------------------
DROP TABLE IF EXISTS `kas_vouchertype`;
CREATE TABLE `kas_vouchertype`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `Name` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DefaultDrCrFirst` bit(1) NULL DEFAULT NULL,
  `KeyCode` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DefaultDrCrSecondId` int NOT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of kas_vouchertype
-- ----------------------------
INSERT INTO `kas_vouchertype` VALUES (1, 0, 'Bank Receipt Voucher', b'0', 'BRV', 1004024, 0, NULL, 0, NULL, b'1');
INSERT INTO `kas_vouchertype` VALUES (1, 2, 'Testing Voucher', b'1', 'TV', 1004003, 0, '2023-10-10 01:51:36', 0, '2023-10-10 01:51:36', b'1');
INSERT INTO `kas_vouchertype` VALUES (1, 3, 'Bank Receipt Voucher', b'0', 'BRV', 1004003, 0, '2023-10-10 00:16:24', 0, '2023-10-10 00:16:24', b'1');
INSERT INTO `kas_vouchertype` VALUES (2, 0, 'Cash Receipt Voucher', b'0', 'CRV', 1004025, 0, NULL, 0, NULL, b'1');
INSERT INTO `kas_vouchertype` VALUES (2, 2, 'Bank Receipt Voucher', b'0', 'BRV', 1004005, 0, '2023-10-10 01:54:43', 0, '2023-10-10 01:54:43', b'1');
INSERT INTO `kas_vouchertype` VALUES (2, 3, 'Cash Receipt Voucher', b'0', 'CRV', 1004004, 0, '2023-10-10 00:32:21', 0, '2023-10-10 00:32:21', b'1');
INSERT INTO `kas_vouchertype` VALUES (3, 0, 'Bank Payment Voucher', b'1', 'BPV', 1004026, 0, NULL, 0, NULL, b'1');
INSERT INTO `kas_vouchertype` VALUES (4, 0, 'Cash Payment Voucher', b'1', 'CPV', 1004027, 0, NULL, 0, NULL, b'1');
INSERT INTO `kas_vouchertype` VALUES (5, 0, 'Journal Voucher', NULL, 'JV', 1004028, 0, NULL, 0, NULL, b'1');
INSERT INTO `kas_vouchertype` VALUES (6, 0, 'Sales Voucher', b'0', 'SALE', 0, 0, '2023-08-19 01:19:45', NULL, NULL, b'1');

-- ----------------------------
-- Table structure for ntf_notificationlog
-- ----------------------------
DROP TABLE IF EXISTS `ntf_notificationlog`;
CREATE TABLE `ntf_notificationlog`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DateTime` datetime NULL DEFAULT NULL,
  `Phone` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `SMS` varchar(6000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsSent` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  CONSTRAINT `NTF_notificationlog_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sec_aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ntf_notificationlog
-- ----------------------------

-- ----------------------------
-- Table structure for ntf_notificationtemplate
-- ----------------------------
DROP TABLE IF EXISTS `ntf_notificationtemplate`;
CREATE TABLE `ntf_notificationtemplate`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `KeyCode` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TemplateName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Subject` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Body` varchar(8000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SMS` varchar(160) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ntf_notificationtemplate
-- ----------------------------
INSERT INTO `ntf_notificationtemplate` VALUES (1, 1, 'NotificationToSupervisor_OnDayStart', '', 'MicroERP : User #User has started her/his day Start @Time', 'Dear #Supervisor, \r\n User #User has started his/her day start by picking the following tasks : \r\n  #Tasks', '', '1900-01-01 00:00:00', 1, NULL, NULL, b'1');
INSERT INTO `ntf_notificationtemplate` VALUES (2, 1, 'NotificationToSupersor_OnDayEnd', NULL, 'MicroERP - User #User has marked his/her day end @Time', 'Dear #Supervisor, User #User has marked his/her day end by mentioning the following status: #DayEndStatus', NULL, '1999-01-10 00:00:00', 1, NULL, NULL, b'1');

-- ----------------------------
-- Table structure for pms_appointment
-- ----------------------------
DROP TABLE IF EXISTS `pms_appointment`;
CREATE TABLE `pms_appointment`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `PatientId` int NULL DEFAULT NULL,
  `GenderId` int NULL DEFAULT NULL,
  `DoctorId` int NULL DEFAULT NULL,
  `StatusId` int NULL DEFAULT NULL,
  `TokenNo` bigint NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `Time` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Age` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Status` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_appointment
-- ----------------------------

-- ----------------------------
-- Table structure for pms_doctor
-- ----------------------------
DROP TABLE IF EXISTS `pms_doctor`;
CREATE TABLE `pms_doctor`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `CountryId` int NULL DEFAULT NULL,
  `CityId` int NULL DEFAULT NULL,
  `AreaId` int NULL DEFAULT NULL,
  `GenderId` int NULL DEFAULT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DoctorName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DateOfBirth` date NULL DEFAULT NULL,
  `ContactNo` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `HouseNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Address` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `DefApptDur` int NULL DEFAULT NULL,
  `Specialization` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `StartTime` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '	' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_doctor
-- ----------------------------

-- ----------------------------
-- Table structure for pms_patient
-- ----------------------------
DROP TABLE IF EXISTS `pms_patient`;
CREATE TABLE `pms_patient`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `CountryId` int NULL DEFAULT NULL,
  `CityId` int NULL DEFAULT NULL,
  `AreaId` int NULL DEFAULT NULL,
  `GenderId` int NULL DEFAULT NULL,
  `Email` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `PatientName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DateOfBirth` datetime NULL DEFAULT NULL,
  `ContactNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `HouseNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Address` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Remarks` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '	' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_patient
-- ----------------------------

-- ----------------------------
-- Table structure for pms_patientreport
-- ----------------------------
DROP TABLE IF EXISTS `pms_patientreport`;
CREATE TABLE `pms_patientreport`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `RxId` int NOT NULL,
  `CategoryId` int NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `Name` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ReportBase64Path` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`, `RxId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_patientreport
-- ----------------------------

-- ----------------------------
-- Table structure for pms_prescription
-- ----------------------------
DROP TABLE IF EXISTS `pms_prescription`;
CREATE TABLE `pms_prescription`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `DoctorId` int NULL DEFAULT NULL,
  `PatientId` int NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `Time` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `TokenNo` int NULL DEFAULT NULL,
  `Amount` float NULL DEFAULT NULL,
  `Remarks` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `MedDetRemarks` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Temperature` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `BPStatusId` int NULL DEFAULT NULL,
  `Weight` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `IsSugarPatient` bit(1) NULL DEFAULT NULL,
  `PrecautionIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `NextVisitNo` int NULL DEFAULT NULL,
  `NextVisitDate` datetime NULL DEFAULT NULL,
  `Comments` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_prescription
-- ----------------------------

-- ----------------------------
-- Table structure for pms_ptextrafieldsdata
-- ----------------------------
DROP TABLE IF EXISTS `pms_ptextrafieldsdata`;
CREATE TABLE `pms_ptextrafieldsdata`  (
  `PatientId` int NOT NULL,
  `ClientId` int NOT NULL,
  `FieldId` int NOT NULL,
  `FieldValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`PatientId`, `ClientId`, `FieldId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_ptextrafieldsdata
-- ----------------------------

-- ----------------------------
-- Table structure for pms_rxextrafieldsdata
-- ----------------------------
DROP TABLE IF EXISTS `pms_rxextrafieldsdata`;
CREATE TABLE `pms_rxextrafieldsdata`  (
  `RxId` int NOT NULL,
  `ClientId` int NOT NULL,
  `FieldId` int NOT NULL,
  `FieldValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`RxId`, `ClientId`, `FieldId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_rxextrafieldsdata
-- ----------------------------

-- ----------------------------
-- Table structure for pms_rxmedicine
-- ----------------------------
DROP TABLE IF EXISTS `pms_rxmedicine`;
CREATE TABLE `pms_rxmedicine`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `RxId` int NOT NULL,
  `MedId` int NULL DEFAULT NULL,
  `MRId` int NULL DEFAULT NULL,
  `RemarksId` int NULL DEFAULT NULL,
  `AMQty` int NULL DEFAULT NULL,
  `EveQty` int NULL DEFAULT NULL,
  `NoonQty` int NULL DEFAULT NULL,
  `Days` int NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`, `RxId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_rxmedicine
-- ----------------------------

-- ----------------------------
-- Table structure for pms_staff
-- ----------------------------
DROP TABLE IF EXISTS `pms_staff`;
CREATE TABLE `pms_staff`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `CountryId` int NULL DEFAULT NULL,
  `CityId` int NULL DEFAULT NULL,
  `AreaId` int NULL DEFAULT NULL,
  `GenderId` int NULL DEFAULT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DateOfBirth` date NULL DEFAULT NULL,
  `ContactNo` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `HouseNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Address` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pms_staff
-- ----------------------------

-- ----------------------------
-- Table structure for sal_dc
-- ----------------------------
DROP TABLE IF EXISTS `sal_dc`;
CREATE TABLE `sal_dc`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `AcId` int NULL DEFAULT NULL,
  `CustId` int NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `InvNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_dc
-- ----------------------------

-- ----------------------------
-- Table structure for sal_dcdetail
-- ----------------------------
DROP TABLE IF EXISTS `sal_dcdetail`;
CREATE TABLE `sal_dcdetail`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `DCId` int NOT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `Qty` int NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`, `DCId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_dcdetail
-- ----------------------------

-- ----------------------------
-- Table structure for sal_documentextras
-- ----------------------------
DROP TABLE IF EXISTS `sal_documentextras`;
CREATE TABLE `sal_documentextras`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `DocExtraTypeId` int NULL DEFAULT NULL,
  `DocExtraId` int NULL DEFAULT NULL,
  `IncDecTypeId` int NULL DEFAULT NULL,
  `FormulaId` int NULL DEFAULT NULL,
  `Value` double NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_documentextras
-- ----------------------------

-- ----------------------------
-- Table structure for sal_itemuom
-- ----------------------------
DROP TABLE IF EXISTS `sal_itemuom`;
CREATE TABLE `sal_itemuom`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `ItemId` int NULL DEFAULT NULL,
  `UOMId` int NULL DEFAULT NULL,
  `UOMTypeId` int NULL DEFAULT NULL,
  `SalePrice` double NULL DEFAULT NULL,
  `PurPrice` double NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_itemuom
-- ----------------------------

-- ----------------------------
-- Table structure for sal_itemvariants
-- ----------------------------
DROP TABLE IF EXISTS `sal_itemvariants`;
CREATE TABLE `sal_itemvariants`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `ItemId` int NULL DEFAULT NULL,
  `AttributeValuesIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `PurchaseExtraRate` double NULL DEFAULT NULL,
  `SaleExtraRate` double NULL DEFAULT NULL,
  `BarCode` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `StockValue` double NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE,
  INDEX `foreign_key_ibfk_1_idx`(`ItemId`) USING BTREE,
  CONSTRAINT `foreign_key_ibfk_1` FOREIGN KEY (`ItemId`) REFERENCES `ctl_item` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_itemvariants
-- ----------------------------

-- ----------------------------
-- Table structure for sal_productattrib
-- ----------------------------
DROP TABLE IF EXISTS `sal_productattrib`;
CREATE TABLE `sal_productattrib`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `AttribId` int NULL DEFAULT NULL,
  `AttribValId` int NULL DEFAULT NULL,
  `SaleRate` double NULL DEFAULT NULL,
  `PurRate` double NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_productattrib
-- ----------------------------

-- ----------------------------
-- Table structure for sal_producttaxes
-- ----------------------------
DROP TABLE IF EXISTS `sal_producttaxes`;
CREATE TABLE `sal_producttaxes`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `TaxId` int NULL DEFAULT NULL,
  `Amount` double NULL DEFAULT NULL,
  `IsVariant` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_producttaxes
-- ----------------------------

-- ----------------------------
-- Table structure for sal_purchase
-- ----------------------------
DROP TABLE IF EXISTS `sal_purchase`;
CREATE TABLE `sal_purchase`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `SupplierId` int NULL DEFAULT NULL,
  `AcId` int NULL DEFAULT NULL,
  `StatusId` int NULL DEFAULT NULL,
  `IsPosted` bit(1) NULL DEFAULT NULL,
  `InvNo` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `Gross` double NULL DEFAULT NULL,
  `Discount` double NULL DEFAULT NULL,
  `Gst` double NULL DEFAULT NULL,
  `Credit` double NULL DEFAULT NULL,
  `Debit` double NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_purchase
-- ----------------------------

-- ----------------------------
-- Table structure for sal_purchaseline
-- ----------------------------
DROP TABLE IF EXISTS `sal_purchaseline`;
CREATE TABLE `sal_purchaseline`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `PrchId` int NOT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `ItemVariantId` int NULL DEFAULT NULL,
  `PurUnitId` int NULL DEFAULT NULL,
  `ProductAttribIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Qty` int NULL DEFAULT NULL,
  `PurchaseRate` double NULL DEFAULT NULL,
  `DiscPer` double NULL DEFAULT NULL,
  `GSTRate` double NULL DEFAULT NULL,
  `GSTRetailRate` double NULL DEFAULT NULL,
  `RetailRate` double NULL DEFAULT NULL,
  `Amount` double NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`, `PrchId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_purchaseline
-- ----------------------------

-- ----------------------------
-- Table structure for sal_sale
-- ----------------------------
DROP TABLE IF EXISTS `sal_sale`;
CREATE TABLE `sal_sale`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `AcId` int NULL DEFAULT NULL,
  `SalesmanId` int NULL DEFAULT NULL,
  `SupplierId` int NULL DEFAULT NULL,
  `StatusId` int NULL DEFAULT NULL,
  `CustId` int NULL DEFAULT NULL,
  `IsPosted` bit(1) NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `InvNo` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Gross` double NULL DEFAULT NULL,
  `Discount` double NULL DEFAULT NULL,
  `GST` double NULL DEFAULT NULL,
  `Debit` double NULL DEFAULT NULL,
  `Credit` double NULL DEFAULT NULL,
  `PackChrgs` double NULL DEFAULT NULL,
  `FreightChrgs` double NULL DEFAULT NULL,
  `NetPayable` double NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_sale
-- ----------------------------

-- ----------------------------
-- Table structure for sal_saleline
-- ----------------------------
DROP TABLE IF EXISTS `sal_saleline`;
CREATE TABLE `sal_saleline`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `SaleId` int NOT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `ItemVariantId` int NULL DEFAULT NULL,
  `SaleUnitId` int NULL DEFAULT NULL,
  `ProductAttribIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `IssueQty` int NULL DEFAULT NULL,
  `RetQty` int NULL DEFAULT NULL,
  `SaleQty` int NULL DEFAULT NULL,
  `SaleRate` double NULL DEFAULT NULL,
  `DiscRate` double NULL DEFAULT NULL,
  `GSTRate` double NULL DEFAULT NULL,
  `GSTRetailRate` double NULL DEFAULT NULL,
  `RetailRate` double NULL DEFAULT NULL,
  `Amount` double NULL DEFAULT NULL,
  `FTaxRate` double NULL DEFAULT NULL,
  `WhtRate` double NULL DEFAULT NULL,
  `Disc` double NULL DEFAULT NULL,
  `BulkDisc` double NULL DEFAULT NULL,
  `QtyDisc` double NULL DEFAULT NULL,
  `GST` double NULL DEFAULT NULL,
  `GSTRet` double NULL DEFAULT NULL,
  `FTax` double NULL DEFAULT NULL,
  `Wht` double NULL DEFAULT NULL,
  `ChrgsAdd` double NULL DEFAULT NULL,
  `ChrgsLess` double NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`, `SaleId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_saleline
-- ----------------------------

-- ----------------------------
-- Table structure for sal_salestock
-- ----------------------------
DROP TABLE IF EXISTS `sal_salestock`;
CREATE TABLE `sal_salestock`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `SaleTransId` int NULL DEFAULT NULL,
  `GodownId` int NULL DEFAULT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `DebitId` int NULL DEFAULT NULL,
  `JobId` int NULL DEFAULT NULL,
  `PurchaseQty` int NULL DEFAULT NULL,
  `IssueQty` int NULL DEFAULT NULL,
  `ReturnQty` int NULL DEFAULT NULL,
  `SaleQty` int NULL DEFAULT NULL,
  `FreeQty` int NULL DEFAULT NULL,
  `SaleRate` int NULL DEFAULT NULL,
  `SaleGstRate` int NULL DEFAULT NULL,
  `ReturnGstRate` int NULL DEFAULT NULL,
  `DiscRate` int NULL DEFAULT NULL,
  `DiscAmt` int NULL DEFAULT NULL,
  `ExtraRate` int NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Conversion` int NULL DEFAULT NULL,
  `RetailRate` int NULL DEFAULT NULL,
  `SoNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_salestock
-- ----------------------------

-- ----------------------------
-- Table structure for sal_stocktransfer
-- ----------------------------
DROP TABLE IF EXISTS `sal_stocktransfer`;
CREATE TABLE `sal_stocktransfer`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `InvNo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `TransferFrom` int NULL DEFAULT NULL,
  `TransferTo` int NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_stocktransfer
-- ----------------------------

-- ----------------------------
-- Table structure for sal_stocktransferline
-- ----------------------------
DROP TABLE IF EXISTS `sal_stocktransferline`;
CREATE TABLE `sal_stocktransferline`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `STId` int NOT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `GodownId` int NULL DEFAULT NULL,
  `ProductUnits` double NULL DEFAULT NULL,
  `Qty` int NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`, `STId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sal_stocktransferline
-- ----------------------------

-- ----------------------------
-- Table structure for sch_schedule
-- ----------------------------
DROP TABLE IF EXISTS `sch_schedule`;
CREATE TABLE `sch_schedule`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EntityId` int NOT NULL,
  `ScheduleTypeId` int NOT NULL,
  `WorkingTypeId` int NULL DEFAULT NULL,
  `WorkingHours` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `StartDate` datetime NULL DEFAULT NULL,
  `EndDate` datetime NULL DEFAULT NULL,
  `EffectiveDate` datetime NULL DEFAULT NULL,
  `CreatedBy` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `ModifiedBy` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sch_schedule
-- ----------------------------

-- ----------------------------
-- Table structure for sch_scheduleday
-- ----------------------------
DROP TABLE IF EXISTS `sch_scheduleday`;
CREATE TABLE `sch_scheduleday`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `SchId` int NULL DEFAULT NULL,
  `DayId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedBy` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `ModifiedBy` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sch_scheduleday
-- ----------------------------

-- ----------------------------
-- Table structure for sch_scheduledayevent
-- ----------------------------
DROP TABLE IF EXISTS `sch_scheduledayevent`;
CREATE TABLE `sch_scheduledayevent`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `SchId` int NULL DEFAULT NULL,
  `ScheduleDayId` int NULL DEFAULT NULL,
  `StartTime` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EndTime` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `LocationId` int NULL DEFAULT NULL,
  `EventTypeId` int NULL DEFAULT NULL,
  `Sp` double NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sch_scheduledayevent
-- ----------------------------

-- ----------------------------
-- Table structure for sec_aspnetusers
-- ----------------------------
DROP TABLE IF EXISTS `sec_aspnetusers`;
CREATE TABLE `sec_aspnetusers`  (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NULL DEFAULT NULL,
  `SupervisorId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ModuleId` int NULL DEFAULT NULL,
  `RoleId` int NULL DEFAULT NULL,
  `FirstName` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DirectSupervisorName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DirectSupervisorId` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `LastName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserPassword` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Name` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EmployeeId` int NULL DEFAULT NULL,
  `FatherName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CNIC` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Address` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Designation` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MSCardNo` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DiscountLimit` int NULL DEFAULT NULL,
  `BranchId` int NULL DEFAULT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) NULL DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sec_aspnetusers
-- ----------------------------
INSERT INTO `sec_aspnetusers` VALUES ('1c20c051-adf3-4e14-8772-65e3ce658a76', 1, '3bba76e4-9e83-4ac2-9ef1-1b26e2e300d5', 1001004, 1054004, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'usman@gmail.com', 'USMAN@GMAIL.COM', 'usman@gmail.com', 'USMAN@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEAnnvRcQ//hraCe47vLQVg2wkd0sQ0bkawR8eWnBGaN8KxYAcP46IMH9+0llowYMAw==', 'VSZCIMKSVFOZWFU4A2OGFJDWORSYMRT6', 'a4f64926-918f-4905-8352-2338b56a4798', '09999999999', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('3bba76e4-9e83-4ac2-9ef1-1b26e2e300d5', 1, '8d4b78f3-a98b-4c48-80be-7435514a3ccd', 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'C1@gmail.com', 'C1@GMAIL.COM', 'C1@gmail.com', 'C1@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEPgefNSv5X0nFqfBfi5cWNXORQ78o4a2d0m0j+F2Ny32nAFmmcwHZwU0yvsurW11PA==', 'DOVDBSWWHEHADYCLKLLOT6MZCSZ6TJRG', '7bbc5f53-c562-4e48-92a8-40c908bde35f', '0999999999', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('41d895c7-ffc3-4ff2-a0f2-e305e4a21802', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'C3@gmail.com', 'C3@GMAIL.COM', 'C3@gmail.com', 'C3@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEKGvQC/yduNLPGkWtP6Yg3Re08G3zn4z4i68U5xI2BnTkfD6Qp/LPxAXrIeo7TSWyw==', 'OYF7EPNXT4BODFRYUUTVLJGRXBR6MQ32', '69ffb845-74e1-4a0b-b51f-afac3557b962', '0990788789', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('6fb87573-da6a-49ca-a9af-79e7d10c39d0', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Client_4', 'CLIENT_4', 'C4@gmail.com', 'C4@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEBgKrFYgW2bVqxikYxad5KE9cvwz1oCG40TFWTqLexVD8rD3ROkvn0W8pMEqbY9D3g==', 'MWXHUPINLPYJLMCEDM6IHWMF5TDD6MDO', 'cf6a3551-2b6f-4c08-99fa-2090f562833c', '9897987897w98', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('70d2b185-cc89-48a7-9194-b1ddd458fa23', 1, '3bba76e4-9e83-4ac2-9ef1-1b26e2e300d5', 1001004, 1054003, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'ahmad@gmail.com', 'AHMAD@GMAIL.COM', 'ahmad@gmail.com', 'AHMAD@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEA7G+r+yCXOMFg1A66sJMh8hoZvwMAWkkcr4Rp+IT3z/QDg3ZpLtzN1XRFSUdhEbBQ==', 'A3K3XDI5L6ULSI26QWXUKROVHMI3NI6G', '9347177a-afe4-48f0-a963-af4abbff2796', '99879806576', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('8d4b78f3-a98b-4c48-80be-7435514a3ccd', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'zaman.badar@gmail.com', 'ZAMAN.BADAR@GMAIL.COM', 'zaman.badar@gmail.com', 'ZAMAN.BADAR@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEKy5jyOgLRretIts12cZzKpVzKP+RwySzJpQeV0sE6Xq2Ev4vvm55gfjG23jsKsolQ==', 'PEAM7OKKGZUELQEPGOWW64QH636CVKMR', '2472d89f-289f-4072-9cf0-61d584591565', '03234027206', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('c35d2b3b-6cb9-480b-9868-ec8dbf343783', 0, NULL, 0, 1054001, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'QST', 'QST', 'S@gmail.com', 'S@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEAIJK2fwgLIzn8ZXyDL2Dpb9AikPUsit1ESdKHzW0NRkK2q0kJXhxJsScA2/LuXpcA==', '52CBWMIWBJM4UK3P23RQTUGYJML62H4U', '44b8b67d-45a6-4383-8602-b0910fe56182', '078979809', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('c7f53801-a301-437a-8627-6c8962646923', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Client_5', 'CLIENT_5', 'C5@gmail.com', 'C5@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEBRq5DFwwN3IXMRwJ6kv3Lw7Mrj52XRmigO8H+Cf+1eOaIl9wew7BxIngX54d35yzQ==', 'MQORTOFWRLM3QIHYCZVNMFEW7GD7IQIT', 'a896107e-c63d-4dde-9e01-041c5ec98b7f', '9897987897w98', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('d0f71c3a-0192-4446-a594-68b4c75cba90', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'C2@gmail.com', 'C2@GMAIL.COM', 'C2@gmail.com', 'C2@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEM6xhbgVtVs7AlpIi0fJEgpjQnNzrJ21qqvB9KLi+mycGQhHQevTY0T+u2B2f9qr2A==', 'LGBQNN3XVA5U3QWOGOI2ZYVTOBUMAC5O', '71656325-ccc1-4f38-9b29-d5679970a6b5', '09999999999', 0, 0, NULL, 1, 0, NULL);
INSERT INTO `sec_aspnetusers` VALUES ('d62b1d37-a608-4dd7-8342-adda5d272a67', 1, '3bba76e4-9e83-4ac2-9ef1-1b26e2e300d5', 1001004, 1054003, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'nimrah@gmail.com', 'NIMRAH@GMAIL.COM', 'nimrah@gmail.com', 'NIMRAH@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEIfCshQYva7g4YneWRjhnOIE66nW/YLFRFd6hlG0VG4rPfpD9/BG64DgWDhUIzQ+HA==', 'KUUACZMHO6QU5GZN4SWLMWYHRYP322U5', '79c4bd49-b47e-4f5e-9d0c-5a1f10e435cd', '0999999999', 0, 0, NULL, 1, 0, NULL);

-- ----------------------------
-- Table structure for sec_permission
-- ----------------------------
DROP TABLE IF EXISTS `sec_permission`;
CREATE TABLE `sec_permission`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `RoleId` int NULL DEFAULT NULL,
  `RouteId` int NULL DEFAULT NULL,
  `PermissionId` int NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sec_permission
-- ----------------------------

-- ----------------------------
-- Table structure for tms_attachments
-- ----------------------------
DROP TABLE IF EXISTS `tms_attachments`;
CREATE TABLE `tms_attachments`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `TaskId` int NOT NULL,
  `Name` varchar(700) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `DocPath` varchar(7000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Base64File` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Size` varchar(900) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE,
  INDEX `TaskId`(`TaskId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tms_attachments
-- ----------------------------

-- ----------------------------
-- Table structure for tms_task
-- ----------------------------
DROP TABLE IF EXISTS `tms_task`;
CREATE TABLE `tms_task`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ModuleId` int NULL DEFAULT NULL,
  `StatusId` int NULL DEFAULT NULL,
  `PriorityId` int NULL DEFAULT NULL,
  `Title` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SP` int NULL DEFAULT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Reason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` int NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tms_task
-- ----------------------------

-- ----------------------------
-- Table structure for tms_taskcomment
-- ----------------------------
DROP TABLE IF EXISTS `tms_taskcomment`;
CREATE TABLE `tms_taskcomment`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `TaskId` int NOT NULL,
  `UserId` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Comment` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Time` datetime NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` int NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE,
  INDEX `TaskId_idx`(`TaskId`) USING BTREE,
  CONSTRAINT `TaskId` FOREIGN KEY (`TaskId`) REFERENCES `tms_task` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tms_taskcomment
-- ----------------------------

-- ----------------------------
-- Table structure for tms_usertask
-- ----------------------------
DROP TABLE IF EXISTS `tms_usertask`;
CREATE TABLE `tms_usertask`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `TaskId` int NULL DEFAULT NULL,
  `ClaimId` int NULL DEFAULT NULL,
  `ApprovedClaimId` int NULL DEFAULT NULL,
  `LastClaimId` int NULL DEFAULT NULL,
  `StatusId` int NULL DEFAULT NULL,
  `WorkTime` float UNSIGNED NULL DEFAULT NULL,
  `Parent` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Date` datetime NULL DEFAULT NULL,
  `Sp` float NULL DEFAULT NULL,
  `Comments` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ReviewedBy` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ReviewComments` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsDayEnded` bit(1) NULL DEFAULT b'0',
  `CreatedById` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tms_usertask
-- ----------------------------

-- ----------------------------
-- Table structure for voc_uservocabulary
-- ----------------------------
DROP TABLE IF EXISTS `voc_uservocabulary`;
CREATE TABLE `voc_uservocabulary`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `WordId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Pronunciation` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Sentence` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `VocabDifficultyLevelId` int NULL DEFAULT NULL,
  `NovelId` int NULL DEFAULT NULL,
  `ChapterId` int NULL DEFAULT NULL,
  `Comments` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsNeedHelp` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of voc_uservocabulary
-- ----------------------------

-- ----------------------------
-- Table structure for voc_vocabulary
-- ----------------------------
DROP TABLE IF EXISTS `voc_vocabulary`;
CREATE TABLE `voc_vocabulary`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `Word` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EnglishMeaning` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `UrduMeaning` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `CreatedById` int NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of voc_vocabulary
-- ----------------------------

-- ----------------------------
-- Procedure structure for ATT_ManageAttendance
-- ----------------------------
DROP PROCEDURE IF EXISTS `ATT_ManageAttendance`;
delimiter ;;
CREATE PROCEDURE `ATT_ManageAttendance`(In prm_Filter varchar (50),
  In prm_Id int ,
  In prm_ClientId int,
  In prm_SchDayId int ,
  In prm_UserId   Varchar(255),
  In prm_DayStartTime  datetime ,
  In prm_DayEndTime datetime ,
  In prm_Date datetime ,
  IN prm_CreatedBy INT,
  IN prm_CreatedOn datetime ( 0 ),
  IN prm_ModifiedBy INT,
  IN prm_ModifiedOn datetime ( 0 ),
  In prm_IsActive bit ( 1 ))
BEGIN

	

	-- SET prm_RetVal =- 1;

	IF

		( prm_Filter = 'Insert' ) THEN

		BEGIN
 
			insert into ATT_Attendance (Id,ClientId, UserId, SchDayId ,DayStartTime ,DayEndTime ,
            Date, CreatedBy, CreatedOn, IsActive) 
			values (prm_Id,prm_ClientId, prm_UserId,prm_SchDayId  ,prm_DayStartTime ,prm_DayEndTime ,
            prm_Date,  prm_CreatedBy, prm_CreatedOn, prm_IsActive );

			-- SET prm_RetVal = LAST_INSERT_ID();

		END;

		ELSE

		IF

			( prm_Filter = 'Update' ) THEN

			BEGIN

				Update ATT_Attendance
				Set 
              UserId= prm_UserId,
              ClientId = prm_ClientId,
			  SchDayId= prm_SchDayId  ,
			  DayStartTime=prm_DayStartTime ,
			  DayEndTime=prm_DayEndTime ,
			  Date= prm_Date,
              ModifiedBy = prm_ModifiedBy,
				ModifiedOn = prm_ModifiedOn,
				IsActive = prm_IsActive
				Where Id = prm_Id;

				-- SET prm_RetVal = prm_Id;

			END;

			ELSE

			IF

				( prm_Filter = 'Delete' ) THEN

				BEGIN

					DELETE FROM `ATT_Attendance` 
					WHERE Id = prm_Id;

					-- SET prm_RetVal = prm_Id;

				END;

				ELSE

				IF

					( prm_Filter = 'Disable' ) THEN

					BEGIN

							UPDATE `ATT_Attendance` 

							SET IsActive = 0,

							ModifiedBy = prm_ModifiedBy,

							ModifiedOn = prm_ModifiedOn 

						WHERE

							Id = prm_Id;

					-- 	SET prm_RetVal = prm_Id;

					END;

				END IF;

			END IF;

		END IF;

	END IF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for ATT_Manage_Attendance
-- ----------------------------
DROP PROCEDURE IF EXISTS `ATT_Manage_Attendance`;
delimiter ;;
CREATE PROCEDURE `ATT_Manage_Attendance`(In prm_Filter varchar (50),
  In prm_Id int ,
  In prm_ClientId int,
  In prm_SchDayId int ,
  In prm_UserId   Varchar(255),
  In prm_DayStartTime  datetime ,
  In prm_DayEndTime datetime ,
  In prm_Date datetime ,
  IN prm_CreatedBy INT,
  IN prm_CreatedOn datetime ( 0 ),
  IN prm_ModifiedBy INT,
  IN prm_ModifiedOn datetime ( 0 ),
  In prm_IsActive bit ( 1 ))
BEGIN

	

	-- SET prm_RetVal =- 1;

	IF

		( prm_Filter = 'Insert' ) THEN

		BEGIN
 
			insert into ATT_Attendance (Id,ClientId, UserId, SchDayId ,DayStartTime ,DayEndTime ,
            Date, CreatedBy, CreatedOn, IsActive) 
			values (prm_Id,prm_ClientId, prm_UserId,prm_SchDayId  ,prm_DayStartTime ,prm_DayEndTime ,
            prm_Date,  prm_CreatedBy, prm_CreatedOn, prm_IsActive );

			-- SET prm_RetVal = LAST_INSERT_ID();

		END;

		ELSE

		IF

			( prm_Filter = 'Update' ) THEN

			BEGIN

				Update ATT_Attendance
				Set 
              UserId= prm_UserId,
              ClientId = prm_ClientId,
			  SchDayId= prm_SchDayId  ,
			  DayStartTime=prm_DayStartTime ,
			  DayEndTime=prm_DayEndTime ,
			  Date= prm_Date,
              ModifiedBy = prm_ModifiedBy,
				ModifiedOn = prm_ModifiedOn,
				IsActive = prm_IsActive
				Where Id = prm_Id;

				-- SET prm_RetVal = prm_Id;

			END;

			ELSE

			IF

				( prm_Filter = 'Delete' ) THEN

				BEGIN

					DELETE FROM `ATT_Attendance` 
					WHERE Id = prm_Id;

					-- SET prm_RetVal = prm_Id;

				END;

				ELSE

				IF

					( prm_Filter = 'Disable' ) THEN

					BEGIN

							UPDATE `ATT_Attendance` 

							SET IsActive = 0,

							ModifiedBy = prm_ModifiedBy,

							ModifiedOn = prm_ModifiedOn 

						WHERE

							Id = prm_Id;

					-- 	SET prm_RetVal = prm_Id;

					END;

				END IF;

			END IF;

		END IF;

	END IF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for ATT_Search_Attendance
-- ----------------------------
DROP PROCEDURE IF EXISTS `ATT_Search_Attendance`;
delimiter ;;
CREATE PROCEDURE `ATT_Search_Attendance`(in whereClause varchar(5000))
BEGIN
  set @querystr ='select * 
			FROM
			`microerp`.`vw_ATT_Attendance`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for cmt_Manage_SMTPCredentials
-- ----------------------------
DROP PROCEDURE IF EXISTS `cmt_Manage_SMTPCredentials`;
delimiter ;;
CREATE PROCEDURE `cmt_Manage_SMTPCredentials`(in prm_id int,
        in prm_clientId int,
		in prm_server text,
        in prm_port text,
        in prm_userName text,
		in prm_password text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into  cmt_smtpcredentials (Id,ClientId,Server,Port,UserName,
           Password,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_server, prm_port, prm_userName,prm_password,
           prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update   cmt_smtpcredentials set 
                          ClientId=prm_clientId,
                          Server=prm_server,
                          Port=prm_port,
                          UserName=prm_userName,
                          Password=prm_password,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where   smtpcredentials.Id =id;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from   cmt_smtpcredentials
			where
			  smtpcredentials.Id=id ; 
        END if;
IF DBoperation = 'Activate'
        then
            update   cmt_smtpcredentials set IsActive=1
			where 
			  smtpcredentials.Id=id ;
        END if;
IF DBoperation = 'DeActivate'
        then
            update   cmt_smtpcredentials set IsActive=0
			where
			  smtpcredentials.Id=id; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_Clients
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_Clients`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_Clients`(in prm_id int,
        in prm_userId varchar(255),
        in prm_moduleIds text,
        in prm_clientsName varchar(200),
		in prm_categoryId int,
        in prm_address varchar(200),
        in prm_countryId int,
		in prm_cityId int,
        in prm_contact varchar(200),
        in prm_owner varchar(200),
        in prm_releventPerson varchar(200),
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into  CTL_clients (Id,UserId,ModuleIds,ClientName,CategoryId,
           Address,CountryId,CityId,Contact,Owner,
           RelevantPerson, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_userId,prm_moduleIds, prm_clientsName, prm_categoryId,prm_address,
           prm_countryId,prm_cityId,prm_contact,prm_owner,
           prm_releventPerson, prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update   CTL_clients set 
                          UserId=prm_userId,
                          ModuleIds=prm_moduleIds,
                          ClientName=prm_clientsName,
                          CategoryId=prm_categoryId,
                          Address=prm_address,
                          CountryId=prm_countryId,
                          CityId=prm_cityId,
                          Contact=prm_contact,
                          Owner=prm_owner,
                          RelevantPerson=prm_releventPerson,
                          CreatedOn=prm_createdOn,
                          CreatedById=createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where   CTL_clients.Id =id;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from   CTL_clients
			where
			  clients.Id=id ; 
        END if;
IF DBoperation = 'Activate'
        then
            update   CTL_clients set IsActive=1
			where 
			  clients.Id=id ;
        END if;
IF DBoperation = 'DeActivate'
        then
            update   CTL_clients set IsActive=0
			where
			  clients.Id=id; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_Customer
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_Customer`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_Customer`(in prm_id int,
		in prm_clientId int,
        in prm_accId int,
        in prm_countryId int,
		in prm_cityId int,
        in prm_supplierId int,
        in prm_name varchar(100),
		in prm_email varchar(100),
        in prm_phone varchar(100),
		in prm_address text,
		in prm_region varchar(100),
		in prm_sendEmail bit, 
        in prm_isSupplier bit,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
    
           insert into CTL_customer (Id,ClientId,AccId,CountryId,CityId,SupplierId,Name,Email,Phone,
           Address,Region,SendEmail,IsSupplier, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_accId,prm_countryId,prm_cityId,prm_supplierId,prm_name,prm_email,prm_phone,prm_address,
           prm_region,prm_sendEmail,prm_isSupplier, prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive);
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update CTL_customer set 
						  ClientId=prm_clientId,
                          AccId=prm_accId,
                          CountryId=prm_countryId,
                          CityId=prm_cityId,
                          SupplierId=prm_supplierId,
                          Name=prm_name,
                          Email=prm_email,
                          Phone=prm_phone,
                          Address=prm_Address,
                          Region=prm_region,
                          SendEmail=prm_sendEmail,
                          IsSupplier=prm_isSupplier,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where customer.Id =id;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from CTL_customer
			where
			customer.Id=id ; 
        END if;
IF DBoperation = 'Activate'
        then
            update CTL_customer set IsActive=1
			where 
			customer.Id=id ;
        END if;
IF DBoperation = 'DeActivate'
        then
            update CTL_customer set IsActive=0
			where
			customer.Id=id; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_Item
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_Item`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_Item`(in prm_id int,
		in prm_clientId int,
        in prm_moduleId int,
        in prm_typeId int,
        in prm_vendorId int,
        in prm_name varchar(100),
        in prm_purRate double,
        in prm_saleRate double,
        in prm_conversion double,
        in prm_gstSaleRate double,
        in prm_gstPurRate double,
        in prm_saleStRate double,
        in prm_purStRate double,
        in prm_saleUnit text,
        in prm_purUnit text,
        in prm_extraRate double,
        in prm_retailRate double,
        in prm_prMazdoori double,
		in prm_unitPrice double,
        in prm_unitsInStock double,  
        in prm_manufacturerId int,
        in prm_formula text,
        in prm_categoryId int,
        in prm_remarks text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into microerp.CTL_item
           (Id,ClientId, ModuleId,TypeId,VendorId,Name,PurRate,SaleRate,Conversion, GstSaleRate,GstPurRate,
           PurUnits,SaleUnits,SaleStRate,PurStRate,RetailRate,ExtraRate,PrMazdoori,UnitPrice,UnitsInStock,
           ManufacturersId,Formula,CategoryId,Remarks,
           CreatedOn, CreatedById,ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId, prm_moduleId,prm_typeId,prm_vendorId,prm_name,prm_purRate,prm_saleRate,prm_conversion,prm_gstSaleRate,prm_gstPurRate,
           prm_purUnit,prm_saleUnit,prm_saleStRate,prm_purStRate,prm_retailRate,prm_extraRate,prm_prMazdoori,prm_unitPrice,prm_unitsInStock,
           prm_manufacturerId,prm_formula,prm_categoryId,prm_remarks,
           prm_createdOn, prm_createdById, prm_modifiedOn,prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update microerp.CTL_item set 
						  ClientId=prm_clientId,
                          ModuleId=prm_moduleId,
                          TypeId=prm_typeId,
                          VendorId=prm_vendorId,
                          Name=prm_name,
                          PurRate=prm_purRate,
                          SaleRate=prm_saleRate,
                          Conversion=prm_conversion,
                          GstSaleRate=prm_gstSaleRate,
                          GstPurRate=prm_gstPurRate,
                          SaleStRate=prm_saleStRate,
                          PurStRate=prm_purStRate,
                          SaleUnits=prm_saleUnit,
                          PurUnits=prm_purUnit,
                          RetailRate=prm_retailRate,
                          ExtraRate=prm_extraRate,
                          PrMazdoori=prm_prMazdoori,
                          UnitPrice=prm_unitPrice,
                          UnitsInStock=prm_unitsInStock,
                          ManufacturersId=prm_manufacturerId,
                          Formula=prm_formula,
                          CategoryId=prm_categoryId,
                          Remarks=prm_remarks,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where microerp.item.Id =id and item.ClientId=clientId;
             if isActive=false
             then update itemvariants set   IsActive=0  where 
             itemvariants.ItemId=id and itemvariants.ClientId=clientId;
             end if;
              if isActive=true
              then update itemvariants set   IsActive=1  where 
             itemvariants.ItemId=id and itemvariants.ClientId=clientId;
             end if;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from microerp.CTL_item
			where
			microerp.item.Id=id and  item.ClientId=clientId;
        END if;
IF DBoperation = 'Activate'
        then
            update microerp.CTL_item set IsActive=1
			where 
			microerp.item.Id=id and  item.ClientId=clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update microerp.CTL_item set IsActive=0
			where
			microerp.item.Id=id and  item.ClientId=clientId;
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_LogEvent
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_LogEvent`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_LogEvent`(in prm_id int,
		in prm_ClientId int,
        in prm_userId VARCHAR(255),
        in prm_inTime datetime,
		in prm_outTime datetime,
		in prm_date datetime,
		in prm_message varchar(5000),
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
    
           insert into CTL_logevent (Id,ClientId,UserId,InTime,OutTime,Date,Message,
           CreatedOn, CreatedById, 
           ModifiedOn, ModifiedById, IsActive )  
           values 
           (prm_id,prm_clientId,prm_userId,prm_inTime,prm_outTime,prm_date,prm_message, prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
     
          update CTL_logevent set  
						  ClientIdId= prm_clientId,
                          UserId= prm_userId,
                          InTime= prm_inTime,
                          OutTime=prm_outTime,
                          Date=prm_date,
                          Message=prm_message,
                          CreatedOn = prm_createdOn,
                          CreatedById =prm_createdById,
                          ModifiedOn = prm_modifiedOn,
                          ModifiedById = prm_modifiedById ,
						  IsActive = prm_isActive 				 
             where logevent.Id =id;
      end if;
       IF DBoperation = 'Delete'
    then
            delete from CTL_logevent
			where
			 logevent.Id=id ; 
        END if;
IF DBoperation = 'Activate'
        then
            update   CTL_logevent set IsActive=1
			where 
			 logevent.Id=id ;
        END if;
IF DBoperation = 'DeActivate'
        then
            update  CTL_logevent set IsActive=0
			where
			 logevent.Id=id; 
        END if;
      
  
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_Settings
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_Settings`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_Settings`(in prm_id int,
        in prm_clientId int,
        in prm_moduleId int,
        in prm_enumTypeId int,
        in prm_parentId int,
		in prm_levelId int,
        in prm_keyCode varchar(2000),
		in prm_accountCode varchar(2000),
        in prm_name varchar(500),
		in prm_value text,
        in prm_description varchar(5000),
		in prm_isSystemDefined bit,
        in prm_istAccountLevel bit,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
    
           insert into CTL_enumline (Id,ClientId, ModuleId,EnumTypeId,ParentId,LevelId,KeyCode,
           AccountCode,Name,Value,Description,IsSystemDefined,IstAccountLevel, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId, prm_moduleId,prm_enumTypeId,prm_parentId,prm_levelId,prm_keyCode,prm_accountCode,prm_name,prm_value,prm_description,prm_isSystemDefined,prm_istAccountLevel, prm_createdOn, prm_createdById,
           prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
/*update*/
if DBoperation ='Update'
 then
            update CTL_enumline set
            ClientId=prm_clientId,
            ModuleId=prm_moduleId,
            EnumTypeId=prm_enumTypeId,
			ParentId=prm_parentId,
		    LevelId=prm_levelId,
			KeyCode=prm_keyCode,
            AccountCode=prm_accountCode,
			Name=prm_name,
            Value=prm_value,
			Description=prm_description,
            IsSystemDefined=prm_isSystemDefined,
            IstAccountLevel=prm_istAccountLevel,
			CreatedOn=prm_createdOn,
			CreatedById=prm_createdById,
			ModifiedOn=prm_modifiedOn,
			ModifiedById=prm_modifiedById,
		    IsActive=prm_isActive						 
             where enumline.Id =id and enumline.ClientId=clientId;
   end if;
IF DBoperation = 'Delete'
    then
            delete from CTL_enumline
			where
			enumline.Id=id and enumline.ClientId=clientId; 
        END if;
IF DBoperation = 'Activate'
        then
            update CTL_enumline set IsActive=1
			where 
			enumline.Id=id and enumline.ClientId=clientId;
               END if;
IF DBoperation = 'DeActivate'
        then
            update CTL_enumline set IsActive=0
			where
			enumline.Id=id and enumline.ClientId=clientId;
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_SettingsType
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_SettingsType`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_SettingsType`(in prm_id int,
	    in prm_clientId int,
        in prm_moduleId int,
        in prm_parentId int,
		in prm_keyCode varchar(2000),
        in prm_name varchar(500),
        in prm_description varchar(5000),
        in prm_isSystemDefined bit,
        in prm_isRequired bit,
		in prm_istAccountLevel bit,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
    
           insert into enums (Id,ClientId, ModuleId,ParentId,KeyCode,Name,Description,IsSystemDefined,
           IsRequired,IstAccountLevel, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId, prm_moduleId,prm_parentId,prm_keyCode,prm_name,prm_description,prm_isSystemDefined,
           prm_isRequired,prm_istAccountLevel, prm_createdOn, prm_createdById,
           prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update CTL_enums set 
                          ClientId = prm_clientId,
                          ModuleId=prm_moduleId,
                          ParentId=prm_parentId,
                          KeyCode=prm_keyCode,
                          Name=prm_name,
                          Description=prm_description,
						  IsSystemDefined=prm_isSystemDefined,
                          IsRequired=prm_isRequired,
						  IstAccountLevel=prm_istAccountLevel,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where enums.Id =id and enums.ClientId=clientId;
             if(isActive=false)
             then
              update enumline set enumline.IsActive=0 where enumline.ParentId=id;
		      update ptextrafieldsdata  set ptextrafieldsdata.IsActive=0 
              where  ptextrafieldsdata.FieldId=id and ptextrafieldsdata.ClientId=clientId;
              update rxmedextrafieldsdata  set rxmedextrafieldsdata.IsActive=0 
              where  rxmedextrafieldsdata.FieldId=id and rxmedextrafieldsdata.ClientId=clientId;
            end if;
			if (isActive=true)
            then
              update ptextrafieldsdata  set ptextrafieldsdata.IsActive=1 where 
              ptextrafieldsdata.FieldId=id  and ptextrafieldsdata.ClientId=clientId;
              update rxmedextrafieldsdata  set rxmedextrafieldsdata.IsActive=1 where 
              rxmedextrafieldsdata.FieldId=id and rxmedextrafieldsdata.ClientId=clientId;
     end if;
   end if;
   IF DBoperation = 'Delete'
    then
     delete from CTL_enumline
			where
			enumline.EnumTypeId=id and enumline.ClientId=clientId; 
            delete from CTL_enums
			where
			enums.Id=id and enums.ClientId=clientId; 
        END if;
IF DBoperation = 'Activate'
        then
        update CTL_enumline set IsActive=1
			where
			enumline.EnumTypeId=id and enumline.ClientId=clientId; 
            update CTL_enums set IsActive=1
			where 
			enums.Id=id and enums.ClientId=clientId;
            
        END if;
IF DBoperation = 'DeActivate'
        then
           update CTL_enumline set IsActive=0
			where
			enumline.EnumTypeId=id and enumline.ClientId=clientId; 
            update CTL_enums set IsActive=0
			where
			enums.Id=id and enums.ClientId=clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_UOMConversion
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_UOMConversion`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_UOMConversion`(in prm_id int,
       in prm_ClientId int,
       in prm_uOMId int,
        in prm_convertedUOMId int,
        in prm_isBaseUnit bit,
        in prm_qty int,
        in prm_multiplier  double,
        in prm_displayUOM text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then    
           insert into CTL_uomconversion (Id,ClientId,UOMId,ConvertedUOMId,IsBaseUnit,Qty,Multiplier,
           DisplayUOM,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_uOMId,prm_convertedUOMId,prm_isBaseUnit,prm_qty,prm_multiplier,
           prm_displayUOM,prm_createdOn,prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update CTL_uomconversion set
						ClientId = prm_clientId,
                          UOMId=prm_uOMId,
                          ConvertedUOMId=prm_convertedUOMId,
                          IsBaseUnit=prm_isBaseUnit,
						  Qty=prm_qty,
                          Multiplier=prm_multiplier,
                          DisplayUOM=prm_displayUOM,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where uomconversion.Id =id;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from CTL_uomconversion
			where
			uomconversion.Id=id ; 
        END if;
IF DBoperation = 'Activate'
        then
            update CTL_uomconversion set IsActive=1
			where 
			uomconversion.Id=id ;
        END if;
IF DBoperation = 'DeActivate'
        then
            update CTL_uomconversion set IsActive=0
			where
			uomconversion.Id=id;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_Clients
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_Clients`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_Clients`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`vw_client`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_Customer
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_Customer`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_Customer`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`vw_CTL_customer`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_Item
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_Item`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_Item`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`vw_CTL_item`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_LogEvent
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_LogEvent`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_LogEvent`(in whereClause varchar(5000))
BEGIN
     set @querystr ='SELECT * 
			FROM
			vw_CTL_logevent  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_Settings
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_Settings`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_Settings`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`vw_CTL_settings`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_SettingsType
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_SettingsType`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_SettingsType`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`vw_CTL_settingstype`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_SMTPCredentials
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_SMTPCredentials`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_SMTPCredentials`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`vw_CTL_smtpcredentials`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_UOMConversion
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_UOMConversion`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_UOMConversion`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`vw_CTL_uomconversion`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for KAS_Manage_Voucher
-- ----------------------------
DROP PROCEDURE IF EXISTS `KAS_Manage_Voucher`;
delimiter ;;
CREATE PROCEDURE `KAS_Manage_Voucher`(in prm_id int,
        in prm_clientId int,
        in prm_vchTypeId int,
        in prm_vendorId int,
		in prm_salesmanId VARCHAR(255),
        in prm_godownId int,
        in prm_approvedById int,
        in prm_statusId int,
        in prm_vchDate datetime,
        in prm_vchNo text,
        in prm_invNo text,
        in prm_docNo text,
        in prm_docDate datetime,
        in prm_description longtext,
        in prm_isPosted bit,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into KAS_voucher (Id,ClientId,VchTypeId,VendorId,SalesmanId,GodownId,ApprovedById,StatusId,
           VchDate,VchNo,InvNo, DocNo,DocDate,Description, IsPosted,
           CreatedOn, CreatedById,ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_vchTypeId,prm_vendorId,prm_salesmanId,prm_godownId,prm_approvedById,prm_statusId,prm_vchDate,prm_vchNo,invNo,
           prm_docNo,prm_docDate ,prm_description, prm_isPosted,prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive);
 end if;   
/*update*/
if DBoperation ='Update'
 then
            update KAS_voucher set 
                          ClientId=prm_clientId,
                          VchTypeId=prm_vchTypeId,
                          VendorId=prm_vendorId,
                          SalesmanId=prm_salesmanId,
                          GodownId=prm_godownId,
                          ApprovedById=prm_approvedById,
                          StatusId=prm_statusId,
                          VchDate=prm_vchDate,
						  VchNo=prm_vchNo,
                          InvNo=prm_invNo,
                          DocNo=prm_docNo,
                          DocDate=prm_docDate,
                          Description=prm_description,
					      IsPosted=prm_isPosted,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where voucher.Id =id and voucher.ClientId=clientId;
   end if;
IF DBoperation = 'Delete'
    then
             delete from voucherdetail where  voucherdetail.VchId=id
              and voucherdetail.ClientId=clientId;
             delete from KAS_voucher
			 where
			 voucher.Id=id  and voucher.ClientId=clientId; 
        END if;
IF DBoperation = 'Activate'
        then
            update voucherdetail set voucherdetail.IsActive=1
			where
			voucherdetail.VchId=id  and voucherdetail.ClientId=clientId;
            update KAS_voucher set IsActive=1
			where 
			voucher.Id=id  and voucher.ClientId=clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update voucherdetail set voucherdetail.IsActive=0
			where
			voucherdetail.VchId=id  and voucherdetail.ClientId=clientId;
            update KAS_voucher set IsActive=0
			where
			voucher.Id=id  and voucher.ClientId=clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for KAS_Manage_Voucherdetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `KAS_Manage_Voucherdetail`;
delimiter ;;
CREATE PROCEDURE `KAS_Manage_Voucherdetail`(in prm_id int,
		in prm_clientId int,
        in prm_vchId int,
        in prm_acId int,
        in prm_billId int,
		in prm_productId int,
        in prm_debit double,
        in prm_credit double ,
        in prm_description text,
        in prm_qty int,
        in prm_rate int,
        in prm_isDefaultDrCr bit,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then 
           insert into KAS_voucherdetail ( Id,ClientId,VchId,AcId,ProductId,BillId,Debit,Credit,Description,Qty,Rate,CreatedOn ,
           IsDefaultDrCr,CreatedById , ModifiedOn,  ModifiedById,  IsActive) 
           values (prm_id,prm_clientId,prm_VchId,prm_acId,prm_productId,prm_billId,prm_debit,prm_credit,prm_description,prm_qty,prm_rate,prm_createdOn ,
           prm_isDefaultDrCr,prm_createdById , prm_modifiedOn,  prm_modifiedById,  prm_isActive);
 end if;   
/*update*/
if DBoperation ='Update'
 then
            update KAS_voucherdetail set 
						  ClientId=prm_clientId,
                          AcId = prm_acId,  
                          ProductId=prm_productId,
                          BillId=prm_billId,
                          Debit=prm_debit,
                          Credit=prm_credit,
						  Description=prm_description,
                          Qty=prm_qty,
                          Rate=prm_rate,
                          IsDefaultDrCr=prm_isDefaultDrCr,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where voucherdetail.Id =id and voucherdetail.VchId=vchId
              and voucherdetail.ClientId=clientId;
   end if;
IF DBoperation = 'Delete'
    then
            delete from KAS_voucherdetail
			 where voucherdetail.Id =id and voucherdetail.VchId=vchId
             and voucherdetail.ClientId=clientId;
        END if;
IF DBoperation = 'Activate'
        then
            update KAS_voucherdetail set IsActive=1
			 where voucherdetail.Id =id and voucherdetail.VchId=vchId
             and voucherdetail.ClientId=clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update KAS_voucherdetail set IsActive=0
			 where voucherdetail.Id =id and voucherdetail.VchId=vchId
             and voucherdetail.ClientId=clientId;         
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for KAS_Search_Voucher
-- ----------------------------
DROP PROCEDURE IF EXISTS `KAS_Search_Voucher`;
delimiter ;;
CREATE PROCEDURE `KAS_Search_Voucher`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`KAS_vw_voucher`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for KAS_Search_Voucherdetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `KAS_Search_Voucherdetail`;
delimiter ;;
CREATE PROCEDURE `KAS_Search_Voucherdetail`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`KAS_vw_vchdetail`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for NTF_Manage_NotificationLog
-- ----------------------------
DROP PROCEDURE IF EXISTS `NTF_Manage_NotificationLog`;
delimiter ;;
CREATE PROCEDURE `NTF_Manage_NotificationLog`(in prm_id int,
        in prm_clientId int,
        in prm_phone varchar(45),
        in prm_userId varchar(255),
        in prm_sMS varchar(600),
        in prm_dateTime datetime,
        in prm_isSent bit,
        in prm_createdOn datetime,
        in prm_createdById int,
        in modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_filter varchar(50))
BEGIN
if filter = 'Insert'
then
    
           insert into NTF_notification_log 
					 (Id
					 , ClientId
					 , Phone
					 , UserId
					 , SMS
					 , DateTime
					 , IsSent
					 , CreatedOn
					 , CreatedById
					 , ModifiedOn
					 , ModifiedById
					 , IsActive 
					 ) 
           values 
					 (prm_id
					 , prm_clientId
					 , prm_phone
					 , prm_userId
					 , prm_sMS
					 , prm_dateTime
					 , prm_isSent
					 , prm_createdOn
					 , prm_createdById
					 , prm_modifiedOn
					 , prm_modifiedById
					 , prm_isActive 
					 );
 end if;   
/*update*/
 if filter ='Update'
 then
            update NTF_notification_log set 
             ClientId=prm_clientId
						 , Phone=prm_phone
						 , UserId=prm_userId
						 , SMS=prm_sMS
						 , DateTime=prm_datetime
						 , IsSent=prm_isSent
						 , CreatedOn=prm_createdOn
						 , CreatedById=prm_createdById
						 , ModifiedOn=prm_modifiedOn
						 , ModifiedById=prm_modifiedById
						 , IsActive=prm_isActive						 
             where NTF_notification_log.Id =prm_id;
   end if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for NTF_Search_NotificationLog
-- ----------------------------
DROP PROCEDURE IF EXISTS `NTF_Search_NotificationLog`;
delimiter ;;
CREATE PROCEDURE `NTF_Search_NotificationLog`(in whereClause varchar(5000))
BEGIN
     set @querystr ='SELECT * 
			FROM
			NTF_NotificationLog  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for NTF_Search_NotificationTemplate
-- ----------------------------
DROP PROCEDURE IF EXISTS `NTF_Search_NotificationTemplate`;
delimiter ;;
CREATE PROCEDURE `NTF_Search_NotificationTemplate`(in whereClause varchar(5000))
BEGIN
     set @querystr ='SELECT * 
			FROM
			NTF_NotificationTemplate ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_Appointment
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_Appointment`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_Appointment`(in prm_id int,
    in prm_clientId int,
    in prm_tokenNo BIGINT,
    in prm_patientid int,
    in prm_doctorId int,
    in prm_statusId int,
	in prm_date date,
    in prm_time text,
    in prm_age text,
    in prm_genderId int,
	in prm_createdOn datetime,
	in prm_createdById int,
	in prm_modifiedOn datetime,
	in prm_modifiedById int,
	in prm_isActive bool,
	in DbOperation varchar(200))
BEGIN

if DbOperation='Insert'
then 
	 insert into PMS_appointment (Id,ClientId, PatientId,DoctorId,StatusId,TokenNo, Date, Time, Age, GenderId, CreatedOn, CreatedById, ModifiedOn,ModifiedById,IsActive ) 
	 values(prm_id,prm_clientId, prm_patientid,prm_doctorId,prm_statusId,prm_tokenNo, prm_date, prm_time, prm_age, prm_genderId,  prm_createdOn,prm_createdById,prm_modifiedOn,prm_modifiedById,prm_isActive );
end If;

if DbOperation='Update'
then 
update PMS_appointment set 
	ClientId=prm_clientId,
    PatientId=prm_patientid,
    DoctorId=prm_doctorId,
    StatusId=prm_statusId,
    TokenNo=prm_tokenNo,
	 Date=prm_date,
	 Time=prm_time,
     Age=prm_age,
     GenderId=prm_genderId,
	 CreatedOn=prm_createdOn,
	 CreatedById=prm_createdById,
	 ModifiedOn=prm_modifiedOn,
	 ModifiedById=prm_modifiedById,
	 IsActive=prm_isActive
 where appointment.Id= id and appointment.ClientId= clientId;
end If;

if DbOperation='Delete'
then
	DELETE FROM PMS_appointment where appointment.Id=id and appointment.ClientId= clientId;
end if;


if DbOperation='Activate'
then 
	update PMS_appointment set IsActive=1 where appointment.Id=id and appointment.ClientId= clientId;
end iF;


if DbOperation='DeActivate'
then 
	update PMS_appointment set IsActive=0 where appointment.Id=id and appointment.ClientId= clientId;
end iF;
End
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_Doctor
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_Doctor`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_Doctor`(in prm_id int,
        in prm_clientId int,
        in prm_userId varchar(255),
        in prm_doctorName varchar(100),
		in prm_dateOfBirth date,
		in prm_genderId int,
		in prm_countryId int,
		in prm_cityId int,
        in prm_areaId int,
        in prm_startTime text,
        in prm_defApptDur int,
		in prm_contactNo VARCHAR(45),
		in prm_houseNo text,
        in prm_address text,
        in prm_specialization text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into   PMS_doctor (Id,ClientId,UserId,DoctorName,GenderId,
           CountryId,CityId,AreaId,DateOfBirth,ContactNo,HouseNo,DefApptDur, 
           Address, Specialization, StartTime,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId, prm_userId,prm_doctorName, prm_genderId,prm_countryId,
           prm_cityId,prm_areaId,prm_dateOfBirth, prm_contactNo, prm_houseNo,prm_defApptDur, prm_address, 
           prm_specialization, prm_startTime,prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update   PMS_doctor set 
            UserId=userId,
            ClientId=prm_clientId,
                          DoctorName=prm_doctorName,
                          GenderId=prm_genderId,
						  DateofBirth=prm_dateofBirth,
                          CountryId=prm_countryId,
						  CityId=prm_cityId,
                          AreaId=prm_areaId,
                          StartTime=prm_startTime,
                          ContactNo=prm_contactNo,
                          DefApptDur=prm_defApptDur,
                          DateOfBirth=prm_dateOfBirth,
                          HouseNo=prm_houseNo,
                          Address=prm_address,
                          Specialization=prm_specialization,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where   doctor.Id =id and doctor.ClientId=clientId;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from   PMS_doctor
			where
			  doctor.Id=id and doctor.ClientId=clientId; 
        END if;
IF DBoperation = 'Activate'
        then
            update   PMS_doctor set IsActive=1
			where 
			  doctor.Id=id and doctor.ClientId=clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update   PMS_doctor set IsActive=0
			where
			  doctor.Id=id and doctor.ClientId=clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_Patient
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_Patient`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_Patient`(in prm_id int,
        in prm_clientId int,
        in prm_patientName varchar(100),
        in prm_dateofBirth datetime,
		in prm_genderId int,
         in prm_countryId int,
        in prm_cityId int,
        in prm_areaId int,
        in prm_email text,
		in prm_contactNo VARCHAR(45),
		in prm_houseNo text,
        in prm_address text,
        in prm_remarks text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into  PMS_Patient (Id,ClientId,PatientName,DateofBirth,
           GenderId,CountryId,CityId,AreaId,Email,
           ContactNo,HouseNo,Address,Remarks, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_patientName,prm_dateofBirth,prm_genderId,prm_countryId,prm_cityId,prm_areaId,prm_email,
           prm_contactNo,prm_houseNo,prm_address,prm_remarks, prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update  PMS_Patient set 
                         ClientId=prm_clientId,
                         Email=prm_email,
                         PatientName = prm_patientName,
                          DateofBirth=prm_dateofBirth,
                          GenderId=prm_genderId,
                          CountryId=prm_countryId,
                          CityId=prm_cityId,
                          AreaId=prm_areaId,
                          ContactNo=prm_contactNo,
                          HouseNo=prm_houseNo,
                          Address=prm_address,
                          Remarks=prm_remarks,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where   Patient.Id =id and 
            Patient.ClientId=clientId;
   end if;
   IF DBoperation = 'Delete'
    then
		delete from ptextrafieldsdata
			where
			 ptextrafieldsdata.PatientId=id and 
             ptextrafieldsdata.ClientId=clientId; 
		delete from PMS_Patient
			where
			 Patient.Id=id  and 
            Patient.ClientId=clientId; 
        END if;
IF DBoperation = 'Activate'
        then
		update ptextrafieldsdata set IsActive=1
			where 
			 ptextrafieldsdata.PatientId=id and 
            ptextrafieldsdata.ClientId=clientId; 
		update PMS_Patient set IsActive=1
			where 
			 Patient.Id=id and 
             Patient.ClientId=clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
		update ptextrafieldsdata set IsActive=0
			where 
			 ptextrafieldsdata.PatientId=id and 
            ptextrafieldsdata.ClientId=clientId; 
		update  PMS_Patient set IsActive=0
			where
			 Patient.Id=id and 
             Patient.ClientId=clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_PatientReport
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_PatientReport`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_PatientReport`(in prm_id int,
        in prm_clientId int,
		in prm_rxId int,
        in prm_date datetime,
		in prm_categoryId int,
        in prm_name text,
        in prm_reportBase64Path longtext,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into PMS_patientreport (Id,ClientId,RxId,Date,CategoryId,Name,ReportBase64Path,
           CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_rxId,prm_date,prm_categoryId,prm_name,prm_reportBase64Path,
           prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update PMS_patientreport set 
                          ClientId=prm_clientId,
                          Date=prm_date,
                          CategoryId=prm_categoryId,
                          Name=prm_name,
                          ReportBase64Path=prm_reportBase64Path,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where patientreport.Id =id and  
             patientreport.RxId=rxId  and  
             patientreport.ClientId=clientId;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from PMS_patientreport
			where
			patientreport.Id=id and  
            patientreport.RxId=rxId  and  
			patientreport.ClientId=clientId; 
        END if;
IF DBoperation = 'Activate'
        then
            update PMS_patientreport set IsActive=1
			where 
			patientreport.Id=id and  
            patientreport.RxId=rxId  and  
			patientreport.ClientId=clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update PMS_patientreport set IsActive=0
			where
			patientreport.Id=id and  
            patientreport.RxId=rxId  and  
			patientreport.ClientId=clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_Prescription
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_Prescription`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_Prescription`(in prm_id int,
		in prm_clientId int,
        in prm_doctorId int,
        in prm_patientId int,
		in prm_date datetime,
        in prm_time text,
        in prm_tokenNo int,
		in prm_amount float,
        in prm_remarks text,
		in prm_medDetRemarks text,
		in prm_temperature text,
		in prm_bPStatusId int, 
        in prm_nextVisitNo int,
        in prm_weight text,
        in prm_isSugarPatient bit,
        in prm_precautionIds text,
        in prm_nextVisitDate datetime,
        in prm_comments text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
    
           insert into PMS_prescription (Id,ClientId,DoctorId,PatientId,Date,Time,
           TokenNo,Amount,Remarks,
           MedDetRemarks,Temperature,BPStatusId,Weight,IsSugarPatient,PrecautionIds,
           NextVisitDate,NextVisitNo,Comments,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_doctorId,prm_patientId,prm_date,prm_time,prm_tokenNo,prm_amount,
           prm_remarks,prm_medDetRemarks,
           prm_temperature,prm_bPStatusId,prm_weight,prm_isSugarPatient,prm_precautionIds,
           prm_nextVisitDate,prm_nextVisitNo,prm_comments,prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update PMS_prescription set 
                          ClientId=prm_clientId,
                          DoctorId=prm_doctorId,
                          PatientId=prm_patientId,
                          Date=prm_date,
                          Time=prm_time,
                          TokenNo=prm_tokenNo,
                          NextVisitNo=prm_nextVisitNo,
                          Amount=prm_amount,
                          Remarks=prm_remarks,
                          MedDetRemarks=prm_medDetRemarks,
                          Temperature=prm_temperature,
                          bPStatusId=prm_BPStatusId,
                          Weight=prm_weight,
                          IsSugarPatient=prm_isSugarPatient,
                          PrecautionIds=prm_precautionIds,
                          NextVisitDate=prm_nextVisitDate,
                          Comments=prm_comments,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where prescription.Id =id and prescription.ClientId=clientId;
   end if;
   IF DBoperation = 'Delete'
    then
     delete from rxmedextrafieldsdata
			where
			rxmedextrafieldsdata.rxId=id and 
             rxmedextrafieldsdata.ClientId=clientId; 
            delete from PMS_prescription
			where
			prescription.Id=id and prescription.ClientId=clientId; 
        END if;
IF DBoperation = 'Activate'
        then
         update rxmedextrafieldsdata set IsActive=1
			where
			rxmedextrafieldsdata.rxId=id and 
             rxmedextrafieldsdata.ClientId=clientId; 
            update PMS_prescription set IsActive=1
			where 
			prescription.Id=id and prescription.ClientId=clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
          update rxmedextrafieldsdata set IsActive=0
			where
			rxmedextrafieldsdata.rxId=id and 
             rxmedextrafieldsdata.ClientId=clientId;
            update PMS_prescription set IsActive=0
			where
			prescription.Id=id and prescription.ClientId=clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_PtExtraFieldsData
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_PtExtraFieldsData`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_PtExtraFieldsData`(in prm_clientId int,
        in prm_patientId int,
        in prm_fieldId int,
        in prm_fieldValue longtext,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into PMS_ptextrafieldsdata (PatientId,ClientId,FieldId,FieldValue,
           CreatedOn, CreatedById,ModifiedOn, ModifiedById, IsActive ) 
           values (prm_patientId,prm_clientId,prm_fieldId,prm_fieldValue,
           prm_createdOn, prm_createdById, prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update PMS_ptextrafieldsdata set 
						 ClientId=prm_clientId,
                          FieldValue=prm_fieldValue,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where ptextrafieldsdata.FieldId =prm_fieldId 
             and ptextrafieldsdata.PatientId=prm_patientId
			 and ptextrafieldsdata.ClientId=prm_clientId;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from PMS_ptextrafieldsdata
			where 
            ptextrafieldsdata.FieldId =prm_fieldId and 
            ptextrafieldsdata.PatientId=prm_patientId and 
            ptextrafieldsdata.ClientId=prm_clientId;
        END if;
IF DBoperation = 'Activate'
        then
            update prm_ptextrafieldsdata set IsActive=1
			where ptextrafieldsdata.FieldId =prm_fieldId and 
            ptextrafieldsdata.PatientId=prm_patientId and 
            ptextrafieldsdata.ClientId=prm_clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update PMS_ptextrafieldsdata set IsActive=0
			where ptextrafieldsdata.FieldId =prm_fieldId and 
            ptextrafieldsdata.PatientId=prm_patientId and 
            ptextrafieldsdata.ClientId=prm_clientId;           
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_RxMedicine
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_RxMedicine`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_RxMedicine`(in prm_id int,
		in prm_clientId int,
        in prm_rxId int,
        in prm_medId int,
		in prm_mRId int,
        in prm_aMQty int,
        in prm_noonQty int,
		in prm_eveQty int,
        in prm_days int,
		in prm_remarksId int,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
    
           insert into PMS_rxmedicine (Id,ClientId,RxId,MedId,MRId,AMQty,NoonQty,EveQty,Days,
           RemarksId, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_rxId,prm_medId,prm_mRId,prm_aMQty,prm_noonQty,prm_eveQty,prm_days,prm_remarksId,
           prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update PMS_rxmedicine set 
                          ClientId=prm_clientId,
                          MedId=prm_medId,
                          MRId=prm_mRId,
                          AMQty=prm_aMQty,
                          NoonQty=prm_noonQty,
                          EveQty=prm_eveQty,
                          Days=prm_days,
                          RemarksId=prm_remarksId,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where rxmedicine.Id =prm_id and  
			 rxmedicine.RxId=prm_rxId and
             rxmedicine.ClientId=prm_clientId;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from PMS_rxmedicine
			where
			PMS_rxmedicine.Id=prm_id and  
            PMS_rxmedicine.RxId=prm_rxId and
			PMS_rxmedicine.ClientId=prm_clientId;
        END if;
IF DBoperation = 'Activate'
        then
            update PMS_rxmedicine set IsActive=1
			where 
			PMS_rxmedicine.Id=prm_id and  
            PMS_rxmedicine.RxId=prm_rxId and
			PMS_rxmedicine.ClientId=prm_clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update PMS_rxmedicine set IsActive=0
			where
			PMS_rxmedicine.Id=prm_id and  
            PMS_rxmedicine.RxId=prm_rxId and
			PMS_rxmedicine.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_Staff
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_Staff`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_Staff`(in prm_id int,
        in prm_clientId int,
        in prm_userId varchar(255),
        in prm_name varchar(100),
		in prm_dateOfBirth date,
		in prm_genderId int,
         in prm_countryId int,
		in prm_cityId int,
        in prm_areaId int,
		in prm_contactNo VARCHAR(45),
		in prm_houseNo text,
        in prm_address text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into   PMS_staff (Id,ClientId,UserId,Name,GenderId,CountryId,
           CityId,AreaId,DateOfBirth,ContactNo,HouseNo, Address, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId, prm_userId,prm_name, prm_genderId,prm_countryId,
           prm_cityId,prm_areaId,prm_dateOfBirth, prm_contactNo, prm_houseNo, prm_address, prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update   PMS_staff set 
						  ClientId=prm_clientId,
                          UserId=prm_userId,
                          Name=prm_name,
                          GenderId=prm_genderId,
						  DateofBirth=prm_dateofBirth,
                          CountryId=prm_countryId,
						  CityId=prm_cityId,
                          AreaId=prm_areaId,
                          ContactNo=prm_contactNo,
                          DateOfBirth=prm_dateOfBirth,
                          HouseNo=prm_houseNo,
                          Address=prm_address,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where   PMS_staff.Id =prm_id and PMS_staff.ClientId=prm_clientId;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from   PMS_staff
			where
			  PMS_staff.Id=prm_id and PMS_staff.ClientId=prm_clientId; 
        END if;
IF DBoperation = 'Activate'
        then
            update   PMS_staff set IsActive=1
			where 
			  PMS_staff.Id=prm_id and PMS_staff.ClientId=prm_clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update   PMS_staff set IsActive=0
			where
			  PMS_staff.Id=prm_id and PMS_staff.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_SearchRxMedicine
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_SearchRxMedicine`;
delimiter ;;
CREATE PROCEDURE `PMS_SearchRxMedicine`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`PMS_vw_rxmedicine`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_Appointment
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_Appointment`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_Appointment`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`PMS_vw_appointment`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_Doctor
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_Doctor`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_Doctor`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`PMS_vw_doctor`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_Patient
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_Patient`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_Patient`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * FROM PMS_vw_patient ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_PatientReport
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_PatientReport`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_PatientReport`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`PMS_vw_patreport`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_Prescription
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_Prescription`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_Prescription`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`PMS_vw_rx`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_PtExtraFieldsData
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_PtExtraFieldsData`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_PtExtraFieldsData`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`PMS_vw_ptextrafieldsdata`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_RxMedExtraFieldsData
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_RxMedExtraFieldsData`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_RxMedExtraFieldsData`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`PMS_vw_rxmedextrafieldsdata`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_RxMedicine
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_RxMedicine`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_RxMedicine`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`PMS_vw_rxmedicine`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Search_Staff
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_Staff`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_Staff`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`PMS_vw_staff`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_DC
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_DC`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_DC`(in prm_id int,
        in prm_clientId int,
		in prm_acId int,
		in prm_custId int,
        in prm_invNo text,
        in prm_date datetime,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into SAL_dc (Id,ClientId,AcId,CustId,Date,InvNo, CreatedOn, CreatedById, ModifiedOn, ModifiedById, IsActive ) 
           values 
           (prm_id,prm_clientId,prm_acId,prm_custId,prm_date,prm_invNo, prm_createdOn, prm_createdById, prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
 if DBoperation ='Update'
       then     
          update PMS_dc set  
                         ClientId=prm_clientId,
                         AcId=prm_acId,
                          CustId=prm_custId,
						  Date=prm_date,
                          InvNo=prm_invNo,
                          CreatedOn = prm_createdOn,
                          CreatedById =prm_createdById,
                          ModifiedOn = prm_modifiedOn,
                          ModifiedById = prm_modifiedById ,
						  IsActive = prm_isActive 				 
             where SAL_dc.Id =prm_id;
      end if;
IF DBoperation = 'Delete'
       then
			delete from SAL_dcdetail
			where
			SAL_dcdetail.DCId=prm_id ; 
            delete from SAL_dc
			where
			SAL_dc.Id=prm_id ; 
        END if;
IF DBoperation = 'Activate'
        then
            update SAL_dc set IsActive=1
			where 
			SAL_dc.Id=prm_id ;
        END if;
IF DBoperation = 'DeActivate'
        then
            update SAL_dc set IsActive=0
			where
			SAL_dc.Id=prm_id; 
             update SAL_dcdetail set IsActive=0
			where
			SAL_dcdetail.DCId=prm_id ; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_DCDetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_DCDetail`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_DCDetail`(in prm_id int,
        in prm_clientId int,
        in prm_dCId int,
		in prm_productId int,
		in prm_qty int,
		in prm_description text,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into SAL_dcdetail (Id,DCId,ProductId,Qty,Description, CreatedOn, CreatedById, ModifiedOn, ModifiedById, IsActive ) 
           values 
           (prm_id,prm_dCId,prm_productId,prm_qty,prm_description, prm_createdOn, prm_createdById, prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
 if DBoperation ='Update'
       then     
          update SAL_dcdetail set  
						 ClietId=prm_clientId,
                          ProductId=prm_productId,
                          Qty=prm_qty,
                          Description=prm_description,
                          CreatedOn = prm_createdOn,
                          CreatedById =prm_createdById,
                          ModifiedOn = prm_modifiedOn,
                          ModifiedById = prm_modifiedById ,
						  IsActive = prm_isActive 				 
             where SAL_dcdetail.Id =prm_id and SAL_dcdetail.DCId=prm_dCId;
      end if;
IF DBoperation = 'Delete'
       then
            delete from SAL_dcdetail
			 where SAL_dcdetail.Id =prm_id and SAL_dcdetail.DCId=prm_dCId;
        END if;
IF DBoperation = 'Activate'
        then
            update SAL_dcdetail set IsActive=1
			 where SAL_dcdetail.Id =prm_id and SAL_dcdetail.DCId=prm_dCId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update SAL_dcdetail set IsActive=0
			 where SAL_dcdetail.Id =prm_id and SAL_dcdetail.DCId=prm_dCId;
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_DocumentExtras
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_DocumentExtras`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_DocumentExtras`(in prm_id int,
        in prm_clientId int,
        in prm_docExtraTypeId int,
        in prm_docExtraId int,
        in prm_incDecTypeId int,
	    in prm_formulaId int,
        in prm_value double,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then    
           insert into SAL_documentExtras (Id,ClientId,DocExtraTypeId,DocExtraId,IncDecTypeId,Value,FormulaId,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_docExtraTypeId,prm_docExtraId,prm_incDecTypeId,prm_value,prm_formulaId,prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive);
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update SAL_documentExtras set 
                          ClientId=prm_clientId,
                          docExtraTypeId=prm_DocExtraTypeId,
                          docExtraId=prm_DocExtraId,
                          incDecTypeId=prm_IncDecTypeId,
                          value=prm_Value,
                          formulaId=prm_FormulaId,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where SAL_documentExtras.Id =prm_id;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from SAL_documentExtras
			where
			SAL_documentExtras.Id=prm_id ; 
        END if;
IF DBoperation = 'Activate'
        then
            update SAL_documentExtras set IsActive=1
			where 
			SAL_documentExtras.Id=prm_id ;
        END if;
IF DBoperation = 'DeActivate'
        then
            update SAL_documentExtras set IsActive=0
			where
			SAL_documentExtras.Id=prm_id;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_ItemUOM
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_ItemUOM`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_ItemUOM`(in prm_id int,
        in prm_clientId int,
        in prm_itemId int,
        in prm_uOMId int,
        in prm_uOMTypeId int,
        in prm_salePrice double,
        in prm_purPrice double,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then    
           insert into SAL_itemuom (Id,ClientId,ItemId,UOMId,UOMTypeId,SalePrice,PurPrice,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_itemId,prm_uOMId,prm_uOMTypeId,prm_salePrice,prm_purPrice,prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if DBoperation ='Update'
 then
            update SAL_itemuom set 
                          ClientId=prm_clientId,
                          itemId=prm_itemId,
                          UOMId=prm_uOMId,
                          UOMTypeId=prm_uOMTypeId,
                          SalePrice=prm_salePrice,
                          PurPrice=prm_purPrice,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where SAL_itemuom.Id =prm_id and SAL_itemuom.ClientId =prm_clientId;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from SAL_itemuom
			where
			SAL_itemuom.Id=prm_id and SAL_itemuom.ClientId =prm_clientId; 
        END if;
IF DBoperation = 'Activate'
        then
            update SAL_itemuom set IsActive=1
			where 
			SAL_itemuom.Id=prm_id and SAL_itemuom.ClientId =prm_clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update SAL_itemuom set IsActive=0
			where
			SAL_itemuom.Id=prm_id and SAL_itemuom.ClientId =prm_clientId;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_ItemVariants
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_ItemVariants`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_ItemVariants`(in prm_id int,
        in prm_clientId int,
        in prm_itemId int,
        in prm_attributeValuesIds text,
        in prm_purchaseExtraRate double,
         in prm_saleExtraRate double,
        in prm_barCode text,
         in prm_stockValue double,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if DBoperation = 'Insert'
then
           insert into SAL_itemvariants (Id,ClientId,ItemId,AttributeValuesIds,PurchaseExtraRate,SaleExtraRate,
           BarCode,StockValue,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_itemId,prm_attributeValuesIds,prm_purchaseExtraRate,prm_saleExtraRate,prm_barCode,prm_stockValue,
           prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
 if DBoperation ='Update'
 then
            update SAL_itemvariants set 
                          ClientId=prm_clientId,
                          ItemId=prm_itemId,
                          AttributeValuesIds=prm_attributeValuesIds,
                          PurchaseExtraRate=prm_purchaseExtraRate,
                          SaleExtraRate=prm_saleExtraRate,
                          BarCode=prm_barCode,
                          StockValue=prm_stockValue,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where SAL_itemvariants.Id =prm_id and SAL_itemvariants.ClientId =prm_clientId;
   end if;
   IF DBoperation = 'Delete'
    then
            delete from SAL_itemvariants
			where
			SAL_itemvariants.Id=prm_id and SAL_itemvariants.ClientId =prm_clientId; 
        END if;
IF DBoperation = 'Activate'
        then
            update SAL_itemvariants set IsActive=1
			where 
			SAL_itemvariants.Id=prm_id and SAL_itemvariants.ClientId =prm_clientId;
        END if;
IF DBoperation = 'DeActivate'
        then
            update SAL_itemvariants set IsActive=0
			where
			SAL_itemvariants.Id=prm_id and SAL_itemvariants.ClientId =prm_clientId; 
           
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Search_DC
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_DC`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_DC`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`SAL_vw_dc`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Search_DCDetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_DCDetail`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_DCDetail`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`SAL_vw_dcdetail`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Search_DocumentExtras
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_DocumentExtras`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_DocumentExtras`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`SAL_vw_documentextras`  ';
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Search_ItemUOM
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_ItemUOM`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_ItemUOM`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`SAL_vw_itemuom`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Search_ItemVariants
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_ItemVariants`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_ItemVariants`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`microerp`.`SAL_vw_itemvariants`';
           
    set @querystr= concat(@querystr,whereClause);
	PREPARE stmt1 
	FROM
		@querystr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
