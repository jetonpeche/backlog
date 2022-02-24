import { TypeRole } from "../enums/TypeRole";
import { Compte } from "./Compte";

export type CompteClient = Compte &
{
    Adresse: string,
    NomEntreprise: string,
}