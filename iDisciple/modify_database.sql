-- Used to modify the Db for the candidates pre-interview coding challenge
--Neal Lowrey, 404-514-3668

USE [FamousFolks]
GO

Create Table FolksGig(
	GigID int not null identity primary key ,
	Gig varchar(50) not null default '' unique 
);
GO

insert into  FolksGig (gig) values ('Physician');
insert into  FolksGig (gig) values ('Scholar');
insert into  FolksGig (gig) values ('Explorer');
insert into  FolksGig (gig) values ('Scientist');
insert into  FolksGig (gig) values ('Saint');
insert into  FolksGig (gig) values ('Rock Star');
insert into  FolksGig (gig) values ('Politician');
GO

ALTER TABLE Folks ADD GigID int CONSTRAINT fkGig FOREIGN KEY (GigID) REFERENCES FolksGig(GigID);  
GO

UPDATE Folks
SET GigID = (select GigID from FolksGig where Gig = 'Politician')
WHERE FirstName='Abraham' and LastName = 'Lincoln';
GO
