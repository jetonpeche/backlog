DROP TABLE Ticket;
DROP TABLE Projet_Compte;
DROP TABLE Compte;
DROP TABLE Projet_Tache;
DROP TABLE Projet;
DROP TABLE EtatTicket;
DROP TABLE TypeCompte;
DROP TABLE TypeRetour;
DROP TABLE StatusProjet;

CREATE TABLE TypeCompte
(
    id int PRIMARY KEY,
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

CREATE TABLE StatusProjet
(
    id int PRIMARY KEY IDENTITY(1,1),
    nom varchar(200) NOT NULL
);

CREATE TABLE StatusTache
(
    id int PRIMARY KEY,
    nom varchar(200) NOT NULL
);

CREATE TABLE Compte
(
    id int PRIMARY KEY IDENTITY (1,1),

    nom varchar(255) NOT NULL,
    prenom varchar(255) NOT NULL,

    mail varchar(300) NOT NULL,
    mdp varchar(300) NOT NULL,

    tel char(10) NOT NULL,

    -- info si compte est client
    adresse varchar(300) DEFAULT '',
    nomEntreprise varchar(300) DEFAULT '',

    idTypeCompte int NOT NULL,

    FOREIGN KEY (idTypeCompte) REFERENCES TypeCompte(id)
);

CREATE TABLE Projet
(
    id int PRIMARY KEY IDENTITY (1,1),

    nom varchar(255) NOT NULL,
    description varchar(1500) NOT NULL,

    idStatus int DEFAULT 2,
    idCompteClient int NOT NULL,

    FOREIGN KEY (idStatus) REFERENCES StatusProjet(id),
    FOREIGN KEY (idCompteClient) REFERENCES Compte(id)
);

CREATE TABLE Projet_Tache
(
    id int PRIMARY KEY,

    description varchar(1500) NOT NULL,
    idStatusTache int NOT NULL,
    idProjet int NOT NULL,

    FOREIGN KEY (idStatusTache) REFERENCES StatusTache(id),
    FOREIGN KEY (idProjet) REFERENCES Projet(id)
);

CREATE TABLE Projet_Compte
(
    idCompte int,
    idProjet int,
    estChefProjet int DEFAULT 0,

    PRIMARY KEY (idCompte, idProjet),

    FOREIGN KEY (idCompte) REFERENCES Compte(id),
    FOREIGN KEY (idProjet) REFERENCES Projet(id)
);

CREATE TABLE Ticket
(
    id int PRIMARY KEY IDENTITY (1,1),

    idProjet int NOT NULL,
    idCompte int NOT NULL,
    idTypeRetour int NOT NULL,
    idEtatTicket int NOT NULL,

    msg varchar(1000) NOT NULL,

    FOREIGN KEY (idProjet) REFERENCES Projet(id),
    FOREIGN KEY (idCompte) REFERENCES Compte(id),
    FOREIGN KEY (idTypeRetour) REFERENCES TypeRetour(id),
    FOREIGN KEY (idEtatTicket) REFERENCES EtatTicket(id)
);

-- insert des données

SET IDENTITY_INSERT TypeRetour ON;
INSERT INTO TypeRetour (id, nom) VALUES (1, 'Bug'), (2, 'Question'), (3, 'Demande d ajout nouvelle fonctionnalitée'), 
                                        (4, 'Demande RDV'), (5, 'Demande modification fonctionnalitée'), 
                                        (6, 'Demande de suppression fonctionnalitée');
SET IDENTITY_INSERT TypeRetour OFF;

SET IDENTITY_INSERT StatusProjet ON;
INSERT INTO StatusProjet (id, nom) VALUES (1, 'En attente client'), (2, 'Pas démarré'), (3, 'En cours'), (4, 'Terminé');
SET IDENTITY_INSERT StatusProjet OFF;

INSERT INTO TypeCompte (id, nom) VALUES (1, 'Admin'), (2, 'Développeur'), (3, 'Client');
INSERT INTO StatusTache (id, nom) VALUES (1, 'A faire'), (2, 'En cours'), (3, 'Terminé');
