# Hi i want to share my mini-project about Todo App.
## The following are the tasks that I will be working on along with the ERD schema for the todo application
![alt text](https://github.com/havidalbar/mini-project/blob/main/dokumentasi/task.jpeg)

## I have done all the functions and here is the list of API on the Swagger
![alt text](https://github.com/havidalbar/mini-project/blob/main/dokumentasi/list_api_1.png)
![alt text](https://github.com/havidalbar/mini-project/blob/main/dokumentasi/list_api_2.png)


## Before running the application, you can compile the SQL code to create all the tables
```sql
-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Aug 27, 2024 at 11:21 AM
-- Server version: 5.7.39
-- PHP Version: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `learn`
--

-- --------------------------------------------------------

--
-- Table structure for table `Roles`
--

CREATE TABLE `Roles` (
  `Id` varchar(36) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL,
  `DateDeleted` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Roles`
--

INSERT INTO `Roles` (`Id`, `Name`, `DateCreated`, `DateUpdated`, `DateDeleted`) VALUES
('6c9f8183-e6c2-452b-b589-ea510c455f63', 'USER', '0001-01-01 00:00:00', NULL, NULL),
('d2a7d771-aa82-4bfb-909b-2a620afad225', 'ADMIN', '0001-01-01 00:00:00', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `Todo`
--

CREATE TABLE `Todo` (
  `TodoId` varchar(36) NOT NULL,
  `Day` varchar(50) DEFAULT NULL,
  `TodayDate` datetime DEFAULT NULL,
  `Note` varchar(200) DEFAULT NULL,
  `DetailCount` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Todo`
--

INSERT INTO `Todo` (`TodoId`, `Day`, `TodayDate`, `Note`, `DetailCount`) VALUES
('08dcc671-29e3-48df-822f-a46ea4eb2c96', 'Sunday', '2024-08-27 00:00:00', 'Second Chance', 5),
('08dcc671-2f5b-4b89-80d5-f027e51e0d34', 'Sunday', '2024-08-27 00:00:00', 'Second First', 3),
('08dcc682-264e-45b3-84b6-adcc05667e1d', 'Monday', '2024-08-27 00:00:00', 'Second Branch', 0);

-- --------------------------------------------------------

--
-- Table structure for table `TodoDetail`
--

CREATE TABLE `TodoDetail` (
  `TodoDetailId` varchar(36) NOT NULL,
  `TodoId` varchar(36) DEFAULT NULL,
  `Activity` varchar(100) DEFAULT NULL,
  `Category` varchar(50) DEFAULT NULL,
  `DetailNote` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `TodoDetail`
--

INSERT INTO `TodoDetail` (`TodoDetailId`, `TodoId`, `Activity`, `Category`, `DetailNote`) VALUES
('08dcc671-6e12-49c3-84e0-4badafaabf41', '08dcc671-29e3-48df-822f-a46ea4eb2c96', 'Begin Init 2', 'Task', 'Init maba 2'),
('08dcc671-7953-4b4a-8229-576176ab8940', '08dcc671-29e3-48df-822f-a46ea4eb2c96', 'Begin Init 2', 'DailyActivity', 'Init maba 2'),
('08dcc672-4412-4b22-8e3e-364a57351005', '08dcc671-2f5b-4b89-80d5-f027e51e0d34', 'Begin Init 1 updated', 'DailyActivity', 'Init maba 1 updated'),
('08dcc675-1faa-40ef-8221-76e90984be88', '08dcc671-2f5b-4b89-80d5-f027e51e0d34', 'Begin Init 1', 'DailyActivity', 'Init maba 1'),
('08dcc675-1faa-497b-840c-622d30452930', '08dcc671-29e3-48df-822f-a46ea4eb2c96', 'Begin Last 3', 'DailyActivity', 'Init maba 3'),
('08dcc678-ad01-4af1-8ed0-0eb32edba8b2', '08dcc671-2f5b-4b89-80d5-f027e51e0d34', 'Begin Init 2', 'DailyActivity', 'Init maba 1'),
('08dcc678-ad02-4746-825f-bbfdf4ed4ba5', '08dcc671-29e3-48df-822f-a46ea4eb2c96', 'Begin Last 4', 'DailyActivity', 'Init maba 3');

-- --------------------------------------------------------

--
-- Table structure for table `UserRole`
--

CREATE TABLE `UserRole` (
  `RoleId` varchar(36) DEFAULT NULL,
  `UserId` varchar(36) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `UserRole`
--

INSERT INTO `UserRole` (`RoleId`, `UserId`) VALUES
('6c9f8183-e6c2-452b-b589-ea510c455f63', '63512f1d-3ca9-4f11-911b-4179d74c7d61'),
('d2a7d771-aa82-4bfb-909b-2a620afad225', '56a13d01-52f0-461b-aa86-07a148bfe7bf');

-- --------------------------------------------------------

--
-- Table structure for table `Users`
--

CREATE TABLE `Users` (
  `Id` varchar(36) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Password` varchar(200) DEFAULT NULL,
  `RefreshToken` varchar(200) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateUpdated` datetime DEFAULT NULL,
  `DateDeleted` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Users`
--

INSERT INTO `Users` (`Id`, `Name`, `Password`, `RefreshToken`, `DateCreated`, `DateUpdated`, `DateDeleted`) VALUES
('56a13d01-52f0-461b-aa86-07a148bfe7bf', 'admin@gmail.com', '$2a$11$cfMHq3GI5V3W7OqLL0aMHOweawP2VKoQcYH3CSU637rgG4/jjz9L2', '00000000-0000-0000-0000-000000000000', '0001-01-01 00:00:00', NULL, NULL),
('63512f1d-3ca9-4f11-911b-4179d74c7d61', 'user@gmail.com', '$2a$11$Ed0J.6zTFFcKl7ExhX.A7uMC1luf.x9BgI4.H8pFS3iGzmPccA77a', '313e43a3-ecbd-4b11-b0bf-ae09a9097c8f', '0001-01-01 00:00:00', NULL, NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Roles`
--
ALTER TABLE `Roles`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `Todo`
--
ALTER TABLE `Todo`
  ADD PRIMARY KEY (`TodoId`);

--
-- Indexes for table `TodoDetail`
--
ALTER TABLE `TodoDetail`
  ADD PRIMARY KEY (`TodoDetailId`);

--
-- Indexes for table `UserRole`
--
ALTER TABLE `UserRole`
  ADD KEY `RoleId` (`RoleId`),
  ADD KEY `UserId` (`UserId`);

--
-- Indexes for table `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`Id`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `UserRole`
--
ALTER TABLE `UserRole`
  ADD CONSTRAINT `userrole_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`),
  ADD CONSTRAINT `userrole_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;



```
