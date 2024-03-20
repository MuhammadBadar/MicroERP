/*
 Navicat Premium Data Transfer

 Source Server         : MySql
 Source Server Type    : MySQL
 Source Server Version : 80020 (8.0.20)
 Source Host           : localhost:3306
 Source Schema         : microerp

 Target Server Type    : MySQL
 Target Server Version : 80020 (8.0.20)
 File Encoding         : 65001

 Date: 20/03/2024 05:33:50
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for acc_voucher
-- ----------------------------
DROP TABLE IF EXISTS `acc_voucher`;
CREATE TABLE `acc_voucher`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of acc_voucher
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for acc_voucherdetail
-- ----------------------------
DROP TABLE IF EXISTS `acc_voucherdetail`;
CREATE TABLE `acc_voucherdetail`  (
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
  PRIMARY KEY (`Id`, `ClientId`, `VchId`) USING BTREE,
  INDEX `vchDetail_fbk_1_idx`(`VchId` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of acc_voucherdetail
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for acc_vouchertype
-- ----------------------------
DROP TABLE IF EXISTS `acc_vouchertype`;
CREATE TABLE `acc_vouchertype`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of acc_vouchertype
-- ----------------------------
BEGIN;
INSERT INTO `acc_vouchertype` (`Id`, `ClientId`, `Name`, `DefaultDrCrFirst`, `KeyCode`, `DefaultDrCrSecondId`, `CreatedById`, `CreatedOn`, `ModifiedById`, `ModifiedOn`, `IsActive`) VALUES (1, 0, 'Bank Receipt Voucher', b'0', 'BRV', 1004024, 0, NULL, 0, NULL, b'1'), (1, 2, 'Testing Voucher', b'1', 'TV', 1004003, 0, '2023-10-10 01:51:36', 0, '2023-10-10 01:51:36', b'1'), (1, 3, 'Bank Receipt Voucher', b'0', 'BRV', 1004003, 0, '2023-10-10 00:16:24', 0, '2023-10-10 00:16:24', b'1'), (2, 0, 'Cash Receipt Voucher', b'0', 'CRV', 1004025, 0, NULL, 0, NULL, b'1'), (2, 2, 'Bank Receipt Voucher', b'0', 'BRV', 1004005, 0, '2023-10-10 01:54:43', 0, '2023-10-10 01:54:43', b'1'), (2, 3, 'Cash Receipt Voucher', b'0', 'CRV', 1004004, 0, '2023-10-10 00:32:21', 0, '2023-10-10 00:32:21', b'1'), (3, 0, 'Bank Payment Voucher', b'1', 'BPV', 1004026, 0, NULL, 0, NULL, b'1'), (4, 0, 'Cash Payment Voucher', b'1', 'CPV', 1004027, 0, NULL, 0, NULL, b'1'), (5, 0, 'Journal Voucher', NULL, 'JV', 1004028, 0, NULL, 0, NULL, b'1'), (6, 0, 'Sales Voucher', b'0', 'SALE', 0, 0, '2023-08-19 01:19:45', NULL, NULL, b'1');
COMMIT;

-- ----------------------------
-- Table structure for aspnetroleclaims
-- ----------------------------
DROP TABLE IF EXISTS `aspnetroleclaims`;
CREATE TABLE `aspnetroleclaims`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_AspNetRoleClaims_RoleId`(`RoleId` ASC) USING BTREE,
  CONSTRAINT `aspnetroleclaims_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of aspnetroleclaims
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for aspnetroles
-- ----------------------------
DROP TABLE IF EXISTS `aspnetroles`;
CREATE TABLE `aspnetroles`  (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  UNIQUE INDEX `RoleNameIndex`(`NormalizedName` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of aspnetroles
-- ----------------------------
BEGIN;
INSERT INTO `aspnetroles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES ('4b5fe73f-16be-44ad-9161-9b3f5fa6ef2b', 'Staff', 'STAFF', '4ffc7d22-a781-42d0-901b-02e5029b951c'), ('b0272db2-beb6-44c2-9a62-5143c1530e6d', 'Super Admin', 'SUPER ADMIN', '5ec84587-ca35-488a-8403-d01de2269774'), ('e89b05ac-acf0-44b2-b0aa-8110d576bdd0', 'Doctor', 'DOCTOR', 'e0b38b05-516e-4e27-8848-636e184e9533'), ('fc9871b8-9cc3-4323-be6f-69bbbcc8e6e3', 'Client Admin', 'CLIENT ADMIN', 'a719804b-ff9e-4916-89f7-5bc3db4ea8b4');
COMMIT;

-- ----------------------------
-- Table structure for aspnetuserclaims
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserclaims`;
CREATE TABLE `aspnetuserclaims`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_AspNetUserClaims_UserId`(`UserId` ASC) USING BTREE,
  CONSTRAINT `aspnetuserclaims_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of aspnetuserclaims
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for aspnetuserroles
-- ----------------------------
DROP TABLE IF EXISTS `aspnetuserroles`;
CREATE TABLE `aspnetuserroles`  (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`) USING BTREE,
  INDEX `IX_AspNetUserRoles_RoleId`(`RoleId` ASC) USING BTREE,
  CONSTRAINT `aspnetuserroles_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `aspnetuserroles_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of aspnetuserroles
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for aspnetusertokens
-- ----------------------------
DROP TABLE IF EXISTS `aspnetusertokens`;
CREATE TABLE `aspnetusertokens`  (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`UserId`, `LoginProvider`, `Name`) USING BTREE,
  CONSTRAINT `aspnetusertokens_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of aspnetusertokens
-- ----------------------------
BEGIN;
COMMIT;

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
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of att_attendance
-- ----------------------------
BEGIN;
INSERT INTO `att_attendance` (`Id`, `ClientId`, `UserId`, `SchDayId`, `DayStartTime`, `DayEndTime`, `Date`, `CreatedBy`, `CreatedOn`, `ModifiedBy`, `ModifiedOn`, `IsActive`) VALUES (1, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, '2024-03-12 14:24:01', '2024-03-12 14:25:10', '2024-03-12 14:24:01', 0, '2024-03-12 14:24:01', 0, '2024-03-12 14:25:10', b'1'), (1, 2, '85060824-9132-4191-aa91-fe3942f00969', 0, '2024-03-15 16:21:47', '2024-03-15 16:22:00', '2024-03-15 16:21:47', 0, '2024-03-15 16:21:47', 0, '2024-03-15 16:22:00', b'1'), (2, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, '2024-03-13 14:26:01', '2024-03-13 14:26:23', '2024-03-13 14:26:01', 0, '2024-03-13 14:26:01', 0, '2024-03-13 14:26:23', b'1'), (2, 2, '85060824-9132-4191-aa91-fe3942f00969', 0, '2024-03-19 14:29:44', '2024-03-19 14:39:13', '2024-03-19 14:29:44', 0, '2024-03-19 14:29:44', 0, '2024-03-19 14:39:13', b'1'), (3, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 8, '2024-03-13 14:26:54', '2024-03-13 14:27:37', '2024-03-13 14:26:54', 0, '2024-03-13 14:26:54', 0, '2024-03-13 14:27:37', b'1'), (4, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 9, '2024-03-14 14:28:45', '2024-03-18 20:26:20', '2024-03-14 14:28:45', 0, '2024-03-14 14:28:45', 0, '2024-03-18 20:26:20', b'1'), (5, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 3, '2024-03-13 14:35:33', '2024-03-13 14:36:20', '2024-03-13 14:35:33', 0, '2024-03-13 14:35:33', 0, '2024-03-13 14:36:20', b'1'), (6, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 17, '2024-03-14 16:59:47', '2024-03-14 17:04:09', '2024-03-14 16:59:47', 0, '2024-03-14 16:59:47', 0, '2024-03-14 17:04:09', b'1'), (7, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 21, '2024-03-15 17:19:53', '2024-03-15 17:21:27', '2024-03-15 17:19:53', 0, '2024-03-15 17:19:53', 0, '2024-03-15 17:21:27', b'1'), (8, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 5, '2024-03-15 17:29:19', '2024-03-18 20:20:26', '2024-03-15 17:29:19', 0, '2024-03-15 17:29:19', 0, '2024-03-18 20:20:26', b'1'), (9, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 7, '2024-03-19 14:49:39', '2024-03-20 01:29:35', '2024-03-19 14:49:39', 0, '2024-03-19 14:49:39', 0, '2024-03-20 01:29:35', b'1');
COMMIT;

-- ----------------------------
-- Table structure for ctl_client
-- ----------------------------
DROP TABLE IF EXISTS `ctl_client`;
CREATE TABLE `ctl_client`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_client
-- ----------------------------
BEGIN;
INSERT INTO `ctl_client` (`Id`, `ModuleIds`, `CountryId`, `CityId`, `CategoryId`, `UserId`, `ClientName`, `Address`, `Contact`, `Owner`, `RelevantPerson`, `CreatedOn`, `CreatedById`, `ModifiedById`, `ModifiedOn`, `IsActive`) VALUES (1, '1001002,1001006,1001007,1001008,1001009', 1015004, 1016011, 1051002, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', 'QamSoft Technologies', '....', '03234027206', '....', '......', '2024-03-03 01:58:38', 0, 0, '2024-03-03 01:58:38', b'1'), (2, '1001002,1001006,1001007,1001008,1001009', NULL, NULL, 1051002, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', 'Client_2', '.....', '0878e798323', '....', NULL, '2024-03-03 01:59:00', 0, 0, '2024-03-03 01:59:00', b'1'), (3, '1001002', NULL, NULL, 1051001, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', 'Client_3', '....', '087987987987', '....', NULL, '2024-03-03 02:24:21', 0, 0, '2024-03-03 02:24:21', b'1'), (4, '1001002,1001006,1001007,1001008,1001009', 1015004, 1016014, 1051002, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', 'Client_4', '....', '0898080980809', '....', NULL, '2024-03-19 20:22:50', 0, 0, '2024-03-19 20:22:50', b'1');
COMMIT;

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
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_customer
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for ctl_employee
-- ----------------------------
DROP TABLE IF EXISTS `ctl_employee`;
CREATE TABLE `ctl_employee`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_employee
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for ctl_enum
-- ----------------------------
DROP TABLE IF EXISTS `ctl_enum`;
CREATE TABLE `ctl_enum`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_enum
-- ----------------------------
BEGIN;
INSERT INTO `ctl_enum` (`Id`, `ClientId`, `ModuleId`, `ParentId`, `KeyCode`, `Name`, `Description`, `IsSystemDefined`, `IstAccountLevel`, `IsRequired`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1001, 0, 0, 0, 'Modules', 'Modules', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1001, 1, 1001004, 1045, 'Radio Button', 'Smoking Status', NULL, b'0', b'0', b'1', '2023-09-27 02:08:18', 0, '2023-09-27 02:08:18', 0, b'0'), (1001, 3, 1001004, 1045, 'Text Box', 'Email', NULL, b'0', b'0', b'0', '2023-09-27 02:27:45', 0, '2023-09-27 02:27:45', 0, b'1'), (1002, 0, 0, 1001, 'Menu', 'Menu', NULL, b'1', b'0', b'0', '2023-03-31 19:59:43', 0, '2023-03-31 19:59:43', 0, b'1'), (1002, 1, 1001004, 1038, 'Text Area', 'Medical History..', NULL, b'0', b'0', b'0', '2023-09-27 02:36:53', 0, '2023-09-27 02:36:53', 0, b'1'), (1002, 3, 1001004, 1038, 'Text Box', 'Email', NULL, b'0', b'0', b'0', '2023-09-27 02:35:38', 0, '2023-09-27 02:35:38', 0, b'1'), (1003, 0, 0, 1002, 'Sub_Menu', 'Sub Menu', NULL, b'1', b'0', b'0', '2023-04-03 20:38:32', 0, '2023-04-03 20:38:32', 0, b'1'), (1003, 3, 1001004, 1038, 'Drop Down', 'Blood Group', NULL, b'0', b'0', b'0', '2023-09-27 02:36:05', 0, '2023-09-27 02:36:05', 0, b'1'), (1004, 0, 1001003, 0, 'Chart_of_Account', 'Chart of Account', NULL, b'1', b'0', b'0', '2023-04-03 21:24:13', 0, '2023-04-03 21:24:13', 0, b'1'), (1004, 1, 1001004, 1045, 'Radio Button', 'Hepatitis', NULL, b'0', b'0', b'0', '2023-10-06 09:00:44', 0, '2023-10-06 09:00:44', 0, b'0'), (1005, 0, 1001003, 0, 'Levels', 'Levels', NULL, b'1', b'0', b'0', '2023-04-03 21:24:20', 0, '2023-04-03 21:24:20', 0, b'1'), (1005, 1, 1001004, 1038, 'Text Box', 'Email', 'text', b'0', b'0', b'1', '2023-10-06 09:10:14', 0, '2023-10-06 09:10:14', 0, b'1'), (1006, 0, 1001003, 1005, 'Main', 'Main', NULL, b'1', b'1', b'0', '2023-04-03 22:52:08', 0, '2023-04-03 22:52:08', 0, b'1'), (1006, 1, 1001004, 1045, 'Text Area', 'Details ', NULL, b'0', b'0', b'0', '2023-10-31 01:38:43', 0, '2023-10-31 01:38:43', 0, b'0'), (1007, 0, 1001003, 1006, 'Group', 'Group', NULL, b'1', b'1', b'0', '2023-04-03 22:52:17', 0, '2023-04-03 22:52:17', 0, b'1'), (1007, 1, 1001004, 1038, 'Drop Down', 'Blood Group', NULL, b'0', b'0', b'0', '2023-11-02 11:57:20', 0, '2023-11-02 11:57:20', 0, b'1'), (1008, 0, 1001003, 1007, 'Detail', 'Detail', NULL, b'1', b'1', b'0', '2023-04-03 22:52:28', 0, '2023-04-03 22:52:28', 0, b'1'), (1008, 1, 1001004, 1045, 'Drop Down', 'Blood Group', NULL, b'0', b'0', b'0', '2023-11-02 12:06:55', 0, '2023-11-02 12:06:55', 0, b'0'), (1009, 0, 0, 0, 'Vendor', 'Vendor', NULL, b'1', b'0', b'0', '2023-04-06 01:38:02', 0, '2023-04-06 01:38:02', 0, b'0'), (1009, 1, 1001004, 1045, 'Check Box', 'Is Suger Patient', NULL, b'0', b'0', b'0', '2023-11-02 12:07:14', 0, '2023-11-02 12:07:14', 0, b'0'), (1010, 0, 1001003, 0, 'Salesman', 'Salesman', NULL, b'1', b'0', b'0', '2023-04-06 01:38:18', 0, '2023-04-06 01:38:18', 0, b'1'), (1010, 1, 1001004, 1045, 'Text Box', 'ECG', 'text', b'0', b'0', b'1', '2023-11-02 12:08:20', 0, '2023-11-02 12:08:20', 0, b'0'), (1011, 0, 1001003, 0, 'Godown', 'Godown', NULL, b'1', b'0', b'0', '2023-04-06 01:38:30', 0, '2023-04-06 01:38:30', 0, b'1'), (1011, 1, 1001004, 1045, 'Drop Down', 'Platelets Count', NULL, b'0', b'0', b'1', '2023-12-02 15:55:45', 0, '2023-12-02 15:55:45', 0, b'1'), (1012, 0, 0, 0, 'Product', 'Product', NULL, b'1', b'0', b'0', '2023-04-06 01:39:29', 0, '2023-04-06 01:39:29', 0, b'0'), (1013, 0, 1001003, 0, 'Voucher_Types', 'Voucher Types', NULL, b'1', b'0', b'0', '2023-04-07 18:59:39', 0, '2023-04-07 18:59:39', 0, b'1'), (1014, 0, 1001003, 0, 'Status', 'Status', NULL, b'1', b'0', b'0', '2023-04-08 18:15:00', 0, '2023-04-08 18:15:00', 0, b'1'), (1015, 0, 0, 0, 'Country', 'Country', NULL, b'1', b'0', b'0', '2023-05-03 15:06:56', 0, '2023-05-03 15:06:56', 0, b'1'), (1016, 0, 0, 1015, 'City', 'City', NULL, b'1', b'0', b'0', '2023-05-03 15:08:16', 0, '2023-05-03 15:08:16', 0, b'1'), (1017, 0, 1001003, 0, 'Attributes', 'Attributes', NULL, b'1', b'0', b'0', '2023-07-17 19:15:32', 0, '2023-07-17 19:15:32', 0, b'1'), (1018, 0, 1001003, 1017, 'AttributeValues', 'AttributeValues', NULL, b'1', b'0', b'0', '2023-07-17 19:15:43', 0, '2023-07-17 19:15:43', 0, b'1'), (1019, 0, 1001003, 0, 'UOM', 'UOM', NULL, b'1', b'0', b'0', '2023-07-25 11:27:47', 0, '2023-07-25 11:27:47', 0, b'1'), (1020, 0, 1001003, 0, 'UOMTypes', 'UOMTypes', NULL, b'1', b'0', b'0', '2023-07-26 00:10:30', 0, '2023-07-26 00:10:30', 0, b'1'), (1021, 0, 1001003, 0, 'Document_Extra_Types', 'Document Extra Types', NULL, b'1', b'0', b'0', '2023-08-01 11:42:01', 0, '2023-08-01 11:42:01', 0, b'1'), (1022, 0, 1001003, 1021, 'Tax', 'Tax', NULL, b'1', b'0', b'0', '2023-08-01 11:42:22', 0, '2023-08-01 11:42:22', 0, b'1'), (1023, 0, 1001003, 1021, 'Freight', 'Freight', NULL, b'1', b'0', b'0', '2023-08-01 11:42:55', 0, '2023-08-01 11:42:55', 0, b'1'), (1024, 0, 1001003, 1021, 'Discount', 'Discount', NULL, b'1', b'0', b'0', '2023-08-01 11:43:08', 0, '2023-08-01 11:43:08', 0, b'1'), (1025, 0, 1001003, 0, 'DocExtrasIncDecTypes', 'DocExtrasIncDecTypes', NULL, b'1', b'0', b'0', '2023-08-01 12:22:58', 0, '2023-08-01 12:22:58', 0, b'1'), (1026, 0, 1001003, 0, 'DocExtraFormulas', 'DocExtraFormulas', NULL, b'1', b'0', b'0', '2023-08-01 12:23:18', 0, '2023-08-01 12:23:18', 0, b'1'), (1027, 0, 1001003, 0, 'UOM_Types', 'UOM Types', NULL, b'1', b'0', b'0', '2023-08-01 12:49:27', 0, '2023-08-01 12:49:27', 0, b'1'), (1028, 0, 1001003, 0, 'Product_Taxes', 'Product Taxes', NULL, b'1', b'0', b'0', '2023-08-08 19:28:31', 0, '2023-08-08 19:28:31', 0, b'1'), (1029, 0, 1001003, 0, 'Item_Types', 'Item Types', NULL, b'1', b'0', b'0', '2023-08-15 20:22:54', 0, '2023-08-15 20:22:54', 0, b'1'), (1030, 0, 1001003, 0, 'Documents', 'Documents', NULL, b'1', b'0', b'0', '2023-08-17 12:21:21', 0, '2023-08-17 12:21:21', 0, b'1'), (1031, 0, 1001003, 1030, 'Accounts', 'Accounts', NULL, b'1', b'0', b'0', '2023-08-17 16:24:52', 0, '2023-08-17 16:24:52', 0, b'1'), (1032, 0, 1001003, 1031, 'Large_Packing', 'Large Packing', NULL, b'1', b'0', b'0', '2023-08-17 16:25:25', 0, '2023-08-17 16:25:25', 0, b'1'), (1033, 0, 1001003, 1031, 'Small_Packing', 'Small Packing', NULL, b'1', b'0', b'0', '2023-08-17 16:25:36', 0, '2023-08-17 16:25:36', 0, b'1'), (1034, 0, 1001003, 0, 'Transaction_Types', 'Transaction Types', NULL, b'1', b'0', b'0', '2023-08-19 00:55:47', 0, '2023-08-19 00:55:47', 0, b'1'), (1035, 0, 0, 0, 'Gender', 'Gender', NULL, b'1', b'0', b'0', '2023-08-30 09:29:00', 0, '2023-08-30 09:29:00', 0, b'1'), (1036, 0, 0, 1016, 'Areas', 'Areas', NULL, b'1', b'0', b'0', '2023-08-30 12:14:03', 0, '2023-08-30 12:14:03', 0, b'1'), (1037, 0, 0, 0, 'Field_Types', 'Field Types', NULL, b'1', b'0', b'0', '2023-08-30 19:19:54', 0, '2023-08-30 19:19:54', 0, b'1'), (1038, 0, 1001004, 0, 'Patient_Extra_Fields', 'Patient Extra Fields', NULL, b'1', b'0', b'0', '2023-08-30 22:44:41', 0, '2023-08-30 22:44:41', 0, b'1'), (1045, 0, 1001004, 0, 'RxMeddicine_Extra_Fields', 'RxMeddicine Extra Fields', NULL, b'1', b'0', b'0', '2023-09-01 12:29:37', 0, '2023-09-01 12:29:37', 0, b'1'), (1047, 0, 1001004, 0, 'Meal_Relations', 'Meal Relations', NULL, b'1', b'0', b'0', '2023-09-01 15:54:46', 0, '2023-09-01 15:54:46', 0, b'1'), (1048, 0, 1001004, 0, 'Precautions', 'Precautions', NULL, b'0', b'0', b'0', '2023-09-27 01:30:49', 0, '2023-09-27 01:30:49', 0, b'1'), (1049, 0, 1001004, 0, 'Report_Categories', 'Report Categories', NULL, b'0', b'0', b'0', '2023-09-27 01:30:58', 0, '2023-09-27 01:30:58', 0, b'1'), (1050, 0, 1001004, 0, 'Med_Remarks', 'Med Remarks', NULL, b'0', b'0', b'0', '2023-09-27 01:31:20', 0, '2023-09-27 01:31:20', 0, b'1'), (1051, 0, 1001004, 0, 'Client_Categories', 'Client Categories', NULL, b'0', b'0', b'0', '2023-09-27 01:31:29', 0, '2023-09-27 01:31:29', 0, b'1'), (1052, 0, 1001004, 0, 'Med_Categories', 'Med Categories', NULL, b'0', b'0', b'0', '2023-09-27 01:32:26', 0, '2023-09-27 01:32:26', 0, b'1'), (1053, 0, 1001004, 0, 'Manufacturer', 'Manufacturer', NULL, b'0', b'0', b'0', '2023-09-27 01:32:53', 0, '2023-09-27 01:32:53', 0, b'1'), (1054, 0, 0, 0, 'Roles', 'Roles', NULL, b'1', b'0', b'0', '2023-09-27 11:26:33', 0, '2023-09-27 11:26:33', 0, b'1'), (1055, 0, 0, 0, 'AppStatus', 'AppStatus', NULL, b'1', b'0', b'0', '2023-10-17 11:38:21', 0, '2023-10-17 11:38:21', 0, b'1'), (1056, 0, 0, 0, 'Permissions', 'Permissions', NULL, b'1', b'0', b'0', '2023-10-24 09:47:24', 0, '2023-10-24 09:47:24', 0, b'1'), (1057, 0, 1001004, 0, 'BP_Statuses', 'BP Statuses', NULL, b'0', b'0', b'0', '2023-12-04 19:30:54', 0, '2023-12-04 19:30:54', 0, b'1'), (1058, 0, 0, 0, 'Input_Types', 'Input Types', NULL, b'0', b'0', b'0', '2023-12-05 16:56:53', 0, '2023-12-05 16:56:53', 0, b'1'), (1101, 0, 0, 0, 'Week_Days', 'Week Days', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1102, 0, 0, 0, 'Entities', 'Entities', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1103, 0, 0, 0, 'Schedule_Types', 'Schedule Types', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1104, 0, 0, 0, 'Working_Type', 'Working Type', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1105, 0, 0, 0, 'EventType', 'EventType', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1106, 0, 0, 0, 'Locations', 'Locations', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1107, 0, 0, 0, 'Task_Status', 'Task Status', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1108, 0, 0, 0, 'Task_Priorities', 'Task Priorities', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1109, 0, 0, 0, 'Task_Modules', 'Task Modules', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1110, 0, 0, 1001, 'Task_Claims', 'Task Claims', NULL, b'0', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1111, 0, 0, 0, 'Stalled_Task_Reasons', 'Stalled Task Reasons', NULL, b'0', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1112, 0, 0, 0, 'Novels', 'Novels', NULL, b'0', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1113, 0, 0, 1016, 'Novel_Chapters', 'Novel Chapters', NULL, b'0', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1'), (1114, 0, 0, 0, 'Vocabulary_Difficuilty_Levels', 'Vocabulary Difficuilty Levels', NULL, b'1', b'0', b'0', '2023-03-31 19:05:05', 0, '2023-03-31 19:05:05', 0, b'1');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_enumline
-- ----------------------------
BEGIN;
INSERT INTO `ctl_enumline` (`Id`, `ClientId`, `ModuleId`, `EnumTypeId`, `ParentId`, `LevelId`, `KeyCode`, `AccountCode`, `Name`, `Value`, `Description`, `IsSystemDefined`, `IstAccountLevel`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1001001, 1, 1001004, 1001, 0, 0, 'Radio Button_Yes.', NULL, 'Yes.', 'Yes.', NULL, b'0', b'0', '2023-09-27 02:08:18', 0, '2023-09-27 02:08:18', 0, b'1'), (1001002, 0, 0, 1001, 0, 0, '/security', NULL, 'Security', 'Security', NULL, b'0', b'0', '2023-09-25 12:25:00', 0, '2023-09-25 12:25:00', 0, b'1'), (1001002, 1, 1001004, 1001, 0, 0, 'No', NULL, 'No', 'No', NULL, b'0', b'0', '2023-09-27 02:08:18', 0, '2023-09-27 02:08:18', 0, b'1'), (1001003, 0, 0, 1001, 0, 0, '/account', NULL, 'GL', 'GL', NULL, b'0', b'0', '2023-09-25 12:25:14', 0, '2023-09-25 12:25:14', 0, b'1'), (1001004, 0, 0, 1001, 0, 0, '/pms', NULL, 'PMS', 'PMS', NULL, b'0', b'0', '2023-09-25 12:25:26', 0, '2023-09-25 12:25:26', 0, b'1'), (1001006, 0, 0, 1001, 0, 0, '/task', NULL, 'Task', 'Task', NULL, b'0', b'0', '2024-02-14 02:12:51', 0, '2024-02-14 02:12:51', 0, b'1'), (1001007, 0, 0, 1001, 0, 0, '/att/att', NULL, 'Attendance', 'Attendance', NULL, b'0', b'0', '2024-02-14 02:18:32', 0, '2024-02-14 02:18:32', 0, b'1'), (1001008, 0, 0, 1001, 0, 0, '/sch/sch', NULL, 'Schedule', 'Schedule', NULL, b'0', b'0', '2024-02-14 02:23:09', 0, '2024-02-14 02:23:09', 0, b'1'), (1001009, 0, 0, 1001, 0, 0, '/voc', NULL, 'Vocabulary', 'Vocabulary', NULL, b'0', b'0', '2024-02-14 02:24:12', 0, '2024-02-14 02:24:12', 0, b'1'), (1001012, 0, 0, 1001, 0, 0, '/ctl', NULL, 'Catalog', 'Catalog', NULL, b'1', b'0', '2024-03-09 11:51:33', 0, '2024-03-09 11:51:33', 0, b'1'), (1002003, 0, 0, 1002, 1001002, 0, 'security/security/roles?type=1054', NULL, 'Manage Roles', 'Manage Roles', NULL, b'1', b'0', '2023-09-25 12:44:32', 0, '2023-09-25 12:44:32', 0, b'1'), (1002007, 0, 0, 1002, 1001004, 0, '/pms/appt/appmntList', NULL, 'Appointments', 'Appointments', NULL, b'1', b'0', '2023-09-25 12:59:35', 0, '2023-09-25 12:59:35', 0, b'1'), (1002008, 0, 0, 1002, 1001004, 0, '/pms/doctor/doctorList', NULL, 'Doctor', 'Doctor', NULL, b'1', b'0', '2023-09-25 13:01:15', 0, '2023-09-25 13:01:15', 0, b'1'), (1002009, 0, 0, 1002, 1001004, 0, '/pms/patient', NULL, 'Patient', 'Patient', NULL, b'1', b'0', '2023-09-25 13:01:43', 0, '2023-09-25 13:01:43', 0, b'1'), (1002010, 0, 0, 1002, 1001004, 0, '/pms/rx', NULL, 'Patient Visits', 'Patient Visits', NULL, b'1', b'0', '2023-09-25 13:02:49', 0, '2023-09-25 13:02:49', 0, b'1'), (1002016, 0, 0, 1002, 1001004, 0, '/pms/pms/manageMedicine', NULL, 'Manage Medicine', 'Manage Medicine', NULL, b'1', b'0', '2023-09-27 17:14:38', 0, '2023-09-27 17:14:38', 0, b'1'), (1002017, 0, 0, 1002, 1001004, 0, '/pms/staff/staffList', NULL, 'Employee', 'Employee', NULL, b'1', b'0', '2023-10-09 14:20:12', 0, '2023-10-09 14:20:12', 0, b'1'), (1002018, 0, 0, 1002, 1001003, 0, '/account/accounts/accountList	', NULL, 'Chart of Account', 'Chart of Account', NULL, b'1', b'0', '2023-10-09 23:00:41', 0, '2023-10-09 23:00:41', 0, b'1'), (1002019, 0, 0, 1002, 1001003, 0, '/account/accounts/vchType', NULL, 'Manage Voucher Type', 'Manage Voucher Type', NULL, b'1', b'0', '2023-10-09 23:03:02', 0, '2023-10-09 23:03:02', 0, b'1'), (1002020, 0, 0, 1002, 1001003, 0, '/account/accounts/vchList ', NULL, 'Voucher', 'Voucher', NULL, b'1', b'0', '2023-10-09 23:03:43', 0, '2023-10-09 23:03:43', 0, b'1'), (1002021, 0, 0, 1002, 1001002, 0, '/security/security/users', NULL, 'Manage Users', 'Manage Users', NULL, b'1', b'0', '2023-10-24 09:35:46', 0, '2023-10-24 09:35:46', 0, b'1'), (1002023, 0, 0, 1002, 1001006, 0, '/task/task/taskList', NULL, 'Task List', 'Task List', NULL, b'1', b'0', '2024-02-14 02:13:49', 0, '2024-02-14 02:13:49', 0, b'1'), (1002024, 0, 0, 1002, 1001007, 0, '/att/att/attReport', NULL, 'Summary Report', 'Summary Report', NULL, b'1', b'0', '2024-02-14 02:19:39', 0, '2024-02-14 02:19:39', 0, b'1'), (1002025, 0, 0, 1002, 1001007, 0, '/att/att/attDetReport', NULL, 'Detail Report', 'Detail Report', NULL, b'1', b'0', '2024-02-14 02:20:15', 0, '2024-02-14 02:20:15', 0, b'1'), (1002026, 0, 0, 1002, 1001006, 0, '/task/task/activityRpt', NULL, 'Activity Report', 'Activity Report', NULL, b'0', b'0', '2024-03-09 18:41:08', 0, '2024-03-09 18:41:08', 0, b'1'), (1002027, 0, 0, 1002, 1001012, 0, '/ctl/ctl/cltList', NULL, 'Manage Client', 'Manage Client', NULL, b'1', b'0', '2024-03-09 11:53:20', 0, '2024-03-09 11:53:20', 0, b'1'), (1002028, 0, 0, 1002, 1001012, 0, '/ctl/security/cltPerms', NULL, 'Clients Permission', 'Clients Permission', NULL, b'1', b'0', '2024-03-09 11:55:05', 0, '2024-03-09 11:55:05', 0, b'1'), (1002029, 0, 0, 1002, 1001012, 0, '/ctl/ctl/manageSetting', NULL, 'Settings', 'Settings', NULL, b'1', b'0', '2024-03-09 11:55:49', 0, '2024-03-09 11:55:49', 0, b'1'), (1002030, 0, 0, 1002, 1001002, 0, '/security/security/perms', NULL, 'Manage Permissions', 'Manage Permissions', NULL, b'1', b'0', '2024-03-09 12:02:13', 0, '2024-03-09 12:02:13', 0, b'1'), (1002031, 0, 0, 1002, 1001008, 0, '/sch/sch/Schedule', NULL, 'Manage Schedule', 'Manage Schedule', NULL, b'1', b'0', '2024-03-19 16:35:32', 0, '2024-03-19 16:35:32', 0, b'1'), (1002032, 0, 0, 1002, 1001009, 0, '/voc/voc/mngVoc', NULL, 'Manage Vocabulary', 'Manage Vocabulary', NULL, b'1', b'0', '2024-03-19 16:37:55', 0, '2024-03-19 16:37:55', 0, b'1'), (1003001, 0, 0, 1003, 1002009, 0, '/pms/patient/patientList', NULL, 'Patients', 'Patients', NULL, b'1', b'0', '2023-09-25 13:04:37', 0, '2023-09-25 13:04:37', 0, b'1'), (1003001, 3, 1001004, 1003, 0, 0, 'A+', NULL, 'A+', 'A+', NULL, b'0', b'0', '2023-09-27 02:36:05', 0, '2023-09-27 02:36:05', 0, b'1'), (1003002, 0, 0, 1003, 1002009, 0, '/pms/patient/patFieldList', NULL, 'Patient Parameters', 'Patient Parameters', NULL, b'1', b'0', '2023-09-25 13:05:08', 0, '2023-09-25 13:05:08', 0, b'1'), (1003002, 3, 1001004, 1003, 0, 0, 'B+', NULL, 'B+', 'B+', NULL, b'0', b'0', '2023-09-27 02:36:05', 0, '2023-09-27 02:36:05', 0, b'1'), (1003003, 0, 0, 1003, 1002010, 0, '/pms/rx/rxList', NULL, 'Patient Visit', 'Patient Visit', NULL, b'1', b'0', '2023-09-25 13:11:15', 0, '2023-09-25 13:11:15', 0, b'1'), (1003004, 0, 0, 1003, 1002010, 0, '/pms/rx/rxMedExtraFieldsList', NULL, 'Patient Visit Parameters', 'Patient Visit Parameters', NULL, b'1', b'0', '2023-09-25 13:12:32', 0, '2023-09-25 13:12:32', 0, b'1'), (1003005, 0, 0, 1003, 1002004, 0, '/account/accounts/accountList', NULL, 'Chart of Accounts', 'Chart of Accounts', NULL, b'1', b'0', '2023-09-25 13:17:42', 0, '2023-09-25 13:17:42', 0, b'0'), (1003006, 0, 0, 1003, 1002004, 0, '/account/accounts/vchList', NULL, 'Voucher ', 'Voucher ', NULL, b'1', b'0', '2023-09-25 13:18:28', 0, '2023-09-25 13:18:28', 0, b'0'), (1003007, 0, 0, 1003, 1002004, 0, '/account/accounts/salesList', NULL, 'Sales', 'Sales', NULL, b'1', b'0', '2023-09-25 13:20:15', 0, '2023-09-25 13:20:15', 0, b'0'), (1003008, 0, 0, 1003, 1002005, 0, '/account/product/proList', NULL, 'Products', 'Products', NULL, b'1', b'0', '2023-09-25 13:26:43', 0, '2023-09-25 13:26:43', 0, b'0'), (1003009, 0, 0, 1003, 1002005, 0, '/account/product/attValues', NULL, 'Product Attributes', 'Product Attributes', NULL, b'1', b'0', '2023-09-25 13:27:29', 0, '2023-09-25 13:27:29', 0, b'0'), (1003010, 0, 0, 1003, 1002005, 0, '/account/product/uom', NULL, 'UOM', 'UOM', NULL, b'1', b'0', '2023-09-25 13:28:13', 0, '2023-09-25 13:28:13', 0, b'0'), (1003011, 0, 0, 1003, 1002005, 0, '/account/product/uomConvrn', NULL, 'UOM Converson', 'UOM Converson', NULL, b'1', b'0', '2023-09-25 13:28:46', 0, '2023-09-25 13:28:46', 0, b'0'), (1003012, 0, 0, 1003, 1002005, 0, '/account/product/docExtras', NULL, 'Doc Extras', 'Doc Extras', NULL, b'1', b'0', '2023-09-25 13:29:27', 0, '2023-09-25 13:29:27', 0, b'0'), (1003013, 0, 0, 1003, 1002006, 0, '/account/tax/taxes', NULL, 'Manage Taxes', 'Manage Taxes', NULL, b'1', b'0', '2023-09-25 13:30:02', 0, '2023-09-25 13:30:02', 0, b'0'), (1003014, 0, 0, 1003, 1002006, 0, '/account/tax/proTaxes', NULL, 'Product Taxes', 'Product Taxes', NULL, b'1', b'0', '2023-09-25 13:30:33', 0, '2023-09-25 13:30:33', 0, b'0'), (1003015, 0, 0, 1003, 1002011, 0, '/account/stakeHolder/customer', NULL, 'Customer', 'Customer', NULL, b'1', b'0', '2023-09-25 13:31:11', 0, '2023-09-25 13:31:11', 0, b'0'), (1003016, 0, 0, 1003, 1002011, 0, '/account/stakeHolder/supplier', NULL, 'Supplier', 'Supplier', NULL, b'1', b'0', '2023-09-25 13:31:37', 0, '2023-09-25 13:31:37', 0, b'0'), (1003017, 0, 0, 1003, 1002014, 0, '/account/security/security/users?moduleId=1001003', NULL, 'Manage User', 'Manage User', NULL, b'1', b'0', '2023-09-27 11:48:26', 0, '2023-09-27 11:48:26', 0, b'0'), (1003018, 0, 0, 1003, 1002014, 0, '/account/security/catalog/enumLine?type=1054&module=1001003', NULL, 'Manage Roles', 'Manage Roles', NULL, b'1', b'0', '2023-09-27 11:50:26', 0, '2023-09-27 11:50:26', 0, b'0'), (1003019, 0, 0, 1003, 1002015, 0, '/pms/security/security/users?moduleId=1001004', NULL, 'Manage User', 'Manage User', NULL, b'1', b'0', '2023-09-27 11:53:11', 0, '2023-09-27 11:53:11', 0, b'0'), (1003020, 0, 0, 1003, 1002015, 0, '/pms/security/catalog/enumLine?type=1054&module=1001004', NULL, 'Manage Roles', 'Manage Roles', NULL, b'1', b'0', '2023-09-27 11:53:37', 0, '2023-09-27 11:53:37', 0, b'0'), (1004001, 0, 1001003, 1004, 0, 1006, '10', '10', 'Fixed Assets', NULL, NULL, b'0', b'0', '2023-08-21 10:59:17', 0, '2023-08-21 10:59:17', 0, b'1'), (1004001, 1, 1001004, 1004, 0, 0, 'Yes', NULL, 'Yes', 'Yes', NULL, b'0', b'0', '2023-10-06 09:00:44', 0, '2023-10-06 09:00:44', 0, b'1'), (1004001, 2, 1001003, 1004, 0, 1006, '18', '18', 'Current Assets', NULL, NULL, b'0', b'0', '2023-10-09 23:54:42', 0, '2023-10-09 23:54:42', 0, b'1'), (1004001, 3, 1001003, 1004, 0, 1006, '10', '10', 'Fixed Assets', NULL, NULL, b'0', b'0', '2023-10-09 23:42:24', 0, '2023-10-09 23:42:24', 0, b'1'), (1004002, 0, 1001003, 1004, 0, 1006, '18', '18', 'Current Assets', NULL, NULL, b'0', b'0', '2023-08-21 10:59:33', 0, '2023-08-21 10:59:33', 0, b'1'), (1004002, 1, 1001004, 1004, 0, 0, 'No', NULL, 'No', 'No', NULL, b'0', b'0', '2023-10-06 09:00:44', 0, '2023-10-06 09:00:44', 0, b'1'), (1004002, 2, 1001003, 1004, 1004001, 1007, '180', '18-180', 'Stock & Stores', NULL, NULL, b'0', b'0', '2023-10-10 01:32:49', 0, '2023-10-10 01:32:49', 0, b'1'), (1004002, 3, 1001003, 1004, 1004001, 1007, '100', '10-100', 'Fixed Assets-Tangible', NULL, NULL, b'0', b'0', '2023-10-09 23:43:20', 0, '2023-10-09 23:43:20', 0, b'1'), (1004003, 0, 1001003, 1004, 0, 1006, '41', '41', 'Capital & Reserves', NULL, NULL, b'0', b'0', '2023-08-21 10:59:57', 0, '2023-08-21 10:59:57', 0, b'1'), (1004003, 2, 1001003, 1004, 1004002, 1008, '1801', '18-180-1801', 'Stock in Trade', NULL, NULL, b'0', b'0', '2023-10-10 01:33:11', 0, '2023-10-10 01:33:11', 0, b'1'), (1004003, 3, 1001003, 1004, 1004002, 1008, '1001', '10-100-1001', 'Building - Office', NULL, NULL, b'0', b'0', '2023-10-09 23:44:35', 0, '2023-10-09 23:44:35', 0, b'1'), (1004004, 0, 1001003, 1004, 0, 1006, '61', '61', 'Current Liabilities', NULL, NULL, b'0', b'0', '2023-08-21 11:00:14', 0, '2023-08-21 11:00:14', 0, b'1'), (1004004, 2, 1001003, 1004, 1004001, 1007, '350', '18-350', 'Tax withholding', NULL, NULL, b'0', b'0', '2023-10-10 01:54:00', 0, '2023-10-10 01:54:00', 0, b'1'), (1004004, 3, 1001003, 1004, 1004002, 1008, '1002', '10-100-1002', 'Building - Factory', NULL, NULL, b'0', b'0', '2023-10-09 23:45:02', 0, '2023-10-09 23:45:02', 0, b'1'), (1004005, 0, 1001003, 1004, 0, 1006, '71', '71', 'Total Revenue', NULL, NULL, b'0', b'0', '2023-08-21 11:00:29', 0, '2023-08-21 11:00:29', 0, b'1'), (1004005, 2, 1001003, 1004, 1004004, 1008, '3501', '18-350-3501', 'Tax deduction - Customers', NULL, NULL, b'0', b'0', '2023-10-10 01:54:22', 0, '2023-10-10 01:54:22', 0, b'1'), (1004005, 3, 1001003, 1004, 1004002, 1008, '1003', '10-100-1003', 'Plant & Machinery', NULL, NULL, b'0', b'0', '2023-10-09 23:53:14', 0, '2023-10-09 23:53:14', 0, b'1'), (1004006, 0, 1001003, 1004, 0, 1006, '75', '75', 'Cost of Goods Sold', NULL, NULL, b'0', b'0', '2023-08-21 11:00:48', 0, '2023-08-21 11:00:48', 0, b'1'), (1004006, 3, 1001003, 1004, 1004002, 1008, '1004', '10-100-1004', 'Electrical Appliances', NULL, NULL, b'0', b'0', '2023-10-09 23:57:43', 0, '2023-10-09 23:57:43', 0, b'1'), (1004007, 0, 1001003, 1004, 0, 1006, '84', '84', 'Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:01:10', 0, '2023-08-21 11:01:10', 0, b'1'), (1004008, 0, 1001003, 1004, 1004001, 1007, '100', '10-100', 'Fixed Assets-Tangible', NULL, NULL, b'0', b'0', '2023-08-21 11:01:43', 0, '2023-08-21 11:01:43', 0, b'1'), (1004009, 0, 1001003, 1004, 1004002, 1007, '180', '18-180', 'Stock & Stores', NULL, NULL, b'0', b'0', '2023-08-21 11:02:45', 0, '2023-08-21 11:02:45', 0, b'1'), (1004010, 0, 1001003, 1004, 1004002, 1007, '190', '18-190', 'Customers', NULL, NULL, b'0', b'0', '2023-08-21 11:02:56', 0, '2023-08-21 11:02:56', 0, b'1'), (1004011, 0, 1001003, 1004, 1004002, 1007, '250', '18-250', 'Advances, Deposits & Prepayments', NULL, NULL, b'0', b'0', '2023-08-21 11:03:31', 0, '2023-08-21 11:03:31', 0, b'1'), (1004012, 0, 1001003, 1004, 1004002, 1007, '350', '18-350', 'Tax withholding', NULL, NULL, b'0', b'0', '2023-08-21 11:03:52', 0, '2023-08-21 11:03:52', 0, b'1'), (1004013, 0, 1001003, 1004, 1004002, 1007, '360', '18-360', 'Cash & Bank Balances', NULL, NULL, b'0', b'0', '2023-08-21 11:04:08', 0, '2023-08-21 11:04:08', 0, b'1'), (1004014, 0, 1001003, 1004, 1004003, 1007, '410', '41-410', 'Capital', NULL, NULL, b'0', b'0', '2023-08-21 11:04:29', 0, '2023-08-21 11:04:29', 0, b'1'), (1004015, 0, 1001003, 1004, 1004004, 1007, '620', '61-620', 'Suppliers', NULL, NULL, b'0', b'0', '2023-08-21 11:04:58', 0, '2023-08-21 11:04:58', 0, b'1'), (1004016, 0, 1001003, 1004, 1004004, 1007, '679', '61-679', 'Tax Payable', NULL, NULL, b'0', b'0', '2023-08-21 11:05:25', 0, '2023-08-21 11:05:25', 0, b'1'), (1004017, 0, 1001003, 1004, 1004005, 1007, '710', '71-710', 'Revenue', NULL, NULL, b'0', b'0', '2023-08-21 11:05:46', 0, '2023-08-21 11:05:46', 0, b'1'), (1004018, 0, 1001003, 1004, 1004005, 1007, '740', '71-740', 'Discount', NULL, NULL, b'0', b'0', '2023-08-21 11:06:06', 0, '2023-08-21 11:06:06', 0, b'1'), (1004019, 0, 1001003, 1004, 1004005, 1007, '741', '71-741', 'Bulk Discount', NULL, NULL, b'0', b'0', '2023-08-21 11:06:25', 0, '2023-08-21 11:06:25', 0, b'1'), (1004020, 0, 1001003, 1004, 1004005, 1007, '742', '71-742', 'Bulk Discount', NULL, NULL, b'0', b'0', '2023-08-21 11:06:50', 0, '2023-08-21 11:06:50', 0, b'1'), (1004021, 0, 1001003, 1004, 1004006, 1007, '750', '75-750', 'Material Cost', NULL, NULL, b'0', b'0', '2023-08-21 11:07:32', 0, '2023-08-21 11:07:32', 0, b'1'), (1004022, 0, 1001003, 1004, 1004007, 1007, '840', '84-840', 'Administrative Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:08:01', 0, '2023-08-21 11:08:01', 0, b'1'), (1004023, 0, 1001003, 1004, 1004007, 1007, '850', '84-850', 'Selling Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:08:22', 0, '2023-08-21 11:08:22', 0, b'1'), (1004024, 0, 1001003, 1004, 1004008, 1008, '1001', '10-100-1001', 'Building - Office', NULL, NULL, b'0', b'0', '2023-08-21 11:08:59', 0, '2023-08-21 11:08:59', 0, b'1'), (1004025, 0, 1001003, 1004, 1004008, 1008, '1002', '10-100-1002', 'Building - Factory', NULL, NULL, b'0', b'0', '2023-08-21 11:09:13', 0, '2023-08-21 11:09:13', 0, b'1'), (1004026, 0, 1001003, 1004, 1004008, 1008, '1003', '10-100-1003', 'Plant & Machinery', NULL, NULL, b'0', b'0', '2023-08-21 11:09:28', 0, '2023-08-21 11:09:28', 0, b'1'), (1004027, 0, 1001003, 1004, 1004008, 1008, '1004', '10-100-1004', 'Electrical Appliances', NULL, NULL, b'0', b'0', '2023-08-21 11:09:43', 0, '2023-08-21 11:09:43', 0, b'1'), (1004028, 0, 1001003, 1004, 1004009, 1008, '1801', '18-180-1801', 'Stock in Trade', NULL, NULL, b'0', b'0', '2023-08-21 11:10:12', 0, '2023-08-21 11:10:12', 0, b'1'), (1004029, 0, 1001003, 1004, 1004010, 1008, '1901', '18-190-1901', 'Raheem Enterprises', NULL, NULL, b'0', b'0', '2023-08-21 11:10:37', 0, '2023-08-21 11:10:37', 0, b'1'), (1004030, 0, 1001003, 1004, 1004010, 1008, '1902', '18-190-1902', 'Nasir Sattar', NULL, NULL, b'0', b'0', '2023-08-21 11:10:54', 0, '2023-08-21 11:10:54', 0, b'1'), (1004031, 0, 1001003, 1004, 1004010, 1008, '1903', '18-190-1903', 'Pervaiz Ashraf', NULL, NULL, b'0', b'0', '2023-08-21 11:11:12', 0, '2023-08-21 11:11:12', 0, b'1'), (1004032, 0, 1001003, 1004, 1004011, 1008, '2501', '18-250-2501', 'Farooq Ahmad', NULL, NULL, b'0', b'0', '2023-08-21 11:11:33', 0, '2023-08-21 11:11:33', 0, b'1'), (1004033, 0, 1001003, 1004, 1004011, 1008, '2502', '18-250-2502', 'Jawad Haider', NULL, NULL, b'0', b'0', '2023-08-21 11:11:51', 0, '2023-08-21 11:11:51', 0, b'1'), (1004034, 0, 1001003, 1004, 1004011, 1008, '2503', '18-250-2503', 'Riaz', NULL, NULL, b'0', b'0', '2023-08-21 11:12:07', 0, '2023-08-21 11:12:07', 0, b'1'), (1004035, 0, 1001003, 1004, 1004012, 1008, '3501', '18-350-3501', 'Tax deduction - Customers', NULL, NULL, b'0', b'0', '2023-08-21 11:12:27', 0, '2023-08-21 11:12:27', 0, b'1'), (1004036, 0, 1001003, 1004, 1004012, 1008, '3502', '18-350-3502', 'Tax deduction - Imports', NULL, NULL, b'0', b'0', '2023-08-21 11:12:43', 0, '2023-08-21 11:12:43', 0, b'1'), (1004037, 0, 1001003, 1004, 1004012, 1008, '3503', '18-350-3503', 'Tax deduction - Banks', NULL, NULL, b'0', b'0', '2023-08-21 11:13:00', 0, '2023-08-21 11:13:00', 0, b'1'), (1004038, 0, 1001003, 1004, 1004013, 1008, '3601', '18-360-3601', 'Cash in Hand', NULL, NULL, b'0', b'0', '2023-08-21 11:13:20', 0, '2023-08-21 11:13:20', 0, b'1'), (1004039, 0, 1001003, 1004, 1004013, 1008, '3602', '18-360-3602', 'Cheques', NULL, NULL, b'0', b'0', '2023-08-21 11:13:51', 0, '2023-08-21 11:13:51', 0, b'1'), (1004040, 0, 1001003, 1004, 1004013, 1008, '3603', '18-360-3603', 'Petty Cash', NULL, NULL, b'0', b'0', '2023-08-21 11:14:16', 0, '2023-08-21 11:14:16', 0, b'1'), (1004041, 0, 1001003, 1004, 1004013, 1008, '3701', '18-360-3701', 'MCB', NULL, NULL, b'0', b'0', '2023-08-21 11:14:31', 0, '2023-08-21 11:14:31', 0, b'1'), (1004042, 0, 1001003, 1004, 1004013, 1008, '3702', '18-360-3702', 'HBL', NULL, NULL, b'0', b'0', '2023-08-21 11:14:52', 0, '2023-08-21 11:14:52', 0, b'1'), (1004043, 0, 1001003, 1004, 1004014, 1008, '4101', '41-410-4101', 'Unpproprited Profit', NULL, NULL, b'0', b'0', '2023-08-21 11:15:13', 0, '2023-08-21 11:15:13', 0, b'1'), (1004044, 0, 1001003, 1004, 1004015, 1008, '6201', '61-620-6201', 'Haji Iqbal', NULL, NULL, b'0', b'0', '2023-08-21 11:15:32', 0, '2023-08-21 11:15:32', 0, b'1'), (1004045, 0, 1001003, 1004, 1004015, 1008, '6202', '61-620-6202', 'Saith Mumtaz', NULL, NULL, b'0', b'0', '2023-08-21 11:16:01', 0, '2023-08-21 11:16:01', 0, b'1'), (1004046, 0, 1001003, 1004, 1004015, 1008, '6203', '61-620-6203', 'Friends Engineering', NULL, NULL, b'0', b'0', '2023-08-21 11:16:23', 0, '2023-08-21 11:16:23', 0, b'1'), (1004047, 0, 1001003, 1004, 1004016, 1008, '6791', '61-679-6791', 'Sales tax - Payable', NULL, NULL, b'0', b'0', '2023-08-21 11:16:44', 0, '2023-08-21 11:16:44', 0, b'1'), (1004048, 0, 1001003, 1004, 1004016, 1008, '6792', '61-679-6792', 'Sales tax Ret - Payable', NULL, NULL, b'0', b'0', '2023-08-21 11:17:07', 0, '2023-08-21 11:17:07', 0, b'1'), (1004049, 0, 1001003, 1004, 1004016, 1008, '6801', '61-679-6801', 'Sales tax - Further Tax', NULL, NULL, b'0', b'0', '2023-08-21 11:17:26', 0, '2023-08-21 11:17:26', 0, b'1'), (1004050, 0, 1001003, 1004, 1004016, 1008, '6901', '61-679-6901', 'Withholding - Income tax', NULL, NULL, b'0', b'0', '2023-08-21 11:17:48', 0, '2023-08-21 11:17:48', 0, b'1'), (1004051, 0, 1001003, 1004, 1004017, 1008, '7101', '71-710-7101', 'Sales - Small Packing', NULL, NULL, b'0', b'0', '2023-08-21 11:18:11', 0, '2023-08-21 11:18:11', 0, b'1'), (1004052, 0, 1001003, 1004, 1004017, 1008, '7102', '71-710-7102', 'Sales - Large Packing', NULL, NULL, b'0', b'0', '2023-08-21 11:18:32', 0, '2023-08-21 11:18:32', 0, b'1'), (1004053, 0, 1001003, 1004, 1004017, 1008, '7106', '71-710-7106', 'Commission', NULL, NULL, b'0', b'0', '2023-08-21 11:19:12', 0, '2023-08-21 11:19:12', 0, b'1'), (1004054, 0, 1001003, 1004, 1004017, 1008, '7201', '71-710-7201', 'Charges to customers', NULL, NULL, b'0', b'0', '2023-08-21 11:19:31', 0, '2023-08-21 11:19:31', 0, b'1'), (1004055, 0, 1001003, 1004, 1004018, 1008, '7401', '71-740-7401', 'Discount on Sales', NULL, NULL, b'0', b'0', '2023-08-21 11:19:52', 0, '2023-08-21 11:19:52', 0, b'1'), (1004056, 0, 1001003, 1004, 1004019, 1008, '7411', '71-741-7411', 'Bulk Discount on Sales', NULL, NULL, b'0', b'0', '2023-08-21 11:20:12', 0, '2023-08-21 11:20:12', 0, b'1'), (1004057, 0, 1001003, 1004, 1004020, 1008, '7421', '71-742-7421', 'Bulk Discount on Sales', NULL, NULL, b'0', b'0', '2023-08-21 11:21:24', 0, '2023-08-21 11:21:24', 0, b'1'), (1004058, 0, 1001003, 1004, 1004021, 1008, '7501', '75-750-7501', 'Purchases', NULL, NULL, b'0', b'0', '2023-08-21 11:21:48', 0, '2023-08-21 11:21:48', 0, b'1'), (1004059, 0, 1001003, 1004, 1004021, 1008, '7701', '75-750-7701', 'Discount on Purchases', NULL, NULL, b'0', b'0', '2023-08-21 11:22:13', 0, '2023-08-21 11:22:13', 0, b'1'), (1004060, 0, 1001003, 1004, 1004022, 1008, '8401', '84-840-8401', 'Printing & Stationery', NULL, NULL, b'0', b'0', '2023-08-21 11:22:43', 0, '2023-08-21 11:22:43', 0, b'1'), (1004061, 0, 1001003, 1004, 1004022, 1008, '8402', '84-840-8402', 'Travelling & Conveyance', NULL, NULL, b'0', b'0', '2023-08-21 11:23:01', 0, '2023-08-21 11:23:01', 0, b'1'), (1004062, 0, 1001003, 1004, 1004022, 1008, '8403', '84-840-8403', 'Vehicle Running & Maintenance', NULL, NULL, b'0', b'0', '2023-08-21 11:23:19', 0, '2023-08-21 11:23:19', 0, b'1'), (1004063, 0, 1001003, 1004, 1004022, 1008, '8404', '84-840-8404', 'Repair & Maintenance', NULL, NULL, b'0', b'0', '2023-08-21 11:23:43', 0, '2023-08-21 11:23:43', 0, b'1'), (1004064, 0, 1001003, 1004, 1004022, 1008, '8405', '84-840-8405', 'Bilty Exp', NULL, NULL, b'0', b'0', '2023-08-21 11:24:01', 0, '2023-08-21 11:24:01', 0, b'1'), (1004065, 0, 1001003, 1004, 1004023, 1008, '8501', '84-850-8501', 'Sales Promotion Expenses', NULL, NULL, b'0', b'0', '2023-08-21 11:24:20', 0, '2023-08-21 11:24:20', 0, b'1'), (1004066, 0, 1001003, 1004, 1004023, 1008, '8502', '84-850-8502', 'Uniform Exp', NULL, NULL, b'0', b'0', '2023-08-21 11:24:36', 0, '2023-08-21 11:24:36', 0, b'1'), (1004067, 0, 1001003, 1004, 1004023, 1008, '8503', '84-850-8503', 'Packing Charges', NULL, NULL, b'0', b'0', '2023-08-21 11:24:58', 0, '2023-08-21 11:24:58', 0, b'1'), (1004068, 0, 1001003, 1004, 1004023, 1008, '8504', '84-850-8504', 'Freight Charges', NULL, NULL, b'0', b'0', '2023-08-21 11:25:24', 0, '2023-08-21 11:25:24', 0, b'1'), (1007001, 1, 1001004, 1007, 0, 0, 'A+', NULL, 'A+', 'A+', NULL, b'0', b'0', '2023-11-02 11:57:20', 0, '2023-11-02 11:57:20', 0, b'1'), (1007002, 1, 1001004, 1007, 0, 0, 'B+', NULL, 'B+', 'B+', NULL, b'0', b'0', '2023-11-02 11:57:20', 0, '2023-11-02 11:57:20', 0, b'1'), (1008001, 1, 1001004, 1008, 0, 0, 'A+', NULL, 'A+', 'A+', NULL, b'0', b'0', '2023-11-02 12:06:55', 0, '2023-11-02 12:06:55', 0, b'1'), (1008002, 1, 1001004, 1008, 0, 0, 'B+', NULL, 'B+', 'B+', NULL, b'0', b'0', '2023-11-02 12:06:55', 0, '2023-11-02 12:06:55', 0, b'1'), (1009001, 0, 0, 1009, 0, 0, 'Vendor', NULL, 'Aaftab', NULL, NULL, b'0', b'0', '2023-04-06 01:40:04', 0, '2023-04-06 01:40:04', 0, b'0'), (1009002, 0, 0, 1009, 0, 0, 'Vendor_Hasan', NULL, 'Hasan', NULL, NULL, b'0', b'0', '2023-04-06 01:40:18', 0, '2023-04-06 01:40:18', 0, b'0'), (1010001, 0, 0, 1010, 0, 0, 'Salesman_Murtaza', NULL, 'Murtaza', NULL, NULL, b'0', b'0', '2023-04-06 01:40:27', 0, '2023-04-06 01:40:27', 0, b'1'), (1010002, 0, 0, 1010, 0, 0, 'Salesman_Humain', NULL, 'Humain', NULL, NULL, b'0', b'0', '2023-04-06 01:41:10', 0, '2023-04-06 01:41:10', 0, b'1'), (1010003, 0, 0, 1010, 0, 0, 'Salesman_Ali', NULL, 'Ali', NULL, NULL, b'0', b'0', '2023-04-06 01:41:24', 0, '2023-04-06 01:41:24', 0, b'1'), (1011001, 0, 0, 1011, 0, 0, 'Godown_Godown_1', NULL, 'Godown 1', NULL, NULL, b'0', b'0', '2023-04-06 01:41:48', 0, '2023-04-06 01:41:48', 0, b'1'), (1011001, 1, 1001004, 1011, 0, 0, '0-100', NULL, '0-100', '0-100', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1'), (1011002, 0, 0, 1011, 0, 0, 'Godown_Godown_2', NULL, 'Godown 2', NULL, NULL, b'0', b'0', '2023-04-06 01:41:57', 0, '2023-04-06 01:41:57', 0, b'1'), (1011002, 1, 1001004, 1011, 0, 0, '101-200', NULL, '101-200', '101-200', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1'), (1011003, 1, 1001004, 1011, 0, 0, '201-300', NULL, '201-300', '201-300', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1'), (1011004, 1, 1001004, 1011, 0, 0, '301-400_Above', NULL, '301-400 Above', '301-400 Above', NULL, b'0', b'0', '2023-12-02 15:59:07', 0, '2023-12-02 15:59:07', 0, b'1'), (1012001, 0, 0, 1012, 0, 0, 'Product_Containers', NULL, 'Containers', NULL, NULL, b'0', b'0', '2023-04-06 01:46:17', 0, '2023-04-06 01:46:17', 0, b'0'), (1012002, 0, 0, 1012, 0, 0, 'Product_Stationary_Products', NULL, 'Stationary Products', NULL, NULL, b'0', b'0', '2023-04-06 01:46:33', 0, '2023-04-06 01:46:33', 0, b'0'), (1012003, 0, 0, 1012, 0, 0, 'Product_Wood_Preservatives', NULL, 'Wood Preservatives', NULL, NULL, b'0', b'0', '2023-04-06 01:47:06', 0, '2023-04-06 01:47:06', 0, b'0'), (1012004, 0, 0, 1012, 0, 0, 'Product_Computing_Infrastructure', NULL, 'Computing Infrastructure', NULL, NULL, b'0', b'0', '2023-04-06 01:49:16', 0, '2023-04-06 01:49:16', 0, b'0'), (1013001, 0, 0, 1013, 0, 0, 'Voucher_Types_Bank_Receipt_Voucher', NULL, 'Bank Receipt Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:02', 0, '2023-04-07 19:00:02', 0, b'1'), (1013002, 0, 0, 1013, 0, 0, 'Voucher_Types_Bank_Payment_Voucher', NULL, 'Bank Payment Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:23', 0, '2023-04-07 19:00:23', 0, b'1'), (1013003, 0, 0, 1013, 0, 0, 'Voucher_Types_Cash_Receipt_Voucher', NULL, 'Cash Receipt Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:39', 0, '2023-04-07 19:00:39', 0, b'1'), (1013004, 0, 0, 1013, 0, 0, 'Voucher_Types_Cash_Payment_Voucher', NULL, 'Cash Payment Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:00:52', 0, '2023-04-07 19:00:52', 0, b'1'), (1013005, 0, 0, 1013, 0, 0, 'Voucher_Types__Journal_Voucher', NULL, ' Journal Voucher', NULL, NULL, b'1', b'0', '2023-04-07 19:01:06', 0, '2023-04-07 19:01:06', 0, b'0'), (1014001, 0, 0, 1014, 0, 0, 'Status_Draft', NULL, 'Draft', NULL, NULL, b'1', b'0', '2023-04-08 18:15:11', 0, '2023-04-08 18:15:11', 0, b'1'), (1014002, 0, 0, 1014, 0, 0, 'Status_UnPosted', NULL, 'UnPosted', NULL, NULL, b'1', b'0', '2023-04-08 18:15:27', 0, '2023-04-08 18:15:27', 0, b'1'), (1014003, 0, 0, 1014, 0, 0, 'Status_Posted', NULL, 'Posted', NULL, NULL, b'1', b'0', '2023-04-08 18:15:35', 0, '2023-04-08 18:15:35', 0, b'1'), (1015001, 0, 0, 1015, 0, 0, 'Country_America', NULL, 'America', NULL, NULL, b'1', b'0', '2023-05-03 15:43:28', 0, '2023-05-03 15:43:28', 0, b'1'), (1015001, 1, 1001004, 1015, 0, 0, 'Pakistan', NULL, 'Pakistan', 'Pakistan', NULL, b'0', b'0', '2023-10-05 12:49:59', 0, '2023-10-05 12:49:59', 0, b'1'), (1015001, 3, 1001004, 1015, 0, 0, 'Pakistan', NULL, 'Pakistan', 'Pakistan', NULL, b'0', b'0', '2023-10-05 13:15:05', 0, '2023-10-05 13:15:05', 0, b'1'), (1015002, 0, 0, 1015, 0, 0, 'Country_Canada', NULL, 'Canada', NULL, NULL, b'1', b'0', '2023-05-03 15:43:40', 0, '2023-05-03 15:43:40', 0, b'1'), (1015002, 1, 1001004, 1015, 0, 0, 'India', NULL, 'India', 'India', NULL, b'0', b'0', '2023-10-05 12:58:50', 0, '2023-10-05 12:58:50', 0, b'1'), (1015003, 0, 0, 1015, 0, 0, 'Country_India', NULL, 'India', NULL, NULL, b'1', b'0', '2023-05-03 15:43:49', 0, '2023-05-03 15:43:49', 0, b'1'), (1015004, 0, 0, 1015, 0, 0, 'Country_Pakistan', NULL, 'Pakistan', 'Pakistan', NULL, b'1', b'0', '2023-05-03 15:44:32', 0, '2023-05-03 15:44:32', 0, b'1'), (1016001, 0, 0, 1016, 1015001, 0, 'Austin...', NULL, 'Austin...', 'Austin...', NULL, b'1', b'0', '2023-05-03 15:45:25', 0, '2023-05-03 15:45:25', 0, b'1'), (1016001, 1, 1001004, 1016, 1015001, 0, 'Lahore', NULL, 'Lahore', 'Lahore', NULL, b'0', b'0', '2023-10-05 12:50:28', 0, '2023-10-05 12:50:28', 0, b'1'), (1016001, 3, 1001004, 1016, 1015001, 0, 'Lahore', NULL, 'Lahore', 'Lahore', NULL, b'0', b'0', '2023-10-05 13:15:15', 0, '2023-10-05 13:15:15', 0, b'1'), (1016002, 0, 0, 1016, 1015001, 0, 'City_Chichago', NULL, 'Chichago', NULL, NULL, b'1', b'0', '2023-05-03 15:46:03', 0, '2023-05-03 15:46:03', 0, b'1'), (1016002, 1, 1001004, 1016, 1015002, 0, 'Mombai', NULL, 'Mombai', 'Mombai', NULL, b'0', b'0', '2023-10-05 12:59:08', 0, '2023-10-05 12:59:08', 0, b'1'), (1016003, 0, 0, 1016, 1015001, 0, 'City_New_York', NULL, 'New York', NULL, NULL, b'1', b'0', '2023-05-03 15:46:20', 0, '2023-05-03 15:46:20', 0, b'1'), (1016004, 0, 0, 1016, 1015002, 0, 'City_Ottawa', NULL, 'Ottawa', NULL, NULL, b'1', b'0', '2023-05-03 15:47:14', 0, '2023-05-03 15:47:14', 0, b'1'), (1016005, 0, 0, 1016, 1015002, 0, 'City_Toronto', NULL, 'Toronto', NULL, NULL, b'1', b'0', '2023-05-03 15:47:27', 0, '2023-05-03 15:47:27', 0, b'1'), (1016006, 0, 0, 1016, 1015003, 0, 'City_Mumbai', NULL, 'Mumbai', NULL, NULL, b'1', b'0', '2023-05-03 15:47:51', 0, '2023-05-03 15:47:51', 0, b'1'), (1016007, 0, 0, 1016, 1015003, 0, 'City_Kolkata', NULL, 'Kolkata', NULL, NULL, b'1', b'0', '2023-05-03 15:48:10', 0, '2023-05-03 15:48:10', 0, b'1'), (1016008, 0, 0, 1016, 1015003, 0, 'City_Jaipur', NULL, 'Jaipur', NULL, NULL, b'1', b'0', '2023-05-03 15:48:28', 0, '2023-05-03 15:48:28', 0, b'1'), (1016009, 0, 0, 1016, 1015003, 0, 'City_Chennal', NULL, 'Chennal', NULL, NULL, b'1', b'0', '2023-05-03 15:48:46', 0, '2023-05-03 15:48:46', 0, b'1'), (1016010, 0, 0, 1016, 1015003, 0, 'City_Tawang', NULL, 'Tawang', NULL, NULL, b'1', b'0', '2023-05-03 15:49:18', 0, '2023-05-03 15:49:18', 0, b'1'), (1016011, 0, 0, 1016, 1015004, 0, 'City_Lahore', NULL, 'Lahore', NULL, NULL, b'1', b'0', '2023-05-03 15:49:32', 0, '2023-05-03 15:49:32', 0, b'1'), (1016012, 0, 0, 1016, 1015004, 0, 'City_Karachi', NULL, 'Karachi', NULL, NULL, b'1', b'0', '2023-05-03 15:49:45', 0, '2023-05-03 15:49:45', 0, b'1'), (1016013, 0, 0, 1016, 1015004, 0, 'City_Islamabad', NULL, 'Islamabad', NULL, NULL, b'1', b'0', '2023-05-03 15:50:11', 0, '2023-05-03 15:50:11', 0, b'1'), (1016014, 0, 0, 1016, 1015004, 0, 'City_Faisalabad', NULL, 'Faisalabad', NULL, NULL, b'1', b'0', '2023-05-03 15:50:39', 0, '2023-05-03 15:50:39', 0, b'1'), (1016015, 0, 0, 1016, 1015004, 0, 'City_Multan', NULL, 'Multan', NULL, NULL, b'1', b'0', '2023-05-03 15:50:50', 0, '2023-05-03 15:50:50', 0, b'1'), (1016016, 0, 0, 1016, 1015004, 0, 'City_Hyderabad', NULL, 'Hyderabad', NULL, NULL, b'1', b'0', '2023-05-03 15:51:20', 0, '2023-05-03 15:51:20', 0, b'0'), (1016019, 0, 0, 1016, 1015004, 0, 'Quetta', NULL, 'Quetta', 'Quetta', NULL, b'0', b'0', '2024-03-20 05:23:31', 0, '2024-03-20 05:23:31', 0, b'1'), (1016020, 0, 0, 1016, 1015004, 0, 'Peshawar', NULL, 'Peshawar', 'Peshawar', NULL, b'0', b'0', '2024-03-20 05:26:33', 0, '2024-03-20 05:26:33', 0, b'1'), (1017001, 0, 0, 1017, 0, 0, '', NULL, 'Color', NULL, NULL, b'0', b'0', '2023-07-17 19:33:17', 0, '2023-07-17 19:33:17', 0, b'1'), (1017002, 0, 0, 1017, 0, 0, '', NULL, 'Brand', NULL, NULL, b'0', b'0', '2023-07-17 19:33:21', 0, '2023-07-17 19:33:21', 0, b'1'), (1017003, 0, 0, 1017, 0, 0, '', NULL, 'Size', NULL, NULL, b'0', b'0', '2023-07-17 19:33:26', 0, '2023-07-17 19:33:26', 0, b'1'), (1017004, 0, 0, 1017, 0, 0, '', NULL, 'Legs', NULL, NULL, b'0', b'0', '2023-07-17 19:33:30', 0, '2023-07-17 19:33:30', 0, b'1'), (1017005, 0, 0, 1017, 0, 0, '', NULL, 'Appearance', NULL, NULL, b'0', b'0', '2023-08-21 11:49:38', 0, '2023-08-21 11:49:38', 0, b'1'), (1018001, 0, 0, 1018, 1017001, 0, 'Black', NULL, 'Black', NULL, NULL, b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1'), (1018002, 0, 0, 1018, 1017001, 0, 'Brown', NULL, 'Brown', NULL, NULL, b'0', b'0', '2023-07-17 19:49:18', 0, '2023-07-17 19:49:18', 0, b'1'), (1018003, 0, 0, 1018, 1017001, 0, 'Blue', NULL, 'Blue', NULL, NULL, b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1'), (1018004, 0, 0, 1018, 1017001, 0, 'Red', NULL, 'Red', NULL, NULL, b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1'), (1018005, 0, 0, 1018, 1017001, 0, 'White', NULL, 'White', NULL, 'des', b'0', b'0', '2023-07-17 19:47:57', 0, '2023-07-17 19:47:57', 0, b'1'), (1018006, 0, 0, 1018, 1017003, 0, 'L', NULL, 'Large', NULL, NULL, b'0', b'0', '2023-07-17 20:23:23', 0, '2023-07-17 20:23:23', 0, b'1'), (1018007, 0, 0, 1018, 1017003, 0, 'M', NULL, 'Medium', NULL, NULL, b'0', b'0', '2023-07-17 20:23:31', 0, '2023-07-17 20:23:31', 0, b'1'), (1018008, 0, 0, 1018, 1017003, 0, 'S', NULL, 'Small', NULL, NULL, b'0', b'0', '2023-07-17 20:23:40', 0, '2023-07-17 20:23:40', 0, b'1'), (1018009, 0, 0, 1018, 1017004, 0, 'Steel', NULL, 'Steel', NULL, NULL, b'0', b'0', '2023-07-18 01:16:46', 0, '2023-07-18 01:16:46', 0, b'1'), (1018010, 0, 0, 1018, 1017004, 0, 'Platinum', NULL, 'Platinum', NULL, NULL, b'0', b'0', '2023-07-18 01:16:57', 0, '2023-07-18 01:16:57', 0, b'1'), (1018011, 0, 0, 1018, 1017002, 0, 'Hoid', NULL, 'Hoid', NULL, NULL, b'0', b'0', '2023-07-21 03:06:06', 0, '2023-07-21 03:06:06', 0, b'1'), (1018012, 0, 0, 1018, 1017002, 0, 'Furniture Hub', NULL, 'Furniture Hub', NULL, NULL, b'0', b'0', '2023-07-21 03:06:19', 0, '2023-07-21 03:06:19', 0, b'1'), (1018013, 0, 0, 1018, 1017002, 0, 'Index Furniture', NULL, 'Index Furniture', NULL, NULL, b'0', b'0', '2023-07-21 03:06:32', 0, '2023-07-21 03:06:32', 0, b'1'), (1018014, 0, 0, 1018, 1017005, 0, 'Granulated_Sugar', NULL, 'Granulated Sugar', NULL, NULL, b'0', b'0', '2023-08-21 11:50:09', 0, '2023-08-21 11:50:09', 0, b'1'), (1018015, 0, 0, 1018, 1017005, 0, 'Powdered_Sugar', NULL, 'Powdered Sugar', NULL, NULL, b'0', b'0', '2023-08-21 11:50:22', 0, '2023-08-21 11:50:22', 0, b'1'), (1019001, 0, 0, 1019, 0, 0, '', NULL, 'Kg', NULL, NULL, b'0', b'0', '2023-08-21 09:42:36', 0, '2023-08-21 09:42:36', 0, b'1'), (1019002, 0, 0, 1019, 0, 0, '', NULL, 'Box', NULL, NULL, b'0', b'0', '2023-08-21 09:42:42', 0, '2023-08-21 09:42:42', 0, b'1'), (1019003, 0, 0, 1019, 0, 0, '', NULL, 'Carton', NULL, NULL, b'0', b'0', '2023-08-21 09:42:48', 0, '2023-08-21 09:42:48', 0, b'1'), (1019004, 0, 0, 1019, 0, 0, '', NULL, 'Bag', NULL, NULL, b'0', b'0', '2023-08-21 09:43:03', 0, '2023-08-21 09:43:03', 0, b'1'), (1019005, 0, 0, 1019, 0, 0, '', NULL, 'Packet', NULL, NULL, b'0', b'0', '2023-08-21 09:43:10', 0, '2023-08-21 09:43:10', 0, b'1'), (1020001, 0, 0, 1020, 0, 0, 'UOMTypes_Sale_UOM', NULL, 'Sale UOM', 'Sale UOM', NULL, b'1', b'0', '2023-08-21 09:46:48', 0, '2023-08-21 09:46:48', 0, b'1'), (1020002, 0, 0, 1020, 0, 0, 'UOMTypes_Purchase_UOM', NULL, 'Purchase UOM', 'Purchase UOM', NULL, b'1', b'0', '2023-08-21 09:47:09', 0, '2023-08-21 09:47:09', 0, b'1'), (1020003, 0, 0, 1020, 0, 0, 'UOMTypes_Inventory_UOM', NULL, 'Inventory UOM', 'Inventory UOM', NULL, b'1', b'0', '2023-08-21 09:47:31', 0, '2023-08-21 09:47:31', 0, b'1'), (1022001, 0, 0, 1022, 0, 0, 'Tax_GST', NULL, 'GST', 'GST', NULL, b'1', b'0', '2023-08-21 09:59:07', 0, '2023-08-21 09:59:07', 0, b'1'), (1022002, 0, 0, 1022, 0, 0, 'Tax_General_Sale_Tax', NULL, 'General Sale Tax', 'General Sale Tax', NULL, b'0', b'0', '2023-08-21 09:59:43', 0, '2023-08-21 09:59:43', 0, b'1'), (1023001, 0, 0, 1023, 0, 0, 'Freight_By_Road_Freight', NULL, 'By Road Freight', 'By Road Freight', NULL, b'1', b'0', '2023-08-21 10:01:42', 0, '2023-08-21 10:01:42', 0, b'1'), (1023002, 0, 0, 1023, 0, 0, 'Freight_Ocean_Freight', NULL, 'Ocean Freight', 'Ocean Freight', NULL, b'0', b'0', '2023-08-21 10:02:04', 0, '2023-08-21 10:02:04', 0, b'1'), (1023003, 0, 0, 1023, 0, 0, 'Freight_By_Rail_Freight', NULL, 'By Rail Freight', 'By Rail Freight', NULL, b'1', b'0', '2023-08-21 10:02:28', 0, '2023-08-21 10:02:28', 0, b'1'), (1023004, 0, 0, 1023, 0, 0, 'Freight_By_Air_Freight', NULL, 'By Air Freight', 'By Air Freight', NULL, b'1', b'0', '2023-08-21 10:02:46', 0, '2023-08-21 10:02:46', 0, b'1'), (1024001, 0, 0, 1024, 0, 0, 'Discount_Invoice_Discount', NULL, 'Invoice Discount', 'Invoice Discount', NULL, b'1', b'0', '2023-08-21 10:00:16', 0, '2023-08-21 10:00:16', 0, b'1'), (1024002, 0, 0, 1024, 0, 0, 'Discount_Line_Discount', NULL, 'Line Discount', 'Line Discount', NULL, b'1', b'0', '2023-08-21 10:00:34', 0, '2023-08-21 10:00:34', 0, b'1'), (1025001, 0, 0, 1025, 0, 0, 'DocExtrasIncDecTypes_Increament', NULL, 'Increament', 'Increament', NULL, b'1', b'0', '2023-08-21 10:04:07', 0, '2023-08-21 10:04:07', 0, b'1'), (1025002, 0, 0, 1025, 0, 0, 'DocExtrasIncDecTypes_Decreament', NULL, 'Decreament', 'Decreament', NULL, b'1', b'0', '2023-08-21 10:04:23', 0, '2023-08-21 10:04:23', 0, b'1'), (1026001, 0, 0, 1026, 0, 0, 'DocExtraFormulas_Formula_1', NULL, 'Formula 1', 'Formula 1', NULL, b'1', b'0', '2023-08-21 10:04:39', 0, '2023-08-21 10:04:39', 0, b'1'), (1026002, 0, 0, 1026, 0, 0, 'DocExtraFormulas_Formula_2', NULL, 'Formula 2', 'Formula 2', NULL, b'1', b'0', '2023-08-21 10:04:50', 0, '2023-08-21 10:04:50', 0, b'1'), (1028001, 0, 0, 1028, 0, 0, 'Product_Taxes_GST_Sale_Rate', NULL, 'GST Sale Rate', 'GST Sale Rate', NULL, b'1', b'0', '2023-08-21 10:10:06', 0, '2023-08-21 10:10:06', 0, b'1'), (1028002, 0, 0, 1028, 0, 0, 'Product_Taxes_GST_Purchase_Rate', NULL, 'GST Purchase Rate', 'GST Purchase Rate', NULL, b'1', b'0', '2023-08-21 10:10:27', 0, '2023-08-21 10:10:27', 0, b'1'), (1028003, 0, 0, 1028, 0, 0, 'Product_Taxes_Wht_Rate', NULL, 'Wht Rate', 'Wht Rate', NULL, b'1', b'0', '2023-08-21 10:10:47', 0, '2023-08-21 10:10:47', 0, b'1'), (1028004, 0, 0, 1028, 0, 0, 'Product_Taxes_F.Tax_Rate', NULL, 'F.Tax Rate', 'F.Tax Rate', NULL, b'1', b'0', '2023-08-21 10:11:19', 0, '2023-08-21 10:11:19', 0, b'1'), (1028005, 0, 0, 1028, 0, 0, 'Product_Taxes_Gst_Retail_Rate', NULL, 'Gst Retail Rate', 'Gst Retail Rate', NULL, b'1', b'0', '2023-08-21 10:11:43', 0, '2023-08-21 10:11:43', 0, b'1'), (1029001, 0, 0, 1029, 0, 0, 'Item_Types_Small_Packing', NULL, 'Small Packing', 'Small Packing', NULL, b'1', b'0', '2023-08-21 10:19:01', 0, '2023-08-21 10:19:01', 0, b'1'), (1029002, 0, 0, 1029, 0, 0, 'Item_Types_Large_Paking', NULL, 'Large Packing', 'Large Packing', NULL, b'1', b'0', '2023-08-21 10:19:17', 0, '2023-08-21 10:19:17', 0, b'1'), (1030001, 0, 0, 1030, 0, 0, 'Documents_Sale_Order', NULL, 'Sale Order', 'Sale Order', NULL, b'1', b'0', '2023-08-21 10:22:30', 0, '2023-08-21 10:22:30', 0, b'1'), (1030002, 0, 0, 1030, 0, 0, 'Documents_Sale_Invoice', NULL, 'Sale Invoice', 'Sale Invoice', NULL, b'1', b'0', '2023-08-21 10:22:46', 0, '2023-08-21 10:22:46', 0, b'1'), (1030003, 0, 0, 1030, 0, 0, 'Documents_Purchase_Order', NULL, 'Purchase Order', 'Purchase Order', NULL, b'1', b'0', '2023-08-21 10:23:06', 0, '2023-08-21 10:23:06', 0, b'1'), (1030004, 0, 0, 1030, 0, 0, 'Documents_Purchase_Invoice', NULL, 'Purchase Invoice', 'Purchase Invoice', NULL, b'1', b'0', '2023-08-21 10:23:22', 0, '2023-08-21 10:23:22', 0, b'1'), (1031001, 0, 0, 1031, 1030001, 0, 'Accounts_Gross_Sale', NULL, 'Gross Sale', 'Gross Sale', '1034001', b'1', b'0', '2023-08-21 10:25:26', 0, '2023-08-21 10:25:26', 0, b'1'), (1031002, 0, 0, 1031, 1030001, 0, 'Accounts_Sale_Discount', NULL, 'Sale Discount', '1004055', '1034002', b'1', b'0', '2023-08-21 10:30:26', 0, '2023-08-21 10:30:26', 0, b'1'), (1031003, 0, 0, 1031, 1030001, 0, 'Accounts_Bulk_Discount', NULL, 'Bulk Discount', '1004056', '1034002', b'1', b'0', '2023-08-21 10:30:56', 0, '2023-08-21 10:30:56', 0, b'1'), (1031004, 0, 0, 1031, 1030001, 0, 'Accounts_Qty_Discount', NULL, 'Qty Discount', '1004057', '1034002', b'1', b'0', '2023-08-21 10:31:19', 0, '2023-08-21 10:31:19', 0, b'1'), (1031005, 0, 0, 1031, 1030001, 0, 'Accounts_Sale_GST', NULL, 'Sale GST', '1004047', '1034001', b'1', b'0', '2023-08-21 10:31:44', 0, '2023-08-21 10:31:44', 0, b'1'), (1031006, 0, 0, 1031, 1030001, 0, 'Accounts_Sale_GST_Ret', NULL, 'Sale GST Ret', '1004048', '1034001', b'1', b'0', '2023-08-21 10:32:09', 0, '2023-08-21 10:32:09', 0, b'1'), (1031007, 0, 0, 1031, 1030001, 0, 'Accounts_Further_TAX', NULL, 'Further TAX', '1004049', '1034001', b'1', b'0', '2023-08-21 10:33:08', 0, '2023-08-21 10:33:08', 0, b'1'), (1031008, 0, 0, 1031, 1030001, 0, 'Accounts_Withholding', NULL, 'Withholding', '1004049', '1043001', b'1', b'0', '2023-08-21 10:33:59', 0, '2023-08-21 10:33:59', 0, b'1'), (1031009, 0, 0, 1031, 1030001, 0, 'Accounts_Charges_Add', NULL, 'Charges Add', '1004054', '1034001', b'1', b'0', '2023-08-21 10:34:42', 0, '2023-08-21 10:34:42', 0, b'1'), (1031010, 0, 0, 1031, 1030001, 0, 'Accounts_Charges_Less', NULL, 'Charges Less', '1004054', '1034002', b'1', b'0', '2023-08-21 10:35:25', 0, '2023-08-21 10:35:25', 0, b'1'), (1031011, 0, 0, 1031, 1030001, 0, 'Accounts_Packing_Charges', NULL, 'Packing Charges', '1004067', '1034001', b'1', b'0', '2023-08-21 10:35:54', 0, '2023-08-21 10:35:54', 0, b'1'), (1031012, 0, 0, 1031, 1030001, 0, 'Accounts_Freight_Charges', NULL, 'Freight Charges', '1004068', '1034002', b'1', b'0', '2023-08-21 10:36:21', 0, '2023-08-21 10:36:21', 0, b'1'), (1031013, 0, 0, 1031, 1030001, 0, 'Accounts_Invoice_Discount', NULL, 'Invoice Discount', '1004055', '1034002', b'1', b'0', '2023-08-21 10:36:56', 0, '2023-08-21 10:36:56', 0, b'1'), (1032001, 0, 0, 1032, 1031001, 0, 'Large_Packing_Gross_Sale_(Large_Packing)', NULL, 'Gross Sale (Large Packing)', '1004052', NULL, b'1', b'0', '2023-08-21 10:26:14', 0, '2023-08-21 10:26:14', 0, b'1'), (1033001, 0, 0, 1033, 1031001, 0, 'Small_Packing_Gross_Sale_(Small_Packing)', NULL, 'Gross Sale (Small Packing)', '1004051', NULL, b'1', b'0', '2023-08-21 10:27:01', 0, '2023-08-21 10:27:01', 0, b'1'), (1034001, 0, 0, 1034, 0, 0, 'Transaction_Types_Credit', NULL, 'Credit', 'Credit', NULL, b'1', b'0', '2023-08-21 10:27:38', 0, '2023-08-21 10:27:38', 0, b'1'), (1034002, 0, 0, 1034, 0, 0, 'Transaction_Types_Debit', NULL, 'Debit', 'Debit', NULL, b'1', b'0', '2023-08-21 10:27:48', 0, '2023-08-21 10:27:48', 0, b'1'), (1035001, 0, 0, 1035, 0, 0, 'Gender_Male', NULL, 'Male', 'Male', NULL, b'1', b'0', '2023-08-30 09:29:13', 0, '2023-08-30 09:29:13', 0, b'1'), (1035002, 0, 0, 1035, 0, 0, 'Gender_Female', NULL, 'Female', 'Female', NULL, b'1', b'0', '2023-08-30 09:29:23', 0, '2023-08-30 09:29:23', 0, b'1'), (1035003, 0, 0, 1035, 0, 0, 'Gender_Other', NULL, 'Other', 'Other', NULL, b'1', b'0', '2023-08-30 09:29:32', 0, '2023-08-30 09:29:32', 0, b'1'), (1036001, 0, 0, 1036, 1016011, 0, 'Areas_Defense', NULL, 'Defense', 'Defense', NULL, b'0', b'0', '2023-08-30 12:14:29', 0, '2023-08-30 12:14:29', 0, b'1'), (1036001, 1, 1001004, 1036, 1016001, 0, 'Iqbal_Town', NULL, 'Iqbal Town', 'Iqbal Town', NULL, b'0', b'0', '2023-10-05 12:52:13', 0, '2023-10-05 12:52:13', 0, b'1'), (1036001, 3, 1001004, 1036, 1016001, 0, 'Shalimar_Town', NULL, 'Shalimar Town', 'Shalimar Town', NULL, b'0', b'0', '2023-10-05 13:15:43', 0, '2023-10-05 13:15:43', 0, b'1'), (1036002, 0, 0, 1036, 1016012, 0, 'Areas_Karachi_Central', NULL, 'Karachi Central', 'Karachi Central', NULL, b'1', b'0', '2023-08-30 12:15:16', 0, '2023-08-30 12:15:16', 0, b'1'), (1036002, 1, 1001004, 1036, 1016002, 0, 'Eksar_Colony', NULL, 'Eksar Colony', 'Eksar Colony', NULL, b'0', b'0', '2023-10-05 12:59:19', 0, '2023-10-05 12:59:19', 0, b'1'), (1036003, 0, 0, 1036, 1016011, 0, 'Areas_Johar_Town', NULL, 'Johar Town', 'Johar Town', NULL, b'1', b'0', '2023-09-01 18:42:41', 0, '2023-09-01 18:42:41', 0, b'1'), (1036004, 0, 0, 1036, 1016011, 0, 'Areas_Aziz_Abad', NULL, 'Aziz Abad', 'Aziz Abad', NULL, b'1', b'0', '2023-09-03 15:24:35', 0, '2023-09-03 15:24:35', 0, b'1'), (1036005, 0, 0, 1036, 1016011, 0, 'Areas_Qaisar', NULL, 'Qaisar', 'Qaisar', NULL, b'1', b'0', '2023-09-03 15:24:54', 0, '2023-09-03 15:24:54', 0, b'1'), (1036006, 0, 0, 1036, 1016011, 0, 'Areas_Dhair', NULL, 'Dhair', 'Dhair', NULL, b'1', b'0', '2023-09-03 15:25:20', 0, '2023-09-03 15:25:20', 0, b'1'), (1037001, 0, 0, 1037, 0, 0, 'Field_Types_Text_Box', NULL, 'Text Box', 'Text Box', NULL, b'1', b'0', '2023-08-30 19:20:18', 0, '2023-08-30 19:20:18', 0, b'1'), (1037002, 0, 0, 1037, 0, 0, 'Field_Types_Text_Area', NULL, 'Text Area', 'Text Area', NULL, b'1', b'0', '2023-08-30 19:20:28', 0, '2023-08-30 19:20:28', 0, b'1'), (1037003, 0, 0, 1037, 0, 0, 'Field_Types_Drop_Down', NULL, 'Drop Down', 'Drop Down', NULL, b'1', b'0', '2023-08-30 19:20:39', 0, '2023-08-30 19:20:39', 0, b'1'), (1037004, 0, 0, 1037, 0, 0, 'Field_Types_Radio_Button', NULL, 'Radio Button', 'Radio Button', NULL, b'1', b'0', '2023-08-30 19:20:50', 0, '2023-08-30 19:20:50', 0, b'1'), (1037005, 0, 0, 1037, 0, 0, 'Field_Types_Check_Box', NULL, 'Check Box', 'Check Box', NULL, b'1', b'0', '2023-08-30 19:21:08', 0, '2023-08-30 19:21:08', 0, b'1'), (1047001, 1, 1001004, 1047, 0, 0, 'Before.....', NULL, 'Before.....', 'Before.....', NULL, b'0', b'0', '2023-09-27 10:12:18', 0, '2023-09-27 10:12:18', 0, b'1'), (1047001, 3, 1001004, 1047, 0, 0, '_Before.', NULL, 'Before', 'Before.', NULL, b'0', b'0', '2023-09-27 10:12:18', 0, '2023-09-27 10:12:18', 0, b'1'), (1047002, 1, 1001004, 1047, 0, 0, 'After', NULL, 'After', 'After', NULL, b'0', b'0', '2023-11-09 15:42:58', 0, '2023-11-09 15:42:58', 0, b'1'), (1047002, 3, 1001004, 1047, 0, 0, '', NULL, 'After', NULL, NULL, b'0', b'0', '2023-09-27 09:55:57', 0, '2023-09-27 09:55:57', 0, b'1'), (1048001, 1, 1001004, 1048, 0, 0, '', NULL, 'میٹھی چیزوں سے پرہیز کریں', NULL, NULL, b'0', b'0', '2023-09-27 10:22:00', 0, '2023-09-27 10:22:00', 0, b'1'), (1048001, 3, 1001004, 1048, 0, 0, '', NULL, 'باقاعدگی سے ورزش کریں', NULL, NULL, b'0', b'0', '2023-09-27 10:21:18', 0, '2023-09-27 10:21:18', 0, b'1'), (1048002, 1, 1001004, 1048, 0, 0, 'باقاعدگی_سے_ورزش_کریں', NULL, 'باقاعدگی سے ورزش کریں', 'باقاعدگی سے ورزش کریں', NULL, b'0', b'0', '2023-10-31 01:22:03', 0, '2023-10-31 01:22:03', 0, b'1'), (1048003, 1, 1001004, 1048, 0, 0, 'پھل_اور_سبزیاں_کھائیں', NULL, 'پھل اور سبزیاں کھائیں', 'پھل اور سبزیاں کھائیں', NULL, b'0', b'0', '2023-10-31 01:22:16', 0, '2023-10-31 01:22:16', 0, b'1'), (1049001, 1, 1001004, 1049, 0, 0, 'RCB', NULL, 'RCB', 'RCB', NULL, b'0', b'0', '2023-10-11 23:27:38', 0, '2023-10-11 23:27:38', 0, b'1'), (1049001, 3, 1001004, 1049, 0, 0, '', NULL, 'RCB', NULL, NULL, b'0', b'0', '2023-09-27 10:31:57', 0, '2023-09-27 10:31:57', 0, b'1'), (1049002, 1, 1001004, 1049, 0, 0, 'CBC', NULL, 'CBC', 'CBC', NULL, b'0', b'0', '2023-11-02 12:12:11', 0, '2023-11-02 12:12:11', 0, b'1'), (1049002, 3, 1001004, 1049, 0, 0, 'XRAY', NULL, 'XRAY', 'XRAY', NULL, b'0', b'0', '2023-10-02 22:46:19', 0, '2023-10-02 22:46:19', 0, b'1'), (1050001, 1, 1001004, 1050, 0, 0, 'remarks', NULL, 'remarks', 'remarks', NULL, b'0', b'0', '2023-10-02 12:37:24', 0, '2023-10-02 12:37:24', 0, b'1'), (1050001, 3, 1001004, 1050, 0, 0, 'Remarks.....', NULL, 'Remarks.....', 'Remarks.....', NULL, b'0', b'0', '2023-10-02 22:53:37', 0, '2023-10-02 22:53:37', 0, b'1'), (1050002, 1, 1001004, 1050, 0, 0, 'Lorem_ipsum_dolor_sit_amet,_consectetur_adipiscing_elit,_sed_do_eiusst_laborum.', NULL, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusst laborum.', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusst laborum.', NULL, b'0', b'0', '2023-11-27 20:07:09', 0, '2023-11-27 20:07:09', 0, b'1'), (1051001, 0, 0, 1051, 0, 0, '', NULL, 'Clinic', NULL, NULL, b'0', b'0', '2023-09-27 11:07:23', 0, '2023-09-27 11:07:23', 0, b'1'), (1051002, 0, 0, 1051, 0, 0, 'Software_Development', NULL, 'Software Development', 'Software Development', NULL, b'0', b'0', '2023-09-27 11:07:34', 0, '2023-09-27 11:07:34', 0, b'1'), (1051003, 0, 0, 1051, 0, 0, 'Business', NULL, 'Business', 'Business', NULL, b'0', b'0', '2023-12-02 15:16:32', 0, '2023-12-02 15:16:32', 0, b'1'), (1052001, 1, 1001004, 1052, 0, 0, 'Painless', NULL, 'Painless', 'Painless', NULL, b'0', b'0', '2023-10-02 22:38:43', 0, '2023-10-02 22:38:43', 0, b'1'), (1052001, 3, 1001004, 1052, 0, 0, 'Antibiotic', NULL, 'Antibiotic', 'Antibiotic', NULL, b'0', b'0', '2023-10-02 22:03:29', 0, '2023-10-02 22:03:29', 0, b'1'), (1052002, 1, 1001004, 1052, 0, 0, 'Sulpha', NULL, 'Sulpha', 'Sulpha', NULL, b'0', b'0', '2023-11-09 15:41:49', 0, '2023-11-09 15:41:49', 0, b'1'), (1052002, 3, 1001004, 1052, 0, 0, 'Sulpha', NULL, 'Sulpha', 'Sulpha', NULL, b'0', b'0', '2023-10-02 22:24:11', 0, '2023-10-02 22:24:11', 0, b'1'), (1053001, 1, 1001004, 1053, 0, 0, 'Venus', NULL, 'Venus', 'Venus', NULL, b'0', b'0', '2023-10-02 22:38:23', 0, '2023-10-02 22:38:23', 0, b'1'), (1053001, 3, 1001004, 1053, 0, 0, 'Glitz', NULL, 'Glitz', 'Glitz', NULL, b'0', b'0', '2023-10-02 22:03:14', 0, '2023-10-02 22:03:14', 0, b'1'), (1053002, 1, 1001004, 1053, 0, 0, 'Glaxo', NULL, 'Glaxo', 'Glaxo', NULL, b'0', b'0', '2023-11-09 15:41:32', 0, '2023-11-09 15:41:32', 0, b'1'), (1053002, 3, 1001004, 1053, 0, 0, 'Glaxo', NULL, 'Glaxo', 'Glaxo', NULL, b'0', b'0', '2023-10-02 22:23:45', 0, '2023-10-02 22:23:45', 0, b'1'), (1054001, 0, 0, 1054, 0, 0, 'Roles_Super_Admin', NULL, 'Super Admin', 'Super Admin', NULL, b'1', b'0', '2023-09-27 11:27:12', 0, '2023-09-27 11:27:12', 0, b'1'), (1054001, 1, 0, 1054, 0, 0, 'Employee', NULL, 'Employee', 'Employee', NULL, b'0', b'0', '2024-03-05 01:06:39', 0, '2024-03-05 01:06:39', 0, b'1'), (1054001, 2, 0, 1054, 0, 0, 'Employee..', NULL, 'Employee..', 'Employee..', NULL, b'0', b'0', '2024-03-05 00:59:42', 0, '2024-03-05 00:59:42', 0, b'1'), (1054001, 3, 0, 1054, 0, 0, 'Student', NULL, 'Student', 'Student', NULL, b'0', b'0', '2024-03-05 06:52:34', 0, '2024-03-05 06:52:34', 0, b'1'), (1054001, 4, 0, 1054, 0, 0, 'Admin', NULL, 'Admin', 'Admin', NULL, b'0', b'0', '2024-03-19 20:30:14', 0, '2024-03-19 20:30:14', 0, b'1'), (1054002, 0, 0, 1054, 0, 0, 'Client_Admin', NULL, 'Client Admin', 'Client Admin', NULL, b'1', b'0', '2023-09-27 11:27:29', 0, '2023-09-27 11:27:29', 0, b'1'), (1054002, 1, 0, 1054, 0, 0, 'Student', NULL, 'Student', 'Student', NULL, b'0', b'0', '2024-03-14 13:49:47', 0, '2024-03-14 13:49:47', 0, b'1'), (1054002, 2, 0, 1054, 0, 0, 'Student', NULL, 'Student', 'Student', NULL, b'0', b'0', '2024-03-05 00:59:50', 0, '2024-03-05 00:59:50', 0, b'1'), (1054003, 1, 0, 1054, 0, 0, 'Admin', NULL, 'Admin', 'Admin', NULL, b'0', b'0', '2024-03-19 06:08:53', 0, '2024-03-19 06:08:53', 0, b'1'), (1055001, 0, 0, 1055, 0, 0, 'AppStatus_Waiting', NULL, 'Waiting', 'Waiting', NULL, b'1', b'0', '2023-10-17 11:43:31', 0, '2023-10-17 11:43:31', 0, b'1'), (1055002, 0, 0, 1055, 0, 0, 'AppStatus', NULL, 'Due', 'Due', NULL, b'1', b'0', '2023-10-17 11:44:11', 0, '2023-10-17 11:44:11', 0, b'1'), (1055003, 0, 0, 1055, 0, 0, 'AppStatus_Cancled', NULL, 'Canceled', 'Canceled', NULL, b'1', b'0', '2023-10-17 11:44:22', 0, '2023-10-17 11:44:22', 0, b'1'), (1055004, 0, 0, 1055, 0, 0, 'AppStatus_Closed', NULL, 'Closed', 'Closed', NULL, b'1', b'0', '2023-10-17 11:44:31', 0, '2023-10-17 11:44:31', 0, b'1'), (1056001, 0, 0, 1056, 0, 0, 'Permissions_Deny', NULL, 'Deny', 'Deny', NULL, b'1', b'0', '2023-10-24 09:47:36', 0, '2023-10-24 09:47:36', 0, b'1'), (1056002, 0, 0, 1056, 0, 0, 'Permissions_View_Only', NULL, 'View Only', 'View Only', NULL, b'1', b'0', '2023-10-24 09:47:51', 0, '2023-10-24 09:47:51', 0, b'1'), (1056003, 0, 0, 1056, 0, 0, 'Permissions_Full_Access', NULL, 'Full Access', 'Full Access', NULL, b'1', b'0', '2023-10-24 09:48:05', 0, '2023-10-24 09:48:05', 0, b'1'), (1057001, 0, 0, 1057, 0, 0, 'BP_Statuses', NULL, 'Low', 'Low', NULL, b'0', b'0', '2023-12-04 19:31:54', 0, '2023-12-04 19:31:54', 0, b'1'), (1057002, 0, 0, 1057, 0, 0, 'BP_Statuses_High', NULL, 'High', 'High', NULL, b'0', b'0', '2023-12-04 19:32:02', 0, '2023-12-04 19:32:02', 0, b'1'), (1058001, 0, 0, 1058, 0, 0, 'Input_Types_number', NULL, 'number', 'number', NULL, b'0', b'0', '2023-12-05 16:57:03', 0, '2023-12-05 16:57:03', 0, b'1'), (1058002, 0, 0, 1058, 0, 0, 'Input_Types_text', NULL, 'text', 'text', NULL, b'0', b'0', '2023-12-05 16:57:14', 0, '2023-12-05 16:57:14', 0, b'1'), (1058003, 0, 0, 1058, 0, 0, 'Input_Types_email', NULL, 'email', 'email', NULL, b'0', b'0', '2023-12-05 17:31:11', 0, '2023-12-05 17:31:11', 0, b'0'), (1101001, 0, 0, 1101, 0, 0, 'Week_Days_Monday', NULL, 'Monday', 'Monday', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1101002, 0, 0, 1101, 0, 0, 'Week_Days_Tuesday', NULL, 'Tuesday', 'Tuesday', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1101003, 0, 0, 1101, 0, 0, 'Week_Days_Wednesday', NULL, 'Wednesday', 'Wednesday', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1101004, 0, 0, 1101, 0, 0, 'Week_Days_Thursday', NULL, 'Thursday', 'Thursday', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1101005, 0, 0, 1101, 0, 0, 'Week_Days_Friday', NULL, 'Friday', 'Friday', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1101006, 0, 0, 1101, 0, 0, 'Week_Days_Saturday', NULL, 'Saturday', 'Saturday', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1101007, 0, 0, 1101, 0, 0, 'Week_Days_Sunday', NULL, 'Sunday', 'Sunday', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1102001, 0, 0, 1102, 0, 0, 'Entities_User', NULL, 'User', 'User', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1102002, 0, 0, 1102, 0, 0, 'Entities_User', NULL, 'User', 'User', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1103001, 0, 0, 1103, 0, 0, 'Schedule_Types_FH', NULL, 'FH', 'FH', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1103002, 0, 0, 1103, 0, 0, 'Schedule_Types_FWH', NULL, 'FWH', 'FWH', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1103003, 0, 0, 1103, 0, 0, 'Schedule_Types_FHD', NULL, 'FHD', 'FHD', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1104001, 0, 0, 1104, 0, 0, 'Working_Type_Day', NULL, 'Day', 'Day', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1104002, 0, 0, 1104, 0, 0, 'Working_Type_Week', NULL, 'Week', 'Week', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1104003, 0, 0, 1104, 0, 0, 'Working_Type_Month', NULL, 'Month', 'Month', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1105001, 0, 0, 1105, 0, 0, 'EventType_Work', NULL, 'Work', 'Work', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1105002, 0, 0, 1105, 0, 0, 'EventType_Break', NULL, 'Break', 'Break', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1106001, 0, 0, 1106, 0, 0, 'Location_RehmanPura', NULL, 'Rehman Pura', 'Rehman Pura', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1106002, 0, 0, 1106, 0, 0, 'Location_SiddiqueTradeCenter', NULL, 'Siddique Trade Center', 'Siddique Trade Center', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1107001, 0, 0, 1107, 0, 0, 'Task_Status_Open', NULL, 'Open', 'Open', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1107002, 0, 0, 1107, 0, 0, 'Task_Status_InProgress', NULL, 'InProgress', 'InProgress', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1107003, 0, 0, 1107, 0, 0, 'Task_Status_InTesting', NULL, 'InTesting', 'InTesting', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1107004, 0, 0, 1107, 0, 0, 'Task_Status_ReOpened', NULL, 'ReOpened', 'ReOpened', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1107005, 0, 0, 1107, 0, 0, 'Task_Status_Resolved', NULL, 'Resolved', 'Resolved', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1107006, 0, 0, 1107, 0, 0, 'Task_Status_Stalled', NULL, 'Stalled', 'Stalled', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1108001, 0, 0, 1108, 0, 0, 'Task_Priority_Emergency', NULL, 'P0', 'P0', 'Emergency', b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1108002, 0, 0, 1108, 0, 0, 'Task_Priority_Highest', NULL, 'P1', 'P1', 'Highest', b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1108003, 0, 0, 1108, 0, 0, 'Task_Priority_High', NULL, 'P2', 'P2', 'High', b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1108004, 0, 0, 1108, 0, 0, 'Task_Priority_Medium', NULL, 'P3', 'P3', 'Medium', b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1108005, 0, 0, 1108, 0, 0, 'Task_Priority_Low', NULL, 'P4', 'P4', 'Low', b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109001, 0, 0, 1109, 0, 0, 'Modules_TMS', NULL, 'TMS', 'TMS', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109002, 0, 0, 1109, 0, 0, 'Modules_SCH', NULL, 'SCH', 'SCH', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109003, 0, 0, 1109, 0, 0, 'Modules_NTF', NULL, 'NTF', 'NTF', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109004, 0, 0, 1109, 0, 0, 'Modules_ATT', NULL, 'ATT', 'ATT', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109005, 0, 0, 1109, 0, 0, 'Modules_SCE', NULL, 'SCE', 'SCE', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109006, 0, 0, 1109, 0, 0, 'Modules_IMS', NULL, 'IMS', 'IMS', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109007, 0, 0, 1109, 0, 0, 'Modules_TST', NULL, 'TST', 'TST', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109008, 0, 0, 1109, 0, 0, 'Modules_TRN', NULL, 'TRN', 'TRN', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1109009, 0, 0, 1109, 0, 0, 'Modules_PRL', NULL, 'PRL', 'PRL', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110001, 0, 0, 1110, 0, 0, 'Claim_100', NULL, '100', '100', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110002, 0, 0, 1110, 0, 0, 'Claim_90', NULL, '90', '90', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110003, 0, 0, 1110, 0, 0, 'Claim_80', NULL, '80', '80', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110004, 0, 0, 1110, 0, 0, 'Claim_70', NULL, '70', '70', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110005, 0, 0, 1110, 0, 0, 'Claim_60', NULL, '60', '60', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110006, 0, 0, 1110, 0, 0, 'Claim_50', NULL, '50', '50', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110007, 0, 0, 1110, 0, 0, 'Claim_40', NULL, '40', '40', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110008, 0, 0, 1110, 0, 0, 'Claim_30', NULL, '30', '30', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110009, 0, 0, 1110, 0, 0, 'Claim_20', NULL, '20', '20', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110010, 0, 0, 1110, 0, 0, 'Claim_10', NULL, '10', '10', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1110011, 0, 0, 1110, 0, 0, 'Claim_0', NULL, '0', '0', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1111001, 0, 0, 1111, 0, 0, 'Stalled_Task_Reasons_Need_Help', NULL, 'Need Help', 'Need Help', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1111001, 1, 0, 1111, 0, 0, 'Need_Help', NULL, 'Need Help', 'Need Help', NULL, b'0', b'0', '2024-03-14 13:03:16', 0, '2024-03-14 13:03:16', 0, b'1'), (1111001, 2, 0, 1111, 0, 0, 'Unable_to_complete', NULL, 'Unable to complete', 'Unable to complete', NULL, b'0', b'0', '2024-03-14 13:04:35', 0, '2024-03-14 13:04:35', 0, b'1'), (1111002, 0, 0, 1111, 0, 0, 'Stalled_Task_Reasons_Unable_To_Complete', NULL, 'Unable to Complete', 'Unable to Complete', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1111002, 1, 0, 1111, 0, 0, 'Prioritization_Issue', NULL, 'Prioritization Issue', 'Prioritization Issue', NULL, b'0', b'0', '2024-03-14 13:03:45', 0, '2024-03-14 13:03:45', 0, b'1'), (1111003, 0, 0, 1111, 0, 0, 'Stalled_Task_Reasons_Prioritization_Issue', NULL, 'Prioritization Issue', 'Prioritization Issue', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1112001, 0, 0, 1112, 0, 0, 'Novels_Echoes_of_Eternity', NULL, 'Echoes of Eternity', 'Echoes of Eternity', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1112001, 1, 0, 1112, 0, 0, 'c1_Novel_1', NULL, 'c1_Novel_1', 'c1_Novel_1', NULL, b'0', b'0', '2024-03-15 17:49:27', 0, '2024-03-15 17:49:27', 0, b'1'), (1112001, 4, 0, 1112, 0, 0, 'c4_Novel_1', NULL, 'c4_Novel_1', 'c4_Novel_1', NULL, b'0', b'0', '2024-03-20 05:02:51', 0, '2024-03-20 05:02:51', 0, b'1'), (1112002, 0, 0, 1112, 0, 0, 'Novels_Sands_of_Serenity', NULL, 'Sands of Serenity', 'Sands of Serenity', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1112002, 1, 0, 1112, 0, 0, 'c1_Novel_2', NULL, 'c1_Novel_2', 'c1_Novel_2', NULL, b'0', b'0', '2024-03-15 17:49:32', 0, '2024-03-15 17:49:32', 0, b'1'), (1112003, 1, 0, 1112, 0, 0, 'c1_Novel_3', NULL, 'c1_Novel_3', 'c1_Novel_3', NULL, b'0', b'0', '2024-03-15 17:49:38', 0, '2024-03-15 17:49:38', 0, b'1'), (1113001, 0, 0, 1113, 0, 0, 'Novel_Chapters_Dunes_of_Destiny', NULL, 'Dunes of Destiny', 'Dunes of Destiny', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1113001, 1, 0, 1113, 1112001, 0, 'Novel_1_Chap_1', NULL, 'Novel_1_Chap_1', 'Novel_1_Chap_1', NULL, b'0', b'0', '2024-03-15 17:49:57', 0, '2024-03-15 17:49:57', 0, b'1'), (1113001, 4, 0, 1113, 1112001, 0, 'c4_Novel1_Chapter_1', NULL, 'c4_Novel1_Chapter_1', 'c4_Novel1_Chapter_1', NULL, b'0', b'0', '2024-03-20 05:03:15', 0, '2024-03-20 05:03:15', 0, b'1'), (1113002, 0, 0, 1113, 1112001, 0, 'Echoes_1', NULL, 'Echoes_1', 'Echoes_1', NULL, b'0', b'0', '2024-03-15 16:44:02', 0, '2024-03-15 16:44:02', 0, b'1'), (1113002, 1, 0, 1113, 1112001, 0, 'Novel_1_Chap_2', NULL, 'Novel_1_Chap_2', 'Novel_1_Chap_2', NULL, b'0', b'0', '2024-03-15 17:51:47', 0, '2024-03-15 17:51:47', 0, b'1'), (1113003, 1, 0, 1113, 1112001, 0, 'Novel_1_Chap_3....', NULL, 'Novel_1_Chap_3....', 'Novel_1_Chap_3....', NULL, b'0', b'0', '2024-03-15 17:52:06', 0, '2024-03-15 17:52:06', 0, b'1'), (1113004, 1, 0, 1113, 1112002, 0, 'Novel_2_Chap_1', NULL, 'Novel_2_Chap_1', 'Novel_2_Chap_1', NULL, b'0', b'0', '2024-03-15 17:52:38', 0, '2024-03-15 17:52:38', 0, b'1'), (1113005, 1, 0, 1113, 1112002, 0, 'Novel_2_Chap_2', NULL, 'Novel_2_Chap_2', 'Novel_2_Chap_2', NULL, b'0', b'0', '2024-03-15 17:52:49', 0, '2024-03-15 17:52:49', 0, b'1'), (1114001, 0, 0, 1114, 0, 0, 'Vocabulary_Difficuilty_Levels_High', NULL, 'High', 'High', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1114002, 0, 0, 1114, 0, 0, 'Vocabulary_Difficuilty_Levels_Medium', NULL, 'Medium', 'Medium', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1114003, 0, 0, 1114, 0, 0, 'Vocabulary_Difficuilty_Levels_Low', NULL, 'Low', 'Low', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1'), (1114004, 0, 0, 1114, 0, 0, 'Vocabulary_Difficuilty_Levels_None', NULL, 'None', 'None', NULL, b'1', b'0', '2023-09-25 12:24:25', 0, '2023-09-25 12:24:25', 0, b'1');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_item
-- ----------------------------
BEGIN;
INSERT INTO `ctl_item` (`Id`, `ClientId`, `ModuleId`, `TypeId`, `VendorId`, `ManufacturersId`, `CategoryId`, `Name`, `PurRate`, `SaleRate`, `Conversion`, `GstSaleRate`, `GstPurRate`, `SaleStRate`, `PurStRate`, `PurUnits`, `SaleUnits`, `ExtraRate`, `RetailRate`, `PrMazdoori`, `UnitPrice`, `UnitsInStock`, `Formula`, `Remarks`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1, 1, 1001004, 0, 0, 1053001, 1052001, 'Zorpent', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Trizex', NULL, '2023-10-02 22:39:24', 0, '2023-10-02 22:39:24', 0, b'1'), (1, 3, 1001004, 0, 0, 1053001, 1052001, 'Calpol', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Epzicom', NULL, '2023-10-02 22:19:45', 0, '2023-10-02 22:19:45', 0, b'1'), (2, 0, 0, 1029001, 0, NULL, NULL, 'Containers', 100, 100, 100, 100, 10, NULL, NULL, '1', '2,5', 100, 0, NULL, 200.98765, 100, NULL, NULL, '2023-05-03 16:00:17', 0, '2023-05-03 16:00:17', 0, b'1'), (2, 1, 1001004, 0, 0, 1053001, 1052001, 'Calpol', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '.', NULL, '2023-11-09 15:40:56', 0, '2023-11-09 15:40:56', 0, b'1'), (2, 3, 1001004, 0, 0, 1053002, 1052002, 'Qalsan D', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Triumpq', NULL, '2023-10-02 22:19:45', 0, '2023-10-02 22:19:45', 0, b'1'), (3, 0, NULL, 1029002, 0, NULL, NULL, 'Stationary Products', 200, 300, 10, NULL, NULL, 100, 9, '1', '2', 40, 250, NULL, 300, 100, NULL, NULL, '2023-05-03 16:00:46', 0, '2023-05-03 16:00:46', 0, b'0'), (3, 1, 1001004, 0, 0, 1053002, 1052002, 'Qalsan D', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Triumpq', NULL, '2023-11-09 15:42:01', 0, '2023-11-09 15:42:01', 0, b'1'), (4, 0, 0, 1029001, 0, NULL, NULL, 'Wood Preservatives', 86, 100, 10, 540, 3, NULL, NULL, '1', '5', 90, 100, NULL, 900, 100, NULL, NULL, '2023-05-03 16:01:08', 0, '2023-05-03 16:01:08', 0, b'1'), (5, 0, 0, 1029001, 0, NULL, NULL, 'Computing Infrastructure', 210, 120, 8, NULL, NULL, 90, 9, '9', '2', 890, 250, NULL, 700, 100, NULL, NULL, '2023-05-03 16:02:02', 0, '2023-05-03 16:02:02', 0, b'1'), (6, 0, 0, 1029002, 0, NULL, NULL, 'Sugar', 100, 100, NULL, NULL, NULL, NULL, NULL, NULL, '1,2,5', NULL, 90, NULL, NULL, NULL, NULL, NULL, '2023-08-21 11:49:19', 0, '2023-08-21 11:49:19', 0, b'0'), (7, 0, NULL, 1029002, 0, NULL, NULL, 'Rice', 100, 100, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 80, NULL, NULL, NULL, NULL, NULL, '2023-08-21 12:14:11', 0, '2023-08-21 12:14:11', 0, b'0');
COMMIT;

-- ----------------------------
-- Table structure for ctl_itemvariants
-- ----------------------------
DROP TABLE IF EXISTS `ctl_itemvariants`;
CREATE TABLE `ctl_itemvariants`  (
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
  INDEX `foreign_key_ibfk_1_idx`(`ItemId` ASC) USING BTREE,
  CONSTRAINT `foreign_key_ibfk_1` FOREIGN KEY (`ItemId`) REFERENCES `ctl_item` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_itemvariants
-- ----------------------------
BEGIN;
COMMIT;

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
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE,
  INDEX `UserId`(`UserId` ASC) USING BTREE,
  CONSTRAINT `ctl_logevent_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sec_user` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_logevent
-- ----------------------------
BEGIN;
INSERT INTO `ctl_logevent` (`Id`, `ClientId`, `UserId`, `InTime`, `OutTime`, `Date`, `Message`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-09 17:50:42', NULL, '2024-03-09 17:50:42', NULL, '2024-03-09 17:50:42', 0, '2024-03-09 17:50:42', 0, b'1'), (1, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 13:56:32', '2024-03-14 13:56:34', '2024-03-14 13:56:32', NULL, '2024-03-14 13:56:32', 0, '2024-03-14 13:56:32', 0, b'1'), (1, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-09 17:46:37', '2024-03-14 14:46:36', '2024-03-09 17:46:37', NULL, '2024-03-09 17:46:37', 0, '2024-03-09 17:46:37', 0, b'1'), (1, 3, '03081917-f57d-4278-90f7-c0528bd16b5c', '2024-03-18 20:17:30', NULL, '2024-03-18 20:17:30', NULL, '2024-03-18 20:17:30', 0, '2024-03-18 20:17:30', 0, b'1'), (1, 4, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', '2024-03-19 20:24:22', NULL, '2024-03-19 20:24:22', NULL, '2024-03-19 20:24:22', 0, '2024-03-19 20:24:22', 0, b'1'), (2, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-09 18:38:36', '2024-03-09 19:18:23', '2024-03-09 18:38:36', NULL, '2024-03-09 18:38:36', 0, '2024-03-09 18:38:36', 0, b'1'), (2, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-14 13:59:04', NULL, '2024-03-14 13:59:04', NULL, '2024-03-14 13:59:04', 0, '2024-03-14 13:59:04', 0, b'1'), (2, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-09 19:19:15', NULL, '2024-03-09 19:19:15', NULL, '2024-03-09 19:19:15', 0, '2024-03-09 19:19:15', 0, b'1'), (2, 3, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', '2024-03-19 02:03:01', NULL, '2024-03-19 02:03:01', NULL, '2024-03-19 02:03:01', 0, '2024-03-19 02:03:01', 0, b'1'), (2, 4, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', '2024-03-19 20:28:06', NULL, '2024-03-19 20:28:06', NULL, '2024-03-19 20:28:06', 0, '2024-03-19 20:28:06', 0, b'1'), (3, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-14 12:33:11', '2024-03-14 12:33:57', '2024-03-14 12:33:11', NULL, '2024-03-14 12:33:11', 0, '2024-03-14 12:33:11', 0, b'1'), (3, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', '2024-03-12 14:24:01', '2024-03-12 14:25:10', '2024-03-12 14:24:01', NULL, '2024-03-12 14:24:01', 0, '2024-03-12 14:24:01', 0, b'1'), (3, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-11 00:09:03', NULL, '2024-03-11 00:09:03', NULL, '2024-03-11 00:09:03', 0, '2024-03-11 00:09:03', 0, b'1'), (3, 3, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', '2024-03-19 17:27:52', NULL, '2024-03-19 17:27:52', NULL, '2024-03-19 17:27:52', 0, '2024-03-19 17:27:52', 0, b'1'), (3, 4, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', '2024-03-19 20:51:23', NULL, '2024-03-19 20:51:23', NULL, '2024-03-19 20:51:23', 0, '2024-03-19 20:51:23', 0, b'1'), (4, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-14 12:34:08', NULL, '2024-03-14 12:34:08', NULL, '2024-03-14 12:34:08', 0, '2024-03-14 12:34:08', 0, b'1'), (4, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', '2024-03-13 14:26:01', '2024-03-13 14:26:23', '2024-03-13 14:26:01', NULL, '2024-03-13 14:26:01', 0, '2024-03-13 14:26:01', 0, b'1'), (4, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-14 13:05:16', NULL, '2024-03-14 13:05:16', NULL, '2024-03-14 13:05:16', 0, '2024-03-14 13:05:16', 0, b'1'), (4, 3, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', '2024-03-19 17:51:25', NULL, '2024-03-19 17:51:25', NULL, '2024-03-19 17:51:25', 0, '2024-03-19 17:51:25', 0, b'1'), (4, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', '2024-03-19 20:51:38', '2024-03-19 20:55:10', '2024-03-19 20:51:38', NULL, '2024-03-19 20:51:38', 0, '2024-03-19 20:51:38', 0, b'1'), (5, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-18 15:33:57', NULL, '2024-03-18 15:33:57', NULL, '2024-03-18 15:33:57', 0, '2024-03-18 15:33:57', 0, b'1'), (5, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', '2024-03-13 14:26:54', '2024-03-13 14:27:37', '2024-03-13 14:26:54', NULL, '2024-03-13 14:26:54', 0, '2024-03-13 14:26:54', 0, b'1'), (5, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-14 14:44:38', NULL, '2024-03-14 14:44:38', NULL, '2024-03-14 14:44:38', 0, '2024-03-14 14:44:38', 0, b'1'), (5, 3, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', '2024-03-19 17:52:25', NULL, '2024-03-19 17:52:25', NULL, '2024-03-19 17:52:25', 0, '2024-03-19 17:52:25', 0, b'1'), (5, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', '2024-03-19 21:24:47', '2024-03-20 01:24:04', '2024-03-19 21:24:47', NULL, '2024-03-19 21:24:47', 0, '2024-03-19 21:24:47', 0, b'1'), (6, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-18 20:02:03', NULL, '2024-03-18 20:02:03', NULL, '2024-03-18 20:02:03', 0, '2024-03-18 20:02:03', 0, b'1'), (6, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', '2024-03-14 14:28:46', '2024-03-18 20:26:21', '2024-03-14 14:28:46', NULL, '2024-03-14 14:28:46', 0, '2024-03-14 14:28:46', 0, b'1'), (6, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-14 14:54:25', '2024-03-14 14:54:55', '2024-03-14 14:54:25', NULL, '2024-03-14 14:54:25', 0, '2024-03-14 14:54:25', 0, b'1'), (6, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', '2024-03-20 01:26:10', '2024-03-20 01:40:31', '2024-03-20 01:26:10', NULL, '2024-03-20 01:26:10', 0, '2024-03-20 01:26:10', 0, b'1'), (7, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-18 20:18:07', NULL, '2024-03-18 20:18:07', NULL, '2024-03-18 20:18:07', 0, '2024-03-18 20:18:07', 0, b'1'), (7, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 14:31:37', '2024-03-14 14:35:05', '2024-03-14 14:31:37', NULL, '2024-03-14 14:31:37', 0, '2024-03-14 14:31:37', 0, b'1'), (7, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-14 16:00:31', NULL, '2024-03-14 16:00:31', NULL, '2024-03-14 16:00:31', 0, '2024-03-14 16:00:31', 0, b'1'), (7, 4, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', '2024-03-20 01:39:28', NULL, '2024-03-20 01:39:28', NULL, '2024-03-20 01:39:28', 0, '2024-03-20 01:39:28', 0, b'1'), (8, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-18 20:41:52', NULL, '2024-03-18 20:41:52', NULL, '2024-03-18 20:41:52', 0, '2024-03-18 20:41:52', 0, b'1'), (8, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-13 14:35:33', '2024-03-13 14:36:20', '2024-03-13 14:35:33', NULL, '2024-03-13 14:35:33', 0, '2024-03-13 14:35:33', 0, b'1'), (8, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-14 16:33:17', NULL, '2024-03-14 16:33:17', NULL, '2024-03-14 16:33:17', 0, '2024-03-14 16:33:17', 0, b'1'), (8, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', '2024-03-20 01:40:40', NULL, '2024-03-20 01:40:40', NULL, '2024-03-20 01:40:40', 0, '2024-03-20 01:40:40', 0, b'1'), (9, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-18 20:54:07', NULL, '2024-03-18 20:54:07', NULL, '2024-03-18 20:54:07', 0, '2024-03-18 20:54:07', 0, b'1'), (9, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 14:37:02', NULL, '2024-03-14 14:37:02', NULL, '2024-03-14 14:37:02', 0, '2024-03-14 14:37:02', 0, b'1'), (9, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-15 12:25:01', NULL, '2024-03-15 12:25:01', NULL, '2024-03-15 12:25:01', 0, '2024-03-15 12:25:01', 0, b'1'), (10, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-19 16:28:36', '2024-03-20 01:22:37', '2024-03-19 16:28:36', NULL, '2024-03-19 16:28:36', 0, '2024-03-19 16:28:36', 0, b'1'), (10, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 14:59:54', '2024-03-14 15:59:56', '2024-03-14 14:59:54', NULL, '2024-03-14 14:59:54', 0, '2024-03-14 14:59:54', 0, b'1'), (10, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-15 16:02:33', NULL, '2024-03-15 16:02:33', NULL, '2024-03-15 16:02:33', 0, '2024-03-15 16:02:33', 0, b'1'), (11, 0, 'c35d2b3b-6cb9-480b-9868-ec8dbf343783', '2024-03-20 03:08:07', NULL, '2024-03-20 03:08:07', NULL, '2024-03-20 03:08:07', 0, '2024-03-20 03:08:07', 0, b'1'), (11, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 16:04:38', '2024-03-14 16:32:23', '2024-03-14 16:04:38', NULL, '2024-03-14 16:04:38', 0, '2024-03-14 16:04:38', 0, b'1'), (11, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-15 16:03:30', '2024-03-15 16:05:05', '2024-03-15 16:03:30', NULL, '2024-03-15 16:03:30', 0, '2024-03-15 16:03:30', 0, b'1'), (12, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 16:36:06', NULL, '2024-03-14 16:36:06', NULL, '2024-03-14 16:36:06', 0, '2024-03-14 16:36:06', 0, b'1'), (12, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-15 16:13:00', '2024-03-15 16:13:15', '2024-03-15 16:13:00', NULL, '2024-03-15 16:13:00', 0, '2024-03-15 16:13:00', 0, b'1'), (13, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 16:46:09', '2024-03-14 16:55:51', '2024-03-14 16:46:09', NULL, '2024-03-14 16:46:09', 0, '2024-03-14 16:46:09', 0, b'1'), (13, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-15 16:21:48', '2024-03-15 16:22:01', '2024-03-15 16:21:48', NULL, '2024-03-15 16:21:48', 0, '2024-03-15 16:21:48', 0, b'1'), (14, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', '2024-03-14 16:59:47', '2024-03-14 17:00:40', '2024-03-14 16:59:47', NULL, '2024-03-14 16:59:47', 0, '2024-03-14 16:59:47', 0, b'1'), (14, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-19 14:29:44', '2024-03-19 14:30:17', '2024-03-19 14:29:44', NULL, '2024-03-19 14:29:44', 0, '2024-03-19 14:29:44', 0, b'1'), (15, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', '2024-03-14 17:00:49', '2024-03-14 17:04:09', '2024-03-14 17:00:49', NULL, '2024-03-14 17:00:49', 0, '2024-03-14 17:00:49', 0, b'1'), (15, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-19 14:36:54', '2024-03-19 14:39:14', '2024-03-19 14:36:54', NULL, '2024-03-19 14:36:54', 0, '2024-03-19 14:36:54', 0, b'1'), (16, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-14 17:05:51', NULL, '2024-03-14 17:05:51', NULL, '2024-03-14 17:05:51', 0, '2024-03-14 17:05:51', 0, b'1'), (16, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-19 17:05:20', NULL, '2024-03-19 17:05:20', NULL, '2024-03-19 17:05:20', 0, '2024-03-19 17:05:20', 0, b'1'), (17, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-14 17:06:13', '2024-03-15 17:17:25', '2024-03-14 17:06:13', NULL, '2024-03-14 17:06:13', 0, '2024-03-14 17:06:13', 0, b'1'), (17, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-19 17:15:34', NULL, '2024-03-19 17:15:34', NULL, '2024-03-19 17:15:34', 0, '2024-03-19 17:15:34', 0, b'1'), (18, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', '2024-03-15 17:19:53', '2024-03-15 17:21:28', '2024-03-15 17:19:53', NULL, '2024-03-15 17:19:53', 0, '2024-03-15 17:19:53', 0, b'1'), (18, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-19 17:28:11', NULL, '2024-03-19 17:28:11', NULL, '2024-03-19 17:28:11', 0, '2024-03-19 17:28:11', 0, b'1'), (19, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-15 17:22:14', '2024-03-15 16:00:48', '2024-03-15 17:22:14', NULL, '2024-03-15 17:22:14', 0, '2024-03-15 17:22:14', 0, b'1'), (19, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', '2024-03-19 17:30:24', NULL, '2024-03-19 17:30:24', NULL, '2024-03-19 17:30:24', 0, '2024-03-19 17:30:24', 0, b'1'), (20, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-15 16:28:16', '2024-03-15 17:03:17', '2024-03-15 16:28:16', NULL, '2024-03-15 16:28:16', 0, '2024-03-15 16:28:16', 0, b'1'), (20, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-19 17:30:40', '2024-03-19 17:34:12', '2024-03-19 17:30:40', NULL, '2024-03-19 17:30:40', 0, '2024-03-19 17:30:40', 0, b'1'), (21, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-15 17:15:44', '2024-03-15 17:17:34', '2024-03-15 17:15:44', NULL, '2024-03-15 17:15:44', 0, '2024-03-15 17:15:44', 0, b'1'), (21, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-19 17:34:25', '2024-03-19 17:35:48', '2024-03-19 17:34:25', NULL, '2024-03-19 17:34:25', 0, '2024-03-19 17:34:25', 0, b'1'), (22, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-15 17:20:53', NULL, '2024-03-15 17:20:53', NULL, '2024-03-15 17:20:53', 0, '2024-03-15 17:20:53', 0, b'1'), (22, 2, '85060824-9132-4191-aa91-fe3942f00969', '2024-03-19 17:35:58', '2024-03-19 17:54:26', '2024-03-19 17:35:58', NULL, '2024-03-19 17:35:58', 0, '2024-03-19 17:35:58', 0, b'1'), (23, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-15 17:29:19', '2024-03-18 20:20:27', '2024-03-15 17:29:19', NULL, '2024-03-15 17:29:19', 0, '2024-03-15 17:29:19', 0, b'1'), (24, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-18 20:01:17', NULL, '2024-03-18 20:01:17', NULL, '2024-03-18 20:01:17', 0, '2024-03-18 20:01:17', 0, b'1'), (25, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-18 20:20:33', NULL, '2024-03-18 20:20:33', NULL, '2024-03-18 20:20:33', 0, '2024-03-18 20:20:33', 0, b'1'), (26, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-18 20:24:25', NULL, '2024-03-18 20:24:25', NULL, '2024-03-18 20:24:25', 0, '2024-03-18 20:24:25', 0, b'1'), (27, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', '2024-03-18 20:26:24', '2024-03-18 20:53:46', '2024-03-18 20:26:24', NULL, '2024-03-18 20:26:24', 0, '2024-03-18 20:26:24', 0, b'1'), (28, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-18 20:53:24', NULL, '2024-03-18 20:53:24', NULL, '2024-03-18 20:53:24', 0, '2024-03-18 20:53:24', 0, b'1'), (29, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-18 20:55:05', NULL, '2024-03-18 20:55:05', NULL, '2024-03-18 20:55:05', 0, '2024-03-18 20:55:05', 0, b'1'), (30, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-18 20:57:20', NULL, '2024-03-18 20:57:20', NULL, '2024-03-18 20:57:20', 0, '2024-03-18 20:57:20', 0, b'1'), (31, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 01:52:32', NULL, '2024-03-19 01:52:32', NULL, '2024-03-19 01:52:32', 0, '2024-03-19 01:52:32', 0, b'1'), (32, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 01:53:23', NULL, '2024-03-19 01:53:23', NULL, '2024-03-19 01:53:23', 0, '2024-03-19 01:53:23', 0, b'1'), (33, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 01:54:34', NULL, '2024-03-19 01:54:34', NULL, '2024-03-19 01:54:34', 0, '2024-03-19 01:54:34', 0, b'1'), (34, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', '2024-03-19 01:56:16', '2024-03-19 02:02:49', '2024-03-19 01:56:16', NULL, '2024-03-19 01:56:16', 0, '2024-03-19 01:56:16', 0, b'1'), (35, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 02:43:48', NULL, '2024-03-19 02:43:48', NULL, '2024-03-19 02:43:48', 0, '2024-03-19 02:43:48', 0, b'1'), (36, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-19 02:45:19', '2024-03-19 06:11:53', '2024-03-19 02:45:19', NULL, '2024-03-19 02:45:19', 0, '2024-03-19 02:45:19', 0, b'1'), (37, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 06:08:32', NULL, '2024-03-19 06:08:32', NULL, '2024-03-19 06:08:32', 0, '2024-03-19 06:08:32', 0, b'1'), (38, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-19 06:12:06', '2024-03-19 14:26:30', '2024-03-19 06:12:06', NULL, '2024-03-19 06:12:06', 0, '2024-03-19 06:12:06', 0, b'1'), (39, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', '2024-03-19 14:30:26', '2024-03-19 14:36:43', '2024-03-19 14:30:26', NULL, '2024-03-19 14:30:26', 0, '2024-03-19 14:30:26', 0, b'1'), (40, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', '2024-03-19 14:46:17', '2024-03-19 14:48:35', '2024-03-19 14:46:17', NULL, '2024-03-19 14:46:17', 0, '2024-03-19 14:46:17', 0, b'1'), (41, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', '2024-03-19 14:48:50', '2024-03-19 14:48:53', '2024-03-19 14:48:50', NULL, '2024-03-19 14:48:50', 0, '2024-03-19 14:48:50', 0, b'1'), (42, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', '2024-03-19 14:49:39', '2024-03-20 01:29:35', '2024-03-19 14:49:39', NULL, '2024-03-19 14:49:39', 0, '2024-03-19 14:49:39', 0, b'1'), (43, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-19 15:43:03', '2024-03-19 16:28:22', '2024-03-19 15:43:03', NULL, '2024-03-19 15:43:03', 0, '2024-03-19 15:43:03', 0, b'1'), (44, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 16:45:03', NULL, '2024-03-19 16:45:03', NULL, '2024-03-19 16:45:03', 0, '2024-03-19 16:45:03', 0, b'1'), (45, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 17:27:32', NULL, '2024-03-19 17:27:32', NULL, '2024-03-19 17:27:32', 0, '2024-03-19 17:27:32', 0, b'1'), (46, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-19 17:54:44', NULL, '2024-03-19 17:54:44', NULL, '2024-03-19 17:54:44', 0, '2024-03-19 17:54:44', 0, b'1'), (47, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-19 20:56:43', '2024-03-20 01:22:00', '2024-03-19 20:56:43', NULL, '2024-03-19 20:56:43', 0, '2024-03-19 20:56:43', 0, b'1'), (48, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '2024-03-20 01:26:58', '2024-03-20 01:27:21', '2024-03-20 01:26:58', NULL, '2024-03-20 01:26:58', 0, '2024-03-20 01:26:58', 0, b'1'), (49, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', '2024-03-20 01:27:32', '2024-03-20 01:27:37', '2024-03-20 01:27:32', NULL, '2024-03-20 01:27:32', 0, '2024-03-20 01:27:32', 0, b'1'), (50, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', '2024-03-20 01:28:25', NULL, '2024-03-20 01:28:25', NULL, '2024-03-20 01:28:25', 0, '2024-03-20 01:28:25', 0, b'1'), (51, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', '2024-03-20 01:29:39', '2024-03-20 02:21:52', '2024-03-20 01:29:39', NULL, '2024-03-20 01:29:39', 0, '2024-03-20 01:29:39', 0, b'1');
COMMIT;

-- ----------------------------
-- Table structure for ctl_productattrib
-- ----------------------------
DROP TABLE IF EXISTS `ctl_productattrib`;
CREATE TABLE `ctl_productattrib`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_productattrib
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for ctl_producttaxes
-- ----------------------------
DROP TABLE IF EXISTS `ctl_producttaxes`;
CREATE TABLE `ctl_producttaxes`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_producttaxes
-- ----------------------------
BEGIN;
COMMIT;

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
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_smtpcredentials
-- ----------------------------
BEGIN;
COMMIT;

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
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_supplier
-- ----------------------------
BEGIN;
COMMIT;

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
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ctl_uomconversion
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for ntf_notificationlog
-- ----------------------------
DROP TABLE IF EXISTS `ntf_notificationlog`;
CREATE TABLE `ntf_notificationlog`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `From` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `To` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `KeyCode` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Subject` varchar(300) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Body` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `SMS` varchar(6000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `IsSent` bit(1) NULL DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE,
  INDEX `UserId`(`UserId` ASC) USING BTREE,
  CONSTRAINT `NTF_notificationlog_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sec_user` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ntf_notificationlog
-- ----------------------------
BEGIN;
INSERT INTO `ntf_notificationlog` (`Id`, `ClientId`, `UserId`, `From`, `To`, `KeyCode`, `Subject`, `Body`, `SMS`, `IsSent`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Jannat has started her/his day Start at 3/12/2024 on 2:24 PM', 'Dear Fatima,  On 3/12/2024 User Jannat has started his/her day start on 2:24 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-12 14:24:01', 0, '2024-03-12 14:24:01', 0, b'1'), (1, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'c2@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User C2_Ali has started her/his day Start at 3/9/2024 on 5:46 PM', 'Dear Client_2,  On 3/9/2024 User C2_Ali has started his/her day start on 5:46 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-09 17:46:37', 0, '2024-03-09 17:46:37', 0, b'1'), (2, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Jannat has started her/his day Start at 3/12/2024 on 2:24 PM', 'Dear MuhammadBadar,  On 3/12/2024 User Jannat has started his/her day start on 2:24 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-12 14:24:01', 0, '2024-03-12 14:24:01', 0, b'1'), (2, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User C2_Ali has marked his/her day end at 5:46 PM', ' Dear Client_2, User C2_Ali has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			  <td style=\"background-color:#faf8f8;\">#TMS_DueTime#</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">Stalled</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n			 <td style=\"background-color:#faf8f8;\">P2</td>\r\n			  <td style=\"background-color:#faf8f8;\">#TMS_DueTime#</td>\r\n            <td style=\"background-color:#faf8f8;\">10</td>\r\n            <td style=\"background-color:#faf8f8;\">10%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 14:46:36', 0, '2024-03-14 14:46:36', 0, b'1'), (3, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Jannat has started her/his day Start at 3/12/2024 on 2:24 PM', 'Dear Client_1,  On 3/12/2024 User Jannat has started his/her day start on 2:24 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-12 14:24:01', 0, '2024-03-12 14:24:01', 0, b'1'), (3, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User C2_Ali has started her/his day Start at 3/14/2024 on 2:54 PM', 'Dear Client_2,  On 3/14/2024 User C2_Ali has started his/her day start on 2:54 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P2</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P2</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">6</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 14:54:25', 0, '2024-03-14 14:54:25', 0, b'1'), (4, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Jannat has marked his/her day end at 2:24 PM', ' Dear Fatima, User Jannat has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">20%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>\r\n            <td style=\"background-color:#faf8f8;\">1</td>\r\n            <td style=\"background-color:#faf8f8;\">30%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-12 14:25:10', 0, '2024-03-12 14:25:10', 0, b'1'), (4, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User C2_Ali has marked his/her day end at 2:54 PM', ' Dear Client_2, User C2_Ali has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>			 <td style=\"background-color:#faf8f8;\">P2</td>			<td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">10%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>			 <td style=\"background-color:#faf8f8;\">P2</td>			<td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">7</td>            <td style=\"background-color:#faf8f8;\">10%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">6</td>\r\n            <td style=\"background-color:#faf8f8;\">0</td>\r\n            <td style=\"background-color:#faf8f8;\">0%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 14:54:55', 0, '2024-03-14 14:54:55', 0, b'1'), (5, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Jannat has marked his/her day end at 2:24 PM', ' Dear MuhammadBadar, User Jannat has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">20%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>\r\n            <td style=\"background-color:#faf8f8;\">1</td>\r\n            <td style=\"background-color:#faf8f8;\">30%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-12 14:25:10', 0, '2024-03-12 14:25:10', 0, b'1'), (5, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User C2_Ali has started her/his day Start at 3/15/2024 on 4:03 PM', 'Dear Client_2,  On 3/15/2024 User C2_Ali has started his/her day start on 4:03 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">P2</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 16:03:30', 0, '2024-03-15 16:03:30', 0, b'1'), (6, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Jannat has marked his/her day end at 2:24 PM', ' Dear Client_1, User Jannat has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">20%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>\r\n            <td style=\"background-color:#faf8f8;\">1</td>\r\n            <td style=\"background-color:#faf8f8;\">30%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-12 14:25:10', 0, '2024-03-12 14:25:10', 0, b'1'), (6, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User C2_Ali has started her/his day Start at 3/15/2024 on 4:05 PM', 'Dear Client_2,  On 3/15/2024 User C2_Ali has started his/her day start on 4:05 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">P2</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 16:05:50', 0, '2024-03-15 16:05:50', 0, b'1'), (7, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Jannat has started her/his day Start at 3/13/2024 on 2:26 PM', 'Dear Fatima,  On 3/13/2024 User Jannat has started his/her day start on 2:26 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">view Lectures of OOP </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">5</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:01', 0, '2024-03-13 14:26:01', 0, b'1'), (7, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User C2_Ali has marked his/her day end at 4:05 PM', ' Dear Client_2, User C2_Ali has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">20</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n			 <td style=\"background-color:#faf8f8;\">P2</td>\r\n			<td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">0</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 16:13:15', 0, '2024-03-15 16:13:15', 0, b'1'), (8, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Jannat has started her/his day Start at 3/13/2024 on 2:26 PM', 'Dear MuhammadBadar,  On 3/13/2024 User Jannat has started his/her day start on 2:26 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">view Lectures of OOP </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">5</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:01', 0, '2024-03-13 14:26:01', 0, b'1'), (8, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User C2_Ali has started her/his day Start at 3/15/2024 on 4:21 PM', 'Dear Client_2,  On 3/15/2024 User C2_Ali has started his/her day start on 4:21 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">P2</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 16:21:47', 0, '2024-03-15 16:21:47', 0, b'1'), (9, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Jannat has started her/his day Start at 3/13/2024 on 2:26 PM', 'Dear Client_1,  On 3/13/2024 User Jannat has started his/her day start on 2:26 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">view Lectures of OOP </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">5</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:01', 0, '2024-03-13 14:26:01', 0, b'1'), (9, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User C2_Ali has marked his/her day end at 4:21 PM', ' Dear Client_2, User C2_Ali has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">20%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n			 <td style=\"background-color:#faf8f8;\">P2</td>\r\n			<td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">1</td>\r\n            <td style=\"background-color:#faf8f8;\">0%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 16:22:00', 0, '2024-03-15 16:22:00', 0, b'1'), (10, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Jannat has marked his/her day end at 2:26 PM', ' Dear Fatima, User Jannat has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">60%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">70%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">view Lectures of OOP </td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">30%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:23', 0, '2024-03-13 14:26:23', 0, b'1'), (10, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User C2_Ali has started her/his day Start at 3/19/2024 on 2:29 PM', 'Dear Client_2,  On 3/19/2024 User C2_Ali has started his/her day start on 2:29 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th style=\"width: 50%;\">Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">P2</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technologies</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-19 14:29:44', 0, '2024-03-19 14:29:44', 0, b'1'), (11, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Jannat has marked his/her day end at 2:26 PM', ' Dear MuhammadBadar, User Jannat has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">60%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">70%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">view Lectures of OOP </td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">30%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:23', 0, '2024-03-13 14:26:23', 0, b'1'), (11, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User C2_Ali has marked his/her day end at 2:29 PM', ' Dear Client_2, User C2_Ali has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th style=\"width: 50%;\">Title</th>            <th >Priority</th>			<th >Due Time</th>			<th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">c2_ali_Task_2</td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">6</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">50%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">c2_ali_Task1..</td>\r\n			<td style=\"background-color:#faf8f8;\">P2</td>\r\n			<td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">30%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technologies</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-19 14:39:14', 0, '2024-03-19 14:39:14', 0, b'1'), (12, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Jannat has marked his/her day end at 2:26 PM', ' Dear Client_1, User Jannat has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Remove path of ManageCityStudent</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">60%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Create Table named Teachers </td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">70%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">view Lectures of OOP </td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">30%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:23', 0, '2024-03-13 14:26:23', 0, b'1'), (13, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Fatima has started her/his day Start at 3/13/2024 on 2:26 PM', 'Dear MuhammadBadar,  On 3/13/2024 User Fatima has started his/her day start on 2:26 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a new Schedule on any update in schedule.only if there is at least one entry of attendance against that schedule </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:54', 0, '2024-03-13 14:26:54', 0, b'1'), (14, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Fatima has started her/his day Start at 3/13/2024 on 2:26 PM', 'Dear Client_1,  On 3/13/2024 User Fatima has started his/her day start on 2:26 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a new Schedule on any update in schedule.only if there is at least one entry of attendance against that schedule </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:26:54', 0, '2024-03-13 14:26:54', 0, b'1'), (15, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Fatima has marked his/her day end at 2:26 PM', ' Dear MuhammadBadar, User Fatima has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">20%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a new Schedule on any update in schedule.only if there is at least one entry of attendance against that schedule </td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">100%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">70%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:27:37', 0, '2024-03-13 14:27:37', 0, b'1'), (16, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Fatima has marked his/her day end at 2:26 PM', ' Dear Client_1, User Fatima has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">20%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a new Schedule on any update in schedule.only if there is at least one entry of attendance against that schedule </td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">100%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">70%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:27:37', 0, '2024-03-13 14:27:37', 0, b'1'), (17, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Fatima has started her/his day Start at 3/14/2024 on 2:28 PM', 'Dear MuhammadBadar,  On 3/14/2024 User Fatima has started his/her day start on 2:28 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 14:28:45', 0, '2024-03-14 14:28:45', 0, b'1'), (18, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Fatima has started her/his day Start at 3/14/2024 on 2:28 PM', 'Dear Client_1,  On 3/14/2024 User Fatima has started his/her day start on 2:28 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 14:28:45', 0, '2024-03-14 14:28:45', 0, b'1'), (19, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User MuhammadBadar has started her/his day Start at 3/13/2024 on 2:35 PM', 'Dear Client_1,  On 3/13/2024 User MuhammadBadar has started his/her day start on 2:35 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to Merge KeyAccounting & TPT</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	\r\n</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">7</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:35:33', 0, '2024-03-13 14:35:33', 0, b'1'), (20, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User MuhammadBadar has marked his/her day end at 2:35 PM', ' Dear Client_1, User MuhammadBadar has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to Merge KeyAccounting & TPT</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">100%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	\r\n</td>\r\n            <td style=\"background-color:#faf8f8;\">5</td>\r\n            <td style=\"background-color:#faf8f8;\">60%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-13 14:36:20', 0, '2024-03-13 14:36:20', 0, b'1'), (21, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'jannat@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/14/2024 on 4:59 PM', 'Dear Jannat,  On 3/14/2024 User DoctorBilal has started his/her day start on 4:59 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 16:59:47', 0, '2024-03-14 16:59:47', 0, b'1'), (22, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/14/2024 on 4:59 PM', 'Dear Fatima,  On 3/14/2024 User DoctorBilal has started his/her day start on 4:59 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 16:59:47', 0, '2024-03-14 16:59:47', 0, b'1');
INSERT INTO `ntf_notificationlog` (`Id`, `ClientId`, `UserId`, `From`, `To`, `KeyCode`, `Subject`, `Body`, `SMS`, `IsSent`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (23, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'zaman.badar@gmail.com1', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/14/2024 on 4:59 PM', 'Dear MuhammadBadar,  On 3/14/2024 User DoctorBilal has started his/her day start on 4:59 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 16:59:47', 0, '2024-03-14 16:59:47', 0, b'1'), (24, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/14/2024 on 4:59 PM', 'Dear Client_1,  On 3/14/2024 User DoctorBilal has started his/her day start on 4:59 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 16:59:47', 0, '2024-03-14 16:59:47', 0, b'1'), (25, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'jannat@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 4:59 PM', ' Dear Jannat, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">70</td>\r\n            <td style=\"background-color:#faf8f8;\">Need some help</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 17:04:09', 0, '2024-03-14 17:04:09', 0, b'1'), (26, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 4:59 PM', ' Dear Fatima, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">70</td>\r\n            <td style=\"background-color:#faf8f8;\">Need some help</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 17:04:09', 0, '2024-03-14 17:04:09', 0, b'1'), (27, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'zaman.badar@gmail.com1', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 4:59 PM', ' Dear MuhammadBadar, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">70</td>\r\n            <td style=\"background-color:#faf8f8;\">Need some help</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 17:04:09', 0, '2024-03-14 17:04:09', 0, b'1'), (28, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 4:59 PM', ' Dear Client_1, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">3</td>\r\n            <td style=\"background-color:#faf8f8;\">70</td>\r\n            <td style=\"background-color:#faf8f8;\">Need some help</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-14 17:04:09', 0, '2024-03-14 17:04:09', 0, b'1'), (29, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'jannat@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/15/2024 on 5:19 PM', 'Dear Jannat,  On 3/15/2024 User DoctorBilal has started his/her day start on 5:19 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:19:53', 0, '2024-03-15 17:19:53', 0, b'1'), (30, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/15/2024 on 5:19 PM', 'Dear Fatima,  On 3/15/2024 User DoctorBilal has started his/her day start on 5:19 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:19:53', 0, '2024-03-15 17:19:53', 0, b'1'), (31, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'zaman.badar@gmail.com1', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/15/2024 on 5:19 PM', 'Dear MuhammadBadar,  On 3/15/2024 User DoctorBilal has started his/her day start on 5:19 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:19:53', 0, '2024-03-15 17:19:53', 0, b'1'), (32, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User DoctorBilal has started her/his day Start at 3/15/2024 on 5:19 PM', 'Dear Client_1,  On 3/15/2024 User DoctorBilal has started his/her day start on 5:19 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:19:53', 0, '2024-03-15 17:19:53', 0, b'1'), (33, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'jannat@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 5:19 PM', ' Dear Jannat, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">100</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InTesting</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:21:28', 0, '2024-03-15 17:21:28', 0, b'1'), (34, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 5:19 PM', ' Dear Fatima, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">100</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InTesting</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:21:28', 0, '2024-03-15 17:21:28', 0, b'1'), (35, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'zaman.badar@gmail.com1', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 5:19 PM', ' Dear MuhammadBadar, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">100</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InTesting</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:21:28', 0, '2024-03-15 17:21:28', 0, b'1'), (36, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 'bintameer212@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User DoctorBilal has marked his/her day end at 5:19 PM', ' Dear Client_1, User DoctorBilal has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">ManageTopic-Add validation for min & max length </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">100</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InTesting</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Manage CityStudent-Add validation messages on Dropdowns</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">100</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InTesting</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table>', '', b'1', '2024-03-15 17:21:28', 0, '2024-03-15 17:21:28', 0, b'1'), (37, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User MuhammadBadar has started her/his day Start at 3/15/2024 on 4:56 PM', 'Dear Client_1,  On 3/15/2024 User MuhammadBadar has started his/her day start on 4:56 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Start Working on SymmetricDS (Syncing Software)</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">7</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">	Define Trial Balance & General Ledger Reports in RMS.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	\r\n</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">7</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 16:56:05', 0, '2024-03-15 16:56:05', 0, b'1'), (38, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User MuhammadBadar has started her/his day Start at 3/15/2024 on 5:01 PM', 'Dear Client_1,  On 3/15/2024 User MuhammadBadar has started his/her day start on 5:01 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Start Working on SymmetricDS (Syncing Software)</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">7</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">	Define Trial Balance & General Ledger Reports in RMS.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Start Working on SymmetricDS (Syncing Software)</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">7</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">	Define Trial Balance & General Ledger Reports in RMS.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 17:01:39', 0, '2024-03-15 17:01:39', 0, b'1'), (39, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User MuhammadBadar has started her/his day Start at 3/15/2024 on 5:15 PM', 'Dear Client_1,  On 3/15/2024 User MuhammadBadar has started his/her day start on 5:15 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Start Working on SymmetricDS (Syncing Software)</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">7</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">	Define Trial Balance & General Ledger Reports in RMS.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">Need to introduce Scheduled Jobs</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">8</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 17:15:43', 0, '2024-03-15 17:15:43', 0, b'1'), (40, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User MuhammadBadar has started her/his day Start at 3/15/2024 on 5:29 PM', 'Dear Client_1,  On 3/15/2024 User MuhammadBadar has started his/her day start on 5:29 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing_1</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Start Working on SymmetricDS (Syncing Software)</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">7</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">	Define Trial Balance & General Ledger Reports in RMS.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-15 17:29:19', 0, '2024-03-15 17:29:19', 0, b'1'), (41, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User MuhammadBadar has marked his/her day end at 5:29 PM', ' Dear Client_1, User MuhammadBadar has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Testing_1</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">20%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Start Working on SymmetricDS (Syncing Software)</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">30%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Implement Multi tenancy in TPT project.	</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">7</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">70%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">	Define Trial Balance & General Ledger Reports in RMS.</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">10%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-18 20:20:26', 0, '2024-03-18 20:20:26', 0, b'1'), (42, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'zaman.badar@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Fatima has marked his/her day end at 2:28 PM', ' Dear MuhammadBadar, User Fatima has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">60%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">80%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">10%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">90%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-18 20:26:20', 0, '2024-03-18 20:26:20', 0, b'1'), (43, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'c1@gmail.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Fatima has marked his/her day end at 2:28 PM', ' Dear Client_1, User Fatima has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th >Title</th>            <th >Priority</th>			 <th >Due Time</th>			  <th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">60%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">1</td>            <td style=\"background-color:#faf8f8;\">80%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>			 <td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">10%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>\r\n			 <td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">4</td>\r\n            <td style=\"background-color:#faf8f8;\">90%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technology</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-18 20:26:20', 0, '2024-03-18 20:26:20', 0, b'1'), (44, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'zaman.badar@gmail.com1', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Fatima has started her/his day Start at 3/19/2024 on 2:49 PM', 'Dear MuhammadBadar,  On 3/19/2024 User Fatima has started his/her day start on 2:49 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th style=\"width: 35%;\">Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">The previous Day must be ended before starting the new Day.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technologies</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-19 14:49:39', 0, '2024-03-19 14:49:39', 0, b'1'), (45, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'info@qamsoft.com', 'ATT_NotificationToSupervisor_OnDayStart', 'MicroERP : User Fatima has started her/his day Start at 3/19/2024 on 2:49 PM', 'Dear QamSoft,  On 3/19/2024 User Fatima has started his/her day start on 2:49 PM by picking the following tasks :   <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th style=\"width: 35%;\">Title</th>            <th >Description</th>            <th >Due Time</th>            <th >Priority</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>            <td style=\"background-color:#faf8f8;\">---</td>            <td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">P0</td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">The previous Day must be ended before starting the new Day.</td>\r\n            <td style=\"background-color:#faf8f8;\">---</td>\r\n            <td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">P0</td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technologies</b></a>        </td>        </td>    </tr>', '', b'0', '2024-03-19 14:49:39', 0, '2024-03-19 14:49:39', 0, b'1'), (46, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'zaman.badar@gmail.com1', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Fatima has marked his/her day end at 2:49 PM', ' Dear MuhammadBadar, User Fatima has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th style=\"width: 35%;\">Title</th>            <th >Priority</th>			<th >Due Time</th>			<th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">8</td>            <td style=\"background-color:#faf8f8;\">60%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">0</td>            <td style=\"background-color:#faf8f8;\">80%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">0</td>            <td style=\"background-color:#faf8f8;\">10%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">0</td>            <td style=\"background-color:#faf8f8;\">90%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">The previous Day must be ended before starting the new Day.</td>\r\n			<td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">0</td>\r\n            <td style=\"background-color:#faf8f8;\">0%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technologies</b></a>        </td>        </td>    </tr>', '', b'1', '2024-03-20 01:29:35', 0, '2024-03-20 01:29:35', 0, b'1'), (47, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 'info@qamsoft.com', 'ATT_NotificationToSupervisor_OnDayEnd', 'MicroERP : User Fatima has marked his/her day end at 2:49 PM', ' Dear QamSoft, User Fatima has marked his/her day end by mentioning the following status: <table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>    <thead>        <tr style=\"background-color:#b5b8bf; color: white;\">            <th style=\"width: 35%;\">Title</th>            <th >Priority</th>			<th >Due Time</th>			<th >Worked Time</th>            <th >Percentage Completion</th>            <th >Comments</th>            <th >Status</th>        </tr>    </thead>    <tbody>        <!-- Detail Section Start Section 1 -->        <tr>            <td style=\"background-color:#faf8f8;\">Need to define Notification Templates (required for pre-alpha version) </td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">5</td>            <td style=\"background-color:#faf8f8;\">8</td>            <td style=\"background-color:#faf8f8;\">60%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">Define a page for Schedule History.</td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">4</td>            <td style=\"background-color:#faf8f8;\">0</td>            <td style=\"background-color:#faf8f8;\">80%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">While performing Day Start/End activity, Schedule of each user should be used </td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">3</td>            <td style=\"background-color:#faf8f8;\">0</td>            <td style=\"background-color:#faf8f8;\">10%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>              <tr>            <td style=\"background-color:#faf8f8;\">On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.</td>			<td style=\"background-color:#faf8f8;\">P0</td>			<td style=\"background-color:#faf8f8;\">2</td>            <td style=\"background-color:#faf8f8;\">0</td>            <td style=\"background-color:#faf8f8;\">90%</td>            <td style=\"background-color:#faf8f8;\"></td>            <td style=\"background-color:#faf8f8;\">InProgress</td>        </tr>      \r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">The previous Day must be ended before starting the new Day.</td>\r\n			<td style=\"background-color:#faf8f8;\">P0</td>\r\n			<td style=\"background-color:#faf8f8;\">2</td>\r\n            <td style=\"background-color:#faf8f8;\">0</td>\r\n            <td style=\"background-color:#faf8f8;\">0%</td>\r\n            <td style=\"background-color:#faf8f8;\"></td>\r\n            <td style=\"background-color:#faf8f8;\">InProgress</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->    </tbody></table><tr>        <td style=\"padding-top:40px;\">            <br /><span>Regards, </span><br />            <span>QamSoft - WebMaster</span>            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'www.qamsoft.com\'                target=\"_blank\"><b>QamSoft Technologies</b></a>        </td>        </td>    </tr>', '', b'0', '2024-03-20 01:29:35', 0, '2024-03-20 01:29:35', 0, b'1');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ntf_notificationtemplate
-- ----------------------------
BEGIN;
INSERT INTO `ntf_notificationtemplate` (`Id`, `ClientId`, `KeyCode`, `TemplateName`, `Subject`, `Body`, `SMS`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1, 1, 'ATT_NotificationToSupervisor_OnDayStart', '', 'MicroERP : User #USER# has started her/his day Start at #DATE# on #TIME#', 'Dear #SUPERVISOR#, \r\n On #DATE# User #USER# has started his/her day start on #TIME# by picking the following tasks : \r\n  #TASKS#\r\n<tr>\r\n        <td style=\"padding-top:40px;\">\r\n            <br /><span>Regards, </span><br />\r\n            <span>#WEBMASTERDISPLAYNAME#</span>\r\n            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'#SITEURL#\'\r\n                target=\"_blank\"><b>#COMPANYNAME#</b></a>\r\n        </td>\r\n        </td>\r\n    </tr>', '', '1900-01-01 00:00:00', 1, NULL, NULL, b'1'), (2, 1, 'ATT_NotificationToSupervisor_OnDayEnd', NULL, 'MicroERP : User #USER# has marked his/her day end at #TIME#', ' Dear #SUPERVISOR#, User #USER# has marked his/her day end by mentioning the following status: #DAYENDSTATUS#\r\n<tr>\r\n        <td style=\"padding-top:40px;\">\r\n            <br /><span>Regards, </span><br />\r\n            <span>#WEBMASTERDISPLAYNAME#</span>\r\n            <hr style=\"width: 100%; text-align: left; \">This is a system generated message by <a href=\'#SITEURL#\'\r\n                target=\"_blank\"><b>#COMPANYNAME#</b></a>\r\n        </td>\r\n        </td>\r\n    </tr>', NULL, '1999-01-10 00:00:00', 1, NULL, NULL, b'1'), (3, 1, 'ATT_TaskDetail', NULL, 'Task Detail', '<table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">\r\n    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>\r\n    <thead>\r\n        <tr style=\"background-color:#b5b8bf; color: white;\">\r\n            <th style=\"width: 35%;\">Title</th>\r\n            <th >Description</th>\r\n            <th >Due Time</th>\r\n            <th >Priority</th>\r\n            <th >Status</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n        <!-- Detail Section Start Section 1 -->\r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_TITLE#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_DESCRIPTION#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_DUE_TIME#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_PRIORITY#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_STATUS#</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->\r\n\r\n    </tbody>\r\n</table>', NULL, '1999-01-10 00:00:00', 1, NULL, NULL, b'1'), (4, 1, 'ATT_DayEndTaskDetail', NULL, 'Task Detail', '\r\n<table style=\"border:0px solid gray; text-align: center;\" cellpadding=0 width=\"100%\">\r\n    <caption style=\"margin-top:15px;padding:5px; background-color: #075376; color: white; text-align: center;\">Task Details</caption>\r\n    <thead>\r\n        <tr style=\"background-color:#b5b8bf; color: white;\">\r\n            <th style=\"width: 35%;\">Title</th>\r\n            <th >Priority</th>\r\n			<th >Due Time</th>\r\n			<th >Worked Time</th>\r\n            <th >Percentage Completion</th>\r\n            <th >Comments</th>\r\n            <th >Status</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n        <!-- Detail Section Start Section 1 -->\r\n        <tr>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_FORMATTED_TITLE#</td>\r\n			<td style=\"background-color:#faf8f8;\">#TMS_PRIORITY#</td>\r\n			<td style=\"background-color:#faf8f8;\">#TMS_DUE_TIME#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_WORKTIME#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_CLAIM_PERCENT#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_COMMENTS#</td>\r\n            <td style=\"background-color:#faf8f8;\">#TMS_STATUS#</td>\r\n        </tr>\r\n      <!-- Detail Section End Section 1 -->\r\n\r\n    </tbody>\r\n</table>', NULL, '1999-01-10 00:00:00', 1, NULL, NULL, b'1');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pms_appointment
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '	';

-- ----------------------------
-- Records of pms_doctor
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci COMMENT = '	';

-- ----------------------------
-- Records of pms_patient
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pms_patientreport
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pms_prescription
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pms_ptextrafieldsdata
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pms_rxextrafieldsdata
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pms_rxmedicine
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for pur_purchase
-- ----------------------------
DROP TABLE IF EXISTS `pur_purchase`;
CREATE TABLE `pur_purchase`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pur_purchase
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for pur_purchaseline
-- ----------------------------
DROP TABLE IF EXISTS `pur_purchaseline`;
CREATE TABLE `pur_purchaseline`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of pur_purchaseline
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for sal_deliverychallan
-- ----------------------------
DROP TABLE IF EXISTS `sal_deliverychallan`;
CREATE TABLE `sal_deliverychallan`  (
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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_deliverychallan
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for sal_deliverychallandetail
-- ----------------------------
DROP TABLE IF EXISTS `sal_deliverychallandetail`;
CREATE TABLE `sal_deliverychallandetail`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `DeliveryChallanId` int NOT NULL,
  `ProductId` int NULL DEFAULT NULL,
  `Qty` int NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `CreatedOn` datetime NOT NULL,
  `CreatedById` int NOT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`, `ClientId`, `DeliveryChallanId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_deliverychallandetail
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_documentextras
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_itemuom
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_sale
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_saleline
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_salestock
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_stocktransfer
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sal_stocktransferline
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sch_schedule
-- ----------------------------
BEGIN;
INSERT INTO `sch_schedule` (`Id`, `ClientId`, `UserId`, `RoleId`, `EntityId`, `ScheduleTypeId`, `WorkingTypeId`, `WorkingHours`, `StartDate`, `EndDate`, `EffectiveDate`, `CreatedBy`, `CreatedOn`, `ModifiedBy`, `ModifiedOn`, `IsActive`) VALUES (1, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 13:59:53', NULL, NULL, b'1'), (1, 2, '85060824-9132-4191-aa91-fe3942f00969', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-07 00:21:53', NULL, NULL, b'1'), (1, 3, '03081917-f57d-4278-90f7-c0528bd16b5c', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-06 11:08:01', NULL, NULL, b'1'), (2, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 14:01:04', NULL, NULL, b'1'), (2, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 16:00:47', NULL, NULL, b'1'), (2, 3, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-06 15:53:19', NULL, NULL, b'1'), (3, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 14:03:07', NULL, NULL, b'1'), (3, 2, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 16:00:47', NULL, NULL, b'1'), (4, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 16:51:45', 0, '2024-03-15 17:18:10', b'0'), (4, 2, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 16:00:47', NULL, NULL, b'1'), (5, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 16:51:45', NULL, NULL, b'1'), (5, 2, 'fa631a98-beb3-462a-97cb-f43a58ac9510', NULL, 1102001, 1103002, 0, NULL, NULL, NULL, NULL, 0, '2024-03-14 16:00:47', NULL, NULL, b'1');
COMMIT;

-- ----------------------------
-- Table structure for sch_scheduleday
-- ----------------------------
DROP TABLE IF EXISTS `sch_scheduleday`;
CREATE TABLE `sch_scheduleday`  (
  `Id` int NOT NULL,
  `ClientId` int NOT NULL,
  `SchId` int NULL DEFAULT NULL,
  `DayId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `WorkTime` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `CreatedBy` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `ModifiedBy` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sch_scheduleday
-- ----------------------------
BEGIN;
INSERT INTO `sch_scheduleday` (`Id`, `ClientId`, `SchId`, `DayId`, `WorkTime`, `CreatedBy`, `CreatedOn`, `ModifiedBy`, `ModifiedOn`, `IsActive`) VALUES (1, 1, 1, '1101001', '8', 0, '2024-03-14 13:59:53', 0, '2024-03-14 13:59:53', b'1'), (1, 2, 1, '1101004', NULL, 0, '2024-03-07 00:21:53', NULL, NULL, b'1'), (1, 3, 1, '1101001', '2', 0, '2024-03-06 11:08:30', 0, '2024-03-06 11:12:24', b'1'), (2, 1, 1, '1101002', '8', 0, '2024-03-14 13:59:53', 0, '2024-03-14 14:00:11', b'1'), (2, 2, 2, '1101001', NULL, 0, '2024-03-14 16:00:47', NULL, NULL, b'1'), (2, 3, 2, '1101003', NULL, 0, '2024-03-06 15:53:19', NULL, NULL, b'1'), (3, 1, 1, '1101003', '8', 0, '2024-03-14 13:59:53', 0, '2024-03-14 14:00:19', b'1'), (3, 2, 2, '1101002', '', 0, '2024-03-14 16:00:47', 0, '2024-03-14 16:02:27', b'1'), (4, 1, 1, '1101004', '8', 0, '2024-03-14 13:59:53', 0, '2024-03-14 14:00:27', b'1'), (4, 2, 3, '1101001', NULL, 0, '2024-03-14 16:48:49', NULL, NULL, b'1'), (5, 1, 1, '1101005', '8', 0, '2024-03-14 13:59:53', 0, '2024-03-14 14:00:39', b'1'), (5, 2, 3, '1101002', NULL, 0, '2024-03-14 16:48:49', NULL, NULL, b'1'), (6, 1, 2, '1101001', '9', 0, '2024-03-14 14:01:04', 0, '2024-03-14 14:01:04', b'1'), (6, 2, 4, '1101001', NULL, 0, '2024-03-19 20:23:24', NULL, NULL, b'1'), (7, 1, 2, '1101002', NULL, 0, '2024-03-14 14:01:04', NULL, NULL, b'1'), (7, 2, 4, '1101002', NULL, 0, '2024-03-19 20:23:24', NULL, NULL, b'1'), (8, 1, 2, '1101003', '8', 0, '2024-03-14 14:01:04', 0, '2024-03-14 14:01:44', b'1'), (8, 2, 5, '1101001', NULL, 0, '2024-03-19 20:30:34', NULL, NULL, b'1'), (9, 1, 2, '1101004', '9', 0, '2024-03-14 14:01:04', 0, '2024-03-14 14:01:53', b'1'), (9, 2, 5, '1101002', NULL, 0, '2024-03-19 20:30:34', NULL, NULL, b'1'), (10, 1, 2, '1101005', NULL, 0, '2024-03-14 14:01:04', NULL, NULL, b'1'), (11, 1, 2, '1101006', '7', 0, '2024-03-14 14:01:04', 0, '2024-03-14 14:02:41', b'1'), (12, 1, 3, '1101004', '4', 0, '2024-03-14 14:03:07', 0, '2024-03-14 14:03:07', b'1'), (13, 1, 3, '1101005', NULL, 0, '2024-03-14 14:03:07', NULL, NULL, b'1'), (14, 1, 3, '1101006', NULL, 0, '2024-03-14 14:03:07', NULL, NULL, b'1'), (15, 1, 4, '1101001', NULL, 0, '2024-03-14 16:51:45', NULL, NULL, b'1'), (16, 1, 4, '1101002', '6', 0, '2024-03-14 16:51:45', 0, '2024-03-14 16:53:00', b'1'), (17, 1, 4, '1101004', NULL, 0, '2024-03-14 16:51:45', NULL, NULL, b'1'), (18, 1, 5, '1101001', NULL, 0, '2024-03-15 17:18:20', NULL, NULL, b'1'), (19, 1, 5, '1101002', NULL, 0, '2024-03-15 17:18:20', NULL, NULL, b'1'), (20, 1, 5, '1101004', NULL, 0, '2024-03-15 17:18:21', NULL, NULL, b'1'), (21, 1, 5, '1101005', '4', 0, '2024-03-15 17:18:21', 0, '2024-03-15 17:18:21', b'1');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sch_scheduledayevent
-- ----------------------------
BEGIN;
INSERT INTO `sch_scheduledayevent` (`Id`, `ClientId`, `SchId`, `ScheduleDayId`, `StartTime`, `EndTime`, `LocationId`, `EventTypeId`, `Sp`, `CreatedById`, `CreatedOn`, `ModifiedById`, `ModifiedOn`, `IsActive`) VALUES (1, 1, 2, 7, '07:00', '11:00', 0, 0, NULL, 0, '2024-03-14 14:01:28', 0, '2024-03-14 14:01:28', b'1'), (1, 2, 1, 1, '09:00', '12:30', 0, 0, NULL, 0, '2024-03-07 00:22:11', 0, '2024-03-07 00:22:11', b'1'), (2, 1, 2, 7, '12:00', '15:00', 0, 0, NULL, 0, '2024-03-14 14:01:40', 0, '2024-03-14 14:01:40', b'1'), (2, 2, 1, 1, '12:30', '17:00', 0, 0, NULL, 0, '2024-03-07 00:22:27', 0, '2024-03-07 00:22:27', b'1'), (3, 1, 2, 10, '08:00', '12:00', 0, 0, NULL, 0, '2024-03-14 14:02:28', 0, '2024-03-14 14:02:28', b'1'), (3, 2, 2, 2, '08:00', '12:00', 0, 0, NULL, 0, '2024-03-14 16:02:25', 0, '2024-03-14 16:02:25', b'1'), (4, 1, 2, 10, '13:00', '20:00', 0, 0, NULL, 0, '2024-03-14 14:02:38', 0, '2024-03-14 14:02:38', b'1'), (4, 2, 3, 5, '08:00', '12:00', 0, 0, NULL, 0, '2024-03-14 16:48:49', 0, '2024-03-14 16:48:49', b'1'), (5, 1, 3, 13, '09:00', '12:45', 0, 0, NULL, 0, '2024-03-14 14:03:33', 0, '2024-03-14 14:03:33', b'1'), (5, 2, 4, 7, '08:00', '12:00', 0, 0, NULL, 0, '2024-03-19 20:23:24', 0, '2024-03-19 20:23:24', b'1'), (6, 1, 3, 13, '13:00', '18:00', 0, 0, NULL, 0, '2024-03-14 14:04:04', 0, '2024-03-14 14:04:04', b'1'), (6, 2, 5, 9, '08:00', '12:00', 0, 0, NULL, 0, '2024-03-19 20:30:34', 0, '2024-03-19 20:30:34', b'1'), (7, 1, 3, 14, '11:00', '21:00', 0, 0, NULL, 0, '2024-03-14 14:04:31', 0, '2024-03-14 14:04:31', b'1'), (8, 1, 4, 15, '08:00', '12:00', 0, 0, NULL, 0, '2024-03-14 16:52:28', 0, '2024-03-14 16:52:28', b'1'), (9, 1, 4, 15, '14:00', '16:00', 0, 0, NULL, 0, '2024-03-14 16:52:49', 0, '2024-03-14 16:52:49', b'1'), (10, 1, 4, 17, '08:00', '11:00', 0, 0, NULL, 0, '2024-03-14 16:54:02', 0, '2024-03-14 16:54:02', b'1');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sec_permission
-- ----------------------------
BEGIN;
INSERT INTO `sec_permission` (`Id`, `ClientId`, `UserId`, `RoleId`, `RouteId`, `PermissionId`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1, 0, '', 1054001, 1001005, 1056003, '2024-03-09 11:57:17', 0, '2024-03-09 11:57:17', 0, b'1'), (2, 0, '', 1054001, 1001008, 1056003, '2024-03-14 12:33:36', 0, '2024-03-14 12:33:36', 0, b'1'), (2, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1001008, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (2, 2, '85060824-9132-4191-aa91-fe3942f00969', 0, 1002003, 1056001, '2024-03-19 17:29:27', 0, '2024-03-19 17:29:27', 0, b'1'), (2, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002003, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (3, 0, '', 1054001, 1001009, 1056003, '2024-03-14 12:33:36', 0, '2024-03-14 12:33:36', 0, b'1'), (3, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1001009, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (3, 2, '85060824-9132-4191-aa91-fe3942f00969', 0, 1002021, 1056001, '2024-03-19 17:29:27', 0, '2024-03-19 17:29:27', 0, b'1'), (3, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002021, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (4, 0, '', 1054001, 1002001, 1056003, '2024-03-03 01:10:14', 0, '2024-03-03 01:10:14', 0, b'1'), (4, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1002023, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (4, 2, '85060824-9132-4191-aa91-fe3942f00969', 0, 1002030, 1056003, '2024-03-19 17:29:27', 0, '2024-03-19 17:29:27', 0, b'1'), (4, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002030, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (5, 0, '', 1054001, 1002003, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (5, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1002026, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (6, 0, '', 1054001, 1002021, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (6, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1002024, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (6, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002023, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (7, 0, '', 1054001, 1002022, 1056003, '2024-03-03 01:10:14', 0, '2024-03-03 01:10:14', 0, b'1'), (7, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1002025, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (8, 0, '', 1054001, 1002018, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (8, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002026, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (9, 0, '', 1054001, 1002019, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (9, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1002023, 1056003, '2024-03-18 20:24:36', 0, '2024-03-18 20:24:36', 0, b'1'), (9, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002024, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (10, 0, '', 1054001, 1002020, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (10, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1002026, 1056003, '2024-03-18 20:24:36', 0, '2024-03-18 20:24:36', 0, b'1'), (10, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002025, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (11, 0, '', 1054001, 1002007, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (11, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1002024, 1056003, '2024-03-18 20:24:36', 0, '2024-03-18 20:24:36', 0, b'1'), (11, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002031, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (12, 0, '', 1054001, 1002008, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (12, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1002025, 1056003, '2024-03-18 20:24:36', 0, '2024-03-18 20:24:36', 0, b'1'), (12, 4, 'fa631a98-beb3-462a-97cb-f43a58ac9510', 0, 1002032, 1056003, '2024-03-20 01:39:45', 0, '2024-03-20 01:39:45', 0, b'1'), (13, 0, '', 1054001, 1002016, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (14, 0, '', 1054001, 1002017, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (14, 1, '', 1054001, 1002003, 1056002, '2024-03-19 01:55:32', 0, '2024-03-19 01:55:32', 0, b'1'), (15, 0, '', 1054001, 1002023, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (15, 1, '', 1054001, 1002021, 1056003, '2024-03-19 01:55:32', 0, '2024-03-19 01:55:32', 0, b'1'), (16, 0, '', 1054001, 1002024, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (17, 0, '', 1054001, 1002025, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (17, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1002003, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (18, 0, '', 1054001, 1003001, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (18, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1002021, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (19, 0, '', 1054001, 1003002, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (19, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1002030, 1056002, '2024-03-19 02:44:07', 0, '2024-03-19 02:44:07', 0, b'1'), (20, 0, '', 1054001, 1003003, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (21, 0, '', 1054001, 1003004, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (22, 0, '', 1054002, 1002023, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (23, 0, '', 1054002, 1002024, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (24, 0, '', 1054002, 1002025, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (25, 0, '', 1054002, 1002018, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (26, 0, '', 1054002, 1002019, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (27, 0, '', 1054002, 1002020, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (28, 0, '', 1054002, 1002007, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (29, 0, '', 1054002, 1002008, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (30, 0, '', 1054002, 1002016, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (31, 0, '', 1054002, 1003001, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (32, 0, '', 1054002, 1003002, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (33, 0, '', 1054002, 1003003, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (34, 0, '', 1054002, 1003004, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (35, 0, '', 1054002, 1001010, 1056003, '2024-03-04 12:00:21', 0, '2024-03-04 12:00:21', 0, b'1'), (36, 0, '', 1054002, 1001008, 1056003, '2024-03-19 01:49:48', 0, '2024-03-19 01:49:48', 0, b'1'), (37, 0, '', 1054002, 1001009, 1056003, '2024-03-19 01:49:48', 0, '2024-03-19 01:49:48', 0, b'1'), (38, 0, '', 1054001, 1001001, 1056003, '2024-03-09 11:57:17', 0, '2024-03-09 11:57:17', 0, b'1'), (39, 0, '', 1054001, 1001011, 1056003, '2024-03-09 11:57:17', 0, '2024-03-09 11:57:17', 0, b'1'), (40, 0, '', 1054002, 1002003, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (41, 0, '', 1054002, 1002021, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (43, 0, '', 1054002, 1002026, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (45, 0, '', 1054001, 1002026, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (46, 0, '', 1054001, 1002027, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (47, 0, '', 1054001, 1002028, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (48, 0, '', 1054001, 1002029, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (50, 0, '', 1054001, 1002030, 1056003, '2024-03-19 17:10:30', 0, '2024-03-19 17:10:30', 0, b'1'), (52, 0, '', 1054002, 1002030, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (54, 0, '', 1054002, 1002028, 0, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (56, 0, '', 1054002, 1002031, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (57, 0, '', 1054002, 1002032, 1056003, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (65, 0, '', 1054002, 1002029, 0, '2024-03-19 17:54:06', 0, '2024-03-19 17:54:06', 0, b'1'), (67, 0, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', 0, 1002003, 0, '2024-03-19 17:51:49', 0, '2024-03-19 17:51:49', 0, b'1'), (68, 0, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', 0, 1002021, 1056003, '2024-03-19 17:51:49', 0, '2024-03-19 17:51:49', 0, b'1'), (69, 0, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', 0, 1002030, 1056002, '2024-03-19 17:51:49', 0, '2024-03-19 17:51:49', 0, b'1'), (71, 0, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', 0, 1002003, 1056002, '2024-03-19 21:11:16', 0, '2024-03-19 21:11:16', 0, b'1'), (72, 0, 'd76ffa59-4bdd-43ff-8ab5-53f247fe2276', 0, 1002021, 1056002, '2024-03-19 21:11:16', 0, '2024-03-19 21:11:16', 0, b'1');
COMMIT;

-- ----------------------------
-- Table structure for sec_user
-- ----------------------------
DROP TABLE IF EXISTS `sec_user`;
CREATE TABLE `sec_user`  (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClientId` int NOT NULL,
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
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of sec_user
-- ----------------------------
BEGIN;
INSERT INTO `sec_user` (`Id`, `ClientId`, `SupervisorId`, `ModuleId`, `RoleId`, `FirstName`, `DirectSupervisorName`, `DirectSupervisorId`, `LastName`, `UserPassword`, `Name`, `EmployeeId`, `FatherName`, `CNIC`, `Address`, `Designation`, `MSCardNo`, `DiscountLimit`, `BranchId`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`, `IsActive`) VALUES ('03081917-f57d-4278-90f7-c0528bd16b5c', 3, 'bb253b3d-aefc-492e-9d1d-d3b2d5224585', 0, 1054001, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'C3_Ali', 'C3_ALI', 'c3_ali@gmail.com', 'C3_ALI@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEAykgZL3H3U7BX3r3ugoLMPnJm9VD5/cyIoIuS/tB9qE8Sv86I+A0hh39D+LyJ7AiQ==', 'KWH5SPWO5QOFUQ443MSQMJHIWJZ4T2HG', '4371f2ee-3d82-4d9e-ad89-11b72bc8078b', '0989898989890', 0, 0, NULL, 1, 0, NULL), ('1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', 0, 1054003, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'MuhammadBadar', 'MUHAMMADBADAR', 'zaman.badar@gmail.com1', 'ZAMAN.BADAR@GMAIL.COM1', 0, 'AQAAAAEAACcQAAAAEGTsgNifhbgu5s7G5LKDje95MCHaS0qyjTGBnU58BUfwEWARfkIzXLDBV7qMw86XcQ==', 'WPPEGJGTAZ4H3CNGIBK4SLET57LZVPN6', '5c4dca3b-7bca-4484-866f-5837548108ea', '027162809808', 0, 0, NULL, 1, 0, NULL), ('45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'QamSoft', 'QAMSOFT', 'info@qamsoft.com', 'INFO@QAMSOFT.COM', 0, 'AQAAAAEAACcQAAAAEDKUPIieB1GLQayer2EqTjquCNjMT9m+HuqM/m5lqa7SVbkASjjBiOq+q2/tipTJJQ==', 'RTOE6QE7A2A6DTQTIEQ3RKBW5FQFHPXQ', '24effc84-46b0-479c-a04a-9f5d012514ed', '07878787877', 0, 0, NULL, 1, 0, NULL), ('464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Jannat', 'JANNAT', 'jannat@gmail.com', 'JANNAT@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEG04jA/3xfCyPYh8pkL1LyDQYkNCK+t7h0CdxGHsTZyNSqxba9p7YnKNw+bqR9knOA==', 'Q3ERBJIDF27XO7TILSCQ6EPJQ6NV3FBE', 'f16e8b99-3f82-4a82-9469-9d773ad1d6b5', '08989977777', 0, 0, NULL, 1, 0, NULL), ('85060824-9132-4191-aa91-fe3942f00969', 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', 0, 1054001, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'C2_Ali', 'C2_ALI', 'c2_ali@gmail.com', 'C2_ALI@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEB5O/+DtwfEcZRCyoVyWXibzX+hv2sFMEGE69k09buC0U+gJ8ylcZJDvalYvQgjfog==', 'NFBALC2AUJHA2XRYYWVS7YU64BDY2CLK', 'db8ce779-5c9f-45a3-8a35-ffc6a87b4cb7', '08979797988', 0, 0, NULL, 1, 0, NULL), ('8f1d8749-fa43-4c23-a7c6-70f41e098319', 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1054001, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Fatima', 'FATIMA', 'bintameer212@gmail.com', 'BINTAMEER212@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEPgKWahj+Dpw2AIgnEGFWqFmR0Fg82WJgYiZNWkvvQdVfFmCimcB3sqKQVoTYzbHzg==', 'XLAX3CIBKPJNKIWL6AG4PNIY53CHVSNJ', '11cf5e0b-127a-49d2-b2cd-fdd4d3244b04', '087688798789', 0, 0, NULL, 1, 0, NULL), ('bb253b3d-aefc-492e-9d1d-d3b2d5224585', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Client_3', 'CLIENT_3', 'c3@gmail.com', 'C3@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEKFo9dQUE4ZHbYcTVnNpYFARpjUpbdsRCO9NS2V+iZAdyhjHF2OEZkGc/VEjqu23YA==', 'O7ZMXEW675NT6UT5LURNOBXJYVEOEVTB', '29470ce5-5191-4e3b-b1ce-e4afd6889f3b', '87987897333', 0, 0, NULL, 1, 0, NULL), ('c35d2b3b-6cb9-480b-9868-ec8dbf343783', 0, NULL, 0, 1054001, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'QST', 'QST', 'S@gmail.com', 'S@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEAIJK2fwgLIzn8ZXyDL2Dpb9AikPUsit1ESdKHzW0NRkK2q0kJXhxJsScA2/LuXpcA==', '52CBWMIWBJM4UK3P23RQTUGYJML62H4U', '44b8b67d-45a6-4383-8602-b0910fe56182', '078979809', 0, 0, NULL, 1, 0, NULL), ('c820114a-c1f2-462e-b813-f560bd7065b1', 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, 1054001, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'DoctorBilal', 'DOCTORBILAL', 'bilal@gmail.com', 'BILAL@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEJ1oA7BxjKdA2GCQwNuCrDaeMDpWe2Qcv+JunHHbL6k13S2xpTykLhVBqjng6SlCig==', '7LGH7ANPIL54EVCQ4JBTOKKQYRGVIEJF', '04c57d65-a728-4bc0-9086-239a6624d7fc', '09808080888', 0, 0, NULL, 1, 0, NULL), ('d76ffa59-4bdd-43ff-8ab5-53f247fe2276', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Client_4', 'CLIENT_4', 'c4@gmail.com', 'C4@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAELg1/M25LWE95rmY9U9sI7vEsoOxf6LAEEQrO8LfiyckSihCw337Fdzgy08ymyssDA==', 'LW4KE62N54FK4UBMJOP5VFAN74EW5GSF', '96e47951-15e8-424a-97e9-da8db41b3a5f', '09887977777', 0, 0, NULL, 1, 0, NULL), ('d948db99-2cb2-4e17-ba0d-f6387444fafe', 0, NULL, 0, 1054002, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Client_2', 'CLIENT_2', 'c2@gmail.com', 'C2@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAEBcAgdWZxtDcU+5ndMl+tbk86TRjxGfoQEyCRkFvCEHjQTCNPR0kTr7IkC6Jico7bA==', 'G2LDZOSBUSEKGEPCK5DVD4T4F2ZAGRHB', '9cd63921-7d3e-4987-ab8c-7f2f5907d0a1', '089798798789', 0, 0, NULL, 1, 0, NULL), ('fa631a98-beb3-462a-97cb-f43a58ac9510', 4, NULL, 0, 1054001, NULL, NULL, NULL, NULL, '7890', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'c4_ali', 'C4_ALI', 'c4_ali@gmail.com', 'C4_ALI@GMAIL.COM', 0, 'AQAAAAEAACcQAAAAECncw4+/ELNXFbI7Zvxu1Nxhz6xnD8PUX3Vq+9RgzsbEO1CGaF6Z5TyCvsf68BOHrA==', 'C2JYGZQQDZHDQ6BFYO3OP32CF3PYOCPZ', '715bfad9-0b1f-4490-b397-0296efd527a7', '0789798798798', 0, 0, NULL, 1, 0, NULL);
COMMIT;

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
  INDEX `TaskId`(`TaskId` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of tms_attachments
-- ----------------------------
BEGIN;
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of tms_task
-- ----------------------------
BEGIN;
INSERT INTO `tms_task` (`Id`, `ClientId`, `UserId`, `ModuleId`, `StatusId`, `PriorityId`, `Title`, `SP`, `Description`, `Reason`, `CreatedOn`, `CreatedById`, `ModifiedOn`, `ModifiedById`, `IsActive`) VALUES (1001, 0, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', 0, 1107001, 1108001, 'c1_Task_1', 4, NULL, NULL, '2024-03-07 16:29:37', 0, '2024-03-07 16:29:37', 0, 1), (1001, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107001, 1108001, '	Design a SignUp Form ', 6, NULL, NULL, '2024-03-14 14:05:50', 0, '2024-03-14 09:04:47', 0, 1), (1001, 2, '85060824-9132-4191-aa91-fe3942f00969', 0, 1107002, 1108003, 'c2_ali_Task1..', 3, NULL, NULL, '2024-03-19 14:39:13', 0, '2024-03-19 14:39:13', 0, 1), (1001, 3, '03081917-f57d-4278-90f7-c0528bd16b5c', 0, 1107001, 1108001, 'c3_ali_Task_1', 2, NULL, NULL, '2024-03-07 01:03:43', 0, '2024-03-06 20:03:06', 0, 1), (1002, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107001, 1108001, 'SignUp page \"Continue\" button', 2, NULL, NULL, '2024-03-14 14:05:50', 0, '2024-03-14 09:04:47', 0, 1), (1002, 2, '85060824-9132-4191-aa91-fe3942f00969', 0, 1107002, 1108001, 'c2_ali_Task_2', 6, NULL, '', '2024-03-19 14:39:13', 0, '2024-03-19 14:39:13', 0, 1), (1003, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107001, 1108001, 'Define a popup for the  verification of email by OTP', 7, NULL, NULL, '2024-03-14 14:05:50', 0, '2024-03-14 09:04:47', 0, 1), (1003, 2, 'd948db99-2cb2-4e17-ba0d-f6387444fafe', 0, 1107002, 1108001, 'c2_task_1', 2, NULL, NULL, '2024-03-15 16:02:33', 0, '2024-03-15 16:02:33', 0, 1), (1004, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107002, 1108001, 'On the Next Day the Tasks Started Previously but not Completed, should be on Top, and already Selected.', 2, NULL, NULL, '2024-03-20 01:29:35', 0, '2024-03-20 01:29:35', 0, 1), (1005, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107002, 1108001, 'While performing Day Start/End activity, Schedule of each user should be used ', 3, NULL, NULL, '2024-03-20 01:29:35', 0, '2024-03-20 01:29:35', 0, 1), (1006, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107002, 1108001, 'Define a page for Schedule History.', 4, NULL, NULL, '2024-03-20 01:29:35', 0, '2024-03-20 01:29:35', 0, 1), (1007, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107003, 1108001, 'Define a new Schedule on any update in schedule.only if there is at least one entry of attendance against that schedule ', 2, NULL, NULL, '2024-03-13 14:27:37', 0, '2024-03-13 14:27:37', 0, 1), (1008, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107002, 1108001, 'Need to define Notification Templates (required for pre-alpha version) ', 5, NULL, NULL, '2024-03-20 01:29:35', 0, '2024-03-20 01:29:35', 0, 1), (1009, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, 1107001, 1108001, 'Manage AssignTask-Add validation on checkbox', 2, NULL, NULL, '2024-03-14 14:05:50', 0, '2024-03-14 09:04:47', 0, 1), (1010, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 0, 1107001, 1108001, 'Manage AssignTask-Define service method &  Controller Method named Assigntask', 7, NULL, NULL, '2024-03-14 16:55:13', 0, '2024-03-14 16:55:13', 0, 1), (1011, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, 1107001, 1108001, 'ManageTopic-Add validation on Description and also add validation message for min&max length', 2, NULL, NULL, '2024-03-14 14:05:50', 0, '2024-03-14 09:04:47', 0, 1), (1012, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 0, 1107003, 1108001, 'ManageTopic-Add validation for min & max length ', 4, NULL, NULL, '2024-03-15 17:21:27', 0, '2024-03-15 17:21:27', 0, 1), (1013, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 0, 1107003, 1108001, 'Manage CityStudent-Add validation messages on Dropdowns', 2, NULL, NULL, '2024-03-15 17:21:27', 0, '2024-03-15 17:21:27', 0, 1), (1014, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, 1107002, 1108001, 'view Lectures of OOP ', 5, NULL, NULL, '2024-03-13 14:26:23', 0, '2024-03-13 14:26:23', 0, 1), (1015, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, 1107002, 1108001, 'Create Table named Teachers ', 3, NULL, NULL, '2024-03-13 14:26:23', 0, '2024-03-13 14:26:23', 0, 1), (1016, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 0, 1107002, 1108001, 'Remove path of ManageCityStudent', 6, NULL, NULL, '2024-03-13 14:26:23', 0, '2024-03-13 14:26:23', 0, 1), (1017, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1107002, 1108001, '	Define Trial Balance & General Ledger Reports in RMS.', 2, NULL, '', '2024-03-18 20:20:26', 0, '2024-03-18 20:20:26', 0, 1), (1018, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1107002, 1108001, 'Implement Multi tenancy in TPT project.	\r\n', 7, NULL, NULL, '2024-03-18 20:20:26', 0, '2024-03-18 20:20:26', 0, 1), (1019, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1107002, 1108001, 'Start Working on SymmetricDS (Syncing Software)', 2, NULL, NULL, '2024-03-18 20:20:26', 0, '2024-03-18 20:20:26', 0, 1), (1020, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1107003, 1108001, 'Need to Merge KeyAccounting & TPT', 4, NULL, NULL, '2024-03-13 14:36:20', 0, '2024-03-13 14:36:20', 0, 1), (1021, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 0, 1107006, 1108001, 'Testing', 1, NULL, 'Need Help', '2024-03-15 16:56:48', 0, '2024-03-15 16:56:48', 0, 1), (1022, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 0, 1107002, 1108001, 'The previous Day must be ended before starting the new Day.', 2, NULL, NULL, '2024-03-20 01:29:35', 0, '2024-03-20 01:29:35', 0, 1), (1023, 1, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', 0, 1107002, 1108001, 'Need to introduce Scheduled Jobs', 8, NULL, NULL, '2024-03-15 17:21:13', 0, '2024-03-15 17:21:13', 0, 1), (1024, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 0, 1107002, 1108001, 'Testing_1', 3, NULL, NULL, '2024-03-18 20:20:26', 0, '2024-03-18 20:20:26', 0, 1);
COMMIT;

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
  INDEX `TaskId_idx`(`TaskId` ASC) USING BTREE,
  CONSTRAINT `TaskId` FOREIGN KEY (`TaskId`) REFERENCES `tms_task` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of tms_taskcomment
-- ----------------------------
BEGIN;
COMMIT;

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
  `StalledReason` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `IsDayEnded` bit(1) NULL DEFAULT b'0',
  `CreatedById` int NULL DEFAULT NULL,
  `CreatedOn` datetime NULL DEFAULT NULL,
  `ModifiedById` int NULL DEFAULT NULL,
  `ModifiedOn` datetime NULL DEFAULT NULL,
  `IsActive` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`, `ClientId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of tms_usertask
-- ----------------------------
BEGIN;
INSERT INTO `tms_usertask` (`Id`, `ClientId`, `UserId`, `TaskId`, `ClaimId`, `ApprovedClaimId`, `LastClaimId`, `StatusId`, `WorkTime`, `Parent`, `Date`, `Sp`, `Comments`, `ReviewedBy`, `ReviewComments`, `StalledReason`, `IsDayEnded`, `CreatedById`, `CreatedOn`, `ModifiedById`, `ModifiedOn`, `IsActive`) VALUES (1, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 1016, 1110009, 0, 0, 1107002, 2, NULL, '2024-03-12 14:24:01', 6, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-12 14:24:56', 0, '2024-03-12 14:24:56', b'1'), (1, 2, '85060824-9132-4191-aa91-fe3942f00969', 1002, 1110009, 0, 0, 1107002, 3, NULL, '2024-03-15 16:21:43', 6, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-15 16:21:52', 0, '2024-03-15 16:21:52', b'0'), (2, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 1015, 1110008, 0, 0, 1107002, 1, NULL, '2024-03-12 14:24:01', 3, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-12 14:24:56', 0, '2024-03-12 14:24:56', b'1'), (2, 2, '85060824-9132-4191-aa91-fe3942f00969', 1001, 1110011, 0, 0, 1107002, 1, NULL, '2024-03-15 16:21:43', 3, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-15 16:21:52', 0, '2024-03-15 16:21:52', b'0'), (3, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 1016, 1110005, 0, 1110009, 1107002, 2, NULL, '2024-03-13 14:26:01', 6, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:26:04', 0, '2024-03-13 14:26:04', b'1'), (3, 2, '85060824-9132-4191-aa91-fe3942f00969', 1002, 1110006, 0, 1110009, 1107002, 1, NULL, '2024-03-19 14:29:44', 6, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-19 14:38:01', 0, '2024-03-19 14:38:01', b'0'), (4, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 1015, 1110004, 0, 1110008, 1107002, 3, NULL, '2024-03-13 14:26:01', 3, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:26:04', 0, '2024-03-13 14:26:04', b'1'), (4, 2, '85060824-9132-4191-aa91-fe3942f00969', 1001, 1110008, 0, 0, 1107002, 2, NULL, '2024-03-19 14:29:44', 3, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-19 14:38:01', 0, '2024-03-19 14:38:01', b'0'), (5, 1, '464d5f3a-fa09-4498-94b0-d6eb51ccadbd', 1014, 1110008, 0, 0, 1107002, 2, NULL, '2024-03-13 14:26:01', 5, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:26:04', 0, '2024-03-13 14:26:04', b'1'), (6, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1008, 1110009, 0, 0, 1107002, 3, NULL, '2024-03-13 14:26:54', 5, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:27:04', 0, '2024-03-13 14:27:04', b'1'), (7, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1007, 1110001, 0, 0, 1107003, 2, NULL, '2024-03-13 14:26:54', 2, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:27:04', 0, '2024-03-13 14:27:04', b'1'), (8, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1006, 1110004, 0, 0, 1107002, 3, NULL, '2024-03-13 14:26:54', 4, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:27:04', 0, '2024-03-13 14:27:04', b'1'), (9, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1008, 1110005, 0, 1110009, 1107002, 2, NULL, '2024-03-14 14:28:45', 5, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:25:47', 0, '2024-03-18 20:25:47', b'0'), (10, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1006, 1110003, 0, 1110004, 1107002, 1, NULL, '2024-03-14 14:28:45', 4, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:25:47', 0, '2024-03-18 20:25:47', b'0'), (11, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1005, 1110010, 0, 0, 1107002, 3, NULL, '2024-03-14 14:28:45', 3, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:25:47', 0, '2024-03-18 20:25:47', b'0'), (12, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1004, 1110002, 0, 0, 1107002, 4, NULL, '2024-03-14 14:28:45', 2, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:25:47', 0, '2024-03-18 20:25:47', b'0'), (13, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 1020, 1110001, 0, 0, 1107003, 3, NULL, '2024-03-13 14:35:33', 4, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:35:58', 0, '2024-03-13 14:35:58', b'1'), (14, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 1018, 1110005, 0, 0, 1107002, 5, NULL, '2024-03-13 14:35:33', 7, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-13 14:35:58', 0, '2024-03-13 14:35:58', b'1'), (15, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 1021, 1110001, 1110003, 0, 1107006, 2, NULL, '2024-03-14 16:59:46', 1, NULL, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', '....', 'Need Help', b'1', 0, '2024-03-15 17:19:34', 0, '2024-03-15 17:19:34', b'1'), (16, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 1012, 1110004, 1110006, 0, 1107002, 3, NULL, '2024-03-14 16:59:46', 4, 'Need some help', '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', NULL, NULL, b'1', 0, '2024-03-14 17:15:21', 0, '2024-03-14 17:15:21', b'1'), (17, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 1012, 1110001, 0, 1110006, 1107003, 3, NULL, '2024-03-15 17:19:53', 4, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-15 17:20:29', 0, '2024-03-15 17:20:29', b'1'), (18, 1, 'c820114a-c1f2-462e-b813-f560bd7065b1', 1013, 1110001, 0, 0, 1107003, 4, NULL, '2024-03-15 17:19:53', 2, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-15 17:20:29', 0, '2024-03-15 17:20:29', b'1'), (19, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 1024, 1110009, 0, 0, 1107002, 1, NULL, '2024-03-15 17:29:19', 3, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:19:59', 0, '2024-03-18 20:19:59', b'0'), (20, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 1019, 1110008, 0, 0, 1107002, 2, NULL, '2024-03-15 17:29:19', 2, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:19:59', 0, '2024-03-18 20:19:59', b'0'), (21, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 1018, 1110004, 0, 1110005, 1107002, 3, NULL, '2024-03-15 17:29:19', 7, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:19:59', 0, '2024-03-18 20:19:59', b'0'), (22, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 1017, 1110010, 0, 0, 1107002, 4, NULL, '2024-03-15 17:29:19', 2, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-18 20:19:59', 0, '2024-03-18 20:19:59', b'0'), (23, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1008, 1110005, 0, 1110005, 1107002, 8, NULL, '2024-03-19 14:49:39', 5, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-20 01:29:27', 0, '2024-03-20 01:29:27', b'0'), (24, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1006, 1110003, 0, 1110003, 1107002, 0, NULL, '2024-03-19 14:49:39', 4, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-20 01:29:27', 0, '2024-03-20 01:29:27', b'0'), (25, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1005, 1110010, 0, 1110010, 1107002, 0, NULL, '2024-03-19 14:49:39', 3, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-20 01:29:27', 0, '2024-03-20 01:29:27', b'0'), (26, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1004, 1110002, 0, 1110002, 1107002, 0, NULL, '2024-03-19 14:49:39', 2, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-20 01:29:27', 0, '2024-03-20 01:29:27', b'0'), (27, 1, '8f1d8749-fa43-4c23-a7c6-70f41e098319', 1022, 1110011, 0, 0, 1107002, 0, NULL, '2024-03-19 14:49:39', 2, NULL, NULL, NULL, NULL, b'1', 0, '2024-03-20 01:29:27', 0, '2024-03-20 01:29:27', b'0');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of voc_uservocabulary
-- ----------------------------
BEGIN;
INSERT INTO `voc_uservocabulary` (`Id`, `ClientId`, `WordId`, `UserId`, `Pronunciation`, `Sentence`, `VocabDifficultyLevelId`, `NovelId`, `ChapterId`, `Comments`, `IsNeedHelp`, `CreatedOn`, `CreatedById`, `ModifiedById`, `ModifiedOn`, `IsActive`) VALUES (1, 1, 1, '1f491f2d-bdb9-4ff5-81a5-67f467c2621a', 'a', '.........', 1114002, 1112001, 0, '.......', b'1', '2024-03-15 16:47:51', 0, 0, '2024-03-15 16:47:51', b'0'), (1, 4, 1, 'fa631a98-beb3-462a-97cb-f43a58ac9510', NULL, NULL, 0, 1112001, 1113001, NULL, NULL, '2024-03-20 05:04:03', 0, 0, '2024-03-20 05:04:03', b'1'), (2, 1, 2, '45d8e4f3-da10-49eb-af0b-f2a5a5300bd5', NULL, NULL, 0, 1112001, 1113001, NULL, NULL, '2024-03-19 03:15:28', 0, 0, '2024-03-19 03:15:28', b'1');
COMMIT;

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
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of voc_vocabulary
-- ----------------------------
BEGIN;
INSERT INTO `voc_vocabulary` (`Id`, `ClientId`, `Word`, `EnglishMeaning`, `UrduMeaning`, `CreatedOn`, `CreatedById`, `ModifiedById`, `ModifiedOn`, `IsActive`) VALUES (1, 1, 'a', 'a', 'a', '2024-03-15 16:42:55', 0, 0, '2024-03-15 16:42:55', b'0'), (1, 4, 'c4_Word_1', '....', NULL, '2024-03-20 05:04:03', 0, 0, '2024-03-20 05:04:03', b'1'), (2, 1, 'Clt_1_Word_1', 'eng meaning', NULL, '2024-03-19 03:15:28', 0, 0, '2024-03-19 03:15:28', b'1');
COMMIT;

-- ----------------------------
-- View structure for att_vw_attendance
-- ----------------------------
DROP VIEW IF EXISTS `att_vw_attendance`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `att_vw_attendance` AS select `attendance`.`Id` AS `Id`,`attendance`.`ClientId` AS `ClientId`,`attendance`.`Date` AS `Date`,`attendance`.`SchDayId` AS `SchDayId`,`attendance`.`UserId` AS `UserId`,`user`.`UserName` AS `User`,`scheduleday`.`SchId` AS `SchId`,`attendance`.`DayStartTime` AS `DayStartTime`,`attendance`.`DayEndTime` AS `DayEndTime`,`attendance`.`IsActive` AS `IsActive` from ((`att_attendance` `attendance` left join `sec_user` `user` on(((`user`.`Id` = `attendance`.`UserId`) and ((`user`.`ClientId` = `attendance`.`ClientId`) or (`user`.`ClientId` = 0))))) left join `sch_scheduleday` `scheduleday` on(((`scheduleday`.`Id` = `attendance`.`SchDayId`) and (`scheduleday`.`ClientId` = `attendance`.`ClientId`))));

-- ----------------------------
-- View structure for ctl_vw_client
-- ----------------------------
DROP VIEW IF EXISTS `ctl_vw_client`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `ctl_vw_client` AS select `ctl_client`.`Id` AS `Id`,`ctl_client`.`UserId` AS `UserId`,`user`.`UserName` AS `User`,`user`.`Email` AS `Email`,`ctl_client`.`ModuleIds` AS `ModuleIds`,group_concat(`module`.`Name` separator ',') AS `Modules`,`country`.`Id` AS `CountryId`,`city`.`Id` AS `CityId`,`city`.`Name` AS `City`,`ctl_client`.`CategoryId` AS `CategoryId`,`category`.`Name` AS `Category`,`ctl_client`.`ClientName` AS `ClientName`,`ctl_client`.`Address` AS `Address`,`ctl_client`.`Contact` AS `Contact`,`ctl_client`.`Owner` AS `Owner`,`ctl_client`.`RelevantPerson` AS `RelevantPerson`,`ctl_client`.`CreatedOn` AS `CreatedOn`,`ctl_client`.`CreatedById` AS `CreatedById`,`ctl_client`.`ModifiedById` AS `ModifiedById`,`ctl_client`.`ModifiedOn` AS `ModifiedOn`,`ctl_client`.`IsActive` AS `IsActive` from (((((`ctl_client` left join `sec_user` `user` on((`user`.`Id` = `ctl_client`.`UserId`))) left join `ctl_enumline` `module` on(((0 <> find_in_set(`module`.`Id`,`ctl_client`.`ModuleIds`)) and (`module`.`ClientId` = 0)))) left join `ctl_enumline` `city` on(((`city`.`Id` = `ctl_client`.`CityId`) and (`city`.`ClientId` = 0)))) left join `ctl_enumline` `country` on(((`country`.`Id` = `ctl_client`.`CountryId`) and (`country`.`ClientId` = 0)))) left join `ctl_enumline` `category` on(((`category`.`Id` = `ctl_client`.`CategoryId`) and (`category`.`ClientId` = 0)))) group by `ctl_client`.`Id`,`ctl_client`.`ModuleIds`,`user`.`UserName`,`user`.`Email`,`city`.`Name`,`category`.`Name`;

-- ----------------------------
-- View structure for ctl_vw_customer
-- ----------------------------
DROP VIEW IF EXISTS `ctl_vw_customer`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `ctl_vw_customer` AS select `customer`.`Id` AS `Id`,`customer`.`ClientId` AS `ClientId`,`account`.`Name` AS `Account`,`customer`.`AccId` AS `AccId`,`customer`.`IsSupplier` AS `IsSupplier`,`customer`.`SupplierId` AS `SupplierId`,`customer`.`CityId` AS `CityId`,`city`.`Name` AS `City`,`customer`.`CountryId` AS `CountryId`,`country`.`Name` AS `Country`,`customer`.`Phone` AS `Phone`,`customer`.`Address` AS `Address`,`customer`.`Region` AS `Region`,`customer`.`Name` AS `Name`,`customer`.`Email` AS `Email`,`customer`.`SendEmail` AS `SendEmail`,`customer`.`CreatedOn` AS `CreatedOn`,`customer`.`CreatedById` AS `CreatedById`,`customer`.`ModifiedOn` AS `ModifiedOn`,`customer`.`ModifiedById` AS `ModifiedById`,`customer`.`IsActive` AS `IsActive` from (((`ctl_customer` `customer` left join `ctl_enumline` `account` on(((`account`.`Id` = `customer`.`AccId`) and (`account`.`ClientId` = `customer`.`ClientId`)))) left join `ctl_enumline` `city` on(((`city`.`Id` = `customer`.`CityId`) and (`city`.`ClientId` = `customer`.`ClientId`)))) left join `ctl_enumline` `country` on(((`country`.`Id` = `customer`.`CountryId`) and (`country`.`ClientId` = `customer`.`ClientId`))));

-- ----------------------------
-- View structure for ctl_vw_logevent
-- ----------------------------
DROP VIEW IF EXISTS `ctl_vw_logevent`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `ctl_vw_logevent` AS select `levnt`.`Id` AS `Id`,`levnt`.`UserId` AS `UserId`,`levnt`.`ClientId` AS `ClientId`,`user`.`UserName` AS `UserName`,`levnt`.`InTime` AS `InTime`,`levnt`.`OutTime` AS `OutTime`,`levnt`.`Date` AS `Date`,`levnt`.`Message` AS `Message`,`levnt`.`CreatedOn` AS `CreatedOn`,`levnt`.`CreatedById` AS `CreatedById`,`levnt`.`ModifiedOn` AS `ModifiedOn`,`levnt`.`ModifiedById` AS `ModifiedById`,`levnt`.`IsActive` AS `IsActive` from (`ctl_logevent` `levnt` join `sec_user` `user` on(((`levnt`.`UserId` = `user`.`Id`) and (`user`.`ClientId` = `levnt`.`ClientId`))));

-- ----------------------------
-- View structure for ctl_vw_settings
-- ----------------------------
DROP VIEW IF EXISTS `ctl_vw_settings`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `ctl_vw_settings` AS select `settings`.`Id` AS `Id`,`settings`.`ClientId` AS `ClientId`,`settings`.`ModuleId` AS `ModuleId`,`module`.`Name` AS `Module`,`settings`.`LevelId` AS `LevelId`,`level`.`Name` AS `level`,`settings`.`EnumTypeId` AS `EnumTypeId`,`settings`.`Name` AS `Name`,`type`.`Name` AS `SettingType`,`type`.`KeyCode` AS `TypeKeyCode`,`type`.`ModuleId` AS `TypeModuleId`,`settings`.`ParentId` AS `ParentId`,`parent`.`Name` AS `ParentName`,`pparent`.`Id` AS `PParentId`,`pparent`.`Name` AS `PParentName`,`settings`.`Description` AS `Description`,`settings`.`AccountCode` AS `AccountCode`,`settings`.`KeyCode` AS `KeyCode`,`settings`.`Value` AS `Value`,`settings`.`IsSystemDefined` AS `IsSystemDefined`,`settings`.`IstAccountLevel` AS `IstAccountLevel`,`settings`.`CreatedOn` AS `CreatedOn`,`settings`.`CreatedById` AS `CreatedById`,`settings`.`ModifiedOn` AS `ModifiedOn`,`settings`.`ModifiedById` AS `ModifiedById`,`settings`.`IsActive` AS `IsActive` from (((((`ctl_enumline` `settings` left join `ctl_enum` `type` on(((`type`.`Id` = `settings`.`EnumTypeId`) and (`type`.`ClientId` = `settings`.`ClientId`)))) left join `ctl_enumline` `module` on(((`module`.`Id` = `settings`.`ModuleId`) and (`module`.`ClientId` = `settings`.`ClientId`)))) left join `ctl_enumline` `parent` on(((`parent`.`Id` = `settings`.`ParentId`) and (`parent`.`ClientId` = `settings`.`ClientId`)))) left join `ctl_enumline` `pparent` on(((`pparent`.`Id` = `parent`.`ParentId`) and (`pparent`.`ClientId` = `settings`.`ClientId`)))) left join `ctl_enum` `level` on(((`level`.`Id` = `settings`.`LevelId`) and (`level`.`ClientId` = 0))));

-- ----------------------------
-- View structure for ctl_vw_settingstype
-- ----------------------------
DROP VIEW IF EXISTS `ctl_vw_settingstype`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `ctl_vw_settingstype` AS select `type`.`Id` AS `Id`,`type`.`ClientId` AS `ClientId`,`type`.`ModuleId` AS `ModuleId`,`type`.`Name` AS `Name`,`type`.`ParentId` AS `ParentId`,`parent`.`Name` AS `ParentName`,`type`.`KeyCode` AS `KeyCode`,`type`.`IsSystemDefined` AS `IsSystemDefined`,`type`.`IstAccountLevel` AS `IstAccountLevel`,`type`.`IsRequired` AS `IsRequired`,`type`.`Description` AS `Description`,`type`.`CreatedOn` AS `CreatedOn`,`type`.`CreatedById` AS `CreatedById`,`type`.`ModifiedOn` AS `ModifiedOn`,`type`.`ModifiedById` AS `ModifiedById`,`type`.`IsActive` AS `IsActive` from (`ctl_enum` `type` left join `ctl_enum` `parent` on(((`parent`.`Id` = `type`.`ParentId`) and (`parent`.`ClientId` = `type`.`ClientId`))));

-- ----------------------------
-- View structure for ctl_vw_supplier
-- ----------------------------
DROP VIEW IF EXISTS `ctl_vw_supplier`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `ctl_vw_supplier` AS select `supplier`.`Id` AS `Id`,`supplier`.`ClientId` AS `ClientId`,`supplier`.`AccId` AS `AccId`,`acc`.`Name` AS `Account`,`supplier`.`CityId` AS `CityId`,`city`.`Name` AS `City`,`supplier`.`Address` AS `Address`,`supplier`.`IsCustomer` AS `IsCustomer`,`supplier`.`CustomerId` AS `CustomerId`,`supplier`.`CompanyName` AS `CompanyName`,`supplier`.`ContactName` AS `ContactName`,`supplier`.`CountryId` AS `CountryId`,`country`.`Name` AS `Country`,`supplier`.`DiscRate` AS `DiscRate`,`supplier`.`Phone` AS `Phone`,`supplier`.`CreatedOn` AS `CreatedOn`,`supplier`.`CreatedById` AS `CreatedById`,`supplier`.`ModifiedOn` AS `ModifiedOn`,`supplier`.`ModifiedById` AS `ModifiedById`,`supplier`.`IsActive` AS `IsActive` from (((`ctl_supplier` `supplier` left join `ctl_enumline` `city` on(((`city`.`Id` = `supplier`.`CityId`) and (`city`.`ClientId` = `supplier`.`ClientId`)))) left join `ctl_enumline` `country` on(((`country`.`Id` = `supplier`.`CountryId`) and (`country`.`ClientId` = `supplier`.`ClientId`)))) left join `ctl_enumline` `acc` on(((`acc`.`Id` = `supplier`.`AccId`) and (`acc`.`ClientId` = `supplier`.`ClientId`))));

-- ----------------------------
-- View structure for ctl_vw_uomconversion
-- ----------------------------
DROP VIEW IF EXISTS `ctl_vw_uomconversion`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `ctl_vw_uomconversion` AS select `uc`.`Id` AS `Id`,`uc`.`ClientId` AS `ClientId`,`uc`.`UOMId` AS `UOMId`,`uc`.`ConvertedUOMId` AS `ConvertedUOMId`,`uc`.`IsBaseUnit` AS `IsBaseUnit`,`uc`.`Qty` AS `Qty`,`uc`.`Multiplier` AS `Multiplier`,`uc`.`DisplayUOM` AS `DisplayUOM`,`uc`.`CreatedOn` AS `CreatedOn`,`uc`.`CreatedById` AS `CreatedById`,`uc`.`ModifiedOn` AS `ModifiedOn`,`uc`.`ModifiedById` AS `ModifiedById`,`uc`.`IsActive` AS `IsActive`,`uom`.`Name` AS `UOM`,`cuom`.`Name` AS `ConvertedUOM` from ((`ctl_uomconversion` `uc` left join `ctl_enumline` `uom` on(((`uom`.`Id` = `uc`.`UOMId`) and (`uom`.`ClientId` = `uc`.`ClientId`)))) left join `ctl_enumline` `cuom` on(((`cuom`.`Id` = `uc`.`ConvertedUOMId`) and (`cuom`.`ClientId` = `uc`.`ClientId`))));

-- ----------------------------
-- View structure for pms_vw_appointment
-- ----------------------------
DROP VIEW IF EXISTS `pms_vw_appointment`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `pms_vw_appointment` AS select `app`.`Id` AS `Id`,`app`.`ClientId` AS `ClientId`,`app`.`TokenNo` AS `TokenNo`,`app`.`PatientId` AS `PatientId`,`patient`.`PatientName` AS `PatientName`,`app`.`DoctorId` AS `DoctorId`,`doc`.`DoctorName` AS `Doctor`,`app`.`GenderId` AS `GenderId`,`gender`.`Name` AS `Gender`,`app`.`StatusId` AS `StatusId`,`status`.`Name` AS `Status`,`app`.`Date` AS `Date`,`app`.`Time` AS `Time`,`app`.`Age` AS `Age`,`patient`.`DateOfBirth` AS `DOB`,`app`.`CreatedOn` AS `CreatedOn`,`app`.`CreatedById` AS `CreatedById`,`app`.`ModifiedOn` AS `ModifiedOn`,`app`.`ModifiedById` AS `ModifiedById`,`app`.`IsActive` AS `IsActive` from ((((`pms_appointment` `app` left join `pms_patient` `patient` on(((`patient`.`Id` = `app`.`PatientId`) and (`patient`.`ClientId` = `app`.`ClientId`)))) left join `ctl_enumline` `status` on(((`status`.`Id` = `app`.`StatusId`) and (`patient`.`ClientId` = `app`.`ClientId`)))) left join `pms_doctor` `doc` on(((`doc`.`Id` = `app`.`DoctorId`) and (`doc`.`ClientId` = `app`.`ClientId`)))) left join `ctl_enumline` `gender` on(((`gender`.`Id` = `app`.`GenderId`) and (`gender`.`ClientId` = `app`.`ClientId`)))) order by `app`.`TokenNo`;

-- ----------------------------
-- View structure for pms_vw_doctor
-- ----------------------------
DROP VIEW IF EXISTS `pms_vw_doctor`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `pms_vw_doctor` AS select `d`.`Id` AS `Id`,`d`.`ClientId` AS `ClientId`,`d`.`UserId` AS `UserId`,`user`.`UserName` AS `User`,`user`.`Email` AS `Email`,`d`.`StartTime` AS `StartTime`,`d`.`DefApptDur` AS `DefApptDur`,`d`.`DoctorName` AS `DoctorName`,`d`.`DateOfBirth` AS `DateOfBirth`,`d`.`GenderId` AS `GenderId`,`gender`.`Name` AS `Gender`,`country`.`Id` AS `CountryId`,`city`.`Id` AS `CityId`,`city`.`Name` AS `City`,`area`.`Id` AS `AreaId`,`area`.`Name` AS `Area`,`d`.`ContactNo` AS `ContactNo`,`d`.`HouseNo` AS `HouseNo`,`d`.`Address` AS `Address`,`d`.`Specialization` AS `Specialization`,`d`.`CreatedOn` AS `CreatedOn`,`d`.`CreatedById` AS `CreatedById`,`d`.`ModifiedById` AS `ModifiedById`,`d`.`ModifiedOn` AS `ModifiedOn`,`d`.`IsActive` AS `IsActive` from (((((`pms_doctor` `d` left join `ctl_enumline` `gender` on(((`gender`.`Id` = `d`.`GenderId`) and (`gender`.`ClientId` = `d`.`ClientId`)))) left join `ctl_enumline` `country` on(((`country`.`Id` = `d`.`CountryId`) and (`country`.`ClientId` = `d`.`ClientId`)))) left join `ctl_enumline` `city` on(((`city`.`Id` = `d`.`CityId`) and (`city`.`ClientId` = `d`.`ClientId`)))) left join `ctl_enumline` `area` on(((`area`.`Id` = `d`.`AreaId`) and (`area`.`ClientId` = `d`.`ClientId`)))) left join `sec_user` `user` on(((`user`.`Id` = `d`.`UserId`) and (`user`.`ClientId` = `d`.`ClientId`))));

-- ----------------------------
-- View structure for sal_vw_dc
-- ----------------------------
DROP VIEW IF EXISTS `sal_vw_dc`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sal_vw_dc` AS select `dc`.`Id` AS `Id`,`dc`.`ClientId` AS `ClientId`,`dc`.`Date` AS `Date`,`dc`.`InvNo` AS `InvNo`,`dc`.`AcId` AS `AcId`,`account`.`Name` AS `AcName`,`dc`.`CustId` AS `CustId`,`cust`.`Name` AS `Customer`,`dc`.`CreatedOn` AS `CreatedOn`,`dc`.`CreatedById` AS `CreatedById`,`dc`.`ModifiedOn` AS `ModifiedOn`,`dc`.`ModifiedById` AS `ModifiedById`,`dc`.`IsActive` AS `IsActive` from ((`sal_deliverychallan` `dc` left join `ctl_customer` `cust` on(((`cust`.`Id` = `dc`.`CustId`) and (`cust`.`ClientId` = `dc`.`ClientId`)))) left join `ctl_enumline` `account` on(((`account`.`Id` = `dc`.`AcId`) and (`account`.`ClientId` = `dc`.`ClientId`))));

-- ----------------------------
-- View structure for sal_vw_dcdetail
-- ----------------------------
DROP VIEW IF EXISTS `sal_vw_dcdetail`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sal_vw_dcdetail` AS select `dcdetail`.`Id` AS `Id`,`dcdetail`.`ClientId` AS `ClientId`,`dcdetail`.`DeliveryChallanId` AS `DcId`,`dcdetail`.`ProductId` AS `ProductId`,`pro`.`Name` AS `Product`,`dcdetail`.`Qty` AS `Qty`,`dcdetail`.`Description` AS `Description`,`dcdetail`.`CreatedOn` AS `CreatedOn`,`dcdetail`.`CreatedById` AS `CreatedById`,`dcdetail`.`ModifiedOn` AS `ModifiedOn`,`dcdetail`.`ModifiedById` AS `ModifiedById`,`dcdetail`.`IsActive` AS `IsActive` from (`sal_deliverychallandetail` `dcdetail` left join `ctl_item` `pro` on(((`pro`.`Id` = `dcdetail`.`ProductId`) and (`pro`.`ClientId` = `dcdetail`.`ClientId`))));

-- ----------------------------
-- View structure for sal_vw_documentextras
-- ----------------------------
DROP VIEW IF EXISTS `sal_vw_documentextras`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sal_vw_documentextras` AS select `docexts`.`Id` AS `Id`,`docexts`.`ClientId` AS `ClientId`,`docexts`.`DocExtraTypeId` AS `DocExtraTypeId`,`docextratype`.`Name` AS `DocExtraType`,`docexts`.`DocExtraId` AS `DocExtraId`,`docextra`.`Name` AS `DocExtra`,`docexts`.`IncDecTypeId` AS `IncDecTypeId`,`incdectype`.`Name` AS `IncDecType`,`docexts`.`FormulaId` AS `FormulaId`,`formula`.`Name` AS `Formula`,`docexts`.`Value` AS `Value`,`docexts`.`CreatedOn` AS `CreatedOn`,`docexts`.`CreatedById` AS `CreatedById`,`docexts`.`ModifiedOn` AS `ModifiedOn`,`docexts`.`ModifiedById` AS `ModifiedById`,`docexts`.`IsActive` AS `IsActive` from ((((`sal_documentextras` `docexts` left join `ctl_enumline` `docextra` on(((`docextra`.`Id` = `docexts`.`DocExtraId`) and (`docextra`.`ClientId` = `docexts`.`ClientId`)))) left join `ctl_enum` `docextratype` on(((`docextratype`.`Id` = `docexts`.`DocExtraTypeId`) and (`docextratype`.`ClientId` = `docexts`.`ClientId`)))) left join `ctl_enumline` `formula` on(((`formula`.`Id` = `docexts`.`FormulaId`) and (`formula`.`ClientId` = `docexts`.`ClientId`)))) left join `ctl_enumline` `incdectype` on(((`incdectype`.`Id` = `docexts`.`IncDecTypeId`) and (`incdectype`.`ClientId` = `docexts`.`ClientId`))));

-- ----------------------------
-- View structure for sal_vw_stocktransfer
-- ----------------------------
DROP VIEW IF EXISTS `sal_vw_stocktransfer`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sal_vw_stocktransfer` AS select `st`.`Id` AS `Id`,`st`.`ClientId` AS `ClientId`,`st`.`Date` AS `Date`,`st`.`InvNo` AS `InvNo`,`st`.`TransferFrom` AS `TransferFrom`,`st`.`TransferTo` AS `TransferTo`,`st`.`CreatedOn` AS `CreatedOn`,`st`.`CreatedById` AS `CreatedById`,`st`.`ModifiedOn` AS `ModifiedOn`,`st`.`ModifiedById` AS `ModifiedById`,`st`.`IsActive` AS `IsActive`,`transferfrom`.`Name` AS `From`,`transferto`.`Name` AS `To` from ((`sal_stocktransfer` `st` left join `ctl_enumline` `transferfrom` on(((`transferfrom`.`Id` = `st`.`TransferFrom`) and (`transferfrom`.`ClientId` = `st`.`ClientId`)))) left join `ctl_enumline` `transferto` on(((`transferto`.`Id` = `st`.`TransferTo`) and (`transferto`.`ClientId` = `st`.`ClientId`))));

-- ----------------------------
-- View structure for sal_vw_stocktransferline
-- ----------------------------
DROP VIEW IF EXISTS `sal_vw_stocktransferline`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sal_vw_stocktransferline` AS select `stocktransferline`.`Id` AS `Id`,`stocktransferline`.`ClientId` AS `ClientId`,`stocktransferline`.`STId` AS `STId`,`stocktransferline`.`ProductId` AS `ProductId`,`pro`.`Name` AS `Product`,`stocktransferline`.`GodownId` AS `GodownId`,`godown`.`Name` AS `Godown`,`stocktransferline`.`ProductUnits` AS `ProductUnits`,`stocktransferline`.`Qty` AS `Qty`,`stocktransferline`.`Description` AS `Description`,`stocktransferline`.`CreatedOn` AS `CreatedOn`,`stocktransferline`.`CreatedById` AS `CreatedById`,`stocktransferline`.`ModifiedOn` AS `ModifiedOn`,`stocktransferline`.`ModifiedById` AS `ModifiedById`,`stocktransferline`.`IsActive` AS `IsActive` from ((`sal_stocktransferline` `stocktransferline` left join `ctl_item` `pro` on(((`pro`.`Id` = `stocktransferline`.`ProductId`) and (`pro`.`ClientId` = `stocktransferline`.`ClientId`)))) left join `ctl_enumline` `godown` on(((`godown`.`Id` = `stocktransferline`.`GodownId`) and (`godown`.`ClientId` = `stocktransferline`.`ClientId`))));

-- ----------------------------
-- View structure for sch_vw_schedule
-- ----------------------------
DROP VIEW IF EXISTS `sch_vw_schedule`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sch_vw_schedule` AS select `schedule`.`Id` AS `Id`,`schedule`.`UserId` AS `UserId`,`schedule`.`ClientId` AS `ClientId`,`user`.`UserName` AS `User`,`schedule`.`RoleId` AS `RoleId`,`schedule`.`EntityId` AS `EntityId`,`entity`.`Name` AS `Entity`,`schedule`.`StartDate` AS `StartDate`,`schedule`.`EndDate` AS `EndDate`,`schedule`.`EffectiveDate` AS `EffectiveDate`,`schedule`.`ScheduleTypeId` AS `ScheduleTypeId`,`scheduletype`.`Name` AS `ScheduleType`,`schedule`.`WorkingTypeId` AS `WorkingTypeId`,`workingtype`.`Name` AS `WorkingType`,`schedule`.`WorkingHours` AS `WorkingHours`,`schedule`.`CreatedOn` AS `CreatedOn`,`schedule`.`CreatedBy` AS `CreatedBy`,`schedule`.`ModifiedOn` AS `ModifiedOn`,`schedule`.`ModifiedBy` AS `ModifiedBy`,`schedule`.`IsActive` AS `IsActive` from ((((`sch_schedule` `schedule` left join `sec_user` `user` on(((`user`.`Id` = `schedule`.`UserId`) and (`user`.`ClientId` = `schedule`.`ClientId`)))) left join `ctl_enumline` `entity` on(((`entity`.`Id` = `schedule`.`EntityId`) and (`entity`.`ClientId` = 0)))) left join `ctl_enumline` `scheduletype` on(((`scheduletype`.`Id` = `schedule`.`ScheduleTypeId`) and (`scheduletype`.`ClientId` = 0)))) left join `ctl_enumline` `workingtype` on(((`workingtype`.`Id` = `schedule`.`WorkingTypeId`) and (`workingtype`.`ClientId` = 0))));

-- ----------------------------
-- View structure for sch_vw_scheduleday
-- ----------------------------
DROP VIEW IF EXISTS `sch_vw_scheduleday`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sch_vw_scheduleday` AS select `scheduleday`.`Id` AS `Id`,`scheduleday`.`ClientId` AS `ClientId`,`scheduleday`.`SchId` AS `SchId`,`scheduleday`.`DayId` AS `DAYId`,`day`.`Name` AS `DAY`,`scheduleday`.`WorkTime` AS `WorkTime`,`scheduleday`.`CreatedOn` AS `CreatedOn`,`scheduleday`.`CreatedBy` AS `CreatedBy`,`scheduleday`.`ModifiedOn` AS `ModifiedOn`,`scheduleday`.`ModifiedBy` AS `ModifiedBy`,`scheduleday`.`IsActive` AS `IsActive` from (`sch_scheduleday` `scheduleday` left join `ctl_enumline` `day` on(((`day`.`Id` = `scheduleday`.`DayId`) and (`day`.`ClientId` = 0))));

-- ----------------------------
-- View structure for sch_vw_scheduledayevent
-- ----------------------------
DROP VIEW IF EXISTS `sch_vw_scheduledayevent`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sch_vw_scheduledayevent` AS select `schdayevent`.`Id` AS `Id`,`schdayevent`.`ClientId` AS `ClientId`,`schdayevent`.`SchId` AS `SchId`,`schday`.`Id` AS `SchDayId`,`schdayevent`.`StartTime` AS `StartTime`,`schdayevent`.`EndTime` AS `EndTime`,`schdayevent`.`LocationId` AS `LocationId`,`locationline`.`Name` AS `Location`,`schdayevent`.`EventTypeId` AS `EventTypeId`,`eventtypeline`.`Name` AS `EventType`,`schdayevent`.`Sp` AS `Sp`,`schdayevent`.`CreatedById` AS `CreatedById`,`schdayevent`.`CreatedOn` AS `CreatedOn`,`schdayevent`.`ModifiedById` AS `ModifiedById`,`schdayevent`.`ModifiedOn` AS `ModifiedOn`,`schdayevent`.`IsActive` AS `IsActive` from ((((`sch_scheduledayevent` `schdayevent` left join `sch_scheduleday` `schday` on(((`schday`.`Id` = `schdayevent`.`ScheduleDayId`) and (`schday`.`ClientId` = `schdayevent`.`ClientId`)))) left join `ctl_enumline` `dayline` on(((`schday`.`DayId` = `dayline`.`Id`) and (`schday`.`ClientId` = `dayline`.`ClientId`)))) left join `ctl_enumline` `locationline` on(((`schdayevent`.`LocationId` = `locationline`.`Id`) and (`schdayevent`.`ClientId` = `locationline`.`ClientId`)))) left join `ctl_enumline` `eventtypeline` on(((`schdayevent`.`EventTypeId` = `eventtypeline`.`Id`) and (`schdayevent`.`ClientId` = 0))));

-- ----------------------------
-- View structure for sec_vw_permission
-- ----------------------------
DROP VIEW IF EXISTS `sec_vw_permission`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sec_vw_permission` AS select `perms`.`Id` AS `Id`,`perms`.`ClientId` AS `ClientId`,`perms`.`UserId` AS `UserId`,`user`.`UserName` AS `User`,`perms`.`RoleId` AS `RoleId`,`role`.`Name` AS `Role`,`perms`.`RouteId` AS `routeId`,`feat`.`Name` AS `route`,`perms`.`PermissionId` AS `PermissionId`,`permsn`.`Name` AS `Permission`,`perms`.`CreatedOn` AS `CreatedOn`,`perms`.`CreatedById` AS `CreatedById`,`perms`.`ModifiedOn` AS `ModifiedOn`,`perms`.`ModifiedById` AS `ModifiedById`,`perms`.`IsActive` AS `IsActive` from ((((`sec_permission` `perms` left join `sec_user` `user` on(((`user`.`Id` = `perms`.`UserId`) and (`user`.`ClientId` = `perms`.`ClientId`)))) left join `ctl_enumline` `role` on(((`role`.`Id` = `perms`.`RoleId`) and (`role`.`ClientId` = `perms`.`ClientId`)))) left join `ctl_enumline` `feat` on(((`feat`.`Id` = `perms`.`RouteId`) and (`feat`.`ClientId` = 0)))) left join `ctl_enumline` `permsn` on(((`permsn`.`Id` = `perms`.`PermissionId`) and (`permsn`.`ClientId` = 0))));

-- ----------------------------
-- View structure for sec_vw_user
-- ----------------------------
DROP VIEW IF EXISTS `sec_vw_user`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `sec_vw_user` AS select `user`.`Id` AS `Id`,`user`.`UserName` AS `UserName`,`user`.`SupervisorId` AS `SupervisorId`,`supervisor`.`UserName` AS `Supervisor`,coalesce(`client`.`ClientName`,`clt`.`ClientName`) AS `Client`,coalesce(`client`.`Id`,`clt`.`Id`) AS `ClientId`,`client`.`ClientName` AS `CtlName`,`client`.`Id` AS `CltId`,coalesce(`client`.`ModuleIds`,`clt`.`ModuleIds`) AS `ModuleIds`,`user`.`ModuleId` AS `ModuleId`,`module`.`Name` AS `Module`,`user`.`RoleId` AS `RoleId`,`role`.`Name` AS `Role`,`doctor`.`Id` AS `DoctorId`,`user`.`PasswordHash` AS `PasswordHash`,`user`.`UserPassword` AS `UserPassword`,`user`.`CNIC` AS `CNIC`,`user`.`Address` AS `Address`,`user`.`FatherName` AS `FatherName`,`user`.`Email` AS `Email`,`user`.`PhoneNumber` AS `PhoneNumber`,`user`.`IsActive` AS `IsActive` from ((((((`sec_user` `user` left join `sec_user` `supervisor` on(((`supervisor`.`Id` = `user`.`SupervisorId`) and ((`user`.`ClientId` = `supervisor`.`ClientId`) or (`supervisor`.`ClientId` = 0))))) left join `pms_doctor` `doctor` on(((`doctor`.`UserId` = `user`.`Id`) and (`doctor`.`ClientId` = `user`.`ClientId`)))) left join `ctl_client` `clt` on((`clt`.`UserId` = `user`.`Id`))) left join `ctl_client` `client` on((`client`.`Id` = `user`.`ClientId`))) left join `ctl_enumline` `role` on(((`role`.`Id` = `user`.`RoleId`) and (`role`.`ClientId` = `user`.`ClientId`)))) left join `ctl_enumline` `module` on(((`module`.`Id` = `user`.`ModuleId`) and (`module`.`ClientId` = `user`.`ClientId`))));

-- ----------------------------
-- View structure for tms_vw_task
-- ----------------------------
DROP VIEW IF EXISTS `tms_vw_task`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `tms_vw_task` AS select `tsk`.`Id` AS `Id`,`tsk`.`ClientId` AS `ClientId`,`tsk`.`UserId` AS `UserId`,`user`.`UserName` AS `User`,`tsk`.`PriorityId` AS `PriorityId`,`priority`.`Name` AS `Priority`,`tsk`.`StatusId` AS `StatusId`,`status`.`Name` AS `Status`,`tsk`.`ModuleId` AS `ModuleId`,`module`.`Name` AS `Module`,`tsk`.`Title` AS `Title`,`tsk`.`Description` AS `Description`,`tsk`.`SP` AS `SP`,`vw_usertask`.`Id` AS `UserTaskId`,`vw_usertask`.`Date` AS `Date`,`vw_usertask`.`ApprovedClaimId` AS `ApprovedClaimId`,`vw_usertask`.`ApprovedClaim` AS `ApprovedClaim`,`vw_usertask`.`LastClaimId` AS `LastClaimId`,`vw_usertask`.`LastClaim` AS `LastClaim`,`vw_usertask`.`WorkTime` AS `WorkTime`,`tsk`.`Reason` AS `Reason`,ifnull(`vw_usertask`.`ClaimId`,1110011) AS `ClaimId`,ifnull(`vw_usertask`.`ClaimPercent`,0) AS `ClaimPercent`,`tsk`.`IsActive` AS `IsActive` from (((((`tms_task` `tsk` left join `tms_vw_usertask` `vw_usertask` on(((`vw_usertask`.`TaskId` = `tsk`.`Id`) and (`vw_usertask`.`ClientId` = `tsk`.`ClientId`)))) left join `sec_user` `user` on(((`user`.`Id` = `tsk`.`UserId`) and ((`user`.`ClientId` = `tsk`.`ClientId`) or (`user`.`ClientId` = 0))))) left join `ctl_enumline` `priority` on(((`priority`.`Id` = `tsk`.`PriorityId`) and (`priority`.`ClientId` = 0)))) left join `ctl_enumline` `status` on(((`status`.`Id` = `tsk`.`StatusId`) and (`status`.`ClientId` = 0)))) left join `ctl_enumline` `module` on(((`module`.`Id` = `tsk`.`ModuleId`) and (`module`.`ClientId` = `tsk`.`ClientId`))));

-- ----------------------------
-- View structure for tms_vw_usertask
-- ----------------------------
DROP VIEW IF EXISTS `tms_vw_usertask`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `tms_vw_usertask` AS select `usertask`.`Id` AS `Id`,`usertask`.`UserId` AS `UserId`,`usertask`.`ClientId` AS `ClientId`,`user`.`UserName` AS `User`,`usertask`.`TaskId` AS `TaskId`,`task`.`Title` AS `Title`,`task`.`Description` AS `Description`,`priority`.`Name` AS `Priority`,`usertask`.`Date` AS `Date`,`usertask`.`Parent` AS `Parent`,`usertask`.`ClaimId` AS `ClaimId`,`claim`.`Name` AS `ClaimPercent`,`usertask`.`ApprovedClaimId` AS `ApprovedClaimId`,`appclaim`.`Name` AS `ApprovedClaim`,`usertask`.`LastClaimId` AS `LastClaimId`,`lastclaim`.`Name` AS `LastClaim`,`task`.`SP` AS `Sp`,`usertask`.`WorkTime` AS `WorkTime`,`usertask`.`Comments` AS `Comments`,`usertask`.`IsDayEnded` AS `IsDayEnded`,`usertask`.`ReviewedBy` AS `ReviewedBy`,`reviewedby`.`UserName` AS `ReviewedName`,`usertask`.`ReviewComments` AS `ReviewComments`,`usertask`.`StalledReason` AS `StalledReason`,`task`.`ModuleId` AS `ModuleId`,`module`.`Name` AS `Module`,`usertask`.`StatusId` AS `StatusId`,`sts`.`Name` AS `Status`,`task`.`StatusId` AS `UStatusId`,`status`.`Name` AS `TaskStatus` from ((((((((((`tms_usertask` `usertask` left join `tms_task` `task` on(((`task`.`Id` = `usertask`.`TaskId`) and (`task`.`ClientId` = `usertask`.`ClientId`)))) left join `ctl_enumline` `priority` on(((`task`.`PriorityId` = `priority`.`Id`) and (`priority`.`ClientId` = 0)))) left join `ctl_enumline` `status` on(((`task`.`StatusId` = `status`.`Id`) and (`status`.`ClientId` = 0)))) left join `ctl_enumline` `sts` on(((`usertask`.`StatusId` = `sts`.`Id`) and (`sts`.`ClientId` = 0)))) left join `sec_user` `user` on(((`user`.`Id` = `usertask`.`UserId`) and ((`user`.`ClientId` = `usertask`.`ClientId`) or (`user`.`ClientId` = 0))))) left join `ctl_enumline` `claim` on(((`claim`.`Id` = `usertask`.`ClaimId`) and (`claim`.`ClientId` = 0)))) left join `sec_user` `reviewedby` on(((`reviewedby`.`Id` = `usertask`.`ReviewedBy`) and (`reviewedby`.`ClientId` = `usertask`.`ClientId`)))) left join `ctl_enumline` `appclaim` on(((`appclaim`.`Id` = `usertask`.`ApprovedClaimId`) and (`appclaim`.`ClientId` = 0)))) left join `ctl_enumline` `lastclaim` on(((`lastclaim`.`Id` = `usertask`.`LastClaimId`) and (`lastclaim`.`ClientId` = 0)))) left join `ctl_enumline` `module` on(((`module`.`Id` = `task`.`ModuleId`) and (`module`.`ClientId` = `task`.`ClientId`))));

-- ----------------------------
-- View structure for voc_vw_uservocabulary
-- ----------------------------
DROP VIEW IF EXISTS `voc_vw_uservocabulary`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `voc_vw_uservocabulary` AS select `uservocabulary`.`Id` AS `Id`,`uservocabulary`.`ClientId` AS `ClientId`,`uservocabulary`.`WordId` AS `WordId`,`vocabulary`.`Word` AS `Word`,`uservocabulary`.`UserId` AS `UserId`,`user`.`UserName` AS `User`,`uservocabulary`.`Pronunciation` AS `Pronunciation`,`uservocabulary`.`Sentence` AS `Sentence`,`uservocabulary`.`VocabDifficultyLevelId` AS `VocabDifficultyLevelId`,`vocabdifficuiltylevels`.`Name` AS `DifficultyLevel`,`uservocabulary`.`NovelId` AS `NovelId`,`novels`.`Name` AS `Novel`,`uservocabulary`.`ChapterId` AS `ChapterId`,`chapters`.`Name` AS `Chapters`,`uservocabulary`.`Comments` AS `Comments`,`uservocabulary`.`IsNeedHelp` AS `IsNeedHelp` from (((((`voc_uservocabulary` `uservocabulary` left join `voc_vocabulary` `vocabulary` on(((`vocabulary`.`Id` = `uservocabulary`.`WordId`) and (`vocabulary`.`ClientId` = `uservocabulary`.`ClientId`)))) left join `sec_user` `user` on(((`user`.`Id` = `uservocabulary`.`UserId`) and (`user`.`ClientId` = `uservocabulary`.`ClientId`)))) left join `ctl_enumline` `vocabdifficuiltylevels` on(((`vocabdifficuiltylevels`.`Id` = `uservocabulary`.`VocabDifficultyLevelId`) and (`vocabdifficuiltylevels`.`ClientId` = 0)))) left join `ctl_enumline` `novels` on(((`novels`.`Id` = `uservocabulary`.`NovelId`) and (`novels`.`ClientId` = `uservocabulary`.`ClientId`)))) left join `ctl_enumline` `chapters` on(((`chapters`.`Id` = `uservocabulary`.`ChapterId`) and (`chapters`.`ClientId` = `uservocabulary`.`ClientId`))));

-- ----------------------------
-- Procedure structure for ACC_GetNextVchNo
-- ----------------------------
DROP PROCEDURE IF EXISTS `ACC_GetNextVchNo`;
delimiter ;;
CREATE PROCEDURE `ACC_GetNextVchNo`(IN `VchKeyCode` varchar(200),
in clientId int,
 OUT `MaxVchNo` varchar(200))
BEGIN
    SELECT COALESCE(
        (SELECT VchNo
         FROM vw_voucher
         WHERE VchTypeKeyCode = VchKeyCode AND ClientId=clientId
         ORDER BY CAST(SUBSTRING_INDEX(VchNo, VchKeyCode, -1) AS SIGNED) DESC
         LIMIT 1), CONCAT(VchKeyCode, '0'))
    INTO MaxVchNo;
    
    SELECT MaxVchNo;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for ACC_Manage_Voucher
-- ----------------------------
DROP PROCEDURE IF EXISTS `ACC_Manage_Voucher`;
delimiter ;;
CREATE PROCEDURE `ACC_Manage_Voucher`(in prm_id int,
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
if prm_DBoperation = 'Insert'
then
           insert into acc_voucher (
		   Id
		   , ClientId
		   , VchTypeId
		   , VendorId
		   , SalesmanId
		   , GodownId
		   , ApprovedById
		   , StatusId
		   , VchDate
		   , VchNo
		   , InvNo
		   , DocNo
		   , DocDate
		   , Description
		   , IsPosted
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values (
		     prm_id
		   , prm_clientId
		   , prm_vchTypeId
		   , prm_vendorId
		   , prm_salesmanId
		   , prm_godownId
		   , prm_approvedById
		   , prm_statusId
		   , prm_vchDate
		   , prm_vchNo
		   , invNo
		   , prm_docNo
		   , prm_docDate
		   , prm_description
		   , prm_isPosted
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive);
 end if;   
/*update*/
if prm_DBoperation ='Update'
 then
            update acc_voucher set 
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
             where acc_voucher.Id =prm_id and acc_voucher.ClientId=prm_clientId;
   end if;
IF prm_DBoperation = 'Delete'
    then
             delete from acc_voucherdetail where acc_voucherdetail.VchId=prm_id
              and acc_voucherdetail.ClientId=prm_clientId;
             delete from acc_voucher
			 where
			 acc_voucher.Id=prm_id  and acc_voucher.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update acc_voucherdetail set acc_voucherdetail.IsActive=1
			where
			acc_voucherdetail.VchId=prm_id  and acc_voucherdetail.ClientId=prm_clientId;
            update acc_voucher set IsActive=1
			where 
			acc_voucher.Id=prm_id  and acc_voucher.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update acc_voucherdetail set acc_voucherdetail.IsActive=0
			where
			acc_voucherdetail.VchId=prm_id  and acc_voucherdetail.ClientId=prm_clientId;
            update acc_voucher set IsActive=0
			where
			acc_voucher.Id=prm_id  and acc_voucher.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for ACC_Manage_Voucherdetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `ACC_Manage_Voucherdetail`;
delimiter ;;
CREATE PROCEDURE `ACC_Manage_Voucherdetail`(in prm_id int,
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then 
           insert into acc_voucherdetail (
		   Id
		   , ClientId
		   , VchId
		   , AcId
		   , ProductId
		   , BillId
		   , Debit
		   , Credit
		   , Description
		   , Qty
		   , Rate
		   , CreatedOn
		   , IsDefaultDrCr
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive) 
           values (prm_id
		   , prm_clientId
		   , prm_VchId
		   , prm_acId
		   , prm_productId
		   , prm_billId
		   , prm_debit
		   , prm_credit
		   , prm_description
		   , prm_qty
		   , prm_rate
		   , prm_createdOn
		   , prm_isDefaultDrCr
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive);
 end if;   
/*update*/
if prm_DBoperation ='Update'
 then
            update acc_voucherdetail set 
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
             where acc_voucherdetail.Id =prm_id and acc_voucherdetail.VchId=prm_vchId
              and acc_voucherdetail.ClientId=prm_clientId;
   end if;
IF prm_DBoperation = 'Delete'
    then
            delete from acc_voucherdetail
			 where acc_voucherdetail.Id =prm_id and acc_voucherdetail.VchId=prm_vchId
             and acc_voucherdetail.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'Activate'
        then
            update acc_voucherdetail set IsActive=1
			 where acc_voucherdetail.Id =prm_id and acc_voucherdetail.VchId=prm_vchId
             and acc_voucherdetail.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update acc_voucherdetail set IsActive=0
			 where acc_voucherdetail.Id =prm_id and acc_voucherdetail.VchId=prm_vchId
             and acc_voucherdetail.ClientId=prm_clientId;         
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for ACC_Search_Voucherdetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `ACC_Search_Voucherdetail`;
delimiter ;;
CREATE PROCEDURE `ACC_Search_Voucherdetail`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`acc_vw_vchdetail`  ';
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
			  SchDayId= prm_SchDayId  ,
			  DayStartTime=prm_DayStartTime ,
			  DayEndTime=prm_DayEndTime ,
			  Date= prm_Date,
              ModifiedBy = prm_ModifiedBy,
			  ModifiedOn = prm_ModifiedOn,
			  IsActive = prm_IsActive
				Where ATT_Attendance.Id = prm_Id and ATT_Attendance.ClientId = prm_ClientId;
				-- SET prm_RetVal = prm_Id;
			END;
			ELSE
			IF
				( prm_Filter = 'Delete' ) THEN
				BEGIN
					DELETE FROM `ATT_Attendance` 
					WHERE ATT_Attendance.Id = prm_Id and ATT_Attendance.ClientId = prm_ClientId;

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
							ATT_Attendance.Id = prm_Id and ATT_Attendance.ClientId = prm_clientId;
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
			  SchDayId= prm_SchDayId  ,
			  DayStartTime=prm_DayStartTime ,
			  DayEndTime=prm_DayEndTime ,
			  Date= prm_Date,
              ModifiedBy = prm_ModifiedBy,
			  ModifiedOn = prm_ModifiedOn,
			  IsActive = prm_IsActive
				Where ATT_Attendance.Id = prm_Id and ATT_Attendance.ClientId = prm_ClientId;
				-- SET prm_RetVal = prm_Id;
			END;
			ELSE
			IF
				( prm_Filter = 'Delete' ) THEN
				BEGIN
					DELETE FROM `ATT_Attendance` 
					WHERE ATT_Attendance.Id = prm_Id and ATT_Attendance.ClientId = prm_ClientId;

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
							ATT_Attendance.Id = prm_Id and ATT_Attendance.ClientId = prm_clientId;
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
			`ATT_vw_Attendance`  ';
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
-- Procedure structure for CTL_GetMaxId
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_GetMaxId`;
delimiter ;;
CREATE PROCEDURE `CTL_GetMaxId`(IN `prm_TableName` VARCHAR ( 200 ))
BEGIN-- select * from prm_TableName
	SET @QueryStr = 'SELECT COALESCE(MAX(Id), 0) FROM ';
	SET @QueryStr = CONCAT( @QueryStr, `prm_TableName` );
	PREPARE stmt1 
	FROM
		@QueryStr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_GetMaxIdByClient
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_GetMaxIdByClient`;
delimiter ;;
CREATE PROCEDURE `CTL_GetMaxIdByClient`(IN `prm_TableName` VARCHAR ( 200 ) ,
								IN `prm_HeaderId` int ,
                                IN `prm_ColumnName` varchar(200))
BEGIN
	
	SET @QueryStr = 'SELECT COALESCE(MAX(Id), 0) FROM ';
	SET @QueryStr = CONCAT( @QueryStr, `prm_TableName` , ' where ' ,`prm_ColumnName`, ' = ' ,`prm_HeaderId` );
	PREPARE stmt1 
	FROM
		@QueryStr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_GetMaxLineId
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_GetMaxLineId`;
delimiter ;;
CREATE PROCEDURE `CTL_GetMaxLineId`(IN `prm_TableName` VARCHAR ( 200 ) ,
								IN `prm_HeaderId` int ,
                                IN `prm_ColumnName` varchar(200))
BEGIN
	
	SET @QueryStr = 'SELECT COALESCE(MAX(Id), 0) FROM ';
	SET @QueryStr = CONCAT( @QueryStr, `prm_TableName` , ' where ' ,`prm_ColumnName`, ' = ' ,`prm_HeaderId` );
	PREPARE stmt1 
	FROM
		@QueryStr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_GetMaxLineIdByClt
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_GetMaxLineIdByClt`;
delimiter ;;
CREATE PROCEDURE `CTL_GetMaxLineIdByClt`(IN `prm_TableName` VARCHAR ( 200 ) ,
								IN `prm_HeaderId` int ,
                                In `prm_ClientId` int ,
                                IN `prm_ColumnName` varchar(200))
BEGIN
	
	SET @QueryStr = 'SELECT COALESCE(MAX(Id), 0) FROM ';
	SET @QueryStr = CONCAT( @QueryStr, `prm_TableName` , ' where ' ,
    `prm_ColumnName`, ' = ' ,`prm_HeaderId` ,
    ' and ',  'ClientId', ' = ' ,`prm_ClientId` );
	PREPARE stmt1 
	FROM
		@QueryStr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_Client
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_Client`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_Client`(in prm_id int,
        in prm_userId varchar(255),
        in prm_moduleIds text,
        in prm_ClientName varchar(200),
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into  ctl_client 
		   (Id
		   , UserId
		   , ModuleIds
		   , ClientName
		   , CategoryId
		   , Address
		   , CountryId
		   , CityId
		   , Contact
		   , Owner
		   , RelevantPerson
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values (prm_id
		   , prm_userId
		   , prm_moduleIds
		   , prm_ClientName
		   , prm_categoryId
		   , prm_address
		   , prm_countryId
		   , prm_cityId
		   , prm_contact
		   , prm_owner
		   , prm_releventPerson
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update   ctl_client set 
                          UserId=prm_userId,
                          ModuleIds=prm_moduleIds,
                          ClientName=prm_ClientName,
                          CategoryId=prm_categoryId,
                          Address=prm_address,
                          CountryId=prm_countryId,
                          CityId=prm_cityId,
                          Contact=prm_contact,
                          Owner=prm_owner,
                          RelevantPerson=prm_releventPerson,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where   ctl_client.Id =prm_id;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from   ctl_client
			where
			  ctl_client.Id=prm_id ; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   ctl_client set IsActive=1
			where 
			  ctl_client.Id=prm_id ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update   ctl_client set IsActive=0
			where
			  ctl_client.Id=prm_id; 
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into ctl_customer 
		   (Id
		   , ClientId
		   , AccId
		   , CountryId
		   , CityId
		   , SupplierId
		   , Name
		   , Email
		   , Phone
		   , Address
		   , Region
		   , SendEmail
		   , IsSupplier
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values (prm_id
		   , prm_clientId
		   , prm_accId
		   , prm_countryId
		   , prm_cityId
		   , prm_supplierId
		   , prm_name
		   , prm_email
		   , prm_phone
		   , prm_address
		   , prm_region
		   , prm_sendEmail
		   , prm_isSupplier
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive);
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update ctl_customer set 
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
             where ctl_customer.Id =prm_id and ctl_customer.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from ctl_customer
			where
			ctl_customer.Id=prm_id and ctl_customer.ClientId=prm_clientId  ; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update ctl_customer set IsActive=1
			where 
			ctl_customer.Id=prm_id and ctl_customer.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update ctl_customer set IsActive=0
			where
			ctl_customer.Id=prm_id and ctl_customer.ClientId=prm_clientId; 
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into ctl_item
           (Id
		   , ClientId
		   , ModuleId
		   , TypeId
		   , VendorId
		   , Name
		   , PurRate
		   , SaleRate
		   , Conversion
		   , GstSaleRate
		   , GstPurRate
		   , PurUnits
		   , SaleUnits
		   , SaleStRate
		   , PurStRate
		   , RetailRate
		   , ExtraRate
		   , PrMazdoori
		   , UnitPrice
		   , UnitsInStock
		   , ManufacturersId
		   , Formula
		   , CategoryId
		   , Remarks
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values (
		   prm_id
		   , prm_clientId
		   , prm_moduleId
		   , prm_typeId
		   , prm_vendorId
		   , prm_name
		   , prm_purRate
		   , prm_saleRate
		   , prm_conversion
		   , prm_gstSaleRate
		   , prm_gstPurRate
		   , prm_purUnit
		   , prm_saleUnit
		   , prm_saleStRate
		   , prm_purStRate
		   , prm_retailRate
		   , prm_extraRate
		   , prm_prMazdoori
		   , prm_unitPrice
		   , prm_unitsInStock
		   , prm_manufacturerId
		   , prm_formula
		   , prm_categoryId
		   , prm_remarks
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update ctl_item set 
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
             where ctl_item.Id =prm_id and ctl_item.ClientId=prm_clientId;
             if isActive=false
             then update ctl_itemvariants set   IsActive=0  
			 where 
				ctl_itemvariants.ItemId=prm_id and ctl_itemvariants.ClientId=prm_clientId;
             end if;
             if isActive=true
              then update ctl_itemvariants set   IsActive=1  where 
             ctl_itemvariants.ItemId=prm_id and ctl_itemvariants.ClientId=prm_clientId;
             end if;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from ctl_item
			where
			ctl_item.Id=prm_id and  ctl_item.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'Activate'
        then
            update ctl_item set IsActive=1
			where 
			ctl_item.Id=prm_id and  ctl_item.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update ctl_item set IsActive=0
			where
			ctl_item.Id=prm_id and  ctl_item.ClientId=prm_clientId;
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_ItemUOM
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_ItemUOM`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_ItemUOM`(in prm_id int,
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
if prm_DBoperation = 'Insert'
then    
           insert into CTL_itemuom (Id,ClientId,ItemId,UOMId,UOMTypeId,SalePrice,PurPrice,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_itemId,prm_uOMId,prm_uOMTypeId,prm_salePrice,prm_purPrice,prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update CTL_itemuom set 
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
             where CTL_itemuom.Id =prm_id and CTL_itemuom.ClientId =prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from CTL_itemuom
			where
			CTL_itemuom.Id=prm_id and CTL_itemuom.ClientId =prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update CTL_itemuom set IsActive=1
			where 
			CTL_itemuom.Id=prm_id and CTL_itemuom.ClientId =prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update ctl_itemuom set IsActive=0
			where
			ctl_itemuom.Id=prm_id and ctl_itemuom.ClientId =prm_clientId;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_ItemVariants
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_ItemVariants`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_ItemVariants`(in prm_id int,
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
if prm_DBoperation = 'Insert'
then
           insert into ctl_itemvariants (Id,ClientId,ItemId,AttributeValuesIds,PurchaseExtraRate,SaleExtraRate,
           BarCode,StockValue,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_itemId,prm_attributeValuesIds,prm_purchaseExtraRate,prm_saleExtraRate,prm_barCode,prm_stockValue,
           prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
 if prm_DBoperation ='Update'
 then
            update ctl_itemvariants set 
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
             where ctl_itemvariants.Id =prm_id and ctl_itemvariants.ClientId =prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from ctl_itemvariants
			where
			ctl_itemvariants.Id=prm_id and ctl_itemvariants.ClientId =prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update ctl_itemvariants set IsActive=1
			where 
			ctl_itemvariants.Id=prm_id and ctl_itemvariants.ClientId =prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update ctl_itemvariants set IsActive=0
			where
			ctl_itemvariants.Id=prm_id and ctl_itemvariants.ClientId =prm_clientId; 
           
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
		in prm_clientId int,
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into ctl_logevent (
		   Id
		   , ClientId
		   , UserId
		   , InTime
		   , OutTime
		   , Date
		   , Message
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive )  
           values 
           (
		   prm_id
		   , prm_clientId
		   , prm_userId
		   , prm_inTime
		   , prm_outTime
		   , prm_date
		   , prm_message
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn,
  
  prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
     
          update ctl_logevent set  
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
             where ctl_logevent.Id =prm_id and ctl_logevent.ClientId= prm_clientId;
      end if;
       IF prm_DBoperation = 'Delete'
    then
            delete from ctl_logevent
			where
			 ctl_logevent.Id=prm_id and ctl_logevent.ClientId= prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   ctl_logevent set IsActive=1
			where 
			 ctl_logevent.Id=prm_id and ctl_logevent.ClientId= prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update  ctl_logevent set IsActive=0
			where
			 ctl_logevent.Id=prm_id and ctl_logevent.ClientId= prm_clientId; 
        END if;
      
  
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_ProductAttrib
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_ProductAttrib`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_ProductAttrib`(in prm_id int,
 In prm_clientId int,
 in prm_productId int,
 in prm_attribId int,
 in prm_attribValId int,
 in prm_saleRate double,
 in prm_purRate double,
 in prm_createdOn datetime,
 in prm_createdById int,
 in prm_modifiedOn datetime,
 in prm_modifiedById int,
 in prm_isActive bit,
 in prm_dboperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into ctl_productattrib (Id
, ClientId
, ProductId
, AttribId
, AttribValId
, SaleRate
, PurRate
, CreatedOn
, CreatedById
, ModifiedOn
, ModifiedById
, IsActive ) 
  values 
(prm_id
, prm_clientId
, prm_productId
, prm_attribId
, prm_attribValId
, prm_saleRate
, prm_purRate
, prm_createdOn
, prm_createdById
, prm_modifiedOn
, prm_modifiedById
, prm_isActive );
 end if;   
 if prm_DBoperation ='Update'
 then
update
 ctl_productattrib set 
                          ProductId=prm_productId,
                          AttribId=prm_attribId,
                          AttribValId=prm_attribValId,
                          SaleRate=prm_saleRate,
                          PurRate=prm_purRate,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where ctl_productattrib.Id =prm_id and ctl_productattrib.ClientId =prm_clientId;
   end if; 
   IF prm_DBoperation = 'Delete'
    then
            delete from ctl_productattrib
			where
			ctl_productattrib.Id=prm_id and 
ctl_productattrib.ClientId =prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update ctl_productattrib set IsActive=1
			where 
			ctl_productattrib.Id=Prm_id and 
ctl_productattrib.ClientId =prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update
 ctl_productattrib set IsActive=0
			where
			
ctl_productattrib.Id=prm_id 
And
ctl_productattrib.ClientId =prm_clientId; 
           
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_ProductTaxes
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_ProductTaxes`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_ProductTaxes`(in prm_id int
, in prm_clientId int
, in prm_productId int
, in prm_taxId int
, in prm_amount double
, in prm_isVariant bit
, in prm_createdOn datetime
, in prm_createdById int
, in prm_modifiedOn datetime
, in prm_modifiedById int
, in prm_isActive bit
, in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then  
insert into
 ctl_producttaxes 
(Id
,ClientId
,ProductId
,TaxId
,Amount
,IsVariant
, CreatedOn
, CreatedById
, ModifiedOn
, ModifiedById
, IsActive ) 
           Values
 (Prm_id
, prm_clientId
, prm_productId
, prm_taxId
, prm_amount
, prm_isVariant
, prm_createdOn
, prm_createdById
, prm_modifiedOn
, prm_modifiedById
, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update
 ctl_producttaxes set 
                          ProductId=prm_productId,
                          TaxId=prm_taxId,
                          Amount=prm_amount,
                          IsVariant=prm_isVariant,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where ctl_producttaxes.Id =prm_id 
and ctl_producttaxes.ClientId =prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from ctl_producttaxes
			where
			ctl_producttaxes.Id=prm_id and
 ctl_producttaxes.ClientId =prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update 
ctl_producttaxes set IsActive=1
			where 
			ctl_producttaxes.Id=prm_id
 And 
 ctl_producttaxes.ClientId =prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update
 ctl_producttaxes set IsActive=0
			where
			ctl_producttaxes.Id=prm_id
 And
 ctl_producttaxes.ClientId =prm_clientId; 
           
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
    
           insert into ctl_enumline (
		   Id
		   , ClientId
		   , ModuleId
		   , EnumTypeId
		   , ParentId
		   , LevelId
		   , KeyCode
		   , AccountCode
		   , Name
		   , Value
		   , Description
		   , IsSystemDefined
		   , IstAccountLevel
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values 
		   (prm_id
		   , prm_clientId
		   , prm_moduleId
		   , prm_enumTypeId
		   , prm_parentId
		   , prm_levelId
		   , prm_keyCode
		   , prm_accountCode
		   , prm_name
		   , prm_value
		   , prm_description
		   , prm_isSystemDefined
		   , prm_istAccountLevel
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive );
 end if;   
/*update*/
if prm_DBoperation ='Update'
 then
            update ctl_enumline set
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
             where ctl_enumline.Id =prm_id and ctl_enumline.ClientId=prm_clientId;
   end if;
IF prm_DBoperation = 'Delete'
    then
            delete from ctl_enumline
			where
			ctl_enumline.Id=prm_id and ctl_enumline.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update ctl_enumline set IsActive=1
			where 
			ctl_enumline.Id=prm_id and ctl_enumline.ClientId=prm_clientId;
               END if;
IF prm_DBoperation = 'DeActivate'
        then
            update ctl_enumline set IsActive=0
			where
			ctl_enumline.Id=prm_id and ctl_enumline.ClientId=prm_clientId;
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
    
           insert into enums (
		   Id
		   , ClientId
		   , ModuleId
		   , ParentId
		   , KeyCode
		   , Name
		   , Description
		   , IsSystemDefined
		   , IsRequired
		   , IstAccountLevel
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values (
		     prm_id
		   , prm_clientId
		   , prm_moduleId
		   , prm_parentId
		   , prm_keyCode
		   , prm_name
		   , prm_description
		   , prm_isSystemDefined
		   , prm_isRequired
		   , prm_istAccountLevel
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update ctl_enum set 
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
             where ctl_enum.Id =prm_id and ctl_enum.ClientId=prm_clientId;
             if(isActive=false)
             then
              update ctl_enumline set ctl_enumline.IsActive=0 where enumline.ParentId=prm_id AND enumline.ClientId=prm_clientId;
		      update pms_ptextrafieldsdata  set pms_ptextrafieldsdata.IsActive=0 
              where  pms_ptextrafieldsdata.FieldId=prm_id and pms_ptextrafieldsdata.ClientId=prm_clientId;
              update pms_rxmedextrafieldsdata  set pms_rxmedextrafieldsdata.IsActive=0 
              where  pms_rxmedextrafieldsdata.FieldId=prm_id and pms_rxmedextrafieldsdata.ClientId=prm_clientId;
            end if;
			if (isActive=true)
            then
              update pms_ptextrafieldsdata  set pms_pms_ptextrafieldsdata.IsActive=1 where 
              pms_ptextrafieldsdata.FieldId=prm_id  and pms_ptextrafieldsdata.ClientId=prm_clientId;
              update pms_pms_rxmedextrafieldsdata  set pms_rxmedextrafieldsdata.IsActive=1 where 
              pms_rxmedextrafieldsdata.FieldId=prm_id and pms_rxmedextrafieldsdata.ClientId=prm_clientId;
     end if;
   end if;
   IF prm_DBoperation = 'Delete'
    then
     delete from ctl_enumline
			where
			ctl_enumline.EnumTypeId=prm_id and ctl_enumline.ClientId=prm_clientId; 
            delete from ctl_enum
			where
			ctl_enum.Id=prm_id and ctl_enum.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
        update ctl_enumline set IsActive=1
			where
			ctl_enumline.EnumTypeId=prm_id and ctl_enumline.ClientId=prm_clientId; 
            update ctl_enum set IsActive=1
			where 
			ctl_enum.Id=prm_id and ctl_enum.ClientId=prm_clientId;
            
        END if;
IF prm_DBoperation = 'DeActivate'
        then
           update ctl_enumline set IsActive=0
			where
			ctl_enumline.EnumTypeId=prm_id and ctl_enumline.ClientId=prm_clientId; 
            update ctl_enum set IsActive=0
			where
			ctl_enum.Id=prm_id and ctl_enum.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_SMTPCredentials
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_SMTPCredentials`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_SMTPCredentials`(in prm_id int,
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into  ctl_smtpcredentials (
		   Id
		   , ClientId
		   , Server
		   , Port
		   , UserName
		   , Password
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values 
		   ( prm_id
		   , prm_clientId
		   , prm_server
		   , prm_port
		   , prm_userName
		   , prm_password
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update   ctl_smtpcredentials set 
                          Server=prm_server,
                          Port=prm_port,
                          UserName=prm_userName,
                          Password=prm_password,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where   ctl_smtpcredentials.Id =prm_id and ctl_smtpcredentials.ClietId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from   ctl_smtpcredentials
			where
			  ctl_smtpcredentials.Id=prm_id and ctl_smtpcredentials.ClietId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   ctl_smtpcredentials set IsActive=1
			where 
			  ctl_smtpcredentials.Id=prm_id and ctl_smtpcredentials.ClietId=prm_clientId ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update   ctl_smtpcredentials set IsActive=0
			where
			  ctl_smtpcredentials.Id=prm_id and ctl_smtpcredentials.ClietId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Manage_Supplier
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Manage_Supplier`;
delimiter ;;
CREATE PROCEDURE `CTL_Manage_Supplier`(in prm_id int
        ,in prm_clientId int
        ,in prm_accId int
        ,in prm_countryId int
		,in prm_cityId int
        ,in prm_customerId int
        ,in prm_discRate double
        ,in prm_companyName varchar(100)
		,in prm_contactName varchar(100)
        ,in prm_phone varchar(100)
		,in prm_address text
        ,in prm_isCustomer bit
        ,in prm_createdOn datetime
        ,in prm_createdById int
        ,in prm_modifiedOn datetime
        ,in prm_modifiedById int
        ,in prm_isActive bit
        ,in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
    
           insert into CTL_Supplier (Id
           ,ClientId
           ,AccId
           ,CountryId
           ,CityId
           ,CustomerId
           ,DiscRate
           ,CompanyName
           ,ContactName
           ,Phone
           ,Address
           ,IsCustomer
           ,CreatedOn
           ,CreatedById
           ,ModifiedOn
           ,ModifiedById
           ,IsActive) 
           values (prm_id
           ,prm_clientId
           ,prm_accId
           ,prm_countryId
           ,prm_cityId
           ,prm_customerId
           ,prm_discRate
           ,prm_companyName
           ,prm_contactName
           ,prm_phone
           ,prm_address
           ,prm_isCustomer
           ,prm_createdOn
           ,prm_createdById
           ,prm_modifiedOn
           ,prm_modifiedById
           ,prm_isActive);
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update ctl_Supplier set 
                          AccId=prm_accId
                          ,CountryId=prm_countryId
                          ,CityId=prm_cityId
                          ,CustomerId=prm_customerId
                          ,DiscRate=prm_discRate
                          ,CompanyName=prm_companyName
                          ,ContactName=prm_contactName
                          ,Phone=prm_phone
                          ,Address=prm_Address
                          ,IsCustomer =prm_isCustomer 
                          ,CreatedOn=prm_createdOn
                          ,CreatedById=prm_createdById
                          ,ModifiedOn=prm_modifiedOn
                          ,ModifiedById=prm_modifiedById
						  ,IsActive=prm_isActive						 
             where ctl_Supplier.Id =prm_id and ctl_Supplier.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from CTL_Supplier
			where
			CTL_Supplier.Id=prm_id and CTL_Supplier.ClientId=prm_clientId ; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update CTL_Supplier set IsActive=1
			where 
			CTL_Supplier.Id=prm_id and CTL_Supplier.ClientId=prm_clientId ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update CTL_Supplier set IsActive=0
			where
			CTL_Supplier.Id=prm_id and CTL_Supplier.ClientId=prm_clientId; 
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
        in prm_clientId int,
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then    
           insert into ctl_uomconversion 
		   (Id
		   , ClientId
		   , UOMId
		   , ConvertedUOMId
		   , IsBaseUnit
		   , Qty
		   , Multiplier
		   , DisplayUOM
		   , CreatedOn
		   , CreatedById
		   , ModifiedOn
		   , ModifiedById
		   , IsActive ) 
           values (
		   prm_id
		   , prm_clientId
		   , prm_uOMId
		   , prm_convertedUOMId
		   , prm_isBaseUnit
		   , prm_qty
		   , prm_multiplier
		   , prm_displayUOM
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update ctl_uomconversion set
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
             where ctl_uomconversion.Id =prm_id and ctl_uomconversion.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from ctl_uomconversion
			where
			ctl_uomconversion.Id=id and ctl_uomconversion.ClientId=clientId ; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update ctl_uomconversion set IsActive=1
			where 
			ctl_uomconversion.Id=prm_id and ctl_uomconversion.ClientId=prm_clientId ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update ctl_uomconversion set IsActive=0
			where
			ctl_uomconversion.Id=prm_id and ctl_uomconversion.ClientId=prm_clientId;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for CTL_Search_Client
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_Client`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_Client`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
		  `ctl_vw_client`  ';
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
			``.`ctl_vw_customer`';
           
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
			``.`ctl_vw_item`';
           
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
-- Procedure structure for CTL_Search_ItemUOM
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_ItemUOM`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_ItemUOM`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`CTL_vw_itemuom`';
           
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
-- Procedure structure for CTL_Search_ItemVariants
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_ItemVariants`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_ItemVariants`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`CTL_vw_itemvariants`';
           
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
			ctl_vw_logevent  ';
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
-- Procedure structure for CTL_Search_ProductAttrib
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_ProductAttrib`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_ProductAttrib`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`CTL_vw_Productattrib`';
           
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
-- Procedure structure for CTL_Search_ProductTaxes
-- ----------------------------
DROP PROCEDURE IF EXISTS `CTL_Search_ProductTaxes`;
delimiter ;;
CREATE PROCEDURE `CTL_Search_ProductTaxes`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`CTL_vw_Producttaxes`  ';
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
			`ctl_vw_settings`';
           
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
			`ctl_vw_settingstype`  ';
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
			``.`ctl_vw_smtpcredentials`  ';
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
			``.`ctl_vw_uomconversion`';
           
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
-- Procedure structure for KAS_Search_Voucher
-- ----------------------------
DROP PROCEDURE IF EXISTS `KAS_Search_Voucher`;
delimiter ;;
CREATE PROCEDURE `KAS_Search_Voucher`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`acc_vw_voucher`  ';
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
CREATE PROCEDURE `NTF_Manage_NotificationLog`(in prm_Id int,
        in prm_ClientId int,
				in prm_UserId varchar(255),
        in prm_Subject varchar(200),
	      in prm_Keycode varchar(100),
        in prm_SMS varchar(600),
        in prm_Body varchar(5000),
				in prm_From varchar(50),
        in prm_To varchar(50),
        in prm_IsSent bit,
        in prm_CreatedOn datetime,
        in prm_CreatedById int,
        in prm_ModifiedOn datetime,
        in prm_ModifiedById int,
        in prm_IsActive bit,
        in prm_Filter varchar(50))
BEGIN
if prm_Filter = 'Insert'
then
           insert into NTF_notificationlog 
					 (Id
					 , ClientId
					 , UserId
					 , KeyCode
					 , Subject
					 , Body
					 , SMS
					 , `From`
					 , `To`
					 , IsSent
					 , CreatedOn
					 , CreatedById
					 , ModifiedOn
					 , ModifiedById
					 , IsActive 
					 ) 
           values 
					 (prm_Id
					 , prm_ClientId
					 , prm_UserId
					 , prm_KeyCode
					 , prm_Subject
					 , prm_Body
					 , prm_SMS
					 , prm_From
					 , prm_To
					 , prm_IsSent
					 , prm_CreatedOn
					 , prm_CreatedById
					 , prm_ModifiedOn
					 , prm_ModifiedById
					 , prm_IsActive 
					 );
 end if;   
/*update*/
 if prm_Filter ='Update'
 then
            update NTF_notificationlog set 
						  -- UserId=prm_userId
						 -- , KeyCode = prm_KeyCode
						 -- , Subject = prm_Subject
						 -- , Body = prm_Body
						 -- , SMS=prm_sMS
						  IsSent = prm_IsSent
						 -- , CreatedOn=prm_createdOn
						 -- , CreatedById=prm_createdById
						 , ModifiedOn=prm_modifiedOn
						 , ModifiedById=prm_modifiedById
						 , IsActive=prm_isActive						 
             where Id =prm_id AND ClientId = prm_ClientId;
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
-- Procedure structure for PMS_GetMaxTokenNo
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_GetMaxTokenNo`;
delimiter ;;
CREATE PROCEDURE `PMS_GetMaxTokenNo`(IN `prm_Date` VARCHAR ( 200 ) ,
in `prm_clientId` int,
in `prm_doctorId` int)
BEGIN
	SET @QueryStr = 'SELECT COALESCE(MAX(TokenNo), 0) 
    FROM appointment where TokenNo like "%';
	SET @QueryStr = CONCAT( @QueryStr, `prm_Date`,'%"' 
     ' and ',  'ClientId', ' = ' ,`prm_clientId`, 'and', 'DoctorId', '=',
     `prm_doctorId`);
PREPARE stmt1 
	FROM
		@QueryStr;
	EXECUTE stmt1;
	DEALLOCATE PREPARE stmt1;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_GetNextAppt
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_GetNextAppt`;
delimiter ;;
CREATE PROCEDURE `PMS_GetNextAppt`(in whereClause varchar(5000),
in clientId int,
in doctorId int)
BEGIN
    DECLARE next_id INT;
    SET @sql =  whereClause;
    PREPARE stmt FROM @sql;
    EXECUTE stmt;
    DEALLOCATE PREPARE stmt;
        SELECT * FROM vw_appointment WHERE TokenNo = @next_id and vw_appointment.ClientId=clientId and vw_appointment.DoctorId=doctorId;
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
	in prm_Dboperation varchar(200))
BEGIN

if prm_Dboperation='Insert'
then 
	 insert into pms_appointment 
	 ( Id
	 , ClientId
	 , PatientId
	 , DoctorId
	 , StatusId
	 , TokenNo
	 , Date
	 , Time
	 , Age
	 , GenderId
	 , CreatedOn
	 , CreatedById
	 , ModifiedOn
	 , ModifiedById
	 , IsActive ) 
	 values(
	   prm_id
	 , prm_clientId
	 , prm_patientid
	 , prm_doctorId
	 , prm_statusId
	 , prm_tokenNo
	 , prm_date
	 , prm_time
	 , prm_age
	 , prm_genderId
	 , prm_createdOn
	 , prm_createdById
	 , prm_modifiedOn
	 , prm_modifiedById
	 , prm_isActive );
end If;

if prm_Dboperation='Update'
then 
update pms_appointment set 
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
 where pms_appointment.Id= prm_id and pms_appointment.ClientId= prm_clientId;
end If;

if prm_Dboperation='Delete'
then
	DELETE FROM pms_appointment where pms_appointment.Id=prm_id and pms_appointment.ClientId= prm_clientId;
end if;


if prm_Dboperation='Activate'
then 
	update pms_appointment set IsActive=1 where pms_appointment.Id=prm_id and pms_appointment.ClientId= prm_clientId;
end iF;


if prm_Dboperation='DeActivate'
then 
	update pms_appointment set IsActive=0 where pms_appointment.Id=prm_id and pms_appointment.ClientId= prm_clientId;
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into   pms_doctor 
		   (
		      Id
			, ClientId
			, UserId
			, DoctorName
			, GenderId
			, CountryId
			, CityId
			, AreaId
			, DateOfBirth
			, ContactNo
			, HouseNo
			, DefApptDur
			, Address
			, Specialization
			, StartTime
			, CreatedOn
			, CreatedById
			, ModifiedOn
			, ModifiedById
			, IsActive ) 
           values (
			 prm_id
		   , prm_clientId
		   , prm_userId
		   , prm_doctorName
		   , prm_genderId
		   , prm_countryId
		   , prm_cityId
		   , prm_areaId
		   , prm_dateOfBirth
		   , prm_contactNo
		   , prm_houseNo
		   , prm_defApptDur
		   , prm_address
		   , prm_specialization
		   , prm_startTime
		   , prm_createdOn
		   , prm_createdById
		   , prm_modifiedOn
		   , prm_modifiedById
		   , prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update   pms_doctor set 
                          UserId=userId,
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
             where   pms_doctor.Id =prm_id and doctor.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from   pms_doctor
			where
			  pms_doctor.Id=prm_id and pms_doctor.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   pms_doctor set IsActive=1
			where 
			  pms_doctor.Id=prm_id and pms_doctor.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update   pms_doctor set IsActive=0
			where
			  pms_doctor.Id=prm_id and pms_doctor.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PMS_Manage_Employee
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Manage_Employee`;
delimiter ;;
CREATE PROCEDURE `PMS_Manage_Employee`(in prm_id int,
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into   ctl_employee (Id,ClientId,UserId,Name,GenderId,CountryId,
           CityId,AreaId,DateOfBirth,ContactNo,HouseNo, Address, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId, prm_userId,prm_name, prm_genderId,prm_countryId,
           prm_cityId,prm_areaId,prm_dateOfBirth, prm_contactNo, prm_houseNo, prm_address, prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update   ctl_employee set 
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
             where   ctl_employee.Id =prm_id and ctl_employee.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from   ctl_employee
			where
			  ctl_employee.Id=prm_id and ctl_employee.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   ctl_employee set IsActive=1
			where 
			  ctl_employee.Id=prm_id and ctl_employee.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update   ctl_employee set IsActive=0
			where
			  ctl_employee.Id=prm_id and ctl_employee.ClientId=prm_clientId; 
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into  pms_patient (Id,ClientId,PatientName,DateofBirth,
           GenderId,CountryId,CityId,AreaId,Email,
           ContactNo,HouseNo,Address,Remarks, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_patientName,prm_dateofBirth,prm_genderId,prm_countryId,prm_cityId,prm_areaId,prm_email,
           prm_contactNo,prm_houseNo,prm_address,prm_remarks, prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update  pms_patient set 
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
             where   pms_patient.Id =prm_id and 
            pms_patient.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
		delete from pms_ptextrafieldsdata
			where
			 pms_ptextrafieldsdata.PatientId=prm_id and 
             pms_ptextrafieldsdata.ClientId=prm_clientId; 
		delete from pms_patient
			where
			 pms_patient.Id=prm_id  and 
             pms_patient.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
		update pms_ptextrafieldsdata set IsActive=1
			where 
			 pms_ptextrafieldsdata.PatientId=prm_id and 
             pms_ptextrafieldsdata.ClientId=prm_clientId; 
		update pms_patient set IsActive=1
			where 
			 pms_patient.Id=prm_id and 
             pms_patient.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
		update ptextrafieldsdata set IsActive=0
			where 
			 pms_ptextrafieldsdata.PatientId=prm_id and 
             pms_ptextrafieldsdata.ClientId=prm_clientId; 
		update  pms_patient set IsActive=0
			where
			 pms_patient.Id=prm_id and 
             pms_patient.ClientId=prm_clientId; 
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into pms_patientreport (Id,ClientId,RxId,Date,CategoryId,Name,ReportBase64Path,
           CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_rxId,prm_date,prm_categoryId,prm_name,prm_reportBase64Path,
           prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update pms_patientreport set 
                          Date=prm_date,
                          CategoryId=prm_categoryId,
                          Name=prm_name,
                          ReportBase64Path=prm_reportBase64Path,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where pms_patientreport.Id =prm_id and  
             pms_patientreport.RxId=prm_rxId  and  
             pms_patientreport.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from pms_patientreport
			where
			pms_patientreport.Id=prm_id and  
            pms_patientreport.RxId=prm_rxId  and  
			pms_patientreport.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update pms_patientreport set IsActive=1
			where 
			pms_patientreport.Id=prm_id and  
            pms_patientreport.RxId=prm_rxId  and  
			pms_patientreport.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update pms_patientreport set IsActive=0
			where
			pms_patientreport.Id=prm_id and  
            pms_patientreport.RxId=prm_rxId  and  
			pms_patientreport.ClientId=prm_clientId; 
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
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
 if prm_DBoperation ='Update'
 then
            update PMS_prescription set 
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
             where pms_prescription.Id =id and pms_prescription.ClientId=clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
     delete from pms_rxmedextrafieldsdata
			where
			pms_rxmedextrafieldsdata.rxId=id and 
             pms_rxmedextrafieldsdata.ClientId=clientId; 
            delete from PMS_prescription
			where
			pms_prescription.Id=id and pms_prescription.ClientId=clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
         update pms_rxmedextrafieldsdata set IsActive=1
			where
			pms_rxmedextrafieldsdata.rxId=id and 
             pms_rxmedextrafieldsdata.ClientId=clientId; 
            update PMS_prescription set IsActive=1
			where 
			pms_prescription.Id=id and pms_prescription.ClientId=clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
          update pms_rxmedextrafieldsdata set IsActive=0
			where
			pms_rxmedextrafieldsdata.rxId=id and 
             pms_rxmedextrafieldsdata.ClientId=clientId;
            update PMS_prescription set IsActive=0
			where
			pms_prescription.Id=id and pms_prescription.ClientId=clientId; 
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into PMS_ptextrafieldsdata (PatientId,ClientId,FieldId,FieldValue,
           CreatedOn, CreatedById,ModifiedOn, ModifiedById, IsActive ) 
           values (prm_patientId,prm_clientId,prm_fieldId,prm_fieldValue,
           prm_createdOn, prm_createdById, prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update PMS_ptextrafieldsdata set 
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
   IF prm_DBoperation = 'Delete'
    then
            delete from PMS_ptextrafieldsdata
			where 
            pms_ptextrafieldsdata.FieldId =prm_fieldId and 
            pms_ptextrafieldsdata.PatientId=prm_patientId and 
            pms_ptextrafieldsdata.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'Activate'
        then
            update prm_ptextrafieldsdata set IsActive=1
			where pms_ptextrafieldsdata.FieldId =prm_fieldId and 
            pms_ptextrafieldsdata.PatientId=prm_patientId and 
            ptextrafieldsdata.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update PMS_ptextrafieldsdata set IsActive=0
			where pms_ptextrafieldsdata.FieldId =prm_fieldId and 
            pms_ptextrafieldsdata.PatientId=prm_patientId and 
            pms_ptextrafieldsdata.ClientId=prm_clientId;           
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
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
    
           insert into PMS_rxmedicine (Id,ClientId,RxId,MedId,MRId,AMQty,NoonQty,EveQty,Days,
           RemarksId, CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_rxId,prm_medId,prm_mRId,prm_aMQty,prm_noonQty,prm_eveQty,prm_days,prm_remarksId,
           prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update PMS_rxmedicine set 
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
   IF prm_DBoperation = 'Delete'
    then
            delete from PMS_rxmedicine
			where
			PMS_rxmedicine.Id=prm_id and  
            PMS_rxmedicine.RxId=prm_rxId and
			PMS_rxmedicine.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'Activate'
        then
            update PMS_rxmedicine set IsActive=1
			where 
			PMS_rxmedicine.Id=prm_id and  
            PMS_rxmedicine.RxId=prm_rxId and
			PMS_rxmedicine.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
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
-- Procedure structure for PMS_SearchRxMedicine
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_SearchRxMedicine`;
delimiter ;;
CREATE PROCEDURE `PMS_SearchRxMedicine`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`PMS_vw_rxmedicine`  ';
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
			``.`PMS_vw_doctor`  ';
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
-- Procedure structure for PMS_Search_Employee
-- ----------------------------
DROP PROCEDURE IF EXISTS `PMS_Search_Employee`;
delimiter ;;
CREATE PROCEDURE `PMS_Search_Employee`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`PMS_vw_staff`  ';
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
			``.`PMS_vw_patreport`';
           
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
			``.`PMS_vw_rx`  ';
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
			``.`PMS_vw_rxmedextrafieldsdata`  ';
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
			``.`PMS_vw_rxmedicine`  ';
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
-- Procedure structure for PUR_Manage_purchase
-- ----------------------------
DROP PROCEDURE IF EXISTS `PUR_Manage_purchase`;
delimiter ;;
CREATE PROCEDURE `PUR_Manage_purchase`(in  prm_id int
, in prm_ClientId int
, in prm_supplierNameId int
, in prm_acId int
, in prm_statusId int
, in prm_isPosted bit
, in prm_invNo varchar(45)
, in prm_date datetime
, in prm_gross double
, in prm_discount double
, in prm_gst double
, in prm_credit double
, in prm_debit double
, in prm_description longtext
, in prm_createdOn datetime
, in prm_createdById int
, in prm_modifiedOn datetime
, in prm_modifiedById int
, in prm_isActive bit
, in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into pur_purchase
 (
  Id
, ClientId	
, SupplierId
, AcId
, StatusId
, IsPosted
, InvNo
, Date
, Gross
, Discount
, Gst
, Credit
, Debit
, Description
, CreatedOn
, CreatedById
, ModifiedOn
, ModifiedById
, IsActive) 
           Values
 (prm_id
, prm_ClientId
, prm_supplierNameId
, prm_acId
, prm_statusId
, prm_isPosted
, prm_invNo
, prm_date
, prm_gross
, prm_discount
, prm_gst
, prm_credit
, prm_debit
, prm_description
, prm_createdOn
, prm_createdById
, prm_modifiedOn
, prm_modifiedById
, prm_isActive);
 end if;   
/*update*/
if prm_DBoperation ='Update'
 then
            update
 pur_purchase set
                          SupplierId=prm_supplierNameId,
                          AcId=prm_acId,
						              StatusId=prm_statusId,
                          IsPosted=prm_isPosted,
                          InvNo=prm_invNo,
						              Date=prm_date,
                          Gross=prm_gross,
                          Discount=prm_discount,
                          Gst= prm_gst,
                          Credit= prm_credit,
                          Debit=prm_debit,
                          Description=prm_description,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where pur_purchase.Id =prm_id 
AND
pur_purchase.ClientId= prm_ClientId 
;
   end if;
IF prm_DBoperation = 'Delete'
    then
             delete from 
pur_purchaseline
 where 
 pur_purchaseline.PrchId=prm_id AND
 pur_purchaseline.ClientId=prm_ClientId 
;
             delete from
 pur_purchase
			 where	 pur_purchase.Id=prm_id
and pur_purchase.ClientId=prm_ClientId; 
   
END if;

IF prm_DBoperation = 'Activate'
 then
      update
      pur_purchaseline set IsActive=1 
      where 
			pur_purchaseline.PrchId=prm_id
      And pur_purchaseline.ClientId=prm_ClientId;
            
			Update
      pur_purchase set IsActive=1
			where 
			pur_purchase.Id=prm_id
      And pur_purchase.ClientId=prm_ClientId ;
END if;
IF prm_DBoperation = 'DeActivate'
        then
            update pur_purchaseline set IsActive=0 where 
            pur_purchaseline.PrchId=prm_id
And pur_purchaseline.clientId=prm_ClientId;

            update pur_purchase set IsActive=0
			where
			pur_purchase.Id=prm_id
And
			pur_purchase.ClientId=prm_ClientId
;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PUR_Manage_purchaseline
-- ----------------------------
DROP PROCEDURE IF EXISTS `PUR_Manage_purchaseline`;
delimiter ;;
CREATE PROCEDURE `PUR_Manage_purchaseline`(in prm_Id int
, in prm_clientId int
, in prm_prchId int
, in prm_proNameId int
, in prm_itemVariantId int
, in prm_purUnitId int
, in prm_productAttribIds text
, in prm_qty int
, in prm_purchaseRate double
, in prm_discPer double
, in prm_gstRate double
, in prm_gSTRetailRate double
, in prm_RetailRate double
, in prm_amount double
, in prm_description longtext
, in prm_createdon datetime
, in prm_createdById int
, in prm_modifiedOn datetime
, in prm_modifiedById int
, in prm_isActive bit
, in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into pur_purchaseline 
(Id
, clientId
, PrchId
, ProductId
, ItemVariantId
, PurUnitId
, ProductAttribIds
, Qty
, PurchaseRate
, DiscPer
, GSTRate
, GSTRetailRate
, RetailRate
, Amount
, Description
, CreatedOn
, CreatedById
, ModifiedOn
, ModifiedById
, IsActive
, ClientId ) 
           Values
 (prm_id
, prm_clientId
, prm_prchId
, prm_proNameId
, prm_itemVariantId
, prm_purUnitId
, prm_productAttribIds
, prm_qty
, prm_purchaseRate
, prm_discPer
, prm_gSTRate
, prm_gSTRetailRate
, prm_retailRate
, prm_amount
, prm_description
, prm_createdOn
, prm_createdById
, prm_modifiedOn
, prm_modifiedById
, prm_isActive
, prm_clientId);
 end if;   
/*update*/
if prm_DBoperation ='Update'
 then
            update pur_purchaseline set 
                          ProductId=prm_proNameId,
						  ItemVariantId=prm_itemVariantId,
                          PurUnitId=prm_purUnitId,
                          ProductAttribIds=prm_productAttribIds,
						  QTY=prm_qty,
                          PurchaseRate=prm_purchaseRate,
                          DiscPer=prm_discPer,
                          GSTRate=prm_gSTRate,
						  Description=prm_description,
                          GStRetailRate=prm_gStRetailRate,
                          RetailRate=prm_retailRate,
                          Amount=prm_amount,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive				
              where pur_purchaseline.Id =prm_id and pur_purchaseline.ClientId =prm_clientId and pur_purchaseline.PrchId=prm_prchId;
   end if;
IF prm_DBoperation = 'Delete'
    then
             delete from
 pur_purchaseline
			where pur_purchaseline.Id =prm_id and pur_purchaseline.ClientId =prm_clientId
and pur_purchaseline.PrchId=prm_prchId;
            END if;
IF prm_DBoperation = 'Activate'
        then
            update pur_purchaseline set IsActive=1
			where pur_purchaseline.Id =prm_id and pur_purchaseline.ClientId =prm_clientId and pur_purchaseline.PrchId=prm_prchId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update pur_purchaseline set IsActive=0
		where
 pur_purchaseline.Id =prm_id and pur_purchaseline.ClientId =prm_clientId and pur_purchaseline.PrchId=prm_prchId;           
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for PUR_Search_Purchase
-- ----------------------------
DROP PROCEDURE IF EXISTS `PUR_Search_Purchase`;
delimiter ;;
CREATE PROCEDURE `PUR_Search_Purchase`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`PUR_vw_purchase`  ';
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
-- Procedure structure for PUR_Search_Purchaseline
-- ----------------------------
DROP PROCEDURE IF EXISTS `PUR_Search_Purchaseline`;
delimiter ;;
CREATE PROCEDURE `PUR_Search_Purchaseline`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`PUR_vw_Purchaseline`  ';
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
-- Procedure structure for SAL_Manage_DeliveryChallan
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_DeliveryChallan`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_DeliveryChallan`(in prm_id int,
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
if prm_DBoperation = 'Insert'
then
           insert into sal_deliverychallan (Id,ClientId,AcId,CustId,Date,InvNo, CreatedOn, CreatedById, ModifiedOn, ModifiedById, IsActive ) 
           values 
           (prm_id,prm_clientId,prm_acId,prm_custId,prm_date,prm_invNo, prm_createdOn, prm_createdById, prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
 if prm_DBoperation ='Update'
       then     
          update sal_DeliveryChallan set  
                         AcId=prm_acId,
                          CustId=prm_custId,
						  Date=prm_date,
                          InvNo=prm_invNo,
                          CreatedOn = prm_createdOn,
                          CreatedById =prm_createdById,
                          ModifiedOn = prm_modifiedOn,
                          ModifiedById = prm_modifiedById ,
						  IsActive = prm_isActive 				 
             where sal_deliverychallan.Id =prm_id and sal_deliverychallan.ClientId =prm_clientId;
      end if;
IF prm_DBoperation = 'Delete'
       then
			delete from sal_deliverychallandetail
			where
			sal_deliverychallandetail.DeliveryChallanId=prm_id and sal_deliverychallandetail.ClientId=prm_clientId ; 
            delete from sal_deliverychallan
			where
			sal_deliverychallan.Id=prm_id and sal_deliverychallan.ClientId=prm_clientId ; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update sal_deliverychallan set IsActive=1
			where 
			sal_deliverychallan.Id=prm_id and sal_deliverychallan.ClientId=prm_clientId ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update sal_deliverychallan set IsActive=0
			where
			sal_deliverychallan.Id=prm_id and sal_deliverychallan.ClientId=prm_clientId; 
             update sal_deliverychallandetail set IsActive=0
			where
			sal_deliverychallandetail.DeliveryChallanId=prm_id and sal_deliverychallandetail.ClientId=prm_clientId ; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_DeliveryChallanDetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_DeliveryChallanDetail`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_DeliveryChallanDetail`(in prm_id int,
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
if prm_DBoperation = 'Insert'
then
           insert into sal_deliverychallandetail (
		   Id
		   , DeliveryChallanId
		   , ProductId
		   , Qty
		   , Description, CreatedOn, CreatedById, ModifiedOn, ModifiedById, IsActive ) 
           values 
           (prm_id,prm_dCId,prm_productId,prm_qty,prm_description, prm_createdOn, prm_createdById, prm_modifiedOn, prm_modifiedById, prm_isActive );
 end if;   
 if prm_DBoperation ='Update'
       then     
          update sal_deliverychallandetail set  
						 ProductId=prm_productId,
                          Qty=prm_qty,
                          Description=prm_description,
                          CreatedOn = prm_createdOn,
                          CreatedById =prm_createdById,
                          ModifiedOn = prm_modifiedOn,
                          ModifiedById = prm_modifiedById ,
						  IsActive = prm_isActive 				 
             where sal_deliverychallandetail.Id =prm_id and sal_deliverychallandetail.ClientId =prm_clientId and sal_deliverychallandetail.DeliveryChallanId=prm_dCId;
      end if;
IF prm_DBoperation = 'Delete'
       then
            delete from sal_deliverychallandetail
			 where sal_deliverychallandetail.Id =prm_id and sal_deliverychallandetail.ClientId =prm_clientId and sal_deliverychallandetail.DeliveryChallanId=prm_dCId;
        END if;
IF prm_DBoperation = 'Activate'
        then
            update sal_deliverychallandetail set IsActive=1
			 where sal_deliverychallandetail.Id =prm_id and sal_deliverychallandetail.ClientId =prm_clientId and sal_deliverychallandetail.DeliveryChallanId=prm_dCId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update sal_deliverychallandetail set IsActive=0
			 where sal_deliverychallandetail.Id =prm_id and sal_deliverychallandetail.ClientId =prm_clientId and sal_deliverychallandetail.DeliveryChallanId=prm_dCId;
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
if prm_DBoperation = 'Insert'
then    
           insert into SAL_documentExtras (Id,ClientId,DocExtraTypeId,DocExtraId,IncDecTypeId,Value,FormulaId,CreatedOn, CreatedById,
           ModifiedOn, ModifiedById, IsActive ) 
           values (prm_id,prm_clientId,prm_docExtraTypeId,prm_docExtraId,prm_incDecTypeId,prm_value,prm_formulaId,prm_createdOn, prm_createdById, prm_modifiedOn,
           prm_modifiedById, prm_isActive);
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update SAL_documentExtras set 
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
             where SAL_documentExtras.Id =prm_id and SAL_documentExtras.ClientId =prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from SAL_documentExtras
			where
			SAL_documentExtras.Id=prm_id and SAL_documentExtras.ClientId=prm_clientId ; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update SAL_documentExtras set IsActive=1
			where 
			SAL_documentExtras.Id=prm_id and SAL_documentExtras.ClientId=prm_clientid ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update SAL_documentExtras set IsActive=0
			where
			SAL_documentExtras.Id=prm_id and SAL_documentExtras.ClientId=prm_clientId;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_Sale
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_Sale`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_Sale`(in prm_id int
, in prm_clientId int
, in prm_supplierId int
, in prm_salesmanId int
, in prm_custId int
, in prm_acId int
, in prm_statusId int
, in prm_isPosted bit
, in prm_invNo varchar(45)
, in prm_date datetime
, in prm_gross double
, in prm_discount double
, in prm_gst double
, in prm_credit double
, in prm_debit double
, in prm_packChrgs double
, in prm_freightChrgs double
, in prm_netPayable double
, in prm_description longtext
, in prm_createdOn datetime
, in prm_createdById int
, in prm_modifiedOn datetime
, in prm_modifiedById int
, in prm_isActive bit
, in prm_dboperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into SAL_sale
 (Id
, clientId
, SupplierId
, SalesmanId
, CustId
, AcId
, StatusId
, IsPosted
, InvNo
, Date
, Gross
, Discount
, Gst
, Credit
, Debit
, PackChrgs
, FreightChrgs
, NetPayable
, Description
, CreatedOn
, CreatedById
, ModifiedOn
, ModifiedById
, IsActive
 ) 
           values 
(prm_id
, prm_clientId
, prm_supplierId
, prm_salesmanId
, prm_custId
, prm_acId
, prm_statusId
, prm_isPosted
, prm_invNo
, prm_date
, prm_gross
, prm_discount
, prm_gst
, prm_credit
, prm_debit
, prm_packChrgs
, prm_freightChrgs
, prm_netPayable
, prm_description
, prm_createdOn
, prm_createdById
, prm_modifiedOn
, prm_modifiedById
, prm_isActive);
 end if;   
/*update*/
if prm_DBoperation ='Update'
 then
            update
 SAL_sale set
                          SupplierId=prm_supplierId,
                          SalesmanId=prm_salesmanId,
                          CustId=prm_custId,
                          AcId=prm_acId,
                          StatusId=prm_statusId,
                          IsPosted=prm_isPosted,
                          InvNo=prm_invNo,
						  Date=prm_date,
                          Gross=prm_gross,
                          Discount=prm_discount,
                          Gst=prm_gst,
                          Credit= prm_credit,
                          Debit=prm_debit,
                          PackChrgs=prm_packChrgs,
                          FreightChrgs=prm_freightChrgs,
                          NetPayable=prm_netPayable,
                          Description=prm_description,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						  IsActive=prm_isActive						 
             where sal_sale.Id =prm_id 
And
sal_sale.clientId =prm_clientid 
;
   end if;
IF prm_DBoperation = 'Delete'
    then
             delete from
 sal_saleline 
where
 sal_saleline.SaleId=prm_id
And
sal_saleline.clientId=prm_clientid;
             delete from
 sal_sale
			 where
			 sal_sale.Id=prm_id 
And
 sal_sale.clientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
             update SAL_saleline set IsActive=1 where 
 sal_saleline.SaleId=prm_id
And
sal_saleline.clientId=prm_clientid
;
            Update
 SAL_sale set IsActive=1
			where 
			sal_sale.Id=prm_id
            and sal_sale.clientId=prm_clientId
 ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
             update sal_saleline set IsActive=0 where 
 sal_saleline.SaleId=prm_id
And
sal_saleline.clientId=prm_clientid
;
            Update
 sal_sale set IsActive=0
			where
			sal_sale.Id=prm_id
And
sal_sale.clientId=prm_clientid;            
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_SaleLine
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_SaleLine`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_SaleLine`(in prm_Id int
        ,in prm_clientId int
        ,in prm_saleId int
        ,in prm_proId int 
        ,in prm_itemVariantId int
        ,in prm_saleUnitId int
		,in prm_productAttribIds text
        ,in prm_issueQty int
		,in prm_retQty int
	    ,in prm_saleQty int
        ,in prm_saleRate double
        ,in prm_discRate double
        ,in prm_gSTRate double
        ,in prm_gSTRetailRate double
        ,in prm_RetailRate double
        ,in prm_amount double
        ,in prm_fTaxRate double
		,in prm_whtRate double
	    ,in prm_disc double
		,in prm_bulkDisc double
		,in prm_qtyDisc double
		,in prm_gST double
		,in prm_gSTRet double
		,in prm_fTax double
		,in prm_wht double
		,in prm_chrgsAdd double
		,in prm_chrgsLess double
        ,in prm_description longtext
        ,in prm_createdon datetime
        ,in prm_createdById int
        ,in prm_modifiedOn datetime
        ,in prm_modifiedById int
        ,in prm_isActive bit
		,in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into SAL_SaleLine (Id
           ,ClientId
           ,SaleId
           ,ProductId
           ,ItemVariantId
           ,SaleUnitId
           ,ProductAttribIds
           ,IssueQty
           ,RetQty
           ,SaleQty
           ,SaleRate
           ,DiscRate
           ,GSTRate
           ,GSTRetailRate
           ,RetailRate
           ,Amount
           ,FTaxRate
           ,WhtRate
           ,Disc
           ,BulkDisc
           ,QtyDisc
           ,GST
           ,GSTRet
           ,FTax
           ,Wht
           ,ChrgsAdd
           ,ChrgsLess
           ,Description
           ,CreatedOn
           ,CreatedById
           ,ModifiedOn
           ,ModifiedById
           ,IsActive) 
           values (prm_id
           ,prm_clientId
           ,prm_saleId
           ,prm_proId
           ,prm_itemVariantId
           ,prm_saleUnitId
           ,prm_productAttribIds
           ,prm_issueQty
           ,prm_retQty
           ,prm_saleQty
           ,prm_saleRate
           ,prm_discRate
           ,prm_gSTRate
           ,prm_gSTRetailRate
           ,prm_retailRate
           ,prm_amount
           ,prm_fTaxRate
           ,prm_whtRate
           ,prm_disc
           ,prm_bulkDisc
           ,prm_qtyDisc
           ,prm_gST
           ,prm_gSTRet
           ,prm_fTax
           ,prm_wht
           ,prm_chrgsAdd
           ,prm_chrgsLess
           ,prm_description
           ,prm_createdOn
           ,prm_createdById
           ,prm_modifiedOn
           ,prm_modifiedById
           ,prm_isActive);
 end if;   
/*update*/
if prm_DBoperation ='Update'
 then
            update SAL_SaleLine set 
                          ProductId=prm_proId
                          ,ItemVariantId=prm_itemVariantId
                          ,SaleUnitId=prm_saleUnitId
                          ,ProductAttribIds=prm_productAttribIds
						  ,IssueQty=prm_issueQty
                          ,RetQty=prm_retQty
                          ,SaleQty=prm_saleQty
                          ,SaleRate=prm_saleRate
                          ,DiscRate=prm_discRate
                          ,GSTRate=prm_gSTRate
                          ,FTaxRate=prm_fTaxRate
                          ,WhtRate=prm_whtRate
                          ,Disc=prm_disc
                          ,BulkDisc=prm_bulkDisc
                          ,QtyDisc=prm_qtyDisc
                          ,GST=prm_gST
                          ,GSTRet=prm_gSTRet
                          ,FTax=prm_fTax
                          ,Wht=prm_wht
                          ,ChrgsAdd=prm_chrgsAdd
                          ,ChrgsLess=prm_chrgsLess
						  ,Description=prm_description
                          ,GStRetailRate=prm_gStRetailRate
                          ,RetailRate=prm_retailRate
                          ,Amount=prm_amount
                          ,CreatedOn=prm_createdOn
                          ,CreatedById=prm_createdById
                          ,ModifiedOn=prm_modifiedOn
                          ,ModifiedById=prm_modifiedById
						  ,IsActive=prm_isActive				
              where SAL_SaleLine.Id =prm_id and SAL_SaleLine.ClientId =prm_clientId and SAL_SaleLine.SaleId=prm_SaleId;
   end if;
IF prm_DBoperation = 'Delete'
    then
             delete from SAL_SaleLine
			where SAL_SaleLine.Id =prm_id and SAL_SaleLine.ClientId =prm_clientId and SAL_SaleLine.SaleId=prm_saleId;
            END if;
IF prm_DBoperation = 'Activate'
        then
            update SAL_SaleLine set IsActive=1
			where SAL_SaleLine.Id =prm_id and SAL_SaleLine.ClientId =prm_clientId and SAL_SaleLine.SaleId=prm_saleId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update SAL_SaleLine set IsActive=0
		where SAL_SaleLine.Id =prm_id and SAL_SaleLine.ClientId =prm_clientId and SAL_SaleLine.SaleId=prm_saleId;           
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Manage_SaleStock
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Manage_SaleStock`;
delimiter ;;
CREATE PROCEDURE `SAL_Manage_SaleStock`(in prm_id int
		,in prm_clientId int
        ,in prm_saleTransId int
        ,in prm_godownId int
		,in prm_productId int
        ,in prm_debitId int
        ,in prm_jobId int
		,in prm_purchaseQty int
        ,in prm_issueQty int
        ,in prm_returnQty int
		,in prm_saleQty int
        ,in prm_freeQty int
        ,in prm_saleRate int
		,in prm_saleGstRate int
        ,in prm_returnGstRate int
        ,in prm_discRate int
		,in prm_discAmt int
        ,in prm_extraRate int
        ,in prm_description longtext
		,in prm_conversion int
        ,in prm_retailRate int
        ,in prm_soNo text
        ,in prm_createdOn datetime
        ,in prm_createdById int
        ,in prm_modifiedOn datetime
        ,in prm_modifiedById int
        ,in prm_isActive bit
        ,in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
    
           insert into SAL_SaleStock (Id
           ,ClientId
           ,SaleTransId
           ,GodownId
           ,ProductId
           ,DebitId
           ,JobId
           ,PurchaseQty
           ,IssueQty
           ,ReturnQty
           ,SaleQty
           ,FreeQty
           ,SaleRate
           ,SaleGstRate
           ,ReturnGstRate
           ,DiscRate
           ,DiscAmt
           ,ExtraRate
           ,Description
           ,Conversion
           ,RetailRate
           ,SoNo
           ,CreatedOn
           ,CreatedById
           ,ModifiedOn
           ,ModifiedById
           ,IsActive ) 
           values (prm_id
           ,prm_clientId
           ,prm_saleTransId
           ,prm_godownId
           ,prm_productId
           ,prm_debitId
           ,prm_jobId
           ,prm_purchaseQty
           ,prm_issueQty
           ,prm_returnQty
           ,prm_saleQty
           ,prm_freeQty
           ,prm_saleRate
           ,prm_saleGstRate
           ,prm_returnGstRate
           ,prm_discRate
           ,prm_discAmt
           ,prm_extraRate
           ,prm_description
           ,prm_conversion
           ,prm_retailRate
           ,prm_soNo
           ,prm_createdOn
           ,prm_createdById
           ,prm_modifiedOn
           ,prm_modifiedById
           ,prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update SAL_SaleStock set 
                          SaleTransId=prm_saleTransId
                          ,GodownId=prm_godownId
                          ,ProductId=prm_productId
                          .DebitId=prm_debitId
                          ,JobId=prm_jobId
                          ,PurchaseQty=prm_purchaseQty
                          .IssueQty=prm_issueQty
                          ,ReturnQty=prm_returnQty
                          ,SaleQty=prm_saleQty
                          ,FreeQty=prm_freeQty
                          ,SaleRate=prm_saleRate
                          ,SaleGstRate=prm_saleGstRate
                          ,ReturnGstRate=prm_returnGstRate
                          ,DiscRate=prm_discRate
                          ,DiscAmt=prm_discAmt
                          .ExtraRate=prm_extraRate
                          ,Description=prm_description
                          ,Conversion=prm_conversion
                          ,RetailRate=prm_retailRate
                          ,SoNo=prm_soNo
                          ,CreatedOn=prm_createdOn
                          ,CreatedById=prm_createdById
                          ,ModifiedOn=prm_modifiedOn
                          ,ModifiedById=prm_modifiedById
						  ,IsActive=prm_isActive					 
             where SAL_SaleStock.Id =prm_id and SAL_SaleStock.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from SAL_SaleStock
			where
			SAL_SaleStock.Id=prm_id and SAL_SaleStock.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update SAL_SaleStock set IsActive=1
			where 
			SAL_SaleStock.Id=prm_id and SAL_SaleStock.ClientId=prm_clientId ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update SAL_SaleStock set IsActive=0
			where
			SAL_SaleStock.Id=prm_id and SAL_SaleStock.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SAL_Search_DeliveryChallan
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_DeliveryChallan`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_DeliveryChallan`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`SAL_vw_dc`';
           
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
-- Procedure structure for SAL_Search_DeliveryChallanDetail
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_DeliveryChallanDetail`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_DeliveryChallanDetail`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`SAL_vw_dcdetail`';
           
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
			``.`SAL_vw_documentextras`  ';
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
-- Procedure structure for SAL_Search_Sale
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_Sale`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_Sale`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`SAL_vw_Sale`';
           
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
-- Procedure structure for SAL_Search_SaleLine
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_SaleLine`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_SaleLine`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`SAL_vw_Saleline`';
           
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
-- Procedure structure for SAL_Search_SaleStock
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_SaleStock`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_SaleStock`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`SAL_Salestock`';
           
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
-- Procedure structure for SAL_Search_StockTransfer
-- ----------------------------
DROP PROCEDURE IF EXISTS `SAL_Search_StockTransfer`;
delimiter ;;
CREATE PROCEDURE `SAL_Search_StockTransfer`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`SAL_vw_Stocktransfer`';
           
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
-- Procedure structure for SCH_Manage_Schedule
-- ----------------------------
DROP PROCEDURE IF EXISTS `SCH_Manage_Schedule`;
delimiter ;;
CREATE PROCEDURE `SCH_Manage_Schedule`(In prm_Id int ,
	In prm_ClientId  int,
  In prm_UserId   Varchar(50),
  In prm_RoleId  Varchar(50) ,
  In prm_EntityId int ,
  In prm_ScheduleTypeId  int ,
  In prm_WorkingTypeId int ,
  In prm_WorkingHours Varchar(500) ,
  In prm_StartDate datetime ,
  In prm_EndDate datetime ,
  In prm_EffectiveDate datetime ,
  IN prm_CreatedBy INT,
  IN prm_CreatedOn datetime ( 0 ),
  IN prm_ModifiedBy INT,
  IN prm_ModifiedOn datetime ( 0 ),
  In prm_IsActive bit ( 1 ),
	In prm_DBoperation varchar (50))
BEGIN
	-- SET prm_RetVal =- 1;
	IF
		( prm_DBoperation = 'Insert' ) THEN
		BEGIN
			insert into sch_schedule (Id
			, ClientId
			, UserId
			, RoleId
			, EntityId
			, ScheduleTypeId
			, WorkingTypeId
			, WorkingHours
			, StartDate
			, EndDate
			, EffectiveDate
			, CreatedBy
			, CreatedOn
			, IsActive) 
			values (prm_Id
			, prm_ClientId
			, prm_UserId
			, prm_RoleId
			, prm_EntityId
			, prm_ScheduleTypeId 
			, prm_WorkingTypeId 
			, prm_WorkingHours 
			, prm_StartDate
			, prm_EndDate
			, prm_EffectiveDate
			,  prm_CreatedBy
			, prm_CreatedOn
			, prm_IsActive );
			-- SET prm_RetVal = LAST_INSERT_ID();
		END;
	ELSE IF
			( prm_DBoperation = 'Update' ) THEN
		BEGIN
			Update sch_schedule
			Set 
             UserId= prm_UserId,
             RoleId= prm_RoleId,
             EntityId= prm_EntityId,
             ScheduleTypeId = prm_ScheduleTypeId,
             WorkingTypeId = prm_WorkingTypeId ,
             WorkingHours= prm_WorkingHours ,
             StartDate = prm_StartDate,
             EndDate = prm_EndDate,
             EffectiveDate=prm_EffectiveDate,
			       ModifiedBy = prm_ModifiedBy,
			       ModifiedOn = prm_ModifiedOn,
			       IsActive = prm_IsActive
			Where Id = prm_Id and ClientId=prm_ClientId;
				-- SET prm_RetVal = prm_Id;
		END;
	ELSE IF ( prm_DBoperation = 'Delete' ) THEN
		BEGIN
             DELETE FROM `sch_schedule` 
			 WHERE Id = prm_Id and ClientId=prm_ClientId;
			-- SET prm_RetVal = prm_Id;
		END;
	ELSE IF ( prm_DBoperation = 'DeActivate' ) THEN
		BEGIN
        update sch_scheduledayevent  set IsActive=0
			  where 
			  sch_scheduledayevent.SchId=prm_id and sch_scheduledayevent.ClientId=prm_ClientId;
		    update sch_scheduleday  set IsActive=0
			  where 
			  sch_scheduleday.SchId=prm_id and sch_scheduleday.ClientId=prm_ClientId;
			  UPDATE `sch_Schedule`SET IsActive = 0,
				ModifiedBy = prm_ModifiedBy,
				ModifiedOn = prm_ModifiedOn 
				WHERE
				Id = prm_Id and sch_schedule.ClientId=prm_ClientId;
					-- 	SET prm_RetVal = prm_Id;
	    END;
	ELSE IF ( prm_DBoperation = 'Activate' ) THEN
		BEGIN
        update sch_scheduledayevent  set IsActive=1
			  where 
			  sch_scheduledayevent.SchId=prm_id and sch_scheduledayevent.ClientId=prm_ClientId;
		    update sch_scheduleday  set IsActive=1
			  where 
			  sch_scheduleday.SchId=prm_id and sch_scheduleday.ClientId=prm_ClientId;
			  UPDATE `sch_Schedule`SET IsActive = 1,
				ModifiedBy = prm_ModifiedBy,
				ModifiedOn = prm_ModifiedOn 
				WHERE
				Id = prm_Id and sch_schedule.ClientId=prm_ClientId;
					-- 	SET prm_RetVal = prm_Id;
	    END;
	END IF;		
	END IF;
	END IF;
    END IF;
	END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SCH_Manage_ScheduleDay
-- ----------------------------
DROP PROCEDURE IF EXISTS `SCH_Manage_ScheduleDay`;
delimiter ;;
CREATE PROCEDURE `SCH_Manage_ScheduleDay`(In prm_filter varchar (50)
	,In prm_id  int
  ,In prm_clientId  int
  ,In prm_dayId  int 
  ,In prm_schId  int 
	,In prm_workTime VARCHAR (50)
	,IN prm_createdBy INT
	,IN prm_createdOn datetime ( 0 )
	,IN prm_modifiedBy INT
	,IN prm_modifiedOn datetime ( 0 )
	,In prm_isActive bit ( 1 ))
BEGIN

	

	-- SET prm_RetVal =- 1;

	IF

		( prm_Filter = 'Insert' ) THEN

		BEGIN

			insert into SCH_ScheduleDay (Id
            , ClientId
            , DayId
            , SchId
						, WorkTime
            , CreatedBy
            , CreatedOn
            , IsActive)
			values 
			( prm_id
            , prm_clientId
            , prm_dayId
            , prm_schId
						, prm_workTime
            , prm_createdBy
            , prm_createdOn
            , prm_isActive );

			-- SET prm_RetVal = LAST_INSERT_ID();

		END;

		ELSE

		IF

			( prm_Filter = 'Update' ) THEN

			BEGIN

				Update SCH_ScheduleDay
				Set 
				WorkTime=prm_workTime,										 
				ModifiedBy = prm_ModifiedBy,
				ModifiedOn = prm_ModifiedOn,
				IsActive = prm_IsActive
				Where  SCH_ScheduleDay.SchId=prm_schId 
				and SCH_ScheduleDay.DayId =prm_dayId 
				and    SCH_ScheduleDay.ClientId =prm_clientId;

				-- SET prm_RetVal = prm_Id;

			END;

			ELSE

				IF

				( prm_Filter = 'Delete' ) THEN

				BEGIN

					DELETE FROM `SCH_ScheduleDay` 
					Where  SCH_ScheduleDay.Id=prm_id  and SCH_ScheduleDay.ClientId =prm_clientId;

					-- SET prm_RetVal = prm_Id;

				END;
				ELSE

				IF

					( prm_Filter = 'Disable' ) THEN

					BEGIN

							UPDATE `SCH_ScheduleDay` 

							SET IsActive = 0,

							ModifiedBy = prm_ModifiedBy,

							ModifiedOn = prm_ModifiedOn 

							Where  SCH_ScheduleDay.SchId=prm_SchId 
                            and SCH_ScheduleDay.DayId =prm_dayId 
                            and SCH_ScheduleDay.ClientId =prm_clientId;

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
-- Procedure structure for SCH_Manage_ScheduleDayEvent
-- ----------------------------
DROP PROCEDURE IF EXISTS `SCH_Manage_ScheduleDayEvent`;
delimiter ;;
CREATE PROCEDURE `SCH_Manage_ScheduleDayEvent`(in prm_Id int
,in prm_ClientId int 
,in prm_StartTime varchar(45)
,in prm_EndTime varchar(45)
,in prm_LocationId int
,in prm_EventTypeId int
,in prm_Sp double
,in prm_SchId int
,in prm_ScheduleDayId int
,in prm_createdOn datetime
,in prm_createdById int
,in prm_modifiedOn datetime
,in prm_modifiedById int
,in prm_isActive bool
,in prm_DbOperation varchar(200))
BEGIN

if prm_Dboperation='Insert'
then 
 insert into SCH_ScheduleDayEvent (
 Id
 , ClientId
 , SchId
 , ScheduleDayId
 , StartTime
 , EndTime
 , LocationId
 , EventTypeId
 , Sp
 , CreatedOn
 , CreatedById
 , ModifiedOn
 , ModifiedById
 , IsActive ) 
 values(
   prm_id
 , prm_ClientId
 , prm_schId
 , prm_scheduleDayId
 , prm_startTime
 , prm_endTime
 , prm_locationId
 , prm_eventTypeId
 , prm_sp
 , prm_createdOn
 , prm_createdById
 , prm_modifiedOn
 , prm_modifiedById
 , prm_isActive );
end If;

if prm_Dboperation='Update'
then 
update SCH_Scheduledayevent set 
 StartTime=prm_startTime
 ,EndTime=prm_endTime
 ,LocationId=prm_locationId
 ,EventTypeId=prm_eventTypeId
 ,Sp =prm_sp
 ,ScheduleDayId =prm_scheduleDayId
 ,SchId=prm_schId
 ,CreatedOn=prm_createdOn
 ,CreatedById=prm_createdById
 ,ModifiedOn=prm_modifiedOn
 ,ModifiedById=prm_modifiedById
 ,IsActive=prm_isActive
 where sch_Scheduledayevent.Id=prm_id and sch_ScheduleDayEvent.ClientId=prm_clientId ;
end If;

if prm_Dboperation='Delete'
then
DELETE FROM SCH_ScheduleDayEvent where SCH_ScheduleDayEvent.Id=prm_id and SCH_ScheduleDayEvent.ClientId=prm_clientId ;
end if;

if prm_Dboperation='Activate'
then 
update SCH_ScheduleDayEvent set IsActive=1
 where SCH_ScheduleDayEvent.Id=prm_id and SCH_ScheduleDayEvent.ClientId=prm_clientId;
end iF;

if prm_Dboperation='DeActivate'
then 
update SCH_ScheduleDayEvent set IsActive=0
 where SCH_ScheduleDayEvent.Id=prm_id and SCH_ScheduleDayEvent.ClientId=prm_clientId;
end iF;

END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SCH_Search_Permission
-- ----------------------------
DROP PROCEDURE IF EXISTS `SCH_Search_Permission`;
delimiter ;;
CREATE PROCEDURE `SCH_Search_Permission`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			``.`SCH_vw_Permission`  ';
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
-- Procedure structure for SCH_Search_Schedule
-- ----------------------------
DROP PROCEDURE IF EXISTS `SCH_Search_Schedule`;
delimiter ;;
CREATE PROCEDURE `SCH_Search_Schedule`(in whereClause varchar(5000))
BEGIN
  set @querystr ='select * FROM `SCH_vw_Schedule`   ';
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
-- Procedure structure for SCH_Search_ScheduleDay
-- ----------------------------
DROP PROCEDURE IF EXISTS `SCH_Search_ScheduleDay`;
delimiter ;;
CREATE PROCEDURE `SCH_Search_ScheduleDay`(in whereClause varchar(5000))
BEGIN
  set @querystr ='select * 
			FROM
			`SCH_vw_ScheduleDay`  ';
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
-- Procedure structure for SCH_Search_ScheduleDayEvent
-- ----------------------------
DROP PROCEDURE IF EXISTS `SCH_Search_ScheduleDayEvent`;
delimiter ;;
CREATE PROCEDURE `SCH_Search_ScheduleDayEvent`(in WhereClause varchar(5000))
BEGIN
 set @queryStr= "select * from `SCH_vw_Scheduledayevent`";
 set @queryStr= concat(@queryStr, WhereClause);
 prepare stm1
 from @queryStr;
 execute stm1;
 deallocate prepare stm1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SEC_Manage_Permission
-- ----------------------------
DROP PROCEDURE IF EXISTS `SEC_Manage_Permission`;
delimiter ;;
CREATE PROCEDURE `SEC_Manage_Permission`(in prm_id int
, in prm_clientId int
, in prm_userId varchar(255)
, in prm_roleId int
, in prm_routeId int
, in prm_permissionId int
, in prm_createdOn datetime
, in prm_createdById int
, in prm_modifiedOn datetime
, in prm_modifiedById int
, in prm_isActive bit
, in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
insert into SEC_Permission (Id
,ClientId
,UserId
,RoleId
,RouteId
,PermissionId
, CreatedOn
, CreatedById
,ModifiedOn
, ModifiedById
, IsActive ) 
 values (prm_id
,prm_clientId
,prm_userId
,prm_roleId
,prm_routeId
,prm_permissionId
,prm_createdOn
,prm_createdById
,prm_modifiedOn
 ,prm_modifiedById
,prm_isActive );
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update SEC_Permission set 
 UserId=prm_userId
,RoleId=prm_roleId
,routeId=prm_routeId
,PermissionId=prm_permissionId
,CreatedOn=prm_createdOn
,CreatedById=prm_createdById
,ModifiedOn=prm_modifiedOn
,ModifiedById=prm_modifiedById
,IsActive=prm_isActive						 
             where SEC_Permission.Id =prm_id and sec_permission.ClientId=prm_clientId ;
 end if;
 IF prm_DBoperation = 'Delete'
    then
            delete from SEC_Permission
			where
			SEC_Permission.Id=prm_id  and sec_permission.ClientId=prm_clientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update SEC_Permission set IsActive=1
			where 
			SEC_Permission.Id=prm_id and sec_permission.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update SEC_Permission set IsActive=0
			where
			SEC_Permission.Id=prm_id  and sec_permission.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SEC_Search_Permission
-- ----------------------------
DROP PROCEDURE IF EXISTS `SEC_Search_Permission`;
delimiter ;;
CREATE PROCEDURE `SEC_Search_Permission`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`SEC_vw_Permission`  ';
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
-- Procedure structure for SEC_Search_User
-- ----------------------------
DROP PROCEDURE IF EXISTS `SEC_Search_User`;
delimiter ;;
CREATE PROCEDURE `SEC_Search_User`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * 
			FROM
			`sec_vw_user`  ';
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
-- Procedure structure for TMS_Manage_Attachment
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Manage_Attachment`;
delimiter ;;
CREATE PROCEDURE `TMS_Manage_Attachment`(in prm_id int
  ,in prm_clientId int
  ,in prm_taskId int
  ,in prm_name varchar(700)
  ,in prm_docPath varchar(7000)
  ,in prm_size varchar(900)
  ,in prm_base64File LONGTEXT
  ,in prm_createdOn datetime
  ,in prm_createdById int
  ,in prm_modifiedOn datetime
  ,in prm_modifiedById int
  ,in prm_isActive bit
  ,in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
    
           insert into TMS_Attachments (Id
,ClientId
,TaskId
,Name
,DocPath
,Size
,Base64File
,CreatedOn
,CreatedById
,ModifiedOn
,ModifiedById
,IsActive )  
 values (prm_id
,prm_clientId
,prm_taskId
,prm_name
,prm_docPath
,prm_size
,prm_base64File
,prm_createdOn
,prm_createdById
,prm_modifiedOn
,prm_modifiedById
,prm_isActive);
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update TMS_Attachments set 
TaskId=prm_taskId
,Name=prm_name
,DocPath=prm_docPath
,Size=prm_size
,Base64File=prm_base64File
,CreatedOn=prm_createdOn
,CreatedById=prm_createdById
,ModifiedOn=prm_modifiedOn
,ModifiedById=prm_modifiedById
,IsActive=prm_isActive						 
             where TMS_Attachments.Id =prm_id and TMS_Attachments.ClientId =clientId;
end if;
IF prm_DBoperation = 'Delete'
then
       delete from TMS_Attachments
			 where
			 TMS_Attachments.Id=prm_id and TMS_Attachments.ClientId=prm_clientId ; 
end if;
IF prm_DBoperation = 'Activate'
then
       update   TMS_Attachments set IsActive=1
			 where 
			 TMS_Attachments.Id=prm_id and TMS_Attachments.ClientId=prm_clientId;
end if;
IF prm_DBoperation = 'DeActivate'
then
       update  TMS_Attachments set IsActive=0
			 where
			 TMS_Attachments.Id=prm_id and TMS_Attachments.ClientId=prm_clientId; 
end if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for TMS_Manage_Task
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Manage_Task`;
delimiter ;;
CREATE PROCEDURE `TMS_Manage_Task`(in prm_id int,
				in prm_clientId int,
        in prm_userId varchar(40),
        in prm_moduleId int,
        in prm_statusId int,
        in prm_priorityId int,
        in prm_title varchar(300),
        in prm_sP float,
        in prm_description longtext,
        in prm_reason longtext,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_filter varchar(50))
BEGIN
if prm_filter = 'Insert'
then
    
           insert into TMS_Task 
					 (Id
					 , ClientId
					 , UserId
					 , ModuleId
					 , StatusId
					 , PriorityId
					 , Title
					 , SP
					 , Description
					 , Reason
					 , CreatedOn
					 , CreatedById
					 , ModifiedOn
					 , ModifiedById
					 , IsActive 
					 ) 
           values 
					 ( prm_id
					 , prm_clientId 
					 , prm_userId
					 , prm_moduleId
					 , prm_statusId
					 , prm_priorityId
					 , prm_title
					 , prm_sP
					 , prm_description
					 , prm_reason
					 , prm_createdOn
					 , prm_createdById
					 , prm_modifiedOn
					 , prm_modifiedById
					 , prm_isActive 
					 );
 end if;   
/*update*/
 if prm_filter ='Update'
 then
            update TMS_Task set 
						 UserId=prm_userId,
                          ModuleId=prm_moduleId,
                          StatusId=prm_statusId,
                          PriorityId=prm_priorityId,
                          Title=prm_title,
                          Description=prm_description,
                          Reason=prm_reason,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						IsActive=prm_isActive	,
                          SP=prm_sp
             where TMS_Task.Id =prm_id AND TMS_Task.ClientId = prm_clientId;
end if;
IF prm_filter = 'Delete'
then
       delete from TMS_Task
			 where
			 TMS_Task.Id= prm_id AND TMS_Task.ClientId = prm_clientId; 
end if;
IF prm_filter = 'Activate'
then
       update   TMS_Task set IsActive=1
			 where 
			 TMS_Task.Id=prm_id AND TMS_Task.ClientId = prm_clientId;
end if;
IF prm_filter = 'DeActivate'
then
       update  TMS_Task set IsActive=0
			 where
			 TMS_Task.Id=prm_id AND TMS_Task.ClientId = prm_clientId; 
end if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for TMS_Manage_TaskComment
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Manage_TaskComment`;
delimiter ;;
CREATE PROCEDURE `TMS_Manage_TaskComment`(in prm_id int
,in prm_ClientId int
,in prm_taskId int
,in prm_userId varchar(40)
,in prm_comment longtext
,in prm_time datetime
,in prm_createdOn datetime
,in prm_createdById int
,in prm_modifiedOn datetime
,in prm_modifiedById int
,in prm_isActive bit
,in prm_filter varchar(50))
BEGIN
if prm_filter = 'Insert'
then
INSERT into TMS_TaskComment (Id
,ClientId
,TaskId
,UserId
,Comment
,Time
, CreatedOn
, CreatedById
, ModifiedOn
, ModifiedById
, IsActive ) 
values (prm_id
,prm_clientId
,prm_taskId
,prm_userId
,prm_comment
,prm_time
,prm_createdOn
,prm_createdById
,prm_modifiedOn
,prm_modifiedById
,prm_isActive );
 end if;   
/*update*/
 if prm_filter ='Update'
 then
update TMS_TaskComment set 
TaskId=prm_taskId
,UserId=prm_userId
,Comment=prm_comment
,Time=prm_time
,CreatedOn=prm_createdOn
,CreatedById=prm_createdById
,ModifiedOn=prm_modifiedOn
,ModifiedById=prm_modifiedById
,IsActive=prm_isActive						 
 where TMS_TaskComment.Id =prm_id and TMS_TaskComment.ClientId =prm_clientId;
   end if;
 IF prm_filter = 'Delete'
then
       delete from TMS_TaskComment
			 where
			 TMS_TaskComment.Id=prm_id and TMS_TaskComment.ClientId=prm_clientId ; 
end if;
IF prm_filter = 'Activate'
then
       update   TMS_TaskComment set IsActive=1
			 where 
			 TMS_TaskComment.Id=prm_id and TMS_TaskComment.ClientId=prm_clientId ;
end if;
IF prm_filter = 'DeActivate'
then
       update  TMS_TaskComment set IsActive=0
			 where
			 TMS_TaskComment.Id=prm_id and TMS_TaskComment.ClientId=prm_clientId; 
end if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for TMS_Manage_UserTask
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Manage_UserTask`;
delimiter ;;
CREATE PROCEDURE `TMS_Manage_UserTask`(in prm_id int,
				in prm_ClientId int,
        in prm_userId varchar(225),
        in prm_taskId int,
        in prm_parent varchar(225),
        in prm_date datetime,
        in prm_claimId int,
        in prm_approvedClaimId int,
				in prm_lastClaimId int,
				in prm_statusId int,
        in prm_sp float,
				in prm_workTime float,
        in prm_comments longtext,
        in prm_isDayEnded bit,
        in prm_reviewedby varchar(255),
        in prm_reviewcomments varchar(255),
				in prm_stalledReason LONGTEXT,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into  TMS_UserTask (
		   Id
			 ,ClientId
		   ,UserId
		   ,TaskId
		   ,Parent
		   ,Date
		   ,ClaimId
		   ,ApprovedClaimId
		   ,LastClaimId
		   ,StatusId
		   ,Sp
		   ,WorkTime
		   ,Comments
		   ,IsDayEnded
		   ,ReviewedBy
		   ,ReviewComments
			 ,StalledReason
		   ,CreatedOn
		   ,CreatedById
		   ,ModifiedOn
		   ,ModifiedById
		   ,IsActive
		   ) 
           values 
		   (
		    prm_id
			 ,prm_ClientId
		   ,prm_userId
		   ,prm_taskId
		   ,prm_parent
		   ,prm_date
		   ,prm_claimId
		   ,prm_approvedClaimId
		   ,prm_lastClaimId
		   ,prm_statusId
		   ,prm_sp
		   ,prm_workTime
		   ,prm_comments
		   ,prm_isDayEnded
		   ,prm_reviewedby
		   ,prm_reviewcomments
			 ,prm_stalledReason
		   ,prm_createdOn
		   ,prm_createdById
		   ,prm_modifiedOn
		   ,prm_modifiedById
		   ,prm_isActive
		   );
 end if;   

 if prm_DBoperation ='Update'
 then
            update  TMS_UserTask set 
                         UserId = prm_userId,
                         TaskId=prm_taskId,
                         Parent=prm_parent,
                         Date=prm_date,
                         ClaimId=prm_claimId,
                         ApprovedClaimId=prm_approvedClaimId,
												 LastClaimId=prm_lastClaimId,
												 StatusId=prm_statusId,
                         Sp=prm_sp,
												 WorkTime=prm_workTime,
                         Comments=prm_comments,
                         IsDayEnded = prm_isDayEnded,
                         ReviewedBy=prm_reviewedby,
                         ReviewComments=prm_reviewcomments,
												 StalledReason=prm_stalledReason,
                         CreatedOn=prm_createdOn,
                         CreatedById=prm_createdById,
                         ModifiedOn=prm_modifiedOn,
                         ModifiedById=prm_modifiedById,
											   IsActive=prm_isActive						 
             where   TMS_UserTask.Id = prm_id AND TMS_UserTask.ClientId = prm_ClientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from TMS_UserTask
			where
			 TMS_UserTask.Id=prm_id AND TMS_UserTask.ClientId = prm_ClientId; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   TMS_UserTask set IsActive=1
			where 
			 TMS_UserTask.Id=prm_id AND TMS_UserTask.ClientId = prm_ClientId ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update  TMS_UserTask set IsActive=0
			where
			 TMS_UserTask.Id=prm_id AND TMS_UserTask.ClientId = prm_ClientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for TMS_Search_Attachment
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Search_Attachment`;
delimiter ;;
CREATE PROCEDURE `TMS_Search_Attachment`(in whereClause varchar(5000))
BEGIN
     set @querystr ='SELECT * 
			FROM
			TMS_Attachments  ';
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
-- Procedure structure for TMS_Search_Task
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Search_Task`;
delimiter ;;
CREATE PROCEDURE `TMS_Search_Task`(in whereClause varchar(5000))
BEGIN
  set @querystr ='select * 
			FROM
			`TMS_vw_Task`  ';
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
-- Procedure structure for TMS_Search_TaskComment
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Search_TaskComment`;
delimiter ;;
CREATE PROCEDURE `TMS_Search_TaskComment`(in whereClause varchar(5000))
BEGIN
     set @querystr ='SELECT * 
			FROM
			TMS_Taskcomment ';
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
-- Procedure structure for TMS_Search_Tasks
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Search_Tasks`;
delimiter ;;
CREATE PROCEDURE `TMS_Search_Tasks`(in whereClause varchar(5000))
BEGIN
  set @querystr ='select * FROM	TMS_vw_Task';
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
-- Procedure structure for TMS_Search_UserTask
-- ----------------------------
DROP PROCEDURE IF EXISTS `TMS_Search_UserTask`;
delimiter ;;
CREATE PROCEDURE `TMS_Search_UserTask`(in WhereClause varchar(5000))
BEGIN
 set @queryStr= "select * from `TMS_vw_UserTask`";
 set @queryStr= concat(@queryStr, WhereClause);
 prepare stm1
 from @queryStr;
 execute stm1;
 deallocate prepare stm1;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for VOC_Manage_UserVocabulary
-- ----------------------------
DROP PROCEDURE IF EXISTS `VOC_Manage_UserVocabulary`;
delimiter ;;
CREATE PROCEDURE `VOC_Manage_UserVocabulary`(in prm_id int,
				in prm_clientId int,
        in prm_wordId varchar(50),
        in prm_userId varchar(255),
        in prm_pronunciation varchar(500),
        in prm_sentence varchar(5000),
        in prm_vocabDifficultyLevelId int,
        in prm_novelId int,
        in prm_chapterId int,
        in prm_comments varchar(5000),
        in prm_isNeedHelp bit,
        in prm_createdOn datetime,
        in prm_createdById int,
        in prm_modifiedOn datetime,
        in prm_modifiedById int,
        in prm_isActive bit,
        in prm_DBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into  voc_uservocabulary (
					 Id
					 ,ClientId
					 ,WordId
					 ,UserId
					 ,Pronunciation
					 ,Sentence
					 ,VocabDifficultyLevelId
					 ,NovelId
					 ,ChapterId
					 ,Comments
					 ,IsNeedHelp
					 ,CreatedOn
					 ,CreatedById
					 ,ModifiedOn
					 ,ModifiedById
					 ,IsActive ) 
           values (
					 prm_id
					 ,prm_clientId
					 ,prm_wordId
					 ,prm_userId
					 ,prm_pronunciation
					 ,prm_sentence
					 ,prm_vocabDifficultyLevelId
					 ,prm_novelId
					 ,prm_chapterId
					 ,prm_comments
					 ,prm_isNeedHelp
					 ,prm_createdOn
					 ,prm_createdById
					 ,prm_modifiedOn
					 ,prm_modifiedById
					 ,prm_isActive );
 end if;   

 if prm_DBoperation ='Update'
 then
            update  voc_uservocabulary set 
                          WordId=prm_wordId,
                          UserId =prm_userId,
                          Pronunciation=prm_pronunciation,
                          Sentence=prm_sentence,
                          VocabDifficultyLevelId=prm_vocabDifficultyLevelId,
                          NovelId=prm_novelId,
                          ChapterId=prm_chapterId,
                          Comments=prm_comments,
                          IsNeedHelp =prm_isNeedHelp,
                          CreatedOn=prm_createdOn,
                          CreatedById=prm_createdById,
                          ModifiedOn=prm_modifiedOn,
                          ModifiedById=prm_modifiedById,
						              IsActive=prm_isActive						 
             where   voc_uservocabulary.Id =prm_id and voc_uservocabulary.ClientId=prm_clientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from voc_uservocabulary
			where
			 voc_uservocabulary.Id =prm_id and voc_uservocabulary.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   voc_uservocabulary set IsActive=1
			where 
			 voc_uservocabulary.Id =prm_id and voc_uservocabulary.ClientId=prm_clientId;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
            update  voc_uservocabulary set IsActive=0
			where
			 voc_uservocabulary.Id =prm_id and voc_uservocabulary.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for VOC_Manage_Vocabulary
-- ----------------------------
DROP PROCEDURE IF EXISTS `VOC_Manage_Vocabulary`;
delimiter ;;
CREATE PROCEDURE `VOC_Manage_Vocabulary`(in prm_id int
  ,in prm_clientId int
  ,in prm_word Varchar(50)
  ,in prm_englishMeaning Varchar(500)
  ,in prm_urduMeaning Varchar(500)
  ,in prm_createdById INT
  ,in prm_createdOn datetime
  ,in prm_modifiedById INT
  ,in prm_modifiedOn datetime
  ,in prm_isActive bit
  ,in prm_dBoperation varchar(50))
BEGIN
if prm_DBoperation = 'Insert'
then
           insert into VOC_Vocabulary (Id
           ,ClientId
           ,Word
           , EnglishMeaning
           ,UrduMeaning
           ,CreatedById
           ,CreatedOn
           ,ModifiedOn
           ,ModifiedById
           , IsActive) 
			values (prm_id
            ,prm_clientId
            ,prm_word
            ,prm_englishMeaning
            ,prm_urduMeaning
            ,prm_createdById
            ,prm_createdOn
            ,prm_modifiedOn
            ,prm_modifiedById
            ,prm_isActive );
           
 end if;   
/*update*/
 if prm_DBoperation ='Update'
 then
            update  VOC_Vocabulary set 
				Word =prm_word
				,EnglishMeaning=prm_englishMeaning
                ,UrduMeaning=prm_urduMeaning
                ,CreatedById =prm_createdById 
				,CreatedOn=prm_createdOn 
				,ModifiedById=prm_modifiedById
				,ModifiedOn=prm_modifiedOn
				,IsActive=prm_isActive
             where   VOC_Vocabulary.Id =prm_id AND VOC_Vocabulary.ClientId = prm_ClientId;
   end if;
   IF prm_DBoperation = 'Delete'
    then
            delete from VOC_Vocabulary
			where
			 VOC_Vocabulary.Id=prm_id and VOC_Vocabulary.ClientId=prm_clientId ; 
        END if;
IF prm_DBoperation = 'Activate'
        then
            update   VOC_Vocabulary set IsActive=1
			where 
			VOC_Vocabulary.Id=prm_id and VOC_Vocabulary.ClientId=prm_clientId ;
        END if;
IF prm_DBoperation = 'DeActivate'
        then
				update VOC_UserVocabulary set IsActive=0 where VOC_UserVocabulary.WordId=prm_id and VOC_UserVocabulary.ClientId=prm_clientId;
            update  VOC_Vocabulary set IsActive=0
			where
			 VOC_Vocabulary.Id=prm_id and VOC_Vocabulary.ClientId=prm_clientId; 
        END if;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for VOC_Search_UserVocabulary
-- ----------------------------
DROP PROCEDURE IF EXISTS `VOC_Search_UserVocabulary`;
delimiter ;;
CREATE PROCEDURE `VOC_Search_UserVocabulary`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * FROM VOC_vw_UserVocabulary ';
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
-- Procedure structure for VOC_Search_Vocabulary
-- ----------------------------
DROP PROCEDURE IF EXISTS `VOC_Search_Vocabulary`;
delimiter ;;
CREATE PROCEDURE `VOC_Search_Vocabulary`(in whereClause varchar(5000))
BEGIN
     set @querystr ='select * FROM VOC_Vocabulary ';
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
