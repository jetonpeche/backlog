import { TypeRole } from "../enums/TypeRole";

export type Compte =
{
    Id: number,
    IdTypeCompte: number,
    Nom: string,
    Prenom: string,
    Mail: string,
    Tel: string,
    NomEntreprise: string,
    TypeCompte: TypeRole
}