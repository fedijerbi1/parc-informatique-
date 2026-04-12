# Parc Informatique

Application ASP.NET Core MVC pour la gestion d'un parc informatique.  
Ce projet permet de gérer les équipements informatiques d'une organisation ainsi que leur affectation aux employés.

## 🚀 Fonctionnalités principales
- Gestion des équipements : ajout, modification, suppression, consultation (CRUD)
- Gestion des employés et affectation du matériel
- Suivi des garanties et des statuts des équipements (En service, En panne, Hors service)
- Historique des affectations avec date de retour et commentaires
- Authentification et gestion des rôles avec **ASP.NET Identity**
- Dashboard (statistiques des équipements)
- Protection contre les attaques courantes (**XSS, CSRF**)

## 🛠️ Technologies utilisées
- **ASP.NET Core MVC 8**
- **Entity Framework Core (Code First)**
- **SQL Server / SQL Server LocalDB**
- **Bootstrap 5** (interface)
- **ASP.NET Identity** (sécurité et gestion des utilisateurs)

## 📁 Structure du projet
```
parc-informatique-/
├── Webapp/
│   ├── Controllers/          # Contrôleurs MVC (Account, Home)
│   ├── Models/               # Modèles de données (Equipment, Employee, Affectation)
│   ├── Views/                # Vues Razor (Account, Home, Shared)
│   ├── Migrations/           # Migrations Entity Framework Core
│   ├── Program.cs            # Point d'entrée et configuration de l'application
│   ├── appsettings.json      # Configuration (chaîne de connexion, etc.)
│   └── wwwroot/              # Fichiers statiques (CSS, JS, images)
└── Webapp.sln                # Solution Visual Studio
```

## ⚙️ Installation et exécution

### Prérequis
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads) ou **SQL Server LocalDB** (inclus avec Visual Studio)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

### Étapes

1. **Cloner le dépôt :**
   ```bash
   git clone https://github.com/fedijerbi1/parc-informatique-.git
   cd parc-informatique-
   ```

2. **Configurer la chaîne de connexion :**  
   Modifier le fichier `Webapp/appsettings.json` si nécessaire :
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WebappDb;Trusted_Connection=true;MultipleActiveResultSets=true"
   }
   ```

3. **Appliquer les migrations (créer la base de données) :**
   ```bash
   cd Webapp
   dotnet ef database update
   ```

4. **Lancer l'application :**
   ```bash
   dotnet run
   ```
   L'application sera accessible à l'adresse indiquée dans le terminal (par défaut `https://localhost:5001`).

## 🗄️ Modèles de données

| Modèle | Description |
|---|---|
| `Equipment` | Équipement informatique (type, marque, modèle, numéro de série, statut, garantie) |
| `Employee` | Employé de l'organisation (nom, prénom, email, téléphone, poste, département) |
| `Affectation` | Liaison entre un employé et un équipement (date d'affectation, date de retour, commentaires) |
| `ApplicationUser` | Utilisateur de l'application (basé sur ASP.NET Identity) |

## 🔐 Sécurité
- Authentification par formulaire via **ASP.NET Identity**
- Verrouillage de compte après 5 tentatives échouées (30 minutes)
- Email unique obligatoire par utilisateur
- Protection **CSRF** intégrée par ASP.NET Core MVC
- Redirection automatique vers la page de connexion pour les routes protégées

## 📄 Licence
Ce projet est à usage académique / personnel.
