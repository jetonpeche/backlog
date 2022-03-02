export type Projet =
{
    Id: number,
    Nom: string,
    Description: string,

    IdStatutProjet: number,
    StatutProjet: number,

    Client: Client
}

type Client =
{
    Id: number,
    Nom: string,
    Prenom: string,
    Mail: string,
    Adresse: string,
    NomEntreprise: string,
    Tel: string
}