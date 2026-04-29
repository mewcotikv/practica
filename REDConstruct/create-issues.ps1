#!/usr/bin/env powershell

# Script pentru creare issues pe GitHub
# Ruleaza: powershell -ExecutionPolicy Bypass -File create-issues.ps1

$owner = "mewcotikv"
$repo = "practica"

# Lista de issues de creat
$issues = @(
    @{
        title = "🐛 #1 - DataGrid nu se reîmprospătează după export"
        body = "**REZOLVAT** ✅`n`nProblema: DataGrid nu se reîmprospătează corect după export Excel.`n`nSoluție: Adaugă UI refresh forțat și error handling în RapoarteViewModel.`n`nBranch: issue/1-datagrid-refresh"
        labels = @("bug", "resolved")
    },
    @{
        title = "🐛 #2 - DatePicker nu validează Data Sfarsit < Data Inceput"
        body = "**REZOLVAT** ✅`n`nProblema: Utilizatorul poate selecta o dată de sfarsit mai mică decât data de inceput.`n`nSoluție: Adaugă validare date cu auto-corectare.`n`nBranch: issue/2-datevalidation"
        labels = @("bug", "resolved")
    },
    @{
        title = "✨ #3 - Raport 2: Costuri pe material (agregat)"
        body = "**IMPLEMENTAT** ✅`n`nCaracteristică: Creare raport nou care agregrează costuri pe material.`n`nInclud:`n- RaportMaterial model`n- GetRaportMaterial() în DataService`n- Grupare pe material cu suma costuri`n- Ordonare descrescătoare după cost`n`nBranch: feature/3-material-report"
        labels = @("feature", "resolved")
    },
    @{
        title = "✨ #4 - Adaugă grafice (Chart) pentru consum materiale"
        body = "Caracteristică: Vizualizare cu grafice pentru:`n- Consum pe obiectiv (pie chart)`n- Cost pe material (bar chart)`n- Tendință consum (line chart)`n`nBranch: feature/4-charts`n`n**Status: ⏳ Planificat**"
        labels = @("feature", "enhancement")
    },
    @{
        title = "✨ #5 - Conectare la bază de date SQL Server"
        body = "Caracteristică: Înlocuiți mock data cu conexiune reală SQL Server.`n`nInclud:`n- Connection string din app.config`n- CRUD operations pe Obiectiv și ConsuMaterial`n- Stored procedures pentru rapoarte`n- Migration script pentru tabele`n`n**Status: ⏳ Planificat**"
        labels = @("feature", "database")
    },
    @{
        title = "✨ #6 - Export în format PDF"
        body = "Caracteristică: Export rapoarte în PDF cu layout profesional.`n`nInclud:`n- Folosire iTextSharp sau SelectPdf`n- Header cu logo\n- Tabel cu date formatate`n- Total calculat`n- Footer cu dată/pagină`n`n**Status: ⏳ Planificat**"
        labels = @("feature", "export")
    },
    @{
        title = "✨ #7 - Importare date din Excel"
        body = "Caracteristică: Citire date din fișier Excel.`n`nInclud:`n- Upload Excel cu validare`n- Parsare rânduri`n- Validare date (duplicate, format)`n- Importare în bază de date`n- Raport cu rezultate importare`n`n**Status: ⏳ Planificat**"
        labels = @("feature", "import")
    },
    @{
        title = "🐛 #8 - Problema cu export Excel pe fișiere mari"
        body = "Problemă: Export Excel se blocează pe >10.000 rânduri.`n`nTrebuie:`n- Optimizare memoria (streaming)`n- Bară progres`n- Posibilitate cancelez`n\n**Status: ⏳ Backlog**"
        labels = @("bug", "performance")
    },
    @{
        title = "🐛 #9 - ComboBox nu se actualizează după adăugare obiectiv nou"
        body = "Problemă: Dacă adaug obiectiv din alta locație, ComboBox pe rapoarte nu se actualiza.`n`nTrebuie:`n- Event notificări (INotifyCollectionChanged)`n- Refresh observabil după insert`n\n**Status: ⏳ Backlog**"
        labels = @("bug", "ui")
    },
    @{
        title = "📚 #10 - Ghid utilizator complet (Wiki GitHub)"
        body = "Documenta ție: Creare wiki complet cu:`n- Instalare și setup`n- Ghid utilizator (screenshots)`n- Tutorial rapoarte`n- FAQ`n- Troubleshooting`n\n**Status: ⏳ Planificat**"
        labels = @("documentation", "wiki")
    },
    @{
        title = "📚 #11 - Screenshots și tutoriale în README"
        body = "Documenta ție: Adaugă în README:`n- Screenshots UI`n- Demo GIF cu workflow`n- Tabel cu rapoarte`n- Links la wiki`n\n**Status: ⏳ Planificat**"
        labels = @("documentation", "readme")
    },
    @{
        title = "📚 #12 - Documenta ție API pentru servicii"
        body = "Documenta ție: Generare XML docs și markdown pentru:`n- DataService metode`n- ExcelExportService`n- ViewModels properties`n- Models classes`n\n**Status: ⏳ Backlog**"
        labels = @("documentation", "api")
    },
    @{
        title = "🔧 #13 - Interfa ță Dark Mode"
        body = "Îmbun ătățire: Adaugă tema întunecată cu toggle:`n- Dark color palette`n- Local storage pentru preferință`n- Smooth transition`n\n**Status: ⏳ Backlog**"
        labels = @("enhancement", "ui")
    },
    @{
        title = "🔧 #14 - Localizare: Englez ă + România"
        body = "Îmbun ătățire: Suport pentru 2 limbi:`n- Resource files (.resx)`n- Language selector în UI`n- Persistență alegere`n\n**Status: ⏳ Backlog**"
        labels = @("enhancement", "localization")
    },
    @{
        title = "🔧 #15 - Unit tests pentru DataService"
        body = "Îmbun ătățire: Coverage teste:`n- GetRaportConsum filters`n- GetRaportMaterial aggregation`n- Date validation`n\n**Status: ⏳ Backlog**"
        labels = @("enhancement", "testing")
    },
    @{
        title = "🔧 #16 - Autentificare și roli utilizator"
        body = "Îmbun ătățire: Adaugă sistem de access control:`n- Login/Register`n- Roli: Admin, User, Viewer`n- Permisiuni pe operații`n\n**Status: ⏳ Backlog**"
        labels = @("enhancement", "security")
    },
    @{
        title = "🔧 #17 - Criptare date sensibile"
        body = "Îmbun ătățire: Securitate date:`n- Criptare connection string`n- Criptare sensitive fields`n- HTTPS enforced`n\n**Status: ⏳ Backlog**"
        labels = @("enhancement", "security")
    }
)

