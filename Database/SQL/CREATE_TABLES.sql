DROP TABLE SubGoal;
DROP TABLE Quest;
DROP TABLE SessionUser;
DROP TABLE Session;
DROP TABLE User;



CREATE TABLE User(
UID int UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Name char(25),
Username char(25),
Password char(60),
Slots int,
SessionToken char(8)
);

CREATE TABLE Session(
SID int UNSIGNED auto_increment PRIMARY KEY,
Name char(50),
Password char(60)
);


CREATE TABLE SessionUser(
SID int UNSIGNED,
UID int UNSIGNED,
isDM boolean,
PRIMARY KEY (SID, UID),
FOREIGN KEY (SID) REFERENCES Session(SID) ON DELETE CASCADE, 
FOREIGN KEY (UID) REFERENCES User(UID) ON DELETE CASCADE
);

CREATE TABLE Quest(
QID int UNSIGNED AUTO_INCREMENT PRIMARY KEY,
SID int UNSIGNED,
Name char(50),
EXP int,
Copper int,
Silver int,
Gold int,
Finish boolean,
OrderNumber int,
FOREIGN KEY (SID) REFERENCES Session(SID) ON DELETE CASCADE
);

CREATE TABLE SubGoal(
SGID int UNSIGNED AUTO_INCREMENT PRIMARY KEY,
QID int UNSIGNED,
Name char(50),
Description char(255),
Finish boolean,
FOREIGN KEY (QID) REFERENCES Quest(QID) ON DELETE CASCADE
);





