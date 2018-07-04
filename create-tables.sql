CREATE TABLE Orders 
(
    OrderId         VARCHAR(16),
    UserContactId   VARCHAR(16),
    InsertDate      DATETIME,
    CONSTRAINT Orders_PK PRIMARY KEY (OrderId)
);

INSERT INTO Orders VALUES('1', 'B', '2018-05-01');
INSERT INTO Orders VALUES('2', 'C', '2018-06-19');
INSERT INTO Orders VALUES('3', 'A', '2018-07-04');

CREATE TABLE Contacts 
(
    UserContactId     VARCHAR(16),
    UserContactName   VARCHAR(16),
    CONSTRAINT Orders_PK PRIMARY KEY (UserContactId)
);
INSERT INTO Contacts VALUES('A', 'Apple');
INSERT INTO Contacts VALUES('B', 'Boy');
INSERT INTO Contacts VALUES('C', 'Cat');