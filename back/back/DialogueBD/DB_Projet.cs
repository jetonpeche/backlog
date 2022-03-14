namespace back.DialogueBD
{
    public static class DB_Projet
    {
        public static backlogContext context;

        public static IQueryable Lister()
        {
            return context.Projets.Select(
                (p) =>
                new
                {
                    p.Id,
                    p.Nom,
                    p.Description,

                    Client = new
                            {
                                Id = p.IdCompteClient,
                                p.IdCompteClientNavigation.Prenom,
                                p.IdCompteClientNavigation.Nom,
                                p.IdCompteClientNavigation.Mail,
                                p.IdCompteClientNavigation.Tel,
                                p.IdCompteClientNavigation.Adresse,
                                p.IdCompteClientNavigation.NomEntreprise
                            },

                    IdStatutProjet = p.IdStatusNavigation.Id,
                    StatutProjet = p.IdStatusNavigation.Nom
                });
        }

        public static IQueryable Lister(int _idCompteClient)
        {
            var liste = from p in context.Projets
                        where p.IdCompteClient == _idCompteClient
                        select new
                        {
                            p.Id,
                            p.Nom,
                            p.Description,

                            Client = new
                            {
                                Id = p.IdCompteClient,
                                p.IdCompteClientNavigation.Prenom,
                                p.IdCompteClientNavigation.Nom,
                                p.IdCompteClientNavigation.Mail,
                                p.IdCompteClientNavigation.Tel,
                                p.IdCompteClientNavigation.Adresse,
                                p.IdCompteClientNavigation.NomEntreprise
                            },

                            IdStatutProjet = p.IdStatusNavigation.Id,
                            StatutProjet = p.IdStatusNavigation.Nom
                        };

            return liste;
        }

        public static IQueryable ListerProjetDuDev(int _idCompteDev)
        {
            var liste = from p in context.ProjetComptes
                        where p.IdCompte == _idCompteDev
                        select new
                        {
                            Id = p.IdProjet,
                            Nom = p.IdProjetNavigation.Nom,
                            Description = p.IdProjetNavigation.Description,

                            Client = new
                            {
                                Id = p.IdProjetNavigation.IdCompteClient,
                                p.IdProjetNavigation.IdCompteClientNavigation.Prenom,
                                p.IdProjetNavigation.IdCompteClientNavigation.Nom,
                                p.IdProjetNavigation.IdCompteClientNavigation.Mail,
                                p.IdProjetNavigation.IdCompteClientNavigation.Tel,
                                p.IdProjetNavigation.IdCompteClientNavigation.Adresse,
                                p.IdProjetNavigation.IdCompteClientNavigation.NomEntreprise
                            },

                            IdStatutProjet = p.IdProjetNavigation.IdStatusNavigation.Id,
                            StatutProjet = p.IdProjetNavigation.IdStatusNavigation.Nom
                        };

            return liste;
        }

        public static IQueryable ListerProjetPasAssocier(int _idCompte)
        {
            var liste = from p in context.ProjetComptes
                        where p.IdCompte != _idCompte
                        select new
                        {
                            Id = p.IdProjet,
                            Nom = p.IdProjetNavigation.Nom,
                            Description = p.IdProjetNavigation.Description,

                            Client = new
                            {
                                Id = p.IdProjetNavigation.IdCompteClient,
                                p.IdProjetNavigation.IdCompteClientNavigation.Prenom,
                                p.IdProjetNavigation.IdCompteClientNavigation.Nom,
                                p.IdProjetNavigation.IdCompteClientNavigation.Mail,
                                p.IdProjetNavigation.IdCompteClientNavigation.Tel,
                                p.IdProjetNavigation.IdCompteClientNavigation.Adresse,
                                p.IdProjetNavigation.IdCompteClientNavigation.NomEntreprise
                            },

                            IdStatutProjet = p.IdProjetNavigation.IdStatusNavigation.Id,
                            StatutProjet = p.IdProjetNavigation.IdStatusNavigation.Nom
                        };

            return liste;
        }

        public static int Ajouter(Projet _projet)
        {
            context.Projets.Add(_projet);
            context.SaveChanges();

            return context.Projets.OrderByDescending(p => p.Id).First().Id;
        }

        public static void Supprimer(int _idProjet)
        {
            Projet projet = context.Projets.Where(p => p.Id == _idProjet).First();

            context.Projets.Remove(projet);
            context.SaveChanges();
        }
    }
}
