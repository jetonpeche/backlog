import { TypeRole } from "../enums/TypeRole";

export type Compte =
{
    Id: number,
    Nom: string,
    Prenom: string,
    Mail: string,
    NomEntreprise: string,
    TypeCompte: TypeRole
}