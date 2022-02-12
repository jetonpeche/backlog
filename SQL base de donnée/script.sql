DROP TABLE Ticket;
DROP TABLE Projet_Compte;
DROP TABLE Compte;
DROP TABLE Projet;
DROP TABLE EtatTicket;
DROP TABLE TypeCompte;
DROP TABLE TypeRetour;

CREATE TABLE TypeCompte
(
    id int PRIMARY KEY IDENTITY (1,1),
    nom varchar(300) NOT NULL
);

CREATE TABLE TypeRetour
(
    id int PRIMARY KEY IDENTITY (1,1),
    nom varchar(300) NOT NULL
);

CREATE TABLE EtatTicket
(
    id int PRIMARY KEY IDENTITY (1,1),
    nom varchar(200) NOT NULL
);

CREATE TABLE Projet
(
    id int PRIMARY KEY IDENTITY (1,1),
    nom varchar(255) NOT NULL,
    description varchar(1500) NOT NULL
);

CREATE TABLE Compte
(
    id int PRIMARY KEY IDENTITY (1,1),
    nom varchar(255) NOT NULL,
    prenom varchar(255) NOT NULL,

    mail varchar(300) NOT NULL,
    mdp varchar(300) NOT NULL,

    tel varchar(20) NOT NULL,

    nomEntreprise varchar(300) NOT NULL,
    idTypeCompte int NOT NULL,

    FOREIGN KEY (idTypeCompte) REFERENCES TypeCompte(id)
);

CREATE TABLE Projet_Compte
(
    idCompte int,
    idProjet int,
    estChefProjet int,

    PRIMARY KEY (idCompte, idProjet),

    FOREIGN KEY (idCompte) REFERENCES Compte(id),
    FOREIGN KEY (idProjet) REFERENCES Projet(id)
);

CREATE TABLE Ticket
(
    id int PRIMARY KEY IDENTITY (1,1),

    idProjet int,
    idCompte int,
    idTypeRetour int,
    idEtatTicket int,

    msg varchar(1000) NOT NULL,

    FOREIGN KEY (idProjet) REFERENCES Projet(id),
    FOREIGN KEY (idCompte) REFERENCES Compte(id),
    FOREIGN KEY (idTypeRetour) REFERENCES TypeRetour(id),
    FOREIGN KEY (idEtatTicket) REFERENCES EtatTicket(id)
);

SET IDENTITY_INSERT utilisateur ON; 

INSERT INTO TypeCompte (id, nom) VALUES (1, 'Admin'), (2, 'Développeur'), (3, 'Client');


SET IDENTITY_INSERT utilisateur OFF; 