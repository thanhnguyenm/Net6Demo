---create database
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'SettlementDb')
BEGIN
	CREATE DATABASE [SettlementDb]
END
GO

USE [SettlementDb]
GO

--Drop Table SettlementBooking
---create tables
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SettlementBooking' and xtype='U')
BEGIN
    CREATE TABLE SettlementBooking (
        SettlementBookingId UNIQUEIDENTIFIER PRIMARY KEY,
        [Name] NVARCHAR(1000),
		BookingTimeFrom DATETIME,
		BookingTimeTo DATETIME,
    )
END