Write-Host "🚀 Creare issues pentru GitHub..." -ForegroundColor Green
Write-Host "Totaluri: $($issues.Count) issues"
Write-Host ""

# Instrucțiuni
Write-Host "⚠️  Pentru a rula comanda de creare issues, trebuie:" -ForegroundColor Yellow
Write-Host "1. Installer GitHub CLI: winget install GitHub.cli"
Write-Host "2. Autentificare: gh auth login"
Write-Host "3. Rulează: gh issue create [opțiuni]"
Write-Host ""
Write-Host "🔗 Sau creează manual pe: https://github.com/$owner/$repo/issues"
Write-Host ""

# Afișez comanda pentru fiecare issue
foreach ($issue in $issues) {
    $title = $issue.title
    $body = $issue.body -replace "`"", "`\`""
    $labels = ($issue.labels | ForEach-Object { """$_""" }) -join ","
    
    Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Cyan
    Write-Host $title -ForegroundColor White
    Write-Host ""
    Write-Host "Comandă GH CLI:" -ForegroundColor Yellow
    Write-Host "gh issue create --title ""$title"" --body ""$body"" --label $labels"
    Write-Host ""
}

Write-Host "✅ Gata! Copiază comenzile de mai sus pentru a crea issues pe GitHub." -ForegroundColor Green
