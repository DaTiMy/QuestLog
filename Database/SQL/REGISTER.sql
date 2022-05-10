DELETE FROM Quest;
DELETE FROM SubGoal;
INSERT INTO User(name,username,password,slots,sessiontoken) VALUES(
"Tim",
"daTimy",
"$2a$12$lYWw4rIBOmQIfzkvNmJdg.vfs5P3ImNkQSBZaFZlwKWl01SZlSExC",
3,
"dsuiouiodhsaooi");

INSERT INTO User(name,username,password,slots,sessiontoken) VALUES(
"Toni",
"Sise",
"$2a$12$lYWw4rIBOmQIfzkvNmJdg.vfs5P3ImNkQSBZaFZlwKWl01SZlSExC",
3,
"dsuiouiodhsaooi32133");

INSERT INTO Session(name,password) VALUES("Blasda","123123");
INSERT INTO Session(name,password) VALUES("testo","1312341");

INSERT INTO Quest (sid,name,exp,copper,silver,gold,finish,ordernumber) VALUES (1,"Quest 1: Find Ledro!",100, 20, 5, 3, 0, 1);
INSERT INTO Quest (sid,name,exp,copper,silver,gold,finish,ordernumber) VALUES (1,"Quest 2: Find Tim!",100, 20, 5, 3, 0, 2);
INSERT INTO Quest (sid,name,exp,copper,silver,gold,finish,ordernumber) VALUES (2,"Quest 1: Find Jannes!",130, 20, 5, 3, 0, 1);
INSERT INTO Quest (sid,name,exp,copper,silver,gold,finish,ordernumber) VALUES (2,"Quest 1: Find Makro!",100, 20, 5, 3, 0, 2);



INSERT INTO SubGoal (qid,name,description,finish,ordernumber) VALUES (1,"SubQuest 1: Buy some Coke!", "Visit REWE!",0,1);
INSERT INTO SubGoal (qid,name,description,finish,ordernumber) VALUES (1,"SubQuest 2: Buy some Spezi!", "Visit Kaufland!",0,2);

INSERT INTO SubGoal (qid,name,description,finish,ordernumber) VALUES (4,"SubQuest 1: Buy some Mako!", "Visit bib!",0,1);
INSERT INTO SubGoal (qid,name,description,finish,ordernumber) VALUES (4,"SubQuest 2: Go skate!", "Visit Kaufland!",0,2);
INSERT INTO SubGoal (qid,name,description,finish,ordernumber) VALUES (4,"SubQuest 3: Listen to DnB!", "Visit Kany!",0,3);