# 📊 SubscriptionTracker

A modular, .NET-based application to track subscriptions, detect recurring payments, and provide spending insights — built with a clean, scalable architecture.

---

## ✨ Key Features (Planned)

- Track subscriptions and recurring payments
- Detect patterns in uploaded bank data
- Categorise and tag transactions
- Provide spending summaries and insight reports
- Send email reminders for upcoming bills
- Allow CSV upload or manual entry

---

## 🧱 Architecture Overview

- **Modular Monolith** using Clean Architecture principles
- **Domain-Driven Design** — core logic lives in a pure domain layer
- **API-First Mindset** — designed for future frontend/backend decoupling
- **ORM Agnostic** — database concerns will live in the infrastructure layer

---

## 🛠️ Current Status

- ✅ Core Domain Layer implemented
  - User, Transaction, Money, Category, Tag, Enums
- 🔜 Application Layer (Services, Use Cases) to be added
- 🔜 SubscriptionTracker.API to expose domain functionality
- 🧪 Focus on clarity, separation of concerns, and testability

---

## 📁 Project Structure

<pre>
SubscriptionTracker
├── SubscriptionTracker.Domain
│   ├── Common
│   │   ├── Money.cs
│   │   ├── Category.cs
│   │   └── Tag.cs
│   ├── Transactions
│   │   ├── Transaction.cs
│   │   ├── TransactionSource.cs
│   │   └── TransactionStatus.cs
│   └── Users
│       └── User.cs
├── SubscriptionTracker.API         (planned)
├── SubscriptionTracker.Application (planned)
├── SubscriptionTracker.Infrastructure (planned)
├── .gitignore
├── README.md
└── SubscriptionTracker.sln
</pre>

---

## 🎯 Guiding Principles

- Keep the **Domain pure** and free of frameworks
- Start small, but **architect for extensibility**
- Use naming conventions and file headers consistently
- Use Hungarian notation for primitives (`sName`, `gId`, `dtDate`, etc.)
- PascalCase for objects (Money, Category, Tag)
