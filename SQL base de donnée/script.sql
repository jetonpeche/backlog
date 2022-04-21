DROP TABLE Ticket;
DROP TABLE Projet_Compte;
DROP TABLE Projet_Tache;
DROP TABLE Projet;
DROP TABLE EtatTicket;
DROP TABLE Compte;
DROP TABLE TypeCompte;
DROP TABLE TypeRetour;
DROP TABLE StatusProjet;
DROP TABLE StatusTache;

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
    id int PRIMARY KEY IDENTITY(1,1),
    nom varchar(200) NOT NULL,
    couleurFont char(7) NOT NULL
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
    id int PRIMARY KEY IDENTITY(1, 1),

    description varchar(1500) NOT NULL,
    idStatusTache int NOT NULL,
    idProjet int NOT NULL,

    FOREIGN KEY (idStatusTache) REFERENCES StatusTache(id),
    FOREIGN KEY (idProjet) REFERENCES Projet(id) ON DELETE CASCADE
);

CREATE TABLE Projet_Compte
(
    idCompte int,
    idProjet int,
    estChefProjet int DEFAULT 0,

    PRIMARY KEY (idCompte, idProjet),

    FOREIGN KEY (idCompte) REFERENCES Compte(id),
    FOREIGN KEY (idProjet) REFERENCES Projet(id) ON DELETE CASCADE
);

CREATE TABLE Ticket
(
    id int PRIMARY KEY IDENTITY (1,1),

    idProjet int NOT NULL,
    idCompte int NOT NULL,
    idTypeRetour int NOT NULL,
    idEtatTicket int NOT NULL,

    msg varchar(1000) NOT NULL,

    FOREIGN KEY (idProjet) REFERENCES Projet(id) ON DELETE CASCADE,
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

SET IDENTITY_INSERT StatusTache ON;
INSERT INTO StatusTache (id, nom, couleurFont) VALUES (1, 'A faire', '#f23a2f'), (2, 'En cours', '#ffb80c'), (3, 'Terminé', '#43a547');
SET IDENTITY_INSERT StatusTache OFF;

-- mdp azertyui
SET IDENTITY_INSERT Compte ON;
INSERT INTO Compte (id, nom, prenom, mail, mdp, tel, idTypeCompte) VALUES 
(1, 'Admin', 'Nicolas', 'admin@gmail.com', '$2a$11$0bCxQ/lStrdY01118wpzGef1/PwWJI7P1b71wRpzPLJ30lne8Zami', '1234567890', 1), 
(2, 'Dev', 'Nicolas', 'dev@gmail.com', '$2a$11$0bCxQ/lStrdY01118wpzGef1/PwWJI7P1b71wRpzPLJ30lne8Zami', '1234567890', 2),
(5, 'Dev1', 'Peche', 'a@b.com', '$2a$11$0bCxQ/lStrdY01118wpzGef1/PwWJI7P1b71wRpzPLJ30lne8Zami', '1234567890', 2),
(6, 'Dev2', 'Peche', 'a@b.com', '$2a$11$0bCxQ/lStrdY01118wpzGef1/PwWJI7P1b71wRpzPLJ30lne8Zami', '1234567890', 2);

INSERT INTO Compte (id, nom, prenom, mail, mdp, tel, adresse, nomEntreprise, idTypeCompte) VALUES 
(3, 'Client1', 'Peche', 'a@c.com', '$2a$11$0bCxQ/lStrdY01118wpzGef1/PwWJI7P1b71wRpzPLJ30lne8Zami', '1234567890', 'rue du test', 'Bungie', 3),
(4, 'Client2', 'Peche', 'a@d.com', '$2a$11$0bCxQ/lStrdY01118wpzGef1/PwWJI7P1b71wRpzPLJ30lne8Zami', '1234567890', 'rue du test', 'Bungie', 3);
SET IDENTITY_INSERT Compte OFF;

SET IDENTITY_INSERT Projet ON;
INSERT INTO Projet (id, nom, description, idCompteClient) VALUES 
(1, 'projet 1', 'lorem', 3),
(2, 'projet 2', 'lorem', 4);
SET IDENTITY_INSERT Projet OFF;

INSERT INTO Projet_Compte (idProjet, idCompte) VALUES (1, 2), (2, 2);

SET IDENTITY_INSERT Projet_Tache ON;
INSERT INTO Projet_Tache (id, description, idStatusTache, idProjet) VALUES 
(1, 'Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature 
    from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, 
    looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of 
    the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de 
    Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of 
    ethics, very popular during the Renaissance. The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", 
    comes from a line in section 1.10.32.', 1, 1),
(2, 'Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature 
from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, 
looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of 
the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de 
Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of 
ethics, very popular during the Renaissance. The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", 
comes from a line in section 1.10.32.', 1, 1),
(3, 'Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature 
    from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, 
    looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of 
    the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of "de 
    Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of 
    ethics, very popular during the Renaissance. The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", 
    comes from a line in section 1.10.32.', 1, 1);
SET IDENTITY_INSERT Projet_Tache OFF;