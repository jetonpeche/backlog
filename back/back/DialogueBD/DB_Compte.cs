namespace back.DialogueBD;

public static class DB_Compte
{
    public static backlogContext context;

    public static IQueryable ListerCompte()
    {
        var liste = from compte in context.Comptes
                    orderby compte.Nom
                    select new
                    {
                        compte.Id,
                        compte.Nom,
                        compte.Prenom,
                        compte.Mail,
                        compte.Tel,
                        compte.Adresse,
                        compte.NomEntreprise,
                        compte.IdTypeCompte,
                        TypeCompte = compte.IdTypeCompteNavigation.Nom,
                    };

        return liste;
    }

    public static IQueryable ListerCompteDev()
    {
        var liste = from compte in context.Comptes
                    where compte.IdTypeCompteNavigation.Nom == "Développeur"
                    orderby compte.Nom
                    select new
                    {
                        compte.Id,
                        compte.Nom,
                        compte.Prenom,
                        compte.Mail,
                        compte.Tel,
                        compte.IdTypeCompte,
                        TypeCompte = compte.IdTypeCompteNavigation.Nom,
                    };

        return liste;
    }

    public static IQueryable ListerCompteClient()
    {
        var liste = from compte in context.Comptes
                    where compte.IdTypeCompteNavigation.Nom == "Client"
                    orderby compte.Nom
                    select new
                    {
                        compte.Id,
                        compte.Nom,
                        compte.Prenom,
                        compte.Mail,
                        compte.Tel,
                        compte.Adresse,
                        compte.NomEntreprise,
                        compte.IdTypeCompte,
                        TypeCompte = compte.IdTypeCompteNavigation.Nom,
                    };

        return liste;
    }

    public static IQueryable ListerCompteProjet(int _idProjet)
    {
        var liste = from c in context.ProjetComptes
                    where c.IdProjet == _idProjet
                    select new
                    {
                        c.IdCompte,
                        c.IdCompteNavigation.Nom,
                        c.IdCompteNavigation.Prenom
                    };

        return liste;
    }

    public static IQueryable ListerIdCompteProjet(int _idProjet)
    {
        var liste =  from c in context.ProjetComptes
                     where c.IdProjet == _idProjet
                     select new { c.IdCompte };

        return liste;
    }

    public static dynamic Compte(int _id)
    {
        dynamic info = (from compte in context.Comptes
                     where compte.Id == _id
                     select new
                     {
                         compte.Id,
                         compte.Nom,
                         compte.Prenom,
                         compte.Mail,
                         compte.Tel,
                         compte.NomEntreprise,
                         compte.IdTypeCompte,
                         TypeCompte = compte.IdTypeCompteNavigation.Nom,
                     }).First();

        return info;
    }

    public static int CreerCompte(Compte _compte)
    {
        context.Comptes.Add(_compte);

        context.SaveChanges();

        return context.Comptes.OrderByDescending(c => c.Id).First().Id;
    }

    public static void Modifier(Compte _compte)
    {
        _compte.Mdp = context.Comptes.Where(c => c.Id == _compte.Id).Select(c => c.Mdp).First();

        context.Comptes.Update(_compte);
        context.SaveChanges();
    }
}

