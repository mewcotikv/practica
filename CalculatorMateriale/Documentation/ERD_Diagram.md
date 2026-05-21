erDiagram
    CLIENT ||--o{ OBIECTIV : "are"
    CLIENT ||--o{ COMANDA : "plaseaza"
    OBIECTIV ||--o{ CALCUL_CONSUM : "contine"
    OBIECTIV ||--o{ COMANDA : "asociata"
    MATERIAL ||--o{ CALCUL_CONSUM : "utilizat_in"
    MATERIAL ||--o{ DETALII_COMANDA : "apartin"
    COMANDA ||--o{ DETALII_COMANDA : "contine"

    CLIENT {
        int IdClient PK
        string Nume
        string CUI
        string Adresa
        string Telefon
        string Email
        string Localitate
        string CodPostal
        datetime DataInregistrare
    }

    OBIECTIV {
        int IdObiectiv PK
        string Denumire
        int IdClient FK
        decimal SuprafataM2
        string Descriere
        string Adresa
        string Localitate
        datetime DataCrearii
        datetime DataFinalizarii
        string Status
    }

    MATERIAL {
        int IdMaterial PK
        string Denumire
        string Tip
        string Descriere
        decimal Pret
        string Unitate
        decimal DensitateKgM3
        decimal ConductivitateTermica
        int StocDisponibil
        datetime DataAdaugarii
        boolean Activ
    }

    CALCUL_CONSUM {
        int IdCalculConsum PK
        int IdObiectiv FK
        int IdMaterial FK
        decimal ConsumPeM2
        decimal ConsumTotal
        decimal PretUnitar
        decimal PretTotal
        int Grosime
        decimal Randament
        datetime DataCalcul
        string Observatii
    }

    COMANDA {
        int IdComanda PK
        int IdClient FK
        int IdObiectiv FK
        datetime DataComanda
        datetime DataLivrare
        string Status
        decimal ValoareTotala
        decimal TVA
        decimal Reducere
        string Observatii
    }

    DETALII_COMANDA {
        int IdDetaliiComanda PK
        int IdComanda FK
        int IdMaterial FK
        decimal Cantitate
        decimal PretUnitar
        decimal PretTotal
        decimal ProcentReducere
    }
