# ⚡ MVP 

# AI-Powered Subscription & Bill Tracker

## Objective

A web-based application that helps users track subscriptions, detect recurring payments, and receive spending insights using AI-powered categorization.

## MVP Scope

1. **User Authentication & Security:**
    1. Users can sign up and log in securely.
    2. JWT authentication for protected API access.
    3. Role-based access control for future scalability.
2. **Web UI for Transaction Management:**
    1. Users can manually add transactions (amount, date, category, merchant).
    2. Users can view, edit, and delete transactions.
    3. Transactions are stored in a database (PostgreSQL / SQL Server).
3. **AI-Powered Subscription Detection:**
    1. The system automatically detects recurring payments based on patterns.
    2. Transactions are categorized as subscriptions or one-time payments.
    3. Basic rule-based AI for initial classification (later upgrade to ML).
4. **Spending Insights Dashboard:**
    1. Users can view a summary of their subscription spending.
    2. Charts & visualizations: Monthly subscription costs, trends.
    3. Basic filters and sorting for easy navigation.
5. **Payment Alerts & Reminders:**
    1. Upcoming subscription charges are displayed on the dashboard.
    2. Users receive basic notifications/reminders for due payments.

### Question for Catalin:

1. Should it be web-app focused for now? PWA? Mobile app?
A. It should be web-app focused.

---

## **EPICS & USER STORIES**

### 1. **User Authentication & Security**

- *As a user, I want to register with an email and password so I can access my account.*
- *As a user, I want to log in securely so my data is protected.*
- *As a system, I want to issue a JWT after login so that API requests can be authorized.*

### 2. **Transaction Management UI**

- *As a user, I want to add a transaction with amount, date, category, etc...*
- *As a user, I want to view my list of transactions so I can review my spending.*
- *As a user, I want to edit and delete transactions in case of mistakes.*

### 3. **Spending Insights Dashboard**

- *As a user, I want to see a chart of monthly subscription spending so I can track trends.*
- *As a user, I want to filter transactions by month and type to analyse my spending.*

### 4. **Alerts & Reminders**

- *As a user, I want to see upcoming subscription charges on the dashboard.*
- *As a user, I want to receive alerts about due payments so I don’t miss them.*

### 5. **Subscription Detection (AI-powered, Rule-based)**

- *As a user, I want the system to identify recurring transactions so I don’t have to.*
- *As a system, I want to label transactions as subscription or one-time based on rules.*

---

## **EPIC 0: Project Setup & Foundations**

### 🎯 Goal:

Prepare the development environment and technical foundation to build the features.

### **Story 0.1: Setup .NET Solution with DDD Structure**

**As a** developer

**I want** a well-organized solution with separate layers

**So that** the system is maintainable and follows Clean Architecture

### Tasks:

- [ ]  Create a `.NET` solution
- [ ]  Create projects:
    - `SubscriptionTracker.Identiy.UI` (MVC, Blazor, React, Angular)
    - `SubscriptionTracker.Identiy.Api` (Web API)
    - `SubscriptionTracker.Identiy.Core` (Use cases, Domain entities, interfaces)
    - `SubscriptionTracker.Identiy.Infrastructure` (EF Core, JWT, persistence)
    - `SubscriptionTracker.UnitTests` (Unit tests)
- [ ]  Reference projects accordingly (`Api` depends on `Core`, but `Core` should not depend on anything etc.)

---

## **EPIC: User Authentication & Security**

### 🎯 Goal:

Enable users to securely sign up, log in, and access protected resources using JWT-based authentication.

### **User Story 1: User Registration**

**As a** user

**I want** to create an account using my email and password

**So that** I can log in and start using the app

### Tasks:

- [ ]  Create `Register` endpoint (POST `/api/v1/auth/register`)
- [ ]  Validate email and password (min length, format, etc.)
- [ ]  Hash password before storing (e.g., BCrypt)
- [ ]  Save user to the database
- [ ]  Return success message or error (e.g., email already exists)

---

### **User Story 2: User Login**

**As a** user

**I want** to log in using my email and password

**So that** I can access my private dashboard

### Tasks:

- [ ]  Create `Login` endpoint (POST `/api/v1/auth/login`)
- [ ]  Validate credentials against stored hash
- [ ]  Generate JWT token with user ID and claims
- [ ]  Return token to the client

---

### **User Story 3: Secure API with JWT**

**As a** developer

**I want** to protect certain API endpoints

**So that** only authenticated users can access them

### Tasks:

- [ ]  Configure JWT authentication in ASP.NET Core
- [ ]  Add `[Authorize]` attribute to relevant controllers/actions
- [ ]  Return 401 if token is missing or invalid

---

### **User Story 4: Get Current User Info**

**As a** logged-in user

**I want** to see my profile info

**So that** I know I’m logged in as the correct person

### Tasks:

- [ ]  Add `GET /api/v1/users` endpoint
- [ ]  Extract user info from JWT token
- [ ]  Return user ID and email (no password!)

---

### **User Story 5: Prevent Duplicate Emails**

**As a** system

**I want** to prevent users from registering with an existing email

**So that** there is no duplicate account conflict

### Tasks:

- [ ]  Add email uniqueness constraint in DB
- [ ]  Add check in registration logic
- [ ]  Return friendly error if email already exists
