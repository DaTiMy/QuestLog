CREATE TABLE Migrations(
versionID int NOT NULL PRIMARY KEY,
versionName char(255),
checksum char(32));
/*
 ROLLBACK INFO V4
 
 DROP TABLE Migrations;
 
 */