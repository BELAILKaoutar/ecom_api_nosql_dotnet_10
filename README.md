# **Ecom API NoSQL .NET**

**Ecom API NoSQL .NET** est une API RESTful développée avec **.NET 10** et utilisant **MongoDB** comme base de données NoSQL. Ce mini-projet permet de gérer les clients et leurs commandes pour une application e-commerce simplifiée.

---

## **Fonctionnalités principales**

### **Gestion des clients**
- Création, lecture, mise à jour et suppression de clients.
- Stockage d’informations détaillées : **nom**, **prénom**, **email**, **téléphone**, **adresse**.

### **Gestion des commandes**
- Création et lecture des commandes associées à chaque client.
- Suivi du **statut**, des **articles commandés**, du **montant total** et de l’**adresse de livraison**.

### **Architecture et technologies**
- **Backend** : .NET 10
- **Base de données** : MongoDB (NoSQL)
- **Documentation API** : Swagger
- **Architecture** : Services et Repository Pattern pour un code structuré et maintenable

### **Endpoints exposés**
- **Clients** : `/api/Customers`
- **Commandes** : `/api/Orders`

---

## **Objectif du projet**
Ce projet a pour objectif de démontrer :  
- Comment construire une API e-commerce légère avec **.NET 10**.  
- L’intégration entre **.NET** et **MongoDB**.  
- L’utilisation de **Swagger** pour documenter et tester les endpoints.  
- La mise en place d’une architecture propre avec **services** et **repository**.

---

## **Comment tester l’API**
1. Cloner le projet :  
   ```bash
   git clone https://github.com/BELAILKaoutar/ecom_api_nosql_dotnet_10.git
