﻿namespace back.DialogueBD;

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
                        compte.NomEntreprise,

                        TypeCompte = compte.IdTypeCompteNavigation.Nom,
                    };

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
        context.Comptes.Update(_compte);
        context.SaveChanges();
    }
}

