import { Compte } from "../types/Compte";
import { TypeCompte } from "../types/TypeCompte";

export class Variable
{
    static EstConnecter: boolean = true;
    static compteConnecter: Compte;
    static listeTypeCompte: TypeCompte[] = [];
}
