erDiagram
    CLIENT ||--o{ OBIECTIV : "are"
    CLIENT ||--o{ COMANDA : "plaseaza"
    OBIECTIV ||--o{ CALCUL_CONSUM : "contine"
    OBIECTIV ||--o{ COMANDA : "asociata"
    MATERIAL ||--o{ CALCUL_CONSUM : "utilizat_in"
    MATERIAL ||--o{ DETALII_COMANDA : "apartin"
    COMANDA ||--o{ DETALII_COMANDA : "contine"

    CLIENT {
        int IdClient "Cheie Primară"
        string Nume "Nume Companie"
        string CUI "Cod Înregistrare Fiscală"
        string Adresa "Adresă"
        string Telefon "Telefon"
        string Email "Email"
        string Localitate "Localitate"
        string CodPostal "Cod Poștal"
        datetime DataInregistrare "Data Înregistrare"
    }

    OBIECTIV {
        int IdObiectiv "Cheie Primară"
        string Denumire "Denumire Proiect"
        int IdClient "Cheie Străină - Client"
        decimal SuprafataM2 "Suprafață în m²"
        string Descriere "Descriere"
        string Adresa "Adresă"
        string Localitate "Localitate"
        datetime DataCrearii "Data Creare"
        datetime DataFinalizarii "Data Finalizare"
        string Status "Status (Activ/Închis/Suspendat)"
    }

    MATERIAL {
        int IdMaterial "Cheie Primară"
        string Denumire "Denumire Material"
        string Tip "Tip (Polistiren/Adeziv/Dibleuri/Plasă/Tencuiala/Amorsa)"
        string Descriere "Descriere"
        decimal Pret "Preț Unitar"
        string Unitate "Unitate (buc/kg/l/m²)"
        decimal DensitateKgM3 "Densitate kg/m³"
        decimal ConductivitateTermica "Conductivitate Termică W/(m·K)"
        int StocDisponibil "Stoc Disponibil"
        datetime DataAdaugarii "Data Adăugare"
        boolean Activ "Activ"
    }

    CALCUL_CONSUM {
        int IdCalculConsum "Cheie Primară"
        int IdObiectiv "Cheie Străină - Obiectiv"
        int IdMaterial "Cheie Străină - Material"
        decimal ConsumPeM2 "Consum pe m²"
        decimal ConsumTotal "Consum Total"
        decimal PretUnitar "Preț Unitar"
        decimal PretTotal "Preț Total"
        int Grosime "Grosime (mm)"
        decimal Randament "Randament Aplicare (%)"
        datetime DataCalcul "Data Calcul"
        string Observatii "Observații"
    }

    COMANDA {
        int IdComanda "Cheie Primară"
        int IdClient "Cheie Străină - Client"
        int IdObiectiv "Cheie Străină - Obiectiv (opțional)"
        datetime DataComanda "Data Comandă"
        datetime DataLivrare "Data Livrare"
        string Status "Status (Nouă/Confirmată/Pregătire/Livrată/Anulată)"
        decimal ValoareTotala "Valoare Totală"
        decimal TVA "TVA"
        decimal Reducere "Reducere"
        string Observatii "Observații"
    }

    DETALII_COMANDA {
        int IdDetaliiComanda "Cheie Primară"
        int IdComanda "Cheie Străină - Comandă"
        int IdMaterial "Cheie Străină - Material"
        decimal Cantitate "Cantitate"
        decimal PretUnitar "Preț Unitar"
        decimal PretTotal "Preț Total"
        decimal ProcentReducere "Procent Reducere (%)"
    }
