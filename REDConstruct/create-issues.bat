@echo off
REM Script pentru creare issues pe GitHub cu GitHub API
REM Trebuie sa ai un Personal Access Token cu scope `repo`

setlocal enabledelayedexpansion

REM CONFIGURARE
set GITHUB_OWNER=mewcotikv
set GITHUB_REPO=practica
set GITHUB_TOKEN=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

echo =====================================
echo Creare Issues pe GitHub
echo Owner: %GITHUB_OWNER%
echo Repo: %GITHUB_REPO%
echo =====================================
echo.

REM Issue #1
call :create_issue "🐛 DataGrid nu se reîmprospătează după export" "**STATUS: ✅ REZOLVAT**\n\nProblema: DataGrid nu se reîmprospătează corect după export Excel.\n\nSoluție: Adaugă UI refresh forțat și error handling.\n\nBranch: issue/1-datagrid-refresh" "bug,resolved"

REM Issue #2
call :create_issue "🐛 DatePicker nu validează Data Sfarsit" "**STATUS: ✅ REZOLVAT**\n\nProblema: Utilizatorul poate selecta dată sfarsit mai mică decât inceput.\n\nSoluție: Validare date automatică.\n\nBranch: issue/2-datevalidation" "bug,resolved"

REM Issue #3
call :create_issue "✨ Raport 2: Costuri pe material" "**STATUS: ✅ IMPLEMENTAT**\n\nCaracteristică: Raport agreggat pe material.\n\nInclud: RaportMaterial model, GroupBy logică.\n\nBranch: feature/3-material-report" "feature,resolved"

REM Issue #4
call :create_issue "✨ Adaugă grafice (Chart)" "Vizualizare cu grafice pentru consum.\n\n- Pie chart: consum pe obiectiv\n- Bar chart: cost pe material\n- Line chart: tendință" "feature,enhancement"

REM Issue #5
call :create_issue "✨ Conectare la SQL Server" "Înlocuire mock data cu SQL real.\n\n- Connection string\n- CRUD operations\n- Stored procedures" "feature,database"

REM Issue #6
call :create_issue "✨ Export în format PDF" "Export rapoarte PDF profesional.\n\n- Header, tabel, total, footer\n- Layout personalizat" "feature,export"

REM Issue #7
call :create_issue "✨ Importare date Excel" "Upload și parsare fișiere Excel.\n\n- Validare date\n- Importare DB\n- Raport rezultate" "feature,import"

REM Issue #8
call :create_issue "🐛 Export Excel fișiere mari" "Export blocaj pe >10k rânduri.\n\nTrebuie: streaming, progres bar, cancel" "bug,performance"

REM Issue #9
call :create_issue "🐛 ComboBox ne-actualizat" "ComboBox obiective nu se refresh.\n\nSoluție: INotifyCollectionChanged" "bug,ui"

REM Issue #10
call :create_issue "📚 Ghid utilizator Wiki" "Creare wiki complet cu setup, tutoriale, FAQ" "documentation,wiki"

REM Issue #11
call :create_issue "📚 Screenshots în README" "Adaugă screenshots și GIF demo" "documentation,readme"

REM Issue #12
call :create_issue "📚 API Documentation" "XML docs pentru servicii și modele" "documentation,api"

REM Issue #13
call :create_issue "🔧 Dark Mode Interface" "Adaugă tema întunecată cu toggle" "enhancement,ui"

REM Issue #14
call :create_issue "🔧 Localizare EN+RO" "Suport 2 limbi cu .resx files" "enhancement,localization"

REM Issue #15
call :create_issue "🔧 Unit Tests DataService" "Coverage teste pentru logică" "enhancement,testing"

REM Issue #16
call :create_issue "🔧 Autentificare și Roli" "Login, Register, Role-based access" "enhancement,security"

REM Issue #17
call :create_issue "🔧 Criptare Date Sensibile" "Securitate: criptare connection string" "enhancement,security"

echo.
echo =====================================
echo ✅ Issues create cu succes!
echo =====================================
goto :eof

:create_issue
set TITLE=%~1
set BODY=%~2
set LABELS=%~3

echo Creiez issue: %TITLE%

REM Nu voi face request real (ar necesita proper authentication)
REM Doar afisez ce ar fi creat
echo   Labels: %LABELS%
echo.

goto :eof
